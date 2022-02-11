using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Go
{
    public class ImmovableHelper
    {
        /// <summary>
        /// Find opponent suicide point and ko fight if not enabled for opponent.
        /// </summary>
        public static void FindOpponentImmovable(GameTryMove tryMove)
        {
            //opponent suicide point
            GameTryMove opponentMove = tryMove.MakeMoveWithOpponentAtSamePoint(false);
            if (opponentMove == null)
            {
                tryMove.IsOpponentSuicide = true;
                return;
            }
            //check if ko fight enabled
            Board tryBoard = opponentMove.TryGame.Board;
            if (KoHelper.IsKoFight(tryBoard))
            {
                Boolean koEnabled = KoHelper.KoContentEnabled(tryBoard.MoveGroup.Content, tryBoard.GameInfo);
                tryMove.IsOpponentSuicide = !koEnabled;
                return;
            }
            tryMove.IsOpponentSuicide = false;
        }

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
                List<Point> stoneNeighbours = board.GetStoneNeighbours(p.x, p.y);
                List<Point> libertyPoint = stoneNeighbours.Where(n => board[n] != c).ToList();
                if (libertyPoint.Count != 1) return null;
                Point q = libertyPoint.First();
                //return if liberty point is empty
                if (board[q] == Content.Empty) return q;
                //make move into tiger mouth
                Board b = board.MakeMoveOnNewBoard(p, c.Opposite(), true);
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
        /// Ensure no snapback <see cref="UnitTestProject.NeutralPointMoveTest.NeutralPointMoveTest_Scenario_WuQingYuan_Q31680" />
        /// Check for ko possibility <see cref="UnitTestProject.LifeCheckTest.LifeCheckTest_Scenario_WuQingYuan_Q30986" />
        /// </summary>
        public static (Boolean, Point?) IsImmovablePoint(Point p, Content c, Board board)
        {
            if (board[p] == Content.Empty) //empty point
            {
                (Boolean suicidal, Board b) = ImmovableHelper.IsSuicidalMove(p, c.Opposite(), board, true);
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
                Point q = libertyPoint.Value;
                //check for ko possibility
                if (targetGroup.Points.Count == 1 && CheckForKoInImmovablePoint(board, q, c))
                    return (false, null);

                //ensure no snapback
                if (AllConnectAndDie(board, p, c))
                    return (false, null);

                return (true, q);
            }
            return (false, null);
        }


        /// <summary>
        /// Check for ko possibility <see cref="UnitTestProject.LifeCheckTest.LifeCheckTest_Scenario_WuQingYuan_Q30986" />
        /// Check for reverse ko fight <see cref="UnitTestProject.LifeCheckTest.LifeCheckTest_Scenario_WindAndTime_Q29998" />
        /// </summary>
        private static Boolean CheckForKoInImmovablePoint(Board board, Point q, Content c)
        {
            if (EyeHelper.FindEye(board, q, c.Opposite()))
            {
                List<Group> neighbourGroups = board.GetGroupsFromStoneNeighbours(q, c).ToList();
                if (neighbourGroups.Any(n => AtariHelper.AtariByGroup(board, n)))
                    return true;
            }
            //check for reverse ko fight 
            List<Point> stoneNeighbours = board.GetStoneNeighbours(q.x, q.y);
            if (stoneNeighbours.Any(n => board[n] == c)) return false;
            List<Point> eyeNeighbour = stoneNeighbours.Where(n => board[n] == Content.Empty).ToList();
            if (eyeNeighbour.Count == 1)
            {
                Board b = board.MakeMoveOnNewBoard(eyeNeighbour.First(), c.Opposite());
                if (b != null && KoHelper.IsReverseKoFight(b))
                    return true;
            }
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
            Content c = tryBoard[tryBoard.Move.Value];

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

            return capturedBoard;
        }

        /// <summary>
        /// Suicide move for survival after must-have neutral move at side of the board.
        /// </summary>
        private static Boolean SuicidalAfterMustHaveMove(Board currentBoard, Board tryBoard, Point libertyPoint)
        {
            Point p = tryBoard.Move.Value;
            Content c = tryBoard[p];
            if (currentBoard[p] != Content.Empty || currentBoard.PointWithinMiddleArea(p.x, p.y)) return false;
            Point eyePoint = currentBoard.GetDiagonalNeighbours(p.x, p.y).FirstOrDefault(n => EyeHelper.FindEye(currentBoard, n));
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
        public static (Boolean, Point?, Board) UnescapableGroup(Board tryBoard, Group group, Boolean checkAtari = true)
        {
            Point? libertyPoint = ImmovableHelper.GetLibertyPointOfSuicide(tryBoard, group);
            if (libertyPoint == null) return (false, null, null);

            //check if atari any neighbour groups
            if (checkAtari)
            {
                Board captureBoard = EscapeByCapture(tryBoard, group);
                if (captureBoard != null)
                    return (false, captureBoard.Move.Value, captureBoard);
            }

            //move at liberty is suicidal or end crawling move
            (Boolean isSuicidal, Board escapeBoard) = IsSuicidalMove(libertyPoint.Value, group.Content, tryBoard);
            if (isSuicidal || IsEndCrawlingMove(new Board(tryBoard), libertyPoint.Value, group.Content))
            {
                return (true, libertyPoint, escapeBoard);
            }
            //recursive connect and die
            if (CheckConnectAndDie(escapeBoard, escapeBoard.GetGroupAt(group.Points.First())))
                return (true, libertyPoint, escapeBoard);

            return (false, libertyPoint, escapeBoard);
        }

        /// <summary>
        /// Check if can escape by capturing neighbour group.
        /// Check snapback <see cref="UnitTestProject.AtariResponseMoveTest.AtariResponseMoveTest_Scenario_TianLongTu_Q16605" />
        /// Connect and die <see cref="UnitTestProject.ImmovableTest.ImmovableTest_Scenario_XuanXuanGo_B32" />
        /// Connect and die for move group <see cref="UnitTestProject.ImmovableTest.ImmovableTest_Scenario_GuanZiPu_A3" />
        /// </summary>
        public static Board EscapeByCapture(Board tryBoard, Group group)
        {
            List<Group> atariTargets = AtariHelper.AtariByGroup(group, tryBoard);
            foreach (Group target in atariTargets)
            {
                (Boolean suicidal, Board b) = ImmovableHelper.IsSuicidalOnCapture(tryBoard, target);
                if (suicidal) continue;
                //Connect and die
                if (CheckConnectAndDie(b, b.GetGroupAt(group.Points.First())) || (target.Points.Count == 2 && CheckConnectAndDie(b, b.MoveGroup))) continue;
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

        public static (Boolean, Board) IsSuicidalMove(Point p, Content c, Board tryBoard, Boolean excludeKo = false)
        {
            if (tryBoard == null) return (false, null);
            Board board = tryBoard.MakeMoveOnNewBoard(p, c, excludeKo);
            if (board == null || board.MoveGroupLiberties == 1)
                return (true, board);
            return (false, board);
        }

        /// <summary>
        /// Is suicide move on capture.
        /// Check ko <see cref="UnitTestProject.ImmovableTest.ImmovableTest_Scenario_WuQingYuan_Q31503" />
        /// </summary>
        public static (Boolean, Board) IsSuicidalOnCapture(Board tryBoard, Group targetGroup = null)
        {
            Board b = ImmovableHelper.CaptureSuicideGroup(tryBoard, targetGroup);
            if (b == null)
                return (true, b);

            if (b.MoveGroupLiberties == 1)
            {
                //check ko
                Boolean isKo = (KoHelper.IsKoFight(b) && KoHelper.KoContentEnabled(b.MoveGroup.Content, b.GameInfo));
                if (!isKo) return (true, b);
            }
            return (false, b);
        }

        /// <summary>
        /// Move escaping by crawling at edge of board.
        /// <see cref="UnitTestProject.ImmovableTest.ImmovableTest_Scenario_XuanXuanQiJing_A36" />
        /// </summary>
        public static Boolean IsEndCrawlingMove(Board board, Point libertyPoint, Content c)
        {
            if (board.PointWithinMiddleArea(libertyPoint.x, libertyPoint.y)) return false;
            if (board.InternalMakeMove(libertyPoint, c) != MakeMoveResult.Legal) return false;
            HashSet<Point> liberties = board.GetGroupAt(libertyPoint).Liberties;
            if (liberties.Count == 1) return true;
            if (liberties.Count != 2) return false;
            List<Point> capturePoint = liberties.Where(q => board.PointWithinMiddleArea(q.x, q.y)).ToList();
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
        public static Board CaptureSuicideGroup(Point p, Board board)
        {
            if (board[p] == Content.Empty) return null;
            return CaptureSuicideGroup(board, board.GetGroupAt(p));
        }

        public static Board CaptureSuicideGroup(Board board, Group group = null)
        {
            if (group == null) group = board.MoveGroup;
            Content c = group.Content.Opposite();
            List<Point> libertyPoint = board.GetGroupLibertyPoints(group);
            if (libertyPoint.Count != 1) return null;
            Point? p = GetLibertyPointOfSuicide(board, group);
            if (p == null) return null;
            return board.MakeMoveOnNewBoard(p.Value, c, true);
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
        /// Is suicide move for both players.
        /// </summary>
        public static Boolean IsSuicidalMoveForBothPlayers(Board tryBoard, Point q)
        {
            return (ImmovableHelper.IsSuicidalMove(tryBoard, q, Content.Black) && ImmovableHelper.IsSuicidalMove(tryBoard, q, Content.White));
        }

        /// <summary>
        /// Simple snapback with liberty of two in the group.
        /// <see cref="UnitTestProject.SpecificNeutralMoveTest.SpecificNeutralMoveTest_Scenario_Corner_A55" />
        /// </summary>
        public static Boolean CheckSnapback(Board board, Point p, Content c = Content.Unknown)
        {
            c = (c == Content.Unknown) ? board.MoveGroup.Content : c;
            IEnumerable<Group> neighbourGroups = board.GetGroupsFromStoneNeighbours(p, c);
            return neighbourGroups.Any(group => CheckSnapback(board, group));
        }

        public static Boolean CheckSnapback(Board board, Group targetGroup)
        {
            if (targetGroup.Points.Count == 1) return false;
            Content c = targetGroup.Content;
            List<Point> libertyPoints = board.GetGroupLibertyPoints(targetGroup);
            if (libertyPoints.Count != 2) return false;

            foreach (Point libertyPoint in libertyPoints)
            {
                //find if liberty is suicide point
                Point? q = ImmovableHelper.FindTigerMouth(board, libertyPoint, c);
                if (!q.HasValue || board[q.Value] != Content.Empty) continue;
                //make move at suicide point
                Board b = board.MakeMoveOnNewBoard(libertyPoint, c.Opposite(), true);
                if (b == null) continue;
                //capture move
                if (IsSnapback(b, targetGroup))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Snapback.
        /// Ensure move group contains previous group <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_TianLongTu_Q16827" />
        /// Check if target group is escapable <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanGo_B31" />
        /// <see cref="UnitTestProject.SurvivalTigerMouthMoveTest.SurvivalTigerMouthMoveTest_Scenario_GuanZiPu_A3" />
        /// Check not more than two stones captured <see cref="UnitTestProject.ImmovableTest.ImmovableTest_Scenario_WuQingYuan_Q31471" />
        /// Check if kill move can escape <see cref="UnitTestProject.ImmovableTest.ImmovableTest_Scenario_Corner_B28" />
        /// </summary>
        public static Boolean IsSnapback(Board tryBoard, Group group)
        {
            if (!tryBoard.IsAtariMove || tryBoard.MoveGroupLiberties != 1) return false;
            (Boolean suicidal, Board b) = ImmovableHelper.IsSuicidalOnCapture(tryBoard);
            if (suicidal && b != null && b.MoveGroup.Points.Count > 1)
            {
                //ensure move group contains previous group
                if (!b.MoveGroup.Points.Contains(group.Points.First()))
                    return false;

                //check if target group is escapable
                (Boolean unEscapable, _, Board escapeBoard) = UnescapableGroup(tryBoard, group);
                if (unEscapable) return true;
                //check not more than two stones captured
                int capturedPoints = escapeBoard.CapturedPoints.Count();
                Boolean capture = (escapeBoard.CapturedList.Count == 1 && capturedPoints >= 1 && capturedPoints <= 2);
                if (!capture) return false;
                //check if kill move can escape
                if (UnescapableGroup(escapeBoard, tryBoard.MoveGroup).Item1)
                    return false;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Check for connect and die on board with captured suicide stone.
        /// <see cref="UnitTestProject.ImmovableTest.ImmovableTest_Scenario_XuanXuanGo_B32" />
        /// Reverse connect and die <see cref="UnitTestProject.ImmovableTest.ImmovableTest_Scenario_WindAndTime_Q29277" />
        /// </summary>
        public static Boolean CheckConnectAndDie(Board board, Group targetGroup = null)
        {
            targetGroup = (targetGroup) ?? board.MoveGroup;
            Content c = targetGroup.Content;
            List<Point> groupLiberties = board.GetGroupLibertyPoints(targetGroup);
            if (groupLiberties.Count > 2) return false;

            foreach (Point liberty in groupLiberties)
            {
                (Boolean isSuicidal, Board b) = ImmovableHelper.IsSuicidalMove(liberty, c.Opposite(), board);
                if (b == null) continue;
                //check if captured
                if (b.CapturedPoints.Contains(targetGroup.Points.First())) return true;

                if (isSuicidal)
                {
                    //check snapback
                    if (IsSnapback(b, targetGroup))
                        return true;
                    continue;
                }

                //check if connect and die
                if (UnescapableGroup(b, targetGroup).Item1)
                {
                    //reverse connect and die
                    if (CheckConnectAndDie(b, b.MoveGroup))
                        continue;
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Check connect and die for captured board.
        /// </summary>
        public static Boolean AllConnectAndDie(Board capturedBoard, Point p, Content c = Content.Unknown)
        {
            if (c == Content.Unknown)
            {
                List<Point> stoneNeighbours = capturedBoard.GetStoneNeighbours(p.x, p.y).Where(q => capturedBoard[q] != Content.Empty).ToList();
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
        /// Rare scenario <see cref="UnitTestProject.GenericNeutralMoveTest.GenericNeutralMoveTest_Scenario_WindAndTime_Q30275" />
        /// Rare scenario ko fight <see cref="UnitTestProject.SpecificNeutralMoveTest.SpecificNeutralMoveTest_Scenario_Corner_A85" />
        /// </summary>
        public static Boolean PreAtariMove(Board board)
        {
            Point move = board.Move.Value;
            Content c = board[move];
            IEnumerable<Group> neighbourGroups = board.GetGroupsFromStoneNeighbours(move);
            foreach (Group targetGroup in neighbourGroups)
            {
                //check conditions for pre-atari
                if (!CheckPreAtariNeighbour(targetGroup, board))
                    continue;

                //check connect and die
                if (CheckConnectAndDie(board, targetGroup))
                    return true;

                //rare scenario ko fight
                if (KoHelper.KoContentEnabled(c, board.GameInfo))
                {
                    (Boolean koAtari, Board koBoard) = AtariHelper.KoAtariByNeighbour(board, targetGroup);
                    if (koAtari && koBoard.AtariTargets.Count >= 2)
                        return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Check conditions for pre-atari.
        /// Check link at liberty to escape atari <see cref="UnitTestProject.SpecificNeutralMoveTest.SpecificNeutralMoveTest_Scenario_TianLongTu_Q16594" />
        /// Check if immovable at liberties <see cref="UnitTestProject.SpecificNeutralMoveTest.SpecificNeutralMoveTest_Scenario_WuQingYuan_Q31154" />
        /// Check if any liberty is suicidal <see cref="UnitTestProject.GenericNeutralMoveTest.GenericNeutralMoveTest_Scenario_XuanXuanQiJing_Weiqi101_18410" />
        /// </summary>
        private static Boolean CheckPreAtariNeighbour(Group targetGroup, Board board)
        {
            HashSet<Point> targetLiberties = targetGroup.Liberties;
            if (targetLiberties.Count != 2) return false;

            //check if any liberty is suicidal
            if (targetLiberties.Any(t => ImmovableHelper.IsSuicidalMove(t, targetGroup.Content.Opposite(), board).Item2 == null))
                return false;

            if (AtariHelper.AtariByGroup(board, targetGroup))
                return false;

            foreach (Point liberty in targetLiberties)
            {
                //check link at liberty to escape atari
                Board b = board.MakeMoveOnNewBoard(liberty, targetGroup.Content, true);
                if (b != null && LinkHelper.LinkForGroups(b, board) && b.GetGroupLiberties(targetGroup.Points.First()) > 2)
                    return true;

                //check if immovable at liberties
                List<Group> neighbourGroups = board.GetNeighbourGroups(targetGroup);
                if (neighbourGroups.Any(group => group.Liberties.Count == 2 && !AtariHelper.AtariByGroup(board, group) && group.Liberties.Any(p => ImmovableHelper.IsSuicidalMoveForBothPlayers(board, p))))
                    return true;
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
