using System;
using System.Collections.Generic;
using System.Linq;

namespace Go
{
    public class BothAliveHelper
    {
        /// <summary>
        /// Add pass move to survival try moves if enabled, in order to check for both alive. Ensure no other try move present other than those within killer group.
        /// Enable pass move for ten thousand year ko as well.
        /// For simple seki which is usually the case, find one killer group with at least two liberties, and one survival neighbour group with at least two liberties. Simple seki <see cref="UnitTestProject.BothAliveTest.BothAliveTest_Scenario_SimpleSeki" />
        /// In most cases, there is only one killer group and one neighbour survival group, but there can also be two neighbour survival groups. Simple seki with two neighbour survival groups <see cref="UnitTestProject.BothAliveTest.BothAliveTest_Scenario_XuanXuanGo_A151_101Weiqi" />
        /// <see cref="UnitTestProject.BothAliveTest.BothAliveTest_Scenario3dan16" />
        /// Fill eye points with content <see cref="UnitTestProject.BothAliveTest.BothAliveTest_Scenario_XuanXuanGo_A27" />
        /// Two liberties for content group <see cref="UnitTestProject.BothAliveTest.BothAliveTest_Scenario_Corner_B43" />
        /// More than one content group in simple seki <see cref="UnitTestProject.BothAliveTest.BothAliveTest_Scenario_WuQingYuan_Q31646" />
        /// Ensure shared liberty suicidal for killer <see cref="UnitTestProject.BothAliveTest.BothAliveTest_Scenario_XuanXuanGo_A28_101Weiqi" />
        /// <see cref="UnitTestProject.BothAliveTest.BothAliveTest_Scenario_WuQingYuan_Q31445" />
        /// </summary>
        public static Boolean EnableCheckForPassMove(Board board, Content c = Content.Unknown, List<GameTryMove> tryMoves = null)
        {
            c = (c == Content.Unknown) ? GameHelper.GetContentForSurviveOrKill(board.GameInfo, SurviveOrKill.Survive) : c;
            List<Group> killerGroups = GroupHelper.GetKillerGroups(board, c, true);
            if (killerGroups.Count == 0) return false;

            Group killerGroup = killerGroups[0];
            Content content = killerGroup.Content;
            List<Point> emptyPoints = killerGroup.Points.Where(k => board[k] == Content.Empty).ToList();
            //two to four liberties for both alive
            if (emptyPoints.Count < 2 || emptyPoints.Count > 4) return false;
            //single empty point found for more than two empty points
            if (emptyPoints.Count > 2 && !emptyPoints.Any(p => board.GetStoneNeighbours(p).All(n => board[n] != Content.Empty)))
                return false;

            if (tryMoves != null)
            {
                //all game try moves should be within killer group to enable pass move
                List<GameTryMove> externalMoves = tryMoves.Where(p => !emptyPoints.Contains(p.Move) && !ImmovableHelper.SinglePointOpponentImmovable(p)).ToList();
                if (externalMoves.Count > 0)
                    return false;
            }

            //ensure at least two liberties in survival neighbour group
            List<Group> targetGroups = board.GetNeighbourGroups(killerGroup);
            if (targetGroups.Any(g => g.Liberties.Count == 1 && GroupHelper.GetKillerGroupFromCache(board, g.Points.First(), g.Content.Opposite()) == null))
                return false;

            //fill eye points with content
            Board filledBoard = FillEyePointsBoard(board, killerGroup);

            //check for simple seki and complex seki
            List<Point> contentPoints = killerGroup.Points.Where(t => board[t] == killerGroup.Content).ToList();
            List<Group> contentGroups = filledBoard.GetGroupsFromPoints(contentPoints).ToList();
            //more than one content group
            if (contentGroups.Count > 2 || (contentGroups.Count == 2 && emptyPoints.Count != 2)) return false;
            if (contentGroups.Count == 2 && !LinkHelper.IsImmediateDiagonallyConnected(board, contentGroups[0], contentGroups[1])) return false;

            if (killerGroups.Count == 1)  //simple seki
            {
                if (targetGroups.Count > 2) return false;
                //at least three content points in killer group
                if (contentPoints.Count < 3) return false;
                //at least two liberties for content groups in filled board
                if (contentGroups.Any(group => group.Liberties.Count == 1)) return false;
                return CheckSimpleSeki(board, filledBoard, targetGroups, killerGroup, emptyPoints);
            }
            else if (killerGroups.Count >= 2) //complex seki
            {
                if (emptyPoints.Count > 3) return false;
                //two liberties for content group
                Boolean oneLiberty = board.GetGroupsFromPoints(contentPoints).Any(group => group.Liberties.Count == 1);
                if (oneLiberty) return false;

                //ensure shared liberty suicidal for killer
                if (!emptyPoints.Any(p => board.GetGroupsFromStoneNeighbours(p, content.Opposite()).Any(group => group.Liberties.Count > 1) && ImmovableHelper.IsSuicidalMove(board, p, content)))
                    return false;

                //find diagonal cut
                (_, List<Point> pointsBetweenDiagonals) = LinkHelper.FindDiagonalCut(board, killerGroup);
                //check complex seki without diagonal cut
                if (pointsBetweenDiagonals == null) return CheckComplexSeki(board, killerGroups, targetGroups);

                //check complex seki with diagonal cut
                HashSet<Group> diagonalGroups = board.GetGroupsFromPoints(pointsBetweenDiagonals);
                if (diagonalGroups.Count != 2) return false;

                List<Group> complexSekiGroups = new List<Group>();
                foreach (Group diagonalGroup in diagonalGroups)
                {
                    Group diagonalKillerGroup = GroupHelper.GetKillerGroupFromCache(board, diagonalGroup.Points.First(), killerGroup.Content);
                    if (diagonalKillerGroup == null) continue;
                    List<Group> cutKillerGroups = killerGroups.Where(g => diagonalKillerGroup.Points.Contains(g.Points.First())).ToList();
                    List<Group> cutTargetGroups = targetGroups.Where(group => diagonalKillerGroup.Points.Contains(group.Points.First())).ToList();
                    if (CheckComplexSeki(board, cutKillerGroups, cutTargetGroups))
                        complexSekiGroups.Add(diagonalGroup);
                }

                if (complexSekiGroups.Count > 1) return true;
                if (complexSekiGroups.Count == 1)
                {
                    Group otherDiagonalGroup = diagonalGroups.First(d => d != complexSekiGroups.First());
                    if (WallHelper.IsNonKillableFromSetupMoves(board, otherDiagonalGroup))
                        return true;
                }

            }
            return false;
        }

        /// <summary>
        /// Check simple seki.
        /// Ensure at least two liberties in survival neighbour group <see cref="UnitTestProject.BothAliveTest.BothAliveTest_Scenario_Corner_A87" />
        /// Cover eye point <see cref="UnitTestProject.BothAliveTest.BothAliveTest_Scenario_Corner_B43" />
        /// Check diagonal at eye point <see cref="UnitTestProject.BothAliveTest.BothAliveTest_Scenario_Corner_A75" />
        /// <see cref="UnitTestProject.BothAliveTest.BothAliveTest_Scenario_WindAndTime_Q30275" />
        /// Check killer formation for two liberties <see cref="UnitTestProject.BothAliveTest.BothAliveTest_Scenario_Side_A23_2" />
        /// Check killer formation for three or more liberties <see cref="UnitTestProject.BothAliveTest.BothAliveTest_Scenario_WuQingYuan_Q31493_4" />
        /// Ensure killer group does not have real eye <see cref="UnitTestProject.BothAliveTest.BothAliveTest_Scenario_TianLongTu_Q16424_2" />
        /// Check for increased killer groups <see cref="UnitTestProject.BothAliveTest.BothAliveTest_Scenario_WuQingYuan_Q31445_2" />
        /// </summary>
        private static Boolean CheckSimpleSeki(Board board, Board filledBoard, List<Group> neighbourGroups, Group killerGroup, List<Point> emptyPoints)
        {
            Content content = killerGroup.Content;

            //ensure at least two liberties within killer group in survival neighbour group
            if (neighbourGroups.Any(n => n.Liberties.Count(p => killerGroup.Points.Contains(p) || BothAliveDiagonalEye(board, killerGroup, p)) < 2))
                return false;

            //check diagonal at eye point
            List<Point> eyePoints = emptyPoints.Where(p => EyeHelper.FindEye(board, p, content)).ToList();
            if (eyePoints.Any(p => board.GetDiagonalNeighbours(p).Any(n => board[n] == Content.Empty && !ImmovableHelper.IsSuicidalMoveForBothPlayers(board, n))))
                return false;

            int emptyPointCount = killerGroup.Points.Count(k => filledBoard[k] == Content.Empty);
            if (emptyPointCount >= 3)
            {
                //check killer formation for three or more liberties
                if (!KillerFormationHelper.DeadFormationInBothAlive(filledBoard, killerGroup, emptyPointCount, 2))
                    return false;
            }
            //check killer formation for two liberties
            else if (KillerFormationHelper.DeadFormationInBothAlive(filledBoard, killerGroup, emptyPointCount))
                return false;

            //ensure killer group does not have real eye
            if (emptyPoints.Any(p => EyeHelper.FindRealEyeWithinEmptySpace(board, p, content)))
                return false;

            //check for increased killer groups
            foreach (Point emptyPoint in emptyPoints)
            {
                Board b = board.MakeMoveOnNewBoard(emptyPoint, content.Opposite());
                if (b != null && b.MoveGroupLiberties > 1 && GameTryMove.IncreaseKillerGroups(b, board))
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Check if eye at diagonal of killer group for both alive.
        /// <see cref="UnitTestProject.FillKoEyeMoveTest.FillKoEyeMoveTest_Scenario_WindAndTime_Q30275" />
        /// </summary>
        private static Boolean BothAliveDiagonalEye(Board board, Group killerGroup, Point eye)
        {
            Content c = killerGroup.Content;
            if (EyeHelper.FindEye(board, eye, c.Opposite()) && board.GetStoneNeighbours(eye).All(n => board.GetGroupAt(n).Liberties.Count > 1))
            {
                if (board.GetDiagonalNeighbours(eye).Any(n => killerGroup.Points.Contains(n)))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Check complex seki.
        /// With diagonal group <see cref="UnitTestProject.BothAliveTest.BothAliveTest_Scenario3dan22" />
        /// Without diagonal group <see cref="UnitTestProject.BothAliveTest.BothAliveTest_Scenario_Corner_A123" />
        /// Check suicidal for both players and not ko move at liberty <see cref="UnitTestProject.BothAliveTest.BothAliveTest_Scenario_XuanXuanGo_A28_101Weiqi" />
        /// <see cref="UnitTestProject.BothAliveTest.BothAliveTest_Scenario_GuanZiPu_B18" />
        /// </summary>
        private static Boolean CheckComplexSeki(Board board, List<Group> killerGroups, List<Group> targetGroups)
        {
            if (killerGroups.Count == 0) return false;
            Content c = killerGroups.First().Content;
            IEnumerable<Point> killerLiberties = killerGroups.First().Points.Where(p => board[p] == Content.Empty);
            //ensure at least one liberty shared with killer group
            Boolean sharedLiberty = targetGroups.All(group => group.Liberties.Intersect(killerLiberties).Any());
            if (!sharedLiberty) return false;
            //check suicidal for both players and not ko move at liberty
            if (!targetGroups.Any(gr => gr.Liberties.Any(liberty => ImmovableHelper.IsSuicidalMoveForBothPlayers(board, liberty) || board.GetStoneNeighbours(liberty).Any(n => board[n] == c && board.GetGroupAt(n).Points.Count == 1 && board.GetGroupAt(n).Liberties.Count > 1))))
                return false;

            //ensure at least two liberties within killer group in survival neighbour group
            if (targetGroups.Any(n => n.Liberties.Count(p => GroupHelper.GetKillerGroupFromCache(board, p, c.Opposite()) != null) < 2))
                return false;
            //find uncovered eye
            if (FindUncoveredEyeInComplexSeki(board, killerGroups))
                return true;
            return false;
        }

        /// <summary>
        /// Fill eye points with stone of same content.
        /// </summary>
        public static Board FillEyePointsBoard(Board board, Group killerGroup)
        {
            IEnumerable<Point> killerLiberties = killerGroup.Points.Where(p => board[p] == Content.Empty);
            //ensure only one killer group with or without eye
            List<Point> eyePoints = killerLiberties.Where(t => EyeHelper.FindEye(board, t, killerGroup.Content)).ToList();
            Board filledBoard = board;
            if (eyePoints.Count > 0)
            {
                //fill eye point with stone
                filledBoard = new Board(board);
                eyePoints.ForEach(p => filledBoard[p] = killerGroup.Content);
            }
            return filledBoard;
        }

        /// <summary>
        /// Find uncovered eye in omplex seki.
        /// <see cref="UnitTestProject.BothAliveTest.BothAliveTest_Scenario_ComplexSeki" />
        /// <see cref="UnitTestProject.BothAliveTest.BothAliveTest_Scenario3dan22" />
        /// Clear all killer groups with empty points <see cref="UnitTestProject.BothAliveTest.BothAliveTest_Scenario_WindAndTime_Q30213" />
        /// <see cref="UnitTestProject.BothAliveTest.BothAliveTest_Scenario_Corner_A123" />
        /// </summary>
        public static Boolean FindUncoveredEyeInComplexSeki(Board board, List<Group> killerGroups)
        {
            //clear all killer groups with empty points
            Board clearedBoard = new Board(board);
            killerGroups.ForEach(group => group.Points.ToList().ForEach(p => clearedBoard[p] = Content.Empty));

            //check for uncovered eye
            foreach (Group group in killerGroups)
            {
                if (group.Points.Count != 1) continue;
                Point p = group.Points.First();
                if (EyeHelper.FindUncoveredEye(clearedBoard, p, group.Content.Opposite()))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Add pass move for game try move.
        /// </summary>
        public static GameTryMove AddPassMove(Game currentGame)
        {
            Game passGame = new Game(currentGame);
            GameTryMove move = new GameTryMove(passGame);
            move.TryGame.Board.Move = Game.PassMove;
            move.MakeMoveResult = MakeMoveResult.Legal;
            move.TryGame.Board.LastMoves.Add(Game.PassMove);
            move.TryGame.KoGameCheck = (move.TryGame.KoGameCheck == KoCheck.Survive) ? KoCheck.Kill : move.TryGame.KoGameCheck;
            return move;
        }
    }
}
