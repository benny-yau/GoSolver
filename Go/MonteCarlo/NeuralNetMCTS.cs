using System;
using System.Collections.Generic;
using System.Threading;

namespace Go
{
    /// <summary>
    /// AlphaZero methodology adapted for GoSolver.
    /// </summary>
    public class NeuralNetMCTS : MonteCarloTreeSearch
    {
        public override int VisitCountMinReq
        {
            get
            {
                return -1;
            }
        }

        /// <summary>
        /// MCTS for neural network.
        /// </summary>
        internal override Node SelectPromisingNode(Node rootNode)
        {
            Node node = rootNode;
            node = moveToLeaf(node);
            double winrate = evaluateLeaf(node);
            backFill(node, winrate);
            return node;
        }

        public NeuralNetMCTS(int mctsDepth = 0)
        {
            this.mctsDepth = mctsDepth;
        }

        internal override Node RandomChildNode(Node node)
        {
            return node;
        }

        internal override Boolean ReachedDepthToVerify(Node node)
        {
            return node.Parent != null && node.Parent.CurrentDepth >= DepthToVerify && node.Parent.State.Stats["N"] >= 3;
        }

        public override void SimulateRandomPlayout(Node node)
        {
            return;
        }

        /// <summary>
        /// Make move to leaf node based on stats value.
        /// </summary>
        internal Node moveToLeaf(Node node)
        {
            double maxQU, Nb, Pb;
            Node currentNode = node;
            while (currentNode.ChildArray.Count > 0)
            {
                maxQU = -99999;
                Nb = 0;
                Pb = 0;
                foreach (Node childNode in currentNode.ChildArray)
                {
                    Nb += childNode.State.Stats["N"];
                    Pb += childNode.State.Stats["P"];
                }
                if (Pb == 0) Pb = 1;

                int idx = 0;
                Node bestNode = null;
                foreach (Node childNode in currentNode.ChildArray)
                {
                    double U = (childNode.State.Stats["P"] / Pb) * Math.Sqrt(Nb) / (1 + childNode.State.Stats["N"]);
                    double Q = childNode.State.Stats["Q"];
                    if (Q + U > maxQU)
                    {
                        maxQU = Q + U;
                        bestNode = childNode;
                    }
                    idx += 1;
                }
                currentNode = bestNode;
            }
            return currentNode;
        }


        /// <summary>
        /// Get neural network value from leelaz.
        /// </summary>
        internal double evaluateLeaf(Node leafNode)
        {
            if (leafNode.State.HeatMap == null)
            {
                MonteCarloGame.isCheckHeatmap = true;
                UCT.node = leafNode;
                //make setup moves
                Game g = leafNode.State.Game;
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
            return leafNode.State.Winrate;
        }

        /// <summary>
        /// Backfill stats in node based on winrate.
        /// </summary>
        internal void backFill(Node leafNode, double winrate)
        {
            Node node = leafNode;
            while (node.Parent != null)
            {
                winrate = 1 - winrate;
                Dictionary<String, double> stats = node.State.Stats;
                stats["N"] += 1;
                stats["W"] += winrate;
                stats["Q"] += stats["W"] / stats["N"];
                node = node.Parent;
            }
        }

    }
}
