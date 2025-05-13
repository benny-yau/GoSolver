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
            if (board.KillerGroups == null || !board.KillerGroups.ContainsKey(c))
                killerGroups = GetAllKillerGroups(board, c);
            else
                killerGroups = board.KillerGroups[c]; //retrieve from cache
            return killerGroups;
        }

        /// <summary>
        /// Get killer groups fully surrounded by opponent.
        /// </summary>
        private static List<Group> GetAllKillerGroups(Board board, Content c)
        {
            List<Group> killerGroups = new List<Group>();
            Board filledBoard = new Board(board);

            //cache groups in board
            if (board.KillerGroups == null) board.KillerGroups = new Dictionary<Content, List<Group>>();
            board.KillerGroups.Add(c, killerGroups);
            Group[,] cache = new Group[board.SizeX, board.SizeY];
            if (c == Content.Black)
                board.BlackKillerGroupCache = cache;
            else
                board.WhiteKillerGroupCache = cache;

            //cover all empty points
            GameInfo gameInfo = board.GameInfo;
            Boolean isKill = (GameHelper.GetContentForSurviveOrKill(gameInfo, SurviveOrKill.Kill) == c.Opposite());
            List<Point> coverPoints = (isKill) ? gameInfo.killMovablePoints : gameInfo.movablePoints;
            List<Point> emptyPoints = coverPoints.Where(p => filledBoard[p] == Content.Empty).ToList();
            emptyPoints.ForEach(p => filledBoard[p] = c.Opposite());

            HashSet<Group> groups = filledBoard.GetGroupsFromPoints(emptyPoints);
            foreach (Group group in groups)
            {
                //find killer groups with no liberties left or surrounded by non movable points
                if (group.Liberties.Count == 0 || (!isKill && group.Liberties.All(n => gameInfo.IsMovablePoint[n.x, n.y] == false)))
                {
                    killerGroups.Add(group);
                    foreach (Point p in group.Points)
                        cache[p.x, p.y] = group;
                }
            }
            return killerGroups;
        }

        /// <summary>
        /// Ensure neighbour groups of killer group are diagonal groups, not separated from one another.
        /// <see cref="UnitTestProject.FillKoEyeMoveTest.FillKoEyeMoveTest_SimpleSeki_2" />
        /// </summary>
        public static (Boolean, List<Group>) CheckNeighbourGroupsOfKillerGroup(Board board, Group killerGroup)
        {
            List<Group> ngroups = GetNeighbourGroupsOfKillerGroup(board, killerGroup);
            if (ngroups.Count == 0) return (false, null);
            if (ngroups.Count == 1) return (true, ngroups);

            List<Group> diagonalGroups = LinkHelper.GetAllDiagonalGroups(board, ngroups.First(), s => !ngroups.Contains(s));
            if (ngroups.Except(diagonalGroups).Any())
                return (false, null);
            return (true, ngroups);
        }

        /// <summary>
        /// Get neighbour groups of killer group that are not surrounded within the killer group.
        /// </summary>
        public static List<Group> GetNeighbourGroupsOfKillerGroup(Board board, Group killerGroup)
        {
            Content c = killerGroup.Content;
            List<Group> ngroups = board.GetNeighbourGroups(killerGroup);
            ngroups.RemoveAll(gr => gr.Neighbours.All(n => GroupHelper.GetKillerGroupFromCache(board, n, c.Opposite()) == killerGroup));
            return ngroups;
        }

        /// <summary>
        /// Get killer group cached in board for single point.
        /// </summary>
        public static Group GetKillerGroupFromCache(Board board, Point p, Content c = Content.Unknown)
        {
            c = (c == Content.Unknown) ? GameHelper.GetContentForSurviveOrKill(board.GameInfo, SurviveOrKill.Survive) : c;
            List<Group> groups = GetKillerGroups(board, c);
            Group[,] cache = (c == Content.Black) ? board.BlackKillerGroupCache : board.WhiteKillerGroupCache;
            return cache[p.x, p.y];
        }

        /// <summary>
        /// Get killer group of strong neighbour groups.
        /// </summary>
        public static Group GetKillerGroupOfStrongNeighbourGroups(Board board, Point p, Content c)
        {
            Group killerGroup = GroupHelper.GetKillerGroupFromCache(board, p, c);
            if (killerGroup == null) return null;

            List<Group> groups = board.GetNeighbourGroups(killerGroup);
            if (!WallHelper.StrongGroups(board, groups)) return null;
            if (LinkHelper.DoubleAtariOnTargetGroups(board, groups)) return null;
            return killerGroup;
        }

        /// <summary>
        /// Is single group within killer group.
        /// </summary>
        public static Boolean IsSingleGroupWithinKillerGroup(Board tryBoard, Group group = null, Boolean checkLiberties = true)
        {
            if (group == null) group = tryBoard.MoveGroup;
            else group = tryBoard.GetCurrentGroup(group);
            Content c = group.Content;
            Group killerGroup = GroupHelper.GetKillerGroupFromCache(tryBoard, group.Points.First(), c.Opposite());
            if (killerGroup == null || killerGroup.Points.Any(p => tryBoard[p] == c && tryBoard.GetGroupAt(p) != group)) return false;
            if (checkLiberties && tryBoard.GetNeighbourGroups(killerGroup).Any(gr => gr.Liberties.Count == 1)) return false;
            return true;
        }

        /// <summary>
        /// Is liberty group.
        /// </summary>
        public static Boolean IsLibertyGroup(Group killerGroup, Board board)
        {
            if (killerGroup.Content == Content.Empty || killerGroup.Points.Count < 4) return false;
            List<Point> contentPoints = killerGroup.Points.Where(t => board[t] == killerGroup.Content).ToList();
            if (contentPoints.Count < 2) return false;
            if (killerGroup.Points.Count(t => board[t] == Content.Empty) < 2) return false;
            return true;
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
