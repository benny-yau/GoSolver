using System;
using System.Collections.Generic;
using System.Linq;

namespace Go
{
    public class ImmovableHelper
    {
        /// <summary>
        /// Find tiger mouth where mouth point is empty or filled. Content in parameter represents content of stones forming the tiger mouth. Snapback or ko not handled (see IsConfirmTigerMouth). 
        /// </summary>
        public static Point? FindTigerMouth(Board board, Point p, Content c)
        {
            if (!board.PointWithinBoard(p))
                return null;
            Content content = board[p];
            if (content == Content.Empty)
            {
                List<Point> stoneNeighbours = board.GetStoneNeighbours(p);
                List<Point> libertyPoint = stoneNeighbours.Where(n => board[n] != c).ToList();
                if (libertyPoint.Count != 1) return null;
                Point q = libertyPoint.First();
                //make move into tiger mouth
                Board b = board.MakeMoveOnNewBoard(p, c.Opposite());
                if (b == null) return q;
                //capture move at tiger mouth
                Board b2 = CaptureSuicideGroup(b);
                if (b2 != null) return b2.Move;
            }
            else if (content == c.Opposite())
            {
                //ensure group in tiger mouth cannot escape
                (Boolean result, Point? libertyPoint, _) = UnescapableGroup(board, board.GetGroupAt(p));
                if (result)
                    return libertyPoint.Value;
            }
            return null;
        }

        public static Boolean FindTigerMouth(Board board, Content c, Point p)
        {
            return (FindTigerMouth(board, p, c) != null);
        }

        /// <summary>
        /// Immovable point to check for links and diagonal points in semi solid eye. For empty point, return if point is immovable which can be a suicide point or tiger's mouth. If not empty point, then check if opponent can escape. Return if immovable and liberty point at tiger mouth.
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
                (Boolean suicidal, Board b) = ImmovableHelper.IsSuicidalMove(p, c.Opposite(), board);
                if (!suicidal)
                    return (false, null);

                if (b == null)
                {
                    //check connect and die
                    if (EyeHelper.FindEye(board, p, c) && AllConnectAndDie(board, p))
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
                Group targetGroup = board.GetGroupAt(p);
                (Boolean unEscapable, Point? libertyPoint, _) = UnescapableGroup(board, targetGroup);
                if (!unEscapable)
                    return (false, null);

                //check filled point connect and die
                if (!WallHelper.StrongNeighbourGroups(board, targetGroup))
                    return (false, null);

                //check for ko possibility
                Point q = libertyPoint.Value;
                if (CheckForKoInImmovablePoint(board, targetGroup, q))
                    return (false, null);

                return (true, q);
            }
            return (false, null);
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
            if (targetGroup.Points.Count != 1) return false;
            Content c = targetGroup.Content.Opposite();
            //check for ko by capture neighbour groups
            if (EyeHelper.IsCovered(board, q, c.Opposite()))
            {
                List<Group> neighbourGroups = board.GetGroupsFromStoneNeighbours(q, c).Where(n => n.Liberties.Count == 1).ToList();
                if (neighbourGroups.Count > 1)
                {
                    foreach (Group group in neighbourGroups)
                    {
                        (Boolean unEscapable, _, Board b) = ImmovableHelper.UnescapableGroup(board, group);
                        if (!unEscapable && KoHelper.IsKoFight(b, targetGroup))
                            return true;
                    }
                }
                else if (KoHelper.IsKoFight(board, targetGroup))
                    return true;
            }

            //check for reverse ko fight 
            List<Point> stoneNeighbours = board.GetStoneNeighbours(q);
            if (stoneNeighbours.Any(n => board[n] == c)) return false;
            List<Point> eyeNeighbour = stoneNeighbours.Where(n => board[n] == Content.Empty).ToList();
            if (eyeNeighbour.Count == 1 && KoHelper.IsReverseKoFight(board, eyeNeighbour.First(), c.Opposite()))
                return true;
            return false;
        }

        /// <summary>
        /// Capture suicide group and check for captured count greater than one or move liberty greater than one to ensure no ko or snapback.
        /// <see cref="UnitTestProject.ImmovableTest.ImmovableTest_Scenario_TianLongTu_Q16827" />
        /// Check connect and die on current board <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_GuanZiPu_A17_2" />
        /// Check all connect and die on captured board <see cref="UnitTestProject.ImmovableTest.ImmovableTest_Scenario_XuanXuanGo_B32" />
        /// <see cref="UnitTestProject.LifeCheckTest.LifeCheckTest_Scenario1dan21" />
        /// </summary>.
        public static Board IsConfirmTigerMouth(Board currentBoard, Board tryBoard, Point? p = null)
        {
            if (p == null) p = tryBoard.Move;
            Content c = tryBoard.MoveGroup.Content;

            Point? libertyPoint = FindTigerMouth(currentBoard, p.Value, c.Opposite());
            if (libertyPoint == null) return null;

            //check connect and die on current board
            if (!WallHelper.StrongNeighbourGroups(currentBoard, p.Value, c))
                return null;

            Board capturedBoard = CaptureSuicideGroup(p.Value, tryBoard);
            if (capturedBoard == null) return null;
            //ensure not ko
            if (KoHelper.IsKoFight(capturedBoard))
                return null;

            //check all connect and die on captured board
            if (AllConnectAndDie(capturedBoard, p.Value))
                return null;

            //check suicidal move at side of board
            if (SuicidalAfterMustHaveMove(currentBoard, tryBoard, libertyPoint.Value))
                return null;

            if (ThreeLibertyConnectAndDie(capturedBoard, p.Value))
                return null;

            return capturedBoard;
        }


        /// <summary>
        /// Three liberty connect and die, not all inclusive. Connect and die group may not be target group for two liberties target group.
        /// <see cref="UnitTestProject.ThreeLibertySuicidalTest.ThreeLibertySuicidalTest_Scenario_TianLongTu_Q14992_2" />
        /// <see cref="UnitTestProject.ThreeLibertySuicidalTest.ThreeLibertySuicidalTest_Scenario_TianLongTu_Q14992" />
        /// Stone neighbours at diagonal of each other <see cref="UnitTestProject.ThreeLibertySuicidalTest.ThreeLibertySuicidalTest_Scenario_Side_B19" />
        /// Check if escapable <see cref="UnitTestProject.ThreeLibertySuicidalTest.ThreeLibertySuicidalTest_Scenario_Corner_A86" />
        /// </summary>
        public static Boolean ThreeLibertyConnectAndDie(Board board, Point p)
        {
            Group targetGroup = board.MoveGroup;
            Content c = targetGroup.Content;

            if (!ImmovableHelper.FindTigerMouth(board, c, p)) return false;

            //stone neighbours at diagonal of each other
            List<Point> stoneNeighbours = LinkHelper.GetNeighboursDiagonallyLinked(board, p, c);
            if (!stoneNeighbours.Any()) return false;

            Board b = board.MakeMoveOnNewBoard(p, c.Opposite());
            if (b == null || b.MoveGroupLiberties != 1) return false;
            if (b.AtariTargets.Count != 1) return false;

            Board b2 = ImmovableHelper.CaptureSuicideGroup(b);
            if (b2 == null || b2.MoveGroupLiberties != 2) return false;
            if (!EyeHelper.FindCoveredEye(b2, p, c)) return false;
            //check if escapable
            Point e = b2.MoveGroup.Liberties.First(lib => !lib.Equals(p));
            if (!ImmovableHelper.IsSuicidalMove(b2, e, c))
                return false;
            if (CheckConnectAndDie(b2, targetGroup))
                return true;
            return false;
        }

        /// <summary>
        /// Escape link.
        /// </summary>
        public static Boolean EscapeLink(Board board, Group targetGroup)
        {
            foreach (Point liberty in targetGroup.Liberties)
            {
                Board b = board.MakeMoveOnNewBoard(liberty, targetGroup.Content);
                if (b == null || !LinkHelper.IsAbsoluteLinkForGroups(board, b))
                    continue;
                if (!ImmovableHelper.CheckConnectAndDie(b, targetGroup))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Escape capture link. Check if escapable for target group to obtain more than two liberties or become non killable.
        /// <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_Scenario_XuanXuanGo_A26_3" />
        /// Check for atari target <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_Scenario_TianLongTu_Q17154" />
        /// </summary>
        public static Boolean EscapeCaptureLink(Board board, Group targetGroup, Point? capturePoint = null)
        {
            //check if absolute link at liberty
            foreach (Point liberty in targetGroup.Liberties)
            {
                Board b = board.MakeMoveOnNewBoard(liberty, targetGroup.Content);
                if (b == null || !LinkHelper.IsAbsoluteLinkForGroups(board, b))
                    continue;
                Group target = b.GetCurrentGroup(targetGroup);
                if (target.Liberties.Count > 2)
                    return true;
                if (WallHelper.IsNonKillableGroup(b, target))
                    return true;
            }
            //check for atari target group other than capture point
            List<Group> ngroups = AtariHelper.AtariByGroup(targetGroup, board);
            foreach (Group ngroup in ngroups)
            {
                if (capturePoint != null && ngroup.Points.Contains(capturePoint.Value)) continue;
                Board b = ImmovableHelper.CaptureSuicideGroup(board, ngroup);
                if (b == null) continue;
                Group target = b.GetCurrentGroup(targetGroup);
                if (target.Liberties.Count > 2 && !KoHelper.IsKoFight(b))
                    return true;
                if (WallHelper.IsNonKillableGroup(b, target))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Suicide move for survival after must-have neutral move at side of the board.
        /// <see cref="UnitTestProject.LifeCheckTest.LifeCheckTest_Scenario_XuanXuanGo_Q18500" />
        /// <see cref="UnitTestProject.LifeCheckTest.LifeCheckTest_Scenario1dan21" />
        /// </summary>
        private static Boolean SuicidalAfterMustHaveMove(Board currentBoard, Board tryBoard, Point libertyPoint)
        {
            Point p = tryBoard.Move.Value;
            Content c = tryBoard.MoveGroup.Content;
            if (currentBoard[p] != Content.Empty) return false;
            Point eyePoint = currentBoard.GetDiagonalNeighbours(p).FirstOrDefault(n => EyeHelper.FindEye(currentBoard, n));
            if (!Convert.ToBoolean(eyePoint.NotEmpty)) return false;
            if (!currentBoard.GetGroupsFromStoneNeighbours(p, c.Opposite()).All(n => n.Liberties.Count <= 2)) return false;

            (Boolean suicidal, Board b) = ImmovableHelper.IsSuicidalMove(libertyPoint, c, currentBoard);
            if (suicidal) return false;

            (Boolean suicidal2, Board b2) = ImmovableHelper.IsSuicidalMove(p, c.Opposite(), b);
            if (suicidal2 && EyeHelper.FindRealSolidEye(eyePoint, c.Opposite(), b2))
                return false;

            return suicidal2;
        }

        /// <summary>
        /// Ensure group cannot escape by moving at liberty point.       
        /// <see cref="UnitTestProject.SpecificNeutralMoveTest.SpecificNeutralMoveTest_Scenario_Corner_A85" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_GuanZiPu_Q14981" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_GuanZiPu_A12" />
        /// Check ko fight <see cref="UnitTestProject.ImmovableTest.ImmovableTest_Scenario_XuanXuanGo_A28_101Weiqi" />
        /// Recursive connect and die <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanGo_A44_101Weiqi" />
        /// <see cref="UnitTestProject.ImmovableTest.ImmovableTest_Scenario_TianLongTu_Q17255" />
        /// </summary>
        public static (Boolean, Point?, Board) UnescapableGroup(Board tryBoard, Group targetGroup, Boolean koEnabled = true)
        {
            Content c = targetGroup.Content;
            Group group = tryBoard.GetCurrentGroup(targetGroup);
            Point? libertyPoint = ImmovableHelper.GetLibertyPointOfSuicide(tryBoard, group);
            if (libertyPoint == null) return (false, null, null);

            //check if atari any neighbour groups
            Board captureBoard = EscapeByCapture(tryBoard, group, koEnabled);
            if (captureBoard != null)
                return (false, null, captureBoard);

            //move at liberty is suicidal
            (Boolean isSuicidal, Board escapeBoard) = IsSuicidalMove(libertyPoint.Value, c, tryBoard);
            if (isSuicidal)
            {
                //check ko fight
                if (koEnabled && KoHelper.IsKoFight(tryBoard, group) && tryBoard.GetGroupsFromStoneNeighbours(group.Liberties.First(), c.Opposite()).Where(gr => !gr.Equals(group)).Any(n => !ImmovableHelper.CheckConnectAndDie(tryBoard, n, !koEnabled)))
                    return (false, null, escapeBoard);
                return (true, libertyPoint, escapeBoard);
            }

            //recursive connect and die
            if (CheckConnectAndDie(escapeBoard, group, !koEnabled))
                return (true, libertyPoint, escapeBoard);

            return (false, null, escapeBoard);
        }

        /// <summary>
        /// Check if can escape by capturing neighbour group.
        /// Check snapback <see cref="UnitTestProject.AtariResponseMoveTest.AtariResponseMoveTest_Scenario_TianLongTu_Q16605" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanQiJing_Weiqi101_2282" />
        /// Connect and die <see cref="UnitTestProject.ImmovableTest.ImmovableTest_Scenario_XuanXuanGo_B32" />
        /// Connect and die for move group <see cref="UnitTestProject.ImmovableTest.ImmovableTest_Scenario_GuanZiPu_A3" />
        /// Check for recursion <see cref="UnitTestProject.CheckForRecursionTest.CheckForRecursionTest_Scenario_XuanXuanGo_Q18331" />
        /// </summary>
        public static Board EscapeByCapture(Board tryBoard, Group group, Boolean koEnabled = true)
        {
            List<Group> atariTargets = AtariHelper.AtariByGroup(group, tryBoard, koEnabled);
            foreach (Group target in atariTargets)
            {
                //make capture move
                (_, Board b) = ImmovableHelper.IsSuicidalOnCapture(tryBoard, target, koEnabled);
                if (b == null) continue;
                //check for recursion
                if (GameHelper.CheckForRecursion(b).Any())
                    continue;
                //connect and die
                if (CheckConnectAndDie(b, group, !koEnabled))
                    continue;
                return b;
            }
            return null;
        }

        /// <summary>
        /// Is suicidal move.
        /// </summary>
        public static Boolean IsSuicidalMove(Board tryBoard, Point p, Content c)
        {
            return IsSuicidalMove(p, c, tryBoard).Item1;
        }

        public static (Boolean, Board) IsSuicidalMove(Point p, Content c, Board tryBoard)
        {
            if (tryBoard == null) return (false, null);
            Board board = tryBoard.MakeMoveOnNewBoard(p, c);
            if (board == null) return (true, null);
            if (board.MoveGroupLiberties == 1)
                return (true, board);
            return (false, board);
        }

        /// <summary>
        /// Suicidal move for connect and die.
        /// <see cref="UnitTestProject.ImmovableTest.ImmovableTest_Scenario_Corner_A80" />
        /// <see cref="UnitTestProject.ImmovableTest.ImmovableTest_Scenario_Corner_A80_2" />
        /// <see cref="UnitTestProject.ImmovableTest.ImmovableTest_Scenario_WuQingYuan_Q31503" />
        /// </summary>
        public static (Boolean, Board) IsSuicidalMoveForConnectAndDie(Point p, Content c, Board tryBoard, Boolean koEnabled = false)
        {
            if (tryBoard == null) return (false, null);
            Board board = tryBoard.MakeMoveOnNewBoard(p, c, koEnabled);
            if (board == null) return (true, null);
            if (board.MoveGroupLiberties == 1)
            {
                if (!koEnabled && KoHelper.IsKoFight(board))
                    return (true, null);
                return (true, board);
            }
            return (false, board);
        }

        /// <summary>
        /// Is suicide move on capture.
        /// <see cref="UnitTestProject.ImmovableTest.ImmovableTest_Scenario_Corner_B28_2" />
        /// </summary>
        public static (Boolean, Board) IsSuicidalOnCapture(Board tryBoard, Group targetGroup = null, Boolean koEnabled = false)
        {
            Board board = ImmovableHelper.CaptureSuicideGroup(tryBoard, targetGroup, koEnabled);
            if (board == null) return (true, null);

            if (board.MoveGroupLiberties == 1)
            {
                if (!koEnabled && KoHelper.IsKoFight(board))
                    return (false, null);
                return (true, board);
            }
            return (false, board);
        }

        /// <summary>
        /// Capture group that has liberty of one only and return the board.
        /// </summary>
        public static Board CaptureSuicideGroup(Point p, Board board, Boolean overrideKo = false)
        {
            if (board[p] == Content.Empty) return null;
            return CaptureSuicideGroup(board, board.GetGroupAt(p), overrideKo);
        }

        public static Board CaptureSuicideGroup(Board board, Group group = null, Boolean overrideKo = false)
        {
            if (group == null) group = board.MoveGroup;
            Content c = group.Content.Opposite();
            Point? p = GetLibertyPointOfSuicide(board, group);
            if (p == null) return null;
            return board.MakeMoveOnNewBoard(p.Value, c, overrideKo);
        }

        /// <summary>
        /// Get liberty point of suicide group.
        /// </summary>
        public static Point? GetLibertyPointOfSuicide(Board tryBoard, Group group = null)
        {
            if (group == null) group = tryBoard.MoveGroup;
            List<Point> liberties = tryBoard.GetGroupLiberties(group);
            if (liberties.Count == 1)
                return liberties.First();
            return null;
        }

        /// <summary>
        /// Make move at liberty point of suicide group.
        /// </summary>
        public static Board MakeMoveAtLibertyPointOfSuicide(Board tryBoard, Group group, Content c)
        {
            Point? libertyPoint = ImmovableHelper.GetLibertyPointOfSuicide(tryBoard, group);
            if (libertyPoint == null) return null;
            Board board = tryBoard.MakeMoveOnNewBoard(libertyPoint.Value, c);
            return board;
        }

        /// <summary>
        /// Check capture secure.
        /// </summary>
        public static Boolean CheckCaptureSecure(Board board, Group group)
        {
            Content c = group.Content;
            Board escapeBoard = ImmovableHelper.MakeMoveAtLibertyPointOfSuicide(board, group, c);
            if (escapeBoard != null && escapeBoard.MoveGroupLiberties > 1)
                return false;

            if (AtariHelper.AtariByGroup(group, board).Any())
                return false;
            return true;
        }

        /// <summary>
        /// Is suicide move for both players.
        /// </summary>
        public static Boolean IsSuicidalMoveForBothPlayers(Board tryBoard, Point p, Boolean connectAndDie = false)
        {
            (Boolean suicidal, Board b) = ImmovableHelper.IsSuicidalMove(p, Content.Black, tryBoard);
            (Boolean suicidal2, Board b2) = ImmovableHelper.IsSuicidalMove(p, Content.White, tryBoard);
            if (!connectAndDie)
            {
                if (suicidal && suicidal2)
                    return true;
            }
            else
            {
                if ((suicidal || ImmovableHelper.CheckConnectAndDie(b)) && (suicidal2 || ImmovableHelper.CheckConnectAndDie(b2)))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Check snapback in neighbour groups.
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WindAndTime_Q30234" />
        /// <see cref="UnitTestProject.SpecificNeutralMoveTest.SpecificNeutralMoveTest_Scenario_Corner_A55" />
        /// </summary>
        public static Boolean CheckSnapbackInNeighbourGroups(Board board, Group moveGroup)
        {
            IEnumerable<Group> neighbourGroups = board.GetNeighbourGroups(moveGroup);
            return neighbourGroups.Any(group => CheckSnapback(board, group));
        }

        /// <summary>
        /// Check snapback.
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_GuanZiPu_A26" />
        /// <see cref="UnitTestProject.LifeCheckTest.LifeCheckTest_Scenario_WuQingYuan_Q31493" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanGo_B31_4" />
        /// Two point move <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WindAndTime_Q30234" />
        /// </summary>
        public static Boolean CheckSnapback(Board board, Group targetGroup)
        {
            Content c = targetGroup.Content;
            if (targetGroup.Points.Count == 1) return false;
            List<Point> libertyPoints = board.GetGroupLiberties(targetGroup);
            if (libertyPoints.Count != 2) return false;

            List<Group> groups = AtariHelper.AtariByGroup(targetGroup, board);
            if (groups.Count == 0) return false;
            Group moveGroup = groups.First();
            if (moveGroup.Points.Count > 2 || moveGroup.Liberties.Count > 1) return false;

            foreach (Point libertyPoint in libertyPoints)
            {
                //make move at suicide point
                Board b = board.MakeMoveOnNewBoard(libertyPoint, c.Opposite());
                if (b == null || !b.IsAtariMove) continue;
                Board b2 = ImmovableHelper.MakeMoveAtLibertyPointOfSuicide(b, targetGroup, c);
                if (b2 == null || b2.MoveGroupLiberties != 1) continue;

                List<Group> suicideGroups = b.GetNeighbourGroups(targetGroup).Where(gr => gr.Points.Count <= 2 && gr.Liberties.Count == 1).ToList();
                if (suicideGroups.Count != 2) continue;

                //one point move within two point group or two point move
                List<Group> suicideWithinTwoPointGroup = suicideGroups.Where(gr => gr.Points.Count == 2 || GroupHelper.GetKillerGroupFromCache(b, gr.Points.First(), c) != null).ToList();
                if (suicideWithinTwoPointGroup.Count != 1) continue;

                Group suicideGroupAtTigerMouth = suicideGroups.Where(gr => gr != suicideWithinTwoPointGroup.First() && gr.Points.Count == 1).FirstOrDefault();
                if (suicideGroupAtTigerMouth == null) continue;
                Point r = suicideGroupAtTigerMouth.Points.First();
                if (!b.GetDiagonalNeighbours(r).Any(n => suicideWithinTwoPointGroup.First().Points.Contains(n))) continue;
                //capture move
                if (IsSnapback(b, targetGroup, suicideGroupAtTigerMouth))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Snapback.
        /// Check if target group is escapable <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanGo_B31" />
        /// <see cref="UnitTestProject.SurvivalTigerMouthMoveTest.SurvivalTigerMouthMoveTest_Scenario_GuanZiPu_A3" />
        /// <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_WuQingYuan_Q31640" />
        /// Check kill eye snapback <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanGo_B32" />
        /// Check not more than two stones captured <see cref="UnitTestProject.ImmovableTest.ImmovableTest_Scenario_WuQingYuan_Q31471" />
        /// Check if kill move can escape <see cref="UnitTestProject.ImmovableTest.ImmovableTest_Scenario_Corner_B28" />
        /// </summary>
        public static Boolean IsSnapback(Board tryBoard, Group targetGroup, Group suicideGroup)
        {
            if (!tryBoard.IsAtariMove) return false;
            (Boolean suicidal, Board b) = ImmovableHelper.IsSuicidalOnCapture(tryBoard, suicideGroup);
            if (suicidal && b != null && b.MoveGroup.Points.Count > 1)
            {
                //check if target group is escapable
                (Boolean unEscapable, _, Board escapeBoard) = UnescapableGroup(tryBoard, targetGroup, false);
                if (unEscapable) return true;
                //check not more than two stones captured
                int capturedPoints = escapeBoard.CapturedPoints.Count();
                Boolean capture = escapeBoard.CapturedList.Count == 1 && capturedPoints >= 1 && capturedPoints <= 2 && escapeBoard.CapturedList.First().Points.Any(p => EyeHelper.IsCovered(escapeBoard, p, targetGroup.Content));
                if (!capture) return false;
                //check if kill move can escape
                if (escapeBoard.IsCapturedGroup(suicideGroup) || UnescapableGroup(escapeBoard, suicideGroup).Item1)
                    return false;
                return true;
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
            List<Point> groupLiberties = board.GetGroupLiberties(group);
            if (groupLiberties.Count > 2) return (false, null);

            List<KeyValuePair<LinkedPoint<Point>, Board>> killBoards = new List<KeyValuePair<LinkedPoint<Point>, Board>>();
            foreach (Point liberty in groupLiberties)
            {
                if (!GameHelper.SetupMoveAvailable(board, liberty)) continue;
                (_, Board b) = ImmovableHelper.IsSuicidalMoveForConnectAndDie(liberty, c.Opposite(), board, koEnabled);
                if (b == null) continue;
                int neighbourCount = b.GetStoneNeighbours().Count(n => b[n] != c.Opposite());
                killBoards.Add(new KeyValuePair<LinkedPoint<Point>, Board>(new LinkedPoint<Point>(liberty, neighbourCount), b));
            }

            foreach (KeyValuePair<LinkedPoint<Point>, Board> kvp in killBoards.OrderByDescending(b => (int)b.Key.CheckMove))
            {
                Board b = kvp.Value;
                LinkedPoint<Point> key = kvp.Key;
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
            return ConnectAndDie(board, targetGroup, koEnabled).Item1;
        }


        /// <summary>
        /// Check connect and die for captured board.
        /// </summary>
        public static Boolean AllConnectAndDie(Board capturedBoard, Point p, Content c = Content.Unknown)
        {
            if (c == Content.Unknown)
            {
                List<Point> stoneNeighbours = capturedBoard.GetStoneNeighbours(p).Where(q => capturedBoard[q] != Content.Empty).ToList();
                c = capturedBoard[stoneNeighbours.First()];
                if (stoneNeighbours.Any(n => capturedBoard[n] != c))
                    throw new Exception("Different content in connect and die.");
            }
            List<Group> neighbourGroups = capturedBoard.GetGroupsFromStoneNeighbours(p, c.Opposite()).ToList();
            return neighbourGroups.Any(group => CheckConnectAndDie(capturedBoard, group));
        }

        /// <summary>
        /// Pre-atari move that targets group with liberty of two. Next atari move will capture the group.
        /// <see cref="UnitTestProject.SpecificNeutralMoveTest.SpecificNeutralMoveTest_Scenario_TianLongTu_Q16594" />
        /// <see cref="UnitTestProject.SpecificNeutralMoveTest.SpecificNeutralMoveTest_Scenario_WuQingYuan_Q31154" />
        /// <see cref="UnitTestProject.SpecificNeutralMoveTest.SpecificNeutralMoveTest_Scenario_XuanXuanGo_A55" />
        /// Check target group <see cref="UnitTestProject.SpecificNeutralMoveTest.SpecificNeutralMoveTest_Scenario_WindAndTime_Q30370" />
        /// <see cref="UnitTestProject.SpecificNeutralMoveTest.CheckForRecursionTest_Scenario_XuanXuanGo_A28_101Weiqi_2" />
        /// Check if any liberty is suicidal <see cref="UnitTestProject.GenericNeutralMoveTest.GenericNeutralMoveTest_Scenario_XuanXuanQiJing_Weiqi101_18410" />
        /// Rare scenario <see cref="UnitTestProject.GenericNeutralMoveTest.GenericNeutralMoveTest_Scenario_WindAndTime_Q30275" />
        /// Check unescapable group <see cref="UnitTestProject.SpecificNeutralMoveTest.SpecificNeutralMoveTest_Scenario_Corner_A85" />
        /// <see cref="UnitTestProject.SpecificNeutralMoveTest.SpecificNeutralMoveTest_Scenario_WuQingYuan_Q31154" />
        /// </summary>
        public static Boolean PreAtariMove(GameTryMove tryMove)
        {
            Board currentBoard = tryMove.CurrentGame.Board;
            Board tryBoard = tryMove.TryGame.Board;
            Point move = tryBoard.Move.Value;
            Content c = tryBoard.MoveGroup.Content;
            IEnumerable<Group> neighbourGroups = tryBoard.GetGroupsFromStoneNeighbours(move);
            foreach (Group targetGroup in neighbourGroups)
            {
                //check conditions for pre-atari
                HashSet<Point> targetLiberties = targetGroup.Liberties;
                if (targetLiberties.Count != 2) continue;
                //check target group
                if (LifeCheck.GetTargets(tryBoard).All(t => targetGroup.Equals(t)) && !AtariHelper.AtariByGroup(tryBoard, targetGroup)) continue;

                //check connect and die
                (_, Board board) = ConnectAndDie(tryBoard, targetGroup);
                if (board != null && board.MoveGroup.Points.Count == 1 && board.GetGroupsFromStoneNeighbours(board.Move.Value, c).Count > 1 && EscapeLink(tryBoard, targetGroup))
                    return true;

                //check unescapable group                
                foreach (Point liberty in currentBoard.GetGroupLiberties(targetGroup))
                {
                    Board b = currentBoard.MakeMoveOnNewBoard(liberty, c.Opposite());
                    if (b == null || b.AtariTargets.Count == 0) continue;
                    if (b.AtariTargets.Any(t => ImmovableHelper.UnescapableGroup(b, t, false).Item1))
                    {
                        if (ImmovableHelper.IsSuicidalMoveForBothPlayers(tryBoard, liberty))
                            return true;
                    }
                }
            }
            return false;
        }
    }
}
