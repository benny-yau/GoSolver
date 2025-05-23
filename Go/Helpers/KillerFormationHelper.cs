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

        public static Boolean IsKillerFormationFromFunc(Board tryBoard, Group group = null)
        {
            if (group == null) group = tryBoard.MoveGroup;
            int contentCount = group.Points.Count;
            if (!KillerFormationFuncs.ContainsKey(contentCount)) return false;
            List<Func<Board, Group, Boolean>> funcs = KillerFormationFuncs[contentCount];
            Func<Board, Group, Boolean> killerFunc = funcs.FirstOrDefault(func => func(tryBoard, group));
            if (killerFunc != null)
                return true;
            return false;
        }

        /// <summary>
        /// Formations that are essentially dead and do not require a pass move to test for both alive.
        /// </summary>
        public static Boolean DeadFormationInBothAlive(Board board, Group killerGroup, int libertyCount = 2, int requiredCount = 1)
        {
            Content c = killerGroup.Content;
            List<Point> emptyPoints = killerGroup.Points.Where(t => board[t] == Content.Empty).ToList();
            if (emptyPoints.Count != libertyCount)
                return false;

            if (TryKillFormation(board, c, emptyPoints, requiredCount))
                return true;
            return false;
        }

        /// <summary>
        /// Make move at each of the empty points to test if formation created.
        /// </summary>
        public static Boolean TryKillFormation(Board board, Content c, List<Point> emptyPoints, int requiredCount = 1)
        {
            int count = 0;
            IEnumerable<Board> moveBoards = GameHelper.GetMoveBoards(board, emptyPoints, c);
            foreach (Board b in moveBoards)
            {
                if (IsKillerFormationFromFunc(b))
                {
                    count++;
                    if (count >= requiredCount)
                        return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Suicidal killer formations within survival group without any real eye.
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
        /// Check if real eye found in neighbour groups.
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_TianLongTu_Q16738_3" />
        /// Allow two-point group without real eye <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanGo_Q18472" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanQiJing_A38" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_TianLongTu_Q17183" />
        /// Check for corner six <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanQiJing_A38" />
        /// </summary>
        public static Boolean CheckRealEyeInNeighbourGroups(Board tryBoard, Board captureBoard)
        {
            Point move = tryBoard.Move.Value;
            Content c = tryBoard.MoveGroup.Content;
            //check for covered eye
            if (EyeHelper.CheckCoveredEyeAtSuicideGroup(tryBoard))
                return false;

            //allow two-point group without real eye
            Group killerGroup = GroupHelper.GetKillerGroupFromCache(captureBoard, move, c.Opposite());
            if (killerGroup != null && killerGroup.Points.Count <= 2 && !EyeHelper.FindRealEyeWithinEmptySpace(captureBoard, killerGroup))
                return false;

            //check for corner six
            if (CornerSixFormation(tryBoard))
                return false;

            //bent three
            if (BentThreeSuicideAtCoveredEye(tryBoard, captureBoard))
                return false;

            if (killerGroup == null) killerGroup = tryBoard.MoveGroup;

            //get all killer groups except move killer group
            List<Group> killerGroups = GroupHelper.GetKillerGroups(captureBoard, c.Opposite());
            List<Group> ngroups = captureBoard.GetNeighbourGroups(killerGroup);

            if (ngroups.Any(n => WallHelper.IsNonKillableGroup(captureBoard, n)))
                return true;

            foreach (Group kgroup in killerGroups.Where(gr => gr != killerGroup))
            {
                List<Group> cgroups = captureBoard.GetNeighbourGroups(kgroup);
                if (!cgroups.Intersect(ngroups).Any()) continue;
                //real eye with one neighbour group only
                if (cgroups.Count == 1)
                    return true;
                //find real eye
                if (EyeHelper.FindRealEyeWithinEmptySpace(captureBoard, kgroup) && WallHelper.StrongGroups(captureBoard, cgroups))
                    return true;
                if (EyeHelper.RealEyeOfDiagonallyConnectedGroups(captureBoard, kgroup))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Two-point move with empty point <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanGo_A48" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_GuanZiPu_A2" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_TianLongTu_Q17250" />
        /// Covered eye <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_TianLongTu_Q16424_2" />
        /// Check for snapback <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WindAndTime_Q30234" />
        /// Corner three formation <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_GuanZiPu_Q18860" />
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
        /// Corner six formation <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanQiJing_A38" />
        /// Corner five formation <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_Corner_A67_4" />
        /// Flower six formation <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_TianLongTu_Q16859" />
        /// Flower seven formation <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanGo_B3" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_TianLongTu_Q2413_2" /> 
        /// Knife six formation <see cref="UnitTestProject.KillerFormationTest.KillerFormationTest_Scenario_WuQingYuan_Q31682" />
        /// <see cref="UnitTestProject.KillerFormationTest.KillerFormationTest_Scenario_WuQingYuan_Q31682_3" />
        /// - Two-by-four side formation <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WuQingYuan_Q31682" />
        /// Bent five formation <see cref="UnitTestProject.KillerFormationTest.KillerFormationTest_Scenario_WuQingYuan_Q31682_2" />
        /// - T side formation (two liberties) <see cref="UnitTestProject.KillerFormationTest.KillerFormationTest_Scenario_WuQingYuan_Q31471" />
        /// - One-by-four side formation <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_Side_B32" />
        /// Seven side formation <see cref="UnitTestProject.KillerFormationTest.KillerFormationTest_Scenario_WuQingYuan_Q31471_6" />
        /// <see cref="UnitTestProject.KillerFormationTest.KillerFormationTest_Scenario_WuQingYuan_Q31471_7" />
        /// </summary>
        private static Boolean FindSuicidalKillerFormation(Board tryBoard, Board currentBoard, Board captureBoard)
        {
            Point move = tryBoard.Move.Value;
            Content c = tryBoard.MoveGroup.Content;
            if (tryBoard.MoveGroupLiberties > 2)
                return false;

            int moveCount = tryBoard.MoveGroup.Points.Count;
            if (moveCount == 2)
            {
                //two-point atari move
                if (TwoPointAtariMove(tryBoard, captureBoard))
                    return true;

                //covered eye
                if (EyeHelper.CheckCoveredEyeAtSuicideGroup(tryBoard))
                    return TwoPointSuicideAtCoveredEye(captureBoard, tryBoard);

                //two-point move with empty point
                if (GetLibertiesAtMove(tryBoard).Any())
                {
                    if (tryBoard.MoveGroupLiberties == 2 || SuicideMoveValidWithOneEmptySpaceLeft(tryBoard))
                        return true;
                    if (!GroupHelper.IsSingleGroupWithinKillerGroup(tryBoard))
                        return true;
                }

                //check for snapback
                if (ImmovableHelper.CheckSnapbackFromMove(tryBoard))
                    return true;

                //suicide for liberty fight
                if (SuicideForLibertyFight(tryBoard, currentBoard))
                    return true;

                if (SuicidalEndMove(tryBoard, currentBoard))
                    return true;
            }
            else if (moveCount == 3)
            {
                //move group binding
                if (tryBoard.GetStoneNeighbours().Count(n => tryBoard[n] == c) > 1)
                    return true;

                if (SuicideMoveValidWithOneEmptySpaceLeft(tryBoard))
                    return true;

                //corner three formation
                if (CornerThreeFormation(tryBoard))
                    return true;

                //bent three
                if (BentThreeSuicideAtCoveredEye(tryBoard, captureBoard))
                    return true;

                if (SuicidalEndMove(tryBoard, currentBoard))
                    return true;
            }
            else
            {
                //check killer formation from functions
                if (IsKillerFormationFromFunc(tryBoard))
                {
                    //check kill group extension
                    if (CheckRedundantKillGroupExtension(tryBoard, currentBoard))
                        return false;
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Redundant extension of kill group.
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_Corner_A8" />
        /// <see cref="UnitTestProject.KillerFormationTest.KillerFormationTest_Scenario_Corner_A113" />
        /// Empty point neighbour <see cref="UnitTestProject.KillerFormationTest.KillerFormationTest_Scenario_WuQingYuan_Q31471_8" />
        /// Atari target <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanQiJing_A40" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WuQingYuan_Q31499_3" />
        /// Whole group dying <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_GuanZiPu_A36" />
        /// Bent four corner formation <see cref="UnitTestProject.BentFourTest.BentFourTest_Scenario7kyu26_3" />
        /// Check previous group for killer formation <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanQiJing_A38" />
        /// Two kill formations <see cref="UnitTestProject.KillerFormationTest.KillerFormationTest_Scenario_XuanXuanGo_A54" />
        /// </summary>
        private static Boolean CheckRedundantKillGroupExtension(Board tryBoard, Board currentBoard)
        {
            Point move = tryBoard.Move.Value;
            Content c = tryBoard.MoveGroup.Content;
            //move group binding
            List<Group> previousGroups = LinkHelper.GetPreviousMoveGroup(currentBoard, tryBoard);
            if (previousGroups.Count > 1)
                return false;
            //empty point neighbour
            if (GetLibertiesAtMove(tryBoard).Any())
            {
                if (tryBoard.MoveGroupLiberties == 2 || SuicideMoveValidWithOneEmptySpaceLeft(tryBoard))
                    return false;
                if (LinkHelper.GetMoveDiagonals(tryBoard).Any())
                    return false;
            }

            //bent four corner formation
            if (BentFourCornerFormation(tryBoard) && UniquePatternsHelper.CheckForBentFour(currentBoard))
                return true;

            Group previousGroup = previousGroups.First();
            //whole group dying
            if (WholeGroupDying(tryBoard))
            {
                Point liberty = tryBoard.MoveGroup.Liberties.First();
                if (TryKillFormation(currentBoard, c, new List<Point>() { liberty }) && SuicidalEndMove(tryBoard, currentBoard))
                    return true;
                return false;
            }

            //check previous group for killer formation
            if (tryBoard.MoveGroupLiberties == 1 && IsKillerFormationFromFunc(currentBoard, previousGroup) && !KillerFormationHelper.CornerSixFormation(tryBoard))
                return true;

            //atari target
            if (tryBoard.AtariTargets.Any())
                return false;

            //grid dimension changed
            if (GridDimensionChanged(previousGroup.Points, tryBoard.MoveGroup.Points))
                return true;

            return false;
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
            if (tryBoard.MoveGroupLiberties == 1 && tryBoard.IsAtariMove)
            {
                List<Group> ngroups = tryBoard.GetNeighbourGroups();
                if (ngroups.Count != 1) return false;
                Point liberty = tryBoard.MoveGroup.Liberties.First();
                if (tryBoard.GetGroupsFromStoneNeighbours(liberty, c).Except(ngroups).Any()) return false;
                if (tryBoard.GetStoneNeighbours(liberty).Except(tryBoard.MoveGroup.Points).All(n => tryBoard[n] == c.Opposite()))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Suicide move with one empty space surrounded by opponent stones.
        /// Move group with three points <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario1kyu29" />
        /// Move group binding <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanQiJing_Weiqi101_B19_2" />
        /// </summary>
        public static Boolean SuicideMoveValidWithOneEmptySpaceLeft(Board tryBoard)
        {
            Point move = tryBoard.Move.Value;
            Content c = tryBoard.MoveGroup.Content;
            if (GetLibertiesAtMove(tryBoard).Any(n => tryBoard.GetStoneNeighbours(n).Where(q => !q.Equals(move)).All(q => tryBoard[q] == c.Opposite())))
                return true;
            return false;
        }

        /// <summary>
        /// Ensure link is connected to both stones from previous move group and to external group.
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_TianLongTu_Q16520_2" />
        /// Check connect and die <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WindAndTime_Q30403" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_GuanZiPu_A4Q11_101Weiqi_2" />
        /// Connect three or more groups <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanGo_B3" />
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
            if (CornerThreeFormation(tryBoard)) return false;
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
            if (lostGroups.Count == 1 && lostGroups.First().Points.Count <= 2)
            {
                if (linkBoard.MoveGroupLiberties == 2 && !WallHelper.IsNonKillableGroup(linkBoard)) return false;
                return true;
            }
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
            if (!WallHelper.IsHostileNeighbourGroup(capturedBoard))
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
        /// Ko move on external liberty (optional) <see cref="UnitTestProject.DailyGoProblems.DailyGoProblems_20221024_5" />
        /// </summary>
        public static Boolean SuicideForLibertyFight(Board tryBoard, Board currentBoard)
        {
            Point move = tryBoard.Move.Value;
            Content c = tryBoard.MoveGroup.Content;
            if (tryBoard.MoveGroupLiberties != 1) return false;
            //suicide within killer group
            Group killerGroup = GroupHelper.GetKillerGroupFromCache(currentBoard, move, c.Opposite());
            if (killerGroup == null || killerGroup.Points.Count != tryBoard.MoveGroup.Points.Count + 1) return false;

            List<Group> targetGroups = currentBoard.GetNeighbourGroups(killerGroup);
            //get only one move within killer group
            if (targetGroups.Count == 1)
            {
                Boolean firstPoint = killerGroup.Points.FirstOrDefault(p => currentBoard[p] == Content.Empty).Equals(move);
                if (!firstPoint) return false;
            }

            //get external liberty
            foreach (Group targetGroup in targetGroups)
            {
                List<Point> externalLiberties = targetGroup.Liberties.Where(n => GroupHelper.GetKillerGroupFromCache(currentBoard, n, c.Opposite()) != killerGroup).ToList();
                if (externalLiberties.Count != 1) continue;
                Point liberty = externalLiberties.First();
                if (KoHelper.IsKoFight(tryBoard, liberty, c.Opposite()).Item1)
                {
                    //check killer ko within killer group
                    if (GroupHelper.GetKillerGroupFromCache(tryBoard, targetGroup.Points.First(), c) == null) continue;
                    if (WallHelper.TargetWithAnyNonKillableGroup(tryBoard, targetGroup)) continue;
                }
                else
                {
                    List<Group> groups = tryBoard.GetGroupsFromStoneNeighbours(liberty, c.Opposite()).ToList();
                    if (groups.Count == 0 || tryBoard.GetLibertiesOfGroups(groups).Count == 1) continue;
                }

                //check suicidal move for both players at external liberty
                if (ImmovableHelper.IsSuicidalMoveForBothPlayers(tryBoard, liberty) || ImmovableHelper.IsSuicidalMoveForBothPlayers(currentBoard, liberty, true))
                {
                    HashSet<Group> groups = currentBoard.GetGroupsFromStoneNeighbours(liberty, c.Opposite());
                    if (groups.Any(n => ImmovableHelper.EscapeCaptureLink(currentBoard, n)))
                        continue;
                    return true;
                }
            }

            //ko move on external liberty (optional)
            foreach (Group targetGroup in targetGroups)
            {
                List<Point> externalLiberties = targetGroup.Liberties.Where(n => GroupHelper.GetKillerGroupFromCache(currentBoard, n, c.Opposite()) != killerGroup).ToList();
                if (externalLiberties.Count != 1) continue;
                Point liberty = externalLiberties.First();
                Board b = currentBoard.MakeMoveOnNewBoard(liberty, c, true);
                if (b != null && KoHelper.IsKoFight(b))
                {
                    Point? eye = KoHelper.GetKoEyePoint(b);
                    if (eye != null && ImmovableHelper.IsSuicidalMoveForBothPlayers(b, eye.Value, true))
                        return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Two-point suicide at covered eye. 
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.CoveredEyeMoveTest_Scenario_WuQingYuan_Q31469" />
        /// Make move at the other empty point <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanQiJing_B57" />
        /// </summary>
        public static Boolean TwoPointSuicideAtCoveredEye(Board capturedBoard, Board tryBoard)
        {
            if (capturedBoard == null || tryBoard == null) return false;
            Point move = tryBoard.Move.Value;
            Content c = capturedBoard.MoveGroup.Content;
            if (tryBoard.MoveGroup.Points.Count != 2) return false;
            foreach (Group group in capturedBoard.CapturedList)
            {
                if (group.Points.Count != 2) continue;
                //make move again at last move
                Board b = capturedBoard.MakeMoveOnNewBoard(move, c.Opposite());
                if (b == null) continue;
                //capture move and find covered eye
                if (EyeHelper.FindCoveredEyeByCapture(b))
                    return true;
                if (!GetLibertiesAtMove(tryBoard).Any()) continue;
                //make move at the other empty point
                Point move2 = group.Points.First(p => !p.Equals(move));
                Board b2 = capturedBoard.MakeMoveOnNewBoard(move2, c.Opposite());
                if (b2 == null) continue;
                if (EyeHelper.FindCoveredEyeByCapture(b2))
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
        /// Check one point atari move
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
            List<Point> emptyPoints = b.GetStoneNeighbours(q).Where(n => b[n] == Content.Empty).ToList();
            if (emptyPoints.Count != 1) return false;

            Group killerGroup = GroupHelper.GetKillerGroupFromCache(b, q, c.Opposite());
            if (killerGroup != null && killerGroup.Points.Count == 2 && EyeHelper.IsCovered(b, emptyPoints.First(), c.Opposite()))
                return true;
            return false;
        }

        /// <summary>
        /// Get liberties at move.
        /// </summary>
        public static IEnumerable<Point> GetLibertiesAtMove(Board tryBoard)
        {
            return tryBoard.GetStoneNeighbours().Where(p => tryBoard[p] == Content.Empty);
        }

        /// <summary>
        /// Three opponent groups at move.
        /// </summary>
        public static Boolean ThreeOpponentGroupsAtMove(Board tryBoard, Point? eyePoint = null)
        {
            if (eyePoint == null) eyePoint = tryBoard.Move.Value;
            Content c = tryBoard[eyePoint.Value];
            if (tryBoard.GetStoneNeighbours(eyePoint).Count(n => tryBoard[n] == c.Opposite()) > 2)
            {
                List<Point> diagonals = ImmovableHelper.GetDiagonalsOfTigerMouth(tryBoard, eyePoint.Value, c.Opposite());
                if (diagonals.All(d => tryBoard[d] != c.Opposite()))
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
                if (endPoints.Any(p => !p.Equals(move) && EyeHelper.IsCovered(b, p, c.Opposite())))
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

        public static Boolean TwoByTwoFormation(Board tryBoard, IEnumerable<Point> contentPoints)
        {
            if (contentPoints.Count() != 4) return false;
            IEnumerable<dynamic> pointIntersect = GetPointIntersect(tryBoard, contentPoints);
            if (pointIntersect.Count(p => p.intersectCount == 2) == 2)
            {
                if (contentPoints.GroupBy(p => p.x).Where(gr => gr.Count() == 2).Count() == 2 || contentPoints.GroupBy(p => p.y).Where(gr => gr.Count() == 2).Count() == 2)
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
        public static Boolean CrowbarFormation(Board tryBoard, Group moveGroup)
        {
            HashSet<Point> contentPoints = moveGroup.Points;
            if (contentPoints.Count() != 4) return false;
            IEnumerable<dynamic> pointIntersect = GetPointIntersect(tryBoard, contentPoints);
            if (pointIntersect.Count(p => p.intersectCount == 2) == 2)
            {
                if (contentPoints.GroupBy(p => p.x).Where(gr => gr.Count() == 3).Count() == 1 || contentPoints.GroupBy(p => p.y).Where(gr => gr.Count() == 3).Count() == 1)
                    return true;
            }
            return false;
        }

        /*
 15 . . . . . . . . . . . . . . . . . . .
 16 . . . . . . . . . . . . . . . . . . . 
 17 . . . . . X X X . . . . . . . . . . . 
 18 . . . . . X . . . . . . . . . . . . . 
         */
        public static Boolean CrowbarEyeFormation(Board tryBoard, Group moveGroup)
        {
            if (CrowbarFormation(tryBoard, moveGroup))
            {
                HashSet<Point> contentPoints = moveGroup.Points;
                Group group = tryBoard.GetGroupAt(contentPoints.First());
                Point eyePoint = group.Liberties.FirstOrDefault(p => tryBoard.GetStoneNeighbours(p).Intersect(contentPoints).Count() == 2);
                if (!Convert.ToBoolean(eyePoint.NotEmpty)) return false;
                if (!tryBoard.PointWithinMiddleArea(eyePoint))
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
                Content c = moveGroup.Content;
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
            Content c = moveGroup.Content;
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
 15 . . . . . . X . . . . . . X . . . . .
 16 . . . . . X X X . . . . X X X X . . . 
 17 . . . . . X X . . . . . . X . . . . . 
 18 . . . . . . . . . . . . . . . . . . . 
         */
        public static Boolean FlowerSixFormation(Board tryBoard, Group moveGroup)
        {
            Content c = moveGroup.Content;
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
 14 X . . . . . . . . . . . . . . . . . .
 15 X X X . . . . . . . . . . . . . . . .
 16 X X . . . . . . . . . . . . . . . . . 
 17 X . . . . . . . X X X . . . . X X . . 
 18 . . . . . . . . X X X X . . X X X X X 
         */
        public static Boolean OddSevenFormation(Board tryBoard, Group moveGroup)
        {
            Content c = moveGroup.Content;
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
            Content c = moveGroup.Content;
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
            Content c = moveGroup.Content;
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
    15 . . . . . . . . . . . . . . . . . X .
    16 . . . . X . . . . . . . X . . . X X . 
    17 . . . X X . . . . . . . X . . . . X . 
    18 . . . . X X . . . . . X X X . . . X . 
            */
        public static Boolean BentFiveFormation(Board tryBoard, Group moveGroup)
        {
            //includes T formation, one-by-four formation
            Content c = moveGroup.Content;
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
            Content c = moveGroup.Content;
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
            Content c = moveGroup.Content;
            if (moveGroup.Liberties.Count > 2) return false;
            IEnumerable<dynamic> pointIntersect = GetPointIntersect(tryBoard, moveGroup.Points);
            List<Point> endPoints = pointIntersect.Where(p => p.intersectCount == 1).Select(p => (Point)p.point).ToList();
            return endPoints.Any(q => EndPointCovered(q, tryBoard, moveGroup));
        }

        public static Boolean EndPointCovered(Point endPoint, Board tryBoard, Group moveGroup)
        {
            Content c = moveGroup.Content;
            Point p = tryBoard.GetStoneNeighbours(endPoint).First(n => tryBoard[n] == c);
            List<Point> diagonals = tryBoard.GetDiagonalNeighbours(endPoint).Where(d => !tryBoard.GetStoneNeighbours(p).Any(n => n.Equals(d))).ToList();
            if (diagonals.Count == 0) return false;
            if (moveGroup.Liberties.Count == 2)
            {
                if (diagonals.Any(d => tryBoard[d] == c.Opposite())) return false;
                if (tryBoard.GetStoneNeighbours(endPoint).Any(n => tryBoard[n] == Content.Empty)) return false;
                return true;
            }
            else if (moveGroup.Liberties.Count == 1)
            {
                if (diagonals.Any(d => tryBoard[d] != c)) return false;
                //suicide move with one empty space or connect groups
                if (tryBoard.Move != null && tryBoard.GetStoneNeighbours().Count(n => tryBoard[n] == c) < 2 && !tryBoard.GetStoneNeighbours().Any(n => tryBoard[n] == Content.Empty)) return false;

                //get neighbour of end point
                List<Point> nEndPoint = tryBoard.GetStoneNeighbours(endPoint).Where(n => tryBoard[n] != c && !tryBoard.GetDiagonalNeighbours(n).Intersect(moveGroup.Points).Any()).ToList();
                if (nEndPoint.Count != 1) return false;
                List<Point> nPoints = tryBoard.GetStoneNeighbours(nEndPoint.First());
                if (nPoints.Count(n => tryBoard[n] == c) >= nPoints.Count - 1 && LinkHelper.GetDiagonalPoints(tryBoard, nEndPoint.First(), c).Any())
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
            Content c = moveGroup.Content;
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
            Content c = moveGroup.Content;
            HashSet<Point> contentPoints = moveGroup.Points;
            if (contentPoints.Count() != 6) return false;
            if (!contentPoints.Any(p => tryBoard.CornerPoint(p))) return false;
            if (contentPoints.Where(p => tryBoard.PointWithinMiddleArea(p)).Count() != 1) return false;
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
            IEnumerable<dynamic> pointIntersect = contentPoints.Select(p => new { point = p, intersectCount = tryBoard.GetStoneNeighbours(p).Intersect(contentPoints).Count() });
            return pointIntersect;
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

        /// <summary>
        /// Return number of possible eyes that can be created at stone neighbour points.
        /// </summary>
        public static int PossibleEyesCreated(Board currentBoard, Point p, Content c)
        {
            List<Point> nstones = currentBoard.GetStoneNeighbours(p);
            List<Point> possibleEyes = nstones.Where(n => currentBoard[n] != c).ToList();
            return possibleEyes.Count;
        }

        /// <summary>
        /// Point for killer to form max binding.
        /// Ordering <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_GuanZiPu_A36_2" />
        /// <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_WuQingYuan_Q30919_2" />
        /// Check for opponent break formation <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_Corner_A80_3" />
        /// </summary>
        public static Point? GetMaxBindingPoint(Board currentBoard, IEnumerable<Board> killBoards, Group killerGroup)
        {
            Content c = killerGroup.Content;
            List<LinkedPoint<Point>> list = new List<LinkedPoint<Point>>();
            foreach (Board b in killBoards)
            {
                Point p = b.Move.Value;
                List<Point> movePoints = b.MoveGroup.Points.ToList();
                int maxLengthOfGrid = MaxLengthOfGrid(movePoints);
                int maxIntersect = movePoints.Max(q => b.GetStoneNeighbours(q).Intersect(movePoints).Count());
                int moveGroupLiberties = b.MoveGroupLiberties;
                list.Add(new LinkedPoint<Point>(p, new { maxLengthOfGrid, maxIntersect, moveGroupLiberties, b }));
            }
            //order by grid length then by max of intersection then by move group liberties
            list = list.OrderBy(m => ((dynamic)m.CheckMove).maxLengthOfGrid).ThenByDescending(m => ((dynamic)m.CheckMove).maxIntersect).ThenByDescending(m => ((dynamic)m.CheckMove).moveGroupLiberties).ToList();

            //check for equals in ordering
            if (list.Count == 1) return list.First().Move;
            dynamic moveA = list[0].CheckMove;
            dynamic moveB = list[1].CheckMove;
            if (moveA.maxLengthOfGrid == moveB.maxLengthOfGrid && moveA.maxIntersect == moveB.maxIntersect && moveA.moveGroupLiberties == moveB.moveGroupLiberties)
                return null;

            //check for opponent break formation
            if (killerGroup.Points.Count == 2 && killerGroup.Points.Any(p => currentBoard.CornerPoint(p)))
            {
                Board killBoard = killBoards.FirstOrDefault(b => OpponentBreakKillFormation(b, currentBoard));
                if (killBoard != null) return killBoard.Move.Value;
            }
            return list.First().Move;
        }

    }
}
