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

            if (!EyeHelper.FindEye(currentBoard, move.x, move.y, c)) return false;
            //find uncovered eye
            if (EyeHelper.FindUncoveredPoint(currentBoard, move.x, move.y, c))
            {
                if (tryBoard.MoveGroupLiberties != 1) return true;
                Board capturedBoard = ImmovableHelper.CaptureSuicideGroup(tryBoard);
                //check for killer formations
                if (KillerFormationHelper.SuicidalKillerFormations(tryBoard, currentBoard, capturedBoard))
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

        #region find covered eye move
        /// <summary>
        /// Remove all covered eye moves for survival.
        /// Ensure all groups have liberty more than two <see cref="UnitTestProject.CheckForRecursionTest.CheckForRecursionTest_Scenario_Corner_B41" /> 
        /// Check if link for groups <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_Scenario_XuanXuanQiJing_Weiqi101_18497" /> 
        /// Check if eye point is link for groups <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_Scenario_XuanXuanQiJing_Weiqi101_18497_2" /> 
        /// </summary>
        public static Boolean FindCoveredEyeMove(GameTryMove tryMove)
        {
            Board tryBoard = tryMove.TryGame.Board;
            Board currentBoard = tryMove.CurrentGame.Board;
            Content c = tryMove.MoveContent;

            if (tryBoard.AtariResolved || tryBoard.IsAtariMove) return false;
            //find eye
            List<Point> eyePoints = tryBoard.GetStoneNeighbours().Where(n => EyeHelper.FindEye(tryBoard, n.x, n.y, c)).ToList();
            if (eyePoints.Count != 1) return false;
            Point q = eyePoints.First();

            //find covered eye where covered point is non killable
            if (!RedundantCoveredEye(currentBoard, tryBoard, q, c)) return false;

            //if groups have liberty less or equals to two and moves at liberties are suicidal then not redundant
            foreach (Group group in currentBoard.GetGroupsFromStoneNeighbours(q, c.Opposite()))
            {
                if (group.Liberties.Count <= 2 && group.Liberties.Any(x => ImmovableHelper.IsSuicidalMove(currentBoard, x, c)))
                    return false;
            }

            //check if link for groups
            if (tryMove.LinkForGroups())
                return false;
            //check if eye point is link for groups
            Board b = new Board(currentBoard);
            b[q] = c;
            b.Move = q;
            if (LinkHelper.LinkForGroups(b, currentBoard))
                return false;
            return true;
        }

        /// <summary>
        /// Covered eye where covered point is non killable.
        /// Check for eye at diagonal <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_Scenario_WindAndTime_Q29277" /> 
        /// </summary>
        public static Boolean RedundantCoveredEye(Board currentBoard, Board tryBoard, Point p, Content c)
        {
            if (!EyeHelper.FindCoveredEye(tryBoard, p, c)) return false;
            List<Point> diagonals = tryBoard.GetDiagonalNeighbours(p.x, p.y);
            //check for eye at diagonal
            List<Point> emptyDiagonals = diagonals.Where(q => tryBoard[q] == Content.Empty).ToList();
            if (emptyDiagonals.Count > 0 && emptyDiagonals.All(q => EyeHelper.FindNonSemiSolidEye(currentBoard, q, c))) return false;

            IEnumerable<Point> adjacentDiagonals = tryBoard.GetStoneNeighbours().Intersect(diagonals);
            if (adjacentDiagonals.All(n => WallHelper.IsNonKillableGroup(tryBoard, n)))
                return true;
            return false;
        }
        #endregion

        #region fill ko eye move
        /// <summary>
        /// Fill ko eye move. <see cref="UnitTestProject.FillKoEyeMoveTest.FillKoEyeMoveTest_Scenario_XuanXuanGo_A46_101Weiqi" />
        /// Check for atari at ko point <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanQiJing_Weiqi101_B74" />
        /// Check for weak eye group <see cref="UnitTestProject.FillKoEyeMoveTest.FillKoEyeMoveTest_Scenario_Corner_B28" />
        /// Ignore if connect more than two groups <see cref="UnitTestProject.FillKoEyeMoveTest.FillKoEyeMoveTest_Scenario_TianLongTu_Q17132" /> 
        /// Ensure group more than one point have more than one liberty <see cref="UnitTestProject.FillKoEyeMoveTest.FillKoEyeMoveTest_Scenario_Nie20" /> 
        /// Check for killer formation <see cref="UnitTestProject.FillKoEyeMoveTest.FillKoEyeMoveTest_Scenario_Corner_A67" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_Nie20" />
        /// </summary>
        public static Boolean FillKoEyeMove(GameTryMove tryMove)
        {
            Point move = tryMove.Move;
            Board currentBoard = tryMove.CurrentGame.Board;
            Board tryBoard = tryMove.TryGame.Board;
            Content c = tryMove.MoveContent;
            //ensure is fill eye
            if (!EyeHelper.FindEye(currentBoard, tryMove.Move, tryMove.MoveContent)) return false;

            List<Group> eyeGroups = currentBoard.GetGroupsFromStoneNeighbours(move, c.Opposite()).ToList();
            List<Group> targetGroups = eyeGroups.Where(group => group.Liberties.Count == 1).ToList();
            //check for atari at ko point
            if (targetGroups.Count == 1)
            {
                Group targetGroup = targetGroups.FirstOrDefault(t => t.Points.Count == 1 && AtariHelper.AtariByGroup(currentBoard, t));
                if (targetGroup != null)
                {
                    //check for weak eye group
                    Boolean weakEyeGroup = eyeGroups.Any(e => e.Points.Count == 1 && e.Liberties.Count <= 2 && tryBoard.GetNeighbourGroups(e).All(g => g.Liberties.Count > 1));
                    if (!weakEyeGroup)
                        return true;
                }
            }

            if (!KoHelper.KoContentEnabled(c, tryBoard.GameInfo)) return false;
            //ignore if connect more than two groups
            List<Group> groups = LinkHelper.GetPreviousMoveGroup(currentBoard, tryBoard);
            if (groups.Count > 2 && eyeGroups.Any(e => e.Liberties.Count <= 2)) return false;

            //check suicide at tiger mouth for covered eye
            if (groups.Count == 2 && eyeGroups.All(e => e.Liberties.Count > 1))
            {
                if (tryBoard.MoveGroupLiberties == 2 && ImmovableHelper.CheckConnectAndDie(tryBoard, tryBoard.MoveGroup)) return true;
                if (SuicideAtBigTigerMouth(tryMove, eyeGroups).Item1)
                    return false;
            }

            //ensure group more than one point have more than one liberty
            if (eyeGroups.Any(e => e.Points.Count > 1 && e.Liberties.Count == 1)) return false;

            //check for killer formation
            if (tryBoard.GetNeighbourGroups(tryBoard.MoveGroup).All(group => !WallHelper.IsNonKillableGroup(tryBoard, group)))
            {
                Board capturedBoard = ImmovableHelper.CaptureSuicideGroup(tryBoard);
                if (KillerFormationHelper.SuicidalKillerFormations(tryBoard, currentBoard, capturedBoard))
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Suicide at big tiger mouth.
        /// <see cref="UnitTestProject.FillKoEyeMoveTest.FillKoEyeMoveTest_Scenario_GuanZiPu_B3" /> 
        /// <see cref="UnitTestProject.FillKoEyeMoveTest.FillKoEyeMoveTest_Scenario_Corner_A85" /> 
        /// </summary>
        private static (Boolean, Point?) SuicideAtBigTigerMouth(GameTryMove tryMove, List<Group> eyeGroups)
        {
            Point move = tryMove.Move;
            Board tryBoard = tryMove.TryGame.Board;
            Board currentBoard = tryMove.CurrentGame.Board;
            List<Group> suicidalEyeGroups = eyeGroups.Where(e => e.Liberties.Count == 2 && tryBoard.GetNeighbourGroups(e).All(g => !WallHelper.IsNonKillableGroup(tryBoard, g))).ToList();
            foreach (Group eyeGroup in suicidalEyeGroups)
            {
                List<Point> liberties = eyeGroup.Liberties.Except(new List<Point> { move }).ToList();
                if (liberties.Count != 1) continue;
                (Boolean suicide, Board b) = ImmovableHelper.IsSuicidalMove(liberties.First(), eyeGroup.Content, currentBoard);
                if (suicide)
                    return (true, liberties.First());
                if (ImmovableHelper.CheckConnectAndDie(b, b.MoveGroup))
                    return (true, liberties.First());
                //check for opponent capture move
                if (b != null && b.MoveGroup.Liberties.Count == 2)
                {
                    List<Point> moveGroupLiberties = b.MoveGroup.Liberties.Except(new List<Point>() { move }).ToList();
                    Board b2 = b.MakeMoveOnNewBoard(moveGroupLiberties.First(), eyeGroup.Content.Opposite());
                    if (b2 != null && b2.CapturedList.Count > 0)
                        return (true, liberties.First());
                }
            }
            return (false, null);
        }
        #endregion

        #region atari response move

        /// <summary>
        /// Redundant atari move that allows target group to escape.
        /// <see cref="UnitTestProject.AtariRedundantMoveTest.AtariRedundantMoveTest_Scenario_XuanXuanGo_A46_101Weiqi_2" />
        /// Check for snapback <see cref="UnitTestProject.AtariRedundantMoveTest.AtariRedundantMoveTest_Scenario_TianLongTu_Q16919" />
        /// Check corner kill formation <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_GuanZiPu_A2Q28_101Weiqi" />
        /// Check two-point covered eye <see cref="UnitTestProject.AtariRedundantMoveTest.AtariRedundantMoveTest_Scenario_TianLongTu_Q16525" />
        /// Check if atari on other groups <see cref="UnitTestProject.AtariRedundantMoveTest.AtariRedundantMoveTest_ScenarioHighLevel18" />
        /// </summary>
        public static Boolean AtariRedundantMove(GameTryMove tryMove)
        {
            Board currentBoard = tryMove.CurrentGame.Board;
            Board tryBoard = tryMove.TryGame.Board;
            Content c = tryBoard.MoveGroup.Content;
            if (GameHelper.GetContentForSurviveOrKill(tryBoard.GameInfo, SurviveOrKill.Kill) == c)
            {
                if (AtariResponseMove(tryMove))
                    return true;
            }
            if (!tryBoard.IsAtariMove || tryBoard.AtariTargets.Count > 1 || tryBoard.AtariResolved || tryBoard.CapturedList.Count > 0) return false;
            Group atariTarget = tryBoard.AtariTargets.First();
            //ensure target group can escape
            if (ImmovableHelper.UnescapableGroup(tryBoard, atariTarget).Item1) return false;
            //check if any move can capture target group
            (Boolean suicidal, Board board) = ImmovableHelper.ConnectAndDie(currentBoard, atariTarget);
            if (!suicidal) return false;

            //check for snapback
            Board escapeBoard = ImmovableHelper.MakeMoveAtLibertyPointOfSuicide(board, atariTarget, c.Opposite());
            if (escapeBoard != null)
            {
                Board captureBoard = ImmovableHelper.CaptureSuicideGroup(escapeBoard);
                if (captureBoard != null && captureBoard.MoveGroup.Liberties.Count == 1)
                    return false;
            }

            //check if target group move at liberty is suicidal
            Point liberty = board.GetGroupLibertyPoints(atariTarget).First();
            (Boolean suicide, Board b) = ImmovableHelper.IsSuicidalMove(liberty, c.Opposite(), board);
            if (!suicide) return false;

            //check corner kill formation
            if (tryBoard.MoveGroup.Points.Count == 1 && tryBoard.MoveGroupLiberties == 1 && tryBoard.CornerPoint(tryBoard.MoveGroup.Liberties.First()))
            {
                if (KillerFormationHelper.PreDeadFormation(currentBoard, atariTarget, atariTarget.Points.ToList(), new List<Point>() { tryBoard.MoveGroup.Points.First() }))
                    return false;
            }
            //check two-point covered eye
            if (b != null && b.MoveGroup.Points.Count == 2)
            {
                Board b2 = ImmovableHelper.CaptureSuicideGroup(b);
                if (b2 != null && EyeHelper.FindRealEyeWithinEmptySpace(b2, b.MoveGroup, EyeType.CoveredEye))
                    return false;
            }

            //check if atari on other groups
            if (b != null && b.GetNeighbourGroups(b.MoveGroup).Any(group => group.Liberties.Count == 1))
                return false;

            return true;
        }

        /// <summary>
        /// Response to atari move in current board.
        /// <see cref="UnitTestProject.AtariRedundantMoveTest.AtariRedundantMoveTest_Scenario_XuanXuanGo_A46_101Weiqi" />
        /// Capture neighbour group <see cref="UnitTestProject.AtariRedundantMoveTest.AtariRedundantMoveTest_Scenario_WindAndTime_Q30370" />
        /// Check for snapback <see cref="UnitTestProject.AtariRedundantMoveTest.AtariRedundantMoveTest_Scenario4dan17" />
        /// Check for ko <see cref="UnitTestProject.AtariRedundantMoveTest.AtariRedundantMoveTest_Scenario1dan10" />
        /// Ensure survival move is neutral point <see cref="UnitTestProject.AtariRedundantMoveTest.AtariRedundantMoveTest_Scenario_Corner_A128" />
        /// Check connect and die <see cref="UnitTestProject.AtariRedundantMoveTest.AtariRedundantMoveTest_Scenario_TianLongTu_Q16490" />
        /// </summary>
        public static Boolean AtariResponseMove(GameTryMove tryMove)
        {
            Board currentBoard = tryMove.CurrentGame.Board;
            Board tryBoard = tryMove.TryGame.Board;
            Point? lastMove = currentBoard.LastMove;
            if (lastMove == null || lastMove.Equals(Game.PassMove)) return false;
            Content c = currentBoard[lastMove.Value];

            //move on current board is atari move
            if (!currentBoard.IsAtariMove || currentBoard.AtariTargets.Count > 1) return false;
            //ensure not first move
            if (currentBoard.LastMoves.Count == 1) return false;
            Group atariTarget = currentBoard.AtariTargets.First();
            //target group three or more points
            if (atariTarget.Points.Count < 3) return false;

            //capture neighbour group
            Board captureBoard = ImmovableHelper.EscapeByCapture(currentBoard, atariTarget);
            if (captureBoard != null && tryMove.Move.Equals(captureBoard.Move))
                return false;

            //group can escape at liberty
            (Boolean result, _, Board escapeBoard) = ImmovableHelper.UnescapableGroup(currentBoard, atariTarget, false);
            if (result || tryMove.Move.Equals(escapeBoard.Move))
                return false;

            //ensure survival move is neutral point
            IEnumerable<Point> neighbourPts = currentBoard.GetStoneAndDiagonalNeighbours(lastMove.Value.x, lastMove.Value.y).Except(atariTarget.Points);
            if (neighbourPts.Any(q => !WallHelper.NoEyeForSurvival(currentBoard, q)))
                return false;

            //check redundant moves
            if (ConnectedNonKillable(captureBoard) || ConnectedNonKillable(escapeBoard))
                return true;
            return false;
        }

        /// <summary>
        /// Group becomes non killable after making move.
        /// </summary>
        private static Boolean ConnectedNonKillable(Board escapeBoard)
        {
            if (escapeBoard == null) return false;
            return WallHelper.IsNonKillableGroup(escapeBoard, escapeBoard.Move.Value);
        }
        #endregion

        #region suicidal move
        /// <summary>
        /// Suicidal moves are moves that have liberty of one only.
        /// </summary>
        public static Boolean SuicidalRedundantMove(GameTryMove tryMove)
        {
            if (SuicidalMove(tryMove))
                return true;

            //test if opponent move at same point is suicidal for single point move
            GameTryMove opponentMove = tryMove.MakeMoveWithOpponentAtSamePoint();
            if (opponentMove == null) return false;
            Board opponentTryBoard = opponentMove.TryGame.Board;
            if (opponentTryBoard.MoveGroupLiberties == 1 && opponentTryBoard.IsSinglePoint())
            {
                if (SinglePointSuicidalMove(opponentMove, tryMove))
                    return true;
            }
            return SuicidalWithinNonKillableGroup(opponentMove, tryMove);
        }


        /// <summary>
        /// Separate suicidal moves into single point or multi point suicide. 
        /// </summary>
        private static Boolean SuicidalMove(GameTryMove tryMove)
        {
            Board tryBoard = tryMove.TryGame.Board;
            Boolean singlePoint = (tryBoard.IsSinglePoint());
            if (tryBoard.MoveGroupLiberties == 1)
            {
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
            return SuicidalWithinNonKillableGroup(tryMove);
        }

        /// <summary>
        /// Check for connect and die moves. <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_TianLongTu_Q16738" />
        /// Check capture moves <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanGo_A75_101Weiqi" />
        /// Check atari moves <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WuQingYuan_Q30986" />
        /// Check for sieged scenario <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_TianLongTu_Q2834" />
        /// Check killer formation <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_GuanZiPu_A17_3" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_GuanZiPu_A17_2" />
        /// Check killer move non killable group <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WuQingYuan_Q31563" />
        /// Find real eye at diagonals for single point move <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanGo_A151_101Weiqi_4" />
        /// Check redundant corner point <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_TianLongTu_Q2834" />
        /// Check for one-by-three kill <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_GuanZiPu_Q18796_2" />
        /// </summary>
        public static Boolean SuicidalConnectAndDie(GameTryMove tryMove)
        {
            Board currentBoard = tryMove.CurrentGame.Board;
            Board tryBoard = tryMove.TryGame.Board;
            Point move = tryMove.Move;
            Content c = tryMove.MoveContent;
            HashSet<Point> movePoints = tryBoard.MoveGroup.Points;

            //check capture moves
            if (tryBoard.CapturedList.Count > 0)
            {
                if (tryBoard.CapturedList.Any(g => AtariHelper.AtariByGroup(currentBoard, g))) return false;
                if (tryBoard.GetStoneNeighbours().Any(n => EyeHelper.FindCoveredEye(tryBoard, n, c))) return false;
            }
            //check connect and die
            (Boolean suicidal, Board captureBoard) = ImmovableHelper.ConnectAndDie(tryBoard);
            if (!suicidal) return false;

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

            //ensure no killable group with two or less liberties
            if (CheckWeakGroupInConnectAndDie(tryMove, captureBoard))
                return false;

            //if all neighbour groups are killable, then check for killer formations 
            IEnumerable<Group> neighbourGroups = tryBoard.GetNeighbourGroups(tryBoard.MoveGroup);
            if (neighbourGroups.All(group => !WallHelper.IsNonKillableGroup(tryBoard, group)))
            {
                foreach (Point p in tryBoard.MoveGroup.Liberties)
                {
                    Board b = tryBoard.MakeMoveOnNewBoard(p, c.Opposite());
                    if (b == null) continue;
                    //check killer move non killable group
                    if (WallHelper.IsNonKillableGroup(b, b.MoveGroup))
                        return true;
                    if (movePoints.Count == 1)
                    {
                        //find real eye at diagonals for single point move
                        foreach (Point d in tryBoard.GetDiagonalNeighbours(move.x, move.y))
                        {
                            if (ImmovableHelper.FindTigerMouth(currentBoard, c.Opposite(), d)) continue;
                            Group killerGroupDiagonal = BothAliveHelper.GetKillerGroupFromCache(b, d, c.Opposite());
                            if (killerGroupDiagonal != null && EyeHelper.FindRealEyeWithinEmptySpace(b, killerGroupDiagonal))
                                return true;
                        }
                    }
                }
                //check redundant corner point
                if (CheckRedundantCornerPoint(tryMove))
                    return true;

                if (RedundantSuicidalConnectAndDie(tryMove))
                    return true;

                //check for sieged scenario
                List<Point> opponentStones = tryBoard.GetClosestNeighbour(move, 3, c.Opposite());
                if (!SiegedScenario(tryBoard, opponentStones, 1)) return true;

                if (movePoints.Count <= 4)
                {
                    //check for no empty points and no diagonals for move
                    if (movePoints.Count > 1 && !tryBoard.GetStoneNeighbours(move.x, move.y).Any(n => tryBoard[n] == Content.Empty) && !tryBoard.GetDiagonalNeighbours(move.x, move.y).Any(n => tryBoard[n] == c) && !LinkHelper.IsAbsoluteLinkForGroups(currentBoard, tryBoard))
                    {
                        //check for three neighbour groups
                        Boolean threeGroups = (movePoints.Count == 2 && tryBoard.GetGroupsFromStoneNeighbours(move, c).Count > 2);
                        if (!threeGroups)
                            return true;
                    }
                    //check for real eye in neighbour groups
                    return CheckAnyRealEyeInSuicidalConnectAndDie(tryBoard, captureBoard);
                }
                //check killer formation
                else if (KillerFormationHelper.SuicidalKillerFormations(tryBoard, currentBoard))
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Check for real eye in neighbour groups.
        /// Check for one-point eye <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_Corner_A30" />
        /// Check for two-point snapback <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_Corner_A55" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WuQingYuan_Q31680_2" />
        /// Check for three neighbour groups <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WindAndTime_Q30198" />
        /// Check snapback in neighbour groups <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WindAndTime_Q30234_2" />
        /// Check for covered eye group <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanGo_A17" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WindAndTime_Q30403_2" />
        /// </summary>
        private static Boolean CheckAnyRealEyeInSuicidalConnectAndDie(Board tryBoard, Board captureBoard)
        {
            Point move = tryBoard.Move.Value;
            HashSet<Point> movePoints = tryBoard.MoveGroup.Points;
            Content c = tryBoard.MoveGroup.Content;
            if (!KillerFormationHelper.CheckRealEyeInNeighbourGroups(tryBoard, move, c)) return false;
            //check for one-point eye
            Boolean onePointEye = (movePoints.Count == 1 && tryBoard.MoveGroup.Liberties.Any(liberty => EyeHelper.FindEye(tryBoard, liberty, c)));
            if (onePointEye) return false;
            //check for two-point snapback
            Boolean twoPointSnapback = (movePoints.Count == 2 && tryBoard.MoveGroup.Liberties.Any(liberty => ImmovableHelper.IsSuicidalMove(tryBoard, liberty, c.Opposite())));
            if (twoPointSnapback) return false;
            //check snapback in neighbour groups
            if (ImmovableHelper.CheckSnapbackInNeighbourGroups(captureBoard, tryBoard.MoveGroup))
                return false;
            //check for one-by-three kill
            if (movePoints.Count == 4 && KillerFormationHelper.OneByThreeFormation(tryBoard, tryBoard.MoveGroup) && tryBoard.GetGroupsFromStoneNeighbours(move, c).Count > 1)
                return false;
            //check for covered eye group
            if (movePoints.Count == 1 || movePoints.Count == 2)
            {
                List<Point> diagonals = LinkHelper.GetGroupDiagonals(captureBoard, tryBoard.MoveGroup).Select(q => q.Move).Where(q => captureBoard[q] == Content.Empty).ToList();
                diagonals = captureBoard.GetStoneNeighbours().Intersect(diagonals).ToList();
                if (diagonals.Count != 1) return true;
                (Boolean suicidal, Board b) = ImmovableHelper.IsSuicidalMove(diagonals.First(), c, captureBoard);
                if (!suicidal)
                {
                    Group capturedGroup = b.GetGroupAt(tryBoard.Move.Value);
                    Board b2 = ImmovableHelper.CaptureSuicideGroup(b, capturedGroup);
                    if (b2 != null && !EyeHelper.FindRealEyeWithinEmptySpace(b2, capturedGroup))
                        return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Check for any weak capture group in connect and die.
        /// Check killable group with two or less liberties <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_WuQingYuan_Q31435" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanGo_B6" />
        /// Check for weak group capturing atari group <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanQiJing_B17" />
        /// Check snapback <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario1dan4_2" />
        /// </summary>
        private static Boolean CheckWeakGroupInConnectAndDie(GameTryMove tryMove, Board captureBoard)
        {
            Board currentBoard = tryMove.CurrentGame.Board;
            Board tryBoard = tryMove.TryGame.Board;
            Content c = tryMove.MoveContent;
            if (tryBoard.MoveGroup.Points.Count == 1) return false;
            //check for weak group capturing atari group
            if (tryBoard.IsAtariMove && captureBoard.MoveGroup.Points.Count == 1)
            {
                Board b = ImmovableHelper.CaptureSuicideGroup(captureBoard, tryBoard.AtariTargets.First());
                if (b != null && ImmovableHelper.CheckConnectAndDie(b, captureBoard.MoveGroup))
                    return true;
            }

            //check killable group with two or less liberties
            IEnumerable<Group> neighbourGroups = tryBoard.GetNeighbourGroups(tryBoard.MoveGroup);
            if (!neighbourGroups.Any(group => group.Liberties.Count > 2)) return false;
            Group weakGroup = neighbourGroups.FirstOrDefault(group => (group.Points.Count >= 2 && group.Liberties.Count == 2 && !WallHelper.IsNonKillableGroup(tryBoard, group)));
            if (weakGroup == null) return false;
            if (tryBoard.GetNeighbourGroups(weakGroup).Any(g => WallHelper.IsNonKillableGroup(tryBoard, g))) return false;

            if (weakGroup.Liberties.Any(liberty => ImmovableHelper.IsSuicidalMove(currentBoard, liberty, c)))
            {
                //check snapback
                if (ImmovableHelper.CheckConnectAndDie(captureBoard, captureBoard.GetGroupAt(weakGroup.Points.First())))
                    return true;
                return false;
            }
            return true;
        }

        /// <summary>
        /// Check for one point move group in connect and die.
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_GuanZiPu_B7" />
        /// <see cref="UnitTestProject.LeapMoveTest.LeapMoveTest_Scenario_TianLongTu_Q16571" />
        /// Check reverse ko fight <see cref="UnitTestProject.RedundantKoMoveTest.RedundantKoMoveTest_Scenario_Corner_A80" />
        /// Check opponent move liberties <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WuQingYuan_Q31680_3" />
        /// Check snapback at diagonal <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanGo_Q6710_2" />
        /// </summary>
        private static Boolean CheckOnePointMoveInConnectAndDie(GameTryMove tryMove, Board captureBoard)
        {
            Board currentBoard = tryMove.CurrentGame.Board;
            Board tryBoard = tryMove.TryGame.Board;
            Point move = tryMove.Move;
            Content c = tryMove.MoveContent;
            if (tryBoard.MoveGroup.Points.Count != 1) return false;

            //check for two or less liberties in neighbour groups
            if (tryBoard.GetNeighbourGroups(tryBoard.MoveGroup).Any(group => group.Liberties.Count <= 2 && group.Liberties.Any(liberty => !ImmovableHelper.IsSuicidalMove(captureBoard, liberty, c))))
                return true;

            //check reverse ko fight
            if (tryBoard.GetStoneNeighbours().Any(n => ImmovableHelper.FindTigerMouth(tryBoard, c, n))) return true;
            //check opponent move liberties
            List<Point> diagonals = tryBoard.GetDiagonalNeighbours(move.x, move.y).Where(n => tryBoard[n] == c).ToList();
            if (diagonals.Count == 1)
            {
                List<Point> diagonalCutPoints = LinkHelper.PointsBetweenDiagonals(move, diagonals.First()).Where(q => tryBoard[q] == Content.Empty).ToList();
                foreach (Point p in tryBoard.MoveGroup.Liberties)
                {
                    Board b = tryBoard.MakeMoveOnNewBoard(p, c.Opposite());
                    if (b != null && b.MoveGroup.Points.Count >= 2 && b.MoveGroupLiberties == 2)
                        return true;
                }
            }
            //check snapback at diagonal
            List<Point> emptyDiagonals = tryBoard.GetDiagonalNeighbours(move.x, move.y).Where(n => tryBoard[n] == Content.Empty).ToList();
            foreach (Point p in emptyDiagonals)
            {
                if (tryBoard.GetGroupsFromStoneNeighbours(p, c).Any(group => ImmovableHelper.CheckSnapback(tryBoard, group)))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Suicide within non killable group.
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanQiJing_Weiqi101_B74_2" />
        /// Check for pre atari move <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanQiJing_A39" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_TianLongTu_Q16925" />
        /// </summary>
        public static Boolean SuicidalWithinNonKillableGroup(GameTryMove tryMove, GameTryMove opponentMove = null)
        {
            Board currentBoard = tryMove.CurrentGame.Board;
            Board tryBoard = tryMove.TryGame.Board;
            Point move = tryMove.Move;
            Content c = tryMove.MoveContent;
            if (tryBoard.MoveGroup.Points.Count != 1 || !tryMove.IsNegligible || tryBoard.IsAtariMove) return false;
            Group killerGroup = BothAliveHelper.GetKillerGroupFromCache(tryBoard, move, c.Opposite());
            if (killerGroup == null || killerGroup.Points.Count(p => tryBoard[p] == c) != 1) return false;

            IEnumerable<Group> groups = tryBoard.GetNeighbourGroups(killerGroup);
            Boolean nonKillable = groups.Any(group => WallHelper.IsNonKillableGroup(tryBoard, group));
            if (!nonKillable) return false;
            //check for pre atari move
            if (tryBoard.GetDiagonalNeighbours(move.x, move.y).Any(n => tryBoard[n] == c) && groups.Any(group => group.Liberties.Count <= 2))
                return false;
            return true;
        }

        /// <summary>
        /// Check for suicidal moves depending on diagonal groups.
        /// Check liberties are connected <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WindAndTime_Q30064" />
        /// Check atari resolved <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_Scenario_GuanZiPu_A4Q11_101Weiqi_2" />
        /// Check for corner point <see cref="UnitTestProject.KillerFormationTest.KillerFormationTest_Scenario_TianLongTu_Q15082" />
        /// Stone neighbours at diagonal of each other <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WindAndTime_Q2757" />
        /// Check diagonal at opposite corner of stone neighbours <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WuQingYuan_Q31493" />
        /// Both groups have limited liberties <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_TianLongTu_Q17081_2" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanQiJing_A61" />
        /// Ensure no diagonal at move <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WindAndTime_Q30064" />
        /// Ensure no shared liberty with neighbour group <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_Corner_A55" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_GuanZiPu_A17_3" />
        /// Check for killer formation <see cref="UnitTestProject.ImmovableTest.ImmovableTest_Scenario_XuanXuanGo_A26" />
        /// </summary>
        private static Boolean RedundantSuicidalConnectAndDie(GameTryMove tryMove)
        {
            Board tryBoard = tryMove.TryGame.Board;
            Point move = tryMove.Move;
            Content c = tryMove.MoveContent;
            if (tryBoard.CapturedList.Count > 0) return false;
            //ensure diagonal groups found
            Boolean diagonalGroups = LinkHelper.GetGroupLinkedDiagonals(tryBoard, tryBoard.MoveGroup, false).Any();
            if (!diagonalGroups)
            {
                if (tryBoard.MoveGroup.Points.Count > 1)
                {
                    //check atari resolved
                    if (tryBoard.AtariResolved) return false;
                    Point p = tryBoard.MoveGroup.Liberties.First();
                    //check liberties are connected
                    if (tryBoard.GetStoneNeighbours(p.x, p.y).Any(q => tryBoard.MoveGroup.Liberties.Contains(q)))
                    {
                        //check for corner point
                        if (tryBoard.MoveGroup.Liberties.Any(q => tryBoard.CornerPoint(q)))
                            return false;
                        return true;
                    }
                }
                else
                {
                    List<Point> stoneNeighbours = tryBoard.GetStoneNeighbours().Where(n => tryBoard[n] == c.Opposite()).ToList();
                    if (stoneNeighbours.Count == 0) return false;
                    Point p = stoneNeighbours.First();
                    //stone neighbours at diagonal of each other
                    if (stoneNeighbours.Any(n => tryBoard.GetDiagonalNeighbours(p.x, p.y).Intersect(stoneNeighbours).Any()))
                    {
                        //check diagonal at opposite corner of stone neighbours
                        List<Point> diagonals = tryBoard.GetDiagonalNeighbours(move.x, move.y).Where(d => tryBoard[d] == c.Opposite()).ToList();
                        if (diagonals.Any(d => !tryBoard.GetStoneNeighbours(d.x, d.y).Intersect(stoneNeighbours).Any()))
                            return false;

                        //both groups have limited liberties
                        if (stoneNeighbours.Count == 2)
                        {
                            List<Point> cutDiagonal = LinkHelper.PointsBetweenDiagonals(stoneNeighbours[0], stoneNeighbours[1]);
                            cutDiagonal.Remove(move);
                            Board b = tryBoard.MakeMoveOnNewBoard(cutDiagonal.First(), c, true);
                            if (b != null && stoneNeighbours.All(n => b.GetGroupLiberties(n) <= 2))
                                return false;
                        }
                        return true;
                    }
                }
                return false;
            }
            //ensure no diagonal at move
            Boolean diagonalAtMove = tryBoard.GetDiagonalNeighbours(move.x, move.y).Any(n => tryBoard[n] == c && !tryBoard.MoveGroup.Points.Contains(n));
            if (diagonalAtMove) return false;

            //ensure no shared liberty with neighbour group
            List<Group> neighbourGroups = tryBoard.GetNeighbourGroups(tryBoard.MoveGroup);
            Boolean sharedLiberty = tryBoard.MoveGroup.Liberties.Any(n => tryBoard.GetGroupsFromStoneNeighbours(n, c).Any(g => neighbourGroups.Contains(g)));
            if (sharedLiberty) return false;
            return true;
        }

        /// <summary>
        /// Single point suicide, either suicide within real eye or near non killable group. Suicide at tiger mouth handled by redundant tiger mouth move.
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
            if (opponentTryMove == null && RedundantSuicideNearNonKillableGroup(tryMove, capturedBoard))
                return true;

            return false;
        }

        /// <summary>
        /// Suicide move creates semi solid eye.
        /// Suicide within real eye <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_ScenarioHighLevel28" />
        /// Check for snapback  <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanGo_B31" />
        /// Atari move required <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WindAndTime_Q2757" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanGo_Q18500_3" />
        /// One liberty - kill from external <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanQiJing_Weiqi101_A19" />
        /// One liberty - suicide for both players <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanQiJing_A40_2" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanQiJing_Weiqi101_A19" />
        /// Crowbar formation <see cref="UnitTestProject.SpecificNeutralMoveTest.SpecificNeutralMoveTest_Scenario_TianLongTu_Q16827" />
        /// Two liberties - suicide for both players <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WindAndTime_Q30215" />
        /// </summary>
        public static Boolean SuicideWithinRealEye(GameTryMove tryMove, Board capturedBoard)
        {
            Point move = tryMove.Move;
            Board tryBoard = tryMove.TryGame.Board;
            Board currentBoard = tryMove.CurrentGame.Board;
            Content c = tryMove.MoveContent;

            //ensure semi-solid eye
            (Boolean isRealEye, _, List<LinkedPoint<Point>> tigerMouthPoints) = EyeHelper.FindSemiSolidEyes(move, capturedBoard, c.Opposite());
            if (!isRealEye)
                return false;

            //remove one point from two-point empty group
            Group eyeGroup = BothAliveHelper.GetKillerGroupFromCache(currentBoard, move, c.Opposite());
            if (eyeGroup != null && eyeGroup.Points.All(p => !currentBoard.CornerPoint(p)))
            {
                Board b = EyeHelper.FindRealEyesWithinTwoEmptyPoints(currentBoard, eyeGroup, EyeType.RealSolidEye);
                if (b != null && !move.Equals(b.Move.Value))
                    return true;
            }

            //check for snapback
            if (ImmovableHelper.CheckSnapbackInNeighbourGroups(tryBoard, tryBoard.MoveGroup))
                return false;

            //atari move required
            if (tryBoard.IsAtariMove)
            {
                //check for non two-point group
                Boolean twoPointGroup = (eyeGroup != null && eyeGroup.Points.Count == 2);
                if (!twoPointGroup && CheckNonTwoPointGroupInSuicideRealEye(tryMove))
                    return true;

                //check two point group
                if (twoPointGroup && CheckTwoPointGroupInSuicideRealEye(tryMove, capturedBoard))
                    return true;

                return false;
            }

            //atari on neighbours then redundant
            if (currentBoard.GetGroupsFromStoneNeighbours(move, c).Any(group => AtariHelper.AtariByGroup(currentBoard, group)))
                return true;

            //retrieve liberties other than eye liberty
            HashSet<Point> liberties = capturedBoard.GetLibertiesOfGroups(capturedBoard.GetNeighbourGroups(tryBoard.MoveGroup));
            liberties.Remove(move);

            //any liberty is eye then redundant
            if (liberties.Any(liberty => EyeHelper.FindEye(capturedBoard, liberty, c.Opposite())))
                return true;

            if (liberties.Count == 1)
            {
                //one liberty - suicide for both players
                Point q = liberties.First();
                if (BothAliveHelper.GetKillerGroupFromCache(tryBoard, q) != null && ImmovableHelper.IsSuicidalMoveForBothPlayers(capturedBoard, q))
                    return false;

                //crowbar formation
                List<Group> neighbourGroups = tryBoard.GetNeighbourGroups(move);
                if (neighbourGroups.Count == 1 && KillerFormationHelper.CrowbarEyeFormation(currentBoard, neighbourGroups.First()))
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
                    if (BothAliveHelper.GetKillerGroupFromCache(tryBoard, q) != null && ImmovableHelper.IsSuicidalMoveForBothPlayers(b, q))
                        return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Check for non two-point group <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WuQingYuan_Q31536" />
        /// Check killer group <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario4dan17_2" />
        /// </summary>
        private static Boolean CheckNonTwoPointGroupInSuicideRealEye(GameTryMove tryMove)
        {
            Point move = tryMove.Move;
            Board tryBoard = tryMove.TryGame.Board;
            Content c = tryMove.MoveContent;
            List<Point> diagonals = tryBoard.GetDiagonalNeighbours(move.x, move.y).Where(n => tryBoard[n] != c.Opposite()).ToList();
            if (diagonals.Count == 0) return false;
            //check killer group
            if (diagonals.Any(d => BothAliveHelper.GetKillerGroupFromCache(tryBoard, d, c.Opposite()) != null)) return true;
            //check eye for survival
            if (diagonals.Any(d => WallHelper.NoEyeForSurvival(tryBoard, d) && !WallHelper.IsNonKillableGroup(tryBoard, d)))
                return true;
            return false;
        }

        /// <summary>
        /// Check for two-point group.
        /// Check for liberty fight <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanQiJing_Weiqi101_1887" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_GuanZiPu_Q18796" />
        /// </summary>
        private static Boolean CheckTwoPointGroupInSuicideRealEye(GameTryMove tryMove, Board capturedBoard)
        {
            Point move = tryMove.Move;
            Board tryBoard = tryMove.TryGame.Board;
            Content c = tryMove.MoveContent;
            HashSet<Group> neighbourGroups = capturedBoard.GetGroupsFromStoneNeighbours(move, c);
            if (neighbourGroups.Count() != 1) return false;
            HashSet<Point> liberties = neighbourGroups.First().Liberties;
            liberties.Remove(move);
            if (liberties.Count > 2) return true;
            foreach (Point liberty in liberties)
            {
                //check for liberty fight
                Board b = capturedBoard.MakeMoveOnNewBoard(liberty, c, true);
                if (b != null && b.GetNeighbourGroups(capturedBoard.MoveGroup).All(group => group.Liberties.Count > 2))
                    return true;
            }

            return false;
        }


        /// <summary>
        /// Kill move make suicide move with diagonally connected non killable group.
        /// Suicidal move next to non killable group for survive <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanGo_A27_2" />
        /// Connect and die <see cref="UnitTestProject.ImmovableTest.ImmovableTest_Scenario_XuanXuanGo_B32" />
        /// Liberty more than two required to prevent snapback <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WuQingYuan_Q31680" />
        /// Diagonal neighbours that are non killable groups <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_TianLongTu_Q17160" />
        /// </summary>
        private static Boolean RedundantSuicideNearNonKillableGroup(GameTryMove tryMove, Board capturedBoard)
        {
            Point p = tryMove.Move;
            Board tryBoard = tryMove.TryGame.Board;
            Content c = tryMove.MoveContent;
            if (capturedBoard.MoveGroupLiberties == 1) return false;
            if (GameHelper.GetContentForSurviveOrKill(tryBoard.GameInfo, SurviveOrKill.Survive) == c)
            {
                //for survive, any suicidal move next to non killable group is redundant
                List<Group> neighbourGroups = tryBoard.GetGroupsFromStoneNeighbours(p);
                if (neighbourGroups.Any(group => WallHelper.IsNonKillableGroup(tryBoard, group)))
                {
                    //check connect and die
                    Boolean connectAndDie = (ImmovableHelper.AllConnectAndDie(capturedBoard, p));
                    return !connectAndDie;
                }
                return false;
            }
            else
            {
                //make move at empty point to connect to non killable group
                (Boolean isSuicide, IEnumerable<Point> diagonalNeighbours) = SuicideNearNonKillableGroup(tryBoard);
                if (isSuicide && diagonalNeighbours.Any(n => LinkHelper.PointsBetweenDiagonals(p, n).Any(d => tryBoard[d] == Content.Empty)))
                    return true;
            }
            return false;
        }

        private static (Boolean, IEnumerable<Point>) SuicideNearNonKillableGroup(Board board)
        {
            Point p = board.Move.Value;
            Content c = board[p];
            //get diagonal neighbours that are non killable groups
            IEnumerable<Point> diagonalNeighbours = board.GetDiagonalNeighbours(p.x, p.y);
            diagonalNeighbours = diagonalNeighbours.Where(n => WallHelper.IsNonKillableGroup(board, n));

            int neighbourCount = diagonalNeighbours.Count();
            if (neighbourCount == 0) return (false, null);
            Boolean middleArea = board.PointWithinMiddleArea(p);
            if (!middleArea && neighbourCount != 1) return (false, null);
            if (middleArea && neighbourCount != 2) return (false, null);
            return (true, diagonalNeighbours);
        }

        /// <summary>
        /// Multi point suicide move.
        /// Check for corner kill <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario7kyu25" />
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
        /// Check for connect and die <see cref="UnitTestProject.ImmovableTest.ImmovableTest_Scenario_TianLongTu_Q15054" />
        /// Check atari by previous move group <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_TianLongTu_Q16424_2" />
        /// Move group binding <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanQiJing_Weiqi101_B19_2" />
        /// Two-point atari covered eye <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_GuanZiPu_A32" />
        /// Suicide at covered eye <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WuQingYuan_Q31499_2" />
        /// Exclude if corner point <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_Corner_A9_Ext_2" />
        /// <see cref="UnitTestProject.KillerFormationTest.KillerFormationTest_Scenario_TianLongTu_Q16424" />
        /// Corner three formation <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_GuanZiPu_Q18860" />
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
            //captured more than move group
            if (capturedBoard.CapturedList.Count > 1)
            {
                Boolean coveredEye = capturedBoard.CapturedList.Any(group => group.Points.Count <= 2 && EyeHelper.FindRealEyeWithinEmptySpace(capturedBoard, group, EyeType.CoveredEye));
                if (!coveredEye)
                    return true;
            }

            //make move at liberty instead of suicide
            Point? liberty = capturedBoard.Move;
            Board tryLinkBoard = currentBoard.MakeMoveOnNewBoard(liberty.Value, c);
            if (tryLinkBoard == null) //capture at tryBoard
            {
                //check for corner kill
                if (tryBoard.CapturedPoints.Count() == 1 && tryBoard.CornerPoint(tryBoard.CapturedPoints.First()) && (moveCount == 5 && KillerFormationHelper.KnifeFiveFormation(tryBoard, tryBoard.MoveGroup)) || (moveCount == 6 && KillerFormationHelper.FlowerSixFormation(tryBoard, tryBoard.MoveGroup) || (moveCount == 7 && KillerFormationHelper.FlowerSevenSideFormation(tryBoard, tryBoard.MoveGroup))))
                    return false;
                //check for connect and die
                if (ImmovableHelper.CheckConnectAndDie(capturedBoard))
                    return false;
                //eternal life
                if (capturedBoard.CapturedPoints.Count() == 2 && moveCount == 2)
                    return false;
                //capture at tryBoard more than recapture        
                if (tryBoard.CapturedPoints.Any(p => !capturedBoard.CapturedPoints.Contains(p) && !capturedBoard.Move.Equals(p)))
                    return false;
                return true;
            }

            if (moveCount == 2)
            {
                //check for recursion
                Point moveGroupPoint = tryBoard.MoveGroup.Points.Where(m => !m.Equals(move)).First();
                if (tryLinkBoard.GetGroupLiberties(moveGroupPoint) > 1 && !ImmovableHelper.IsSuicidalOnCapture(capturedBoard).Item1)
                    return true;
                //two-point atari move
                if (TwoPointAtariMove(tryBoard, capturedBoard))
                    return false;
            }
            else if (moveCount == 3)
            {
                //corner three formation
                if (KillerFormationHelper.CornerThreeFormation(tryBoard, tryBoard.MoveGroup))
                    return false;
            }

            //check for connect and die
            if (capturedBoard.MoveGroup.Points.Count > 1 && ImmovableHelper.CheckConnectAndDie(capturedBoard))
                return false;

            //killer formations
            if (KillerFormationHelper.SuicidalKillerFormations(tryBoard, currentBoard, capturedBoard))
            {
                //check link to external group
                if (IsLinkToExternalGroup(tryMove, tryLinkBoard))
                    return true;
                return false;
            }
            //no hope of escape
            return true;
        }

        /// <summary>
        /// Two point atari move 
        /// Check for three groups <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WuQingYuan_Q30935" />
        /// Check for ko fight 
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WuQingYuan_Q31672" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WuQingYuan_Q31428" />
        /// </summary>
        private static Boolean TwoPointAtariMove(Board tryBoard, Board capturedBoard)
        {
            Point move = tryBoard.Move.Value;
            Content c = tryBoard[move];
            if (capturedBoard.CapturedPoints.Count() != 2 || !tryBoard.IsAtariMove) return false;
            //check for three groups
            if (tryBoard.GetGroupsFromStoneNeighbours(move, c).Count > 2) return true;
            //check for ko fight
            if (!KoHelper.KoContentEnabled(c, tryBoard.GameInfo)) return false;
            (Boolean isAtari, Board board) = AtariHelper.CheckAtariMove(capturedBoard, move, c);
            if (isAtari && board.AtariTargets.Count == 1 && board.AtariTargets.First().Points.Count == 1)
            {
                Point? libertyPoint = ImmovableHelper.GetLibertyPointOfSuicide(board, board.AtariTargets.First());
                if (libertyPoint == null) return false;
                Point q = libertyPoint.Value;
                if (EyeHelper.FindNonSemiSolidEye(capturedBoard, q, c.Opposite()))
                    return true;

                List<Point> emptyPoints = board.GetStoneNeighbours(q.x, q.y).Where(n => board[n] == Content.Empty).ToList();
                if (emptyPoints.Count != 1) return false;
                Board b = board.MakeMoveOnNewBoard(q, c.Opposite());
                if (b != null && EyeHelper.FindCoveredEye(b, emptyPoints.First(), c.Opposite()))
                    return true;
            }
            return false;
        }


        /// <summary>
        /// Ensure link is connected to both stones from previous move group and to external group.
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_TianLongTu_Q16520_2" />
        /// Not more than three points <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WuQingYuan_Q31682" />
        /// Connect three or more groups <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanGo_B3" />
        /// No lost groups <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanQiJing_Weiqi101_18402_2" />
        /// Check connect and die <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WindAndTime_Q30403" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_GuanZiPu_A4Q11_101Weiqi_2" />
        /// </summary>
        private static Boolean IsLinkToExternalGroup(GameTryMove tryMove, Board tryLinkBoard)
        {
            Point move = tryMove.Move;
            Board tryBoard = tryMove.TryGame.Board;
            Board currentBoard = tryMove.CurrentGame.Board;
            Content c = tryMove.MoveContent;
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
                //not more than three points
                if (lostGroups.Count == 1 && lostGroups.First().Points.Count <= 3) return true;
            }
            return false;
        }
        #endregion

        #region base line
        /// <summary>
        /// Base line moves are moves that occur on the edge of the board.        
        /// Base line survival move, directly below or diagonal to non killable group <see cref="UnitTestProject.BaseLineSurvivalMoveTest.BaseLineSurvivalMoveTest__Scenario_XuanXuanGo_A23" />
        /// If next to opponent stone then not redundant <see cref="UnitTestProject.BaseLineSurvivalMoveTest.BaseLineSurvivalMoveTest_Scenario_XuanXuanQiJing_Weiqi101_18473" />
        /// </summary>
        public static Boolean BaseLineSurvivalMove(GameTryMove tryMove)
        {
            Board tryBoard = tryMove.TryGame.Board;
            Point move = tryMove.Move;
            Content c = tryMove.MoveContent;

            if (!tryMove.IsNegligible)
                return false;

            //check for non killable group near base line survival move
            for (int i = 0; i <= dh.DirectionLinkedList.Count - 1; i++)
            {
                //start with bottom edge
                Direction currentDirection = dh.GetNewDirection(Direction.Up, i);
                if (!dh.IsEdgeInDirection(tryBoard, move, currentDirection.Opposite())) continue;

                Point pointUp = dh.GetPointInDirection(tryBoard, move, currentDirection);
                if (!tryBoard.PointWithinMiddleArea(pointUp)) return false;
                Point pointUpLeft = dh.GetPointInDirection(tryBoard, pointUp, dh.GetNewDirection(Direction.Left, i));
                Point pointUpRight = dh.GetPointInDirection(tryBoard, pointUp, dh.GetNewDirection(Direction.Right, i));
                //if point up is non killable group and current move not connected then redundant
                if (tryBoard[pointUp] == c.Opposite() && WallHelper.IsNonKillableGroup(tryBoard, pointUp))
                {
                    if ((tryBoard[pointUpLeft] != c && tryBoard[pointUpRight] != c))
                        return true;
                }
                //if diagonal point is non killable group and point up is empty and not next to opponent stone then redundant
                Boolean found = false;
                if (tryBoard[pointUp] == Content.Empty && tryBoard[pointUpLeft] == c.Opposite() && CheckBaseLineDiagonalPoint(tryBoard, pointUpLeft))
                {
                    Point pointRight = dh.GetPointInDirection(tryBoard, move, dh.GetNewDirection(Direction.Right, i));
                    if (tryBoard[pointRight] != c.Opposite())
                        found = true;
                }
                else if (tryBoard[pointUp] == Content.Empty && tryBoard[pointUpRight] == c.Opposite() && CheckBaseLineDiagonalPoint(tryBoard, pointUpRight))
                {
                    Point pointLeft = dh.GetPointInDirection(tryBoard, move, dh.GetNewDirection(Direction.Left, i));
                    if (tryBoard[pointLeft] != c.Opposite())
                        found = true;
                }

                //check for connect and die
                if (found)
                {
                    Board b = tryBoard.MakeMoveOnNewBoard(pointUp, c.Opposite());
                    if (b != null && !ImmovableHelper.CheckConnectAndDie(b))
                        return true;
                }
            }
            return false;
        }

        private static Boolean CheckBaseLineDiagonalPoint(Board tryBoard, Point point)
        {
            if (WallHelper.IsNonKillableGroup(tryBoard, point))
                return true;
            if (tryBoard.MoveGroup.Points.Count != 1) return false;
            Content c = tryBoard[tryBoard.Move.Value];
            List<Point> opponentStones = tryBoard.GetClosestNeighbour(point, 3, c.Opposite());
            if (!SiegedScenario(tryBoard, opponentStones, 1)) return true;
            return false;
        }

        #endregion

        #region leap move
        /// <summary>
        /// Leap moves occur where there is one space between the move and the closest neighbour stone of same content.
        /// The leap move is redundant where there is a non killable group one space above or below the space between the leap.
        /// <see cref="UnitTestProject.LeapMoveTest.LeapMoveTest_Scenario_XuanXuanQiJing_A1" />
        /// </summary>

        public static Boolean SurvivalLeapMove(GameTryMove tryMove)
        {
            if (!tryMove.IsNegligible)
                return false;

            Board tryBoard = tryMove.TryGame.Board;
            Point move = tryMove.Move;

            if (!tryBoard.IsSinglePoint())
                return false;

            //get leap move by finding closest neighbours
            List<Point> closestNeighbours = tryBoard.GetClosestNeighbour(move, 3);

            List<KeyValuePair<Direction, Point>> leapMoves = GetLeapMove(move, closestNeighbours);
            if (leapMoves.Count == 0) return false;

            //leap move cannot be in both directions
            if (leapMoves.Any(t => t.Key == Direction.Left) && leapMoves.Any(t => t.Key == Direction.Right))
                return false;

            if (leapMoves.Any(t => t.Key == Direction.Up) && leapMoves.Any(t => t.Key == Direction.Down))
                return false;

            //if movable points exist on bottom edge, then possible to leap in x-direction. if movable points exist on left edge, then possible to leap in y-direction.
            List<int> coordinates = new List<int>();
            for (int i = 0; i <= tryBoard.SizeX - 1; i++)
                coordinates.Add(i);

            Boolean x_axis = coordinates.Any(s => tryBoard.GameInfo.IsMovablePoint[s, tryBoard.SizeY - 1] == true); //bottom edge
            Boolean y_axis = coordinates.Any(s => tryBoard.GameInfo.IsMovablePoint[0, s] == true); //left edge

            if (x_axis && y_axis)
                leapMoves = leapMoves.Where(t => (t.Key == Direction.Right) || (t.Key == Direction.Up)).ToList();
            else if (x_axis)
                leapMoves = leapMoves.Where(t => (t.Key == Direction.Left || t.Key == Direction.Right)).ToList();
            else if (y_axis)
                leapMoves = leapMoves.Where(t => (t.Key == Direction.Up || t.Key == Direction.Down)).ToList();
            else
                return false;

            if (leapMoves.Count == 0) return false;

            //validate if leap move is redundant
            if (leapMoves.Any(leapMove => ValidateLeapMove(tryBoard, move, leapMove)))
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
        /// Get leap move where distance is two points away. Return result with leap in x or y direction and closest neighbour.
        /// </summary>
        public static List<KeyValuePair<Direction, Point>> GetLeapMove(Point move, List<Point> closestNeighbours)
        {
            List<KeyValuePair<Direction, Point>> result = new List<KeyValuePair<Direction, Point>>();
            foreach (Point q in closestNeighbours)
            {
                (Direction direction, int x_dist, int y_dist) = DirectionHelper.GetDirectionFromTwoPoints(move, q);
                x_dist = Math.Abs(x_dist);
                y_dist = Math.Abs(y_dist);

                if (x_dist <= 1 && y_dist <= 1)
                    break;
                else if (x_dist >= 2 && y_dist >= 2)
                    break;
                result.Add(new KeyValuePair<Direction, Point>(direction, q));
            }
            return result;
        }

        /// <summary>
        /// Check for non killable group next to leap point as well as one space above or below the space between the leap.
        /// </summary>
        public static Boolean ValidateLeapMove(Board tryBoard, Point p, KeyValuePair<Direction, Point> leapMove)
        {
            Direction direction = leapMove.Key;
            Point q = leapMove.Value;
            Content c = tryBoard[p];

            Boolean leapOnSameLine = (p.x.Equals(leapMove.Value.x) || p.y.Equals(leapMove.Value.y));
            //get middle points between the leap
            List<Point> middlePoints = new List<Point>();
            if (direction == Direction.Left || direction == Direction.Right)
            {
                int y_min = Math.Min(p.y, q.y);
                int y_max = Math.Max(p.y, q.y);
                if (leapOnSameLine)
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
            else if (direction == Direction.Up || direction == Direction.Down)
            {
                int x_min = Math.Min(p.x, q.x);
                int x_max = Math.Max(p.x, q.x);
                if (leapOnSameLine)
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

            //check for non killable groups at middle points
            foreach (Point midPt in middlePoints)
            {
                if (!tryBoard.PointWithinBoard(midPt) || tryBoard[midPt] == Content.Empty)
                    continue;
                if (tryBoard[midPt] == c.Opposite() && WallHelper.IsNonKillableGroup(tryBoard, midPt))
                    return true;
            }
            return false;
        }
        #endregion

        #region neutral point
        /// <summary>
        /// Neutral points are moves that cannot create eye for the survival. Neutral points are usually present on the external liberties of the target group and redundant for the survival to make the move.
        /// <see cref="UnitTestProject.NeutralPointMoveTest.NeutralPointMoveTest_Scenario_WuQingYuan_Q30935" />
        /// </summary>
        public static Boolean NeutralPointSurvivalMove(GameTryMove tryMove, Boolean survivalMove = true)
        {
            if (survivalMove && !tryMove.IsNegligible)
            {
                if (!RedundantAtariAtCoveredEye(tryMove))
                    return false;
            }
            //validate neutral point
            Boolean isNeutralPoint = ValidateNeutralPoint(tryMove);
            if (isNeutralPoint)
                return CheckKoForNeutralPoint(tryMove);

            return isNeutralPoint;
        }

        /// <summary>
        /// Ensure neutral point move is not required for ko.
        /// Rare scenario where neutral point required for ko <see cref="UnitTestProject.RedundantKoMoveTest.RedundantKoMoveTest_Scenario_Corner_A80" />
        /// </summary>
        public static Boolean CheckKoForNeutralPoint(GameTryMove tryMove)
        {
            Board tryBoard = tryMove.TryGame.Board;
            Point move = tryMove.Move;
            if (!tryBoard.IsSinglePoint()) return true;
            if (!KoHelper.KoSurvivalEnabled(SurviveOrKill.Survive, tryBoard.GameInfo))
                return true;
            if (tryBoard.GetClosestNeighbour(move, 1).Count == 0)
                return true;
            Direction wallDirection = WallHelper.IsWallNeighbour(tryBoard, move).Item2;
            if (wallDirection == Direction.None) return true;
            Point p1 = dh.GetPointInDirection(tryBoard, tryMove.Move, wallDirection.Opposite());
            Point p2 = dh.GetPointInDirection(tryBoard, p1, wallDirection.Opposite());
            if (p1.Equals(Game.PassMove) || p2.Equals(Game.PassMove))
                return true;

            Content c = tryMove.MoveContent;
            if (tryBoard[p1] == Content.Empty && tryBoard[p2] == Content.Empty)
            {
                Board b = new Board(tryBoard);
                b[p2] = c;
                List<Point> diagonals = b.GetDiagonalNeighbours(p1.x, p1.y);
                diagonals = diagonals.Where(diagonal => BothAliveHelper.GetKillerGroupFromCache(tryBoard, diagonal) != null).ToList();
                if (diagonals.Any(diagonal => b[diagonal] == Content.Empty && ImmovableHelper.FindTigerMouth(b, c, diagonal)))
                    return RedundantMoveHelper.CheckRedundantKo(tryMove, true);
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
            Board tryBoard = tryMove.TryGame.Board;
            if (!tryMove.IsNegligible)
            {
                if (!RedundantAtariAtCoveredEye(tryMove))
                    return false;
            }
            //make move from perspective of survival
            GameTryMove move = tryMove.MakeMoveWithOpponentAtSamePoint();
            if (move == null) return false;

            Boolean isNeutralPoint = NeutralPointSurvivalMove(move, false);
            if (isNeutralPoint)
            {
                if (ImmovableHelper.CheckConnectAndDie(tryBoard, tryBoard.MoveGroup)) return isNeutralPoint;
                //must have neutral point
                (Boolean mustHave, Point? linkPoint) = MustHaveNeutralPoint(move);
                if (mustHave)
                {
                    tryMove.MustHaveNeutralPoint = true;
                    tryMove.LinkPoint = new LinkedPoint<Point>(linkPoint.Value, tryMove.Move);
                }
            }
            return isNeutralPoint;
        }

        /// <summary>
        /// Redundant atari at covered eye.
        /// <see cref="UnitTestProject.NeutralPointMoveTest.NeutralPointMoveTest_Scenario4dan17" />
        /// Check for ko fight <see cref="UnitTestProject.NeutralPointMoveTest.NeutralPointMoveTest_Scenario_Corner_A36" />
        /// Check for groups with two or less liberties <see cref="UnitTestProject.NeutralPointMoveTest.NeutralPointMoveTest_Scenario_Phenomena_B7" />
        /// <see cref="UnitTestProject.NeutralPointMoveTest.NeutralPointMoveTest_Scenario_XuanXuanGo_A28_101Weiqi_3" />
        /// </summary>
        private static Boolean RedundantAtariAtCoveredEye(GameTryMove tryMove)
        {
            Board currentBoard = tryMove.CurrentGame.Board;
            Board tryBoard = tryMove.TryGame.Board;
            Content c = tryBoard.MoveGroup.Content;
            if (tryBoard.AtariResolved || tryBoard.CapturedList.Count > 0 || tryBoard.MoveGroupLiberties == 1) return false;
            if (tryBoard.AtariTargets.Count == 1 && tryBoard.AtariTargets.First().Points.Count == 1)
            {
                Point p = tryBoard.AtariTargets.First().Points.First();
                if (!tryBoard.GetStoneNeighbours(p.x, p.y).Any(q => EyeHelper.FindCoveredEye(currentBoard, q, c.Opposite()))) return false;
                Board b = ImmovableHelper.CaptureSuicideGroup(p, tryBoard);
                if (b != null && KoHelper.IsKoFight(b))
                {
                    //check for ko fight
                    if (!KoHelper.KoContentEnabled(c, currentBoard.GameInfo))
                        return true;

                    List<Group> neighbourGroups = b.GetNeighbourGroups(b.MoveGroup);
                    if (neighbourGroups.All(group => WallHelper.IsNonKillableGroup(b, group)))
                        return true;

                    //check for groups with two or less liberties
                    if (WallHelper.StrongNeighbourGroups(b, neighbourGroups))
                        return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Must have neutral point allows move to be made to prevent suicidal move at generic neutral points.
        /// Neutral point at tiger mouth <see cref="UnitTestProject.MustHaveNeutralMoveTest.MustHaveNeutralMoveTest_Scenario5dan27_3" />
        /// Neutral point at bigger tiger mouth <see cref="UnitTestProject.MustHaveNeutralMoveTest.MustHaveNeutralMoveTest_Scenario5dan27_Variation" />
        /// Negative example <see cref="UnitTestProject.MustHaveNeutralMoveTest.MustHaveNeutralMoveTest_Scenario_XuanXuanGo_A27" />
        /// <see cref="UnitTestProject.MustHaveNeutralMoveTest.MustHaveNeutralMoveTest_Scenario_XuanXuanGo_A23" />
        /// <see cref="UnitTestProject.MustHaveNeutralMoveTest.MustHaveNeutralMoveTest_Scenario_GuanZiPu_Weiqi101_19138" />
        /// </summary>
        private static (Boolean, Point?) MustHaveNeutralPoint(GameTryMove tryMove)
        {
            Point move = tryMove.Move;
            Board tryBoard = tryMove.TryGame.Board;
            Board currentBoard = tryMove.CurrentGame.Board;
            Content c = GameHelper.GetContentForSurviveOrKill(tryBoard.GameInfo, SurviveOrKill.Kill);
            Point p = tryBoard.Move.Value;

            //neutral point at small tiger mouth
            Point tigerMouth = tryBoard.GetStoneNeighbours().FirstOrDefault(n => EyeHelper.FindEye(tryBoard, n));
            if (Convert.ToBoolean(tigerMouth.NotEmpty))
            {
                //redundant suicidal at tiger mouth
                if (RedundantSuicidalForMustHaveNeutralPoint(currentBoard, c, tigerMouth))
                    return (false, null);
                return (true, tigerMouth);
            }
            //neutral point at big tiger mouth
            List<Group> eyeGroups = currentBoard.GetGroupsFromStoneNeighbours(move, c.Opposite()).ToList();
            (Boolean suicide, Point? liberty) = SuicideAtBigTigerMouth(tryMove, eyeGroups);
            if (suicide)
            {
                //redundant suicidal at tiger mouth
                if (RedundantSuicidalForMustHaveNeutralPoint(currentBoard, c, liberty.Value))
                    return (false, null);
                return (true, liberty.Value);
            }

            return (false, null);
        }

        /// <summary>
        /// Redundant suicidal at tiger mouth.
        /// <see cref="UnitTestProject.MustHaveNeutralMoveTest.MustHaveNeutralMoveTest_Scenario5dan27_3" />
        /// </summary>
        private static Boolean RedundantSuicidalForMustHaveNeutralPoint(Board currentBoard, Content c, Point tigerMouth)
        {
            Board board = currentBoard.MakeMoveOnNewBoard(tigerMouth, c);
            if (board != null && board.MoveGroupLiberties == 1)
            {
                HashSet<Group> neighbourGroups = board.GetGroupsFromStoneNeighbours(tigerMouth, c);
                if (WallHelper.StrongNeighbourGroups(board, neighbourGroups))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Validate neutral point by checking if move creates eye for survival at any of the stone and diagonal neighbours.
        /// Check link for groups <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_Scenario_XuanXuanQiJing_Weiqi101_18497" />
        /// <see cref="UnitTestProject.NeutralPointMoveTest.NeutralPointMoveTest_Scenario_XuanXuanQiJing_Weiqi101_7245" />
        /// 
        /// </summary>
        public static Boolean ValidateNeutralPoint(GameTryMove tryMove)
        {
            Board tryBoard = tryMove.TryGame.Board;
            Point move = tryMove.Move;
            //ensure eye cannot be created at any stone or diagonal neighbours
            IEnumerable<Point> neighbourPts = tryBoard.GetStoneAndDiagonalNeighbours(move.x, move.y);
            if (neighbourPts.Any(q => !WallHelper.NoEyeForSurvival(tryBoard, q)))
                return false;
            //check link for groups
            if (tryMove.LinkForGroups())
                return false;
            return true;
        }


        #endregion

        #region restore neutral points
        /// <summary>
        /// Neutral points for kill moves have to be restored on end game one at a time to surround external liberties of target group in order to kill it
        /// Remaining move at liberty point <see cref="UnitTestProject.MustHaveNeutralMoveTest.MustHaveNeutralMoveTest_Scenario_XuanXuanQiJing_Weiqi101_7245" />
        /// </summary>
        public static void RestoreNeutralMove(Game currentGame, List<GameTryMove> tryMoves, List<GameTryMove> neutralPointMoves)
        {
            //Remove moves that are within killer group
            neutralPointMoves.RemoveAll(n => n.TryGame.Board.MoveGroupLiberties == 1 || BothAliveHelper.GetKillerGroupFromCache(n.TryGame.Board, n.Move) != null);

            if (neutralPointMoves.Count == 0) return;
            GameTryMove genericNeutralMove = null;
            //specific neutral point
            GameTryMove specificNeutralMove = GetSpecificNeutralMove(currentGame, neutralPointMoves);
            if (specificNeutralMove != null)
            {
                tryMoves.Add(specificNeutralMove);
                neutralPointMoves.Remove(specificNeutralMove);

                //must have neutral point for specific neutral move
                GameTryMove mustHaveNeutralMove = GetMustHaveNeutralMoveForSpecificNeutralMove(specificNeutralMove, neutralPointMoves);
                if (mustHaveNeutralMove != null)
                {
                    tryMoves.Add(mustHaveNeutralMove);
                    neutralPointMoves.Remove(mustHaveNeutralMove);
                }
            }
            else
            {
                //generic neutral point
                genericNeutralMove = GetGenericNeutralMove(currentGame, tryMoves, neutralPointMoves);
                if (genericNeutralMove != null)
                {
                    tryMoves.Add(genericNeutralMove);
                    neutralPointMoves.Remove(genericNeutralMove);
                }
                else
                {
                    //must have neutral point
                    List<GameTryMove> mustHaveNeutralMoves = GetMustHaveNeutralMove(currentGame, neutralPointMoves);
                    foreach (GameTryMove tryMove in mustHaveNeutralMoves)
                    {
                        tryMoves.Add(tryMove);
                        neutralPointMoves.Remove(tryMove);
                    }
                }
            }
            //return neutral move if no more moves
            if (neutralPointMoves.Count == 0) return;
            if (tryMoves.Count == 0)
                tryMoves.Add(neutralPointMoves.First());
            else if (tryMoves.Count == 1)
            {
                //remaining move at liberty point
                GameTryMove neutralPointMove = neutralPointMoves.FirstOrDefault(move => move.MustHaveNeutralPoint && move.LinkPoint.Move.Equals(tryMoves.First().Move));
                if (neutralPointMove != null)
                    tryMoves.Add(neutralPointMove);
            }
        }

        /// <summary>
        /// Get must have neutral move.
        /// Check if atari <see cref="UnitTestProject.MustHaveNeutralMoveTest.MustHaveNeutralMoveTest_Scenario_XuanXuanGo_A23" />
        /// <see cref="UnitTestProject.MustHaveNeutralMoveTest.MustHaveNeutralMoveTest_Scenario_TianLongTu_Q17136" />
        /// <see cref="UnitTestProject.MustHaveNeutralMoveTest.MustHaveNeutralMoveTest_Scenario_XuanXuanGo_Q18500_4" />
        /// <see cref="UnitTestProject.MustHaveNeutralMoveTest.MustHaveNeutralMoveTest_Scenario_Corner_A68" />
        /// Check if link for groups <see cref="UnitTestProject.MustHaveNeutralMoveTest.MustHaveNeutralMoveTest_Scenario_Corner_A84" />
        /// Check for tiger mouth <see cref="UnitTestProject.MustHaveNeutralMoveTest.MustHaveNeutralMoveTest_Scenario_Corner_A27" />
        /// <see cref="UnitTestProject.MustHaveNeutralMoveTest.MustHaveNeutralMoveTest_Scenario_Phenomena_Q25182" />
        /// <see cref="UnitTestProject.MustHaveNeutralMoveTest.MustHaveNeutralMoveTest_Scenario_TianLongTu_Q16827" />
        /// Two must have neutral moves <see cref="UnitTestProject.MustHaveNeutralMoveTest.MustHaveNeutralMoveTest_Scenario_GuanZiPu_Weiqi101_19138" />
        /// </summary>
        public static List<GameTryMove> GetMustHaveNeutralMove(Game currentGame, List<GameTryMove> neutralPointMoves)
        {
            Content c = GameHelper.GetContentForSurviveOrKill(currentGame.GameInfo, SurviveOrKill.Survive);
            List<GameTryMove> mustHaveNeutralMoves = neutralPointMoves.Where(n => n.MustHaveNeutralPoint).ToList();
            List<GameTryMove> result = new List<GameTryMove>();
            foreach (GameTryMove mustHaveNeutralMove in mustHaveNeutralMoves)
            {
                Board tryBoard = mustHaveNeutralMove.TryGame.Board;
                if (mustHaveNeutralMove.LinkPoint == null) continue;
                Point liberty = mustHaveNeutralMove.LinkPoint.Move;

                //check if atari / link for groups
                Board b = tryBoard.MakeMoveOnNewBoard(liberty, c.Opposite(), true);
                if (b != null && (AtariHelper.AtariByGroup(b, b.MoveGroup) || LinkHelper.IsAbsoluteLinkForGroups(tryBoard, b)))
                {
                    result.Add(mustHaveNeutralMove);
                    continue;
                }

                //check for tiger mouth
                Point tigerMouth = tryBoard.GetDiagonalNeighbours(liberty.x, liberty.y).FirstOrDefault(n => EyeHelper.FindEye(tryBoard, n, c) || ImmovableHelper.FindTigerMouth(tryBoard, c, n));
                if (Convert.ToBoolean(tigerMouth.NotEmpty))
                    result.Add(mustHaveNeutralMove);
            }
            return result;
        }

        /// <summary>
        /// Get must have neutral move for specific neutral move.
        /// <see cref="UnitTestProject.MustHaveNeutralMoveTest.MustHaveNeutralMoveTest_Scenario_XuanXuanGo_A28_101Weiqi" />
        /// </summary>
        public static GameTryMove GetMustHaveNeutralMoveForSpecificNeutralMove(GameTryMove specificNeutralMove, List<GameTryMove> neutralPointMoves)
        {
            List<GameTryMove> mustHaveNeutralMoves = neutralPointMoves.Where(n => n.MustHaveNeutralPoint).ToList();
            Board board = specificNeutralMove.TryGame.Board;
            Content c = specificNeutralMove.MoveContent;

            foreach (GameTryMove mustHaveNeutralMove in mustHaveNeutralMoves)
            {
                Board tryBoard = mustHaveNeutralMove.TryGame.Board;
                //specific neutral move and must have neutral move target same group
                if (tryBoard.GetNeighbourGroups(tryBoard.MoveGroup).Intersect(tryBoard.GetGroupsFromStoneNeighbours(board.Move.Value, c)).Any())
                {
                    Point liberty = mustHaveNeutralMove.LinkPoint.Move;
                    Board b = tryBoard.MakeMoveOnNewBoard(liberty, c);
                    if (b == null) continue;

                    //connect and die for target group
                    HashSet<Group> targetGroups = b.GetGroupsFromStoneNeighbours(b.Move.Value, c);
                    if (targetGroups.Count <= 1) continue;
                    if (targetGroups.Any(group => ImmovableHelper.CheckConnectAndDie(b, group)))
                        return mustHaveNeutralMove;
                }
            }
            return null;
        }

        /// <summary>
        /// Get specific neutral move to target survival groups with limited liberties.
        /// Two specific moves <see cref="UnitTestProject.SpecificNeutralMoveTest.SpecificNeutralMoveTest_Scenario_XuanXuanQiJing_Weiqi101_B51" />
        /// Pre-atari move <see cref="UnitTestProject.SpecificNeutralMoveTest.SpecificNeutralMoveTest_Scenario_TianLongTu_Q16594" />
        /// Check snapback <see cref="UnitTestProject.SpecificNeutralMoveTest.SpecificNeutralMoveTest_ScenarioHighLevel18" />
        /// </summary>
        public static GameTryMove GetSpecificNeutralMove(Game g, List<GameTryMove> neutralPointMoves)
        {
            GameTryMove gameTryMove;
            List<Group> killerGroups = BothAliveHelper.GetCorneredKillerGroup(g.Board);

            if (IsImmovableKill(g, killerGroups))
                gameTryMove = SpecificKillWithImmovablePoints(g.Board, neutralPointMoves, killerGroups[0]);
            else
                gameTryMove = SpecificKillWithLibertyFight(g.Board, neutralPointMoves, killerGroups);

            if (gameTryMove != null)
                return gameTryMove;

            //check pre-atari move
            gameTryMove = neutralPointMoves.FirstOrDefault(move => ImmovableHelper.PreAtariMove(move.TryGame.Board));
            return gameTryMove;

        }

        /// <summary>
        /// Conditions for specific kill with immovable points. <see cref="UnitTestProject.SpecificNeutralMoveTest.SpecificNeutralMoveTest_Scenario_XuanXuanGo_A54" />
        /// Covered eye liberty <see cref="UnitTestProject.SpecificNeutralMoveTest.SpecificNeutralMoveTest_Scenario_XuanXuanGo_A54_3" />
        /// </summary>
        public static Boolean IsImmovableKill(Game g, List<Group> killerGroups)
        {
            if (killerGroups.Count == 0) return false;
            Group killerGroup = killerGroups[0];
            //more than one neighbour group
            if (g.Board.GetNeighbourGroups(killerGroup).Count == 1) return false;
            List<Point> killerLiberties = killerGroups[0].Points.Where(p => g.Board[p] == Content.Empty).ToList();
            //ensure two killer liberties without covered eye
            if (killerLiberties.Count(liberty => !EyeHelper.FindCoveredEye(g.Board, liberty, killerGroup.Content)) != 2)
                return false;
            return true;
        }

        /// <summary>
        /// Specific kill with immovable points. At least two neighbour groups.
        /// Survival group has liberty less or equals to two <see cref="UnitTestProject.SpecificNeutralMoveTest.SpecificNeutralMoveTest_Scenario5dan27" />
        /// <see cref="UnitTestProject.SpecificNeutralMoveTest.SpecificNeutralMoveTest_Scenario_TianLongTu_Q16735" />
        /// At least one liberty shared with killer group <see cref="UnitTestProject.SpecificNeutralMoveTest.SpecificNeutralMoveTest_Scenario_XuanXuanGo_A54_2" />
        /// Check that survival cannot clear space <see cref="UnitTestProject.SpecificNeutralMoveTest.SpecificNeutralMoveTest_Scenario_XuanXuanGo_A54" />
        /// </summary>
        public static GameTryMove SpecificKillWithImmovablePoints(Board board, List<GameTryMove> neutralPointMoves, Group killerGroup)
        {
            //check for atari
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
                Content c = neutralPointMove.MoveContent;
                IEnumerable<Group> targetGroups = tryBoard.GetGroupsFromStoneNeighbours(move, c);
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
        /// Specific kill with liberty fight. No killer group or only one neighbour group.
        /// <see cref="UnitTestProject.SpecificNeutralMoveTest.SpecificNeutralMoveTest_Scenario3kyu24_3" />
        /// Real solid eye <see cref="UnitTestProject.SpecificNeutralMoveTest.SpecificNeutralMoveTest_Scenario3kyu24" />
        /// Crowbar formation <see cref="UnitTestProject.SpecificNeutralMoveTest.SpecificNeutralMoveTest_Scenario_TianLongTu_Q16827" />
        /// Only one neighbour group for killer group <see cref="UnitTestProject.SpecificNeutralMoveTest.SpecificNeutralMoveTest_Scenario_TianLongTu_Q2413" />
        /// </summary>
        public static GameTryMove SpecificKillWithLibertyFight(Board board, List<GameTryMove> neutralPointMoves, List<Group> killerGroups)
        {
            //no killer group or only one neighbour group
            if (!(killerGroups.Count == 0 || board.GetNeighbourGroups(killerGroups.First()).Where(group => group.Points.Count > 1).Count() <= 1))
                return null;

            Content c = neutralPointMoves.First().MoveContent;
            //all moves are valid if liberty fight
            GameTryMove neutralPointMove = neutralPointMoves.FirstOrDefault(t => t.TryGame.Board.GetGroupsFromStoneNeighbours(t.Move, c).Count > 0);
            if (neutralPointMove == null) return null;
            Board tryBoard = neutralPointMove.TryGame.Board;
            foreach (Group targetGroup in tryBoard.GetGroupsFromStoneNeighbours(neutralPointMove.Move, c))
            {
                List<Point> neighbourLiberties;
                if (killerGroups.Count == 0)
                {
                    //ensure only two neighbour groups
                    List<Group> neighbourGroups = tryBoard.GetNeighbourGroups(targetGroup);
                    if (neighbourGroups.Count != 2) return null;
                    if (neighbourGroups.Any(group => AtariHelper.AtariByGroup(tryBoard, group))) return null;
                    //get the group other than neutral point group
                    Group neighbourGroup = neighbourGroups.First(group => !group.Equals(tryBoard.GetGroupAt(neutralPointMove.Move)));
                    neighbourLiberties = neighbourGroup.Liberties.ToList();
                }
                else
                {
                    //include all empty points within killer group
                    neighbourLiberties = killerGroups.First().Points.Where(p => tryBoard[p] == Content.Empty).ToList();
                }

                //compare liberties to see if target group can be killed
                if (neighbourLiberties.Count == targetGroup.Liberties.Count + 1)
                    return neutralPointMove;
                else if (neighbourLiberties.Count == targetGroup.Liberties.Count)
                {
                    //killer group available
                    if (killerGroups.Count > 0) return neutralPointMove;
                    //real solid eye found
                    if (neighbourLiberties.Any(liberty => BothAliveHelper.GetKillerGroupFromCache(tryBoard, liberty, c) != null))
                        return neutralPointMove;
                }
            }
            return null;
        }

        /// <summary>
        /// Ensure target group of neutral move is not already targeted by other try moves.
        /// </summary>
        public static Boolean CheckIfGroupAlreadyTargeted(GameTryMove neutralMove, List<GameTryMove> tryMoves)
        {
            if (tryMoves == null) return false;
            Board tryBoard = neutralMove.TryGame.Board;
            Content c = neutralMove.MoveContent;
            //get target groups of neutral move
            HashSet<Group> groups = tryBoard.GetGroupsFromStoneNeighbours(neutralMove.Move, c);

            foreach (GameTryMove tryMove in tryMoves)
            {
                //exclude try moves within killer group
                if (BothAliveHelper.GetKillerGroupFromCache(tryBoard, tryMove.Move) != null)
                    continue;

                //target group already targeted by other try moves
                HashSet<Group> neighbourGroups = tryBoard.GetGroupsFromStoneNeighbours(tryMove.Move, c);
                if (neighbourGroups.Intersect(groups).Any())
                    return true;
            }
            return false;
        }


        /// <summary>
        /// Get generic neutral moves that are not specific and target group not targeted by other try moves. Killer group required.
        /// One neighbour group <see cref="UnitTestProject.GenericNeutralMoveTest.GenericNeutralMoveTest_Scenario_XuanXuanGo_Q18500" />
        /// More than one neighbour group <see cref="UnitTestProject.GenericNeutralMoveTest.GenericNeutralMoveTest_Scenario5dan27_2" />
        /// Get all extended groups <see cref="UnitTestProject.GenericNeutralMoveTest.GenericNeutralMoveTest_Scenario_XuanXuanGo_Q18340" />
        /// Get all groups including eyes <see cref="UnitTestProject.GenericNeutralMoveTest.GenericNeutralMoveTest_Scenario4dan17_2" />
        /// </summary>
        public static GameTryMove GetGenericNeutralMove(Game g, List<GameTryMove> tryMoves, List<GameTryMove> neutralPointMoves)
        {
            //check for killer group
            List<Group> killerGroups = BothAliveHelper.GetCorneredKillerGroup(g.Board, false);
            if (killerGroups.Count == 0) return null;
            Group killerGroup = killerGroups[0];

            //get all extended neighbour groups
            HashSet<Group> groups = new HashSet<Group>();
            List<Group> neighbourGroups = g.Board.GetNeighbourGroups(killerGroup);
            neighbourGroups.ForEach(gp => LinkHelper.GetAllDiagonalConnectedGroups(g.Board, gp, groups));

            //get all groups including eyes
            groups = LinkHelper.GetAllDiagonalConnectedGroupsIncludingEyes(g.Board, groups);

            //cover all neutral points
            Board coveredBoard = new Board(g.Board);
            neutralPointMoves.ForEach(m => coveredBoard[m.Move] = killerGroup.Content);

            //order by inner liberties of neighbour group
            List<Group> orderedGroups = groups.OrderBy(n => coveredBoard.GetGroupLiberties(n)).ToList();

            //get liberties by order
            HashSet<Point> libertyPoints = g.Board.GetLibertiesOfGroups(orderedGroups);

            //get neutral points of killer group neighbours
            neutralPointMoves = neutralPointMoves.Where(n => libertyPoints.Contains(n.Move)).ToList();
            foreach (Point p in libertyPoints)
            {
                GameTryMove neutralMove = neutralPointMoves.FirstOrDefault(n => n.Move.Equals(p));
                if (neutralMove == null) continue;

                //ensure target group has two or less liberties
                Board b = neutralMove.TryGame.Board;
                HashSet<Group> stoneNeighbours = coveredBoard.GetGroupsFromStoneNeighbours(b.Move.Value, b.MoveGroup.Content);
                if (WallHelper.StrongNeighbourGroups(coveredBoard, stoneNeighbours))
                    continue;

                //restore the first generic neutral move if neighbour group not targeted by other try moves
                if (CheckIfGroupAlreadyTargeted(neutralMove, tryMoves)) continue;
                return neutralMove;
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

            if (RedundantTigerMouth(opponentMove, true))
                return true;

            return false;
        }

        /// <summary>
        /// Check eye points at diagonals of tiger mouth. If all eye points are tiger mouth then is redundant. <see cref="UnitTestProject.SurvivalTigerMouthMoveTest.SurvivalTigerMouthMoveTest_Scenario_XuanXuanGo_B31" />
        /// <see cref="UnitTestProject.SurvivalTigerMouthMoveTest.SurvivalTigerMouthMoveTest_Scenario_TianLongTu_Q16827" />
        /// <see cref="UnitTestProject.SurvivalTigerMouthMoveTest.SurvivalTigerMouthMoveTest_Scenario_GuanZiPu_Q18860" />
        /// <see cref="UnitTestProject.SurvivalTigerMouthMoveTest.RedundantTigerMouthMove_Scenario_WuQingYuan_Q15126" />
        /// Check two point atari move
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanGo_A82_101Weiqi" />
        /// Check corner three formation <see cref="UnitTestProject.SurvivalTigerMouthMoveTest.RedundantTigerMouthMove_Scenario_GuanZiPu_Q18860" />
        /// Find real eyes at both spaces <see cref="UnitTestProject.SurvivalTigerMouthMoveTest.RedundantTigerMouthMove_Scenario_Nie4" />
        /// </summary>
        private static Boolean RedundantTigerMouth(GameTryMove tryMove, Boolean isOpponent = false)
        {
            Point move = tryMove.Move;
            Board tryBoard = tryMove.TryGame.Board;
            Board currentBoard = tryMove.CurrentGame.Board;
            Content c = tryMove.MoveContent;
            //ensure is tiger mouth
            Board capturedBoard = ImmovableHelper.IsConfirmTigerMouth(currentBoard, tryBoard);
            if (capturedBoard == null) return false;

            //check eye points at diagonals of tiger mouth
            List<Point> libertyPoint = tryBoard.GetStoneNeighbours().Where(n => tryBoard[n] != c.Opposite()).ToList();
            if (libertyPoint.Count != 1) return false;
            List<Point> eyePoints = TigerMouthEyePoints(tryBoard, move, libertyPoint.First()).Where(e => tryBoard[e] != c.Opposite()).ToList();
            if (eyePoints.Count == 0) return false;

            //check if eye point is tiger mouth 
            if (eyePoints.Any(eyePoint => ImmovableHelper.IsConfirmTigerMouth(currentBoard, tryBoard, eyePoint) != null))
                return true;

            Group moveKillerGroup = BothAliveHelper.GetKillerGroupFromCache(currentBoard, move, c.Opposite());
            //check if eye point is killer group
            foreach (Point eyePoint in eyePoints)
            {
                Group killerGroup = BothAliveHelper.GetKillerGroupFromCache(currentBoard, eyePoint, c.Opposite());
                if (killerGroup == null) continue;
                //check two point atari move
                if (TwoPointAtariMove(tryBoard, capturedBoard)) continue;
                //check corner three formation
                if (KillerFormationHelper.CornerThreeFormation(tryBoard, tryBoard.MoveGroup)) continue;
                if (moveKillerGroup == null)
                {
                    //ensure killer group is empty
                    List<Point> contentPoints = killerGroup.Points.Where(t => currentBoard[t] == killerGroup.Content).ToList();
                    if (contentPoints.Count == 0) return true;
                    //find real eye at diagonal
                    if (EyeHelper.FindRealEyeWithinEmptySpace(currentBoard, killerGroup)) return true;
                }
                else
                {
                    //find real eyes at both spaces
                    if (EyeHelper.FindRealEyeWithinEmptySpace(currentBoard, killerGroup) && EyeHelper.FindRealEyeWithinEmptySpace(capturedBoard, moveKillerGroup)) return true;
                }
            }
            return false;
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
        /// Ensure diagonal not required for both alive. 
        /// <see cref="UnitTestProject.RedundantEyeDiagonalMoveTest.RedundantEyeDiagonalMoveTestScenario_Scenario_SiHuoDaQuan_CornerA29" />
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
            List<Point> diagonals = tryBoard.GetDiagonalNeighbours(move.x, move.y).Where(q => tryBoard[q] != c).ToList();
            diagonals = diagonals.Where(eye => LinkHelper.PointsBetweenDiagonals(eye, move).All(d => tryBoard[d] == c)).ToList();
            if (diagonals.Count == 0)
                return false;

            diagonals.RemoveAll(d => BothAliveHelper.GetKillerGroupFromCache(currentBoard, d) == null);
            if (diagonals.Count == 0) return false;
            //if all diagonals are real eyes then redundant
            if (diagonals.All(eye => RealEyeAtDiagonal(tryMove, eye)))
            {
                //check other surrounding points are not possible eyes
                IEnumerable<Point> neighbourPts = tryBoard.GetStoneAndDiagonalNeighbours(move.x, move.y).Except(diagonals);
                if (neighbourPts.Any(q => !WallHelper.NoEyeForSurvival(tryBoard, q)))
                    return false;

                //check link to groups other than eye groups
                List<Group> linkedGroups = LinkHelper.GetPreviousMoveGroup(currentBoard, tryBoard);
                List<Group> eyeNeighbourGroups = new List<Group>();
                diagonals.ForEach(d => eyeNeighbourGroups.AddRange(currentBoard.GetGroupsFromStoneNeighbours(d, c.Opposite())));
                eyeNeighbourGroups = eyeNeighbourGroups.Distinct().ToList();
                if (linkedGroups.Any(group => !eyeNeighbourGroups.Contains(group)))
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

        /// <summary>
        /// Real eye at diagonal with empty point should be semi solid eye or within enclosed killer group. If eye is filled then check if possible to create real eye.
        /// Eye filled <see cref="UnitTestProject.RedundantEyeDiagonalMoveTest.RedundantEyeDiagonalMoveTestScenario_XuanXuanGo_A46_101Weiqi" />
        /// Check if covered eye <see cref="UnitTestProject.RedundantEyeDiagonalMoveTest.RedundantEyeDiagonalMoveTest_Scenario_GuanZiPu_A2Q29_101Weiqi" />
        /// </summary>
        public static Boolean RealEyeAtDiagonal(GameTryMove tryMove, Point eye)
        {
            GameInfo gameInfo = tryMove.TryGame.GameInfo;
            GameTryMove opponentMove = tryMove.MakeMoveWithOpponentAtSamePoint();
            if (opponentMove == null) return false;
            Board currentBoard = tryMove.CurrentGame.Board;
            Board killerBoard = opponentMove.TryGame.Board;

            //check if eye is within enclosed killer group
            Group eyeGroup = BothAliveHelper.GetKillerGroupFromCache(currentBoard, eye);
            if (eyeGroup == null) return false;

            if (EyeHelper.FindRealEyeWithinEmptySpace(killerBoard, eyeGroup, EyeType.SemiSolidEye))
                return true;

            if (DiagonalRedundancy(tryMove, eye, eyeGroup))
                return true;

            return false;
        }

        /// <summary>
        /// Redundant eye diagonal move.
        /// Two point empty group <see cref="UnitTestProject.RedundantEyeDiagonalMoveTest.RedundantEyeDiagonalMoveTest_Scenario_XuanXuanGo_Q18331" />
        /// </summary>
        private static Boolean DiagonalRedundancy(GameTryMove tryMove, Point eye, Group eyeGroup)
        {
            Board currentBoard = tryMove.CurrentGame.Board;
            int eyeGroupCount = eyeGroup.Points.Count;
            GameInfo gameInfo = tryMove.TryGame.GameInfo;
            Content c = GameHelper.GetContentForSurviveOrKill(gameInfo, SurviveOrKill.Survive);
            if (eyeGroupCount == 2)
            {
                //diagonal eye empty
                List<Point> oppositeContent = eyeGroup.Points.Where(p => currentBoard[p] == c.Opposite()).ToList();
                if (oppositeContent.Count == 1 && !oppositeContent.First().Equals(eye))
                    return true;
                //two-point empty group
                if (EyeHelper.FindRealEyesWithinTwoEmptyPoints(currentBoard, eyeGroup, EyeType.SemiSolidEye) != null)
                    return true;
            }
            return false;
        }

        #endregion

        #region eye filler
        /// <summary>
        /// Survival eye filler moves. Get specific move for group not more than five points and not filled. 
        /// </summary>
        public static Boolean SurvivalEyeFillerMove(GameTryMove tryMove)
        {
            if (!tryMove.IsNegligible)
                return false;
            Board currentBoard = tryMove.CurrentGame.Board;
            Group killerGroup = BothAliveHelper.GetKillerGroupFromCache(currentBoard, tryMove.Move);
            if (killerGroup == null) return FillerMoveWithoutKillerGroup(tryMove);

            //check if any move in killer group
            if (killerGroup.Points.Count <= 5)
                return SpecificEyeFillerMove(tryMove);
            else
                return GenericEyeFillerMove(tryMove);
        }

        /// <summary>
        /// Kill eye filler moves valid within only small space about five points. 
        /// </summary>
        public static Boolean KillEyeFillerMove(GameTryMove tryMove)
        {
            if (!tryMove.IsNegligible)
                return false;
            Board currentBoard = tryMove.CurrentGame.Board;
            Group killerGroup = BothAliveHelper.GetKillerGroupFromCache(currentBoard, tryMove.Move);
            if (killerGroup == null) return FillerMoveWithoutKillerGroup(tryMove);
            if (killerGroup.Points.Count > 5) return (FillerMoveWithoutKillerGroup(tryMove, true));

            GameTryMove opponentMove = tryMove.MakeMoveWithOpponentAtSamePoint();
            if (opponentMove == null) return false;
            return SpecificEyeFillerMove(opponentMove, true);
        }

        /// <summary>
        /// Filler moves without killer group. <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_WuQingYuan_Q6150" />
        /// Filler moves with killer group <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_GuanZiPu_B3_2" />
        /// Check for one point leap move <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_XuanXuanGo_B10_2" />
        /// <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_Corner_B40" />
        /// Check two-point group <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_Side_B35" />
        /// Check if killer group created with opposite content within the group <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_XuanXuanGo_A171_101Weiqi" />
        /// </summary>
        private static Boolean FillerMoveWithoutKillerGroup(GameTryMove tryMove, Boolean isKillerGroup = false)
        {
            Board tryBoard = tryMove.TryGame.Board;
            Point move = tryBoard.Move.Value;
            Content c = tryMove.MoveContent;
            //check for one point leap move
            if (tryBoard.MoveGroup.Points.Count == 1 && !tryBoard.CornerPoint(move))
            {
                Boolean singlePoint = tryBoard.GetStoneAndDiagonalNeighbours(move.x, move.y).All(n => tryBoard[n] == Content.Empty);
                if (singlePoint)
                {
                    //check survival move
                    if (SiegedScenario(tryBoard, tryBoard.GetClosestNeighbour(move, 2), 1)) return false;
                    //check kill move
                    List<Point> oppositeStones = tryBoard.GetClosestNeighbour(move, 2, c.Opposite());
                    if (SiegedScenario(tryBoard, oppositeStones))
                        return false;
                }
            }

            //check two-point group
            if (tryBoard.MoveGroup.Points.Count == 2 && LinkHelper.GetGroupLinkedDiagonals(tryBoard, tryBoard.MoveGroup, false).Count == 0)
            {
                List<Point> neighbours = tryBoard.GetClosestNeighbour(move, 2).Except(tryBoard.MoveGroup.Points).ToList();
                if (SiegedScenario(tryBoard, neighbours))
                    return false;
            }

            //check for opposite content
            if (CheckOppositeContentForFillerMove(tryMove, isKillerGroup))
                return false;

            //check if killer group created with opposite content within the group
            if (tryMove.IncreasedKillerGroups)
            {
                IEnumerable<Group> createdGroups = BothAliveHelper.GetCorneredKillerGroup(tryBoard, false).Except(BothAliveHelper.GetCorneredKillerGroup(tryMove.CurrentGame.Board, false));
                if (createdGroups.Any(group => group.Points.Any(p => tryBoard[p] == group.Content)))
                    return false;
            }

            return GenericEyeFillerMove(tryMove, isKillerGroup);
        }

        /// <summary>
        /// Check opposite content for filler move.
        /// Ensure no opposite content at stone and diagonal <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_WindAndTime_Q29998_2" />
        /// Ensure no opposite content at stone neighbour points for killer group <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_TianLongTu_Q15017" />
        /// Closest neighbour within killer group <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_WuQingYuan_Q31537_2" />
        /// <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_WuQingYuan_Q31445" />
        /// </summary>
        private static Boolean CheckOppositeContentForFillerMove(GameTryMove tryMove, Boolean isKillerGroup = false)
        {
            Board tryBoard = tryMove.TryGame.Board;
            Point move = tryBoard.Move.Value;
            Content c = tryMove.MoveContent;
            if (!isKillerGroup)
            {
                //ensure no opposite content at stone and diagonal
                if (tryBoard.GetStoneAndDiagonalNeighbours(tryMove.Move.x, tryMove.Move.y).Any(n => tryBoard[n] == c.Opposite()))
                    return true;
            }
            else
            {
                //ensure no opposite content at stone neighbour points for killer group or any closest neighbour within killer group
                if (tryBoard.GetStoneNeighbours(tryMove.Move.x, tryMove.Move.y).Any(n => tryBoard[n] == c.Opposite() && tryBoard.GetGroupLiberties(n) <= 2) || tryBoard.GetClosestNeighbour(tryMove.Move, 2).Any(n => BothAliveHelper.GetKillerGroupFromCache(tryBoard, n, c.Opposite()) != null))
                    return true;
            }
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
        /// Check any opponent stone at neighbour points <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_TianLongTu_Q16827" />
        /// <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_TianLongTu_Q16827_2" />
        /// </summary>
        public static Boolean GenericEyeFillerMove(GameTryMove tryMove, Boolean isKillerGroup = false)
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

            //get stone neighbours only for killer group
            List<Point> stoneNeighbours = (isKillerGroup) ? tryBoard.GetStoneNeighbours() : tryBoard.GetStoneAndDiagonalNeighbours(move.x, move.y);
            List<Point> emptyNeighbours = stoneNeighbours.Where(p => tryBoard[p] == Content.Empty).ToList();
            Content cc = (isKillerGroup ? c.Opposite() : c);
            //count eyes created at move
            int possibleEyes = PossibleEyesCreated(currentBoard, move, cc);

            foreach (Point p in emptyNeighbours)
            {
                //count eyes created at empty neighbour points
                int possibleEyesAtNeighbourPt = PossibleEyesCreated(currentBoard, p, cc);
                //check if possibility of eyes created by move at any stone neighbour is more than at try move point
                if (possibleEyesAtNeighbourPt <= possibleEyes) continue;
                if (tryBoard.CornerPoint(p)) continue;
                //corner point for try move
                if (CheckRedundantCornerPoint(tryMove))
                    return true;

                //check any opponent stone at neighbour points
                if (!isKillerGroup && CheckOpponentStoneAtFillerMove(tryMove, p))
                    continue;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Check opponent stone at filler move. <see cref="UnitTestProject.RedundantEyeFillerTest.BaseLineKillerMoveTest_Scenario_TianLongTu_Q16520" />
        /// <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_Corner_A80_2" />
        /// Check for opponent stone at try move <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_TianLongTu_Q16827_2" />
        /// </summary>
        private static Boolean CheckOpponentStoneAtFillerMove(GameTryMove tryMove, Point p)
        {
            Point move = tryMove.TryGame.Board.Move.Value;
            Board currentBoard = tryMove.CurrentGame.Board;
            Content c = tryMove.MoveContent;
            List<Point> stoneNeighbours = currentBoard.GetStoneNeighbours(p.x, p.y).Where(n => currentBoard[n] == c.Opposite()).ToList();
            if (stoneNeighbours.Count > 1 || stoneNeighbours.Any(n => currentBoard.GetGroupAt(n).Points.Count > 1))
                return true;

            //check for opponent stone at try move
            if (stoneNeighbours.Count == 1 && currentBoard.GetGroupAt(stoneNeighbours.First()).Points.Count == 1)
            {
                if (currentBoard.GetStoneNeighbours(move.x, move.y).Any(n => currentBoard[n] == c.Opposite()))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Check redundant corner point.
        /// <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_Corner_A95" />
        /// Two point kill <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_WuQingYuan_Q16508" />
        /// <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_Corner_A6" />
        /// Check for kill formation <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_XuanXuanQiJing_Weiqi101_7245" />
        /// </summary>
        private static Boolean CheckRedundantCornerPoint(GameTryMove tryMove)
        {
            Board tryBoard = tryMove.TryGame.Board;
            Point move = tryBoard.Move.Value;
            Content c = tryBoard[move];
            if (!tryBoard.CornerPoint(move) || tryBoard.IsAtariMove || !tryMove.IsNegligible) return false;
            //Two point kill
            Boolean twoPointKill = (tryBoard.MoveGroup.Points.Count == 2 && tryBoard.MoveGroupLiberties <= 2 && tryBoard.GetStoneNeighbours().Any(q => tryBoard[q] == Content.Empty));
            if (twoPointKill) return false;

            //check for kill formation
            if (tryBoard.IsSinglePoint() && tryBoard.MoveGroupLiberties == 2)
            {
                Boolean killFormation = (tryBoard.GetClosestNeighbour(move, 2, c.Opposite()).Count >= 3 && !tryBoard.GetClosestNeighbour(move, 2, c).Any());
                if (killFormation) return false;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Link for groups where diagonal is non killable.
        /// Opponent stones at diagonal <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_TianLongTu_Q17077" />
        /// <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_XuanXuanGo_A82_101Weiqi" />
        /// <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_Corner_A132" />
        /// <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_WuQingYuan_Q30919" />
        /// Opponent stones at neighbour points <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_XuanXuanGo_B10_3" />
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
            //ensure link for groups
            if (!LinkHelper.IsAbsoluteLinkForGroups(currentBoard, tryBoard)) return false;

            //check for opponent stones at neighbour points
            Point move = tryBoard.Move.Value;
            if (tryBoard.GetStoneNeighbours().Where(n => tryBoard[n] == c.Opposite()).Any())
                return true;

            //check for opponent stones at diagonal
            List<Point> diagonals = tryBoard.GetDiagonalNeighbours(move.x, move.y);
            List<Point> filledDiagonals = diagonals.Where(d => currentBoard[d] == c.Opposite()).ToList();
            if (filledDiagonals.Count > 0)
                return true;

            return false;
        }

        /// <summary>
        /// Return specific survival or killer move if killer group contains five points or less. 
        /// Neighbour groups liberty more than one <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_GuanZiPu_A37" />
        /// Check immovable at liberties <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_WuQingYuan_Q31602" />
        /// Not link for groups <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_WuQingYuan_Q31537" />
        /// Prevent survival creating eye <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_Corner_A61" />
        /// <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_WuQingYuan_Q31428" />
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
            GameTryMove opponentMove = tryMove.MakeMoveWithOpponentAtSamePoint();
            if (opponentMove != null && !opponentMove.IsNegligible) return false;

            Group killerGroup = BothAliveHelper.GetKillerGroupFromCache(currentBoard, move);
            if (killerGroup == null)
                return false;

            //neighbour groups should have liberty more than one
            if (AtariHelper.AtariByGroup(currentBoard, killerGroup))
                return false;

            //check for ko fight
            if (EyeFillerKo(tryMove))
                return false;

            List<Point> emptyNeighbours = killerGroup.Points.Where(p => currentBoard[p] == Content.Empty).ToList();
            //remove move with no neighbour group
            List<Group> neighbourGroups = currentBoard.GetNeighbourGroups(killerGroup);
            if (neighbourGroups.Count > 1)
            {
                Point noNeighbourPoint = emptyNeighbours.FirstOrDefault(m => currentBoard.GetStoneNeighbours(m.x, m.y).Count(p => currentBoard[p] == c) == 0);
                if (Convert.ToBoolean(noNeighbourPoint.NotEmpty)) return false;
            }

            //count possible eyes created
            Dictionary<Point, int> fillerMoves = new Dictionary<Point, int>();
            emptyNeighbours.ForEach(p => fillerMoves.Add(p, PossibleEyesCreated(currentBoard, p, c)));
            int maxPossibleEyes = fillerMoves.Max(f => f.Value);
            List<Point> bestMoves = fillerMoves.Where(m => m.Value == maxPossibleEyes).Select(f => f.Key).ToList();

            Dictionary<Point, Board> killBoards = new Dictionary<Point, Board>();
            foreach (Point p in emptyNeighbours)
            {
                if (!bestMoves.Contains(p)) continue;
                Board b = currentBoard.MakeMoveOnNewBoard(p, c.Opposite(), true);
                if (b == null) continue;
                killBoards.Add(p, b);
            }
            //check immovable at liberties
            KeyValuePair<Point, Board> immovableAtLiberties = killBoards.FirstOrDefault(b => b.Value.MoveGroupLiberties == 2 && b.Value.MoveGroup.Liberties.All(m => ImmovableHelper.IsSuicidalMoveForBothPlayers(b.Value, m)) && b.Value.GetNeighbourGroups(b.Value.MoveGroup).Count > 1);
            if (immovableAtLiberties.Value != null)
                return !tryMove.Move.Equals(immovableAtLiberties.Key);

            //ensure not link for groups
            if (EyeFillerLinkForGroups(tryMove))
                return false;

            //select max count only
            if (bestMoves.Count == 1)
                return !tryMove.Move.Equals(bestMoves.First());

            //select move that prevent survival creating eye
            Boolean eyeCreated = tryBoard.GetStoneNeighbours().Any(n => EyeHelper.FindSemiSolidEyes(n, tryBoard, c).Item1);
            if (eyeCreated) return false;

            foreach (Point p in bestMoves)
            {
                Board b = currentBoard.MakeMoveOnNewBoard(p, c, true);
                if (b == null) continue;
                if (b.GetStoneNeighbours().Any(n => EyeHelper.FindSemiSolidEyes(n, b, c).Item1))
                    return !tryMove.Move.Equals(p);
            }

            //select move with max binding
            Point bestMove = KillerFormationHelper.GetMaxBindingPoint(currentBoard, killBoards.Values).Move;
            return !tryMove.Move.Equals(bestMove);
        }

        /// <summary>
        /// Check if killer can make ko fight.
        /// <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_XuanXuanGo_B12" />
        /// <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_Corner_A67_2" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WindAndTime_Q29277" />
        /// Survival ko <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_WuQingYuan_Q31498" />
        /// Ko fight without killer group <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_GuanZiPu_A36_3" />
        /// <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_Corner_A67" />
        /// </summary>
        public static Boolean EyeFillerKo(GameTryMove tryMove)
        {
            Board tryBoard = tryMove.TryGame.Board;
            Point move = tryBoard.Move.Value;
            Content c = tryBoard[move];

            //check to ensure is ko
            List<Point> eyePoints = tryBoard.GetStoneNeighbours().Where(n => EyeHelper.FindNonSemiSolidEye(tryBoard, n, c)).ToList();
            foreach (Point eyePoint in eyePoints)
            {
                //ko fight without killer group
                Board b = tryBoard.MakeMoveOnNewBoard(eyePoint, c.Opposite(), true);
                if (b != null && KoHelper.IsKoFight(b))
                    return true;
            }

            //check to ensure group has only one liberty
            Boolean killKoEnabled = KoHelper.KoSurvivalEnabled(SurviveOrKill.Kill, tryMove.TryGame.GameInfo);
            if (killKoEnabled)
            {
                GameTryMove opponentMove = tryMove.MakeMoveWithOpponentAtSamePoint();
                if (opponentMove == null) return false;
                tryBoard = opponentMove.TryGame.Board;
                c = tryBoard[tryBoard.Move.Value];
                Group killerGroup = BothAliveHelper.GetKillerGroupFromCache(tryBoard, tryMove.Move, c.Opposite());
                if (killerGroup == null) return false;
                eyePoints = tryBoard.GetStoneNeighbours().Where(n => EyeHelper.FindNonSemiSolidEye(tryBoard, n, c)).ToList();
                if (eyePoints.Count == 0) return false;

                Board coveredBoard = BothAliveHelper.FillEyePointsBoard(tryBoard, killerGroup);
                //group has one or no liberties
                if (coveredBoard.GetGroupLiberties(move) <= 1)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Return number of possible eyes that can be created at stone neighbour points.
        /// </summary>
        public static int PossibleEyesCreated(Board currentBoard, Point p, Content c)
        {
            List<Point> stoneNeighbours = currentBoard.GetStoneNeighbours(p.x, p.y);
            List<Point> possibleEyes = stoneNeighbours.Where(n => currentBoard[n] != c).ToList();
            return possibleEyes.Count;
        }

        #endregion

        #region redundant ko
        /// <summary>
        /// Redundant ko moves from the perspective of the survival are ko moves that serve no purpose to its objective of survival.
        /// RedundantSurvivalPreKoMove <see cref="UnitTestProject.RedundantKoMoveTest.RedundantKoMoveTest_Scenario_XuanXuanGo_A46_101Weiqi_2" />
        /// Double ko recursion <see cref="UnitTestProject.CheckForRecursionTest.CheckForRecursionTest_Scenario_Corner_B41" />
        /// </summary>
        public static Boolean RedundantSurvivalPreKoMove(GameTryMove tryMove)
        {
            if (tryMove.IsKoFight)
                return RedundantSurvivalKoMove(tryMove);
            return false;
        }

        /// <summary>
        /// RedundantSurvivalKoMove <see cref="UnitTestProject.RedundantKoMoveTest.RedundantKoMoveTest_Scenario_XuanXuanGo_A46_101Weiqi" />
        /// <see cref="UnitTestProject.RedundantKoMoveTest.RedundantKoMoveTest_SimpleSeki" />
        /// </summary>
        public static Boolean RedundantSurvivalKoMove(GameTryMove tryMove)
        {
            Boolean koEnabled = KoHelper.KoSurvivalEnabled(SurviveOrKill.Survive, tryMove.CurrentGame.GameInfo);
            if (!koEnabled && !PossibilityOfDoubleKo(tryMove)) return true;
            if (!tryMove.IsNegligibleForKo)
                return false;
            return CheckRedundantKo(tryMove);
        }
        /// <summary>
        /// RedundantKillerPreKoMove <see cref="UnitTestProject.RedundantKoMoveTest.RedundantKillerKoMoveTest_Scenario_XuanXuanGo_A46_101Weiqi_2" />
        /// Added as neutral point if found redundant ko.
        /// </summary>
        public static Boolean RedundantKillerPreKoMove(GameTryMove tryMove)
        {
            if (tryMove.IsKoFight)
                return RedundantKillerKoMove(tryMove);
            return false;
        }

        /// <summary>
        /// RedundantKillerKoMove <see cref="UnitTestProject.RedundantKoMoveTest.RedundantKillerKoMoveTest_Scenario_XuanXuanGo_A46_101Weiqi" />
        /// Check if redundant ko from point of view of survival.
        /// </summary>
        public static Boolean RedundantKillerKoMove(GameTryMove tryMove)
        {
            Board tryBoard = tryMove.TryGame.Board;
            Boolean koEnabled = KoHelper.KoSurvivalEnabled(SurviveOrKill.Kill, tryMove.CurrentGame.GameInfo);
            if (!koEnabled && !PossibilityOfDoubleKo(tryMove)) return true;
            if (!tryMove.IsNegligibleForKo)
                return false;
            Content c = tryMove.MoveContent;

            //make move as survival ko
            Point eyePoint = tryBoard.GetStoneNeighbours().FirstOrDefault(n => EyeHelper.FindEye(tryBoard, n, c));
            if (!Convert.ToBoolean(eyePoint.NotEmpty)) return false;

            GameTryMove move = new GameTryMove(tryMove.TryGame);
            move.TryGame.Board.InternalMakeMove(eyePoint, c.Opposite(), true);
            if (!move.IsNegligibleForKo)
                return false;
            return CheckRedundantKo(move);
        }

        /// <summary>
        /// Check for possibility of double ko, for both survival and kill. Check for end ko moves as well.
        /// Survival double ko <see cref="UnitTestProject.CheckForRecursionTest.CheckForRecursionTest_Scenario_TianLongTu_Q16446" />
        /// Kill double ko <see cref="UnitTestProject.RedundantKoMoveTest.RedundantKoMoveTest_Scenario_Corner_A23" />
        /// </summary>
        public static Boolean PossibilityOfDoubleKo(GameTryMove tryMove)
        {
            Board currentBoard = tryMove.CurrentGame.Board;
            Board tryBoard = tryMove.TryGame.Board;
            //allow pre-ko moves without capture
            if (tryBoard.singlePointCapture == null) return true;
            //get all target connected groups
            HashSet<Group> groups = LifeCheck.GetTargetConnectedGroups(currentBoard);
            Boolean isSurvive = (GameHelper.GetContentForSurviveOrKill(currentBoard.GameInfo, SurviveOrKill.Survive) == tryMove.MoveContent);
            HashSet<Group> targetGroups = new HashSet<Group>(groups);
            if (!isSurvive)
            {
                //include all stones at eye point
                targetGroups = LinkHelper.GetAllDiagonalConnectedGroupsIncludingEyes(currentBoard, targetGroups);
            }
            //check for another ko other than current ko move
            foreach (Group group in targetGroups)
            {
                if (isSurvive)
                {
                    if (AtariHelper.KoAtariByNeighbour(currentBoard, group, tryBoard.singlePointCapture.Value).Item1)
                        return true;
                }
                else
                {
                    if (group.Points.Count == 1 && !group.Points.Contains(tryBoard.singlePointCapture.Value))
                    {
                        Board b = ImmovableHelper.CaptureSuicideGroup(currentBoard, group);
                        if (b != null && KoHelper.IsKoFight(b))
                            return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Check if redundant ko from point of view of survival. Check ko move not required to create eyes at the two diagonals of ko eye opposite of ko move direction.
        /// ko fight necessary (avoid use of atari resolved) <see cref="UnitTestProject.RedundantKoMoveTest.RedundantKoMoveTest_Scenario2kyu18" /> 
        /// <see cref="UnitTestProject.RedundantKoMoveTest.RedundantKoMoveTest_Scenario_XuanXuanQiJing_Weiqi101_B74" />
        /// <see cref="UnitTestProject.RedundantKoMoveTest.RedundantKoMoveTest_Scenario_Corner_A62" /> 
        /// <see cref="UnitTestProject.RedundantKoMoveTest.RedundantKoMoveTest_Scenario_Nie20" /> 
        /// <see cref="UnitTestProject.RedundantKoMoveTest.RedundantKoMoveTest_Scenario_TianLongTu_Q2413" /> 
        /// Real eye at diagonal <see cref="UnitTestProject.RedundantKoMoveTest.RedundantKoMoveTest_Scenario_GuanZiPu_A4Q11_101Weiqi" /> 
        /// </summary>
        public static Boolean CheckRedundantKo(GameTryMove tryMove, Boolean isNeutralMove = false)
        {
            Board tryBoard = tryMove.TryGame.Board;
            Point move = tryMove.Move;
            Point capturedPoint;
            Content c = tryMove.MoveContent;
            if (tryBoard.singlePointCapture != null)
                capturedPoint = tryBoard.singlePointCapture.Value;
            else
            {
                //pre ko moves and neutral moves
                capturedPoint = tryBoard.GetStoneNeighbours().FirstOrDefault(n => EyeHelper.FindEye(tryBoard, n.x, n.y, c) || ImmovableHelper.FindTigerMouth(tryBoard, c, n));
                if (!Convert.ToBoolean(capturedPoint.NotEmpty)) return true;
            }

            //check diagonals opposite of ko move direction are filled with same content
            List<Point> diagonals = RedundantMoveHelper.TigerMouthEyePoints(tryBoard, capturedPoint, move).Where(q => tryBoard[q] != c).ToList();
            if (diagonals.Count == 0)
            {
                //check that ko fight is necessary
                List<Group> ngroups = tryBoard.GetGroupsFromStoneNeighbours(capturedPoint, c.Opposite()).ToList();
                ngroups.RemoveAll(ngroup => ngroup.Points.Contains(move));
                if (ngroups.Count == 1 && tryBoard.GetNeighbourGroups(ngroups.First()).Any(group => group.Liberties.Count <= 2 && !WallHelper.IsNonKillableGroup(tryBoard, group)))
                    return false;
            }

            //if is neutral move and eye is empty point then ensure killer group is all empty points
            if (isNeutralMove)
            {
                Point eye = diagonals.FirstOrDefault(d => tryBoard[d] == Content.Empty);
                if (Convert.ToBoolean(eye.NotEmpty))
                {
                    Group killerGroup = BothAliveHelper.GetKillerGroupFromCache(tryBoard, eye);
                    if (killerGroup != null && !killerGroup.Points.All(q => tryBoard[q] == Content.Empty))
                        return false;
                }
            }

            //if all diagonals are real eyes then redundant
            if (!diagonals.All(eye => RedundantMoveHelper.RealEyeAtDiagonal(tryMove, eye)))
                return false;

            return true;
        }

        #endregion
    }
}
