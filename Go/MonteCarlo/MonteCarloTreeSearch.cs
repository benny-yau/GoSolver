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
        public int mctsDepth = 0;
        public int maxIterations = Game.MapMovesOrSearchAnswer ? Int32.MaxValue : 6000;
        public long? elapsedTime;
        public static Random random = new Random();
        public static int searchDepthToVerify = Convert.ToInt32(ConfigurationSettings.AppSettings["MAPPING_DEPTH_TO_VERIFY"]);
        public static int realTimeDepthToVerify = Convert.ToInt32(ConfigurationSettings.AppSettings["REALTIME_DEPTH_TO_VERIFY"]);

        /// <summary>
        /// Visit count minimum requirement.
        /// </summary>
        public int VisitCountMinReq
        {
            get
            {
                return (Game.MapMovesOrSearchAnswer) ? 3 : 10;
            }
        }

        /// <summary>
        /// Answer node.
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
        /// Depth to verify.
        /// </summary>
        public int DepthToVerify
        {
            get
            {
                //mapping or search answer
                if (Game.MapMovesOrSearchAnswer)
                {
                    Boolean mapPlayerMove = (tree.GameInfo.UserFirst == PlayerOrComputer.Player);
                    return searchDepthToVerify + 1 + (mapPlayerMove ? 0 : 1);
                }
                //real-time and verification
                return tree.AbsoluteRoot.CurrentDepth + realTimeDepthToVerify;
            }
        }

        /// <summary>
        /// Monte carlo tree search.
        /// </summary>
        public MonteCarloTreeSearch(Node rootNode, int mctsDepth = 0)
        {
            tree.Root = rootNode;
            this.mctsDepth = mctsDepth;
        }

        /// <summary>
        /// Find next move. Start the mcts until answer is found or all nodes are pruned.
        /// <see cref="UnitTestProject.PerformanceBenchmarkTest.PerformanceBenchmarkTest_Scenario2dan15" />
        /// <see cref="UnitTestProject.PerformanceBenchmarkTest.PerformanceBenchmarkTest_Scenario_GuanZiPu_A3" />
        /// <see cref="UnitTestProject.PerformanceBenchmarkTest.PerformanceBenchmarkTest_Scenario3dan17" />
        /// </summary>
        public void FindNextMove()
        {
            DebugHelper.WriteLine("Start of mcts: " + tree.Root.GetLastMoves(), mctsDepth);
            Stopwatch watch = Stopwatch.StartNew();
            int count = 0;
            do
            {
                count++;
                //select best node
                Node promisingNode = SelectPromisingNode(tree.Root);

                //ensure visit count has reached min requirement
                if (!promisingNode.Expanded && (promisingNode == tree.Root || promisingNode.State.VisitCount >= VisitCountMinReq))
                {
                    //expand possible states
                    ExpandNode(promisingNode);
                    if (HandleConfirmedCases(promisingNode)) continue;
                    promisingNode = RandomChildNode(promisingNode);
                }
                //all nodes pruned
                if (promisingNode.ChildArray.Count == 0 && promisingNode.Expanded)
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
                    DebugHelper.WriteLine("Count: " + count + " | Last moves: " + promisingNode.GetLastMoves(), mctsDepth);

                //break on answer found or no answer
                if (AnswerNode != null || tree.Root.ChildArray.Count == 0)
                    break;
            } while (count <= maxIterations);
            CheckTimeTaken(watch);
        }

        /// <summary>
        /// Random child node.
        /// </summary>
        private Node RandomChildNode(Node node)
        {
            int count = node.ChildArray.Count;
            if (count == 0) return node;
            int selectRandom = random.Next(0, count);
            return node.ChildArray[selectRandom];
        }

        /// <summary>
        /// Reached depth to verify.
        /// </summary>
        private Boolean ReachedDepthToVerify(Node node)
        {
            return (node.Parent != null && node.Parent.CurrentDepth >= DepthToVerify);
        }

        /// <summary>
        /// Verify on depth reached.
        /// </summary>
        private void VerifyOnDepthReached(Node promisingNode)
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
        /// Verify with exhaustive search.
        /// </summary>
        private Boolean VerifyWithExhaustiveSearch(Node verifyNode)
        {
            if (verifyNode == null || verifyNode.Parent == null)
                return false;

            Game verifyGame = new Game(verifyNode.State.Game);
            DebugHelper.WriteLine("Verifying game: " + verifyGame.Board.GetLastMoves(), mctsDepth);

            //exhaustive search
            int depth = tree.GameInfo.SearchDepth - verifyNode.State.Depth;
            ConfirmAliveResult verifyResult = verifyGame.MakeExhaustiveSearch(depth);

            if (GameHelper.WinOrLose(verifyNode.State.SurviveOrKill, verifyResult, tree.GameInfo))
            {
                DebugHelper.WriteLine("Verified: " + verifyNode.GetLastMoves(), mctsDepth);
                return true;
            }
            else
            {
                DebugHelper.WriteLine("Not verified: " + verifyNode.GetLastMoves(), mctsDepth);
                return false;
            }
        }

        /// <summary>
        /// Prune promising node, after verifying with exhaustive search. If result is a win then check if parent node is correct by trying to prune all child nodes.
        /// After all nodes are pruned, move up the level by recursion to check if current path is correct and the answer node will be the first node of the tree.
        /// </summary>
        private Boolean PrunePromisingNode(Node prunedNode, Node verifyNode, Boolean winResult)
        {
            Node parentNode = prunedNode.Parent;
            if (prunedNode == null || parentNode == null) return false;

            //prune node
            Pruning(prunedNode, verifyNode);

            if (prunedNode.CurrentDepth == this.tree.Root.CurrentDepth + 1)
            {
                //return after hitting the top of tree
                DebugHelper.WriteLine("Hit top at level: " + prunedNode.CurrentDepth, mctsDepth);
                return true;
            }

            //recursive search through siblings of pruned node to check if parent node is correct
            if (winResult)
            {
                List<Node> siblingNodes = parentNode.ChildArray.OrderBy(n => n.State.VisitCount).ToList();
                for (int i = siblingNodes.Count - 1; i >= 0; i--)
                {
                    Node siblingNode = siblingNodes[i];

                    //initialize new mcts with sibling node
                    MonteCarloTreeSearch mcts = new MonteCarloTreeSearch(siblingNode, mctsDepth + 1);
                    mcts.FindNextMove();
                    Boolean winOrLose = (mcts.AnswerNode == null);
                    if (!winOrLose)
                    {
                        //prune sibling node (default pathway if parent node is correct)
                        DebugHelper.WriteLine("Sibling node pruned.", mctsDepth);
                        Pruning(siblingNode, mcts.AnswerNode);
                        //continue to prune all siblings to confirm answer
                    }
                    else
                    {
                        //answer found or prune parent node
                        if (AnswerFound(siblingNode))
                            return true;
                        if (parentNode.Parent != null)
                        {
                            DebugHelper.WriteLine("Parent node pruned.", mctsDepth);
                            Pruning(parentNode, siblingNode);
                            return true;
                        }
                    }
                }
            }

            //check all child nodes pruned
            CheckAllChildNodesPruned(parentNode);
            return true;
        }

        /// <summary>
        /// Check all child nodes pruned.
        /// </summary>
        private Boolean CheckAllChildNodesPruned(Node node)
        {
            if (node.ChildArray.Count > 0) return false;
            DebugHelper.WriteLine("All child nodes pruned.", mctsDepth);

            //check if answer found
            if (AnswerFound(node))
                return true;

            //prune parent node
            if (node.Parent == null) return false;
            Pruning(node.Parent, node);
            return false;
        }

        /// <summary>
        /// Answer found.
        /// </summary>
        private Boolean AnswerFound(Node node)
        {
            if (node.CurrentDepth == 1 || node.CurrentDepth == this.tree.Root.CurrentDepth + 1)
            {
                if (Game.debugMode)
                {
                    String msg = (node.CurrentDepth == 1) ? "Answer move: " + node.State.Game.Board.Move : "Answer move for " + this.tree.Root.GetLastMoves() + ": " + node.State.Game.Board.Move;
                    DebugHelper.WriteLine(msg, mctsDepth);
                }
                AnswerNode = node;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Pruning. Set pruned node in json map.
        /// </summary>
        private void Pruning(Node prunedNode, Node verifyNode)
        {
            if (prunedNode == null || prunedNode.Parent == null) return;
            if (Game.MapMovesOrSearchAnswer && verifyNode != null)
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
            int incrementScore = 20;
            if (prunedNode.CurrentDepth > 0 && prunedNode.CurrentDepth <= 4)
                incrementScore += (4 / prunedNode.CurrentDepth) * 5;
            BackPropagation(prunedNode.Parent, true, incrementScore);

            DebugHelper.WriteLine("Pruned node: " + prunedNode.GetLastMoves(), mctsDepth);

        }

        /// <summary>
        /// Selection phase - select the most promising node from sibling nodes based on UCT value.
        /// </summary>
        private Node SelectPromisingNode(Node rootNode)
        {
            Node node = rootNode;
            while (node.ChildArray.Count != 0)
            {
                node = UCT.FindBestNodeWithUCT(node);
                if (ReachedDepthToVerify(node))
                    break;
            }
            return node;
        }

        /// <summary>
        /// Expansion phase - expand all possible states and check confirm alive.
        /// </summary>
        private void ExpandNode(Node node)
        {
            if (node.Expanded) return;
            List<State> possibleStates = node.State.AllPossibleStates;
            for (int i = 0; i <= possibleStates.Count - 1; i++)
            {
                State state = possibleStates[i];
                Node childNode = new Node(state);
                childNode.Parent = node;
                node.ChildArray.Add(childNode);
                childNode.State.Depth = node.State.Depth + 1;

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
        /// Back propagation phase - increase score alternately up the levels for the winner.
        /// </summary>
        private void BackPropagation(Node node, Boolean winOrLose, int incrementScore)
        {
            while (node != null)
            {
                node.State.IncrementVisit(1);

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
        private (ConfirmAliveResult, Board) SimulateRandomPlayout(Node node)
        {
            (ConfirmAliveResult result, Board board) = InitializeMonteCarloPlayout(node);
            Boolean winLose = GameHelper.WinOrLose(node.State.SurviveOrKill, result, tree.GameInfo);
            int incrementScore = (winLose && node.State.SurviveOrKill == SurviveOrKill.Survive) ? 12 : 6;
            BackPropagation(node, winLose, incrementScore);
            return (result, board);
        }

        /// <summary>
        /// Handle confirmed cases, for confirm alive.
        /// </summary>
        private Boolean HandleConfirmedCases(Node promisingNode)
        {
            Node node = promisingNode.ChildArray.FirstOrDefault(m => m.State.WinOrLose);
            if (node == null) return false;
            ConfirmAliveResult confirmAlive = node.State.ConfirmAlive;
            DebugHelper.WriteLine("Confirm alive at: " + node.GetLastMoves() + " | " + confirmAlive.ToString(), mctsDepth);
            if (AnswerFound(node))
                return true;
            Pruning(node.Parent, node);
            return true;
        }

        /// <summary>
        /// Initialize monte carlo playout.
        /// </summary>
        private (ConfirmAliveResult, Board) InitializeMonteCarloPlayout(Node node)
        {
            Game g = node.State.Game;
            SurviveOrKill surviveOrKill = node.State.SurviveOrKill;

            int depth = g.GameInfo.SearchDepth - node.State.Depth;
            ConfirmAliveResult confirmAlive = ConfirmAliveResult.Unknown;
            Board board;
            if (surviveOrKill == SurviveOrKill.Kill)
                (confirmAlive, board) = MonteCarloMakeSurvivalMove(depth, g);
            else
                (confirmAlive, board) = MonteCarloMakeKillMove(depth, g);
            return (confirmAlive, board);
        }

        /// <summary>
        /// Monte carlo make kill move. Select random move from all possible moves. Include ko moves.
        /// </summary>
        private (ConfirmAliveResult, Board) MonteCarloMakeKillMove(int depth, Game g)
        {
            ConfirmAliveResult bestResult = ConfirmAliveResult.Alive;
            Board b = g.Board;
            g.isMonteCarloPlayout = true;
            (ConfirmAliveResult result, List<GameTryMove> tryMoves, GameTryMove koBlockedMove) = g.GetKillMoves();
            g.isMonteCarloPlayout = false;
            if (koBlockedMove != null) tryMoves.Add(koBlockedMove);
            if (result != ConfirmAliveResult.Unknown)
                return (result, tryMoves.First().TryGame.Board);

            //make single random move out of possible moves
            int possibleMoves = tryMoves.Count;
            if (possibleMoves == 0) return (bestResult, b);
            int selectRandom = random.Next(0, possibleMoves);
            GameTryMove tryMove = tryMoves[selectRandom];
            Game tryGame = tryMove.TryGame;
            if (tryMove.MakeMoveResult == MakeMoveResult.Legal)
            {
                (tryMove.ConfirmAlive, b) = MonteCarloMakeSurvivalMove(depth - 1, tryGame);
            }
            else if (tryMove.MakeMoveResult == MakeMoveResult.KoBlocked)
            {
                (tryMove.ConfirmAlive, b) = MonteCarloMakeSurvivalMove(depth, tryGame);
                if (GameHelper.WinOrLose(SurviveOrKill.Kill, result, tryGame.GameInfo))
                    tryMove.ConfirmAlive = ConfirmAliveResult.KoAlive;
            }
            bestResult = tryMove.ConfirmAlive;
            return (bestResult, b);
        }

        /// <summary>
        /// Monte carlo make survival move. Select random move from all possible moves. Include ko moves.
        /// </summary>
        private (ConfirmAliveResult, Board) MonteCarloMakeSurvivalMove(int depth, Game g)
        {
            ConfirmAliveResult bestResult = ConfirmAliveResult.Dead;
            Board b = g.Board;
            if (depth <= 0)
                return (ConfirmAliveResult.Dead, b);

            g.isMonteCarloPlayout = true;
            (ConfirmAliveResult result, List<GameTryMove> tryMoves, GameTryMove koBlockedMove) = g.GetSurvivalMoves();
            g.isMonteCarloPlayout = false;
            if (koBlockedMove != null) tryMoves.Add(koBlockedMove);
            if (result != ConfirmAliveResult.Unknown)
                return (result, tryMoves.First().TryGame.Board);
            //make single random move out of possible moves
            int possibleMoves = tryMoves.Count;
            if (possibleMoves == 0) return (bestResult, b);
            int selectRandom = random.Next(0, possibleMoves);
            GameTryMove tryMove = tryMoves[selectRandom];
            Game tryGame = tryMove.TryGame;

            if (tryMove.MakeMoveResult == MakeMoveResult.Legal)
            {
                (tryMove.ConfirmAlive, b) = MonteCarloMakeKillMove(depth - 1, tryGame);
                if (tryMove.ConfirmAlive == ConfirmAliveResult.Alive && tryGame.Board.IsPassMove && tryGame.Board.KoGameCheck == KoCheck.None)
                    tryMove.ConfirmAlive = ConfirmAliveResult.BothAlive;
            }
            else if (tryMove.MakeMoveResult == MakeMoveResult.KoBlocked)
            {
                (tryMove.ConfirmAlive, b) = MonteCarloMakeKillMove(depth, tryGame);
                if (GameHelper.WinOrLose(SurviveOrKill.Survive, result, tryGame.GameInfo))
                    tryMove.ConfirmAlive = ConfirmAliveResult.KoAlive;
            }
            bestResult = tryMove.ConfirmAlive;
            return (bestResult, b);
        }

        /// <summary>
        /// Check time taken.
        /// </summary>
        private void CheckTimeTaken(Stopwatch watch)
        {
            watch.Stop();
            long timeTaken = watch.ElapsedMilliseconds;
            elapsedTime = timeTaken;
            if (tree.Root == tree.AbsoluteRoot)
            {
                DebugHelper.WriteLine(DebugHelper.PrintTimeTaken(timeTaken), mctsDepth);
                DebugHelper.WriteLine("Total time taken (mcts): " + timeTaken + Environment.NewLine + Environment.NewLine, mctsDepth);
            }
            else
                DebugHelper.WriteLine("Time taken (mcts): " + timeTaken, mctsDepth);
        }
    }
}
