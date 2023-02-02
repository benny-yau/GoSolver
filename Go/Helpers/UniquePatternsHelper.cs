using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dh = Go.DirectionHelper;

namespace Go
{
    /// <summary>
    /// Unique patterns such as BentFour and Ten Thousand Year Ko have to identified and return the correct result that can be different from calculated result.
    /// </summary>
    public class UniquePatternsHelper
    {
        /// <summary>
        /// Bent four is a unique scenario where it appears to be ko alive but is essentially dead. 
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

        /// <summary>
        /// The ten thousand year ko appears to be ko alive but is essentially alive. 
        /// https://senseis.xmp.net/?TenThousandYearKo
        /// <see cref="UnitTestProject.TenThousandYearKoTest.TenThousandYearKoTest_Scenario_XuanXuanGo_Q18500" />
        /// </summary>
        public static Boolean CheckForTenThousandYearKo(Game game)
        {
            Board board = game.Board;
            List<Group> killerGroups = GroupHelper.GetKillerGroups(board);
            if (killerGroups.Count != 1)
                return false;
            //check for only one neighbour group
            Group killerGroup = killerGroups[0];
            List<Group> survivalGroups = board.GetNeighbourGroups(killerGroup);
            if (survivalGroups.Count != 1)
                return false;
            //at least 7 points in killer group with 3 empty points
            if (killerGroup.Points.Count >= 7)
            {
                List<Point> emptyPoints = killerGroup.Points.Where(m => board[m] == Content.Empty).ToList();
                //ensure at least 3 external liberties 
                if (emptyPoints.Count == 3 && survivalGroups.First().Liberties.Except(emptyPoints).Count() >= 3)
                {
                    //one ten thousand year eye plus one empty group with two points
                    List<Point> eyeFound = emptyPoints.Where(p => TenThousandYearKoEye(board, p, killerGroup.Content)).ToList();
                    if (eyeFound.Count == 1 && emptyPoints.Except(eyeFound).All(e => !board.CornerPoint(e) && board.GetStoneNeighbours(e).Count(n => board[n] == Content.Empty) == 1))
                        return true;
                }
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
        currentDirection == Direction.Up
        */
        public static Boolean TenThousandYearKoEye(Board board, Point p, Content c)
        {
            if (!EyeHelper.FindEye(board, p, c)) return false;
            int isOppositeContent = 0;
            //ensure eye found at the edge only
            if (board.PointWithinMiddleArea(p)) //middle area
                return false;

            Direction currentDirection;
            for (int i = 0; i <= dh.DirectionLinkedList.Count - 1; i++)
            {
                //start with eye at bottom edge
                currentDirection = dh.GetNewDirection(Direction.Up, i);
                Point upPoint = dh.GetPointInDirection(board, p, currentDirection, false);
                if (dh.IsEdgeInDirection(board, p, currentDirection.Opposite()))
                {
                    Point leftPoint = dh.GetPointInDirection(board, upPoint, dh.GetNewDirection(Direction.Left, i));
                    if (leftPoint.Equals(Game.PassMove)) return false;

                    Point rightPoint = dh.GetPointInDirection(board, upPoint, dh.GetNewDirection(Direction.Right, i));
                    if (rightPoint.Equals(Game.PassMove)) return false;

                    if (board[leftPoint] == c.Opposite())
                    {
                        isOppositeContent += 1;
                        Point leftLeftPoint = dh.GetPointInDirection(2, board, p, dh.GetNewDirection(Direction.Left, i));
                        if (!leftLeftPoint.Equals(Game.PassMove) && board[leftLeftPoint] != c.Opposite())
                            return false;
                        if (board[rightPoint] != c)
                            return false;
                        break;
                    }
                    if (board[rightPoint] == c.Opposite())
                    {
                        isOppositeContent += 1;
                        Point rightRightPoint = dh.GetPointInDirection(2, board, p, dh.GetNewDirection(Direction.Right, i));
                        if (!rightRightPoint.Equals(Game.PassMove) && board[rightRightPoint] != c.Opposite())
                            return false;
                        if (board[leftPoint] != c)
                            return false;
                        break;
                    }
                }
            }

            if (isOppositeContent == 1)
                return true;

            return false;
        }


    }
}
