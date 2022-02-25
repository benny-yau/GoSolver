using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Go
{
    public class KillerFormationHelper
    {

        /// <summary>
        /// Formations that are essentially dead and do not require a pass move to test for both alive.
        /// </summary>
        public static Boolean DeadFormationInBothAlive(Board board, Group killerGroup, List<Point> contentPoints, List<Point> emptyPoints)
        {
            if (emptyPoints.Count != 2) return false;
            int contentCount = contentPoints.Count;
            return PreDeadFormation(board, killerGroup, contentPoints, emptyPoints);
        }

        public static Boolean PreDeadFormation(Board board, Group killerGroup, List<Point> contentPoints, List<Point> emptyPoints)
        {
            int contentCount = contentPoints.Count;
            if (contentCount == 3)
            {
                if (TryKillFormation(board, killerGroup, emptyPoints, new List<Func<Board, Group, Boolean>>() { OneByThreeFormation, BoxFormation, CrowbarEdgeFormation, TwoByTwoFormation, BentFourCornerFormation }))
                    return true;
            }
            else if (contentCount == 4)
            {
                if (TryKillFormation(board, killerGroup, emptyPoints, new List<Func<Board, Group, Boolean>>() { KnifeFiveFormation, OneByFourSideFormation, TSideFormation }))
                    return true;
            }
            else if (contentCount == 5)
            {
                if (TryKillFormation(board, killerGroup, emptyPoints, new List<Func<Board, Group, Boolean>>() { FlowerSixFormation, TwoByFourSideFormation, CornerSixFormation }))
                    return true;
            }
            else if (contentCount == 6)
            {
                if (TryKillFormation(board, killerGroup, emptyPoints, new List<Func<Board, Group, Boolean>>() { FlowerSevenSideFormation }))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Make move at each of the empty points to test if formation created.
        /// </summary>
        public static Boolean TryKillFormation(Board board, Group killerGroup, List<Point> emptyPoints, List<Func<Board, Group, Boolean>> functions)
        {
            foreach (Point emptyPoint in emptyPoints)
            {
                Board b = board.MakeMoveOnNewBoard(emptyPoint, killerGroup.Content);
                if (b == null) return false;
                Group group = b.GetGroupAt(emptyPoint);
                if (functions.Any(func => func(b, group)))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Suicidal killer formations within survival group without any real eye.
        /// Check if real eye found in neighbour groups <see cref="UnitTestProject.KillerFormationTest.KillerFormationTest_Scenario5dan27" />
        /// </summary>
        public static Boolean SuicidalKillerFormations(Board tryBoard, Board currentBoard = null, Board capturedBoard = null)
        {
            //find killer formation
            if (!FindSuicidalKillerFormation(tryBoard, currentBoard, capturedBoard)) return false;
            if (tryBoard.MoveGroup.Points.Count <= 2 || (tryBoard.MoveGroup.Points.Count == 6 && KillerFormationHelper.CornerSixFormation(tryBoard, tryBoard.MoveGroup))) return true;
            //check if neighbour group is non-killable
            if (tryBoard.GetNeighbourGroups(tryBoard.MoveGroup).Any(n => WallHelper.IsNonKillableGroup(tryBoard, n)))
                return false;
            //check if real eye found in neighbour groups
            return !CheckRealEyeInNeighbourGroups(tryBoard, tryBoard.Move.Value);
        }

        public static Boolean CheckRealEyeInNeighbourGroups(Board tryBoard, Point move)
        {
            Content c = tryBoard[move];
            Group moveKillerGroup = BothAliveHelper.GetKillerGroupFromCache(tryBoard, move, c.Opposite());
            if (moveKillerGroup == null) moveKillerGroup = tryBoard.MoveGroup;
            List<Group> killerGroups = BothAliveHelper.GetCorneredKillerGroup(tryBoard, c.Opposite(), false);
            killerGroups = killerGroups.Except(new List<Group> { moveKillerGroup }).ToList();
            List<Group> neighbourGroups = tryBoard.GetNeighbourGroups(moveKillerGroup);
            foreach (Group killerGroup in killerGroups)
            {
                List<Group> neighbourKillerGroups = tryBoard.GetNeighbourGroups(killerGroup);
                if (!neighbourKillerGroups.Intersect(neighbourGroups).Any()) continue;
                if (neighbourKillerGroups.Count == 1)
                    return true;
                if (killerGroup.Points.Count <= 3 && EyeHelper.FindRealEyeWithinEmptySpace(tryBoard, killerGroup, EyeType.SemiSolidEye))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Two-point move <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanGo_A48" />
        /// Covered eye <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanGo_B3_3" />
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
        /// Two-by-four side formation <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WuQingYuan_Q31682" />
        /// Corner six formation <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanQiJing_A38" />
        /// Flower six formation <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_TianLongTu_Q16859" />
        /// Flower seven side formation <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanGo_B3" />
        /// </summary>
        private static Boolean FindSuicidalKillerFormation(Board tryBoard, Board currentBoard = null, Board capturedBoard = null)
        {
            Point move = tryBoard.Move.Value;
            Content c = tryBoard.MoveGroup.Content;
            if (tryBoard.MoveGroupLiberties > 2 || tryBoard.MoveGroup.Points.Count <= 4 && tryBoard.MoveGroupLiberties > 1)
                return false;

            int moveCount = tryBoard.MoveGroup.Points.Count;
            if (moveCount == 2)
            {
                //two-point move
                if (tryBoard.GetStoneNeighbours().Any(p => tryBoard[p] == Content.Empty))
                    return true;
                //covered eye
                if (EyeHelper.SuicideAtCoveredEye(capturedBoard, tryBoard))
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
            }
            else if (moveCount == 4)
            {
                //check kill group extension
                if (CheckRedundantKillGroupExtension(tryBoard, currentBoard, capturedBoard))
                    return false;
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
            else
            {
                //check kill group extension
                if (CheckRedundantKillGroupExtension(tryBoard, currentBoard, capturedBoard))
                {
                    List<Group> groups = LinkHelper.GetPreviousMoveGroup(currentBoard, tryBoard);
                    if (KillerFormationHelper.GridDimensionChanged(groups.First().Points, tryBoard.MoveGroup.Points))
                        return false;
                }

                if (moveCount == 5)
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
            }
            return false;
        }

        /// <summary>
        /// Redundant extension of kill group.
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_Corner_A8" />
        /// </summary>
        private static Boolean CheckRedundantKillGroupExtension(Board tryBoard, Board currentBoard = null, Board capturedBoard = null)
        {
            if (tryBoard.AtariTargets.Count == 0 && LinkHelper.GetPreviousMoveGroup(currentBoard, tryBoard).Count == 1)
            {
                if (!SuicideMoveValidWithOneEmptySpaceLeft(tryBoard, capturedBoard))
                    return true;
            }
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
            if (capturedBoard == null) return false;
            int moveCount = tryBoard.MoveGroup.Points.Count;
            Point move = tryBoard.Move.Value;
            Content c = tryBoard[move];
            Group killerGroup = BothAliveHelper.GetKillerGroupFromCache(tryBoard, move, c.Opposite());
            //killer group contains only one more empty space
            if (killerGroup != null && killerGroup.Points.Count == moveCount + 1)
            {
                //covered eye
                if (moveCount == 2 && EyeHelper.SuicideAtCoveredEye(capturedBoard, tryBoard))
                    return true;

                //whole survival group dying
                if (tryBoard.IsAtariMove && tryBoard.GetNeighbourGroups(tryBoard.MoveGroup).Count == 1) return true;

                //get empty point
                Point p = killerGroup.Points.FirstOrDefault(k => tryBoard[k] == Content.Empty);
                //move is next to empty point
                if (tryBoard.GetStoneNeighbours(p.x, p.y).Any(n => n.Equals(move)))
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
        public static Boolean StraightThreeFormation(Board tryBoard, Group killerGroup)
        {
            List<Point> contentPoints = killerGroup.Points.Where(t => tryBoard[t] == killerGroup.Content).ToList();
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
        public static Boolean BentThreeFormation(Board tryBoard, Group killerGroup)
        {
            List<Point> contentPoints = killerGroup.Points.Where(t => tryBoard[t] == killerGroup.Content).ToList();
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
        public static Boolean OneByThreeFormation(Board tryBoard, Group killerGroup)
        {
            List<Point> contentPoints = killerGroup.Points.Where(t => tryBoard[t] == killerGroup.Content).ToList();
            if (contentPoints.Count() != 4) return false;
            return (contentPoints.Any(p => tryBoard.GetStoneNeighbours(p.x, p.y).Intersect(contentPoints).Count() == 3));
        }

        /*
 15 . . . . . . . . . . . . . . . . . . .
 16 . . . . . X X . . . . . . . . . . . . 
 17 . . . . . . X X . . . . . . . . . . . 
 18 . . . . . . . . . . . . . . . . . . . 
         */
        public static Boolean TwoByTwoFormation(Board tryBoard, Group killerGroup)
        {
            if (TwoByTwoFormation(tryBoard, killerGroup.Points, killerGroup.Content))
            {
                Content c = killerGroup.Content;
                Board capturedBoard = ImmovableHelper.CaptureSuicideGroup(tryBoard);
                if (capturedBoard == null) return false;
                foreach (Point p in tryBoard.MoveGroup.Points)
                {
                    (Boolean isSuicidal, Board b) = ImmovableHelper.IsSuicidalMove(p, killerGroup.Content, capturedBoard);
                    if (isSuicidal) continue;
                    if (b != null && b.AtariTargets.Count == 1 && b.AtariTargets.First().Points.Count > 1)
                        return true;
                }
                List<Point> contentPoints = killerGroup.Points.Where(t => tryBoard[t] == c).ToList();
                if (CheckSideFormationDiagonal(contentPoints, tryBoard, killerGroup))
                    return true;
            }
            return false;
        }

        public static Boolean TwoByTwoFormation(Board tryBoard, IEnumerable<Point> killerPoints, Content c)
        {
            List<Point> contentPoints = killerPoints.Where(t => tryBoard[t] == c).ToList();
            if (contentPoints.Count() != 4) return false;
            if (contentPoints.Count(p => tryBoard.GetStoneNeighbours(p.x, p.y).Intersect(contentPoints).Count() == 2) == 2)
            {
                if (contentPoints.GroupBy(p => p.x).All(group => group.Count() == 2) || contentPoints.GroupBy(p => p.y).All(group => group.Count() == 2))
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
        public static Boolean BoxFormation(Board tryBoard, Group killerGroup)
        {
            List<Point> contentPoints = killerGroup.Points.Where(t => tryBoard[t] == killerGroup.Content).ToList();
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
        public static Boolean CrowbarFormation(Board tryBoard, Group killerGroup)
        {
            List<Point> contentPoints = killerGroup.Points.Where(t => tryBoard[t] == killerGroup.Content).ToList();
            if (contentPoints.Count() != 4) return false;
            if (contentPoints.Count(p => tryBoard.GetStoneNeighbours(p.x, p.y).Intersect(contentPoints).Count() == 2) == 2)
            {
                if (contentPoints.GroupBy(p => p.x).Any(group => group.Count() == 3) || contentPoints.GroupBy(p => p.y).Any(group => group.Count() == 3))
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
        public static Boolean CrowbarEyeFormation(Board tryBoard, Group killerGroup)
        {
            if (CrowbarFormation(tryBoard, killerGroup))
            {
                List<Point> contentPoints = killerGroup.Points.Where(t => tryBoard[t] == killerGroup.Content).ToList();
                Group group = tryBoard.GetGroupAt(contentPoints.First());
                Point eyePoint = group.Liberties.FirstOrDefault(p => tryBoard.GetStoneNeighbours(p.x, p.y).Intersect(contentPoints).Count() == 2);
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
        public static Boolean CrowbarEdgeFormation(Board tryBoard, Group killerGroup)
        {
            if (CrowbarFormation(tryBoard, killerGroup))
            {
                if (tryBoard.GetNeighbourGroups(tryBoard.MoveGroup).Count <= 1) return false;
                List<Point> contentPoints = killerGroup.Points.Where(t => tryBoard[t] == killerGroup.Content).ToList();
                if (contentPoints.Count(p => !tryBoard.PointWithinMiddleArea(p)) >= 2)
                    return true;
            }
            return false;
        }


        /*
 15 . . . . . . . . . . . . . . . X . . .
 16 . . . . . X X X . . . . . . X X X . . 
 17 . . . . . X X . . . . . . . . X . . . 
 18 . . . . . . . . . . . . . . . . . . . 
         */
        public static Boolean KnifeFiveFormation(Board tryBoard, Group killerGroup)
        {
            Content c = killerGroup.Content;
            List<Point> contentPoints = killerGroup.Points.Where(t => tryBoard[t] == c).ToList();
            if (contentPoints.Count() != 5) return false;
            //knife five formation
            if (WithinThreeByTwoGrid(killerGroup))
            {
                if (contentPoints.Count(p => tryBoard.GetStoneNeighbours(p.x, p.y).Intersect(contentPoints).Count() == 3) == 1)
                    return true;
            }
            //star formation
            if (contentPoints.Count(p => tryBoard.GetStoneNeighbours(p.x, p.y).Intersect(contentPoints).Count() == 4) == 1)
                return true;
            return false;
        }


        /*
 15 . . . . . . X . . . . . . . . . . . .
 16 . . . . . X X X . . . . . . . . . . . 
 17 . . . . . X X . . . . . . . . . . . . 
 18 . . . . . . . . . . . . . . . . . . . 
         */
        public static Boolean FlowerSixFormation(Board tryBoard, Group killerGroup)
        {
            Content c = killerGroup.Content;
            List<Point> contentPoints = killerGroup.Points.Where(t => tryBoard[t] == c).ToList();
            if (contentPoints.Count() != 6) return false;
            if (contentPoints.Count(p => tryBoard.GetStoneNeighbours(p.x, p.y).Intersect(contentPoints).Count() == 4) == 1)
            {
                if (contentPoints.Count(p => tryBoard.GetStoneNeighbours(p.x, p.y).Intersect(contentPoints).Count() == 2) == 3)
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
        public static Boolean FlowerSevenSideFormation(Board tryBoard, Group killerGroup)
        {
            Content c = killerGroup.Content;
            List<Point> contentPoints = killerGroup.Points.Where(t => tryBoard[t] == c).ToList();
            if (contentPoints.Count() != 7) return false;
            if (contentPoints.Count(p => tryBoard.GetStoneNeighbours(p.x, p.y).Intersect(contentPoints).Count() == 4) == 1)
            {
                Boolean isEdge = (contentPoints.Count(p => !tryBoard.PointWithinMiddleArea(p)) == 3);
                if (isEdge && contentPoints.Count(p => tryBoard.GetStoneNeighbours(p.x, p.y).Intersect(contentPoints).Count() == 3) == 1 && contentPoints.Count(p => tryBoard.GetStoneNeighbours(p.x, p.y).Intersect(contentPoints).Count() == 2) == 2)
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
        public static Boolean OneByFourSideFormation(Board tryBoard, Group killerGroup)
        {
            Content c = killerGroup.Content;
            if (tryBoard.GetStoneNeighbours().Any(n => tryBoard[n] != c)) return false;
            List<Point> contentPoints = killerGroup.Points.Where(t => tryBoard[t] == c).ToList();
            if (contentPoints.Count() != 5) return false;
            if (contentPoints.Count(p => tryBoard.GetStoneNeighbours(p.x, p.y).Intersect(contentPoints).Count() == 3) == 1)
            {
                List<Point> middlePoint = contentPoints.Where(p => tryBoard.PointWithinMiddleArea(p)).ToList();
                if (middlePoint.Count != 1) return false;
                //get end point at longer end
                List<Point> endPoints = contentPoints.Except(middlePoint).Where(p => tryBoard.GetStoneNeighbours(p.x, p.y).Intersect(contentPoints).Count() == 1).ToList();
                endPoints.RemoveAll(p => tryBoard.GetDiagonalNeighbours(p.x, p.y).Contains(middlePoint.First()));
                if (endPoints.Any(p => tryBoard.CornerPoint(p))) return false;
                if (endPoints.Count != 1) return false;
                //check diagonal
                Boolean oneLiberty = (killerGroup.Liberties.Count == 1);
                Point q = endPoints.First();
                if (tryBoard.GetDiagonalNeighbours(q.x, q.y).Where(n => !tryBoard.GetStoneNeighbours(n.x, n.y).Contains(middlePoint.First())).Any(n => tryBoard[n] == ((oneLiberty) ? c : Content.Empty)))
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
        public static Boolean TSideFormation(Board tryBoard, Group killerGroup)
        {
            Content c = killerGroup.Content;
            if (tryBoard.GetStoneNeighbours().Any(n => tryBoard[n] != c)) return false;
            List<Point> contentPoints = killerGroup.Points.Where(t => tryBoard[t] == c).ToList();
            if (contentPoints.Count() != 5) return false;
            if (contentPoints.Count(p => tryBoard.GetStoneNeighbours(p.x, p.y).Intersect(contentPoints).Count() == 3) == 1)
            {
                List<Point> middlePoint = contentPoints.Where(p => tryBoard.PointWithinMiddleArea(p)).ToList();
                if (middlePoint.Count != 2) return false;

                if (CheckSideFormationDiagonal(contentPoints, tryBoard, killerGroup))
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
        public static Boolean TwoByFourSideFormation(Board tryBoard, Group killerGroup)
        {
            Content c = killerGroup.Content;
            if (tryBoard.GetStoneNeighbours().Any(n => tryBoard[n] != c)) return false;
            List<Point> contentPoints = killerGroup.Points.Where(t => tryBoard[t] == c).ToList();
            if (contentPoints.Count() != 6) return false;
            if (contentPoints.Count(p => tryBoard.GetStoneNeighbours(p.x, p.y).Intersect(contentPoints).Count() == 3) == 2)
            {
                if (contentPoints.Count(p => !tryBoard.PointWithinMiddleArea(p)) != 4) return false;
                if (CheckSideFormationDiagonal(contentPoints, tryBoard, killerGroup))
                    return true;
            }
            return false;
        }

        private static Boolean CheckSideFormationDiagonal(List<Point> contentPoints, Board tryBoard, Group killerGroup)
        {
            Content c = killerGroup.Content;
            List<Point> endPoints = contentPoints.Where(p => tryBoard.GetStoneNeighbours(p.x, p.y).Where(n => !tryBoard.PointWithinMiddleArea(n.x, n.y)).Intersect(contentPoints).Count() == 1).ToList();
            Boolean oneLiberty = (killerGroup.Liberties.Count == 1);
            foreach (Point q in endPoints)
            {
                if (tryBoard.GetDiagonalNeighbours(q.x, q.y).Where(n => !killerGroup.Points.Contains(n)).Any(n => tryBoard[n] == ((oneLiberty) ? c : Content.Empty)))
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
        public static Boolean CornerThreeFormation(Board tryBoard, Group killerGroup)
        {
            List<Point> contentPoints = killerGroup.Points.Where(t => tryBoard[t] == killerGroup.Content).ToList();
            if (contentPoints.Count() != 3) return false;
            if (!contentPoints.Any(p => tryBoard.CornerPoint(p)) || contentPoints.Any(p => tryBoard.PointWithinMiddleArea(p.x, p.y))) return false;
            if (MaxLengthOfGrid(killerGroup.Points) != 1) return false;
            //ensure at least two atari targets
            if (tryBoard.AtariTargets.Count <= 1) return false;
            return true;
        }


        /*
    15 . . . . . . . . . . . . . . . . . . .
    16 X . . . . . . . . . . . . . . . . . . 
    17 X X . . . . . . . . . . . . . . . . . 
    18 X X X . . . . . . . . . . . . . . . . 
        */
        public static Boolean CornerSixFormation(Board tryBoard, Group killerGroup)
        {
            List<Point> contentPoints = killerGroup.Points.Where(t => tryBoard[t] == killerGroup.Content).ToList();
            if (contentPoints.Count() != 6) return false;
            if (!contentPoints.Any(p => tryBoard.CornerPoint(p))) return false;
            if (contentPoints.Where(p => tryBoard.PointWithinMiddleArea(p.x, p.y)).Count() != 1) return false;
            return (MaxLengthOfGrid(killerGroup.Points) == 2);
        }

        /*
 15 . . . . . . . . . . . . . . . . . . .
 16 . . . . . . . . . . . . . . . . . . . 
 17 X X . . . . . . . . . . . . . . . . . 
 18 X X X . . . . . . . . . . . . . . . . 
         */
        public static Boolean TwoByThreeCornerFormation(Board tryBoard, Group killerGroup)
        {
            List<Point> contentPoints = killerGroup.Points.Where(t => tryBoard[t] == killerGroup.Content).ToList();
            if (contentPoints.Count() != 5) return false;
            if (!contentPoints.Any(p => tryBoard.CornerPoint(p))) return false;
            if (contentPoints.Where(p => tryBoard.PointWithinMiddleArea(p.x, p.y)).Count() != 1) return false;
            return (MaxLengthOfGrid(killerGroup.Points) == 2);
        }

        /*
    15 . . . . . . . . . . . . . . . . . . .
    16 X . . . . . . . . . . . . . . . . . . 
    17 X . . . . . . . . . . . . . . . . . . 
    18 X X . . . . . . . . . . . . . . . . . 
        */
        public static Boolean BentFourCornerFormation(Board tryBoard, Group killerGroup)
        {
            List<Point> contentPoints = killerGroup.Points.Where(t => tryBoard[t] == killerGroup.Content).ToList();
            if (contentPoints.Count() != 4) return false;
            if (!contentPoints.Any(p => tryBoard.CornerPoint(p)) || contentPoints.Any(p => tryBoard.PointWithinMiddleArea(p.x, p.y))) return false;
            if (MaxLengthOfGrid(killerGroup.Points) != 2) return false;
            return true;
        }

        /// <summary>
        /// Killer group within 3 by 2 grid.
        /// </summary>
        public static Boolean WithinThreeByTwoGrid(Group killerGroup)
        {
            (int xLength, int yLength) = WithinGrid(killerGroup.Points);
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
        /// </summary>
        public static LinkedPoint<Point> GetMaxBindingPoint(Board currentBoard, IEnumerable<Board> killBoards)
        {
            List<LinkedPoint<Point>> list = new List<LinkedPoint<Point>>();
            foreach (Board killBoard in killBoards)
            {
                Point p = killBoard.Move.Value;
                List<Point> moveGroup = killBoard.MoveGroup.Points.ToList();
                int maxLengthOfGrid = MaxLengthOfGrid(moveGroup);
                int maxIntersect = moveGroup.Max(q => killBoard.GetStoneNeighbours(q.x, q.y).Intersect(moveGroup).Count());
                HashSet<Group> neighbourGroups = killBoard.GetGroupsFromStoneNeighbours(p, killBoard.MoveGroup.Content);
                int minNeighbourLiberties = (neighbourGroups.Count == 0) ? 0 : neighbourGroups.Min(group => group.Liberties.Count);
                list.Add(new LinkedPoint<Point>(p, new { maxLengthOfGrid, maxIntersect, minNeighbourLiberties, killBoard }));
            }
            //order by grid length then by max of intersection then by minimum neighbour liberties
            list = list.OrderBy(m => ((dynamic)m.CheckMove).maxLengthOfGrid).OrderByDescending(m => ((dynamic)m.CheckMove).maxIntersect).OrderBy(m => ((dynamic)m.CheckMove).minNeighbourLiberties).ToList();

            //check for dead formation
            List<Group> killerGroups = BothAliveHelper.GetCorneredKillerGroup(currentBoard, false);
            Group killerGroup = killerGroups.First();
            foreach (LinkedPoint<Point> p in list)
            {
                Board killBoard = ((dynamic)p.CheckMove).killBoard;
                List<Point> contentPoints = killerGroup.Points.Where(t => killBoard[t] == killerGroup.Content).ToList();
                List<Point> emptyPoints = killerGroup.Points.Where(t => killBoard[t] == Content.Empty).ToList();
                if (DeadFormationInBothAlive(killBoard, killerGroup, contentPoints, emptyPoints))
                    return p;
            }
            return list.First();
        }

    }
}
