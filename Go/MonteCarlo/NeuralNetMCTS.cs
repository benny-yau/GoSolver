using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;

namespace Go
{
    public class NeuralNetMCTS : MonteCarloTreeSearch
    {
        public NeuralNetMCTS(int mctsDepth = 0)
        {
            this.mctsDepth = mctsDepth;
        }

        internal override Boolean ExpandNode(Node node, List<State> possibleStates)
        {
            if (!base.ExpandNode(node, possibleStates)) return false;

            if (node.State.HeatMap == null)
                GetHeatMap(node);

            foreach (Node childNode in node.ChildArray)
            {
                Point move = childNode.State.Game.Board.Move.Value;
                if (move.Equals(Game.PassMove)) continue;
                childNode.State.WinScore = node.State.HeatMap[move.x, move.y];
            }
            return true;
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

        internal override Node RandomChildNode(Node node)
        {
            if (node.ChildArray.Any(n => n.State.WinScore > 0))
                return node.ChildArray.MaxObject(n => n.State.WinScore);
            else
                return base.RandomChildNode(node);
        }
    }
}
