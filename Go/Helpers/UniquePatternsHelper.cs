using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dh = Go.DirectionHelper;

namespace Go
{
    public class UniquePatternsHelper
    {
        #region bent four
        /// <summary>
        /// Bent four is a unique scenario which appears to be ko alive but is essentially dead. 
        /// https://senseis.xmp.net/?BentFourInTheCorner
        /// <see cref="UnitTestProject.BentFourTest.BentFourTest_Scenario7kyu26_2" />
        /// Check for covered eye <see cref="UnitTestProject.BentFourTest.BentFourTest_Scenario_Corner_A87" />
        /// </summary>
        /*
 12 . X . . . . . . . . . . . . . . . . . 
 13 . . . . . . . . . . . . . . . . . . . 
 14 . X X X . . . . . . . . . . . . . . . 
 15 O O O X . . . . . . . . . . . . . . . 
 16 X . O X . . . . . . . . . . . . . . . 
 17 X O O X . . . . . . . . . . . . . . . 
 18 X . O . . . . . . . . . . . . . . . .
        killer makes move at (1, 18) at end game after removing all ko threats
         */
        public static Boolean CheckForBentFour(Board board, List<GameTryMove> tryMoves = null)
        {
            List<Group> killerGroups = GroupHelper.GetKillerGroups(board);
            if (killerGroups.Count == 0)
                return false;
            Group killerGroup = killerGroups.First();
            if (killerGroup.Points.Count != 5)
                return false;
            List<Point> emptyPoints = killerGroup.Points.Where(p => board[p] == Content.Empty).ToList();
            if (emptyPoints.Count != 2) return false;
            if (board.GetNeighbourGroups(killerGroup).Count != 1) return false;

            //all game try moves should be within killer group
            if (tryMoves != null && tryMoves.Where(p => !emptyPoints.Contains(p.Move)).Any()) return false;

            if (PreCornerBentFourFormation(board, killerGroup))
                return true;
            return false;
        }

        public static Boolean CheckForBentFour(Game currentGame, List<GameTryMove> tryMoves = null)
        {
            Board board = currentGame.Board;
            return CheckForBentFour(board, tryMoves);
        }

        /// <summary>
        /// Bent three or straight three formation at corner with two liberty points in killer group.
        /// </summary>
        public static Boolean PreCornerBentFourFormation(Board tryBoard, Group killerGroup)
        {
            List<Point> contentPoints = killerGroup.Points.Where(t => tryBoard[t] == killerGroup.Content).ToList();
            //ensure formation at corner point
            if (!contentPoints.Any(p => tryBoard.CornerPoint(p)) || contentPoints.Any(p => tryBoard.PointWithinMiddleArea(p))) return false;
            //bent three or straight three formation
            if (KillerFormationHelper.BentThreeFormation(tryBoard, contentPoints) || KillerFormationHelper.StraightThreeFormation(tryBoard, contentPoints))
            {
                //ensure two liberties in killer group
                List<Point> emptyPoints = killerGroup.Points.Where(t => tryBoard[t] == Content.Empty).ToList();
                if (emptyPoints.Count != 2) return false;
                //get end points of content group
                List<Point> endPoints = contentPoints.Where(p => tryBoard.GetStoneNeighbours(p).Intersect(contentPoints).Count() == 1).ToList();
                //both end points connect with one empty point each
                Boolean endConnect = endPoints.All(p => tryBoard.GetStoneNeighbours(p).Intersect(emptyPoints).Count() == 1);
                if (!endConnect) return false;
                //each empty point connect with only one content point
                Boolean emptyConnect = emptyPoints.All(q => tryBoard.GetStoneNeighbours(q).Intersect(contentPoints).Count() == 1);
                if (!emptyConnect) return false;
                return true;
            }
            return false;
        }
        #endregion

        #region ten thousand year ko
        /// <summary>
        /// Check for ten thousand year ko. 
        /// https://senseis.xmp.net/?TenThousandYearKo
        /// <see cref="UnitTestProject.DailyGoProblems.DailyGoProblems_20260813_8" />
        /// <see cref="UnitTestProject.TenThousandYearKoTest.TenThousandYearKoTest_Scenario_XuanXuanGo_Q18500" />
        /// </summary>
        public static Boolean CheckForTenThousandYearKo(Board board)
        {
            GameInfo gi = board.GameInfo;
            if (!gi.IncludeTenThousandYearKo) return false;
            if (gi.Survival != SurviveOrKill.Survive && gi.Survival != SurviveOrKill.KillWithKo) return false;
            foreach (Group kgroup in GroupHelper.GetKillerGroups(board))
            {
                Content c = kgroup.Content;
                List<Group> ngroups = board.GetNeighbourGroups(kgroup);
                if (ngroups.Count != 1) continue;
                Group ngroup = ngroups.First();
                if (kgroup.Points.Count < 7) continue;
                //check target group
                if (!LifeCheck.GetTargets(board).Any(n => n.Equals(ngroup))) continue;
                //check empty points
                List<Point> emptyPoints = kgroup.Points.Where(n => board[n] == Content.Empty).ToList();
                if (emptyPoints.Count != 3) continue;
                //check ten thousand year eye
                Point eye = emptyPoints.FirstOrDefault(p => TenThousandYearKoEye(board, p, c));
                if (eye.IsEmpty()) continue;
                //check content points
                List<Point> contentPoints = kgroup.Points.Where(n => board[n] == c).ToList();
                if (board.GetGroupsFromPoints(contentPoints).Count != 2) continue;
                //check points in empty space
                emptyPoints.Remove(eye);
                if (emptyPoints.Any(n => board.CornerPoint(n))) continue;
                if (!emptyPoints.Any(n => board.GetGroupsFromStoneNeighbours(n, c).Contains(ngroup))) continue;
                if (!board.GetStoneNeighbours(emptyPoints[0]).Contains(emptyPoints[1])) continue;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Ten thousand year ko eye.
        /// </summary>
        /*
15 O O O O O O . . . . . . . . . . . . . 
16 X X X X X O . . . . . . . . . . . . . 
17 X O O . X O . O . . . . . . . . . . . 
18 O . O . X O . . . . . . . . . . . . .
        */
        public static Boolean TenThousandYearKoEye(Board board, Point p, Content c)
        {
            if (board.PointWithinMiddleArea(p))
                return false;
            if (!KoHelper.IsKoFight(board, p, c).Item1)
                return false;
            if (board.GetGroupsFromStoneNeighbours(p, c.Opposite()).Count != 2)
                return false;
            return true;
        }
        #endregion
    }
}
