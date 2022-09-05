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
                    killerFormationFuncs.Add(4, new List<Func<Board, Group, Boolean>>() { OneByThreeFormation, BoxFormation, CrowbarEdgeFormation, StraightFourFormation, TwoByTwoFormation, BentFourCornerFormation });
                    killerFormationFuncs.Add(5, new List<Func<Board, Group, Boolean>>() { KnifeFiveFormation, CrowbarFiveFormation, BentFiveFormation });
                    killerFormationFuncs.Add(6, new List<Func<Board, Group, Boolean>>() { FlowerSixFormation, KnifeSixFormation, CornerSixFormation });
                    killerFormationFuncs.Add(7, new List<Func<Board, Group, Boolean>>() { FlowerSevenFormation, OddSevenFormation });
                }
                return killerFormationFuncs;
            }
        }

        public static Boolean IsKillerFormationFromFunc(Board tryBoard, Group group)
        {
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
            foreach (Point emptyPoint in emptyPoints)
            {
                Board b = board.MakeMoveOnNewBoard(emptyPoint, c);
                if (b == null) return false;
                Group group = b.GetGroupAt(emptyPoint);
                if (IsKillerFormationFromFunc(b, group))
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
        public static Boolean SuicidalKillerFormations(Board tryBoard, Board currentBoard, Board capturedBoard = null)
        {
            Point move = tryBoard.Move.Value;
            Content c = tryBoard.MoveGroup.Content;

            //check liberties of move group
            if (tryBoard.MoveGroupLiberties > 2) return false;
            //check suicide at eye point
            if (tryBoard.MoveGroupLiberties == 2 && tryBoard.GetStoneNeighbours().All(n => tryBoard[n] == c) && LinkHelper.GetPreviousMoveGroup(currentBoard, tryBoard).All(n => n.Liberties.Count > 1))
                return false;

            //check if neighbour group is non-killable
            if (!EyeHelper.CheckCoveredEyeAtSuicideGroup(tryBoard) && tryBoard.GetNeighbourGroups().Any(n => WallHelper.IsNonKillableGroup(tryBoard, n)))
                return false;

            //create captured board
            if (capturedBoard == null)
            {
                if (tryBoard.MoveGroupLiberties == 1)
                    capturedBoard = ImmovableHelper.CaptureSuicideGroup(tryBoard);
                else if (tryBoard.MoveGroupLiberties == 2)
                    (_, capturedBoard) = ImmovableHelper.ConnectAndDie(tryBoard);
            }

            //find killer formation
            if (!FindSuicidalKillerFormation(tryBoard, currentBoard, capturedBoard)) return false;

            //check if real eye found in neighbour groups
            if (CheckRealEyeInNeighbourGroups(tryBoard, capturedBoard))
                return false;

            //check link to external group
            if (IsLinkToExternalGroup(tryBoard, currentBoard, capturedBoard))
                return false;
            return true;
        }

        /// <summary>
        /// Check if real eye found in neighbour groups.
        /// Check for covered eye <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_TianLongTu_Q16738_3" />
        /// Allow two-point group without real eye <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanGo_Q18472" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanQiJing_A38" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_TianLongTu_Q17183" />
        /// Check for corner six <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanQiJing_A38" />
        /// Find real eye <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_GuanZiPu_Q18796_2" />
        /// </summary>
        public static Boolean CheckRealEyeInNeighbourGroups(Board tryBoard, Board captureBoard = null)
        {
            Board b = captureBoard ?? tryBoard;
            Point move = tryBoard.Move.Value;
            Content c = tryBoard.MoveGroup.Content;
            //check for covered eye
            if (EyeHelper.CheckCoveredEyeAtSuicideGroup(b, tryBoard.MoveGroup))
                return false;

            //allow two-point group without real eye
            Group killerGroup = GroupHelper.GetKillerGroupFromCache(b, move, c.Opposite());
            if (tryBoard.MoveGroup.Points.Count <= 2)
            {
                if (killerGroup != null && killerGroup.Points.Count <= 2 && !EyeHelper.FindRealEyeWithinEmptySpace(b, killerGroup))
                    return false;
            }

            //check for corner six
            if (KillerFormationHelper.CornerSixFormation(tryBoard, tryBoard.MoveGroup))
                return false;

            //corner ko move
            if (CheckCornerKoMoveForRealEye(tryBoard))
                return false;

            if (killerGroup == null) killerGroup = tryBoard.MoveGroup;

            //get all killer groups except move killer group
            List<Group> killerGroups = GroupHelper.GetKillerGroups(b, c.Opposite());
            List<Group> neighbourGroups = b.GetNeighbourGroups(killerGroup);

            if (neighbourGroups.Any(group => WallHelper.IsNonKillableGroup(b, group)))
                return true;

            foreach (Group kgroup in killerGroups.Where(gr => gr != killerGroup))
            {
                List<Group> neighbourKillerGroups = b.GetNeighbourGroups(kgroup);
                if (!neighbourKillerGroups.Intersect(neighbourGroups).Any()) continue;
                //real eye with one neighbour group only
                if (neighbourKillerGroups.Count == 1)
                    return true;
                //find real eye
                if (EyeHelper.FindRealEyeWithinEmptySpace(b, kgroup, EyeType.SemiSolidEye) && WallHelper.StrongNeighbourGroups(b, neighbourKillerGroups))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Check corner ko move for real eye. Similar to CheckOnePointMoveInConnectAndDie.
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_TianLongTu_Q16446" /> 
        /// </summary>
        private static Boolean CheckCornerKoMoveForRealEye(Board tryBoard)
        {
            Content c = tryBoard.MoveGroup.Content;
            List<Point> corner = tryBoard.GetStoneNeighbours().Where(n => tryBoard.CornerPoint(n)).ToList();
            if (corner.Count != 1) return false;
            if (tryBoard.MoveGroup.Liberties.Count != 2 || tryBoard.MoveGroup.Points.Count > 3) return false;
            if (tryBoard.GetNeighbourGroups().Count <= 1) return false;
            Boolean koEnabled = KoHelper.KoContentEnabled(c, tryBoard.GameInfo);
            if (!koEnabled) return false;
            Point? tigerMouth = ImmovableHelper.FindTigerMouth(tryBoard, corner.First(), c);
            if (tigerMouth == null) return false;

            (_, Board captureBoard) = ImmovableHelper.ConnectAndDie(tryBoard);
            if (captureBoard == null) return false;
            (Boolean suicidal, Board b) = ImmovableHelper.IsSuicidalMove(tigerMouth.Value, c, captureBoard);
            if (!suicidal && !ImmovableHelper.CheckConnectAndDie(b))
                return true;
            return false;
        }

        /// <summary>
        /// Two-point move with empty point <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanGo_A48" />
        /// Covered eye <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_TianLongTu_Q16424_2" />
        /// Check for snapback <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WindAndTime_Q30234" />
        /// Whole survival group dying <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_GuanZiPu_B3" />
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
        /// Flower six formation <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_TianLongTu_Q16859" />
        /// Flower seven formation <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanGo_B3" />
        /// Knife six formation <see cref="UnitTestProject.KillerFormationTest.KillerFormationTest_Scenario_WuQingYuan_Q31682" />
        /// <see cref="UnitTestProject.KillerFormationTest.KillerFormationTest_Scenario_WuQingYuan_Q31682_3" />
        /// - Two-by-four side formation <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WuQingYuan_Q31682" />
        /// Bent five formation <see cref="UnitTestProject.KillerFormationTest.KillerFormationTest_Scenario_WuQingYuan_Q31682_2" />
        /// - T side formation (two liberties) <see cref="UnitTestProject.KillerFormationTest.KillerFormationTest_Scenario_WuQingYuan_Q31471" />
        /// - One-by-four side formation <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_Side_B32" />
        /// Seven side formation <see cref="UnitTestProject.KillerFormationTest.KillerFormationTest_Scenario_WuQingYuan_Q31471_6" />
        /// <see cref="UnitTestProject.KillerFormationTest.KillerFormationTest_Scenario_WuQingYuan_Q31471_7" />
        /// </summary>
        private static Boolean FindSuicidalKillerFormation(Board tryBoard, Board currentBoard, Board capturedBoard = null)
        {
            Point move = tryBoard.Move.Value;
            Content c = tryBoard.MoveGroup.Content;
            if (tryBoard.MoveGroupLiberties > 2)
                return false;

            int moveCount = tryBoard.MoveGroup.Points.Count;
            if (moveCount == 2)
            {
                //covered eye
                if (EyeHelper.CheckCoveredEyeAtSuicideGroup(tryBoard))
                    return EyeHelper.TwoPointSuicideAtCoveredEye(capturedBoard, tryBoard);

                //two-point move with empty point
                if (tryBoard.GetStoneNeighbours().Any(p => tryBoard[p] == Content.Empty))
                    return true;

                //check for snapback
                if (ImmovableHelper.CheckSnapbackInNeighbourGroups(tryBoard, tryBoard.MoveGroup))
                    return true;
            }
            else if (moveCount == 3)
            {
                //move group binding
                if (tryBoard.GetStoneNeighbours().Count(n => tryBoard[n] == c) > 1)
                    return true;

                if (SuicideMoveValidWithOneEmptySpaceLeft(tryBoard))
                    return true;

                //whole survival group dying
                if (tryBoard.IsAtariMove && tryBoard.GetNeighbourGroups().Count == 1)
                    return true;

                if (KillerFormationHelper.CornerThreeFormation(tryBoard, tryBoard.MoveGroup))
                    return true;
            }
            else
            {
                //check kill group extension
                if (CheckRedundantKillGroupExtension(tryBoard, currentBoard))
                {
                    if (moveCount == 4) return false;
                    if (KillerFormationHelper.GridDimensionChanged(LinkHelper.GetPreviousMoveGroup(currentBoard, tryBoard).First().Points, tryBoard.MoveGroup.Points))
                        return false;
                }

                //check killer formation from functions
                if (IsKillerFormationFromFunc(tryBoard, tryBoard.MoveGroup))
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Redundant extension of kill group.
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_Corner_A8" />
        /// Atari target <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanQiJing_A40" />
        /// <see cref="UnitTestProject.KillerFormationTest.KillerFormationTest_Scenario_Corner_A8" />
        /// </summary>
        private static Boolean CheckRedundantKillGroupExtension(Board tryBoard, Board currentBoard)
        {
            if (tryBoard.MoveGroupLiberties != 1) return false;
            if (tryBoard.AtariTargets.Count > 0 && !BentFourCornerFormation(tryBoard, tryBoard.MoveGroup)) return false;
            if (LinkHelper.GetPreviousMoveGroup(currentBoard, tryBoard).Count > 1) return false;
            if (SuicideMoveValidWithOneEmptySpaceLeft(tryBoard))
                return false;
            return true;
        }

        /// <summary>
        /// Suicide move with one empty space in killer group. If two-point move, ensure is covered eye. If three-point move, ensure move is next to empty point.
        /// Move group with three points <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario1kyu29" />
        /// Move group binding <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanQiJing_Weiqi101_B19_2" />
        /// </summary>
        public static Boolean SuicideMoveValidWithOneEmptySpaceLeft(Board tryBoard)
        {
            int moveCount = tryBoard.MoveGroup.Points.Count;
            Point move = tryBoard.Move.Value;
            Content c = tryBoard.MoveGroup.Content;
            Group killerGroup = GroupHelper.GetKillerGroupFromCache(tryBoard, move, c.Opposite());
            //killer group contains only one more empty space
            if (killerGroup != null && killerGroup.Points.Count == moveCount + 1)
            {
                //get empty point
                Point p = killerGroup.Points.First(k => tryBoard[k] == Content.Empty);
                //move is next to empty point
                List<Point> stoneNeighbours = tryBoard.GetStoneNeighbours(p);
                if (stoneNeighbours.Any(n => n.Equals(move)) && stoneNeighbours.Where(n => !n.Equals(move)).All(n => tryBoard[n] == c.Opposite()))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Ensure link is connected to both stones from previous move group and to external group.
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_TianLongTu_Q16520_2" />
        /// Lost group not more than two points <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WuQingYuan_Q31682" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_TianLongTu_Q17154" />
        /// Connect three or more groups <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanGo_B3" />
        /// No lost groups <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanQiJing_Weiqi101_18402_2" />
        /// Check connect and die <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WindAndTime_Q30403" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_GuanZiPu_A4Q11_101Weiqi_2" />
        /// </summary>
        private static Boolean IsLinkToExternalGroup(Board tryBoard, Board currentBoard, Board capturedBoard)
        {
            Point move = tryBoard.Move.Value;
            Content c = tryBoard.MoveGroup.Content;
            if (tryBoard.MoveGroupLiberties != 1 || capturedBoard == null) return false;
            Point? liberty = capturedBoard.Move;
            Board tryLinkBoard = currentBoard.MakeMoveOnNewBoard(liberty.Value, c);
            if (tryLinkBoard == null || tryLinkBoard.MoveGroupLiberties == 1) return false;

            //get previous move group
            List<Group> groups = LinkHelper.GetPreviousMoveGroup(currentBoard, tryBoard);
            //connect three or more groups
            if (groups.Count >= 3) return false;
            //connected to previous move group
            Boolean moveConnected = tryLinkBoard.GetStoneNeighbours().Any(p => groups.Any(group => group.Points.Contains(p)));
            if (moveConnected && LinkHelper.IsAbsoluteLinkForGroups(currentBoard, tryLinkBoard))
            {
                HashSet<Group> linkGroups = currentBoard.GetGroupsFromStoneNeighbours(tryLinkBoard.Move.Value, c.Opposite());
                //connected to external group not from previous move group
                if (!linkGroups.Except(groups).Any()) return false;
                //check connect and die
                if (ImmovableHelper.CheckConnectAndDie(tryLinkBoard))
                    return false;
                //saved groups
                List<Group> savedGroups = linkGroups.Intersect(groups).ToList();
                if (savedGroups.Count == 0) return false;
                List<Group> lostGroups = groups.Except(savedGroups).ToList();
                //no lost groups
                if (lostGroups.Count == 0) return true;
                //lost group not more than two points
                if (lostGroups.Count == 1 && lostGroups.First().Points.Count <= 2)
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
        public static Boolean StraightThreeFormation(Board tryBoard, List<Point> contentPoints)
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
        public static Boolean BentThreeFormation(Board tryBoard, List<Point> contentPoints)
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
        public static Boolean TwoByTwoFormation(Board tryBoard, Group moveGroup)
        {
            Content c = moveGroup.Content;
            if (TwoByTwoFormation(tryBoard, moveGroup.Points, c))
            {
                //check for atari after capture
                Board capturedBoard = ImmovableHelper.CaptureSuicideGroup(tryBoard, moveGroup);
                if (capturedBoard == null) return false;
                foreach (Point p in moveGroup.Points)
                {
                    (Boolean isSuicidal, Board b) = ImmovableHelper.IsSuicidalMove(p, c, capturedBoard);
                    if (isSuicidal) continue;
                    if (b != null && b.AtariTargets.Count == 1 && b.AtariTargets.First().Points.Count > 1)
                        return true;
                }
                //check end point covered
                List<Point> contentPoints = moveGroup.Points.Where(t => tryBoard[t] == c).ToList();
                if (CheckAnyEndPointCovered(contentPoints, tryBoard, moveGroup))
                    return true;
            }
            return false;
        }

        public static Boolean TwoByTwoFormation(Board tryBoard, IEnumerable<Point> killerPoints, Content c)
        {
            List<Point> contentPoints = killerPoints.Where(t => tryBoard[t] == c).ToList();
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
                //edge formation
                if (moveGroup.Points.Count(p => !tryBoard.PointWithinMiddleArea(p)) == 3 && LinkHelper.GetGroupDiagonals(tryBoard, moveGroup).Any(d => tryBoard[d.Move] == c))
                    return true;
                //check end point covered
                List<Point> contentPoints = moveGroup.Points.Where(t => tryBoard[t] == c).ToList();
                if (CheckAnyEndPointCovered(contentPoints, tryBoard, moveGroup))
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
                return CheckAnyEndPointCovered(contentPoints, tryBoard, moveGroup);
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
                else if (CheckAnyEndPointCovered(contentPoints, tryBoard, moveGroup))
                    return true;
            }
            return false;
        }

        /*
 14 X . . . . . . . . . . . . . . . . . .
 15 X X X . . . . . . . . . . . . . . . .
 16 X X . . . . . . . . . . . . . . . . . 
 17 X . . . . . . . X X X . . . . . . . . 
 18 . . . . . . . . X X X X . . . . . . . 
         */
        public static Boolean OddSevenFormation(Board tryBoard, Group moveGroup)
        {
            Content c = moveGroup.Content;
            HashSet<Point> contentPoints = moveGroup.Points;
            if (contentPoints.Count() != 7) return false;

            IEnumerable<dynamic> pointIntersect = GetPointIntersect(tryBoard, contentPoints);
            if (pointIntersect.Count(p => p.intersectCount == 3) == 3)
            {
                if (CheckAnyEndPointCovered(contentPoints, tryBoard, moveGroup))
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
        
15 . . . . X . . . . . . . . . . . . . .
16 . . . X X X X X . . . . . . . . . . . 
17 . . . . X . . . . . . . . . . . . . . 
18 . . . . . . . . . . . . . . . . . . . 
         */
        public static Boolean FlowerSevenFormation(Board tryBoard, Group moveGroup)
        {
            Content c = moveGroup.Content;
            HashSet<Point> contentPoints = moveGroup.Points;
            if (contentPoints.Count() != 7) return false;
            IEnumerable<dynamic> pointIntersect = GetPointIntersect(tryBoard, contentPoints);
            if (pointIntersect.Count(p => p.intersectCount == 4) == 1)
            {
                int threeAdjPoints = pointIntersect.Count(p => p.intersectCount == 3);
                int twoAdjPoints = pointIntersect.Count(p => p.intersectCount == 2);

                if ((threeAdjPoints == 1 && twoAdjPoints == 2) || (threeAdjPoints == 0 && twoAdjPoints >= 2))
                {
                    if (CheckAnyEndPointCovered(contentPoints, tryBoard, moveGroup))
                        return true;
                }
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
                if (CheckAnyEndPointCovered(contentPoints, tryBoard, moveGroup))
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
            if (CheckAnyEndPointCovered(contentPoints, tryBoard, moveGroup))
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
                if (CheckAnyEndPointCovered(contentPoints, tryBoard, moveGroup))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Check any end point covered. 
        /// </summary>
        private static Boolean CheckAnyEndPointCovered(IEnumerable<Point> contentPoints, Board tryBoard, Group moveGroup)
        {
            Content c = moveGroup.Content;
            if (moveGroup.Liberties.Count == 1)
            {
                //suicide move at eye point or atari resolved or suicide move with one empty space
                if (tryBoard.Move != null && tryBoard.GetStoneNeighbours().Count(n => tryBoard[n] == c) < 3 && !SuicideMoveValidWithOneEmptySpaceLeft(tryBoard)) return false;
            }
            else
            {
                if (tryBoard.GetNeighbourGroups(moveGroup).Count(n => n.Liberties.Count <= 2) < 2) return false;
            }

            IEnumerable<dynamic> pointIntersect = GetPointIntersect(tryBoard, contentPoints);
            List<Point> endPoints = pointIntersect.Where(p => p.intersectCount == 1).Select(p => (Point)p.point).ToList();
            return endPoints.Any(q => EndPointCovered(q, tryBoard, moveGroup));
        }

        private static Boolean EndPointCovered(Point endPoint, Board tryBoard, Group moveGroup)
        {
            Content c = moveGroup.Content;
            Boolean oneLiberty = (moveGroup.Liberties.Count == 1);
            if (!tryBoard.PointWithinMiddleArea(endPoint))
                return tryBoard.GetDiagonalNeighbours(endPoint).Where(n => !moveGroup.Points.Contains(n)).Any(n => tryBoard[n] == ((oneLiberty) ? c : Content.Empty));
            else
            {
                if (!oneLiberty) return false;
                if (EyeHelper.IsCovered(tryBoard, endPoint, c.Opposite()))
                    return true;
                return false;
            }
        }


        /*
    15 . . . . . . . . . . . . . . . . . . .
    16 . . . . . . . . . . . . . . . . . . . 
    17 X . . . . . . . . . . . . . . . . . . 
    18 X X . . . . . . . . . . . . . . . . . 
        */
        public static Boolean CornerThreeFormation(Board tryBoard, Group moveGroup)
        {
            Content c = moveGroup.Content;
            if (!KoHelper.KoContentEnabled(c, tryBoard.GameInfo)) return false;
            HashSet<Point> contentPoints = moveGroup.Points;
            if (contentPoints.Count() != 3) return false;
            if (!contentPoints.Any(p => tryBoard.CornerPoint(p)) || contentPoints.Any(p => tryBoard.PointWithinMiddleArea(p))) return false;
            if (MaxLengthOfGrid(moveGroup.Points) != 1) return false;
            //ensure at least two atari targets
            if (tryBoard.AtariTargets.Count <= 1) return false;
            return true;
        }

        public static Boolean PossibleCornerThreeFormation(Board currentBoard, Point p, Content c)
        {
            if (!KoHelper.KoContentEnabled(c.Opposite(), currentBoard.GameInfo)) return false;
            Point corner = currentBoard.GetStoneNeighbours(p).FirstOrDefault(n => currentBoard.CornerPoint(n));
            if (!Convert.ToBoolean(corner.NotEmpty)) return false;
            if (currentBoard.GetStoneNeighbours(corner).Any(n => currentBoard[n] != Content.Empty)) return false;
            if (currentBoard.GetDiagonalNeighbours(p).Any(n => currentBoard.PointWithinMiddleArea(n) && EyeHelper.FindSemiSolidEyes(n, currentBoard, c).Item1))
            {
                foreach (Point q in currentBoard.GetStoneNeighbours(corner))
                {
                    Board b = currentBoard.MakeMoveOnNewBoard(q, c.Opposite());
                    if (b != null && !ImmovableHelper.CheckConnectAndDie(b)) return true;
                }
            }
            return false;
        }


        /*
    15 . . . . . . . . . . . . . . . . . . .
    16 X . . . . . . . . . . . . . . . . . . 
    17 X X . . . . . . . . . . . . . . . . . 
    18 X X X . . . . . . . . . . . . . . . . 
        */
        public static Boolean CornerSixFormation(Board tryBoard, Group moveGroup)
        {
            Content c = moveGroup.Content;
            if (!KoHelper.KoContentEnabled(c, tryBoard.GameInfo)) return false;
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
 16 . . . . . . . . . . . . . . . . . . . 
 17 X X . . . . . . . . . . . . . . . . . 
 18 X X X . . . . . . . . . . . . . . . . 
         */
        public static Boolean CornerFiveFormation(Board tryBoard, Group moveGroup)
        {
            Content c = moveGroup.Content;
            if (!KoHelper.KoContentEnabled(c, tryBoard.GameInfo)) return false;
            HashSet<Point> contentPoints = moveGroup.Points;
            if (contentPoints.Count() != 5) return false;
            if (!contentPoints.Any(p => tryBoard.CornerPoint(p))) return false;
            if (contentPoints.Where(p => tryBoard.PointWithinMiddleArea(p)).Count() != 1) return false;
            IEnumerable<dynamic> pointIntersect = GetPointIntersect(tryBoard, contentPoints);
            if (pointIntersect.Count(p => p.intersectCount == 3) != 1) return false;
            return (MaxLengthOfGrid(moveGroup.Points) == 2);
        }

        /*
    15 . . . . . . . . . . . . . . . . . . .
    16 X . . . . . . . . . . . . . . . . . . 
    17 X . . . . . . . . . . . . . . . . . . 
    18 X X . . . . . . . . . . . . . . . . . 
        */
        public static Boolean BentFourCornerFormation(Board tryBoard, Group moveGroup)
        {
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
            (int xLength, int yLength) = KillerFormationHelper.WithinGrid(points);
            int maxLength = Math.Max(xLength, yLength);
            return maxLength;
        }

        /// <summary>
        /// Point for killer to form killer formation. Order by min of max length of grid then by max of intersection points then by minimum neighbour liberties.
        /// Ordering <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_GuanZiPu_A36_2" />
        /// <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_WuQingYuan_Q30919_2" />
        /// Check for dead formation <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_TianLongTu_Q16902" />
        /// Check for suicidal move by survival <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_TianLongTu_Q17154" />
        /// </summary>
        public static LinkedPoint<Point> GetMaxBindingPoint(Board currentBoard, IEnumerable<Board> killBoards)
        {
            Content c = killBoards.First().MoveGroup.Content;
            List<LinkedPoint<Point>> list = new List<LinkedPoint<Point>>();
            foreach (Board killBoard in killBoards)
            {
                Point p = killBoard.Move.Value;
                List<Point> moveGroup = killBoard.MoveGroup.Points.ToList();
                int maxLengthOfGrid = MaxLengthOfGrid(moveGroup);
                int maxIntersect = moveGroup.Max(q => killBoard.GetStoneNeighbours(q).Intersect(moveGroup).Count());
                List<Group> neighbourGroups = killBoard.GetGroupsFromStoneNeighbours(p, c).OrderBy(n => n.Liberties.Count).ToList();
                int minNeighbourLiberties = (neighbourGroups.Count == 0) ? 0 : neighbourGroups.First().Liberties.Count;
                int minNeighbourPointCount = (neighbourGroups.Count == 0) ? 0 : neighbourGroups.First().Points.Count;
                list.Add(new LinkedPoint<Point>(p, new { maxLengthOfGrid, maxIntersect, minNeighbourLiberties, minNeighbourPointCount, killBoard }));
            }
            //order by grid length then by max of intersection then by minimum neighbour liberties then by minimum neighbour point count
            list = list.OrderBy(m => ((dynamic)m.CheckMove).maxLengthOfGrid).OrderByDescending(m => ((dynamic)m.CheckMove).maxIntersect).OrderBy(m => ((dynamic)m.CheckMove).minNeighbourLiberties).OrderBy(m => ((dynamic)m.CheckMove).minNeighbourPointCount).ToList();

            //check for dead formation
            foreach (LinkedPoint<Point> p in list)
            {
                //check for suicidal move by survival
                if (ImmovableHelper.IsSuicidalMove(p.Move, c.Opposite(), currentBoard).Item1) continue;
                Board killBoard = ((dynamic)p.CheckMove).killBoard;
                Group killerGroup = GroupHelper.GetKillerGroupFromCache(currentBoard, p.Move, c.Opposite());
                if (DeadFormationInBothAlive(killBoard, killerGroup))
                    return p;
            }
            return list.First();
        }

    }
}
