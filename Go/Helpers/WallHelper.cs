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
            c = (c == Content.Unknown) ? GameHelper.GetContentForSurviveOrKill(board.GameInfo, SurviveOrKill.Survive) : c;
            if (board[eyePoint] == c || IsWall(board, eyePoint, c))
                return true;

            if (board.GetStoneNeighbours(eyePoint).Any(n => IsWall(board, n, c)))
                return true;

            Boolean eyeInMiddleArea = board.PointWithinMiddleArea(eyePoint);
            int diagonalWallCount = 0;
            foreach (Point q in board.GetDiagonalNeighbours(eyePoint))
            {
                if (IsWall(board, q, c)) diagonalWallCount += 1;
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
        /// Wall is either non killable or is empty point or opponent point which is not movable.
        /// </summary>
        public static Boolean IsWall(Board board, Point p, Content c = Content.Unknown)
        {
            Content surviveContent = GameHelper.GetContentForSurviveOrKill(board.GameInfo, SurviveOrKill.Survive);
            c = (c == Content.Unknown) ? surviveContent : c;
            if (c == surviveContent && IsNonKillableGroup(board, p)) return true;
            if (board[p] != c && !GameHelper.SetupMoveAvailable(board, p, c)) return true;
            return false;
        }

        /// <summary>
        /// Non killable group cannot be surrounded and killed as neighbour points are not movable.
        /// </summary>
        public static Boolean IsNonKillableGroup(Board board, Point p)
        {
            if (GameHelper.GetContentForSurviveOrKill(board.GameInfo, SurviveOrKill.Kill) != board[p]) return false;
            return IsNonKillableGroup(board, board.GetGroupAt(p));
        }

        public static Boolean IsNonKillableGroup(Board board, Group targetGroup = null)
        {
            if (targetGroup == null) targetGroup = board.MoveGroup;
            Group group = board.GetCurrentGroup(targetGroup);
            if (GameHelper.GetContentForSurviveOrKill(board.GameInfo, SurviveOrKill.Kill) != group.Content) return false;
            if (group.IsNonKillable != null) return group.IsNonKillable.Value;

            //check if group is non killable
            Func<Group, Boolean> func = s => (s.IsNonKillable != null) ? s.IsNonKillable.Value : false || IsNonKillableFromSetupMoves(board, s);
            group.IsNonKillable = func(group);
            if (group.IsNonKillable.Value) return true;

            //search all connected groups if non killable
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
        /// Strong groups that cannot be captured by connect and die.
        /// </summary>
        public static Boolean StrongGroups(Board board, IEnumerable<Group> ngroups)
        {
            if (ngroups.Any(n => !IsStrongGroup(board, n)))
                return false;
            return true;
        }

        public static Boolean IsStrongGroup(Board board, Group group = null)
        {
            if (group == null) group = board.MoveGroup;
            if (group.Liberties.Count < 2 || ImmovableHelper.TwoAndThreeLibertiesConnectAndDie(board, group))
                return false;
            return true;
        }

        public static Boolean StrongNeighbourGroups(Board board, Point move, Content c)
        {
            HashSet<Group> ngroups = board.GetGroupsFromStoneNeighbours(move, c);
            return StrongGroups(board, ngroups);
        }

        public static Boolean StrongNeighbourGroups(Board board, Group group = null)
        {
            if (group == null) group = board.MoveGroup;
            List<Group> ngroups = board.GetNeighbourGroups(group);
            return StrongGroups(board, ngroups);
        }

        /// <summary>
        /// Hostile neighbour groups with two liberties that are suicidal to opponent.
        /// </summary>
        public static Boolean HostileNeighbourGroups(Board board, Point move, Content c)
        {
            HashSet<Group> ngroups = board.GetGroupsFromStoneNeighbours(move, c);
            if (ngroups.Any(n => !IsHostileNeighbourGroup(board, n))) return false;
            return true;
        }

        public static Boolean IsHostileNeighbourGroup(Board board, Group targetGroup = null)
        {
            if (targetGroup == null) targetGroup = board.MoveGroup;
            Group group = board.GetCurrentGroup(targetGroup);
            if (group.Liberties.Count > 2) return true;
            if (group.Liberties.Count == 2 && group.Liberties.All(liberty => ImmovableHelper.IsSuicidalMove(board, liberty, group.Content.Opposite(), true)))
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
            if (board.GetNeighbourGroups(group).All(n => WallHelper.IsNonKillableGroup(board, n) || KoHelper.IsNonKillableGroupKoFight(board, n)))
                return true;
            return false;
        }

        /// <summary>
        /// All target within non killable groups.
        /// </summary>
        public static Boolean AllTargetWithinNonKillableGroups(Board board, Group targetGroup)
        {
            if (LinkHelper.CheckAllDiagonalGroups(board, targetGroup, t => !WallHelper.TargetWithKoFightAtAllNonKillableGroups(board, t)))
                return false;
            return true;
        }

        /// <summary>
        /// Strong groups at covered board.
        /// </summary>
        public static Boolean StrongGroupsAtCoveredBoard(Board board, Group targetGroup)
        {
            Content c = targetGroup.Content;
            List<Group> groups = LinkHelper.GetAllDiagonalGroups(board, targetGroup).ToList();
            Board coveredBoard = new Board(board);
            //cover external liberties
            foreach (Point p in board.GetLibertiesOfGroups(groups))
            {
                Group killerGroup = GroupHelper.GetKillerGroupFromCache(board, p, c);
                if (killerGroup != null && killerGroup.Points.Count <= 3) continue;
                coveredBoard[p] = c.Opposite();
            }
            //check for connect and die
            if (groups.Select(n => coveredBoard.GetCurrentGroup(n)).Any(n => !IsStrongGroup(coveredBoard, n))) return false;
            return true;
        }
    }
}
