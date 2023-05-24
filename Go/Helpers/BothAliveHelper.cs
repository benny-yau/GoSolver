using System;
using System.Collections.Generic;
using System.Linq;

namespace Go
{
    public class BothAliveHelper
    {
        /// <summary>
        /// Enable pass move for both alive.
        /// </summary>
        public static void EnablePassMoveForBothAlive(Game currentGame, List<GameTryMove> tryMoves, SurviveOrKill surviveOrKill)
        {
            Board board = currentGame.Board;
            Content c = GameHelper.GetContentForSurviveOrKill(currentGame.GameInfo, surviveOrKill);
            if (surviveOrKill == SurviveOrKill.Survive)
            {
                if (!EnableCheckForPassMove(board, c, tryMoves)) return;
                tryMoves.Add(BothAliveHelper.AddPassMove(currentGame));
            }
            else
            {
                if (board.LastMove != null && board.LastMove.Value.Equals(Game.PassMove)) return;
                if (tryMoves.Count == 1 && tryMoves.Select(n => n.TryGame.Board).Any(b => b.IsRandomMove)) return;
                if (!EnableCheckForPassMove(board, c, tryMoves)) return;
                GameTryMove tryMove = Game.GetRandomMove(currentGame);
                if (tryMove != null) tryMoves.Add(tryMove);
            }
        }

        public static Boolean EnableCheckForPassMove(Board board, Content c = Content.Unknown, List<GameTryMove> tryMoves = null)
        {
            if (tryMoves != null && tryMoves.Any(p => GroupHelper.GetKillerGroupFromCache(board, p.Move, c) == null)) return false;
            c = (c == Content.Unknown) ? GameHelper.GetContentForSurviveOrKill(board.GameInfo, SurviveOrKill.Survive) : c;
            List<Group> killerGroups = GetKillerGroupsForBothAlive(board, c);
            if (killerGroups.Any(killerGroup => CheckForBothAlive(board, killerGroup)))
                return true;
            return false;
        }

        /// <summary>
        /// Check for both alive at move.
        /// </summary>
        public static Boolean CheckForBothAliveAtMove(Board board)
        {
            Content c = board.MoveGroup.Content;
            List<Group> killerGroups = board.GetStoneAndDiagonalNeighbours().Where(n => board[n] != c).Select(n => GroupHelper.GetKillerGroupFromCache(board, n, c)).Distinct().ToList();

            if (killerGroups.Any(killerGroup => killerGroup != null && CheckForBothAlive(board, killerGroup)))
                return true;
            return false;
        }

        /// <summary>
        /// Check for both alive.
        /// For simple seki which is usually the case, find one killer group with at least two liberties, and one survival neighbour group with at least two liberties. Simple seki <see cref="UnitTestProject.BothAliveTest.BothAliveTest_Scenario_SimpleSeki" />
        /// In most cases, there is only one killer group and one neighbour survival group, but there can also be two neighbour survival groups. Simple seki with two neighbour survival groups <see cref="UnitTestProject.BothAliveTest.BothAliveTest_Scenario_XuanXuanGo_A151_101Weiqi" />
        /// <see cref="UnitTestProject.BothAliveTest.BothAliveTest_Scenario3dan16" />
        /// Get target groups not within killer group <see cref="UnitTestProject.BothAliveTest.BothAliveTest_Scenario_WuQingYuan_Q15126_2" />
        /// Fill eye points with content <see cref="UnitTestProject.BothAliveTest.BothAliveTest_Scenario_XuanXuanGo_A27" />
        /// Two liberties for content group <see cref="UnitTestProject.BothAliveTest.BothAliveTest_Scenario_Corner_B43" />
        /// More than one content group in simple seki <see cref="UnitTestProject.BothAliveTest.BothAliveTest_Scenario_WuQingYuan_Q31646" />
        /// <see cref="UnitTestProject.BothAliveTest.BothAliveTest_20230430_8" />
        /// Ensure shared liberty suicidal for killer <see cref="UnitTestProject.BothAliveTest.BothAliveTest_Scenario_XuanXuanGo_A28_101Weiqi" />
        /// <see cref="UnitTestProject.BothAliveTest.BothAliveTest_Scenario_WuQingYuan_Q31445" />
        /// </summary>
        private static Boolean CheckForBothAlive(Board board, Group killerGroup)
        {
            Content c = killerGroup.Content;
            List<Point> emptyPoints = killerGroup.Points.Where(n => board[n] == Content.Empty).ToList();
            //two to four liberties for both alive
            if (emptyPoints.Count < 2 || emptyPoints.Count > 4) return false;

            //fill eye points with content
            Board filledBoard = FillEyePointsBoard(board, killerGroup);

            List<Point> contentPoints = killerGroup.Points.Where(n => board[n] == c).ToList();
            List<Group> contentGroups = filledBoard.GetGroupsFromPoints(contentPoints).ToList();
            //more than one content group
            if (contentGroups.Count > 2 || (contentGroups.Count == 2 && emptyPoints.Count != 2)) return false;

            List<Group> ngroups = GroupHelper.GetNeighbourGroupsOfKillerGroup(board, killerGroup);
            List<Group> killerGroups = GetKillerGroupsForBothAlive(board, c.Opposite());
            List<Group> associatedKillerGroups = killerGroups.Where(n => !n.Equals(killerGroup) && board.GetNeighbourGroups(n).Any(gr => ngroups.Contains(gr))).ToList();
            associatedKillerGroups.Insert(0, killerGroup);

            if (associatedKillerGroups.Count == 1)  //simple seki
            {
                if (ngroups.Count > 2) return false;
                //at least three content points in killer group
                if (contentPoints.Count < 3) return false;
                //at least two liberties for content groups in filled board
                if (contentGroups.Any(n => n.Liberties.Count == 1)) return false;
                return CheckSimpleSeki(board, filledBoard, ngroups, killerGroup, emptyPoints);
            }
            else if (associatedKillerGroups.Count >= 2) //complex seki
            {
                //two liberties for content group
                Boolean oneLiberty = board.GetGroupsFromPoints(contentPoints).Any(n => n.Liberties.Count == 1);
                if (oneLiberty) return false;

                //ensure shared liberty suicidal for killer
                if (!emptyPoints.Any(p => ImmovableHelper.IsSuicidalMove(board, p, c)))
                    return false;

                //find diagonal cut
                (_, List<Point> diagonals) = LinkHelper.FindDiagonalCut(board, killerGroup);
                //check complex seki without diagonal cut
                if (diagonals == null) return CheckComplexSeki(board, associatedKillerGroups, ngroups);

                //check complex seki with diagonal cut
                HashSet<Group> diagonalGroups = board.GetGroupsFromPoints(diagonals);
                if (diagonalGroups.Any(n => ImmovableHelper.CheckConnectAndDie(board, n))) return false;

                foreach (Group diagonalGroup in diagonalGroups)
                {
                    Group diagonalKillerGroup = GroupHelper.GetKillerGroupFromCache(board, diagonalGroup.Points.First(), c);
                    if (diagonalKillerGroup == null) continue;
                    List<Group> cutKillerGroups = associatedKillerGroups.Where(n => diagonalKillerGroup.Points.Contains(n.Points.First())).ToList();
                    List<Group> cutTargetGroups = ngroups.Where(n => diagonalKillerGroup.Points.Contains(n.Points.First())).ToList();
                    if (CheckComplexSeki(board, cutKillerGroups, cutTargetGroups))
                        return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Check simple seki.
        /// Ensure at least two liberties in survival neighbour group <see cref="UnitTestProject.BothAliveTest.BothAliveTest_Scenario_Corner_A87" />
        /// Cover eye point <see cref="UnitTestProject.BothAliveTest.BothAliveTest_Scenario_Corner_B43" />
        /// Check killer formation for two liberties <see cref="UnitTestProject.BothAliveTest.BothAliveTest_Scenario_Side_A23_2" />
        /// Check killer formation for three or more liberties <see cref="UnitTestProject.BothAliveTest.BothAliveTest_Scenario_WuQingYuan_Q31493_4" />
        /// Ensure killer group does not have real eye <see cref="UnitTestProject.BothAliveTest.BothAliveTest_Scenario_TianLongTu_Q16424_2" />
        /// Check for increased killer groups <see cref="UnitTestProject.BothAliveTest.BothAliveTest_Scenario_WuQingYuan_Q31445_2" />
        /// Check content group connect and die <see cref="UnitTestProject.BothAliveTest.BothAliveTest_Scenario_TianLongTu_Q16424_3" />
        /// </summary>
        private static Boolean CheckSimpleSeki(Board board, Board filledBoard, List<Group> ngroups, Group killerGroup, List<Point> emptyPoints)
        {
            Content c = killerGroup.Content;
            //ensure at least two liberties within killer group in survival neighbour group
            if (ngroups.Any(n => n.Liberties.Count(p => killerGroup.Points.Contains(p) || BothAliveDiagonalEye(board, killerGroup, p)) < 2))
                return false;

            int emptyPointCount = killerGroup.Points.Count(k => filledBoard[k] == Content.Empty);
            if (emptyPointCount >= 3)
            {
                if (!WallHelper.StrongNeighbourGroups(board, ngroups))
                    return false;
                //check killer formation for three or more liberties
                if (!KillerFormationHelper.DeadFormationInBothAlive(filledBoard, killerGroup, emptyPointCount, 2))
                    return false;
            }
            //check killer formation for two liberties
            else if (KillerFormationHelper.DeadFormationInBothAlive(filledBoard, killerGroup, emptyPointCount))
                return false;

            //ensure killer group does not have real eye
            if (emptyPoints.Any(p => EyeHelper.FindRealEyeWithinEmptySpace(board, p, c)))
                return false;

            //check for increased killer groups
            IEnumerable<Board> moveBoards = GameHelper.GetMoveBoards(board, emptyPoints, c.Opposite());
            if (moveBoards.Any(b => b.MoveGroupLiberties > 1 && GroupHelper.IncreasedKillerGroups(b, board)))
                return false;

            //check content group connect and die
            HashSet<Group> contentGroups = board.GetGroupsFromPoints(killerGroup.Points.Where(p => board[p] == c).ToList());
            if (contentGroups.Count == 1 && ImmovableHelper.CheckConnectAndDie(board, contentGroups.First()))
            {
                int contentCount = contentGroups.First().Points.Count;
                if (KillerFormationHelper.KillerFormationFuncs.ContainsKey(contentCount))
                {
                    if (!KillerFormationHelper.IsKillerFormationFromFunc(board, contentGroups.First()))
                        return false;
                }
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
        /// <see cref="UnitTestProject.BothAliveTest.BothAliveTest_20230422_8" />
        /// Without diagonal group <see cref="UnitTestProject.BothAliveTest.BothAliveTest_Scenario_Corner_A123" />
        /// Check suicidal for both players and covered eye move at liberty <see cref="UnitTestProject.BothAliveTest.BothAliveTest_Scenario_XuanXuanGo_A28_101Weiqi" />
        /// <see cref="UnitTestProject.BothAliveTest.BothAliveTest_Scenario_GuanZiPu_B18" />
        /// Find uncovered eye <see cref="UnitTestProject.BothAliveTest.BothAliveTest_Scenario_ComplexSeki" />
        /// <see cref="UnitTestProject.BothAliveTest.BothAliveTest_Scenario3dan22" />
        /// <see cref="UnitTestProject.BothAliveTest.BothAliveTest_Scenario_GuanZiPu_B18_2" />
        /// Clear all killer groups with empty points <see cref="UnitTestProject.BothAliveTest.BothAliveTest_Scenario_WindAndTime_Q30213" />
        /// <see cref="UnitTestProject.BothAliveTest.BothAliveTest_Scenario_Corner_A123" />
        /// </summary>
        private static Boolean CheckComplexSeki(Board board, List<Group> killerGroups, List<Group> ngroups)
        {
            if (killerGroups.Count == 0) return false;
            Content c = killerGroups.First().Content;

            //ensure at least two liberties within killer group in survival neighbour group
            if (ngroups.Any(n => n.Liberties.Count(p => GroupHelper.GetKillerGroupFromCache(board, p, c.Opposite()) != null) < 2))
                return false;

            //check suicidal for both players and covered eye move at liberty
            HashSet<Point> liberties = board.GetLibertiesOfGroups(ngroups);
            Boolean suicidalForBothPlayers = liberties.Any(n => ImmovableHelper.IsSuicidalMoveForBothPlayers(board, n));
            if (!suicidalForBothPlayers && !liberties.Any(n => board.GetGroupsFromStoneNeighbours(n, c.Opposite()).Any(gr => gr.Liberties.Any(s => EyeHelper.FindCoveredEyeWithLiberties(board, s, c)))))
                return false;

            //ensure at least one liberty shared with killer group
            foreach (Group killerGroup in killerGroups)
            {
                IEnumerable<Point> killerLiberties = killerGroup.Points.Where(p => board[p] == Content.Empty);
                Boolean sharedLiberty = ngroups.All(n => n.Liberties.Intersect(killerLiberties).Any());
                if (!sharedLiberty)
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Fill eye points with stone of same content.
        /// </summary>
        public static Board FillEyePointsBoard(Board board, Group killerGroup)
        {
            Content c = killerGroup.Content;
            IEnumerable<Point> killerLiberties = killerGroup.Points.Where(p => board[p] == Content.Empty);
            //ensure only one killer group with or without eye
            List<Point> eyePoints = killerLiberties.Where(t => EyeHelper.FindEye(board, t, c)).ToList();
            Board filledBoard = board;
            if (eyePoints.Count > 0)
            {
                //fill eye point with stone
                filledBoard = new Board(board);
                eyePoints.ForEach(p => filledBoard[p] = c);
            }
            return filledBoard;
        }

        /// <summary>
        /// Get killer groups for both alive.
        /// </summary>
        public static List<Group> GetKillerGroupsForBothAlive(Board board, Content c = Content.Unknown)
        {
            List<Group> killerGroups = GroupHelper.GetKillerGroups(board, c);
            if (killerGroups.Any(n => n.IsCoveredEye == null))
                CheckCoveredEyeInKillerGroup(board, killerGroups);
            return killerGroups.Where(n => !n.IsCoveredEye.Value).ToList();
        }

        /// <summary>
        /// Check covered eye in killer group.
        /// <see cref="UnitTestProject.BothAliveTest.BothAliveTest_Scenario_WindAndTime_Q30005" />
        /// Not covered eye <see cref="UnitTestProject.BothAliveTest.BothAliveTest_Scenario_Corner_A123" />
        /// <see cref="UnitTestProject.BothAliveTest.BothAliveTest_Scenario_WindAndTime_Q30213" />
        /// </summary>
        private static void CheckCoveredEyeInKillerGroup(Board board, List<Group> killerGroups)
        {
            if (killerGroups.Any(n => n.Points.Count <= 2))
            {
                //find immovable groups
                List<Group> immovableGroups = new List<Group>();
                foreach (Group killerGroup in killerGroups)
                {
                    List<Point> emptyPoints = killerGroup.Points.Where(p => board[p] == Content.Empty).ToList();
                    if (emptyPoints.Count != 2) continue;
                    if (emptyPoints.All(p => ImmovableHelper.IsSuicidalMoveForBothPlayers(board, p)))
                        immovableGroups.Add(killerGroup);
                }

                //clear immovable groups with empty points
                Board b = board;
                if (immovableGroups.Any())
                {
                    b = new Board(board);
                    immovableGroups.ForEach(n => n.Points.ToList().ForEach(p => b[p] = Content.Empty));
                }
                //find uncovered eye
                foreach (Group group in killerGroups)
                {
                    Boolean unCoveredEye = group.Points.Count > 2 || EyeHelper.FindRealEyeWithinEmptySpace(b, group, EyeType.UnCoveredEye);
                    if (!unCoveredEye)
                        group.IsCoveredEye = true;
                }
            }
            killerGroups.Where(n => n.IsCoveredEye == null).ToList().ForEach(n => n.IsCoveredEye = false);
        }

        /// <summary>
        /// Add pass move for game try move.
        /// </summary>
        public static GameTryMove AddPassMove(Game currentGame)
        {
            GameTryMove tryMove = new GameTryMove(currentGame);
            tryMove.TryGame.Board.Move = Game.PassMove;
            tryMove.MakeMoveResult = MakeMoveResult.Legal;
            tryMove.TryGame.Board.LastMoves.Add(Game.PassMove);
            return tryMove;
        }
    }
}
