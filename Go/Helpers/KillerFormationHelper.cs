using System;
using System.Collections.Generic;
using System.Linq;

namespace Go
{
    public class KillerFormationHelper
    {

        /// <summary>
        /// Formations that are essentially dead and do not require a pass move to test for both alive.
        /// </summary>
        public static Boolean DeadFormationInBothAlive(Board board, Group killerGroup, int libertyCount = 2, int requiredCount = 1)
        {
            List<Point> contentPoints = killerGroup.Points.Where(t => board[t] == killerGroup.Content).ToList();
            List<Point> emptyPoints = killerGroup.Points.Where(t => board[t] == Content.Empty).ToList();
            if (emptyPoints.Count != libertyCount) return false;
            return PreDeadFormation(board, killerGroup, contentPoints, emptyPoints, requiredCount);
        }

        public static Boolean PreDeadFormation(Board board, Group killerGroup, List<Point> contentPoints, List<Point> emptyPoints, int requiredCount = 1)
        {
            int contentCount = contentPoints.Count;
            Content c = killerGroup.Content;
            if (contentCount == 3)
            {
                if (TryKillFormation(board, c, emptyPoints, new List<Func<Board, Group, Boolean>>() { OneByThreeFormation, BoxFormation, CrowbarEdgeFormation, TwoByTwoFormation, BentFourCornerFormation }, requiredCount))
                    return true;
            }
            else if (contentCount == 4)
            {
                if (TryKillFormation(board, c, emptyPoints, new List<Func<Board, Group, Boolean>>() { KnifeFiveFormation, OneByFourSideFormation, TSideFormation, ThreeByTwoSideFormation }, requiredCount))
                    return true;
            }
            else if (contentCount == 5)
            {
                if (TryKillFormation(board, c, emptyPoints, new List<Func<Board, Group, Boolean>>() { FlowerSixFormation, TwoByFourSideFormation, CornerSixFormation }, requiredCount))
                    return true;
            }
            else if (contentCount == 6)
            {
                if (TryKillFormation(board, c, emptyPoints, new List<Func<Board, Group, Boolean>>() { FlowerSevenSideFormation }, requiredCount))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Make move at each of the empty points to test if formation created.
        /// </summary>
        public static Boolean TryKillFormation(Board board, Content c, List<Point> emptyPoints, List<Func<Board, Group, Boolean>> functions, int requiredCount = 1)
        {
            int count = 0;
            foreach (Point emptyPoint in emptyPoints)
            {
                Board b = board.MakeMoveOnNewBoard(emptyPoint, c);
                if (b == null) return false;
                Group group = b.GetGroupAt(emptyPoint);
                if (functions.Any(func => func(b, group)))
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
        /// Check if real eye found in neighbour groups <see cref="UnitTestProject.KillerFormationTest.KillerFormationTest_Scenario5dan27" />
        /// Check covered eye at non-killable group <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_AncientJapanese_B6" />
        /// </summary>
        public static Boolean SuicidalKillerFormations(Board tryBoard, Board currentBoard, Board capturedBoard = null)
        {
            Point move = tryBoard.Move.Value;
            Content c = tryBoard.MoveGroup.Content;

            //check liberties of move group
            if (tryBoard.MoveGroupLiberties > 2) return false;

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
            if (tryBoard.MoveGroupLiberties == 1 && capturedBoard != null)
            {
                Point? liberty = capturedBoard.Move;
                Board tryLinkBoard = currentBoard.MakeMoveOnNewBoard(liberty.Value, c);
                if (tryLinkBoard != null && IsLinkToExternalGroup(tryBoard, currentBoard, tryLinkBoard))
                    return false;
            }
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
            if (tryBoard.MoveGroup.Points.Count <= 2)
            {
                Group killerGroup = GroupHelper.GetKillerGroupFromCache(b, move, c.Opposite());
                if (killerGroup != null && killerGroup.Points.Count <= 2 && !EyeHelper.FindRealEyeWithinEmptySpace(b, killerGroup))
                    return false;
            }

            //check for corner six
            if (KillerFormationHelper.CornerSixFormation(tryBoard, tryBoard.MoveGroup))
                return false;

            //corner ko move
            if (CheckCornerKoMoveForRealEye(tryBoard))
                return false;

            Group moveKillerGroup = GroupHelper.GetKillerGroupFromCache(b, move, c.Opposite());
            if (moveKillerGroup == null) moveKillerGroup = tryBoard.MoveGroup;

            //get all killer groups except move killer group
            List<Group> killerGroups = GroupHelper.GetKillerGroups(b, c.Opposite());
            List<Group> neighbourGroups = b.GetNeighbourGroups(moveKillerGroup);

            if (neighbourGroups.Any(group => WallHelper.IsNonKillableGroup(b, group)))
                return true;

            foreach (Group killerGroup in killerGroups.Where(group => group != moveKillerGroup))
            {
                List<Group> neighbourKillerGroups = b.GetNeighbourGroups(killerGroup);
                if (!neighbourKillerGroups.Intersect(neighbourGroups).Any()) continue;
                //real eye with one neighbour group only
                if (neighbourKillerGroups.Count == 1)
                    return true;
                //find real eye
                if (EyeHelper.FindRealEyeWithinEmptySpace(b, killerGroup, EyeType.SemiSolidEye) && WallHelper.StrongNeighbourGroups(b, neighbourKillerGroups))
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
        /// One-by-three formation <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_Corner_A8" />
        /// Crowbar edge formation <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanGo_Q6710" />
        /// <see cref="UnitTestProject.KillerFormationTest.KillerFormationTest_Scenario_TianLongTu_Q16738" />
        /// Two-by-two formation <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanQiJing_A40" />
        /// <see cref="UnitTestProject.KillerFormationTest.KillerFormationTest_Scenario_TianLongTu_Q16738_2" />
        /// Bent four corner formation <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_Nie20" />
        /// Knife five formation <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_Corner_A113" />
        /// One-by-four side formation <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_Side_B32" />
        /// T side formation <see cref="UnitTestProject.KillerFormationTest.KillerFormationTest_Scenario_WuQingYuan_Q31471" />
        /// Three-by-two formation <see cref="UnitTestProject.KillerFormationTest.KillerFormationTest_Scenario_Corner_A132" />
        /// Two-by-four side formation <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WuQingYuan_Q31682" />
        /// Corner six formation <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanQiJing_A38" />
        /// Flower six formation <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_TianLongTu_Q16859" />
        /// Flower seven side formation <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanGo_B3" />
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
                    return EyeHelper.SuicideAtCoveredEye(capturedBoard, tryBoard);

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

                if (SuicideMoveValidWithOneEmptySpaceLeft(tryBoard, capturedBoard))
                    return true;

                if (KillerFormationHelper.CornerThreeFormation(tryBoard, tryBoard.MoveGroup))
                    return true;
            }
            //check kill group extension
            else if (CheckRedundantKillGroupExtension(tryBoard, currentBoard, capturedBoard))
            {
                if (moveCount == 4) return false;
                if (KillerFormationHelper.GridDimensionChanged(LinkHelper.GetPreviousMoveGroup(currentBoard, tryBoard).First().Points, tryBoard.MoveGroup.Points))
                    return false;
            }

            if (moveCount == 4)
            {
                //one-by-three formation
                if (KillerFormationHelper.OneByThreeFormation(tryBoard, tryBoard.MoveGroup)) return true;
                //box formation
                if (KillerFormationHelper.BoxFormation(tryBoard, tryBoard.MoveGroup)) return true;
                //crowbar edge formation
                if (KillerFormationHelper.CrowbarEdgeFormation(tryBoard, tryBoard.MoveGroup)) return true;
                //two-by-two formation
                if (KillerFormationHelper.TwoByTwoFormation(tryBoard, tryBoard.MoveGroup)) return true;
                //bent four corner formation
                if (KillerFormationHelper.BentFourCornerFormation(tryBoard, tryBoard.MoveGroup)) return true;
            }

            else if (moveCount == 5)
            {
                //knife five formation
                if (KillerFormationHelper.KnifeFiveFormation(tryBoard, tryBoard.MoveGroup))
                    return true;

                //one-by-four side formation
                if (KillerFormationHelper.OneByFourSideFormation(tryBoard, tryBoard.MoveGroup))
                    return true;

                //T side formation
                if (KillerFormationHelper.TSideFormation(tryBoard, tryBoard.MoveGroup))
                    return true;

                //three-by-two side formation
                if (KillerFormationHelper.ThreeByTwoSideFormation(tryBoard, tryBoard.MoveGroup))
                    return true;
            }
            else if (moveCount == 6)
            {
                //flower six formation
                if (KillerFormationHelper.FlowerSixFormation(tryBoard, tryBoard.MoveGroup))
                    return true;

                //two-by-four side formation
                if (KillerFormationHelper.TwoByFourSideFormation(tryBoard, tryBoard.MoveGroup))
                    return true;

                //check for corner six formation
                if (KillerFormationHelper.CornerSixFormation(tryBoard, tryBoard.MoveGroup))
                    return true;
            }
            else if (moveCount == 7)
            {
                //flower seven side formation
                if (KillerFormationHelper.FlowerSevenSideFormation(tryBoard, tryBoard.MoveGroup))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Redundant extension of kill group.
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_Corner_A8" />
        /// </summary>
        private static Boolean CheckRedundantKillGroupExtension(Board tryBoard, Board currentBoard, Board capturedBoard = null)
        {
            if (tryBoard.MoveGroupLiberties != 1 || tryBoard.AtariTargets.Count > 0) return false;
            if (LinkHelper.GetPreviousMoveGroup(currentBoard, tryBoard).Count > 1) return false;
            if (!SuicideMoveValidWithOneEmptySpaceLeft(tryBoard, capturedBoard))
                return true;
            return false;
        }

        /// <summary>
        /// Suicide move with one empty space in killer group. If two-point move, ensure is covered eye. If three-point move, ensure move is next to empty point.
        /// Covered eye <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WuQingYuan_Q31563_2" />
        /// Move group with three points <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario1kyu29" />
        /// Move group binding <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanQiJing_Weiqi101_B19_2" />
        /// </summary>
        public static Boolean SuicideMoveValidWithOneEmptySpaceLeft(Board tryBoard, Board capturedBoard)
        {
            int moveCount = tryBoard.MoveGroup.Points.Count;
            Point move = tryBoard.Move.Value;
            Content c = tryBoard[move];
            Group killerGroup = GroupHelper.GetKillerGroupFromCache(tryBoard, move, c.Opposite());
            //killer group contains only one more empty space
            if (killerGroup != null && killerGroup.Points.Count == moveCount + 1)
            {
                //covered eye
                if (moveCount == 2 && EyeHelper.SuicideAtCoveredEye(capturedBoard, tryBoard))
                    return true;

                //whole survival group dying
                if (tryBoard.IsAtariMove && tryBoard.GetNeighbourGroups().Count == 1) return true;

                //get empty point
                Point p = killerGroup.Points.FirstOrDefault(k => tryBoard[k] == Content.Empty);
                //move is next to empty point
                if (tryBoard.GetStoneNeighbours(p).Any(n => n.Equals(move)))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Ensure link is connected to both stones from previous move group and to external group.
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_TianLongTu_Q16520_2" />
        /// Lost group not more than three points <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WuQingYuan_Q31682" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_TianLongTu_Q17154" />
        /// Connect three or more groups <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanGo_B3" />
        /// No lost groups <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanQiJing_Weiqi101_18402_2" />
        /// Check connect and die <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WindAndTime_Q30403" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_GuanZiPu_A4Q11_101Weiqi_2" />
        /// </summary>
        private static Boolean IsLinkToExternalGroup(Board tryBoard, Board currentBoard, Board tryLinkBoard)
        {
            Point move = tryBoard.Move.Value;
            Content c = tryBoard.MoveGroup.Content;
            if (tryLinkBoard.MoveGroupLiberties == 1)
                return false;
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
                //lost group not more than three points
                if (lostGroups.Count == 1 && lostGroups.First().Points.Count <= 3)
                {
                    if (lostGroups.First().Points.Count == 3 && tryLinkBoard.MoveGroupLiberties <= 2)
                        return false;
                    return true;
                }
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
            return (contentPoints.Any(p => tryBoard.GetStoneNeighbours(p).Intersect(contentPoints).Count() == 3));
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
                Board capturedBoard = ImmovableHelper.CaptureSuicideGroup(tryBoard);
                if (capturedBoard == null) return false;
                foreach (Point p in tryBoard.MoveGroup.Points)
                {
                    (Boolean isSuicidal, Board b) = ImmovableHelper.IsSuicidalMove(p, c, capturedBoard);
                    if (isSuicidal) continue;
                    if (b != null && b.AtariTargets.Count == 1 && b.AtariTargets.First().Points.Count > 1)
                        return true;
                }
                List<Point> contentPoints = moveGroup.Points.Where(t => tryBoard[t] == c).ToList();
                if (CheckBothSideFormationDiagonal(contentPoints, tryBoard, moveGroup))
                    return true;
            }
            return false;
        }

        public static Boolean TwoByTwoFormation(Board tryBoard, IEnumerable<Point> killerPoints, Content c)
        {
            List<Point> contentPoints = killerPoints.Where(t => tryBoard[t] == c).ToList();
            if (contentPoints.Count() != 4) return false;
            if (contentPoints.Count(p => tryBoard.GetStoneNeighbours(p).Intersect(contentPoints).Count() == 2) == 2)
            {
                if (contentPoints.GroupBy(p => p.x).Count() == 2 || contentPoints.GroupBy(p => p.y).Count() == 2)
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
            if (contentPoints.Count(p => tryBoard.GetStoneNeighbours(p).Intersect(contentPoints).Count() == 2) == 2)
            {
                if (contentPoints.GroupBy(p => p.x).Count() == 3 || contentPoints.GroupBy(p => p.y).Count() == 3)
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
                if (tryBoard.GetNeighbourGroups().Count <= 1) return false;
                if (!LinkHelper.GetGroupDiagonals(tryBoard, tryBoard.MoveGroup).Any(d => tryBoard[d.Move] == moveGroup.Content)) return false;
                if (moveGroup.Points.Count(p => !tryBoard.PointWithinMiddleArea(p)) >= 2)
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
            return (xLength == 0 || yLength == 0);
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
            if (WithinThreeByTwoGrid(moveGroup))
            {
                if (contentPoints.Count(p => tryBoard.GetStoneNeighbours(p).Intersect(contentPoints).Count() == 3) == 1)
                    return true;
            }
            //star formation
            if (contentPoints.Count(p => tryBoard.GetStoneNeighbours(p).Intersect(contentPoints).Count() == 4) == 1)
                return true;
            return false;
        }


        /*
 15 . . . . . . X . . . . . . . . . . . .
 16 . . . . . X X X . . . . . . . . . . . 
 17 . . . . . X X . . . . . . . . . . . . 
 18 . . . . . . . . . . . . . . . . . . . 
         */
        public static Boolean FlowerSixFormation(Board tryBoard, Group moveGroup)
        {
            Content c = moveGroup.Content;
            HashSet<Point> contentPoints = moveGroup.Points;
            if (contentPoints.Count() != 6) return false;
            if (contentPoints.Count(p => tryBoard.GetStoneNeighbours(p).Intersect(contentPoints).Count() == 4) == 1)
            {
                if (contentPoints.Count(p => tryBoard.GetStoneNeighbours(p).Intersect(contentPoints).Count() == 2) == 3)
                    return true;
            }
            return false;
        }

        /*
 14 . X . . . . . . . . . . . . . . . . .
 15 X X X . . . . . . . . . . . . . . . .
 16 X X . . . . . . . . . . . . . . . . . 
 17 X . . . . . . . . . . . . . . . . . . 
 18 . . . . . . . . . . . . . . . . . . . 
         */
        public static Boolean FlowerSevenSideFormation(Board tryBoard, Group moveGroup)
        {
            Content c = moveGroup.Content;
            HashSet<Point> contentPoints = moveGroup.Points;
            if (contentPoints.Count() != 7) return false;
            if (contentPoints.Count(p => tryBoard.GetStoneNeighbours(p).Intersect(contentPoints).Count() == 4) == 1)
            {
                Boolean isEdge = (contentPoints.Count(p => !tryBoard.PointWithinMiddleArea(p)) == 3);
                if (isEdge && contentPoints.Count(p => tryBoard.GetStoneNeighbours(p).Intersect(contentPoints).Count() == 3) == 1 && contentPoints.Count(p => tryBoard.GetStoneNeighbours(p).Intersect(contentPoints).Count() == 2) == 2)
                    return true;
            }
            return false;
        }

        /*
 15 . . . . . . . . . . . . . . . . . . .
 16 . . . . . . . . . . . . . . . . . . . 
 17 . . . . X . . . . . . . . . . . . . . 
 18 . . X X X X . . . . . . . . . . . . . 
         */
        public static Boolean OneByFourSideFormation(Board tryBoard, Group moveGroup)
        {
            Content c = moveGroup.Content;
            if (tryBoard.GetStoneNeighbours().Any(n => tryBoard[n] != c)) return false;
            HashSet<Point> contentPoints = moveGroup.Points;
            if (contentPoints.Count() != 5) return false;
            if (contentPoints.Count(p => tryBoard.GetStoneNeighbours(p).Intersect(contentPoints).Count() == 3) == 1)
            {
                List<Point> middlePoint = contentPoints.Where(p => tryBoard.PointWithinMiddleArea(p)).ToList();
                if (middlePoint.Count != 1) return false;
                //get end point at longer end
                List<Point> endPoints = contentPoints.Except(middlePoint).Where(p => tryBoard.GetStoneNeighbours(p).Intersect(contentPoints).Count() == 1).ToList();
                endPoints.RemoveAll(p => tryBoard.GetDiagonalNeighbours(p).Contains(middlePoint.First()));
                if (endPoints.Any(p => tryBoard.CornerPoint(p))) return false;
                if (endPoints.Count != 1) return false;
                //check diagonal for end point at longer end
                if (CheckSideFormationDiagonal(endPoints.First(), tryBoard, moveGroup))
                    return true;
            }
            return false;
        }


        /*
 15 . . . . . . . . . . . . . . . . . . .
 16 . . . . X . . . . . . . . . . . . . . 
 17 . . . . X . . . . . . . . . . . . . . 
 18 . . . X X X . . . . . . . . . . . . . 
         */
        public static Boolean TSideFormation(Board tryBoard, Group moveGroup)
        {
            Content c = moveGroup.Content;
            if (tryBoard.GetStoneNeighbours().Any(n => tryBoard[n] != c)) return false;
            HashSet<Point> contentPoints = moveGroup.Points;
            if (contentPoints.Count() != 5) return false;
            if (contentPoints.Count(p => tryBoard.GetStoneNeighbours(p).Intersect(contentPoints).Count() == 3) == 1)
            {
                List<Point> middlePoint = contentPoints.Where(p => tryBoard.PointWithinMiddleArea(p)).ToList();
                if (middlePoint.Count != 2) return false;

                if (CheckBothSideFormationDiagonal(contentPoints, tryBoard, moveGroup))
                    return true;
            }
            return false;
        }

        /*
    15 . . . . . . . . . . . . . . . . . . .
    16 . . . . . . . . . . . . . . . . . . . 
    17 . . . X X . . . . . . . . . . . . . . 
    18 . . X X X X . . . . . . . . . . . . . 
            */
        public static Boolean TwoByFourSideFormation(Board tryBoard, Group moveGroup)
        {
            Content c = moveGroup.Content;
            if (tryBoard.GetStoneNeighbours().Any(n => tryBoard[n] != c)) return false;
            HashSet<Point> contentPoints = moveGroup.Points;
            if (contentPoints.Count() != 6) return false;
            if (contentPoints.Count(p => tryBoard.GetStoneNeighbours(p).Intersect(contentPoints).Count() == 3) == 2)
            {
                if (contentPoints.Count(p => !tryBoard.PointWithinMiddleArea(p)) != 4) return false;
                if (CheckBothSideFormationDiagonal(contentPoints, tryBoard, moveGroup))
                    return true;
            }
            return false;
        }

        /*
    15 . . . . . . . . . . . . . . . . . . .
    16 . . . . . . . . . . . . . . . . . . . 
    17 X X X . . . . . . . . . . . . . . . . 
    18 . . X X . . . . . . . . . . . . . . . 
            */
        public static Boolean ThreeByTwoSideFormation(Board tryBoard, Group moveGroup)
        {
            Content c = moveGroup.Content;
            HashSet<Point> contentPoints = moveGroup.Points;
            if (contentPoints.Count() != 5) return false;
            if (contentPoints.Count(p => tryBoard.GetStoneNeighbours(p).Intersect(contentPoints).Count() == 2) == 3)
            {
                if ((contentPoints.GroupBy(p => p.x).Count() == 4 && contentPoints.GroupBy(p => p.y).Count() == 2) || (contentPoints.GroupBy(p => p.x).Count() == 2 && contentPoints.GroupBy(p => p.y).Count() == 4))
                {
                    Boolean edge = (contentPoints.Count(p => p.x == 0) == 2 || contentPoints.Count(p => p.x == tryBoard.SizeX - 1) == 2 || contentPoints.Count(p => p.y == 0) == 2 || contentPoints.Count(p => p.y == tryBoard.SizeY - 1) == 2);
                    if (!edge) return false;
                    if (CheckBothSideFormationDiagonal(contentPoints, tryBoard, moveGroup))
                        return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Check diagonals for side formations 
        /// </summary>
        private static Boolean CheckBothSideFormationDiagonal(IEnumerable<Point> contentPoints, Board tryBoard, Group moveGroup)
        {
            List<Point> endPoints = contentPoints.Where(p => tryBoard.GetStoneNeighbours(p).Where(n => !tryBoard.PointWithinMiddleArea(n)).Intersect(contentPoints).Count() == 1).ToList();
            return endPoints.Any(q => CheckSideFormationDiagonal(q, tryBoard, moveGroup));
        }

        private static Boolean CheckSideFormationDiagonal(Point endPoint, Board tryBoard, Group moveGroup)
        {
            Content c = moveGroup.Content;
            Boolean oneLiberty = (moveGroup.Liberties.Count == 1);
            return tryBoard.GetDiagonalNeighbours(endPoint).Where(n => !moveGroup.Points.Contains(n)).Any(n => tryBoard[n] == ((oneLiberty) ? c : Content.Empty));
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
            if (contentPoints.Count(p => tryBoard.GetStoneNeighbours(p).Intersect(contentPoints).Count() == 3) != 2) return false;
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
            if (contentPoints.Count(p => tryBoard.GetStoneNeighbours(p).Intersect(contentPoints).Count() == 3) != 1) return false;
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
            return true;
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
            List<Group> killerGroups = GroupHelper.GetKillerGroups(currentBoard, c.Opposite());
            Group killerGroup = killerGroups.First();
            foreach (LinkedPoint<Point> p in list)
            {
                Board killBoard = ((dynamic)p.CheckMove).killBoard;
                if (DeadFormationInBothAlive(killBoard, killerGroup))
                {
                    //check for suicidal move by survival
                    if (ImmovableHelper.IsSuicidalMove(p.Move, c.Opposite(), currentBoard).Item1) continue;
                    return p;
                }
            }
            return list.First();
        }

    }
}
