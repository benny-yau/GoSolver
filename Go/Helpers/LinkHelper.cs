using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Go
{
    public class LinkHelper
    {

        /// <summary>
        /// Check if move links two or more groups together which are not previously linked in current board.
        /// <see cref="UnitTestProject.BaseLineSurvivalMoveTest.BaseLineSurvivalMoveTest_Scenario5dan25" />
        /// <see cref="UnitTestProject.BaseLineSurvivalMoveTest.BaseLineSurvivalMoveTest_Scenario_XuanXuanGo_Q18358" />
        /// <see cref="UnitTestProject.NeutralPointMoveTest.NeutralPointMoveTest_Scenario_XuanXuanQiJing_Weiqi101_18497" />
        /// </summary>
        public static Boolean LinkForGroups(Board tryBoard, Board currentBoard)
        {
            Point move = tryBoard.Move.Value;
            Content c = tryBoard[move];

            List<Point> groupPoints = currentBoard.GetStoneAndDiagonalNeighbours(move.x, move.y).Where(n => tryBoard[n] == c).ToList();
            List<Group> groups = currentBoard.GetGroupsFromPoints(groupPoints).ToList();
            if (groups.Count <= 1) return false;
            for (int i = 0; i <= groups.Count - 2; i++)
            {
                for (int j = (i + 1); j <= groups.Count - 1; j++)
                {
                    if (groups[i] == groups[j]) continue;
                    //check if previously linked
                    Boolean previousLinked = IsDiagonallyConnectedGroups(currentBoard, groups[i], groups[j]);
                    if (previousLinked) continue;
                    Group groupI = tryBoard.GetGroupAt(groups[i].Points.First());
                    Group groupJ = tryBoard.GetGroupAt(groups[j].Points.First());
                    if (groupI == groupJ) return true;

                    //check if currently linked
                    Boolean isLinked = IsDiagonallyConnectedGroups(tryBoard, groupI, groupJ);
                    if (isLinked) return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Is linked by stone only, ignoring whether empty points are immovable.
        /// </summary>
        public static Boolean IsAbsoluteLinkForGroups(Board currentBoard, Board tryBoard)
        {
            Point move = tryBoard.Move.Value;
            Content c = tryBoard[move];
            if (tryBoard.IsSinglePoint()) return false;
            List<Group> linkedGroups = currentBoard.GetGroupsFromStoneNeighbours(move, c.Opposite()).ToList();
            return (linkedGroups.Count > 1);
        }

        /// <summary>
        /// Check if diagonals are linked either by same content or immovable.
        /// Both diagonals empty <see cref="UnitTestProject.LifeCheckTest.LifeCheckTest_Scenario_TianLongTu_Q16571_4" />
        /// <see cref="UnitTestProject.LinkHelperTest.LinkHelperTest_Scenario_TianLongTu_Q16571_2" />
        /// <see cref="UnitTestProject.LinkHelperTest.LinkHelperTest_Scenario_TianLongTu_Q17078" />
        /// </summary>
        public static Boolean CheckIsDiagonalLinked(Point pointA, Point pointB, Board board)
        {
            Content c = board[pointA];
            List<Point> diagonals = LinkHelper.PointsBetweenDiagonals(pointA, pointB);
            //if any diagonal is same content then is linked
            if (diagonals.Any(d => board[d] == c))
                return true;

            //if both diagonals empty then is linked
            if (diagonals.All(d => board[d] == Content.Empty))
            {
                //any diagonal is immovable
                foreach (Point p in diagonals)
                {
                    //make opponent move at diagonal
                    (Boolean suicidal, Board b) = ImmovableHelper.IsSuicidalMove(p, c.Opposite(), board, true);
                    if (suicidal) return true;
                    Point q = diagonals.First(d => !d.Equals(p));
                    //make connection at other diagonal
                    if (ImmovableHelper.IsSuicidalMove(q, c, b).Item1)
                        return false;
                    //check if immovable already
                    if (ImmovableAtLinkDiagonal(b, c, q))
                        return true;
                }
                return true;
            }
            //check if immovable for any diagonal separated by opposite content
            if (diagonals.Any(diagonal => ImmovableAtLinkDiagonal(board, c, diagonal)))
                return true;
            return false;
        }

        /// <summary>
        /// Check for double linkage.
        /// <see cref="UnitTestProject.LinkHelperTest.LinkHelperTest_Scenario_TianLongTu_Q16571_2" />
        /// <see cref="UnitTestProject.LinkHelperTest.LinkHelperTest_Scenario_TianLongTu_Q16571_3" />
        /// </summary>
        private static Boolean CheckDoubleLinkage(Board board, LinkedPoint<Point> diagonalLink, IEnumerable<Group> groups)
        {
            List<Group> threeGroups = groups.Skip(groups.Count() - 3).Take(3).ToList();
            if (threeGroups.Count != 3) return true;
            Content c = threeGroups.First().Content;
            List<Point> diagonals = LinkHelper.PointsBetweenDiagonals(diagonalLink.Move, (Point)diagonalLink.CheckMove);
            foreach (Point p in diagonals)
            {
                //ensure three opponent groups
                List<Point> opponentStones = board.GetStoneNeighbours(p.x, p.y).Where(n => board[n] == c).ToList();
                HashSet<Group> neighbourGroups = board.GetGroupsFromPoints(opponentStones);
                if (neighbourGroups.Count != 3 || neighbourGroups.Any(group => !threeGroups.Contains(group))) continue;

                //make opponent move at diagonal
                (Boolean suicidal, Board b) = ImmovableHelper.IsSuicidalMove(p, c.Opposite(), board, true);
                if (suicidal) continue;

                //check if any of the other two diagonals are immovable
                List<Point> otherDiagonals = board.GetDiagonalNeighbours(p.x, p.y).Where(n => board.GetStoneNeighbours(n.x, n.y).Intersect(opponentStones).Count() >= 2).ToList();
                if (otherDiagonals.All(d => !ImmovableAtLinkDiagonal(b, c, d)))
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Immovable at link diagonal.
        /// Check for double tiger mouth <see cref="UnitTestProject.LinkHelperTest.LinkHelperTest_Scenario_TianLongTu_Q16571" />
        /// <see cref="UnitTestProject.LinkHelperTest.LinkHelperTest_Scenario_WindAndTime_Q30150" />
        /// </summary>
        private static Boolean ImmovableAtLinkDiagonal(Board board, Content c, Point diagonal)
        {
            (Boolean immovable, Point? isTigerMouth) = ImmovableHelper.IsImmovablePoint(diagonal, c, board);
            if (!immovable) return false;
            if (isTigerMouth == null) return true;
            //check for double tiger mouth
            if (!LifeCheck.DoubleTigerMouthLink(board, c, diagonal, isTigerMouth.Value)) return true;
            return false;
        }


        /// <summary>
        /// Get all diagonal connected groups. Ensure connected parameter to check if is immovable for opponent at any of the two points between the diagonals.
        /// </summary>
        public static void GetAllDiagonalConnectedGroups(Board board, Group group, HashSet<Group> groups)
        {
            HashSet<Group> allConnectedGroups = GetAllDiagonalConnectedGroups(board, group);
            groups.UnionWith(allConnectedGroups);
        }

        public static HashSet<Group> GetAllDiagonalConnectedGroups(Board board, Group group)
        {
            HashSet<Group> allConnectedGroups = new HashSet<Group>() { group };
            IsDiagonallyConnectedGroups(allConnectedGroups, new HashSet<Group>(), board, group);
            return allConnectedGroups;
        }

        /// <summary>
        /// Get all diagonal connected groups including eyes. 
        /// </summary>
        public static HashSet<Group> GetAllDiagonalConnectedGroupsIncludingEyes(Board board, HashSet<Group> groups)
        {
            HashSet<Point> liberties = board.GetLibertiesOfGroups(groups.ToList());
            foreach (Point liberty in liberties)
            {
                if (EyeHelper.FindEye(board, liberty, groups.First().Content))
                    board.GetStoneNeighbours(liberty.x, liberty.y).Where(n => board.GetGroupAt(n).Points.Count == 1).ToList().ForEach(x => groups.Add(board.GetGroupAt(x)));
            }
            return groups;
        }

        /// <summary>
        /// Get linked diagonals of group.
        /// </summary>
        public static List<LinkedPoint<Point>> GetGroupLinkedDiagonals(Board board, Group group, Boolean checkLinked = true)
        {
            List<LinkedPoint<Point>> rc = new List<LinkedPoint<Point>>();
            Content c = group.Content;
            foreach (Point p in group.Points)
            {
                foreach (Point q in board.GetDiagonalNeighbours(p.x, p.y))
                {
                    if (board[q] != c) continue;
                    if (PointsBetweenDiagonals(p, q).Any(r => board[r] == c)) continue;
                    if (board.GetGroupAt(p) == board.GetGroupAt(q)) continue;

                    //ensure diagonal is linked
                    if (!checkLinked || CheckIsDiagonalLinked(p, q, board))
                        rc.Add(new LinkedPoint<Point>(q, p));
                }
            }
            return rc;
        }

        public static List<LinkedPoint<Point>> GetGroupDiagonals(Board board, Group group)
        {
            List<LinkedPoint<Point>> rc = new List<LinkedPoint<Point>>();
            Content c = group.Content;
            foreach (Point p in group.Points)
            {
                foreach (Point q in board.GetDiagonalNeighbours(p.x, p.y))
                {
                    if (PointsBetweenDiagonals(p, q).Any(r => board[r] == c)) continue;
                    rc.Add(new LinkedPoint<Point>(q, p));
                }
            }
            return rc;
        }

        /// <summary>
        /// Find if two groups are connected diagonally.
        /// </summary>
        public static Boolean IsDiagonallyConnectedGroups(HashSet<Group> allConnectedGroups, HashSet<Group> groups, Board board, Group group, Group findGroup = null)
        {
            groups.Add(group);
            //find group diagonals of same content
            List<LinkedPoint<Point>> diagonalPoints = GetGroupDiagonals(board, group).Where(d => board[d.Move] == group.Content).ToList();
            foreach (LinkedPoint<Point> diagonalPoint in diagonalPoints)
            {
                Group g = board.GetGroupAt(diagonalPoint.Move);
                if (groups.Contains(g)) continue;

                //check if diagonally linked
                if (!CheckIsDiagonalLinked(diagonalPoint.Move, (Point)diagonalPoint.CheckMove, board))
                    continue;

                //check for links with double linkage
                if (!CheckDoubleLinkage(board, diagonalPoint, groups.Union(new HashSet<Group> { g })))
                    continue;

                allConnectedGroups.Add(g);

                //check if group found
                if (findGroup != null && findGroup == g)
                    return true;

                //get diagonal connected groups recursively
                Boolean connected = IsDiagonallyConnectedGroups(allConnectedGroups, new HashSet<Group>(groups), board, g, findGroup);
                if (connected) return true;
            }
            return false;
        }

        public static Boolean IsDiagonallyConnectedGroups(Board board, Group group, Group findGroup)
        {
            return IsDiagonallyConnectedGroups(new HashSet<Group>() { group }, new HashSet<Group>(), board, group, findGroup);
        }

        /// <summary>
        /// Get the opposite diagonals of the two diagonal points.
        /// </summary>
        public static List<Point> PointsBetweenDiagonals(Point p, Point q)
        {
            List<Point> diagonalPoints = new List<Point>();
            int diff_x = Math.Abs(p.x - q.x);
            int diff_y = Math.Abs(p.y - q.y);

            if (diff_x == 1 && diff_y == 1)
            {
                diagonalPoints.Add(new Point(p.x, q.y));
                diagonalPoints.Add(new Point(q.x, p.y));
            }
            return diagonalPoints;
        }

        /// <summary>
        /// Diagonal cut between two neighbour groups.
        /// </summary>
        public static (Point?, List<Point>) FindDiagonalCut(Board board, Group group)
        {
            Content c = group.Content;
            List<LinkedPoint<Point>> diagonals = GetGroupDiagonals(board, group).Where(d => board[d.Move] == c).ToList();
            foreach (LinkedPoint<Point> diagonal in diagonals)
            {
                List<Point> pointsBetweenDiagonals = PointsBetweenDiagonals(diagonal.Move, (Point)diagonal.CheckMove);
                if (pointsBetweenDiagonals.All(d => board[d] == c.Opposite()) && board.GetGroupAt(diagonal.Move).Liberties.Count > 1)
                    return (diagonal.Move, pointsBetweenDiagonals);
            }
            return (null, null);
        }

        /// <summary>
        /// Get stones within move group on current board.
        /// </summary>
        public static List<Group> GetPreviousMoveGroup(Board currentBoard, Board tryBoard)
        {
            if (currentBoard == null || tryBoard == null) return new List<Group>();
            Point p = tryBoard.Move.Value;
            Content c = tryBoard.MoveGroup.Content;
            return currentBoard.GetGroupsFromStoneNeighbours(p, c.Opposite()).ToList();
        }

    }
}
