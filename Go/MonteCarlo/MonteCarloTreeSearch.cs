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
        public Tree tree = new Tree();
        public const int winScore = 1;
        public int mctsDepth = 0;
        public int maxIterations = MonteCarloMapping.mapMovesOrSearchAnswer ? Int32.MaxValue : 6000;
        public long? elapsedTime;
        public static Random random = new Random();

        public static int mappingDepthToVerify = Convert.ToInt32(ConfigurationSettings.AppSettings["MAPPING_DEPTH_TO_VERIFY"]);
        public static int realTimeDepthToVerify = Convert.ToInt32(ConfigurationSettings.AppSettings["REALTIME_DEPTH_TO_VERIFY"]);

        /// <summary>
        /// Set the visit count required to reach before moving down the tree to the child node.
        /// </summary>
        public virtual int VisitCountMinReq
        {
            get
            {
                return (MonteCarloMapping.mapMovesOrSearchAnswer) ? 5 : 10;
            }
        }

        /// <summary>
        /// Set the answer and end the search immediately.
        /// </summary>
        protected Node answerNode;
        public virtual Node AnswerNode
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

        public MonteCarloTreeSearch(Node rootNode, int mctsDepth = 0)
        {
            tree.Root = rootNode;
            this.mctsDepth = mctsDepth;
        }

        /// <summary>
        /// Start the mcts until answer is found or all nodes are pruned (or max iterations reached).
        /// <see cref="UnitTestProject.PerformanceBenchmarkTest.PerformanceBenchmarkTest_Scenario2dan15" />
        /// <see cref="UnitTestProject.PerformanceBenchmarkTest.PerformanceBenchmarkTest_Scenario_GuanZiPu_A3" />
        /// <see cref="UnitTestProject.PerformanceBenchmarkTest.PerformanceBenchmarkTest_Scenario3dan17" />
        /// </summary>
        public virtual void FindNextMove()
        {
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
                if (ReachedDepthToVerify(promisingNode) || promisingNode.NoPossibleStates)
                    VerifyOnDepthReached(promisingNode);

                //simulate random playout
                SimulateRandomPlayout(promisingNode);

                if (count % 60 == 0)
                    DebugHelper.DebugWriteWithTab("Count: " + count + " | Last moves: " + promisingNode.GetLastMoves(), mctsDepth);

                //break on answer found or no answer
                if (AnswerNode != null || tree.Root.ChildArray.Count == 0)
                    break;

                if (Game.TimeOut(tree.Root.State.Game))
                {
                    DebugHelper.DebugWriteWithTab("Break real time...");
                    break;
                }
            } while (count <= maxIterations);
            PostProcess(watch);
        }

        protected Boolean NodeToExpand(Node node)
        {
            return !node.Expanded && (node.State.Depth > 0 || node.State.SurviveOrKill == SurviveOrKill.Survive);
        }

        protected virtual Node RandomChildNode(Node node)
        {
            int count = node.ChildArray.Count;
            if (count == 0) return node;
            int selectRandom = random.Next(0, count);
            return node.ChildArray[selectRandom];
        }

        protected virtual Boolean ReachedDepthToVerify(Node node)
        {
            return (node.Parent != null && node.Parent.CurrentDepth >= DepthToVerify);
        }

        /// <summary>
        /// Verify on depth reached.
        /// </summary>
        protected virtual void VerifyOnDepthReached(Node promisingNode)
        {
            Node verifyNode = (promisingNode.NoPossibleStates) ? promisingNode : promisingNode.Parent;
            Boolean isWin = VerifyWithExhaustiveSearch(verifyNode);
            if (isWin && AnswerFound(verifyNode))
                return;

            //prune node based on result from exhaustive search
            if (isWin)
                PrunePromisingNode(verifyNode.Parent, verifyNode, isWin);
            else
                PrunePromisingNode(verifyNode, null, isWin);
        }

        /// <summary>
        /// Verify with exhaustive search on reaching specified depth.
        /// </summary>
        public Boolean VerifyWithExhaustiveSearch(Node verifyNode)
        {
            if (verifyNode == null || verifyNode.Parent == null)
                return false;

            Game verifyGame = new Game(verifyNode.State.Game);
            DebugHelper.DebugWriteWithTab("Verifying game: " + verifyGame.Board.GetLastMoves(), mctsDepth);

            //exhaustive search to find definite result
            ConfirmAliveResult verifyResult = verifyGame.MakeExhaustiveSearch();

            if (GameHelper.WinOrLose(verifyNode.State.SurviveOrKill, verifyResult, verifyGame.GameInfo))
            {
                DebugHelper.DebugWriteWithTab("Verified: " + verifyNode.GetLastMoves(), mctsDepth);
                return true;
            }
            else
            {
                DebugHelper.DebugWriteWithTab("Not verified: " + verifyNode.GetLastMoves(), mctsDepth);
                return false;
            }
        }

        /// <summary>
        /// Prune node after verifying with exhaustive search and if result is a win then check if parent node is correct by trying to prune all child nodes.
        /// After all nodes are pruned, move up the level by recursion to check if current path is correct and the answer node will be the first node of the tree.
        /// </summary>
        public Boolean PrunePromisingNode(Node prunedNode, Node verifyNode, Boolean winResult, Boolean recursion = false)
        {
            Node parentNode = prunedNode.Parent;
            if (prunedNode == null || parentNode == null) return false;

            //prune node
            Pruning(prunedNode, verifyNode);

            if (prunedNode.CurrentDepth == this.tree.Root.CurrentDepth + 1)
            {
                //return after hitting the top of tree
                DebugHelper.DebugWriteWithTab("Hit top at level: " + prunedNode.CurrentDepth + " WinResult: " + winResult + " Recursion: " + recursion, mctsDepth);
                return true;
            }

            //recursive search through siblings of pruned node to check if parent node is correct
            if (winResult)
            {
                List<Node> siblingNodes = parentNode.ChildArray.OrderBy(n => n.State.VisitCount).ToList();
                for (int i = siblingNodes.Count - 1; i >= 0; i--)
                {
                    Node siblingNode = siblingNodes[i];

                    //initialize new mcts with sibling node, and loop the loop with each sibling node
                    MonteCarloTreeSearch mcts = new MonteCarloTreeSearch(siblingNode, mctsDepth + 1);
                    mcts.FindNextMove();
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
        protected Boolean CheckAllChildNodesPruned(Node node, Boolean winResult = false)
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
                {
                    String msg = (node.CurrentDepth == 1) ? "Answer move: " + node.State.Game.Board.Move : "Answer move for " + this.tree.Root.GetLastMoves() + ": " + node.State.Game.Board.Move;
                    DebugHelper.DebugWriteWithTab(msg, mctsDepth);
                }
                AnswerNode = node;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Prune node after verification and set pruned node in json map.
        /// </summary>
        protected virtual void Pruning(Node prunedNode, Node verifyNode)
        {
            if (prunedNode == null || prunedNode.Parent == null) return;
            if (MonteCarloMapping.mapMovesOrSearchAnswer && verifyNode != null)
            {
                //set move of pruned node with corresponding answer from verifyNode in PrunedJson of parent node
                Game verifyGame = verifyNode.State.Game;
                Point verifyPoint = (verifyGame.Board.Move != null) ? verifyGame.Board.Move.Value : Game.PassMove;
                JObject firstLevel = JsonHelper.FirstLevelMapping(prunedNode.Parent.PrunedJson, prunedNode.State.Game.Board.Move.Value, verifyPoint);

                //include PrunedJson of verifyNode in PrunedJson of parent node as second level
                if (verifyNode.PrunedJson.Count > 0)
                    JsonHelper.SecondLevelMapping(firstLevel, verifyNode.PrunedJson);
            }

            //remove node from parent
            prunedNode.Parent.ChildArray.Remove(prunedNode);

            //increase score for parent
            BackPropagation(prunedNode.Parent, true, 20 * winScore);

            DebugHelper.DebugWriteWithTab("Pruned node: " + prunedNode.GetLastMoves(), mctsDepth);

        }

        /// <summary>
        /// Selection phase - to select the most promising node from sibling nodes based on UCT value.
        /// </summary>
        protected virtual Node SelectPromisingNode(Node rootNode)
        {
            Node node = rootNode;
            while (node.ChildArray.Count != 0)
            {
                node = UCT.findBestNodeWithUCT(node);
                if (ReachedDepthToVerify(node))
                    break;
            }
            return node;
        }

        /// <summary>
        /// Expansion phase - to expand all possible states as child nodes.
        /// Confirm alive for each possible state to check if game ended with objective reached already.
        /// </summary>
        protected virtual void ExpandNode(Node node)
        {
            NodeExpansion(node);
        }

        protected void NodeExpansion(Node node)
        {
            if (node.Expanded) return;
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
                ConfirmAliveResult confirmAlive = LifeCheck.CheckIfDeadOrAlive(surviveOrKill, g.Board);
                childNode.State.ConfirmAlive = confirmAlive;
                if (confirmAlive != ConfirmAliveResult.Unknown && GameHelper.WinOrLose(surviveOrKill, confirmAlive, g.GameInfo))
                    childNode.State.WinOrLose = true;
            }
            node.Expanded = true;
            if (node.ChildArray.Count == 0) node.NoPossibleStates = true;
        }

        /// <summary>
        /// Back propagation phase - to increase score alternately up the levels for the winner.
        /// </summary>
        private void BackPropagation(Node node, Boolean winOrLose, int incrementScore = winScore)
        {
            while (node != null)
            {
                node.State.IncrementVisit(winScore);

                if (winOrLose)
                    node.State.AddScore(incrementScore);

                if (node.Parent == null)
                    break;
                node = node.Parent;
                winOrLose = !winOrLose;
                if (incrementScore > 1) incrementScore--;
            }
        }

        /// <summary>
        /// Simulation phase - to simulate monte carlo playout by randomization of moves.
        /// </summary>
        public virtual (ConfirmAliveResult, Board) SimulateRandomPlayout(Node node)
        {
            (ConfirmAliveResult result, Board board) = InitializeMonteCarloPlayout(node);
            Boolean winLose = GameHelper.WinOrLose(node.State.SurviveOrKill, result, node.State.Game.GameInfo);
            int incrementScore = (winLose && node.State.SurviveOrKill == SurviveOrKill.Survive) ? 10 : 1;
            BackPropagation(node, winLose, incrementScore);
            return (result, board);
        }


        /// <summary>
        /// Confirmed cases represent possible game state where the game has ended with confirm alive already.
        /// </summary>
        protected Boolean HandleConfirmedCases(Node promisingNode)
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

        public (ConfirmAliveResult, Board) InitializeMonteCarloPlayout(Node node)
        {
            Game g = node.State.Game;
            SurviveOrKill surviveOrKill = node.State.SurviveOrKill;

            int depth = g.GetStartingDepth();
            ConfirmAliveResult confirmAlive = ConfirmAliveResult.Unknown;
            Board board;
            if (surviveOrKill == SurviveOrKill.Kill)
                (confirmAlive, board) = MonteCarloMakeSurvivalMove(depth, g);
            else
                (confirmAlive, board) = MonteCarloMakeKillMove(depth, g);
            return (confirmAlive, board);
        }

        /// <summary>
        /// Make kill move in mcts simulation phase by selecting from all possible moves by randomization.
        /// Include ko moves and set result as KoAlive if ko wins.
        /// </summary>
        private (ConfirmAliveResult, Board) MonteCarloMakeKillMove(int depth, Game g)
        {
            ConfirmAliveResult bestResult = ConfirmAliveResult.Alive;
            Board b = g.Board;
            (ConfirmAliveResult result, List<GameTryMove> tryMoves, GameTryMove koBlockedMove) = g.GetKillMoves();
            if (koBlockedMove != null) tryMoves.Add(koBlockedMove);
            if (result != ConfirmAliveResult.Unknown)
                return (result, tryMoves.First().TryGame.Board);

            //make single random move out of possible moves
            int possibleMoves = tryMoves.Count;
            if (possibleMoves == 0) return (bestResult, b);
            int selectRandom = random.Next(0, possibleMoves);
            GameTryMove gameTryMove = tryMoves[selectRandom];
            Game tryGame = gameTryMove.TryGame;
            if (gameTryMove.MakeMoveResult == MakeMoveResult.Legal)
            {
                (gameTryMove.ConfirmAlive, b) = MonteCarloMakeSurvivalMove(depth - 1, tryGame);
            }
            else if (gameTryMove.MakeMoveResult == MakeMoveResult.KoBlocked)
            {
                (gameTryMove.ConfirmAlive, b) = MonteCarloMakeSurvivalMove(depth, tryGame);
                if (GameHelper.WinOrLose(SurviveOrKill.Kill, result, tryGame.GameInfo))
                    gameTryMove.ConfirmAlive = ConfirmAliveResult.KoAlive;
            }
            bestResult = gameTryMove.ConfirmAlive;
            return (bestResult, b);
        }


        /// <summary>
        /// Make survival move in mcts simulation phase by selecting from all possible moves by randomization.
        /// Include ko moves and set result as KoAlive if ko wins.
        /// </summary>
        private (ConfirmAliveResult, Board) MonteCarloMakeSurvivalMove(int depth, Game g)
        {
            ConfirmAliveResult bestResult = ConfirmAliveResult.Dead;
            Board b = g.Board;
            if (depth <= 0)
                return (ConfirmAliveResult.Dead, b);

            (ConfirmAliveResult result, List<GameTryMove> tryMoves, GameTryMove koBlockedMove) = g.GetSurvivalMoves();
            if (koBlockedMove != null) tryMoves.Add(koBlockedMove);
            if (result != ConfirmAliveResult.Unknown)
                return (result, tryMoves.First().TryGame.Board);
            //make single random move out of possible moves
            int possibleMoves = tryMoves.Count;
            if (possibleMoves == 0) return (bestResult, b);
            int selectRandom = random.Next(0, possibleMoves);
            GameTryMove gameTryMove = tryMoves[selectRandom];
            Game tryGame = gameTryMove.TryGame;

            if (gameTryMove.MakeMoveResult == MakeMoveResult.Legal)
            {
                (gameTryMove.ConfirmAlive, b) = MonteCarloMakeKillMove(depth - 1, tryGame);
                if (gameTryMove.ConfirmAlive == ConfirmAliveResult.Alive && gameTryMove.Move.Equals(Game.PassMove) && tryGame.Board.KoGameCheck == KoCheck.None)
                    gameTryMove.ConfirmAlive = ConfirmAliveResult.BothAlive;
            }
            else if (gameTryMove.MakeMoveResult == MakeMoveResult.KoBlocked)
            {
                (gameTryMove.ConfirmAlive, b) = MonteCarloMakeKillMove(depth, tryGame);
                if (GameHelper.WinOrLose(SurviveOrKill.Survive, result, tryGame.GameInfo))
                    gameTryMove.ConfirmAlive = ConfirmAliveResult.KoAlive;
            }
            bestResult = gameTryMove.ConfirmAlive;
            return (bestResult, b);
        }

        protected virtual void PostProcess(Stopwatch watch)
        {
            watch.Stop();
            long timeTaken = watch.ElapsedMilliseconds;
            elapsedTime = timeTaken;
            if (tree.Root == tree.AbsoluteRoot)
            {
                DebugHelper.DebugWriteWithTab(DebugHelper.PrintTimeTaken(timeTaken), mctsDepth);
                DebugHelper.DebugWriteWithTab("Total time taken (mcts): " + timeTaken + Environment.NewLine + Environment.NewLine, mctsDepth);
            }
            else
                DebugHelper.DebugWriteWithTab("Time taken (mcts): " + timeTaken, mctsDepth);
        }
    }
}
