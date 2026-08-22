using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Go
{
    public class LinkHelper
    {
        #region possible link for groups
        /// <summary>
        /// Possible link for groups.
        /// <see cref="UnitTestProject.BaseLineSurvivalMoveTest.BaseLineSurvivalMoveTest_Scenario5dan25" />
        /// <see cref="UnitTestProject.BaseLineSurvivalMoveTest.BaseLineSurvivalMoveTest_Scenario_XuanXuanGo_Q18358" />
        /// <see cref="UnitTestProject.NeutralPointMoveTest.NeutralPointMoveTest_Scenario_XuanXuanQiJing_Weiqi101_18497" />
        /// Check captured groups <see cref="UnitTestProject.LinkHelperTest.LinkHelperTest_Scenario_XuanXuanQiJing_Weiqi101_18497_3" />
        /// Check covered eye <see cref="UnitTestProject.LinkHelperTest.LinkHelperTest_Scenario_WindAndTime_Q30274_2" />
        /// Check ko link <see cref="UnitTestProject.LinkHelperTest.LinkHelperTest_Scenario_WindAndTime_Q30274_3" />
        /// Check opponent suicidal <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_Side_B35" />
        /// </summary>
        public static Boolean PossibleLinkForGroups(Board tryBoard, Board currentBoard)
        {
            Point move = tryBoard.Move.Value;
            Content c = tryBoard.MoveGroup.Content;
            if (tryBoard.CapturedList.Count == 0 && ImmovableHelper.CheckConnectAndDie(tryBoard, tryBoard.MoveGroup, false))
            {
                //check covered eye
                if (!EyeHelper.FindCoveredEyeAtStoneNeighbour(tryBoard).Any())
                    return false;
            }

            //get all possible link groups
            List<Point> npoints = currentBoard.GetStoneAndDiagonalNeighbours(move).Where(n => currentBoard[n] == c).ToList();
            HashSet<Group> ngroups = currentBoard.GetGroupsFromPoints(npoints);

            //check captured groups
            if (tryBoard.CapturedList.Any(n => !ImmovableHelper.UnescapableGroup(currentBoard, n).Item1))
            {
                if (tryBoard.CapturedList.Count > 1)
                    return true;
                Group capturedGroup = tryBoard.CapturedList.First();
                if (currentBoard.GetNeighbourGroups(capturedGroup).Count > 1)
                    return true;
            }

            //get leap groups
            HashSet<Group> leapGroups = GetPossibleLeapGroups(tryBoard, currentBoard);
            ngroups.UnionWith(leapGroups);
            //find possible links between all groups
            List<Group> groups = ngroups.ToList();
            if (groups.Count == 0) return false;
            for (int i = 0; i <= groups.Count - 2; i++)
            {
                for (int j = (i + 1); j <= groups.Count - 1; j++)
                {
                    if (groups[i] == groups[j]) continue;
                    Group groupI = tryBoard.GetCurrentGroup(groups[i]);
                    Group groupJ = tryBoard.GetCurrentGroup(groups[j]);
                    //check if currently linked
                    Boolean isLinked = (groupI == groupJ) || PossibleLinkToAnyGroup(tryBoard, groupI, groupJ);
                    Boolean isLeapGroups = leapGroups.Contains(groups[i]) && leapGroups.Contains(groups[j]);
                    if (!isLinked && !isLeapGroups) continue;
                    //check if previously linked
                    Boolean previousLinked = IsImmediateDiagonallyConnected(currentBoard, groups[i], groups[j]);
                    if (previousLinked) continue;

                    //check non killable groups
                    if (WallHelper.IsNonKillableGroup(currentBoard, groups[i]) && WallHelper.IsNonKillableGroup(currentBoard, groups[j])) continue;
                    //check if diagonal groups
                    if (LinkHelper.GetDiagonalGroups(currentBoard, groups[i]).Any(n => n.Equals(groups[j])) && (!groupI.Equals(tryBoard.MoveGroup) || !groupJ.Equals(tryBoard.MoveGroup)))
                        continue;
                    //check connect and die
                    if (ImmovableHelper.CheckConnectAndDie(tryBoard, groupI, false) || ImmovableHelper.CheckConnectAndDie(tryBoard, groupJ, false))
                        continue;
                    //check opponent suicidal
                    (Boolean suicidal, Board b) = ImmovableHelper.IsSuicidalMove(move, c.Opposite(), currentBoard, true);
                    if (suicidal && (b == null || b.MoveGroup.Points.Count == 1))
                        continue;
                    return true;
                }
            }

            //check for possible big leap
            if (CheckForPossibleBigLeap(tryBoard))
                return true;
            return false;
        }

        public static Boolean PossibleLinkForGroups(GameTryMove tryMove)
        {
            Board currentBoard = tryMove.CurrentGame.Board;
            Board tryBoard = tryMove.TryGame.Board;
            return PossibleLinkForGroups(tryBoard, currentBoard);
        }

        /// <summary>
        /// Check for possible big leap.
        /// <see cref="UnitTestProject.LinkHelperTest.LinkHelperTest_Scenario_XuanXuanQiJing_Weiqi101_18497_5" />
        /// </summary>
        private static Boolean CheckForPossibleBigLeap(Board tryBoard)
        {
            Point move = tryBoard.Move.Value;
            Content c = tryBoard.MoveGroup.Content;
            if (tryBoard.PointWithinMiddleArea(move)) return false;

            //ensure non killable group at point up
            List<Point> pointUp = tryBoard.GetStoneNeighbours().Where(n => tryBoard.PointWithinMiddleArea(n) && WallHelper.IsNonKillableGroup(tryBoard, n)).ToList();
            if (pointUp.Count != 1) return false;
            List<Point> npoints = tryBoard.GetStoneAndDiagonalNeighbours().Where(n => tryBoard[n] == c).ToList();
            if (npoints.Count == 0) return false;
            if (npoints.Count > 1 && !tryBoard.GetStoneNeighbours(npoints[0]).Contains(npoints[1])) return false;

            //get diagonal in leap direction
            List<Point> diagonals = tryBoard.GetDiagonalNeighbours().Where(n => tryBoard[n] == Content.Empty && !tryBoard.GetStoneNeighbours(n).Intersect(npoints).Any()).ToList();
            if (diagonals.Count != 1) return false;
            Point d = diagonals.First();
            if (!GameHelper.SetupMoveAvailable(tryBoard, d, c)) return false;

            //make block move
            Point blockMove = tryBoard.GetMoveLiberties().FirstOrDefault(n => !tryBoard.PointWithinMiddleArea(n) && tryBoard.GetStoneNeighbours(n).Contains(d));
            if (blockMove.IsEmpty()) return false;
            Board b = tryBoard.MakeMoveOnNewBoard(blockMove, c.Opposite(), true);
            if (b == null) return false;
            if (b.MoveGroupLiberties == 1 && b.GetNeighbourGroups().Count > 1) return true;
            if (b.MoveGroupLiberties == 2 && ImmovableHelper.CheckConnectAndDie(b)) return true;
            return false;
        }

        /// <summary>
        /// Get possible leap groups.
        /// <see cref="UnitTestProject.LinkHelperTest.LinkHelperTest_Scenario_WindAndTime_Q30274" />
        /// </summary>
        public static HashSet<Group> GetPossibleLeapGroups(Board tryBoard, Board currentBoard)
        {
            Point move = tryBoard.Move.Value;
            Content c = tryBoard.MoveGroup.Content;
            List<Point> points = tryBoard.GetClosestPoints(move, c);
            points = points.Except(tryBoard.GetStoneAndDiagonalNeighbours()).ToList();
            //validate leap move
            HashSet<Group> leapGroups = new HashSet<Group>();
            foreach (Point p in points)
            {
                Group group = currentBoard.GetGroupAt(p);
                if (leapGroups.Contains(group)) continue;
                if (ValidateLeapMove(tryBoard, move, p))
                    leapGroups.Add(group);
            }
            return leapGroups;
        }

        /// <summary>
        /// Validate leap move.
        /// </summary>
        public static Boolean ValidateLeapMove(Board tryBoard, Point p, Point q)
        {
            Content c = tryBoard[p];
            //get middle points between the leap points
            List<Point> middlePoints = new List<Point>();
            if (Math.Abs(p.x - q.x) == 2)
            {
                if (Math.Abs(p.y - q.y) > 2) return false;
                int y_min = Math.Min(p.y, q.y);
                int y_max = Math.Max(p.y, q.y);
                if (p.y.Equals(q.y)) //leap on same line
                {
                    y_min -= 1;
                    y_max += 1;
                }
                for (int i = y_min; i <= y_max; i++)
                {
                    int middle_x = (p.x > q.x) ? q.x + 1 : q.x - 1;
                    middlePoints.Add(new Point(middle_x, i));
                }
            }
            else if (Math.Abs(p.y - q.y) == 2)
            {
                if (Math.Abs(p.x - q.x) > 2) return false;
                int x_min = Math.Min(p.x, q.x);
                int x_max = Math.Max(p.x, q.x);
                if (p.x.Equals(q.x)) //leap on same line
                {
                    x_min -= 1;
                    x_max += 1;
                }
                for (int i = x_min; i <= x_max; i++)
                {
                    int middle_y = (p.y < q.y) ? q.y - 1 : q.y + 1;
                    middlePoints.Add(new Point(i, middle_y));
                }
            }
            //check for same content at middle points
            middlePoints.RemoveAll(n => !tryBoard.PointWithinBoard(n));
            if (middlePoints.Count == 0 || middlePoints.Any(t => tryBoard[t] == c)) return false;
            //check for opposite content at middle points
            middlePoints = middlePoints.Where(n => tryBoard[n] == c.Opposite()).ToList();
            if (middlePoints.Count() <= 1) return true;

            Boolean leapOnSameLine = p.y.Equals(q.y) || p.x.Equals(q.x);
            if (!leapOnSameLine) return false;
            if (middlePoints.Any(n => n.x == p.x || n.y == p.y))
                return false;
            return true;
        }

        /// <summary>
        /// Possible link to any group.
        /// <see cref="UnitTestProject.LinkHelperTest.LinkHelperTest_Scenario_XuanXuanQiJing_Weiqi101_18497_4" />
        /// Link for kill <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_Scenario_XuanXuanQiJing_Weiqi101_B74" />
        /// Link through move group <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_Scenario_XuanXuanQiJing_Weiqi101_18497" /> 
        /// <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_Scenario_TianLongTu_Q16902" /> 
        /// Captured eye point <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_Scenario_XuanXuanQiJing_Weiqi101_18497_2" />
        /// <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_Scenario_XuanXuanGo_Q18340" /> 
        /// </summary>
        public static Boolean PossibleLinkToAnyGroup(Board tryBoard, Group group, Group findGroup)
        {
            //link between the two groups
            if (CheckPossibleLink(tryBoard, group, findGroup))
                return true;

            //link through move group
            Boolean isLinked = (group == tryBoard.MoveGroup || CheckPossibleLink(tryBoard, group, tryBoard.MoveGroup));
            Boolean isLinked2 = (findGroup == tryBoard.MoveGroup || CheckPossibleLink(tryBoard, findGroup, tryBoard.MoveGroup));

            return isLinked && isLinked2;
        }

        /// <summary>
        /// Check possible link.
        /// </summary>
        private static Boolean CheckPossibleLink(Board tryBoard, Group group, Group findGroup)
        {
            if (GetGroupLinkedDiagonals(tryBoard, group).Any(d => tryBoard.GetGroupAt(d.Move) == findGroup))
                return true;
            return false;
        }
        #endregion

        #region diagonal connected groups
        /// <summary>
        /// Check is diagonal linked.
        /// Check both diagonals empty <see cref="UnitTestProject.LifeCheckTest.LifeCheckTest_Scenario_TianLongTu_Q16571_4" />
        /// Check negligible for links <see cref="UnitTestProject.LinkHelperTest.LinkHelperTest_Scenario_WindAndTime_Q30150_8" />
        /// Check killer group <see cref="UnitTestProject.LinkHelperTest.LinkHelperTest_Scenario_WindAndTime_Q30150_5" />
        /// </summary>
        public static Boolean CheckIsDiagonalLinked(Point pointA, Point pointB, Board board, Boolean immediateLink = true)
        {
            Content c = board[pointA];
            List<Point> diagonals = LinkHelper.PointsBetweenDiagonals(pointA, pointB);
            //check any diagonal same content
            if (diagonals.Any(d => board[d] == c))
                return true;

            //check immovable at any diagonal
            if (diagonals.Any(d => board[d] == Content.Empty && ImmovableHelper.IsImmovablePoint(board, d, c)))
                return true;

            //check both diagonals empty
            if (diagonals.All(d => board[d] == Content.Empty))
            {
                if (immediateLink) return true;
                foreach (Board b in GameHelper.GetMoveBoards(board, diagonals, c.Opposite(), true))
                {
                    //check connect and die move
                    Point q = diagonals.First(d => !d.Equals(b.Move.Value));
                    if (ImmovableHelper.ConnectAndDieMove(b, q, c).Item1) return false;
                    //check negligible for links
                    if (LinkHelper.CheckNegligibleForLinks(b, board, n => !n.Equals(b.GetGroupAt(pointA)) && !n.Equals(b.GetGroupAt(pointB))))
                        return false;
                }
                return true;
            }
            //check any diagonal opposite content
            foreach (Point p in diagonals)
            {
                if (board[p] != c.Opposite()) continue;
                //check immovable at diagonal
                if (!ImmovableHelper.IsImmovablePoint(board, p, c)) continue;
                if (immediateLink) return true;
                //check killer group
                if (GroupHelper.GetKillerGroupOfStrongNeighbourGroups(board, p, c) == null)
                    continue;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Check is diagonal linked.
        /// </summary>
        public static Boolean CheckIsDiagonalLinked(Link<Point> diagonal, Board board, Boolean immediateLink = true)
        {
            if (CheckIsDiagonalLinked(diagonal.Move, (Point)diagonal.CheckMove, board, immediateLink))
                return true;
            return false;
        }

        /// <summary>
        /// Get diagonal groups.
        /// </summary>
        public static List<Group> GetDiagonalGroups(Board board, Group group = null)
        {
            return LinkHelper.GetGroupLinkedDiagonals(board, group).Select(t => board.GetGroupAt(t.Move)).ToList();
        }

        /// <summary>
        /// Get group diagonals with same content.
        /// </summary>
        public static List<Link<Point>> GetGroupLinkedDiagonals(Board board, Group group = null, Boolean checkLinked = false)
        {
            List<Link<Point>> rc = new List<Link<Point>>();
            if (group == null) group = board.MoveGroup;
            Content c = group.Content;
            foreach (Point p in group.Points)
            {
                if (board[p] != c) continue;
                foreach (Point q in GetMoveDiagonals(board, p))
                {
                    //ensure diagonal is linked
                    if (!checkLinked || CheckIsDiagonalLinked(p, q, board))
                        rc.Add(new Link<Point>(q, p));
                }
            }
            return rc;
        }

        /// <summary>
        /// Get group diagonals regardless of content.
        /// </summary>
        public static List<Link<Point>> GetGroupDiagonals(Board board, Group group = null)
        {
            if (group == null) group = board.MoveGroup;
            List<Link<Point>> rc = new List<Link<Point>>();
            foreach (Point p in group.Points)
            {
                foreach (Point q in GetDiagonalPoints(board, p))
                    rc.Add(new Link<Point>(q, p));
            }
            return rc;
        }

        /// <summary>
        /// Get diagonal points regardless of content.
        /// </summary>
        public static List<Point> GetDiagonalPoints(Board board, Point? p = null)
        {
            if (p == null) p = board.Move.Value;
            Content c = board[p.Value];
            return board.GetDiagonalNeighbours(p).Where(q => !PointsBetweenDiagonals(p.Value, q).Any(r => board[r] == c)).ToList();
        }

        /// <summary>
        /// Get move diagonals of same content that are not part of the move group.
        /// </summary>
        public static List<Point> GetMoveDiagonals(Board tryBoard, Point? p = null)
        {
            if (p == null) p = tryBoard.Move.Value;
            Content c = tryBoard[p.Value];
            List<Point> points = GetDiagonalPoints(tryBoard, p.Value);
            return points.Where(n => tryBoard[n] == c && !tryBoard.GetGroupAt(n).Equals(tryBoard.GetGroupAt(p.Value))).ToList();
        }

        /// <summary>
        /// Is diagonally connected groups. 
        /// </summary>
        public static Boolean IsDiagonallyConnectedGroups(Board board, Group group, Group findGroup)
        {
            if (group.Equals(findGroup)) return true;
            return IsDiagonallyConnectedGroups(new HashSet<Group>() { group }, board, n => n.Equals(findGroup));
        }

        /// <summary>
        /// Get all diagonal connected groups.
        /// </summary>
        public static HashSet<Group> GetAllDiagonalConnectedGroups(Board board, Group group, Func<Group, Boolean> func = null)
        {
            HashSet<Group> allConnectedGroups = new HashSet<Group>() { group };
            IsDiagonallyConnectedGroups(allConnectedGroups, board, func);
            return allConnectedGroups;
        }

        /// <summary>
        /// Is diagonally connected groups. Use func to find specific group else look for all connected groups.
        /// </summary>
        public static Boolean IsDiagonallyConnectedGroups(HashSet<Group> connectedGroups, Board board, Func<Group, Boolean> func = null)
        {
            Group group = connectedGroups.Last();
            List<Link<Point>> diagonals = GetGroupLinkedDiagonals(board, group);

            foreach (Link<Point> d in diagonals)
            {
                Group g = board.GetGroupAt(d.Move);
                if (g.Liberties.Count == 1 || connectedGroups.Contains(g)) continue;

                //check diagonal link
                if (!CheckIsDiagonalLinked(d, board, false))
                    continue;

                //check double linkage
                if (CheckDoubleLinkage(board, d))
                    continue;

                //check tiger mouth exceptions
                if (CheckTigerMouthExceptionsForLinks(board, d))
                    continue;

                //check double atari for links
                if (CheckDoubleAtariForLinks(board, d))
                    continue;

                //add group
                connectedGroups.Add(g);

                //check if group found
                if (func != null && func(g))
                    return true;

                //get diagonal connected groups recursively
                if (IsDiagonallyConnectedGroups(connectedGroups, board, func))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Is immediate diagonally connected. 
        /// </summary>
        public static Boolean IsImmediateDiagonallyConnected(Board board, Group group, Group findGroup)
        {
            Link<Point> diagonalLink = GetGroupLinkedDiagonals(board, group).FirstOrDefault(d => board.GetGroupAt(d.Move) == findGroup);
            if (diagonalLink == null) return false;
            return CheckIsDiagonalLinked(diagonalLink, board, true);
        }

        /// <summary>
        /// Get all diagonal groups.
        /// </summary>
        public static List<Group> GetAllDiagonalGroups(Board board, Group group, Func<Group, Boolean> func = null, List<Group> groups = null)
        {
            if (groups == null)
            {
                groups = new List<Group>();
                groups.Add(group);
            }
            //get all diagonal points
            foreach (Group g in GetDiagonalGroups(board, group))
            {
                if (groups.Contains(g)) continue;
                if (func != null && !func(g)) continue;
                groups.Add(g);
                //get all diagonal groups by recursion
                GetAllDiagonalGroups(board, g, func, groups);
            }
            return groups;
        }
        #endregion

        #region common link functions
        /// <summary>
        /// Points between diagonals.
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

        public static List<Point> PointsBetweenDiagonals(Link<Point> diagonal)
        {
            return PointsBetweenDiagonals(diagonal.Move, (Point)diagonal.CheckMove);
        }

        /// <summary>
        /// Check points between diagonals at move.
        /// </summary>
        public static Point? CheckPointsBetweenDiagonalsAtMove(Board board, Content c = Content.Unknown)
        {
            Point move = board.Move.Value;
            List<Point> epoints = GetDiagonalsAtStoneNeighbours(board, move, c);
            if (epoints.Count != 2) return null;
            return PointsBetweenDiagonals(epoints[0], epoints[1]).First(n => !n.Equals(move));
        }


        /// <summary>
        /// Is external link to target group.
        /// </summary>
        public static Boolean IsExternalLinkToTargetGroup(Board board, Point linkPoint)
        {
            if (board.GameInfo.targetPoints.Any(n => LinkHelper.IsDiagonallyConnectedGroups(board, board.GetGroupAt(linkPoint), board.GetGroupAt(n))))
                return true;
            return false;
        }

        /// <summary>
        /// Is absolute link for groups.
        /// </summary>
        public static Boolean IsAbsoluteLinkForGroups(Board currentBoard, Board tryBoard)
        {
            if (tryBoard.MoveGroup.Points.Count <= 2) return false;
            return (LinkHelper.GetPreviousMoveGroup(currentBoard, tryBoard).Count > 1);
        }

        /// <summary>
        /// Find diagonal cut.
        /// </summary>
        public static IEnumerable<(Point, List<Point>)> FindDiagonalCut(Board board, Group group = null, Boolean checkConnectAndDie = false)
        {
            if (group == null) group = board.MoveGroup;
            Content c = group.Content;
            foreach (Link<Point> p in GetGroupLinkedDiagonals(board, group))
            {
                if (ImmovableHelper.IsSuicidalWithoutKo(board, board.GetGroupAt(p.Move))) continue;
                if (ImmovableHelper.IsSuicidalWithoutKo(board, board.GetGroupAt((Point)p.CheckMove))) continue;
                List<Point> diagonals = PointsBetweenDiagonals(p);
                if (checkConnectAndDie)
                {
                    if (diagonals.All(d => board[d] == c.Opposite() && !ImmovableHelper.CheckConnectAndDie(board, board.GetGroupAt(d), false)))
                        yield return (p.Move, diagonals);
                }
                else
                {
                    if (diagonals.All(d => board[d] == c.Opposite() && !ImmovableHelper.IsSuicidalWithoutKo(board, board.GetGroupAt(d))))
                        yield return (p.Move, diagonals);
                }
            }
        }

        /// <summary>
        /// Get diagonal groups without cut.
        /// </summary>
        public static IEnumerable<Group> GetDiagonalGroupsWithoutCut(Board board, Group group = null)
        {
            if (group == null) group = board.MoveGroup;
            Content c = group.Content;
            foreach (Link<Point> q in LinkHelper.GetGroupLinkedDiagonals(board, group))
            {
                List<Point> points = LinkHelper.PointsBetweenDiagonals(q.Move, (Point)q.CheckMove);
                if (points.Count(n => board[n] == c.Opposite()) == 1 && points.Count(n => board[n] == Content.Empty) == 1)
                    yield return board.GetGroupAt(q.Move);
            }
        }

        /// <summary>
        /// Get diagonal groups with cut.
        /// </summary>
        public static IEnumerable<Group> GetDiagonalGroupsWithCut(Board board, Group group = null)
        {
            if (group == null) group = board.MoveGroup;
            foreach (Link<Point> q in LinkHelper.GetGroupLinkedDiagonals(board, group))
            {
                if (!LinkHelper.FindLibertyBetweenDiagonals(board, q.Move, (Point)q.CheckMove).Any())
                    yield return board.GetGroupAt(q.Move);
            }
        }

        /// <summary>
        /// Find liberty between diagonals.
        /// </summary>
        public static List<Point> FindLibertyBetweenDiagonals(Board board, Point p, Point q)
        {
            List<Point> points = LinkHelper.PointsBetweenDiagonals(p, q);
            return points.Where(n => board[n] == Content.Empty).ToList();
        }

        /// <summary>
        /// Get previous move group.
        /// </summary>
        public static List<Group> GetPreviousMoveGroup(Board currentBoard, Board tryBoard)
        {
            Point move = tryBoard.Move.Value;
            Content c = tryBoard.MoveGroup.Content;
            return currentBoard.GetGroupsFromStoneNeighbours(move, c.Opposite());
        }

        /// <summary>
        /// Get diagonals at stone neighbours.
        /// </summary>
        public static List<Point> GetDiagonalsAtStoneNeighbours(Board board, Point? p = null, Content c = Content.Unknown)
        {
            if (p == null) p = board.Move.Value;
            if (c == Content.Unknown) c = board.MoveGroup.Content.Opposite();
            List<Point> npoints = board.GetStoneNeighbours(p.Value).Where(n => board[n] == c).ToList();
            if (npoints.Count == 0) return npoints;
            npoints = npoints.Where(n => board.GetDiagonalNeighbours(n).Intersect(npoints).Any()).ToList();
            return npoints;
        }
        #endregion

        #region link exceptions
        /// <summary>
        /// Check for double linkage.
        /// <see cref="UnitTestProject.LinkHelperTest.LinkHelperTest_Scenario_TianLongTu_Q16571_3" />
        /// </summary>
        private static Boolean CheckDoubleLinkage(Board board, Link<Point> diagonalLink)
        {
            Content c = board[diagonalLink.Move];
            List<Point> diagonals = LinkHelper.PointsBetweenDiagonals(diagonalLink);
            foreach (Point p in diagonals.Where(d => board[d] == Content.Empty))
            {
                //ensure three opponent groups
                List<Point> opponentStones = board.OpponentAtStoneNeighbour(p, c.Opposite());
                if (opponentStones.Count < 3 || board.GetGroupsFromPoints(opponentStones).Count < 3) continue;

                //make opponent move at diagonal
                (Boolean connectAndDie, Board b) = ImmovableHelper.ConnectAndDieMove(board, p, c.Opposite(), false, false);
                if (connectAndDie || b == null) continue;

                //check diagonal links
                Point middleStone = opponentStones.First(n => b.GetDiagonalNeighbours(n).Count(d => opponentStones.Contains(d)) >= 2);
                if (opponentStones.Where(n => !n.Equals(middleStone)).All(n => CheckIsDiagonalLinked(middleStone, n, board) && !CheckIsDiagonalLinked(middleStone, n, b)))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Check tiger mouth exceptions for links.
        /// </summary>
        public static Boolean CheckTigerMouthExceptionsForLinks(Board board, Link<Point> diagonalPoint)
        {
            Content c = board[diagonalPoint.Move];
            List<Point> tigerMouthList = GetTigerMouthsOfLinks(board, diagonalPoint);
            if (LifeCheck.CheckTigerMouthExceptions(board, tigerMouthList, c))
                return true;
            return false;
        }

        /// <summary>
        /// Get tiger mouth of links.
        /// </summary>
        public static List<Point> GetTigerMouthsOfLinks(Board board, Link<Point> diagonalPoint)
        {
            Content c = board[diagonalPoint.Move];
            List<Point> tigerMouthList = new List<Point>();
            foreach (Point q in LinkHelper.PointsBetweenDiagonals(diagonalPoint))
            {
                if (ImmovableHelper.FindTigerMouthForLink(board, q, c))
                    tigerMouthList.Add(q);
            }
            return tigerMouthList;
        }

        /// <summary>
        /// Tiger mouth threat group.
        /// </summary>
        public static Group TigerMouthThreatGroup(Board board, Point tigerMouth, Content c)
        {
            if (board[tigerMouth] != Content.Empty) return null;
            List<Point> npoints = board.OpponentAtStoneNeighbour(tigerMouth, c);
            if (npoints.Count != 1) return null;
            Group threatGroup = board.GetGroupAt(npoints.First());
            if (threatGroup.Liberties.Count == 2)
                return threatGroup;
            return null;
        }

        /// <summary>
        /// Check double atari for links.
        /// <see cref="UnitTestProject.LinkHelperTest.LinkHelperTest_DoubleAtariOnSemiSolidEye" />
        /// </summary>
        private static Boolean CheckDoubleAtariForLinks(Board board, Link<Point> diagonalPoint)
        {
            Content c = board[diagonalPoint.Move];
            foreach (Point d in LinkHelper.PointsBetweenDiagonals(diagonalPoint))
            {
                List<Group> ngroups = board.GetGroupsFromStoneNeighbours(d, c.Opposite());
                if (DoubleKillAtariOnTargetGroups(board, ngroups))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Double kill atari on target groups.
        /// <see cref="UnitTestProject.LinkHelperTest.LinkHelperTest_DoubleAtariOnSemiSolidEye" />
        /// <see cref="UnitTestProject.LinkHelperTest.LinkHelperTest_DoubleAtariOnLinkage" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanQiJing_A39" />
        /// </summary>
        public static Boolean DoubleKillAtariOnTargetGroups(Board board, List<Group> targetGroups)
        {
            if (targetGroups.Count == 0) return false;
            Content c = targetGroups.First().Content;
            List<Group> groups = targetGroups.Where(t => board.GetGroupLiberties(t).Count == 2).ToList();
            if (groups.Count > 0)
            {
                //double kill atari
                HashSet<Point> liberties = board.GetLibertiesOfGroups(groups);
                IEnumerable<Board> moveBoards = GameHelper.GetMoveBoards(board, liberties, c.Opposite(), true);
                if (moveBoards.Any(b => AtariHelper.DoubleKillAtariWithoutEscape(b)))
                    return true;
            }
            //double connect and die
            if (DoubleConnectAndDieOnTargetGroups(board, targetGroups))
                return true;
            return false;
        }

        /// <summary>
        /// Double connect and die on target groups.
        /// </summary>
        public static Boolean DoubleConnectAndDieOnTargetGroups(Board board, List<Group> targetGroups)
        {
            if (targetGroups.Count == 0) return false;
            Content c = targetGroups.First().Content;
            targetGroups = targetGroups.Where(t => board.GetGroupLiberties(t).Count == 3).ToList();
            if (targetGroups.Count == 0) return false;

            HashSet<Point> liberties = board.GetLibertiesOfGroups(targetGroups);
            IEnumerable<Board> moveBoards = GameHelper.GetMoveBoards(board, liberties, c.Opposite());
            moveBoards = moveBoards.Where(b => !ImmovableHelper.CheckConnectAndDie(b, b.MoveGroup, false) && !WallHelper.StrongGroups(b, targetGroups));
            //double connect and die
            Boolean rc = liberties.Any(n => targetGroups.All(s => board.GetGroupsFromStoneNeighbours(n, c.Opposite()).Contains(s)));
            if (!rc && moveBoards.Any(b => b.GetGroupsFromStoneNeighbours().Count(n => !WallHelper.IsStrongGroup(b, n)) >= 2))
                return true;
            //check exceptions
            if (moveBoards.Any(b => CheckDoubleConnectAndDieExceptions(board, b)))
                return true;
            return false;
        }

        /// <summary>
        /// Check double connect and die exceptions.
        /// <see cref="UnitTestProject.LifeCheckTest.LifeCheckTest_Scenario_WindAndTime_Q30315" />
        /// </summary>
        public static Boolean CheckDoubleConnectAndDieExceptions(Board board, Board b)
        {
            if (LinkWithImmovableGroup(b, board) || CheckForKoBreak(b))
                return true;
            return false;
        }

        /// <summary>
        /// Link with immovable group.
        /// <see cref="UnitTestProject.LinkHelperTest.LinkHelperTest_Scenario_WindAndTime_Q30150_6" />
        /// <see cref="UnitTestProject.LinkHelperTest.LinkHelperTest_Scenario_WindAndTime_Q30150_7" />
        /// </summary>
        public static Boolean LinkWithImmovableGroup(Board b, Board board, Func<Group, Boolean> func = null)
        {
            Content c = b.MoveGroup.Content;
            if (b.MoveGroupLiberties <= 2) return false;
            List<Group> groups = LinkHelper.GetPreviousMoveGroup(board, b);
            if (groups.Count == 1) return false;
            groups = groups.Where(n => n.Liberties.Count == 2).ToList();
            if (func != null) groups.RemoveAll(s => func(s));
            if (CheckImmovableGroups(b, board, groups).Any())
                return true;
            return false;
        }

        /// <summary>
        /// Move at tiger mouth.
        /// <see cref="UnitTestProject.LifeCheckTest.LifeCheckTest_Scenario_XuanXuanGo_B3" />
        /// <see cref="UnitTestProject.LinkHelperTest.LinkHelperTest_Scenario_TianLongTu_Q16571" />
        /// </summary>
        public static List<Point> MoveAtTigerMouth(Board b, Board board)
        {
            Content c = b.MoveGroup.Content;
            return b.GetStoneNeighbours().Where(n => ImmovableHelper.FindTigerMouthForLink(board, n, c.Opposite())).ToList();
        }

        /// <summary>
        /// Check for ko break.
        /// </summary>
        public static Boolean CheckForKoBreak(Board b, Func<Point, Boolean> func = null)
        {
            Content c = b.MoveGroup.Content;
            foreach (Point p in b.GetStoneNeighbours())
            {
                if (!ImmovableHelper.FindTigerMouthForLink(b, p, c)) continue;
                if (func != null && !func(p)) continue;
                Point q = b.GetStoneNeighbours(p).First(n => b[n] != c);
                if (!KoHelper.MakeKoFight(b, q, c).Item1) continue;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Double ko break.
        /// <see cref="UnitTestProject.LinkHelperTest.LinkHelperTest_y" />
        /// <see cref="UnitTestProject.LifeCheckTest.LifeCheckTest_Scenario_Corner_A139_2" />
        /// </summary>
        public static Boolean DoubleKoBreak(Board b, Point tigerMouth, Content c)
        {
            Point p = b.GetMoveLiberties(tigerMouth).FirstOrDefault();
            if (p.IsEmpty()) return false;
            List<Point> points = b.GetStoneNeighbours(p).Where(n => !n.Equals(tigerMouth)).ToList();
            List<Point> rc = points.Where(n => b[n] == c.Opposite()).ToList();
            if (rc.Count != points.Count - 1) return false;
            Point q = points.Except(rc).First();
            if (b[q] != Content.Empty) return false;
            //make move to form tiger mouth
            (_, Board b2) = ImmovableHelper.IsSuicidalMove(q, c.Opposite(), b, true);
            if (b2 == null) return false;
            //make ko move
            if (!KoHelper.MakeKoFight(b2, tigerMouth, c.Opposite()).Item1)
                return false;
            //check for another ko
            if (CheckForKoBreak(b2, s => !s.Equals(p)))
                return true;
            return false;
        }

        /// <summary>
        /// Link breakage.
        /// <see cref="UnitTestProject.LinkHelperTest.LinkHelperTest_Scenario_WindAndTime_Q30150_2" />
        /// </summary>
        public static Boolean LinkBreakage(Board b, Board board, Point? p = null)
        {
            if (p == null) p = b.Move.Value;
            Content c = b.MoveGroup.Content;
            List<Group> ngroups = board.GetGroupsFromStoneNeighbours(p, c);
            if (!WallHelper.TargetAttackWithKillableGroup(board, ngroups)) return false;

            List<Point> diagonals = ImmovableHelper.GetDiagonalsOfTigerMouth(board, p.Value, c.Opposite());
            if (!diagonals.Any()) return false;
            foreach (Point d in diagonals)
            {
                List<Point> points = LinkHelper.PointsBetweenDiagonals(d, p.Value);
                if (!LinkHelper.CheckIsDiagonalLinked(points[0], points[1], board)) continue;
                if (!LinkHelper.CheckIsDiagonalLinked(points[0], points[1], b))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Check negligible for links.
        /// Check capture <see cref="UnitTestProject.LinkHelperTest.LinkHelperTest_Scenario_WindAndTime_Q30150_9" />
        /// Check connect and die <see cref="UnitTestProject.LifeCheckTest.LifeCheckTest_Scenario_Nie60_4" />
        /// <see cref="UnitTestProject.LifeCheckTest.LifeCheckTest_Scenario_WindAndTime_Q30150" />
        /// </summary>
        public static Boolean CheckNegligibleForLinks(Board b, Board board, Func<Group, Boolean> func = null)
        {
            //check capture
            if (b.CapturedList.Any(n => CheckImmovableNeighbourGroups(b, board, n).Any()))
                return true;

            //check connect and die
            List<Group> ngroups = b.GetNeighbourGroups().Where(n => (func != null ? func(n) : true)).ToList();
            if (ngroups.Any(n => ImmovableHelper.CheckConnectAndDie(b, n) && !ImmovableHelper.CheckConnectAndDie(board, n, false)))
                return true;

            return false;
        }

        /// <summary>
        /// Check immovable neighbour groups.
        /// </summary>
        public static IEnumerable<Group> CheckImmovableNeighbourGroups(Board b, Board board, Group group)
        {
            List<Group> ngroups = board.GetNeighbourGroups(group).Where(n => n.Liberties.Count <= 2).ToList();
            return CheckImmovableGroups(b, board, ngroups);
        }

        /// <summary>
        /// Check immovable groups.
        /// </summary>
        public static IEnumerable<Group> CheckImmovableGroups(Board b, Board board, List<Group> groups)
        {
            foreach (Group group in groups)
            {
                Content c = group.Content;
                foreach (Point p in group.Liberties)
                {
                    if (!ImmovableHelper.IsSuicidalMove(board, p, c)) continue;
                    if (LinkBreakage(b, board, p))
                        yield return group;
                }
            }
        }
        #endregion
    }
}
