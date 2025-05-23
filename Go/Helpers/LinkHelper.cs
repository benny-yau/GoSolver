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
        /// Possible link for groups. For neutral point move, covered eye move, and eye diagonal move.
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
                if (!tryBoard.GetStoneNeighbours().Any(n => EyeHelper.FindCoveredEye(tryBoard, n, c)))
                    return false;
            }

            //get all possible link groups
            List<Point> npoints = currentBoard.GetStoneAndDiagonalNeighbours(move).Where(n => currentBoard[n] == c).ToList();
            HashSet<Group> ngroups = currentBoard.GetGroupsFromPoints(npoints);

            //check captured groups
            if (tryBoard.CapturedList.Any())
            {
                if (tryBoard.CapturedList.Count > 1)
                    return true;
                Group capturedGroup = tryBoard.CapturedList.First();
                if (currentBoard.GetNeighbourGroups(capturedGroup).Count > 1 && !ImmovableHelper.UnescapableGroup(currentBoard, capturedGroup).Item1)
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
                    //check ko link
                    if (ImmovableHelper.IsSuicidalWithoutKo(tryBoard, groupI) || ImmovableHelper.IsSuicidalWithoutKo(tryBoard, groupJ))
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

        /// <summary>
        /// Check for possible big leap.
        /// <see cref="UnitTestProject.LinkHelperTest.LinkHelperTest_Scenario_XuanXuanQiJing_Weiqi101_18497_5" />
        /// </summary>
        private static Boolean CheckForPossibleBigLeap(Board tryBoard)
        {
            Point move = tryBoard.Move.Value;
            Content c = tryBoard.MoveGroup.Content;

            //ensure base line move
            if (tryBoard.PointWithinMiddleArea(move)) return false;

            //ensure non killable group at point up
            List<Point> pointUp = tryBoard.GetStoneNeighbours().Where(n => tryBoard.PointWithinMiddleArea(n) && WallHelper.IsNonKillableGroup(tryBoard, n)).ToList();
            if (pointUp.Count != 1) return false;
            List<Point> npoints = tryBoard.GetStoneAndDiagonalNeighbours().Where(n => tryBoard[n] == c).ToList();
            if (npoints.Count == 0) return false;
            if (npoints.Count > 1 && !tryBoard.GetStoneNeighbours(npoints[0]).Contains(npoints[1])) return false;

            //get diagonal in leap direction
            List<Point> diagonalInLeapDirection = tryBoard.GetDiagonalNeighbours().Where(n => tryBoard.PointWithinMiddleArea(n) && !npoints.Contains(n) && !tryBoard.GetStoneNeighbours(n).Any(s => npoints.Contains(s))).ToList();
            if (diagonalInLeapDirection.Count != 1) return false;
            Point d = diagonalInLeapDirection.First();

            //ensure movable point at diagonal
            if (!tryBoard.GameInfo.IsMovablePoint[d.x, d.y]) return false;

            //make block move
            Point blockMove = tryBoard.GetStoneNeighbours(d).First(n => !tryBoard.PointWithinMiddleArea(n));
            Board b = tryBoard.MakeMoveOnNewBoard(blockMove, c.Opposite(), true);
            if (b == null) return false;
            return ImmovableHelper.CheckConnectAndDie(b);
        }

        /// <summary>
        /// Get possible leap groups.
        /// <see cref="UnitTestProject.LinkHelperTest.LinkHelperTest_Scenario_WindAndTime_Q30274" />
        /// </summary>
        public static HashSet<Group> GetPossibleLeapGroups(Board tryBoard, Board currentBoard)
        {
            Point move = tryBoard.Move.Value;
            Content c = tryBoard.MoveGroup.Content;
            List<Point> closestPoints = tryBoard.GetClosestPoints(move, c);
            closestPoints = closestPoints.Except(tryBoard.GetStoneAndDiagonalNeighbours()).ToList();
            //validate leap move
            HashSet<Group> leapGroups = new HashSet<Group>();
            foreach (Point p in closestPoints)
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
        /// Is absolute link by stone only.
        /// </summary>
        public static Boolean IsAbsoluteLinkForGroups(Board currentBoard, Board tryBoard)
        {
            if (tryBoard.MoveGroup.Points.Count == 1) return false;
            List<Group> linkedGroups = LinkHelper.GetPreviousMoveGroup(currentBoard, tryBoard);
            return (linkedGroups.Count > 1);
        }

        /// <summary>
        /// Check if diagonals are linked.
        /// Both diagonals empty <see cref="UnitTestProject.LifeCheckTest.LifeCheckTest_Scenario_TianLongTu_Q16571_4" />
        /// Check negligible for links <see cref="UnitTestProject.LinkHelperTest.LinkHelperTest_Scenario_TianLongTu_Q17078" />
        /// Check any diagonal separated by opposite content <see cref="UnitTestProject.LinkHelperTest.LinkHelperTest_Scenario_TianLongTu_Q16571" />
        /// Check threat group <see cref="UnitTestProject.LinkHelperTest.LinkHelperTest_Scenario_WindAndTime_Q30150" />
        /// Check capture secure <see cref="UnitTestProject.LinkHelperTest.LinkHelperTest_Scenario_TianLongTu_Q16571_2" />
        /// </summary>
        public static Boolean CheckIsDiagonalLinked(Point pointA, Point pointB, Board board, Boolean immediateLink = true)
        {
            Content c = board[pointA];
            List<Point> diagonals = LinkHelper.PointsBetweenDiagonals(pointA, pointB);
            //if any diagonal is same content then is linked
            if (diagonals.Any(d => board[d] == c))
                return true;

            //if both diagonals empty then is linked
            if (diagonals.All(d => board[d] == Content.Empty))
            {
                //check is immovable
                if (diagonals.Any(d => ImmovableHelper.IsImmovablePoint(board, d, c))) return true;
                IEnumerable<Board> moveBoards = GameHelper.GetMoveBoards(board, diagonals, c.Opposite(), true);
                foreach (Board b in moveBoards)
                {
                    //make connection at other diagonal
                    Point q = diagonals.First(d => !d.Equals(b.Move.Value));
                    Board b2 = b.MakeMoveOnNewBoard(q, c);
                    if (b2 == null || ImmovableHelper.CheckConnectAndDie(b2))
                        return false;

                    //check negligible for links
                    if (immediateLink) continue;
                    if (LinkHelper.CheckNegligibleForLinks(b, board, n => !n.Equals(b.GetGroupAt(pointA)) && !n.Equals(b.GetGroupAt(pointB))))
                        return false;
                }
                return true;
            }
            //check any diagonal separated by opposite content
            foreach (Point p in diagonals)
            {
                if (!ImmovableHelper.IsImmovablePoint(board, p, c)) continue;
                if (immediateLink) return true;

                Group killerGroup = GroupHelper.GetKillerGroupOfStrongNeighbourGroups(board, p, c);
                if (killerGroup == null) continue;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Check is diagonal linked.
        /// </summary>
        public static Boolean CheckIsDiagonalLinked(LinkedPoint<Point> diagonal, Board board, Boolean immediateLink = true)
        {
            if (CheckIsDiagonalLinked(diagonal.Move, (Point)diagonal.CheckMove, board, immediateLink))
                return true;
            return false;
        }

        /// <summary>
        /// Check for double linkage.
        /// <see cref="UnitTestProject.LifeCheckTest.LifeCheckTest_Scenario_TianLongTu_Q16571_8" />
        /// </summary>
        private static Boolean CheckDoubleLinkage(Board board, LinkedPoint<Point> diagonalLink)
        {
            Content c = board[diagonalLink.Move];
            List<Point> diagonals = LinkHelper.PointsBetweenDiagonals(diagonalLink);
            foreach (Point p in diagonals.Where(d => board[d] == Content.Empty))
            {
                //ensure three opponent groups
                List<Point> opponentStones = board.GetStoneNeighbours(p).Where(n => board[n] == c).ToList();
                if (opponentStones.Count < 3) continue;

                //make opponent move at diagonal
                Board b = board.MakeMoveOnNewBoard(p, c.Opposite(), true);
                if (b == null) continue;
                opponentStones = b.GetStoneNeighbours(p).Where(n => b[n] == c).ToList();
                if (b.GetGroupsFromPoints(opponentStones).Count < 3) continue;
                if (!KillerFormationHelper.ThreeOpponentGroupsAtMove(b, p)) continue;
                if (ImmovableHelper.CheckConnectAndDie(b, b.MoveGroup, false)) continue;

                //check diagonal links
                Point middleStone = opponentStones.First(n => b.GetDiagonalNeighbours(n).Count(d => opponentStones.Contains(d)) >= 2);
                if (opponentStones.Where(n => !n.Equals(middleStone)).All(n => !CheckIsDiagonalLinked(middleStone, n, b)))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Check double atari for links.
        /// <see cref="UnitTestProject.LinkHelperTest.LinkHelperTest_DoubleAtariOnSemiSolidEye" />
        /// </summary>
        private static Boolean CheckDoubleAtariForLinks(Board board, LinkedPoint<Point> diagonalPoint)
        {
            Content c = board[diagonalPoint.Move];
            foreach (Point d in LinkHelper.PointsBetweenDiagonals(diagonalPoint))
            {
                List<Group> tigerMouthGroups = board.GetGroupsFromStoneNeighbours(d, c.Opposite()).ToList();
                if (DoubleAtariOnTargetGroups(board, tigerMouthGroups))
                    return true;
            }
            return false;
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
        /// Get group linked diagonals.
        /// </summary>
        public static List<LinkedPoint<Point>> GetGroupLinkedDiagonals(Board board, Group group = null, Boolean checkLinked = false)
        {
            List<LinkedPoint<Point>> rc = new List<LinkedPoint<Point>>();
            if (group == null) group = board.MoveGroup;
            Content c = group.Content;
            foreach (Point p in group.Points)
            {
                if (board[p] != c) continue;
                foreach (Point q in board.GetDiagonalNeighbours(p))
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

        /// <summary>
        /// Get diagonal groups.
        /// </summary>
        public static List<Group> GetDiagonalGroups(Board board, Group group = null)
        {
            return LinkHelper.GetGroupLinkedDiagonals(board, group).Select(t => board.GetGroupAt(t.Move)).ToList();
        }

        /// <summary>
        /// Get all diagonals of group regardless of content.
        /// </summary>
        public static List<LinkedPoint<Point>> GetGroupDiagonals(Board board, Group group)
        {
            List<LinkedPoint<Point>> rc = new List<LinkedPoint<Point>>();
            Content c = group.Content;
            foreach (Point p in group.Points)
            {
                foreach (Point q in board.GetDiagonalNeighbours(p))
                {
                    if (PointsBetweenDiagonals(p, q).Any(r => board[r] == c)) continue;
                    rc.Add(new LinkedPoint<Point>(q, p));
                }
            }
            return rc;
        }

        /// <summary>
        /// Get diagonals of move of same content that are not part of the move group.
        /// </summary>
        public static List<Point> GetMoveDiagonals(Board tryBoard)
        {
            Content c = tryBoard.MoveGroup.Content;
            return tryBoard.GetDiagonalNeighbours().Where(n => tryBoard[n] == c && !tryBoard.GetGroupAt(n).Equals(tryBoard.MoveGroup)).ToList();
        }

        /// <summary>
        /// Is diagonally connected groups.  Use func to find specific group else look for all connected groups.
        /// </summary>
        public static Boolean IsDiagonallyConnectedGroups(HashSet<Group> allConnectedGroups, Board board, Func<Group, Boolean> func = null)
        {
            Group group = allConnectedGroups.Last();
            //find group diagonals of same content
            List<LinkedPoint<Point>> diagonalPoints = GetGroupLinkedDiagonals(board, group).OrderBy(d => board.GetGroupAt(d.Move).Liberties.Count).ToList();
            if (func != null)
            {
                //set priority for diagonal with find group
                List<LinkedPoint<Point>> diagonalWithFindGroup = diagonalPoints.Where(d => func(board.GetGroupAt(d.Move))).ToList();
                diagonalWithFindGroup.ForEach(d => { diagonalPoints.Remove(d); diagonalPoints.Insert(0, d); });
            }

            foreach (LinkedPoint<Point> diagonalPoint in diagonalPoints)
            {
                Group g = board.GetGroupAt(diagonalPoint.Move);
                if (g.Liberties.Count == 1 || allConnectedGroups.Contains(g)) continue;

                //check if diagonally linked
                if (!CheckIsDiagonalLinked(diagonalPoint, board, false))
                    continue;

                //check tiger mouth exceptions
                if (CheckTigerMouthExceptionsForLinks(board, diagonalPoint))
                    continue;

                //check for links with double linkage
                if (CheckDoubleLinkage(board, diagonalPoint))
                    continue;

                //check double atari for links
                if (CheckDoubleAtariForLinks(board, diagonalPoint))
                    continue;

                allConnectedGroups.Add(g);

                //check if group found
                if (func != null && func(g))
                    return true;

                //get diagonal connected groups recursively
                Boolean connected = IsDiagonallyConnectedGroups(allConnectedGroups, board, func);
                if (connected) return true;
            }
            return false;
        }

        /// <summary>
        /// Is diagonally connected groups. 
        /// </summary>
        public static Boolean IsDiagonallyConnectedGroups(Board board, Group group, Group findGroup)
        {
            return IsDiagonallyConnectedGroups(new HashSet<Group>() { group }, board, s => s == findGroup);
        }

        /// <summary>
        /// Is immediate diagonally connected. 
        /// </summary>
        public static Boolean IsImmediateDiagonallyConnected(Board board, Group group, Group findGroup)
        {
            LinkedPoint<Point> diagonalLink = GetGroupLinkedDiagonals(board, group).FirstOrDefault(d => board.GetGroupAt(d.Move) == findGroup);
            if (diagonalLink == null) return false;
            return CheckIsDiagonalLinked(diagonalLink, board, true);
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

        /// <summary>
        /// Check tiger mouth exceptions for links.
        /// </summary>
        public static Boolean CheckTigerMouthExceptionsForLinks(Board board, LinkedPoint<Point> diagonalPoint)
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
        public static List<Point> GetTigerMouthsOfLinks(Board board, LinkedPoint<Point> diagonalPoint)
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
        /// Get all diagonal groups by recursion.
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
                if (func != null && func(g)) continue;
                groups.Add(g);
                //get all diagonal groups by recursion
                GetAllDiagonalGroups(board, g, func, groups);
            }
            return groups;
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

        public static List<Point> PointsBetweenDiagonals(LinkedPoint<Point> diagonal)
        {
            return PointsBetweenDiagonals(diagonal.Move, (Point)diagonal.CheckMove);
        }

        /// <summary>
        /// Diagonal cut between two neighbour groups.
        /// </summary>
        public static (Point?, List<Point>) FindDiagonalCut(Board board, Group group)
        {
            Content c = group.Content;
            foreach (LinkedPoint<Point> diagonal in GetGroupLinkedDiagonals(board, group))
            {
                if (ImmovableHelper.IsSuicidalWithoutKo(board, board.GetGroupAt(diagonal.Move))) continue;
                if (ImmovableHelper.IsSuicidalWithoutKo(board, board.GetGroupAt((Point)diagonal.CheckMove))) continue;
                List<Point> diagonals = PointsBetweenDiagonals(diagonal);
                if (diagonals.All(d => board[d] == c.Opposite() && !ImmovableHelper.IsSuicidalWithoutKo(board, board.GetGroupAt(d))))
                    return (diagonal.Move, diagonals);
            }
            return (null, null);
        }

        /// <summary>
        /// Get stones within move group on current board.
        /// </summary>
        public static List<Group> GetPreviousMoveGroup(Board currentBoard, Board tryBoard)
        {
            Point p = tryBoard.Move.Value;
            Content c = tryBoard.MoveGroup.Content;
            return currentBoard.GetGroupsFromStoneNeighbours(p, c.Opposite()).ToList();
        }

        /// <summary>
        /// Get diagonal points.
        /// </summary>

        public static List<Point> GetDiagonalPoints(Board board, Point? p = null, Content c = Content.Unknown)
        {
            if (p == null)
            {
                p = board.Move.Value;
                c = board.MoveGroup.Content.Opposite();
            }
            List<Point> npoints = board.GetStoneNeighbours(p.Value).Where(n => board[n] == c).ToList();
            if (npoints.Count == 0) return npoints;
            npoints = npoints.Where(n => board.GetDiagonalNeighbours(n).Intersect(npoints).Any()).ToList();
            return npoints;
        }

        /// <summary>
        /// Get tiger mouth threat group.
        /// <see cref="UnitTestProject.LinkHelperTest.LinkHelperTest_Scenario_WindAndTime_Q30150_7" />
        /// <see cref="UnitTestProject.LinkHelperTest.LinkHelperTest_Scenario_WindAndTime_Q30150_6" />
        /// </summary>
        public static Group TigerMouthThreatGroup(Board board, Point tigerMouth, Content c)
        {
            if (board[tigerMouth] != Content.Empty) return null;
            List<Point> npoints = board.GetStoneNeighbours(tigerMouth).Where(n => board[n] == c.Opposite()).ToList();
            if (npoints.Count != 1) return null;
            Group threatGroup = board.GetGroupAt(npoints.First());
            if (threatGroup.Liberties.Count == 2)
                return threatGroup;
            return null;
        }

        /// <summary>
        /// Double atari on target groups.
        /// </summary>
        public static Boolean DoubleAtariOnTargetGroups(Board board, List<Group> targetGroups)
        {
            if (targetGroups.Count == 0) return false;
            Content c = targetGroups.First().Content;
            List<Group> groups = targetGroups.Where(t => board.GetGroupLiberties(t).Count == 2).ToList();
            if (groups.Count > 0)
            {
                //double atari
                HashSet<Point> liberties = board.GetLibertiesOfGroups(groups);
                IEnumerable<Board> moveBoards = GameHelper.GetMoveBoards(board, liberties, c.Opposite(), true);
                if (moveBoards.Any(b => AtariHelper.DoubleAtariWithoutEscape(b)))
                    return true;
                if (moveBoards.Any(b => CheckMoveGroupForTigerMouthExceptions(board, b)))
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
            targetGroups = targetGroups.Where(t => board.GetGroupLiberties(t).Count == 3 || board.GetGroupLiberties(t).Count == 4).ToList();
            if (targetGroups.Count == 0) return false;

            HashSet<Point> liberties = board.GetLibertiesOfGroups(targetGroups);
            IEnumerable<Board> moveBoards = GameHelper.GetMoveBoards(board, liberties, c.Opposite(), true);
            moveBoards = moveBoards.Where(b => !WallHelper.StrongGroups(b, targetGroups));
            //double connect and die
            if (moveBoards.Any(b => b.GetGroupsFromStoneNeighbours().Count(n => !WallHelper.IsStrongGroup(b, n)) >= 2))
                return true;
            if (moveBoards.Any(b => CheckMoveGroupForTigerMouthExceptions(board, b)))
                return true;
            return false;
        }

        /// <summary>
        /// Check move group for tiger mouth exceptions.
        /// </summary>
        public static Boolean CheckMoveGroupForTigerMouthExceptions(Board board, Board b)
        {
            if (Board.ResolveAtari(board, b) || b.CapturedList.Any(n => CheckImmovableNeighbourGroups(board, n).Any()))
                return true;

            if (LinkWithImmovableGroup(b, board) || MoveAtTigerMouth(b, board).Any() || CheckForKoBreak(b) || LinkBreakage(b))
                return true;
            return false;
        }

        /// <summary>
        /// Link with immovable group.
        /// </summary>
        public static Boolean LinkWithImmovableGroup(Board b, Board board, Func<Group, Boolean> func = null)
        {
            Content c = b.MoveGroup.Content;
            if (b.MoveGroupLiberties <= 2) return false;
            List<Group> groups = LinkHelper.GetPreviousMoveGroup(board, b);
            if (groups.Count == 1) return false;
            groups = groups.Where(n => n.Liberties.Count == 2).ToList();
            if (func != null) groups.RemoveAll(s => func(s));
            if (CheckImmovableGroups(board, groups).Any())
                return true;
            return false;
        }

        /// <summary>
        /// Move at tiger mouth.
        /// <see cref="UnitTestProject.LifeCheckTest.LifeCheckTest_Scenario_XuanXuanGo_B3" />
        /// <see cref="UnitTestProject.LifeCheckTest.LifeCheckTest_Scenario_TianLongTu_Q16571" />
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
                if (!KoHelper.MakeKoFight(b, q, c)) continue;
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
            Point p = b.GetStoneNeighbours(tigerMouth).FirstOrDefault(n => b[n] == Content.Empty);
            if (!Convert.ToBoolean(p.NotEmpty)) return false;
            List<Point> nPoints = b.GetStoneNeighbours(p).Where(n => !n.Equals(tigerMouth)).ToList();
            List<Point> rc = nPoints.Where(n => b[n] == c.Opposite()).ToList();
            if (rc.Count != nPoints.Count - 1) return false;
            Point q = nPoints.Except(rc).First();
            if (b[q] != Content.Empty) return false;
            //make move to form tiger mouth
            (_, Board b2) = ImmovableHelper.IsSuicidalMove(q, c.Opposite(), b, true);
            if (b2 == null) return false;
            //make ko move
            if (!KoHelper.MakeKoFight(b2, tigerMouth, c.Opposite()))
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
        public static Boolean LinkBreakage(Board b)
        {
            Point move = b.Move.Value;
            Content c = b.MoveGroup.Content;
            List<Point> npoints = LinkHelper.GetDiagonalPoints(b);
            List<Point> diagonals = b.GetDiagonalNeighbours().Where(n => b[n] == Content.Empty && b.GetStoneNeighbours(n).Intersect(npoints).Count() >= 2).ToList();
            if (!diagonals.Any()) return false;
            foreach (Point d in diagonals)
            {
                if (ImmovableHelper.IsSuicidalMove(b, d, c, true)) continue;
                HashSet<Group> ngroups = b.GetGroupsFromPoints(LinkHelper.PointsBetweenDiagonals(move, d));
                if (ngroups.Count == 1) continue;
                if (ngroups.Any(n => n.Liberties.Count == 1)) continue;
                if (ngroups.Any(n => !WallHelper.IsNonKillableGroup(b, n)))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Check negligible for links.
        /// <see cref="UnitTestProject.LifeCheckTest.LifeCheckTest_Scenario_Nie60_4" />
        /// <see cref="UnitTestProject.LifeCheckTest.LifeCheckTest_Scenario_WindAndTime_Q30150" />
        /// <see cref="UnitTestProject.LinkHelperTest.LinkHelperTest_Scenario_WindAndTime_Q30150_9" />
        /// </summary>
        public static Boolean CheckNegligibleForLinks(Board b, Board board, Func<Group, Boolean> func = null)
        {
            if (Board.ResolveAtari(board, b) || b.CapturedList.Any(n => CheckImmovableNeighbourGroups(board, n).Any()))
                return true;

            List<Group> ngroups = b.GetNeighbourGroups().Where(n => (func != null ? func(n) : true)).ToList();
            if (ngroups.Any(n => ImmovableHelper.CheckConnectAndDie(b, n) && !ImmovableHelper.CheckConnectAndDie(board, n, false)))
                return true;

            return false;
        }

        public static IEnumerable<Group> CheckImmovableNeighbourGroups(Board board, Group group)
        {
            List<Group> ngroups = board.GetNeighbourGroups(group).Where(g => g.Liberties.Count <= 2).ToList();
            return CheckImmovableGroups(board, ngroups);
        }

        public static IEnumerable<Group> CheckImmovableGroups(Board board, List<Group> groups)
        {
            foreach (Group group in groups)
            {
                Content c = group.Content;
                if (group.Liberties.Any(n => ImmovableHelper.IsSuicidalMove(board, n, c)))
                    yield return group;
            }
        }
    }
}
