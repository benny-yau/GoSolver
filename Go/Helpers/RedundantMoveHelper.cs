using System;
using System.Collections.Generic;
using System.Linq;

namespace Go
{
    public class RedundantMoveHelper
    {
        #region find potential eye
        /// <summary>
        /// Find potential eye that should not be filled. 
        /// Check for killer formations 
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_Corner_A113_2" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanGo_A151_101Weiqi" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_GuanZiPu_A18" />
        /// </summary>
        public static Boolean FindPotentialEye(GameTryMove tryMove)
        {
            Point move = tryMove.Move;
            Board currentBoard = tryMove.CurrentGame.Board;
            Board tryBoard = tryMove.TryGame.Board;
            Content c = tryMove.MoveContent;

            if (!EyeHelper.FindEye(currentBoard, move, c)) return false;
            //find uncovered eye
            if (EyeHelper.FindUncoveredPoint(currentBoard, move, c))
            {
                //check for killer formations
                if (tryBoard.MoveGroupLiberties == 1 && KillerFormationHelper.SuicidalKillerFormations(tryBoard, currentBoard))
                    return false;
                if (EyeDoubleAtari(tryMove))
                    return false;
            }
            else
            {
                //covered eye with more than two liberties
                if (LinkHelper.GetPreviousMoveGroup(currentBoard, tryBoard).Any(n => n.Liberties.Count <= 2))
                    return false;
                //check three liberty group
                if (ImmovableHelper.CheckThreeLibertyGroupAtBigTigerMouth(tryMove))
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Eye double atari <see cref="UnitTestProject.DailyGoProblems.DailyGoProblems_20250326_8" /> 
        /// </summary>
        public static Boolean EyeDoubleAtari(GameTryMove tryMove)
        {
            Point move = tryMove.Move;
            Board currentBoard = tryMove.CurrentGame.Board;
            Board tryBoard = tryMove.TryGame.Board;
            Content c = tryMove.MoveContent;
            List<Group> eyeGroups = LinkHelper.GetPreviousMoveGroup(currentBoard, tryBoard);
            foreach (Group g in eyeGroups.Where(e => e.Liberties.Count == 2))
            {
                Point p = g.Liberties.First(n => !n.Equals(move));
                if (!currentBoard.GetDiagonalNeighbours(move).Contains(p)) continue;
                if (!currentBoard.GetGroupsFromStoneNeighbours(p, c.Opposite()).Except(eyeGroups).Any(n => n.Liberties.Count == 2)) continue;
                if (ImmovableHelper.IsSuicidalMove(currentBoard, p, c.Opposite(), true)) continue;
                return true;
            }
            return false;
        }
        #endregion

        #region redundant covered eye move

        /// <summary>
        /// Redundant covered eye move.
        /// Two-point covered eye <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_Scenario_Corner_A68" /> 
        /// <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_Scenario_WuQingYuan_Q31673" /> 
        /// </summary>
        public static Boolean RedundantCoveredEyeMove(GameTryMove tryMove)
        {
            if (FindCoveredEyeMove(tryMove))
                return true;

            //find covered eye for opponent
            GameTryMove opponentMove = tryMove.MakeMoveWithOpponentAtSamePoint();
            if (opponentMove != null && FindCoveredEyeMove(opponentMove, tryMove))
                return true;

            return false;
        }

        /// <summary>
        /// Find covered eye.
        /// Check eye for survival <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_Scenario_WindAndTime_Q29277" /> 
        /// <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_Scenario_Corner_B25" /> 
        /// Check kill opponent <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_Scenario_XuanXuanGo_A34" />
        /// Check possible links <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_Scenario_XuanXuanQiJing_Weiqi101_18497_2" />
        /// <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_Scenario_XuanXuanQiJing_Weiqi101_B74" />
        /// </summary>
        public static Boolean FindCoveredEyeMove(GameTryMove tryMove, GameTryMove opponentTryMove = null)
        {
            Board tryBoard = tryMove.TryGame.Board;
            Board currentBoard = tryMove.CurrentGame.Board;
            Content c = tryMove.MoveContent;
            if (tryMove.AtariResolved) return false;
            Group eyeGroup = null;
            Point eyePoint = new Point();
            List<Point> eyePoints = tryBoard.GetStoneNeighbours().Where(n => EyeHelper.FindCoveredEye(tryBoard, n, c)).ToList();
            if (eyePoints.Count == 1)
            {
                //one-point covered eye
                eyePoint = eyePoints.First();
                if (!EyeHelper.CoveredMove(tryBoard, eyePoint, c) || KoHelper.IsKoFight(tryBoard)) return false;
                Board b = new Board(tryBoard);
                b[eyePoint] = c.Opposite();
                eyeGroup = b.GetGroupAt(eyePoint);
            }
            else if (tryBoard.CapturedList.Count == 1 && tryBoard.CapturedPoints.Count() == 2 && EyeHelper.FindCoveredEyeAfterCapture(tryBoard, tryBoard.CapturedList.First()))
            {
                //two-point covered eye
                eyePoint = tryBoard.CapturedPoints.First(q => tryBoard.GetStoneNeighbours().Contains(q));
                if (!EyeHelper.CoveredMove(tryBoard, eyePoint, c)) return false;
                Boolean unEscapable = tryBoard.MoveGroup.Liberties.Any(n => tryBoard.GameInfo.IsMovablePoint[n.x, n.y] == false);
                if (unEscapable)
                    eyeGroup = tryBoard.CapturedList.First();
            }
            if (eyeGroup == null) return false;
            if (!tryBoard.IsCapturedGroup(eyeGroup)) return false;

            //check no eye for survival
            if (!WallHelper.NoEyeForSurvivalAtNeighbourPoints(tryBoard))
                return false;

            //check eye for survival
            Point p = eyeGroup.Points.Count == 1 ? eyePoint : eyeGroup.Points.First(n => !n.Equals(eyePoint));
            List<Point> diagonals = ImmovableHelper.GetDiagonalsOfTigerMouth(currentBoard, p, c).Where(n => !WallHelper.NoEyeForSurvival(tryBoard, n, c)).ToList();
            if (diagonals.Any() && !FindRealEyeAtDiagonal(diagonals, currentBoard, c))
                return false;

            //check kill opponent
            List<Point> opponentPoints = tryBoard.GetStoneAndDiagonalNeighbours().Except(tryBoard.GetStoneNeighbours(eyePoint)).ToList();
            opponentPoints.Remove(eyePoint);
            if (opponentPoints.Any(n => !WallHelper.NoEyeForSurvival(currentBoard, n, c.Opposite()) && !tryBoard.GetGroupsFromStoneNeighbours(n, c).Any(s => WallHelper.IsNonKillableGroup(tryBoard, s))))
                return false;

            //check two liberty group to capture neighbour
            if (currentBoard.GetNeighbourGroups(eyeGroup).Any(n => CheckTwoLibertyGroupToCaptureNeighbour(currentBoard, tryBoard, n)))
                return false;

            //check possible links
            if (LinkHelper.PossibleLinkForGroups(tryBoard, currentBoard))
                return false;

            //check liberty fight
            if (CheckLibertyFightAtCoveredEye(currentBoard, eyePoint, c))
                return false;

            if (opponentTryMove != null)
            {
                Board opponentBoard = opponentTryMove.TryGame.Board;
                //check no eye for survival for opponent
                if (!WallHelper.NoEyeForSurvivalAtNeighbourPoints(opponentBoard))
                    return false;

                //check must-have move
                if (!StrongGroupsAtMustHaveMove(opponentBoard, eyePoint))
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Check two liberty group to capture neighbour.
        /// <see cref="UnitTestProject.CheckForRecursionTest.CheckForRecursionTest_Scenario_Corner_B41" /> 
        /// <see cref="UnitTestProject.NeutralPointMoveTest.NeutralPointMoveTest_Scenario_XuanXuanQiJing_A38" /> 
        /// Check eye for suicidal move <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_Scenario_WindAndTime_Q30275" />
        /// <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_Scenario_Corner_A84_3" />
        /// Capture opponent groups <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_Scenario_TianLongTu_Q17154" />
        /// Check escape capture link <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_Scenario_XuanXuanGo_A26_3" />
        /// </summary>
        private static Boolean CheckTwoLibertyGroupToCaptureNeighbour(Board currentBoard, Board tryBoard, Group group)
        {
            Content c = group.Content;
            if (group.Liberties.Count != 2) return false;
            foreach (Point liberty in group.Liberties)
            {
                (Boolean suicidal, Board b) = ImmovableHelper.IsSuicidalMove(liberty, c, currentBoard, true);
                if (!suicidal) continue;
                //check eye for suicidal move
                if (b != null && GroupHelper.IncreasedKillerGroups(b, currentBoard))
                    return true;
                //capture opponent groups
                if (!tryBoard.GetGroupsFromStoneNeighbours(liberty, c).Any(n => n.Liberties.Count == 2 && ImmovableHelper.CheckConnectAndDie(tryBoard, n)))
                    continue;
                //check escape capture link
                if (ImmovableHelper.EscapeCaptureLink(currentBoard, group))
                    continue;
                return true;
            }
            return false;
        }
        #endregion

        #region fill ko eye move
        /// <summary>
        /// Fill ko eye move. <see cref="UnitTestProject.FillKoEyeMoveTest.FillKoEyeMoveTest_Scenario_XuanXuanGo_A46_101Weiqi" />
        /// <see cref="UnitTestProject.FillKoEyeMoveTest.FillKoEyeMoveTest_Scenario_WuQingYuan_Q31657" /> 
        /// Double atari <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WindAndTime_Q30358" /> 
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanGo_A82_101Weiqi" /> 
        /// Check both alive <see cref="UnitTestProject.FillKoEyeMoveTest.FillKoEyeMoveTest_SimpleSeki" /> 
        /// <see cref="UnitTestProject.RedundantKoMoveTest.RedundantKoMoveTest_Scenario_XuanXuanGo_A151_101Weiqi_2" /> 
        /// Ensure group more than one point have more than one liberty <see cref="UnitTestProject.FillKoEyeMoveTest.FillKoEyeMoveTest_Scenario_Nie20" /> 
        /// Check for killer formation <see cref="UnitTestProject.FillKoEyeMoveTest.FillKoEyeMoveTest_Scenario_Corner_A67" />
        /// Check weak group in connect and die <see cref="UnitTestProject.FillKoEyeMoveTest.FillKoEyeMoveTest_Scenario_XuanXuanGo_B6" /> 
        /// Check suicide at tiger mouth <see cref="UnitTestProject.FillKoEyeMoveTest.FillKoEyeMoveTest_Scenario_TianLongTu_Q16867" /> 
        /// <see cref="UnitTestProject.FillKoEyeMoveTest.FillKoEyeMoveTest_Scenario_GuanZiPu_B3" /> 
        /// Two covered eyes <see cref="UnitTestProject.FillKoEyeMoveTest.FillKoEyeMoveTest_Scenario5dan18" />
        /// <see cref="UnitTestProject.LifeCheckTest.LifeCheckTest_Scenario_XuanXuanQiJing_Weiqi101_18497_2" /> 
        /// Check double ko <see cref = "UnitTestProject.FillKoEyeMoveTest.FillKoEyeMoveTest_Scenario_TianLongTu_Q16975" />
        /// <see cref = "UnitTestProject.FillKoEyeMoveTest.FillKoEyeMoveTest_Scenario_WindAndTime_Q30275_2" />
        /// </summary>
        public static Boolean FillKoEyeMove(GameTryMove tryMove)
        {
            Point move = tryMove.Move;
            Board currentBoard = tryMove.CurrentGame.Board;
            Board tryBoard = tryMove.TryGame.Board;
            Content c = tryMove.MoveContent;
            //ensure is fill covered eye
            if (!EyeHelper.FindCoveredEye(currentBoard, move, c)) return false;

            (Boolean connectAndDie, Board captureBoard) = ImmovableHelper.ConnectAndDie(tryBoard, tryBoard.MoveGroup, false);
            if (connectAndDie)
            {
                //check for killer formation
                if (KillerFormationHelper.SuicidalKillerFormations(tryBoard, currentBoard))
                    return false;

                //check weak group in connect and die
                if (!CheckWeakGroupInConnectAndDie(tryMove, captureBoard))
                    return true;
            }

            //not ko enabled
            List<Group> eyeGroups = LinkHelper.GetPreviousMoveGroup(currentBoard, tryBoard);
            if (!KoHelper.KoContentEnabled(c, tryBoard.GameInfo) && eyeGroups.Any(e => KoHelper.IsKoFight(currentBoard, e)))
                return false;

            //ensure group more than one point have more than one liberty
            if (eyeGroups.Any(e => e.Points.Count > 1 && e.Liberties.Count == 1))
                return false;

            //double atari
            if (eyeGroups.Count(n => n.Liberties.Count == 1) >= 2)
                return false;

            if (EyeDoubleAtari(tryMove))
                return false;

            //check both alive
            if (BothAliveHelper.CheckForBothAliveAtMove(tryBoard))
                return false;

            //check suicide at tiger mouth
            if (ImmovableHelper.SuicideAtBigTigerMouth(tryMove).Item1)
                return false;

            //two covered eyes
            if (eyeGroups.Any(e => e.Liberties.Count == 2 && e.Liberties.All(n => EyeHelper.FindCoveredEye(currentBoard, n, c))))
            {
                if (!WallHelper.StrongGroupsAtCoveredBoard(currentBoard, eyeGroups.First()))
                    return false;
            }

            //check double ko
            Board b = currentBoard.MakeMoveOnNewBoard(move, c.Opposite(), true);
            if (b != null)
            {
                if (KoHelper.IsKoFight(b) && KoHelper.PossibilityOfDoubleKo(b, currentBoard))
                    return false;
                Board b2 = ImmovableHelper.CaptureSuicideGroup(b);
                if (b2 != null && KoHelper.IsKoFight(b2) && KoHelper.PossibilityOfDoubleKo(b2, b))
                    return false;
            }

            return true;
        }
        #endregion

        #region atari redundant move

        /// <summary>
        /// Redundant atari move.
        /// <see cref="UnitTestProject.AtariRedundantMoveTest.AtariRedundantMoveTest_Scenario_Corner_A9_Ext" />
        /// Ensure more than one liberty for move group <see cref="UnitTestProject.AtariRedundantMoveTest.AtariRedundantMoveTest_Scenario_Corner_A68" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_TianLongTu_Q16748" />
        /// Check capture secure <see cref="UnitTestProject.AtariRedundantMoveTest.AtariRedundantMoveTest_Scenario_WindAndTime_Q30225_2" />
        /// <see cref="UnitTestProject.AtariRedundantMoveTest.AtariRedundantMoveTest_Scenario_WindAndTime_Q30225_3" />
        /// <see cref="UnitTestProject.AtariRedundantMoveTest.AtariRedundantMoveTest_Scenario_Side_A23" />
        /// Check killer group <see cref="UnitTestProject.AtariRedundantMoveTest.AtariRedundantMoveTest_Scenario_Side_A25" />
        /// </summary>
        public static Boolean AtariRedundantMove(GameTryMove tryMove)
        {
            Board currentBoard = tryMove.CurrentGame.Board;
            Board tryBoard = tryMove.TryGame.Board;
            Point move = tryMove.Move;
            Content c = tryBoard.MoveGroup.Content;
            if (tryBoard.AtariTargets.Count != 1 || tryMove.AtariResolved || tryBoard.MoveGroupLiberties == 1 || tryMove.Captured) return false;
            Group atariTarget = tryBoard.AtariTargets.First();
            Point atariPoint = tryBoard.GetStoneNeighbours().First(n => tryBoard[n] == c.Opposite() && tryBoard.GetGroupAt(n).Equals(atariTarget));

            Point q = atariTarget.Liberties.First();
            if (!KillerFormationHelper.IsFirstPoint(currentBoard, q, move)) return false;

            //check killer group
            Group killerGroup = GroupHelper.GetKillerGroupOfDirectNeighbourGroups(currentBoard, atariPoint, c);
            if (killerGroup == null || currentBoard.GetNeighbourGroups(killerGroup).Any(n => n.Liberties.Count <= 2))
                return false;

            if (!GroupHelper.IsSingleGroupWithinKillerGroup(currentBoard, atariTarget))
                return false;

            //ensure target group cannot escape
            if (!ImmovableHelper.CheckCaptureSecure(tryBoard, atariTarget, true))
                return false;

            //make move at the other liberty
            (Boolean suicidal, Board board) = ImmovableHelper.IsSuicidalMove(q, c, currentBoard);
            if (suicidal) return false;
            Group target = board.GetGroupAt(atariPoint);
            if (!GameTryMove.IsNegligibleForBoard(board, currentBoard, n => !n.Equals(target))) return false;

            //ensure the other move can capture atari target as well
            if (!ImmovableHelper.CheckCaptureSecure(board, target, true))
                return false;
            return true;
        }

        #endregion

        #region suicidal move
        /// <summary>
        /// Suicidal redundant move.
        /// </summary>
        public static Boolean SuicidalRedundantMove(GameTryMove tryMove)
        {
            Board tryBoard = tryMove.TryGame.Board;
            if (tryBoard.MoveGroupLiberties == 1)
            {
                Boolean singlePoint = tryBoard.MoveGroup.Points.Count == 1;
                if (singlePoint && SinglePointSuicidalMove(tryMove))
                    return true;
                if (!singlePoint && MultiPointSuicidalMove(tryMove))
                    return true;
            }
            else if (tryBoard.MoveGroupLiberties == 2)
            {
                if (SuicidalConnectAndDie(tryMove))
                    return true;
            }
            if (SuicidalMoveAtNonKillableGroup(tryMove))
                return true;

            //test if opponent move at same point is suicidal
            GameTryMove opponentMove = tryMove.MakeMoveWithOpponentAtSamePoint();
            if (opponentMove == null) return false;
            Board opponentTryBoard = opponentMove.TryGame.Board;
            if (opponentTryBoard.MoveGroupLiberties == 1)
            {
                Boolean singlePoint = opponentTryBoard.MoveGroup.Points.Count == 1;
                if (singlePoint && SinglePointSuicidalMove(opponentMove, tryMove))
                    return true;
                if (!singlePoint && MultiPointOpponentSuicidalMove(tryMove))
                    return true;
            }

            if (SuicidalMoveAtNonKillableGroup(opponentMove, tryMove))
                return true;
            return false;
        }

        private static Boolean SuicidalMoveAtNonKillableGroup(GameTryMove tryMove, GameTryMove opponentTryMove = null)
        {
            if (MoveWithinNonKillableGroup(tryMove, opponentTryMove))
                return true;
            if (opponentTryMove == null && MoveNextToNonKillableGroup(tryMove))
                return true;
            return false;
        }

        /// <summary>
        /// Suicidal move within non killable group.
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario3dan17" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario3kyu28_2" />
        /// Check for negligible in opponent move <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanQiJing_A38_3" />
        /// Check any is non killable <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WindAndTime_Q30370" />
        /// Check for covered eye <see cref="UnitTestProject.RedundantTigerMouthMove.RedundantTigerMouthMove_Scenario_WindAndTime_Q30225_2" />
        /// </summary>
        private static Boolean MoveWithinNonKillableGroup(GameTryMove tryMove, GameTryMove opponentTryMove = null)
        {
            Board currentBoard = tryMove.CurrentGame.Board;
            Board tryBoard = tryMove.TryGame.Board;
            Point move = tryBoard.Move.Value;
            Content c = tryBoard.MoveGroup.Content;
            if (GameHelper.GetContentForSurviveOrKill(tryBoard.GameInfo, SurviveOrKill.Survive) != c) return false;
            //check for negligible in opponent move
            if (opponentTryMove != null && !opponentTryMove.IsNegligible) return false;

            Group killerGroup = GroupHelper.GetKillerGroupFromCache(tryBoard, move, c.Opposite());
            if (killerGroup == null) return false;

            if (LifeCheck.GetTargets(tryBoard).Any(t => GroupHelper.GetKillerGroupFromCache(tryBoard, t.Points.First(), c.Opposite()) == killerGroup)) return false;

            if (LinkHelper.FindDiagonalCut(tryBoard, tryBoard.MoveGroup).Item1 != null) return false;

            //all neighbour groups are non-killable
            List<Group> ngroups = tryBoard.GetNeighbourGroups(killerGroup);
            if (ngroups.All(n => WallHelper.IsNonKillableGroup(tryBoard, n)))
                return true;

            //check any is non killable
            if (!ngroups.Any(n => WallHelper.IsNonKillableGroup(tryBoard, n)))
                return false;

            foreach (Group ngroup in ngroups)
            {
                List<LinkedPoint<Point>> diagonalPoints = LinkHelper.GetGroupLinkedDiagonals(tryBoard, ngroup);
                foreach (LinkedPoint<Point> p in diagonalPoints)
                {
                    if (LinkHelper.CheckIsDiagonalLinked(p, tryBoard)) continue;
                    List<Point> diagonals = LinkHelper.PointsBetweenDiagonals(p.Move, (Point)p.CheckMove);
                    diagonals = diagonals.Where(q => GroupHelper.GetKillerGroupFromCache(tryBoard, q, c.Opposite()) == killerGroup).ToList();
                    if (diagonals.Count == 0) continue;
                    Point d = diagonals.First();
                    Board b = null;
                    if (tryBoard[d] == Content.Empty) //connect at diagonal
                        b = tryBoard.MakeMoveOnNewBoard(d, c.Opposite());
                    else //capture opponent at diagonal
                        b = ImmovableHelper.CaptureSuicideGroup(d, tryBoard);
                    //check if all changed to non killable groups
                    if (b == null || !diagonalPoints.All(n => LinkHelper.CheckIsDiagonalLinked(n, b))) continue;
                    Group kgroup = GroupHelper.GetKillerGroupFromCache(b, move, c.Opposite());
                    if (kgroup != null && WallHelper.TargetWithAllNonKillableGroups(b, kgroup))
                    {
                        //check for covered eye
                        if (opponentTryMove != null && EyeHelper.IsCovered(tryBoard, move, c.Opposite()))
                            continue;
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Move next to non killable group.
        /// <see cref="UnitTestProject.RestoreNeutralMoveTest.RestoreNeutralMoveTest_Scenario4dan17" />
        /// Check strong groups <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_TianLongTu_Q17241" />
        /// </summary>
        public static Boolean MoveNextToNonKillableGroup(GameTryMove tryMove)
        {
            Board currentBoard = tryMove.CurrentGame.Board;
            Board tryBoard = tryMove.TryGame.Board;
            Point move = tryBoard.Move.Value;
            Content c = GameHelper.GetContentForSurviveOrKill(tryBoard.GameInfo, SurviveOrKill.Kill);

            if (currentBoard.GetStoneAndDiagonalNeighbours(move).Any(n => currentBoard[n] == c.Opposite()))
                return false;

            if (!currentBoard.GetStoneNeighbours(move).Any(n => WallHelper.IsNonKillableGroup(currentBoard, n)))
                return false;

            //check strong groups
            if (!WallHelper.StrongGroups(tryBoard, tryBoard.GetGroupsFromStoneNeighbours()))
                return false;

            if (currentBoard.GetStoneAndDiagonalNeighbours(move).Count(n => currentBoard[n] == c && WallHelper.IsNonKillableGroup(currentBoard, n)) >= 2)
                return true;
            return false;
        }

        /// <summary>
        /// Multi point opponent suicidal move.
        /// <see cref="UnitTestProject.SurvivalTigerMouthMoveTest.RedundantTigerMouthMove_Scenario_XuanXuanGo_A26" />
        /// Check move group liberties <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_TianLongTu_Q14916_2" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_Corner_A67" />
        /// Check for unescapable group <see cref = "UnitTestProject.ImmovableTest.ImmovableTest_Scenario_TianLongTu_Q17255" />
        /// Find eye at move <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_TianLongTu_Q16850" />
        /// Check for ko or capture move by atari target <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_TianLongTu_Q14992" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanGo_A28_101Weiqi" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanGo_A145_101Weiqi" />
        /// Check snapback <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanGo_B31_4" />
        /// Check for suicide at big tiger mouth <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanGo_A55_2" />
        /// Check for eye at liberty point <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanQiJing_A8" />
        /// Check for tiger mouth at liberty point <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WuQingYuan_Q31646" />
        /// Check for suicidal at other end <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_TianLongTu_Q16867" />
        /// Check for both alive <see cref="UnitTestProject.SurvivalTigerMouthMoveTest.SurvivalTigerMouthMoveTest_Scenario_TianLongTu_Q16827" />
        /// Check link for groups <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WindAndTime_Q30358_3" />
        /// Set diagonal eye move <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_Nie4_4" />
        /// <see cref="UnitTestProject.RedundantEyeDiagonalMoveTest.RedundantEyeDiagonalMoveTest_Scenario_XuanXuanGo_A16" />
        /// </summary>
        private static Boolean MultiPointOpponentSuicidalMove(GameTryMove tryMove)
        {
            Board currentBoard = tryMove.CurrentGame.Board;
            Board tryBoard = tryMove.TryGame.Board;
            Point move = tryBoard.Move.Value;
            Content c = tryBoard.MoveGroup.Content;
            if (tryBoard.MoveGroupLiberties == 1 || tryMove.Captured || tryMove.AtariResolved || tryBoard.AtariTargets.Count != 1) return false;

            Group atariTarget = tryBoard.AtariTargets.First();
            if (tryBoard.GetStoneNeighbours().Any(n => tryBoard[n] == Content.Empty || (tryBoard[n] == c.Opposite() && !tryBoard.GetGroupAt(n).Equals(atariTarget)))) return false;
            //check for unescapable group
            (Boolean unEscapable, Board escapeBoard) = ImmovableHelper.UnescapableGroup(tryBoard, atariTarget, false);
            if (unEscapable) return false;

            //check for weak group
            if (CheckWeakGroupInOpponentSuicide(tryBoard, atariTarget))
                return false;

            //check for suicide at big tiger mouth
            if (ImmovableHelper.SuicideAtBigTigerMouth(tryMove).Item1)
                return false;

            //check for both alive
            if (BothAliveHelper.CheckForBothAliveAtMove(tryBoard))
                return false;

            //check for bloated eye
            if (KoFightAtBloatedEye(tryBoard, currentBoard))
                return false;

            //check link for groups
            if (LinkHelper.PossibleLinkForGroups(tryBoard, currentBoard))
                return false;

            //set diagonal eye move
            if (tryBoard.GetDiagonalNeighbours().Any(n => EyeHelper.FindEye(currentBoard, n, c)) && ImmovableHelper.IsImmovablePoint(currentBoard, move, c))
                tryMove.IsDiagonalEyeMove = true;
            return true;
        }

        /// <summary>
        /// Ko fight at bloated eye.
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_Corner_A85" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_x_2" />
        /// </summary>
        private static Boolean KoFightAtBloatedEye(Board tryBoard, Board currentBoard)
        {
            Content c = tryBoard.MoveGroup.Content;
            foreach (Point d in tryBoard.GetDiagonalNeighbours())
            {
                if (tryBoard[d] != Content.Empty) continue;
                if ((tryBoard.GetStoneNeighbours(d).Any(n => KoHelper.MakeKoFight(currentBoard, n, c)) || KoHelper.IsKoFight(currentBoard, d, c).Item1))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Check weak group in opponent suicide.
        /// <see cref = "UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_TianLongTu_Q16604_3" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_TianLongTu_Q16604_4" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanGo_B32_2" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_Corner_A67_3" />
        /// Check suicidal for both <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_Corner_A67_3" />
        /// </summary>
        private static Boolean CheckWeakGroupInOpponentSuicide(Board tryBoard, Group atariTarget)
        {
            Content c = tryBoard.MoveGroup.Content;

            //escape by capture
            List<Group> atariGroups = AtariHelper.AtariByGroup(atariTarget, tryBoard);
            if (atariGroups.Count > 1) return true;

            if (atariGroups.Count == 1)
            {
                Board captureBoard = ImmovableHelper.CaptureSuicideGroup(tryBoard, atariGroups.First());
                //capture path escapable
                Group escapeGroup = captureBoard.GetCurrentGroup(atariTarget);
                if (!ImmovableHelper.CheckConnectAndDie(captureBoard, escapeGroup))
                {
                    //check weak group
                    if (AtariHelper.IsWeakNeighbourGroup(captureBoard, escapeGroup))
                        return true;
                    //continue escape
                    if (escapeGroup.Liberties.Count == 2 && !WallHelper.IsHostileNeighbourGroup(captureBoard, escapeGroup))
                        return true;
                }
            }

            //escape at liberty point
            Board b = ImmovableHelper.MakeMoveAtLiberty(tryBoard, atariTarget, c.Opposite());
            if (b == null) return true;
            //check weak group
            if (AtariHelper.IsWeakNeighbourGroup(b, b.MoveGroup))
                return true;
            //check suicidal for both
            List<Point> liberties = b.GetGroupLiberties(atariTarget);
            if (liberties.Count == 2 && liberties.All(n => ImmovableHelper.IsSuicidalMoveForBothPlayers(b, n)))
                return true;
            //continue escape
            if (b.MoveGroupLiberties == 2 && !WallHelper.IsHostileNeighbourGroup(b))
                return true;
            return false;
        }

        /// <summary>
        /// Check for connect and die moves. <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_TianLongTu_Q16738" />
        /// Check capture moves <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanGo_A75_101Weiqi" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.CheckForRecursionTest_Scenario_Corner_B41" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_Corner_A113_3" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanQiJing_B36" />
        /// Check atari moves <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WuQingYuan_Q30986" />
        /// Check killer formation <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_GuanZiPu_A17_3" />
        /// </summary>
        public static Boolean SuicidalConnectAndDie(GameTryMove tryMove)
        {
            Board currentBoard = tryMove.CurrentGame.Board;
            Board tryBoard = tryMove.TryGame.Board;
            Point move = tryMove.Move;
            Content c = tryMove.MoveContent;

            //check connect and die
            (Boolean connectAndDie, Board captureBoard) = ImmovableHelper.ConnectAndDie(tryBoard, tryBoard.MoveGroup, false);
            if (!connectAndDie) return false;

            if (LifeCheck.GetTargets(tryBoard).All(t => tryBoard.MoveGroup.Equals(t))) return true;

            //check capture moves
            if (tryBoard.CapturedList.Any(g => AtariHelper.AtariByGroup(currentBoard, g))) return false;

            //check atari moves
            foreach (Group atariTarget in tryBoard.AtariTargets)
            {
                Board b = ImmovableHelper.CaptureSuicideGroup(captureBoard, atariTarget);
                if (b != null && b.CapturedList.Count > 1)
                    return false;
            }

            //check weak group
            if (CheckWeakGroupInConnectAndDie(tryMove, captureBoard))
                return false;

            //find bloated eye suicide
            if (FindBloatedEyeSuicide(tryMove, captureBoard))
                return true;

            //check redundant corner point
            if (CheckRedundantCornerPoint(tryMove, captureBoard))
                return true;

            //check diagonals
            if (CheckDiagonalForSuicidalConnectAndDie(tryMove, captureBoard))
                return true;

            //capture at all non killable groups
            if (CheckCaptureAtAllNonKillableGroups(captureBoard, tryBoard.MoveGroup))
                return true;

            if (tryBoard.MoveGroup.Points.Count <= 4)
            {
                //check for real eye in neighbour groups
                return CheckAnyRealEyeInSuicidalConnectAndDie(tryBoard, captureBoard);
            }
            //check killer formation
            else if (KillerFormationHelper.SuicidalKillerFormations(tryBoard, currentBoard, captureBoard))
                return false;
            return true;
        }

        /// <summary>
        /// Redundant one point move in connect and die.
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanGo_A151_101Weiqi_4" />
        /// Ensure killer group contains only try move <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_TianLongTu_Q16594" />
        /// Ensure all strong neighbour groups <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanGo_A151_101Weiqi_7" />
        /// </summary>
        private static Boolean RedundantOnePointMoveInConnectAndDie(GameTryMove tryMove, Board captureBoard)
        {
            Board tryBoard = tryMove.TryGame.Board;
            Point move = tryMove.Move;
            Content c = tryMove.MoveContent;

            if (tryBoard.MoveGroup.Points.Count != 1 || !tryMove.IsNegligible) return false;

            //check liberty surrounded by opponent
            if (KillerFormationHelper.SuicideMoveValidWithOneEmptySpaceLeft(tryBoard))
                return false;

            //ensure killer group contains only try move
            if (!GroupHelper.IsSingleGroupWithinKillerGroup(tryBoard))
                return false;

            //check capture secure
            if (!ImmovableHelper.CheckCaptureSecure(captureBoard, tryBoard.MoveGroup))
                return false;

            //ensure all strong neighbour groups
            if (WallHelper.StrongNeighbourGroups(captureBoard, move, c))
                return true;

            return false;
        }

        /// <summary>
        /// Check for weak group with two or less liberties in connect and die.
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_x" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanGo_B6" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanQiJing_B17" />
        /// </summary>
        private static Boolean CheckWeakGroup(Board tryBoard, Group targetGroup)
        {
            Content c = targetGroup.Content;
            Group group = tryBoard.GetCurrentGroup(targetGroup);

            //capture move
            (_, Board b) = ImmovableHelper.ConnectAndDie(tryBoard, group, false);
            if (b == null || b.MoveGroupLiberties == 1 || b.IsCapturedGroup(group)) return false;

            //check weak group
            if (AtariHelper.IsWeakNeighbourGroup(b, b.GetCurrentGroup(group)))
                return true;

            //escape move at liberty
            Board b2 = ImmovableHelper.MakeMoveAtLiberty(b, group, c);
            if (b2 != null && b2.MoveGroupLiberties == 2 && CheckWeakGroup(b2, group))
                return true;

            //escape by capture
            foreach (Group gr in AtariHelper.AtariByGroup(group, b))
            {
                Board b3 = ImmovableHelper.CaptureSuicideGroup(b, gr);
                Group target = b3.GetCurrentGroup(group);
                if (target.Liberties.Count == 2 && CheckWeakGroup(b3, target))
                    return true;
                if (!b3.MoveGroup.Equals(target) && AtariHelper.IsWeakNeighbourGroup(b, b3.MoveGroup))
                    return true;
            }
            return false;
        }


        /// <summary>
        /// Check weak group in connect and die.
        /// Check three liberty weak group <see cref="UnitTestProject.DailyGoProblems.DailyGoProblems_20250311_8" /> 
        /// </summary>
        private static Boolean CheckWeakGroupInConnectAndDie(GameTryMove tryMove, Board captureBoard)
        {
            Board currentBoard = tryMove.CurrentGame.Board;
            Board tryBoard = tryMove.TryGame.Board;
            if (!tryBoard.GetStoneNeighbours().Any(n => tryBoard[n] == Content.Empty) && !Board.ResolveAtari(currentBoard, tryBoard))
                return false;
            if (CheckWeakGroup(tryBoard, tryBoard.MoveGroup)) return true;

            //check three liberty weak group
            if (captureBoard.MoveGroupLiberties == 3)
            {
                if (captureBoard.GetStoneNeighbours().Any(n => captureBoard[n] == Content.Empty)) return false;
                List<Group> ngroups = captureBoard.GetGroupsFromStoneNeighbours().ToList();
                ngroups.Remove(captureBoard.GetCurrentGroup(tryBoard.MoveGroup));
                if (ngroups.Count == 0) return false;
                if (ngroups.Any(n => !WallHelper.IsNonKillableGroup(captureBoard, n)))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Find bloated eye suicide <see cref="UnitTestProject.GenericNeutralMoveTest.GenericNeutralMoveTest_Scenario_GuanZiPu_A35" />
        /// Check killer formation <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_Corner_A113_4" />
        /// Check reverse ko fight <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_Corner_A30" />
        /// Check for eye at corner point <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_Corner_A67_2" />
        /// </summary>
        public static Boolean FindBloatedEyeSuicide(GameTryMove tryMove, Board captureBoard)
        {
            Board currentBoard = tryMove.CurrentGame.Board;
            Board tryBoard = tryMove.TryGame.Board;
            Content c = tryMove.MoveContent;
            if (tryBoard.IsAtariMove) return false;

            //make suicide move
            Point liberty = captureBoard.GetCurrentGroup(tryBoard.MoveGroup).Liberties.First();
            Board b = captureBoard.MakeMoveOnNewBoard(liberty, c);
            if (b != null) return false;

            //check killer formation
            HashSet<Group> eyeGroups = captureBoard.GetGroupsFromStoneNeighbours(liberty, c.Opposite());
            if (eyeGroups.Count == 1 && KillerFormationHelper.SuicidalKillerFormations(tryBoard, currentBoard, captureBoard))
                return false;

            //check reverse ko fight
            if (eyeGroups.Count > 1 && eyeGroups.Any(n => AtariHelper.AtariByGroup(tryBoard, n)))
                return false;

            //check for eye at corner point
            if (tryBoard.MoveGroup.Liberties.Any(n => tryBoard.CornerPoint(n) && tryBoard.GetStoneNeighbours(n).Intersect(tryBoard.MoveGroup.Points).Count() >= 2))
            {
                if (LinkHelper.GetPreviousMoveGroup(currentBoard, tryBoard).Count > 1)
                    return false;
            }

            if (EyeHelper.FindEye(tryBoard, liberty, c) || eyeGroups.Count > 1)
                return true;
            return false;
        }

        /// <summary>
        /// Check redundant corner point.
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_TianLongTu_Q2834" />
        /// Check for kill formation <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_XuanXuanQiJing_Weiqi101_7245" />
        /// Multipoint snapback <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_Corner_B43" />
        /// Two point kill <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_WuQingYuan_Q16508" />
        /// <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_Corner_A6" />
        /// </summary>
        private static Boolean CheckRedundantCornerPoint(GameTryMove tryMove, Board captureBoard)
        {
            Board tryBoard = tryMove.TryGame.Board;
            Point move = tryBoard.Move.Value;
            Content c = tryBoard[move];
            if (tryBoard.MoveGroup.Points.Count != 1 || !tryBoard.CornerPoint() || !tryMove.IsNegligible) return false;

            //check for kill formation
            Boolean killFormation = (tryBoard.GetClosestPoints(move, c.Opposite()).Count >= 3 && !tryBoard.GetClosestPoints(move, c).Any());
            if (killFormation) return false;

            //multipoint snapback
            if (captureBoard.GetNeighbourGroups(tryBoard.MoveGroup).Any(gr => gr.Points.Count > 1 && ImmovableHelper.CheckConnectAndDie(captureBoard, gr)))
                return false;
            return true;
        }

        /// <summary>
        /// Check for real eye in neighbour groups.
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_GuanZiPu_B3_3" />
        /// </summary>
        private static Boolean CheckAnyRealEyeInSuicidalConnectAndDie(Board tryBoard, Board captureBoard)
        {
            Point move = tryBoard.Move.Value;
            Content c = tryBoard.MoveGroup.Content;
            Group killerGroup = GroupHelper.GetKillerGroupFromCache(captureBoard, move, c.Opposite());
            if (killerGroup == null) return false;
            if (!EyeHelper.FindRealEyeOfAnyKillerGroup(captureBoard, killerGroup)) return false;
            return KillerFormationHelper.CheckRealEyeInNeighbourGroups(tryBoard, captureBoard);
        }

        /// <summary>
        /// Check for suicidal moves depending on diagonal groups.
        /// Ensure no diagonal at move <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_GuanZiPu_Q18796_2" />
        /// Ensure no diagonal groups <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_Corner_A55" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_GuanZiPu_A17_3" />
        /// Check liberties are connected <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WindAndTime_Q30064" />
        /// Check for killer formation <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_Scenario_GuanZiPu_A4Q11_101Weiqi_2" />
        /// <see cref="UnitTestProject.KillerFormationTest.KillerFormationTest_Scenario_TianLongTu_Q15082" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_TianLongTu_Q16748" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_GuanZiPu_A2Q28_101Weiqi" />
        /// Stone neighbours at diagonal of each other <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WindAndTime_Q2757" />
        /// Check diagonal at opposite corner of stone neighbours <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WuQingYuan_Q31493" />
        /// Cut diagonal and kill <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanQiJing_Weiqi101_B74_3" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_TianLongTu_Q17081_2" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanQiJing_A61" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_20230603_4" />
        /// Check move next to covered point <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_TianLongTu_Q17132_4" />
        /// Check killer move non killable group <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WuQingYuan_Q31563" />
        /// </summary>
        private static Boolean CheckDiagonalForSuicidalConnectAndDie(GameTryMove tryMove, Board captureBoard)
        {
            Board currentBoard = tryMove.CurrentGame.Board;
            Board tryBoard = tryMove.TryGame.Board;
            Point move = tryMove.Move;
            Content c = tryMove.MoveContent;

            //ensure no diagonal at move
            Boolean diagonalAtMove = LinkHelper.GetMoveDiagonals(tryBoard).Any();
            if (diagonalAtMove) return false;

            if (CheckNoDiagonalAndNoLibertyAtMove(tryMove, captureBoard))
                return true;

            //ensure no diagonal groups
            Boolean diagonalGroups = LinkHelper.GetGroupLinkedDiagonals(tryBoard).Any();
            if (diagonalGroups) return false;

            if (WallHelper.TargetWithAllNonKillableGroups(captureBoard, tryBoard.MoveGroup))
                return true;

            if (tryBoard.MoveGroup.Points.Count > 1)
            {
                Point p = tryBoard.MoveGroup.Liberties.First();
                //check liberties are connected
                if (tryBoard.GetStoneNeighbours(p).Any(q => tryBoard.MoveGroup.Liberties.Contains(q)))
                {
                    //check for killer formation
                    if (tryBoard.MoveGroup.Points.Count >= 3 && KillerFormationHelper.SuicidalKillerFormations(tryBoard, currentBoard, captureBoard))
                        return false;
                    return true;
                }
            }
            else
            {
                //check move next to covered point
                if (tryMove.IsNegligible && tryBoard.GetStoneNeighbours().Any(p => tryBoard[p] == Content.Empty && EyeHelper.IsCovered(tryBoard, p, c.Opposite())) && GroupHelper.IsSingleGroupWithinKillerGroup(tryBoard))
                    return true;

                //stone neighbours at diagonal of each other
                List<Point> npoints = LinkHelper.GetDiagonalPoints(tryBoard);
                if (npoints.Count == 2)
                {
                    //check diagonal at opposite corner of stone neighbours
                    List<Point> diagonals = tryBoard.GetDiagonalNeighbours().Where(d => tryBoard[d] == c.Opposite()).ToList();
                    if (diagonals.Any(d => !tryBoard.GetStoneNeighbours(d).Intersect(npoints).Any()))
                        return false;

                    //cut diagonal and kill
                    List<Point> cutDiagonal = LinkHelper.PointsBetweenDiagonals(npoints[0], npoints[1]);
                    cutDiagonal.Remove(move);
                    Board b = tryBoard.MakeMoveOnNewBoard(cutDiagonal.First(), c, true);
                    if (b != null && npoints.Any(n => ImmovableHelper.CheckConnectAndDie(b, b.GetGroupAt(n))))
                        return false;
                    if (b == null && tryBoard.GetGroupsFromPoints(npoints).Count > 1 && npoints.Any(n => ImmovableHelper.CheckConnectAndDie(tryBoard, tryBoard.GetGroupAt(n))))
                        return false;
                    return true;
                }

                //redundant one point move
                if (RedundantOnePointMoveInConnectAndDie(tryMove, captureBoard))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Check for no diagonals and no liberties at move.
        /// Ensure no liberties <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WindAndTime_Q30064" />
        /// Check for three neighbour groups <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WindAndTime_Q30198" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_TianLongTu_Q16605" />
        /// Check killer formation <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WuQingYuan_Q31499_3" />
        /// </summary>
        private static Boolean CheckNoDiagonalAndNoLibertyAtMove(GameTryMove tryMove, Board captureBoard)
        {
            Board currentBoard = tryMove.CurrentGame.Board;
            Board tryBoard = tryMove.TryGame.Board;
            Point move = tryMove.Move;

            if (tryBoard.MoveGroup.Points.Count == 1) return false;
            //ensure no liberties
            if (KillerFormationHelper.GetLibertiesAtMove(tryBoard).Any())
                return false;

            //check for three neighbour groups
            if (KillerFormationHelper.ThreeOpponentGroupsAtMove(tryBoard))
                return false;

            //check killer formation
            if (KillerFormationHelper.SuicidalKillerFormations(tryBoard, currentBoard, captureBoard))
                return false;

            return true;
        }

        /// <summary>
        /// Check capture at all non killable groups.
        /// </summary>
        public static Boolean CheckCaptureAtAllNonKillableGroups(Board board, Group group)
        {
            Content c = group.Content;
            if (AtariHelper.AtariByGroup(group, board).Any())
                return false;
            Board escapeBoard = ImmovableHelper.MakeMoveAtLiberty(board, group, c);
            if (escapeBoard == null)
            {
                if (board.GetNeighbourGroups(group).All(n => WallHelper.IsNonKillableGroup(board, n)))
                {
                    if (EyeHelper.CheckCoveredEyeAtSuicideGroup(board, group)) return false;
                    return true;
                }
            }
            else if (escapeBoard.MoveGroupLiberties == 1)
            {
                Board captureBoard = ImmovableHelper.CaptureSuicideGroup(escapeBoard);
                if (captureBoard.GetNeighbourGroups(escapeBoard.MoveGroup).All(n => WallHelper.IsNonKillableGroup(captureBoard, n)))
                {
                    if (EyeHelper.CheckCoveredEyeAtSuicideGroup(escapeBoard)) return false;
                    return true;
                }
            }
            else if (escapeBoard.MoveGroupLiberties == 2)
            {
                (_, Board b) = ImmovableHelper.ConnectAndDie(escapeBoard, escapeBoard.MoveGroup, false);
                return CheckCaptureAtAllNonKillableGroups(b, escapeBoard.MoveGroup);
            }
            return false;
        }

        /// <summary>
        /// Single point suicide.
        /// </summary>
        public static Boolean SinglePointSuicidalMove(GameTryMove tryMove, GameTryMove opponentTryMove = null)
        {
            Board tryBoard = tryMove.TryGame.Board;
            if (!tryMove.IsNegligible)
                return false;

            //capture suicide stone
            (_, Board capturedBoard) = ImmovableHelper.IsSuicidalOnCapture(tryBoard);
            if (capturedBoard == null) return false;
            if (capturedBoard.CapturedPoints.Count() > 1) return true;
            if (SuicideWithinRealEye(tryMove, capturedBoard))
                return true;
            if (MiscSinglePointSuicide(tryMove, capturedBoard, opponentTryMove))
                return true;
            return false;
        }

        /// <summary>
        /// Suicide within real eye. 
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_ScenarioHighLevel28" />
        /// Check corner point <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_Corner_A80" />
        /// Check for snapback <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanGo_B31" />
        /// Check atari move <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WindAndTime_Q2757" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanGo_Q18500_3" />
        /// Suicide for liberty fight <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanQiJing_A40_2" />
        /// <see cref="UnitTestProject.BothAliveTest.BothAliveTest_Scenario_WuQingYuan_Q15126" />
        /// <see cref="UnitTestProject.BothAliveTest.BothAliveTest_Scenario_GuanZiPu_B18_3" />
        /// Two liberties - suicide for both players <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanQiJing_Weiqi101_A19" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WindAndTime_Q30215" />
        /// Three liberties <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_20221019_6" />
        /// </summary>
        public static Boolean SuicideWithinRealEye(GameTryMove tryMove, Board capturedBoard)
        {
            Point move = tryMove.Move;
            Board tryBoard = tryMove.TryGame.Board;
            Board currentBoard = tryMove.CurrentGame.Board;
            Content c = tryMove.MoveContent;

            //ensure semi-solid eye
            if (!EyeHelper.FindSemiSolidEye(move, capturedBoard, c.Opposite()).Item1)
                return false;

            //opponent break kill formation
            if (KillerFormationHelper.OpponentBreakKillFormation(tryBoard, currentBoard))
                return false;

            //remove one point from two-point empty group
            Group eyeGroup = GroupHelper.GetKillerGroupFromCache(currentBoard, move, c.Opposite());
            Board board = EyeHelper.FindRealEyesWithinTwoEmptyPoints(currentBoard, eyeGroup);
            if (board != null && !move.Equals(board.Move.Value))
                return true;
            if (capturedBoard.MoveGroupLiberties == 1) return false;

            //check non killable neighbour group
            if (WallHelper.TargetWithAnyNonKillableGroup(tryBoard)) return true;

            //check for snapback
            if (ImmovableHelper.CheckSnapbackFromMove(tryBoard))
                return false;

            //kill covered eye at diagonal point
            if (KillCoveredEyeAtDiagonal(tryBoard, currentBoard))
                return false;

            //retrieve liberties other than eye liberty
            List<Group> ngroups = capturedBoard.GetNeighbourGroups(tryBoard.MoveGroup);
            HashSet<Point> liberties = capturedBoard.GetLibertiesOfGroups(ngroups);
            liberties.Remove(move);

            if (liberties.Count == 1)
            {
                //suicide for liberty fight
                if (KillerFormationHelper.SuicideForLibertyFight(tryBoard, currentBoard))
                    return false;
            }
            else if (liberties.Count == 2 && !liberties.Any(n => EyeHelper.FindEye(capturedBoard, n, c.Opposite())))
            {
                //two liberties - suicide for both players
                IEnumerable<Board> moveBoards = GameHelper.GetMoveBoards(capturedBoard, liberties, c);
                foreach (Board b in moveBoards)
                {
                    //both players are suicidal at the liberty
                    Point q = liberties.First(n => !n.Equals(b.Move));
                    if (GroupHelper.GetKillerGroupFromCache(tryBoard, q, c.Opposite()) == null) continue;
                    if (ImmovableHelper.IsSuicidalMoveForBothPlayers(b, q))
                    {
                        HashSet<Group> groups = currentBoard.GetGroupsFromStoneNeighbours(q, c.Opposite());
                        if (groups.Any(n => ImmovableHelper.EscapeCaptureLink(currentBoard, n)))
                            continue;
                        return false;
                    }
                }
            }
            else if (liberties.Count == 3 && !liberties.Any(n => EyeHelper.FindEye(capturedBoard, n, c.Opposite())))
            {
                //three liberties - suicide for both players
                foreach (Group ngroup in ngroups)
                {
                    List<Point> nLiberties = ngroup.Liberties.Where(n => !n.Equals(move)).ToList();
                    if (nLiberties.Count != 2) continue;
                    IEnumerable<Board> moveBoards = GameHelper.GetMoveBoards(capturedBoard, nLiberties, c);
                    foreach (Board b in moveBoards)
                    {
                        //both players are suicidal at the liberty
                        Point q = nLiberties.First(n => !n.Equals(b.Move));
                        if (ImmovableHelper.IsSuicidalMoveForBothPlayers(b, q))
                            return false;
                    }
                }
            }

            //check atari move
            if (tryBoard.IsAtariMove)
            {
                //check for non two-point group
                Boolean twoPointGroup = eyeGroup != null && eyeGroup.Points.Count == 2;
                if (!twoPointGroup && CheckNonTwoPointGroupInSuicideRealEye(tryMove, capturedBoard))
                    return true;
                if (twoPointGroup && CheckTwoPointGroupInSuicideRealEye(tryMove, capturedBoard))
                    return true;
                return false;
            }
            return true;
        }

        /// <summary>
        /// Check for non two-point group.
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanGo_B31_3" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario4dan17_2" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario4dan17_2" />
        /// Not redundant <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WuQingYuan_Q31536" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_Corner_A139" />
        /// Real solid eye <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario1dan4_3" />
        /// </summary>
        private static Boolean CheckNonTwoPointGroupInSuicideRealEye(GameTryMove tryMove, Board captureBoard)
        {
            Point move = tryMove.Move;
            Board tryBoard = tryMove.TryGame.Board;
            Content c = tryMove.MoveContent;
            //real solid eye
            if (EyeHelper.FindRealSolidEye(move, c.Opposite(), captureBoard))
                return true;
            //get diagonals next to atari target
            List<Point> diagonals = tryBoard.GetDiagonalNeighbours().Where(n => tryBoard[n] != c.Opposite() && tryBoard.GetGroupsFromStoneNeighbours(n, c).Intersect(tryBoard.AtariTargets).Any()).ToList();
            //check killer group
            if (diagonals.Any(d => GroupHelper.GetKillerGroupOfDirectNeighbourGroups(tryBoard, d, c.Opposite()) != null))
                return true;
            return false;
        }

        /// <summary>
        /// Check for two-point group.
        /// Check connect and die <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WuQingYuan_Q15126" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanQiJing_Weiqi101_1887" />
        /// Check for liberty fight <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_GuanZiPu_Q18796" />
        /// </summary>
        private static Boolean CheckTwoPointGroupInSuicideRealEye(GameTryMove tryMove, Board capturedBoard)
        {
            Point move = tryMove.Move;
            Board currentBoard = tryMove.CurrentGame.Board;
            Board tryBoard = tryMove.TryGame.Board;
            Content c = tryMove.MoveContent;
            //capture group
            Point liberty = tryBoard.MoveGroup.Liberties.First();
            if (currentBoard.GetGroupsFromStoneNeighbours(liberty, c).Any(n => n.Liberties.Count == 1))
                return true;

            //check for liberty fight
            List<Group> eyeGroups = capturedBoard.GetGroupsFromStoneNeighbours();
            if (eyeGroups.Count() != 1) return false;
            Group eyeGroup = eyeGroups.First();
            List<Point> liberties = eyeGroup.Liberties.Where(n => !n.Equals(move)).ToList();
            if (liberties.Count > 2)
                return true;
            return false;
        }


        /// <summary>
        /// Miscellaneous single point suicide.
        /// Check connect and die at diagonal group <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_TianLongTu_Q16738_6" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_TianLongTu_Q16738_7" />
        /// Connect and die  <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanGo_A27_2" />
        /// <see cref="UnitTestProject.ImmovableTest.ImmovableTest_Scenario_XuanXuanGo_B32" />
        /// Diagonal non killable groups <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_TianLongTu_Q17160" />
        /// Opponent suicide <see cref="UnitTestProject.FillKoEyeMoveTest.FillKoEyeMoveTest_Scenario_TianLongTu_Q16490" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanGo_A55" />
        /// <see cref="UnitTestProject.SurvivalTigerMouthMoveTest.SurvivalTigerMouthMoveTest_Scenario_Nie67" />
        /// Check real eye at diagonal <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_TianLongTu_Q17132_3" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanQiJing_B25_2" />
        /// Without opposite content <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_Side_B4" />
        /// </summary>
        private static Boolean MiscSinglePointSuicide(GameTryMove tryMove, Board capturedBoard, GameTryMove opponentTryMove = null)
        {
            Point move = tryMove.Move;
            Board currentBoard = tryMove.CurrentGame.Board;
            Board tryBoard = tryMove.TryGame.Board;
            Content c = tryMove.MoveContent;
            if (capturedBoard.MoveGroupLiberties == 1) return false;

            //check corner point suicide
            if (CornerPointSuicide(tryMove, capturedBoard))
                return true;

            //check connect and die at diagonal group
            if (opponentTryMove == null && tryBoard.GetDiagonalNeighbours().Any(n => tryBoard[n] == c && ImmovableHelper.CheckConnectAndDie(tryBoard, tryBoard.GetGroupAt(n)) && !ImmovableHelper.CheckConnectAndDie(currentBoard, currentBoard.GetGroupAt(n))))
                return true;

            //opponent suicide
            if (opponentTryMove != null && (ImmovableHelper.SuicideAtBigTigerMouth(opponentTryMove).Item1 || BothAliveHelper.CheckForBothAliveAtMove(opponentTryMove.TryGame.Board)))
                return false;

            //suicide near non killable group
            if (GameHelper.GetContentForSurviveOrKill(tryBoard.GameInfo, SurviveOrKill.Survive) == c)
            {
                if (WallHelper.TargetWithAnyNonKillableGroup(tryBoard))
                {
                    //check connect and die
                    Boolean connectAndDie = ImmovableHelper.AllConnectAndDie(capturedBoard, move);
                    return !connectAndDie;
                }
            }
            else
            {
                //diagonal non killable groups
                List<Point> diagonals = tryBoard.GetDiagonalNeighbours().Where(n => WallHelper.IsNonKillableGroup(tryBoard, n)).ToList();
                Boolean nonKillableSuicide = tryBoard.PointWithinMiddleArea(move) ? diagonals.Count >= 2 : diagonals.Count >= 1;
                if (!nonKillableSuicide) return false;

                if (CoveredPointSuicidalMove(tryMove)) return false;

                if (diagonals.Any(n => LinkHelper.PointsBetweenDiagonals(move, n).Any(d => tryBoard[d] == Content.Empty)))
                    return true;

                //check real eye at diagonal without opposite content
                if (ImmovableHelper.AllConnectAndDie(capturedBoard, move, c.Opposite())) return false;
                foreach (Point d in capturedBoard.GetDiagonalNeighbours(move))
                {
                    if (capturedBoard[d] != Content.Empty) continue;
                    if (!EyeHelper.FindRealEyeWithinEmptySpace(capturedBoard, d, c.Opposite())) continue;
                    if (!GroupHelper.GetKillerGroupFromCache(capturedBoard, d, c.Opposite()).Points.Any(p => capturedBoard[p] == c))
                        return true;
                }
            }
            return false;
        }


        /// Check corner point <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanGo_A26_2" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_TianLongTu_Q16738_5" />
        /// One point target <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_Corner_A84_2" />
        /// <see cref="UnitTestProject.KoTest.KoTest_Scenario_WuQingYuan_Q31680" />
        /// Not suicidal <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_Corner_A95" />
        /// <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_GuanZiPu_A17_2" />
        private static Boolean CornerPointSuicide(GameTryMove tryMove, Board captureBoard)
        {
            Board currentBoard = tryMove.CurrentGame.Board;
            Board tryBoard = tryMove.TryGame.Board;
            Content c = tryMove.MoveContent;
            if (!tryBoard.CornerPoint()) return false;

            //one point target
            if (!tryBoard.AtariTargets.Any())
                return true;
            else if (tryBoard.AtariTargets.Count == 1)
            {
                Group atariTarget = tryBoard.AtariTargets.First();
                if (tryBoard.GetDiagonalNeighbours().Any(n => tryBoard[n] == c))
                {
                    Board b = ImmovableHelper.MakeMoveAtLiberty(tryBoard, atariTarget, c.Opposite());
                    if (b != null && b.MoveGroupLiberties > 1)
                        return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Multi point suicide move.
        /// Captured more than move group <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanGo_A42" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WuQingYuan_Q31682" />
        /// Four-point group scenario <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_TianLongTu_Q16604" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WuQingYuan_Q31435" />
        /// Two-point atari move <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanGo_A82_101Weiqi" />
        /// Atari on next move <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WuQingYuan_Q30935" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanGo_A171_101Weiqi" />
        /// Check atari by previous move group <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_TianLongTu_Q16424_2" />
        /// Move group binding <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanQiJing_Weiqi101_B19_2" />
        /// Two-point atari covered eye <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_GuanZiPu_A32" />
        /// Suicide at covered eye <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WuQingYuan_Q31499_2" />
        /// No hope of escape <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_TianLongTu_Q17132_2" />
        /// Check for recursion <see cref="UnitTestProject.CheckForRecursionTest.CheckForRecursionTest_Scenario_XuanXuanGo_A27" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_GuanZiPu_Q14981" />
        /// Eternal life <see cref="UnitTestProject.CheckForRecursionTest.CheckForRecursionTest_Scenario_GuanZiPu_Q14971" />
        /// </summary>
        public static Boolean MultiPointSuicidalMove(GameTryMove tryMove)
        {
            Board tryBoard = tryMove.TryGame.Board;
            Board currentBoard = tryMove.CurrentGame.Board;
            Board capturedBoard = ImmovableHelper.CaptureSuicideGroup(tryBoard);

            //capture at tryBoard
            if (KillerFormationHelper.CheckKoFightAfterSuicidal(tryBoard, capturedBoard))
                return false;

            //killer formations
            if (KillerFormationHelper.SuicidalKillerFormations(tryBoard, currentBoard, capturedBoard))
                return false;

            //no hope of escape
            return true;
        }
        #endregion

        #region leap move
        /// <summary>
        /// Leap moves are moves two spaces away from the closest neighbour stone of same content.
        /// <see cref="UnitTestProject.LeapMoveTest.LeapMoveTest_Scenario_XuanXuanQiJing_A1" />
        /// Check non killable group <see cref="UnitTestProject.LeapMoveTest.LeapMoveTest_Scenario_XuanXuanGo_A23" />
        /// Check for kill move by survival <see cref="UnitTestProject.LifeCheckTest.LifeCheckTest_Scenario_GuanZiPu_B3" />
        /// Not redundant leap move <see cref="UnitTestProject.LeapMoveTest.LeapMoveTest_Scenario_GuanZiPu_B3" />
        /// </summary>

        public static Boolean SurvivalLeapMove(GameTryMove tryMove)
        {
            Board tryBoard = tryMove.TryGame.Board;
            Point move = tryMove.Move;
            Content c = tryBoard.MoveGroup.Content;

            if (!tryMove.IsNegligible)
                return false;

            if (tryBoard.GetStoneAndDiagonalNeighbours().Any(n => tryBoard[n] == c))
                return false;

            //find closest points within two spaces
            List<Point> closestNeighbours = tryBoard.GetClosestPoints(move, c);
            if (closestNeighbours.Count == 0) return false;

            //validate if leap move is redundant
            if (closestNeighbours.All(n => !LinkHelper.ValidateLeapMove(tryBoard, move, n)))
                return true;

            return false;
        }

        public static Boolean KillLeapMove(GameTryMove tryMove)
        {
            //test if opponent move at same point is suicidal
            GameTryMove opponentMove = tryMove.MakeMoveWithOpponentAtSamePoint();
            if (opponentMove != null)
                return SurvivalLeapMove(opponentMove);
            return false;
        }
        #endregion

        #region neutral point
        /// <summary>
        /// Neutral points are moves that cannot create eye for the survival group. 
        /// <see cref="UnitTestProject.NeutralPointMoveTest.NeutralPointMoveTest_Scenario_WuQingYuan_Q30935" />
        /// </summary>
        public static Boolean NeutralPointSurvivalMove(GameTryMove tryMove, GameTryMove opponentMove = null)
        {
            if (opponentMove == null && !tryMove.IsNegligible && EssentialAtariAtCoveredEye(tryMove))
                return false;
            //validate neutral point
            return ValidateNeutralPoint(tryMove);
        }

        /// <summary>
        /// Neutral point kill moves - Check if neutral point from point of view of survival
        /// <see cref="UnitTestProject.NeutralPointMoveTest.NeutralPointMoveTest_Scenario_XuanXuanGo_B12_2" />
        /// <see cref="UnitTestProject.NeutralPointMoveTest.NeutralPointMoveTest_Scenario_XuanXuanGo_Q18500_2" />
        /// </summary>
        public static Boolean NeutralPointKillMove(GameTryMove tryMove)
        {
            if (!tryMove.IsNegligible && EssentialAtariAtCoveredEye(tryMove))
                return false;
            //make move from perspective of survival
            GameTryMove opponentMove = tryMove.MakeMoveWithOpponentAtSamePoint();
            if (opponentMove == null) return false;

            Boolean isNeutralPoint = NeutralPointSurvivalMove(opponentMove, tryMove);
            if (isNeutralPoint)
            {
                //must have neutral point
                if (MustHaveNeutralPoint(tryMove, opponentMove))
                    tryMove.MustHaveNeutralPoint = true;
            }
            return isNeutralPoint;
        }

        /// <summary>
        /// Covered point suicidal move.
        /// <see cref="UnitTestProject.SurvivalTigerMouthMoveTest.RedundantTigerMouthMove_20221214_5" />
        /// </summary>
        public static Boolean CoveredPointSuicidalMove(GameTryMove tryMove)
        {
            Board tryBoard = tryMove.TryGame.Board;
            Content c = tryBoard.MoveGroup.Content;
            if (tryBoard.MoveGroupLiberties != 1 || tryBoard.MoveGroup.Points.Count != 1) return false;
            if (!tryBoard.PointWithinMiddleArea()) return false;

            List<Point> coveredPoints = tryBoard.GetDiagonalNeighbours().Where(q => tryBoard[q] == c).ToList();
            if (coveredPoints.Count != 2) return false;
            if (coveredPoints[0].x == coveredPoints[1].x || coveredPoints[0].y == coveredPoints[1].y) return false;
            foreach (Point p in tryBoard.GetDiagonalNeighbours())
            {
                if (tryBoard[p] == c.Opposite()) continue;
                Group killerGroup = GroupHelper.GetKillerGroupFromCache(tryBoard, p, c.Opposite());
                if (killerGroup == null) continue;
                if (killerGroup == GroupHelper.GetKillerGroupFromCache(tryBoard, tryMove.Move, c.Opposite())) continue;

                if (tryBoard.GetGroupsFromPoints(coveredPoints).Any(n => ImmovableHelper.CheckConnectAndDie(tryBoard, n)))
                    continue;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Essential atari at covered eye.
        /// <see cref="UnitTestProject.NeutralPointMoveTest.NeutralPointMoveTest_Scenario4dan17" />
        /// Check neighbour groups <see cref="UnitTestProject.BaseLineSurvivalMoveTest.BaseLineSurvivalMoveTest_Scenario_TianLongTu_Q16456" />
        /// </summary>
        private static Boolean EssentialAtariAtCoveredEye(GameTryMove tryMove)
        {
            Board tryBoard = tryMove.TryGame.Board;
            if (tryMove.AtariResolved || tryMove.Captured || tryBoard.MoveGroupLiberties == 1) return true;
            if (tryBoard.AtariTargets.Count != 1) return true;

            Group atariTarget = tryBoard.AtariTargets.First();
            if (!KoHelper.IsKoFight(tryBoard, atariTarget)) return true;

            //check neighbour groups
            Board b = ImmovableHelper.CaptureSuicideGroup(tryBoard, atariTarget);
            if (WallHelper.StrongNeighbourGroups(b))
                return false;
            return true;
        }

        /// <summary>
        /// Must have neutral point.
        /// Neutral point at small tiger mouth <see cref="UnitTestProject.MustHaveNeutralMoveTest.MustHaveNeutralMoveTest_Scenario_Corner_A27" />
        /// Neutral point at big tiger mouth <see cref="UnitTestProject.MustHaveNeutralMoveTest.MustHaveNeutralMoveTest_Scenario5dan27_Variation" />
        /// Negative example <see cref="UnitTestProject.MustHaveNeutralMoveTest.MustHaveNeutralMoveTest_Scenario_XuanXuanGo_A27" />
        /// Check if atari <see cref="UnitTestProject.MustHaveNeutralMoveTest.MustHaveNeutralMoveTest_Scenario_XuanXuanGo_A23" />
        /// <see cref="UnitTestProject.MustHaveNeutralMoveTest.MustHaveNeutralMoveTest_Scenario_Corner_A68" />
        /// Two must have neutral moves <see cref="UnitTestProject.MustHaveNeutralMoveTest.MustHaveNeutralMoveTest_Scenario_GuanZiPu_Weiqi101_19138" />
        /// Generic neutral move with must have neutral move <see cref="UnitTestProject.MustHaveNeutralMoveTest.MustHaveNeutralMoveTest_Scenario_Corner_A68_2" />
        /// </summary>
        private static Boolean MustHaveNeutralPoint(GameTryMove tryMove, GameTryMove opponentMove)
        {
            Board tryBoard = tryMove.TryGame.Board;
            Board currentBoard = tryMove.CurrentGame.Board;
            Board opponentBoard = opponentMove.TryGame.Board;
            Point move = tryBoard.Move.Value;
            Content c = tryBoard.MoveGroup.Content;

            //neutral point at small tiger mouth
            List<Point> eyePoint = opponentBoard.GetStoneNeighbours().Where(n => EyeHelper.FindEye(opponentBoard, n, c.Opposite())).ToList();
            if (eyePoint.Any(n => !StrongGroupsAtMustHaveMove(tryBoard, n)))
                return true;
            //neutral point at big tiger mouth
            (Boolean suicide, Board suicideBoard) = ImmovableHelper.SuicideAtBigTigerMouth(tryMove);
            if (suicide)
            {
                if (suicideBoard == null) return true;
                if (MustHaveMoveAtBigTigerMouth(suicideBoard, tryMove))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Must have move at big tiger mouth.        
        /// Liberties more than one <see cref="UnitTestProject.MustHaveNeutralMoveTest.MustHaveNeutralMoveTest_Scenario_XuanXuanQiJing_Weiqi101_7245" />
        /// <see cref="UnitTestProject.MustHaveNeutralMoveTest.MustHaveNeutralMoveTest_Scenario_XuanXuanGo_A23" />
        /// Strong groups at tiger mouth <see cref="UnitTestProject.MustHaveNeutralMoveTest.MustHaveNeutralMoveTest_Scenario_Corner_A68" />
        /// <see cref="UnitTestProject.MustHaveNeutralMoveTest.MustHaveNeutralMoveTest_Scenario_TianLongTu_Q17136" />
        /// <see cref="UnitTestProject.MustHaveNeutralMoveTest.MustHaveNeutralMoveTest_Scenario_Corner_A84" />
        /// Capture at liberty <see cref="UnitTestProject.MustHaveNeutralMoveTest.MustHaveNeutralMoveTest_Scenario_TianLongTu_Q17132" />
        /// </summary>
        private static Boolean MustHaveMoveAtBigTigerMouth(Board suicideBoard, GameTryMove tryMove)
        {
            Point move = tryMove.Move;
            Board tryBoard = tryMove.TryGame.Board;
            Board currentBoard = tryMove.CurrentGame.Board;
            Content c = tryMove.MoveContent;

            Point suicideMove = suicideBoard.Move.Value;
            //liberties more than one
            if (suicideBoard.MoveGroupLiberties > 1)
                return true;

            //strong groups at tiger mouth
            if (!StrongGroupsAtMustHaveMove(tryBoard, suicideMove))
                return true;

            //capture at liberty
            List<Group> eyeGroups = LinkHelper.GetPreviousMoveGroup(currentBoard, tryBoard).Where(e => e.Liberties.Count == 2).ToList();
            IEnumerable<Point> moves = eyeGroups.Select(e => e.Liberties.First(n => !n.Equals(move)));
            if (moves.Any(p => tryBoard.GetGroupsFromStoneNeighbours(p, c.Opposite()).Any(n => !n.Equals(tryBoard.MoveGroup) && n.Liberties.Count == 1)))
                return true;
            return false;
        }

        /// <summary>
        /// Strong neighbour groups at tiger mouth for must-have move.
        /// <see cref="UnitTestProject.MustHaveNeutralMoveTest.MustHaveNeutralMoveTest_Scenario5dan27_3" />
        /// <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_20221024_4" />
        /// </summary>
        private static Boolean StrongGroupsAtMustHaveMove(Board tryBoard, Point tigerMouth)
        {
            Content c = tryBoard.MoveGroup.Content;
            Board board = tryBoard.MakeMoveOnNewBoard(tigerMouth, c);
            if (board == null) board = tryBoard;
            if (!WallHelper.StrongNeighbourGroups(board, tigerMouth, c))
                return false;

            //check liberty fight
            if (CheckLibertyFightAtMustHaveMove(board))
                return false;
            return true;
        }

        /// <summary>
        /// Check liberty fight at covered eye.
        /// <see cref="UnitTestProject.MustHaveNeutralMoveTest.MustHaveNeutralMoveTest_20221128_2" />
        /// <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_x" />
        /// <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_x_2" />
        /// </summary>
        private static Boolean CheckLibertyFightAtCoveredEye(Board currentBoard, Point eye, Content c)
        {
            Group group = currentBoard.GetGroupsFromStoneNeighbours(eye, c.Opposite()).FirstOrDefault();
            if (group == null) return false;
            List<Group> groups = LinkHelper.GetAllDiagonalGroups(currentBoard, group).ToList();
            foreach (Group gr in groups)
            {
                (_, List<Point> diagonals) = LinkHelper.FindDiagonalCut(currentBoard, gr);
                if (diagonals == null) continue;
                if (diagonals.Any(n => !WallHelper.IsStrongGroup(currentBoard, currentBoard.GetGroupAt(n))))
                    continue;
                return true;
            }
            return false;
        }

        private static Boolean CheckLibertyFightAtCoveredEye(GameTryMove tryMove)
        {
            Board tryBoard = tryMove.TryGame.Board;
            Board currentBoard = tryMove.CurrentGame.Board;
            Content c = tryMove.MoveContent;
            if (tryBoard.GetStoneNeighbours().Any(n => EyeHelper.FindCoveredEye(tryBoard, n, c) && CheckLibertyFightAtCoveredEye(currentBoard, n, c)))
                return true;
            return false;
        }

        /// <summary>
        /// Check liberty fight at must have move.
        /// </summary>
        private static Boolean CheckLibertyFightAtMustHaveMove(Board board)
        {
            Content c = board.MoveGroup.Content;
            Point move = board.Move.Value;
            return CheckLibertyFightAtCoveredEye(board, move, c.Opposite());
        }

        /// <summary>
        /// Validate neutral point by checking if move creates eye for survival at any of the stone and diagonal neighbours.
        /// Check link for groups <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_Scenario_XuanXuanQiJing_Weiqi101_18497" />
        /// <see cref="UnitTestProject.NeutralPointMoveTest.NeutralPointMoveTest_Scenario_XuanXuanQiJing_Weiqi101_7245" />
        /// </summary>
        public static Boolean ValidateNeutralPoint(GameTryMove tryMove)
        {
            Board currentBoard = tryMove.CurrentGame.Board;
            Board tryBoard = tryMove.TryGame.Board;
            //ensure eye cannot be created at any stone or diagonal neighbours
            if (!WallHelper.NoEyeForSurvivalAtNeighbourPoints(tryBoard))
                return false;
            //check link for groups
            if (LinkHelper.PossibleLinkForGroups(tryBoard, currentBoard))
                return false;
            //check for double ko
            if (KoHelper.NeutralPointDoubleKo(tryBoard, currentBoard))
                return false;
            //check reverse ko for neutral point
            if (KoHelper.CheckReverseKoForNeutralPoint(tryBoard))
                return false;
            //check liberty fight
            if (CheckLibertyFightAtCoveredEye(tryMove))
                return false;
            GameTryMove opponentMove = tryMove.MakeMoveWithOpponentAtSamePoint();
            if (opponentMove != null && CoveredPointSuicidalMove(opponentMove))
                return false;
            return true;
        }

        #endregion

        #region restore neutral points
        /// <summary>
        /// Neutral points for kill moves have to be restored on end game in order to kill survival group.
        /// No try moves left <see cref="UnitTestProject.MustHaveNeutralMoveTest.MustHaveNeutralMoveTest_Scenario_Side_A20" />
        /// </summary>
        public static void RestoreNeutralMove(Game currentGame, List<GameTryMove> tryMoves, List<GameTryMove> neutralPointMoves)
        {
            if (neutralPointMoves.Count == 0) return;
            Content c = neutralPointMoves.First().MoveContent;
            //remove unnecessary moves
            neutralPointMoves.RemoveAll(n => n.MoveGroupLiberties == 1);
            neutralPointMoves.RemoveAll(n => !n.TryGame.Board.GetStoneAndDiagonalNeighbours().Any(s => n.TryGame.Board[s] == c.Opposite()));
            if (neutralPointMoves.Count == 0) return;
            GameTryMove genericNeutralMove = null;
            //specific neutral point
            GameTryMove specificNeutralMove = GetSpecificNeutralMove(currentGame, neutralPointMoves);
            if (specificNeutralMove != null)
            {
                tryMoves.Add(specificNeutralMove);
                neutralPointMoves.Remove(specificNeutralMove);
            }
            else
            {
                //check pre-atari moves
                Boolean preAtariAdded = false;
                List<GameTryMove> preAtariMoves = neutralPointMoves.Where(n => ImmovableHelper.PreAtariMove(n)).ToList();
                for (int i = preAtariMoves.Count - 1; i >= 0; i--)
                {
                    GameTryMove tryMove = preAtariMoves[i];
                    preAtariMoves.Remove(tryMove);
                    tryMoves.Add(tryMove);
                    neutralPointMoves.Remove(tryMove);
                    preAtariAdded = true;
                }
                if (!preAtariAdded)
                {
                    //generic neutral point
                    genericNeutralMove = GetGenericNeutralMove(currentGame, neutralPointMoves);
                    if (genericNeutralMove != null)
                    {
                        tryMoves.Add(genericNeutralMove);
                        neutralPointMoves.Remove(genericNeutralMove);
                    }
                }
            }
            //must have neutral point
            List<GameTryMove> mustHaveNeutralMoves = neutralPointMoves.Where(n => n.MustHaveNeutralPoint).ToList();
            mustHaveNeutralMoves.ForEach(n => { tryMoves.Add(n); neutralPointMoves.Remove(n); });
            if (neutralPointMoves.Count == 0) return;
            //no try moves left
            if (tryMoves.Count == 0)
                tryMoves.Add(neutralPointMoves.First());
            else if (tryMoves.Count <= 2)
            {
                //check connect and die for last two try moves
                if (tryMoves.Select(t => t.TryGame.Board).All(t => ConnectAndDieEndMove(t) || SuicideGroupNearCapture(t)))
                    tryMoves.Add(neutralPointMoves.First());
            }
        }

        /// <summary>
        /// Connect and die end move.
        /// <see cref="UnitTestProject.RestoreNeutralMoveTest.RestoreNeutralMoveTest_Scenario_Side_A25" />
        /// <see cref="UnitTestProject.RestoreNeutralMoveTest.RestoreNeutralMoveTest_Scenario4dan17" />
        /// <see cref="UnitTestProject.RestoreNeutralMoveTest.RestoreNeutralMoveTest_Scenario_XuanXuanGo_A26" />
        /// </summary>
        private static Boolean ConnectAndDieEndMove(Board tryBoard)
        {
            Content c = tryBoard.MoveGroup.Content;
            (_, Board captureBoard) = ImmovableHelper.ConnectAndDie(tryBoard);
            if (captureBoard == null) return false;
            if (tryBoard.MoveGroupLiberties == 2)
            {
                Board b = ImmovableHelper.MakeMoveAtLiberty(captureBoard, tryBoard.MoveGroup, c);
                if (b == null) return true;
            }
            Boolean eyeFound = tryBoard.MoveGroup.Liberties.Any(n => EyeHelper.FindEye(tryBoard, n));
            if (eyeFound && tryBoard.MoveGroup.Points.Count > 1)
                return true;
            if (!eyeFound && tryBoard.MoveGroup.Points.Count > 3)
                return true;
            return false;
        }

        /// <summary>
        /// Suicide group near capture.
        /// <see cref="UnitTestProject.NeutralPointMoveTest.RestoreNeutralMoveTest_Scenario_Corner_B21" /> 
        /// <see cref="UnitTestProject.NeutralPointMoveTest.RestoreNeutralMoveTest_Scenario_WuQingYuan_Q6150" /> 
        /// <see cref="UnitTestProject.NeutralPointMoveTest.RestoreNeutralMoveTest_Scenario_TianLongTu_Q16490" /> 
        /// </summary>
        private static Boolean SuicideGroupNearCapture(Board board)
        {
            if (board.MoveGroupLiberties < 2 || board.MoveGroupLiberties > 3) return false;
            if (!WallHelper.IsStrongGroup(board)) return false;
            foreach (Group ngroup in board.GetNeighbourGroups())
            {
                if (ngroup.Liberties.Count > 2 || WallHelper.IsNonKillableGroup(board, ngroup)) continue;
                foreach (Group targetGroup in AtariHelper.AtariByGroup(ngroup, board))
                {
                    Board b = ImmovableHelper.CaptureSuicideGroup(board, targetGroup);
                    if (!WallHelper.IsStrongGroup(b, board.MoveGroup))
                        return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Get specific neutral move to target survival groups with limited liberties.
        /// <see cref="UnitTestProject.SpecificNeutralMoveTest.SpecificNeutralMoveTest_Scenario_XuanXuanQiJing_Weiqi101_B51" />
        /// </summary>
        public static GameTryMove GetSpecificNeutralMove(Game g, List<GameTryMove> neutralPointMoves)
        {
            GameTryMove gameTryMove = null;
            List<Group> killerGroups = GroupHelper.GetKillerGroups(g.Board);
            List<Group> immovableGroups = IsImmovableKill(g, killerGroups).ToList();
            if (immovableGroups.Any())
                gameTryMove = immovableGroups.Select(gr => SpecificKillWithImmovablePoints(g.Board, neutralPointMoves, gr)).FirstOrDefault(n => n != null);
            else
                gameTryMove = SpecificKillWithLibertyFight(g.Board, neutralPointMoves, killerGroups);
            return gameTryMove;
        }

        /// <summary>
        /// Conditions for specific kill with immovable points. <see cref="UnitTestProject.SpecificNeutralMoveTest.SpecificNeutralMoveTest_Scenario_XuanXuanGo_A54" />
        /// Covered eye liberty <see cref="UnitTestProject.SpecificNeutralMoveTest.SpecificNeutralMoveTest_Scenario_XuanXuanGo_A54_3" />
        /// One liberty <see cref="UnitTestProject.SpecificNeutralMoveTest.SpecificNeutralMoveTest_Scenario_TianLongTu_Q16827_4" />
        /// </summary>
        public static IEnumerable<Group> IsImmovableKill(Game g, List<Group> killerGroups)
        {
            foreach (Group killerGroup in killerGroups)
            {
                Content c = killerGroup.Content;
                if (!GroupHelper.IsLibertyGroup(killerGroup, g.Board)) continue;
                List<Group> ngroups = g.Board.GetNeighbourGroups(killerGroup).Where(n => n.Liberties.Count == 3).ToList();
                foreach (Group ngroup in ngroups)
                {
                    List<Point> killerLiberties = ngroup.Liberties.Where(n => GroupHelper.GetKillerGroupFromCache(g.Board, n, c.Opposite()) == killerGroup).ToList();
                    if (killerLiberties.Count >= 1 || killerLiberties.Count <= 2)
                        yield return killerGroup;
                }
            }
        }

        /// <summary>
        /// Specific kill with immovable points.
        /// Survival group has liberty less or equals to two <see cref="UnitTestProject.SpecificNeutralMoveTest.SpecificNeutralMoveTest_Scenario5dan27" />
        /// <see cref="UnitTestProject.SpecificNeutralMoveTest.SpecificNeutralMoveTest_Scenario_TianLongTu_Q16735" />
        /// At least one liberty shared with killer group <see cref="UnitTestProject.SpecificNeutralMoveTest.SpecificNeutralMoveTest_Scenario_XuanXuanGo_A54_2" />
        /// <see cref="UnitTestProject.SpecificNeutralMoveTest.SpecificNeutralMoveTest_Scenario_XuanXuanGo_A54" />
        /// Check one liberty group <see cref="UnitTestProject.SpecificNeutralMoveTest.SpecificNeutralMoveTest_Scenario_XuanXuanQiJing_Weiqi101_B51_2" />
        /// </summary>
        public static GameTryMove SpecificKillWithImmovablePoints(Board board, List<GameTryMove> neutralPointMoves, Group killerGroup)
        {
            Content c = killerGroup.Content;
            List<Point> contentPoints = killerGroup.Points.Where(t => board[t] == c).ToList();
            List<Point> killerLiberties = killerGroup.Points.Where(p => board[p] == Content.Empty).ToList();

            HashSet<Group> groups = new HashSet<Group>();
            for (int i = 0; i <= neutralPointMoves.Count - 1; i++)
            {
                GameTryMove neutralPointMove = neutralPointMoves[i];
                Board tryBoard = neutralPointMove.TryGame.Board;
                IEnumerable<Group> targetGroups = tryBoard.GetGroupsFromStoneNeighbours();
                foreach (Group group in targetGroups)
                {
                    if (groups.Contains(group)) continue;
                    groups.Add(group);
                    if (group.Liberties.Count != 2) continue;
                    List<Point> sharedLiberties = group.Liberties.Intersect(killerLiberties).ToList();
                    if (!(sharedLiberties.Count >= 1 && sharedLiberties.Count <= 2)) continue;
                    if (sharedLiberties.All(p => ImmovableHelper.IsSuicidalMove(tryBoard, p, c.Opposite())))
                        return neutralPointMove;

                    //check one liberty group
                    List<Group> oneLibertyGroup = board.GetNeighbourGroups(group).Where(n => n.Liberties.Count == 1).ToList();
                    if (oneLibertyGroup.Count != 1) continue;
                    Point? q = sharedLiberties.FirstOrDefault(n => !oneLibertyGroup.First().Liberties.Contains(n));
                    if (q != null && ImmovableHelper.IsSuicidalMove(tryBoard, q.Value, c.Opposite()))
                        return neutralPointMove;
                }
            }
            return null;
        }

        /// <summary>
        /// Specific kill with liberty fight.
        /// Find neighbour groups at diagonal cut <see cref="UnitTestProject.SpecificNeutralMoveTest.SpecificNeutralMoveTest_20221017_5" />
        /// <see cref="UnitTestProject.SpecificNeutralMoveTest.SpecificNeutralMoveTest_Scenario3kyu24_3" />
        /// <see cref="UnitTestProject.SpecificNeutralMoveTest.SpecificNeutralMoveTest_Scenario3kyu24_5" />
        /// <see cref="UnitTestProject.DailyGoProblems.DailyGoProblems_20221017_5" />
        /// Target group contains killer group <see cref="UnitTestProject.SpecificNeutralMoveTest.SpecificNeutralMoveTest_Scenario_TianLongTu_Q2413" />
        /// <see cref="UnitTestProject.SpecificNeutralMoveTest.SpecificNeutralMoveTest_Scenario_TianLongTu_Q16827" />
        /// Real solid eye found <see cref="UnitTestProject.SpecificNeutralMoveTest.SpecificNeutralMoveTest_Scenario_XuanXuanGo_B7" />
        /// <see cref="UnitTestProject.SpecificNeutralMoveTest.SpecificNeutralMoveTest_Scenario3kyu24" />
        /// </summary>
        public static GameTryMove SpecificKillWithLibertyFight(Board board, List<GameTryMove> neutralPointMoves, List<Group> killerGroups)
        {
            if (neutralPointMoves.Count == 0) return null;
            Content c = neutralPointMoves.First().MoveContent;
            //get any neutral point move next to target group
            GameTryMove neutralPointMove = neutralPointMoves.FirstOrDefault(t => t.TryGame.Board.GetGroupsFromStoneNeighbours().Count > 0);
            if (neutralPointMove == null) return null;
            Board tryBoard = neutralPointMove.TryGame.Board;
            foreach (Group targetGroup in tryBoard.GetGroupsFromStoneNeighbours())
            {
                List<Point> nliberties = null;
                //find neighbour groups at diagonal cut
                (_, List<Point> diagonals) = LinkHelper.FindDiagonalCut(tryBoard, targetGroup);
                if (diagonals != null)
                {
                    //get the group other than neutral point group
                    Group ngroup = tryBoard.GetGroupsFromPoints(diagonals).FirstOrDefault(gr => !gr.Equals(tryBoard.MoveGroup) && !WallHelper.IsNonKillableGroup(tryBoard, gr));
                    if (ngroup == null) continue;
                    nliberties = ngroup.Liberties.ToList();

                    //compare liberties to see if target group can be killed
                    if (nliberties.Count == targetGroup.Liberties.Count + 1)
                        return neutralPointMove;
                }
                else
                {
                    //target group contains killer group
                    List<Group> kgroups = killerGroups.Where(gr => board.GetNeighbourGroups(gr).Contains(board.GetCurrentGroup(targetGroup))).ToList();
                    if (kgroups.Count != 1) continue;
                    Group kgroup = kgroups.First();
                    if (!kgroup.Points.Any(p => tryBoard[p] == c && tryBoard.GetGroupAt(p).Liberties.Count > 1)) continue;
                    nliberties = kgroup.Points.Where(p => tryBoard[p] == Content.Empty).ToList();

                    //compare liberties to see if target group can be killed
                    if (nliberties.Count == targetGroup.Liberties.Count)
                        return neutralPointMove;
                }

                //real eye found
                if (nliberties.Any(n => EyeHelper.FindRealEyeWithinEmptySpace(tryBoard, n, c)))
                    return neutralPointMove;
            }
            return null;
        }

        /// <summary>
        /// Get generic neutral moves that are not specific. Killer group required.
        /// <see cref="UnitTestProject.GenericNeutralMoveTest.GenericNeutralMoveTest_XuanXuanGo_A55" />
        /// Check covered eye <see cref="UnitTestProject.GenericNeutralMoveTest.GenericNeutralMoveTest_Scenario_XuanXuanQiJing_Weiqi101_18410" />
        /// </summary>
        public static GameTryMove GetGenericNeutralMove(Game g, List<GameTryMove> neutralPointMoves)
        {
            if (neutralPointMoves.Count == 0) return null;
            Content c = neutralPointMoves.First().MoveContent;
            List<Group> killerGroups = GroupHelper.GetKillerGroups(g.Board);
            foreach (Group killerGroup in killerGroups)
            {
                if (!GroupHelper.IsLibertyGroup(killerGroup, g.Board)) continue;
                //cover all neutral points
                Board coveredBoard = new Board(g.Board);
                neutralPointMoves.ForEach(m => coveredBoard[m.Move] = c);

                //order by liberties
                List<Group> orderedGroups = g.Board.GetNeighbourGroups(killerGroup).OrderBy(n => coveredBoard.GetGroupLiberties(n).Count).ToList();
                foreach (Point p in g.Board.GetLibertiesOfGroups(orderedGroups))
                {
                    GameTryMove neutralMove = neutralPointMoves.FirstOrDefault(n => n.Move.Equals(p));
                    if (neutralMove == null) continue;

                    //check neighbour groups
                    if (WallHelper.StrongNeighbourGroups(coveredBoard, neutralMove.Move, c)) continue;

                    //check covered eye
                    if (LinkHelper.GetGroupDiagonals(g.Board, killerGroup).Any(n => EyeHelper.FindCoveredEye(g.Board, n.Move, c.Opposite())))
                        return neutralMove;

                    //check for diagonal cut
                    foreach (Point q in killerGroup.Points)
                    {
                        if (g.Board[q] != killerGroup.Content) continue;
                        if (!g.Board.GetGroupsFromStoneNeighbours(q).Any(n => LinkHelper.FindDiagonalCut(g.Board, n).Item1 != null)) continue;
                        return neutralMove;
                    }
                }
            }
            return null;
        }
        #endregion

        #region redundant tiger mouth
        /// <summary>
        /// Redundant tiger mouth.
        /// <see cref="UnitTestProject.SurvivalTigerMouthMoveTest.SurvivalTigerMouthMoveTest_Scenario_XuanXuanQiJing_Weiqi101_18473" />
        /// </summary>
        public static Boolean RedundantTigerMouthMove(GameTryMove tryMove)
        {
            //find tiger mouth
            if (RedundantTigerMouth(tryMove))
                return true;

            //find tiger mouth for opponent
            GameTryMove opponentMove = tryMove.MakeMoveWithOpponentAtSamePoint();
            if (opponentMove != null && RedundantTigerMouth(opponentMove, tryMove))
                return true;
            return false;
        }

        /// <summary>
        /// Redundant tiger mouth.
        /// Check killer groups <see cref="UnitTestProject.SurvivalTigerMouthMoveTest.RedundantTigerMouthMove_Scenario_WuQingYuan_Q15126" />
        /// <see cref="UnitTestProject.SurvivalTigerMouthMoveTest.SurvivalTigerMouthMoveTest_Scenario_XuanXuanGo_B31" />
        /// Check one point atari move <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WuQingYuan_Q31428" />
        /// Opponent move at tiger mouth <see cref="UnitTestProject.SurvivalTigerMouthMoveTest.RedundantTigerMouthMove_Scenario_XuanXuanGo_A151_101Weiqi" />
        /// <see cref="UnitTestProject.SurvivalTigerMouthMoveTest.SurvivalTigerMouthMoveTest_Scenario_Nie67" />
        /// </summary>
        private static Boolean RedundantTigerMouth(GameTryMove tryMove, GameTryMove opponentMove = null)
        {
            Point move = tryMove.Move;
            Board tryBoard = tryMove.TryGame.Board;
            Board currentBoard = tryMove.CurrentGame.Board;
            Content c = tryMove.MoveContent;
            //ensure is tiger mouth
            if (tryBoard.MoveGroup.Points.Count != 1 || !tryMove.IsNegligible) return false;
            if (ImmovableHelper.IsConfirmTigerMouth(currentBoard, tryBoard) == null) return false;

            //check eye points at diagonals of tiger mouth
            List<Point> diagonalPoints = ImmovableHelper.GetDiagonalsOfTigerMouth(tryBoard, move, c.Opposite()).Where(e => tryBoard[e] != c.Opposite()).ToList();
            if (diagonalPoints.Count == 0) return false;

            (Boolean suicidal, Board capturedBoard) = ImmovableHelper.IsSuicidalOnCapture(tryBoard);
            if (suicidal || capturedBoard == null) return false;

            foreach (Point d in diagonalPoints)
            {
                //find immovable point at diagonal
                if (!ImmovableHelper.IsImmovablePoint(currentBoard, d, c.Opposite()))
                    continue;

                if (opponentMove == null)
                {
                    //check killer groups
                    Group diagonalKillerGroup = GroupHelper.GetKillerGroupOfDirectNeighbourGroups(currentBoard, d, c.Opposite());
                    if (diagonalKillerGroup == null)
                        continue;
                    HashSet<Group> opponentGroups = currentBoard.GetGroupsFromPoints(diagonalKillerGroup.Points.Where(n => currentBoard[n] == c).ToList());
                    if (opponentGroups.Any(n => !ImmovableHelper.CheckCaptureSecure(currentBoard, n)))
                        continue;

                    //check one point atari move
                    if (KillerFormationHelper.OnePointAtariMove(tryBoard, currentBoard)) 
                        continue;

                    if (CoveredPointSuicidalMove(tryMove))
                        continue;
                    if (KillCoveredEyeAtDiagonal(tryBoard, currentBoard))
                        continue;
                    return true;
                }
                else
                {
                    //opponent move at tiger mouth
                    if (BothAliveHelper.CheckForBothAliveAtMove(opponentMove.TryGame.Board))
                        continue;
                    return true;
                }
            }

            //no diagonal tiger mouth
            if (TigerMouthWithoutDiagonalMouth(tryMove, capturedBoard))
                return true;

            return false;
        }


        /// <summary>
        /// Redundant tiger mouth without diagonal mouth.
        /// Check for covered eye <see cref="UnitTestProject.SurvivalTigerMouthMoveTest.RedundantTigerMouthMove_Scenario_TianLongTu_Q16738" />
        /// <see cref="UnitTestProject.SurvivalTigerMouthMoveTest.RedundantTigerMouthMove_Scenario_WindAndTime_Q30225" />
        /// Check for three groups <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_GuanZiPu_Q1970" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WuQingYuan_Q30935" />
        /// Check for strong neighbour groups <see cref="UnitTestProject.SurvivalTigerMouthMoveTest.RedundantTigerMouthMove_Scenario3dan22" />
        /// <see cref="UnitTestProject.SurvivalTigerMouthMoveTest.RedundantTigerMouthMove_Scenario_XuanXuanGo_A28" />
        /// Check for liberty fight <see cref="UnitTestProject.SurvivalTigerMouthMoveTest.RedundantTigerMouthMove_20221220_7" />
        /// Check for killer group <see cref="UnitTestProject.SurvivalTigerMouthMoveTest.RedundantTigerMouthMove_20230505_8" />
        /// </summary>
        private static Boolean TigerMouthWithoutDiagonalMouth(GameTryMove tryMove, Board capturedBoard)
        {
            Point move = tryMove.Move;
            Board currentBoard = tryMove.CurrentGame.Board;
            Board tryBoard = tryMove.TryGame.Board;
            Content c = tryMove.MoveContent;

            //suicide within real eye at suicidal redundant move
            if (EyeHelper.FindSemiSolidEye(move, capturedBoard, c.Opposite()).Item1)
                return false;
            //check for covered eye
            if (EyeHelper.IsCovered(tryBoard, move, c.Opposite()))
                return false;

            //check for three groups
            if (KillerFormationHelper.ThreeOpponentGroupsAtMove(tryBoard))
                return false;

            //check for strong neighbour groups
            Boolean strongGroups = WallHelper.HostileNeighbourGroups(currentBoard, move, c) && capturedBoard.MoveGroupLiberties > 2;
            if (!strongGroups)
                return false;

            //check for liberty fight
            List<Group> groups = LinkHelper.GetAllDiagonalGroups(capturedBoard, capturedBoard.MoveGroup).ToList();
            if (groups.Any(n => LinkHelper.FindDiagonalCut(capturedBoard, n).Item1 != null))
                return false;

            //check for killer group
            if (!GroupHelper.IsSingleGroupWithinKillerGroup(tryBoard, tryBoard.MoveGroup))
                return false;
            return true;
        }

        /// <summary>
        /// Kill covered eye at diagonal point.
        /// <see cref="UnitTestProject.SurvivalTigerMouthMoveTest.RedundantTigerMouthMove_20221231_6" />
        /// <see cref="UnitTestProject.SurvivalTigerMouthMoveTest.RedundantTigerMouthMove_20230423_8" />
        /// </summary>
        private static Boolean KillCoveredEyeAtDiagonal(Board tryBoard, Board currentBoard)
        {
            Content c = tryBoard.MoveGroup.Content;
            List<Point> diagonalEyes = tryBoard.GetDiagonalNeighbours().Where(n => EyeHelper.FindUncoveredEye(currentBoard, n, c.Opposite()) && EyeHelper.FindCoveredEye(tryBoard, n, c.Opposite())).ToList();
            foreach (Point e in diagonalEyes)
            {
                if (!tryBoard.GetDiagonalNeighbours(e).Any(n => tryBoard[n] == Content.Empty)) continue;
                if (tryBoard.GetGroupsFromStoneNeighbours(e, c).Count(n => n.Liberties.Count <= 2) >= 2)
                    return true;
            }
            return false;
        }

        #endregion

        #region redundant eye diagonal
        /// <summary>
        /// Survival eye diagonal move.
        /// <see cref="UnitTestProject.RedundantEyeDiagonalMoveTest.RedundantEyeDiagonalMoveTest_Scenario_XuanXuanQiJing_Weiqi101_18473_2" />
        /// Check diagonals are real eyes <see cref="UnitTestProject.RedundantEyeDiagonalMoveTest.RedundantEyeDiagonalMoveTest_Scenario_SiHuoDaQuan_CornerA29_2" />
        /// <see cref="UnitTestProject.ImmovableTest.ImmovableTest_Scenario_XuanXuanGo_B31" />
        /// Check link to groups <see cref="UnitTestProject.RedundantEyeDiagonalMoveTest.RedundantEyeDiagonalMoveTest_Scenario_WuQingYuan_Q31154" />
        /// </summary>
        public static Boolean SurvivalEyeDiagonalMove(GameTryMove tryMove)
        {
            if (!tryMove.IsNegligible)
                return false;
            Board currentBoard = tryMove.CurrentGame.Board;
            Board tryBoard = tryMove.TryGame.Board;
            Point move = tryMove.Move;
            Content c = GameHelper.GetContentForSurviveOrKill(tryBoard.GameInfo, SurviveOrKill.Survive);

            //get diagonals
            List<Point> diagonals = tryBoard.GetDiagonalNeighbours().Where(q => tryBoard[q] != c).ToList();
            diagonals = diagonals.Where(eye => LinkHelper.PointsBetweenDiagonals(eye, move).All(d => tryBoard[d] == c)).ToList();
            if (diagonals.Count == 0) return false;
            diagonals.RemoveAll(d => GroupHelper.GetKillerGroupOfDirectNeighbourGroups(currentBoard, d, c) == null);
            if (diagonals.Count == 0) return false;

            //make opponent move
            GameTryMove opponentMove = tryMove.MakeMoveWithOpponentAtSamePoint();
            if (opponentMove == null) return false;
            Board opponentBoard = opponentMove.TryGame.Board;
            //check diagonals are real eyes
            if (!diagonals.All(eye => EyeHelper.FindRealEyeWithinEmptySpace(opponentBoard, eye, c)))
                return false;
            //ensure no weak groups
            if (diagonals.Count > 1 && LinkHelper.GetPreviousMoveGroup(currentBoard, opponentBoard).Count(n => n.Liberties.Count <= 2) >= 2)
                return false;

            //check other surrounding points are not possible eyes
            IEnumerable<Point> neighbourPts = tryBoard.GetStoneAndDiagonalNeighbours().Except(diagonals);
            if (neighbourPts.Any(q => !WallHelper.NoEyeForSurvival(tryBoard, q, c)))
                return false;

            //check link to groups other than eye groups
            if (LinkHelper.PossibleLinkForGroups(tryBoard, currentBoard))
                return false;

            if (CoveredPointSuicidalMove(opponentMove))
                return false;

            return true;
        }

        /// <summary>
        /// Kill eye diagonal move.
        /// </summary>
        public static Boolean KillEyeDiagonalMove(GameTryMove tryMove)
        {
            if (!tryMove.IsNegligible)
                return false;
            GameTryMove opponentMove = tryMove.MakeMoveWithOpponentAtSamePoint();
            if (opponentMove != null)
                return SurvivalEyeDiagonalMove(opponentMove);
            return false;
        }
        #endregion

        #region eye filler
        /// <summary>
        /// Survival eye filler moves. Get specific move for group of two to five points. 
        /// </summary>
        public static Boolean SurvivalEyeFillerMove(GameTryMove tryMove)
        {
            Board currentBoard = tryMove.CurrentGame.Board;
            Board tryBoard = tryMove.TryGame.Board;
            Point move = tryMove.Move;
            Content c = tryMove.MoveContent;
            if (!tryMove.IsNegligible) return false;
            Group killerGroup = GroupHelper.GetKillerGroupFromCache(currentBoard, move, c);
            if (killerGroup == null || killerGroup.Points.Count == 1 || killerGroup.Points.Count > 5) return false;
            return SpecificEyeFillerMove(tryMove);
        }

        /// <summary>
        /// Kill eye filler moves. Get specific move for group of three to five points.
        /// </summary>
        public static Boolean KillEyeFillerMove(GameTryMove tryMove)
        {
            Board currentBoard = tryMove.CurrentGame.Board;
            Board tryBoard = tryMove.TryGame.Board;
            Point move = tryMove.Move;
            Content c = tryMove.MoveContent;
            if (!tryMove.IsNegligible) return false;
            Group killerGroup = GroupHelper.GetKillerGroupFromCache(currentBoard, move, c.Opposite());
            if (killerGroup == null || killerGroup.Points.Count <= 2 || killerGroup.Points.Count > 5) return false;
            //make survival move
            GameTryMove opponentMove = tryMove.MakeMoveWithOpponentAtSamePoint();
            if (opponentMove == null) return false;
            return SpecificEyeFillerMove(opponentMove, true);
        }

        /// <summary>
        /// Filler moves without killer group <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_XuanXuanQiJing_A1" />
        /// Filler moves with killer group <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_GuanZiPu_B3" />
        /// Check for one point leap move <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_XuanXuanGo_B10_2" />
        /// <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_Corner_B40" />
        /// <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_WindAndTime_Q30278" />
        /// Check killer leap move <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_TianLongTu_Q16985" />
        /// <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_Corner_A56" />
        /// <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario3dan22" />
        /// Check two-point group <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_Side_B35" />
        /// </summary>
        private static Boolean FillerMoveWithoutKillerGroup(GameTryMove tryMove)
        {
            Board tryBoard = tryMove.TryGame.Board;
            Point move = tryBoard.Move.Value;

            //check generic eye filler move
            if (!GenericEyeFillerMove(tryMove)) return false;

            //check for one point leap move
            if (SiegeScenario(tryBoard, tryBoard.GetClosestPoints(move)))
                return false;
            return true;
        }

        /// <summary>
        /// Siege scenario. At least one closest group targeted by neighbour group.
        /// <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_WindAndTime_Q30278" />
        /// </summary>
        private static Boolean SiegeScenario(Board tryBoard, List<Point> points)
        {
            HashSet<Group> groups = tryBoard.GetGroupsFromPoints(points);
            foreach (Group group in groups)
            {
                HashSet<Group> connectedGroups = LinkHelper.GetAllDiagonalConnectedGroups(tryBoard, group);
                if (connectedGroups.Any(gr => gr.Neighbours.Except(tryBoard.MoveGroup.Points).Count(n => tryBoard[n] == gr.Content.Opposite()) >= 2))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Remove redundant moves that fill eyes instead of creating eyes within eye space for survival.
        /// <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_GuanZiPu_B3" />
        /// <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_GuanZiPu_B3_2" />
        /// Ensure not link for groups <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_WuQingYuan_Q5971" />
        /// Check for ko fight <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_GuanZiPu_A36_3" />
        /// Get stone neighbours only for killer group <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_TianLongTu_Q15017" />
        /// Check any opponent stone at stone and diagonal points <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_XuanXuanGo_Q18500" />
        /// <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_Corner_A56_3" />
        /// Check any opponent stone at neighbour points <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_TianLongTu_Q16827" />
        /// <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_TianLongTu_Q16827_2" />
        /// <see cref="UnitTestProject.BaseLineKillerMoveTest.BaseLineKillerMoveTest_Scenario_TianLongTu_Q16520" />
        /// Check corner point <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_XuanXuanGo_A26" />
        /// <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_Corner_B8" />
        /// <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_XuanXuanQiJing_Weiqi101_7245" />
        /// </summary>
        public static Boolean GenericEyeFillerMove(GameTryMove tryMove)
        {
            Board tryBoard = tryMove.TryGame.Board;
            Board currentBoard = tryMove.CurrentGame.Board;
            Point move = tryMove.Move;
            Content c = tryMove.MoveContent;
            if (!tryMove.IsNegligible) return false;

            //check any opponent stone at stone and diagonal points
            if (tryBoard.GetStoneAndDiagonalNeighbours().Any(n => tryBoard[n] == c.Opposite()))
                return false;

            //ensure not link for groups
            if (LinkHelper.IsAbsoluteLinkForGroups(currentBoard, tryBoard))
                return false;

            //check for increased killer groups
            if (GroupHelper.IncreasedKillerGroups(tryBoard, currentBoard))
                return false;

            //check link for groups
            if (LinkHelper.PossibleLinkForGroups(tryBoard, currentBoard))
                return false;

            //count eyes created at move
            int possibleEyes = KillerFormationHelper.PossibleEyesCreated(currentBoard, move, c);

            foreach (Point p in KillerFormationHelper.GetLibertiesAtMove(tryBoard))
            {
                //check any opponent stone at neighbour points
                if (currentBoard.GetStoneNeighbours(p).Any(n => currentBoard[n] == c.Opposite()))
                    continue;
                //count eyes created at empty neighbour points
                int possibleEyesAtNeighbourPt = KillerFormationHelper.PossibleEyesCreated(currentBoard, p, c);
                //possibility of eyes created more than at try move point
                if (possibleEyesAtNeighbourPt > possibleEyes)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Return specific survival or killer move if killer group contains five points or less. 
        /// Check killer group <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_Phenomena_Q25182" />
        /// <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_GuanZiPu_A37" />
        /// Check immovable at liberties <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_WuQingYuan_Q31602" />
        /// Not link for groups <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_WuQingYuan_Q31537" />
        /// Check increased killer group <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_GuanZiPu_A17_4" />
        /// Group binding <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_GuanZiPu_A16" />
        /// <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_GuanZiPu_A36" />
        /// No neighbour group <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_Corner_A80" />
        /// <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_Corner_A61_2" />
        /// <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_XuanXuanGo_A4" />
        /// Check for atari on neighbour groups <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_GuanZiPu_A36_2" />
        /// Check eye for survival <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_WindAndTime_Q30275_2" />
        /// Check for dead formation <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_TianLongTu_Q16902" />
        /// <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_GuanZiPu_A16" />
        /// Check multiple groups <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_WuQingYuan_Q31646" />
        /// </summary>
        public static Boolean SpecificEyeFillerMove(GameTryMove tryMove, Boolean isOpponent = false)
        {
            Board tryBoard = tryMove.TryGame.Board;
            Board currentBoard = tryMove.CurrentGame.Board;
            Point move = tryMove.Move;
            Content c = tryMove.MoveContent;
            if (!tryMove.IsNegligible) return false;

            //check killer group
            Group killerGroup = GroupHelper.GetKillerGroupOfDirectNeighbourGroups(currentBoard, move, c);
            if (killerGroup == null || currentBoard.GetNeighbourGroups(killerGroup).Any(n => n.Liberties.Count <= 2)) return false;

            //no neighbour group
            List<Point> emptyPoints = killerGroup.Points.Where(p => currentBoard[p] == Content.Empty).ToList();
            if (emptyPoints.Any(p => currentBoard.GetGroupsFromStoneNeighbours(p, c.Opposite()).Count == 0))
                return false;

            //ensure not link for groups
            if (LinkHelper.IsAbsoluteLinkForGroups(currentBoard, tryBoard))
                return false;

            //check increased killer group
            if (GroupHelper.IncreasedKillerGroups(tryBoard, currentBoard))
                return false;

            //check eye for survival
            Boolean eyeForSurvival = currentBoard.GetNeighbourGroups(killerGroup).Count > 1 && emptyPoints.Any(p => currentBoard.GetStoneNeighbours(p).Count(n => currentBoard[n] == Content.Empty) >= 2);
            if (eyeForSurvival) return false;

            //redundant for survival
            if (!isOpponent) return true;

            //check multiple groups
            List<Board> moveBoards = GameHelper.GetMoveBoards(currentBoard, emptyPoints, c.Opposite()).ToList();
            List<Board> multipleGroups = moveBoards.Where(b => !GroupHelper.IsSingleGroupWithinKillerGroup(b, b.MoveGroup)).ToList();
            if (multipleGroups.Any(b => !ImmovableHelper.CheckConnectAndDie(b, b.MoveGroup, false))) return false;

            //check for dead formation
            if (moveBoards.Any(b => KillerFormationHelper.IsKillerFormationFromFunc(b, b.MoveGroup) || KillerFormationHelper.DeadFormationInBothAlive(b, killerGroup)))
                return false;

            moveBoards.RemoveAll(n => multipleGroups.Any(b => b.Move.Equals(n.Move)));
            if (!moveBoards.Any()) return false;

            //select move with max binding
            Point? bestMove = KillerFormationHelper.GetMaxBindingPoint(currentBoard, moveBoards, killerGroup);
            if (bestMove != null && !tryMove.Move.Equals(bestMove))
                return true;

            return false;
        }
        #endregion

        #region redundant ko
        /// <summary>
        /// Redundant survival ko moves <see cref="UnitTestProject.RedundantKoMoveTest.RedundantKoMoveTest_Scenario_XuanXuanGo_A46_101Weiqi" />
        /// <see cref="UnitTestProject.RedundantKoMoveTest.RedundantKoMoveTest_SimpleSeki" />
        /// </summary>
        public static Boolean RedundantSurvivalKoMove(GameTryMove tryMove)
        {
            Board tryBoard = tryMove.TryGame.Board;
            Board currentBoard = tryMove.CurrentGame.Board;
            Content c = tryBoard.MoveGroup.Content;
            if (!KoHelper.IsKoFight(tryBoard)) return false;
            Boolean koEnabled = KoHelper.KoContentEnabled(c, tryBoard.GameInfo);
            if (!koEnabled)
            {
                //check pre-ko moves
                if (tryBoard.singlePointCapture == null) return false;
                //check double ko
                if (!KoHelper.PossibilityOfDoubleKo(tryBoard, currentBoard))
                    return true;
                return false;
            }
            return CheckRedundantKoMove(tryBoard, currentBoard);
        }

        /// <summary>
        /// Check redundant ko move.
        /// </summary>
        public static Boolean CheckRedundantKoMove(Board tryBoard, Board currentBoard)
        {
            Content c = tryBoard.MoveGroup.Content;
            //check redundant ko
            if (!CheckRedundantKo(tryBoard, currentBoard)) return false;
            //check for opponent
            Point? eyePoint = KoHelper.GetKoEyePoint(tryBoard);
            if (eyePoint == null) return false;
            Board opponentBoard = tryBoard.MakeMoveOnNewBoard(eyePoint.Value, c.Opposite(), true);
            if (CheckRedundantKo(opponentBoard, tryBoard))
                return true;
            return false;
        }


        /// <summary>
        /// Check redundant ko. 
        /// ko fight at non killable group <see cref="UnitTestProject.RedundantKoMoveTest.RedundantKoMoveTest_Scenario_Corner_A27" />
        /// <see cref="UnitTestProject.RedundantKoMoveTest.RedundantKoMoveTest_Scenario_GuanZiPu_A4Q11_101Weiqi" />
        /// <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_Scenario_XuanXuanQiJing_A64" />
        /// <see cref="UnitTestProject.RedundantKoMoveTest.RedundantKoMoveTest_20221128" /> 
        /// Check liberty fight <see cref="UnitTestProject.RedundantKoMoveTest.RedundantKoMoveTest_20221128_4" />
        /// <see cref="UnitTestProject.RedundantKoMoveTest.RedundantKoMoveTest_Scenario_XuanXuanQiJing_A38_2" /> 
        /// Target with all non killable groups <see cref="UnitTestProject.RedundantKoMoveTest.RedundantKoMoveTest_Scenario_TianLongTu_Q16693_2" /> 
        /// <see cref="UnitTestProject.RedundantKoMoveTest.RedundantKoMoveTest_x_2" /> 
        /// Check link for groups <see cref="UnitTestProject.RedundantKoMoveTest.RedundantKoMoveTest_Scenario_WindAndTime_Q30152_2" /> 
        /// <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_Scenario_WindAndTime_Q30152" /> 
        /// </summary>
        public static Boolean CheckRedundantKo(Board tryBoard, Board currentBoard)
        {
            Content c = tryBoard.MoveGroup.Content;
            Point? eyePoint = KoHelper.GetKoEyePoint(tryBoard);
            if (eyePoint == null) return false;

            //ko fight at non killable group
            if (KoHelper.IsNonKillableGroupKoFight(tryBoard))
            {
                List<Group> ngroups = tryBoard.GetGroupsFromStoneNeighbours();
                if (LifeCheck.GetTargets(tryBoard).All(t => ngroups.Contains(t) && WallHelper.TargetWithAllNonKillableGroups(tryBoard, t)))
                    return true;
                if (!WallHelper.StrongNeighbourGroups(tryBoard))
                    return false;
                //check liberty fight
                if (CheckLibertyFightAtMustHaveMove(tryBoard))
                    return false;
                //check two liberty group
                if (ngroups.Any(n => CheckTwoLibertyGroupToCaptureNeighbour(tryBoard, currentBoard, n)))
                    return false;
                return true;
            }

            //target with all non killable groups
            if (!WallHelper.TargetWithAllNonKillableGroups(tryBoard))
                return false;

            //real eye at diagonal
            List<Point> diagonals = ImmovableHelper.GetDiagonalsOfTigerMouth(currentBoard, eyePoint.Value, c).Where(q => tryBoard[q] != c).ToList();
            if (diagonals.Any() && !FindRealEyeAtDiagonal(diagonals, currentBoard, c))
                return false;

            //check link for groups
            if (LinkHelper.PossibleLinkForGroups(tryBoard, currentBoard))
                return false;

            return true;
        }

        /// <summary>
        /// Real eye at diagonal.
        /// <see cref="UnitTestProject.RedundantKoMoveTest.RedundantKoMoveTest_Scenario_XuanXuanGo_A151_101Weiqi" /> 
        /// <see cref="UnitTestProject.RedundantKoMoveTest.RedundantKoMoveTest_Scenario_XuanXuanGo_A151_101Weiqi_2" /> 
        /// Three point real eye <see cref="UnitTestProject.RedundantKoMoveTest.RedundantKoMoveTest_Scenario_WindAndTime_Q30188" /> 
        /// <see cref="UnitTestProject.RedundantKoMoveTest.RedundantKoMoveTest_Scenario_WuQingYuan_Q30982" /> 
        /// </summary>
        private static Boolean FindRealEyeAtDiagonal(List<Point> diagonals, Board b, Content c)
        {
            foreach (Point d in diagonals)
            {
                Group killerGroup = GroupHelper.GetKillerGroupFromCache(b, d, c);
                if (killerGroup == null || killerGroup.Points.Count > 2) continue;
                if (EyeHelper.FindRealEyeWithinEmptySpace(b, d, c)) return true;
            }
            return false;
        }

        #endregion
    }
}
