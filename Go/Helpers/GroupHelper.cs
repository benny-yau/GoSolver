using System;
using System.Collections.Generic;
using System.Linq;

namespace Go
{
    public class GroupHelper
    {
        /// <summary>
        /// Get killer group fully surrounded by survival stones. Content in parameter refer to target content (usually survival). 
        /// </summary>
        public static List<Group> GetKillerGroups(Board board, Content c = Content.Unknown)
        {
            List<Group> killerGroups = null;
            if (c == Content.Unknown)
                c = GameHelper.GetContentForSurviveOrKill(board.GameInfo, SurviveOrKill.Survive);
            if (board.killerGroup == null || !board.killerGroup.ContainsKey(c))
            {
                //get killer groups
                killerGroups = GetAllKillerGroups(board, c);

                //cache groups in board
                if (board.killerGroup == null) board.killerGroup = new Dictionary<Content, List<Group>>();
                board.killerGroup.Add(c, killerGroups);
            }
            else
            {
                //retrieve from cache
                killerGroups = board.killerGroup[c];
            }
            return killerGroups;
        }

        /// <summary>
        /// Get killer groups fully surrounded by opponent.
        /// </summary>
        public static List<Group> GetAllKillerGroups(Board board, Content content)
        {
            List<Group> killerGroups = new List<Group>();
            Board filledBoard = new Board(board);
            //cover all empty points
            GameInfo gameInfo = board.GameInfo;
            Boolean isKill = (GameHelper.GetContentForSurviveOrKill(gameInfo, SurviveOrKill.Kill) == content.Opposite());
            List<Point> coverPoints = (isKill) ? gameInfo.killMovablePoints : gameInfo.movablePoints;
            List<Point> emptyPoints = coverPoints.Where(p => filledBoard[p] == Content.Empty).ToList();
            emptyPoints.ForEach(p => filledBoard[p] = content.Opposite());

            HashSet<Group> groups = filledBoard.GetGroupsFromPoints(emptyPoints);
            foreach (Group group in groups)
            {
                //find killer groups with no liberties left or surrounded by non movable points
                if (group.Liberties.Count == 0 || (!isKill && group.Liberties.All(n => gameInfo.IsMovablePoint[n.x, n.y] == false) && !LifeCheck.GetTargets(board).Any(t => group.Points.Contains(t.Points.First()))))
                {
                    (Boolean isKillerGroup, List<Group> neighbourGroups) = CheckNeighbourGroupsOfKillerGroup(filledBoard, group, true);
                    if (!isKillerGroup) continue;
                    killerGroups.Add(group);
                }
            }
            return killerGroups;
        }

        /// <summary>
        /// Ensure neighbour groups of killer group are diagonal groups, not separated from one another.
        /// </summary>
        public static (Boolean, List<Group>) CheckNeighbourGroupsOfKillerGroup(Board board, Group killerGroup, Boolean isFilledBoard = false)
        {
            List<Group> neighbourGroups = GetNeighbourGroupsOfKillerGroup(board, killerGroup, isFilledBoard);
            if (neighbourGroups.Count == 0) return (false, null);
            if (neighbourGroups.Count == 1) return (true, neighbourGroups);

            List<Group> diagonalGroups = LinkHelper.GetAllDiagonalGroups(board, neighbourGroups.First(), s => !neighbourGroups.Contains(s));
            if (neighbourGroups.Except(diagonalGroups).Any())
                return (false, null);
            return (true, neighbourGroups);
        }

        /// <summary>
        /// Get neighbour groups of killer group that are not surrounded within the killer group.
        /// </summary>
        public static List<Group> GetNeighbourGroupsOfKillerGroup(Board board, Group killerGroup, Boolean isFilledBoard = false)
        {
            List<Group> neighbourGroups = board.GetNeighbourGroups(killerGroup);
            if (isFilledBoard)
                neighbourGroups.RemoveAll(group => group.Neighbours.All(n => board[n] == killerGroup.Content && board.GetGroupAt(n) == killerGroup));
            else
                neighbourGroups.RemoveAll(group => group.Neighbours.All(n => killerGroup.Points.Contains(n)));
            return neighbourGroups;
        }

        /// <summary>
        /// Get killer group cached in board for single point.
        /// </summary>
        public static Group GetKillerGroupFromCache(Board board, Point p, Content c = Content.Unknown)
        {
            c = (c == Content.Unknown) ? GameHelper.GetContentForSurviveOrKill(board.GameInfo, SurviveOrKill.Survive) : c;
            List<Group> groups = GetKillerGroups(board, c);
            Group group = groups.FirstOrDefault(g => g.Points.Contains(p));
            if (group == null) return null;
            return (c == group.Content.Opposite()) ? group : null;
        }

        /// <summary>
        /// Get killer group of neighbour groups.
        /// </summary>
        public static Group GetKillerGroupOfNeighbourGroups(Board board, Point p, Content c)
        {
            Group killerGroup = GroupHelper.GetKillerGroupFromCache(board, p, c);
            if (killerGroup == null) return null;
            HashSet<Group> neighbourGroups = board.GetGroupsFromStoneNeighbours(p, c.Opposite());
            if (GroupHelper.GetNeighbourGroupsOfKillerGroup(board, killerGroup).Any(n => neighbourGroups.Contains(n)))
                return killerGroup;
            return null;
        }

        /// <summary>
        /// Is single group within killer group.
        /// </summary>
        public static Boolean IsSingleGroupWithinKillerGroup(Board tryBoard, Group group = null, Boolean checkLiberties = true)
        {
            if (group == null) group = tryBoard.MoveGroup;
            Content c = group.Content;
            Group killerGroup = GroupHelper.GetKillerGroupFromCache(tryBoard, group.Points.First(), c.Opposite());
            if (killerGroup == null || killerGroup.Points.Any(p => !group.Points.Contains(p) && tryBoard[p] == c)) return false;
            if (checkLiberties && tryBoard.GetNeighbourGroups(killerGroup).Any(gr => gr.Liberties.Count == 1)) return false;
            return true;
        }

        /// <summary>
        /// Liberty group requires at least two content points and two empty points.
        /// </summary>
        public static Boolean IsLibertyGroup(Group group, Board board)
        {
            if (group.Content == Content.Empty || group.Points.Count < 4) return false;
            return (group.Points.Count(t => board[t] == group.Content) >= 2 && group.Points.Count(t => board[t] == Content.Empty) >= 2);
        }

        /// <summary>
        /// Increased killer groups.
        /// </summary>
        public static Boolean IncreasedKillerGroups(Board tryBoard, Board currentBoard)
        {
            Content c = tryBoard.MoveGroup.Content;
            int tryCount = GroupHelper.GetKillerGroups(tryBoard, c).Count;
            int currentCount = GroupHelper.GetKillerGroups(currentBoard, c).Count;
            return (tryCount > currentCount);
        }
    }
}
