using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using System.Diagnostics;

namespace Go
{
    public class NeuralNetMCTS : FirstRunMCTS
    {
        public NeuralNetMCTS(int mctsDepth = 0)
        {
            this.mctsDepth = mctsDepth;
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
            if (NeuralNetMCTS.FirstRunStrategy)
                RemoveHalfOfNodes(node);
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
    }
}
