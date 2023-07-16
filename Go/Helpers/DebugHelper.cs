using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Go
{
    public class DebugHelper
    {
        /// <summary>
        /// Print debug statements with tabs in front.
        /// </summary>
        public static void DebugWriteWithTab(String content, int gameDepth = 0)
        {
            if (!Game.debugMode) return;
            String tabs = (gameDepth == 0) ? "" : string.Concat(Enumerable.Repeat('\t', gameDepth));
            Debug.WriteLine(tabs + content);
        }

        /// <summary>
        /// Print game try moves and redundant try moves on exhaustive search mode.
        /// </summary>
        /// <returns></returns>
        public static String PrintGameTryMoves(Game currentGame, List<GameTryMove> tryMoves, List<GameTryMove> redundantTryMoves)
        {
            String content = currentGame.Board.ToString() + Environment.NewLine + "Scenario: " + currentGame.GameInfo.ScenarioName + Environment.NewLine + "Last moves: " + currentGame.Board.GetLastMoves() + Environment.NewLine;

            String msg = "";
            foreach (GameTryMove tryMove in tryMoves)
            {
                if (msg != "") msg += ",";
                msg += "(" + tryMove.Move.x + "," + tryMove.Move.y + ")";
            }
            content += "Game try moves: " + msg + Environment.NewLine;
            if (redundantTryMoves == null) return content;
            msg = "";
            foreach (GameTryMove tryMove in redundantTryMoves)
            {
                if (msg != "") msg += ",";
                msg += "(" + tryMove.Move.x + "," + tryMove.Move.y + ")";
            }
            content += "Redundant try moves: " + msg + Environment.NewLine;
            return content;
        }

        public static void PrintBoardToText(Board board)
        {
            PrintBoardToText(board, "GameBoards.txt");
        }

        public static void PrintBoardToText(Board board, String fileName)
        {
            String content = board.ToString() + Environment.NewLine + board.GameInfo.ScenarioName + Environment.NewLine + board.GetLastMoves() + Environment.NewLine;
            File.AppendAllText(Directory.GetCurrentDirectory() + "\\" + fileName, content);
            Debug.WriteLine(content);
        }

        public static void PrintGameTryMovesToText(Game currentGame, List<GameTryMove> tryMoves, List<GameTryMove> redundantTryMoves)
        {
            String content = PrintGameTryMoves(currentGame, tryMoves, redundantTryMoves);
            File.AppendAllText(Directory.GetCurrentDirectory() + "\\GameBoards.txt", content);
            Debug.WriteLine(content);
        }
        public static void PrintToText(String text, String fileName)
        {
            File.AppendAllText(Directory.GetCurrentDirectory() + "\\" + fileName, text + Environment.NewLine);
        }

        public static String CreateSetupMovesScript(String contents)
        {
            MatchCollection matches = Regex.Matches(contents, @"\(-?\d+,-?\d+\)");
            String script = "";
            foreach (Match match in matches)
            {
                script += "g.MakeMove" + match.Value + ";" + Environment.NewLine;
            }
            return script;
        }

        public static String ShowMovablePoints(Game game)
        {
            List<Point> points = new List<Point>();
            List<GameTryMove> gameTryMoves = GameHelper.GetTryMovesForGame(game);
            gameTryMoves.ForEach(t => points.Add(t.TryGame.Board.Move.Value));
            return ShowMovablePoints(game, points);
        }

        public static String ShowMovablePoints(Game game, List<Point> points)
        {
            string rc = "\n" + new String(' ', 4);
            for (int j = 0; j < game.GameInfo.BoardSizeX; j++)
            {
                rc += j.ToString().PadRight(2, ' ');
            }
            for (int i = 0; i < game.GameInfo.BoardSizeY; i++)
            {
                rc += "\n" + i.ToString().PadLeft(3, ' ') + " ";
                for (int j = 0; j < game.GameInfo.BoardSizeX; j++)
                {
                    if (points.Any(p => p.x == j && p.y == i))
                        rc += "X";
                    else
                        rc += ".";
                    rc += " ";
                }
            }
            return rc;
        }

        public static void ReadCountFromFile()
        {
            String contents = File.ReadAllText(Directory.GetCurrentDirectory() + "\\RedundantCount.txt");
            String[] array = contents.Split('|');
            int totalCount = 0;
            foreach (String s in array)
            {
                if (s == "") continue;
                int i = Convert.ToInt32(s);
                totalCount += i;
            }
            Debug.WriteLine("Total count: " + totalCount);
        }

        public static String PrintTimeTaken(long timeTaken)
        {
            int msPerMinute = 60000;
            if (timeTaken > msPerMinute)
            {
                int timeTakenInMinutes = (int)Math.Floor((double)(timeTaken / msPerMinute));
                long millisecondsRemaining = timeTaken - (timeTakenInMinutes * msPerMinute);

                int timeTakenInSeconds = (int)Math.Floor((double)(millisecondsRemaining / 1000));
                return timeTakenInMinutes + " minute" + ((timeTakenInMinutes <= 1) ? "" : "s") + " and " + timeTakenInSeconds + " second" + ((timeTakenInSeconds <= 1) ? "" : "s");
            }
            else
            {
                int timeTakenInSeconds = (int)Math.Floor((double)(timeTaken / 1000));
                return timeTakenInSeconds + " second" + ((timeTakenInSeconds <= 1) ? "" : "s");
            }
        }
    }
}
