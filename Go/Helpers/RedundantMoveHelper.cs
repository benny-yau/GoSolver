using System;
using System.Collections.Generic;
using System.Linq;
using dh = Go.DirectionHelper;

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
                if (tryBoard.MoveGroupLiberties != 1) return true;
                //check for killer formations
                if (KillerFormationHelper.SuicidalKillerFormations(tryBoard, currentBoard))
                    return false;
            }
            else
            {
                //covered eye with more than two liberties
                if (currentBoard.GetGroupsFromStoneNeighbours(move, c.Opposite()).Any(group => group.Liberties.Count <= 2))
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
            if (opponentMove == null) return false;

            if (FindCoveredEyeMove(opponentMove, tryMove))
                return true;

            return false;
        }

        /// <summary>
        /// Check groups with two liberties <see cref="UnitTestProject.CheckForRecursionTest.CheckForRecursionTest_Scenario_Corner_B41" /> 
        /// <see cref="UnitTestProject.NeutralPointMoveTest.NeutralPointMoveTest_Scenario_XuanXuanQiJing_A38" /> 
        /// <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_Scenario_XuanXuanQiJing_A64" />
        /// <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_Scenario_XuanXuanGo_Q18341_2" />
        /// Check eye for suicidal move <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_Scenario_WindAndTime_Q30275" />
        /// <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_Scenario_Corner_A84_3" />
        /// Check escape capture link <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_Scenario_XuanXuanGo_A26_3" />
        /// Ensure neighbour groups are escapable <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_Scenario_WuQingYuan_Q31398" /> 
        /// Check no eye for survival <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_Scenario_XuanXuanQiJing_A52" />
        /// <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_Scenario_TianLongTu_Q16594" />
        /// <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_Scenario_XuanXuanGo_A41" /> 
        /// <see cref="UnitTestProject.NeutralPointMoveTest.NeutralPointMoveTest_Scenario_XuanXuanGo_A28_101Weiqi_3" />
        /// <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_Scenario_XuanXuanGo_A26_4" /> 
        /// Check no eye for survival for opponent <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_Scenario_WindAndTime_Q30332" /> 
        /// Check eye for survival <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_Scenario_WuQingYuan_Q30982" /> 
        /// <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_Scenario_Corner_B25" /> 
        /// <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_Scenario_WindAndTime_Q29277" /> 
        /// <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_Scenario4dan10" /> 
        /// <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_Scenario_XuanXuanGo_A26" /> 
        /// Check kill opponent <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_Scenario_XuanXuanGo_A34" />
        /// <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_Scenario_WindAndTime_Q30198" />
        /// <see cref="UnitTestProject.RedundantKoMoveTest.RedundantKoMoveTest_Scenario_WindAndTime_Q29998" />
        /// <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_Scenario_XuanXuanGo_A28_101Weiqi" />
        /// Check liberty count without covered eye <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_Scenario_XuanXuanQiJing_A64" />
        /// Check must-have move <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_20221019_7" />
        /// <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_20221024_4" />
        /// Check one-point snapback <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_Scenario_WuQingYuan_Q31453" />
        /// <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_Scenario_XuanXuanGo_A37_101Weiqi" />
        /// Check for double ko <see cref="UnitTestProject.NeutralPointMoveTest.NeutralPointMoveTest_Scenario_XuanXuanGo_A28_101Weiqi" />
        /// Check atari for ko move <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_Scenario_XuanXuanGo_A26_2" />
        /// Check break link <see cref="UnitTestProject.RedundantKoMoveTest.RedundantKoMoveTest_Scenario_WindAndTime_Q30152_3" />
        /// Check possible links <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_Scenario_XuanXuanQiJing_Weiqi101_18497_2" />
        /// <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_Scenario_XuanXuanQiJing_Weiqi101_B74" />
        /// <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_Scenario_XuanXuanQiJing_Weiqi101_A40" />
        /// Set neutral point for opponent <see cref="UnitTestProject.NeutralPointMoveTest.NeutralPointMoveTest_Scenario_Corner_B21" />
        /// </summary>
        public static Boolean FindCoveredEyeMove(GameTryMove tryMove, GameTryMove opponentTryMove = null)
        {
            Board tryBoard = tryMove.TryGame.Board;
            Board currentBoard = tryMove.CurrentGame.Board;
            Point move = tryBoard.Move.Value;
            Content c = tryMove.MoveContent;
            if (GameHelper.GetContentForSurviveOrKill(tryBoard.GameInfo, SurviveOrKill.Kill) == c) return false;
            if (tryMove.AtariResolved) return false;
            Group eyeGroup = null;
            Point eyePoint = new Point();
            List<Point> eyePoints = tryBoard.GetStoneNeighbours().Where(n => EyeHelper.FindCoveredEye(tryBoard, n, c)).ToList();
            if (eyePoints.Count == 1)
            {
                //one-point covered eye
                eyePoint = eyePoints.First();
                if (!EyeHelper.CoveredMove(tryBoard, eyePoint, c)) return false;
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

            //check atari for ko move
            if (KoHelper.EssentialAtariForKoMove(tryMove))
                return false;

            //check groups with two liberties 
            foreach (Group group in currentBoard.GetNeighbourGroups(eyeGroup).Where(gr => gr.Liberties.Count == 2))
            {
                foreach (Point liberty in group.Liberties)
                {
                    (Boolean suicidal, Board b) = ImmovableHelper.IsSuicidalMove(liberty, c, currentBoard);
                    if (!suicidal) continue;
                    Point liberty2 = group.Liberties.First(p => !p.Equals(liberty));
                    //check eye for suicidal move
                    if (b != null && EyeHelper.FindEye(b, liberty2, c))
                        return false;
                    //check connect and die for opponent groups
                    if (WallHelper.StrongNeighbourGroups(tryBoard, liberty, c))
                        continue;
                    //check escape capture link
                    if (ImmovableHelper.EscapeCaptureLink(currentBoard, group, eyePoint))
                        continue;
                    return false;
                }
            }

            //check for double ko
            if (KoHelper.NeutralPointDoubleKo(tryBoard))
                return false;

            //check kill opponent
            if (tryBoard.GetStoneAndDiagonalNeighbours().Any(n => tryBoard[n] == Content.Empty && !eyeGroup.Points.Contains(n) && tryBoard.GetStoneNeighbours(n).Any(s => tryBoard[s] == c.Opposite()) && !WallHelper.NoEyeForSurvival(currentBoard, n, c.Opposite())))
                return false;

            //check eye for survival
            if (eyeGroup.Points.Any(e => tryBoard.GetDiagonalNeighbours(e).Any(n => !WallHelper.NoEyeForSurvival(tryBoard, n, c) && !EyeHelper.RealEyeAtDiagonal(tryMove, n))))
                return false;

            //check no eye for survival
            if (!WallHelper.NoEyeForSurvivalAtNeighbourPoints(tryBoard))
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
            //check one-point snapback
            foreach (Group group in tryBoard.GetGroupsFromStoneNeighbours(eyePoint, c.Opposite()))
            {
                if (group.Liberties.Count <= 2 && group.Points.Count >= 2)
                {
                    (_, Board b) = ImmovableHelper.ConnectAndDie(tryBoard, group);
                    if (b == null) continue;
                    Boolean captured = b.IsCapturedGroup(group);
                    if (captured && b.MoveGroupLiberties == 1) return false;
                    if (!captured)
                    {
                        Board b2 = ImmovableHelper.CaptureSuicideGroup(b, b.GetCurrentGroup(group));
                        if (b2 != null && b2.MoveGroupLiberties == 1) return false;
                    }
                }
            }

            //check break link
            List<Group> neighbourGroups = tryBoard.GetNeighbourGroups();
            if (neighbourGroups.Count >= 2 && neighbourGroups.Any(n => !WallHelper.IsNonKillableGroup(tryBoard, n)))
                return false;

            //check possible links
            if (LinkHelper.PossibleLinkForGroups(tryBoard, currentBoard))
                return false;

            //set neutral point for opponent
            if (opponentTryMove != null && WallHelper.IsNonKillableGroup(opponentTryMove.TryGame.Board))
                opponentTryMove.IsNeutralPoint = true;
            return true;
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
        /// Check opponent double ko <see cref="UnitTestProject.FillKoEyeMoveTest.FillKoEyeMoveTest_Scenario_WindAndTime_Q30275_2" /> 
        /// <see cref="UnitTestProject.LifeCheckTest.LifeCheckTest_Scenario_XuanXuanQiJing_Weiqi101_18497_2" /> 
        /// Set as neutral point <see cref="UnitTestProject.FillKoEyeMoveTest.FillKoEyeMoveTest_Scenario_TianLongTu_Q16490" />
        /// Two covered eyes <see cref="UnitTestProject.FillKoEyeMoveTest.FillKoEyeMoveTest_Scenario5dan18" />
        /// Three liberty eye group <see cref="UnitTestProject.FillKoEyeMoveTest.FillKoEyeMoveTest_Scenario5dan18_2" />
        /// </summary>
        public static Boolean FillKoEyeMove(GameTryMove tryMove)
        {
            Point move = tryMove.Move;
            Board currentBoard = tryMove.CurrentGame.Board;
            Board tryBoard = tryMove.TryGame.Board;
            Content c = tryMove.MoveContent;
            //ensure is fill eye
            if (!EyeHelper.FindEye(currentBoard, move, c)) return false;

            (Boolean connectAndDie, Board captureBoard) = ImmovableHelper.ConnectAndDie(tryBoard, tryBoard.MoveGroup, false);
            if (connectAndDie)
            {
                //check for killer formation
                if (KillerFormationHelper.SuicidalKillerFormations(tryBoard, currentBoard))
                    return false;

                //check weak group in connect and die
                if (!CheckWeakGroupInConnectAndDie(tryBoard, tryBoard.MoveGroup))
                    return true;
            }

            List<Group> eyeGroups = currentBoard.GetGroupsFromStoneNeighbours(move, c.Opposite()).ToList();

            //not ko enabled
            if (!KoHelper.KoContentEnabled(c, tryBoard.GameInfo) && eyeGroups.Any(e => e.Points.Count == 1 && e.Liberties.Count == 1))
                return false;

            //ensure group more than one point have more than one liberty
            if (eyeGroups.Any(e => e.Points.Count > 1 && e.Liberties.Count == 1)) return false;

            if (eyeGroups.Count > 2)
            {
                //double atari
                if (eyeGroups.Count(group => group.Liberties.Count == 1) >= 2)
                    return false;

                //check both alive
                if (BothAliveHelper.CheckForBothAliveAtMove(tryBoard))
                    return false;

                //check break link
                if (KoHelper.CheckBaseLineLeapLink(currentBoard, move, c))
                    return false;
            }
            //check suicide at tiger mouth
            if (SuicideAtBigTigerMouth(tryMove).Item1)
                return false;

            //check opponent double ko
            Board opponentBoard = currentBoard.MakeMoveOnNewBoard(move, c.Opposite(), true);
            if (opponentBoard != null && KoHelper.IsKoFight(opponentBoard) && KoHelper.PossibilityOfDoubleKo(opponentBoard, currentBoard))
                return false;

            //two covered eyes
            if (eyeGroups.Any(e => e.Liberties.Count == 2 && e.Liberties.All(lib => EyeHelper.FindCoveredEye(currentBoard, lib, c))))
                return false;

            //three liberty eye group
            if (eyeGroups.Any(e => ThreeLibertyGroupNearCapture(currentBoard, e)))
                return false;

            return true;
        }

        /// <summary>
        /// Three liberty group near capture.
        /// </summary>
        private static Boolean ThreeLibertyGroupNearCapture(Board board, Group eyeGroup)
        {
            if (eyeGroup.Liberties.Count != 3) return false;
            foreach (Point liberty in eyeGroup.Liberties)
            {
                Board b = board.MakeMoveOnNewBoard(liberty, eyeGroup.Content.Opposite());
                if (b == null || !b.CapturedList.Any(p => p.Points.Count > 1)) continue;
                if (WallHelper.IsNonKillableGroup(b)) continue;
                if (ImmovableHelper.CheckConnectAndDie(b, b.GetCurrentGroup(eyeGroup)))
                    return true;
            }
            return false;
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
                List<Group> targetGroups = AtariHelper.AtariByGroup(ngroup, board);
                if (targetGroups.Count == 0) continue;
                Board b = ImmovableHelper.CaptureSuicideGroup(board, targetGroups.First(), true);
                if (b == null) continue;
                if (ImmovableHelper.CheckConnectAndDie(b, b.GetCurrentGroup(board.MoveGroup)))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Suicide at big tiger mouth.
        /// <see cref="UnitTestProject.FillKoEyeMoveTest.FillKoEyeMoveTest_Scenario_GuanZiPu_B3" /> 
        /// <see cref="UnitTestProject.FillKoEyeMoveTest.FillKoEyeMoveTest_Scenario_Corner_A85" /> 
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario6kyu13" />
        /// Opponent capture two or more points <see cref="UnitTestProject.MustHaveNeutralMoveTest.MustHaveNeutralMoveTest_Scenario_XuanXuanGo_A23" />
        /// <see cref="UnitTestProject.MustHaveNeutralMoveTest.MustHaveNeutralMoveTest_Scenario_XuanXuanQiJing_Weiqi101_7245" />
        /// <see cref="UnitTestProject.MustHaveNeutralMoveTest.MustHaveNeutralMoveTest_Scenario_TianLongTu_Q16827_2" />
        /// Check for opponent survival move <see cref="UnitTestProject.FillKoEyeMoveTest.FillKoEyeMoveTest_Scenario_WindAndTime_Q29475" /> 
        /// Unstoppable group <see cref="UnitTestProject.BaseLineKillerMoveTest.BaseLineKillerMoveTest_Scenario_XuanXuanQiJing_A53" /> 
        /// </summary>
        private static (Boolean, Board) SuicideAtBigTigerMouth(GameTryMove tryMove)
        {
            Point move = tryMove.Move;
            Board tryBoard = tryMove.TryGame.Board;
            Board currentBoard = tryMove.CurrentGame.Board;
            Content c = tryMove.MoveContent;
            List<Group> eyeGroups = currentBoard.GetGroupsFromStoneNeighbours(move, c.Opposite()).Where(e => e.Liberties.Count == 2).ToList();
            foreach (Group eyeGroup in eyeGroups)
            {
                List<Point> liberties = eyeGroup.Liberties.Where(lib => !lib.Equals(move)).ToList();
                if (liberties.Count != 1) continue;
                Point liberty = liberties.First();
                (Boolean suicidal, Board b) = ImmovableHelper.IsSuicidalMove(liberty, eyeGroup.Content, currentBoard);
                if (b == null || b.GetNeighbourGroups().All(n => WallHelper.IsNonKillableGroup(b, n))) continue;
                if (suicidal || ImmovableHelper.CheckConnectAndDie(b))
                    return (true, b);

                if (b == null || b.MoveGroup.Liberties.Count != 2) continue;

                //make block move
                List<Point> moveGroupLiberties = b.MoveGroup.Liberties.Where(lib => !lib.Equals(move)).ToList();
                (Boolean suicidal2, Board b2) = ImmovableHelper.IsSuicidalMove(moveGroupLiberties.First(), eyeGroup.Content.Opposite(), b);
                if (suicidal2) continue;

                //opponent capture two or more points
                if (b2.CapturedList.Any(gr => gr.Points.Count >= 2))
                    return (true, b);
                //check for opponent survival move
                if (b2.GetStoneNeighbours().Where(n => b2[n] != c.Opposite()).Select(n => GroupHelper.GetKillerGroupOfNeighbourGroups(b2, n, c.Opposite())).Any(n => n != null && n.Points.Count >= 3))
                    return (true, b);

                b2[move] = c;
                //unstoppable group
                if (ImmovableHelper.CheckConnectAndDie(b2))
                {
                    if (b2.GetGroupsFromStoneNeighbours(liberty, c).Count == 1) continue;
                    return (true, b);
                }
            }
            return (false, null);
        }
        #endregion

        #region atari redundant move

        /// <summary>
        /// Redundant atari move.
        /// </summary>
        public static Boolean AtariRedundantMove(GameTryMove tryMove)
        {
            Board tryBoard = tryMove.TryGame.Board;
            if (tryBoard.AtariTargets.Count != 1 || tryMove.AtariResolved || tryBoard.MoveGroupLiberties == 1 || tryBoard.CapturedList.Count > 0) return false;
            Group atariTarget = tryBoard.AtariTargets.First();
            //ensure target group can escape
            if (!ImmovableHelper.UnescapableGroup(tryBoard, atariTarget).Item1) return false;
            return RedundantAtariWithinKillerGroup(tryMove);
        }

        /// <summary>
        /// Redundant atari within killer group.
        /// <see cref="UnitTestProject.AtariRedundantMoveTest.AtariRedundantMoveTest_Scenario_Corner_A9_Ext" />
        /// Check for increased killer groups <see cref="UnitTestProject.AtariRedundantMoveTest.AtariRedundantMoveTest_Scenario_TianLongTu_Q16487" />
        /// <see cref="UnitTestProject.AtariRedundantMoveTest.AtariRedundantMoveTest_Scenario_WuQingYuan_Q31493" />
        /// Check for reverse ko fight <see cref="UnitTestProject.AtariRedundantMoveTest.AtariRedundantMoveTest_Scenario_WuQingYuan_Q30982" />
        /// Check for diagonal killer group <see cref="UnitTestProject.AtariRedundantMoveTest.AtariRedundantMoveTest_Scenario_WindAndTime_Q30225_2" />
        /// Ensure more than one liberty for move group <see cref="UnitTestProject.AtariRedundantMoveTest.AtariRedundantMoveTest_Scenario_Corner_A68" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_TianLongTu_Q16748" />
        /// Check for weak groups <see cref="UnitTestProject.AtariRedundantMoveTest.AtariRedundantMoveTest_Scenario_WuQingYuan_Q31503" />
        /// Check capture secure <see cref="UnitTestProject.AtariRedundantMoveTest.AtariRedundantMoveTest_Scenario_XuanXuanQiJing_Weiqi101_B74" />
        /// Check killer formation <see cref="UnitTestProject.AtariRedundantMoveTest.AtariRedundantMoveTest_Scenario_Side_A25" />
        /// <see cref="UnitTestProject.AtariRedundantMoveTest.AtariRedundantMoveTest_Scenario_Side_A23" />
        /// Count possible eyes at stone neighbours <see cref="UnitTestProject.AtariRedundantMoveTest.AtariRedundantMoveTest_Scenario_XuanXuanGo_A151_101Weiqi" />
        /// </summary>
        private static Boolean RedundantAtariWithinKillerGroup(GameTryMove tryMove)
        {
            Board currentBoard = tryMove.CurrentGame.Board;
            Board tryBoard = tryMove.TryGame.Board;
            Point move = tryMove.Move;
            Content c = tryBoard.MoveGroup.Content;
            Group atariTarget = tryBoard.AtariTargets.First();
            Point atariPoint = atariTarget.Points.First();
            //check for redundant atari within killer group
            Group killerGroup = GroupHelper.GetKillerGroupFromCache(tryBoard, atariPoint, c);
            if (killerGroup == null) return false;
            //ensure more than one liberty for move group
            if (tryBoard.MoveGroupLiberties == 1) return false;
            //check for increased killer groups
            if (tryBoard.GetStoneNeighbours().Any(n => tryBoard[n] != c && GroupHelper.GetKillerGroupFromCache(tryBoard, n, c) != killerGroup)) return false;

            //make move at the other liberty
            Point q = atariTarget.Liberties.First();
            (Boolean suicidal, Board board) = ImmovableHelper.IsSuicidalMove(q, c, currentBoard);
            if (suicidal) return false;
            Group killerGroup2 = GroupHelper.GetKillerGroupFromCache(board, atariPoint, c);
            if (killerGroup2 == null) return false;
            //ensure the other move can capture atari target as well
            if (!ImmovableHelper.UnescapableGroup(board, board.GetGroupAt(atariPoint)).Item1) return false;
            //check capture secure
            if (!ImmovableHelper.CheckCaptureSecure(tryBoard, atariTarget))
                return false;
            //check for weak groups
            if (LinkHelper.GetPreviousMoveGroup(currentBoard, tryBoard).Any(gr => gr.Liberties.Count <= 2)) return false;
            //check for reverse ko fight
            if (KoHelper.IsReverseKoFight(tryBoard)) return false;
            //check for diagonal killer group
            if (tryBoard.GetDiagonalNeighbours().Any(n => EyeHelper.FindNonSemiSolidEye(tryBoard, n, c))) return false;


            //check killer formation
            Board b = tryBoard.MakeMoveOnNewBoard(q, c.Opposite());
            if (b != null)
            {
                Boolean killerFormation = KillerFormationHelper.SuicidalKillerFormations(b, tryBoard);
                Board b2 = board.MakeMoveOnNewBoard(move, c.Opposite());
                if (b2 != null)
                {
                    Boolean killerFormation2 = KillerFormationHelper.SuicidalKillerFormations(b2, board);
                    if (killerFormation && !killerFormation2) return true;
                    if (!killerFormation && killerFormation2) return false;
                }
            }
            //count possible eyes at stone neighbours
            int qEyeCount = PossibleEyesCreated(currentBoard, q, c);
            int moveEyeCount = PossibleEyesCreated(currentBoard, move, c);
            if (qEyeCount > moveEyeCount) return true;
            else if (qEyeCount < moveEyeCount) return false;
            //return only one move if both moves valid
            else return (q.x + q.y * board.SizeX) < (move.x + move.y * board.SizeX);
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
            if (ThreeLibertySuicidal(tryMove))
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
            return false;
        }

        /// <summary>
        /// Three liberty suicidal.
        /// <see cref="UnitTestProject.ThreeLibertySuicidalTest.ThreeLibertySuicidalTest_Scenario5dan18" />
        /// <see cref="UnitTestProject.ThreeLibertySuicidalTest.ThreeLibertySuicidalTest_Scenario_GuanZiPu_A2Q29_101Weiqi" />
        /// </summary>
        private static Boolean ThreeLibertySuicidal(GameTryMove tryMove)
        {
            Board tryBoard = tryMove.TryGame.Board;
            if (!tryMove.IsNegligible) return false;
            if (tryBoard.MoveGroupLiberties != 3) return false;
            if (tryBoard.MoveGroup.Liberties.Any(lib => ImmovableHelper.ThreeLibertyConnectAndDie(tryBoard, lib)))
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
        /// Check for bloated eye move <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_Corner_A85" />
        /// Check for eye at liberty point <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanQiJing_A8" />
        /// Check for tiger mouth at liberty point <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WuQingYuan_Q31646" />
        /// Check for suicidal at other end <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_TianLongTu_Q16867" />
        /// Check if link for groups <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanGo_B32_2" />
        /// Check for both alive <see cref="UnitTestProject.SurvivalTigerMouthMoveTest.SurvivalTigerMouthMoveTest_Scenario_TianLongTu_Q16827" />
        /// Set diagonal eye move <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WindAndTime_Q29273" />
        /// </summary>
        private static Boolean MultiPointOpponentSuicidalMove(GameTryMove tryMove, GameTryMove opponentMove)
        {
            Board opponentTryBoard = opponentMove.TryGame.Board;
            Board currentBoard = tryMove.CurrentGame.Board;
            Board tryBoard = tryMove.TryGame.Board;
            Point move = tryBoard.Move.Value;
            Content c = tryBoard.MoveGroup.Content;
            if (opponentTryBoard.MoveGroup.Points.Count < 2) return false;
            if (tryBoard.MoveGroupLiberties == 1 || tryBoard.CapturedList.Count != 0 || tryMove.AtariResolved || tryBoard.AtariTargets.Count != 1) return false;
            if (!MultiPointSuicidalMove(opponentMove)) return false;

            if (tryBoard.GetStoneNeighbours().Any(n => tryBoard[n] == Content.Empty)) return false;

            Group atariTarget = tryBoard.AtariTargets.First();
            //check for unescapable group
            (Boolean unEscapable, _, Board escapeBoard) = ImmovableHelper.UnescapableGroup(tryBoard, atariTarget, false);
            if (unEscapable) return false;


            //check for weak group
            if (CheckWeakGroupInOpponentSuicide(tryBoard, atariTarget))
                return false;

            //check for suicide at big tiger mouth
            if (SuicideAtBigTigerMouth(tryMove).Item1)
                return false;

            //check for bloated eye move
            if (tryBoard.GetDiagonalNeighbours().Any(d => tryBoard[d] == Content.Empty && tryBoard.GetStoneNeighbours(d).Any(n => tryBoard[n] == Content.Empty && tryBoard.CornerPoint(n) && KoHelper.IsReverseKoFight(currentBoard, n, c))))
                return false;

            //check for both alive
            if (BothAliveHelper.CheckForBothAliveAtMove(tryBoard)) return false;

            if (WallHelper.IsNonKillableGroup(tryBoard)) //set neutral point move
                tryMove.IsNeutralPoint = true;
            if (tryBoard.GetDiagonalNeighbours().Any(n => GroupHelper.GetKillerGroupFromCache(tryBoard, n, c) != null)) //set diagonal eye move
                tryMove.IsDiagonalEyeMove = true;

            return true;
        }

        /// <summary>
        /// Check weak group in opponent suicide.
        /// <see cref = "UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_TianLongTu_Q16604_3" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_TianLongTu_Q16604_4" />
        /// </summary>
        private static Boolean CheckWeakGroupInOpponentSuicide(Board tryBoard, Group atariTarget)
        {
            Content c = tryBoard.MoveGroup.Content;
            Board b = ImmovableHelper.MakeMoveAtLibertyPointOfSuicide(tryBoard, atariTarget, c.Opposite());
            if (b == null || b.MoveGroupLiberties == 1) return false;
            return GetWeakGroup(b, b.MoveGroup);
        }

        /// <summary>
        /// Check for connect and die moves. <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_TianLongTu_Q16738" />
        /// Reverse connect and die <see cref="UnitTestProject.ImmovableTest.ImmovableTest_Scenario_WindAndTime_Q29277" />
        /// Check capture moves <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanGo_A75_101Weiqi" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.CheckForRecursionTest_Scenario_Corner_B41" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_Corner_A113_3" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanQiJing_B36" />
        /// Check atari moves <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WuQingYuan_Q30986" />
        /// Check for sieged scenario <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_TianLongTu_Q2834" />
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
            (Boolean suicidal, Board captureBoard) = ImmovableHelper.ConnectAndDie(tryBoard, tryBoard.MoveGroup, false);
            if (!suicidal) return false;

            if (LifeCheck.GetTargets(tryBoard).All(t => tryBoard.MoveGroup.Equals(t))) return true;

            //reverse connect and die
            if (tryBoard.MoveGroup.Points.Count == 1 && captureBoard.MoveGroup.Points.Count == 1 && ImmovableHelper.CheckConnectAndDie(captureBoard))
                return false;

            //check capture moves
            if (tryBoard.CapturedList.Any(g => AtariHelper.AtariByGroup(currentBoard, g))) return false;

            //check atari moves
            foreach (Group atariTarget in tryBoard.AtariTargets)
            {
                Board b = ImmovableHelper.CaptureSuicideGroup(captureBoard, atariTarget);
                if (b != null && b.CapturedList.Count > 1)
                    return false;
            }

            //check for one point move group
            if (CheckOnePointMoveInConnectAndDie(tryMove, captureBoard))
                return false;

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
                if (KillerFormationHelper.MultipointSnapbackAfterCapture(tryBoard, captureBoard))
                    return false;
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
            Board currentBoard = tryMove.CurrentGame.Board;
            Board tryBoard = tryMove.TryGame.Board;
            Point move = tryMove.Move;
            Content c = tryMove.MoveContent;

            if (tryBoard.MoveGroup.Points.Count != 1 || !tryMove.IsNegligible) return false;
            //ensure killer group contains only try move
            if (!GroupHelper.IsSingleGroupWithinKillerGroup(tryBoard, currentBoard))
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
        /// Check for double atari for one-point move <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WindAndTime_Q29481" />
        /// Check killable group with two or less liberties <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanGo_B6" />
        /// Check for weak group capturing atari group <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanQiJing_B17" />
        /// </summary>
        private static Boolean CheckWeakGroupInConnectAndDie(Board tryBoard, Group moveGroup)
        {
            Content c = moveGroup.Content;

            //capture move
            (_, Board b) = ImmovableHelper.ConnectAndDie(tryBoard, moveGroup, false);
            if (b == null || b.IsCapturedGroup(moveGroup)) return false;

            //check weak group
            if (GetWeakGroup(b, b.GetCurrentGroup(moveGroup)))
                return true;

            //escape move at liberty
            Board b2 = ImmovableHelper.MakeMoveAtLibertyPointOfSuicide(b, moveGroup, c);
            if (b2 != null && b2.MoveGroupLiberties == 2 && CheckWeakGroupInConnectAndDie(b2, b2.GetCurrentGroup(moveGroup)))
                return true;

            //escape by capture
            foreach (Group gr in AtariHelper.AtariByGroup(b.GetCurrentGroup(moveGroup), b))
            {
                Board b3 = ImmovableHelper.CaptureSuicideGroup(b, gr);
                if (b3 == null) continue;
                Group target = b3.GetCurrentGroup(moveGroup);
                if (b3 != null && target.Liberties.Count == 2 && CheckWeakGroupInConnectAndDie(b3, target))
                    return true;
                if (!b3.MoveGroup.Equals(target) && GetWeakGroup(b, b3.MoveGroup))
                    return true;
            }
            return false;
        }

        private static Boolean GetWeakGroup(Board tryBoard, Group moveGroup)
        {
            if (WallHelper.IsNonKillableFromSetupMoves(tryBoard, moveGroup))
                return false;

            foreach (Group group in tryBoard.GetNeighbourGroups(moveGroup).Where(n => n.Liberties.Count <= 2))
            {
                foreach (Point liberty in group.Liberties)
                {
                    (Boolean suicidal, Board b) = ImmovableHelper.IsSuicidalMove(liberty, group.Content.Opposite(), tryBoard);
                    if (suicidal) continue;
                    if (WallHelper.IsNonKillableGroup(b)) continue;
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Find bloated eye suicide <see cref="UnitTestProject.GenericNeutralMoveTest.GenericNeutralMoveTest_Scenario_GuanZiPu_A35" />
        /// </summary>
        public static Boolean FindBloatedEyeSuicide(GameTryMove tryMove, Board captureBoard)
        {
            Board tryBoard = tryMove.TryGame.Board;
            Content c = tryMove.MoveContent;
            foreach (Point p in tryBoard.MoveGroup.Liberties)
            {
                if (!EyeHelper.FindEye(tryBoard, p, c)) continue;
                if (tryBoard.GetDiagonalNeighbours(p).Count(q => tryBoard[q] == Content.Empty) == 1)
                {
                    if (!tryBoard.GetGroupsFromStoneNeighbours(p, c.Opposite()).All(group => group.Liberties.Count <= 2)) continue;
                    Board b = ImmovableHelper.MakeMoveAtLibertyPointOfSuicide(captureBoard, captureBoard.GetCurrentGroup(tryBoard.MoveGroup), c);
                    if (b != null) continue;
                    return true;
                }
            }
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
        /// Check for one point move group in connect and die.
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario2dan21" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanQiJing_Weiqi101_2398" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanQiJing_A39" />
        /// <see cref="UnitTestProject.BaseLineSurvivalMoveTest.BaseLineSurvivalMoveTest_Scenario_XuanXuanQiJing_Weiqi101_18473" />
        /// Check tiger mouth at corner point <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_Nie1" />
        /// One point move near non killable group
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_GuanZiPu_B7" />
        /// <see cref="UnitTestProject.LeapMoveTest.LeapMoveTest_Scenario_TianLongTu_Q16571" />
        /// Check reverse ko fight <see cref="UnitTestProject.RedundantKoMoveTest.RedundantKoMoveTest_Scenario_Corner_A80" />
        /// Check opponent move liberties <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WuQingYuan_Q31680_3" />
        /// Check snapback at diagonal <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanGo_Q6710_2" />
        /// </summary>
        private static Boolean CheckOnePointMoveInConnectAndDie(GameTryMove tryMove, Board captureBoard)
        {
            Board tryBoard = tryMove.TryGame.Board;
            Point move = tryBoard.Move.Value;
            Content c = tryMove.MoveContent;
            if (tryBoard.MoveGroup.Points.Count != 1) return false;

            //check reverse ko fight
            if (KoHelper.CheckReverseKoForNeutralPoint(tryBoard))
                return true;

            //check tiger mouth at corner point
            List<Point> corner = tryBoard.GetStoneNeighbours().Where(n => tryBoard.CornerPoint(n)).ToList();
            if (corner.Count != 1) return false;
            Point? tigerMouth = ImmovableHelper.FindTigerMouth(tryBoard, corner.First(), c);
            if (tigerMouth == null) return false;
            (Boolean suicidal, Board b) = ImmovableHelper.IsSuicidalMove(tigerMouth.Value, c, captureBoard);
            if (suicidal) return false;
            Board b2 = ImmovableHelper.CaptureSuicideGroup(b, tryBoard.MoveGroup);
            if (b2 == null) return false;
            Group escapeGroup = b2.GetGroupAt(b.Move.Value);
            if (escapeGroup.Liberties.Count > 1 || !ImmovableHelper.UnescapableGroup(b2, escapeGroup).Item1)
                return true;
            return false;
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

            if (captureBoard.GetNeighbourGroups(move).All(n => WallHelper.IsNonKillableGroup(captureBoard, n)))
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
                Group tryKillerGroup = GroupHelper.GetKillerGroupFromCache(tryBoard, move, c.Opposite());
                if (tryKillerGroup != null && tryMove.IsNegligible && tryBoard.GetStoneNeighbours().Any(p => tryBoard[p] == Content.Empty && EyeHelper.IsCovered(tryBoard, p, c.Opposite())))
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
                    Board b = tryBoard.MakeMoveOnNewBoard(cutDiagonal.First(), c);
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

            //remove one point from two-point empty group
            Group eyeGroup = GroupHelper.GetKillerGroupFromCache(currentBoard, move, c.Opposite());
            if (eyeGroup != null && eyeGroup.Points.All(p => !currentBoard.CornerPoint(p)))
            {
                Board b = EyeHelper.FindRealEyesWithinTwoEmptyPoints(currentBoard, eyeGroup, EyeType.RealSolidEye);
                if (b != null && !move.Equals(b.Move.Value))
                    return true;
            }
            if (capturedBoard.MoveGroupLiberties == 1) return false;

            //check non killable neighbour group
            if (tryBoard.GetNeighbourGroups().Any(n => WallHelper.IsNonKillableGroup(tryBoard, n))) return true;

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

            //opponent break kill formation
            if (OpponentBreakKillFormation(tryMove))
                return false;

            //retrieve liberties other than eye liberty
            List<Group> ngroups = capturedBoard.GetNeighbourGroups(tryBoard.MoveGroup);
            HashSet<Point> liberties = capturedBoard.GetLibertiesOfGroups(ngroups);
            liberties.Remove(move);

            if (liberties.Count == 1)
            {
                //suicide for liberty fight
                if (SuicideForLibertyFight(tryMove, false))
                    return false;
            }
            else if (liberties.Count == 2)
            {
                //two liberties - suicide for both players
                foreach (Point p in liberties)
                {
                    (Boolean isSuicidal, Board b) = ImmovableHelper.IsSuicidalMove(p, c, capturedBoard);
                    if (isSuicidal) continue;
                    //both players are suicidal at the liberty
                    Point q = liberties.First(liberty => !liberty.Equals(p));
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
                    foreach (Point p in nLiberties)
                    {
                        (Boolean isSuicidal, Board b) = ImmovableHelper.IsSuicidalMove(p, c, capturedBoard);
                        if (isSuicidal) continue;
                        //both players are suicidal at the liberty
                        Point q = nLiberties.First(liberty => !liberty.Equals(p));
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
        /// Opponent break kill formation.
        /// <see cref="UnitTestProject.SpecificNeutralMoveTest.SpecificNeutralMoveTest_Scenario_TianLongTu_Q16827" />
        /// <see cref="UnitTestProject.KillerFormationTest.KillerFormationTest_Scenario_TianLongTu_Q16859_2" />
        /// </summary>
        private static Boolean OpponentBreakKillFormation(GameTryMove tryMove)
        {
            Point move = tryMove.Move;
            Board tryBoard = tryMove.TryGame.Board;
            Board currentBoard = tryMove.CurrentGame.Board;
            Content c = tryMove.MoveContent;
            if (tryBoard.GetStoneAndDiagonalNeighbours().Count(n => tryBoard[n] == c.Opposite()) < 4) return false;
            if (KillerFormationHelper.TryKillFormation(currentBoard, c.Opposite(), new List<Point>() { move }))
                return true;
            return false;
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

            foreach (Point lib in liberties)
            {
                Board b = capturedBoard.MakeMoveOnNewBoard(lib, c);
                if (b != null && WallHelper.StrongNeighbourGroups(b, b.GetNeighbourGroups(capturedBoard.MoveGroup)))
                    return true;
            }
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

            //suicide near non killable group
            if (opponentTryMove != null)
            {
                //opponent suicide
                if (SuicideAtBigTigerMouth(opponentTryMove).Item1 || BothAliveHelper.CheckForBothAliveAtMove(opponentTryMove.TryGame.Board))
                    return false;
            }

            if (GameHelper.GetContentForSurviveOrKill(tryBoard.GameInfo, SurviveOrKill.Survive) == c)
            {
                //for survive, any suicidal move next to non killable group is redundant
                List<Group> neighbourGroups = tryBoard.GetGroupsFromStoneNeighbours(move);
                if (neighbourGroups.Any(group => WallHelper.IsNonKillableGroup(tryBoard, group)))
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
                    Board b = ImmovableHelper.MakeMoveAtLibertyPointOfSuicide(tryBoard, atariTarget, c.Opposite());
                    if (b != null && b.MoveGroupLiberties > 1)
                        return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Multi point suicide move.
        /// Check for corner kill <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario7kyu25" />
        /// Check for connect and die <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanQiJing_A36" />
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
        /// Bent three formation <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WuQingYuan_Q31453" />
        /// No hope of escape <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_TianLongTu_Q17132_2" />
        /// </summary>
        public static Boolean MultiPointSuicidalMove(GameTryMove tryMove)
        {
            Point move = tryMove.Move;
            Board tryBoard = tryMove.TryGame.Board;
            Board currentBoard = tryMove.CurrentGame.Board;
            Content c = tryMove.MoveContent;
            int moveCount = tryBoard.MoveGroup.Points.Count;

            Board capturedBoard = ImmovableHelper.CaptureSuicideGroup(tryBoard);
            if (capturedBoard == null) return false;

            if (tryBoard.CapturedList.Count > 0) //capture at tryBoard
            {
                //check for connect and die
                if (ImmovableHelper.CheckConnectAndDie(capturedBoard))
                    return false;

                //capture at tryBoard more than recapture        
                if (tryBoard.CapturedPoints.Any(p => !capturedBoard.CapturedPoints.Contains(p) && !capturedBoard.Move.Equals(p)))
                    return false;
            }

            if (moveCount == 2)
            {
                //suicide for liberty fight
                if (SuicideForLibertyFight(tryMove))
                    return false;

                //two-point atari move
                if (TwoPointAtariMove(tryBoard, capturedBoard))
                    return false;
            }
            else if (moveCount == 3)
            {
                //bent three formation
                if (capturedBoard.MoveGroupLiberties == 1 && KillerFormationHelper.BentThreeFormation(tryBoard, tryBoard.MoveGroup.Points))
                {
                    (_, Board b) = ImmovableHelper.ConnectAndDie(capturedBoard);
                    if (b != null)
                    {
                        IEnumerable<dynamic> pointIntersect = KillerFormationHelper.GetPointIntersect(tryBoard, tryBoard.MoveGroup.Points);
                        List<Point> endPoints = pointIntersect.Where(p => p.intersectCount == 1).Select(p => (Point)p.point).ToList();
                        if (endPoints.Any(p => !p.Equals(move) && EyeHelper.IsCovered(b, p, c.Opposite())))
                            return false;
                    }
                }
            }

            //killer formations
            if (KillerFormationHelper.SuicidalKillerFormations(tryBoard, currentBoard, capturedBoard))
                return false;

            //no hope of escape
            return true;
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
        private static Boolean SuicideForLibertyFight(GameTryMove tryMove, Boolean removeOnePoint = true)
        {
            Board tryBoard = tryMove.TryGame.Board;
            Board currentBoard = tryMove.CurrentGame.Board;
            Point move = tryBoard.Move.Value;
            Content c = tryBoard.MoveGroup.Content;
            //suicide within killer group
            Group killerGroup = GroupHelper.GetKillerGroupFromCache(currentBoard, move, c.Opposite());
            if (killerGroup == null || killerGroup.Points.Count != tryBoard.MoveGroup.Points.Count + 1) return false;

            List<Group> targetGroups = currentBoard.GetNeighbourGroups(killerGroup);
            //get only one move within killer group
            if (removeOnePoint && targetGroups.Count == 1)
            {
                Boolean firstPoint = killerGroup.Points.FirstOrDefault(p => currentBoard[p] == Content.Empty).Equals(move);
                if (!firstPoint) return false;
            }

            //get external liberty
            foreach (Group targetGroup in targetGroups)
            {
                List<Point> externalLiberties = targetGroup.Liberties.Except(killerGroup.Points).ToList();
                if (externalLiberties.Count != 1) continue;
                Point liberty = externalLiberties.First();
                if (KoHelper.IsKoFight(tryBoard, liberty, c.Opposite()).Item1)
                {
                    //check killer ko within killer group
                    if (GroupHelper.GetKillerGroupFromCache(tryBoard, targetGroup.Points.First(), c) == null) continue;
                    if (tryBoard.GetNeighbourGroups(targetGroup).Any(n => WallHelper.IsNonKillableGroup(tryBoard, n))) continue;
                }
                else
                {
                    List<Group> groups = tryBoard.GetGroupsFromStoneNeighbours(liberty, c.Opposite()).ToList();
                    if (groups.Count == 0 || tryBoard.GetLibertiesOfGroups(groups).Count == 1) continue;
                }

                //check suicidal move for both players at external liberty
                if (ImmovableHelper.IsSuicidalMoveForBothPlayers(tryBoard, liberty))
                    return true;

                if (ImmovableHelper.IsSuicidalMoveForBothPlayers(currentBoard, liberty, true))
                    return true;
            }

            //ko move on external liberty (optional)
            foreach (Group targetGroup in targetGroups)
            {
                List<Point> externalLiberties = targetGroup.Liberties.Except(killerGroup.Points).ToList();
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
        /// Two point atari move 
        /// Check for three groups <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WuQingYuan_Q30935" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WindAndTime_Q2757_2" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanGo_A82_101Weiqi" />
        /// <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_TianLongTu_Q15017" />
        /// Check snapback <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_Scenario_WuQingYuan_Q31469" />
        /// Check for ko fight 
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WuQingYuan_Q31672" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WuQingYuan_Q31428" />
        /// </summary>
        private static Boolean TwoPointAtariMove(Board tryBoard, Board capturedBoard)
        {
            Point move = tryBoard.Move.Value;
            Content c = tryBoard.MoveGroup.Content;
            if (!capturedBoard.CapturedList.Any(gr => gr.Points.Count == 2) || !tryBoard.IsAtariMove) return false;
            //check for three groups
            if (tryBoard.GetGroupsFromStoneNeighbours(move).Count > 2) return true;

            Board board = capturedBoard.MakeMoveOnNewBoard(move, c);
            if (board == null || board.AtariTargets.Count != 1) return false;
            //check snapback
            if (board.GetDiagonalNeighbours().Any(n => board[n] == c) && ImmovableHelper.IsSuicidalOnCapture(board).Item1)
                return true;
            //check for ko fight
            if (board.AtariTargets.Count == 1 && board.AtariTargets.First().Points.Count == 1)
            {
                Point? libertyPoint = ImmovableHelper.GetLibertyPointOfSuicide(board, board.AtariTargets.First());
                if (libertyPoint == null) return false;
                Point q = libertyPoint.Value;
                if (EyeHelper.FindNonSemiSolidEye(capturedBoard, q, c.Opposite()))
                    return true;

                List<Point> emptyPoints = board.GetStoneNeighbours(q).Where(n => board[n] == Content.Empty).ToList();
                if (emptyPoints.Count != 1) return false;

                Group killerGroup = GroupHelper.GetKillerGroupFromCache(board, q, c.Opposite());
                if (killerGroup != null && killerGroup.Points.Count == 2 && EyeHelper.IsCovered(board, emptyPoints.First(), c.Opposite()))
                    return true;
            }
            return false;
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
            if (closestNeighbours.All(leapMove => !ValidateLeapMove(tryBoard, move, leapMove)))
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

        /// <summary>
        /// For leap on same line, check for non killable group between the two points as well as one space above or below the space between the leap.
        /// For leap on different lines, check for non killable group between the two points from min to max of the lines.
        /// </summary>
        public static Boolean ValidateLeapMove(Board tryBoard, Point p, Point q, Boolean checkNonKillable = true)
        {
            Content c = tryBoard[p];
            //get middle points between the leap points
            List<Point> middlePoints = new List<Point>();
            if (Math.Abs(p.x - q.x) == 2)
            {
                if (Math.Abs(p.y - q.y) > 2) return false;
                int y_min = Math.Min(p.y, q.y);
                int y_max = Math.Max(p.y, q.y);
                if (p.y.Equals(q.y) && !tryBoard.PointWithinMiddleArea(p) && !tryBoard.PointWithinMiddleArea(q)) //leap on same line
                {
                    y_min -= 1;
                    y_max += 1;
                }
                for (int i = y_min; i <= y_max; i++)
                {
                    int middle_x = (p.x > q.x) ? q.x + 1 : q.x - 1;
                    middlePoints.Add(new Point(middle_x, i));
                }
            }
            else if (Math.Abs(p.y - q.y) == 2)
            {
                if (Math.Abs(p.x - q.x) > 2) return false;
                int x_min = Math.Min(p.x, q.x);
                int x_max = Math.Max(p.x, q.x);
                if (p.x.Equals(q.x) && !tryBoard.PointWithinMiddleArea(p) && !tryBoard.PointWithinMiddleArea(q)) //leap on same line
                {
                    x_min -= 1;
                    x_max += 1;
                }
                for (int i = x_min; i <= x_max; i++)
                {
                    int middle_y = (p.y < q.y) ? q.y - 1 : q.y + 1;
                    middlePoints.Add(new Point(i, middle_y));
                }
            }
            middlePoints.RemoveAll(n => !tryBoard.PointWithinBoard(n));
            if (middlePoints.Count == 0 || middlePoints.Any(t => tryBoard[t] == c)) return false;
            //check for opposite content at middle points
            foreach (Point midPt in middlePoints)
            {
                if (tryBoard[midPt] == Content.Empty)
                    continue;
                if (tryBoard[midPt] == c.Opposite())
                {
                    if (!checkNonKillable) return false;
                    if (WallHelper.IsNonKillableGroup(tryBoard, midPt))
                        return false;
                }
            }
            return true;
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
            Boolean isNeutralPoint = ValidateNeutralPoint(tryMove);
            if (!isNeutralPoint && opponentMove == null)
            {
                if (NeutralPointAtNonKillableCorner(tryMove))
                    return true;
            }
            return isNeutralPoint;
        }

        /// <summary>
        /// Neutral point at non killable corner. 
        /// <see cref="UnitTestProject.NeutralPointMoveTest.NeutralPointMoveTest_Scenario_XuanXuanGo_A26" />
        /// Check if any killable group <see cref="UnitTestProject.NeutralPointMoveTest.NeutralPointMoveTest_Scenario_WindAndTime_Q29277" />
        /// Check killer group for captured points <see cref="UnitTestProject.NeutralPointMoveTest.NeutralPointMoveTest_ScenarioHighLevel18" />
        /// <see cref="UnitTestProject.NeutralPointMoveTest.NeutralPointMoveTest_Scenario_TianLongTu_Q17132" />
        /// <see cref="UnitTestProject.RedundantEyeDiagonalMoveTest.RedundantEyeDiagonalMoveTest_Scenario_SiHuoDaQuan_CornerA29_2" />
        /// Check eye or tiger mouth at stone and diagonal <see cref="UnitTestProject.NeutralPointMoveTest.NeutralPointMoveTest_Scenario_XuanXuanGo_A26_2" />
        /// <see cref="UnitTestProject.NeutralPointMoveTest.NeutralPointMoveTest_Scenario3kyu28" />
        /// <see cref="UnitTestProject.NeutralPointMoveTest.NeutralPointMoveTest_Scenario_TianLongTu_Q17132_3" />
        /// <see cref="UnitTestProject.NeutralPointMoveTest.NeutralPointMoveTest_Scenario_TianLongTu_Q16466" />
        /// <see cref="UnitTestProject.NeutralPointMoveTest.NeutralPointMoveTest_Scenario_XuanXuanGo_A151_101Weiqi" />
        /// </summary>
        private static Boolean NeutralPointAtNonKillableCorner(GameTryMove tryMove)
        {
            Board currentBoard = tryMove.CurrentGame.Board;
            Board tryBoard = tryMove.TryGame.Board;
            Point move = tryBoard.Move.Value;
            Content c = tryBoard.MoveGroup.Content;
            //ensure move at non killable corner
            List<Point> nonKillablePoints = tryBoard.GetStoneNeighbours().Where(n => tryBoard[n] == c.Opposite() && WallHelper.IsNonKillableFromSetupMoves(tryBoard, tryBoard.GetGroupAt(n))).ToList();
            if (nonKillablePoints.Count != 2) return false;
            Point k = nonKillablePoints[0];
            Point k2 = nonKillablePoints[1];
            if (!tryBoard.GetDiagonalNeighbours(k).Any(n => n.Equals(k2))) return false;

            //check if any killable group
            if (tryBoard.GetStoneAndDiagonalNeighbours().Any(n => tryBoard[n] == c.Opposite() && !WallHelper.IsNonKillableFromSetupMoves(tryBoard, tryBoard.GetGroupAt(n))))
                return false;

            //check killer group for captured points
            if (tryBoard.GetStoneAndDiagonalNeighbours().Where(n => tryBoard[n] != c).Select(n => GroupHelper.GetKillerGroupFromCache(tryBoard, n, c)).Any(n => n != null && n.Points.Any(q => tryBoard[q] == c.Opposite())))
                return false;

            //check eye or tiger mouth at stone and diagonal
            foreach (Point p in tryBoard.GetStoneAndDiagonalNeighbours().Where(n => tryBoard[n] == Content.Empty))
            {
                //check eye
                if (EyeHelper.FindEye(tryBoard, p, c) && tryBoard.GetGroupsFromStoneNeighbours(p, c.Opposite()).All(n => n.Liberties.Count > 1 || KoHelper.IsKoFight(tryBoard, n)))
                    return false;

                //check tiger mouth
                if (ImmovableHelper.FindTigerMouth(tryBoard, c, p))
                    return false;
            }
            return true;
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
                (Boolean mustHave, Point? linkPoint) = MustHaveNeutralPoint(tryMove, opponentMove);
                if (mustHave)
                {
                    tryMove.MustHaveNeutralPoint = true;
                    tryMove.LinkPoint = new LinkedPoint<Point>(linkPoint.Value, tryMove.Move);
                }
            }
            return isNeutralPoint;
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
            Board currentBoard = tryMove.CurrentGame.Board;
            Board tryBoard = tryMove.TryGame.Board;
            Content c = tryBoard.MoveGroup.Content;
            if (tryMove.AtariResolved || tryBoard.CapturedList.Count > 0 || tryBoard.MoveGroupLiberties == 1) return true;
            if (tryBoard.AtariTargets.Count != 1) return true;

            Group atariTarget = tryBoard.AtariTargets.First();
            if (atariTarget.Points.Count != 1) return true;

            //check neighbour groups
            Board b = ImmovableHelper.CaptureSuicideGroup(tryBoard, atariTarget, true);
            if (b != null && !WallHelper.StrongNeighbourGroups(b, b.MoveGroup))
                return true;

            return false;
        }

        /// <summary>
        /// Must have neutral point.
        /// Neutral point at tiger mouth <see cref="UnitTestProject.MustHaveNeutralMoveTest.MustHaveNeutralMoveTest_Scenario5dan27_3" />
        /// Neutral point at bigger tiger mouth <see cref="UnitTestProject.MustHaveNeutralMoveTest.MustHaveNeutralMoveTest_Scenario5dan27_Variation" />
        /// Negative example <see cref="UnitTestProject.MustHaveNeutralMoveTest.MustHaveNeutralMoveTest_Scenario_XuanXuanGo_A27" />
        /// <see cref="UnitTestProject.MustHaveNeutralMoveTest.MustHaveNeutralMoveTest_Scenario_XuanXuanGo_A23" />
        /// <see cref="UnitTestProject.MustHaveNeutralMoveTest.MustHaveNeutralMoveTest_Scenario_GuanZiPu_Weiqi101_19138" />
        /// 
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
        private static (Boolean, Point?) MustHaveNeutralPoint(GameTryMove tryMove, GameTryMove opponentMove)
        {
            Board tryBoard = tryMove.TryGame.Board;
            Board opponentBoard = opponentMove.TryGame.Board;

            //neutral point at small tiger mouth
            Point tigerMouth = opponentBoard.GetStoneNeighbours().FirstOrDefault(n => EyeHelper.FindEye(opponentBoard, n));
            if (Convert.ToBoolean(tigerMouth.NotEmpty))
            {
                //redundant suicidal at tiger mouth
                if (StrongGroupsAtMustHaveMove(tryBoard, tigerMouth))
                    return (false, null);
                return (true, tigerMouth);
            }

            //neutral point at big tiger mouth
            (Boolean suicide, Board suicideBoard) = SuicideAtBigTigerMouth(tryMove);
            if (suicide)
            {
                Point suicideMove = suicideBoard.Move.Value;
                if (MustHaveMoveAtBigTigerMouth(suicideBoard, tryMove))
                    return (true, suicideMove);
            }

            return (false, null);
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
            foreach (Group eyeGroup in eyeGroups)
            {
                List<Point> liberties = eyeGroup.Liberties.Where(lib => !lib.Equals(move)).ToList();
                if (liberties.Count != 1) continue;
                Point liberty = liberties.First();
                Board b = currentBoard.MakeMoveOnNewBoard(liberty, c.Opposite());
                if (b != null && b.CapturedList.Any(gr => gr.Points.Count >= 2))
                    return true;
            }

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
            List<Group> neighbourGroups = board.GetGroupsFromStoneNeighbours(tigerMouth, c).ToList();
            if (!WallHelper.StrongNeighbourGroups(board, neighbourGroups))
                return false;

            //check liberty fight
            if (board.GetLibertiesOfGroups(neighbourGroups).Count == 3)
            {
                foreach (Group ngroup in neighbourGroups)
                {
                    (_, List<Point> pointsBetweenDiagonals) = LinkHelper.FindDiagonalCut(tryBoard, ngroup);
                    if (pointsBetweenDiagonals == null) continue;
                    if (ngroup.Liberties.Select(lib => GroupHelper.GetKillerGroupFromCache(board, lib, c.Opposite())).Any(kgroup => kgroup != null && kgroup.Points.Count > 1 && EyeHelper.FindRealEyeWithinEmptySpace(tryBoard, kgroup)))
                        return false;
                }
            }
            return true;
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
            if (KoHelper.NeutralPointDoubleKo(tryBoard))
                return false;

            //check reverse ko for neutral point
            if (KoHelper.CheckReverseKoForNeutralPoint(tryBoard))
                return false;
            return true;
        }

        #endregion

        #region restore neutral points
        /// <summary>
        /// Neutral points for kill moves have to be restored on end game one at a time to surround external liberties of target group in order to kill it
        /// Two pre-atari moves <see cref="UnitTestProject.SpecificNeutralMoveTest.SpecificNeutralMoveTest_Scenario_Corner_A55" />
        /// No try moves left <see cref="UnitTestProject.MustHaveNeutralMoveTest.MustHaveNeutralMoveTest_Scenario_Side_A20" />
        /// Remaining move at liberty point <see cref="UnitTestProject.MustHaveNeutralMoveTest.MustHaveNeutralMoveTest_Scenario_XuanXuanQiJing_Weiqi101_7245" />
        /// Check connect and die for last two try moves <see cref="UnitTestProject.SuicidalRedundantMoveTest.NeutralPointMoveTest_Scenario_XuanXuanGo_B32" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_Side_B35" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanGo_A151_101Weiqi_5" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WuQingYuan_Q31580" />
        /// Suicide group near capture <see cref="UnitTestProject.FillKoEyeMoveTest.FillKoEyeMoveTest_Scenario_TianLongTu_Q16490" />
        /// <see cref="UnitTestProject.NeutralPointMoveTest.NeutralPointMoveTest_Scenario_WuQingYuan_Q6150" />
        /// </summary>
        public static void RestoreNeutralMove(Game currentGame, List<GameTryMove> tryMoves, List<GameTryMove> neutralPointMoves)
        {
            //Remove moves that are within killer group
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
                IEnumerable<Board> tryBoards = tryMoves.Select(t => t.TryGame.Board);
                if (tryBoards.All(t => ImmovableHelper.CheckConnectAndDie(t) || SuicideGroupNearCapture(t)))
                    tryMoves.Add(neutralPointMoves.First());
            }
        }

        /// <summary>
        /// Get specific neutral move to target survival groups with limited liberties.
        /// Two specific moves <see cref="UnitTestProject.SpecificNeutralMoveTest.SpecificNeutralMoveTest_Scenario_XuanXuanQiJing_Weiqi101_B51" />
        /// Check snapback <see cref="UnitTestProject.SpecificNeutralMoveTest.SpecificNeutralMoveTest_ScenarioHighLevel18" />
        /// </summary>
        public static GameTryMove GetSpecificNeutralMove(Game g, List<GameTryMove> neutralPointMoves)
        {
            GameTryMove gameTryMove = null;
            List<Group> killerGroups = GroupHelper.GetKillerGroups(g.Board, Content.Unknown, true);
            List<Group> immovableGroups = IsImmovableKill(g, killerGroups).ToList();
            if (immovableGroups.Any())
            {
                foreach (Group immovableGroup in immovableGroups)
                {
                    gameTryMove = SpecificKillWithImmovablePoints(g.Board, neutralPointMoves, immovableGroup);
                    if (gameTryMove != null) break;
                }
            }
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
                if (tryBoard.MoveGroupLiberties == 1) continue;
                IEnumerable<Group> targetGroups = tryBoard.GetGroupsFromStoneNeighbours(move);
                //ensure target group has two liberties and share at least one liberty with killer group
                foreach (Group group in targetGroups)
                {
                    if (groups.Contains(group)) continue;
                    groups.Add(group);
                    HashSet<Point> targetLiberties = group.Liberties;
                    if (targetLiberties.Count != 2) continue;
                    List<Point> sharedLiberties = targetLiberties.Intersect(killerLiberties).ToList();
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
            foreach (Group targetGroup in tryBoard.GetGroupsFromStoneNeighbours(neutralPointMove.Move))
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
                    List<Group> kgroup = killerGroups.Where(group => board.GetNeighbourGroups(group).Contains(board.GetCurrentGroup(targetGroup))).ToList();
                    if (kgroup.Count != 1) continue;
                    //include all empty points within killer group
                    neighbourLiberties = kgroup.First().Points.Where(p => tryBoard[p] == Content.Empty).ToList();

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
            List<Group> killerGroups = GroupHelper.GetKillerGroups(g.Board, Content.Unknown, true);
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
        /// <see cref="UnitTestProject.SurvivalTigerMouthMoveTest.SurvivalTigerMouthMoveTest_Scenario_TianLongTu_Q16827" />
        /// <see cref="UnitTestProject.SurvivalTigerMouthMoveTest.SurvivalTigerMouthMoveTest_Scenario_GuanZiPu_Q18860" />
        /// <see cref="UnitTestProject.SurvivalTigerMouthMoveTest.RedundantTigerMouthMove_Scenario_WuQingYuan_Q15126" />
        /// Check two point atari move <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WuQingYuan_Q31428" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WuQingYuan_Q31672" />
        /// Check corner three formation <see cref="UnitTestProject.SurvivalTigerMouthMoveTest.RedundantTigerMouthMove_Scenario_GuanZiPu_Q18860" />
        /// Check possible corner three formation <see cref="UnitTestProject.SurvivalTigerMouthMoveTest.RedundantTigerMouthMove_Scenario_WuQingYuan_Q31503_2" />
        /// Opponent move at tiger mouth <see cref="UnitTestProject.SurvivalTigerMouthMoveTest.RedundantTigerMouthMove_Scenario_XuanXuanGo_A151_101Weiqi" />
        /// <see cref="UnitTestProject.SurvivalTigerMouthMoveTest.SurvivalTigerMouthMoveTest_Scenario_Nie67" />
        /// Real eye at diagonal point <see cref="UnitTestProject.SurvivalTigerMouthMoveTest.RedundantTigerMouthMove_20221209_5" />
        /// Check for strong neighbour groups <see cref="UnitTestProject.SurvivalTigerMouthMoveTest.SurvivalTigerMouthMoveTest_Scenario_XuanXuanGo_A46_101Weiqi" />
        /// <see cref="UnitTestProject.SurvivalTigerMouthMoveTest.RedundantTigerMouthMove_Scenario3dan22" />
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
            Board capturedBoard = ImmovableHelper.CaptureSuicideGroup(tryBoard);
            if (capturedBoard == null || capturedBoard.MoveGroupLiberties == 1) return false;

            //check eye points at diagonals of tiger mouth
            List<Point> libertyPoint = tryBoard.GetStoneNeighbours().Where(n => tryBoard[n] != c.Opposite()).ToList();
            if (libertyPoint.Count != 1) return false;
            List<Point> diagonalPoints = TigerMouthEyePoints(tryBoard, move, libertyPoint.First()).Where(e => tryBoard[e] != c.Opposite()).ToList();
            if (diagonalPoints.Count == 0) return false;

            //check if diagonal point is tiger mouth 
            if (diagonalPoints.Any(d => ImmovableHelper.IsConfirmTigerMouth(currentBoard, tryBoard, d) != null))
                return true;

            //check two point atari move
            if (TwoPointAtariMove(tryBoard, capturedBoard)) return false;

            //check corner three and possible corner three formation
            if (KillerFormationHelper.CornerThreeFormation(tryBoard, tryBoard.MoveGroup) || KillerFormationHelper.PossibleCornerThreeFormation(currentBoard, move, c.Opposite())) return false;

            foreach (Point d in diagonalPoints)
            {
                //opponent move at tiger mouth
                if (opponentMove != null)
                {
                    if (SuicideAtBigTigerMouth(opponentMove).Item1 || BothAliveHelper.CheckForBothAliveAtMove(opponentMove.TryGame.Board))
                        continue;
                    if (ImmovableHelper.IsImmovablePoint(d, c.Opposite(), currentBoard).Item1)
                        return true;
                }

                Group diagonalKillerGroup = GroupHelper.GetKillerGroupFromCache(currentBoard, d, c.Opposite());
                if (diagonalKillerGroup == null) continue;

                //real eye at diagonal point
                if (EyeHelper.FindRealEyeOfAnyKillerGroup(tryBoard, diagonalKillerGroup))
                    return true;

                //check move and diagonal space
                if (ImmovableHelper.IsConfirmTigerMouth(currentBoard, tryBoard) == null) continue;

                if (EyeHelper.FindRealEyeWithinEmptySpace(currentBoard, diagonalKillerGroup))
                    return true;

                Group moveKillerGroup = GroupHelper.GetKillerGroupFromCache(currentBoard, move, c.Opposite());
                if (moveKillerGroup == null)
                {
                    //check for strong neighbour groups
                    if (WallHelper.StrongNeighbourGroups(currentBoard, move, c) && capturedBoard.MoveGroupLiberties > 2)
                        return true;

                    //find immovable point at diagonal
                    if (ImmovableHelper.IsImmovablePoint(d, c.Opposite(), currentBoard).Item1)
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
        /// <see cref="UnitTestProject.SurvivalTigerMouthMoveTest.RedundantTigerMouthMove_Scenario_XuanXuanGo_A26" />
        /// Check for covered eye <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_AncientJapanese_B6" />
        /// <see cref="UnitTestProject.SurvivalTigerMouthMoveTest.RedundantTigerMouthMove_Scenario_TianLongTu_Q16738" />
        /// <see cref="UnitTestProject.SurvivalTigerMouthMoveTest.RedundantTigerMouthMove_Scenario_WindAndTime_Q30225" />
        /// Check for three groups <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_GuanZiPu_Q1970" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WuQingYuan_Q30935" />
        /// Check for strong neighbour groups <see cref="UnitTestProject.SurvivalTigerMouthMoveTest.RedundantTigerMouthMove_Scenario3dan22" />
        /// <see cref="UnitTestProject.SurvivalTigerMouthMoveTest.RedundantTigerMouthMove_Scenario_TianLongTu_Q16605" />
        /// <see cref="UnitTestProject.SurvivalTigerMouthMoveTest.RedundantTigerMouthMove_Scenario_XuanXuanGo_A28" />
        /// Suicide for liberty fight <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanQiJing_A40_3" />
        /// </summary>
        private static Boolean TigerMouthWithoutDiagonalMouth(GameTryMove tryMove, Board capturedBoard)
        {
            Point move = tryMove.Move;
            Board currentBoard = tryMove.CurrentGame.Board;
            Board tryBoard = tryMove.TryGame.Board;
            Content c = tryMove.MoveContent;

            //suicide within real eye at suicidal redundant move
            if (tryBoard.MoveGroup.Points.Count == 1 && EyeHelper.FindSemiSolidEye(tryBoard.MoveGroup.Points.First(), capturedBoard).Item1)
                return false;
            //check for covered eye
            if (EyeHelper.IsCovered(tryBoard, move, c.Opposite()))
                return false;
            //check for three groups
            List<Group> neighbourGroups = tryBoard.GetGroupsFromStoneNeighbours(move);
            if (neighbourGroups.Count >= 3 && (neighbourGroups.Count(g => g.Liberties.Count <= 2) >= 2 || LinkHelper.DiagonalCutMove(tryBoard).Item1)) return false;

            //check for strong neighbour groups
            Boolean strongGroups = WallHelper.StrongNeighbourGroups(currentBoard, move, c, true) && capturedBoard.MoveGroupLiberties > 2;
            if (!strongGroups) return false;

            //suicide for liberty fight
            if (SuicideForLibertyFight(tryMove)) return false;
            return true;
        }

        /// <summary>
        /// Get eye points at the opposite diagonals of the tiger mouth.
        /// </summary>
        public static List<Point> TigerMouthEyePoints(Board board, Point tigerMouthPoint, Point libertyPoint)
        {
            List<Point> eyePoints = new List<Point>();
            Direction direction = dh.GetDirectionFromTwoPoints(libertyPoint, tigerMouthPoint).Item1;
            int rotation = dh.GetRotationIndex(direction);
            Point pointSide = dh.GetPointInDirection(board, tigerMouthPoint, direction.Opposite());
            if (!board.PointWithinBoard(pointSide))
                return eyePoints;
            Point pointSideLeft = dh.GetPointInDirection(board, pointSide, dh.GetNewDirection(Direction.Left, rotation));
            Point pointSideRight = dh.GetPointInDirection(board, pointSide, dh.GetNewDirection(Direction.Right, rotation));
            if (board.PointWithinBoard(pointSideLeft))
                eyePoints.Add(pointSideLeft);
            if (board.PointWithinBoard(pointSideRight))
                eyePoints.Add(pointSideRight);
            return eyePoints;
        }

        #endregion

        #region redundant eye diagonal
        /// <summary>
        /// Find eye diagonal moves that are redundant.
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

            //suicide moves handled by redundant tiger mouth
            if (tryBoard.MoveGroupLiberties == 1)
                return false;

            //get diagonals
            List<Point> diagonals = tryBoard.GetDiagonalNeighbours().Where(q => tryBoard[q] != c).ToList();
            diagonals = diagonals.Where(eye => LinkHelper.PointsBetweenDiagonals(eye, move).All(d => tryBoard[d] == c)).ToList();
            if (diagonals.Count == 0)
                return false;

            diagonals.RemoveAll(d => GroupHelper.GetKillerGroupFromCache(currentBoard, d, c) == null);
            if (diagonals.Count == 0) return false;
            //check diagonals are real eyes
            if (diagonals.All(eye => EyeHelper.RealEyeAtDiagonal(tryMove, eye)))
            {
                //check other surrounding points are not possible eyes
                IEnumerable<Point> neighbourPts = tryBoard.GetStoneAndDiagonalNeighbours().Except(diagonals);
                if (neighbourPts.Any(q => !WallHelper.NoEyeForSurvival(tryBoard, q)))
                    return false;

                //check link to groups other than eye groups
                if (LinkHelper.PossibleLinkForGroups(tryBoard, currentBoard))
                    return false;
                return true;
            }
            return false;
        }


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
        /// Survival eye filler moves. Get specific move for group not more than five points and not filled. 
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


            //check if any move in killer group
            if (killerGroup != null && killerGroup.Points.Count <= 5)
                return SpecificEyeFillerMove(tryMove);
            return false;
        }

        /// <summary>
        /// Kill eye filler moves valid within only small space about five points. 
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

            GameTryMove opponentMove = tryMove.MakeMoveWithOpponentAtSamePoint();
            if (opponentMove == null) return false;
            if (killerGroup != null && killerGroup.Points.Count <= 5)
                return SpecificEyeFillerMove(opponentMove, true);
            return false;
        }

        /// <summary>
        /// Filler moves without killer group. <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_WuQingYuan_Q6150" />
        /// Filler moves with killer group <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_GuanZiPu_B3" />
        /// Check for one point leap move <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_XuanXuanGo_B10_2" />
        /// <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_Corner_B40" />
        /// <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_WindAndTime_Q30278" />
        /// Check two-point group <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_Side_B35" />
        /// Check if killer group created with opposite content within the group <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_XuanXuanGo_A171_101Weiqi" />
        /// <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario3dan22" />
        /// </summary>
        private static Boolean FillerMoveWithoutKillerGroup(GameTryMove tryMove, GameTryMove opponentMove = null)
        {
            Board tryBoard = tryMove.TryGame.Board;
            Point move = tryBoard.Move.Value;
            Content c = tryMove.MoveContent;

            //check for opposite content
            if (CheckOppositeContentForFillerMove(tryMove))
                return false;

            if (!GenericEyeFillerMove(tryMove, opponentMove)) return false;

            if (WallHelper.IsNonKillableGroup(tryBoard)) return true;

            //check for one point leap move
            if (tryBoard.MoveGroup.Points.Count == 1 && !tryBoard.CornerPoint(move))
            {
                Boolean singlePoint = tryBoard.GetStoneAndDiagonalNeighbours().All(n => tryBoard[n] == Content.Empty);
                if (singlePoint)
                {
                    //check survival move
                    if (SiegedScenario(tryBoard, tryBoard.GetClosestPoints(move), 1)) return false;
                    //check kill move
                    List<Point> oppositeStones = tryBoard.GetClosestPoints(move, c.Opposite());
                    if (SiegedScenario(tryBoard, oppositeStones))
                        return false;
                }
            }

            //check two-point group
            if (tryBoard.MoveGroup.Points.Count == 2 && LinkHelper.GetGroupLinkedDiagonals(tryBoard).Count == 0)
            {
                List<Point> neighbours = tryBoard.GetClosestPoints(move).Except(tryBoard.MoveGroup.Points).ToList();
                if (SiegedScenario(tryBoard, neighbours))
                    return false;
            }

            //check if killer group created with opposite content within the group
            if (tryBoard.MoveGroup.Points.Count > 1 && tryMove.IncreasedKillerGroups)
            {
                IEnumerable<Group> createdGroups = GroupHelper.GetKillerGroups(tryBoard, c).Except(GroupHelper.GetKillerGroups(tryMove.CurrentGame.Board, c));
                if (createdGroups.Any(group => group.Points.Any(p => tryBoard[p] == group.Content)))
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Check opposite content for filler move.
        /// Ensure no opposite content at diagonal <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_WindAndTime_Q29998_2" />
        /// Ensure no opposite content at stone neighbour points for killer group <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_TianLongTu_Q15017" />
        /// Closest neighbour within killer group <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_WuQingYuan_Q31537_2" />
        /// <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_WuQingYuan_Q31445" />
        /// </summary>
        private static Boolean CheckOppositeContentForFillerMove(GameTryMove tryMove)
        {
            Board tryBoard = tryMove.TryGame.Board;
            Point move = tryBoard.Move.Value;
            Content c = tryMove.MoveContent;
            if (tryBoard.CornerPoint(move)) return false;
            //ensure no opposite content at stone and diagonal
            if (tryBoard.GetStoneAndDiagonalNeighbours().Any(n => tryBoard[n] == c.Opposite()))
                return true;
            return false;
        }

        /// <summary>
        /// At least two groups surrounded by a neighbour group each.
        /// <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_WindAndTime_Q30278" />
        /// </summary>
        private static Boolean SiegedScenario(Board tryBoard, List<Point> points, int groupCount = 2)
        {
            HashSet<Group> groups = tryBoard.GetGroupsFromPoints(points);
            return (groups.Count >= groupCount && groups.Count(group => group.Neighbours.Except(tryBoard.MoveGroup.Points).Any(n => tryBoard[n] == group.Content.Opposite())) >= groupCount);
        }

        /// <summary>
        /// Remove redundant moves that fill eyes instead of creating eyes within eye space for survival.
        /// <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_GuanZiPu_B3" />
        /// <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_GuanZiPu_B3_2" />
        /// Ensure not link for groups <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_WuQingYuan_Q5971" />
        /// Get stone neighbours only for killer group <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_TianLongTu_Q15017" />
        /// Eye created by try move <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_WuQingYuan_Q6150_2" />
        /// <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_WindAndTime_Q29378" />
        /// Check any opponent stone at stone points <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_XuanXuanGo_Q18500" />
        /// Check any opponent stone at neighbour points <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_TianLongTu_Q16827" />
        /// <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_TianLongTu_Q16827_2" />
        /// Check corner point <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_XuanXuanGo_A26" />
        /// <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_Corner_B8" />
        /// <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_XuanXuanQiJing_Weiqi101_7245" />
        /// </summary>
        public static Boolean GenericEyeFillerMove(GameTryMove tryMove, GameTryMove opponentMove = null)
        {
            Board tryBoard = tryMove.TryGame.Board;
            Board currentBoard = tryMove.CurrentGame.Board;
            Point move = tryMove.Move;
            Content c = tryMove.MoveContent;
            if (!tryMove.IsNegligible) return false;

            //ensure not link for groups
            if (EyeFillerLinkForGroups(tryMove))
                return false;
            //check for opponent ko fight
            if (EyeFillerKo(tryMove))
                return false;

            List<Point> emptyNeighbours = tryBoard.GetStoneNeighbours().Where(p => tryBoard[p] == Content.Empty).ToList();

            //eye created by try move
            if (emptyNeighbours.Any(n => EyeHelper.FindSemiSolidEye(n, tryBoard, c).Item1))
                return false;

            //count eyes created at move
            int possibleEyes = PossibleEyesCreated(currentBoard, move, c);

            foreach (Point p in emptyNeighbours)
            {
                if (tryBoard.CornerPoint(p)) continue;
                //check any opponent stone at neighbour points
                if (CheckOpponentStoneAtFillerMove(tryMove, p))
                    continue;
                //count eyes created at empty neighbour points
                int possibleEyesAtNeighbourPt = PossibleEyesCreated(currentBoard, p, c);
                //check if possibility of eyes created by move at any stone neighbour is more than at try move point
                if (possibleEyesAtNeighbourPt > possibleEyes)
                    return true;

                //check corner point
                if (tryBoard.CornerPoint(move) && possibleEyesAtNeighbourPt >= possibleEyes)
                {
                    if (tryBoard.GetStoneNeighbours(p.x, p.y).Any(n => !move.Equals(n) && !tryBoard.PointWithinMiddleArea(n) && (currentBoard[n] != Content.Empty || PossibleEyesCreated(currentBoard, n, c) > possibleEyes)))
                        return true;
                }

            }
            return false;
        }

        /// <summary>
        /// Check opponent stone at filler move. 
        /// <see cref="UnitTestProject.BaseLineKillerMoveTest.BaseLineKillerMoveTest_Scenario_TianLongTu_Q16520" />
        /// Check for opponent stone at try move <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_TianLongTu_Q16827_2" />
        /// </summary>
        private static Boolean CheckOpponentStoneAtFillerMove(GameTryMove tryMove, Point p)
        {
            Point move = tryMove.TryGame.Board.Move.Value;
            Board currentBoard = tryMove.CurrentGame.Board;
            Content c = tryMove.MoveContent;
            List<Point> stoneNeighbours = currentBoard.GetStoneNeighbours(p).Where(n => currentBoard[n] == c.Opposite()).ToList();
            if (stoneNeighbours.Count > 1 || stoneNeighbours.Any(n => currentBoard.GetGroupAt(n).Points.Count > 1))
                return true;

            //check for opponent stone at try move
            if (stoneNeighbours.Count == 1 && currentBoard.GetGroupAt(stoneNeighbours.First()).Points.Count == 1)
            {
                if (currentBoard.GetStoneNeighbours(move).Any(n => currentBoard[n] == c.Opposite()))
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
        /// Link for groups where diagonal is non killable.
        /// Opponent stones at diagonal points <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_TianLongTu_Q17077" />
        /// <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_XuanXuanGo_A82_101Weiqi" />
        /// <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_Corner_A132" />
        /// <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_WuQingYuan_Q30919" />
        /// Opponent stones at stone points <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_XuanXuanGo_B10_3" />
        /// <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_XuanXuanQiJing_Weiqi101_2282" />
        /// <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_AncientJapanese_B6" />
        /// <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_TianLongTu_Q17239" />
        /// <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_TianLongTu_Q17239" />
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
        public static Boolean SpecificEyeFillerMove(GameTryMove tryMove, Boolean isOpponent = false)
        {
            Board tryBoard = tryMove.TryGame.Board;
            Board currentBoard = tryMove.CurrentGame.Board;
            Point move = tryMove.Move;
            Content c = tryMove.MoveContent;
            if (!tryMove.IsNegligible) return false;
            Group killerGroup = GroupHelper.GetKillerGroupFromCache(currentBoard, move, c);
            if (killerGroup == null)
                return false;

            //neighbour groups should have liberty more than one
            if (AtariHelper.AtariByGroup(currentBoard, killerGroup))
                return false;

            List<Point> emptyPoints = killerGroup.Points.Where(p => currentBoard[p] == Content.Empty).ToList();

            //no neighbour group
            List<Group> neighbourGroups = GroupHelper.GetNeighbourGroupsOfKillerGroup(currentBoard, killerGroup);
            if (emptyPoints.Any(p => !currentBoard.GetGroupsFromStoneNeighbours(p, c.Opposite()).Any(gr => neighbourGroups.Contains(gr))))
                return false;

            //ensure not link for groups
            if (EyeFillerLinkForGroups(tryMove))
                return false;

            //select move that prevent survival creating eye
            Boolean eyeCreated = tryBoard.GetStoneNeighbours().Any(n => EyeHelper.FindSemiSolidEye(n, tryBoard, c).Item1);
            if (eyeCreated) return false;

            //count possible eyes created
            Dictionary<Point, int> fillerMoves = new Dictionary<Point, int>();
            emptyPoints.ForEach(p => fillerMoves.Add(p, PossibleEyesCreated(currentBoard, p, c)));
            int maxPossibleEyes = fillerMoves.Max(f => f.Value);
            List<Point> bestMoves = fillerMoves.Where(m => m.Value == maxPossibleEyes).Select(f => f.Key).ToList();

            if (bestMoves.Count == 1)
                return !tryMove.Move.Equals(bestMoves.First());
            //select move with max binding
            Dictionary<Point, Board> killBoards = new Dictionary<Point, Board>();
            foreach (Point p in bestMoves)
            {
                Board b = currentBoard.MakeMoveOnNewBoard(p, c.Opposite());
                if (b == null) continue;
                killBoards.Add(p, b);
            }
            Point bestMove = KillerFormationHelper.GetMaxBindingPoint(currentBoard, killBoards.Values).Move;
            return !tryMove.Move.Equals(bestMove);
        }

        /// <summary>
        /// Check if killer can make ko fight.
        /// Survival ko <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_GuanZiPu_A36_3" />
        /// <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_Corner_A67" />
        /// <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_WuQingYuan_Q31498" />
        /// 
        /// Killer ko <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_XuanXuanGo_B12" />
        /// <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_Corner_A67_2" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WindAndTime_Q29277" />
        /// </summary>
        public static Boolean EyeFillerKo(GameTryMove tryMove)
        {
            Board tryBoard = tryMove.TryGame.Board;
            Point move = tryBoard.Move.Value;
            Content c = tryBoard.MoveGroup.Content;

            //check to ensure is ko
            List<Point> eyePoints = tryBoard.GetStoneNeighbours().Where(n => EyeHelper.FindNonSemiSolidEye(tryBoard, n, c)).ToList();
            Boolean koEnabled = KoHelper.KoContentEnabled(c, tryMove.TryGame.GameInfo);
            if (koEnabled)
            {
                //survival ko fight
                foreach (Point eyePoint in eyePoints)
                {
                    Board b = tryBoard.MakeMoveOnNewBoard(eyePoint, c.Opposite());
                    if (b != null && KoHelper.IsKoFight(b))
                        return true;
                }
            }
            else
            {
                //killer ko
                GameTryMove opponentMove = tryMove.MakeMoveWithOpponentAtSamePoint();
                if (opponentMove == null) return false;
                Board opponentBoard = opponentMove.TryGame.Board;
                Group killerGroup = GroupHelper.GetKillerGroupFromCache(opponentBoard, move, c);
                if (killerGroup == null) return false;
                eyePoints = opponentBoard.GetStoneNeighbours().Where(n => EyeHelper.FindNonSemiSolidEye(opponentBoard, n, c.Opposite())).ToList();
                if (eyePoints.Count == 0) return false;

                Board coveredBoard = BothAliveHelper.FillEyePointsBoard(opponentBoard, killerGroup);
                //group has one or no liberties
                if (coveredBoard.GetGroupLiberties(tryBoard.MoveGroup).Count <= 1)
                    return true;
            }
            return false;
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
        /// Redundant survival ko moves <see cref="UnitTestProject.RedundantKoMoveTest.RedundantKoMoveTest_Scenario_XuanXuanGo_A46_101Weiqi" />
        /// <see cref="UnitTestProject.RedundantKoMoveTest.RedundantKoMoveTest_SimpleSeki" />
        /// Check for opponent <see cref="UnitTestProject.RedundantKoMoveTest.RedundantKoMoveTest_Scenario_WindAndTime_Q30152" />
        /// <see cref="UnitTestProject.RedundantKoMoveTest.RedundantKoMoveTest_Scenario_XuanXuanGo_B10" />
        /// </summary>
        public static Boolean RedundantSurvivalKoMove(GameTryMove tryMove)
        {
            Board tryBoard = tryMove.TryGame.Board;
            Content c = tryBoard.MoveGroup.Content;
            Boolean koEnabled = KoHelper.KoContentEnabled(c, tryBoard.GameInfo);
            if (!koEnabled)
            {
                //check pre-ko moves
                if (tryBoard.singlePointCapture == null) return false;
                return !KoHelper.PossibilityOfDoubleKo(tryMove);
            }

            //check redundant ko
            if (!tryMove.IsNegligibleForKo())
                return false;
            if (!CheckRedundantKo(tryMove)) return false;

            //check for opponent
            Point? eyePoint = KoHelper.GetKoEyePoint(tryBoard);
            if (eyePoint == null) return false;
            GameTryMove opponentMove = new GameTryMove(tryMove.TryGame);
            opponentMove.TryGame.Board.InternalMakeMove(eyePoint.Value, c.Opposite(), true);

            if (!opponentMove.IsNegligibleForKo(true))
                return false;
            if (CheckRedundantKo(opponentMove))
                return true;
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
        /// Check redundant ko. 
        /// ko fight necessary <see cref="UnitTestProject.RedundantKoMoveTest.RedundantKoMoveTest_Scenario2kyu18" /> 
        /// <see cref="UnitTestProject.RedundantKoMoveTest.RedundantKoMoveTest_Scenario_XuanXuanQiJing_Weiqi101_B74" />
        /// <see cref="UnitTestProject.RedundantKoMoveTest.RedundantKoMoveTest_Scenario_Corner_A62" /> 
        /// <see cref="UnitTestProject.RedundantKoMoveTest.RedundantKoMoveTest_Scenario_Nie20" /> 
        /// <see cref="UnitTestProject.RedundantKoMoveTest.RedundantKoMoveTest_Scenario_TianLongTu_Q2413" /> 
        /// Real eye at diagonal <see cref="UnitTestProject.RedundantKoMoveTest.RedundantKoMoveTest_Scenario_WuQingYuan_Q30982" /> 
        /// <see cref="UnitTestProject.RedundantKoMoveTest.RedundantKoMoveTest_Scenario_XuanXuanGo_A151_101Weiqi" /> 
        /// <see cref="UnitTestProject.RedundantKoMoveTest.RedundantKoMoveTest_Scenario_XuanXuanGo_A151_101Weiqi_2" /> 
        /// Suicide group ko fight <see cref="UnitTestProject.RedundantKoMoveTest.RedundantKoMoveTest_Scenario_TianLongTu_Q16693_2" /> 
        /// End game redundant ko <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_Scenario_XuanXuanQiJing_A64" />
        /// Check break link <see cref="UnitTestProject.RedundantKoMoveTest.RedundantKoMoveTest_Scenario_WindAndTime_Q30152_2" /> 
        /// <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_Scenario_WindAndTime_Q30152" /> 
        /// </summary>
        public static Boolean CheckRedundantKo(GameTryMove tryMove)
        {
            Board currentBoard = tryMove.CurrentGame.Board;
            Board tryBoard = tryMove.TryGame.Board;
            Point move = tryMove.Move;
            Content c = tryMove.MoveContent;
            Point? eyePoint = KoHelper.GetKoEyePoint(tryBoard);
            if (eyePoint == null) return false;

            //check all eye groups are non killable
            List<Group> ngroups = tryBoard.GetGroupsFromStoneNeighbours(eyePoint.Value, c.Opposite()).Where(ngroup => ngroup != tryBoard.MoveGroup).ToList();
            if (ngroups.All(n => WallHelper.IsNonKillableGroup(tryBoard, n)))
                return true;

            //check ko fight necessary
            if (!WallHelper.StrongNeighbourGroups(tryBoard, tryBoard.MoveGroup))
                return false;

            //suicide group ko fight
            if (ngroups.Any(n => GroupHelper.GetKillerGroupFromCache(tryBoard, n.Points.First(), c.Opposite()) != null && tryBoard.GetNeighbourGroups(n).Any(gr => !WallHelper.IsNonKillableGroup(tryBoard, gr) && !KoHelper.IsKoFightAtNonKillableGroup(tryBoard, gr))))
                return false;

            //check break link
            List<Point> diagonals = RedundantMoveHelper.TigerMouthEyePoints(tryBoard, eyePoint.Value, move).Where(q => tryBoard[q] != c).ToList();
            if (diagonals.Count == 0 && KoHelper.CheckBaseLineLeapLink(tryBoard, eyePoint.Value, c))
                return false;

            //if all diagonals are real eyes then redundant
            if (!diagonals.All(eye => EyeHelper.RealEyeAtDiagonal(tryMove, eye)))
                return false;

            return true;
        }

        #endregion
    }
}
