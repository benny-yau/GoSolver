using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;

namespace Go
{
    public class MonteCarloTreeSearch
    {
        public Tree tree;
        public const int winScore = 1;
        public int mctsDepth = 0;
        public int maxIterations = MonteCarloMapping.mapMovesOrSearchAnswer ? Int32.MaxValue : 6000;
        public long? elapsedTime;

        public static int mappingDepthToVerify = Convert.ToInt32(ConfigurationSettings.AppSettings["MAPPING_DEPTH_TO_VERIFY"]);
        public static int realTimeDepthToVerify = Convert.ToInt32(ConfigurationSettings.AppSettings["REALTIME_DEPTH_TO_VERIFY"]);

        /// <summary>
        /// Set the visit count required to reach before moving down the tree to the child node. Reduce required count after hitting depth to verify.
        /// </summary>
        int? visitCount;
        public virtual int VisitCountMinReq
        {
            get
            {
                if (visitCount != null) return visitCount.Value;
                if (MonteCarloMapping.mapMovesOrSearchAnswer)
                    return (tree.HitDepthToVerify) ? 3 : 5;
                else
                    return 10;
            }
        }

        /// <summary>
        /// Set the answer and end the search immediately.
        /// </summary>
        Node answerNode;
        public Node AnswerNode
        {
            get
            {
                return answerNode;
            }
            set
            {
                answerNode = value;
            }
        }

        /// <summary>
        /// Depth to start exhaustive search to verify.
        /// </summary>
        public int DepthToVerify
        {
            get
            {
                if (MonteCarloMapping.mapMovesOrSearchAnswer)
                {
                    //mapping or search answer
                    Boolean mapPlayerMove = (tree.Root.State.Game.GameInfo.UserFirst == PlayerOrComputer.Player);
                    return mappingDepthToVerify + 1 + (mapPlayerMove ? 0 : 1);
                }
                else
                {
                    //real-time and verification
                    int addLevels = realTimeDepthToVerify;
                    return tree.AbsoluteRoot.CurrentDepth + addLevels;
                }
            }
        }

        public MonteCarloTreeSearch(int mctsDepth = 0)
        {
            this.mctsDepth = mctsDepth;
        }

        /// <summary>
        /// Start the mcts until answer is found or all nodes are pruned (or max iterations reached).
        /// <see cref="UnitTestProject.PerformanceBenchmarkTest.PerformanceBenchmarkTest_Scenario2dan15" />
        /// <see cref="UnitTestProject.PerformanceBenchmarkTest.PerformanceBenchmarkTest_Scenario_GuanZiPu_A3" />
        /// <see cref="UnitTestProject.PerformanceBenchmarkTest.PerformanceBenchmarkTest_Scenario3dan17" />
        /// </summary>
        public virtual Tree FindNextMove(Node node)
        {
            tree = new Tree();
            tree.Root = node;
            if (Game.debugMode)
                DebugHelper.DebugWriteWithTab("Start of mcts: " + tree.Root.GetLastMoves(), mctsDepth);
            Stopwatch watch = Stopwatch.StartNew();
            int count = 0;
            do
            {
                count++;
                //select best node
                Node promisingNode = SelectPromisingNode(tree.Root);

                //ensure visit count has reached min requirement
                if (NodeToExpand(promisingNode) && (promisingNode == tree.Root || promisingNode.State.VisitCount >= VisitCountMinReq))
                {
                    //expand possible states
                    ExpandNode(promisingNode);
                    if (HandleConfirmedCases(promisingNode)) continue;
                    promisingNode = RandomChildNode(promisingNode);
                }
                //all nodes pruned
                if (promisingNode.ChildArray.Count == 0 && !NodeToExpand(promisingNode))
                {
                    if (promisingNode.CurrentDepth == this.tree.Root.CurrentDepth) break;
                    if (CheckAllChildNodesPruned(promisingNode)) break;
                }

                //verify on depth reached or no possible states to expand
                if (ReachedDepthToVerify(promisingNode))
                {
                    tree.HitDepthToVerify = true;
                    Node verifyNode = (promisingNode.NoPossibleStates) ? promisingNode : promisingNode.Parent;
                    Boolean winOrLose = VerifyOnDepthReached(verifyNode);
                    if (winOrLose && AnswerFound(verifyNode))
                        break;

                    //prune node based on result from exhaustive search
                    Boolean pruned = PruneBasedOnWinResult(verifyNode, winOrLose);
                    if (pruned) continue;
                }

                //simulate random playout
                SimulateRandomPlayout(promisingNode);

                if (Game.debugMode && count % 60 == 0)
                    DebugHelper.DebugWriteWithTab("Count: " + count + " | Depth: " + promisingNode.CurrentDepth + " | Last moves: " + promisingNode.GetLastMoves(), mctsDepth);

                //break on answer found or no answer
                if (AnswerNode != null || tree.Root.ChildArray.Count == 0)
                    break;

                if (Game.TimeOut(node.State.Game))
                {
                    if (Game.debugMode) Debug.WriteLine("Break real time...");
                    break;
                }
            } while (count <= maxIterations);
            watch.Stop();
            if (Game.debugMode)
            {
                DebugHelper.DebugWriteWithTab("End of mcts: " + tree.Root.GetLastMoves(), mctsDepth);
                long timeTaken = watch.ElapsedMilliseconds;
                elapsedTime = timeTaken;
                if (tree.Root == tree.AbsoluteRoot)
                {
                    DebugHelper.DebugWriteWithTab("Root: " + tree.AbsoluteRoot.GetLastMoves(), mctsDepth);
                    DebugHelper.DebugWriteWithTab(DebugHelper.PrintTimeTaken(timeTaken), mctsDepth);
                    DebugHelper.DebugWriteWithTab("Total time taken (mcts): " + timeTaken + Environment.NewLine + Environment.NewLine, mctsDepth);
                }
                else
                    DebugHelper.DebugWriteWithTab("Time taken (mcts): " + timeTaken, mctsDepth);
            }
            return tree;
        }

        private Boolean NodeToExpand(Node node)
        {
            return !node.Expanded && node.State.Depth > 0;
        }

        internal virtual Node RandomChildNode(Node node)
        {
            int count = node.ChildArray.Count;
            if (count == 0) return node;
            int selectRandom = GlobalRandom.NextRange(0, count);
            return node.ChildArray[selectRandom];
        }

        internal virtual Boolean ReachedDepthToVerify(Node node)
        {
            return (node.Parent != null && node.Parent.CurrentDepth >= DepthToVerify) || node.NoPossibleStates;
        }

        /// <summary>
        /// To prune node based on exhaustive search result or confirm alive result.
        /// </summary>
        private Boolean PruneBasedOnWinResult(Node verifyNode, Boolean winOrLose)
        {
            Boolean pruned = false;
            if (winOrLose)
                pruned = PrunePromisingNode(verifyNode.Parent, verifyNode, winOrLose);
            else
                pruned = PrunePromisingNode(verifyNode, null, winOrLose);
            return pruned;
        }

        /// <summary>
        /// Verify with exhaustive search on reaching specified depth.
        /// </summary>
        public Boolean VerifyOnDepthReached(Node verifyNode)
        {
            if (verifyNode == null || verifyNode.Parent == null)
                return false;

            Game verifyGame = new Game(verifyNode.State.Game);
            if (Game.debugMode)
                DebugHelper.DebugWriteWithTab("Verifying game: " + verifyGame.Board.GetLastMoves(), mctsDepth);

            //exhaustive search to find definite result
            ConfirmAliveResult verifyResult = verifyGame.MakeExhaustiveSearch();

            if (GameHelper.WinOrLose(verifyNode.State.SurviveOrKill, verifyResult, verifyGame.GameInfo))
            {
                if (Game.debugMode)
                    DebugHelper.DebugWriteWithTab("Verified: " + verifyNode.GetLastMoves(), mctsDepth);
                return true;
            }
            else
            {
                if (Game.debugMode)
                    DebugHelper.DebugWriteWithTab("Not verified: " + verifyNode.GetLastMoves(), mctsDepth);
                return false;
            }
        }

        /// <summary>
        /// Prune node after verifying with exhaustive search and if result is a win then check if parent node is correct by trying to prune all child nodes.
        /// After all nodes are pruned, move up the level by recursion to check if current path is correct and the answer node will be the first node of the tree.
        /// </summary>
        public Boolean PrunePromisingNode(Node pruneNode, Node verifyNode, Boolean winResult, Boolean recursion = false)
        {
            Node parentNode = pruneNode.Parent;
            if (pruneNode == null || parentNode == null) return false;

            //prune node
            Pruning(pruneNode, verifyNode);

            if (pruneNode.CurrentDepth == this.tree.Root.CurrentDepth + 1)
            {
                //return after hitting the top of tree
                DebugHelper.DebugWriteWithTab("Hit top at level: " + pruneNode.CurrentDepth + " WinResult: " + winResult + " Recursion: " + recursion, mctsDepth);
                return true;
            }

            //recursive search through siblings of pruned node to check if parent node is correct
            if (winResult)
            {
                List<Node> siblingNodes = parentNode.ChildArray.OrderBy(m => UCT.uctValue(m)).ToList();
                for (int i = siblingNodes.Count - 1; i >= 0; i--)
                {
                    Node siblingNode = siblingNodes[i];

                    //initialize new mcts with sibling node, and loop the loop with each sibling node
                    MonteCarloTreeSearch mcts = MonteCarloGame.InitializeMonteCarloComputerMove(siblingNode.State.Game, siblingNode, mctsDepth + 1);
                    Boolean winOrLose = (mcts.AnswerNode == null);
                    if (!winOrLose)
                    {
                        //game lost - prune sibling node (default pathway if parent node is correct)
                        DebugHelper.DebugWriteWithTab("Sibling node pruned.", mctsDepth);
                        Pruning(siblingNode, mcts.AnswerNode);
                        //continue to prune all siblings to confirm answer
                    }
                    else
                    {
                        //game won - answer found or prune parent node
                        if (AnswerFound(siblingNode))
                            return true;
                        if (parentNode.Parent != null)
                        {
                            DebugHelper.DebugWriteWithTab("Parent node pruned.", mctsDepth);
                            Pruning(parentNode, siblingNode);
                            return true;
                        }
                    }
                }
            }

            CheckAllChildNodesPruned(parentNode, winResult);
            return true;
        }

        /// <summary>
        /// Check if all nodes have been pruned. Check if answer found else continue to prune parent of current node.
        /// </summary>
        private Boolean CheckAllChildNodesPruned(Node node, Boolean winResult = false)
        {
            if (node.ChildArray.Count > 0) return false;
            DebugHelper.DebugWriteWithTab("All child nodes pruned.", mctsDepth);
            if (AnswerFound(node))
                return true;

            //if parent is not null then prune parent of win node moving up the tree by recursion
            if (node.Parent != null)
            {
                DebugHelper.DebugWriteWithTab("MCTS recursion up level. WinResult: " + winResult, mctsDepth);
                PrunePromisingNode(node.Parent, node, winResult, true);
            }
            return false;
        }

        /// <summary>
        /// Answer found when current depth of node is count of last moves of root node plus one. Set as AnswerNode and return true.
        /// </summary>
        private Boolean AnswerFound(Node node)
        {
            if (node.CurrentDepth == 1 || node.CurrentDepth == this.tree.Root.CurrentDepth + 1)
            {
                //top node reached and answer found
                if (Game.debugMode)
                    DebugHelper.DebugWriteWithTab("Answer move: " + node.State.Game.Board.Move, mctsDepth);
                AnswerNode = node;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Prune node after verification and set pruned node in json map.
        /// </summary>
        private void Pruning(Node pruneNode, Node verifyNode)
        {
            if (pruneNode == null || pruneNode.Parent == null) return;
            if (MonteCarloMapping.mapMovesOrSearchAnswer && verifyNode != null)
            {
                //set move of pruned node with corresponding answer from verifyNode in PrunedJson of parent node
                Game verifyGame = verifyNode.State.Game;
                Point verifyPoint = (verifyGame.Board.Move != null) ? verifyGame.Board.Move.Value : Game.PassMove;
                JObject firstLevel = JsonHelper.FirstLevelMapping(pruneNode.Parent.PrunedJson, pruneNode.State.Game.Board.Move.Value, verifyPoint);

                //include PrunedJson of verifyNode in PrunedJson of parent node as second level
                if (verifyNode.PrunedJson.Count > 0)
                    JsonHelper.SecondLevelMapping(firstLevel, verifyNode.PrunedJson);
            }

            //remove node from parent
            pruneNode.Parent.ChildArray.Remove(pruneNode);

            //increase score for parent
            BackPropagation(pruneNode.Parent, true, 20 * winScore);

            if (Game.debugMode)
                DebugHelper.DebugWriteWithTab("Pruned node: " + pruneNode.GetLastMoves(), mctsDepth);

        }

        /// <summary>
        /// Selection phase - to select the most promising node from sibling nodes based on UCT value.
        /// </summary>
        internal virtual Node SelectPromisingNode(Node rootNode)
        {
            Node node = rootNode;
            while (node.ChildArray.Count != 0)
            {
                node = UCT.findBestNodeWithUCT(node);
            }
            return node;
        }

        /// <summary>
        /// Expansion phase - to expand all possible states as child nodes.
        /// Confirm alive for each possible state to check if game ended with objective reached already.
        /// </summary>
        internal virtual Boolean ExpandNode(Node node)
        {
            List<State> possibleStates = node.State.AllPossibleStates;
            for (int i = 0; i <= possibleStates.Count - 1; i++)
            {
                State state = possibleStates[i];
                Node childNode = new Node(state);
                childNode.Parent = node;
                node.ChildArray.Add(childNode);
                childNode.State.Depth = node.State.Depth - 1;

                //check if game ended by confirm alive
                SurviveOrKill surviveOrKill = childNode.State.SurviveOrKill;
                Game g = childNode.State.Game;
                ConfirmAliveResult confirmAlive = LifeCheck.CheckIfDeadOrAlive(surviveOrKill, g);
                childNode.State.ConfirmAlive = confirmAlive;
                if (confirmAlive != ConfirmAliveResult.Unknown && GameHelper.WinOrLose(surviveOrKill, confirmAlive, g.GameInfo))
                    childNode.State.WinOrLose = true;
            }
            node.Expanded = true;
            if (node.ChildArray.Count == 0) node.NoPossibleStates = true;
            return (node.ChildArray.Count > 0);
        }

        /// <summary>
        /// Back propagation phase - to increase score alternately up the levels for the winner.
        /// </summary>
        private void BackPropagation(Node node, Boolean winOrLose, int incrementScore = winScore)
        {
            while (node != null)
            {
                node.State.IncrementVisit(Convert.ToInt32(winScore));

                if (winOrLose)
                    node.State.AddScore(incrementScore);

                if (node.Parent == null)
                    break;
                node = node.Parent;
                winOrLose = !winOrLose;
            }
        }

        /// <summary>
        /// Simulation phase - to simulate monte carlo playout by randomization of moves.
        /// </summary>
        public virtual void SimulateRandomPlayout(Node node)
        {
            //Monte Carlo random playout
            ConfirmAliveResult result = InitializeMonteCarloPlayout(node);
            Boolean winLose = GameHelper.WinOrLose(node.State.SurviveOrKill, result, node.State.Game.GameInfo);
            BackPropagation(node, winLose);
        }


        /// <summary>
        /// Confirmed cases represent possible game state where the game has ended with confirm alive already.
        /// </summary>
        private Boolean HandleConfirmedCases(Node promisingNode)
        {
            Node node = promisingNode.ChildArray.FirstOrDefault(m => m.State.WinOrLose);
            if (node == null) return false;
            ConfirmAliveResult confirmAlive = node.State.ConfirmAlive;
            DebugHelper.DebugWriteWithTab("Confirm alive at: " + node.GetLastMoves() + " | " + confirmAlive.ToString(), mctsDepth);
            if (AnswerFound(node))
                return true;
            Pruning(node.Parent, node);
            return true;
        }

        public ConfirmAliveResult InitializeMonteCarloPlayout(Node node)
        {
            Game g = node.State.Game;
            SurviveOrKill surviveOrKill = node.State.SurviveOrKill;

            int depth = g.GetStartingDepth();
            ConfirmAliveResult confirmAlive = ConfirmAliveResult.Unknown;
            if (surviveOrKill == SurviveOrKill.Kill)
                confirmAlive = MonteCarloMakeSurvivalMove(depth, g);
            else
                confirmAlive = MonteCarloMakeKillMove(depth, g);
            return confirmAlive;
        }

        /// <summary>
        /// Make kill move in mcts simulation phase by selecting from all possible moves by randomization.
        /// Include ko moves and set result as KoAlive if ko wins.
        /// </summary>
        private ConfirmAliveResult MonteCarloMakeKillMove(int depth, Game g)
        {
            (ConfirmAliveResult result, List<GameTryMove> tryMoves, GameTryMove koBlockedMove) = g.GetKillMoves();
            if (koBlockedMove != null) tryMoves.Add(koBlockedMove);
            if (result != ConfirmAliveResult.Unknown)
                return result;

            ConfirmAliveResult bestResult = ConfirmAliveResult.Alive;
            //make single random move out of total possibilities
            int totalPossibilities = tryMoves.Count;
            if (totalPossibilities == 0) return bestResult;
            int selectRandom = GlobalRandom.NextRange(0, totalPossibilities);
            GameTryMove gameTryMove = tryMoves[selectRandom];
            Game tryGame = gameTryMove.TryGame;
            if (gameTryMove.MakeMoveResult == MakeMoveResult.Legal)
            {
                gameTryMove.ConfirmAlive = MonteCarloMakeSurvivalMove(depth - 1, tryGame);
            }
            else if (gameTryMove.MakeMoveResult == MakeMoveResult.KoBlocked)
            {
                gameTryMove.ConfirmAlive = MonteCarloMakeSurvivalMove(depth, tryGame);
                if (GameHelper.WinOrLose(SurviveOrKill.Kill, result, tryGame.GameInfo))
                    gameTryMove.ConfirmAlive = ConfirmAliveResult.KoAlive;
            }
            bestResult = gameTryMove.ConfirmAlive;
            return bestResult;
        }


        /// <summary>
        /// Make survival move in mcts simulation phase by selecting from all possible moves by randomization.
        /// Include ko moves and set result as KoAlive if ko wins.
        /// </summary>
        private ConfirmAliveResult MonteCarloMakeSurvivalMove(int depth, Game g)
        {
            if (depth <= 0)
                return ConfirmAliveResult.Dead;

            (ConfirmAliveResult result, List<GameTryMove> tryMoves, GameTryMove koBlockedMove) = g.GetSurvivalMoves();
            if (koBlockedMove != null) tryMoves.Add(koBlockedMove);
            if (result != ConfirmAliveResult.Unknown)
                return result;
            ConfirmAliveResult bestResult = ConfirmAliveResult.Dead;
            //make single random move out of total possibilities
            int totalPossibilities = tryMoves.Count;
            if (totalPossibilities == 0) return bestResult;
            int selectRandom = GlobalRandom.NextRange(0, totalPossibilities);
            GameTryMove gameTryMove = tryMoves[selectRandom];
            Game tryGame = gameTryMove.TryGame;

            int nextDepth = gameTryMove.Move.Equals(Game.PassMove) ? depth : depth - 1;
            if (gameTryMove.MakeMoveResult == MakeMoveResult.Legal)
            {
                gameTryMove.ConfirmAlive = MonteCarloMakeKillMove(nextDepth, tryGame);
                if (gameTryMove.ConfirmAlive == ConfirmAliveResult.Alive && gameTryMove.Move.Equals(Game.PassMove) && gameTryMove.TryGame.KoGameCheck == KoCheck.None)
                    gameTryMove.ConfirmAlive = ConfirmAliveResult.BothAlive;
            }
            else if (gameTryMove.MakeMoveResult == MakeMoveResult.KoBlocked)
            {
                gameTryMove.ConfirmAlive = MonteCarloMakeKillMove(depth, tryGame);
                if (GameHelper.WinOrLose(SurviveOrKill.Survive, result, tryGame.GameInfo))
                    gameTryMove.ConfirmAlive = ConfirmAliveResult.KoAlive;
            }
            bestResult = gameTryMove.ConfirmAlive;
            return bestResult;
        }

    }
}
