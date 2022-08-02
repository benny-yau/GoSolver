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
        public static List<Group> GetKillerGroups(Board board, Content c = Content.Unknown, Boolean checkCoveredEye = false)
        {
            List<Group> killerGroups = null;
            if (c == Content.Unknown)
                c = GameHelper.GetContentForSurviveOrKill(board.GameInfo, SurviveOrKill.Survive);
            if (board.killerGroup == null || !board.killerGroup.ContainsKey(c))
            {
                //get killer groups
                killerGroups = GetAllKillerGroups(board, c);
                if (killerGroups.Count == 0) return killerGroups;

                //return liberty group as first group
                Group libertyGroup = killerGroups.FirstOrDefault(group => IsLibertyGroup(group, board));
                if (libertyGroup != null)
                {
                    killerGroups.Remove(libertyGroup);
                    killerGroups.Insert(0, libertyGroup);
                }

                //cache groups in board
                if (board.killerGroup == null)
                    board.killerGroup = new Dictionary<Content, List<Group>>();
                board.killerGroup.Add(c, killerGroups);
            }
            else
            {
                //retrieve from cache
                killerGroups = board.killerGroup[c];
            }

            if (checkCoveredEye && killerGroups.Any(group => group.Points.Count <= 2))
            {
                //clear all killer groups with empty points
                Board clearedBoard = new Board(board);
                killerGroups.ForEach(group => group.Points.ToList().ForEach(p => clearedBoard[p] = Content.Empty));
                //remove covered eye
                return killerGroups.Where(group => group.Points.Count > 2 || EyeHelper.FindRealEyeWithinEmptySpace(clearedBoard, group, EyeType.UnCoveredEye)).ToList();
            }
            return killerGroups;
        }

        /// <summary>
        /// Get killer groups fully surrounded by opponent.
        /// Remove where group is covered eye <see cref="UnitTestProject.BothAliveTest.BothAliveTest_Scenario_WindAndTime_Q30005" />
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
                //find killer groups with no liberties left
                if (group.Liberties.Count == 0)
                {
                    if (!CheckNeighbourGroupsOfKillerGroup(filledBoard, group).Item1) continue;
                    killerGroups.Add(group);
                }
            }
            return killerGroups;
        }


        /// <summary>
        /// Ensure neighbour groups of killer group are diagonal groups.
        /// </summary>
        public static (Boolean, List<Group>) CheckNeighbourGroupsOfKillerGroup(Board board, Group killerGroup, Boolean isFilledBoard = true)
        {
            List<Group> neighbourGroups = board.GetNeighbourGroups(killerGroup);
            //remove groups surrounded by killer group
            if (isFilledBoard)
                neighbourGroups.RemoveAll(group => group.Neighbours.All(n => board[n] == killerGroup.Content && board.GetGroupAt(n) == killerGroup));
            else
                neighbourGroups.RemoveAll(group => group.Neighbours.All(n => killerGroup.Points.Contains(n)));
            if (neighbourGroups.Count == 0) return (false, null);
            if (neighbourGroups.Count == 1) return (true, neighbourGroups);
            //get all diagonal groups
            List<Group> diagonalGroups = LinkHelper.GetAllDiagonalGroups(board, neighbourGroups.First());
            if (neighbourGroups.Except(diagonalGroups).Any())
                return (false, null);
            return (true, neighbourGroups);
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
        /// Liberty group requires at least two content points and two empty points.
        /// </summary>
        private static Boolean IsLibertyGroup(Group group, Board board)
        {
            if (group.Content == Content.Empty) return false;
            return (group.Points.Count(t => board[t] == group.Content) >= 2 && group.Points.Count(t => board[t] == Content.Empty) >= 2);
        }

    }
}
