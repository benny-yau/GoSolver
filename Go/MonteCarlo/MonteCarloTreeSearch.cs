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
        public int maxIterations = MonteCarloMapping.mapMoves ? Int32.MaxValue : 6000;
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
                if (MonteCarloMapping.mapMoves)
                {
                    if (mappingDepthToVerify <= 5)
                        return this.tree.HitDepthToVerify ? 5 : 10;
                    else
                        return 5;
                }
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
                if (MonteCarloMapping.mapMoves)
                {
                    //mapping
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

                Boolean noPossibleStates = false;
                //ensure visit count has reached min requirement
                if (!promisingNode.Expanded && promisingNode.State.Depth > 0 && (promisingNode == tree.Root || promisingNode.State.VisitCount >= VisitCountMinReq))
                {
                    //expand possible states
                    List<State> possibleStates = promisingNode.State.AllPossibleStates;
                    noPossibleStates = !ExpandNode(promisingNode, possibleStates);

                    if (promisingNode.ChildArray.Count > 0)
                    {
                        //select random node or confirmed alive node after expansion
                        Node winNode = promisingNode.ChildArray.Where(m => m.State.WinOrLose).FirstOrDefault();
                        if (winNode != null)
                        {
                            HandleConfirmedCases(winNode);
                            continue;
                        }
                        else
                            promisingNode = RandomChildNode(promisingNode);
                    }
                }
                //all nodes pruned
                if (promisingNode.ChildArray.Count == 0 && (promisingNode.Expanded || promisingNode.State.Depth == 0))
                {
                    if (CheckAllChildNodesPruned(promisingNode, true)) break;
                }

                //verify on depth reached or no possible states to expand
                Boolean pruned = false;
                if (ReachedDepthToVerify(promisingNode) || noPossibleStates)
                {
                    tree.HitDepthToVerify = true;
                    Node verifyNode = noPossibleStates ? promisingNode : promisingNode.Parent;
                    Boolean winOrLose = VerifyOnDepthReached(verifyNode);
                    if (winOrLose && AnswerFound(verifyNode))
                        break;

                    //prune node based on result from exhaustive search
                    pruned = PruneBasedOnWinResult(verifyNode, winOrLose);

                }

                //simulate random playout
                if (!pruned)
                    SimulateRandomPlayout(promisingNode);

                if (Game.debugMode && (count % 60 == 0 || MonteCarloGame.useLeelaZero))
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

        internal virtual Node RandomChildNode(Node node)
        {
            int noOfPossibleMoves = node.ChildArray.Count;
            int selectRandom = GlobalRandom.NextRange(0, noOfPossibleMoves);
            return node.ChildArray[selectRandom];
        }

        internal virtual Boolean ReachedDepthToVerify(Node node)
        {
            return node.Parent != null && node.Parent.CurrentDepth >= DepthToVerify;
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

            if (GameHelper.WinOrLose(verifyNode.State.SurviveOrKill, verifyResult, verifyGame))
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
        private Boolean CheckAllChildNodesPruned(Node node, Boolean winResult)
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
            if (MonteCarloMapping.mapMoves && verifyNode != null)
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
        internal virtual Boolean ExpandNode(Node node, List<State> possibleStates)
        {
            for (int i = 0; i <= possibleStates.Count - 1; i++)
            {
                State state = possibleStates[i];
                Node childNode = new Node(state);
                childNode.Parent = node;
                node.ChildArray.Add(childNode);
                childNode.State.Depth = node.State.Depth - 1;

                //check if game ended by confirm alive
                ConfirmAliveResult confirmAlive = LifeCheck.CheckIfDeadOrAlive(childNode.State.SurviveOrKill, childNode.State.Game);
                childNode.State.ConfirmAlive = confirmAlive;
                if (confirmAlive != ConfirmAliveResult.Unknown)
                {
                    Boolean winOrLose = GameHelper.WinOrLose(childNode.State.SurviveOrKill, confirmAlive, childNode.State.Game);
                    if (winOrLose)
                        childNode.State.WinOrLose = true;
                }
            }
            node.Expanded = true;
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
            ConfirmAliveResult result = InitializeMonteCarloPlayout(node.State.Game, node.State.SurviveOrKill);
            Boolean winLose = (GameHelper.WinOrLose(node.State.SurviveOrKill, result, node.State.Game));
            BackPropagation(node, winLose);
        }


        /// <summary>
        /// Confirmed cases represent possible game state where the game has ended with confirm alive already.
        /// </summary>
        private ConfirmAliveResult HandleConfirmedCases(Node node)
        {
            ConfirmAliveResult confirmAlive = node.State.ConfirmAlive;
            if (confirmAlive == ConfirmAliveResult.Unknown) return confirmAlive;

            DebugHelper.DebugWriteWithTab("Confirm alive at: " + node.GetLastMoves() + " | " + confirmAlive.ToString(), mctsDepth);
            Boolean winOrLose = node.State.WinOrLose;
            if (winOrLose && AnswerFound(node))
                return confirmAlive;
            Node parentNode = node.Parent;
            if (parentNode == null)
                return confirmAlive;
            if (!winOrLose)
                Pruning(node, null);
            else
            {
                if (parentNode.Parent != null)
                    Pruning(parentNode, node);
            }
            return confirmAlive;
        }

        public ConfirmAliveResult InitializeMonteCarloPlayout(Game g, SurviveOrKill surviveOrKill)
        {
            int depth = g.GetStartingDepth();
            ConfirmAliveResult confirmAlive = ConfirmAliveResult.Unknown;
            Game game = new Game(g);
            if (surviveOrKill == SurviveOrKill.Kill)
                confirmAlive = MonteCarloMakeSurvivalMove(depth, g);
            else
                confirmAlive = MonteCarloMakeKillMove(depth, g);

            return confirmAlive;
        }

        /// <summary>
        /// Ko blocked moves added to game try moves in mcts simulation phase. Only in mcts, ko blocked moves run concurrently with other moves, while in exhaustive search, ko blocked moves run only after all other moves have completed.
        /// </summary>
        public static void MonteCarloIncludeKoMoves(Game currentGame, List<GameTryMove> tryMoves, GameTryMove koBlockedMove, SurviveOrKill surviveOrKill)
        {
            KoCheck koCheck = (surviveOrKill == SurviveOrKill.Kill) ? KoCheck.Kill : KoCheck.Survive;
            Boolean includeKoMoves = currentGame.KoGameCheck != koCheck.Opposite();
            if (includeKoMoves && koBlockedMove != null && KoHelper.KoSurvivalEnabled(surviveOrKill, currentGame.GameInfo))
                tryMoves.Add(koBlockedMove);
        }

        /// <summary>
        /// Make kill move in mcts simulation phase by selecting from all possible moves by randomization.
        /// Include ko moves and set result as KoAlive if ko wins.
        /// </summary>
        private ConfirmAliveResult MonteCarloMakeKillMove(int depth, Game m)
        {
            (ConfirmAliveResult result, List<GameTryMove> tryMoves, GameTryMove koBlockedMove) = m.GetKillMoves(m);
            if (result != ConfirmAliveResult.Unknown)
                return result;

            MonteCarloIncludeKoMoves(m, tryMoves, koBlockedMove, SurviveOrKill.Kill);

            for (int i = 0; i <= tryMoves.Count - 1; i++)
            {
                GameTryMove tryMove = tryMoves[i];
                if (tryMove.MakeMoveResult != MakeMoveResult.Legal) continue;
                ConfirmAliveResult confirmAlive = LifeCheck.CheckIfDeadOrAlive(SurviveOrKill.Kill, tryMove.TryGame, true);
                if (confirmAlive == ConfirmAliveResult.Dead)
                    return confirmAlive;
            }
            ConfirmAliveResult bestResult = ConfirmAliveResult.Alive;

            //make single random move out of total possibilities
            int totalPossibilities = tryMoves.Count;
            if (totalPossibilities == 0) return bestResult;
            int selectRandom = GlobalRandom.NextRange(0, totalPossibilities);
            GameTryMove gameTryMove = tryMoves[selectRandom];

            if (gameTryMove.MakeMoveResult == MakeMoveResult.Legal)
            {
                gameTryMove.ConfirmAlive = MonteCarloMakeSurvivalMove(depth - 1, gameTryMove.TryGame);
            }
            else if (gameTryMove.MakeMoveResult == MakeMoveResult.KoBlocked)
            {
                if (KoHelper.KoSurvivalEnabled(SurviveOrKill.Kill, gameTryMove.TryGame.GameInfo))
                {
                    gameTryMove.ConfirmAlive = MonteCarloMakeSurvivalMove(depth, gameTryMove.TryGame);
                    if (gameTryMove.ConfirmAlive.HasFlag(ConfirmAliveResult.Dead))
                        gameTryMove.ConfirmAlive = ConfirmAliveResult.KoAlive;
                }
            }
            bestResult = gameTryMove.ConfirmAlive;
            return bestResult;
        }


        /// <summary>
        /// Make survival move in mcts simulation phase by selecting from all possible moves by randomization.
        /// Include ko moves and set result as KoAlive if ko wins.
        /// </summary>
        private ConfirmAliveResult MonteCarloMakeSurvivalMove(int depth, Game m)
        {
            if (depth <= 0)
                return ConfirmAliveResult.Dead;

            (ConfirmAliveResult result, List<GameTryMove> tryMoves, GameTryMove koBlockedMove) = m.GetSurvivalMoves(m);
            if (result != ConfirmAliveResult.Unknown)
                return result;

            MonteCarloIncludeKoMoves(m, tryMoves, koBlockedMove, SurviveOrKill.Survive);

            for (int i = 0; i <= tryMoves.Count - 1; i++)
            {
                GameTryMove tryMove = tryMoves[i];
                if (tryMove.MakeMoveResult != MakeMoveResult.Legal) continue;
                ConfirmAliveResult confirmAlive = LifeCheck.CheckIfDeadOrAlive(SurviveOrKill.Survive, tryMove.TryGame, true);
                if (confirmAlive == ConfirmAliveResult.Alive)
                    return confirmAlive;
            }
            ConfirmAliveResult bestResult = ConfirmAliveResult.Dead;

            //make single random move out of total possibilities
            int totalPossibilities = tryMoves.Count;
            if (totalPossibilities == 0) return bestResult;
            int selectRandom = GlobalRandom.NextRange(0, totalPossibilities);
            GameTryMove gameTryMove = tryMoves[selectRandom];

            int nextDepth = gameTryMove.Move.Equals(Game.PassMove) ? depth : depth - 1;
            if (gameTryMove.MakeMoveResult == MakeMoveResult.Legal)
            {
                gameTryMove.ConfirmAlive = MonteCarloMakeKillMove(nextDepth, gameTryMove.TryGame);

                if (gameTryMove.ConfirmAlive == ConfirmAliveResult.Alive)
                    bestResult = (gameTryMove.Move.Equals(Game.PassMove)) ? ConfirmAliveResult.BothAlive : ConfirmAliveResult.Alive;
            }
            else if (gameTryMove.MakeMoveResult == MakeMoveResult.KoBlocked)
            {
                if (KoHelper.KoSurvivalEnabled(SurviveOrKill.Survive, gameTryMove.TryGame.GameInfo))
                {
                    gameTryMove.ConfirmAlive = MonteCarloMakeKillMove(depth, gameTryMove.TryGame);
                    if (gameTryMove.ConfirmAlive.HasFlag(ConfirmAliveResult.Alive))
                        gameTryMove.ConfirmAlive = ConfirmAliveResult.KoAlive;
                }
            }
            return bestResult;
        }

    }
}
