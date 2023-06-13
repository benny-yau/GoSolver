using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Go
{
    /// <summary>
    /// Retrieves solution move from solutionPoints in GameInfo if solution is followed.
    /// </summary>
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
    }
}
