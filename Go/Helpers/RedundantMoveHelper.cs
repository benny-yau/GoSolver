using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Go
{
    public class RedundantMoveHelper
    {
        #region find potential eye
        /// <summary>
        /// Find potential eye. 
        /// Check for killer formations <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_Corner_A113_2" />
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
            if (!EyeHelper.IsCovered(currentBoard, move, c))
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
        /// Eye double atari.
        /// <see cref="UnitTestProject.DailyGoProblems.DailyGoProblems_20250326_8" /> 
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
                List<Group> ngroups = currentBoard.GetGroupsFromStoneNeighbours(p, c.Opposite());
                if (!ngroups.Except(eyeGroups).Any(n => n.Liberties.Count == 2)) continue;
                if (ImmovableHelper.IsSuicidalMove(currentBoard, p, c.Opposite()) || ngroups.Any(n => ImmovableHelper.CheckConnectAndDie(currentBoard, n, false))) continue;
                if (eyeGroups.Any(n => WallHelper.IsNonKillableGroup(currentBoard, n))) continue;
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
        /// Find covered eye move.
        /// Check eye for survival <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_Scenario_WindAndTime_Q29277" /> 
        /// <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_Scenario_Corner_B25" /> 
        /// Check kill opponent <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_Scenario_XuanXuanGo_A34" />
        /// Check possible links <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_Scenario_XuanXuanQiJing_Weiqi101_18497_2" />
        /// <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_Scenario_XuanXuanQiJing_Weiqi101_B74" />
        /// </summary>
        public static Boolean FindCoveredEyeMove(GameTryMove tryMove, GameTryMove opponentMove = null)
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
                if (!EyeHelper.IsCovered(tryBoard, eyePoint, c) || KoHelper.IsKoFight(tryBoard)) return false;
                Board b = new Board(tryBoard);
                b[eyePoint] = c.Opposite();
                eyeGroup = b.GetGroupAt(eyePoint);
            }
            else if (tryBoard.CapturedList.Count == 1 && tryBoard.CapturedPoints.Count() == 2 && EyeHelper.FindCoveredEyeAfterCapture(tryBoard, tryBoard.CapturedList.First()))
            {
                //two-point covered eye
                eyePoint = tryBoard.CapturedPoints.First(q => tryBoard.GetStoneNeighbours().Contains(q));
                if (!EyeHelper.IsCovered(tryBoard, eyePoint, c)) return false;
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
            if (diagonals.Any() && !EyeHelper.FindRealEyeAtDiagonal(diagonals, currentBoard, c))
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

            //check double ko
            if (KoHelper.IsCoveredEyeDoubleKo(tryBoard))
                return false;

            if (opponentMove != null)
            {
                //check no eye for survival for opponent
                Board opponentBoard = opponentMove.TryGame.Board;
                if (!WallHelper.NoEyeForSurvivalAtNeighbourPoints(opponentBoard))
                    return false;

                //check must-have move
                if (!RedundantAtMustHaveMove(opponentBoard, eyePoint))
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
        /// ensure eye groups not suicidal <see cref="UnitTestProject.FillKoEyeMoveTest.FillKoEyeMoveTest_Scenario_Nie20" /> 
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
            //ensure is covered eye
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
            Boolean isKoFight = eyeGroups.Any(e => KoHelper.IsKoFight(currentBoard, e));
            if (!KoHelper.KoContentEnabled(c, tryBoard.GameInfo) && isKoFight)
                return false;

            //ensure eye groups not suicidal
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
                //check covered eye survival
                if (!WallHelper.StrongGroupsAtCoveredBoard(currentBoard, eyeGroups.First()))
                    return false;
            }

            //check double ko
            if (isKoFight)
            {
                Board b = currentBoard.MakeMoveOnNewBoard(move, c.Opposite(), true);
                if (KoHelper.PossibilityOfDoubleKo(b, currentBoard))
                    return false;
                Board b2 = ImmovableHelper.CaptureSuicideGroup(b);
                if (b2 != null && KoHelper.PossibilityOfDoubleKo(b2, b))
                    return false;
            }
            return true;
        }
        #endregion

        #region atari redundant move

        /// <summary>
        /// Atari redundant move.
        /// <see cref="UnitTestProject.AtariRedundantMoveTest.AtariRedundantMoveTest_Scenario_Corner_A9_Ext" />
        /// One liberty move group <see cref="UnitTestProject.AtariRedundantMoveTest.AtariRedundantMoveTest_Scenario_Corner_A68" />
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
            Point atariPoint = tryBoard.OpponentAtStoneNeighbour().FirstOrDefault(n => tryBoard.GetGroupAt(n).Equals(atariTarget));
            if (atariPoint.IsEmpty()) return false;

            Point q = atariTarget.Liberties.First();
            if (!KillerFormationHelper.IsFirstPoint(currentBoard, q, move)) return false;

            //check killer group
            Group killerGroup = GroupHelper.GetDirectKillerGroup(currentBoard, atariPoint, c);
            if (killerGroup == null || currentBoard.GetNeighbourGroups(killerGroup).Any(n => n.Liberties.Count <= 2))
                return false;

            if (!GroupHelper.IsSingleGroupWithinKillerGroup(currentBoard, atariTarget))
                return false;

            //ensure capture secure
            if (!ImmovableHelper.CheckCaptureSecure(tryBoard, atariTarget, true))
                return false;

            //make move at the other liberty
            (Boolean suicidal, Board board) = ImmovableHelper.IsSuicidalMove(q, c, currentBoard);
            if (suicidal) return false;
            Group target = board.GetGroupAt(atariPoint);
            if (!GameTryMove.IsNegligibleForBoard(board, currentBoard, n => !n.Equals(target))) return false;

            //ensure capture secure
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
            else if (opponentTryBoard.MoveGroupLiberties == 2)
            {
                if (OpponentSuicidalConnectAndDie(opponentMove, tryMove))
                    return true;
            }

            if (SuicidalMoveAtNonKillableGroup(opponentMove, tryMove))
                return true;
            return false;
        }

        /// <summary>
        /// Suicidal move at non killable group.
        /// </summary>
        private static Boolean SuicidalMoveAtNonKillableGroup(GameTryMove tryMove, GameTryMove opponentMove = null)
        {
            if (MoveWithinNonKillableGroup(tryMove, opponentMove))
                return true;
            if (opponentMove == null && MoveNextToNonKillableGroup(tryMove))
                return true;
            return false;
        }

        /// <summary>
        /// Move within non killable group.
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario3kyu28_2" />
        /// Check for negligible in opponent move <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanQiJing_A38_3" />
        /// Check any is non killable <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WindAndTime_Q30370" />
        /// Check for covered eye <see cref="UnitTestProject.RedundantTigerMouthMove.RedundantTigerMouthMove_Scenario_WindAndTime_Q30225_2" />
        /// </summary>
        private static Boolean MoveWithinNonKillableGroup(GameTryMove tryMove, GameTryMove opponentMove = null)
        {
            Board currentBoard = tryMove.CurrentGame.Board;
            Board tryBoard = tryMove.TryGame.Board;
            Point move = tryBoard.Move.Value;
            Content c = tryBoard.MoveGroup.Content;
            if (GameHelper.GetContentForSurviveOrKill(tryBoard.GameInfo, SurviveOrKill.Survive) != c) return false;
            //check for negligible in opponent move
            if (opponentMove != null && !opponentMove.IsNegligible) return false;

            Group killerGroup = GroupHelper.GetKillerGroupFromCache(tryBoard, move, c.Opposite());
            if (killerGroup == null) return false;

            if (LifeCheck.GetTargets(tryBoard).Any(t => GroupHelper.GetKillerGroupFromCache(tryBoard, t.Points.First(), c.Opposite()) == killerGroup)) return false;

            //all neighbour groups are non-killable
            List<Group> ngroups = tryBoard.GetNeighbourGroups(killerGroup);
            if (ngroups.All(n => WallHelper.IsNonKillableGroup(tryBoard, n)))
                return true;

            //check any is non killable
            if (!ngroups.Any(n => WallHelper.IsNonKillableGroup(tryBoard, n)))
                return false;

            foreach (Group ngroup in ngroups)
            {
                foreach (Link<Point> p in LinkHelper.GetGroupLinkedDiagonals(tryBoard, ngroup))
                {
                    List<Point> diagonals = LinkHelper.PointsBetweenDiagonals(p.Move, (Point)p.CheckMove);
                    diagonals = diagonals.Where(q => GroupHelper.GetDirectKillerGroup(tryBoard, q, c.Opposite()) == killerGroup).ToList();
                    if (diagonals.Count == 0) continue;
                    Point d = diagonals.First();
                    Board b = null;
                    if (tryBoard[d] == Content.Empty) //connect at diagonal
                        b = tryBoard.MakeMoveOnNewBoard(d, c.Opposite());
                    else //capture opponent at diagonal
                        b = ImmovableHelper.CaptureSuicideGroup(d, tryBoard);
                    if (b == null) continue;
                    //check if all changed to non killable groups
                    Group kgroup = GroupHelper.GetDirectKillerGroup(b, move, c.Opposite());
                    if (kgroup != null && WallHelper.TargetWithAllNonKillableGroups(b, kgroup))
                    {
                        //check for covered eye
                        if (opponentMove != null && EyeHelper.IsCovered(tryBoard, move, c.Opposite()))
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
        /// Check for suicide at big tiger mouth <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanGo_A55_2" />
        /// Check for both alive <see cref="UnitTestProject.SurvivalTigerMouthMoveTest.SurvivalTigerMouthMoveTest_Scenario_TianLongTu_Q16827" />
        /// Check link for groups <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_Side_B35" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WindAndTime_Q30358_3" />
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
            if (tryBoard.GetMoveLiberties().Any()) return false;
            //check for unescapable group
            (Boolean unEscapable, Board escapeBoard) = ImmovableHelper.UnescapableGroup(tryBoard, atariTarget, false);
            if (unEscapable) return false;

            if (ImmovableHelper.CheckConnectAndDie(currentBoard, atariTarget, false))
                return true;

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
        /// Check weak group <see cref = "UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_TianLongTu_Q16604_3" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanGo_B32_2" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_TianLongTu_Q14916_2" />
        /// Check suicidal for both <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_Corner_A67_3" />
        /// Continue escape <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_GuanZiPu_A14" />
        /// </summary>
        private static Boolean CheckWeakGroupInOpponentSuicide(Board tryBoard, Group atariTarget)
        {
            //escape at liberty point
            Board b = ImmovableHelper.MakeMoveAtLiberty(tryBoard, atariTarget);
            if (b == null) return false;
            //check weak group
            if (AtariHelper.IsWeakNeighbourGroup(b))
                return true;
            //check suicidal for both
            List<Point> liberties = b.GetGroupLiberties(atariTarget);
            if (liberties.Count == 2 && liberties.All(n => ImmovableHelper.IsSuicidalMoveForBothPlayers(b, n)))
                return true;
            //continue escape
            if (b.MoveGroupLiberties == 2 && !WallHelper.IsHostileGroup(b))
                return true;
            return false;
        }

        /// <summary>
        /// Opponent suicidal connect and die.
        /// Check killer formation <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WindAndTime_Q29378" />
        /// Check eye <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_WindAndTime_Q30275" />
        /// <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_XuanXuanGo_B12" />
        /// Check increased killer groups <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario3dan17_3" />
        /// Check one neighbour group <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_TianLongTu_Q2413_4" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_TianLongTu_Q16827_3" />
        /// Check link for groups <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_TianLongTu_Q16925" />
        /// Check isolated group <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WuQingYuan_Q31499" />
        /// Check diagonal not cut <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_TianLongTu_Q27661" />
        /// Check diagonal cut <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_Nie61" />
        /// Get first point <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanGo_A151_101Weiqi_9" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_Corner_B43" />
        /// Check point next to corner point <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_Phenomena_B12" />
        /// Check corner point <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_Corner_B8" />
        /// <see cref="UnitTestProject.DailyGoProblems.DailyGoProblems_20221109_7" />
        /// Check four-point killer formation <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_GuanZiPu_B3_5" />
        /// </summary>
        public static Boolean OpponentSuicidalConnectAndDie(GameTryMove tryMove, GameTryMove opponentMove)
        {
            Board currentBoard = tryMove.CurrentGame.Board;
            Board tryBoard = tryMove.TryGame.Board;
            Board opponentBoard = opponentMove.TryGame.Board;
            Point move = tryMove.Move;
            Content c = tryMove.MoveContent;
            if (!opponentMove.IsNegligible || opponentBoard.MoveGroupLiberties == 1) return false;

            //check connect and die
            (Boolean connectAndDie, Board captureBoard) = ImmovableHelper.ConnectAndDie(tryBoard, tryBoard.MoveGroup, false);
            if (!connectAndDie) return false;

            //check killer formation
            if (KillerFormationHelper.IsKillerFormationFromFunc(tryBoard))
                return false;

            //check eye
            if (tryBoard.MoveGroup.Liberties.Any(n => EyeHelper.FindEye(tryBoard, n, c)))
                return false;

            //check increased killer groups
            if (opponentMove.IncreasedKillerGroups && !WallHelper.IsNonKillableGroup(opponentBoard))
                return false;

            //check one neighbour group
            Group killerGroup = GroupHelper.GetDirectKillerGroup(currentBoard, move, c.Opposite());
            if (killerGroup != null && GroupHelper.GetNeighbourGroupsOfKillerGroup(currentBoard, killerGroup).Count == 1)
            {
                //check isolated group
                List<Group> previousGroup = LinkHelper.GetPreviousMoveGroup(currentBoard, opponentBoard);
                if (previousGroup.Any(n => !GroupHelper.GetNeighbourGroupsOfKillerGroup(currentBoard, killerGroup).Contains(n)))
                    return false;
                //get first point
                Point p = KillerFormationHelper.FirstPointInKillerGroup(currentBoard, killerGroup, true);
                if (p.IsEmpty() || move.Equals(p))
                    return false;
                return true;
            }

            //check link for groups
            if (LinkHelper.IsAbsoluteLinkForGroups(currentBoard, opponentBoard))
            {
                if (killerGroup == null) return false;
                //check covered point
                if (EyeHelper.IsCovered(currentBoard, move, c.Opposite())) return false;
                //check isolated group
                List<Group> previousGroup = LinkHelper.GetPreviousMoveGroup(currentBoard, opponentBoard);
                if (previousGroup.Any(n => !GroupHelper.GetNeighbourGroupsOfKillerGroup(currentBoard, killerGroup).Contains(n)))
                    return false;
                //get first point with link for groups
                Point p = killerGroup.Points.FirstOrDefault(n => currentBoard[n] == Content.Empty && currentBoard.GetGroupsFromStoneNeighbours(n, c).Count() > 1);
                if (p.IsEmpty() || move.Equals(p))
                    return false;
                return true;
            }

            //check diagonal not cut
            if (tryBoard.MoveGroup.Points.Count > 1 && LinkHelper.GetGroupLinkedDiagonals(tryBoard).Any(n => LinkHelper.PointsBetweenDiagonals(n).Any(s => tryBoard[s] == Content.Empty)))
                return false;

            if (tryBoard.GetNeighbourGroups().Count > 1)
            {
                //check diagonal cut
                if (LinkHelper.FindDiagonalCut(tryBoard).Item1 != null)
                    return false;

                //get first point
                if (killerGroup != null)
                {
                    Point p = KillerFormationHelper.FirstPointInKillerGroup(currentBoard, killerGroup, true);
                    if (p.IsEmpty() || move.Equals(p))
                        return false;
                }
            }

            //check point next to corner point
            if (tryBoard.GetStoneNeighbours().Any(n => tryBoard.CornerPoint(n) && captureBoard.PointWithinMiddleArea() && captureBoard.MoveGroupLiberties <= 2 && captureBoard.MoveGroup.Points.Count == 1))
                return false;

            //check corner point
            if (KillerFormationHelper.CornerKillFormation(tryBoard))
                return false;

            if (tryBoard.GetDiagonalNeighbours().Any(n => tryBoard[n] == c.Opposite() && ImmovableHelper.CheckConnectAndDie(tryBoard, tryBoard.GetGroupAt(n)) && !ImmovableHelper.CheckConnectAndDie(opponentBoard, opponentBoard.GetGroupAt(n))))
                return false;

            //check four-point killer formation
            Group kgroup = GroupHelper.GetDirectKillerGroup(currentBoard, move, c);
            if (kgroup != null && (KillerFormationHelper.OneByThreeFormation(currentBoard, kgroup) || KillerFormationHelper.BoxFormation(currentBoard, kgroup)))
            {
                Point p = KillerFormationHelper.FirstPointInKillerGroup(currentBoard, kgroup);
                if (move.Equals(p))
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Suicidal connect and die. 
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_TianLongTu_Q16738" />
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
            if (tryBoard.CapturedList.Any(n => AtariHelper.AtariByGroup(currentBoard, n).Any())) return false;

            //check atari moves
            if (AtariHelper.AtariByGroup(tryBoard).Any(n => AtariHelper.IsDoubleAtari(tryBoard, n.Liberties.First(), c)))
                return false;

            //find bloated eye suicide
            if (FindBloatedEyeSuicide(tryMove, captureBoard))
                return true;

            //check redundant corner point
            if (CheckRedundantCornerPoint(tryMove, captureBoard))
                return true;

            //check weak group
            if (CheckWeakGroupInConnectAndDie(tryMove, captureBoard))
                return false;

            //check diagonals
            if (CheckDiagonalForSuicidalConnectAndDie(tryMove, captureBoard))
                return true;

            if (tryBoard.MoveGroup.Points.Count <= 4)
            {
                return CheckRedundantInSuicidalConnectAndDie(tryMove, captureBoard);
            }
            //check killer formation
            else if (KillerFormationHelper.SuicidalKillerFormations(tryBoard, currentBoard, captureBoard))
                return false;
            return true;
        }

        /// <summary>
        /// Redundant one point move in connect and die.
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_GuanZiPu_B3_3" />
        /// Check move next to covered point <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_TianLongTu_Q17132_4" />
        /// <see cref="UnitTestProject.DailyGoProblems.DailyGoProblems_20260123_7" />
        /// Check box formation <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanGo_A151_101Weiqi_4" />
        /// Check diagonal for real eye <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WuQingYuan_Q5971" />
        /// Ensure all strong neighbour groups <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanGo_A151_101Weiqi_7" />
        /// Cut diagonal and kill <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanQiJing_Weiqi101_B74_3" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_TianLongTu_Q17081_2" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_20230603_4" />
        /// Check single group <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_TianLongTu_Q16594" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanQiJing_Weiqi101_2398" />   
        /// </summary>
        private static Boolean RedundantOnePointMoveInConnectAndDie(GameTryMove tryMove, Board captureBoard)
        {
            Board currentBoard = tryMove.CurrentGame.Board;
            Board tryBoard = tryMove.TryGame.Board;
            Point move = tryMove.Move;
            Content c = tryMove.MoveContent;

            //check move next to covered point
            if (tryBoard.GetMoveLiberties().Any(p => EyeHelper.IsCovered(tryBoard, p, c.Opposite())) && GroupHelper.IsSingleGroupWithinKillerGroup(tryBoard) && !tryBoard.GetNeighbourGroups().Any(n => n.Liberties.Count <= 2))
                return true;

            //check box formation
            Group killerGroup = GroupHelper.GetDirectKillerGroup(currentBoard, move, c.Opposite());
            if (killerGroup != null && KillerFormationHelper.BoxFormation(tryBoard, killerGroup) && tryBoard.GetNeighbourGroups(killerGroup).Count == 1 && !killerGroup.Points.First().Equals(move))
                return true;

            //check diagonal for real eye
            if (EyeHelper.CheckDiagonalForRealEye(tryBoard, captureBoard).Any())
            {
                if (!LinkHelper.GetDiagonalGroups(captureBoard, tryBoard.MoveGroup).Any())
                    return true;
            }

            //check atari targets
            if (tryBoard.AtariTargets.Any(n => n.Points.Count > 1))
            {
                Point p = captureBoard.GetMoveLiberties(move).First();
                if (tryBoard.GetMoveLiberties(p).Count > 1)
                    return false;
            }

            //ensure all strong neighbour groups
            if (!WallHelper.StrongNeighbourGroups(captureBoard, move, c))
                return false;

            //check one empty space left
            if (KillerFormationHelper.SuicideMoveValidWithOneEmptySpaceLeft(tryBoard))
                return false;

            //check empty points at stone and diagonal
            if (CheckEmptyPointsAtStoneAndDiagonal(tryMove))
                return true;

            //check single group
            if (GroupHelper.IsSingleGroupWithinKillerGroup(tryBoard) && !LinkHelper.GetMoveDiagonals(tryBoard).Any())
                return true;

            //check immovable point at diagonal
            if (CheckImmovablePointAtDiagonal(tryMove, captureBoard))
                return true;

            //check one point move diagonals
            if (CheckOnePointMoveDiagonalsInConnectAndDie(tryMove, captureBoard))
                return true;

            return false;
        }

        /// <summary>
        /// Check immovable point at diagonal.
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_TianLongTu_Q17250_3" />
        /// Check opponent at diagonal <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WindAndTime_Q30196" />
        /// Diagonal move without diagonal cut <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_TianLongTu_Q16483" />
        /// </summary>
        private static Boolean CheckImmovablePointAtDiagonal(GameTryMove tryMove, Board captureBoard)
        {
            Board currentBoard = tryMove.CurrentGame.Board;
            Board tryBoard = tryMove.TryGame.Board;
            Point move = tryMove.Move;
            Content c = tryMove.MoveContent;
            List<Point> diagonals = LinkHelper.GetMoveDiagonals(tryBoard);
            if (!diagonals.Any())
            {
                //check opponent at diagonal
                if (!tryBoard.GetDiagonalNeighbours().Any(n => tryBoard[n] == c.Opposite())) return false;
                //check immovable point at diagonal
                if (tryBoard.GetDiagonalNeighbours().Any(n => tryBoard.PointWithinMiddleArea(n) && ImmovableHelper.IsImmovablePoint(tryBoard, n, c.Opposite())))
                    return true;
            }
            else
            {
                //diagonal move without diagonal cut
                if (diagonals.Count != 1) return false;
                if (!tryBoard.PointWithinMiddleArea()) return false;
                if (!LinkHelper.PointsBetweenDiagonals(move, diagonals.First()).Any(n => tryBoard[n] == Content.Empty)) return false;
                if (ImmovableHelper.CheckConnectAndDie(currentBoard, currentBoard.GetGroupAt(diagonals.First()))) return false;
                //check immovable point at diagonal
                if (tryBoard.GetDiagonalNeighbours().Any(n => tryBoard.PointWithinMiddleArea(n) && ImmovableHelper.IsImmovablePoint(captureBoard, n, c.Opposite())))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Check empty points at stone and diagonal.
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanGo_Q18500_3" />
        /// Check point next to corner <see cref="UnitTestProject.DailyGoProblems.DailyGoProblems_20221027_6" />
        /// Check for one neighbour group <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanQiJing_Weiqi101_B74_4" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanQiJing_A39" />
        /// Check connect and die <see cref="UnitTestProject.DailyGoProblems.DailyGoProblems_20260206_6" />
        /// <see cref="UnitTestProject.DailyGoProblems.DailyGoProblems_20260114_8" />
        /// </summary>
        private static Boolean CheckEmptyPointsAtStoneAndDiagonal(GameTryMove tryMove)
        {
            Board currentBoard = tryMove.CurrentGame.Board;
            Board tryBoard = tryMove.TryGame.Board;
            Point move = tryMove.Move;
            Content c = tryMove.MoveContent;
            //empty points at stone and diagonal
            Point? e = LinkHelper.CheckPointsBetweenDiagonalsAtMove(tryBoard, Content.Empty);
            if (e == null || tryBoard[e.Value] != Content.Empty) return false;

            //check point next to corner
            if (tryBoard.GetStoneNeighbours().Any(n => tryBoard.CornerPoint(n)))
            {
                Group kgroup = GroupHelper.GetDirectKillerGroup(tryBoard, move, c.Opposite());
                if (kgroup != null && !GroupHelper.IsSingleGroupWithinKillerGroup(tryBoard))
                    return false;
            }

            //check for one neighbour group
            List<Group> ngroups = currentBoard.GetGroupsFromStoneNeighbours(move, c);
            if (ngroups.Count == 1)
            {
                //check connect and die
                Group ngroup = ngroups.First();
                Boolean connectAndDie = ngroup.Points.Count == 2 && currentBoard.GetNeighbourGroups(ngroup).Any(n => ImmovableHelper.CheckConnectAndDie(currentBoard, n));
                if (!connectAndDie)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Check one point move diagonals in connect and die.
        /// Check empty point at diagonal <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanQiJing_Weiqi101_B74_3" />
        /// Check eye <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_WindAndTime_Q30275" />     
        /// Check diagonal move <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WindAndTime_Q29264" />
        /// Check weak group <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_TianLongTu_Q17241_2" />
        /// Check capture move <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WuQingYuan_Q31453_2" />
        /// Check for weak groups <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_TianLongTu_Q17250_3" />
        /// Check no diagonal groups <see cref="UnitTestProject.DailyGoProblems.DailyGoProblems_20260111_8" />
        /// Check multi-point group <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanQiJing_Weiqi101_7245_2" />
        /// Check for neighbour weak groups <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_GuanZiPu_A37" />
        /// Check move liberties <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanQiJing_A38_4" />
        /// </summary>
        private static Boolean CheckOnePointMoveDiagonalsInConnectAndDie(GameTryMove tryMove, Board captureBoard)
        {
            Board currentBoard = tryMove.CurrentGame.Board;
            Board tryBoard = tryMove.TryGame.Board;
            Point move = tryMove.Move;
            Content c = tryMove.MoveContent;
            Point? d = LinkHelper.CheckPointsBetweenDiagonalsAtMove(tryBoard);
            if (d != null)
            {
                //check empty point at diagonal
                if (tryBoard[d.Value] == Content.Empty && tryBoard.GetNeighbourGroups().Any(n => n.Liberties.Count <= 2))
                    return false;

                //check eye
                if (tryBoard.GetStoneNeighbours().Any(n => EyeHelper.FindEye(tryBoard, n, c)))
                    return false;

                //check diagonal move
                List<Point> dpoints = captureBoard.GetDiagonalNeighbours(move).Where(n => captureBoard[n] == Content.Empty).Intersect(captureBoard.GetStoneNeighbours(captureBoard.GetMoveLiberties(move).First())).ToList();
                foreach (Board b in GameHelper.GetMoveBoards(captureBoard, dpoints, c))
                {
                    if (!ImmovableHelper.CheckConnectAndDie(b, b.MoveGroup, false))
                        return false;
                }

                //check weak group
                foreach (Group ngroup in captureBoard.GetGroupsFromStoneNeighbours(move, c))
                {
                    if (ngroup.Liberties.Count != 2) continue;
                    foreach (Board b in GameHelper.GetMoveBoards(captureBoard, ngroup.Liberties, c, true))
                    {
                        if (b.GetStoneAndDiagonalNeighbours().Any(n => !n.Equals(move) && !WallHelper.NoEyeForSurvival(captureBoard, n, c.Opposite())))
                            return false;
                    }
                }
                return true;
            }
            else
            {
                if (tryBoard.GetDiagonalNeighbours().Any(n => tryBoard[n] == c)) return false;

                //check capture move
                List<Group> ngroups = tryBoard.GetGroupsFromStoneNeighbours();
                if (ngroups.Any(n => n.Liberties.Count <= 2)) return false;
                if (captureBoard.MoveGroup.Points.Count == 1 && ngroups.All(n => n.Liberties.Count > n.Neighbours.Count * 0.5))
                {
                    Point? v = LinkHelper.CheckPointsBetweenDiagonalsAtMove(captureBoard, Content.Empty);
                    if (v != null && captureBoard[v.Value] == Content.Empty)
                        return true;
                }

                //check for weak groups
                foreach (Group ngroup in ngroups)
                {
                    List<Group> diagonalGroups = LinkHelper.GetDiagonalGroups(tryBoard, ngroup);
                    //check no diagonal groups
                    if (diagonalGroups.Count == 0 && ngroup.Points.Count == 1) continue;
                    //check one point group
                    if (diagonalGroups.Any(s => s.Liberties.Count <= 2)) continue;
                    if (ngroup.Points.Count == 1)
                        return true;
                    //check multi-point group
                    if (LinkHelper.GetGroupDiagonals(tryBoard, ngroup).Any(n => tryBoard[n.Move] == c)) continue;
                    if (tryBoard.GetNeighbourGroups(ngroup).Count(s => s != tryBoard.MoveGroup) <= 1)
                        return true;
                }

                //check for neighbour weak groups
                foreach (Point p in tryBoard.GetDiagonalNeighbours().Where(n => tryBoard[n] == Content.Empty))
                {
                    if (!captureBoard.GetGroupsFromStoneNeighbours(p, c).Any(s => s.Liberties.Count <= 3)) continue;
                    if (ImmovableHelper.IsSuicidalMove(tryBoard, p, c, true)) continue;
                    return false;
                }

                //check move liberties
                foreach (Point p in tryBoard.GetMoveLiberties())
                {
                    List<Group> sgroups = tryBoard.GetGroupsFromStoneNeighbours(p, c.Opposite()).Where(n => n != tryBoard.MoveGroup).ToList();
                    if (!sgroups.Any()) continue;
                    Board b = tryBoard.MakeMoveOnNewBoard(p, c.Opposite());
                    if (b == null) continue;
                    Group kgroup = GroupHelper.GetDirectKillerGroup(b, move, c.Opposite());
                    if (kgroup != null && !GroupHelper.IsSingleGroupWithinKillerGroup(b, tryBoard.MoveGroup))
                        return false;
                }
                return true;
            }
        }

        /// <summary>
        /// Check weak group.
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_x" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanGo_B6" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanQiJing_B17" />
        /// </summary>
        private static Boolean CheckWeakGroup(Board tryBoard, Group group = null)
        {
            if (group == null) group = tryBoard.MoveGroup;
            else group = tryBoard.GetCurrentGroup(group);
            //capture move
            (_, Board b) = ImmovableHelper.ConnectAndDie(tryBoard, group, false);
            if (b == null || b.MoveGroupLiberties == 1 || b.IsCapturedGroup(group)) return false;
            if (LifeCheck.GetTargets(tryBoard).All(t => tryBoard.MoveGroup.Equals(t))) return false;

            //check weak group
            if (AtariHelper.IsWeakNeighbourGroup(b, group))
                return true;

            //escape move at liberty
            Board b2 = ImmovableHelper.MakeMoveAtLiberty(b, group);
            if (b2 != null && b2.MoveGroupLiberties == 2 && CheckWeakGroup(b2, group))
                return true;

            //escape by capture
            foreach (Group gr in AtariHelper.AtariByGroup(b, group))
            {
                Board b3 = ImmovableHelper.CaptureSuicideGroup(b, gr);
                Group target = b3.GetCurrentGroup(group);
                if (target.Liberties.Count == 2 && CheckWeakGroup(b3, target))
                    return true;
                if (!b3.MoveGroup.Equals(target) && AtariHelper.IsWeakNeighbourGroup(b3))
                    return true;
            }
            return false;
        }


        /// <summary>
        /// Check weak group in connect and die.
        /// Check one point group <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WuQingYuan_Q5971" /> 
        /// Check three liberty weak group <see cref="UnitTestProject.DailyGoProblems.DailyGoProblems_20250311_8" /> 
        /// </summary>
        private static Boolean CheckWeakGroupInConnectAndDie(GameTryMove tryMove, Board captureBoard)
        {
            Board currentBoard = tryMove.CurrentGame.Board;
            Board tryBoard = tryMove.TryGame.Board;
            if (!tryBoard.GetMoveLiberties().Any() && !tryMove.AtariResolved)
                return false;

            //check one point group
            if (tryBoard.MoveGroup.Points.Count == 1)
            {
                List<Group> ngroups = tryBoard.GetGroupsFromStoneNeighbours();
                if (!(ngroups.Count == 0 || ngroups.Any(n => n.Points.Count == 1 && n.Liberties.Count <= 2)))
                    return false;
            }

            //check weak group
            if (CheckWeakGroup(tryBoard))
                return true;

            //check three liberty weak group
            if (captureBoard.MoveGroupLiberties == 3)
            {
                if (captureBoard.GetMoveLiberties().Any()) return false;
                List<Group> ngroups = captureBoard.GetGroupsFromStoneNeighbours();
                ngroups.Remove(captureBoard.GetCurrentGroup(tryBoard.MoveGroup));
                if (ngroups.Count == 0) return false;
                if (!ngroups.Any(n => WallHelper.IsStrongGroup(captureBoard, n))) return false;
                if (ngroups.Any(n => !WallHelper.IsNonKillableGroup(captureBoard, n)))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Find bloated eye suicide.
        /// <see cref="UnitTestProject.GenericNeutralMoveTest.GenericNeutralMoveTest_Scenario_GuanZiPu_A35" />
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
            List<Group> eyeGroups = captureBoard.GetGroupsFromStoneNeighbours(liberty, c.Opposite());
            if (eyeGroups.Count == 1 && KillerFormationHelper.SuicidalKillerFormations(tryBoard, currentBoard, captureBoard))
                return false;

            //check reverse ko fight
            if (eyeGroups.Count > 1 && eyeGroups.Any(n => AtariHelper.AtariByGroup(tryBoard, n).Any()))
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
            if (tryBoard.GetDiagonalNeighbours().Any(n => tryBoard[n] == Content.Empty))
                return true;

            //check for kill formation
            if (KillerFormationHelper.CornerKillFormation(tryBoard))
                return false;

            //multipoint snapback
            if (captureBoard.GetNeighbourGroups(tryBoard.MoveGroup).Any(gr => gr.Points.Count > 1 && ImmovableHelper.CheckConnectAndDie(captureBoard, gr)))
                return false;
            return true;
        }

        /// <summary>
        /// Check redundant in suicidal connect and die.
        /// Check for real eye <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_GuanZiPu_B3_3" />
        /// </summary>
        private static Boolean CheckRedundantInSuicidalConnectAndDie(GameTryMove tryMove, Board captureBoard)
        {
            Board tryBoard = tryMove.TryGame.Board;
            Point move = tryBoard.Move.Value;
            Content c = tryBoard.MoveGroup.Content;
            //check for real eye
            if (!EyeHelper.FindRealEyeOfAnyKillerGroup(captureBoard, move, c.Opposite())) return false;
            return EyeHelper.CheckRealEyeInNeighbourGroups(tryBoard, captureBoard);
        }

        /// <summary>
        /// Check diagonal for suicidal connect and die.
        /// Check non killable group <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WuQingYuan_Q31680_3" />
        /// Ensure no diagonal at move <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_GuanZiPu_Q18796_2" />
        /// Ensure no diagonal groups <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_Corner_A55" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_GuanZiPu_A17_3" />
        /// Check connected liberties <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WindAndTime_Q30064" />
        /// Check for killer formation <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_Scenario_GuanZiPu_A4Q11_101Weiqi_2" />
        /// <see cref="UnitTestProject.KillerFormationTest.KillerFormationTest_Scenario_TianLongTu_Q15082" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_TianLongTu_Q16748" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_GuanZiPu_A2Q28_101Weiqi" />
        /// </summary>
        private static Boolean CheckDiagonalForSuicidalConnectAndDie(GameTryMove tryMove, Board captureBoard)
        {
            Board currentBoard = tryMove.CurrentGame.Board;
            Board tryBoard = tryMove.TryGame.Board;
            Point move = tryMove.Move;
            Content c = tryMove.MoveContent;

            //check non killable group
            if (WallHelper.TargetWithAllNonKillableGroups(captureBoard, tryBoard.MoveGroup))
            {
                Boolean isCovered = EyeHelper.IsCovered(captureBoard, move, c.Opposite()) || tryBoard.GetMoveLiberties().Any(n => EyeHelper.IsCovered(tryBoard, n, c));
                if (!isCovered)
                    return true;
            }

            if (tryBoard.MoveGroup.Points.Count == 1)
            {
                //redundant one point move
                if (RedundantOnePointMoveInConnectAndDie(tryMove, captureBoard))
                    return true;
            }
            else
            {
                //check diagonal and liberty at move
                if (CheckDiagonalAndLibertyAtMove(tryMove, captureBoard))
                    return true;

                //check non killable group
                if (WallHelper.TargetWithAnyNonKillableGroup(tryBoard))
                    return true;

                //check connected liberties
                Point p = tryBoard.MoveGroup.Liberties.First();
                if (tryBoard.GetStoneNeighbours(p).Any(q => tryBoard.MoveGroup.Liberties.Contains(q)))
                {
                    if (tryBoard.GetGroupsFromStoneNeighbours().Count > 1 && LinkHelper.GetDiagonalGroups(tryBoard).Any(n => !ImmovableHelper.CheckConnectAndDie(tryBoard, n, false)))
                        return false;
                    //check for killer formation
                    if (tryBoard.MoveGroup.Points.Count >= 3 && KillerFormationHelper.SuicidalKillerFormations(tryBoard, currentBoard, captureBoard))
                        return false;
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Check diagonal and liberty at move.
        /// Check for three neighbour groups <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WindAndTime_Q30198" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_TianLongTu_Q16605" />
        /// Check killer formation <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WuQingYuan_Q31499_3" />
        /// Check move liberties <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WindAndTime_Q30064" />
        /// Check is negligible <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanQiJing_B25" />
        /// Check opponent at diagonal points <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WindAndTime_Q30403_2" />
        /// Check capture move liberty <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanQiJing_A64_2" />
        /// </summary>
        private static Boolean CheckDiagonalAndLibertyAtMove(GameTryMove tryMove, Board captureBoard)
        {
            Board currentBoard = tryMove.CurrentGame.Board;
            Board tryBoard = tryMove.TryGame.Board;
            Content c = tryMove.MoveContent;

            //check diagonal for real eye
            if (EyeHelper.CheckDiagonalForRealEye(tryBoard, captureBoard).Any())
            {
                if (tryBoard.MoveGroup.Points.Count > 2 || !LinkHelper.GetDiagonalGroups(captureBoard, tryBoard.MoveGroup).Any())
                    return true;
            }

            List<Point> moveLiberties = tryBoard.GetMoveLiberties();
            if (!moveLiberties.Any())
            {
                //check for three neighbour groups
                if (KillerFormationHelper.ThreeOpponentGroupsAtMove(tryBoard))
                    return false;

                //check link for groups
                if (!LinkHelper.IsAbsoluteLinkForGroups(currentBoard, tryBoard) && tryBoard.MoveGroup.Points.Count <= 3)
                    return true;

                //check killer formation
                if (KillerFormationHelper.SuicidalKillerFormations(tryBoard, currentBoard, captureBoard))
                    return false;

                //check move diagonals 
                if (LinkHelper.GetMoveDiagonals(tryBoard).Any())
                    return false;

                return true;
            }

            //check is negligible
            if (!tryMove.IsNegligible) return false;

            //check one empty space left
            if (KillerFormationHelper.SuicideMoveValidWithOneEmptySpaceLeft(tryBoard))
                return false;

            //check opponent at diagonal points
            List<Point> diagonals = LinkHelper.GetDiagonalPoints(tryBoard);
            if (!diagonals.All(n => tryBoard[n] == c.Opposite()))
                return false;

            //check capture move liberty
            if (moveLiberties.Count == 1)
            {
                Point liberty = moveLiberties.First();
                Board b = captureBoard;
                if (!captureBoard.Move.Equals(liberty)) b = tryBoard.MakeMoveOnNewBoard(liberty, c.Opposite());
                if (b != null && EyeHelper.CheckCaptureMoveLiberty(tryBoard, b))
                    return false;
            }

            //check killer formation
            if (KillerFormationHelper.IsKillerFormationFromFunc(tryBoard))
                return false;

            //check eye
            if (moveLiberties.Any(n => GroupHelper.CheckKillerGroupPoints(tryBoard, n, c, 2, false) != null))
                return false;

            return true;
        }

        /// <summary>
        /// Single point suicidal move.
        /// </summary>
        public static Boolean SinglePointSuicidalMove(GameTryMove tryMove, GameTryMove opponentMove = null)
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
            if (MiscSinglePointSuicide(tryMove, capturedBoard, opponentMove))
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
        /// Two liberties <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WindAndTime_Q30215" />
        /// Two liberties connect and die <see cref="UnitTestProject.DailyGoProblems.DailyGoProblems_20260112_8" />
        /// Three liberties <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_20221019_6" />
        /// </summary>
        public static Boolean SuicideWithinRealEye(GameTryMove tryMove, Board capturedBoard)
        {
            Point move = tryMove.Move;
            Board tryBoard = tryMove.TryGame.Board;
            Board currentBoard = tryMove.CurrentGame.Board;
            Content c = tryMove.MoveContent;

            //ensure semi-solid eye
            if (!EyeHelper.FindSemiSolidEye(capturedBoard, move, c.Opposite()))
                return false;

            //check non killable neighbour group
            if (WallHelper.TargetWithAnyNonKillableGroup(currentBoard, move, c))
                return true;

            //opponent break kill formation
            if (KillerFormationHelper.OpponentBreakKillFormation(tryBoard, currentBoard))
                return false;

            //remove one point from two-point empty group
            Group eyeGroup = GroupHelper.GetDirectKillerGroup(currentBoard, move, c.Opposite());
            Board board = EyeHelper.FindRealEyeWithinTwoEmptyPoints(currentBoard, eyeGroup);
            if (board != null && !move.Equals(board.Move.Value))
                return true;
            if (capturedBoard.MoveGroupLiberties == 1) return false;


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
            else if (liberties.Count == 2)
            {
                //two liberties 
                foreach (Board b in GameHelper.GetMoveBoards(capturedBoard, liberties, c))
                {
                    Point q = liberties.First(n => !n.Equals(b.Move));
                    //both players suicidal at liberty
                    if (GroupHelper.GetDirectKillerGroup(tryBoard, q, c.Opposite()) == null) continue;
                    if (ImmovableHelper.IsSuicidalMoveForBothPlayers(b, q))
                    {
                        if (b.GetNeighbourGroups(tryBoard.MoveGroup).All(n => LinkHelper.FindDiagonalCut(b, n).Item1 == null))
                            continue;
                        return false;
                    }
                }
                //two liberties connect and die
                List<Group> groups = capturedBoard.GetGroupsFromStoneNeighbours(liberties.First(), c.Opposite());
                if (groups.Count >= 2 && groups.Any(n => ImmovableHelper.CheckConnectAndDie(capturedBoard, n) && !ImmovableHelper.CheckConnectAndDie(tryBoard, n)))
                    return false;
            }
            else if (liberties.Count == 3)
            {
                //three liberties
                foreach (Group ngroup in ngroups)
                {
                    List<Point> nLiberties = ngroup.Liberties.Where(n => !n.Equals(move)).ToList();
                    if (nLiberties.Count != 2) continue;
                    foreach (Board b in GameHelper.GetMoveBoards(capturedBoard, nLiberties, c))
                    {
                        //both players suicidal at liberty
                        Point q = nLiberties.First(n => !n.Equals(b.Move));
                        if (ImmovableHelper.IsSuicidalMoveForBothPlayers(b, q))
                            return false;
                    }
                }
            }

            //check atari move
            if (tryBoard.IsAtariMove)
            {
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
        /// Check non two-point group in suicide real eye.
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanGo_B31_3" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario4dan17_2" />
        /// Check eye groups <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WuQingYuan_Q31536" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_Corner_A139" />
        /// </summary>
        private static Boolean CheckNonTwoPointGroupInSuicideRealEye(GameTryMove tryMove, Board captureBoard)
        {
            Point move = tryMove.Move;
            Board tryBoard = tryMove.TryGame.Board;
            Content c = tryMove.MoveContent;

            if (EyeHelper.FindRealSolidEye(captureBoard, move, c.Opposite()))
                return true;

            //check eye groups
            if (captureBoard.GetGroupsFromStoneNeighbours(move, c).All(n => n.Liberties.Count > 2))
                return true;
            if (captureBoard.GetDiagonalNeighbours(move).Where(d => captureBoard[d] == Content.Empty).All(n => !captureBoard.GetStoneAndDiagonalNeighbours(n).Any(s => captureBoard[s] == c), true))
                return true;

            //get diagonals next to atari target
            List<Point> diagonals = tryBoard.GetDiagonalNeighbours().Where(n => tryBoard[n] != c.Opposite() && tryBoard.GetGroupsFromStoneNeighbours(n, c).Intersect(tryBoard.AtariTargets).Any()).ToList();
            //check killer group
            if (diagonals.Any(d => GroupHelper.GetDirectKillerGroup(tryBoard, d, c.Opposite()) != null))
                return true;

            return false;
        }

        /// <summary>
        /// Check two-point group in suicide real eye.
        /// Check connect and die <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WuQingYuan_Q15126" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanQiJing_Weiqi101_1887" />
        /// Check for liberty fight <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_GuanZiPu_Q18796" />
        /// </summary>
        private static Boolean CheckTwoPointGroupInSuicideRealEye(GameTryMove tryMove, Board capturedBoard)
        {
            Board currentBoard = tryMove.CurrentGame.Board;
            Board tryBoard = tryMove.TryGame.Board;
            Content c = tryMove.MoveContent;
            Point liberty = tryBoard.MoveGroup.Liberties.First();
            if (currentBoard.OneLibertyGroup(liberty, c).Any())
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
        private static Boolean MiscSinglePointSuicide(GameTryMove tryMove, Board capturedBoard, GameTryMove opponentMove = null)
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
            if (opponentMove == null && LinkHelper.GetMoveDiagonals(tryBoard).Any(n => ImmovableHelper.CheckConnectAndDie(tryBoard, tryBoard.GetGroupAt(n)) && !ImmovableHelper.CheckConnectAndDie(currentBoard, currentBoard.GetGroupAt(n))))
                return true;

            //opponent suicide
            if (opponentMove != null && (ImmovableHelper.SuicideAtBigTigerMouth(opponentMove).Item1 || BothAliveHelper.CheckForBothAliveAtMove(opponentMove.TryGame.Board)))
                return false;

            //suicide near non killable group
            if (GameHelper.GetContentForSurviveOrKill(tryBoard.GameInfo, SurviveOrKill.Survive) == c)
            {
                if (WallHelper.TargetWithAnyNonKillableGroup(tryBoard) && WallHelper.StrongGroups(capturedBoard, capturedBoard.GetGroupsFromStoneNeighbours(move, c)))
                    return true;
            }
            else
            {
                //diagonal non killable groups
                List<Point> diagonals = tryBoard.GetDiagonalNeighbours().Where(n => WallHelper.IsNonKillableGroup(currentBoard, n)).ToList();
                Boolean rc = tryBoard.PointWithinMiddleArea(move) ? diagonals.Count >= 2 : diagonals.Count >= 1;
                if (!rc) return false;

                //covered point side move
                if (CoveredPointSideMove(tryMove, opponentMove))
                    return true;

                //covered point suicidal move
                if (CoveredPointSuicidalWithCornerFormation(tryMove, capturedBoard)) return false;

                if (diagonals.Any(n => LinkHelper.PointsBetweenDiagonals(move, n).Any(d => tryBoard[d] == Content.Empty)))
                    return true;

                //check real eye at diagonal without opposite content
                if (!WallHelper.StrongGroups(capturedBoard, capturedBoard.GetGroupsFromStoneNeighbours(move, c))) return false;
                foreach (Point d in capturedBoard.GetDiagonalNeighbours(move))
                {
                    if (capturedBoard[d] != Content.Empty) continue;
                    if (!EyeHelper.FindRealEyeWithinEmptySpace(capturedBoard, d, c.Opposite())) continue;
                    if (!GroupHelper.GetDirectKillerGroup(capturedBoard, d, c.Opposite()).Points.Any(p => capturedBoard[p] == c))
                        return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Covered point side move.
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanGo_A41_2" />
        /// Check diagonal <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_Side_B29" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario3dan8_2" />
        /// Check corner <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_Corner_A36" />
        /// Check one opponent group in killer group <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_WuQingYuan_Q5971" />
        /// </summary>
        private static Boolean CoveredPointSideMove(GameTryMove tryMove, GameTryMove opponentMove = null)
        {
            if (opponentMove == null) return false;
            Board opponentBoard = opponentMove.TryGame.Board;
            Board currentBoard = tryMove.CurrentGame.Board;
            Point move = opponentBoard.Move.Value;
            Content c = opponentBoard.MoveGroup.Content;
            if (opponentBoard.PointWithinMiddleArea())
                return false;
            Point? p = LinkHelper.CheckPointsBetweenDiagonalsAtMove(opponentBoard, c);
            if (p == null) return false;
            if (opponentBoard[p.Value] != c.Opposite())
                return false;
            if (!WallHelper.IsNonKillableGroup(currentBoard, currentBoard.GetGroupAt(p.Value)))
                return false;
            //check killer group points
            if (GroupHelper.CheckKillerGroupPoints(currentBoard, move, c) != null)
                return false;
            //check diagonal
            if (opponentBoard.GetDiagonalNeighbours().Any(n => opponentBoard[n] == c))
                return false;
            //check corner
            if (opponentBoard.GetMoveLiberties().Any(n => opponentBoard.CornerPoint(n)))
                return false;
            //check one opponent group in killer group
            Group kgroup = GroupHelper.GetDirectKillerGroup(currentBoard, move, c);
            if (kgroup != null)
            {
                List<Point> contentPoints = kgroup.Points.Where(n => currentBoard[n] == c.Opposite()).ToList();
                if (currentBoard.GetGroupsFromPoints(contentPoints).Count == 1)
                    return false;
            }
            return true;
        }

        /// Corner point suicide.
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanGo_A26_2" />
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
                Point diagonal = tryBoard.GetDiagonalNeighbours().First();
                if (tryBoard[diagonal] == Content.Empty && captureBoard.MoveGroup.Points.Count == 1)
                    return true;
                else if (tryBoard[diagonal] == c)
                {
                    Board b = ImmovableHelper.MakeMoveAtLiberty(tryBoard, atariTarget);
                    if (b != null && b.MoveGroupLiberties > 1)
                        return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Multi point suicidal move.
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

            //killer formations
            if (KillerFormationHelper.SuicidalKillerFormations(tryBoard, currentBoard, capturedBoard))
                return false;

            //check ko fight
            if (KillerFormationHelper.CheckKoFightAfterSuicidal(tryBoard, capturedBoard))
                return false;

            //no hope of escape
            return true;
        }
        #endregion

        #region leap move

        /// <summary>
        /// Redundant kill leap move.
        /// <see cref="UnitTestProject.DailyGoProblems.DailyGoProblems_20250714_8" />
        /// </summary>
        public static Boolean RedundantKillLeapMove(GameTryMove tryMove)
        {
            if (!tryMove.IsNegligible)
                return false;
            GameTryMove opponentMove = tryMove.MakeMoveWithOpponentAtSamePoint();
            if (opponentMove != null)
                return RedundantSurvivalLeapMove(opponentMove, tryMove);
            return false;
        }

        /// <summary>
        /// Redundant survival leap move.
        /// <see cref="UnitTestProject.LeapMoveTest.LeapMoveTest_Scenario_XuanXuanQiJing_A1" />
        /// Check opponent groups <see cref="UnitTestProject.LeapMoveTest.LeapMoveTest_Scenario_GuanZiPu_B3" />
        /// </summary>
        public static Boolean RedundantSurvivalLeapMove(GameTryMove tryMove, GameTryMove opponentMove = null)
        {
            Board tryBoard = tryMove.TryGame.Board;
            Point move = tryMove.Move;
            Content c = tryBoard.MoveGroup.Content;

            if (!tryMove.IsNegligible)
                return false;

            if (LifeCheck.GetTargets(tryBoard).Contains(tryBoard.MoveGroup)) return false;

            //check opponent groups
            List<Point> rc = tryBoard.GetClosestPoints(move, c.Opposite(), 3);
            if (rc.Count(n => !CheckNonKillableAtDiagonalGroups(tryBoard, tryBoard.GetGroupAt(n))) >= 3)
                return false;

            if (!WallHelper.StrongNeighbourGroups(tryBoard))
                return false;

            //check leap move to target
            if (CheckLeapMoveToTarget(tryBoard))
                return false;
            return true;
        }

        /// <summary>
        /// Check leap move to target.
        /// </summary>
        public static Boolean CheckLeapMoveToTarget(Board tryBoard, HashSet<Group> groups = null)
        {
            Content c = tryBoard.MoveGroup.Content;
            if (groups == null) groups = new HashSet<Group>() { tryBoard.MoveGroup };
            Group group = groups.Last();

            foreach (Point p in group.Points)
            {
                List<Point> rc = tryBoard.GetClosestPoints(p, c, 2).Where(r => tryBoard.GetGroupAt(r) != group).ToList();
                if (!rc.Any()) continue;

                foreach (Point r in rc)
                {
                    Group rgroup = tryBoard.GetGroupAt(r);
                    if (groups.Contains(rgroup)) continue;

                    //verify leap move
                    if (tryBoard.GetClosestPoints(p, c, 2, 2).Any(n => n.Equals(r)))
                    {
                        List<Point> mpoints = GetMidPointsOfLeapMove(p, r).Where(n => tryBoard[n] == c.Opposite()).ToList();
                        if (mpoints.Count > 0)
                        {
                            Group mgroup = tryBoard.GetGroupAt(mpoints.First());
                            if (CheckNonKillableAtDiagonalGroups(tryBoard, mgroup))
                                continue;
                        }
                    }

                    //check if target found
                    if (LifeCheck.GetTargets(tryBoard).Contains(rgroup))
                        return true;

                    //recursive check leap move
                    groups.Add(rgroup);
                    if (CheckLeapMoveToTarget(tryBoard, groups))
                        return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Check non killable at diagonal groups.
        /// </summary>
        public static Boolean CheckNonKillableAtDiagonalGroups(Board tryBoard, Group group)
        {
            if (WallHelper.IsNonKillableGroup(tryBoard, group))
                return true;

            if (LinkHelper.GetDiagonalGroupsWithoutCut(tryBoard, group).Any(n => WallHelper.IsNonKillableGroup(tryBoard, n.Move)))
                return true;

            return false;
        }

        /// <summary>
        /// Get mid points of leap move.
        /// </summary>
        public static List<Point> GetMidPointsOfLeapMove(Point p, Point q)
        {
            List<Point> points = new List<Point>();
            Boolean isLeapOnX = Math.Abs(p.x - q.x) == 2;
            Boolean isLeapOnY = Math.Abs(p.y - q.y) == 2;
            if (isLeapOnX)
            {
                if (p.y == q.y)
                    points.Add(new Point(Math.Min(p.x, q.x) + 1, p.y));
                else
                {
                    points.Add(new Point(Math.Min(p.x, q.x) + 1, p.y));
                    points.Add(new Point(Math.Min(p.x, q.x) + 1, q.y));
                }
            }
            else if (isLeapOnY)
            {
                if (p.x == q.x)
                    points.Add(new Point(p.x, Math.Min(p.y, q.y) + 1));
                else
                {
                    points.Add(new Point(p.x, Math.Min(p.y, q.y) + 1));
                    points.Add(new Point(q.x, Math.Min(p.y, q.y) + 1));
                }
            }
            return points;
        }
        #endregion

        #region neutral point
        /// <summary>
        /// Neutral point survival move. Survival group cannot create eye. 
        /// <see cref="UnitTestProject.NeutralPointMoveTest.NeutralPointMoveTest_Scenario_WuQingYuan_Q30935" />
        /// </summary>
        public static Boolean NeutralPointSurvivalMove(GameTryMove tryMove, GameTryMove opponentMove = null)
        {
            if (opponentMove == null && !tryMove.IsNegligible && EssentialAtariAtCoveredEye(tryMove))
                return false;
            //validate neutral point
            return ValidateNeutralPoint(tryMove, opponentMove);
        }

        /// <summary>
        /// Neutral point kill move.
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

            //check neutral point
            Boolean isNeutralPoint = NeutralPointSurvivalMove(opponentMove, tryMove);
            if (isNeutralPoint)
            {
                //must have neutral point
                if (MustHaveNeutralPoint(tryMove, opponentMove))
                    tryMove.MustHaveNeutralPoint = true;
            }

            //kill move in middle area
            if (NeutralPointKillMoveInMiddleArea(tryMove))
                return true;
            return isNeutralPoint;
        }

        /// <summary>
        /// Neutral point kill move in middle area.
        /// <see cref="UnitTestProject.NeutralPointMoveTest.NeutralPointMoveTest_Scenario_XuanXuanGo_A26_3" />
        /// Check middle area <see cref="UnitTestProject.NeutralPointMoveTest.NeutralPointMoveTest_Scenario_Corner_A40" />
        /// Check one empty space left <see cref="UnitTestProject.NeutralPointMoveTest.NeutralPointMoveTest_Scenario_WindAndTime_Q29264" />
        /// Check opponent at stone and diagonal neighbour <see cref="UnitTestProject.NeutralPointMoveTest.NeutralPointMoveTest_Scenario_XuanXuanGo_A82_101Weiqi" />
        /// Check connect and die <see cref="UnitTestProject.NeutralPointMoveTest.NeutralPointMoveTest_Scenario_XuanXuanGo_B31" />
        /// </summary>
        public static Boolean NeutralPointKillMoveInMiddleArea(GameTryMove tryMove)
        {
            Board tryBoard = tryMove.TryGame.Board;
            Board currentBoard = tryMove.CurrentGame.Board;
            Point move = tryBoard.Move.Value;
            Content c = tryBoard.MoveGroup.Content;
            if (!tryMove.IsNegligible) return false;
            //check middle area
            if (!tryBoard.PointWithinMiddleArea()) return false;
            //check no eye for survival
            if (!WallHelper.NoEyeForSurvival(currentBoard, move, c.Opposite())) return false;
            //check opponent at stone neighbour
            List<Group> ngroups = tryBoard.GetGroupsFromStoneNeighbours();
            if (ngroups.Any())
            {
                Boolean rc = (ngroups.Count == 1 && ngroups.First().Points.Count == 1);
                if (!rc)
                    return false;
            }
            //check one empty space left
            if (KillerFormationHelper.SuicideMoveValidWithOneEmptySpaceLeft(tryBoard))
                return false;
            //check opponent at stone and diagonal neighbour
            List<Point> opponentPoints = tryBoard.GetStoneAndDiagonalNeighbours().Where(n => tryBoard[n] == c.Opposite()).ToList();
            if (tryBoard.GetGroupsFromPoints(opponentPoints).Count >= 2) return false;
            //check connect and die
            if (LinkHelper.GetPreviousMoveGroup(currentBoard, tryBoard).Any(n => ImmovableHelper.CheckConnectAndDie(currentBoard, n)))
                return false;
            return true;
        }

        /// <summary>
        /// Covered point suicidal with corner formation.
        /// <see cref="UnitTestProject.SurvivalTigerMouthMoveTest.RedundantTigerMouthMove_20221214_5" />
        /// <see cref="UnitTestProject.SurvivalTigerMouthMoveTest.RedundantTigerMouthMove_20221214_6" />
        /// </summary>
        public static Boolean CoveredPointSuicidalWithCornerFormation(GameTryMove tryMove, Board captureBoard = null)
        {
            Board tryBoard = tryMove.TryGame.Board;
            Point move = tryBoard.Move.Value;
            Content c = tryBoard.MoveGroup.Content;
            if (tryBoard.MoveGroupLiberties != 1 || tryBoard.MoveGroup.Points.Count != 1) return false;
            if (!tryBoard.PointWithinMiddleArea()) return false;

            if (!KillerFormationHelper.TigerMouthAtDiagonal(tryBoard)) return false;
            if (captureBoard == null) captureBoard = ImmovableHelper.CaptureSuicideGroup(tryBoard);
            if (captureBoard.GetMoveLiberties().Count != 1) return false;
            if (!EyeHelper.FindCoveredEye(captureBoard, move, c.Opposite())) return false;
            if (!LinkHelper.GetGroupDiagonals(captureBoard).Any(n => captureBoard[n.Move] == Content.Empty && KillerFormationHelper.CornerKillFormation(captureBoard, n.Move, c)))
                return false;
            return true;
        }

        /// <summary>
        /// Essential atari at covered eye.
        /// Check ko fight <see cref="UnitTestProject.NeutralPointMoveTest.NeutralPointMoveTest_Scenario4dan17" />
        /// Check covered <see cref="UnitTestProject.NeutralPointMoveTest.NeutralPointMoveTest_Scenario_XuanXuanGo_A41_2" />
        /// Check reverse ko <see cref="UnitTestProject.BaseLineSurvivalMoveTest.BaseLineSurvivalMoveTest_Scenario_TianLongTu_Q16456" />
        /// Check double atari <see cref="UnitTestProject.NeutralPointMoveTest.NeutralPointMoveTest_Scenario_WindAndTime_Q30224" />
        /// Check opponent at liberty point <see cref="UnitTestProject.NeutralPointMoveTest.NeutralPointMoveTest_Scenario_WindAndTime_Q30199" />
        /// Check capture at liberty point <see cref="UnitTestProject.NeutralPointMoveTest.NeutralPointMoveTest_Scenario4dan17_2" />
        /// </summary>
        private static Boolean EssentialAtariAtCoveredEye(GameTryMove tryMove)
        {
            Board tryBoard = tryMove.TryGame.Board;
            Board currentBoard = tryMove.CurrentGame.Board;
            Content c = tryBoard.MoveGroup.Content;
            if (tryMove.AtariResolved || tryMove.Captured || tryBoard.MoveGroupLiberties == 1) return true;
            if (tryBoard.AtariTargets.Count != 1) return true;

            Group atariTarget = tryBoard.AtariTargets.First();
            if (atariTarget.Points.Count != 1) return true;

            //check ko fight
            if (KoHelper.IsKoFight(tryBoard, atariTarget))
            {
                //check neighbour groups
                Board b = ImmovableHelper.CaptureSuicideGroup(tryBoard, atariTarget);
                if (WallHelper.StrongNeighbourGroups(b))
                    return false;
                return true;
            }

            //check covered
            Point p = atariTarget.Liberties.First();
            if (!EyeHelper.IsCovered(tryBoard, p, c.Opposite())) return true;

            //check reverse ko
            if (KoHelper.CheckReverseKoForNeutralPoint(currentBoard, atariTarget))
                return true;

            //check double atari
            if (AtariHelper.IsDoubleAtari(tryBoard, p, c))
                return true;

            //check opponent at liberty point
            if (tryBoard.OpponentAtStoneNeighbour(p, c.Opposite()).Any())
                return true;

            //check capture at liberty point
            Point q = tryBoard.GetMoveLiberties(p).FirstOrDefault();
            if (!q.IsEmpty() && tryBoard.OneLibertyGroup(q, c).Any())
                return true;

            return false;
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
            if (eyePoint.Any(n => !RedundantAtMustHaveMove(tryBoard, n)))
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
        /// Capture at liberty <see cref="UnitTestProject.MustHaveNeutralMoveTest.MustHaveNeutralMoveTest_Scenario_TianLongTu_Q17132" />
        /// </summary>
        private static Boolean MustHaveMoveAtBigTigerMouth(Board suicideBoard, GameTryMove tryMove)
        {
            Point move = tryMove.Move;
            Board tryBoard = tryMove.TryGame.Board;
            Board currentBoard = tryMove.CurrentGame.Board;
            Content c = tryMove.MoveContent;

            //liberties more than one
            if (suicideBoard.MoveGroupLiberties > 1)
                return true;

            //check if redundant
            Point suicideMove = suicideBoard.Move.Value;
            if (!RedundantAtMustHaveMove(tryBoard, suicideMove))
                return true;

            //capture at liberty
            List<Group> eyeGroups = LinkHelper.GetPreviousMoveGroup(currentBoard, tryBoard).Where(e => e.Liberties.Count == 2).ToList();
            IEnumerable<Point> moves = eyeGroups.Select(e => e.Liberties.First(n => !n.Equals(move)));
            if (moves.Any(p => tryBoard.OneLibertyGroup(p, c.Opposite()).Any(n => n.Points.Count >= 3)))
                return true;
            return false;
        }

        /// <summary>
        /// Redundant at must have move.
        /// Check strong neighbour groups <see cref="UnitTestProject.MustHaveNeutralMoveTest.MustHaveNeutralMoveTest_Scenario_Corner_A68" />
        /// <see cref="UnitTestProject.MustHaveNeutralMoveTest.MustHaveNeutralMoveTest_Scenario_TianLongTu_Q17136" />
        /// <see cref="UnitTestProject.MustHaveNeutralMoveTest.MustHaveNeutralMoveTest_Scenario_Corner_A84" />
        /// Check liberty fight <see cref="UnitTestProject.MustHaveNeutralMoveTest.MustHaveNeutralMoveTest_Scenario_XuanXuanGo_A54_2" />
        /// </summary>
        private static Boolean RedundantAtMustHaveMove(Board tryBoard, Point tigerMouth)
        {
            Content c = tryBoard.MoveGroup.Content;
            Board b = tryBoard;
            if (tryBoard[tigerMouth] == Content.Empty)
            {
                b = tryBoard.MakeMoveOnNewBoard(tigerMouth, c);
                if (b == null) return true;
            }

            //check strong neighbour groups
            if (WallHelper.StrongNeighbourGroups(b, tigerMouth, c)) return true;

            //check one neighbour group
            List<Group> ngroups = tryBoard.GetGroupsFromStoneNeighbours(tigerMouth, c);
            if (ngroups.Count == 1)
            {
                //check liberty fight
                if (tryBoard.GetNeighbourGroups(ngroups.First()).Any(n => !WallHelper.IsNonKillableGroup(tryBoard, n) && LinkHelper.FindDiagonalCut(tryBoard, n).Item1 != null))
                    return false;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Check liberty fight at covered eye.
        /// <see cref="UnitTestProject.MustHaveNeutralMoveTest.MustHaveNeutralMoveTest_20221128_2" />
        /// <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_x" />
        /// <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_x_2" />
        /// </summary>
        private static Boolean CheckLibertyFightAtCoveredEye(Board board, Point eye, Content c)
        {
            Group group = board.GetGroupsFromStoneNeighbours(eye, c.Opposite()).First();
            foreach (Group dgroup in LinkHelper.GetAllDiagonalGroups(board, group))
            {
                (_, List<Point> diagonals) = LinkHelper.FindDiagonalCut(board, dgroup);
                if (diagonals == null) continue;
                if (diagonals.Any(n => ImmovableHelper.CheckConnectAndDie(board, board.GetGroupAt(n), false))) continue;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Check covered eye at neutral point.
        /// Check connect and die <see cref="UnitTestProject.DailyGoProblems.DailyGoProblems_20260103_8" />
        /// </summary>
        private static Boolean CheckCoveredEyeAtNeutralPoint(GameTryMove tryMove)
        {
            Board tryBoard = tryMove.TryGame.Board;
            Board currentBoard = tryMove.CurrentGame.Board;
            Point move = tryBoard.Move.Value;
            Content c = tryMove.MoveContent;
            //check liberty fight
            if (tryBoard.GetStoneNeighbours().Any(n => EyeHelper.FindCoveredEye(tryBoard, n, c) && CheckLibertyFightAtCoveredEye(currentBoard, n, c)))
                return true;

            //check connect and die
            if (tryBoard.MoveGroup.Points.Count == 1 && tryBoard.MoveGroupLiberties == 1 && EyeHelper.IsCovered(currentBoard, move, c.Opposite()))
            {
                Board captureBoard = ImmovableHelper.CaptureSuicideGroup(tryBoard);
                List<Group> ngroups = captureBoard.GetGroupsFromStoneNeighbours(move, c);
                if (ngroups.Any(n => n.Points.Count >= 3 && ImmovableHelper.CheckConnectAndDie(captureBoard, n)))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Validate neutral point.
        /// Check link for groups <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_Scenario_XuanXuanQiJing_Weiqi101_18497" />
        /// <see cref="UnitTestProject.NeutralPointMoveTest.NeutralPointMoveTest_Scenario_XuanXuanQiJing_Weiqi101_7245" />
        /// </summary>
        public static Boolean ValidateNeutralPoint(GameTryMove tryMove, GameTryMove opponentMove = null)
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
            if (KoHelper.NeutralPointDoubleKo(tryBoard))
                return false;
            //check reverse ko for neutral point
            if (KoHelper.CheckReverseKoForNeutralPoint(tryBoard))
                return false;
            //check covered eye
            if (CheckCoveredEyeAtNeutralPoint(tryMove))
                return false;
            if (opponentMove != null)
            {
                //check opponent kill formation
                if (CheckOpponentKillFormationAtNeutralPoint(opponentMove))
                    return false;
                //check covered point suicidal move
                if (CoveredPointSuicidalWithCornerFormation(opponentMove))
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Check opponent kill formation at neutral point.
        /// <see cref="UnitTestProject.KillerFormationTest.KillerFormationTest_Scenario_TianLongTu_Q16827" />
        /// <see cref="UnitTestProject.KillerFormationTest.KillerFormationTest_Scenario_TianLongTu_Q16827_2" />
        /// </summary>
        private static Boolean CheckOpponentKillFormationAtNeutralPoint(GameTryMove tryMove)
        {
            Board tryBoard = tryMove.TryGame.Board;
            Board currentBoard = tryMove.CurrentGame.Board;
            Point move = tryBoard.Move.Value;
            Content c = tryMove.MoveContent;
            HashSet<Point> liberties = tryBoard.GetLibertiesOfGroups(tryBoard.GetGroupsFromStoneNeighbours());
            List<Group> kgroups = liberties.Select(n => GroupHelper.GetDirectKillerGroup(tryBoard, n, c.Opposite())).Where(n => n != null).Distinct().ToList();
            foreach (Group kgroup in kgroups)
            {
                List<Point> contentPoints = kgroup.Points.Where(n => tryBoard[n] == c).ToList();
                List<Group> groups = tryBoard.GetGroupsFromPoints(contentPoints).ToList();
                if (groups.Count != 1 || groups.First().Points.Count < 4) continue;
                if (liberties.Count(n => GroupHelper.GetKillerGroupFromCache(tryBoard, n, c.Opposite()) != kgroup) != 1) continue;

                //check kill formation
                Board b = KillerFormationHelper.DeadFormationInBothAlive(tryBoard, kgroup, 3).Item2;
                if (b == null) continue;
                if (tryBoard.GetStoneNeighbours(b.Move).Any(n => tryBoard[n] == c.Opposite())) continue;
                if (KillerFormationHelper.IsKillerFormationFromFunc(tryBoard, groups.First())) continue;
                return true;
            }
            return false;
        }
        #endregion

        #region restore neutral points
        /// <summary>
        /// Restore neutral move. Move restored on end game to kill survival group.
        /// No try moves left <see cref="UnitTestProject.MustHaveNeutralMoveTest.MustHaveNeutralMoveTest_Scenario_Side_A20" />
        /// Connect and die end move <see cref="UnitTestProject.RestoreNeutralMoveTest.RestoreNeutralMoveTest_Scenario_XuanXuanGo_A26" />
        /// </summary>
        public static void RestoreNeutralMove(Game g, List<GameTryMove> tryMoves, List<GameTryMove> redundantTryMoves)
        {
            List<GameTryMove> neutralPointMoves = redundantTryMoves.Where(e => e.IsNeutralPoint).ToList();
            if (neutralPointMoves.Count == 0) return;
            Content c = neutralPointMoves.First().MoveContent;
            //remove unnecessary moves
            neutralPointMoves.RemoveAll(n => n.MoveGroupLiberties == 1);
            neutralPointMoves.RemoveAll(n => !n.TryGame.Board.GetStoneAndDiagonalNeighbours().Any(s => n.TryGame.Board[s] == c.Opposite()));
            if (neutralPointMoves.Count == 0) return;
            //specific neutral point
            GameTryMove specificNeutralMove = GetSpecificNeutralMove(g, neutralPointMoves);
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
                    GameTryMove tryMove = GetGenericNeutralMove(g, neutralPointMoves);
                    if (tryMove != null)
                    {
                        tryMoves.Add(tryMove);
                        neutralPointMoves.Remove(tryMove);
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
                if (tryMoves.Select(t => t.TryGame.Board).All(t => ImmovableHelper.CheckConnectAndDie(t) || SuicideGroupNearCapture(t)))
                    tryMoves.Add(neutralPointMoves.First());
            }
        }

        /// <summary>
        /// Suicide group near capture.
        /// <see cref="UnitTestProject.RestoreNeutralMoveTest.RestoreNeutralMoveTest_Scenario_Corner_B21" /> 
        /// <see cref="UnitTestProject.RestoreNeutralMoveTest.RestoreNeutralMoveTest_Scenario_WuQingYuan_Q6150" /> 
        /// <see cref="UnitTestProject.RestoreNeutralMoveTest.RestoreNeutralMoveTest_Scenario_TianLongTu_Q16490" /> 
        /// </summary>
        private static Boolean SuicideGroupNearCapture(Board board)
        {
            if (board.MoveGroupLiberties < 2 || board.MoveGroupLiberties > 3) return false;
            if (!WallHelper.IsStrongGroup(board)) return false;
            foreach (Group ngroup in board.GetNeighbourGroups())
            {
                if (ngroup.Liberties.Count > 2 || WallHelper.IsNonKillableGroup(board, ngroup)) continue;
                foreach (Group targetGroup in AtariHelper.AtariByGroup(board, ngroup))
                {
                    Board b = ImmovableHelper.CaptureSuicideGroup(board, targetGroup);
                    if (!WallHelper.IsStrongGroup(b, board.MoveGroup))
                        return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Get specific neutral move to target survival groups.
        /// <see cref="UnitTestProject.SpecificNeutralMoveTest.SpecificNeutralMoveTest_Scenario_XuanXuanQiJing_Weiqi101_B51" />
        /// </summary>
        public static GameTryMove GetSpecificNeutralMove(Game g, List<GameTryMove> neutralPointMoves)
        {
            GameTryMove tryMove = null;
            List<Group> killerGroups = GroupHelper.GetKillerGroups(g.Board);
            List<Group> immovableGroups = IsImmovableKill(g, killerGroups).ToList();
            if (immovableGroups.Any())
                tryMove = immovableGroups.Select(gr => SpecificKillWithImmovablePoints(g.Board, neutralPointMoves, gr)).FirstOrDefault(n => n != null);
            else
                tryMove = SpecificKillWithLibertyFight(g.Board, neutralPointMoves, killerGroups);
            return tryMove;
        }

        /// <summary>
        /// Is immovable kill.
        /// Conditions for specific kill with immovable points. <see cref="UnitTestProject.SpecificNeutralMoveTest.SpecificNeutralMoveTest_Scenario_XuanXuanGo_A54" />
        /// Covered eye liberty <see cref="UnitTestProject.SpecificNeutralMoveTest.SpecificNeutralMoveTest_Scenario_XuanXuanGo_A54_3" />
        /// One liberty <see cref="UnitTestProject.SpecificNeutralMoveTest.SpecificNeutralMoveTest_Scenario_TianLongTu_Q16827_4" />
        /// </summary>
        public static IEnumerable<Group> IsImmovableKill(Game g, List<Group> killerGroups)
        {
            foreach (Group killerGroup in killerGroups)
            {
                Content c = killerGroup.Content;
                List<Group> ngroups = g.Board.GetNeighbourGroups(killerGroup).Where(n => n.Liberties.Count == 3).ToList();
                foreach (Group ngroup in ngroups)
                {
                    List<Point> killerLiberties = ngroup.Liberties.Where(n => GroupHelper.GetDirectKillerGroup(g.Board, n, c.Opposite()) == killerGroup).ToList();
                    if (killerLiberties.Count < 1 || killerLiberties.Count > 2) continue;
                    if (!GroupHelper.IsLibertyGroup(killerGroup, g.Board)) continue;
                    yield return killerGroup;
                }
            }
        }

        /// <summary>
        /// Specific kill with immovable points.
        /// <see cref="UnitTestProject.SpecificNeutralMoveTest.SpecificNeutralMoveTest_Scenario5dan27" />
        /// <see cref="UnitTestProject.SpecificNeutralMoveTest.SpecificNeutralMoveTest_Scenario_TianLongTu_Q16735" />
        /// One shared liberty <see cref="UnitTestProject.SpecificNeutralMoveTest.SpecificNeutralMoveTest_Scenario_XuanXuanGo_A54_2" />
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
                foreach (Group group in tryBoard.GetGroupsFromStoneNeighbours())
                {
                    if (groups.Contains(group)) continue;
                    groups.Add(group);
                    if (group.Liberties.Count != 2) continue;

                    //shared liberty within killer group
                    List<Point> sharedLiberties = group.Liberties.Intersect(killerLiberties).ToList();
                    if (!(sharedLiberties.Count >= 1 && sharedLiberties.Count <= 2)) continue;
                    if (sharedLiberties.All(p => ImmovableHelper.IsSuicidalMove(tryBoard, p, c.Opposite())))
                        return neutralPointMove;

                    //check one liberty group
                    List<Group> oneLibertyGroup = board.OneLibertyNeighbourGroup(group);
                    if (oneLibertyGroup.Count != 1) continue;
                    Point q = sharedLiberties.FirstOrDefault(n => !oneLibertyGroup.First().Liberties.Contains(n));
                    if (q.IsEmpty() || ImmovableHelper.IsSuicidalMove(tryBoard, q, c.Opposite()))
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
        /// Get generic neutral move. Killer group required.
        /// Check diagonal cut <see cref="UnitTestProject.GenericNeutralMoveTest.GenericNeutralMoveTest_XuanXuanGo_A55" />
        /// Check covered eye <see cref="UnitTestProject.GenericNeutralMoveTest.GenericNeutralMoveTest_Scenario_XuanXuanQiJing_Weiqi101_18410" />
        /// </summary>
        public static GameTryMove GetGenericNeutralMove(Game g, List<GameTryMove> neutralPointMoves)
        {
            if (neutralPointMoves.Count == 0) return null;
            Content c = neutralPointMoves.First().MoveContent;

            List<Group> killerGroups = GroupHelper.GetKillerGroups(g.Board).Where(n => GroupHelper.IsLibertyGroup(n, g.Board)).ToList();
            if (killerGroups.Count == 0) return null;
            //cover all neutral points
            Board coveredBoard = new Board(g.Board);
            neutralPointMoves.ForEach(m => coveredBoard[m.Move] = c);

            foreach (Group killerGroup in killerGroups)
            {
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

                    //check diagonal cut
                    Board b = neutralMove.TryGame.Board;
                    if (b.GetGroupsFromStoneNeighbours().Any(n => LinkHelper.FindDiagonalCut(b, n).Item1 != null))
                        return neutralMove;
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
        /// Check diagonal killer group <see cref="UnitTestProject.SurvivalTigerMouthMoveTest.RedundantTigerMouthMove_Scenario_WuQingYuan_Q15126" />
        /// <see cref="UnitTestProject.SurvivalTigerMouthMoveTest.SurvivalTigerMouthMoveTest_Scenario_XuanXuanGo_B31" />
        /// Check one point atari move <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WuQingYuan_Q31428" />
        /// Check both alive for opponent move <see cref="UnitTestProject.SurvivalTigerMouthMoveTest.RedundantTigerMouthMove_Scenario_XuanXuanGo_A151_101Weiqi" />
        /// <see cref="UnitTestProject.SurvivalTigerMouthMoveTest.SurvivalTigerMouthMoveTest_Scenario_Nie67" />
        /// Check snapback <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_GuanZiPu_A26" />
        /// </summary>
        private static Boolean RedundantTigerMouth(GameTryMove tryMove, GameTryMove opponentMove = null)
        {
            Point move = tryMove.Move;
            Board tryBoard = tryMove.TryGame.Board;
            Board currentBoard = tryMove.CurrentGame.Board;
            Content c = tryMove.MoveContent;
            //ensure is tiger mouth
            if (tryBoard.MoveGroup.Points.Count != 1 || !tryMove.IsNegligible) return false;
            Board capturedBoard = ImmovableHelper.IsConfirmTigerMouth(currentBoard, tryBoard);
            if (capturedBoard == null) return false;

            //check covered point suicidal move
            if (CoveredPointSuicidalWithCornerFormation(tryMove, capturedBoard))
                return false;

            //check one point atari move
            if (KillerFormationHelper.OnePointAtariMove(tryBoard, currentBoard))
                return false;

            //check strong groups
            if (tryBoard.GetNeighbourGroups().All(n => n.Liberties.Count > 2) && GroupHelper.CheckKillerGroupPoints(tryBoard, move, c.Opposite()) == null)
                return true;

            //find immovable point at diagonal
            List<Point> diagonalPoints = ImmovableHelper.GetDiagonalsOfTigerMouth(tryBoard, move, c.Opposite());
            diagonalPoints = diagonalPoints.Where(d => ImmovableHelper.IsImmovablePoint(currentBoard, d, c.Opposite())).ToList();
            foreach (Point d in diagonalPoints)
            {
                if (WallHelper.TargetWithAnyNonKillableGroup(currentBoard, d, c))
                    return true;

                if (opponentMove == null)
                {
                    Group diagonalKillerGroup = GroupHelper.GetDirectKillerGroup(currentBoard, d, c.Opposite());
                    if (diagonalKillerGroup != null)
                    {
                        //check diagonal killer group
                        HashSet<Group> opponentGroups = currentBoard.GetGroupsFromPoints(diagonalKillerGroup.Points.Where(n => currentBoard[n] == c).ToList());
                        if (opponentGroups.Any(n => !ImmovableHelper.CheckConnectAndDie(tryBoard, n, false)))
                            continue;
                    }
                    else
                    {
                        //check atari
                        if (CheckAtariAtTigerMouth(tryMove, d))
                            continue;
                        //check snapback
                        if (ImmovableHelper.CheckSnapbackFromMove(tryBoard))
                            continue;
                    }
                    //check kill covered eye
                    if (KillCoveredEyeAtDiagonal(tryBoard, currentBoard))
                        continue;
                    return true;
                }
                else
                {
                    //check both alive for opponent move
                    if (BothAliveHelper.CheckForBothAliveAtMove(opponentMove.TryGame.Board))
                        continue;
                    return true;
                }
            }

            //no diagonal tiger mouth
            if (diagonalPoints.Count == 0)
            {
                if (TigerMouthWithoutDiagonalMouth(tryMove, capturedBoard, opponentMove))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Check atari at tiger mouth.
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WuQingYuan_Q31536" />
        /// <see cref="UnitTestProject.SurvivalTigerMouthMoveTest.RedundantTigerMouthMove_Scenario_GuanZiPu_A4" />
        /// Check no eye for survival <see cref="UnitTestProject.SurvivalTigerMouthMoveTest.RedundantTigerMouthMove_Scenario_Corner_A27_2" />
        /// Check killer group <see cref="UnitTestProject.SurvivalTigerMouthMoveTest.RedundantTigerMouthMove_Scenario_WuQingYuan_Q31673" />
        /// Check diagonal cut <see cref="UnitTestProject.SurvivalTigerMouthMoveTest.RedundantTigerMouthMove_Scenario_Corner_A20" />
        /// </summary>
        private static Boolean CheckAtariAtTigerMouth(GameTryMove tryMove, Point diagonal)
        {
            Point move = tryMove.Move;
            Board currentBoard = tryMove.CurrentGame.Board;
            Board tryBoard = tryMove.TryGame.Board;
            Content c = tryMove.MoveContent;
            if (!tryBoard.AtariTargets.Any()) return false;
            if (currentBoard[diagonal] != Content.Empty) return false;
            //check no eye for survival and killer group
            if (WallHelper.NoEyeForSurvival(currentBoard, diagonal, c.Opposite()) && GroupHelper.CheckKillerGroupPoints(tryBoard, move, c.Opposite()) == null) return false;
            if (tryBoard.PointWithinMiddleArea())
                return true;
            //check diagonal cut
            if (tryBoard.GetStoneNeighbours(diagonal).Any(n => tryBoard[n] == c && LinkHelper.FindDiagonalCut(tryBoard, tryBoard.GetGroupAt(n)).Item1 != null))
                return true;
            return false;
        }

        /// <summary>
        /// Redundant tiger mouth without diagonal mouth.
        /// Check both alive for opponent move <see cref="UnitTestProject.SurvivalTigerMouthMoveTest.RedundantTigerMouthMove_Scenario_GuanZiPu_A35" />
        /// </summary>
        private static Boolean TigerMouthWithoutDiagonalMouth(GameTryMove tryMove, Board capturedBoard, GameTryMove opponentMove = null)
        {
            Point move = tryMove.Move;
            Board currentBoard = tryMove.CurrentGame.Board;
            Board tryBoard = tryMove.TryGame.Board;
            Content c = tryMove.MoveContent;
            //suicide within real eye at suicidal redundant move
            if (EyeHelper.FindSemiSolidEye(capturedBoard, move, c.Opposite()))
                return false;
            //check covered eye
            if (CheckCoveredEyeAtTigerMouth(tryBoard, capturedBoard, opponentMove))
                return false;
            //check for three opponent groups
            if (CheckThreeOpponentGroupsAtTigerMouth(tryMove, capturedBoard))
                return false;
            //check weak group
            if (CheckWeakGroupAtTigerMouth(tryBoard, capturedBoard))
                return false;
            //check side move
            if (CheckSideMoveAtTigerMouth(tryMove))
                return false;
            //check both alive for opponent move
            if (opponentMove != null && BothAliveHelper.CheckForBothAliveAtMove(opponentMove.TryGame.Board))
                return false;
            return true;
        }

        /// <summary>
        /// Check three opponent groups at tiger mouth.
        /// <see cref="UnitTestProject.SurvivalTigerMouthMoveTest.RedundantTigerMouthMove_Scenario_TianLongTu_Q16925" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_GuanZiPu_Q1970" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WuQingYuan_Q30935" />
        /// Check atari move <see cref="UnitTestProject.SurvivalTigerMouthMoveTest.RedundantTigerMouthMove_Scenario_WindAndTime_Q29277" />
        /// Check immovable point <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanQiJing_A38_3" />
        /// Check for killer group <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_Phenomena_B6" />
        /// <see cref="UnitTestProject.DailyGoProblems.DailyGoProblems_20221020_6" />
        /// </summary>
        private static Boolean CheckThreeOpponentGroupsAtTigerMouth(GameTryMove tryMove, Board capturedBoard)
        {
            Point move = tryMove.Move;
            Board currentBoard = tryMove.CurrentGame.Board;
            Board tryBoard = tryMove.TryGame.Board;
            Content c = tryMove.MoveContent;
            //check three opponent groups
            if (!KillerFormationHelper.ThreeOpponentGroupsAtMove(tryBoard))
                return false;
            if (WallHelper.TargetWithAnyNonKillableGroup(currentBoard, move, c))
                return false;

            //check atari move
            if (tryBoard.IsAtariMove)
            {
                if (tryBoard.AtariTargets.Count > 1) return true;
                if (!WallHelper.NoEyeForSurvival(capturedBoard, tryBoard.AtariTargets.First().Liberties.First(), c.Opposite()))
                    return true;
            }

            //check diagonal point
            foreach (Point p in capturedBoard.GetDiagonalNeighbours(move))
            {
                //check immovable point
                if (GroupHelper.CheckKillerGroupPoints(tryBoard, move, c.Opposite()) != null && !ImmovableHelper.IsImmovablePoint(capturedBoard, p, c.Opposite()))
                    continue;
                //check for killer group
                Group kgroup = GroupHelper.GetDirectKillerGroup(capturedBoard, p, c.Opposite());
                if (kgroup != null && kgroup.Points.Count <= 3)
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Check side move at tiger mouth.
        /// No opponent in middle area <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_Corner_A84" />
        /// <see cref="UnitTestProject.SurvivalTigerMouthMoveTest.RedundantTigerMouthMove_Scenario_TianLongTu_Q16827" />
        /// Check for killer group <see cref="UnitTestProject.SurvivalTigerMouthMoveTest.RedundantTigerMouthMove_20230505_8" />
        /// <see cref="UnitTestProject.SurvivalTigerMouthMoveTest.RedundantTigerMouthMove_20221220_7" />
        /// <see cref="UnitTestProject.SurvivalTigerMouthMoveTest.RedundantTigerMouthMove_Scenario_XuanXuanGo_A28" />
        /// </summary>
        private static Boolean CheckSideMoveAtTigerMouth(GameTryMove tryMove)
        {
            Point move = tryMove.Move;
            Board currentBoard = tryMove.CurrentGame.Board;
            Board tryBoard = tryMove.TryGame.Board;
            Content c = tryMove.MoveContent;
            if (tryBoard.PointWithinMiddleArea()) return false;
            Point? d = LinkHelper.CheckPointsBetweenDiagonalsAtMove(tryBoard);
            if (d == null)
            {
                //no opponent in middle area
                if (!tryBoard.CornerPoint() && currentBoard.GetGroupsFromStoneNeighbours(move, c).Any(n => n.Liberties.Count <= 2))
                    return true;
            }
            else
            {
                //check for killer group
                if (tryBoard.GetDiagonalNeighbours().Any(n => tryBoard[n] == c)) return false;
                if (tryBoard.GetNeighbourGroups().Count != 2) return false;
                if (tryBoard[d.Value] != Content.Empty || tryBoard.GetGroupsFromStoneNeighbours(d.Value, c).Count != 2) return false;
                if (!GroupHelper.IsSingleGroupWithinKillerGroup(tryBoard))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Check covered eye at tiger mouth.
        /// <see cref="UnitTestProject.SurvivalTigerMouthMoveTest.RedundantTigerMouthMove_Scenario_TianLongTu_Q16738" />
        /// Check diagonal at move <see cref="UnitTestProject.SurvivalTigerMouthMoveTest.RedundantTigerMouthMove_Scenario_XuanXuanQiJing_B57_2" />
        /// Check suicidal move <see cref="UnitTestProject.SurvivalTigerMouthMoveTest.RedundantTigerMouthMove_Scenario_WindAndTime_Q30225_2" />
        /// </summary>
        private static Boolean CheckCoveredEyeAtTigerMouth(Board tryBoard, Board capturedBoard, GameTryMove opponentMove = null)
        {
            Point move = tryBoard.Move.Value;
            Content c = tryBoard.MoveGroup.Content;
            //check is covered
            if (!EyeHelper.IsCovered(tryBoard, move, c.Opposite())) return false;

            List<Point> npoints = capturedBoard.GetStoneNeighbours().Where(n => !n.Equals(move) && capturedBoard[n] != c.Opposite()).ToList();
            List<Group> killerGroups = npoints.Select(n => GroupHelper.GetDirectKillerGroup(capturedBoard, n, c.Opposite())).Where(s => s != null).Distinct().ToList();

            //check diagonal at move
            if (opponentMove == null && KillerFormationHelper.TigerMouthAtDiagonal(tryBoard))
            {
                if (killerGroups.Count == 0 || killerGroups.Any(n => n.Points.Count(s => capturedBoard[s] == c) <= 2))
                    return false;
            }

            //check real eye
            if (!killerGroups.Any(n => EyeHelper.FindRealEyeWithinEmptySpace(capturedBoard, n)))
                return true;

            //check suicidal move
            if (opponentMove != null)
            {
                foreach (Group group in tryBoard.GetGroupsFromStoneNeighbours())
                {
                    if (group.Liberties.Count != 1) continue;
                    Point p = group.Liberties.First();
                    if (tryBoard.GetGroupsFromStoneNeighbours(p, c.Opposite()).Count == 0) continue;
                    if (ImmovableHelper.IsSuicidalMoveForBothPlayers(capturedBoard, p))
                        return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Check weak group at tiger mouth.
        /// Check snapback <see cref="UnitTestProject.SurvivalTigerMouthMoveTest.RedundantTigerMouthMove_Scenario_TianLongTu_Q17081" />
        /// Check diagonal at move <see cref="UnitTestProject.SurvivalTigerMouthMoveTest.RedundantTigerMouthMove_Scenario_XuanXuanGo_A26_2" />
        /// <see cref="UnitTestProject.SurvivalTigerMouthMoveTest.RedundantTigerMouthMove_Scenario_XuanXuanGo_A26_3" />
        /// Check hostile group <see cref="UnitTestProject.SurvivalTigerMouthMoveTest.RedundantTigerMouthMove_Scenario_GuanZiPu_A2Q29_101Weiqi" />
        /// </summary>
        private static Boolean CheckWeakGroupAtTigerMouth(Board tryBoard, Board capturedBoard)
        {
            Point move = tryBoard.Move.Value;
            Content c = tryBoard.MoveGroup.Content;
            List<Group> ngroups = capturedBoard.GetGroupsFromStoneNeighbours(move, c);
            if (ngroups.Count == 1) return false;
            if (!ngroups.Any(n => n.Liberties.Count <= 2 && n.Points.Count > 1)) return false;
            //check snapback
            if (LinkHelper.GetDiagonalGroups(tryBoard).Any(n => ImmovableHelper.IsSnapback(tryBoard, tryBoard.MoveGroup, n)))
                return true;

            //check diagonal at move
            if (capturedBoard.GetMoveLiberties().Count == 1 && KillerFormationHelper.TigerMouthAtDiagonal(tryBoard))
                return true;

            //check hostile group
            if (capturedBoard.MoveGroup.Points.Count > 1 && capturedBoard.MoveGroupLiberties == 2)
            {
                if (!WallHelper.IsHostileGroup(capturedBoard))
                    return true;
            }
            return false;
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
                List<Group> ngroups = tryBoard.GetGroupsFromStoneNeighbours(e, c);
                if (ngroups.Any(n => WallHelper.IsNonKillableGroup(tryBoard, n))) continue;
                List<Group> groups = ngroups.Where(n => n.Liberties.Count <= 2).ToList();
                if (groups.Count < 2) continue;
                if (groups.Any(n => tryBoard.GetNeighbourGroups(n).Any(s => s != tryBoard.MoveGroup && !WallHelper.IsNonKillableGroup(tryBoard, s))))
                    return true;
            }
            return false;
        }

        #endregion

        #region redundant eye diagonal
        /// <summary>
        /// Survival eye diagonal move.
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
            diagonals.RemoveAll(d => GroupHelper.GetDirectKillerGroup(currentBoard, d, c) == null);
            if (diagonals.Count == 0) return false;

            //make opponent move
            GameTryMove opponentMove = tryMove.MakeMoveWithOpponentAtSamePoint();
            if (opponentMove == null) return false;
            Board opponentBoard = opponentMove.TryGame.Board;
            //check diagonals are real eyes
            if (!diagonals.All(eye => EyeHelper.FindRealEyeWithinEmptySpace(opponentBoard, eye, c)))
                return false;

            //check other surrounding points are not possible eyes
            IEnumerable<Point> neighbourPts = tryBoard.GetStoneAndDiagonalNeighbours().Except(diagonals);
            if (neighbourPts.Any(q => !WallHelper.NoEyeForSurvival(tryBoard, q, c)))
                return false;

            //check link to groups other than eye groups
            if (LinkHelper.PossibleLinkForGroups(tryBoard, currentBoard))
                return false;

            if (CoveredPointSuicidalWithCornerFormation(opponentMove))
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

        #region redundant ko
        /// <summary>
        /// Redundant survival ko move.
        /// <see cref="UnitTestProject.RedundantKoMoveTest.RedundantKoMoveTest_Scenario_XuanXuanGo_A46_101Weiqi" />
        /// <see cref="UnitTestProject.RedundantKoMoveTest.RedundantKoMoveTest_SimpleSeki" />
        /// </summary>
        public static Boolean RedundantSurvivalKoMove(GameTryMove tryMove)
        {
            Board tryBoard = tryMove.TryGame.Board;
            Board currentBoard = tryMove.CurrentGame.Board;
            Content c = tryBoard.MoveGroup.Content;
            if (!KoHelper.IsKoFight(tryBoard)) return false;
            if (!KoHelper.KoContentEnabled(c, tryBoard.GameInfo))
            {
                //check pre-ko moves
                if (tryBoard.KoCapture == null) return false;
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
            Point move = tryBoard.Move.Value;
            Content c = tryBoard.MoveGroup.Content;
            Point? eyePoint = KoHelper.GetKoEyePoint(tryBoard);
            if (eyePoint == null) return false;

            //ko fight at non killable group
            if (KoHelper.IsNonKillableGroupKoFight(tryBoard))
            {
                List<Group> ngroups = tryBoard.GetGroupsFromStoneNeighbours();
                if (ngroups.All(t => WallHelper.TargetWithAllNonKillableGroups(tryBoard, t)))
                    return true;
                if (!WallHelper.StrongNeighbourGroups(tryBoard))
                    return false;
                //check liberty fight
                if (CheckLibertyFightAtCoveredEye(tryBoard, move, c.Opposite()))
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
            List<Point> diagonals = ImmovableHelper.GetDiagonalsOfTigerMouth(currentBoard, eyePoint.Value, c);
            if (diagonals.Any() && !EyeHelper.FindRealEyeAtDiagonal(diagonals, currentBoard, c))
                return false;

            //check link for groups
            if (LinkHelper.PossibleLinkForGroups(tryBoard, currentBoard))
                return false;

            return true;
        }
        #endregion

        #region filler move

        /// <summary>
        /// Redundant filler move.
        /// <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_TianLongTu_Q17132" /> 
        /// <see cref="UnitTestProject.DailyGoProblems.DailyGoProblems_20250311_8" /> 
        /// Not redundant <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_XuanXuanGo_B10_2" />
        /// Check diagonal cut <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_XuanXuanGo_A171_101Weiqi" />
        /// <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario3dan22" />
        /// </summary>
        public static Boolean RedundantFillerMove(GameTryMove tryMove)
        {
            Board currentBoard = tryMove.CurrentGame.Board;
            Board tryBoard = tryMove.TryGame.Board;
            Point move = tryMove.Move;
            Content c = tryBoard.MoveGroup.Content;

            if (!tryMove.IsNegligible)
                return false;

            //check opponent
            if (tryBoard.OpponentAtStoneNeighbour(move, c).Any())
                return false;
            if (tryBoard.GetStoneNeighbours().Any(n => tryBoard.OpponentAtStoneNeighbour(n, c).Any()))
                return false;

            //check possible space
            int possibleSpace = PossibleSpace(currentBoard, move, c);
            List<int> npossibleSpace = tryBoard.GetMoveLiberties().Select(n => PossibleSpace(currentBoard, n, c)).ToList();
            if (npossibleSpace.Any(n => n < possibleSpace))
                return false;
            if (npossibleSpace.Any(n => n > possibleSpace))
            {
                //check diagonal cut
                if (LinkHelper.FindDiagonalCut(tryBoard).Item1 == null)
                    return true;
            }

            //check edge points
            if (!tryBoard.CornerPoint(move) && !tryBoard.PointWithinMiddleArea(move) && npossibleSpace.Any(n => n >= possibleSpace))
            {
                if (!tryBoard.GetClosestPoints(move, c.Opposite(), 2).Any() && !tryMove.IncreasedKillerGroups)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Possible space.
        /// </summary>
        public static int PossibleSpace(Board board, Point p, Content c)
        {
            return board.GetStoneNeighbours(p).Count(n => board[n] != c);
        }

        #endregion

        #region redundant neural net move
        /// <summary>
        /// Redundant neural net move. For use in LeelaSharp project.
        /// <see cref="UnitTestProject.RedundantNeuralNetMoveTest.RedundantNeuralNetMoveTest_20230423_8" />
        /// Check killer formation <see cref="UnitTestProject.KillerFormationTest.KillerFormationTest_Scenario_WuQingYuan_Q31499" />
        /// Check covered point <see cref="UnitTestProject.BaseLineKillerMoveTest.BaseLineKillerMoveTest_Scenario_XuanXuanQiJing_A53" />
        /// Check both alive <see cref="UnitTestProject.RedundantNeuralNetMoveTest.RedundantNeuralNetMoveTest_Scenario_Corner_B23" />
        /// Check move liberties <see cref="UnitTestProject.RedundantNeuralNetMoveTest.RedundantNeuralNetMoveTest_Scenario_Corner_A130" />
        /// Check ko fight <see cref="UnitTestProject.RedundantNeuralNetMoveTest.RedundantNeuralNetMoveTest_Scenario_XuanXuanGo_A151_101Weiqi" />
        /// </summary>
        public static Boolean RedundantNeuralNetMove(GameTryMove tryMove)
        {
            Game g = tryMove.CurrentGame;
            Board currentBoard = g.Board;
            Board tryBoard = tryMove.TryGame.Board;
            Point move = tryMove.Move;
            Content c = tryBoard.MoveGroup.Content;

            if (!MonteCarloGame.useLeelaZero) return false;
            if (!tryMove.IsNegligible) return false;

            //check opponent at stone neighbour
            if (!tryBoard.GetStoneNeighbours().All(n => tryBoard[n] != c.Opposite()))
                return false;

            //check killer formation
            if (tryBoard.MoveGroupLiberties == 1 && KillerFormationHelper.IsKillerFormationFromFunc(tryBoard))
                return false;

            //check covered point
            if (EyeHelper.IsCovered(currentBoard, move, c))
                return false;

            if (tryBoard.GetDiagonalNeighbours().Any(n => tryBoard[n] == c.Opposite()))
            {
                //check both alive
                if (BothAliveHelper.CheckForBothAliveAtMove(tryBoard))
                    return false;

                //check move liberties
                foreach (Point p in tryBoard.GetMoveLiberties())
                {
                    List<Point> diagonals = LinkHelper.GetDiagonalsAtStoneNeighbours(tryBoard, p, c.Opposite());
                    if (diagonals.Count() > 2) return false;
                    if (diagonals.Count() == 2 && LinkHelper.SingleLibertyBetweenDiagonals(tryBoard, diagonals[0], diagonals[1]))
                        return false;
                }
            }

            //check ko fight
            if (KoHelper.IsForwardOrReverseKoFight(tryBoard) && GroupHelper.CheckKillerGroupPoints(currentBoard, move, c) != null)
                return false;

            //get heat map
            if (g.heatMap == null)
                MonteCarloGame.GetHeatMap(g);

            //check low heat value
            if (g.heatMap[move.x, move.y] <= 1)
                return true;

            return false;
        }

        /// <summary>
        /// Restore neural net move.
        /// <see cref="UnitTestProject.RedundantNeuralNetMoveTest.RedundantNeuralNetMoveTest_Scenario_WindAndTime_Q30199" />
        /// </summary>
        public static void RestoreNeuralNetMove(List<GameTryMove> tryMoves, List<GameTryMove> redundantTryMoves)
        {
            if (MonteCarloGame.useLeelaZero && redundantTryMoves.Any(t => t.IsRedundantNeuralNetMove))
                tryMoves.AddRange(redundantTryMoves.Where(t => t.IsRedundantNeuralNetMove));
        }
        #endregion

    }
}
