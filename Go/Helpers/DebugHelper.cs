using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Go
{
    public class DebugHelper
    {
        public static void WriteLine(String msg, int gameDepth = 0)
        {
            if (!Game.debugMode) return;
            String tabs = (gameDepth == 0) ? "" : string.Concat(Enumerable.Repeat('\t', gameDepth));
            Debug.WriteLine(tabs + msg);
        }

        public static String PrintGameTryMoves(Game g, List<GameTryMove> tryMoves, List<GameTryMove> redundantTryMoves)
        {
            String msg = g.Board.ToString() + Environment.NewLine + "Scenario: " + g.GameInfo.ScenarioName + Environment.NewLine + "Last moves: " + g.Board.GetLastMoves() + Environment.NewLine;
            
            msg += "Game try moves: " + tryMoves.GetConcatenatedString() + Environment.NewLine;
            if (redundantTryMoves == null) return msg;
            msg += "Redundant try moves: " + redundantTryMoves.GetConcatenatedString() + Environment.NewLine;
            return msg;
        }

        public static void PrintBoardToText(Board board)
        {
            PrintBoardToText(board, "GameBoards.txt");
        }

        public static void PrintBoardToText(Board board, String fileName)
        {
            String msg = board.ToString() + Environment.NewLine + board.GameInfo.ScenarioName + Environment.NewLine + board.GetLastMoves() + Environment.NewLine;
            File.AppendAllText(Directory.GetCurrentDirectory() + "\\" + fileName, msg);
            Debug.WriteLine(msg);
        }

        public static void PrintBoardToText(Board board, String info, String fileName)
        {
            String msg = board.ToString() + Environment.NewLine + board.GameInfo.ScenarioName + Environment.NewLine + board.GetLastMoves() + Environment.NewLine + info + Environment.NewLine;
            File.AppendAllText(Directory.GetCurrentDirectory() + "\\" + fileName, msg);
            Debug.WriteLine(msg);
        }

        public static void PrintGameTryMovesToText(Game g, List<GameTryMove> tryMoves, List<GameTryMove> redundantTryMoves)
        {
            String msg = PrintGameTryMoves(g, tryMoves, redundantTryMoves);
            File.AppendAllText(Directory.GetCurrentDirectory() + "\\GameBoards.txt", msg);
            Debug.WriteLine(msg);
        }

        public static void PrintToText(String text, String fileName)
        {
            File.AppendAllText(Directory.GetCurrentDirectory() + "\\" + fileName, text + Environment.NewLine);
        }

        public static String CreateSetupMovesScript(String script)
        {
            MatchCollection matches = Regex.Matches(script, @"\(-?\d+,-?\d+\)");
            String msg = "";
            foreach (Match match in matches)
                msg += "g.MakeMove" + match.Value + ";" + Environment.NewLine;
            return msg;
        }

        public static String ShowTryMoves(Game g)
        {
            List<Point> points = new List<Point>();
            List<GameTryMove> gameTryMoves = GameHelper.GetTryMovesForGame(g);
            gameTryMoves.ForEach(t => points.Add(t.TryGame.Board.Move.Value));
            return ShowPointsInBoard(g, points);
        }

        public static String ShowPointsInBoard(Game g, List<Point> points)
        {
            string msg = "\n" + new String(' ', 4);
            for (int j = 0; j < g.GameInfo.BoardSizeX; j++)
                msg += j.ToString().PadRight(2, ' ');
            for (int i = 0; i < g.GameInfo.BoardSizeY; i++)
            {
                msg += "\n" + i.ToString().PadLeft(3, ' ') + " ";
                for (int j = 0; j < g.GameInfo.BoardSizeX; j++)
                {
                    if (points.Any(p => p.x == j && p.y == i))
                        msg += "X";
                    else
                        msg += ".";
                    msg += " ";
                }
            }
            return msg;
        }

        public static String ShowHeatMapValues(Game g, int[,] list)
        {
            string msg = "\n" + new String(' ', 4);
            for (int j = 0; j < g.GameInfo.BoardSizeX; j++)
                msg += j.ToString().PadRight(2, ' ');
            for (int i = 0; i < g.GameInfo.BoardSizeY; i++)
            {
                msg += "\n" + i.ToString().PadLeft(3, ' ') + " ";
                for (int j = 0; j < g.GameInfo.BoardSizeX; j++)
                    msg += list[j, i].ToString().PadRight(2, ' ');
            }
            return msg;
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
