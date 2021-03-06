using System;
using System.Collections.Generic;
using System.Linq;

namespace Go
{
    public class ImmovableHelper
    {
        /// <summary>
        /// Eye point that is immovable to opponent.
        /// </summary>
        public static Boolean SinglePointOpponentImmovable(GameTryMove tryMove)
        {
            return EyeHelper.FindEye(tryMove.CurrentGame.Board, tryMove.Move, tryMove.MoveContent) && (tryMove.MakeMoveWithOpponentAtSamePoint() == null);
        }

        /// <summary>
        /// Find tiger mouth where mouth point is empty or filled. Content in parameter represents content of stones forming the tiger mouth. Snapback or ko not handled (see IsConfirmTigerMouth). 
        /// </summary>
        public static Point? FindTigerMouth(Board board, Point p, Content c)
        {
            if (!board.PointWithinBoard(p))
                return null;
            Content content = board[p];
            if (content == Content.Empty)
            {
                List<Point> stoneNeighbours = board.GetStoneNeighbours(p);
                List<Point> libertyPoint = stoneNeighbours.Where(n => board[n] != c).ToList();
                if (libertyPoint.Count != 1) return null;
                Point q = libertyPoint.First();
                //make move into tiger mouth
                Board b = board.MakeMoveOnNewBoard(p, c.Opposite());
                if (b == null) return q;
                //capture move at tiger mouth
                Board b2 = CaptureSuicideGroup(b);
                if (b2 != null) return b2.Move;
            }
            else if (content == c.Opposite())
            {
                //ensure group in tiger mouth cannot escape
                (Boolean result, Point? libertyPoint, _) = UnescapableGroup(board, board.GetGroupAt(p));
                if (result)
                    return libertyPoint.Value;
            }
            return null;
        }

        public static Boolean FindTigerMouth(Board board, Content c, Point p)
        {
            return (FindTigerMouth(board, p, c) != null);
        }

        /// <summary>
        /// Immovable point to check for links and diagonal points in semi solid eye. For empty point, return if point is immovable which can be a suicide point or tiger's mouth. If not empty point, then check if opponent can escape. Return if immovable and liberty point at tiger mouth.
        /// Empty point <see cref="UnitTestProject.SurvivalTigerMouthMoveTest.SurvivalTigerMouthMoveTest_Scenario_GuanZiPu_A3" />
        /// Check connect and die <see cref="UnitTestProject.LifeCheckTest.LifeCheckTest_Scenario_Corner_A28" />
        /// Check filled point connect and die <see cref="UnitTestProject.NeutralPointMoveTest.NeutralPointMoveTest_Scenario_TianLongTu_Q16975" />
        /// <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_Scenario_XuanXuanGo_Q18341" />
        /// <see cref="UnitTestProject.ThreeLibertySuicidalTest.ThreeLibertySuicidalTest_Scenario_TianLongTu_Q14992_2" />
        /// Check for ko possibility <see cref="UnitTestProject.LifeCheckTest.LifeCheckTest_Scenario_WuQingYuan_Q30986" />
        /// </summary>
        public static (Boolean, Point?) IsImmovablePoint(Point p, Content c, Board board)
        {
            if (board[p] == Content.Empty) //empty point
            {
                (Boolean suicidal, Board b) = ImmovableHelper.IsSuicidalMove(p, c.Opposite(), board);
                if (!suicidal) return (false, null);
                if (b == null)
                {
                    //check connect and die
                    if (EyeHelper.FindEye(board, p, c) && AllConnectAndDie(board, p))
                        return (false, null);
                    return (true, null);
                }
                //ensure tiger mouth without ko or snapback
                Board b2 = IsConfirmTigerMouth(board, b);
                if (b2 != null)
                    return (true, b2.Move);
            }
            else //filled point
            {
                Group targetGroup = board.GetGroupAt(p);
                (Boolean unEscapable, Point? libertyPoint, _) = UnescapableGroup(board, targetGroup);
                if (!unEscapable)
                    return (false, null);

                //check filled point connect and die
                if (targetGroup.Points.Count <= 2 && board.GetNeighbourGroups(targetGroup).Any(n => CheckConnectAndDie(board, n)))
                    return (false, null);

                //check for ko possibility
                Point q = libertyPoint.Value;
                if (CheckForKoInImmovablePoint(board, targetGroup, q))
                    return (false, null);

                return (true, q);
            }
            return (false, null);
        }


        /// <summary>
        /// Check for ko in immovable point.
        /// check for ko by capture neighbour groups <see cref="UnitTestProject.LifeCheckTest.LifeCheckTest_Scenario_WuQingYuan_Q30986" />
        /// <see cref="UnitTestProject.LifeCheckTest.LifeCheckTest_Scenario_TianLongTu_Q16446" />
        /// Check for reverse ko fight <see cref="UnitTestProject.LifeCheckTest.LifeCheckTest_Scenario_WindAndTime_Q29998" />
        /// </summary>
        private static Boolean CheckForKoInImmovablePoint(Board board, Group targetGroup, Point q)
        {
            if (targetGroup.Points.Count != 1) return false;
            Content c = targetGroup.Content.Opposite();
            if (!KoHelper.KoContentEnabled(c.Opposite(), board.GameInfo)) return false;
            //check for ko by capture neighbour groups
            if (EyeHelper.FindCoveredEye(board, q, c.Opposite()))
            {
                List<Group> neighbourGroups = board.GetGroupsFromStoneNeighbours(q, c).ToList();
                if (neighbourGroups.All(n => n.Liberties.Count == 1))
                {
                    foreach (Group group in neighbourGroups)
                    {
                        (Boolean unEscapable, _, Board b) = ImmovableHelper.UnescapableGroup(board, group);
                        if (!unEscapable && KoHelper.IsKoFight(b, b.GetGroupAt(targetGroup.Points.First())))
                            return true;
                    }
                }
                if (KoHelper.IsKoFight(board, targetGroup))
                    return true;
            }

            //check for reverse ko fight 
            List<Point> stoneNeighbours = board.GetStoneNeighbours(q);
            if (stoneNeighbours.Any(n => board[n] == c)) return false;
            List<Point> eyeNeighbour = stoneNeighbours.Where(n => board[n] == Content.Empty).ToList();
            if (eyeNeighbour.Count == 1 && KoHelper.IsReverseKoFight(board, eyeNeighbour.First(), c.Opposite()))
                return true;
            return false;
        }

        /// <summary>
        /// Capture suicide group and check for captured count greater than one or move liberty greater than one to ensure no ko or snapback.
        /// <see cref="UnitTestProject.ImmovableTest.ImmovableTest_Scenario_TianLongTu_Q16827" />
        /// Suicidal move at side of board <see cref="UnitTestProject.LifeCheckTest.LifeCheckTest_Scenario_XuanXuanGo_Q18500" />
        /// Check connect and die on current board <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_GuanZiPu_A17_2" />
        /// Check all connect and die on captured board <see cref="UnitTestProject.ImmovableTest.ImmovableTest_Scenario_XuanXuanGo_B32" />
        /// <see cref="UnitTestProject.LifeCheckTest.LifeCheckTest_Scenario1dan21" />
        /// </summary>.
        public static Board IsConfirmTigerMouth(Board currentBoard, Board tryBoard, Point? p = null)
        {
            if (p == null) p = tryBoard.Move;
            Content c = tryBoard.MoveGroup.Content;

            Point? libertyPoint = FindTigerMouth(currentBoard, p.Value, c.Opposite());
            if (libertyPoint == null) return null;

            //check suicidal move at side of board
            if (SuicidalAfterMustHaveMove(currentBoard, tryBoard, libertyPoint.Value))
                return null;

            //check connect and die on current board
            if (currentBoard.GetGroupsFromStoneNeighbours(p.Value, c).Any(n => CheckConnectAndDie(currentBoard, n)))
                return null;

            Board capturedBoard = CaptureSuicideGroup(p.Value, tryBoard);
            if (capturedBoard == null) return null;
            //ensure not ko
            if (KoHelper.IsKoFight(capturedBoard))
                return null;

            //check all connect and die on captured board
            if (AllConnectAndDie(capturedBoard, p.Value))
                return null;

            if (ThreeLibertyConnectAndDie(capturedBoard, p.Value))
                return null;

            return capturedBoard;
        }


        /// <summary>
        /// Three liberty connect and die, not all inclusive. Connect and die group may not be target group for two liberties target group.
        /// <see cref="UnitTestProject.ThreeLibertySuicidalTest.ThreeLibertySuicidalTest_Scenario_TianLongTu_Q14992_2" />
        /// <see cref="UnitTestProject.ThreeLibertySuicidalTest.ThreeLibertySuicidalTest_Scenario_TianLongTu_Q14992" />
        /// Stone neighbours at diagonal of each other <see cref="UnitTestProject.ThreeLibertySuicidalTest.ThreeLibertySuicidalTest_Scenario_Side_B19" />
        /// Check if escapable <see cref="UnitTestProject.ThreeLibertySuicidalTest.ThreeLibertySuicidalTest_Scenario_Corner_A86" />
        /// </summary>
        public static Boolean ThreeLibertyConnectAndDie(Board board, Point p)
        {
            Group targetGroup = board.MoveGroup;
            Content c = targetGroup.Content;

            if (!ImmovableHelper.FindTigerMouth(board, c, p)) return false;

            //stone neighbours at diagonal of each other
            List<Point> stoneNeighbours = LinkHelper.GetNeighboursDiagonallyLinked(board, p, c);
            if (!stoneNeighbours.Any()) return false;

            Board b = board.MakeMoveOnNewBoard(p, c.Opposite());
            if (b == null || b.MoveGroupLiberties != 1) return false;
            if (b.AtariTargets.Count != 1) return false;

            Board b2 = ImmovableHelper.CaptureSuicideGroup(b);
            if (b2 == null || b2.MoveGroupLiberties != 2) return false;
            if (!EyeHelper.FindCoveredEye(b2, p, c)) return false;
            //check if escapable
            Point e = b2.MoveGroup.Liberties.First(lib => !lib.Equals(p));
            if (!ImmovableHelper.IsSuicidalMove(b2, e, c))
                return false;
            if (CheckConnectAndDie(b2, b2.GetGroupAt(targetGroup.Points.First())))
                return true;
            return false;
        }

        /// <summary>
        /// Check if escapable for target group with two liberties.
        /// </summary>
        public static Boolean EscapeCaptureLink(Board board, Group targetGroup, Point? capturePoint = null)
        {
            //check if absolute link at liberty
            foreach (Point liberty in targetGroup.Liberties)
            {
                Board b = board.MakeMoveOnNewBoard(liberty, targetGroup.Content);
                if (b == null || !LinkHelper.IsAbsoluteLinkForGroups(board, b))
                    continue;
                if (b.GetGroupLiberties(targetGroup.Points.First()) > 2)
                    return true;
            }
            //check for atari target group other than capture point
            List<Group> ngroups = board.GetNeighbourGroups(targetGroup).Where(n => n.Liberties.Count == 1).ToList();
            foreach (Group ngroup in ngroups)
            {
                if (capturePoint != null && ngroup.Points.Contains(capturePoint.Value)) continue;
                Board b = ImmovableHelper.CaptureSuicideGroup(board, ngroup);
                if (b != null && b.GetGroupAt(targetGroup.Points.First()).Liberties.Count > 2)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Suicide move for survival after must-have neutral move at side of the board.
        /// </summary>
        private static Boolean SuicidalAfterMustHaveMove(Board currentBoard, Board tryBoard, Point libertyPoint)
        {
            Point p = tryBoard.Move.Value;
            Content c = tryBoard[p];
            if (currentBoard[p] != Content.Empty || currentBoard.PointWithinMiddleArea(p)) return false;
            Point eyePoint = currentBoard.GetDiagonalNeighbours(p).FirstOrDefault(n => EyeHelper.FindEye(currentBoard, n));
            if (!Convert.ToBoolean(eyePoint.NotEmpty)) return false;

            (Boolean suicidal, Board b) = ImmovableHelper.IsSuicidalMove(libertyPoint, c, currentBoard);
            if (suicidal) return false;

            (Boolean suicidal2, Board b2) = ImmovableHelper.IsSuicidalMove(p, c.Opposite(), b);
            if (EyeHelper.FindRealSolidEyes(eyePoint, c.Opposite(), b2)) return false;
            return suicidal2;
        }

        /// <summary>
        /// Ensure group cannot escape by moving at liberty point.       
        /// <see cref="UnitTestProject.SpecificNeutralMoveTest.SpecificNeutralMoveTest_Scenario_Corner_A85" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_GuanZiPu_Q14981" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_GuanZiPu_A12" />
        /// Recursive connect and die <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanGo_A44_101Weiqi" />
        /// <see cref="UnitTestProject.ImmovableTest.ImmovableTest_Scenario_TianLongTu_Q17255" />
        /// </summary>
        public static (Boolean, Point?, Board) UnescapableGroup(Board tryBoard, Group group)
        {
            Point? libertyPoint = ImmovableHelper.GetLibertyPointOfSuicide(tryBoard, group);
            if (libertyPoint == null) return (false, null, null);

            //check if atari any neighbour groups
            Board captureBoard = EscapeByCapture(tryBoard, group);
            if (captureBoard != null)
                return (false, null, captureBoard);

            //double ko fight
            (Boolean doubleKo, Board b) = KoHelper.DoubleKoFight(tryBoard, group);
            if (doubleKo)
                return (false, null, b);

            //move at liberty is suicidal or end crawling move
            (Boolean isSuicidal, Board escapeBoard) = IsSuicidalMove(libertyPoint.Value, group.Content, tryBoard);
            if (isSuicidal || IsEndCrawlingMove(new Board(tryBoard), libertyPoint.Value, group.Content))
            {
                return (true, libertyPoint, escapeBoard);
            }
            //recursive connect and die
            if (CheckConnectAndDie(escapeBoard, escapeBoard.GetGroupAt(group.Points.First())))
                return (true, libertyPoint, escapeBoard);

            return (false, null, escapeBoard);
        }

        /// <summary>
        /// Check if can escape by capturing neighbour group.
        /// Check snapback <see cref="UnitTestProject.AtariResponseMoveTest.AtariResponseMoveTest_Scenario_TianLongTu_Q16605" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanQiJing_Weiqi101_2282" />
        /// Connect and die <see cref="UnitTestProject.ImmovableTest.ImmovableTest_Scenario_XuanXuanGo_B32" />
        /// Connect and die for move group <see cref="UnitTestProject.ImmovableTest.ImmovableTest_Scenario_GuanZiPu_A3" />
        /// </summary>
        public static Board EscapeByCapture(Board tryBoard, Group group)
        {
            List<Group> atariTargets = AtariHelper.AtariByGroup(group, tryBoard);
            foreach (Group target in atariTargets)
            {
                (Boolean suicidal, Board b) = ImmovableHelper.IsSuicidalOnCapture(tryBoard, target);
                if (b == null) continue;
                //connect and die
                if (CheckConnectAndDie(b, b.GetGroupAt(group.Points.First())))
                    continue;
                //connect and die for move group
                if (target.Points.Count == 2)
                {
                    (_, Board b2) = ConnectAndDie(b, b.MoveGroup);
                    if (b2 != null && UnescapableGroup(b2, b2.GetGroupAt(group.Points.First())).Item1)
                        continue;
                }
                return b;
            }
            return null;
        }

        /// <summary>
        /// Is suicide move or only liberty of one.
        /// </summary>
        public static Boolean IsSuicidalMove(Board tryBoard, Point p, Content c)
        {
            return IsSuicidalMove(p, c, tryBoard).Item1;
        }

        public static (Boolean, Board) IsSuicidalMove(Point p, Content c, Board tryBoard)
        {
            if (tryBoard == null) return (false, null);
            Board board = tryBoard.MakeMoveOnNewBoard(p, c);
            if (board == null) return (true, null);

            if (board.MoveGroupLiberties == 1)
            {
                //check ko
                if (KoHelper.IsKoFight(board))
                {
                    Boolean koEnabled = KoHelper.KoContentEnabled(board.MoveGroup.Content, board.GameInfo);
                    if (!koEnabled) return (true, null);
                    else return (false, board);
                }
                return (true, board);
            }
            return (false, board);
        }

        /// <summary>
        /// Is suicide move on capture.
        /// Check ko <see cref="UnitTestProject.ImmovableTest.ImmovableTest_Scenario_WuQingYuan_Q31503" />
        /// </summary>
        public static (Boolean, Board) IsSuicidalOnCapture(Board tryBoard, Group targetGroup = null)
        {
            Board board = ImmovableHelper.CaptureSuicideGroup(tryBoard, targetGroup);
            if (board == null)
                return (true, board);

            if (board.MoveGroupLiberties == 1)
            {
                //check ko
                Boolean isKo = (KoHelper.IsKoFight(board) && KoHelper.KoContentEnabled(board.MoveGroup.Content, board.GameInfo));
                if (!isKo) return (true, board);
            }
            return (false, board);
        }

        /// <summary>
        /// Move escaping by crawling at edge of board.
        /// <see cref="UnitTestProject.ImmovableTest.ImmovableTest_Scenario_XuanXuanQiJing_A36" />
        /// </summary>
        public static Boolean IsEndCrawlingMove(Board board, Point libertyPoint, Content c)
        {
            if (board.PointWithinMiddleArea(libertyPoint)) return false;
            if (board.InternalMakeMove(libertyPoint, c) != MakeMoveResult.Legal) return false;
            HashSet<Point> liberties = board.GetGroupAt(libertyPoint).Liberties;
            if (liberties.Count == 1) return true;
            if (liberties.Count != 2) return false;
            List<Point> capturePoint = liberties.Where(q => board.PointWithinMiddleArea(q)).ToList();
            if (capturePoint.Count == 1)
            {
                if (board.InternalMakeMove(capturePoint.First(), c.Opposite()) != MakeMoveResult.Legal) return false;
                if (board.MoveGroupLiberties == 1) return false;
                Point escapePoint = liberties.Except(capturePoint).First();
                return IsEndCrawlingMove(board, escapePoint, c);
            }
            return false;
        }

        /// <summary>
        /// Capture group that has liberty of one only and return the board.
        /// </summary>
        public static Board CaptureSuicideGroup(Point p, Board board, Boolean excludeKo = false)
        {
            if (board[p] == Content.Empty) return null;
            return CaptureSuicideGroup(board, board.GetGroupAt(p), excludeKo);
        }

        public static Board CaptureSuicideGroup(Board board, Group group = null, Boolean excludeKo = false)
        {
            if (group == null) group = board.MoveGroup;
            Content c = group.Content.Opposite();
            Point? p = GetLibertyPointOfSuicide(board, group);
            if (p == null) return null;
            return board.MakeMoveOnNewBoard(p.Value, c, excludeKo);
        }

        /// <summary>
        /// Get liberty point of suicide group.
        /// </summary>
        public static Point? GetLibertyPointOfSuicide(Board tryBoard, Group group = null)
        {
            if (group == null) group = tryBoard.MoveGroup;
            List<Point> liberties = tryBoard.GetGroupLibertyPoints(group);
            if (liberties.Count == 1)
                return liberties.First();
            return null;
        }

        /// <summary>
        /// Make move at liberty point of suicide group.
        /// </summary>
        public static Board MakeMoveAtLibertyPointOfSuicide(Board tryBoard, Group group, Content c)
        {
            Point? libertyPoint = ImmovableHelper.GetLibertyPointOfSuicide(tryBoard, group);
            if (libertyPoint == null) return null;
            Board board = tryBoard.MakeMoveOnNewBoard(libertyPoint.Value, c);
            return board;
        }

        /// <summary>
        /// Is suicide move for both players.
        /// </summary>
        public static Boolean IsSuicidalMoveForBothPlayers(Board tryBoard, Point q)
        {
            return (ImmovableHelper.IsSuicidalMove(tryBoard, q, Content.Black) && ImmovableHelper.IsSuicidalMove(tryBoard, q, Content.White));
        }

        /// <summary>
        /// Simple snapback with liberty of two in the group.
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WindAndTime_Q30234" />
        /// <see cref="UnitTestProject.SpecificNeutralMoveTest.SpecificNeutralMoveTest_Scenario_Corner_A55" />
        /// </summary>
        public static Boolean CheckSnapbackInNeighbourGroups(Board board, Group moveGroup)
        {
            IEnumerable<Group> neighbourGroups = board.GetNeighbourGroups(moveGroup);
            return neighbourGroups.Any(group => CheckSnapback(board, group));
        }

        /// <summary>
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanGo_B31_4" />
        /// Two point move <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WindAndTime_Q30234" />
        /// </summary>
        public static Boolean CheckSnapback(Board board, Group targetGroup)
        {
            Content c = targetGroup.Content;
            if (targetGroup.Points.Count == 1) return false;
            List<Point> libertyPoints = board.GetGroupLibertyPoints(targetGroup);
            if (libertyPoints.Count != 2) return false;

            List<Group> groups = board.GetNeighbourGroups(targetGroup).Where(gr => gr.Liberties.Count == 1).ToList();
            if (groups.Count != 1) return false;
            Group moveGroup = groups.First();
            if (moveGroup.Points.Count > 2 || moveGroup.Liberties.Count > 1) return false;

            foreach (Point libertyPoint in libertyPoints)
            {
                //find if liberty is suicide point
                Point? q = ImmovableHelper.FindTigerMouth(board, libertyPoint, c);
                if (!q.HasValue || board[q.Value] != Content.Empty) continue;
                //make move at suicide point
                Board b = board.MakeMoveOnNewBoard(libertyPoint, c.Opposite());
                if (b == null) continue;

                List<Group> suicideGroups = b.GetNeighbourGroups(targetGroup).Where(gr => gr.Points.Count <= 2 && gr.Liberties.Count == 1).ToList();
                if (suicideGroups.Count != 2) continue;
                //one point move within two point group or two point move
                List<Group> suicideWithinTwoPointGroup = suicideGroups.Select(gr => new { group = gr, liberty = gr.Liberties.First() }).Where(gr => gr.group.Points.Count == 2 || board.GetStoneNeighbours(gr.liberty).Where(n => !n.Equals(gr.group.Points.First())).All(n => board[n] == c)).Select(gr => gr.group).ToList();
                if (suicideWithinTwoPointGroup.Count != 1) continue;

                Group suicideGroupAtTigerMouth = suicideGroups.Where(gr => gr != suicideWithinTwoPointGroup.First() && gr.Points.Count == 1).FirstOrDefault();
                if (suicideGroupAtTigerMouth == null) continue;
                Point r = suicideGroupAtTigerMouth.Points.First();
                if (!b.GetDiagonalNeighbours(r).Any(n => suicideWithinTwoPointGroup.First().Points.Contains(n))) continue;
                //capture move
                if (IsSnapback(b, targetGroup, suicideGroupAtTigerMouth))
                    return true;

            }
            return false;
        }

        /// <summary>
        /// Snapback.
        /// Check if target group is escapable <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanGo_B31" />
        /// <see cref="UnitTestProject.SurvivalTigerMouthMoveTest.SurvivalTigerMouthMoveTest_Scenario_GuanZiPu_A3" />
        /// <see cref="UnitTestProject.RedundantEyeFillerTest.RedundantEyeFillerTest_Scenario_WuQingYuan_Q31640" />
        /// Check kill eye snapback <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanGo_B32" />
        /// Check not more than two stones captured <see cref="UnitTestProject.ImmovableTest.ImmovableTest_Scenario_WuQingYuan_Q31471" />
        /// Check if kill move can escape <see cref="UnitTestProject.ImmovableTest.ImmovableTest_Scenario_Corner_B28" />
        /// </summary>
        public static Boolean IsSnapback(Board tryBoard, Group targetGroup, Group suicideGroup)
        {
            if (!tryBoard.IsAtariMove || tryBoard.MoveGroupLiberties != 1) return false;
            (Boolean suicidal, Board b) = ImmovableHelper.IsSuicidalOnCapture(tryBoard, suicideGroup);
            if (suicidal && b != null && b.MoveGroup.Points.Count > 1)
            {
                //check if target group is escapable
                (Boolean unEscapable, _, Board escapeBoard) = UnescapableGroup(tryBoard, targetGroup);
                if (unEscapable) return true;
                //check not more than two stones captured
                int capturedPoints = escapeBoard.CapturedPoints.Count();
                Boolean capture = (escapeBoard.CapturedList.Count == 1 && capturedPoints >= 1 && capturedPoints <= 2 && !EyeHelper.FindRealEyeWithinEmptySpace(escapeBoard, escapeBoard.CapturedList.First(), EyeType.UnCoveredEye));
                if (!capture) return false;
                //check if kill move can escape
                if (UnescapableGroup(escapeBoard, suicideGroup).Item1)
                    return false;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Check for connect and die on board with captured suicide stone.
        /// <see cref="UnitTestProject.ImmovableTest.ImmovableTest_Scenario_XuanXuanGo_B32" />
        /// Suicidal capture <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanQiJing_B25" />
        /// <see cref="UnitTestProject.SpecificNeutralMoveTest.SpecificNeutralMoveTest_Scenario_Corner_A55" />
        /// </summary>
        public static (Boolean, Board) ConnectAndDie(Board board, Group targetGroup = null)
        {
            targetGroup = (targetGroup) ?? board.MoveGroup;
            Content c = targetGroup.Content;
            List<Point> groupLiberties = board.GetGroupLibertyPoints(targetGroup);
            if (groupLiberties.Count > 2) return (false, null);

            List<KeyValuePair<LinkedPoint<Point>, Board>> killBoards = new List<KeyValuePair<LinkedPoint<Point>, Board>>();
            foreach (Point liberty in groupLiberties)
            {
                if (!GameHelper.SetupMoveAvailable(board, liberty)) continue;
                (Boolean isSuicidal, Board b) = ImmovableHelper.IsSuicidalMove(liberty, c.Opposite(), board);
                if (b == null) continue;
                int neighbourCount = b.GetStoneNeighbours().Count(n => b[n] != c.Opposite());
                killBoards.Add(new KeyValuePair<LinkedPoint<Point>, Board>(new LinkedPoint<Point>(liberty, new { isSuicidal, neighbourCount }), b));
            }

            foreach (KeyValuePair<LinkedPoint<Point>, Board> kvp in killBoards.OrderByDescending(b => ((dynamic)b.Key.CheckMove).neighbourCount))
            {
                Board b = kvp.Value;
                LinkedPoint<Point> key = kvp.Key;
                //check if captured
                if (b.CapturedPoints.Contains(targetGroup.Points.First()))
                    return (true, b);

                //check if connect and die
                if (UnescapableGroup(b, targetGroup).Item1)
                    return (true, b);
            }
            return (false, null);
        }

        public static Boolean CheckConnectAndDie(Board board, Group targetGroup = null)
        {
            return ConnectAndDie(board, targetGroup).Item1;
        }


        /// <summary>
        /// Check connect and die for captured board.
        /// </summary>
        public static Boolean AllConnectAndDie(Board capturedBoard, Point p, Content c = Content.Unknown)
        {
            if (c == Content.Unknown)
            {
                List<Point> stoneNeighbours = capturedBoard.GetStoneNeighbours(p).Where(q => capturedBoard[q] != Content.Empty).ToList();
                c = capturedBoard[stoneNeighbours.First()];
                if (stoneNeighbours.Any(n => capturedBoard[n] != c))
                    throw new Exception("Different content in connect and die.");
            }
            List<Group> neighbourGroups = capturedBoard.GetGroupsFromStoneNeighbours(p, c.Opposite()).ToList();
            return neighbourGroups.Any(group => CheckConnectAndDie(capturedBoard, group));
        }

        /// <summary>
        /// Pre-atari move that targets group with liberty of two. Next atari move will capture the group.
        /// <see cref="UnitTestProject.SpecificNeutralMoveTest.SpecificNeutralMoveTest_Scenario_TianLongTu_Q16594" />
        /// <see cref="UnitTestProject.SpecificNeutralMoveTest.SpecificNeutralMoveTest_Scenario_WuQingYuan_Q31154" />
        /// <see cref="UnitTestProject.SpecificNeutralMoveTest.SpecificNeutralMoveTest_Scenario_WindAndTime_Q30370" />
        /// <see cref="UnitTestProject.SpecificNeutralMoveTest.SpecificNeutralMoveTest_Scenario_XuanXuanGo_A55" />
        /// Check if any liberty is suicidal <see cref="UnitTestProject.GenericNeutralMoveTest.GenericNeutralMoveTest_Scenario_XuanXuanQiJing_Weiqi101_18410" />
        /// Rare scenario <see cref="UnitTestProject.GenericNeutralMoveTest.GenericNeutralMoveTest_Scenario_WindAndTime_Q30275" />
        /// Check unescapable group <see cref="UnitTestProject.SpecificNeutralMoveTest.SpecificNeutralMoveTest_Scenario_Corner_A85" />
        /// <see cref="UnitTestProject.SpecificNeutralMoveTest.SpecificNeutralMoveTest_Scenario_WuQingYuan_Q31154" />
        /// </summary>
        public static Boolean PreAtariMove(GameTryMove tryMove)
        {
            Board currentBoard = tryMove.CurrentGame.Board;
            Board tryBoard = tryMove.TryGame.Board;
            Point move = tryBoard.Move.Value;
            Content c = tryBoard.MoveGroup.Content;
            IEnumerable<Group> neighbourGroups = tryBoard.GetGroupsFromStoneNeighbours(move);
            foreach (Group targetGroup in neighbourGroups)
            {
                //check conditions for pre-atari
                HashSet<Point> targetLiberties = targetGroup.Liberties;
                if (targetLiberties.Count != 2) continue;

                //check if any liberty is suicidal
                if (targetLiberties.Any(t => ImmovableHelper.IsSuicidalMove(t, c, tryBoard).Item2 == null))
                    continue;
                if (AtariHelper.AtariByGroup(tryBoard, targetGroup))
                    continue;

                //check connect and die
                (_, Board board) = ConnectAndDie(tryBoard, targetGroup);
                if (board != null && board.MoveGroup.Points.Count == 1 && board.GetGroupsFromStoneNeighbours(board.Move.Value, c).Count > 1 && EscapeCaptureLink(tryBoard, targetGroup))
                    return true;

                //check unescapable group                
                foreach (Point liberty in currentBoard.GetGroupLibertyPoints(targetGroup))
                {
                    Board b = currentBoard.MakeMoveOnNewBoard(liberty, c.Opposite());
                    if (b == null || b.AtariTargets.Count == 0) continue;
                    if (b.AtariTargets.Any(t => ImmovableHelper.UnescapableGroup(b, t).Item1))
                    {
                        if (ImmovableHelper.IsSuicidalMoveForBothPlayers(tryBoard, liberty))
                            return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Ensure that liberties within killer group can be cleared.
        /// </summary>
        public static Boolean ClearEmptySpace(Board board, Group killerGroup)
        {
            Content c = killerGroup.Content.Opposite();
            List<Point> availablePoints = killerGroup.Points.Where(p => board[p] == Content.Empty && !EyeHelper.FindEye(board, p, c)).ToList();
            if (availablePoints.Count == 0) return true;
            return availablePoints.Any(p => !ImmovableHelper.IsSuicidalMove(board, p, c));
        }
    }
}
