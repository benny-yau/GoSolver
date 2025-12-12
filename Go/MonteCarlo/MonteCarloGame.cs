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
        private static Game game;
        private static Boolean isCheckHeatmap = false;
        private static ManualResetEvent checkHeatmap = null;
        private static List<String> heatMapLines = new List<String>();
        private static String alphabets = "ABCDEFGHJKLMNOPQRSTUVQXYZ";

        /// <summary>
        /// Initialize monte carlo computer move.
        /// </summary>
        public static MonteCarloTreeSearch InitializeMonteCarloComputerMove(Game g, Node rootNode = null, int mctsDepth = 0)
        {
            Game.UseMCTS = true;
            if (rootNode == null)
            {
                State state = new State(g);
                rootNode = new Node(state);
                rootNode.State.Depth = g.GameInfo.SearchDepth;
                state.SurviveOrKill = GameHelper.KillOrSurvivalForNextMove(g.Board);
            }
            MonteCarloTreeSearch mcts = new MonteCarloTreeSearch(rootNode, mctsDepth);
            mcts.FindNextMove();
            return mcts;
        }


        /// <summary>
        /// Make monte carlo tree search.
        /// </summary>
        public static (ConfirmAliveResult, Node, long?) MakeMonteCarloTreeSearch(Game game)
        {
            MonteCarloTreeSearch mcts = InitializeMonteCarloComputerMove(game);

            //make the move on the board
            if (mcts.AnswerNode != null)
            {
                Game g = mcts.AnswerNode.State.Game;
                game.MakeMove(g.Board);
            }
            ConfirmAliveResult result = GetResultForMCTS(mcts);
            return (result, mcts.AnswerNode, mcts.elapsedTime);

        }

        /// <summary>
        /// Get result for MCTS.
        /// </summary>
        private static ConfirmAliveResult GetResultForMCTS(MonteCarloTreeSearch mcts)
        {
            State state = mcts.tree.Root.State;
            Game game = state.Game;
            Boolean answerFound = (mcts.AnswerNode != null);
            //return result as dead or alive
            ConfirmAliveResult confirmAlive = ConfirmAliveResult.Unknown;
            if (state.SurviveOrKill == SurviveOrKill.Kill)
                confirmAlive = (answerFound) ? ConfirmAliveResult.Dead : ConfirmAliveResult.Alive;
            else if (state.SurviveOrKill == SurviveOrKill.Survive)
                confirmAlive = (answerFound) ? ConfirmAliveResult.Alive : ConfirmAliveResult.Dead;

            if (!answerFound)
                return confirmAlive;

            Game g = mcts.AnswerNode.State.Game;
            Point answerMove = g.Board.Move.Value;

            //show answer moves
            Debug.WriteLine(MonteCarloGame.GetAnswerJson(g, mcts.AnswerNode));

            //check if both alive
            if (mcts.AnswerNode.State.SurviveOrKill == SurviveOrKill.Survive && answerMove.Equals(Game.PassMove))
            {
                if (ResultBothAlive(g))
                    return ConfirmAliveResult.BothAlive;
            }

            //check if ko alive
            if (game.Board.IsPassMove && game.Board.KoCapture != null && answerMove.Equals(game.Board.KoCapture.Value))
                return ConfirmAliveResult.KoAlive;

            //return result with target killed or target survived
            confirmAlive = LifeCheck.CheckIfTargetSurvivedOrKilled(confirmAlive, state.SurviveOrKill, state.Game.Board);
            return confirmAlive;
        }

        /// <summary>
        /// Return both alive only if no more external liberties.
        /// </summary>
        private static Boolean ResultBothAlive(Game g)
        {
            List<Group> targets = LifeCheck.GetTargets(g.Board);
            Content c = GameHelper.GetContentForSurviveOrKill(g.Board.GameInfo, SurviveOrKill.Survive);
            if (targets.Any(t => t.Liberties.All(liberty => GroupHelper.GetDirectKillerGroup(g.Board, liberty, c) != null)))
                return true;
            return false;
        }

        /// <summary>
        /// Json for search answer.
        /// </summary>
        public static String GetAnswerJson(Game game, Node answerNode)
        {
            JObject json = MonteCarloMapping.MapAnswerNodeToJson(game, Game.PassMove, answerNode, false);
            return JsonConvert.SerializeObject(json);
        }

        #region neural network
        /// <summary>
        /// Get heat map.
        /// </summary>
        public static void GetHeatMap(Game g)
        {
            isCheckHeatmap = true;
            MonteCarloGame.game = g;
            //make setup moves
            SetupLeelazGame(g);
            //make last moves in game
            List<Point> lastMoves = g.Board.LastMoves;
            Content startContent = g.GameInfo.StartContent;
            for (int i = 0; i <= lastMoves.Count - 1; i++)
            {
                Point p = lastMoves[i];
                if (p.Equals(Game.PassMove))
                    continue;
                Content c = (i % 2 == 0) ? startContent : startContent.Opposite();
                ConvertAndMakeMoveInLeelaBoard(p, c);
            }
            //get neural network values
            inputWriter.WriteLine("heatmap");
            //wait for response from leelaz
            checkHeatmap = new ManualResetEvent(false);
            checkHeatmap.WaitOne();

            isCheckHeatmap = false;
        }

        /// <summary>
        /// Setup leela zero game.
        /// </summary>
        public static void SetupLeelazGame(Game g, Boolean setHandicapMoves = true)
        {
            inputWriter.WriteLine("clear_board");
            foreach (SetupMove move in g.GameInfo.SetupMoves)
                ConvertAndMakeMoveInLeelaBoard(move.Move, move.Content);

            if (setHandicapMoves)
            {
                List<String> handicapMoves = new List<String>() { "Q16", "Q10", "Q4", "K16", "K10", "D16", "D10", "C13" };
                String contentToMove = (g.GameInfo.StartContent == Content.Black) ? "W" : "B";
                handicapMoves.ForEach(n => MonteCarloGame.inputWriter.WriteLine("play " + contentToMove + " " + n));
            }
        }

        /// <summary>
        /// Convert coordinates and make move in leela board.
        /// </summary>
        public static void ConvertAndMakeMoveInLeelaBoard(Point point, Content c)
        {
            String x = alphabets.Substring(point.x, 1);
            int y = 18 - point.y + 1;
            String content = (c == Content.Black) ? "B" : "W";
            inputWriter.WriteLine("play " + content + " " + x + y.ToString());
        }

        /// <summary>
        /// Retrieve heatmap from leela zero neural network.
        /// </summary>
        public static void MyProcess_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            String line = e.Data;
            if (line == "" || line.StartsWith("=") || line.StartsWith("?")) return;
            if (!isCheckHeatmap)
            {
                Console.WriteLine(line);
                return;
            }
            if (line.StartsWith("winrate:"))
            {
                String winrate = line.Replace("winrate:", "");
                //ensure all lines of heatmap collected
                if (heatMapLines.Count == 19)
                {
                    //store entire heatmap
                    game.heatMap = new int[19, 19];
                    for (int y = 0; y <= heatMapLines.Count - 1; y++)
                    {
                        String heatMapLine = heatMapLines[y];
                        char[] delimiterChars = { ' ' };
                        String[] heatNumbers = heatMapLine.Split(delimiterChars, StringSplitOptions.RemoveEmptyEntries);
                        for (int x = 0; x <= heatNumbers.Length - 1; x++)
                            game.heatMap[x, y] = Convert.ToInt32(heatNumbers[x]);
                    }
                    heatMapLines.Clear();
                    //continue
                    checkHeatmap.Set();
                }
                return;
            }
            if (line.Length != 76) return;
            heatMapLines.Add(line);
        }
        #endregion
    }
}
