using System;
using System.Collections.Generic;
using System.Linq;

namespace Go
{
    public class KillerFormationHelper
    {
        static Dictionary<int, List<Func<Board, Group, Boolean>>> killerFormationFuncs;
        public static Dictionary<int, List<Func<Board, Group, Boolean>>> KillerFormationFuncs
        {
            get
            {
                if (killerFormationFuncs == null)
                {
                    killerFormationFuncs = new Dictionary<int, List<Func<Board, Group, Boolean>>>();
                    killerFormationFuncs.Add(4, new List<Func<Board, Group, Boolean>>() { OneByThreeFormation, BoxFormation, CrowbarEdgeFormation, StraightFourFormation, TwoByTwoSuicidalFormation, BentFourCornerFormation });
                    killerFormationFuncs.Add(5, new List<Func<Board, Group, Boolean>>() { KnifeFiveFormation, CrowbarFiveFormation, BentFiveFormation });
                    killerFormationFuncs.Add(6, new List<Func<Board, Group, Boolean>>() { FlowerSixFormation, KnifeSixFormation, CornerSixFormation });
                    killerFormationFuncs.Add(7, new List<Func<Board, Group, Boolean>>() { FlowerSevenFormation, OddSevenFormation });
                }
                return killerFormationFuncs;
            }
        }

        /// <summary>
        /// Is killer formation from func.
        /// One-by-three formation <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_Corner_A8" />
        /// Crowbar edge formation <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanGo_Q6710" />
        /// <see cref="UnitTestProject.KillerFormationTest.KillerFormationTest_Scenario_TianLongTu_Q16738" />
        /// Two-by-two formation <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanQiJing_A40" />
        /// <see cref="UnitTestProject.KillerFormationTest.KillerFormationTest_Scenario_TianLongTu_Q16738_2" />
        /// Straight four formation <see cref="UnitTestProject.KillerFormationTest.KillerFormationTest_Scenario_WuQingYuan_Q31471_5" />
        /// Bent four corner formation <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_Nie20" />
        /// Knife five formation <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_Corner_A113" />
        /// Crowbar five formation --Three-by-two formation (two liberties) <see cref="UnitTestProject.KillerFormationTest.KillerFormationTest_Scenario_Corner_A132" />
        /// <see cref="UnitTestProject.KillerFormationTest.KillerFormationTest_Scenario_WuQingYuan_Q31471_4" />
        /// - Corner formation <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_Corner_A67_4" />
        /// Bent five formation <see cref="UnitTestProject.KillerFormationTest.KillerFormationTest_Scenario_WuQingYuan_Q31682_2" />
        /// - T side formation (two liberties) <see cref="UnitTestProject.KillerFormationTest.KillerFormationTest_Scenario_WuQingYuan_Q31471" />
        /// - One-by-four side formation <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_Side_B32" />
        /// Corner six formation <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanQiJing_A38" />
        /// Flower six formation <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_TianLongTu_Q16859" />
        /// Knife six formation <see cref="UnitTestProject.KillerFormationTest.KillerFormationTest_Scenario_WuQingYuan_Q31682" />
        /// <see cref="UnitTestProject.KillerFormationTest.KillerFormationTest_Scenario_WuQingYuan_Q31682_3" />
        /// - Two-by-four side formation <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WuQingYuan_Q31682" />
        /// Flower seven formation <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanGo_B3" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_TianLongTu_Q2413_2" /> 
        /// Odd seven formation <see cref="UnitTestProject.KillerFormationTest.KillerFormationTest_Scenario_WuQingYuan_Q31471_6" />
        /// <see cref="UnitTestProject.KillerFormationTest.KillerFormationTest_Scenario_WuQingYuan_Q31471_7" />
        /// </summary>
        public static Boolean IsKillerFormationFromFunc(Board tryBoard, Group group = null)
        {
            if (group == null) group = tryBoard.MoveGroup;
            else group = tryBoard.GetCurrentGroup(group);
            int contentCount = group.Points.Count;
            if (!KillerFormationFuncs.ContainsKey(contentCount)) return false;
            List<Func<Board, Group, Boolean>> funcs = KillerFormationFuncs[contentCount];
            Func<Board, Group, Boolean> killerFunc = funcs.FirstOrDefault(func => func(tryBoard, group));
            if (killerFunc != null)
                return true;
            return false;
        }

        /// <summary>
        /// Dead formation in both alive.
        /// </summary>
        public static Boolean DeadFormationInBothAlive(Board board, Group killerGroup, int libertyCount = 2, int requiredCount = 1)
        {
            Content c = killerGroup.Content;
            List<Point> emptyPoints = killerGroup.Points.Where(t => board[t] == Content.Empty).ToList();
            if (emptyPoints.Count != libertyCount)
                return false;
            if (emptyPoints.Any(n => !board.GetStoneNeighbours(n).Any(s => board[s] == c)))
                return false;

            if (TryKillFormation(board, c, emptyPoints, requiredCount))
                return true;
            return false;
        }

        /// <summary>
        /// Try kill formation. Make move at each liberty to test if formation created.
        /// </summary>
        public static Boolean TryKillFormation(Board board, Content c, List<Point> emptyPoints, int requiredCount = 1)
        {
            int count = 0;
            foreach (Board b in GameHelper.GetMoveBoards(board, emptyPoints, c))
            {
                if (!IsKillerFormationFromFunc(b)) continue;
                count++;
                if (count >= requiredCount)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Suicidal killer formations.
        /// Check suicide at eye point <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_Side_B19" />
        /// Check if real eye found in neighbour groups <see cref="UnitTestProject.KillerFormationTest.KillerFormationTest_Scenario5dan27" />
        /// Check covered eye at non-killable group <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_AncientJapanese_B6" />
        /// </summary>
        public static Boolean SuicidalKillerFormations(Board tryBoard, Board currentBoard, Board captureBoard = null)
        {
            Point move = tryBoard.Move.Value;
            Content c = tryBoard.MoveGroup.Content;

            //check liberties of move group
            if (tryBoard.MoveGroupLiberties > 2) return false;

            //create captured board
            if (captureBoard == null)
            {
                if (tryBoard.MoveGroupLiberties == 1)
                    captureBoard = ImmovableHelper.CaptureSuicideGroup(tryBoard);
                else if (tryBoard.MoveGroupLiberties == 2)
                    (_, captureBoard) = ImmovableHelper.ConnectAndDie(tryBoard);
            }
            if (captureBoard == null) return false;

            //check multipoint snapback after capture
            if (MultipointSnapbackAfterCapture(tryBoard, captureBoard))
                return true;

            //check suicide at eye point
            if (tryBoard.MoveGroupLiberties == 2 && tryBoard.GetStoneNeighbours().All(n => tryBoard[n] == c) && LinkHelper.GetPreviousMoveGroup(currentBoard, tryBoard).All(n => n.Liberties.Count > 1))
                return false;

            //check if neighbour group is non-killable
            if (WallHelper.TargetWithAnyNonKillableGroup(tryBoard))
                return false;

            //find killer formation
            if (!FindSuicidalKillerFormation(tryBoard, currentBoard, captureBoard))
                return false;

            //check if real eye found in neighbour groups
            if (CheckRealEyeInNeighbourGroups(tryBoard, captureBoard))
                return false;

            //check link to external group
            if (IsLinkToExternalGroup(tryBoard, currentBoard, captureBoard))
                return false;
            return true;
        }

        /// <summary>
        /// Multipoint snapback after capture
        /// One liberty <see cref="UnitTestProject.ImmovableTest.ImmovableTest_Scenario_TianLongTu_Q15054" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_GuanZiPu_B3_4" />
        /// Two liberties <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario1dan4_2" />
        /// <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_WuQingYuan_Q31435" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_GuanZiPu_Q18710" />
        /// </summary>
        public static Boolean MultipointSnapbackAfterCapture(Board tryBoard, Board captureBoard)
        {
            Content c = tryBoard.MoveGroup.Content;
            if (WholeGroupDying(tryBoard)) return false;
            if (tryBoard.MoveGroupLiberties == 1)
            {
                if (captureBoard.MoveGroup.Points.Count > 1 && ImmovableHelper.CheckConnectAndDie(captureBoard))
                    return true;
            }
            else if (tryBoard.MoveGroupLiberties == 2)
            {
                Group weakGroup = tryBoard.GetNeighbourGroups().FirstOrDefault(n => n.Points.Count >= 2 && n.Liberties.Count == 2 && ImmovableHelper.CheckConnectAndDie(tryBoard, n));
                if (weakGroup != null && ImmovableHelper.CheckConnectAndDie(captureBoard, weakGroup))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Check real eye in neighbour groups.
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_TianLongTu_Q16738_3" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanGo_Q18472" />
        /// Check for corner six <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanQiJing_A38" />
        /// Find real eye with strong groups <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanQiJing_B25" />
        /// </summary>
        public static Boolean CheckRealEyeInNeighbourGroups(Board tryBoard, Board captureBoard)
        {
            Point move = tryBoard.Move.Value;
            Content c = tryBoard.MoveGroup.Content;

            //real eye at move killer group
            Group killerGroup = GroupHelper.GetKillerGroupFromCache(captureBoard, move, c.Opposite());
            if (killerGroup != null && killerGroup.Points.Count <= 2 && !EyeHelper.FindRealEyeWithinEmptySpace(captureBoard, killerGroup))
                return false;

            //check for corner six
            if (CornerSixFormation(tryBoard))
                return false;

            //bent three
            if (BentThreeSuicideAtCoveredEye(tryBoard, captureBoard))
                return false;

            //find real eye in neighbour killer groups
            List<Group> killerGroups = GroupHelper.GetKillerGroups(captureBoard, c.Opposite());
            if (killerGroup == null) killerGroup = tryBoard.MoveGroup;
            List<Group> ngroups = captureBoard.GetNeighbourGroups(killerGroup);

            foreach (Group kgroup in killerGroups.Where(gr => gr != killerGroup))
            {
                List<Group> cgroups = captureBoard.GetNeighbourGroups(kgroup);
                if (!cgroups.Intersect(ngroups).Any()) continue;
                if (cgroups.Count == 1) return true;
                if (!WallHelper.StrongGroups(captureBoard, cgroups)) continue;
                if (EyeHelper.FindRealEyeOfAnyKillerGroup(captureBoard, kgroup))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Find suicidal killer formation.
        /// Two-point move with liberty <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanGo_A48" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_GuanZiPu_A2" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_TianLongTu_Q17250" />
        /// Covered eye <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_TianLongTu_Q16424_2" />
        /// Check for snapback <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WindAndTime_Q30234" />
        /// Corner three formation <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_GuanZiPu_Q18860" />
        /// </summary>
        private static Boolean FindSuicidalKillerFormation(Board tryBoard, Board currentBoard, Board captureBoard)
        {
            if (tryBoard.MoveGroupLiberties > 2)
                return false;

            int moveCount = tryBoard.MoveGroup.Points.Count;
            if (moveCount == 2)
            {
                //two-point atari move
                if (TwoPointAtariMove(tryBoard, captureBoard))
                    return true;

                //covered eye
                if (TwoPointSuicideAtCoveredEye(tryBoard))
                    return true;

                //two-point suicide with liberty
                if (TwoPointSuicideWithLiberty(tryBoard, captureBoard))
                    return true;

                //check for snapback
                if (ImmovableHelper.CheckSnapbackFromMove(tryBoard))
                    return true;

                //suicide for liberty fight
                if (SuicideForLibertyFight(tryBoard, currentBoard))
                    return true;

                //suicidal end move
                if (SuicidalEndMove(tryBoard, currentBoard))
                    return true;
            }
            else if (moveCount == 3)
            {
                if (SuicideMoveValidWithOneEmptySpaceLeft(tryBoard))
                    return true;

                //move group binding
                if (ThreePointMoveBinding(tryBoard, currentBoard))
                    return true;

                //corner three formation
                if (CornerThreeFormation(tryBoard))
                    return true;

                //bent three
                if (BentThreeSuicideAtCoveredEye(tryBoard, captureBoard))
                    return true;

                //suicide for liberty fight
                if (SuicideForLibertyFight(tryBoard, currentBoard))
                    return true;

                //suicidal end move
                if (SuicidalEndMove(tryBoard, currentBoard))
                    return true;
            }
            else
            {
                //check killer formation from functions
                if (IsKillerFormationFromFunc(tryBoard))
                {
                    //check kill group extension
                    if (CheckRedundantKillGroupExtension(tryBoard, currentBoard, captureBoard))
                        return false;
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Check redundant kill group extension.
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_Corner_A8" />
        /// <see cref="UnitTestProject.KillerFormationTest.KillerFormationTest_Scenario_Corner_A113" />
        /// Check move liberty <see cref="UnitTestProject.KillerFormationTest.KillerFormationTest_Scenario_Corner_B41_2" />
        /// Check end point extension <see cref="UnitTestProject.KillerFormationTest.KillerFormationTest_Scenario_WuQingYuan_Q31471_8" />
        /// <see cref="UnitTestProject.KillerFormationTest.KillerFormationTest_Scenario_WuQingYuan_Q31471_9" />
        /// Whole group dying <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_GuanZiPu_A36" />
        /// Bent four corner formation <see cref="UnitTestProject.BentFourTest.BentFourTest_Scenario7kyu26_3" />
        /// Corner six formation <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanQiJing_A38" />
        /// Two kill formations <see cref="UnitTestProject.KillerFormationTest.KillerFormationTest_Scenario_XuanXuanGo_A54" />
        /// Check atari target <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanQiJing_A40" />
        /// </summary>
        private static Boolean CheckRedundantKillGroupExtension(Board tryBoard, Board currentBoard, Board captureBoard)
        {
            Content c = tryBoard.MoveGroup.Content;
            //move group binding
            List<Group> previousGroups = LinkHelper.GetPreviousMoveGroup(currentBoard, tryBoard);
            if (previousGroups.Count > 1)
                return false;

            //check move liberty
            List<Point> liberties = tryBoard.GetMoveLiberties();
            if (liberties.Any())
            {
                if (SuicideMoveValidWithOneEmptySpaceLeft(tryBoard))
                    return false;
                if (tryBoard.MoveGroupLiberties == 2 && liberties.Any(n => EyeHelper.FindEye(tryBoard, n, c)))
                    return false;
            }

            //check end point extension
            if (tryBoard.MoveGroup.Points.Count >= 5 && LinkHelper.GetMoveDiagonals(tryBoard).Any())
                return false;

            //bent four corner formation
            if (BentFourCornerFormation(tryBoard) && UniquePatternsHelper.CheckForBentFour(currentBoard))
                return true;

            //whole group dying
            if (WholeGroupDying(tryBoard))
            {
                Point liberty = tryBoard.MoveGroup.Liberties.First();
                if (TryKillFormation(currentBoard, c, new List<Point>() { liberty }) && SuicidalEndMove(tryBoard, currentBoard))
                    return true;
                return false;
            }

            //corner six formation
            if (KillerFormationHelper.CornerSixFormation(tryBoard))
                return false;

            //check previous group for killer formation
            Group previousGroup = previousGroups.First();
            if (tryBoard.MoveGroupLiberties == 1 && IsKillerFormationFromFunc(currentBoard, previousGroup))
                return true;

            //grid dimension changed
            if (!GridDimensionChanged(previousGroup.Points, tryBoard.MoveGroup.Points))
                return false;

            //check atari target
            if (tryBoard.AtariTargets.Any() && captureBoard.MoveGroupLiberties == 2)
            {
                IEnumerable<Board> moveBoards = GameHelper.GetMoveBoards(captureBoard, captureBoard.MoveGroup.Liberties, c);
                if (moveBoards.Any(n => n.MoveGroupLiberties > 1 && n.GetGroupsFromStoneNeighbours().Count > 1))
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Suicidal end move.
        /// </summary>
        public static Boolean SuicidalEndMove(Board tryBoard, Board currentBoard)
        {
            Point move = tryBoard.Move.Value;
            Content c = tryBoard.MoveGroup.Content;
            //check whole group dying
            if (!WholeGroupDying(tryBoard)) return false;

            //get first point
            List<Group> previousGroups = LinkHelper.GetPreviousMoveGroup(currentBoard, tryBoard);
            if (previousGroups.Count != 1) return false;
            Group group = previousGroups.First();
            if (group.Liberties.Count != 2) return false;
            Point q = group.Liberties.First(p => !p.Equals(move));
            return IsFirstPoint(currentBoard, move, q);
        }

        /// <summary>
        /// Is first point.
        /// </summary>
        public static Boolean IsFirstPoint(Board board, Point p, Point q)
        {
            return (p.x + p.y * board.SizeX) < (q.x + q.y * board.SizeX);
        }

        /// <summary>
        /// Whole group dying.
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_GuanZiPu_B3" />
        /// </summary>
        public static Boolean WholeGroupDying(Board tryBoard)
        {
            Content c = tryBoard.MoveGroup.Content;
            if (tryBoard.MoveGroupLiberties != 1 || !tryBoard.IsAtariMove) return false;
            List<Group> ngroups = tryBoard.GetNeighbourGroups();
            if (ngroups.Count != 1) return false;
            Point liberty = tryBoard.MoveGroup.Liberties.First();
            if (tryBoard.GetGroupsFromStoneNeighbours(liberty, c).Except(ngroups).Any()) return false;
            if (tryBoard.GetGroupsFromStoneNeighbours(liberty, c.Opposite()).Any(n => !n.Equals(tryBoard.MoveGroup))) return false;
            return true;
        }

        /// <summary>
        /// Suicide move valid with one empty space, surrounded by opponent stones.
        /// Move group with three points <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario1kyu29" />
        /// Move group binding <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanQiJing_Weiqi101_B19_2" />
        /// </summary>
        public static Boolean SuicideMoveValidWithOneEmptySpaceLeft(Board tryBoard)
        {
            Point move = tryBoard.Move.Value;
            Content c = tryBoard.MoveGroup.Content;
            if (tryBoard.GetMoveLiberties().Any(n => tryBoard.GetStoneNeighbours(n).Where(q => !q.Equals(move)).All(q => tryBoard[q] == c.Opposite())))
                return true;
            return false;
        }

        /// <summary>
        /// Is link to external group.
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_TianLongTu_Q16520_2" />
        /// Check connect and die <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WindAndTime_Q30403" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_GuanZiPu_A4Q11_101Weiqi_2" />
        /// Connect three or more groups <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanGo_B3" />
        /// Corner three formation <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_GuanZiPu_Q18860" />
        /// Two point atari move <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanGo_A82_101Weiqi" />
        /// No lost groups <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanQiJing_Weiqi101_18402_2" />
        /// Single lost group <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WuQingYuan_Q31682" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_TianLongTu_Q17154" />
        /// </summary>
        private static Boolean IsLinkToExternalGroup(Board tryBoard, Board currentBoard, Board captureBoard)
        {
            Content c = tryBoard.MoveGroup.Content;
            if (tryBoard.MoveGroupLiberties != 1) return false;
            Point liberty = tryBoard.MoveGroup.Liberties.First();
            (Boolean suicidal, Board linkBoard) = ImmovableHelper.IsSuicidalMove(liberty, c, currentBoard);
            if (suicidal) return false;
            //ensure link for groups
            if (!LinkHelper.IsAbsoluteLinkForGroups(currentBoard, linkBoard)) return false;
            //connect three or more groups
            List<Group> groups = LinkHelper.GetPreviousMoveGroup(currentBoard, tryBoard);
            if (groups.Count >= 3) return false;

            List<Group> linkGroups = LinkHelper.GetPreviousMoveGroup(currentBoard, linkBoard);
            //connected to external group not from previous move group
            if (!linkGroups.Except(groups).Any()) return false;
            //check connect and die
            if (ImmovableHelper.CheckConnectAndDie(linkBoard)) return false;
            //corner three formation
            if (CornerThreeFormation(tryBoard)) return false;
            //two point atari move
            if (TwoPointAtariMove(tryBoard, captureBoard)) return false;
            //saved groups
            List<Group> savedGroups = linkGroups.Intersect(groups).ToList();
            if (savedGroups.Count == 0)
                return false;
            //no lost groups
            List<Group> lostGroups = groups.Except(savedGroups).ToList();
            if (lostGroups.Count == 0)
                return true;
            //single lost group
            if (lostGroups.Count != 1) return false;
            if (lostGroups.First().Points.Count <= 2)
                return true;
            return false;
        }

        /// <summary>
        /// Check ko fight after suicidal.
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanGo_A23" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanQiJing_A36" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_GuanZiPu_A2Q71_101Weiqi" />
        /// <see cref="UnitTestProject.KillerFormationTest.KillerFormationTest_Scenario_WuQingYuan_Q31498" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WuQingYuan_Q30935_2" />
        /// </summary>
        public static Boolean CheckKoFightAfterSuicidal(Board tryBoard, Board capturedBoard = null)
        {
            if (tryBoard.MoveGroup.Points.Count == 1 || tryBoard.MoveGroupLiberties > 1) return false;
            if (tryBoard.CapturedList.Count == 0) return false;
            if (capturedBoard == null) capturedBoard = ImmovableHelper.CaptureSuicideGroup(tryBoard);
            if (!WallHelper.IsHostileGroup(capturedBoard))
                return true;
            return false;
        }

        /// <summary>
        /// Suicide for liberty fight.
        /// Both alive <see cref="UnitTestProject.BothAliveTest.BothAliveTest_Scenario_WuQingYuan_Q15126_2" />
        /// <see cref="UnitTestProject.BothAliveTest.BothAliveTest_Scenario_WuQingYuan_Q15126_3" />
        /// Not both alive <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanQiJing_A40_3" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WindAndTime_Q30215_2" />
        /// Two target groups <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WindAndTime_Q30215_3" />
        /// <see cref="UnitTestProject.BothAliveTest.BothAliveTest_Scenario_GuanZiPu_B18_4" />
        /// Check killer ko within killer group <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanGo_A28_101Weiqi_2" />
        /// Ko move on external liberty <see cref="UnitTestProject.DailyGoProblems.DailyGoProblems_20221024_5" />
        /// </summary>
        public static Boolean SuicideForLibertyFight(Board tryBoard, Board currentBoard)
        {
            Point move = tryBoard.Move.Value;
            Content c = tryBoard.MoveGroup.Content;
            if (tryBoard.MoveGroupLiberties != 1) return false;
            //suicide within killer group
            Group killerGroup = GroupHelper.GetDirectKillerGroup(currentBoard, move, c.Opposite());
            if (killerGroup == null || killerGroup.Points.Count != tryBoard.MoveGroup.Points.Count + 1) return false;

            List<Group> targetGroups = currentBoard.GetNeighbourGroups(killerGroup);
            //get only one move within killer group
            if (targetGroups.Count == 1)
            {
                Boolean firstPoint = killerGroup.Points.FirstOrDefault(p => currentBoard[p] == Content.Empty).Equals(move);
                if (!firstPoint) return false;
            }

            foreach (Group targetGroup in targetGroups)
            {
                //get external liberty
                List<Point> externalLiberties = targetGroup.Liberties.Where(n => GroupHelper.GetDirectKillerGroup(currentBoard, n, c.Opposite()) != killerGroup).ToList();
                if (externalLiberties.Count != 1) continue;
                Point liberty = externalLiberties.First();
                HashSet<Group> groups = currentBoard.GetGroupsFromStoneNeighbours(liberty, c.Opposite());
                if (!ImmovableHelper.IsSuicidalMove(tryBoard, liberty, c.Opposite()))
                    continue;
                if (groups.Any(n => ImmovableHelper.EscapeCaptureLink(currentBoard, n)))
                    continue;

                if (KoHelper.IsKoFight(tryBoard, liberty, c.Opposite()).Item1)
                {
                    //check killer ko within killer group
                    if (targetGroups.Any(n => WallHelper.TargetWithAnyNonKillableGroup(currentBoard, n)))
                        continue;
                }
                else
                {
                    if (!groups.Any(n => n.Liberties.Any(s => !s.Equals(liberty) && GroupHelper.GetDirectKillerGroup(currentBoard, s, c) != null)))
                        continue;

                    if (!groups.Any(n => targetGroup.Liberties.Count >= n.Liberties.Count - 1 && !WallHelper.IsNonKillableGroup(currentBoard, n)))
                        continue;
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// Two-point suicide at covered eye. 
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.CoveredEyeMoveTest_Scenario_WuQingYuan_Q31469" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanQiJing_B57" />
        /// <see cref="UnitTestProject.DailyGoProblems.DailyGoProblems_20250420_7" />
        /// </summary>
        public static Boolean TwoPointSuicideAtCoveredEye(Board tryBoard)
        {
            Content c = tryBoard.MoveGroup.Content;
            if (tryBoard.MoveGroup.Points.Count != 2) return false;
            if (tryBoard.MoveGroup.Points.Any(p => EyeHelper.IsCovered(tryBoard, p, c.Opposite())))
                return true;
            return false;
        }

        /// <summary>
        /// Two-point suicide with liberty.
        /// Check corner point <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_WuQingYuan_Q16508" />
        /// Check double atari <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanGo_Q18472" />
        /// </summary>
        public static Boolean TwoPointSuicideWithLiberty(Board tryBoard, Board captureBoard)
        {
            Content c = tryBoard.MoveGroup.Content;
            if (!tryBoard.GetMoveLiberties().Any()) return false;
            //check one empty space left
            if (SuicideMoveValidWithOneEmptySpaceLeft(tryBoard))
                return true;
            //check diagonal groups
            if (LinkHelper.GetDiagonalGroups(tryBoard).Any())
                return true;

            if (GroupHelper.IsSingleGroupWithinKillerGroup(tryBoard)) return false;
            //check corner point
            if (tryBoard.CornerPoint())
                return true;
            //check double atari
            if (captureBoard.MoveGroupLiberties == 2)
            {
                foreach (Board b in GameHelper.GetMoveBoards(captureBoard, captureBoard.MoveGroup.Liberties, c))
                    if (AtariHelper.DoubleAtariWithoutEscape(b)) 
                        return true;
            }
            return false;
        }       
        
        /// <summary>
         /// Two point atari move.
         /// Check for three groups <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WuQingYuan_Q30935" />
         /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WindAndTime_Q2757_2" />
         /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanGo_A82_101Weiqi" />
         /// <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_TianLongTu_Q15017" />
         /// Check snapback <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_Scenario_WuQingYuan_Q31469" />
         /// </summary>
        public static Boolean TwoPointAtariMove(Board tryBoard, Board captureBoard = null)
        {
            Point move = tryBoard.Move.Value;
            Content c = tryBoard.MoveGroup.Content;
            if (tryBoard.MoveGroup.Points.Count != 2 || tryBoard.MoveGroupLiberties != 1 || !tryBoard.IsAtariMove) return false;
            if (captureBoard == null) captureBoard = ImmovableHelper.CaptureSuicideGroup(tryBoard);
            //check for three groups
            if (ThreeOpponentGroupsAtMove(tryBoard)) return true;

            Board b = captureBoard.MakeMoveOnNewBoard(move, c);
            if (b == null || b.AtariTargets.Count == 0) return false;
            //check snapback
            if (b.GetDiagonalNeighbours().Any(n => b[n] == c) && ImmovableHelper.IsSuicidalOnCapture(b).Item1)
                return true;
            //check one point atari move
            return OnePointAtariMove(b, captureBoard);
        }

        /// <summary>
        /// One point atari move.
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WuQingYuan_Q31672" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WuQingYuan_Q31428" />
        /// </summary>
        public static Boolean OnePointAtariMove(Board b, Board board)
        {
            Content c = b.MoveGroup.Content;
            if (b.AtariTargets.Count != 1) return false;
            Group atariTarget = b.AtariTargets.First();
            if (atariTarget.Points.Count != 1) return false;
            Point q = atariTarget.Liberties.First();
            if (EyeHelper.FindNonSemiSolidEye(board, q, c.Opposite()))
                return true;
            List<Point> emptyPoints = b.GetMoveLiberties(q);
            if (emptyPoints.Count != 1) return false;

            Group killerGroup = GroupHelper.GetDirectKillerGroup(b, q, c.Opposite());
            if (killerGroup != null && killerGroup.Points.Count == 2 && EyeHelper.IsCovered(b, emptyPoints.First(), c.Opposite()))
                return true;
            return false;
        }

        /// <summary>
        /// Three opponent groups at move.
        /// </summary>
        public static Boolean ThreeOpponentGroupsAtMove(Board tryBoard, Point? eyePoint = null)
        {
            if (eyePoint == null) eyePoint = tryBoard.Move.Value;
            Content c = tryBoard[eyePoint.Value];
            if (tryBoard.GetGroupsFromStoneNeighbours(eyePoint).Count >= 3)
                return true;
            return false;
        }

        /// <summary>
        /// Three opponent stones at move.
        /// </summary>
        public static Boolean ThreeOpponentStonesAtMove(Board tryBoard, Point? eyePoint = null)
        {
            if (eyePoint == null) eyePoint = tryBoard.Move.Value;
            Content c = tryBoard[eyePoint.Value];
            if (tryBoard.GetStoneNeighbours(eyePoint).Count(n => tryBoard[n] == c.Opposite()) >= 3)
            {
                List<Point> diagonals = ImmovableHelper.GetDiagonalsOfTigerMouth(tryBoard, eyePoint.Value, c.Opposite(), true);
                if (diagonals.Any())
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Three point move binding.
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanGo_A28" />
        /// Return first point <see cref="UnitTestProject.KillerFormationTest.KillerFormationTest_Scenario_WindAndTime_Q30256" />
        /// <see cref="UnitTestProject.KillerFormationTest.KillerFormationTest_Scenario_XuanXuanGo_A7" />
        /// Check covered eye <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanQiJing_Weiqi101_18402_2" />
        /// </summary>
        public static Boolean ThreePointMoveBinding(Board tryBoard, Board currentBoard)
        {
            Point move = tryBoard.Move.Value;
            Content c = tryBoard.MoveGroup.Content;
            //move binding
            if (tryBoard.GetStoneNeighbours().Count(n => tryBoard[n] == c) == 1) return false;
            //check diagonals
            List<Point> s = LinkHelper.GetDiagonalsAtStoneNeighbours(tryBoard, move, c);
            if (s.Count != 2) return true;
            Point q = LinkHelper.PointsBetweenDiagonals(s[0], s[1]).First(n => !n.Equals(move));
            if (tryBoard[q] != Content.Empty) return true;
            //return first point
            if (tryBoard.GetStoneNeighbours(q).Count(n => tryBoard[n] == c) == 2 && KillerFormationHelper.IsFirstPoint(tryBoard, move, q))
                return true;
            //check covered eye
            if (EyeHelper.FindEye(currentBoard, q, c))
            {
                Board b = currentBoard.MakeMoveOnNewBoard(q, c);
                if (b.MoveGroup.Liberties.Any(n => EyeHelper.IsCovered(b, n, c)) && ImmovableHelper.CheckConnectAndDie(b))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Bent three suicide at covered eye.
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WuQingYuan_Q31453" />
        /// </summary>
        public static Boolean BentThreeSuicideAtCoveredEye(Board tryBoard, Board captureBoard)
        {
            Point move = tryBoard.Move.Value;
            Content c = tryBoard.MoveGroup.Content;
            if (tryBoard.MoveGroupLiberties != 1) return false;
            //bent three formation
            if (captureBoard.MoveGroupLiberties == 1 && BentThreeFormation(tryBoard, tryBoard.MoveGroup.Points))
            {
                (_, Board b) = ImmovableHelper.ConnectAndDie(captureBoard);
                if (b == null) return false;
                //get other end of move group
                IEnumerable<dynamic> pointIntersect = GetPointIntersect(tryBoard, tryBoard.MoveGroup.Points);
                List<Point> endPoints = pointIntersect.Where(p => p.intersectCount == 1).Select(p => (Point)p.point).ToList();
                //check for covered eye
                if (endPoints.Any(p => !p.Equals(move) && WallHelper.NoEyeForSurvival(b, p, c.Opposite())))
                    return true;
            }
            return false;
        }


        /*
 15 . . . . . . . . . . . . . . . . . . .
 16 . . . . . . . . . . . . . . . . . . . 
 17 . . . . . X X X . . . . . . . . . . . 
 18 . . . . . . . . . . . . . . . . . . . 
         */
        public static Boolean StraightThreeFormation(Board tryBoard, IEnumerable<Point> contentPoints)
        {
            if (contentPoints.Count() != 3) return false;
            (int xLength, int yLength) = WithinGrid(contentPoints);
            return (xLength == 0 || yLength == 0);
        }

        /*
 15 . . . . . . . . . . . . . . . . . . .
 16 . . . . . . X . . . . . . . . . . . . 
 17 . . . . . . X X . . . . . . . . . . . 
 18 . . . . . . . . . . . . . . . . . . . 
         */
        public static Boolean BentThreeFormation(Board tryBoard, IEnumerable<Point> contentPoints)
        {
            if (contentPoints.Count() != 3) return false;
            (int xLength, int yLength) = WithinGrid(contentPoints);
            return (xLength == 1 && yLength == 1);
        }

        /*
    15 . . . . . . . . . . . . . . . . . . .
    16 . . . . . . X . . . . . . . . . . . . 
    17 . . . . . X X X . . . . . . . . . . . 
    18 . . . . . . . . . . . . . . . . . . . 
        */
        public static Boolean OneByThreeFormation(Board tryBoard, Group moveGroup)
        {
            HashSet<Point> contentPoints = moveGroup.Points;
            if (contentPoints.Count() != 4) return false;
            IEnumerable<dynamic> pointIntersect = GetPointIntersect(tryBoard, contentPoints);
            return pointIntersect.Any(p => p.intersectCount == 3);
        }

        /*
 15 . . . . . . . . . . . . . . . . . . .
 16 . . . . . X X . . . . . . . . . . . . 
 17 . . . . . . X X . . . . . . . . . . . 
 18 . . . . . . . . . . . . . . . . . . . 
         */
        public static Boolean TwoByTwoSuicidalFormation(Board tryBoard, Group moveGroup)
        {
            Content c = moveGroup.Content;
            if (TwoByTwoFormation(tryBoard, moveGroup.Points))
            {
                //check for atari after capture
                Board captureBoard = ImmovableHelper.CaptureSuicideGroup(tryBoard, moveGroup);
                if (captureBoard == null) return false;
                foreach (Point p in moveGroup.Points)
                {
                    (Boolean isSuicidal, Board b) = ImmovableHelper.IsSuicidalMove(p, c, captureBoard);
                    if (isSuicidal) continue;
                    if (b != null && b.AtariTargets.Any(t => t.Points.Count > 1))
                        return true;
                }
                //check end point covered
                if (CheckAnyEndPointCovered(tryBoard, moveGroup))
                    return true;
            }
            return false;
        }

        private static Boolean TwoByTwoFormation(Board tryBoard, IEnumerable<Point> contentPoints)
        {
            if (contentPoints.Count() != 4) return false;
            IEnumerable<dynamic> pointIntersect = GetPointIntersect(tryBoard, contentPoints);
            if (pointIntersect.Count(p => p.intersectCount == 2) == 2)
            {
                if (contentPoints.GroupBy(p => p.x).Count(gr => gr.Count() == 2) == 2 || contentPoints.GroupBy(p => p.y).Count(gr => gr.Count() == 2) == 2)
                    return true;
            }
            return false;
        }


        /*
 15 . . . . . . . . . . . . . . . . . . .
 16 . . . . . X X . . . . . . . . . . . . 
 17 . . . . . X X . . . . . . . . . . . . 
 18 . . . . . . . . . . . . . . . . . . . 
         */
        public static Boolean BoxFormation(Board tryBoard, Group moveGroup)
        {
            HashSet<Point> contentPoints = moveGroup.Points;
            if (contentPoints.Count() != 4) return false;
            (int xLength, int yLength) = WithinGrid(contentPoints);
            return (xLength <= 1 && yLength <= 1);
        }


        /*
 15 . . . . . . . . . . . . . . . . . . .
 16 . . . . . X X X . . . . . . . . . . . 
 17 . . . . . X . . . . . . . . . . . . . 
 18 . . . . . . . . . . . . . . . . . . . 
         */
        private static Boolean CrowbarFormation(Board tryBoard, Group moveGroup)
        {
            HashSet<Point> contentPoints = moveGroup.Points;
            if (contentPoints.Count() != 4) return false;
            IEnumerable<dynamic> pointIntersect = GetPointIntersect(tryBoard, contentPoints);
            if (pointIntersect.Count(p => p.intersectCount == 2) == 2)
            {
                if (contentPoints.GroupBy(p => p.x).Count(gr => gr.Count() == 3) == 1 || contentPoints.GroupBy(p => p.y).Count(gr => gr.Count() == 3) == 1)
                    return true;
            }
            return false;
        }

        /*
 15 . . . . . . . . . . . . . . . . . . .
 16 . . . . . . . . . . . . . . . . . . . 
 17 . . . . . X . . . . . . . . . . . . . 
 18 . . . . . X X X . . . . . . . . . . . 
         */
        public static Boolean CrowbarEdgeFormation(Board tryBoard, Group moveGroup)
        {
            if (CrowbarFormation(tryBoard, moveGroup))
            {
                if (tryBoard.GetNeighbourGroups(moveGroup).Count <= 1) return false;
                //check end point covered
                if (CheckAnyEndPointCovered(tryBoard, moveGroup))
                    return true;
                //edge formation
                if (moveGroup.Points.Count(p => !tryBoard.PointWithinMiddleArea(p)) == 3 && LinkHelper.GetGroupLinkedDiagonals(tryBoard, moveGroup).Any())
                    return true;
            }
            return false;
        }

        /*
 15 . . . . . . . . . . . . . . . . . . .
 16 . . . . . . . . . . . . . . . . . . . 
 17 . . . . X X X X . . . . . . . . . . . 
 18 . . . . . . . . . . . . . . . . . . . 
         */
        public static Boolean StraightFourFormation(Board tryBoard, Group moveGroup)
        {
            HashSet<Point> contentPoints = moveGroup.Points;
            if (contentPoints.Count() != 4) return false;
            (int xLength, int yLength) = WithinGrid(contentPoints);
            if ((xLength == 0 || yLength == 0))
                return CheckAnyEndPointCovered(tryBoard, moveGroup);
            return false;
        }

        /*
 15 . . . . . . . . . . . . . . . X . . .
 16 . . . . . X X X . . . . . . X X X . . 
 17 . . . . . X X . . . . . . . . X . . . 
 18 . . . . . . . . . . . . . . . . . . . 
         */
        public static Boolean KnifeFiveFormation(Board tryBoard, Group moveGroup)
        {
            HashSet<Point> contentPoints = moveGroup.Points;
            if (contentPoints.Count() != 5) return false;
            //knife five formation
            IEnumerable<dynamic> pointIntersect = GetPointIntersect(tryBoard, contentPoints);
            if (WithinThreeByTwoGrid(moveGroup))
            {
                if (pointIntersect.Count(p => p.intersectCount == 3) == 1)
                    return true;
            }
            //star formation
            if (pointIntersect.Count(p => p.intersectCount == 4) == 1)
                return true;
            return false;
        }

        /*
    15 . . . . . . . . . . . . . . . . . X .
    16 . . . . X . . . . . . . X . . . X X . 
    17 . . . X X . . . . . . . X . . . . X . 
    18 . . . . X X . . . . . X X X . . . X . 
            */
        public static Boolean BentFiveFormation(Board tryBoard, Group moveGroup)
        {
            //includes T formation, one-by-four formation
            HashSet<Point> contentPoints = moveGroup.Points;
            if (contentPoints.Count() != 5) return false;

            IEnumerable<dynamic> pointIntersect = GetPointIntersect(tryBoard, contentPoints);
            List<Point> middlePoint = pointIntersect.Where(p => p.intersectCount >= 3).Select(p => (Point)p.point).ToList();
            if (middlePoint.Count != 1) return false;
            if (CheckAnyEndPointCovered(tryBoard, moveGroup))
                return true;
            return false;
        }

        /*
    15 . . . . . . . . . . . . . . . . . . .
    16 . . . . . . . . X . . . . . X . X . . 
    17 X X X . . . . . X . . . . . X X X . . 
    18 . . X X . . . . X X X . . . . . . . . 
            */
        public static Boolean CrowbarFiveFormation(Board tryBoard, Group moveGroup)
        {
            //includes three-by-two side formation
            HashSet<Point> contentPoints = moveGroup.Points;
            if (contentPoints.Count() != 5) return false;

            IEnumerable<dynamic> pointIntersect = GetPointIntersect(tryBoard, contentPoints);
            if (pointIntersect.Count(p => p.intersectCount == 2) == 3 && pointIntersect.Count(p => p.intersectCount == 1) == 2)
            {
                if (CheckAnyEndPointCovered(tryBoard, moveGroup))
                    return true;
            }
            return false;
        }

        /*
 15 . . . . . . X . . . . . . X . . . . .
 16 . . . . . X X X . . . . X X X X . . . 
 17 . . . . . X X . . . . . . X . . . . . 
 18 . . . . . . . . . . . . . . . . . . . 
         */
        public static Boolean FlowerSixFormation(Board tryBoard, Group moveGroup)
        {
            HashSet<Point> contentPoints = moveGroup.Points;
            if (contentPoints.Count() != 6) return false;
            IEnumerable<dynamic> pointIntersect = GetPointIntersect(tryBoard, contentPoints);
            if (pointIntersect.Count(p => p.intersectCount == 4) == 1)
            {
                if (pointIntersect.Count(p => p.intersectCount == 2) == 3)
                    return true;
                else if (CheckAnyEndPointCovered(tryBoard, moveGroup))
                    return true;
            }
            return false;
        }

        /*
    15 . . . . . . . . . . . . . . . . . . .
    16 . . . . X . . . . . . . . . . . . . . 
    17 . . . X X . . . . X X . . X X . . . . 
    18 . . . X X X . . X X X X . X X X X . . 

    15 . . . . X . . . . . . . . . . . . . .
    16 . . . . X X X X . . . . . . . . . . . 
    17 . . . . X . . . . . . . . . . . . . . 
    18 . . . . . . . . . . . . . . . . . . . 
            */
        public static Boolean KnifeSixFormation(Board tryBoard, Group moveGroup)
        {
            //includes two-by-four formation
            HashSet<Point> contentPoints = moveGroup.Points;
            if (contentPoints.Count() != 6) return false;
            IEnumerable<dynamic> pointIntersect = GetPointIntersect(tryBoard, contentPoints);
            if (pointIntersect.Count(p => p.intersectCount == 3) >= 1)
            {
                if (CheckAnyEndPointCovered(tryBoard, moveGroup))
                    return true;
            }
            return false;
        }

        /*
 14 X . . . . . . . . . . . . . . . . . .
 15 X X X . . . . . . . . . . . . . . . .
 16 X X . . . . . . . . . . . . . . . . . 
 17 X . . . . . . . X X X . . . . X X . . 
 18 . . . . . . . . X X X X . . X X X X X 
         */
        public static Boolean OddSevenFormation(Board tryBoard, Group moveGroup)
        {
            HashSet<Point> contentPoints = moveGroup.Points;
            if (contentPoints.Count() != 7) return false;

            IEnumerable<dynamic> pointIntersect = GetPointIntersect(tryBoard, contentPoints);
            if (pointIntersect.Count(p => p.intersectCount == 3) >= 2)
            {
                if (CheckAnyEndPointCovered(tryBoard, moveGroup))
                    return true;
            }
            return false;
        }

        /*
 14 . X . . . . . . . . . X . . . X X . .
 15 X X X . . . . . . . X X X . . X X X .
 16 X X . . . . . . . . X X . . . . X . . 
 17 X . . . . . . . . . . X . . . . X . . 
 18 . . . . . . . . . . . . . . . . . . . 
        
15 . . . . . . . . . . . . . x . . . . .
16 x x . . . . . . . . . . x x x x x . . 
17 x x x . . . . . . . . . . x . . . . . 
18 . x x . . . . . . . . . . . . . . . . 
         */
        public static Boolean FlowerSevenFormation(Board tryBoard, Group moveGroup)
        {
            HashSet<Point> contentPoints = moveGroup.Points;
            if (contentPoints.Count() != 7) return false;
            IEnumerable<dynamic> pointIntersect = GetPointIntersect(tryBoard, contentPoints);
            if (pointIntersect.Count(p => p.intersectCount == 4) == 1)
            {
                if (pointIntersect.Count(p => p.intersectCount == 2) == 6)
                    return true;
                else if (CheckAnyEndPointCovered(tryBoard, moveGroup))
                    return true;
            }
            return false;
        }


        /// <summary>
        /// Check any end point covered. 
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WuQingYuan_Q31682" />
        /// <see cref="UnitTestProject.KillerFormationTest.KillerFormationTest_Scenario_WuQingYuan_Q31682_2" />
        /// <see cref="UnitTestProject.KillerFormationTest.KillerFormationTest_Scenario_WuQingYuan_Q31682_x" />
        /// Two liberties <see cref="UnitTestProject.KillerFormationTest.KillerFormationTest_Scenario_WuQingYuan_Q31471" />
        /// <see cref="UnitTestProject.KillerFormationTest.KillerFormationTest_Scenario_Corner_A132" />
        /// <see cref="UnitTestProject.KillerFormationTest.KillerFormationTest_Scenario_WuQingYuan_Q31471_x" />
        /// <see cref="UnitTestProject.KillerFormationTest.KillerFormationTest_20230121_8" />
        /// </summary>
        private static Boolean CheckAnyEndPointCovered(Board tryBoard, Group moveGroup)
        {
            if (moveGroup.Liberties.Count > 2) return false;
            IEnumerable<dynamic> endPoints = GetPointIntersect(tryBoard, moveGroup.Points).Where(p => p.intersectCount == 1);
            return endPoints.Any(q => EndPointCovered((Point)q.point, tryBoard, moveGroup));
        }

        private static Boolean EndPointCovered(Point endPoint, Board tryBoard, Group moveGroup)
        {
            Content c = moveGroup.Content;
            List<Point> diagonals = LinkHelper.GetDiagonalPoint(tryBoard, endPoint);
            if (diagonals.Count == 0) return false;
            if (moveGroup.Liberties.Count == 2)
            {
                if (diagonals.Any(d => tryBoard[d] == c.Opposite())) return false;
                if (tryBoard.GetMoveLiberties(endPoint).Any()) return false;
                //check connect and die at end
                List<Point> nEndPoint = tryBoard.GetStoneNeighbours(endPoint).Where(n => tryBoard[n] == c.Opposite() && !tryBoard.GetDiagonalNeighbours(n).Any(s => tryBoard[s] == c && tryBoard.GetGroupAt(s) == moveGroup)).ToList();
                if (nEndPoint.Count != 1) return false;
                if (!ImmovableHelper.CheckConnectAndDie(tryBoard, tryBoard.GetGroupAt(nEndPoint.First()), false))
                    return false;
                return true;
            }
            else if (moveGroup.Liberties.Count == 1)
            {
                if (diagonals.Any(d => tryBoard[d] != c)) return false;
                //suicide move with one empty space or connect groups
                if (tryBoard.Move != null && tryBoard.GetStoneNeighbours().Count(n => tryBoard[n] == c) == 1 && !tryBoard.GetMoveLiberties().Any() && !tryBoard.Move.Equals(endPoint))
                    return false;
                return true;
            }
            return false;
        }

        /*
    15 . . . . . . . . . . . . . . . . . . .
    16 . . . . . . . . . . . . . . . . . . . 
    17 X . . . . . . . . . . . . . . . . . . 
    18 X X . . . . . . . . . . . . . . . . . 
        */
        public static Boolean CornerThreeFormation(Board tryBoard, Group moveGroup = null)
        {
            if (moveGroup == null) moveGroup = tryBoard.MoveGroup;
            else moveGroup = tryBoard.GetCurrentGroup(moveGroup);
            HashSet<Point> contentPoints = moveGroup.Points;
            if (contentPoints.Count() != 3 || tryBoard.MoveGroupLiberties != 1) return false;
            if (!contentPoints.Any(p => tryBoard.CornerPoint(p)) || contentPoints.Any(p => tryBoard.PointWithinMiddleArea(p))) return false;
            if (MaxLengthOfGrid(moveGroup.Points) != 1) return false;
            if (tryBoard.AtariTargets.Count == 0) return false;
            return true;
        }


        /// <summary>
        /// Possible corner three formation. 
        /// <see cref="UnitTestProject.LifeCheckTest.LifeCheckTest_Scenario_Corner_A139_2" />
        /// </summary>
        public static Boolean PossibleCornerThreeFormation(Board currentBoard, Point p, Content c)
        {
            Point corner = currentBoard.GetStoneNeighbours(p).FirstOrDefault(n => currentBoard.CornerPoint(n));
            if (!Convert.ToBoolean(corner.NotEmpty) || currentBoard[corner] != Content.Empty) return false;
            if (currentBoard.GetStoneNeighbours(corner).Any(n => currentBoard[n] != Content.Empty)) return false;
            if (currentBoard.GetDiagonalNeighbours(p).Any(n => currentBoard.PointWithinMiddleArea(n) && EyeHelper.FindRealEyeWithinEmptySpace(currentBoard, n, c)))
            {
                IEnumerable<Board> moveBoards = GameHelper.GetMoveBoards(currentBoard, currentBoard.GetStoneNeighbours(corner), c.Opposite());
                if (moveBoards.Any(b => !ImmovableHelper.CheckConnectAndDie(b, b.MoveGroup, false)))
                    return true;
            }
            return false;
        }

        /*
    15 . . . . . . . . . . . . . . . . . . .
    16 X . . . . . . . . . . . . . . . . . . 
    17 X X . . . . . . . . . . . . . . . . . 
    18 X X X . . . . . . . . . . . . . . . . 
        */
        public static Boolean CornerSixFormation(Board tryBoard, Group moveGroup = null)
        {
            if (moveGroup == null) moveGroup = tryBoard.MoveGroup;
            else moveGroup = tryBoard.GetCurrentGroup(moveGroup);
            HashSet<Point> contentPoints = moveGroup.Points;
            if (contentPoints.Count() != 6) return false;
            if (!contentPoints.Any(p => tryBoard.CornerPoint(p)) || contentPoints.Where(p => tryBoard.PointWithinMiddleArea(p)).Count() != 1) return false;
            IEnumerable<dynamic> pointIntersect = GetPointIntersect(tryBoard, contentPoints);
            if (pointIntersect.Count(p => p.intersectCount == 3) != 2) return false;
            return (MaxLengthOfGrid(moveGroup.Points) == 2);
        }

        /*
    15 . . . . . . . . . . . . . . . . . . .
    16 X . . . . . . . . . . . . . . . . . . 
    17 X . . . . . . . . . . . . . . . . . . 
    18 X X . . . . . . . . . . . . . . . . . 
        */
        public static Boolean BentFourCornerFormation(Board tryBoard, Group moveGroup = null)
        {
            if (moveGroup == null) moveGroup = tryBoard.MoveGroup;
            else moveGroup = tryBoard.GetCurrentGroup(moveGroup);
            HashSet<Point> contentPoints = moveGroup.Points;
            if (contentPoints.Count() != 4) return false;
            if (!contentPoints.Any(p => tryBoard.CornerPoint(p)) || contentPoints.Any(p => tryBoard.PointWithinMiddleArea(p))) return false;
            if (MaxLengthOfGrid(moveGroup.Points) != 2) return false;

            if (tryBoard.GetNeighbourGroups(moveGroup).Count != 1) return false;
            return true;
        }

        /// <summary>
        /// Get point intersect.
        /// </summary>
        public static IEnumerable<dynamic> GetPointIntersect(Board tryBoard, IEnumerable<Point> contentPoints)
        {
            return contentPoints.Select(p => new { point = p, intersectCount = tryBoard.GetStoneNeighbours(p).Intersect(contentPoints).Count() });
        }

        /// <summary>
        /// Killer group within 3 by 2 grid.
        /// </summary>
        public static Boolean WithinThreeByTwoGrid(Group moveGroup)
        {
            (int xLength, int yLength) = WithinGrid(moveGroup.Points);
            return ((xLength <= 2 && yLength <= 1) || (xLength <= 1 && yLength <= 2));
        }

        /// <summary>
        /// Rectangular space defining the max and min of points in x-axis and y-axis.
        /// </summary>
        public static (int, int) WithinGrid(IEnumerable<Point> points)
        {
            int xLength = points.Max(p => p.x) - points.Min(p => p.x);
            int yLength = points.Max(p => p.y) - points.Min(p => p.y);
            return (xLength, yLength);
        }

        /// <summary>
        /// Grid dimension changed.
        /// </summary>
        public static Boolean GridDimensionChanged(IEnumerable<Point> pointsA, IEnumerable<Point> pointsB)
        {
            (int xLengthA, int yLengthA) = WithinGrid(pointsA);
            (int xLengthB, int yLengthB) = WithinGrid(pointsB);
            return (xLengthA != xLengthB || yLengthA != yLengthB);
        }

        /// <summary>
        /// Max length of x and y length of grid.
        /// </summary>
        public static int MaxLengthOfGrid(IEnumerable<Point> points)
        {
            (int xLength, int yLength) = WithinGrid(points);
            int maxLength = Math.Max(xLength, yLength);
            return maxLength;
        }

        /// <summary>
        /// Opponent break kill formation.
        /// <see cref="UnitTestProject.SpecificNeutralMoveTest.SpecificNeutralMoveTest_Scenario_TianLongTu_Q16827" />
        /// <see cref="UnitTestProject.KillerFormationTest.KillerFormationTest_Scenario_TianLongTu_Q16859_2" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_TianLongTu_Q2413_2" /> 
        /// </summary>
        public static Boolean OpponentBreakKillFormation(Board tryBoard, Board currentBoard)
        {
            Point move = tryBoard.Move.Value;
            Content c = tryBoard.MoveGroup.Content;
            List<Group> groups = tryBoard.GetGroupsFromStoneNeighbours();
            if (groups.Count == 0 || groups.All(n => n.Points.Count < 4)) return false;
            if (KillerFormationHelper.TryKillFormation(currentBoard, c.Opposite(), new List<Point>() { move }))
                return true;
            return false;
        }

    }
}
