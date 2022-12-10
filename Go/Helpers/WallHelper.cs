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

            if (board.GetStoneNeighbours(eyePoint).Any(n => IsWall(board, n)))
                return true;

            Boolean eyeInMiddleArea = board.PointWithinMiddleArea(eyePoint);
            int diagonalWallCount = 0;
            foreach (Point q in board.GetDiagonalNeighbours(eyePoint))
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
        public static Boolean NoEyeForSurvivalAtNeighbourPoints(Board tryBoard)
        {
            IEnumerable<Point> neighbourPts = tryBoard.GetStoneAndDiagonalNeighbours();
            Content c = tryBoard.MoveGroup.Content;
            if (!WallHelper.IsNonKillableGroup(tryBoard) && neighbourPts.Any(q => !NoEyeForSurvival(tryBoard, q, c)))
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
        /// Non killable group cannot be surrounded and killed as neighbour points are not movable.
        /// </summary>
        public static Boolean IsNonKillableGroup(Board board, Point p)
        {
            if (GameHelper.GetContentForSurviveOrKill(board.GameInfo, SurviveOrKill.Kill) != board[p])
                return false;
            Group group = board.GetGroupAt(p);
            return IsNonKillableGroup(board, group);
        }

        public static Boolean IsNonKillableGroup(Board board, Group group = null)
        {
            if (group == null) group = board.MoveGroup;
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
        public static Boolean StrongNeighbourGroups(Board board, IEnumerable<Group> neighbourGroups, Boolean checkSuicidal = false)
        {
            if (!neighbourGroups.Any()) return true;
            if (neighbourGroups.Any(group => !IsStrongNeighbourGroup(board, group, checkSuicidal)))
                return false;
            return true;
        }

        public static Boolean StrongNeighbourGroups(Board board, Point move, Content c, Boolean checkSuicidal = false)
        {
            HashSet<Group> neighbourGroups = board.GetGroupsFromStoneNeighbours(move, c);
            return StrongNeighbourGroups(board, neighbourGroups, checkSuicidal);
        }

        public static Boolean StrongNeighbourGroups(Board board, Group group, Boolean checkSuicidal = false)
        {
            List<Group> neighbourGroups = board.GetNeighbourGroups(group);
            return StrongNeighbourGroups(board, neighbourGroups, checkSuicidal);
        }


        public static Boolean IsStrongNeighbourGroup(Board board, Group group, Boolean checkSuicidal = false)
        {
            Content c = group.Content;
            int liberties = group.Liberties.Count;
            if (liberties < 2) return false;

            if (checkSuicidal && group.Liberties.Count == 2)
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
