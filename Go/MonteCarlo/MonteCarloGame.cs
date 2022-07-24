using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;

namespace Go
{
    public class MonteCarloGame
    {
        public static Boolean useLeelaZero = false;
        public static StreamWriter inputWriter = null;
        public static Boolean isCheckHeatmap = false;
        public static ManualResetEvent checkHeatmap = null;
        private static List<String> heatMapLines = new List<String>();
        private static String alphabets = "ABCDEFGHJKLMNOPQRSTUVQXYZ";
        /// <summary>
        /// Create new mcts search tree and initialize search.
        /// </summary>
        public static MonteCarloTreeSearch InitializeMonteCarloComputerMove(Game g, Node rootNode = null, int mctsDepth = 0)
        {
            if (rootNode == null)
            {
                State state = new State(g);
                rootNode = new Node(state);
                rootNode.State.Depth = g.GetStartingDepth();
                state.SurviveOrKill = GameHelper.KillOrSurvivalForNextMove(g.Board);
            }
            MonteCarloTreeSearch mcts;
            if (useLeelaZero)
                mcts = new NeuralNetMCTS(mctsDepth);
            else
                mcts = new MonteCarloTreeSearch(mctsDepth);
            mcts.FindNextMove(rootNode);
            return mcts;
        }


        /// <summary>
        /// Start mcts search for real-time move. Not used in mapping.
        /// </summary>
        public static (ConfirmAliveResult, Node) MonteCarloRealTimeMove(Game game)
        {
            MonteCarloTreeSearch mcts = InitializeMonteCarloComputerMove(game);
            State state = mcts.tree.Root.State;

            //answer found
            if (mcts.AnswerNode != null)
            {
                Game g = mcts.AnswerNode.State.Game;
                Point answerMove = g.Board.Move.Value;
                //make the move on the board
                game.MakeMove(answerMove);

                //check if both alive
                if (mcts.AnswerNode.State.SurviveOrKill == SurviveOrKill.Survive && answerMove.Equals(Game.PassMove))
                {
                    if (ResultBothAlive(g))
                        return (ConfirmAliveResult.BothAlive, mcts.AnswerNode);
                }

                //check if ko alive
                if (game.Board.Move.Equals(Game.PassMove) && game.Board.singlePointCapture != null && answerMove.Equals(game.Board.singlePointCapture.Value))
                    return (ConfirmAliveResult.KoAlive, mcts.AnswerNode);
            }

            //return result as dead or alive
            ConfirmAliveResult confirmAlive = ConfirmAliveResult.Unknown;
            if (state.SurviveOrKill == SurviveOrKill.Kill)
                confirmAlive = (mcts.AnswerNode != null) ? ConfirmAliveResult.Dead : ConfirmAliveResult.Alive;
            else if (state.SurviveOrKill == SurviveOrKill.Survive)
                confirmAlive = (mcts.AnswerNode != null) ? ConfirmAliveResult.Alive : ConfirmAliveResult.Dead;

            //return result with target killed or target survived
            confirmAlive = LifeCheck.CheckIfTargetSurvivedOrKilled(confirmAlive, state.SurviveOrKill, state.Game);

            Node answerNode = (mcts.AnswerNode != null) ? mcts.AnswerNode : mcts.tree.Root;
            return (confirmAlive, answerNode);
        }

        private static Boolean ResultBothAlive(Game g)
        {
            //ensure all liberties are in killer groups
            List<Group> killerGroups = GroupHelper.GetKillerGroups(g.Board, Content.Unknown, true);
            if (killerGroups.Count == 0) return false;
            List<Point> targets = LifeCheck.GetTargets(g.Board, g.GameInfo.targetPoints);
            foreach (Point target in targets)
            {
                HashSet<Point> liberties = g.Board.GetGroupAt(target).Liberties;
                if (liberties.All(liberty => GroupHelper.GetKillerGroupFromCache(g.Board, liberty) != null))
                    return true;
            }
            return false;
        }

        #region neural network
        /// <summary>
        /// Make setup moves in leela board.
        /// </summary>
        /// <param name="g"></param>
        public static void SetupLeelazGame(Game g)
        {
            MonteCarloGame.inputWriter.WriteLine("clear_board");
            foreach (SetupMove move in g.GameInfo.SetupMoves)
                ConvertAndMakeMoveInLeelaBoard(move.Move, move.Content);

            if (g.GameInfo.SetupMoves.Count > 0)
            {
                if (g.GameInfo.StartContent == Content.Black)
                {
                    MonteCarloGame.inputWriter.WriteLine("play W Q16");
                    MonteCarloGame.inputWriter.WriteLine("play W Q10");
                    MonteCarloGame.inputWriter.WriteLine("play W Q4");
                }
                else
                {
                    MonteCarloGame.inputWriter.WriteLine("play B Q16");
                    MonteCarloGame.inputWriter.WriteLine("play B Q10");
                    MonteCarloGame.inputWriter.WriteLine("play B Q4");
                }
            }
        }

        public static void ConvertAndMakeMoveInLeelaBoard(Point point, Content c)
        {
            String x = alphabets.Substring(point.x, 1);
            int y = 18 - point.y + 1;
            String content = (c == Content.Black) ? "B" : "W";
            MonteCarloGame.inputWriter.WriteLine("play " + content + " " + x + y.ToString());
        }

        /// <summary>
        /// Retrieve neural network heatmap and other output data from leelaz.
        /// </summary>
        public static void MyProcess_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            String line = e.Data;
            if (line == "" || line.StartsWith("=") || line.StartsWith("?")) return;
            if (MonteCarloGame.isCheckHeatmap)
            {
                if (line.StartsWith("winrate:"))
                {
                    String winrate = line.Replace("winrate:", "");
                    UCT.node.State.Winrate = Convert.ToDouble(winrate);
                    //ensure all lines of heatmap collected
                    if (heatMapLines.Count == 19)
                    {
                        //store entire heatmap to node state heatmap
                        UCT.node.State.HeatMap = new int[19, 19];
                        for (int y = 0; y <= heatMapLines.Count - 1; y++)
                        {
                            String heatMapLine = heatMapLines[y];
                            char[] delimiterChars = { ' ' };
                            String[] heatNumbers = heatMapLine.Split(delimiterChars, StringSplitOptions.RemoveEmptyEntries);
                            for (int x = 0; x <= heatNumbers.Length - 1; x++)
                                UCT.node.State.HeatMap[x, y] = Convert.ToInt32(heatNumbers[x]);
                        }
                        heatMapLines.Clear();
                        //continue
                        MonteCarloGame.checkHeatmap.Set();
                    }
                    return;
                }
                if (line.Length != 76) return;
                heatMapLines.Add(line);

            }
            else
                Console.WriteLine(line);
        }
        #endregion
    }
}
