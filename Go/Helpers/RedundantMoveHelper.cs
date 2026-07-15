using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
                if (tryBoard.MoveGroupLiberties == 1 && KillerFormationHelper.SuicidalKillerFormations(tryMove))
                    return false;
                if (EyeDoubleAtariException(tryMove))
                    return false;
            }
            else
            {
                //covered eye with more than two liberties
                if (LinkHelper.GetPreviousMoveGroup(currentBoard, tryBoard).Any(n => n.Liberties.Count <= 2))
                    return false;
                //check three liberty group
                if (ImmovableHelper.CheckThreeLibertyGroupAtBigTigerMouth(tryBoard, currentBoard))
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Eye double atari exception.
        /// <see cref="UnitTestProject.DailyGoProblems.DailyGoProblems_20250326_8" /> 
        /// </summary>
        public static Boolean EyeDoubleAtariException(GameTryMove tryMove)
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
        /// </summary>
        public static Boolean RedundantCoveredEyeMove(GameTryMove tryMove)
        {
            if (FindCoveredEyeMove(tryMove))
                return true;

            //find covered eye for opponent
            if (tryMove.OpponentMove != null && FindCoveredEyeMove(tryMove.OpponentMove, tryMove))
                return true;

            return false;
        }

        /// <summary>
        /// Find covered eye move.
        /// <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_Scenario_GuanZiPu_A2Q28_101Weiqi" /> 
        /// Two-point covered eye <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_Scenario_Corner_A68" /> 
        /// Check kill opponent <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_Scenario_XuanXuanGo_A34" />
        /// Check possible links <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_Scenario_XuanXuanQiJing_Weiqi101_18497_2" />
        /// <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_Scenario_XuanXuanQiJing_Weiqi101_B74" />
        /// Check ko fight <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_Scenario_XuanXuanGo_Q18341_3" />
        /// </summary>
        public static Boolean FindCoveredEyeMove(GameTryMove tryMove, GameTryMove opponentMove = null)
        {
            Board tryBoard = tryMove.TryGame.Board;
            Board currentBoard = tryMove.CurrentGame.Board;
            Point move = tryMove.Move;
            Content c = tryMove.MoveContent;
            if (tryBoard.CapturedList.Count == 0) return false;
            if (tryMove.AtariResolved) return false;
            Group eyeGroup = null;
            Point eyePoint = new Point();
            List<Point> eyePoints = EyeHelper.FindCoveredEyeAtStoneNeighbour(tryBoard);
            if (eyePoints.Count == 1)
            {
                if (KoHelper.IsKoFight(tryBoard)) return false;
                //one-point covered eye
                eyePoint = eyePoints.First();
                eyeGroup = new Group(c.Opposite());
                eyeGroup.Points.Add(eyePoint);
                tryBoard.GetStoneNeighbours(eyePoint).ForEach(n => eyeGroup.AddNeighbour(n));
            }
            else if (tryBoard.CapturedList.Count == 1 && tryBoard.CapturedPoints.Count() == 2)
            {
                //two-point covered eye
                eyePoints = tryBoard.CapturedPoints.Where(n => EyeHelper.CoveredPointWithinTwoPointGroup(tryBoard, n, c)).ToList();
                if (eyePoints.Count == 0) return false;
                eyePoint = eyePoints.First();
                eyeGroup = tryBoard.CapturedList.First();
            }
            if (eyeGroup == null) return false;
            if (!tryBoard.IsCapturedGroup(eyeGroup)) return false;

            //check no eye for survival
            if (!WallHelper.NoEyeForSurvivalAtNeighbourPoints(tryBoard))
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

            //check ko fight
            if (currentBoard.CornerPoint(move) && KoHelper.MakeKoFightFromEyePoint(currentBoard, move, c.Opposite(), false))
                return false;

            return true;
        }

        /// <summary>
        /// Check two liberty group to capture neighbour.
        /// <see cref="UnitTestProject.CheckForRecursionTest.CheckForRecursionTest_Scenario_Corner_B41" /> 
        /// <see cref="UnitTestProject.NeutralPointMoveTest.NeutralPointMoveTest_Scenario_XuanXuanQiJing_A38" /> 
        /// Check increased killer group <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_Scenario_WindAndTime_Q30275" />
        /// <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_Scenario_Corner_A84_3" />
        /// Check suicidal move <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_Scenario_XuanXuanGo_Q18341_2" />
        /// Capture opponent groups <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_Scenario_TianLongTu_Q17154" />
        /// Check escape capture link <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_Scenario_XuanXuanGo_A26_3" />
        /// </summary>
        private static Boolean CheckTwoLibertyGroupToCaptureNeighbour(Board currentBoard, Board tryBoard, Group group)
        {
            Content c = group.Content;
            if (group.Liberties.Count != 2) return false;
            foreach (Point liberty in group.Liberties)
            {
                (Boolean suicidal, Board b) = ImmovableHelper.IsSuicidalMove(liberty, c, currentBoard);
                if (!suicidal) continue;
                //check increased killer group
                if (b != null && GroupHelper.IncreasedKillerGroups(b, currentBoard))
                    return true;
                //check suicidal move
                if (ImmovableHelper.IsSuicidalMove(tryBoard, liberty, c))
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
        /// Check possible recursion <see cref = "UnitTestProject.FillKoEyeMoveTest.FillKoEyeMoveTest_Scenario_GuanZiPu_A2Q28_101Weiqi" />
        /// </summary>
        public static Boolean FillKoEyeMove(GameTryMove tryMove)
        {
            Point move = tryMove.Move;
            Board currentBoard = tryMove.CurrentGame.Board;
            Board tryBoard = tryMove.TryGame.Board;
            Content c = tryMove.MoveContent;
            //ensure is covered eye
            if (!EyeHelper.FindCoveredEye(currentBoard, move, c)) return false;

            if (tryMove.MoveConnectAndDie)
            {
                //check for killer formation
                if (KillerFormationHelper.SuicidalKillerFormations(tryMove))
                    return false;

                //check weak group in connect and die
                if (!CheckWeakGroupInConnectAndDie(tryMove, tryMove.CaptureBoard))
                    return true;
            }

            //not ko enabled
            List<Group> eyeGroups = LinkHelper.GetPreviousMoveGroup(currentBoard, tryBoard);
            Boolean isKoFight = eyeGroups.Any(e => KoHelper.IsKoFight(currentBoard, e));
            if (isKoFight && !KoHelper.KoContentEnabled(c, tryBoard.GameInfo))
                return false;

            //ensure eye groups not suicidal
            if (eyeGroups.Any(e => e.Points.Count > 1 && e.Liberties.Count == 1))
                return false;

            //double atari
            if (AtariHelper.IsDoubleAtari(currentBoard, move, c.Opposite()))
                return false;

            if (EyeDoubleAtariException(tryMove))
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

            if (isKoFight)
            {
                //check double ko
                Board b = currentBoard.MakeMoveOnNewBoard(move, c.Opposite(), true);
                if (KoHelper.PossibilityOfDoubleKo(b, currentBoard))
                    return false;
                Board b2 = ImmovableHelper.CaptureSuicideGroup(b);
                if (b2 != null && KoHelper.PossibilityOfDoubleKo(b2, b))
                    return false;

                //check possible recursion
                if (tryBoard.LastMoves.Count >= 6)
                {
                    Point p = tryBoard.LastMoves[tryBoard.LastMoves.Count - 3];
                    if (tryBoard.GetStoneNeighbours().Any(n => n.Equals(p)))
                    {
                        if (tryBoard.LastMoves.GetRange(tryBoard.LastMoves.Count - 6, 3).Any(n => n.Equals(move)))
                            return false;
                    }
                }
            }
            return true;
        }
        #endregion

        #region atari redundant move

        /// <summary>
        /// Atari redundant move.
        /// <see cref="UnitTestProject.AtariRedundantMoveTest.AtariRedundantMoveTest_Scenario_Corner_A9_Ext" />
        /// Check increased killer group <see cref="UnitTestProject.AtariRedundantMoveTest.AtariRedundantMoveTest_Scenario_GuanZiPu_B3" />
        /// Check killer group <see cref="UnitTestProject.AtariRedundantMoveTest.AtariRedundantMoveTest_Scenario_WuQingYuan_Q31503" />
        /// Make move at the other liberty <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_Scenario_TianLongTu_Q17154" />
        /// Check killer formation <see cref="UnitTestProject.AtariRedundantMoveTest.AtariRedundantMoveTest_Scenario_Side_A23" />
        /// Check one point atari target <see cref="UnitTestProject.AtariRedundantMoveTest.AtariRedundantMoveTest_Scenario_WindAndTime_Q30225_3" />
        /// </summary>
        public static Boolean AtariRedundantMove(GameTryMove tryMove)
        {
            Board currentBoard = tryMove.CurrentGame.Board;
            Board tryBoard = tryMove.TryGame.Board;
            Point move = tryMove.Move;
            Content c = tryBoard.MoveGroup.Content;
            if (tryBoard.AtariTargets.Count != 1 || tryMove.AtariResolved || tryBoard.MoveGroupLiberties == 1 || tryMove.Captured) return false;
            Group atariTarget = tryBoard.AtariTargets.First();
            Point atariPoint = tryBoard.OpponentAtStoneNeighbour().First(n => tryBoard.GetGroupAt(n).Equals(atariTarget));

            //check increased killer group
            Boolean rc = GroupHelper.IncreasedKillerGroups(tryBoard, currentBoard);
            if (rc) return false;

            //check killer group
            Group killerGroup = GroupHelper.GetDirectKillerGroup(currentBoard, atariPoint, c);
            if (killerGroup == null || currentBoard.GetNeighbourGroups(killerGroup).Any(n => n.Liberties.Count <= 2))
                return false;

            //ensure capture secure
            if (!ImmovableHelper.CheckCaptureSecure(tryBoard, atariTarget))
                return false;

            //make move at the other liberty
            Point q = atariTarget.Liberties.First();
            (Boolean suicidal, Board board) = ImmovableHelper.IsSuicidalMove(q, c, currentBoard);
            if (suicidal)
                return false;

            //ensure capture secure
            if (!ImmovableHelper.CheckCaptureSecure(board, board.GetCurrentGroup(atariTarget)))
                return false;

            //check first point
            Boolean rc2 = GroupHelper.IncreasedKillerGroups(board, currentBoard);
            if (!rc2 && !KillerFormationHelper.IsFirstPoint(currentBoard, q, move)) return false;

            //check killer formation
            int points = currentBoard.GetGroupsFromStoneNeighbours(move, c).Sum(n => n.Points.Count);
            if (points >= 3 && KillerFormationHelper.TryKillFormation(currentBoard, c.Opposite(), new List<Point> { move }).Item1)
                return false;

            //check one point atari target
            if (atariTarget.Points.Count == 1 && KillerFormationHelper.BoxFormation(currentBoard, killerGroup))
            {
                if (ImmovableHelper.GetDiagonalsOfTigerMouth(tryBoard, move, c).Any(n => GroupHelper.CheckIfDifferentKillerGroup(tryBoard, n, atariPoint, c).Item1))
                    return false;
            }
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
            if (SuicidalMoveWithinKillerGroup(tryMove))
                return true;
            if (MoveWithinNonKillableGroup(tryMove))
                return true;

            //check opponent move
            GameTryMove opponentMove = tryMove.OpponentMove;
            if (opponentMove == null) return false;
            Board opponentBoard = opponentMove.TryGame.Board;
            if (opponentBoard.MoveGroupLiberties == 1)
            {
                Boolean singlePoint = opponentBoard.MoveGroup.Points.Count == 1;
                if (singlePoint && SinglePointSuicidalMove(opponentMove, tryMove))
                    return true;
                if (!singlePoint && MultiPointOpponentSuicidalMove(tryMove))
                    return true;
            }
            else if (opponentBoard.MoveGroupLiberties == 2)
            {
                if (OpponentSuicidalConnectAndDie(opponentMove, tryMove))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Suicidal move within killer group.
        /// <see cref="UnitTestProject.SuicidalMoveWithinKillerGroupTest.SuicidalMoveWithinKillerGroupTest_Scenario_WuQingYuan_Q30919" />
        /// Check diagonal groups <see cref="UnitTestProject.SuicidalMoveWithinKillerGroupTest.SuicidalMoveWithinKillerGroupTest_Scenario_WuQingYuan_Q31603" />
        /// Check strong groups <see cref="UnitTestProject.SuicidalMoveWithinKillerGroupTest.SuicidalMoveWithinKillerGroupTest_Scenario_GuanZiPu_A20" />
        /// Check isolated group <see cref="UnitTestProject.SuicidalMoveWithinKillerGroupTest.SuicidalMoveWithinKillerGroupTest_Scenario_WuQingYuan_Q31498" />
        /// Check previous groups <see cref="UnitTestProject.SuicidalMoveWithinKillerGroupTest.SuicidalMoveWithinKillerGroupTest_Scenario_TianLongTu_Q16444" />
        /// <see cref="UnitTestProject.SuicidalMoveWithinKillerGroupTest.SuicidalMoveWithinKillerGroupTest_Scenario_WuQingYuan_Q30934" />
        /// <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_AncientJapanese_B6" />
        /// </summary>
        private static Boolean SuicidalMoveWithinKillerGroup(GameTryMove tryMove)
        {
            Board currentBoard = tryMove.CurrentGame.Board;
            Board tryBoard = tryMove.TryGame.Board;
            Point move = tryBoard.Move.Value;
            Content c = tryBoard.MoveGroup.Content;
            if (tryMove.AtariResolved || tryMove.Captured) return false;
            if (tryBoard.GetMoveLiberties().Any()) return false;
            //check killer group
            Group killerGroup = GroupHelper.GetDirectKillerGroup(currentBoard, move, c);
            if (killerGroup == null) return false;
            //check suicidal move
            List<Point> points = killerGroup.Points.Where(n => tryBoard[n] == c.Opposite()).ToList();
            if (points.Count > 2) return false;
            List<Group> groups = tryBoard.GetGroupsFromPoints(points).ToList();
            if (groups.Count != 1) return false;
            //check diagonal groups
            if (LinkHelper.GetDiagonalGroups(tryBoard, groups.First()).Any()) return false;
            //check neighbour groups
            List<Group> ngroups = currentBoard.GetNeighbourGroups(killerGroup);
            if (ngroups.Count == 1) return true;

            //check strong groups
            if (points.Count == 2 && !WallHelper.StrongGroups(currentBoard, ngroups))
                return false;
            List<Group> previousGroups = LinkHelper.GetPreviousMoveGroup(currentBoard, tryBoard);
            if (previousGroups.Count == 1)
                return true;

            //check isolated group
            if (ngroups.Except(GroupHelper.GetNeighbourGroupsOfKillerGroup(currentBoard, killerGroup)).Any())
                return false;
            //check previous groups
            if (previousGroups.All(n => WallHelper.IsHostileGroup(currentBoard, n)) && ImmovableHelper.GetDiagonalsOfTigerMouth(currentBoard, move, c).All(n => currentBoard[n] != c.Opposite() && GroupHelper.GetDirectKillerGroup(currentBoard, n, c) == null))
                return true;
            return false;
        }

        /// <summary>
        /// Move within non killable group.
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WindAndTime_Q29961" />
        /// Check any is non killable <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WindAndTime_Q30370" />
        /// Convert to non killable groups <see cref="UnitTestProject.DailyGoProblems.DailyGoProblems_20221206_8" />
        /// </summary>
        private static Boolean MoveWithinNonKillableGroup(GameTryMove tryMove)
        {
            Board currentBoard = tryMove.CurrentGame.Board;
            Board tryBoard = tryMove.TryGame.Board;
            Point move = tryBoard.Move.Value;
            Content c = tryBoard.MoveGroup.Content;
            if (GameHelper.GetContentForSurviveOrKill(tryBoard.GameInfo, SurviveOrKill.Survive) != c) return false;

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

            //convert to non killable groups
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
                    Group kgroup = GroupHelper.GetDirectKillerGroup(b, move, c.Opposite());
                    if (kgroup != null && WallHelper.TargetWithAllNonKillableGroups(b, kgroup))
                        return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Multi point opponent suicidal move.
        /// <see cref="UnitTestProject.SurvivalTigerMouthMoveTest.RedundantTigerMouthMove_Scenario_XuanXuanGo_A26" />
        /// Check for suicide at big tiger mouth <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanGo_A55_2" />
        /// Check for both alive <see cref="UnitTestProject.SurvivalTigerMouthMoveTest.SurvivalTigerMouthMoveTest_Scenario_TianLongTu_Q16827" />
        /// Check link for groups <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_Side_B35" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WindAndTime_Q30358_3" />
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

            //check capture
            if (CaptureAtOpponentSuicidalMove(tryBoard, currentBoard))
                return false;

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
            return true;
        }

        /// <summary>
        /// Capture at opponent suicidal move.
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_TianLongTu_Q16867_2" />
        /// </summary>
        private static Boolean CaptureAtOpponentSuicidalMove(Board tryBoard, Board currentBoard)
        {
            List<Group> previousGroups = LinkHelper.GetPreviousMoveGroup(currentBoard, tryBoard);
            if (previousGroups.Count < 2) return false;
            Group atariTarget = tryBoard.AtariTargets.First();
            foreach (Group group in AtariHelper.AtariByGroup(tryBoard, atariTarget).Where(n => n.Points.Count >= 3))
            {
                if (WallHelper.TargetWithAnyNonKillableGroup(tryBoard, group)) continue;
                Board b = ImmovableHelper.CaptureSuicideGroup(currentBoard, group);
                if (previousGroups.Any(n => ImmovableHelper.CheckConnectAndDie(b, n) && !ImmovableHelper.CheckConnectAndDie(currentBoard, n)))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Ko fight at bloated eye.
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_Corner_A85" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_x_2" />
        /// </summary>
        private static Boolean KoFightAtBloatedEye(Board tryBoard, Board currentBoard)
        {
            Point move = tryBoard.Move.Value;
            Content c = tryBoard.MoveGroup.Content;
            foreach (Point d in ImmovableHelper.GetDiagonalsOfTigerMouth(tryBoard, move, c))
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
        /// Check connect and die <see cref="UnitTestProject.DailyGoProblems.DailyGoProblems_20221109_7" />
        /// <see cref="UnitTestProject.DailyGoProblems.DailyGoProblems_20221112_5" />
        /// Check four-point killer formation <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_GuanZiPu_B3_5" />
        /// Check three liberty group <see cref="UnitTestProject.MustHaveNeutralMoveTest.MustHaveNeutralMoveTest_Scenario_XuanXuanGo_A54" />
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
            if (!tryMove.MoveConnectAndDie) return false;
            Board captureBoard = tryMove.CaptureBoard;

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
            if (tryBoard.MoveGroup.Points.Count > 1 && LinkHelper.GetDiagonalGroupsWithoutCut(tryBoard, tryBoard.MoveGroup).Any())
                return false;

            if (tryBoard.GetNeighbourGroups().Count > 1)
            {
                //check diagonal cut
                if (LinkHelper.FindDiagonalCut(tryBoard).Any())
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

            //check connect and die
            HashSet<Group> opponentGroups = tryBoard.GetGroupsFromPoints(tryBoard.OpponentAtStoneAndDiagonalNeighbour());
            if (opponentGroups.Any(n => ImmovableHelper.CheckConnectAndDie(tryBoard, n) && !ImmovableHelper.CheckConnectAndDie(opponentBoard, n)))
                return false;

            //check four-point killer formation
            Group kgroup = GroupHelper.GetDirectKillerGroup(currentBoard, move, c);
            if (kgroup != null && (KillerFormationHelper.OneByThreeFormation(currentBoard, kgroup) || KillerFormationHelper.BoxFormation(currentBoard, kgroup)))
            {
                Point p = KillerFormationHelper.FirstPointInKillerGroup(currentBoard, kgroup);
                if (move.Equals(p))
                    return false;
            }

            //check three liberty group
            if (ImmovableHelper.CheckThreeLibertyGroupAtBigTigerMouth(opponentBoard, currentBoard))
                return false;
            return true;
        }

        /// <summary>
        /// Suicidal connect and die. 
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_TianLongTu_Q16738" />
        /// Check capture moves <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanGo_A75_101Weiqi" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_Corner_A113_3" />
        /// Check atari moves <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WuQingYuan_Q30986" />
        /// </summary>
        public static Boolean SuicidalConnectAndDie(GameTryMove tryMove)
        {
            Board currentBoard = tryMove.CurrentGame.Board;
            Board tryBoard = tryMove.TryGame.Board;
            Point move = tryMove.Move;
            Content c = tryMove.MoveContent;

            //check connect and die
            if (!tryMove.MoveConnectAndDie) return false;
            Board captureBoard = tryMove.CaptureBoard;

            if (LifeCheck.GetTargets(tryBoard).All(t => tryBoard.MoveGroup.Equals(t))) return false;

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

            //check non killable
            if (CheckNonKillableInConnectAndDie(tryMove, captureBoard))
                return true;

            //redundant one point move
            if (RedundantOnePointMoveInConnectAndDie(tryMove, captureBoard))
                return true;

            //redundant multi point move
            if (RedundantMultiPointMoveInConnectAndDie(tryMove, captureBoard))
                return true;

            //check real eye
            if (CheckRealEyeInSuicidalConnectAndDie(tryMove, captureBoard))
                return true;

            return false;
        }

        /// <summary>
        /// Check non killable in connect and die.
        /// Check is covered <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WuQingYuan_Q31680_3" />
        /// <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_Scenario_Corner_A84_3" />
        /// </summary>
        private static Boolean CheckNonKillableInConnectAndDie(GameTryMove tryMove, Board captureBoard)
        {
            Board tryBoard = tryMove.TryGame.Board;
            Point move = tryMove.Move;
            Content c = tryMove.MoveContent;

            if (!WallHelper.TargetWithAllNonKillableGroups(captureBoard, tryBoard.MoveGroup)) return false;
            //check is covered
            if (EyeHelper.IsCovered(captureBoard, move, c.Opposite())) return false;
            if (tryBoard.GetMoveLiberties().Any(n => EyeHelper.IsCovered(tryBoard, n, c))) return false;
            return true;
        }

        /// <summary>
        /// Redundant one point move in connect and die.
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_GuanZiPu_B3_3" />
        /// Ensure all strong neighbour groups <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanGo_A151_101Weiqi_7" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_20230603_4" />
        /// </summary>
        private static Boolean RedundantOnePointMoveInConnectAndDie(GameTryMove tryMove, Board captureBoard)
        {
            Board currentBoard = tryMove.CurrentGame.Board;
            Board tryBoard = tryMove.TryGame.Board;
            Point move = tryMove.Move;
            Content c = tryMove.MoveContent;

            if (tryBoard.MoveGroup.Points.Count != 1) return false;

            //check move next to covered point
            if (CheckMoveNextToCoveredPoint(tryMove, captureBoard))
                return true;

            //check box formation
            if (KillerFormationHelper.CheckBoxFormationSuicidalMove(tryBoard, currentBoard).Item1)
                return true;

            //check diagonal for real eye
            if (CheckDiagonalForRealEyeForSuicidalConnectAndDie(tryMove, captureBoard))
                return true;

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

            //check immovable point at diagonal
            if (CheckImmovablePointAtDiagonal(tryMove, captureBoard))
                return true;

            //check one point move diagonals
            if (CheckOnePointMoveDiagonalsInConnectAndDie(tryMove, captureBoard))
                return true;

            //check one point move without diagonals
            if (CheckOnePointMoveWithoutDiagonalsInConnectAndDie(tryMove, captureBoard))
                return true;

            return false;
        }

        /// <summary>
        /// Check move next to covered point.
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_Corner_A61" />
        /// Check neighbour group <see cref="UnitTestProject.DailyGoProblems.DailyGoProblems_20260123_7" />
        /// <see cref="UnitTestProject.DailyGoProblems.DailyGoProblems_20260322_8" />
        /// </summary>
        private static Boolean CheckMoveNextToCoveredPoint(GameTryMove tryMove, Board captureBoard)
        {
            Board tryBoard = tryMove.TryGame.Board;
            Content c = tryMove.MoveContent;
            if (!tryBoard.GetMoveLiberties().Any(p => EyeHelper.IsCovered(tryBoard, p, c.Opposite()))) return false;
            if (!GroupHelper.IsSingleGroupWithinKillerGroup(captureBoard, tryBoard.MoveGroup)) return false;
            //check neighbour group
            if (WallHelper.HostileNeighbourGroups(captureBoard, tryBoard.MoveGroup))
                return true;
            if (captureBoard.MoveGroup.Points.Count == 1 && WallHelper.HostileNeighbourGroups(tryBoard))
                return true;
            return false;
        }

        /// <summary>
        /// Check diagonal for real eye for suicidal connect and die.
        /// Check diagonal for real eye <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario2dan21_2" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanQiJing_Weiqi101_B74" />
        /// Find diagonal cut <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WuQingYuan_Q5971" />
        /// Find covered eye <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_Nie1" />
        /// </summary>
        private static Boolean CheckDiagonalForRealEyeForSuicidalConnectAndDie(GameTryMove tryMove, Board captureBoard)
        {
            Board tryBoard = tryMove.TryGame.Board;
            Content c = tryMove.MoveContent;
            //check diagonal for real eye
            if (!EyeHelper.CheckDiagonalForRealEyeOnCapture(tryBoard, captureBoard).Any()) return false;

            //find diagonal cut 
            if (LinkHelper.FindDiagonalCut(tryBoard).Any())
                return false;

            //find covered eye
            foreach (Point p in tryBoard.GetMoveLiberties())
            {
                Point? q = ImmovableHelper.FindTigerMouth(tryBoard, p, c);
                if (q == null || tryBoard[q.Value] != Content.Empty) continue;
                (Boolean suicidal, Board b) = ImmovableHelper.IsSuicidalMove(q.Value, c, tryBoard);
                if (suicidal) continue;
                if (EyeHelper.FindCoveredEye(b, p, c))
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Check immovable point at diagonal.
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_TianLongTu_Q17250_3" />
        /// Check isolated neighbour group <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_Nie137_2" />
        /// Check point next to corner <see cref="UnitTestProject.DailyGoProblems.DailyGoProblems_20260111_8" />
        /// Check three-point killer group <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_Corner_A41" />
        /// Check opponent at diagonal <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WindAndTime_Q30196" />
        /// Check diagonal group <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanGo_A66" />
        /// Check side point <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WuQingYuan_Q31510_2" />
        /// Check real eye <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_GuanZiPu_A12_2" />
        /// Without diagonal cut <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_TianLongTu_Q16483" />
        /// With diagonal cut <see cref="UnitTestProject.RedundantNonSuicidalMoveTest.RedundantNonSuicidalMoveTest_ScenarioHighLevel28" />
        /// Check killer group <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanGo_Q18474" />
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
                //check isolated neighbour group
                foreach (Point d in tryBoard.GetDiagonalNeighbours())
                {
                    if (!ImmovableHelper.IsImmovablePoint(captureBoard, d, c.Opposite())) continue;
                    Boolean rc = tryBoard.GetNeighbourGroups().Any(n => tryBoard.GetNeighbourGroups(n).Count == 1 && LinkHelper.GetDiagonalGroups(tryBoard, n).Count == 0);
                    if (!rc) continue;
                    if (!tryBoard.PointWithinMiddleArea())
                    {
                        //check point next to corner
                        if (tryBoard.IsPointNextToCorner()) continue;
                        //check three-point killer group
                        Group kgroup = GroupHelper.GetDirectKillerGroup(captureBoard, move, c.Opposite());
                        if (kgroup != null && kgroup.Points.Count == 3) continue;
                    }
                    return true;
                }
                //check opponent at diagonal
                if (!tryBoard.GetDiagonalNeighbours().Any(n => tryBoard[n] == c.Opposite())) return false;
                //check immovable point at diagonal
                if (tryBoard.GetDiagonalNeighbours().Any(n => tryBoard.PointWithinMiddleArea(n) && ImmovableHelper.IsImmovablePoint(tryBoard, n, c.Opposite())))
                    return true;

                foreach (Point d in tryBoard.GetDiagonalNeighbours())
                {
                    if (!ImmovableHelper.IsImmovablePoint(captureBoard, d, c.Opposite())) continue;
                    //check hostile neighbour group
                    if (WallHelper.HostileNeighbourGroups(captureBoard, tryBoard.MoveGroup))
                        return true;
                    //check multi-point neighbour group
                    List<Group> ngroups = tryBoard.GetNeighbourGroups();
                    if (ngroups.All(n => n.Points.Count > 1))
                    {
                        //check diagonal group
                        if (ngroups.Any(n => LinkHelper.GetDiagonalGroups(tryBoard, n).Any(s => !WallHelper.IsHostileGroup(tryBoard, s))))
                            continue;
                        //check side point
                        if (!tryBoard.PointWithinMiddleArea() && !captureBoard.PointWithinMiddleArea() && ImmovableHelper.GetDiagonalsOfTigerMouth(captureBoard, d, c.Opposite()).Count(n => !captureBoard.PointWithinMiddleArea(n)) == 2)
                            continue;
                        return true;
                    }
                }

                //check real eye
                if (EyeHelper.FindRealEyeWithinEmptySpace(captureBoard, move, c.Opposite()))
                {
                    if (tryBoard.PointWithinMiddleArea() || tryBoard.GetDiagonalNeighbours().All(n => tryBoard[n] == c.Opposite())) return true;
                    List<Group> kgroups = EyeHelper.CheckDiagonalForKillerGroupOnCapture(tryBoard, captureBoard).ToList();
                    if (!kgroups.Any() || kgroups.Any(n => n != null))
                        return true;
                }
            }
            else
            {
                //diagonal move
                if (diagonals.Count != 1) return false;
                if (EyeHelper.FindCoveredEyeAtStoneNeighbour(tryBoard).Any()) return false;
                if (ImmovableHelper.CheckConnectAndDie(currentBoard, currentBoard.GetGroupAt(diagonals.First()))) return false;
                //check immovable point at diagonal
                foreach (Point p in tryBoard.GetDiagonalNeighbours())
                {
                    if (!ImmovableHelper.IsImmovablePoint(captureBoard, p, c.Opposite())) continue;
                    //check hostile neighbour group
                    if (WallHelper.HostileNeighbourGroups(captureBoard, tryBoard.MoveGroup))
                        return true;
                    if (!tryBoard.PointWithinMiddleArea(p)) continue;
                    //no diagonal cut
                    if (LinkHelper.FindLibertyBetweenDiagonals(tryBoard, move, diagonals.First()).Any())
                        return true;
                    //with diagonal cut
                    if (tryBoard.GetGroupsFromStoneNeighbours().Any(n => n.Points.Count == 1)) continue;
                    //check killer group
                    Group kgroup = GroupHelper.GetDirectKillerGroup(captureBoard, p, c.Opposite());
                    if (kgroup != null && kgroup.Points.Count == 2)
                    {
                        if (kgroup.Points.Any(n => captureBoard[n] == c) && !EyeHelper.FindRealEyeWithinEmptySpace(captureBoard, kgroup))
                            continue;
                    }
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Check empty points at stone and diagonal.
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanGo_Q18500_3" />
        /// Check point next to corner <see cref="UnitTestProject.DailyGoProblems.DailyGoProblems_20221027_6" />
        /// Check for one neighbour group <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanQiJing_Weiqi101_B74_4" />
        /// Check connect and die <see cref="UnitTestProject.DailyGoProblems.DailyGoProblems_20260206_6" />
        /// <see cref="UnitTestProject.DailyGoProblems.DailyGoProblems_20260114_8" />
        /// <see cref="UnitTestProject.DailyGoProblems.DailyGoProblems_20260401_8" />
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
            if (tryBoard.IsPointNextToCorner())
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
                Boolean connectAndDie = ngroup.Points.Count == 2 && !WallHelper.StrongNeighbourGroups(currentBoard, ngroup);
                if (!connectAndDie)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Check one point move diagonals in connect and die.
        /// Check empty point at diagonal <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanQiJing_Weiqi101_B74_3" />
        /// Check killer formation <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_WindAndTime_Q30275" />     
        /// Ensure liberty at side <see cref="UnitTestProject.DailyGoProblems.DailyGoProblems_20260516_8" />
        /// Check diagonal move <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanQiJing_A61" />
        /// Check weak group <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanQiJing_A39" />
        /// </summary>
        private static Boolean CheckOnePointMoveDiagonalsInConnectAndDie(GameTryMove tryMove, Board captureBoard)
        {
            Board currentBoard = tryMove.CurrentGame.Board;
            Board tryBoard = tryMove.TryGame.Board;
            Point move = tryMove.Move;
            Content c = tryMove.MoveContent;
            Point? d = LinkHelper.CheckPointsBetweenDiagonalsAtMove(tryBoard);
            if (d == null) return false;

            //check killer formation
            Point p = tryBoard.GetStoneNeighbours().FirstOrDefault(n => EyeHelper.FindEye(tryBoard, n, c));
            if (!p.IsEmpty() && KillerFormationHelper.TryKillFormation(tryBoard, c, new List<Point> { p }).Item1)
                return false;

            if (tryBoard[d.Value] == c.Opposite())
                return true;

            //check empty point at diagonal
            if (tryBoard[d.Value] == Content.Empty && !WallHelper.HostileNeighbourGroups(tryBoard))
                return false;

            //ensure liberty at side
            Point q = captureBoard.GetMoveLiberties(move).First();
            if (captureBoard.PointWithinMiddleArea(q))
                return false;

            //check diagonal move
            List<Point> dpoints = captureBoard.GetDiagonalNeighbours(move).Where(n => captureBoard[n] == Content.Empty).Intersect(captureBoard.GetStoneNeighbours(q)).ToList();
            if (GameHelper.GetMoveBoards(captureBoard, dpoints, c).Any(b => !ImmovableHelper.CheckConnectAndDie(b, b.MoveGroup, false)))
                return false;

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

        /// <summary>
        /// Check one point move diagonals in connect and die.
        /// Check move diagonals <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_TianLongTu_Q17154_3" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.RedundantEyeFillerTest_Scenario_WuQingYuan_Q31445" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WuQingYuan_Q31680_3" />
        /// Check capture move <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WuQingYuan_Q31453_2" />
        /// Check no diagonal groups <see cref="UnitTestProject.DailyGoProblems.DailyGoProblems_20260111_8" />
        /// Check weak diagonal group <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_TianLongTu_Q17250_3" />
        /// Check multi-point group <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanQiJing_Weiqi101_7245_2" />
        /// Check move liberties <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanQiJing_A38_4" />
        /// Check move at diagonal <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_GuanZiPu_A37" />
        /// Check ko fight <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_Nie1" />
        /// Check for strong neighbour groups <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_TianLongTu_Q16456" />
        /// </summary>
        private static Boolean CheckOnePointMoveWithoutDiagonalsInConnectAndDie(GameTryMove tryMove, Board captureBoard)
        {
            Board currentBoard = tryMove.CurrentGame.Board;
            Board tryBoard = tryMove.TryGame.Board;
            Point move = tryMove.Move;
            Content c = tryMove.MoveContent;
            Point? d = LinkHelper.CheckPointsBetweenDiagonalsAtMove(tryBoard);
            if (d != null) return false;

            //check move diagonals
            if (LinkHelper.GetMoveDiagonals(tryBoard).Any())
            {
                if (tryBoard.GetStoneNeighbours().Any(n => EyeHelper.FindEye(tryBoard, n, c) || ImmovableHelper.FindEmptyTigerMouth(tryBoard, n, c)))
                    return false;
                if (EyeHelper.IsCovered(captureBoard, move, c.Opposite()))
                    return false;
            }

            //check capture move
            List<Group> ngroups = tryBoard.GetGroupsFromStoneNeighbours();
            if (ngroups.Any(n => n.Liberties.Count <= 2)) return false;

            if (captureBoard.MoveGroup.Points.Count == 1 && ngroups.All(n => n.Liberties.Count > 3 || n.Liberties.Count > n.Neighbours.Count * 0.5))
            {
                Point? v = LinkHelper.CheckPointsBetweenDiagonalsAtMove(captureBoard, Content.Empty);
                if (v != null && captureBoard[v.Value] == Content.Empty)
                    return true;
            }

            //check for weak groups
            foreach (Group ngroup in ngroups)
            {
                if (ngroup.Points.Count == 1)
                {
                    //check no diagonal groups
                    List<Group> diagonalGroups = LinkHelper.GetDiagonalGroups(tryBoard, ngroup);
                    if (diagonalGroups.Count == 0) continue;
                    //check weak diagonal group
                    if (diagonalGroups.Any(s => !WallHelper.IsHostileGroup(tryBoard, s) && tryBoard.GetClosestPoints(move, c.Opposite()).Any(n => s.Equals(tryBoard.GetGroupAt(n))))) continue;
                    return true;
                }
                else
                {
                    //check multi-point group
                    if (!tryBoard.GetDiagonalNeighbours().Any(n => tryBoard[n] == c.Opposite())) continue;
                    if (LinkHelper.GetGroupDiagonals(tryBoard, ngroup).Any(n => tryBoard[n.Move] == c)) continue;
                    if (tryBoard.GetNeighbourGroups(ngroup).Count(s => s != tryBoard.MoveGroup) <= 1)
                        return true;
                }
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

            //check move at diagonal
            foreach (Point p in tryBoard.GetDiagonalNeighbours().Where(n => tryBoard[n] == Content.Empty))
            {
                if (!captureBoard.GetGroupsFromStoneNeighbours(p, c).Any(s => s.Liberties.Count <= 3)) continue;

                Board b = tryBoard.MakeMoveOnNewBoard(p, c, true);
                if (b == null) continue;
                (Boolean connectAndDie, Board b2) = ImmovableHelper.ConnectAndDie(b, b.MoveGroup, false);
                if (!connectAndDie) return false;

                //check ko fight
                if (b.MoveGroup.Liberties.Count == 2)
                {
                    Point q = b2.GetCurrentGroup(b.MoveGroup).Liberties.First();
                    if (KoHelper.IsKoFight(b2, q, c).Item1 || KoHelper.MakeKoFightFromEyePoint(b2, q, c))
                        return false;
                }

                //check for strong neighbour groups
                if (WallHelper.StrongNeighbourGroups(b2, b.MoveGroup))
                    continue;
                return false;
            }
            return true;
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
        /// Check groups at covered eye <see cref="UnitTestProject.DailyGoProblems.DailyGoProblems_20260625_7" />        
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
            if (eyeGroups.Count == 1 && KillerFormationHelper.SuicidalKillerFormations(tryMove))
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

            //check groups at covered eye
            Point q = captureBoard.GetMoveLiberties().FirstOrDefault(n => EyeHelper.FindCoveredEye(captureBoard, n, c.Opposite()));
            if (!q.IsEmpty() && currentBoard.GetGroupsFromStoneNeighbours(q, c).Any(n => ImmovableHelper.CheckConnectAndDie(captureBoard, n, false) && !ImmovableHelper.CheckConnectAndDie(currentBoard, n, false) && LinkHelper.FindDiagonalCut(currentBoard, n).Any()))
                return false;

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
        /// Check real eye in suicidal connect and die.
        /// Check four-point group <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_Corner_A67_2" />
        /// </summary>
        private static Boolean CheckRealEyeInSuicidalConnectAndDie(GameTryMove tryMove, Board captureBoard)
        {
            Board currentBoard = tryMove.CurrentGame.Board;
            Board tryBoard = tryMove.TryGame.Board;
            Point move = tryBoard.Move.Value;
            Content c = tryBoard.MoveGroup.Content;
            if (tryBoard.MoveGroup.Points.Count <= 3)
            {
                //check for real eye
                if (!EyeHelper.FindRealEyeOfAnyKillerGroup(captureBoard, move, c.Opposite())) return false;
                if (!EyeHelper.CheckRealEyeInNeighbourGroups(tryBoard, captureBoard))
                    return false;
            }
            else
            {
                //check killer formation
                if (KillerFormationHelper.SuicidalKillerFormations(tryMove))
                    return false;

                //check four-point group
                if (tryBoard.MoveGroup.Points.Count == 4 && !EyeHelper.CheckRealEyeInNeighbourGroups(tryBoard, captureBoard))
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Redundant multi point move in suicidal connect and die.
        /// Check suicide for liberty fight <see cref="UnitTestProject.DailyGoProblems.DailyGoProblems_20260522_4" />
        /// Check diagonal cut <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WindAndTime_Q30064" />
        /// Check suicidal move <see cref="UnitTestProject.DailyGoProblems.DailyGoProblems_20260321_3" />
        /// Check for killer formation <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_Scenario_GuanZiPu_A4Q11_101Weiqi_2" />
        /// <see cref="UnitTestProject.NeutralPointMoveTest.NeutralPointMoveTest_Scenario_TianLongTu_Q16424" />
        /// </summary>
        private static Boolean RedundantMultiPointMoveInConnectAndDie(GameTryMove tryMove, Board captureBoard)
        {
            Board currentBoard = tryMove.CurrentGame.Board;
            Board tryBoard = tryMove.TryGame.Board;
            Point move = tryMove.Move;
            Content c = tryMove.MoveContent;
            if (tryBoard.MoveGroup.Points.Count == 1) return false;

            //check suicide for liberty fight
            if (tryBoard.GetGroupsFromStoneNeighbours().Any(n => n.Liberties.Count == 2 && n.Liberties.All(s => ImmovableHelper.IsSuicidalMoveForBothPlayers(tryBoard, s) && GroupHelper.CheckKillerGroupPoints(tryBoard, s, c.Opposite()) == null)))
                return false;

            //check diagonal and liberty at move
            if (CheckDiagonalAndLibertyAtMove(tryMove, captureBoard))
                return true;

            //check connected liberties
            Point p = tryBoard.MoveGroup.Liberties.First();
            if (tryBoard.GetStoneNeighbours(p).Any(q => tryBoard.MoveGroup.Liberties.Contains(q)))
            {
                List<Group> ngroups = tryBoard.GetGroupsFromStoneNeighbours();
                if (ngroups.Count > 1)
                {
                    //check diagonal cut
                    if (LinkHelper.FindDiagonalCut(tryBoard).Any())
                        return false;
                    //check suicidal move
                    if (ngroups.Any(n => n.Liberties.Count == 2 && n.Liberties.Any(s => ImmovableHelper.IsSuicidalMove(tryBoard, s, c.Opposite()))))
                        return false;
                }
                //check for killer formation
                if (tryBoard.MoveGroup.Points.Count >= 3 && KillerFormationHelper.SuicidalKillerFormations(tryMove))
                    return false;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Check diagonal and liberty at move.
        /// Check diagonal for real eye <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanGo_A26_4" />
        /// Check for three neighbour groups <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WindAndTime_Q30198" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_TianLongTu_Q16605" />
        /// Check killer formation <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_GuanZiPu_A17_3" />
        /// Check move diagonals <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_GuanZiPu_Q18796_2" />
        /// Check atari resolved <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanQiJing_B25" />
        /// Check one empty space left <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_Corner_A55" />
        /// Check opponent at diagonal points <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WindAndTime_Q30403_2" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WindAndTime_Q30064" />
        /// Check capture move liberty <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanQiJing_A64_2" />
        /// </summary>
        private static Boolean CheckDiagonalAndLibertyAtMove(GameTryMove tryMove, Board captureBoard)
        {
            Board currentBoard = tryMove.CurrentGame.Board;
            Board tryBoard = tryMove.TryGame.Board;
            Content c = tryMove.MoveContent;

            //check diagonal for real eye
            if (EyeHelper.CheckDiagonalForRealEyeOnCapture(tryBoard, captureBoard).Any())
            {
                Boolean rc = tryBoard.MoveGroup.Points.Count == 2 && KillerFormationHelper.ThreeOpponentGroupsAtMove(captureBoard, tryBoard.Move.Value);
                if (!rc)
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
                if (KillerFormationHelper.SuicidalKillerFormations(tryMove))
                    return false;

                //check move diagonals 
                if (LinkHelper.GetMoveDiagonals(tryBoard).Any())
                    return false;

                return true;
            }

            //check atari resolved
            if (tryMove.AtariResolved) return false;

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
            Point move = tryMove.Move;
            Board tryBoard = tryMove.TryGame.Board;
            if (!tryMove.IsNegligible)
                return false;

            //capture suicide move
            if (!tryMove.MoveConnectAndDie) return false;
            Board captureBoard = tryMove.CaptureBoard;
            if (!captureBoard.CapturedPoints.Any(n => n.Equals(move))) return false;
            if (captureBoard.CapturedPoints.Count() > 1) return true;
            if (SuicideWithinRealEye(tryMove, captureBoard))
                return true;
            if (MiscSinglePointSuicide(tryMove, captureBoard, opponentMove))
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
                        if (b.GetNeighbourGroups(tryBoard.MoveGroup).Any(n => LinkHelper.FindDiagonalCut(b, n, true).Any()))
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
            if (captureBoard.GetDiagonalNeighbours(move).Where(d => captureBoard[d] == Content.Empty).All(n => !captureBoard.OpponentAtStoneAndDiagonalNeighbour(n, c.Opposite()).Any(), true))
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
                if (WallHelper.TargetWithAnyNonKillableGroup(tryBoard) && WallHelper.StrongNeighbourGroups(capturedBoard, move, c))
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
                if (CoveredPointSuicidalWithCornerFormation(tryMove)) return false;

                if (diagonals.Any(n => LinkHelper.FindLibertyBetweenDiagonals(tryBoard, move, n).Any()))
                    return true;

                //check real eye at diagonal without opposite content
                if (!WallHelper.StrongNeighbourGroups(capturedBoard, move, c)) return false;
                foreach (Point d in EyeHelper.FindRealEyeAtDiagonal(capturedBoard, move, c.Opposite()))
                {
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
            if (!WallHelper.IsNonKillableGroup(currentBoard, p.Value))
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
        /// Check opponent at diagonal  <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_Corner_A84_2" />
        /// <see cref="UnitTestProject.KoTest.KoTest_Scenario_WuQingYuan_Q31680" />
        /// No opponent at diagonal <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_Corner_A95" />
        private static Boolean CornerPointSuicide(GameTryMove tryMove, Board captureBoard)
        {
            Board currentBoard = tryMove.CurrentGame.Board;
            Board tryBoard = tryMove.TryGame.Board;
            Point move = tryMove.Move;
            Content c = tryMove.MoveContent;
            if (!tryBoard.CornerPoint()) return false;
            if (GroupHelper.CheckKillerGroupPoints(currentBoard, move, c.Opposite()) != null) return false;

            if (!tryBoard.AtariTargets.Any())
                return true;
            Group atariTarget = tryBoard.AtariTargets.First();
            Point diagonal = tryBoard.GetDiagonalNeighbours().First();
            //no opponent at diagonal
            if (tryBoard[diagonal] == Content.Empty && captureBoard.MoveGroup.Points.Count == 1)
                return true;
            //check opponent at diagonal
            if (tryBoard[diagonal] == c)
            {
                Board b = ImmovableHelper.MakeMoveAtLiberty(tryBoard, atariTarget);
                if (b != null && b.MoveGroupLiberties == 1 && LinkHelper.IsAbsoluteLinkForGroups(tryBoard, b))
                    return false;
                return true;
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
            Point move = tryMove.Move;
            if (!tryMove.MoveConnectAndDie) return false;
            Board captureBoard = tryMove.CaptureBoard;
            if (!captureBoard.CapturedPoints.Any(n => n.Equals(move))) return false;

            //killer formations
            if (KillerFormationHelper.SuicidalKillerFormations(tryMove))
                return false;

            //check ko fight
            if (KillerFormationHelper.CheckKoFightAfterSuicidal(tryBoard, captureBoard))
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
            if (tryMove.OpponentMove != null)
                return RedundantSurvivalLeapMove(tryMove.OpponentMove, tryMove);
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

            if (KoHelper.IsKoFight(tryBoard)) return false;

            //check leap move to target
            if (CheckLeapMoveToTarget(tryBoard))
                return false;

            //check opponent groups
            List<Point> rc = tryBoard.GetClosestPoints(move, c.Opposite(), 3);
            rc = rc.Where(n => !CheckNonKillableAtDiagonalGroups(tryBoard, tryBoard.GetGroupAt(n))).ToList();
            if (rc.Count >= 3)
                return false;
            return true;
        }

        /// <summary>
        /// Check leap move to target.
        /// </summary>
        public static Boolean CheckLeapMoveToTarget(Board tryBoard, HashSet<Group> groups = null)
        {
            if (groups == null) groups = new HashSet<Group>() { tryBoard.MoveGroup };
            Group group = groups.Last();
            Content c = group.Content;

            //check if target found
            if (LifeCheck.GetTargets(tryBoard).Contains(group))
                return true;

            foreach (Point p in group.Points)
            {
                List<Point> rc = tryBoard.GetClosestPoints(p, c, 2).Where(r => tryBoard.GetGroupAt(r) != group).ToList();
                if (!rc.Any()) continue;

                foreach (Point r in rc)
                {
                    Group rgroup = tryBoard.GetGroupAt(r);
                    if (groups.Contains(rgroup)) continue;

                    //verify leap move
                    if (VerifyLeapMove(tryBoard, p, r, c))
                        continue;

                    //recursive check leap move
                    groups.Add(rgroup);
                    if (CheckLeapMoveToTarget(tryBoard, groups))
                        return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Verify leap move.
        /// Check point next to corner <see cref="UnitTestProject.LeapMoveTest.LeapMoveTest_Scenario_TianLongTu_Q14992" />
        /// Check strong neighbour groups <see cref="UnitTestProject.LeapMoveTest.LeapMoveTest_Scenario_GuanZiPu_B7" />
        /// </summary>
        public static Boolean VerifyLeapMove(Board tryBoard, Point p, Point r, Content c)
        {
            if (!tryBoard.GetClosestPoints(p, c, 2, 2).Any(n => n.Equals(r))) return false;
            List<Point> mpoints = GetMidPointsOfLeapMove(p, r).Where(n => tryBoard[n] == c.Opposite()).ToList();
            if (mpoints.Count == 0) return false;
            //check point next to corner
            if (tryBoard.IsPointNextToCorner(p)) return false;
            Group mgroup = tryBoard.GetGroupAt(mpoints.First());
            //check strong neighbour groups
            if (mgroup.Points.Count == 1 && !WallHelper.StrongNeighbourGroups(tryBoard, tryBoard.GetGroupAt(p))) return false;
            return true;
        }

        /// <summary>
        /// Check non killable at diagonal groups.
        /// </summary>
        public static Boolean CheckNonKillableAtDiagonalGroups(Board tryBoard, Group group)
        {
            if (WallHelper.IsNonKillableGroup(tryBoard, group))
                return true;

            if (LinkHelper.GetDiagonalGroupsWithoutCut(tryBoard, group).Any(n => WallHelper.IsNonKillableGroup(tryBoard, n)))
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
            if (opponentMove == null && !tryMove.IsNegligible && EssentialAtariAtCoveredPoint(tryMove))
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
            if (!tryMove.IsNegligible && EssentialAtariAtCoveredPoint(tryMove))
                return false;
            //make move from perspective of survival
            GameTryMove opponentMove = tryMove.OpponentMove;
            if (opponentMove == null) return false;

            //check neutral point
            Boolean isNeutralPoint = NeutralPointSurvivalMove(opponentMove, tryMove);
            if (isNeutralPoint)
            {
                //must have neutral point
                if (MustHaveNeutralPoint(tryMove, opponentMove))
                    return false;
            }
            else
            {
                //kill move only
                if (NeutralPointKillMoveOnly(tryMove))
                    return true;
            }
            return isNeutralPoint;
        }

        /// <summary>
        /// Neutral point kill move only.
        /// <see cref="UnitTestProject.NeutralPointMoveTest.NeutralPointMoveTest_Scenario_XuanXuanGo_A26_3" />
        /// <see cref="UnitTestProject.NeutralPointMoveTest.NeutralPointMoveTest_Scenario_XuanXuanQiJing_Weiqi101_18410" />
        /// Check opponent at diagonal neighbour <see cref="UnitTestProject.NeutralPointMoveTest.NeutralPointMoveTest_Scenario_Corner_A136" />
        /// Check middle area <see cref="UnitTestProject.NeutralPointMoveTest.NeutralPointMoveTest_Scenario_Corner_A40" />
        /// Check one empty space left <see cref="UnitTestProject.NeutralPointMoveTest.NeutralPointMoveTest_Scenario_WindAndTime_Q29264" />
        /// Check opponent at stone and diagonal neighbour <see cref="UnitTestProject.NeutralPointMoveTest.NeutralPointMoveTest_Scenario_XuanXuanGo_A82_101Weiqi" />
        /// Check connect and die <see cref="UnitTestProject.NeutralPointMoveTest.NeutralPointMoveTest_Scenario_XuanXuanGo_B31" />
        /// </summary>
        public static Boolean NeutralPointKillMoveOnly(GameTryMove tryMove)
        {
            Board tryBoard = tryMove.TryGame.Board;
            Board currentBoard = tryMove.CurrentGame.Board;
            Point move = tryBoard.Move.Value;
            Content c = tryBoard.MoveGroup.Content;
            if (!tryMove.IsNegligible) return false;
            //check no eye for survival
            if (!WallHelper.NoEyeForSurvival(currentBoard, move, c.Opposite())) return false;
            //check middle area
            if (!tryBoard.PointWithinMiddleArea())
            {
                if (tryBoard.MoveGroup.Points.Count == 1) return false;
                if (LinkHelper.IsAbsoluteLinkForGroups(currentBoard, tryBoard)) return false;

                //check opponent at stone neighbour
                if (tryBoard.GetGroupsFromStoneNeighbours().Any()) return false;
                //check opponent at diagonal neighbour
                if (tryBoard.GetDiagonalNeighbours().Any(n => tryBoard[n] == c.Opposite() && tryBoard.GetNeighbourGroups().Contains(tryBoard.GetGroupAt(n))))
                    return false;
            }
            else
            {
                //check opponent at stone neighbour
                List<Group> ngroups = tryBoard.GetGroupsFromStoneNeighbours();
                if (ngroups.Any(n => n.Liberties.Count <= n.Neighbours.Count * 0.5))
                {
                    Boolean rc = (ngroups.Count == 1 && ngroups.First().Points.Count == 1);
                    if (!rc)
                        return false;
                }
            }
            //check one empty space left
            if (KillerFormationHelper.SuicideMoveValidWithOneEmptySpaceLeft(tryBoard))
                return false;
            //check opponent at stone and diagonal neighbour
            List<Point> opponentPoints = tryBoard.OpponentAtStoneAndDiagonalNeighbour();
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
        public static Boolean CoveredPointSuicidalWithCornerFormation(GameTryMove tryMove)
        {
            Board tryBoard = tryMove.TryGame.Board;
            Point move = tryBoard.Move.Value;
            Content c = tryBoard.MoveGroup.Content;
            if (tryBoard.MoveGroupLiberties != 1 || tryBoard.MoveGroup.Points.Count != 1) return false;
            if (!tryBoard.PointWithinMiddleArea()) return false;

            if (!KillerFormationHelper.TigerMouthAtDiagonal(tryBoard)) return false;
            if (!tryMove.MoveConnectAndDie) return false;
            Board captureBoard = tryMove.CaptureBoard;
            if (captureBoard.GetMoveLiberties().Count != 1) return false;
            if (!EyeHelper.FindCoveredEye(captureBoard, move, c.Opposite())) return false;
            if (!LinkHelper.GetGroupDiagonals(captureBoard).Any(n => captureBoard[n.Move] == Content.Empty && KillerFormationHelper.CornerKillFormation(captureBoard, n.Move, c)))
                return false;
            return true;
        }

        /// <summary>
        /// Essential atari at covered point.
        /// Check ko fight <see cref="UnitTestProject.NeutralPointMoveTest.NeutralPointMoveTest_Scenario4dan17" />
        /// Check covered <see cref="UnitTestProject.NeutralPointMoveTest.NeutralPointMoveTest_Scenario_XuanXuanGo_A41_2" />
        /// Check reverse ko <see cref="UnitTestProject.BaseLineSurvivalMoveTest.BaseLineSurvivalMoveTest_Scenario_TianLongTu_Q16456" />
        /// Check double atari <see cref="UnitTestProject.NeutralPointMoveTest.NeutralPointMoveTest_Scenario_WindAndTime_Q30224" />
        /// Check opponent at liberty point <see cref="UnitTestProject.NeutralPointMoveTest.NeutralPointMoveTest_Scenario_WindAndTime_Q30199" />
        /// Check capture at liberty point <see cref="UnitTestProject.NeutralPointMoveTest.NeutralPointMoveTest_Scenario4dan17_2" />
        /// </summary>
        private static Boolean EssentialAtariAtCoveredPoint(GameTryMove tryMove)
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
            List<Point> eyePoint = EyeHelper.FindCoveredEyeAtStoneNeighbour(opponentBoard);
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
        /// Check covered eye <see cref="UnitTestProject.DailyGoProblems.DailyGoProblems_20260330_8" />
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
            if (ImmovableHelper.CheckConnectAndDie(b, b.MoveGroup, false)) return true;
            if (WallHelper.StrongNeighbourGroups(b, tigerMouth, c)) return true;

            //check one neighbour group
            List<Group> ngroups = b.GetGroupsFromStoneNeighbours();
            if (ngroups.Count != 1) return false;
            //check liberty fight
            Group ngroup = ngroups.First();
            if (tryBoard.GetNeighbourGroups(ngroup).Any(n => !WallHelper.IsNonKillableGroup(tryBoard, n) && LinkHelper.FindDiagonalCut(tryBoard, n).Any()))
                return false;

            //check covered eye
            if (ngroup.Liberties.Count == 1 && EyeHelper.FindCoveredEye(b, ngroup.Liberties.First(), c.Opposite()))
            {
                if (ImmovableHelper.UnescapableGroup(b, ngroup).Item1)
                    return false;
            }
            return true;
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
            if (LinkHelper.GetAllDiagonalConnectedGroups(board, group).Any(n => LinkHelper.FindDiagonalCut(board, n, true).Any()))
                return true;
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
                if (!tryMove.MoveConnectAndDie) return false;
                Board captureBoard = tryMove.CaptureBoard;
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
            foreach (Group ngroup in tryBoard.GetGroupsFromStoneNeighbours())
            {
                List<Group> kgroups = GroupHelper.GetKillerGroupsFromPoints(ngroup.Liberties, tryBoard, c.Opposite());
                foreach (Group kgroup in kgroups)
                {
                    if (kgroup.Liberties.Count > 3) continue;
                    List<Point> contentPoints = kgroup.Points.Where(n => tryBoard[n] == c).ToList();
                    List<Group> groups = tryBoard.GetGroupsFromPoints(contentPoints).ToList();
                    if (groups.Count != 1 || groups.First().Points.Count < 4) continue;
                    //check kill formation
                    Board b = KillerFormationHelper.DeadFormationInBothAlive(tryBoard, kgroup, 3).Item2;
                    if (b == null) continue;
                    if (tryBoard.GetStoneNeighbours(b.Move).Any(n => tryBoard[n] == c.Opposite())) continue;
                    if (KillerFormationHelper.IsKillerFormationFromFunc(tryBoard, groups.First())) continue;
                    return true;
                }
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
            neutralPointMoves.RemoveAll(n => !n.TryGame.Board.OpponentAtStoneAndDiagonalNeighbour().Any());
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
        /// Check weak group within killer group <see cref="UnitTestProject.DailyGoProblems.DailyGoProblems_20260601_8" /> 
        /// </summary>
        private static Boolean SuicideGroupNearCapture(Board board)
        {
            Point move = board.Move.Value;
            Content c = board.MoveGroup.Content;
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
            //check weak group within killer group
            if (board.CapturedPoints.Count() != 1) return false;
            Point p = board.CapturedPoints.First();
            if (!WallHelper.StrongNeighbourGroups(board, p, c.Opposite()) && GroupHelper.GetDirectKillerGroup(board, move, c.Opposite()) != null)
                return true;
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
        /// Check diagonal cut <see cref="UnitTestProject.SpecificNeutralMoveTest.SpecificNeutralMoveTest_20221017_5" />
        /// <see cref="UnitTestProject.SpecificNeutralMoveTest.SpecificNeutralMoveTest_Scenario3kyu24" />
        /// <see cref="UnitTestProject.DailyGoProblems.DailyGoProblems_20260609_5" />
        /// <see cref="UnitTestProject.DailyGoProblems.DailyGoProblems_20260614_4" />
        /// Real eye found <see cref="UnitTestProject.SpecificNeutralMoveTest.SpecificNeutralMoveTest_Scenario3kyu24" />
        /// Target group contains killer group <see cref="UnitTestProject.SpecificNeutralMoveTest.SpecificNeutralMoveTest_Scenario_TianLongTu_Q2413" />
        /// <see cref="UnitTestProject.SpecificNeutralMoveTest.SpecificNeutralMoveTest_Scenario_TianLongTu_Q16827" />
        /// Real eye found <see cref="UnitTestProject.SpecificNeutralMoveTest.SpecificNeutralMoveTest_Scenario_XuanXuanGo_B7" />
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
                //check diagonal cut
                List<(Point, List<Point>)> diagonalCuts = LinkHelper.FindDiagonalCut(tryBoard, targetGroup, true).ToList();
                foreach ((_, List<Point> diagonals) in diagonalCuts)
                {
                    Group ngroup = tryBoard.GetGroupsFromPoints(diagonals).FirstOrDefault(gr => !gr.Equals(tryBoard.MoveGroup) && !WallHelper.IsNonKillableGroup(tryBoard, gr));
                    if (ngroup == null) continue;
                    foreach (Group gr in LinkHelper.GetAllDiagonalConnectedGroups(tryBoard, ngroup))
                    {
                        List<Point> nliberties = gr.Liberties.ToList();
                        //compare liberties to see if target group can be killed
                        if (nliberties.Count == targetGroup.Liberties.Count + 1 || nliberties.Count == targetGroup.Liberties.Count + 2)
                            return neutralPointMove;
                        //real eye found
                        if (nliberties.Any(n => EyeHelper.FindRealEyeWithinEmptySpace(tryBoard, n, c)))
                            return neutralPointMove;
                    }
                }
                //no diagonal cut
                if (diagonalCuts.Count > 0) continue;
                //target group contains killer group
                List<Group> kgroups = killerGroups.Where(gr => board.GetNeighbourGroups(gr).Contains(board.GetCurrentGroup(targetGroup))).ToList();
                if (kgroups.Count != 1) continue;
                Group kgroup = kgroups.First();
                if (!kgroup.Points.Any(p => tryBoard[p] == c && tryBoard.GetGroupAt(p).Liberties.Count > 1)) continue;
                List<Point> kliberties = kgroup.Points.Where(p => tryBoard[p] == Content.Empty).ToList();

                //compare liberties to see if target group can be killed
                if (kliberties.Count == targetGroup.Liberties.Count)
                    return neutralPointMove;

                //real eye found
                if (kliberties.Any(n => EyeHelper.FindRealEyeWithinEmptySpace(tryBoard, n, c)))
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
            neutralPointMoves.ForEach(n => coveredBoard[n.Move] = c);

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
                    if (b.GetGroupsFromStoneNeighbours().Any(n => LinkHelper.FindDiagonalCut(b, n, true).Any()))
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
            if (tryMove.OpponentMove != null && RedundantTigerMouth(tryMove.OpponentMove, tryMove))
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
            if (CoveredPointSuicidalWithCornerFormation(tryMove))
                return false;

            //check one point atari move
            if (KillerFormationHelper.OnePointAtariMove(tryBoard, currentBoard))
                return false;

            //check strong groups
            if (tryBoard.GetNeighbourGroups().All(n => n.Liberties.Count > 2) && GroupHelper.CheckKillerGroupPoints(tryBoard, move, c.Opposite()) == null)
                return true;
            if (capturedBoard.GetGroupsFromStoneNeighbours(move, c).All(n => n.Liberties.Count > 3) && !EyeHelper.CoveredPointWithinTwoPointGroup(tryBoard, move, c.Opposite()))
                return true;

            //check immovable point at diagonal
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
                        if (CheckAtariAtTigerMouth(tryMove, d, capturedBoard))
                            continue;
                        //check snapback
                        if (ImmovableHelper.CheckSnapbackFromMove(tryBoard))
                            continue;
                        //check three liberty group
                        if (CheckThreeLibertyGroupAtTigerMouth(tryMove, capturedBoard))
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

                if (CheckImmovableAtTigerMouthWithoutDiagonal(tryMove, opponentMove))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Check atari at tiger mouth.
        /// Check diagonal cut <see cref="UnitTestProject.SurvivalTigerMouthMoveTest.RedundantTigerMouthMove_Scenario_Corner_A20" />
        /// Check capture group <see cref="UnitTestProject.SurvivalTigerMouthMoveTest.RedundantTigerMouthMove_Scenario_WindAndTime_Q30267" />
        /// <see cref="UnitTestProject.SurvivalTigerMouthMoveTest.RedundantTigerMouthMove_Scenario_WuQingYuan_Q31673" />
        /// Check two point killer group <see cref="UnitTestProject.SurvivalTigerMouthMoveTest.RedundantTigerMouthMove_Scenario_GuanZiPu_A4" />
        /// Check weak groups at diagonal <see cref="UnitTestProject.DailyGoProblems.DailyGoProblems_20260512_7" />
        /// Check multi point target <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WuQingYuan_Q31536" />
        /// </summary>
        private static Boolean CheckAtariAtTigerMouth(GameTryMove tryMove, Point diagonal, Board captureBoard = null)
        {
            Point move = tryMove.Move;
            Board currentBoard = tryMove.CurrentGame.Board;
            Board tryBoard = tryMove.TryGame.Board;
            Content c = tryMove.MoveContent;
            if (tryBoard.AtariTargets.Count != 1) return false;
            if (currentBoard[diagonal] != Content.Empty) return false;
            //check diagonal cut
            if (tryBoard.GetGroupsFromStoneNeighbours(diagonal, c.Opposite()).Any(n => LinkHelper.FindDiagonalCut(tryBoard, n, true).Any()))
                return true;
            //check capture group
            if (captureBoard.MoveGroup.Points.Count > 1 && captureBoard.MoveGroupLiberties == 2)
                return true;
            Group atariTarget = tryBoard.AtariTargets.First();
            Point p = atariTarget.Liberties.First();
            if (atariTarget.Points.Count == 1)
            {
                //check two point killer group
                if (GroupHelper.CheckKillerGroupPoints(tryBoard, move, c.Opposite()) != null)
                    return true;
                //check weak groups at diagonal
                if (currentBoard.GetGroupsFromStoneNeighbours(diagonal, c).Count(n => n.Liberties.Count <= 2) >= 2)
                    return true;
            }
            else
            {
                //check multi point target
                if (tryBoard.PointWithinMiddleArea(p))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Check three liberty group at tiger mouth.
        /// <see cref="UnitTestProject.DailyGoProblems.DailyGoProblems_20260708_7" />
        /// </summary>
        private static Boolean CheckThreeLibertyGroupAtTigerMouth(GameTryMove tryMove, Board captureBoard)
        {
            Point move = tryMove.Move;
            Board currentBoard = tryMove.CurrentGame.Board;
            Board tryBoard = tryMove.TryGame.Board;
            Content c = tryMove.MoveContent;
            if (captureBoard.MoveGroup.Points.Count != 1 || captureBoard.MoveGroupLiberties != 2) return false;
            List<Group> groups = captureBoard.GetGroupsFromStoneNeighbours(move, c);
            if (captureBoard.GetLibertiesOfGroups(groups).Count != 5) return false;

            //make ko fight move
            Point p = captureBoard.MoveGroup.Liberties.First(n => !n.Equals(move));
            Board b = captureBoard.MakeMoveOnNewBoard(p, c);
            if (b == null) return false;

            //fill ko eye move
            Board b2 = b.MakeMoveOnNewBoard(move, c.Opposite());
            if (b2 == null || b2.MoveGroupLiberties != 3) return false;
            foreach (Point q in b2.MoveGroup.Liberties)
            {
                //check connect and die move
                if (b2.GetMoveLiberties(q).Count() != 2) continue;
                Board b3 = b2.MakeMoveOnNewBoard(q, c);
                if (b3 == null || b3.MoveGroupLiberties != 2) continue;
                if (ImmovableHelper.CheckConnectAndDie(b3)) continue;
                if (ImmovableHelper.ConnectAndDieMove(currentBoard, q, c).Item1)
                    return true;
            }
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
            if (CheckCoveredEyeAtTigerMouth(tryMove, capturedBoard, opponentMove))
                return false;
            //check for three opponent groups
            if (CheckThreeOpponentGroupsAtTigerMouth(tryMove, capturedBoard))
                return false;
            //check weak group
            if (CheckWeakGroupAtTigerMouth(tryBoard, capturedBoard))
                return false;
            //check side move
            if (CheckSideMoveAtTigerMouth(tryMove, capturedBoard))
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
        /// Check for two empty diagonals <see cref="UnitTestProject.SurvivalTigerMouthMoveTest.RedundantTigerMouthMove_Scenario_TianLongTu_Q17250" />
        /// Check for real eye at diagonal <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_Phenomena_B6" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanQiJing_A38_3" />
        /// Check connect and die at diagonal <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_TianLongTu_Q17077" />
        /// Check for immovable point at diagonal <see cref="UnitTestProject.SurvivalTigerMouthMoveTest.RedundantTigerMouthMove_Scenario_XuanXuanGo_A171_101Weiqi" />
        /// <see cref="UnitTestProject.DailyGoProblems.DailyGoProblems_20221020_6" />
        /// Check move at diagonal <see cref="UnitTestProject.SurvivalTigerMouthMoveTest.RedundantTigerMouthMove_Scenario4dan13" />
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
            if (tryBoard.AtariTargets.Any(n => n.Points.Count > 1 && !WallHelper.NoEyeForSurvival(capturedBoard, n.Liberties.First(), c.Opposite())))
                return true;

            //check for two empty diagonals
            if (capturedBoard.GetStoneNeighbours().Intersect(capturedBoard.GetDiagonalNeighbours(move)).Count(n => capturedBoard[n] == Content.Empty) == 2)
                return false;

            //check for real eye at diagonal
            foreach (Point p in EyeHelper.FindRealEyeAtDiagonal(capturedBoard, move, c.Opposite()))
            {
                //check connect and die at diagonal
                if (tryBoard.AtariTargets.Any() && LinkHelper.GetDiagonalGroups(tryBoard).Select(n => currentBoard.GetCurrentGroup(n)).Any(n => n.Points.Count > 1 && n.Liberties.Count == 2 && ImmovableHelper.CheckConnectAndDie(currentBoard, n)))
                    continue;
                return false;
            }

            //check for immovable point at diagonal
            foreach (Point p in capturedBoard.GetDiagonalNeighbours(move))
            {
                if (capturedBoard[p] == c.Opposite()) continue;
                if (!ImmovableHelper.IsImmovablePoint(capturedBoard, p, c.Opposite())) continue;
                if (tryBoard.AtariTargets.Any()) continue;
                List<Group> kgroups = GroupHelper.GetKillerGroupsFromPoints(capturedBoard.GetMoveLiberties(), capturedBoard, c.Opposite());
                if (kgroups.Any(n => n.Points.Any(q => capturedBoard[q] == c)))
                    continue;
                return false;
            }

            //check move at diagonal
            foreach (Point d in ImmovableHelper.GetDiagonalsOfTigerMouth(tryBoard, move, c.Opposite()))
            {
                if (tryBoard[d] != Content.Empty) continue;
                if (GroupHelper.CheckKillerGroupPoints(tryBoard, move, c.Opposite()) != null) continue;
                if (tryBoard.AtariTargets.Any()) continue;
                Board b = tryBoard.MakeMoveOnNewBoard(d, c.Opposite());
                if (b != null && b.GetGroupsFromStoneNeighbours(move, c).All(n => WallHelper.IsHostileGroup(b, n)))
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Check side move at tiger mouth.
        /// No diagonal at move <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_Corner_A84" />
        /// <see cref="UnitTestProject.SurvivalTigerMouthMoveTest.RedundantTigerMouthMove_Scenario_TianLongTu_Q16827" />
        /// Check for killer group <see cref="UnitTestProject.SurvivalTigerMouthMoveTest.RedundantTigerMouthMove_20230505_8" />
        /// <see cref="UnitTestProject.SurvivalTigerMouthMoveTest.RedundantTigerMouthMove_20221220_7" />
        /// <see cref="UnitTestProject.SurvivalTigerMouthMoveTest.RedundantTigerMouthMove_Scenario_XuanXuanGo_A28" />
        /// </summary>
        private static Boolean CheckSideMoveAtTigerMouth(GameTryMove tryMove, Board capturedBoard)
        {
            Point move = tryMove.Move;
            Board currentBoard = tryMove.CurrentGame.Board;
            Board tryBoard = tryMove.TryGame.Board;
            Content c = tryMove.MoveContent;
            if (tryBoard.PointWithinMiddleArea()) return false;
            //check for real eye at diagonal
            if (EyeHelper.FindRealEyeAtDiagonal(capturedBoard, move, c.Opposite()).Any())
                return false;

            //check move at diagonal
            foreach (Point s in ImmovableHelper.GetDiagonalsOfTigerMouth(tryBoard, move, c.Opposite()))
            {
                if (tryBoard[s] != Content.Empty) continue;
                if (GroupHelper.CheckKillerGroupPoints(tryBoard, move, c.Opposite()) != null) continue;
                Board b = tryBoard.MakeMoveOnNewBoard(s, c.Opposite());
                if (b != null && b.GetGroupsFromStoneNeighbours(move, c).All(n => n.Liberties.Count > 3))
                    return false;
            }

            Point? d = LinkHelper.CheckPointsBetweenDiagonalsAtMove(tryBoard);
            if (d == null)
            {
                //no diagonal at move
                if (!tryBoard.CornerPoint() && currentBoard.GetGroupsFromStoneNeighbours(move, c).Any(n => n.Liberties.Count <= 2))
                    return true;
            }
            else
            {
                //check for killer group
                if (LinkHelper.GetMoveDiagonals(tryBoard).Any()) return false;
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
        /// Check opponent move <see cref="UnitTestProject.SurvivalTigerMouthMoveTest.RedundantTigerMouthMove_Scenario_WindAndTime_Q30225_2" />
        /// Check diagonal at move <see cref="UnitTestProject.SurvivalTigerMouthMoveTest.RedundantTigerMouthMove_Scenario_XuanXuanQiJing_B57_2" />
        /// </summary>
        private static Boolean CheckCoveredEyeAtTigerMouth(GameTryMove tryMove, Board capturedBoard, GameTryMove opponentMove = null)
        {
            Board currentBoard = tryMove.CurrentGame.Board;
            Board tryBoard = tryMove.TryGame.Board;
            Point move = tryBoard.Move.Value;
            Content c = tryBoard.MoveGroup.Content;
            //check is covered
            if (!EyeHelper.IsCovered(tryBoard, move, c.Opposite())) return false;
            //check opponent move
            if (opponentMove != null && ImmovableHelper.SuicideAtBigTigerMouth(opponentMove).Item1)
                return true;

            //check diagonal at move
            List<Point> npoints = capturedBoard.GetStoneNeighbours().Where(n => !n.Equals(move) && capturedBoard[n] != c.Opposite()).ToList();
            List<Group> killerGroups = GroupHelper.GetKillerGroupsFromPoints(npoints, capturedBoard, c.Opposite());
            if (KillerFormationHelper.TigerMouthAtDiagonal(tryBoard))
            {
                if (killerGroups.Count == 0 || killerGroups.Any(n => n.Points.Count(s => capturedBoard[s] == c) <= 1))
                    return false;
            }
            //check real eye
            if (!killerGroups.Any(n => EyeHelper.FindRealEyeWithinEmptySpace(capturedBoard, n)))
                return true;

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

        /// <summary>
        /// Check immovable at tiger mouth without diagonal.
        /// <see cref="UnitTestProject.SurvivalTigerMouthMoveTest.RedundantTigerMouthMove_Scenario_AncientJapanese_B6" />
        /// Check opponent move <see cref="UnitTestProject.SurvivalTigerMouthMoveTest.RedundantTigerMouthMove_Scenario_WindAndTime_Q30225_2" />
        /// </summary>
        private static Boolean CheckImmovableAtTigerMouthWithoutDiagonal(GameTryMove tryMove, GameTryMove opponentMove = null)
        {
            Board currentBoard = tryMove.CurrentGame.Board;
            Board tryBoard = tryMove.TryGame.Board;
            Content c = tryMove.MoveContent;
            //check immovable point at diagonal
            if (!tryBoard.GetDiagonalNeighbours().Any(n => currentBoard.PointWithinMiddleArea(n) && ImmovableHelper.IsImmovablePoint(currentBoard, n, c.Opposite())))
                return false;
            //check opponent move
            if (opponentMove != null && ImmovableHelper.SuicideAtBigTigerMouth(opponentMove).Item1)
                return false;
            return true;
        }

        #endregion

        #region redundant eye diagonal
        /// <summary>
        /// Survival eye diagonal move.
        /// <see cref="UnitTestProject.RedundantEyeDiagonalMoveTest.RedundantEyeDiagonalMoveTest_x" />
        /// Check real eye at all diagonals <see cref="UnitTestProject.RedundantEyeDiagonalMoveTest.RedundantEyeDiagonalMoveTest_Scenario_SiHuoDaQuan_CornerA29_2" />
        /// <see cref="UnitTestProject.ImmovableTest.ImmovableTest_Scenario_XuanXuanGo_B31" />
        /// Check link to groups <see cref="UnitTestProject.RedundantEyeDiagonalMoveTest.RedundantEyeDiagonalMoveTest_Scenario_WuQingYuan_Q31154" />
        /// </summary>
        public static Boolean SurvivalEyeDiagonalMove(GameTryMove tryMove, GameTryMove opponentMove = null)
        {
            Board currentBoard = tryMove.CurrentGame.Board;
            Board tryBoard = tryMove.TryGame.Board;
            Point move = tryMove.Move;
            Content c = GameHelper.GetContentForSurviveOrKill(tryBoard.GameInfo, SurviveOrKill.Survive);
            if (!tryMove.IsNegligible) return false;

            //get diagonals of killer groups
            List<Point> diagonals = tryBoard.GetDiagonalNeighbours().Where(q => tryBoard[q] != c).ToList();
            diagonals = diagonals.Where(eye => LinkHelper.PointsBetweenDiagonals(eye, move).All(d => tryBoard[d] == c)).ToList();
            if (diagonals.Count == 0) return false;
            diagonals.RemoveAll(d => GroupHelper.GetDirectKillerGroup(currentBoard, d, c) == null);

            //check other surrounding points are not possible eyes
            IEnumerable<Point> neighbourPts = tryBoard.GetStoneAndDiagonalNeighbours().Except(diagonals);
            if (neighbourPts.Any(q => !WallHelper.NoEyeForSurvival(tryBoard, q, c)))
                return false;

            //make opponent move
            if (opponentMove == null) opponentMove = tryMove.OpponentMove;
            if (opponentMove == null) return false;

            //check real eye at all diagonals
            Board opponentBoard = opponentMove.TryGame.Board;
            if (!diagonals.All(eye => EyeHelper.FindRealEyeWithinEmptySpace(opponentBoard, eye, c)))
                return false;

            //check link to groups other than eye groups
            if (LinkHelper.PossibleLinkForGroups(tryBoard, currentBoard))
                return false;

            //check covered point suicidal
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
            if (tryMove.OpponentMove != null)
                return SurvivalEyeDiagonalMove(tryMove.OpponentMove, tryMove);
            return false;
        }
        #endregion

        #region redundant ko
        /// <summary>
        /// Redundant ko move.
        /// <see cref="UnitTestProject.RedundantKoMoveTest.RedundantKoMoveTest_Scenario_XuanXuanGo_A46_101Weiqi" />
        /// <see cref="UnitTestProject.RedundantKoMoveTest.RedundantKoMoveTest_SimpleSeki" />
        /// Check redundant ko for opponent <see cref="UnitTestProject.RedundantKoMoveTest.RedundantKoMoveTest_Scenario_XuanXuanGo_A151_101Weiqi_3" />
        /// Check end game ko <see cref="UnitTestProject.FillKoEyeMoveTest.FillKoEyeMoveTest_Scenario_GuanZiPu_A2Q28_101Weiqi" />
        /// </summary>
        public static Boolean RedundantKoMove(GameTryMove tryMove)
        {
            Board tryBoard = tryMove.TryGame.Board;
            Board currentBoard = tryMove.CurrentGame.Board;
            Content c = tryBoard.MoveGroup.Content;
            if (KoHelper.IsKoFight(tryBoard))
            {
                if (!KoHelper.KoContentEnabled(c, tryBoard.GameInfo))
                {
                    //check pre-ko moves
                    if (tryBoard.KoCapture == null) return false;
                    //check double ko
                    if (!KoHelper.PossibilityOfDoubleKo(tryBoard, currentBoard))
                        return true;
                    return false;
                }
                //check redundant ko
                if (CheckRedundantKoMove(tryBoard, currentBoard))
                    return true;
            }

            //check redundant ko for opponent
            if (tryMove.OpponentMove == null) return false;
            Board opponentBoard = tryMove.OpponentMove.TryGame.Board;
            if (KoHelper.IsKoFight(opponentBoard) && CheckRedundantKoMove(opponentBoard, currentBoard))
            {
                //check end game ko
                Point? eyePoint = KoHelper.GetKoEyePoint(opponentBoard);
                List<Group> ngroups = currentBoard.GetGroupsFromStoneNeighbours(eyePoint.Value, c);
                ngroups = LinkHelper.GetAllDiagonalGroups(currentBoard, ngroups.First()).ToList();
                if (!ngroups.Any(n => KoHelper.IsKoFight(currentBoard, n)))
                    return true;
            }
            return false;
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

            //check kill ko at non killable group
            if (KoHelper.IsNonKillableGroupKoFight(tryBoard))
            {
                List<Group> ngroups = tryBoard.GetGroupsFromStoneNeighbours();
                if (ngroups.All(t => WallHelper.TargetWithAllNonKillableGroups(tryBoard, t)))
                    return true;
                //check resolve atari
                if (Board.ResolveAtari(currentBoard, tryBoard))
                    return false;
                //check strong neighbour groups
                if (!WallHelper.StrongNeighbourGroups(tryBoard))
                    return false;
                //check liberty fight
                if (CheckLibertyFightAtCoveredEye(tryBoard, move, c.Opposite()))
                    return false;
                return true;
            }

            //target with all non killable groups
            if (!WallHelper.TargetWithAllNonKillableGroups(tryBoard))
                return false;

            //check survival ko at non killable group
            if (CheckSurvivalKoAtNonKillableGroup(tryBoard, currentBoard))
                return true;

            return false;
        }

        /// <summary>
        /// Check survival ko at non killable group. 
        /// <see cref="UnitTestProject.RedundantKoMoveTest.RedundantKoMoveTest_Scenario_XuanXuanGo_A151_101Weiqi_3" /> 
        /// Check covered eye <see cref="UnitTestProject.RedundantKoMoveTest.RedundantKoMoveTest_Scenario_XuanXuanGo_A151_101Weiqi" /> 
        /// Check both alive <see cref="UnitTestProject.RedundantKoMoveTest.RedundantKoMoveTest_Scenario_TianLongTu_Q17081" /> 
        /// </summary>
        public static Boolean CheckSurvivalKoAtNonKillableGroup(Board tryBoard, Board currentBoard)
        {
            Content c = tryBoard.MoveGroup.Content;
            //check link for groups
            if (!LinkHelper.PossibleLinkForGroups(tryBoard, currentBoard))
                return true;
            //check ko eye groups
            Point? eyePoint = KoHelper.GetKoEyePoint(tryBoard);
            if (currentBoard.GetGroupsFromStoneNeighbours(eyePoint.Value, c.Opposite()).Count != 2)
                return false;
            //check resolve atari
            if (Board.ResolveAtari(currentBoard, tryBoard))
                return false;
            //check covered eye
            List<Point> diagonals = ImmovableHelper.GetDiagonalsOfTigerMouth(currentBoard, eyePoint.Value, c);
            Point p = diagonals.FirstOrDefault(n => currentBoard[n] == Content.Empty);
            if (p.IsEmpty()) return false;
            if (EyeHelper.FindCoveredEye(currentBoard, p, c))
                return false;
            //check both alive
            Group killerGroup = GroupHelper.GetDirectKillerGroup(tryBoard, p, c);
            if (killerGroup != null)
            {
                List<Point> contentPoints = killerGroup.Points.Where(n => tryBoard[n] == c.Opposite()).ToList();
                if (contentPoints.Count > 1 && tryBoard.GetGroupsFromPoints(contentPoints).All(n => n.Liberties.Count > 1))
                    return false;
            }
            return true;
        }
        #endregion

        #region nonsuicidal move

        /// <summary>
        /// Redundant non suicidal move.
        /// </summary>
        public static Boolean RedundantNonSuicidalMove(GameTryMove tryMove)
        {
            //non suicidal move
            if (RedundantNonSuicidal(tryMove))
                return true;

            //non suicidal move for opponent
            if (tryMove.OpponentMove != null && RedundantNonSuicidal(tryMove.OpponentMove, tryMove))
                return true;
            return false;
        }

        /// <summary>
        /// Redundant non suicidal move.
        /// <see cref="UnitTestProject.RedundantNonSuicidalMoveTest.RedundantNonSuicidalMoveTest_Scenario_XuanXuanGo_A23" /> 
        /// <see cref="UnitTestProject.RedundantNonSuicidalMoveTest.RedundantNonSuicidalMoveTest_Scenario_Corner_A84" /> 
        /// Check neighbour groups <see cref="UnitTestProject.RedundantNonSuicidalMoveTest.RedundantNonSuicidalMoveTest_Scenario_WindAndTime_Q30064" />
        /// Check diagonal group of neighbour group <see cref="UnitTestProject.DailyGoProblems.DailyGoProblems_20260528_7" />
        /// <see cref="UnitTestProject.RedundantNonSuicidalMoveTest.RedundantNonSuicidalMoveTest_Scenario_XuanXuanGo_A151_101Weiqi" />
        /// <see cref="UnitTestProject.RedundantNonSuicidalMoveTest.RedundantNonSuicidalMoveTest_Scenario_WindAndTime_Q30403" />
        /// Check opponent move <see cref="UnitTestProject.RedundantNonSuicidalMoveTest.RedundantNonSuicidalMoveTest_Scenario_XuanXuanGo_A26" />
        /// </summary>
        public static Boolean RedundantNonSuicidal(GameTryMove tryMove, GameTryMove opponentMove = null)
        {
            Board tryBoard = tryMove.TryGame.Board;
            Point move = tryMove.Move;
            Content c = tryBoard.MoveGroup.Content;
            if (tryBoard.MoveGroup.Points.Count != 1) return false;
            if (LinkHelper.GetDiagonalGroups(tryBoard).Any()) return false;
            if (!tryBoard.PointWithinMiddleArea() && tryBoard.GetStoneNeighbours().Any(n => ImmovableHelper.FindEmptyTigerMouth(tryBoard, n, c))) return false;
            if (KillerFormationHelper.SuicideMoveValidWithOneEmptySpaceLeft(tryBoard)) return false;

            List<Group> ngroups = tryBoard.GetGroupsFromStoneNeighbours();
            if (ngroups.Count != 1) return false;
            Group ngroup = ngroups.First();
            if (ngroup.Points.Count == 1) return false;

            //check immovable at diagonal
            if (CheckImmovableAtDiagonalForNonSuicidalMove(tryBoard, ngroup))
                return true;

            if (tryBoard.MoveGroup.Liberties.Count != 3)
                return false;

            //check liberties of neighbour groups
            if (ngroup.Liberties.Count < ngroup.Neighbours.Count * 0.5)
                return false;
            if (ngroup.Points.Count == 2 && ngroup.Liberties.Count <= ngroup.Neighbours.Count * 0.5)
                return false;

            //check neighbour groups
            List<Point> npoints = tryBoard.OpponentAtStoneAndDiagonalNeighbour();
            if (tryBoard.GetGroupsFromPoints(npoints).Count != 1) return false;

            //check diagonal group of neighbour group
            foreach (Group group in LinkHelper.GetDiagonalGroups(tryBoard, ngroup))
            {
                if (group.Points.Count == 1 && !tryBoard.PointWithinMiddleArea(group.Points.First())) continue;
                if (!WallHelper.IsHostileGroup(tryBoard, group))
                    return false;
            }

            //check opponent move
            if (opponentMove != null)
            {
                if (ngroup.Points.Count == 2 && tryBoard.GetNeighbourGroups(ngroup).Count > 1 && tryBoard.GetDiagonalNeighbours().All(n => tryBoard[n] == Content.Empty))
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Check immovable at diagonal for non suicidal move.
        /// <see cref="UnitTestProject.RedundantNonSuicidalMoveTest.RedundantNonSuicidalMoveTest_Scenario_TianLongTu_Q16738" /> 
        /// Check immovable point <see cref="UnitTestProject.DailyGoProblems.DailyGoProblems_20260515_8" /> 
        /// Check neighbour group with two liberties <see cref="UnitTestProject.RedundantNonSuicidalMoveTest.RedundantNonSuicidalMoveTest_Scenario_TianLongTu_Q15618" /> 
        /// </summary>
        public static Boolean CheckImmovableAtDiagonalForNonSuicidalMove(Board tryBoard, Group ngroup)
        {
            Point move = tryBoard.Move.Value;
            Content c = tryBoard.MoveGroup.Content;
            Point immovablePoint = tryBoard.GetDiagonalNeighbours().FirstOrDefault(n => ImmovableHelper.IsImmovablePoint(tryBoard, n, c.Opposite()));
            if (immovablePoint.IsEmpty()) return false;
            if (tryBoard.PointWithinMiddleArea(immovablePoint))
            {
                if (ngroup.Liberties.Count > 2)
                {
                    //check immovable point
                    if (!tryBoard.PointWithinMiddleArea() || !tryBoard.PointWithinMiddleArea(immovablePoint)) return true;
                    Point p = tryBoard.GetDiagonalNeighbours(immovablePoint).First(n => n.x != move.x && n.y != move.y);
                    if (tryBoard[p] == c)
                        return false;
                    if (EyeHelper.FindEye(tryBoard, p, c.Opposite()))
                        return false;
                    return true;
                }
                if (ngroup.Liberties.Count == 2)
                {
                    //check neighbour group with two liberties
                    Point p = ngroup.Liberties.First(n => !n.Equals(immovablePoint));
                    Board b = tryBoard.MakeMoveOnNewBoard(p, c.Opposite());
                    if (b != null && WallHelper.IsHostileGroup(b))
                        return true;
                }
            }
            if (ngroup.Liberties.Count < ngroup.Neighbours.Count * 0.5)
                return false;

            if (!tryBoard.PointWithinMiddleArea(immovablePoint))
                return true;

            return false;
        }

        #endregion

        #region filler move

        /// <summary>
        /// Redundant filler move.
        /// <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_TianLongTu_Q17132" /> 
        /// <see cref="UnitTestProject.DailyGoProblems.DailyGoProblems_20250311_8" /> 
        /// Not redundant <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_XuanXuanGo_B10_2" />
        /// Check weak group <see cref="UnitTestProject.DailyGoProblems.DailyGoProblems_20260223_8" />
        /// Check edge points <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_TianLongTu_Q17132" />
        /// Check diagonal cut <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_XuanXuanGo_A171_101Weiqi" />
        /// <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario3dan22" />
        /// Check diagonal of corner point <see cref="UnitTestProject.DailyGoProblems.DailyGoProblems_20260403_8" />
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
            List<KeyValuePair<Point, int>> npossibleSpace = tryBoard.GetMoveLiberties().Select(n => new KeyValuePair<Point, int>(n, PossibleSpace(currentBoard, n, c))).ToList();
            if (npossibleSpace.Any(n => n.Value < possibleSpace))
                return false;

            //check weak group
            List<Group> groups = tryBoard.GetClosestPoints(move, c, 2).Select(n => tryBoard.GetGroupAt(n)).ToList();
            if (groups.Any(n => n.Liberties.Count <= 2 && n.Points.Count >= 2 && AtariHelper.AtariByGroup(tryBoard, n).Any()))
                return false;

            //check edge points
            if (!tryBoard.CornerPoint(move) && !tryBoard.PointWithinMiddleArea(move) && npossibleSpace.Any(n => n.Value >= possibleSpace))
            {
                if (!tryBoard.GetClosestPoints(move, c.Opposite(), 2).Any() && !tryMove.IncreasedKillerGroups)
                    return true;
            }

            //check all points
            if (npossibleSpace.Any(n => n.Value > possibleSpace))
            {
                //check diagonal cut
                if (LinkHelper.FindDiagonalCut(tryBoard).Any())
                    return false;
                //check diagonal of corner point
                if (tryBoard.GetDiagonalNeighbours().Any(n => tryBoard.CornerPoint(n) && npossibleSpace.Any(s => s.Value > possibleSpace && tryBoard.PointWithinMiddleArea(s.Key))))
                    return false;
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
            if (tryBoard.OpponentAtStoneNeighbour().Any())
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
                    if (diagonals.Count() == 2 && LinkHelper.FindLibertyBetweenDiagonals(tryBoard, diagonals[0], diagonals[1]).Any())
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
