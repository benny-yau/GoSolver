using System;
using System.Collections.Generic;
using System.Linq;

namespace Go
{
    public class ImmovableHelper
    {
        /// <summary>
        /// Find tiger mouth.
        /// </summary>
        public static Point? FindTigerMouth(Board board, Point p, Content c)
        {
            Content content = board[p];
            List<Point> nstones = board.GetStoneNeighbours(p);
            if (nstones.Count(n => board[n] == c) != nstones.Count - 1) return null;
            if (content == Content.Empty)
            {
                if (board.GetGroupsFromStoneNeighbours(p, c.Opposite()).Any(n => n.Liberties.Count == 1)) return null;
                Point libertyPoint = nstones.First(n => board[n] != c);
                if (board[libertyPoint] == Content.Empty)
                    return libertyPoint;
                Group group = board.GetGroupAt(libertyPoint);
                if (group.Liberties.Count == 2)
                    return group.Liberties.First(n => !n.Equals(p));
            }
            else if (content == c.Opposite())
            {
                Group group = board.GetGroupAt(p);
                if (group.Liberties.Count == 1)
                    return group.Liberties.First();
            }
            return null;
        }

        /// <summary>
        /// Find empty tiger mouth.
        /// </summary>
        public static Boolean FindEmptyTigerMouth(Board board, Point p, Content c)
        {
            return (board[p] == Content.Empty && FindTigerMouth(board, p, c) != null);
        }

        /// <summary>
        /// Find tiger mouth for link.
        /// </summary>
        public static Boolean FindTigerMouthForLink(Board board, Point p, Content c)
        {
            if (!ImmovableHelper.FindEmptyTigerMouth(board, p, c)) return false;
            HashSet<Group> groups = board.GetGroupsFromStoneNeighbours(p, c.Opposite());
            if (groups.Count == 1 || groups.All(n => WallHelper.IsNonKillableGroup(board, n)))
                return false;
            return true;
        }

        /// <summary>
        /// Get diagonals of tiger mouth.
        /// </summary>
        public static List<Point> GetDiagonalsOfTigerMouth(Board board, Point p, Content c, Boolean checkContent = false)
        {
            List<Point> npoints = LinkHelper.GetDiagonalsAtStoneNeighbours(board, p, c);
            List<Point> diagonals = board.GetDiagonalNeighbours(p).Where(n => board.GetStoneNeighbours(n).Intersect(npoints).Count() >= 2).ToList();
            if (checkContent) return diagonals.Where(d => board[d] != c).ToList();
            return diagonals;
        }

        /// <summary>
        /// Is immovable point. Check for links and semi solid eye. 
        /// Empty point <see cref="UnitTestProject.SurvivalTigerMouthMoveTest.SurvivalTigerMouthMoveTest_Scenario_GuanZiPu_A3" />
        /// Check connect and die <see cref="UnitTestProject.LifeCheckTest.LifeCheckTest_Scenario_Corner_A28" />
        /// Check filled point connect and die <see cref="UnitTestProject.NeutralPointMoveTest.NeutralPointMoveTest_Scenario_TianLongTu_Q16975" />
        /// <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_Scenario_XuanXuanGo_Q18341" />
        /// <see cref="UnitTestProject.ThreeLibertySuicidalTest.ThreeLibertySuicidalTest_Scenario_TianLongTu_Q14992_2" />
        /// Check for ko possibility <see cref="UnitTestProject.LifeCheckTest.LifeCheckTest_Scenario_WuQingYuan_Q30986" />
        /// </summary>
        public static Boolean IsImmovablePoint(Board board, Point p, Content c)
        {
            if (board[p] == Content.Empty)
            {
                if (PrecheckNotSuicidal(board, p, c.Opposite()))
                    return false;
                (Boolean suicidal, Board b) = ImmovableHelper.IsSuicidalMove(p, c.Opposite(), board, true);
                if (!suicidal)
                    return false;
                if (b == null)
                    return WallHelper.StrongNeighbourGroups(board, p, c.Opposite());
                if (IsConfirmTigerMouth(board, b) != null)
                    return true;
            }
            else if (board[p] == c.Opposite())
            {
                Group targetGroup = board.GetGroupAt(p);
                if (!UnescapableGroup(board, targetGroup).Item1)
                    return false;
                if (!WallHelper.StrongNeighbourGroups(board, targetGroup))
                    return false;
                if (CheckForKoInImmovablePoint(board, targetGroup))
                    return false;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Check for ko in immovable point.
        /// Check capture neighbour groups <see cref="UnitTestProject.LifeCheckTest.LifeCheckTest_Scenario_WuQingYuan_Q30986" />
        /// <see cref="UnitTestProject.LifeCheckTest.LifeCheckTest_Scenario_WindAndTime_Q29998_2" />
        /// <see cref="UnitTestProject.LifeCheckTest.LifeCheckTest_Scenario_TianLongTu_Q16446" />
        /// Check for reverse ko fight <see cref="UnitTestProject.LifeCheckTest.LifeCheckTest_Scenario_WindAndTime_Q29998" />
        /// </summary>
        private static Boolean CheckForKoInImmovablePoint(Board board, Group targetGroup)
        {
            if (targetGroup.Points.Count != 1 || targetGroup.Liberties.Count != 1) return false;
            Content c = targetGroup.Content.Opposite();
            Point liberty = targetGroup.Liberties.First();

            if (KoHelper.IsKoFight(board, targetGroup))
                return true;
            //check capture neighbour groups
            List<Group> ngroups = board.GetGroupsFromStoneNeighbours(liberty, c).Where(n => n.Liberties.Count == 1).ToList();
            if (ngroups.Count > 1)
            {
                foreach (Group group in ngroups)
                {
                    (Boolean unEscapable, Board b) = ImmovableHelper.UnescapableGroup(board, group);
                    if (!unEscapable && KoHelper.IsKoFight(b, targetGroup))
                        return true;
                }
            }

            //check for reverse ko fight 
            List<Point> nstones = board.GetStoneNeighbours(liberty);
            if (nstones.Any(n => board[n] == c)) return false;
            List<Point> eyeNeighbour = nstones.Where(n => board[n] == Content.Empty).ToList();
            if (eyeNeighbour.Count == 1 && KoHelper.MakeKoFight(board, eyeNeighbour.First(), c.Opposite()))
                return true;
            return false;
        }

        /// <summary>
        /// Is confirm tiger mouth.
        /// Check connect and die on current board <see cref="UnitTestProject.ImmovableTest.ImmovableTest_Scenario_TianLongTu_Q16827" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_GuanZiPu_A17_2" />
        /// Check connect and die on captured board <see cref="UnitTestProject.ImmovableTest.ImmovableTest_Scenario_XuanXuanGo_B32" />
        /// <see cref="UnitTestProject.LifeCheckTest.LifeCheckTest_Scenario1dan21" />
        /// </summary>.
        public static Board IsConfirmTigerMouth(Board currentBoard, Board tryBoard)
        {
            Point move = tryBoard.Move.Value;
            Content c = tryBoard.MoveGroup.Content;

            Point? libertyPoint = FindTigerMouth(currentBoard, move, c.Opposite());
            if (libertyPoint == null) return null;

            Board capturedBoard = CaptureSuicideGroup(move, tryBoard);
            if (capturedBoard == null) return null;

            if (!WallHelper.StrongNeighbourGroups(currentBoard, move, c))
                return null;
            if (!WallHelper.StrongNeighbourGroups(capturedBoard, move, c))
                return null;
            if (SuicidalAfterMustHaveMove(currentBoard, tryBoard, libertyPoint.Value))
                return null;
            return capturedBoard;
        }

        /// <summary>
        /// Suicide move after must have move.
        /// <see cref="UnitTestProject.LifeCheckTest.LifeCheckTest_Scenario_XuanXuanGo_Q18500" />
        /// <see cref="UnitTestProject.LifeCheckTest.LifeCheckTest_Scenario1dan21" />
        /// </summary>
        public static Boolean SuicidalAfterMustHaveMove(Board currentBoard, Board tryBoard, Point libertyPoint)
        {
            Point move = tryBoard.Move.Value;
            Content c = tryBoard.MoveGroup.Content;
            Point eyePoint = currentBoard.GetDiagonalNeighbours(move).FirstOrDefault(n => EyeHelper.FindCoveredEye(currentBoard, n, c.Opposite()));
            if (!Convert.ToBoolean(eyePoint.NotEmpty)) return false;
            if (!LinkHelper.GetPreviousMoveGroup(currentBoard, tryBoard).All(n => n.Liberties.Count <= 2)) return false;

            (Boolean suicidal, Board b) = ImmovableHelper.IsSuicidalMove(libertyPoint, c, currentBoard);
            if (suicidal) return false;

            if (ImmovableHelper.IsSuicidalMove(b, move, c.Opposite()))
                return true;
            return false;
        }

        /// <summary>
        /// Three liberty connect and die. Only for tiger mouth at liberty.
        /// <see cref="UnitTestProject.ThreeLibertySuicidalTest.ThreeLibertySuicidalTest_Scenario_TianLongTu_Q14992_2" />
        /// <see cref="UnitTestProject.ThreeLibertySuicidalTest.ThreeLibertySuicidalTest_Scenario_TianLongTu_Q14992" />
        /// Check is covered <see cref="UnitTestProject.ThreeLibertySuicidalTest.ThreeLibertySuicidalTest_Scenario_Side_B19" />
        /// Check if escapable <see cref="UnitTestProject.ThreeLibertySuicidalTest.ThreeLibertySuicidalTest_Scenario_Corner_A86" />
        /// </summary>
        public static (Boolean, Board) ThreeLibertyConnectAndDie(Board board, Group targetGroup = null, Boolean koEnabled = true)
        {
            if (targetGroup == null) targetGroup = board.MoveGroup;
            else targetGroup = board.GetCurrentGroup(targetGroup);
            if (targetGroup.Liberties.Count != 3) return (false, null);
            Content c = targetGroup.Content;
            //find tiger mouth at liberty
            List<Point> liberties = targetGroup.Liberties.Where(n => ImmovableHelper.FindEmptyTigerMouth(board, n, c) && EyeHelper.IsCovered(board, n, c)).ToList();
            foreach (Point p in liberties)
            {
                Board b = board.MakeMoveOnNewBoard(p, c.Opposite(), false);
                if (b == null || b.MoveGroupLiberties != 1) continue;

                Board b2 = ImmovableHelper.CaptureSuicideGroup(b);
                if (b2.MoveGroupLiberties != 2) continue;
                if (!EyeHelper.FindCoveredEye(b2, p, c)) continue;
                if (CheckConnectAndDie(b2, targetGroup, koEnabled))
                    return (true, b2);
            }
            return (false, null);
        }

        /// <summary>
        /// Two and three liberties connect and die.
        /// </summary>
        public static Boolean TwoAndThreeLibertiesConnectAndDie(Board board, Group targetGroup = null, Boolean koEnabled = true)
        {
            if (ImmovableHelper.CheckConnectAndDie(board, targetGroup, koEnabled)) return true;
            if (ImmovableHelper.ThreeLibertyConnectAndDie(board, targetGroup, koEnabled).Item1) return true;
            return false;
        }

        /// <summary>
        /// Escape pre atari.
        /// </summary>
        public static Boolean EscapePreAtari(Board board, Group targetGroup)
        {
            foreach (Board b in GameHelper.GetMoveBoards(board, targetGroup.Liberties, targetGroup.Content))
            {
                if (!LinkHelper.IsAbsoluteLinkForGroups(board, b)) continue;
                if (!ImmovableHelper.CheckConnectAndDie(b, targetGroup, false))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Escape capture link.
        /// <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_Scenario_XuanXuanGo_A26_3" />
        /// </summary>
        public static Boolean EscapeCaptureLink(Board board, Group targetGroup)
        {
            foreach (Board b in GameHelper.GetMoveBoards(board, targetGroup.Liberties, targetGroup.Content))
            {
                if (!LinkHelper.IsAbsoluteLinkForGroups(board, b)) continue;
                Group target = b.GetCurrentGroup(targetGroup);
                if (target.Liberties.Count > 2 || WallHelper.IsNonKillableGroup(b, target))
                    return true;
            }
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
        public static (Boolean, Board) UnescapableGroup(Board board, Group targetGroup, Boolean koEnabled = true)
        {
            Group group = board.GetCurrentGroup(targetGroup);
            if (group.Liberties.Count != 1) return (false, null);

            //check escape by capture
            Board captureBoard = EscapeByCapture(board, group, koEnabled);
            if (captureBoard != null)
                return (false, captureBoard);

            //make move at liberty
            Board escapeBoard = MakeMoveAtLiberty(board, group);

            //recursive connect and die
            if (escapeBoard == null || escapeBoard.MoveGroupLiberties == 1 || CheckConnectAndDie(escapeBoard, group, !koEnabled))
                return (true, escapeBoard);

            return (false, escapeBoard);
        }

        /// <summary>
        /// Escape by capture.
        /// Check snapback <see cref="UnitTestProject.AtariResponseMoveTest.AtariResponseMoveTest_Scenario_TianLongTu_Q16605" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanQiJing_Weiqi101_2282" />
        /// Connect and die <see cref="UnitTestProject.ImmovableTest.ImmovableTest_Scenario_XuanXuanGo_B32" />
        /// Connect and die for move group <see cref="UnitTestProject.ImmovableTest.ImmovableTest_Scenario_GuanZiPu_A3" />
        /// </summary>
        public static Board EscapeByCapture(Board board, Group group, Boolean koEnabled = true)
        {
            foreach (Group target in AtariHelper.AtariByGroup(group, board, koEnabled))
            {
                //make capture move
                (_, Board b) = ImmovableHelper.IsSuicidalOnCapture(board, target, koEnabled);
                if (b == null) continue;
                //connect and die
                if (CheckConnectAndDie(b, group, !koEnabled))
                    continue;
                return b;
            }
            return null;
        }

        /// <summary>
        /// Precheck not suicidal.
        /// </summary>
        public static Boolean PrecheckNotSuicidal(Board board, Point p, Content c)
        {
            if (board.GetMoveLiberties(p).Count() >= 2)
                return true;
            if (board.GetGroupsFromStoneNeighbours(p, c.Opposite()).Any(n => n.Liberties.Count >= 3))
                return true;
            return false;
        }

        /// <summary>
        /// Is suicidal move.
        /// </summary>
        public static Boolean IsSuicidalMove(Board board, Point p, Content c, Boolean overrideKo = false)
        {
            if (PrecheckNotSuicidal(board, p, c))
                return false;
            return IsSuicidalMove(p, c, board, overrideKo).Item1;
        }

        /// <summary>
        /// Is suicidal move.
        /// <see cref="UnitTestProject.ImmovableTest.ImmovableTest_Scenario_Corner_A80" />
        /// <see cref="UnitTestProject.ImmovableTest.ImmovableTest_Scenario_Corner_A80_2" />
        /// <see cref="UnitTestProject.ImmovableTest.ImmovableTest_Scenario_WuQingYuan_Q31503" />
        /// </summary>
        public static (Boolean, Board) IsSuicidalMove(Point p, Content c, Board board, Boolean overrideKo = false)
        {
            if (EyeHelper.FindEye(board, p, c.Opposite()))
            {
                List<Group> eyeGroups = board.GetGroupsFromStoneNeighbours(p, c).ToList();
                if (eyeGroups.All(n => n.Liberties.Count > 1)) return (true, null);
            }
            Board b = board.MakeMoveOnNewBoard(p, c, overrideKo);
            if (b == null) return (true, null);
            if (b.MoveGroupLiberties != 1) return (false, b);
            if (KoHelper.IsKoFight(b))
            {
                if (overrideKo) return (false, b);
                return (true, null);
            }
            return (true, b);
        }

        /// <summary>
        /// Is suicidal without ko.
        /// </summary>
        public static Boolean IsSuicidalWithoutKo(Board board, Group group = null)
        {
            if (group == null) group = board.MoveGroup;
            else group = board.GetCurrentGroup(group);
            return group.Liberties.Count == 1 && !KoHelper.IsKoFight(board, group);
        }

        /// <summary>
        /// Is suicide move on capture.
        /// <see cref="UnitTestProject.ImmovableTest.ImmovableTest_Scenario_Corner_B28_2" />
        /// </summary>
        public static (Boolean, Board) IsSuicidalOnCapture(Board board, Group targetGroup = null, Boolean koEnabled = false)
        {
            Board b = ImmovableHelper.CaptureSuicideGroup(board, targetGroup, koEnabled);
            if (b == null)
            {
                if (KoHelper.IsKoFight(board, targetGroup)) return (true, null);
                return (false, null);
            }
            if (b.MoveGroupLiberties != 1) return (false, b);
            if (KoHelper.IsKoFight(b))
            {
                if (koEnabled) return (false, b);
                return (true, null);
            }
            return (true, b);
        }

        /// <summary>
        /// Capture suicide group.
        /// </summary>
        public static Board CaptureSuicideGroup(Point p, Board board, Boolean overrideKo = true)
        {
            if (board[p] == Content.Empty) return null;
            return CaptureSuicideGroup(board, board.GetGroupAt(p), overrideKo);
        }

        public static Board CaptureSuicideGroup(Board board, Group group = null, Boolean overrideKo = true)
        {
            if (group == null) group = board.MoveGroup;
            else group = board.GetCurrentGroup(group);
            Content c = group.Content.Opposite();
            if (group.Liberties.Count != 1) return null;
            return board.MakeMoveOnNewBoard(group.Liberties.First(), c, overrideKo);
        }

        /// <summary>
        /// Make move at liberty.
        /// </summary>
        public static Board MakeMoveAtLiberty(Board board, Group group)
        {
            List<Point> liberties = board.GetGroupLiberties(group);
            if (liberties.Count != 1) return null;
            return board.MakeMoveOnNewBoard(liberties.First(), group.Content);
        }

        /// <summary>
        /// Check capture secure.
        /// </summary>
        public static Boolean CheckCaptureSecure(Board board, Group group, Boolean immovable = false)
        {
            Content c = group.Content;
            if (board.GetGroupLiberties(group).Count > 1) return false;
            Board escapeBoard = ImmovableHelper.MakeMoveAtLiberty(board, group);
            if (immovable)
            {
                if (escapeBoard != null) return false;
            }
            else if (escapeBoard != null && escapeBoard.MoveGroupLiberties > 1)
                return false;
            return true;
        }

        /// <summary>
        /// Is suicidal move for both players.
        /// </summary>
        public static Boolean IsSuicidalMoveForBothPlayers(Board board, Point p, Boolean connectAndDie = false)
        {
            if (!connectAndDie)
            {
                if (ImmovableHelper.IsSuicidalMove(board, p, Content.Black) && ImmovableHelper.IsSuicidalMove(board, p, Content.White))
                    return true;
            }
            else
            {
                (Boolean suicidal, Board b) = ImmovableHelper.IsSuicidalMove(p, Content.Black, board);
                (Boolean suicidal2, Board b2) = ImmovableHelper.IsSuicidalMove(p, Content.White, board);
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
                //check three opponent stones
                List<Point> nstones = board.GetStoneNeighbours(eyePoint).Where(n => board[n] == c.Opposite()).ToList();
                if (nstones.Count != 3) return false;
                Point middleStone = nstones.FirstOrDefault(n => board.GetDiagonalNeighbours(n).Count(d => nstones.Contains(d) && board.GetGroupAt(n) != board.GetGroupAt(d)) >= 2);
                if (!Convert.ToBoolean(middleStone.NotEmpty)) return false;
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
            foreach (Board b in GameHelper.GetMoveBoards(board, target.Liberties, c.Opposite()))
            {
                if (b.MoveGroup.Points.Count == 1)
                {
                    if (IsSnapback(b, eyeGroup, b.MoveGroup))
                        return true;
                }
                else
                {
                    List<Group> groups = AtariHelper.AtariByGroup(target, b).Where(n => !n.Equals(b.GetCurrentGroup(eyeGroup))).ToList();
                    if (groups.Any(n => IsSnapback(b, eyeGroup, n)))
                        return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Is snapback.
        /// </summary>
        public static Boolean IsSnapback(Board board, Group eyeGroup, Group suicideGroup)
        {
            //capture suicide group
            if (suicideGroup.Liberties.Count != 1) return false;
            Board b = ImmovableHelper.CaptureSuicideGroup(board, suicideGroup);
            if (b.MoveGroup.Points.Count == 1 || b.MoveGroupLiberties != 1) return false;
            //capture eye group
            Board b2 = ImmovableHelper.CaptureSuicideGroup(board, eyeGroup);
            if (b2 == null) return false;
            if (b2.MoveGroupLiberties == 1) return true;
            //escape suicide group
            if (!ImmovableHelper.UnescapableGroup(b2, suicideGroup).Item1)
                return true;
            return false;
        }

        /// <summary>
        /// Connect and die.
        /// <see cref="UnitTestProject.ImmovableTest.ImmovableTest_Scenario_Corner_A80" />
        /// <see cref="UnitTestProject.ImmovableTest.ImmovableTest_Scenario_XuanXuanGo_B32" />
        /// Suicidal capture <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanQiJing_B25" />
        /// <see cref="UnitTestProject.SpecificNeutralMoveTest.SpecificNeutralMoveTest_Scenario_Corner_A55" />
        /// </summary>
        public static (Boolean, Board) ConnectAndDie(Board board, Group group = null, Boolean koEnabled = true)
        {
            if (group == null) group = board.MoveGroup;
            else group = board.GetCurrentGroup(group);
            Content c = group.Content;
            if (group.Liberties.Count > 2) return (false, null);

            //make kill move
            List<dynamic> killBoards = new List<dynamic>();
            foreach (Point liberty in group.Liberties)
            {
                if (!GameHelper.SetupMoveAvailable(board, liberty, c.Opposite())) continue;
                (_, Board b) = ImmovableHelper.IsSuicidalMove(liberty, c.Opposite(), board, koEnabled);
                if (b == null) continue;
                Boolean resolveAtari = Board.ResolveAtari(board, b);
                Boolean captured = b.CapturedList.Any();
                int moveLiberties = b.GetMoveLiberties().Count;
                int moveGroupLiberties = b.MoveGroupLiberties;
                killBoards.Add(new { b = b, resolveAtari = resolveAtari, captured = captured, moveLiberties = moveLiberties, moveGroupLiberties = moveGroupLiberties });
            }

            killBoards = killBoards.OrderByDescending(k => k.resolveAtari).ThenByDescending(k => k.captured).ThenByDescending(k => k.moveLiberties).ThenByDescending(k => k.moveGroupLiberties).ToList();
            foreach (dynamic k in killBoards)
            {
                Board b = k.b;
                //check if captured
                if (b.IsCapturedGroup(group))
                    return (true, b);

                //check if escapable
                if (UnescapableGroup(b, group, !koEnabled).Item1)
                    return (true, b);
            }
            return (false, null);
        }

        /// <summary>
        /// Check connect and die.
        /// </summary>
        public static Boolean CheckConnectAndDie(Board board, Group targetGroup = null, Boolean koEnabled = true)
        {
            return ConnectAndDie(board, targetGroup, koEnabled).Item1;
        }

        /// <summary>
        /// Suicide at big tiger mouth.
        /// <see cref="UnitTestProject.FillKoEyeMoveTest.FillKoEyeMoveTest_Scenario_GuanZiPu_B3" /> 
        /// <see cref="UnitTestProject.FillKoEyeMoveTest.FillKoEyeMoveTest_Scenario_Corner_A85" /> 
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario6kyu13" />
        /// Check groups at liberty <see cref="UnitTestProject.MustHaveNeutralMoveTest.MustHaveNeutralMoveTest_Scenario_Side_B19" />
        /// <see cref="UnitTestProject.MustHaveNeutralMoveTest.MustHaveNeutralMoveTest_Scenario_XuanXuanGo_A23" />
        /// <see cref="UnitTestProject.MustHaveNeutralMoveTest.MustHaveNeutralMoveTest_Scenario_XuanXuanQiJing_Weiqi101_7245" />
        /// <see cref="UnitTestProject.MustHaveNeutralMoveTest.MustHaveNeutralMoveTest_Scenario_TianLongTu_Q16827_2" />
        /// Check opponent survival move <see cref="UnitTestProject.FillKoEyeMoveTest.FillKoEyeMoveTest_Scenario_WindAndTime_Q29475" /> 
        /// <see cref="UnitTestProject.MustHaveNeutralMoveTest.MustHaveNeutralMoveTest_Scenario_XuanXuanQiJing_Weiqi101_7245_2" />
        /// <see cref="UnitTestProject.BaseLineKillerMoveTest.BaseLineKillerMoveTest_Scenario_XuanXuanQiJing_A53" /> 
        /// Check covered eye survival <see cref="UnitTestProject.LifeCheckTest.LifeCheckTest_20230422_8" /> 
        /// </summary>
        public static (Boolean, Board) SuicideAtBigTigerMouth(GameTryMove tryMove)
        {
            Point move = tryMove.Move;
            Board tryBoard = tryMove.TryGame.Board;
            Board currentBoard = tryMove.CurrentGame.Board;
            Content c = tryMove.MoveContent;
            foreach (Group eyeGroup in LinkHelper.GetPreviousMoveGroup(currentBoard, tryBoard))
            {
                if (eyeGroup.Liberties.Count != 2) continue;
                Point liberty = eyeGroup.Liberties.First(n => !n.Equals(move));

                //make move at liberty
                Board b = currentBoard.MakeMoveOnNewBoard(liberty, c, true);
                if (b == null || WallHelper.TargetWithAllNonKillableGroups(b)) continue;
                //check covered eye survival 
                if (b.GetGroupsFromStoneNeighbours(move, c.Opposite()).Count == 1 && EyeHelper.FindEye(b, move, c)) continue;
                
                //check if suicide
                if (!WallHelper.IsStrongGroup(b))
                    return (true, b);

                //check groups at liberty
                if (b.MoveGroupLiberties != 2) continue;
                Point liberty2 = b.MoveGroup.Liberties.First(n => !n.Equals(move));
                List<Group> ngroups = b.GetGroupsFromStoneNeighbours(liberty2, c.Opposite()).Where(n => !n.Equals(b.MoveGroup)).ToList();
                if (!WallHelper.StrongGroups(b, ngroups))
                    return (true, b);

                //make block move
                Board b2 = b.MakeMoveOnNewBoard(liberty2, c.Opposite(), true);
                if (b2 == null) continue;

                //check opponent survival move
                if (b.MoveGroup.Points.Count >= 3)
                {
                    List<Point> npoints = b2.GetStoneNeighbours().Where(n => b2[n] != c.Opposite() && !n.Equals(b.Move.Value)).ToList();
                    if (npoints.Any(n => !WallHelper.NoEyeForSurvival(b, n, c.Opposite())))
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
        /// </summary>
        public static Boolean CheckThreeLibertyGroupAtBigTigerMouth(GameTryMove tryMove)
        {
            Point move = tryMove.Move;
            Board tryBoard = tryMove.TryGame.Board;
            Board currentBoard = tryMove.CurrentGame.Board;
            Content c = tryMove.MoveContent;
            foreach (Group eyeGroup in LinkHelper.GetPreviousMoveGroup(currentBoard, tryBoard))
            {
                if (eyeGroup.Liberties.Count != 3) continue;
                foreach (Point liberty in eyeGroup.Liberties)
                {
                    if (liberty.Equals(move)) continue;
                    if (!EyeHelper.IsCovered(tryBoard, liberty, c)) continue;
                    foreach (Group group in tryBoard.GetGroupsFromStoneNeighbours(liberty, c.Opposite()))
                    {
                        if (group.Equals(tryBoard.MoveGroup)) continue;
                        if (WallHelper.TargetWithAnyNonKillableGroup(tryBoard, group)) continue;
                        //check capture at liberty
                        if (group.Liberties.Count == 1 && group.Points.Count >= 3)
                        {
                            Board b = currentBoard.MakeMoveOnNewBoard(group.Liberties.First(), c);
                            if (ImmovableHelper.TwoAndThreeLibertiesConnectAndDie(b, group))
                                return true;
                        }
                        //check suicidal group
                        if (ImmovableHelper.TwoAndThreeLibertiesConnectAndDie(tryBoard, group)) continue;
                        if (ImmovableHelper.TwoAndThreeLibertiesConnectAndDie(currentBoard, group))
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
            foreach (Group targetGroup in tryBoard.GetGroupsFromStoneNeighbours())
            {
                if (targetGroup.Liberties.Count != 2) continue;
                //check connect and die at each liberty
                foreach (Board b in GameHelper.GetMoveBoards(tryBoard, targetGroup.Liberties, c))
                {
                    if (!UnescapableGroup(b, targetGroup).Item1) continue;
                    if (b.MoveGroup.Points.Count == 1 && b.GetGroupsFromStoneNeighbours().Count > 1 && EscapePreAtari(tryBoard, targetGroup))
                        return true;
                }
                //check unescapable group       
                foreach (Board b in GameHelper.GetMoveBoards(currentBoard, currentBoard.GetGroupLiberties(targetGroup), c.Opposite()))
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
