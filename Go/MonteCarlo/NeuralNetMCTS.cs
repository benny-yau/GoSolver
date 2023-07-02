using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using System.Diagnostics;

namespace Go
{
    /// <summary>
    /// Neural Net MCTS - neural net from Leela zero
    /// </summary>
    public class NeuralNetMCTS : NoExhaustiveSearchMCTS
    {
        public NeuralNetMCTS(Node rootNode, int mctsDepth = 0) : base(rootNode, mctsDepth)
        {
        }

        /// <summary>
        /// Expand node.
        /// </summary>
        protected override void ExpandNode(Node node)
        {
            NodeExpansion(node);

            //get heat map
            if (node.State.HeatMap == null)
                GetHeatMap(node);

            foreach (Node childNode in node.ChildArray)
            {
                Board b = childNode.State.Game.Board;
                Point move = b.Move.Value;
                if (move.Equals(Game.PassMove)) continue;
                //set heatmap value
                childNode.State.HeatValue = node.State.HeatMap[move.x, move.y];
                childNode.State.WinScore = childNode.State.HeatValue;
            }
            base.ExpandNode(node);
        }

        /// <summary>
        /// Get heat map.
        /// </summary>
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
    }
}
