using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public static Boolean IsNonKillableGroup(Board board, Group targetGroup = null)
        {
            if (targetGroup == null) targetGroup = board.MoveGroup;
            Group group = board.GetCurrentGroup(targetGroup);
            if (GameHelper.GetContentForSurviveOrKill(board.GameInfo, SurviveOrKill.Kill) != group.Content) return false;
            if (group.IsNonKillable != null) return group.IsNonKillable.Value;

            //check if group is non killable
            group.IsNonKillable = IsNonKillableFromSetupMoves(board, group);
            if (group.IsNonKillable.Value)
                return true;

            //search all connected groups if non killable
            Func<Group, Boolean> func = s => (s.IsNonKillable != null) ? s.IsNonKillable.Value : false || IsNonKillableFromSetupMoves(board, s);
            HashSet<Group> groups = LinkHelper.GetAllDiagonalConnectedGroups(board, group, func);
            groups.Remove(group);
            Boolean nonKillable = groups.Any(s => func(s));
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
        /// Strong neighbour groups that cannot be captured by connect and die.
        /// </summary>
        public static Boolean StrongNeighbourGroups(Board board, IEnumerable<Group> neighbourGroups)
        {
            if (!neighbourGroups.Any()) return true;
            if (neighbourGroups.Any(group => !IsStrongNeighbourGroup(board, group)))
                return false;
            return true;
        }

        public static Boolean StrongNeighbourGroups(Board board, Point move, Content c)
        {
            HashSet<Group> neighbourGroups = board.GetGroupsFromStoneNeighbours(move, c);
            return StrongNeighbourGroups(board, neighbourGroups);
        }

        public static Boolean StrongNeighbourGroups(Board board, Group group = null)
        {
            if (group == null) group = board.MoveGroup;
            List<Group> neighbourGroups = board.GetNeighbourGroups(group);
            return StrongNeighbourGroups(board, neighbourGroups);
        }


        public static Boolean IsStrongNeighbourGroup(Board board, Group group)
        {
            int liberties = group.Liberties.Count;
            if (liberties < 2) return false;
            if (ImmovableHelper.CheckConnectAndDie(board, group)) return false;
            return true;
        }

        /// <summary>
        /// Hostile neighbour groups with two liberties that are suicidal to opponent.
        /// </summary>
        public static Boolean HostileNeighbourGroups(Board board, Point move, Content c)
        {
            HashSet<Group> neighbourGroups = board.GetGroupsFromStoneNeighbours(move, c);
            if (neighbourGroups.Any(n => !IsHostileNeighbourGroup(board, n))) return false;
            return true;
        }

        public static Boolean IsHostileNeighbourGroup(Board board, Group group = null)
        {
            if (group == null) group = board.MoveGroup;
            if (group.Liberties.Count > 2) return true;
            if (group.Liberties.Count == 2 && group.Liberties.All(liberty => ImmovableHelper.IsSuicidalMove(liberty, group.Content.Opposite(), board, true).Item1))
                return true;
            return false;
        }

        /// <summary>
        /// Target with all non killable groups.
        /// </summary>
        public static Boolean TargetWithAllNonKillableGroups(Board board, Group group = null)
        {
            if (group == null) group = board.MoveGroup;
            if (board.GetNeighbourGroups(group).All(n => WallHelper.IsNonKillableGroup(board, n)))
                return true;
            return false;
        }

        /// <summary>
        /// Target with any non killable group.
        /// </summary>
        public static Boolean TargetWithAnyNonKillableGroup(Board board, Group group = null)
        {
            if (group == null) group = board.MoveGroup;
            if (board.GetNeighbourGroups(group).Any(n => WallHelper.IsNonKillableGroup(board, n)))
                return true;
            return false;
        }

        /// <summary>
        /// Target with ko fight at all non killable groups.
        /// </summary>
        public static Boolean TargetWithKoFightAtAllNonKillableGroups(Board board, Group group = null)
        {
            if (group == null) group = board.MoveGroup;
            if (board.GetNeighbourGroups(group).All(n => WallHelper.IsNonKillableGroup(board, n) || KoHelper.IsNonKillableGroupKoFight(board, n) || (n.Liberties.Count == 1 && !KoHelper.IsKoFight(board, n))))
                return true;
            return false;
        }

        /// <summary>
        /// All target within non killable groups.
        /// </summary>
        public static Boolean AllTargetWithinNonKillableGroups(Board board)
        {
            foreach (Group targetGroup in LifeCheck.GetTargets(board))
            {
                if (LinkHelper.CheckAllDiagonalGroups(board, targetGroup, t => !WallHelper.TargetWithKoFightAtAllNonKillableGroups(board, t)))
                    return false;
            }
            return true;
        }
    }
}
