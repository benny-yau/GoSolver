using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

namespace Go
{
    public class SolutionHelper
    {
        /// <summary>
        /// Get solution move and make move on the board.
        /// </summary>
        public static bool UseSolutionPoints(Game g)
        {
            Point? solutionMove = GetSolutionMove(g.Board);
            if (solutionMove != null)
            {
                Point p = solutionMove.Value;
                g.MakeMove(p);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Check if last moves followed any of the solutions and return the solution move.
        /// </summary>
        public static Point? GetSolutionMove(Board b)
        {
            List<Point> solutionMoves = new List<Point>();
            List<List<Point>> solutions = b.GameInfo.CombinedSolutions.Where(s => s.Count > b.LastMoves.Count).ToList();
            int? solutionIndex = FollowedSolution(solutions, b.LastMoves).FirstOrDefault();
            if (solutionIndex != null)
            {
                List<Point> solution = solutions[solutionIndex.Value];
                return solution[b.LastMoves.Count];
            }
            return null;
        }

        /// <summary>
        /// Check if last moves followed any of the solutions and return confirm alive result.
        /// </summary>
        public static ConfirmAliveResult CheckSolutionComplete(Board b)
        {
            List<List<Point>> solutions = b.GameInfo.CombinedSolutions.Where(s => s.Count == b.LastMoves.Count).ToList();
            int? solutionIndex = FollowedSolution(solutions, b.LastMoves).FirstOrDefault();
            if (solutionIndex == null)
                return ConfirmAliveResult.Unknown;
            else
            {
                List<Point> solution = solutions[solutionIndex.Value];
                if (solution is CorrectedList)
                    return ConfirmAliveResult.CorrectedSolution;
                else
                {
                    if (b.GameInfo.UserFirst == PlayerOrComputer.Computer)
                        return ConfirmAliveResult.SolutionDisplayed;
                    else
                        return ConfirmAliveResult.Answer;
                }
            }
        }

        /// <summary>
        /// Get next move as hint.
        /// </summary>
        public static Point? GetNextAnswerHint(Game m)
        {
            List<List<Point>> solutions = m.GameInfo.solutionPoints;
            int? solutionIndex = FollowedSolution(solutions, m.Board.LastMoves).FirstOrDefault();
            if (solutionIndex != null)
            {
                List<Point> solution = solutions[solutionIndex.Value];
                if (m.Board.LastMoves.Count < solution.Count)
                    return solution[m.Board.LastMoves.Count];
            }
            return null;
        }

        /// <summary>
        /// Check if end of solution reached.
        /// </summary>
        public static Boolean AnswerFound(Game m)
        {
            List<List<Point>> solutions = m.GameInfo.CombinedSolutions.Where(s => s.Count == m.Board.LastMoves.Count).ToList();
            return (FollowedSolution(solutions, m.Board.LastMoves).Any());
        }

        /// <summary>
        /// Get dictate move where dictate points are specified.
        /// </summary>
        public static Point? GetDictateMove(Game g)
        {
            List<List<Point>> dictates = g.GameInfo.dictatePoints.Where(m => m.Count > g.Board.LastMoves.Count).ToList();
            int? solutionIndex = FollowedSolution(dictates, g.Board.LastMoves).FirstOrDefault();
            if (solutionIndex != null)
            {
                List<Point> solution = dictates[solutionIndex.Value];
                return solution[g.Board.LastMoves.Count];
            }
            return null;
        }

        /// <summary>
        /// Check if last moves followed any of the solutions and return the index of the solution move.
        /// </summary>
        public static IEnumerable<int?> FollowedSolution(List<List<Point>> solutions, List<Point> lastMoves)
        {
            for (int i = 0; i <= solutions.Count - 1; i++)
            {
                List<Point> solution = solutions[i];
                if (solution.Count < lastMoves.Count)
                    continue;

                Boolean blnFollowed = true;
                for (int j = 0; j <= lastMoves.Count - 1; j++)
                {
                    if (!lastMoves[j].Equals(solution[j]))
                    {
                        blnFollowed = false;
                        break;
                    }
                }
                if (blnFollowed)
                    yield return i;
            }
        }

        #region mapped moves
        /// <summary>
        /// Check solution and mapped points. 
        /// </summary>
        public static ConfirmAliveResult CheckSolutionAndMappedPoints(Game g)
        {
            //check solution points
            ConfirmAliveResult result = ConfirmAliveResult.Unknown;
            if (g.GameInfo.solutionPoints.Count > 0)
            {
                ConfirmAliveResult solutionComplete = SolutionHelper.CheckSolutionComplete(g.Board);
                if (solutionComplete != ConfirmAliveResult.Unknown)
                    return solutionComplete | ConfirmAliveResult.Mapped;
                else
                {
                    //get solution move and make move on board
                    if (SolutionHelper.UseSolutionPoints(g))
                    {
                        result = ConfirmAliveResult.Mapped | ConfirmAliveResult.UseSolution;
                        solutionComplete = SolutionHelper.CheckSolutionComplete(g.Board);
                        if (solutionComplete != ConfirmAliveResult.Unknown)
                            result |= solutionComplete;
                        return result;
                    }
                    else
                        result = ConfirmAliveResult.Incorrect;
                }
            }
            else if (g.GameInfo.solutionPoints.Count == 0 && g.GameInfo.UserFirst == PlayerOrComputer.Computer)
            {
                return ConfirmAliveResult.Mapped | ConfirmAliveResult.NoSolution;
            }

            //check mapped points
            if (!result.HasFlag(ConfirmAliveResult.Mapped))
                result = UseDictatePoints(g, result);

            if (!result.HasFlag(ConfirmAliveResult.Mapped))
            {
                int isChallenge = Convert.ToInt32(g.GameInfo.UserFirst == PlayerOrComputer.Computer);
                if (g.Board.LastMoves.Count == 1 + isChallenge)
                {
                    //get second mapped move from json
                    dynamic json = (isChallenge == 0) ? g.GameInfo.PlayerMoveJson : g.GameInfo.ChallengeMoveJson;
                    if (json == null) return result;
                    return FindSecondMoveMapped(g, json);
                }
                else if (g.Board.LastMoves.Count == 3 + isChallenge)
                {
                    //get fourth mapped move from json
                    dynamic json = (isChallenge == 0) ? g.GameInfo.PlayerMoveJson : g.GameInfo.ChallengeMoveJson;
                    if (json == null) return result;
                    return FindFourthMoveMapped(g, json);
                }
                else if (g.Board.LastMoves.Count == 5 + isChallenge)
                {
                    //get sixth mapped move from json
                    dynamic json = (isChallenge == 0) ? g.GameInfo.PlayerMoveJsonExtension : g.GameInfo.ChallengeMoveJsonExtension;
                    if (json == null) return result;
                    return FindSixthMoveMapped(g, json);
                }
            }
            return result;
        }

        /// <summary>
        /// Use dictate points specified to by-pass mapped points and reduce calculation time.
        /// </summary>
        private static ConfirmAliveResult UseDictatePoints(Game g, ConfirmAliveResult result)
        {
            Point? p = SolutionHelper.GetDictateMove(g);
            if (p == null) return result;
            MakeMoveResult moveResult = g.MakeMove(p.Value);
            result = ConfirmAliveResult.Incorrect | ConfirmAliveResult.Mapped;
            if (moveResult == MakeMoveResult.KoBlocked)
                result |= ConfirmAliveResult.KoAlive;
            return result;
        }

        /// <summary>
        /// Get second move from json map and return confirm alive result.
        /// </summary>
        private static ConfirmAliveResult FindSecondMoveMapped(Game g, dynamic jsonMap)
        {
            ConfirmAliveResult result = ConfirmAliveResult.Incorrect;
            if (g.Board.LastMoves.Count == 2 && !g.Board.LastMoves[0].Equals(g.GameInfo.solutionPoints[0][0]))
                return result;

            Point firstMovePt = g.Board.LastMoves[g.Board.LastMoves.Count - 1];
            JToken firstMove = ((JArray)jsonMap).Where(s => (int)s["FirstMove"]["x"] == firstMovePt.x && (int)s["FirstMove"]["y"] == firstMovePt.y).FirstOrDefault();

            if (firstMove == null) return result;
            int x = (int)firstMove["SecondMove"]["x"];
            int y = (int)firstMove["SecondMove"]["y"];
            MakeMoveResult moveResult = g.MakeMove(x, y);
            result |= ConfirmAliveResult.Mapped;

            if (moveResult == MakeMoveResult.KoBlocked)
                result |= ConfirmAliveResult.KoAlive;
            return result;
        }

        /// <summary>
        /// Get fourth move from json map and return confirm alive result.
        /// </summary>
        private static ConfirmAliveResult FindFourthMoveMapped(Game g, dynamic jsonMap)
        {
            ConfirmAliveResult result = ConfirmAliveResult.Incorrect;
            if (g.Board.LastMoves.Count == 4 && !g.Board.LastMoves[0].Equals(g.GameInfo.solutionPoints[0][0]))
                return result;

            Point firstMovePt = g.Board.LastMoves[g.Board.LastMoves.Count - 3];
            Point secondMovePt = g.Board.LastMoves[g.Board.LastMoves.Count - 2];

            JObject firstLevelMove = (JObject)((JArray)jsonMap).Where(m => (int)m["FirstMove"]["x"] == firstMovePt.x && (int)m["FirstMove"]["y"] == firstMovePt.y && (int)m["SecondMove"]["x"] == secondMovePt.x && (int)m["SecondMove"]["y"] == secondMovePt.y).FirstOrDefault();

            if (firstLevelMove == null) return result;

            JArray SecondLevel = (JArray)firstLevelMove["SecondLevel"];
            if (SecondLevel == null) return result;
            Point lastMovePt = g.Board.LastMoves[g.Board.LastMoves.Count - 1];
            JToken secondLevelMove = SecondLevel.Where(m => (int)m["ThirdMove"]["x"] == lastMovePt.x && (int)m["ThirdMove"]["y"] == lastMovePt.y).FirstOrDefault();
            if (secondLevelMove == null) return result;
            JToken fourthMove = secondLevelMove["FourthMove"];
            int x = (int)fourthMove["x"];
            int y = (int)fourthMove["y"];
            MakeMoveResult moveResult = g.MakeMove(x, y);
            result |= ConfirmAliveResult.Mapped;

            if (moveResult == MakeMoveResult.KoBlocked)
                result |= ConfirmAliveResult.KoAlive;
            return result;
        }


        /// <summary>
        /// Get sixth move from json map and return confirm alive result.
        /// </summary>
        private static ConfirmAliveResult FindSixthMoveMapped(Game g, dynamic jsonMap)
        {
            ConfirmAliveResult result = ConfirmAliveResult.Incorrect;
            if (g.Board.LastMoves.Count == 6 && !g.Board.LastMoves[0].Equals(g.GameInfo.solutionPoints[0][0]))
                return result;

            Point firstMovePt = g.Board.LastMoves[g.Board.LastMoves.Count - 5];
            Point secondMovePt = g.Board.LastMoves[g.Board.LastMoves.Count - 4];

            JObject firstLevelMove = (JObject)((JArray)jsonMap).Where(m => (int)m["FirstMove"]["x"] == firstMovePt.x && (int)m["FirstMove"]["y"] == firstMovePt.y && (int)m["SecondMove"]["x"] == secondMovePt.x && (int)m["SecondMove"]["y"] == secondMovePt.y).FirstOrDefault();

            if (firstLevelMove == null) return result;

            JArray SecondLevel = (JArray)firstLevelMove["SecondLevel"];
            if (SecondLevel == null) return result;

            Point thirdMovePt = g.Board.LastMoves[g.Board.LastMoves.Count - 3];
            Point fourthMovePt = g.Board.LastMoves[g.Board.LastMoves.Count - 2];

            JObject secondLevelMove = (JObject)SecondLevel.Where(m => (int)m["ThirdMove"]["x"] == thirdMovePt.x && (int)m["ThirdMove"]["y"] == thirdMovePt.y && (int)m["FourthMove"]["x"] == fourthMovePt.x && (int)m["FourthMove"]["y"] == fourthMovePt.y).FirstOrDefault();

            if (secondLevelMove == null) return result;

            JArray ThirdLevel = (JArray)secondLevelMove["ThirdLevel"];
            if (ThirdLevel == null) return result;

            Point lastMovePt = g.Board.LastMoves[g.Board.LastMoves.Count - 1];
            JToken thirdLevelMove = ThirdLevel.Where(m => (int)m["FifthMove"]["x"] == lastMovePt.x && (int)m["FifthMove"]["y"] == lastMovePt.y).FirstOrDefault();
            if (thirdLevelMove == null) return result;
            JToken sixthMove = thirdLevelMove["SixthMove"];
            int x = (int)sixthMove["x"];
            int y = (int)sixthMove["y"];
            MakeMoveResult moveResult = g.MakeMove(x, y);
            result |= ConfirmAliveResult.Mapped;

            if (moveResult == MakeMoveResult.KoBlocked)
                result |= ConfirmAliveResult.KoAlive;
            return result;
        }
        #endregion

        /// <summary>
        /// Display game ended message from flags in confirm alive result.
        /// </summary>
        public static String GameEndedMessage(ConfirmAliveResult result, Game g)
        {
            String msg = "";
            if (result.HasFlag(ConfirmAliveResult.Answer) || result.HasFlag(ConfirmAliveResult.SolutionDisplayed))
            {
                Boolean isKo = (g.GameInfo.Survival == SurviveOrKill.KillWithKo || g.GameInfo.Survival == SurviveOrKill.SurviveWithKo);
                if (result.HasFlag(ConfirmAliveResult.SolutionDisplayed))
                    msg = "Solution complete" + (isKo ? " (Ko)." : ".");
                else
                    msg = "Question solved" + (isKo ? " (Ko)." : ".");
            }
            else if (result.HasFlag(ConfirmAliveResult.CorrectedSolution))
                msg = "Incorrect move. Try again.";
            else if (result.HasFlag(ConfirmAliveResult.KoAlive))
                msg = "Computer Ko move. Try again.";
            else if (result.HasFlag(ConfirmAliveResult.BothAlive))
                msg = "Both alive. Try again.";
            else if (result.HasFlag(ConfirmAliveResult.TargetKilled))
                msg = "Target killed. Try again.";
            else if (result.HasFlag(ConfirmAliveResult.TargetSurvived))
                msg = "Target survived. Try again.";
            return msg;
        }


    }
}
