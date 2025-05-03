using System;
using System.Collections.Generic;
using System.Linq;

namespace Go
{
    public class ImmovableHelper
    {
        /// <summary>
        /// Find tiger mouth where mouth point is empty or filled. Content in parameter represents content of stones forming the tiger mouth. 
        /// </summary>
        public static Point? FindTigerMouth(Board board, Point p, Content c)
        {
            Content content = board[p];
            List<Point> nstones = board.GetStoneNeighbours(p);
            if (nstones.Count(n => board[n] == c) != nstones.Count - 1) return null;
            if (content == Content.Empty)
            {
                //make move into tiger mouth
                Board b = board.MakeMoveOnNewBoard(p, c.Opposite(), true);
                if (b == null || b.MoveGroupLiberties != 1) return null;
                return b.MoveGroup.Liberties.First();
            }
            else if (content == c.Opposite())
            {
                Board b = ImmovableHelper.CaptureSuicideGroup(p, board);
                if (b != null) return b.Move;
            }
            return null;
        }

        public static Boolean FindEmptyTigerMouth(Board board, Content c, Point p)
        {
            return (board[p] == Content.Empty && FindTigerMouth(board, p, c) != null);
        }

        /// <summary>
        /// Get diagonals of tiger mouth.
        /// </summary>
        public static List<Point> GetDiagonalsOfTigerMouth(Board board, Point p, Content c)
        {
            List<Point> opponentStones = board.GetStoneNeighbours(p).Where(n => board[n] == c).ToList();
            return board.GetDiagonalNeighbours(p).Where(n => board.GetStoneNeighbours(n).Intersect(opponentStones).Count() >= 2).ToList();
        }

        /// <summary>
        /// Immovable point to check for links and diagonal points in semi solid eye. 
        /// Empty point <see cref="UnitTestProject.SurvivalTigerMouthMoveTest.SurvivalTigerMouthMoveTest_Scenario_GuanZiPu_A3" />
        /// Check connect and die <see cref="UnitTestProject.LifeCheckTest.LifeCheckTest_Scenario_Corner_A28" />
        /// Check filled point connect and die <see cref="UnitTestProject.NeutralPointMoveTest.NeutralPointMoveTest_Scenario_TianLongTu_Q16975" />
        /// <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_Scenario_XuanXuanGo_Q18341" />
        /// <see cref="UnitTestProject.ThreeLibertySuicidalTest.ThreeLibertySuicidalTest_Scenario_TianLongTu_Q14992_2" />
        /// Check for ko possibility <see cref="UnitTestProject.LifeCheckTest.LifeCheckTest_Scenario_WuQingYuan_Q30986" />
        /// </summary>
        public static (Boolean, Point?) IsImmovablePoint(Point p, Content c, Board board)
        {
            if (board[p] == Content.Empty) //empty point
            {
                if (PrecheckNotSuicidal(board, p, c.Opposite()))
                    return (false, null);
                (Boolean suicidal, Board b) = ImmovableHelper.IsSuicidalMove(p, c.Opposite(), board, true);
                if (!suicidal)
                    return (false, null);

                if (b == null)
                {
                    //check connect and die
                    if (!WallHelper.StrongNeighbourGroups(board, p, c.Opposite()))
                        return (false, null);
                    return (true, null);
                }
                //ensure tiger mouth without ko or snapback
                Board b2 = IsConfirmTigerMouth(board, b);
                if (b2 != null)
                    return (true, b2.Move);
            }
            else //filled point
            {
                if (board[p] == c) return (true, null);

                Group targetGroup = board.GetGroupAt(p);
                (Boolean unEscapable, _) = UnescapableGroup(board, targetGroup);
                if (!unEscapable)
                    return (false, null);

                //check filled point connect and die
                if (!WallHelper.StrongNeighbourGroups(board, targetGroup))
                    return (false, null);

                //check for ko possibility
                Point? q = ImmovableHelper.GetLibertyPoint(board, targetGroup);
                if (q != null && CheckForKoInImmovablePoint(board, targetGroup, q.Value))
                    return (false, null);

                return (true, q);
            }
            return (false, null);
        }

        public static Boolean IsImmovablePoint(Board board, Point p, Content c)
        {
            return IsImmovablePoint(p, c, board).Item1;
        }

        /// <summary>
        /// Check for ko in immovable point.
        /// Check for ko by capture neighbour groups <see cref="UnitTestProject.LifeCheckTest.LifeCheckTest_Scenario_WuQingYuan_Q30986" />
        /// <see cref="UnitTestProject.LifeCheckTest.LifeCheckTest_Scenario_WindAndTime_Q29998_2" />
        /// <see cref="UnitTestProject.LifeCheckTest.LifeCheckTest_Scenario_TianLongTu_Q16446" />
        /// Check for reverse ko fight <see cref="UnitTestProject.LifeCheckTest.LifeCheckTest_Scenario_WindAndTime_Q29998" />
        /// </summary>
        private static Boolean CheckForKoInImmovablePoint(Board board, Group targetGroup, Point q)
        {
            if (targetGroup.Points.Count != 1 || targetGroup.Liberties.Count != 1) return false;
            Content c = targetGroup.Content.Opposite();
            //check for ko by capture neighbour groups
            List<Group> ngroups = board.GetGroupsFromStoneNeighbours(q, c).Where(n => n.Liberties.Count == 1).ToList();
            if (ngroups.Count > 1)
            {
                foreach (Group group in ngroups)
                {
                    (Boolean unEscapable, Board b) = ImmovableHelper.UnescapableGroup(board, group);
                    if (!unEscapable && KoHelper.IsKoFight(b, targetGroup))
                        return true;
                }
            }
            else if (KoHelper.IsKoFight(board, targetGroup))
                return true;

            //check for reverse ko fight 
            List<Point> nstones = board.GetStoneNeighbours(q);
            if (nstones.Any(n => board[n] == c)) return false;
            List<Point> eyeNeighbour = nstones.Where(n => board[n] == Content.Empty).ToList();
            if (eyeNeighbour.Count == 1 && KoHelper.MakeKoFight(board, eyeNeighbour.First(), c.Opposite()))
                return true;
            return false;
        }

        /// <summary>
        /// Is confirm tiger mouth.
        /// <see cref="UnitTestProject.ImmovableTest.ImmovableTest_Scenario_TianLongTu_Q16827" />
        /// Check connect and die on current board <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_GuanZiPu_A17_2" />
        /// Check connect and die on captured board <see cref="UnitTestProject.ImmovableTest.ImmovableTest_Scenario_XuanXuanGo_B32" />
        /// <see cref="UnitTestProject.LifeCheckTest.LifeCheckTest_Scenario1dan21" />
        /// </summary>.
        public static Board IsConfirmTigerMouth(Board currentBoard, Board tryBoard, Point? p = null)
        {
            if (p == null) p = tryBoard.Move;
            Content c = tryBoard.MoveGroup.Content;

            Point? libertyPoint = FindTigerMouth(currentBoard, p.Value, c.Opposite());
            if (libertyPoint == null) return null;

            Board capturedBoard = CaptureSuicideGroup(p.Value, tryBoard);
            if (capturedBoard == null) return null;
            //ensure not ko
            if (KoHelper.IsKoFight(capturedBoard))
                return null;

            //check connect and die on current board
            if (!WallHelper.StrongNeighbourGroups(currentBoard, p.Value, c))
                return null;

            //check connect and die on captured board
            if (!WallHelper.StrongNeighbourGroups(capturedBoard, p.Value, c))
                return null;

            //check suicidal move at side of board
            if (SuicidalAfterMustHaveMove(currentBoard, tryBoard, libertyPoint.Value))
                return null;

            return capturedBoard;
        }


        /// <summary>
        /// Three liberty connect and die.
        /// <see cref="UnitTestProject.ThreeLibertySuicidalTest.ThreeLibertySuicidalTest_Scenario_TianLongTu_Q14992_2" />
        /// <see cref="UnitTestProject.ThreeLibertySuicidalTest.ThreeLibertySuicidalTest_Scenario_TianLongTu_Q14992" />
        /// Stone neighbours at diagonal of each other <see cref="UnitTestProject.ThreeLibertySuicidalTest.ThreeLibertySuicidalTest_Scenario_Side_B19" />
        /// Check if escapable <see cref="UnitTestProject.ThreeLibertySuicidalTest.ThreeLibertySuicidalTest_Scenario_Corner_A86" />
        /// </summary>
        public static (Boolean, Board) ThreeLibertyConnectAndDie(Board board, Point? p = null, Group targetGroup = null)
        {
            if (targetGroup == null) targetGroup = board.MoveGroup;
            if (targetGroup.Liberties.Count != 3) return (false, null);
            Content c = targetGroup.Content;

            if (p == null)
            {
                Point liberty = targetGroup.Liberties.FirstOrDefault(n => ImmovableHelper.FindEmptyTigerMouth(board, c, n) && EyeHelper.IsCovered(board, n, c));
                if (!Convert.ToBoolean(liberty.NotEmpty)) return (false, null);
                p = liberty;
            }
            else if (!ImmovableHelper.FindEmptyTigerMouth(board, c, p.Value)) return (false, null);

            //stone neighbours at diagonal of each other
            List<Point> nstones = LinkHelper.GetNeighboursDiagonallyLinked(board, p.Value, c);
            if (!nstones.Any()) return (false, null);

            Board b = board.MakeMoveOnNewBoard(p.Value, c.Opposite());
            if (b == null || b.MoveGroupLiberties != 1) return (false, null);

            Board b2 = ImmovableHelper.CaptureSuicideGroup(b);
            if (b2 == null || b2.MoveGroupLiberties != 2) return (false, null);
            if (!EyeHelper.FindCoveredEye(b2, p.Value, c)) return (false, null);
            if (CheckConnectAndDie(b2, targetGroup))
                return (true, b2);
            return (false, null);
        }

        /// <summary>
        /// Two and three liberties connect and die.
        /// </summary>
        public static Boolean TwoAndThreeLibertiesConnectAndDie(Board board, Group targetGroup = null, Point? p = null)
        {
            if (ImmovableHelper.CheckConnectAndDie(board, targetGroup)) return true;
            if (ImmovableHelper.ThreeLibertyConnectAndDie(board, p, targetGroup).Item1) return true;
            return false;
        }

        /// <summary>
        /// Escape link.
        /// </summary>
        public static Boolean EscapePreAtari(Board board, Group targetGroup)
        {
            IEnumerable<Board> moveBoards = GameHelper.GetMoveBoards(board, targetGroup.Liberties, targetGroup.Content);
            foreach (Board b in moveBoards)
            {
                if (!LinkHelper.IsAbsoluteLinkForGroups(board, b)) continue;
                if (!ImmovableHelper.CheckConnectAndDie(b, targetGroup, false))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Escape capture link. Check if escapable for target group to obtain more than two liberties or become non killable.
        /// <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_Scenario_XuanXuanGo_A26_3" />
        /// </summary>
        public static Boolean EscapeCaptureLink(Board board, Group targetGroup)
        {
            IEnumerable<Board> moveBoards = GameHelper.GetMoveBoards(board, targetGroup.Liberties, targetGroup.Content);
            foreach (Board b in moveBoards)
            {
                if (!LinkHelper.IsAbsoluteLinkForGroups(board, b)) continue;
                Group target = b.GetCurrentGroup(targetGroup);
                if (target.Liberties.Count > 2 || WallHelper.IsNonKillableGroup(b, target))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Suicide move for survival after must-have neutral move at side of the board.
        /// <see cref="UnitTestProject.LifeCheckTest.LifeCheckTest_Scenario_XuanXuanGo_Q18500" />
        /// <see cref="UnitTestProject.LifeCheckTest.LifeCheckTest_Scenario1dan21" />
        /// </summary>
        public static Boolean SuicidalAfterMustHaveMove(Board currentBoard, Board tryBoard, Point libertyPoint)
        {
            Point move = tryBoard.Move.Value;
            Content c = tryBoard.MoveGroup.Content;
            Point eyePoint = currentBoard.GetDiagonalNeighbours(move).FirstOrDefault(n => EyeHelper.FindCoveredEye(currentBoard, n, c.Opposite()));
            if (!Convert.ToBoolean(eyePoint.NotEmpty)) return false;
            if (!currentBoard.GetGroupsFromStoneNeighbours(move, c.Opposite()).All(n => n.Liberties.Count <= 2)) return false;

            (Boolean suicidal, Board b) = ImmovableHelper.IsSuicidalMove(libertyPoint, c, currentBoard);
            if (suicidal) return false;

            if (ImmovableHelper.IsSuicidalMove(b, move, c.Opposite()))
                return true;
            return false;
        }

        /// <summary>
        /// Unescapable group. Ensure target group cannot escape by moving at liberty point or capturing neighbour groups.   
        /// <see cref="UnitTestProject.PreAtariMoveTest.PreAtariMoveTest_Scenario_Corner_A85" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_GuanZiPu_Q14981" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_GuanZiPu_A12" />
        /// Check killer ko within killer group <see cref="UnitTestProject.ImmovableTest.ImmovableTest_Scenario_XuanXuanGo_A28_101Weiqi" />
        /// Recursive connect and die <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanGo_A44_101Weiqi" />
        /// <see cref="UnitTestProject.ImmovableTest.ImmovableTest_Scenario_TianLongTu_Q17255" />
        /// </summary>
        public static (Boolean, Board) UnescapableGroup(Board tryBoard, Group targetGroup, Boolean koEnabled = true)
        {
            Content c = targetGroup.Content;
            Group group = tryBoard.GetCurrentGroup(targetGroup);
            if (group.Liberties.Count != 1) return (false, null);

            //check escape by capture
            Board captureBoard = EscapeByCapture(tryBoard, group, koEnabled);
            if (captureBoard != null)
                return (false, captureBoard);

            //make move at liberty
            Board escapeBoard = MakeMoveAtLiberty(tryBoard, group, c);

            //recursive connect and die
            if (escapeBoard == null || CheckConnectAndDie(escapeBoard, group, !koEnabled))
                return (true, escapeBoard);

            return (false, escapeBoard);
        }

        /// <summary>
        /// Check if can escape by capturing neighbour group.
        /// Check snapback <see cref="UnitTestProject.AtariResponseMoveTest.AtariResponseMoveTest_Scenario_TianLongTu_Q16605" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanQiJing_Weiqi101_2282" />
        /// Connect and die <see cref="UnitTestProject.ImmovableTest.ImmovableTest_Scenario_XuanXuanGo_B32" />
        /// Connect and die for move group <see cref="UnitTestProject.ImmovableTest.ImmovableTest_Scenario_GuanZiPu_A3" />
        /// </summary>
        public static Board EscapeByCapture(Board tryBoard, Group group, Boolean koEnabled = true)
        {
            List<Group> atariTargets = AtariHelper.AtariByGroup(group, tryBoard, koEnabled);
            foreach (Group target in atariTargets)
            {
                //make capture move
                (_, Board b) = ImmovableHelper.IsSuicidalOnCapture(tryBoard, target, koEnabled);
                if (b == null) continue;
                //connect and die
                if (CheckConnectAndDie(b, group, !koEnabled))
                    continue;
                return b;
            }
            return null;
        }

        public static Boolean PrecheckNotSuicidal(Board tryBoard, Point p, Content c)
        {
            if (tryBoard.GetStoneNeighbours(p).Count(n => tryBoard[n] == Content.Empty) >= 2)
                return true;
            if (tryBoard.GetGroupsFromStoneNeighbours(p, c.Opposite()).Any(n => n.Liberties.Count >= 3))
                return true;
            return false;
        }

        /// <summary>
        /// Is suicidal move.
        /// </summary>
        public static Boolean IsSuicidalMove(Board tryBoard, Point p, Content c, Boolean overrideKo = false)
        {
            if (PrecheckNotSuicidal(tryBoard, p, c))
                return false;
            return IsSuicidalMove(p, c, tryBoard, overrideKo).Item1;
        }

        /// <summary>
        /// Suicidal move for connect and die <see cref="UnitTestProject.ImmovableTest.ImmovableTest_Scenario_Corner_A80" />
        /// <see cref="UnitTestProject.ImmovableTest.ImmovableTest_Scenario_Corner_A80_2" />
        /// <see cref="UnitTestProject.ImmovableTest.ImmovableTest_Scenario_WuQingYuan_Q31503" />
        /// </summary>
        public static (Boolean, Board) IsSuicidalMove(Point p, Content c, Board tryBoard, Boolean overrideKo = false)
        {
            if (EyeHelper.FindEye(tryBoard, p, c.Opposite()))
            {
                List<Group> eyeGroups = tryBoard.GetGroupsFromStoneNeighbours(p, c).ToList();
                if (eyeGroups.All(n => n.Liberties.Count > 1)) return (true, null);
            }
            Board board = tryBoard.MakeMoveOnNewBoard(p, c, overrideKo);
            if (board == null) return (true, null);
            if (board.MoveGroupLiberties != 1) return (false, board);
            if (KoHelper.IsKoFight(board))
            {
                if (overrideKo) return (false, board);
                return (true, null);
            }
            return (true, board);
        }

        public static Boolean IsSuicidalWithoutKo(Board tryBoard, Group targetGroup = null)
        {
            if (targetGroup == null) targetGroup = tryBoard.MoveGroup;
            return targetGroup.Liberties.Count == 1 && !KoHelper.IsKoFight(tryBoard, targetGroup);
        }

        public static Boolean IsSuicidalWithoutBaseLineKo(Board tryBoard, Group targetGroup = null)
        {
            if (targetGroup == null) targetGroup = tryBoard.MoveGroup;
            if (targetGroup.Liberties.Count != 1) return false;
            if (KoHelper.IsKoFight(tryBoard, targetGroup) && !tryBoard.PointWithinMiddleArea(targetGroup.Liberties.First()))
                return false;
            return true;
        }

        /// <summary>
        /// Is suicide move on capture.
        /// <see cref="UnitTestProject.ImmovableTest.ImmovableTest_Scenario_Corner_B28_2" />
        /// </summary>
        public static (Boolean, Board) IsSuicidalOnCapture(Board tryBoard, Group targetGroup = null, Boolean koEnabled = false)
        {
            Board board = ImmovableHelper.CaptureSuicideGroup(tryBoard, targetGroup, koEnabled);
            if (board == null)
            {
                if (KoHelper.IsKoFight(tryBoard, targetGroup)) return (true, null);
                return (false, null);
            }
            if (board.MoveGroupLiberties != 1) return (false, board);
            if (KoHelper.IsKoFight(board))
            {
                if (koEnabled) return (false, board);
                return (true, null);
            }
            return (true, board);
        }

        /// <summary>
        /// Capture group that has liberty of one only and return the board.
        /// </summary>
        public static Board CaptureSuicideGroup(Point p, Board board, Boolean overrideKo = true)
        {
            if (board[p] == Content.Empty) return null;
            return CaptureSuicideGroup(board, board.GetGroupAt(p), overrideKo);
        }

        public static Board CaptureSuicideGroup(Board board, Group group = null, Boolean overrideKo = true)
        {
            if (group == null) group = board.MoveGroup;
            Content c = group.Content.Opposite();
            Point? p = GetLibertyPoint(board, group);
            if (p == null) return null;
            return board.MakeMoveOnNewBoard(p.Value, c, overrideKo);
        }

        /// <summary>
        /// Get liberty point.
        /// </summary>
        public static Point? GetLibertyPoint(Board tryBoard, Group group = null)
        {
            if (group == null) group = tryBoard.MoveGroup;
            List<Point> liberties = tryBoard.GetGroupLiberties(group);
            if (liberties.Count == 1)
                return liberties.First();
            return null;
        }

        /// <summary>
        /// Make move at liberty.
        /// </summary>
        public static Board MakeMoveAtLiberty(Board tryBoard, Group group, Content c)
        {
            Point? libertyPoint = ImmovableHelper.GetLibertyPoint(tryBoard, group);
            if (libertyPoint == null) return null;
            Board board = tryBoard.MakeMoveOnNewBoard(libertyPoint.Value, c);
            return board;
        }

        /// <summary>
        /// Check capture secure.
        /// </summary>
        public static Boolean CheckCaptureSecure(Board board, Group group, Boolean immovable = false)
        {
            Content c = group.Content;
            Board escapeBoard = ImmovableHelper.MakeMoveAtLiberty(board, group, c);
            if (immovable)
            {
                if (escapeBoard != null) return false;
            }
            else if (escapeBoard != null && escapeBoard.MoveGroupLiberties > 1)
                return false;

            if (AtariHelper.AtariByGroup(group, board).Any())
                return false;
            return true;
        }

        /// <summary>
        /// Check capture secure for single group within killer group.
        /// </summary>
        public static Boolean CheckCaptureSecureForSingleGroup(Board board, Group group)
        {
            if (!GroupHelper.IsSingleGroupWithinKillerGroup(board, group)) return false;
            if (!ImmovableHelper.CheckCaptureSecure(board, group)) return false;
            return true;
        }

        /// <summary>
        /// Is suicide move for both players.
        /// </summary>
        public static Boolean IsSuicidalMoveForBothPlayers(Board tryBoard, Point p, Boolean connectAndDie = false)
        {
            if (!connectAndDie)
            {
                if (ImmovableHelper.IsSuicidalMove(tryBoard, p, Content.Black) && ImmovableHelper.IsSuicidalMove(tryBoard, p, Content.White))
                    return true;
            }
            else
            {
                (Boolean suicidal, Board b) = ImmovableHelper.IsSuicidalMove(p, Content.Black, tryBoard);
                (Boolean suicidal2, Board b2) = ImmovableHelper.IsSuicidalMove(p, Content.White, tryBoard);
                if ((suicidal || ImmovableHelper.CheckConnectAndDie(b)) && (suicidal2 || ImmovableHelper.CheckConnectAndDie(b2)))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Check snapback from move.
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WindAndTime_Q30234" />
        /// <see cref="UnitTestProject.PreAtariMoveTest.PreAtariMoveTest_Scenario_Corner_A55" />
        /// Check base line move <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_TianLongTu_Q16851" /> 
        /// </summary>
        public static Boolean CheckSnapbackFromMove(Board board, Point? eyePoint = null)
        {
            if (eyePoint == null) eyePoint = board.Move.Value;
            Content c = board[eyePoint.Value];
            Group eyeGroup = board.GetGroupAt(eyePoint.Value);
            if (eyeGroup.Liberties.Count != 1) return false;

            if (board.PointWithinMiddleArea(eyePoint.Value))
            {
                //check three opponent groups
                if (!KillerFormationHelper.ThreeOpponentGroupsAtMove(board, eyePoint)) return false;
                List<Point> nstones = board.GetStoneNeighbours(eyePoint).Where(n => board[n] == c.Opposite()).ToList();
                Point middleStone = nstones.First(n => board.GetDiagonalNeighbours(n).Count(d => nstones.Contains(d)) >= 2);
                Group target = board.GetGroupAt(middleStone);
                if (CheckSnapback(board, target, eyeGroup))
                    return true;
            }
            else
            {
                //check base line move
                if (!board.GetDiagonalNeighbours(eyePoint).Any(d => board[d] != c.Opposite())) return false;
                if (board.GetNeighbourGroups(eyeGroup).Any(n => CheckSnapback(board, n, eyeGroup)))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Check snapback.
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_GuanZiPu_A26" />
        /// <see cref="UnitTestProject.LifeCheckTest.LifeCheckTest_Scenario_WuQingYuan_Q31493" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanGo_B31_4" />
        /// Two point move <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WindAndTime_Q30234" />
        /// <see cref="UnitTestProject.LifeCheckTest.LifeCheckTest_Scenario_TianLongTu_Q16924" />
        /// Escape suicide group <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario3dan17_2" />
        /// </summary>
        public static Boolean CheckSnapback(Board board, Group target, Group eyeGroup)
        {
            Content c = target.Content;
            if (target.Points.Count == 1 || target.Liberties.Count != 2) return false;
            IEnumerable<Board> moveBoards = GameHelper.GetMoveBoards(board, target.Liberties, c.Opposite());
            foreach (Board b in moveBoards)
            {
                List<Group> groups = AtariHelper.AtariByGroup(target, b).Where(n => !n.Equals(b.GetCurrentGroup(eyeGroup))).ToList();
                foreach (Group group in groups)
                {
                    //capture suicide group
                    Board b2 = ImmovableHelper.CaptureSuicideGroup(b, group);
                    if (b2 == null || b2.MoveGroup.Points.Count == 1 || b2.MoveGroupLiberties != 1) continue;
                    //capture eye group
                    Board b3 = ImmovableHelper.CaptureSuicideGroup(b, eyeGroup);
                    if (b3 == null) continue;
                    if (b3.MoveGroupLiberties == 1) return true;
                    //escape suicide group
                    Board escapeBoard = MakeMoveAtLiberty(b3, group, c.Opposite());
                    if (escapeBoard != null && LinkHelper.IsAbsoluteLinkForGroups(b3, escapeBoard))
                        return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Check for connect and die on board with captured suicide stone.
        /// <see cref="UnitTestProject.ImmovableTest.ImmovableTest_Scenario_Corner_A80" />
        /// <see cref="UnitTestProject.ImmovableTest.ImmovableTest_Scenario_XuanXuanGo_B32" />
        /// Suicidal capture <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanQiJing_B25" />
        /// <see cref="UnitTestProject.SpecificNeutralMoveTest.SpecificNeutralMoveTest_Scenario_Corner_A55" />
        /// </summary>
        public static (Boolean, Board) ConnectAndDie(Board board, Group targetGroup = null, Boolean koEnabled = true)
        {
            targetGroup = (targetGroup) ?? board.MoveGroup;
            Content c = targetGroup.Content;
            Group group = board.GetCurrentGroup(targetGroup);
            if (group.Liberties.Count > 2) return (false, null);

            List<KeyValuePair<LinkedPoint<Point>, Board>> killBoards = new List<KeyValuePair<LinkedPoint<Point>, Board>>();
            foreach (Point liberty in group.Liberties)
            {
                if (!GameHelper.SetupMoveAvailable(board, liberty)) continue;
                (_, Board b) = ImmovableHelper.IsSuicidalMove(liberty, c.Opposite(), board, koEnabled);
                if (b == null) continue;
                int moveLiberties = KillerFormationHelper.GetLibertiesAtMove(b).Count();
                int moveGroupLiberties = b.MoveGroupLiberties;
                killBoards.Add(new KeyValuePair<LinkedPoint<Point>, Board>(new LinkedPoint<Point>(liberty, new { moveLiberties, moveGroupLiberties }), b));
            }

            foreach (KeyValuePair<LinkedPoint<Point>, Board> kvp in killBoards.OrderByDescending(b => ((dynamic)b.Key.CheckMove).moveLiberties).ThenByDescending(b => ((dynamic)b.Key.CheckMove).moveGroupLiberties))
            {
                Board b = kvp.Value;
                //check if captured
                if (b.IsCapturedGroup(group))
                    return (true, b);

                //check if connect and die
                if (UnescapableGroup(b, group, !koEnabled).Item1)
                    return (true, b);
            }
            return (false, null);
        }

        public static Boolean CheckConnectAndDie(Board board, Group targetGroup = null, Boolean koEnabled = true)
        {
            targetGroup = (targetGroup) ?? board.MoveGroup;
            if (board.GetGroupLiberties(targetGroup).Count == 1) return true;
            return ConnectAndDie(board, targetGroup, koEnabled).Item1;
        }


        /// <summary>
        /// Check connect and die for captured board.
        /// </summary>
        public static Boolean AllConnectAndDie(Board capturedBoard, Point p, Content c = Content.Unknown)
        {
            if (c == Content.Unknown)
            {
                List<Point> nstones = capturedBoard.GetStoneNeighbours(p).Where(q => capturedBoard[q] != Content.Empty).ToList();
                c = capturedBoard[nstones.First()];
                if (nstones.Any(n => capturedBoard[n] != c))
                    throw new Exception("Different content in connect and die.");
            }
            List<Group> ngroups = capturedBoard.GetGroupsFromStoneNeighbours(p, c.Opposite()).ToList();
            return ngroups.Any(group => CheckConnectAndDie(capturedBoard, group));
        }

        /// <summary>
        /// Suicide at big tiger mouth.
        /// <see cref="UnitTestProject.FillKoEyeMoveTest.FillKoEyeMoveTest_Scenario_GuanZiPu_B3" /> 
        /// <see cref="UnitTestProject.FillKoEyeMoveTest.FillKoEyeMoveTest_Scenario_Corner_A85" /> 
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario6kyu13" />
        /// Check for groups at liberty <see cref="UnitTestProject.MustHaveNeutralMoveTest.MustHaveNeutralMoveTest_Scenario_Side_B19" />
        /// <see cref="UnitTestProject.MustHaveNeutralMoveTest.MustHaveNeutralMoveTest_Scenario_XuanXuanGo_A23" />
        /// <see cref="UnitTestProject.MustHaveNeutralMoveTest.MustHaveNeutralMoveTest_Scenario_XuanXuanQiJing_Weiqi101_7245" />
        /// <see cref="UnitTestProject.MustHaveNeutralMoveTest.MustHaveNeutralMoveTest_Scenario_TianLongTu_Q16827_2" />
        /// Check for opponent survival move <see cref="UnitTestProject.FillKoEyeMoveTest.FillKoEyeMoveTest_Scenario_WindAndTime_Q29475" /> 
        /// <see cref="UnitTestProject.MustHaveNeutralMoveTest.MustHaveNeutralMoveTest_Scenario_XuanXuanQiJing_Weiqi101_7245_2" />
        /// <see cref="UnitTestProject.BaseLineKillerMoveTest.BaseLineKillerMoveTest_Scenario_XuanXuanQiJing_A53" /> 
        /// </summary>
        public static (Boolean, Board) SuicideAtBigTigerMouth(GameTryMove tryMove)
        {
            Point move = tryMove.Move;
            Board tryBoard = tryMove.TryGame.Board;
            Board currentBoard = tryMove.CurrentGame.Board;
            Content c = tryMove.MoveContent;
            List<Group> eyeGroups = currentBoard.GetGroupsFromStoneNeighbours(move, c.Opposite()).ToList();
            foreach (Group eyeGroup in eyeGroups)
            {
                if (eyeGroup.Liberties.Count != 2) continue;
                Point liberty = eyeGroup.Liberties.First(n => !n.Equals(move));
                Board b = currentBoard.MakeMoveOnNewBoard(liberty, c, true);
                if (b == null || WallHelper.TargetWithAllNonKillableGroups(b)) continue;
                if (b.GetGroupsFromStoneNeighbours(move, c.Opposite()).Count == 1 && EyeHelper.FindEye(b, move, c)) continue;
                if (ImmovableHelper.CheckConnectAndDie(b))
                    return (true, b);

                if (b.MoveGroupLiberties != 2) continue;
                Point liberty2 = b.MoveGroup.Liberties.First(n => !n.Equals(move));
                //check for groups at liberty
                List<Group> ngroups = b.GetGroupsFromStoneNeighbours(liberty2, c.Opposite()).Where(n => !n.Equals(b.MoveGroup)).ToList();
                if (!WallHelper.StrongNeighbourGroups(b, ngroups))
                    return (true, b);

                //make block move
                Board b2 = b.MakeMoveOnNewBoard(liberty2, c.Opposite(), true);
                if (b2 == null) continue;

                //check for opponent survival move
                if (b.MoveGroup.Points.Count >= 3)
                {
                    List<Point> npoints = b2.GetStoneNeighbours().Where(n => b2[n] != c.Opposite() && !n.Equals(b.Move.Value)).ToList();
                    if (npoints.Select(n => GroupHelper.GetKillerGroupFromCache(b2, n, c)).Any(n => n != null && n.Points.Count >= 3))
                        return (true, b);
                }
            }

            //check three liberty group
            if (CheckThreeLibertyGroupAtBigTigerMouth(tryMove))
                return (true, null);
            return (false, null);
        }

        /// <summary>
        /// Check three liberty group at big tiger mouth.
        /// Check capture at liberty <see cref="UnitTestProject.FillKoEyeMoveTest.FillKoEyeMoveTest_Scenario5dan18_2" />
        /// Check suicidal group <see cref="UnitTestProject.FillKoEyeMoveTest.FillKoEyeMoveTest_Scenario5dan18_3" /> 
        /// <see cref="UnitTestProject.FillKoEyeMoveTest.FillKoEyeMoveTest_Scenario5dan18_4" /> 
        /// Check covered eye suicidal group <see cref="UnitTestProject.FillKoEyeMoveTest.FillKoEyeMoveTest_Scenario5dan18_5" /> 
        /// </summary>
        public static Boolean CheckThreeLibertyGroupAtBigTigerMouth(GameTryMove tryMove)
        {
            Point move = tryMove.Move;
            Board tryBoard = tryMove.TryGame.Board;
            Board currentBoard = tryMove.CurrentGame.Board;
            Content c = tryMove.MoveContent;
            List<Group> eyeGroups = currentBoard.GetGroupsFromStoneNeighbours(move, c.Opposite()).ToList();
            foreach (Group eyeGroup in eyeGroups)
            {
                if (eyeGroup.Liberties.Count != 3) continue;
                List<Point> liberties = eyeGroup.Liberties.Where(n => !n.Equals(move)).ToList();
                foreach (Point liberty in liberties)
                {
                    List<Group> groups = tryBoard.GetGroupsFromStoneNeighbours(liberty, c.Opposite()).Where(n => !n.Equals(tryBoard.MoveGroup)).ToList();
                    foreach (Group group in groups)
                    {
                        if (WallHelper.TargetWithAnyNonKillableGroup(tryBoard, group)) continue;
                        //check capture at liberty
                        if (group.Liberties.Count == 1 && group.Points.Count > 1 && EyeHelper.IsCovered(tryBoard, group.Liberties.First(), c))
                        {
                            Board b = currentBoard.MakeMoveOnNewBoard(group.Liberties.First(), c);
                            if (b != null && b.MoveGroupLiberties <= 2)
                                return true;
                        }
                        //check suicidal group
                        if (ImmovableHelper.CheckConnectAndDie(tryBoard, group, false)) continue;
                        if (ImmovableHelper.TwoAndThreeLibertiesConnectAndDie(currentBoard, group, liberty))
                            return true;
                        //check covered eye suicidal group
                        if (EyeHelper.FindCoveredEye(currentBoard, liberty, c) && EyeHelper.FindCoveredEyeWithLiberties(currentBoard, move, c))
                            return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Pre-atari move. 
        /// <see cref="UnitTestProject.PreAtariMoveTest.PreAtariMoveTest_ScenarioHighLevel18" />
        /// <see cref="UnitTestProject.PreAtariMoveTest.PreAtariMoveTest_Scenario_TianLongTu_Q16594" />
        /// <see cref="UnitTestProject.PreAtariMoveTest.PreAtariMoveTest_Scenario_WindAndTime_Q30370" />
        /// <see cref="UnitTestProject.PreAtariMoveTest.PreAtariMoveTest_Scenario_TianLongTu_Q16747" />
        /// Check unescapable group <see cref="UnitTestProject.PreAtariMoveTest.PreAtariMoveTest_Scenario_Corner_A85" />
        /// <see cref="UnitTestProject.PreAtariMoveTest.PreAtariMoveTest_Scenario_WuQingYuan_Q31154" />
        /// Two pre-atari moves <see cref="UnitTestProject.PreAtariMoveTest.PreAtariMoveTest_Scenario_Corner_A55" />
        /// </summary>
        public static Boolean PreAtariMove(GameTryMove tryMove)
        {
            Board currentBoard = tryMove.CurrentGame.Board;
            Board tryBoard = tryMove.TryGame.Board;
            Content c = tryBoard.MoveGroup.Content;
            IEnumerable<Group> targetGroups = tryBoard.GetGroupsFromStoneNeighbours().Where(gr => gr.Liberties.Count == 2);
            foreach (Group targetGroup in targetGroups)
            {
                //check connect and die at each liberty
                IEnumerable<Board> killBoards = GameHelper.GetMoveBoards(tryBoard, targetGroup.Liberties, c);
                foreach (Board b in killBoards)
                {
                    if (!UnescapableGroup(b, targetGroup).Item1) continue;
                    if (b.MoveGroup.Points.Count == 1 && b.GetGroupsFromStoneNeighbours().Count > 1 && EscapePreAtari(tryBoard, targetGroup))
                        return true;
                }

                //check unescapable group       
                IEnumerable<Board> moveBoards = GameHelper.GetMoveBoards(currentBoard, currentBoard.GetGroupLiberties(targetGroup), c.Opposite());
                foreach (Board b in moveBoards)
                {
                    if (!b.AtariTargets.Any(t => UnescapableGroup(b, t).Item1)) continue;
                    if (IsSuicidalMoveForBothPlayers(tryBoard, b.Move.Value))
                        return true;
                }
            }
            return false;
        }
    }
}
