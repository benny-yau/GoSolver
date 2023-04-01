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
            }
            else
            {
                //covered eye with more than two liberties
                if (currentBoard.GetGroupsFromStoneNeighbours(move, c.Opposite()).Any(group => group.Liberties.Count <= 2))
                    return false;
                //check three liberty group
                if (ImmovableHelper.CheckThreeLibertyGroupAtBigTigerMouth(tryMove))
                    return false;
            }
            return true;
        }
        #endregion

        #region redundant covered eye move

        /// <summary>
        /// Redundant covered eye move.
        /// <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_Scenario_GuanZiPu_A2Q28_101Weiqi" /> 
        /// Two-point covered eye <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_Scenario_Corner_A68" /> 
        /// Find covered eye for opponent <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_Scenario_XuanXuanQiJing_Weiqi101_18410" /> 
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
        /// Check eye for survival <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_Scenario_WuQingYuan_Q30982" /> 
        /// <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_Scenario_WuQingYuan_Q31398" /> 
        /// <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_Scenario_Corner_B25" /> 
        /// <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_Scenario_WindAndTime_Q29277" /> 
        /// <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_Scenario4dan10" /> 
        /// Check kill opponent <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_Scenario_XuanXuanGo_A34" />
        /// <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_Scenario_WindAndTime_Q30198" />
        /// Check no eye for survival for opponent <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_Scenario_WindAndTime_Q30332" /> 
        /// Check liberty count without covered eye <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_Scenario_XuanXuanQiJing_A64" />
        /// Check must-have move <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_20221019_7" />
        /// <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_20221024_4" />
        /// Check one-point snapback <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_Scenario_WuQingYuan_Q31453" />
        /// <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_Scenario_XuanXuanGo_A37_101Weiqi" />
        /// <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_Scenario_XuanXuanGo_A26_4" /> 
        /// Check atari for ko move <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_Scenario_XuanXuanGo_A26_2" />
        /// Check possible links <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_Scenario_XuanXuanQiJing_Weiqi101_18497_2" />
        /// <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_Scenario_XuanXuanQiJing_Weiqi101_B74" />
        /// <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_Scenario_XuanXuanQiJing_Weiqi101_A40" />
        /// Check for double ko <see cref="UnitTestProject.NeutralPointMoveTest.NeutralPointMoveTest_Scenario_XuanXuanGo_A28_101Weiqi" />
        /// Set neutral point for opponent <see cref="UnitTestProject.NeutralPointMoveTest.NeutralPointMoveTest_Scenario_Corner_B21" />
        /// </summary>
        public static Boolean FindCoveredEyeMove(GameTryMove tryMove, GameTryMove opponentTryMove = null)
        {
            Board tryBoard = tryMove.TryGame.Board;
            Board currentBoard = tryMove.CurrentGame.Board;
            Point move = tryBoard.Move.Value;
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
                Boolean unEscapable = tryBoard.MoveGroup.Liberties.Any(lib => tryBoard.GameInfo.IsMovablePoint[lib.x, lib.y] == false);
                if (unEscapable)
                    eyeGroup = tryBoard.CapturedList.First();
            }
            if (eyeGroup == null) return false;
            if (tryBoard.CapturedList.Any(gr => !eyeGroup.Points.Contains(gr.Points.First()))) return false;

            //check eye for survival
            if (eyeGroup.Points.Any(e => tryBoard.GetDiagonalNeighbours(e).Any(n => !WallHelper.NoEyeForSurvival(tryBoard, n, c) && !EyeHelper.FindRealEyeWithinEmptySpace(currentBoard, n, c))))
                return false;

            //check kill opponent
            if (tryBoard.GetStoneAndDiagonalNeighbours().Append(move).Any(n => !WallHelper.NoEyeForSurvival(currentBoard, n, c.Opposite())))
                return false;

            //check no eye for survival
            if (!WallHelper.NoEyeForSurvivalAtNeighbourPoints(tryBoard))
                return false;

            //check two liberty group to capture neighbour
            if (currentBoard.GetNeighbourGroups(eyeGroup).Any(n => CheckTwoLibertyGroupToCaptureNeighbour(currentBoard, tryBoard, n, eyePoint)))
                return false;

            //check possible links
            if (LinkHelper.PossibleLinkForGroups(tryBoard, currentBoard))
                return false;

            //check for double ko
            if (KoHelper.NeutralPointDoubleKo(tryBoard, currentBoard))
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

                //set neutral point for opponent
                if (WallHelper.IsNonKillableGroup(opponentBoard))
                    opponentTryMove.IsNeutralPoint = true;
            }
            return true;
        }

        /// <summary>
        /// Check two liberty group to capture neighbour.
        /// <see cref="UnitTestProject.CheckForRecursionTest.CheckForRecursionTest_Scenario_Corner_B41" /> 
        /// <see cref="UnitTestProject.NeutralPointMoveTest.NeutralPointMoveTest_Scenario_XuanXuanQiJing_A38" /> 
        /// <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_Scenario_XuanXuanQiJing_A64" />
        /// <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_Scenario_XuanXuanGo_Q18341_2" />
        /// Check eye for suicidal move <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_Scenario_WindAndTime_Q30275" />
        /// <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_Scenario_Corner_A84_3" />
        /// Capture opponent groups <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_Scenario_TianLongTu_Q17154" />
        /// <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_Scenario_WuQingYuan_Q30982_3" />
        /// Check escape capture link <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_Scenario_XuanXuanGo_A26_3" />
        /// Ko fight <see cref="UnitTestProject.RedundantKoMoveTest.RedundantKoMoveTest_Scenario_XuanXuanQiJing_A38_2" />
        /// </summary>
        private static Boolean CheckTwoLibertyGroupToCaptureNeighbour(Board currentBoard, Board tryBoard, Group group, Point capturePoint)
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
                if (ImmovableHelper.EscapeCaptureLink(currentBoard, group, capturePoint))
                    continue;
                return true;
            }
            return false;
        }
        #endregion

        #region fill ko eye move
        /// <summary>
        /// Fill ko eye move. <see cref="UnitTestProject.FillKoEyeMoveTest.FillKoEyeMoveTest_Scenario_XuanXuanGo_A46_101Weiqi" />
        /// Double atari <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WindAndTime_Q30358" /> 
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanGo_A82_101Weiqi" /> 
        /// Check for weak eye group <see cref="UnitTestProject.FillKoEyeMoveTest.FillKoEyeMoveTest_Scenario_Corner_B28" />
        /// Check both alive <see cref="UnitTestProject.FillKoEyeMoveTest.FillKoEyeMoveTest_SimpleSeki" /> 
        /// <see cref="UnitTestProject.RedundantKoMoveTest.RedundantKoMoveTest_Scenario_XuanXuanGo_A151_101Weiqi_2" /> 
        /// Check break link <see cref="UnitTestProject.FillKoEyeMoveTest.FillKoEyeMoveTest_Scenario_WuQingYuan_Q31657" /> 
        /// Ensure group more than one point have more than one liberty <see cref="UnitTestProject.FillKoEyeMoveTest.FillKoEyeMoveTest_Scenario_Nie20" /> 
        /// Check for killer formation <see cref="UnitTestProject.FillKoEyeMoveTest.FillKoEyeMoveTest_Scenario_Corner_A67" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_Nie20" />
        /// Check weak group in connect and die <see cref="UnitTestProject.FillKoEyeMoveTest.FillKoEyeMoveTest_Scenario_XuanXuanGo_B6" /> 
        /// Check suicide at tiger mouth <see cref="UnitTestProject.FillKoEyeMoveTest.FillKoEyeMoveTest_Scenario_TianLongTu_Q16867" /> 
        /// <see cref="UnitTestProject.FillKoEyeMoveTest.FillKoEyeMoveTest_Scenario_GuanZiPu_B3" /> 
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WindAndTime_Q30358" /> 
        /// <see cref="UnitTestProject.SurvivalTigerMouthMoveTest.RedundantTigerMouthMove_Scenario_WindAndTime_Q30225" /> 
        /// Check survival eye <see cref="UnitTestProject.FillKoEyeMoveTest.FillKoEyeMoveTest_Scenario_Corner_A36" /> 
        /// <see cref="UnitTestProject.KoTest.KoTest_Scenario_Corner_A80" /> 
        /// <see cref="UnitTestProject.AtariRedundantMoveTest.AtariRedundantMoveTest_Scenario_WuQingYuan_Q30982" /> 
        /// Two covered eyes <see cref="UnitTestProject.FillKoEyeMoveTest.FillKoEyeMoveTest_Scenario5dan18" />
        /// Check double ko <see cref = "UnitTestProject.FillKoEyeMoveTest.FillKoEyeMoveTest_Scenario_TianLongTu_Q16975" />
        /// <see cref="UnitTestProject.LifeCheckTest.LifeCheckTest_Scenario_XuanXuanQiJing_Weiqi101_18497_2" /> 
        /// <see cref = "UnitTestProject.FillKoEyeMoveTest.FillKoEyeMoveTest_Scenario_WindAndTime_Q30275_2" />
        /// Set as neutral point <see cref="UnitTestProject.FillKoEyeMoveTest.FillKoEyeMoveTest_Scenario_TianLongTu_Q16490" />
        /// </summary>
        public static Boolean FillKoEyeMove(GameTryMove tryMove)
        {
            Point move = tryMove.Move;
            Board currentBoard = tryMove.CurrentGame.Board;
            Board tryBoard = tryMove.TryGame.Board;
            Content c = tryMove.MoveContent;
            //ensure is fill eye
            if (!EyeHelper.FindEye(currentBoard, move, c)) return false;

            (Boolean connectAndDie, Board captureBoard) = ImmovableHelper.ConnectAndDie(tryBoard);
            if (connectAndDie)
            {
                //check for killer formation
                if (KillerFormationHelper.SuicidalKillerFormations(tryBoard, currentBoard))
                    return false;

                //check weak group in connect and die
                if (!CheckWeakGroupInConnectAndDie(tryBoard, tryBoard.MoveGroup))
                    return true;
            }

            //not ko enabled
            List<Group> eyeGroups = currentBoard.GetGroupsFromStoneNeighbours(move, c.Opposite()).ToList();
            if (!KoHelper.KoContentEnabled(c, tryBoard.GameInfo) && eyeGroups.Any(e => e.Points.Count == 1 && e.Liberties.Count == 1))
                return false;

            //ensure group more than one point have more than one liberty
            if (eyeGroups.Any(e => e.Points.Count > 1 && e.Liberties.Count == 1)) return false;

            //double atari
            if (eyeGroups.Count(group => group.Liberties.Count == 1) >= 2)
                return false;

            //check both alive
            if (BothAliveHelper.CheckForBothAliveAtMove(tryBoard))
                return false;

            //check suicide at tiger mouth
            if (ImmovableHelper.SuicideAtBigTigerMouth(tryMove).Item1)
                return false;

            //two covered eyes
            if (eyeGroups.Any(e => e.Liberties.Count == 2 && e.Liberties.All(lib => EyeHelper.FindCoveredEye(currentBoard, lib, c) && !KoHelper.IsKoFight(currentBoard, lib, c).Item1)))
                return false;

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

            //set diagonal eye move
            if (tryBoard.GetDiagonalNeighbours().Any(n => EyeHelper.FindEye(currentBoard, n, c) && !ImmovableHelper.IsImmovablePoint(move, c, currentBoard).Item1))
                tryMove.IsDiagonalEyeMove = true;
            return true;
        }

        /// <summary>
        /// Suicide group near capture.
        /// </summary>
        private static Boolean SuicideGroupNearCapture(Board board)
        {
            if (board.MoveGroupLiberties < 2 || board.MoveGroupLiberties > 3) return false;
            if (ImmovableHelper.CheckConnectAndDie(board)) return false;
            foreach (Group ngroup in board.GetNeighbourGroups())
            {
                if (ngroup.Liberties.Count > 2 || WallHelper.IsNonKillableGroup(board, ngroup)) continue;
                foreach (Group targetGroup in AtariHelper.AtariByGroup(ngroup, board))
                {
                    Board b = ImmovableHelper.CaptureSuicideGroup(board, targetGroup, true);
                    if (b != null && ImmovableHelper.CheckConnectAndDie(b, board.MoveGroup))
                        return true;
                }
            }
            return false;
        }
        #endregion

        #region atari redundant move

        /// <summary>
        /// Redundant atari move.
        /// <see cref="UnitTestProject.AtariRedundantMoveTest.AtariRedundantMoveTest_Scenario_Corner_A9_Ext" />
        /// Check for increased killer groups <see cref="UnitTestProject.AtariRedundantMoveTest.AtariRedundantMoveTest_Scenario_TianLongTu_Q16487" />
        /// <see cref="UnitTestProject.AtariRedundantMoveTest.AtariRedundantMoveTest_Scenario_WuQingYuan_Q31493" />
        /// Check for reverse ko fight <see cref="UnitTestProject.AtariRedundantMoveTest.AtariRedundantMoveTest_Scenario_WuQingYuan_Q30982" />
        /// Check for diagonal killer group <see cref="UnitTestProject.AtariRedundantMoveTest.AtariRedundantMoveTest_Scenario_WindAndTime_Q30225_2" />
        /// <see cref="UnitTestProject.AtariRedundantMoveTest.AtariRedundantMoveTest_Scenario_WindAndTime_Q30225_3" />
        /// Ensure more than one liberty for move group <see cref="UnitTestProject.AtariRedundantMoveTest.AtariRedundantMoveTest_Scenario_Corner_A68" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_TianLongTu_Q16748" />
        /// Check for weak groups <see cref="UnitTestProject.AtariRedundantMoveTest.AtariRedundantMoveTest_Scenario_WuQingYuan_Q31503" />
        /// Check capture secure <see cref="UnitTestProject.AtariRedundantMoveTest.AtariRedundantMoveTest_Scenario_XuanXuanQiJing_Weiqi101_B74" />
        /// Check killer formation <see cref="UnitTestProject.AtariRedundantMoveTest.AtariRedundantMoveTest_Scenario_Side_A25" />
        /// <see cref="UnitTestProject.AtariRedundantMoveTest.AtariRedundantMoveTest_Scenario_Side_A23" />
        /// Count possible eyes at stone neighbours <see cref="UnitTestProject.AtariRedundantMoveTest.AtariRedundantMoveTest_Scenario_XuanXuanGo_A151_101Weiqi" />
        /// </summary>
        public static Boolean AtariRedundantMove(GameTryMove tryMove)
        {
            Board currentBoard = tryMove.CurrentGame.Board;
            Board tryBoard = tryMove.TryGame.Board;
            Point move = tryMove.Move;
            Content c = tryBoard.MoveGroup.Content;
            if (tryBoard.AtariTargets.Count != 1 || tryMove.AtariResolved || tryBoard.MoveGroupLiberties == 1 || tryBoard.CapturedList.Count > 0) return false;
            Group atariTarget = tryBoard.AtariTargets.First();
            Point atariPoint = atariTarget.Points.First();

            Point q = atariTarget.Liberties.First();
            if (!KillerFormationHelper.IsFirstPoint(currentBoard, q, move)) return false;

            //ensure target group cannot escape
            (_, _, Board escapeBoard) = ImmovableHelper.UnescapableGroup(tryBoard, atariTarget);
            if (escapeBoard != null) return false;

            //check for redundant atari within killer group
            Group killerGroup = GroupHelper.GetKillerGroupOfNeighbourGroups(currentBoard, atariPoint, c);
            if (killerGroup == null) return false;
            if (!GroupHelper.IsSingleGroupWithinKillerGroup(currentBoard, atariTarget)) return false;

            //make move at the other liberty
            (Boolean suicidal, Board board) = ImmovableHelper.IsSuicidalMove(q, c, currentBoard);
            if (suicidal) return false;
            //ensure the other move can capture atari target as well
            (_, _, Board escapeBoard2) = ImmovableHelper.UnescapableGroup(board, board.GetGroupAt(atariPoint));
            if (escapeBoard2 != null) return false;

            //check for weak groups
            if (LinkHelper.GetPreviousMoveGroup(currentBoard, tryBoard).Any(gr => gr.Liberties.Count <= 2)) return false;

            //check for bloated eye
            if (KoFightAtBloatedEye(tryBoard, currentBoard))
                return false;
            return true;
        }

        #endregion

        #region suicidal move
        /// <summary>
        /// Suicidal moves are moves that have liberty of one only.
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
            if (SuicidalMoveWithinNonKillableGroup(tryMove))
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
                if (!singlePoint && MultiPointOpponentSuicidalMove(tryMove, opponentMove))
                    return true;
            }
            if (SuicidalMoveWithinNonKillableGroup(opponentMove, tryMove))
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
        private static Boolean SuicidalMoveWithinNonKillableGroup(GameTryMove tryMove, GameTryMove opponentTryMove = null)
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

            //all neighbour groups are non-killable
            List<Group> neighbourGroups = tryBoard.GetNeighbourGroups(killerGroup);
            if (neighbourGroups.All(n => WallHelper.IsNonKillableGroup(tryBoard, n)))
                return true;

            //check any is non killable
            if (!neighbourGroups.Any(n => WallHelper.IsNonKillableGroup(tryBoard, n)))
                return false;

            foreach (Group ngroup in neighbourGroups)
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
        /// Set neutral point move <see cref="UnitTestProject.MonteCarloRuntimeTest.MonteCarloRuntimeTest_Scenario_WuQingYuan_Q31499" />
        /// </summary>
        private static Boolean MultiPointOpponentSuicidalMove(GameTryMove tryMove, GameTryMove opponentMove)
        {
            Board currentBoard = tryMove.CurrentGame.Board;
            Board tryBoard = tryMove.TryGame.Board;
            Point move = tryBoard.Move.Value;
            Content c = tryBoard.MoveGroup.Content;
            if (tryBoard.MoveGroupLiberties == 1 || tryBoard.CapturedList.Count != 0 || tryMove.AtariResolved || tryBoard.AtariTargets.Count != 1) return false;

            Group atariTarget = tryBoard.AtariTargets.First();
            if (tryBoard.GetStoneNeighbours().Any(n => tryBoard[n] == Content.Empty || (tryBoard[n] == c.Opposite() && !tryBoard.GetGroupAt(n).Equals(atariTarget)))) return false;
            //check for unescapable group
            (Boolean unEscapable, _, Board escapeBoard) = ImmovableHelper.UnescapableGroup(tryBoard, atariTarget, false);
            if (unEscapable) return false;

            //check for weak group
            if (CheckWeakGroupInOpponentSuicide(tryBoard, atariTarget))
                return false;

            //check for suicide at big tiger mouth
            if (ImmovableHelper.SuicideAtBigTigerMouth(tryMove).Item1)
                return false;

            //check for both alive
            if (BothAliveHelper.CheckForBothAliveAtMove(tryBoard)) return false;

            //check for bloated eye
            if (KoFightAtBloatedEye(tryBoard, currentBoard))
                return false;

            //check two point atari move
            if (KillerFormationHelper.TwoPointAtariMove(opponentMove.TryGame.Board))
                return false;

            //check link for groups
            if (LinkHelper.PossibleLinkForGroups(tryBoard, currentBoard))
                return false;

            //set neutral point move
            if (WallHelper.IsNonKillableGroup(tryBoard))
                tryMove.IsNeutralPoint = true;
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
            if (tryBoard.GetDiagonalNeighbours().Any(d => tryBoard[d] == Content.Empty && (tryBoard.GetStoneNeighbours(d).Any(n => tryBoard[n] == Content.Empty && KoHelper.MakeKoFight(currentBoard, n, c)) || KoHelper.IsKoFight(currentBoard, d, c).Item1)))
                return true;
            return false;
        }

        /// <summary>
        /// Check weak group in opponent suicide.
        /// <see cref = "UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_TianLongTu_Q16604_3" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_TianLongTu_Q16604_4" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanGo_B32_2" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_Corner_A67_3" />
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
                    if (GetWeakGroup(captureBoard, escapeGroup))
                        return true;
                    //continue escape
                    if (escapeGroup.Liberties.Count == 2 && !WallHelper.IsHostileNeighbourGroup(captureBoard, escapeGroup))
                        return true;
                    return false;
                }
            }

            //escape at liberty point
            Board b = ImmovableHelper.MakeMoveAtLiberty(tryBoard, atariTarget, c.Opposite());
            if (b == null) return true;

            //check weak group
            if (GetWeakGroup(b, b.MoveGroup))
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
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_GuanZiPu_A17_2" />
        /// Check killer move non killable group <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WuQingYuan_Q31563" />
        /// Check redundant corner point <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_TianLongTu_Q2834" />
        /// Check for one-by-three kill <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_GuanZiPu_Q18796_2" />
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
            if (CheckWeakGroupInConnectAndDie(tryBoard, tryBoard.MoveGroup))
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
            //ensure killer group contains only try move
            if (!GroupHelper.IsSingleGroupWithinKillerGroup(tryBoard))
                return false;

            //check liberty surrounded by opponent
            if (KillerFormationHelper.SuicideMoveValidWithOneEmptySpaceLeft(tryBoard))
                return false;

            //ensure all strong neighbour groups
            if (WallHelper.StrongNeighbourGroups(captureBoard, move, c))
                return true;

            return false;
        }

        /// <summary>
        /// Check for weak group with two or less liberties in connect and die.
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_x" />
        /// Reverse connect and die <see cref="UnitTestProject.ImmovableTest.ImmovableTest_Scenario_WindAndTime_Q29277" />
        /// Corner move <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_TianLongTu_Q16446" /> 
        /// Check for double atari for one-point move <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WindAndTime_Q29481" />
        /// Check killable group with two or less liberties <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanGo_B6" />
        /// Check for weak group capturing atari group <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanQiJing_B17" />
        /// </summary>
        private static Boolean CheckWeakGroupInConnectAndDie(Board tryBoard, Group targetGroup)
        {
            Content c = targetGroup.Content;
            Group group = tryBoard.GetCurrentGroup(targetGroup);

            //capture move
            (_, Board b) = ImmovableHelper.ConnectAndDie(tryBoard, group, false);
            if (b == null || b.IsCapturedGroup(group)) return false;

            //check weak group
            if (GetWeakGroup(b, b.GetCurrentGroup(group)))
                return true;

            //escape move at liberty
            Board b2 = ImmovableHelper.MakeMoveAtLiberty(b, group, c);
            if (b2 != null && b2.MoveGroupLiberties == 2 && CheckWeakGroupInConnectAndDie(b2, group))
                return true;

            //escape by capture
            foreach (Group gr in AtariHelper.AtariByGroup(group, b))
            {
                Board b3 = ImmovableHelper.CaptureSuicideGroup(b, gr);
                if (b3 == null) continue;
                Group target = b3.GetCurrentGroup(group);
                if (target.Liberties.Count == 2 && CheckWeakGroupInConnectAndDie(b3, target))
                    return true;
                if (!b3.MoveGroup.Equals(target) && GetWeakGroup(b, b3.MoveGroup))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Get weak group.
        /// </summary>
        private static Boolean GetWeakGroup(Board tryBoard, Group moveGroup)
        {
            if (WallHelper.IsNonKillableFromSetupMoves(tryBoard, moveGroup))
                return false;

            foreach (Group group in tryBoard.GetNeighbourGroups(moveGroup).Where(n => n.Liberties.Count <= 2))
            {
                foreach (Point liberty in group.Liberties)
                {
                    (Boolean suicidal, Board b) = ImmovableHelper.IsSuicidalMove(liberty, group.Content.Opposite(), tryBoard, true);
                    if (suicidal || KoHelper.IsNonKillableGroupKoFight(b, b.MoveGroup)) continue;
                    if (WallHelper.IsNonKillableGroup(b)) continue;
                    return true;
                }
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
            if (tryBoard.MoveGroup.Liberties.Any(lib => tryBoard.CornerPoint(lib) && tryBoard.GetStoneNeighbours(lib).Intersect(tryBoard.MoveGroup.Points).Count() >= 2))
            {
                if (KillerFormationHelper.TwoByTwoFormation(tryBoard, tryBoard.MoveGroup.Points) || LinkHelper.GetPreviousMoveGroup(currentBoard, tryBoard).Count > 1)
                    return false;
            }

            if (EyeHelper.FindEye(tryBoard, liberty, c) || eyeGroups.Count > 1)
                return true;
            return false;
        }

        /// <summary>
        /// Check for real eye in neighbour groups.
        /// Check for split killer group <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_GuanZiPu_B3_3" />
        /// Check for corner six formation <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanQiJing_A38_3" /> 
        /// Check for one-point eye <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_Corner_A30" />
        /// Check for two-point snapback <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_Corner_A55" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WuQingYuan_Q31680_2" />
        /// Check snapback in neighbour groups <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WindAndTime_Q30234_2" />
        /// Check for one-by-three kill <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_GuanZiPu_Q18796_2" />
        /// Check for covered eye group <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WuQingYuan_Q6150" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanGo_A17" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WindAndTime_Q30403_2" />
        /// Check for covered eye move <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_TianLongTu_Q16738_8" />
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
        /// Ensure no shared liberty with neighbour group <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_Corner_A55" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_GuanZiPu_A17_3" />
        /// Check move next to covered point <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_TianLongTu_Q17132_4" />
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

            //ensure no diagonal groups found
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
                List<Point> stoneNeighbours = LinkHelper.GetNeighboursDiagonallyLinked(tryBoard);
                if (stoneNeighbours.Count == 2)
                {
                    //check diagonal at opposite corner of stone neighbours
                    List<Point> diagonals = tryBoard.GetDiagonalNeighbours().Where(d => tryBoard[d] == c.Opposite()).ToList();
                    if (diagonals.Any(d => !tryBoard.GetStoneNeighbours(d).Intersect(stoneNeighbours).Any()))
                        return false;

                    //cut diagonal and kill
                    List<Point> cutDiagonal = LinkHelper.PointsBetweenDiagonals(stoneNeighbours[0], stoneNeighbours[1]);
                    cutDiagonal.Remove(move);
                    Board b = tryBoard.MakeMoveOnNewBoard(cutDiagonal.First(), c, true);
                    if (b != null && stoneNeighbours.Any(n => ImmovableHelper.CheckConnectAndDie(b, b.GetGroupAt(n))))
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
            if (tryBoard.GetStoneNeighbours().Any(n => tryBoard[n] == Content.Empty))
                return false;

            //check for three neighbour groups
            Boolean threeGroups = tryBoard.GetGroupsFromStoneNeighbours(move).Count >= 3;
            if (threeGroups) return false;

            //check killer formation
            if (KillerFormationHelper.SuicidalKillerFormations(tryBoard, currentBoard, captureBoard))
                return false;

            return true;
        }

        /// <summary>
        /// Single point suicide.
        /// </summary>
        public static Boolean SinglePointSuicidalMove(GameTryMove tryMove, GameTryMove opponentTryMove = null)
        {
            Board tryBoard = tryMove.TryGame.Board;
            if (!tryMove.IsNegligible)
                return false;

            //capture suicide stone by move at liberty point
            Board capturedBoard = ImmovableHelper.CaptureSuicideGroup(tryBoard);
            if (capturedBoard == null || KoHelper.IsKoFight(capturedBoard)) return false;
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
        /// Check for snapback  <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanGo_B31" />
        /// Atari move required <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WindAndTime_Q2757" />
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
            if (ImmovableHelper.CheckSnapbackInNeighbourGroups(tryBoard, tryBoard.MoveGroup))
                return false;

            //atari move required
            if (tryBoard.IsAtariMove)
            {
                //check for non two-point group
                Boolean twoPointGroup = KillerFormationHelper.SuicideMoveValidWithOneEmptySpaceLeft(tryBoard);
                if (!twoPointGroup && CheckNonTwoPointGroupInSuicideRealEye(tryMove, capturedBoard))
                    return true;

                //check two point group
                if (twoPointGroup && CheckTwoPointGroupInSuicideRealEye(tryMove, capturedBoard))
                    return true;

                return false;
            }


            //retrieve liberties other than eye liberty
            List<Group> ngroups = capturedBoard.GetNeighbourGroups(tryBoard.MoveGroup);
            HashSet<Point> liberties = capturedBoard.GetLibertiesOfGroups(ngroups);
            liberties.Remove(move);

            if (liberties.Count == 1)
            {
                //suicide for liberty fight
                if (KillerFormationHelper.SuicideForLibertyFight(tryBoard, currentBoard, false))
                    return false;
            }
            else if (liberties.Count == 2)
            {
                //two liberties - suicide for both players
                IEnumerable<Board> moveBoards = GameHelper.GetMoveBoards(capturedBoard, liberties, c);
                foreach (Board b in moveBoards)
                {
                    //both players are suicidal at the liberty
                    Point q = liberties.First(liberty => !liberty.Equals(b.Move));
                    if (GroupHelper.GetKillerGroupFromCache(tryBoard, q, c.Opposite()) != null && ImmovableHelper.IsSuicidalMoveForBothPlayers(b, q))
                        return false;
                }
            }
            else if (liberties.Count == 3)
            {
                //three liberties - suicide for both players
                if (ngroups.Any(n => GroupHelper.GetKillerGroupFromCache(capturedBoard, n.Points.First(), c) == null))
                    return true;
                foreach (Group ngroup in ngroups)
                {
                    List<Point> nLiberties = ngroup.Liberties.Where(lib => !lib.Equals(move)).ToList();
                    if (nLiberties.Count != 2) continue;

                    IEnumerable<Board> moveBoards = GameHelper.GetMoveBoards(capturedBoard, nLiberties, c);
                    foreach (Board b in moveBoards)
                    {
                        //both players are suicidal at the liberty
                        Point q = nLiberties.First(liberty => !liberty.Equals(b.Move));
                        if (!ImmovableHelper.IsSuicidalMove(b, q, c)) continue;
                        Board b2 = ImmovableHelper.IsSuicidalMove(q, c.Opposite(), b).Item2;
                        if (b2 != null && b2.MoveGroupLiberties <= 2)
                            return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Check for non two-point group.
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanGo_B31_3" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario4dan17_2" />
        /// Not redundant <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WuQingYuan_Q31536" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_Corner_A139" />
        /// Real solid eye <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario1dan4_3" />
        /// Check killer group <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario4dan17_2" />
        /// </summary>
        private static Boolean CheckNonTwoPointGroupInSuicideRealEye(GameTryMove tryMove, Board captureBoard)
        {
            Point move = tryMove.Move;
            Board currentBoard = tryMove.CurrentGame.Board;
            Board tryBoard = tryMove.TryGame.Board;
            Content c = tryMove.MoveContent;
            //real solid eye
            if (EyeHelper.FindRealSolidEye(move, c.Opposite(), captureBoard)) return true;
            //get diagonals next to atari target
            List<Point> diagonals = tryBoard.GetDiagonalNeighbours().Where(n => tryBoard[n] != c.Opposite() && tryBoard.GetGroupsFromStoneNeighbours(n, c).Intersect(tryBoard.AtariTargets).Any()).ToList();
            //check killer group
            if (diagonals.Any(d => GroupHelper.GetKillerGroupOfNeighbourGroups(tryBoard, d, c.Opposite()) != null))
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

            //check connect and die
            if (ImmovableHelper.CheckConnectAndDie(capturedBoard))
                return false;

            //check for liberty fight
            HashSet<Group> eyeGroups = capturedBoard.GetGroupsFromStoneNeighbours(move, c);
            if (eyeGroups.Count() != 1) return false;
            Group eyeGroup = eyeGroups.First();
            List<Point> liberties = eyeGroup.Liberties.Where(lib => !lib.Equals(move)).ToList();
            if (liberties.Count > 2) return true;

            IEnumerable<Board> moveBoards = GameHelper.GetMoveBoards(capturedBoard, liberties, c);
            if (moveBoards.Any(b => WallHelper.StrongNeighbourGroups(b, b.GetNeighbourGroups(capturedBoard.MoveGroup))))
                return true;
            return false;
        }


        /// <summary>
        /// Miscellaneous single point suicide.
        /// Check connect and die at diagonal group <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_TianLongTu_Q16738_6" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_TianLongTu_Q16738_7" />
        /// <see cref="UnitTestProject.SurvivalTigerMouthMoveTest.RedundantTigerMouthMove_Scenario_GuanZiPu_B18" />
        /// Suicidal move next to non killable group for survive <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanGo_A27_2" />
        /// Connect and die <see cref="UnitTestProject.ImmovableTest.ImmovableTest_Scenario_XuanXuanGo_B32" />
        /// Liberty more than two required to prevent snapback <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WuQingYuan_Q31680" />
        /// Diagonal neighbours that are non killable groups <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_TianLongTu_Q17160" />
        /// Opponent suicide <see cref="UnitTestProject.FillKoEyeMoveTest.FillKoEyeMoveTest_Scenario_TianLongTu_Q16490" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanGo_A55" />
        /// <see cref="UnitTestProject.SurvivalTigerMouthMoveTest.SurvivalTigerMouthMoveTest_Scenario_Nie67" />
        /// Check connect end move <see cref="UnitTestProject.SurvivalTigerMouthMoveTest.RedundantTigerMouthMove_Scenario_TianLongTu_Q16738_2" />
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
                    //check connect end move
                    if (opponentTryMove != null && !connectAndDie && EyeHelper.IsCovered(tryBoard, move, c.Opposite()))
                        return false;
                    return !connectAndDie;
                }
            }
            else
            {
                //get diagonal neighbours that are non killable groups
                List<Point> diagonalNeighbours = tryBoard.GetDiagonalNeighbours().Where(n => WallHelper.IsNonKillableGroup(tryBoard, n)).ToList();
                Boolean nonKillableSuicide = tryBoard.PointWithinMiddleArea(move) ? diagonalNeighbours.Count >= 2 : diagonalNeighbours.Count >= 1;
                if (!nonKillableSuicide) return false;

                if (NeutralPointSuicidalMove(tryMove)) return false;

                if (diagonalNeighbours.Any(n => LinkHelper.PointsBetweenDiagonals(move, n).Any(d => tryBoard[d] == Content.Empty)))
                    return true;

                //check real eye at diagonal without opposite content
                if (ImmovableHelper.AllConnectAndDie(capturedBoard, move, c.Opposite())) return false;
                if (capturedBoard.GetDiagonalNeighbours(move).Any(n => capturedBoard[n] == Content.Empty && EyeHelper.FindRealEyeWithinEmptySpace(capturedBoard, n, c.Opposite()) && !GroupHelper.GetKillerGroupFromCache(capturedBoard, n, c.Opposite()).Points.Any(p => capturedBoard[p] == c)))
                    return true;
            }
            return false;
        }


        /// Check corner point <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanGo_A26_2" />
        /// Check connect and die <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_TianLongTu_Q16738_5" />
        /// Specific filler move <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_GuanZiPu_A17_2" />
        /// One point target <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanGo_A26_2" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_Corner_A84_2" />
        /// <see cref="UnitTestProject.KoTest.KoTest_Scenario_WuQingYuan_Q31680" />
        /// Not suicidal for semi-solid eye <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_Corner_A95" />
        private static Boolean CornerPointSuicide(GameTryMove tryMove, Board captureBoard)
        {
            Point move = tryMove.Move;
            Board currentBoard = tryMove.CurrentGame.Board;
            Board tryBoard = tryMove.TryGame.Board;
            Content c = tryMove.MoveContent;
            if (!tryBoard.CornerPoint(move)) return false;

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
        /// Capture at tryBoard <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanGo_A23" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanQiJing_A36" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_GuanZiPu_A2Q71_101Weiqi" />
        /// Eternal life <see cref="UnitTestProject.CheckForRecursionTest.CheckForRecursionTest_Scenario_GuanZiPu_Q14971" />
        /// Capture at tryBoard more than recapture <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WuQingYuan_Q30935_2" />
        /// Captured more than move group <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanGo_A42" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WuQingYuan_Q31682" />
        /// Four-point group scenario <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_TianLongTu_Q16604" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WuQingYuan_Q31435" />
        /// Check for recursion <see cref="UnitTestProject.CheckForRecursionTest.CheckForRecursionTest_Scenario_XuanXuanGo_A27" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_GuanZiPu_Q14981" />
        /// Two-point atari move <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanGo_A82_101Weiqi" />
        /// Atari on next move <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WuQingYuan_Q30935" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanGo_A171_101Weiqi" />
        /// Check atari by previous move group <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_TianLongTu_Q16424_2" />
        /// Move group binding <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanQiJing_Weiqi101_B19_2" />
        /// Two-point atari covered eye <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_GuanZiPu_A32" />
        /// Suicide at covered eye <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WuQingYuan_Q31499_2" />
        /// Exclude if corner point <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_Corner_A9_Ext_2" />
        /// <see cref="UnitTestProject.KillerFormationTest.KillerFormationTest_Scenario_TianLongTu_Q16424" />
        /// No hope of escape <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_TianLongTu_Q17132_2" />
        /// </summary>
        public static Boolean MultiPointSuicidalMove(GameTryMove tryMove)
        {
            Point move = tryMove.Move;
            Board tryBoard = tryMove.TryGame.Board;
            Board currentBoard = tryMove.CurrentGame.Board;

            Board capturedBoard = ImmovableHelper.CaptureSuicideGroup(tryBoard);
            if (capturedBoard == null) return false;

            //capture at tryBoard
            if (tryBoard.CapturedList.Count > 0 && !WallHelper.IsHostileNeighbourGroup(capturedBoard))
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
            if (closestNeighbours.All(leapMove => !LinkHelper.ValidateLeapMove(tryBoard, move, leapMove)))
                return true;

            return false;
        }

        public static Boolean KillLeapMove(GameTryMove tryMove)
        {
            //test if opponent move at same point is suicidal
            GameTryMove move = tryMove.MakeMoveWithOpponentAtSamePoint();
            if (move != null)
                return SurvivalLeapMove(move);
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
        /// Neutral point suicidal move.
        /// <see cref="UnitTestProject.SurvivalTigerMouthMoveTest.RedundantTigerMouthMove_20221214_5" />
        /// </summary>
        public static Boolean NeutralPointSuicidalMove(GameTryMove tryMove)
        {
            Board tryBoard = tryMove.TryGame.Board;
            Point move = tryBoard.Move.Value;
            Content c = tryBoard.MoveGroup.Content;
            if (tryBoard.MoveGroupLiberties != 1 || tryBoard.MoveGroup.Points.Count != 1) return false;
            if (!tryBoard.PointWithinMiddleArea(move)) return false;

            List<Point> coveredPoints = tryBoard.GetDiagonalNeighbours(move).Where(q => tryBoard[q] == c).ToList();
            if (coveredPoints.Count > 2) return true;
            if (coveredPoints.Count != 2) return false;
            if (coveredPoints[0].x == coveredPoints[1].x || coveredPoints[0].y == coveredPoints[1].y) return false;
            return true;
        }

        /// <summary>
        /// Essential atari at covered eye.
        /// <see cref="UnitTestProject.NeutralPointMoveTest.NeutralPointMoveTest_Scenario4dan17" />
        /// <see cref="UnitTestProject.BaseLineSurvivalMoveTest.BaseLineSurvivalMoveTest_Scenario_Corner_A84" />
        /// Check for ko fight <see cref="UnitTestProject.NeutralPointMoveTest.NeutralPointMoveTest_Scenario_Corner_A36" />
        /// Check neighbour groups <see cref="UnitTestProject.NeutralPointMoveTest.NeutralPointMoveTest_Scenario_Phenomena_B7" />
        /// <see cref="UnitTestProject.NeutralPointMoveTest.NeutralPointMoveTest_Scenario_XuanXuanGo_A28_101Weiqi_3" />
        /// <see cref="UnitTestProject.BaseLineSurvivalMoveTest.BaseLineSurvivalMoveTest_Scenario_TianLongTu_Q16456" />
        /// </summary>
        private static Boolean EssentialAtariAtCoveredEye(GameTryMove tryMove)
        {
            Board tryBoard = tryMove.TryGame.Board;
            if (tryMove.AtariResolved || tryBoard.CapturedList.Count > 0 || tryBoard.MoveGroupLiberties == 1) return true;
            if (tryBoard.AtariTargets.Count != 1) return true;

            Group atariTarget = tryBoard.AtariTargets.First();
            if (atariTarget.Points.Count != 1) return true;

            //check neighbour groups
            Board b = ImmovableHelper.CaptureSuicideGroup(tryBoard, atariTarget, true);
            if (b != null && !WallHelper.StrongNeighbourGroups(b))
                return true;
            return false;
        }

        /// <summary>
        /// Must have neutral point.
        /// Neutral point at small tiger mouth <see cref="UnitTestProject.MustHaveNeutralMoveTest.MustHaveNeutralMoveTest_Scenario5dan27_3" />
        /// Neutral point at big tiger mouth <see cref="UnitTestProject.MustHaveNeutralMoveTest.MustHaveNeutralMoveTest_Scenario5dan27_Variation" />
        /// Negative example <see cref="UnitTestProject.MustHaveNeutralMoveTest.MustHaveNeutralMoveTest_Scenario_XuanXuanGo_A27" />
        /// <see cref="UnitTestProject.MustHaveNeutralMoveTest.MustHaveNeutralMoveTest_Scenario_XuanXuanGo_A23" />
        /// <see cref="UnitTestProject.MustHaveNeutralMoveTest.MustHaveNeutralMoveTest_Scenario_GuanZiPu_Weiqi101_19138" />
        /// Check if atari <see cref="UnitTestProject.MustHaveNeutralMoveTest.MustHaveNeutralMoveTest_Scenario_XuanXuanGo_A23" />
        /// <see cref="UnitTestProject.MustHaveNeutralMoveTest.MustHaveNeutralMoveTest_Scenario_TianLongTu_Q17136" />
        /// <see cref="UnitTestProject.MustHaveNeutralMoveTest.MustHaveNeutralMoveTest_Scenario_XuanXuanGo_Q18500_4" />
        /// <see cref="UnitTestProject.MustHaveNeutralMoveTest.MustHaveNeutralMoveTest_Scenario_Corner_A68" />
        /// Check if link for groups <see cref="UnitTestProject.MustHaveNeutralMoveTest.MustHaveNeutralMoveTest_Scenario_Corner_A84" />
        /// Check for tiger mouth <see cref="UnitTestProject.MustHaveNeutralMoveTest.MustHaveNeutralMoveTest_Scenario_Corner_A27" />
        /// <see cref="UnitTestProject.MustHaveNeutralMoveTest.MustHaveNeutralMoveTest_Scenario_Phenomena_Q25182" />
        /// <see cref="UnitTestProject.MustHaveNeutralMoveTest.MustHaveNeutralMoveTest_Scenario_TianLongTu_Q16827" />
        /// Connect and die <see cref="UnitTestProject.MustHaveNeutralMoveTest.MustHaveNeutralMoveTest_Scenario_XuanXuanGo_A28_101Weiqi" />
        /// <see cref="UnitTestProject.MustHaveNeutralMoveTest.MustHaveNeutralMoveTest_Scenario_Corner_A67" />
        /// Two must have neutral moves <see cref="UnitTestProject.MustHaveNeutralMoveTest.MustHaveNeutralMoveTest_Scenario_GuanZiPu_Weiqi101_19138" />
        /// Generic neutral move with must have neutral move <see cref="UnitTestProject.MustHaveNeutralMoveTest.MustHaveNeutralMoveTest_Scenario_Corner_A68_2" />
        /// </summary>
        private static Boolean MustHaveNeutralPoint(GameTryMove tryMove, GameTryMove opponentMove)
        {
            Board tryBoard = tryMove.TryGame.Board;
            Board currentBoard = tryMove.CurrentGame.Board;
            Board opponentBoard = opponentMove.TryGame.Board;
            Content c = tryBoard.MoveGroup.Content;

            //neutral point at small tiger mouth
            if (opponentBoard.GetStoneNeighbours().Where(n => EyeHelper.FindEye(opponentBoard, n)).Any(n => !StrongGroupsAtMustHaveMove(tryBoard, n)))
                return true;

            //neutral point at big tiger mouth
            (Boolean suicide, Board suicideBoard) = ImmovableHelper.SuicideAtBigTigerMouth(tryMove);
            if (suicide)
            {
                if (suicideBoard == null) return true;
                if (MustHaveMoveAtBigTigerMouth(suicideBoard, tryMove))
                    return true;
            }

            //ko fight
            if (MustHaveMoveAtKoFight(tryMove))
                return true;
            return false;
        }

        /// <summary>
        /// Must have move at ko fight.
        /// <see cref="UnitTestProject.MustHaveNeutralMoveTest.MustHaveNeutralMoveTest_20221229_7" />
        /// </summary>
        private static Boolean MustHaveMoveAtKoFight(GameTryMove tryMove)
        {
            Board tryBoard = tryMove.TryGame.Board;
            Point move = tryBoard.Move.Value;
            Content c = tryBoard.MoveGroup.Content;
            if (tryBoard.PointWithinMiddleArea(move)) return false;
            if (tryBoard.GetDiagonalNeighbours().Any(d => tryBoard[d] == c.Opposite() && tryBoard.IsSinglePoint(d) && tryBoard.GetStoneNeighbours(d).Any(n => tryBoard[n] == Content.Empty && ImmovableHelper.FindTigerMouth(tryBoard, c.Opposite(), n))))
                return true;
            return false;
        }

        /// <summary>
        /// Must have move at big tiger mouth.        
        /// Liberties more than one <see cref="UnitTestProject.MustHaveNeutralMoveTest.MustHaveNeutralMoveTest_Scenario_XuanXuanQiJing_Weiqi101_7245" />
        /// <see cref="UnitTestProject.MustHaveNeutralMoveTest.MustHaveNeutralMoveTest_Scenario_XuanXuanGo_A23" />
        /// Strong groups at tiger mouth <see cref="UnitTestProject.MustHaveNeutralMoveTest.MustHaveNeutralMoveTest_Scenario_Corner_A68" />
        /// <see cref="UnitTestProject.MustHaveNeutralMoveTest.MustHaveNeutralMoveTest_Scenario_TianLongTu_Q17136" />
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
            if (suicideBoard.MoveGroup.Liberties.Count > 1)
                return true;

            //strong groups at tiger mouth
            if (!StrongGroupsAtMustHaveMove(tryBoard, suicideMove))
                return true;

            //capture at liberty
            List<Group> eyeGroups = currentBoard.GetGroupsFromStoneNeighbours(move, c.Opposite()).Where(e => e.Points.Count >= 2 && e.Liberties.Count == 2).ToList();
            IEnumerable<Point> moves = eyeGroups.Select(e => e.Liberties.First(lib => !lib.Equals(move)));
            if (moves.Any(p => tryBoard.GetGroupsFromStoneNeighbours(p, c.Opposite()).Any(n => !n.Equals(tryBoard.MoveGroup) && n.Liberties.Count == 1 && n.Points.Count >= 2)))
                return true;
            return false;
        }

        /// <summary>
        /// Strong neighbour groups at tiger mouth for must-have move.
        /// <see cref="UnitTestProject.MustHaveNeutralMoveTest.MustHaveNeutralMoveTest_Scenario5dan27_3" />
        /// <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_20221024_4" />
        /// Check liberty fight <see cref="UnitTestProject.MustHaveNeutralMoveTest.MustHaveNeutralMoveTest_20221128_2" />
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
        /// <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_x" />
        /// <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_x_2" />
        /// </summary>
        private static Boolean CheckLibertyFightAtCoveredEye(Board currentBoard, Point eye, Content c)
        {
            Group group = currentBoard.GetGroupsFromStoneNeighbours(eye, c.Opposite()).FirstOrDefault();
            if (group == null) return false;
            List<Group> groups = LinkHelper.GetAllDiagonalConnectedGroups(currentBoard, group).ToList();
            if (!groups.Any(n => LinkHelper.FindDiagonalCut(currentBoard, n).Item1 != null)) return false;
            if (currentBoard.GetLibertiesOfGroups(groups).Select(lib => GroupHelper.GetKillerGroupFromCache(currentBoard, lib, c)).Any(kgroup => kgroup != null && EyeHelper.FindRealEyeWithinEmptySpace(currentBoard, kgroup)))
                return true;
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
            if (opponentMove != null && NeutralPointSuicidalMove(opponentMove))
                return false;

            return true;
        }

        #endregion

        #region restore neutral points
        /// <summary>
        /// Neutral points for kill moves have to be restored on end game in order to kill survival group.
        /// Two pre-atari moves <see cref="UnitTestProject.SpecificNeutralMoveTest.SpecificNeutralMoveTest_Scenario_Corner_A55" />
        /// No try moves left <see cref="UnitTestProject.MustHaveNeutralMoveTest.MustHaveNeutralMoveTest_Scenario_Side_A20" />
        /// Remaining move at liberty point <see cref="UnitTestProject.MustHaveNeutralMoveTest.MustHaveNeutralMoveTest_Scenario_XuanXuanQiJing_Weiqi101_7245" />
        /// Check connect and die for last two try moves <see cref="UnitTestProject.SuicidalRedundantMoveTest.NeutralPointMoveTest_Scenario_XuanXuanGo_B32" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_Side_B35" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanGo_A151_101Weiqi_5" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WuQingYuan_Q31580" />
        /// Suicide group near capture <see cref="UnitTestProject.FillKoEyeMoveTest.FillKoEyeMoveTest_Scenario_TianLongTu_Q16490" />
        /// <see cref="UnitTestProject.NeutralPointMoveTest.NeutralPointMoveTest_Scenario_WuQingYuan_Q6150" />
        /// <see cref="UnitTestProject.NeutralPointMoveTest.NeutralPointMoveTest_Scenario_Corner_B21" />
        /// </summary>
        public static void RestoreNeutralMove(Game currentGame, List<GameTryMove> tryMoves, List<GameTryMove> neutralPointMoves)
        {
            //remove moves that are within killer group
            neutralPointMoves.RemoveAll(n => n.TryGame.Board.MoveGroupLiberties == 1 || GroupHelper.GetKillerGroupFromCache(n.TryGame.Board, n.Move) != null);

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
                List<GameTryMove> preAtariMoves = neutralPointMoves.Where(move => ImmovableHelper.PreAtariMove(move)).ToList();
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
        /// </summary>
        private static Boolean ConnectAndDieEndMove(Board tryBoard)
        {
            Content c = tryBoard.MoveGroup.Content;
            (Boolean suicidal, Board captureBoard) = ImmovableHelper.ConnectAndDie(tryBoard);
            if (captureBoard == null) return false;
            //check for bloated suicide
            if (tryBoard.MoveGroupLiberties == 2)
            {
                Board b = ImmovableHelper.MakeMoveAtLiberty(captureBoard, tryBoard.MoveGroup, c);
                if (b == null) return true;
            }
            if (tryBoard.MoveGroup.Points.Count > 1) return true;
            return false;
        }

        /// <summary>
        /// Get specific neutral move to target survival groups with limited liberties.
        /// Two specific moves <see cref="UnitTestProject.SpecificNeutralMoveTest.SpecificNeutralMoveTest_Scenario_XuanXuanQiJing_Weiqi101_B51" />
        /// Check snapback <see cref="UnitTestProject.SpecificNeutralMoveTest.SpecificNeutralMoveTest_ScenarioHighLevel18" />
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
        /// </summary>
        public static IEnumerable<Group> IsImmovableKill(Game g, List<Group> killerGroups)
        {
            foreach (Group killerGroup in killerGroups)
            {
                if (!GroupHelper.IsLibertyGroup(killerGroup, g.Board)) continue;
                //more than one neighbour group
                if (g.Board.GetNeighbourGroups(killerGroup).Count == 1) continue;
                List<Point> killerLiberties = killerGroup.Points.Where(p => g.Board[p] == Content.Empty).ToList();
                //ensure two killer liberties without covered eye
                if (killerLiberties.Count(liberty => !EyeHelper.FindCoveredEye(g.Board, liberty, killerGroup.Content)) != 2)
                    continue;
                yield return killerGroup;
            }
        }

        /// <summary>
        /// Specific kill with immovable points.
        /// Survival group has liberty less or equals to two <see cref="UnitTestProject.SpecificNeutralMoveTest.SpecificNeutralMoveTest_Scenario5dan27" />
        /// <see cref="UnitTestProject.SpecificNeutralMoveTest.SpecificNeutralMoveTest_Scenario_TianLongTu_Q16735" />
        /// At least one liberty shared with killer group <see cref="UnitTestProject.SpecificNeutralMoveTest.SpecificNeutralMoveTest_Scenario_XuanXuanGo_A54_2" />
        /// Check that survival cannot clear space <see cref="UnitTestProject.SpecificNeutralMoveTest.SpecificNeutralMoveTest_Scenario_XuanXuanGo_A54" />
        /// </summary>
        public static GameTryMove SpecificKillWithImmovablePoints(Board board, List<GameTryMove> neutralPointMoves, Group killerGroup)
        {
            List<Point> contentPoints = killerGroup.Points.Where(t => board[t] == killerGroup.Content).ToList();
            if (board.GetGroupsFromPoints(contentPoints).Any(group => group.Liberties.Count == 1)) return null;
            List<Point> killerLiberties = killerGroup.Points.Where(p => board[p] == Content.Empty).ToList();

            HashSet<Group> groups = new HashSet<Group>();
            for (int i = 0; i <= neutralPointMoves.Count - 1; i++)
            {
                GameTryMove neutralPointMove = neutralPointMoves[i];
                Board tryBoard = neutralPointMove.TryGame.Board;
                Point move = neutralPointMove.Move;
                IEnumerable<Group> targetGroups = tryBoard.GetGroupsFromStoneNeighbours(move);
                //ensure target group has two liberties and share at least one liberty with killer group
                foreach (Group group in targetGroups)
                {
                    if (groups.Contains(group)) continue;
                    groups.Add(group);
                    if (group.Liberties.Count != 2) continue;
                    List<Point> sharedLiberties = group.Liberties.Intersect(killerLiberties).ToList();
                    if (sharedLiberties.Count >= 1 && sharedLiberties.Count <= 2)
                    {
                        //check that both players cannot clear space
                        if (sharedLiberties.Any(p => !ImmovableHelper.IsSuicidalMoveForBothPlayers(tryBoard, p)))
                            continue;
                        return neutralPointMove;
                    }
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
            Content c = neutralPointMoves.First().MoveContent;
            //any neutral point move next to target group and more than one point
            GameTryMove neutralPointMove = neutralPointMoves.FirstOrDefault(t => t.TryGame.Board.GetGroupsFromStoneNeighbours(t.Move).Count > 0 && t.TryGame.Board.MoveGroup.Points.Count > 1);
            if (neutralPointMove == null) return null;
            Board tryBoard = neutralPointMove.TryGame.Board;
            foreach (Group targetGroup in tryBoard.GetGroupsFromStoneNeighbours(tryBoard.Move.Value))
            {
                List<Point> neighbourLiberties = null;
                //find neighbour groups at diagonal cut
                (_, List<Point> pointsBetweenDiagonals) = LinkHelper.FindDiagonalCut(tryBoard, targetGroup);
                if (pointsBetweenDiagonals != null)
                {
                    HashSet<Group> neighbourGroups = tryBoard.GetGroupsFromPoints(pointsBetweenDiagonals);
                    //get the group other than neutral point group
                    Group neighbourGroup = neighbourGroups.FirstOrDefault(group => !group.Equals(tryBoard.MoveGroup) && !WallHelper.IsNonKillableGroup(tryBoard, group));
                    if (neighbourGroup == null) continue;
                    neighbourLiberties = neighbourGroup.Liberties.ToList();

                    //compare liberties to see if target group can be killed
                    if (neighbourLiberties.Count == targetGroup.Liberties.Count + 1)
                        return neutralPointMove;
                }
                else
                {
                    //target group contains killer group
                    List<Group> kgroups = killerGroups.Where(group => board.GetNeighbourGroups(group).Contains(board.GetCurrentGroup(targetGroup))).ToList();
                    if (kgroups.Count != 1) continue;
                    Group kgroup = kgroups.First();
                    if (!kgroup.Points.Any(p => tryBoard[p] == c && tryBoard.GetGroupAt(p).Liberties.Count > 1)) continue;
                    //include all empty points within killer group
                    neighbourLiberties = kgroup.Points.Where(p => tryBoard[p] == Content.Empty).ToList();

                    //compare liberties to see if target group can be killed
                    if (neighbourLiberties.Count == targetGroup.Liberties.Count)
                        return neutralPointMove;
                }

                //real solid eye found
                if (neighbourLiberties.Any(liberty => EyeHelper.FindRealEyeWithinEmptySpace(tryBoard, liberty, c)))
                    return neutralPointMove;
            }
            return null;
        }

        /// <summary>
        /// Get generic neutral moves that are not specific. Killer group required.
        /// One neighbour group <see cref="UnitTestProject.GenericNeutralMoveTest.GenericNeutralMoveTest_Scenario_XuanXuanGo_Q18500" />
        /// More than one neighbour group <see cref="UnitTestProject.GenericNeutralMoveTest.GenericNeutralMoveTest_Scenario5dan27_2" />
        /// Get all extended groups <see cref="UnitTestProject.GenericNeutralMoveTest.GenericNeutralMoveTest_Scenario_XuanXuanGo_Q18340" />
        /// Get all groups including eyes <see cref="UnitTestProject.GenericNeutralMoveTest.GenericNeutralMoveTest_Scenario4dan17_2" />
        /// </summary>
        public static GameTryMove GetGenericNeutralMove(Game g, List<GameTryMove> neutralPointMoves)
        {
            List<Group> killerGroups = GroupHelper.GetKillerGroups(g.Board);
            foreach (Group killerGroup in killerGroups)
            {
                if (!GroupHelper.IsLibertyGroup(killerGroup, g.Board)) continue;

                //cover all neutral points
                Board coveredBoard = new Board(g.Board);
                neutralPointMoves.ForEach(m => coveredBoard[m.Move] = killerGroup.Content);

                //order by inner liberties of neighbour group
                List<Group> orderedGroups = g.Board.GetNeighbourGroups(killerGroup).OrderBy(n => coveredBoard.GetGroupLiberties(n).Count).ToList();

                //get liberties by order
                HashSet<Point> libertyPoints = g.Board.GetLibertiesOfGroups(orderedGroups);
                //get neutral points of killer group neighbours
                neutralPointMoves = neutralPointMoves.Where(n => libertyPoints.Contains(n.Move)).ToList();
                foreach (Point p in libertyPoints)
                {
                    GameTryMove neutralMove = neutralPointMoves.FirstOrDefault(n => n.Move.Equals(p));
                    if (neutralMove == null) continue;

                    //check neighbour groups
                    Board b = neutralMove.TryGame.Board;
                    if (WallHelper.StrongNeighbourGroups(coveredBoard, b.Move.Value, b.MoveGroup.Content))
                        continue;
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
            if (!tryMove.IsNegligible)
                return false;

            if (RedundantTigerMouth(tryMove))
                return true;

            //find tiger mouth for opponent
            GameTryMove opponentMove = tryMove.MakeMoveWithOpponentAtSamePoint();
            if (opponentMove == null) return false;
            if (!opponentMove.IsNegligible)
                return false;

            if (RedundantTigerMouth(opponentMove, tryMove))
                return true;
            return false;
        }

        /// <summary>
        /// Check eye points at diagonals of tiger mouth. If all eye points are tiger mouth then is redundant. <see cref="UnitTestProject.SurvivalTigerMouthMoveTest.SurvivalTigerMouthMoveTest_Scenario_XuanXuanGo_B31" />
        /// <see cref="UnitTestProject.SurvivalTigerMouthMoveTest.RedundantTigerMouthMove_Scenario_WuQingYuan_Q15126" />
        /// Check possible corner three formation <see cref="UnitTestProject.SurvivalTigerMouthMoveTest.RedundantTigerMouthMove_Scenario_WuQingYuan_Q31503_2" />
        /// <see cref="UnitTestProject.SurvivalTigerMouthMoveTest.SurvivalTigerMouthMoveTest_Scenario_GuanZiPu_Q18860" />
        /// Opponent move at tiger mouth <see cref="UnitTestProject.SurvivalTigerMouthMoveTest.RedundantTigerMouthMove_Scenario_XuanXuanGo_A151_101Weiqi" />
        /// <see cref="UnitTestProject.SurvivalTigerMouthMoveTest.SurvivalTigerMouthMoveTest_Scenario_Nie67" />
        /// Uncovered eye at diagonal point <see cref="UnitTestProject.SurvivalTigerMouthMoveTest.RedundantTigerMouthMove_20221231_6" />
        /// Check for non killable group <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WindAndTime_Q30370" />
        /// Check for suicide at big tiger mouth <see cref="UnitTestProject.SurvivalTigerMouthMoveTest.RedundantTigerMouthMove_Scenario_Corner_A87" />
        /// <see cref="UnitTestProject.SurvivalTigerMouthMoveTest.SurvivalTigerMouthMoveTest_Scenario_TianLongTu_Q16470" />
        /// Check move and diagonal space <see cref="UnitTestProject.SurvivalTigerMouthMoveTest.RedundantTigerMouthMove_Scenario_Nie4" />
        /// <see cref="UnitTestProject.SurvivalTigerMouthMoveTest.RedundantTigerMouthMove_Scenario_WuQingYuan_Q15126" />
        /// <see cref="UnitTestProject.SurvivalTigerMouthMoveTest.SuicidalRedundantMoveTest_Scenario_WuQingYuan_Q31428" />
        /// Check connect end move <see cref="UnitTestProject.SurvivalTigerMouthMoveTest.RedundantTigerMouthMove_Scenario_TianLongTu_Q16738_2" />
        /// </summary>
        private static Boolean RedundantTigerMouth(GameTryMove tryMove, GameTryMove opponentMove = null)
        {
            Point move = tryMove.Move;
            Board tryBoard = tryMove.TryGame.Board;
            Board currentBoard = tryMove.CurrentGame.Board;
            Content c = tryMove.MoveContent;
            //ensure is tiger mouth
            if (tryBoard.MoveGroup.Points.Count != 1) return false;
            if (ImmovableHelper.IsConfirmTigerMouth(currentBoard, tryBoard) == null) return false;

            //check eye points at diagonals of tiger mouth
            List<Point> diagonalPoints = ImmovableHelper.GetDiagonalsOfTigerMouth(tryBoard, move, c.Opposite()).Where(e => tryBoard[e] != c.Opposite()).ToList();
            if (diagonalPoints.Count == 0) return false;

            Board capturedBoard = ImmovableHelper.CaptureSuicideGroup(tryBoard);
            if (capturedBoard == null || capturedBoard.MoveGroupLiberties == 1) return false;
            if (KoHelper.IsForwardOrReverseKoFight(capturedBoard)) return false;

            //check possible corner three formation
            if (KillerFormationHelper.PossibleCornerThreeFormation(currentBoard, move, c.Opposite())) return false;

            foreach (Point d in diagonalPoints)
            {
                if (NeutralPointSuicidalMove(tryMove))
                    continue;
                //uncovered eye at diagonal point
                if (EyeHelper.FindUncoveredEye(currentBoard, d, c.Opposite()) && EyeHelper.FindCoveredEye(tryBoard, d, c.Opposite()))
                    continue;

                //opponent move at tiger mouth
                if (opponentMove != null)
                {
                    if (ImmovableHelper.SuicideAtBigTigerMouth(opponentMove).Item1 || BothAliveHelper.CheckForBothAliveAtMove(opponentMove.TryGame.Board))
                        continue;
                    if (ImmovableHelper.IsImmovablePoint(d, c.Opposite(), currentBoard).Item1)
                        return true;
                }
                //check killer groups
                Group diagonalKillerGroup = GroupHelper.GetKillerGroupOfNeighbourGroups(currentBoard, d, c.Opposite());
                if (diagonalKillerGroup == null || !WallHelper.StrongNeighbourGroups(currentBoard, currentBoard.GetNeighbourGroups(diagonalKillerGroup))) continue;

                Group moveKillerGroup = GroupHelper.GetKillerGroupOfNeighbourGroups(currentBoard, move, c.Opposite());
                if (moveKillerGroup != null) continue;

                //find immovable point at diagonal
                if (ImmovableHelper.IsImmovablePoint(d, c.Opposite(), currentBoard).Item1)
                    return true;
            }

            //no diagonal tiger mouth
            if (TigerMouthWithoutDiagonalMouth(tryMove, capturedBoard))
                return true;

            return false;
        }


        /// <summary>
        /// Redundant tiger mouth without diagonal mouth.
        /// <see cref="UnitTestProject.SurvivalTigerMouthMoveTest.RedundantTigerMouthMove_Scenario_XuanXuanGo_A26" />
        /// Check for covered eye <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_AncientJapanese_B6" />
        /// <see cref="UnitTestProject.SurvivalTigerMouthMoveTest.RedundantTigerMouthMove_Scenario_TianLongTu_Q16738" />
        /// <see cref="UnitTestProject.SurvivalTigerMouthMoveTest.RedundantTigerMouthMove_Scenario_WindAndTime_Q30225" />
        /// Check for three groups <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_GuanZiPu_Q1970" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WuQingYuan_Q30935" />
        /// Check for strong neighbour groups <see cref="UnitTestProject.SurvivalTigerMouthMoveTest.RedundantTigerMouthMove_Scenario3dan22" />
        /// <see cref="UnitTestProject.SurvivalTigerMouthMoveTest.RedundantTigerMouthMove_Scenario_TianLongTu_Q16605" />
        /// <see cref="UnitTestProject.SurvivalTigerMouthMoveTest.RedundantTigerMouthMove_Scenario_XuanXuanGo_A28" />
        /// Check for no liberty at move <see cref="UnitTestProject.SurvivalTigerMouthMoveTest.RedundantTigerMouthMove_20221220_7" />
        /// </summary>
        private static Boolean TigerMouthWithoutDiagonalMouth(GameTryMove tryMove, Board capturedBoard)
        {
            Point move = tryMove.Move;
            Board currentBoard = tryMove.CurrentGame.Board;
            Board tryBoard = tryMove.TryGame.Board;
            Content c = tryMove.MoveContent;

            //suicide within real eye at suicidal redundant move
            if (EyeHelper.FindSemiSolidEye(move, capturedBoard).Item1)
                return false;
            //check for covered eye
            if (EyeHelper.IsCovered(tryBoard, move, c.Opposite()))
                return false;

            //check for three groups
            List<Group> neighbourGroups = tryBoard.GetGroupsFromStoneNeighbours(move);
            if (neighbourGroups.Count >= 3)
                return false;

            //check for strong neighbour groups
            Boolean strongGroups = WallHelper.HostileNeighbourGroups(currentBoard, move, c) && capturedBoard.MoveGroupLiberties > 2;
            if (!strongGroups)
                return false;

            //check for no liberty at move
            if (!capturedBoard.GetStoneNeighbours().Any(n => capturedBoard[n] == Content.Empty && !n.Equals(move)))
                return false;
            return true;
        }

        #endregion

        #region redundant eye diagonal
        /// <summary>
        /// Survival eye diagonal move.
        /// <see cref="UnitTestProject.RedundantEyeDiagonalMoveTest.RedundantEyeDiagonalMoveTest_Scenario_XuanXuanQiJing_Weiqi101_18473" />
        /// Check diagonals are real eyes <see cref="UnitTestProject.ImmovableTest.ImmovableTest_Scenario_XuanXuanGo_B31" />
        /// Ensure diagonal not required for both alive. 
        /// <see cref="UnitTestProject.RedundantEyeDiagonalMoveTest.RedundantEyeDiagonalMoveTest_Scenario_SiHuoDaQuan_CornerA29_2" />
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
            diagonals.RemoveAll(d => GroupHelper.GetKillerGroupFromCache(currentBoard, d, c) == null);
            if (diagonals.Count == 0) return false;

            //check diagonals are real eyes
            if (!diagonals.All(eye => EyeHelper.RealEyeAtDiagonal(tryMove, eye))) return false;

            //check other surrounding points are not possible eyes
            IEnumerable<Point> neighbourPts = tryBoard.GetStoneAndDiagonalNeighbours().Except(diagonals);
            if (neighbourPts.Any(q => !WallHelper.NoEyeForSurvival(tryBoard, q)))
                return false;

            //check link to groups other than eye groups
            if (LinkHelper.PossibleLinkForGroups(tryBoard, currentBoard))
                return false;

            GameTryMove opponentMove = tryMove.MakeMoveWithOpponentAtSamePoint();
            if (opponentMove != null && NeutralPointSuicidalMove(opponentMove))
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
        /// Survival eye filler moves. Get specific move for group not more than five points and generic move for more than five points. 
        /// </summary>
        public static Boolean SurvivalEyeFillerMove(GameTryMove tryMove)
        {
            Board currentBoard = tryMove.CurrentGame.Board;
            Board tryBoard = tryMove.TryGame.Board;
            Point move = tryMove.Move;
            Content c = tryMove.MoveContent;
            if (!tryMove.IsNegligible || tryBoard.IsAtariMove)
                return false;
            Group killerGroup = GroupHelper.GetKillerGroupFromCache(currentBoard, move, c);
            if (killerGroup == null) return FillerMoveWithoutKillerGroup(tryMove);
            if (killerGroup.Points.Count == 1) return false;

            if (killerGroup.Points.Count <= 5)
                return SpecificEyeFillerMove(tryMove);
            else
                return GenericEyeFillerMove(tryMove);
        }

        /// <summary>
        /// Kill eye filler moves. Get specific move for group not more than five points.
        /// </summary>
        public static Boolean KillEyeFillerMove(GameTryMove tryMove)
        {
            Board currentBoard = tryMove.CurrentGame.Board;
            Board tryBoard = tryMove.TryGame.Board;
            Point move = tryMove.Move;
            Content c = tryMove.MoveContent;
            if (!tryMove.IsNegligible || tryBoard.IsAtariMove)
                return false;
            Group killerGroup = GroupHelper.GetKillerGroupFromCache(currentBoard, move, c.Opposite());
            if (killerGroup != null && killerGroup.Points.Count <= 2) return false;
            //make survival move
            GameTryMove opponentMove = tryMove.MakeMoveWithOpponentAtSamePoint();
            if (opponentMove == null) return false;
            if (killerGroup != null && killerGroup.Points.Count <= 5)
                return SpecificEyeFillerMove(opponentMove);
            else
                return FillerMoveWithoutKillerGroup(tryMove, opponentMove);
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
        private static Boolean FillerMoveWithoutKillerGroup(GameTryMove tryMove, GameTryMove opponentMove = null)
        {
            Board tryBoard = tryMove.TryGame.Board;
            Point move = tryBoard.Move.Value;

            //check generic eye filler move
            if (!GenericEyeFillerMove(tryMove)) return false;

            if (WallHelper.IsNonKillableGroup(tryBoard))
                return true;

            if (opponentMove == null)
            {
                //check for one point leap move
                if (SiegeScenario(tryBoard, tryBoard.GetClosestPoints(move)))
                    return false;
            }
            else
            {
                //check killer leap move
                Board opponentBoard = opponentMove.TryGame.Board;
                if (SiegeScenario(opponentBoard, opponentBoard.GetClosestPoints(move)))
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Siege scenario. At least one closest group targeted by neighbour group.
        /// <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_WindAndTime_Q30278" />
        /// </summary>
        private static Boolean SiegeScenario(Board tryBoard, List<Point> points, int groupCount = 1)
        {
            HashSet<Group> groups = tryBoard.GetGroupsFromPoints(points);
            if (groups.Count < groupCount) return false;
            return groups.Count(gr => gr.Neighbours.Except(tryBoard.MoveGroup.Points).Count(n => tryBoard[n] == gr.Content.Opposite()) >= 2) >= groupCount;
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
            if (EyeFillerLinkForGroups(tryMove))
                return false;

            //check for increased killer groups
            if (GroupHelper.IncreasedKillerGroups(tryBoard, currentBoard))
                return false;

            //check link for groups
            if (LinkHelper.PossibleLinkForGroups(tryBoard, currentBoard))
                return false;

            //count eyes created at move
            int possibleEyes = PossibleEyesCreated(currentBoard, move, c);

            List<Point> emptyNeighbours = tryBoard.GetStoneNeighbours().Where(p => tryBoard[p] == Content.Empty).ToList();
            foreach (Point p in emptyNeighbours)
            {
                //check any opponent stone at neighbour points
                if (currentBoard.GetStoneNeighbours(p).Any(n => currentBoard[n] == c.Opposite()))
                    continue;
                //count eyes created at empty neighbour points
                int possibleEyesAtNeighbourPt = PossibleEyesCreated(currentBoard, p, c);
                //possibility of eyes created more than at try move point
                if (possibleEyesAtNeighbourPt > possibleEyes)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Check redundant corner point.
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
            if (!tryBoard.CornerPoint(move) || tryBoard.IsAtariMove || !tryMove.IsNegligible) return false;

            if (tryBoard.MoveGroup.Points.Count != 1) return false;
            //check for kill formation
            Boolean killFormation = (tryBoard.GetClosestPoints(move, c.Opposite()).Count >= 3 && !tryBoard.GetClosestPoints(move, c).Any());
            if (killFormation) return false;

            //multipoint snapback
            if (captureBoard.GetNeighbourGroups(tryBoard.MoveGroup).Any(gr => gr.Points.Count > 1 && ImmovableHelper.CheckConnectAndDie(captureBoard, gr)))
                return false;
            return true;
        }

        /// <summary>
        /// Eye filler link for groups.
        /// </summary>
        public static Boolean EyeFillerLinkForGroups(GameTryMove tryMove)
        {
            Board tryBoard = tryMove.TryGame.Board;
            Board currentBoard = tryMove.CurrentGame.Board;
            Content c = tryMove.MoveContent;

            //check for opponent stones at stone and diagonal points
            if (tryBoard.GetStoneAndDiagonalNeighbours().Any(n => tryBoard[n] == c.Opposite()))
            {
                //ensure link for groups
                if (!LinkHelper.IsAbsoluteLinkForGroups(currentBoard, tryBoard)) return false;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Return specific survival or killer move if killer group contains five points or less. 
        /// Neighbour groups liberty more than one <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_GuanZiPu_A37" />
        /// Check immovable at liberties <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_WuQingYuan_Q31602" />
        /// Not link for groups <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_WuQingYuan_Q31537" />
        /// Prevent survival creating eye <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_GuanZiPu_A17_4" />
        /// Group binding <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_GuanZiPu_A16" />
        /// <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_GuanZiPu_A36" />
        /// No neighbour group <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_Corner_A80" />
        /// <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_Corner_A61_2" />
        /// <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_XuanXuanGo_A4" />
        /// Check for atari on neighbour groups <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_GuanZiPu_A36_2" />
        /// </summary>
        public static Boolean SpecificEyeFillerMove(GameTryMove tryMove)
        {
            Board tryBoard = tryMove.TryGame.Board;
            Board currentBoard = tryMove.CurrentGame.Board;
            Point move = tryMove.Move;
            Content c = tryMove.MoveContent;
            if (!tryMove.IsNegligible) return false;

            //neighbour groups should have liberty more than one
            Group killerGroup = GroupHelper.GetKillerGroupFromCache(currentBoard, move, c);
            if (killerGroup == null || AtariHelper.AtariByGroup(currentBoard, killerGroup)) return false;

            List<Point> emptyPoints = killerGroup.Points.Where(p => currentBoard[p] == Content.Empty).ToList();

            //no neighbour group
            if (emptyPoints.Any(p => currentBoard.GetGroupsFromStoneNeighbours(p, c.Opposite()).Count == 0))
                return false;

            //ensure not link for groups
            if (EyeFillerLinkForGroups(tryMove))
                return false;

            //prevent survival creating eye
            if (GroupHelper.IncreasedKillerGroups(tryBoard, currentBoard))
                return false;

            //select move with max binding
            IEnumerable<Board> moveBoards = GameHelper.GetMoveBoards(currentBoard, emptyPoints, c.Opposite());
            if (!moveBoards.Any()) return false;
            Point bestMove = KillerFormationHelper.GetMaxBindingPoint(currentBoard, moveBoards, killerGroup);
            return !tryMove.Move.Equals(bestMove);
        }

        /// <summary>
        /// Return number of possible eyes that can be created at stone neighbour points.
        /// </summary>
        public static int PossibleEyesCreated(Board currentBoard, Point p, Content c)
        {
            List<Point> stoneNeighbours = currentBoard.GetStoneNeighbours(p);
            List<Point> possibleEyes = stoneNeighbours.Where(n => currentBoard[n] != c).ToList();
            return possibleEyes.Count;
        }

        #endregion

        #region redundant ko
        /// <summary>
        /// Redundant survival pre ko moves <see cref="UnitTestProject.RedundantKoMoveTest.RedundantKoMoveTest_Scenario_XuanXuanGo_A46_101Weiqi_2" />
        /// Double ko recursion <see cref="UnitTestProject.CheckForRecursionTest.CheckForRecursionTest_Scenario_Corner_B41" />
        /// </summary>
        public static Boolean RedundantSurvivalPreKoMove(GameTryMove tryMove)
        {
            if (tryMove.IsKoFight)
                return RedundantSurvivalKoMove(tryMove);
            return false;
        }

        /// <summary>
        /// Redundant killer pre ko moves <see cref="UnitTestProject.RedundantKoMoveTest.RedundantKillerKoMoveTest_Scenario_XuanXuanGo_A46_101Weiqi_2" />
        /// </summary>
        public static Boolean RedundantKillerPreKoMove(GameTryMove tryMove)
        {
            return RedundantSurvivalPreKoMove(tryMove);
        }

        /// <summary>
        /// Redundant killer ko moves <see cref="UnitTestProject.RedundantKoMoveTest.RedundantKillerKoMoveTest_Scenario_XuanXuanGo_A46_101Weiqi" />
        /// </summary>
        public static Boolean RedundantKillerKoMove(GameTryMove tryMove)
        {
            return RedundantSurvivalKoMove(tryMove);
        }

        /// <summary>
        /// Redundant survival ko moves <see cref="UnitTestProject.RedundantKoMoveTest.RedundantKoMoveTest_Scenario_XuanXuanGo_A46_101Weiqi" />
        /// <see cref="UnitTestProject.RedundantKoMoveTest.RedundantKoMoveTest_SimpleSeki" />
        /// Check for opponent <see cref="UnitTestProject.RedundantKoMoveTest.RedundantKoMoveTest_Scenario_WindAndTime_Q30152" />
        /// <see cref="UnitTestProject.RedundantKoMoveTest.RedundantKoMoveTest_Scenario_XuanXuanGo_B10" />
        /// </summary>
        public static Boolean RedundantSurvivalKoMove(GameTryMove tryMove)
        {
            Board tryBoard = tryMove.TryGame.Board;
            Board currentBoard = tryMove.CurrentGame.Board;
            Content c = tryBoard.MoveGroup.Content;
            Boolean koEnabled = KoHelper.KoContentEnabled(c, tryBoard.GameInfo);
            if (!koEnabled)
            {
                //check pre-ko moves
                if (tryBoard.singlePointCapture == null) return false;
                //check double ko
                if (!KoHelper.PossibilityOfDoubleKo(tryMove))
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
            Board opponentBoard = new Board(tryBoard);
            opponentBoard.InternalMakeMove(eyePoint.Value, c.Opposite(), true);

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
        /// Check two liberty group <see cref="UnitTestProject.RedundantKoMoveTest.RedundantKoMoveTest_Scenario_XuanXuanQiJing_A38_2" /> 
        /// Target with all non killable groups <see cref="UnitTestProject.RedundantKoMoveTest.RedundantKoMoveTest_Scenario2kyu18" /> 
        /// <see cref="UnitTestProject.RedundantKoMoveTest.RedundantKoMoveTest_Scenario_XuanXuanQiJing_Weiqi101_B74" />
        /// Real eye at diagonal <see cref="UnitTestProject.RedundantKoMoveTest.RedundantKoMoveTest_Scenario_WuQingYuan_Q30982" /> 
        /// <see cref="UnitTestProject.RedundantKoMoveTest.RedundantKoMoveTest_Scenario_XuanXuanGo_A151_101Weiqi" /> 
        /// <see cref="UnitTestProject.RedundantKoMoveTest.RedundantKoMoveTest_Scenario_XuanXuanGo_A151_101Weiqi_2" /> 
        /// Suicide group ko fight <see cref="UnitTestProject.RedundantKoMoveTest.RedundantKoMoveTest_Scenario_XuanXuanQiJing_A38_2" /> 
        /// <see cref="UnitTestProject.RedundantKoMoveTest.RedundantKoMoveTest_Scenario_TianLongTu_Q16693_2" /> 
        /// <see cref="UnitTestProject.RedundantKoMoveTest.RedundantKoMoveTest_x_2" /> 
        /// Check break link <see cref="UnitTestProject.RedundantKoMoveTest.RedundantKoMoveTest_Scenario_WindAndTime_Q30152_2" /> 
        /// <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_Scenario_WindAndTime_Q30152" /> 
        /// </summary>
        public static Boolean CheckRedundantKo(Board tryBoard, Board currentBoard)
        {
            Point move = tryBoard.Move.Value;
            Content c = tryBoard.MoveGroup.Content;
            Point? eyePoint = KoHelper.GetKoEyePoint(tryBoard);
            if (eyePoint == null) return false;

            //ko fight at non killable group
            if (KoHelper.IsNonKillableGroupKoFight(tryBoard, tryBoard.MoveGroup))
            {
                HashSet<Group> neighbourGroups = tryBoard.GetGroupsFromStoneNeighbours(move, c);
                if (LifeCheck.GetTargets(tryBoard).All(t => neighbourGroups.Contains(t)) && WallHelper.AllTargetWithinNonKillableGroups(tryBoard))
                    return true;
                if (!WallHelper.StrongNeighbourGroups(tryBoard)) return false;
                //check liberty fight
                if (CheckLibertyFightAtMustHaveMove(tryBoard)) return false;
                //check two liberty group
                if (neighbourGroups.Any(n => CheckTwoLibertyGroupToCaptureNeighbour(tryBoard, currentBoard, n, eyePoint.Value)))
                    return false;
                return true;
            }

            //target with all non killable groups
            if (!WallHelper.TargetWithAllNonKillableGroups(tryBoard))
                return false;

            //if all diagonals are real eyes then redundant
            List<Point> diagonals = ImmovableHelper.GetDiagonalsOfTigerMouth(currentBoard, eyePoint.Value, c).Where(q => tryBoard[q] != c).ToList();
            foreach (Point d in diagonals)
            {
                Board opponentBoard = currentBoard;
                if (opponentBoard[eyePoint.Value] == Content.Empty)
                {
                    opponentBoard = new Board(currentBoard);
                    opponentBoard.MakeMoveOnNewBoard(eyePoint.Value, c.Opposite(), true);
                }
                if (!EyeHelper.FindRealEyeWithinEmptySpace(opponentBoard, d, c))
                    return false;
            }

            //check break link
            if (diagonals.Count == 0 && KoHelper.CheckBaseLineLeapLink(tryBoard, eyePoint.Value, c))
                return false;
            return true;
        }

        #endregion
    }
}
