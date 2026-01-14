using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Go
{
    public class WallHelper
    {
        /// <summary>
        /// No eye for survival. Check if all stone and diagonal neighbours is same content or is wall.
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
        /// No eye for survival at neighbour points.
        /// </summary>
        public static Boolean NoEyeForSurvivalAtNeighbourPoints(Board tryBoard)
        {
            Content c = tryBoard.MoveGroup.Content;
            IEnumerable<Point> neighbourPts = tryBoard.GetStoneAndDiagonalNeighbours();
            if (!WallHelper.IsNonKillableGroup(tryBoard) && neighbourPts.Any(q => !NoEyeForSurvival(tryBoard, q, c)))
                return false;
            return true;
        }

        /// <summary>
        /// Is wall.
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
        /// Is non killable group. Cannot be surrounded and killed as neighbour points are not movable.
        /// </summary>
        public static Boolean IsNonKillableGroup(Board board, Point p)
        {
            if (GameHelper.GetContentForSurviveOrKill(board.GameInfo, SurviveOrKill.Kill) != board[p]) return false;
            return IsNonKillableGroup(board, board.GetGroupAt(p));
        }

        public static Boolean IsNonKillableGroup(Board board, Group group = null)
        {
            if (group == null) group = board.MoveGroup;
            else group = board.GetCurrentGroup(group);
            if (GameHelper.GetContentForSurviveOrKill(board.GameInfo, SurviveOrKill.Kill) != group.Content) return false;
            if (group.IsNonKillable != null) return group.IsNonKillable.Value;

            //check if group is non killable
            Func<Group, Boolean> func = s => (s.IsNonKillable != null) ? s.IsNonKillable.Value : false || IsNonKillableFromSetupMoves(board, s);
            group.IsNonKillable = func(group);
            if (group.IsNonKillable.Value) return true;

            //search all connected groups if non killable
            List<Group> groups = LinkHelper.GetAllDiagonalConnectedGroups(board, group, func).ToList();
            groups.Remove(group);
            Boolean nonKillable = groups.Any(s => func(s));
            group.IsNonKillable = nonKillable;
            groups.ForEach(g => g.IsNonKillable = nonKillable);
            return nonKillable;
        }

        /// <summary>
        /// Is non killable from setup moves.
        /// </summary>
        public static Boolean IsNonKillableFromSetupMoves(Board board, Group group)
        {
            Content c = group.Content;
            return group.Neighbours.Any(p => board[p] == Content.Empty && !GameHelper.SetupMoveAvailable(board, p, c.Opposite()));
        }

        /// <summary>
        /// Strong groups.
        /// </summary>
        public static Boolean StrongGroups(Board board, IEnumerable<Group> ngroups)
        {
            if (ngroups.Any(n => !IsStrongGroup(board, n)))
                return false;
            return true;
        }

        /// <summary>
        /// Is strong group.
        /// </summary>
        public static Boolean IsStrongGroup(Board board, Group group = null)
        {
            if (group == null) group = board.MoveGroup;
            else group = board.GetCurrentGroup(group);
            if (group.Liberties.Count < 2 || ImmovableHelper.TwoAndThreeLibertiesConnectAndDie(board, group))
                return false;
            return true;
        }

        /// <summary>
        /// Strong neighbour groups.
        /// </summary>
        public static Boolean StrongNeighbourGroups(Board board, Point move, Content c)
        {
            List<Group> ngroups = board.GetGroupsFromStoneNeighbours(move, c);
            return StrongGroups(board, ngroups);
        }

        public static Boolean StrongNeighbourGroups(Board board, Group group = null)
        {
            if (group == null) group = board.MoveGroup;
            List<Group> ngroups = board.GetNeighbourGroups(group);
            return StrongGroups(board, ngroups);
        }

        /// <summary>
        /// Is hostile group. Two liberties suicidal to opponent.
        /// </summary>
        public static Boolean IsHostileGroup(Board board, Group group = null)
        {
            if (group == null) group = board.MoveGroup;
            else group = board.GetCurrentGroup(group);
            Content c = group.Content;
            if (group.Liberties.Count > 2) return true;
            if (group.Liberties.Count != 2) return false;
            if (group.Liberties.All(n => ImmovableHelper.IsSuicidalMove(board, n, c.Opposite(), true) || !GameHelper.SetupMoveAvailable(board, n, c.Opposite())))
                return true;
            return false;
        }

        /// <summary>
        /// Target with all non killable groups.
        /// </summary>
        public static Boolean TargetWithAllNonKillableGroups(Board board, Group group = null)
        {
            if (group == null) group = board.MoveGroup;
            if (board.GetNeighbourGroups(group).All(n => IsNonKillableOrKo(board, n)))
                return true;
            return false;
        }

        public static Boolean IsNonKillableOrKo(Board board, Group group = null)
        {
            if (group == null) group = board.MoveGroup;
            else group = board.GetCurrentGroup(group);
            return WallHelper.IsNonKillableGroup(board, group) || KoHelper.IsNonKillableGroupKoFight(board, group);
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
        /// Target attack with killable group.
        /// </summary>
        public static Boolean TargetAttackWithKillableGroup(Board board, IEnumerable<Group> groups)
        {
            if (groups.Count() < 2) return false;
            if (groups.Any(n => !WallHelper.IsNonKillableGroup(board, n)))
                return true;
            return false;
        }

        /// <summary>
        /// Strong groups at covered board. Applies only to covered eye survival.
        /// <see cref="UnitTestProject.LifeCheckTest.LifeCheckTest_20230422_8" />
        /// </summary>
        public static Boolean StrongGroupsAtCoveredBoard(Board board, Group targetGroup)
        {
            Content c = targetGroup.Content;
            List<Group> groups = LinkHelper.GetAllDiagonalGroups(board, targetGroup);
            Board coveredBoard = new Board(board);
            //cover external liberties only
            foreach (Point p in board.GetLibertiesOfGroups(groups))
            {
                if (GroupHelper.CheckKillerGroupPoints(board, p, c, 3, false) != null) continue;
                coveredBoard[p] = c.Opposite();
            }
            return StrongGroups(coveredBoard, groups);
        }
    }
}
