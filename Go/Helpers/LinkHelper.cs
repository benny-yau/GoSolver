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
        /// </summary>
        public static Boolean PossibleLinkForGroups(Board tryBoard, Board currentBoard)
        {
            Point move = tryBoard.Move.Value;
            Content c = tryBoard.MoveGroup.Content;
            if (tryBoard.CapturedList.Count == 0 && ImmovableHelper.CheckConnectAndDie(tryBoard))
                return false;

            //get all possible link groups
            List<Point> groupPoints = currentBoard.GetStoneAndDiagonalNeighbours(move).Where(n => currentBoard[n] == c).ToList();
            tryBoard.CapturedList.ForEach(q => groupPoints.AddRange(q.Neighbours.Where(n => currentBoard[n] == c)));
            List<Group> groups = currentBoard.GetGroupsFromPoints(groupPoints).ToList();
            //get leap groups
            GetPossibleLeapGroups(tryBoard, currentBoard, groups);

            //find possible links between all groups
            if (groups.Count > 1)
            {
                for (int i = 0; i <= groups.Count - 2; i++)
                {
                    for (int j = (i + 1); j <= groups.Count - 1; j++)
                    {
                        if (groups[i] == groups[j]) continue;
                        if (WallHelper.IsNonKillableGroup(currentBoard, groups[i]) && WallHelper.IsNonKillableGroup(currentBoard, groups[j])) continue;
                        Group groupI = tryBoard.GetGroupAt(groups[i].Points.First());
                        groupI.LinkedPoint = groups[i].LinkedPoint;
                        Group groupJ = tryBoard.GetGroupAt(groups[j].Points.First());
                        groupJ.LinkedPoint = groups[j].LinkedPoint;
                        if ((groupI.Liberties.Count == 1 && KoHelper.IsCaptureKoFight(tryBoard, groupI) == null) || (groupJ.Liberties.Count == 1 && KoHelper.IsCaptureKoFight(tryBoard, groupJ) == null))
                            continue;
                        //check if currently linked
                        Boolean isLinked = (groupI == groupJ) || PossibleLinkToAnyGroup(tryBoard, groupI, groupJ);
                        if (isLinked)
                        {
                            //check if previously linked
                            Boolean previousLinked = IsImmediateDiagonallyConnected(currentBoard, groups[i], groups[j]) || IsDiagonallyConnectedGroups(currentBoard, groups[i], groups[j]);
                            if (previousLinked) continue;
                            return true;
                        }
                    }
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
            List<Point> adjacentPoints = tryBoard.GetStoneAndDiagonalNeighbours().Where(n => tryBoard[n] == c).ToList();
            if (adjacentPoints.Count == 0) return false;
            if (adjacentPoints.Count > 1 && !tryBoard.GetStoneNeighbours(adjacentPoints[0]).Contains(adjacentPoints[1])) return false;

            //get diagonal in leap direction
            List<Point> diagonalInLeapDirection = tryBoard.GetDiagonalNeighbours().Where(n => tryBoard.PointWithinMiddleArea(n) && !adjacentPoints.Contains(n) && !tryBoard.GetStoneNeighbours(n).Any(s => adjacentPoints.Contains(s))).ToList();
            if (diagonalInLeapDirection.Count != 1) return false;
            Point d = diagonalInLeapDirection.First();

            //ensure movable point at diagonal
            if (!tryBoard.GameInfo.IsMovablePoint[d.x, d.y]) return false;

            //make block move
            Point blockMove = tryBoard.GetStoneNeighbours(d).First(n => !tryBoard.PointWithinMiddleArea(n));
            Board b = tryBoard.MakeMoveOnNewBoard(blockMove, c.Opposite());
            if (b == null || !ImmovableHelper.CheckConnectAndDie(b)) return false;
            return true;
        }

        /// <summary>
        /// Get possible leap groups.
        /// <see cref="UnitTestProject.LinkHelperTest.LinkHelperTest_Scenario_WindAndTime_Q30274" />
        /// </summary>
        public static void GetPossibleLeapGroups(Board tryBoard, Board currentBoard, List<Group> groups)
        {
            Point move = tryBoard.Move.Value;
            Content c = tryBoard.MoveGroup.Content;
            List<Point> closestNeighbours = tryBoard.GetClosestNeighbour(move, 2, c);
            //validate leap move
            closestNeighbours = closestNeighbours.Where(leapMove => RedundantMoveHelper.ValidateLeapMove(tryBoard, move, leapMove, false)).ToList();
            //add to groups with linked point
            foreach (Point p in closestNeighbours)
            {
                Group group = currentBoard.GetGroupAt(p);
                if (groups.Contains(group)) continue;
                group.LinkedPoint = new LinkedPoint<Point>(move, p);
                groups.Add(group);
            }
        }

        /// <summary>
        /// Is absolute link by stone only.
        /// </summary>
        public static Boolean IsAbsoluteLinkForGroups(Board currentBoard, Board tryBoard)
        {
            Point move = tryBoard.Move.Value;
            Content c = tryBoard.MoveGroup.Content;
            if (tryBoard.MoveGroup.Points.Count == 1) return false;
            List<Group> linkedGroups = currentBoard.GetGroupsFromStoneNeighbours(move, c.Opposite()).ToList();
            return (linkedGroups.Count > 1);
        }

        /// <summary>
        /// Check if diagonals are linked.
        /// Both diagonals empty <see cref="UnitTestProject.LifeCheckTest.LifeCheckTest_Scenario_TianLongTu_Q16571_4" />
        /// <see cref="UnitTestProject.LinkHelperTest.LinkHelperTest_Scenario_TianLongTu_Q16571_2" />
        /// <see cref="UnitTestProject.LinkHelperTest.LinkHelperTest_Scenario_TianLongTu_Q17078" />
        /// Check not negligible <see cref="UnitTestProject.LinkHelperTest.LinkHelperTest_Scenario_WindAndTime_Q30150_4" />
        /// Check any diagonal separated by opposite content <see cref="UnitTestProject.LinkHelperTest.LinkHelperTest_Scenario_WindAndTime_Q30150_5" />
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
                    (Boolean suicidal, Board b) = ImmovableHelper.IsSuicidalMove(p, c.Opposite(), board, false);
                    if (suicidal) return true;
                    //check not negligible
                    Boolean isNegligible = !b.AtariTargets.Any(t => !t.Points.Contains(pointA) && !t.Points.Contains(pointB)) && b.CapturedList.Count == 0 && !Board.ResolveAtari(board, b);
                    if (!isNegligible)
                        return false;

                    Point q = diagonals.First(d => !d.Equals(p));
                    //make connection at other diagonal
                    if (ImmovableHelper.IsSuicidalMove(q, c, b).Item1)
                        return false;
                }
                return true;
            }
            //check any diagonal separated by opposite content
            foreach (Point diagonal in diagonals)
            {
                if (!ImmovableHelper.IsImmovablePoint(diagonal, c, board).Item1) continue;
                //empty point
                if (board[diagonal] == Content.Empty) return true;
                //filled point
                Group killerGroup = GroupHelper.GetKillerGroupFromCache(board, diagonal, c);
                if (killerGroup == null) continue;
                //ensure only one opponent group within killer group
                if (board.GetGroupsFromPoints(killerGroup.Points.Where(p => board[p] == c.Opposite()).ToList()).Count > 1) continue;
                //check capture secure
                if (!ImmovableHelper.CheckCaptureSecure(board, board.GetGroupAt(diagonal))) continue;
                //check double atari
                List<Group> groups = board.GetNeighbourGroups(killerGroup);
                if (groups.Any(group => group.Liberties.Count == 1) || AtariHelper.DoubleAtariOnTargetGroups(board, groups))
                    continue;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Check is diagonal linked, including capture ko test.
        /// </summary>
        public static Boolean CheckIsDiagonalLinked(LinkedPoint<Point> diagonal, Board board)
        {
            if (!CheckIsDiagonalLinked(diagonal.Move, (Point)diagonal.CheckMove, board))
                return false;
            if (CaptureKoToTestDiagonalLink(diagonal, board))
                return true;
            return false;
        }

        /// <summary>
        /// Capture ko to test diagonal link, in case of double ko.
        /// <see cref="UnitTestProject.LifeCheckTest.LifeCheckTest_Scenario_XuanXuanQiJing_Weiqi101_18497_2" />
        /// </summary>
        private static Boolean CaptureKoToTestDiagonalLink(LinkedPoint<Point> diagonal, Board board, Boolean immediateLink = false)
        {
            Content c = board[diagonal.Move];
            List<Point> diagonals = LinkHelper.PointsBetweenDiagonals(diagonal);
            foreach (Point d in diagonals)
            {
                Board b = KoHelper.IsCaptureKoFight(board, d, c, true);
                if (b == null) continue;
                if (b[diagonal.Move] != c || b[(Point)diagonal.CheckMove] != c) return false;
                if (immediateLink && !CheckIsDiagonalImmediateLinked(diagonal, b)) return false;
                if (!immediateLink && !CheckIsDiagonalLinked(diagonal, b)) return false;
            }
            return true;
        }

        /// <summary>
        /// Check if diagonals are immediately linked.
        /// </summary>
        public static Boolean CheckIsDiagonalImmediateLinked(Point pointA, Point pointB, Board board)
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
                    (Boolean suicidal, Board b) = ImmovableHelper.IsSuicidalMove(p, c.Opposite(), board, false);
                    if (suicidal) return true;

                    Point q = diagonals.First(d => !d.Equals(p));
                    //make connection at other diagonal
                    if (ImmovableHelper.IsSuicidalMove(q, c, b).Item1)
                        return false;
                }
                return true;
            }
            //check any diagonal separated by opposite content
            foreach (Point diagonal in diagonals)
            {
                if (ImmovableHelper.IsImmovablePoint(diagonal, c, board).Item1) return true;
            }
            return false;
        }

        /// <summary>
        /// Check is diagonal immediate linked, including capture ko test.
        /// </summary>
        public static Boolean CheckIsDiagonalImmediateLinked(LinkedPoint<Point> diagonal, Board board)
        {
            if (!CheckIsDiagonalImmediateLinked(diagonal.Move, (Point)diagonal.CheckMove, board))
                return false;
            if (CaptureKoToTestDiagonalLink(diagonal, board, true))
                return true;
            return false;
        }

        /// <summary>
        /// Check for double linkage.
        /// <see cref="UnitTestProject.LinkHelperTest.LinkHelperTest_Scenario_TianLongTu_Q16571_2" />
        /// <see cref="UnitTestProject.LinkHelperTest.LinkHelperTest_Scenario_TianLongTu_Q16571_3" />
        /// Check double atari at link <see cref="UnitTestProject.LinkHelperTest.LinkHelperTest_DoubleAtariOnLinkage" />
        /// </summary>
        private static Boolean CheckDoubleLinkage(Board board, LinkedPoint<Point> diagonalLink, Content c)
        {
            List<Point> diagonals = LinkHelper.PointsBetweenDiagonals(diagonalLink);
            foreach (Point p in diagonals.Where(d => board[d] == Content.Empty))
            {
                //ensure three opponent groups
                List<Point> opponentStones = board.GetStoneNeighbours(p).Where(n => board[n] == c).ToList();
                HashSet<Group> neighbourGroups = board.GetGroupsFromPoints(opponentStones);
                if (neighbourGroups.Count != 3) continue;

                //make opponent move at diagonal
                (Boolean suicidal, Board b) = ImmovableHelper.IsSuicidalMove(p, c.Opposite(), board);
                if (suicidal)
                {
                    //check double atari at link
                    Point? liberty = ImmovableHelper.FindTigerMouth(board, p, c);
                    if (liberty != null && board[liberty.Value] != Content.Empty)
                    {
                        Group killerGroup = GroupHelper.GetKillerGroupFromCache(board, p, c);
                        if (killerGroup != null && LifeCheck.CheckOpponentDoubleAtari(board, new List<Group>() { killerGroup }))
                            return false;
                    }
                    continue;
                }

                //check if any of the other two diagonals are immovable
                List<Point> otherDiagonals = board.GetDiagonalNeighbours(p).Where(n => board.GetStoneNeighbours(n).Intersect(opponentStones).Count() >= 2).ToList();
                if (otherDiagonals.All(d => !ImmovableHelper.IsImmovablePoint(d, c, b).Item1))
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Check double atari for links.
        /// <see cref="UnitTestProject.LifeCheckTest.LifeCheckTest_Scenario_TianLongTu_Q16571_7" />
        /// <see cref="UnitTestProject.LifeCheckTest.LifeCheckTest_Scenario_TianLongTu_Q16571_5" />
        /// <see cref="UnitTestProject.LinkHelperTest.LinkHelperTest_Scenario_Nie60" />
        /// </summary>
        private static Boolean CheckDoubleAtariForLinks(Board board, Group group, LinkedPoint<Point> diagonalPoint)
        {
            Content c = group.Content;
            List<Point> diagonals = LinkHelper.PointsBetweenDiagonals(diagonalPoint);
            diagonals.RemoveAll(diagonal => board[diagonal] != Content.Empty);
            if (diagonals.Count != 1) return false;
            Point d = diagonals.First();
            List<Group> tigerMouthGroups = board.GetGroupsFromStoneNeighbours(d, c.Opposite()).Where(n => n.Liberties.Count == 2).ToList();
            return AtariHelper.DoubleAtariOnTargetGroups(board, tigerMouthGroups);
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
            IsDiagonallyConnectedGroups(allConnectedGroups, board, group);
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
            return tryBoard.GetDiagonalNeighbours().Where(n => tryBoard[n] == c && !tryBoard.MoveGroup.Points.Contains(n)).ToList();
        }

        /// <summary>
        /// Find if two groups are connected diagonally. If find group parameter is null then look for all connected groups.
        /// </summary>
        public static Boolean IsDiagonallyConnectedGroups(HashSet<Group> allConnectedGroups, Board board, Group group, Group findGroup = null)
        {
            //find group diagonals of same content
            List<LinkedPoint<Point>> diagonalPoints = GetGroupLinkedDiagonals(board, group).OrderBy(d => board.GetGroupAt(d.Move).Liberties.Count).ToList();
            if (findGroup != null)
            {
                //diagonal with find group
                List<LinkedPoint<Point>> diagonalWithFindGroup = diagonalPoints.Where(d => board.GetGroupAt(d.Move) == findGroup).ToList();
                if (diagonalWithFindGroup.Count > 0) diagonalPoints = diagonalWithFindGroup;
            }
            foreach (LinkedPoint<Point> diagonalPoint in diagonalPoints)
            {
                Group g = board.GetGroupAt(diagonalPoint.Move);
                if (g.Liberties.Count == 1 || allConnectedGroups.Contains(g)) continue;

                //check if diagonally linked
                if (!CheckIsDiagonalLinked(diagonalPoint, board))
                    continue;

                //check tiger mouth exceptions
                if (CheckTigerMouthExceptionsForLinks(board, g))
                    continue;

                //check for links with double linkage
                if (!CheckDoubleLinkage(board, diagonalPoint, group.Content))
                    continue;

                //check double atari for links
                if (CheckDoubleAtariForLinks(board, g, diagonalPoint))
                    continue;

                allConnectedGroups.Add(g);

                //check if group found
                if (findGroup != null && findGroup == g)
                    return true;

                //get diagonal connected groups recursively
                Boolean connected = IsDiagonallyConnectedGroups(allConnectedGroups, board, g, findGroup);
                if (connected) return true;
            }
            return false;
        }

        public static Boolean IsDiagonallyConnectedGroups(Board board, Group group, Group findGroup)
        {
            return IsDiagonallyConnectedGroups(new HashSet<Group>() { group }, board, group, findGroup);
        }

        /// <summary>
        /// Is immediate diagonally connected. 
        /// </summary>
        public static Boolean IsImmediateDiagonallyConnected(Board board, Group group, Group findGroup)
        {
            LinkedPoint<Point> diagonalLink = GetGroupLinkedDiagonals(board, group).FirstOrDefault(d => board.GetGroupAt(d.Move) == findGroup);
            if (diagonalLink == null) return false;
            return CheckIsDiagonalImmediateLinked(diagonalLink, board);
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
        public static Boolean PossibleLinkToAnyGroup(Board board, Group group, Group findGroup)
        {
            //link between the two groups
            if (CheckPossibleLink(board, group, findGroup))
                return true;

            //link through move group
            Boolean isLinked = (group == board.MoveGroup || CheckPossibleLink(board, group, board.MoveGroup));
            Boolean isLinked2 = (findGroup == board.MoveGroup || CheckPossibleLink(board, findGroup, board.MoveGroup));

            return isLinked && isLinked2;
        }

        private static Boolean CheckPossibleLink(Board board, Group group, Group findGroup)
        {
            //check diagonal links
            LinkedPoint<Point> diagonalLink = GetGroupLinkedDiagonals(board, group).FirstOrDefault(d => board.GetGroupAt(d.Move) == findGroup);
            if (diagonalLink != null) return true;

            //check leap moves
            if (group.LinkedPoint != null && findGroup.Points.Contains(group.LinkedPoint.Move))
                return true;
            if (findGroup.LinkedPoint != null && group.Points.Contains(findGroup.LinkedPoint.Move))
                return true;
            return false;
        }

        /// <summary>
        /// Check tiger mouth exceptions for links.
        /// <see cref="UnitTestProject.LinkHelperTest.LinkHelperTest_Scenario_WindAndTime_Q30150_2" /> 
        /// <see cref="UnitTestProject.LinkHelperTest.LinkHelperTest_Scenario_WindAndTime_Q30150_3" /> 
        /// </summary>
        public static Boolean CheckTigerMouthExceptionsForLinks(Board board, Group group)
        {
            List<Point> tigerMouthList = GetTigerMouthsOfLinks(board, group);
            if (LifeCheck.CheckTigerMouthExceptions(board, tigerMouthList, group.Content))
                return true;
            return false;
        }

        /// <summary>
        /// Get tiger mouth of links.
        /// </summary>
        public static List<Point> GetTigerMouthsOfLinks(Board board, Group group)
        {
            List<Point> tigerMouthList = new List<Point>();
            Content c = group.Content;
            List<LinkedPoint<Point>> diagonalPoints = LinkHelper.GetGroupDiagonals(board, group);
            foreach (LinkedPoint<Point> p in diagonalPoints)
            {
                List<Point> pointsBetweenDiagonals = LinkHelper.PointsBetweenDiagonals(p);
                foreach (Point q in pointsBetweenDiagonals)
                {
                    if (board[q] == Content.Empty && ImmovableHelper.FindTigerMouth(board, c, q))
                    {
                        if (board.GetGroupsFromStoneNeighbours(q, c.Opposite()).Count() == 1) continue;
                        //ensure tiger mouth is immovable
                        if (ImmovableHelper.IsImmovablePoint(q, c, board).Item1)
                            tigerMouthList.Add(q);
                    }
                }
            }
            return tigerMouthList.Distinct().ToList();
        }

        /// <summary>
        /// Get all diagonal groups for checking neighbour groups of killer group.
        /// </summary>
        public static List<Group> GetAllDiagonalGroups(Board board, Group group, List<Group> groups = null, Boolean recursive = true)
        {
            if (groups == null)
            {
                groups = new List<Group>();
                groups.Add(group);
            }
            List<LinkedPoint<Point>> diagonalPoints = GetGroupLinkedDiagonals(board, group);
            foreach (LinkedPoint<Point> diagonalPoint in diagonalPoints)
            {
                Group g = board.GetGroupAt(diagonalPoint.Move);
                if (groups.Contains(g)) continue;
                groups.Add(g);
                if (recursive)
                    GetAllDiagonalGroups(board, g, groups);
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
            if (board.GetNeighbourGroups(group).Count <= 1) return (null, null);
            List<LinkedPoint<Point>> diagonals = GetGroupDiagonals(board, group).Where(d => board[d.Move] == c && board[(Point)d.CheckMove] == c).ToList();
            foreach (LinkedPoint<Point> diagonal in diagonals)
            {
                if (board.GetGroupAt(diagonal.Move).Liberties.Count == 1) continue;
                List<Point> pointsBetweenDiagonals = PointsBetweenDiagonals(diagonal);
                if (pointsBetweenDiagonals.All(d => board[d] == c.Opposite() && board.GetGroupAt(d).Liberties.Count > 1))
                    return (diagonal.Move, pointsBetweenDiagonals);
            }
            return (null, null);
        }

        /// <summary>
        /// Diagonal cut at move.
        /// </summary>
        public static (Boolean, List<Point>) DiagonalCutMove(Board board)
        {
            Point move = board.Move.Value;
            Content c = board.MoveGroup.Content;
            if (board.GetNeighbourGroups().Count <= 1) return (false, null);
            List<Point> diagonals = board.GetDiagonalNeighbours().Where(d => board[d] == c).ToList();
            foreach (Point diagonal in diagonals)
            {
                if (board.GetGroupAt(diagonal).Liberties.Count == 1) continue;
                List<Point> pointsBetweenDiagonals = PointsBetweenDiagonals(diagonal, move);
                if (pointsBetweenDiagonals.All(d => board[d] == c.Opposite()))
                    return (true, pointsBetweenDiagonals);
            }
            return (false, null);
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

        /// <summary>
        /// Get stone neighbours at diagonal of each other.
        /// </summary>
        /// <returns></returns>
        public static List<Point> GetNeighboursDiagonallyLinked(Board board)
        {
            Point move = board.Move.Value;
            Content c = board.MoveGroup.Content;
            return GetNeighboursDiagonallyLinked(board, move, c.Opposite());
        }

        public static List<Point> GetNeighboursDiagonallyLinked(Board board, Point p, Content c)
        {
            List<Point> stoneNeighbours = board.GetStoneNeighbours(p).Where(n => board[n] == c).ToList();
            if (stoneNeighbours.Count == 0) return stoneNeighbours;
            stoneNeighbours = stoneNeighbours.Where(n => board.GetDiagonalNeighbours(n).Intersect(stoneNeighbours).Any()).ToList();
            return stoneNeighbours;
        }
    }
}
