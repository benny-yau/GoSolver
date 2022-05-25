using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dh = Go.DirectionHelper;

namespace Go
{
    public class WallHelper
    {

        /// <summary>
        /// Check if move creates eye for survival. If all stone and diagonal neighbours is same content or is wall then move is redundant neutral point.
        /// </summary>
        public static Boolean NoEyeForSurvival(Board board, Point eyePoint, Content c = Content.Unknown)
        {
            if (!board.PointWithinBoard(eyePoint)) return false;
            c = (c == Content.Unknown) ? GameHelper.GetContentForSurviveOrKill(board.GameInfo, SurviveOrKill.Survive) : c;
            if (board[eyePoint] == c || IsWall(board, eyePoint))
                return true;

            if (board.GetStoneNeighbours(eyePoint.x, eyePoint.y).Any(n => IsWall(board, n)))
                return true;

            Boolean eyeInMiddleArea = board.PointWithinMiddleArea(eyePoint);
            int diagonalWallCount = 0;
            foreach (Point q in board.GetDiagonalNeighbours(eyePoint.x, eyePoint.y))
            {
                if (IsWall(board, q))
                    diagonalWallCount += 1;
                if (eyeInMiddleArea && diagonalWallCount > 1 || !eyeInMiddleArea && diagonalWallCount > 0)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Check no eye for survival.
        /// </summary>
        public static Boolean NoEyeForSurvivalAtNeighbourPoints(Board tryBoard, Content c = Content.Unknown)
        {
            IEnumerable<Point> neighbourPts = tryBoard.GetStoneAndDiagonalNeighbours();
            if (neighbourPts.Any(q => !NoEyeForSurvival(tryBoard, q, c)))
                return false;
            return true;
        }

        /// <summary>
        /// Wall is either opposite content which is non killable or empty point which is not movable.
        /// </summary>
        public static Boolean IsWall(Board board, Point p, Boolean outsideBoard = false)
        {
            if (!(board.PointWithinBoard(p)))
                return outsideBoard;
            return (IsNonKillableGroup(board, p) || board[p] == Content.Empty && !board.GameInfo.IsMovablePoint[p.x, p.y] == true);
        }

        /// <summary>
        /// Check for presence of any wall neighbours in all four directions. For a point in the middle area, there should be three points to form the wall.        
        /// For example for a wall on the right, there should be one adjacent right point, one on top of the adjacent point, and one below the adjacent point.
        /// </summary>
        public static (Point, Direction) IsWallNeighbour(Board board, Point p)
        {
            Content c = board[p];
            for (int i = 0; i <= dh.DirectionLinkedList.Count - 1; i++)
            {
                //start with top wall
                Point wallPoint = dh.GetPointInDirection(board, p, dh.GetNewDirection(Direction.Up, i), false);
                if (!board.PointWithinBoard(wallPoint)) continue;
                Point wallOppositePoint = dh.GetPointInDirection(board, p, dh.GetNewDirection(Direction.Down, i), false);
                if (IsSurvivalTerritory(board, wallOppositePoint, c))
                {
                    Point wallAdjPoint1 = dh.GetPointInDirection(board, wallPoint, dh.GetNewDirection(Direction.Left, i), false);
                    Point wallAdjPoint2 = dh.GetPointInDirection(board, wallPoint, dh.GetNewDirection(Direction.Right, i), false);
                    if ((!board.PointWithinMiddleArea(p) || IsWall(board, wallPoint)) && IsWall(board, wallAdjPoint1, true) && IsWall(board, wallAdjPoint2, true))
                        return (wallPoint, dh.DirectionLinkedList[i].Move);
                }
            }
            return (Game.PassMove, Direction.None);
        }

        /// <summary>
        /// Check territory behind point belongs to survival so as to ensure point is facing correct wall.
        /// </summary>
        public static Boolean IsSurvivalTerritory(Board board, Point p, Content c)
        {
            return board.PointWithinBoard(p) && (board.GameInfo.IsMovablePoint[p.x, p.y] == true || board[p] == c);
        }
        
        /// <summary>
        /// Non killable group cannot be surrounded and killed as neighbour points are not movable.
        /// </summary>
        public static Boolean IsNonKillableGroup(Board board, Point p)
        {
            if (GameHelper.GetContentForSurviveOrKill(board.GameInfo, SurviveOrKill.Kill) != board[p])
                return false;
            Group group = board.GetGroupAt(p);
            return IsNonKillableGroup(board, group);
        }

        public static Boolean IsNonKillableGroup(Board board, Group group)
        {
            if (group.IsNonKillable != null) return group.IsNonKillable.Value;

            if (GameHelper.GetContentForSurviveOrKill(board.GameInfo, SurviveOrKill.Kill) != group.Content)
            {
                group.IsNonKillable = false;
                return false;
            }
            
            //check if group is non killable
            group.IsNonKillable = IsNonKillableFromSetupMoves(board, group);
            if (group.IsNonKillable.Value)
                return true;

            //search all connected groups if non killable
            HashSet<Group> groups = LinkHelper.GetAllDiagonalConnectedGroups(board, group);
            groups.Remove(group);
            Boolean nonKillable = groups.Any(g => IsNonKillableFromSetupMoves(board, g));
            group.IsNonKillable = nonKillable;
            groups.ToList().ForEach(g => g.IsNonKillable = nonKillable);

            return nonKillable;
        }

        /// <summary>
        /// Check if non-killable at neighbour points that are empty.
        /// </summary>
        public static Boolean IsNonKillableFromSetupMoves(Board board, Group group)
        {
            return group.Neighbours.Any(p => board[p] == Content.Empty && board.GameInfo.IsKillMovablePoint[p.x, p.y] != true);
        }

        /// <summary>
        /// Strong neighbour groups with more than two liberties or two liberties that are suicidal to opponent.
        /// </summary>
        public static Boolean StrongNeighbourGroups(Board board, IEnumerable<Group> neighbourGroups, Boolean checkSuicidal = true)
        {
            if (!neighbourGroups.Any()) return false;
            if (neighbourGroups.Any(group => !IsStrongNeighbourGroup(board, group, checkSuicidal)))
                return false;
            return true;
        }

        public static Boolean IsStrongNeighbourGroup(Board board, Group group, Boolean checkSuicidal = true)
        {
            Content c = group.Content;
            int liberties = group.Liberties.Count;
            if (liberties < 2) return false;

            if (!checkSuicidal && group.Liberties.Count == 2)
            {
                Boolean opponentMovable = group.Liberties.All(liberty => ImmovableHelper.IsSuicidalMove(board, liberty, c.Opposite()));
                if (opponentMovable) return true;
                return false;
            }

            if (ImmovableHelper.CheckConnectAndDie(board, group)) return false;
            return true;
        }
    }
}
