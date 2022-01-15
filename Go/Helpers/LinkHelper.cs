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
        /// Check for double linkage <see cref="UnitTestProject.LinkHelperTest.LinkHelperTest_Scenario_TianLongTu_Q16571_3" />
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

                //check for double linkage
                foreach (Point p in diagonals)
                {
                    if (!board.PointWithinMiddleArea(p)) continue;
                    //ensure three opponent groups
                    List<Point> opponentStones = board.GetStoneNeighbours(p.x, p.y).Where(n => board[n] == c).ToList();
                    if (board.GetGroupsFromPoints(opponentStones).Count != 3) continue;
                    Point q = diagonals.First(d => !d.Equals(p));
                    //find the other diagonal
                    List<Point> otherDiagonals = board.GetDiagonalNeighbours(p.x, p.y).Where(n => board.GetStoneNeighbours(n.x, n.y).Intersect(opponentStones).Count() >= 2).ToList();
                    Point otherDiagonal = otherDiagonals.Where(d => !d.Equals(q)).FirstOrDefault();
                    if (!Convert.ToBoolean(otherDiagonal.NotEmpty)) continue;

                    //make opponent move at diagonal
                    (Boolean suicidal, Board b) = ImmovableHelper.IsSuicidalMove(p, c.Opposite(), board, true);
                    if (suicidal) continue;

                    //check if immovable already
                    if (!ImmovableAtLinkDiagonal(b, c, otherDiagonal))
                        return false;
                }
                return true;
            }
            //check if immovable for any diagonal separated by opposite content
            if (diagonals.Any(diagonal => ImmovableAtLinkDiagonal(board, c, diagonal)))
                return true;
            return false;
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
        /// Get diagonal neighbours of group. Ensure connected parameter to check if is immovable for opponent at any of the two points between the diagonals.
        /// </summary>
        public static void GetAllDiagonalConnectedGroups(Board board, Group group, HashSet<Group> groups)
        {
            groups.Add(group);
            List<LinkedPoint<Point>> diagonalPoints = GetGroupLinkedDiagonals(board, group);
            foreach (LinkedPoint<Point> diagonalPoint in diagonalPoints)
            {
                Group g = board.GetGroupAt(diagonalPoint.Move);
                if (groups.Contains(g)) continue;
                groups.Add(g);
                GetAllDiagonalConnectedGroups(board, g, groups);
            }
        }
        public static HashSet<Group> GetAllDiagonalConnectedGroups(Board board, Group group)
        {
            HashSet<Group> groups = new HashSet<Group>();
            GetAllDiagonalConnectedGroups(board, group, groups);
            return groups;
        }

        /// <summary>
        /// Get linked diagonals of group.
        /// </summary>
        private static List<LinkedPoint<Point>> GetGroupLinkedDiagonals(Board board, Group group)
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
                    if (CheckIsDiagonalLinked(p, q, board))
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
        public static Boolean IsDiagonallyConnectedGroups(Board board, Group group, Group findGroup, HashSet<Group> groups)
        {
            groups.Add(group);
            List<LinkedPoint<Point>> diagonalPoints = GetGroupLinkedDiagonals(board, group);
            foreach (LinkedPoint<Point> diagonalPoint in diagonalPoints)
            {
                Group g = board.GetGroupAt(diagonalPoint.Move);
                if (findGroup == g)
                    return true;

                if (groups.Contains(g)) continue;
                groups.Add(g);
                Boolean connected = IsDiagonallyConnectedGroups(board, g, findGroup, groups);
                if (connected) return true;
            }
            return false;
        }

        public static Boolean IsDiagonallyConnectedGroups(Board board, Group group, Group findGroup)
        {
            return IsDiagonallyConnectedGroups(board, group, findGroup, new HashSet<Group>());
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
