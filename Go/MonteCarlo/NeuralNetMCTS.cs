using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using System.Diagnostics;

namespace Go
{
    public class NeuralNetMCTS : MonteCarloTreeSearch
    {
        public static Boolean FirstRun = true;
        public static List<Node> RemovedNodes = new List<Node>();
        public static List<Node> PrunedNodes = new List<Node>();
        public NeuralNetMCTS(int mctsDepth = 0)
        {
            this.mctsDepth = mctsDepth;
        }

        public override Node AnswerNode
        {
            get
            {
                return base.answerNode;
            }
            set
            {
                //validate answer after first run
                if (FirstRun && this.tree.Root.CurrentDepth == 0)
                {
                    FirstRun = false;
                    RestoreNodes();
                    DebugHelper.DebugWriteWithTab("First run completed" + Environment.NewLine + "Verifying answer...");
                    MonteCarloTreeSearch mcts = MonteCarloGame.InitializeMonteCarloComputerMove(value.State.Game, value);
                    if (mcts.AnswerNode != null)
                    {
                        //first run not valid
                        Pruning(value, mcts.AnswerNode);
                        return;
                    }
                    DebugHelper.DebugWriteWithTab("Answer move: " + value.State.Game.Board.Move);
                }
                base.answerNode = value;
            }
        }

        protected override Boolean ExpandNode(Node node)
        {
            if (!base.ExpandNode(node)) return false;

            if (node.State.HeatMap == null)
                GetHeatMap(node);

            foreach (Node childNode in node.ChildArray)
            {
                Board b = childNode.State.Game.Board;
                Point move = b.Move.Value;
                if (move.Equals(Game.PassMove)) continue;
                //set heatmap value in winscore
                childNode.State.WinScore = node.State.HeatMap[move.x, move.y];
            }
            //remove half of child nodes on first run
            if (FirstRun && node.ChildArray.Count >= 6)
            {
                node.ChildArray = node.ChildArray.OrderByDescending(n => n.State.WinScore).ToList();
                int halfCount = Convert.ToInt32(Math.Ceiling(node.ChildArray.Count * 0.5));
                for (int i = node.ChildArray.Count - 1; i > halfCount - 1; i--)
                {
                    Node removedNode = node.ChildArray[i];
                    if (!GameTryMove.IsNegligibleForBoard(removedNode.State.Game.Board, node.State.Game.Board)) continue;
                    node.ChildArray.Remove(removedNode);
                    RemovedNodes.Add(removedNode);
                }
            }
            return true;
        }

        protected override void Pruning(Node prunedNode, Node verifyNode)
        {
            base.Pruning(prunedNode, verifyNode);
            PrunedNodes.Add(prunedNode);
        }

        public static void GetHeatMap(Node node)
        {
            MonteCarloGame.isCheckHeatmap = true;
            UCT.node = node;
            //make setup moves
            Game g = node.State.Game;
            MonteCarloGame.SetupLeelazGame(g);
            //make last moves in game
            List<Point> lastMoves = g.Board.LastMoves;
            Content startContent = g.GameInfo.StartContent;
            for (int i = 0; i <= lastMoves.Count - 1; i++)
            {
                Point p = lastMoves[i];
                if (p.Equals(Game.PassMove)) continue;
                Content c = (i % 2 == 0) ? startContent : startContent.Opposite();
                MonteCarloGame.ConvertAndMakeMoveInLeelaBoard(p, c);
            }
            //get neural network values
            MonteCarloGame.inputWriter.WriteLine("heatmap");
            //wait for response from leelaz
            MonteCarloGame.checkHeatmap = new ManualResetEvent(false);
            MonteCarloGame.checkHeatmap.WaitOne();

            MonteCarloGame.isCheckHeatmap = false;
        }

        protected override Node RandomChildNode(Node node)
        {
            if (node.ChildArray.Any(n => n.State.WinScore > 0))
                return node.ChildArray.MaxObject(n => n.State.WinScore);
            else
                return base.RandomChildNode(node);
        }

        private void RestoreNodes()
        {
            RemovedNodes.ForEach(n => n.Parent.ChildArray.Add(n));
            RemovedNodes.Clear();
            PrunedNodes.ForEach(n => n.Parent.ChildArray.Add(n));
            PrunedNodes.ForEach(n => n.Parent.PrunedJson = null);
            PrunedNodes.Clear();
        }

        protected override void PostProcess(Node rootNode, Stopwatch watch)
        {
            //no answer found on first run
            if (NeuralNetMCTS.FirstRun && tree.Root.CurrentDepth == 0 && tree.Root.ChildArray.Count == 0)
            {
                //rerun mcts
                NeuralNetMCTS.FirstRun = false;
                RestoreNodes();
                DebugHelper.DebugWriteWithTab("First run completed" + Environment.NewLine + "Verifying answer...");
                MonteCarloTreeSearch mcts = MonteCarloGame.InitializeMonteCarloComputerMove(rootNode.State.Game, rootNode);
                if (mcts.AnswerNode != null)
                {
                    this.AnswerNode = mcts.AnswerNode;
                    DebugHelper.DebugWriteWithTab("Answer move: " + AnswerNode.State.Game.Board.Move);
                }
            }
            base.PostProcess(rootNode, watch);
        }
    }
}
