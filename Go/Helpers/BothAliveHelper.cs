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
        public static void EnablePassMoveForBothAlive(Game g, List<GameTryMove> tryMoves, SurviveOrKill surviveOrKill)
        {
            Board board = g.Board;
            Content c = GameHelper.GetContentForSurviveOrKill(g.GameInfo, surviveOrKill);
            if (surviveOrKill == SurviveOrKill.Survive)
            {
                if (!EnableCheckForPassMove(board, c, tryMoves)) return;
                tryMoves.Add(BothAliveHelper.AddPassMove(g));
            }
            else
            {
                if (board.IsPassMove) return;
                if (tryMoves.Count == 1 && tryMoves.Select(n => n.TryGame.Board).Any(b => b.IsRandomMove)) return;
                if (!EnableCheckForPassMove(board, c, tryMoves)) return;
                GameTryMove tryMove = Game.GetRandomMove(g);
                if (tryMove != null) tryMoves.Add(tryMove);
            }
        }

        public static Boolean EnableCheckForPassMove(Board board, Content c = Content.Unknown, List<GameTryMove> tryMoves = null)
        {
            if (tryMoves != null && tryMoves.Any(p => GroupHelper.GetDirectKillerGroup(board, p.Move, c) == null)) return false;
            c = (c == Content.Unknown) ? GameHelper.GetContentForSurviveOrKill(board.GameInfo, SurviveOrKill.Survive) : c;
            IEnumerable<Group> killerGroups = GetKillerGroupsForBothAlive(board, c);
            if (killerGroups.Any(n => CheckForBothAlive(board, n)))
                return true;
            return false;
        }

        /// <summary>
        /// Check for both alive at move.
        /// </summary>
        public static Boolean CheckForBothAliveAtMove(Board board)
        {
            Content c = board.MoveGroup.Content;
            List<Group> killerGroups = GroupHelper.GetKillerGroupsFromPoints(board.GetStoneAndDiagonalNeighbours(), board, c);
            if (killerGroups.Any(n => n != null && CheckForBothAlive(board, n)))
                return true;
            return false;
        }

        /// <summary>
        /// Check for both alive.
        /// Simple seki <see cref="UnitTestProject.BothAliveTest.BothAliveTest_Scenario_SimpleSeki" />
        /// Complex seki <see cref="UnitTestProject.BothAliveTest.BothAliveTest_Scenario_WuQingYuan_Q15126_2" />
        /// </summary>
        private static Boolean CheckForBothAlive(Board board, Group killerGroup)
        {
            Content c = killerGroup.Content;
            List<Point> emptyPoints = killerGroup.Points.Where(n => board[n] == Content.Empty).ToList();
            if (emptyPoints.Count < 2 || emptyPoints.Count > 4) return false;

            List<Group> ngroups = GroupHelper.GetNeighbourGroupsOfKillerGroup(board, killerGroup);
            if (!WallHelper.StrongGroups(board, ngroups)) return false;

            //simple seki
            if (CheckSimpleSeki(board, killerGroup))
                return true;

            //complex seki
            if (!emptyPoints.Any(p => ImmovableHelper.IsSuicidalMove(board, p, c)))
                return false;
            if (CheckComplexSeki(board, killerGroup, ngroups))
                return true;
            return false;
        }

        /// <summary>
        /// Check simple seki.
        /// Ensure no real eye <see cref="UnitTestProject.BothAliveTest.BothAliveTest_Scenario_TianLongTu_Q16424_2" />
        /// Two content groups <see cref="UnitTestProject.BothAliveTest.BothAliveTest_Scenario_WuQingYuan_Q31646" />
        /// <see cref="UnitTestProject.BothAliveTest.BothAliveTest_20230430_8" />
        /// Check for two liberty formation <see cref="UnitTestProject.BothAliveTest.BothAliveTest_Scenario_Side_A23_2" />
        /// Check for three or more liberty formation <see cref="UnitTestProject.BothAliveTest.BothAliveTest_Scenario_WuQingYuan_Q31493_4" />
        /// Check for increased killer groups <see cref="UnitTestProject.BothAliveTest.BothAliveTest_Scenario_WuQingYuan_Q31445_2" />
        /// Check content group connect and die <see cref="UnitTestProject.BothAliveTest.BothAliveTest_Scenario_TianLongTu_Q16424_3" />
        /// Check for two groups <see cref="UnitTestProject.BothAliveTest.BothAliveTest_20230430_8_2" />
        /// </summary>
        private static Boolean CheckSimpleSeki(Board board, Group killerGroup)
        {
            Content c = killerGroup.Content;
            List<Point> contentPoints = killerGroup.Points.Where(n => board[n] == c).ToList();
            if (contentPoints.Count < 3) return false;

            //ensure no real eye
            List<Point> emptyPoints = killerGroup.Points.Where(n => board[n] == Content.Empty).ToList();
            if (emptyPoints.Any(p => EyeHelper.FindRealEyeWithinEmptySpace(board, p, c)))
                return false;

            //fill eye points with content
            Board filledBoard = FillEyePointsBoard(board, killerGroup);
            killerGroup = GroupHelper.GetKillerGroupFromCache(filledBoard, killerGroup.Points.First(), c.Opposite());
            if (killerGroup == null) return false;

            //two content groups
            List<Group> contentGroups = filledBoard.GetGroupsFromPoints(contentPoints).ToList();
            if (contentGroups.Count > 2 || contentGroups.Any(n => n.Liberties.Count == 1)) return false;

            //ensure at least two liberties within killer group
            List<Group> ngroups = GroupHelper.GetNeighbourGroupsOfKillerGroup(filledBoard, killerGroup);
            if (ngroups.Any(n => n.Liberties.Count(p => GroupHelper.GetDirectKillerGroup(filledBoard, p, c.Opposite()) == killerGroup) < 2))
                return false;

            if (contentGroups.Count() == 1)
            {
                int emptyPointCount = killerGroup.Points.Count(k => filledBoard[k] == Content.Empty);
                if (emptyPointCount >= 3)
                {
                    //check for three or more liberty formation
                    if (!KillerFormationHelper.DeadFormationInBothAlive(filledBoard, killerGroup, emptyPointCount, 2).Item1)
                        return false;
                }
                else if (emptyPointCount == 2)
                {
                    //check for two liberty formation
                    if (KillerFormationHelper.DeadFormationInBothAlive(filledBoard, killerGroup, emptyPointCount).Item1)
                        return false;
                }
            }

            //check content group connect and die
            if (board.Move != null && board[board.Move.Value] == c)
            {
                HashSet<Group> cGroups = board.GetGroupsFromPoints(contentPoints);
                if (cGroups.Count == 1)
                {
                    Group cGroup = cGroups.First();
                    if (ImmovableHelper.CheckConnectAndDie(board, cGroup))
                    {
                        if (cGroup.Points.Count > 3 && !KillerFormationHelper.IsKillerFormationFromFunc(board, cGroup))
                            return false;
                    }
                }
            }

            //check for increased killer groups
            List<Point> ePoints = emptyPoints.Where(n => board.GetStoneNeighbours(n).Intersect(emptyPoints).Any()).ToList();
            if (ePoints.Any())
            {
                IEnumerable<Board> moveBoards = GameHelper.GetMoveBoards(board, ePoints, c.Opposite());
                if (moveBoards.Any(b => b.MoveGroupLiberties > 1 && GroupHelper.IncreasedKillerGroups(b, board)))
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Check complex seki.
        /// With diagonal cut <see cref="UnitTestProject.BothAliveTest.BothAliveTest_Scenario3dan22" />
        /// <see cref="UnitTestProject.BothAliveTest.BothAliveTest_20230422_8" />
        /// Without diagonal cut <see cref="UnitTestProject.BothAliveTest.BothAliveTest_Scenario_Corner_A123" />
        /// <see cref="UnitTestProject.BothAliveTest.BothAliveTest_Scenario_GuanZiPu_B18" />
        /// </summary>
        private static Boolean CheckComplexSeki(Board board, Group killerGroup, List<Group> ngroups)
        {
            Content c = killerGroup.Content;
            List<Group> killerGroups = GetKillerGroupsForBothAlive(board, c.Opposite()).ToList();
            if (killerGroups.Count < 2) return false;
            List<Point> contentPoints = killerGroup.Points.Where(n => board[n] == c).ToList();
            if (board.GetGroupsFromPoints(contentPoints).Any(n => n.Liberties.Count == 1)) return false;

            //check complex seki without diagonal cut
            (_, List<Point> diagonals) = LinkHelper.FindDiagonalCut(board, killerGroup, true);
            if (diagonals == null) return IsComplexSeki(board, killerGroups, ngroups);

            //check complex seki with diagonal cut
            foreach (Point d in diagonals)
            {
                Group dkillerGroup = GroupHelper.GetDirectKillerGroup(board, d, c);
                if (dkillerGroup == null) continue;
                List<Group> cutKillerGroups = killerGroups.Where(n => GroupHelper.GetKillerGroupFromCache(board, n.Points.First(), c) == dkillerGroup).ToList();
                List<Group> cutTargetGroups = ngroups.Where(n => GroupHelper.GetKillerGroupFromCache(board, n.Points.First(), c) == dkillerGroup).ToList();
                if (IsComplexSeki(board, cutKillerGroups, cutTargetGroups))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Is complex seki.
        /// Check covered eye <see cref="UnitTestProject.BothAliveTest.BothAliveTest_Scenario_XuanXuanGo_A28_101Weiqi" />
        /// Ensure shared liberty <see cref="UnitTestProject.BothAliveTest.BothAliveTest_Scenario_XuanXuanGo_A28_101Weiqi_3" />
        /// </summary>
        private static Boolean IsComplexSeki(Board board, List<Group> killerGroups, List<Group> ngroups)
        {
            if (killerGroups.Count == 0) return false;
            Content c = killerGroups.First().Content;

            //ensure at least two liberties
            if (ngroups.Any(n => n.Liberties.Count(p => GroupHelper.GetDirectKillerGroup(board, p, c.Opposite()) != null) < 2))
                return false;

            //ensure suicidal move
            HashSet<Point> liberties = board.GetLibertiesOfGroups(ngroups);
            Boolean suicidalForBothPlayers = liberties.Any(n => ImmovableHelper.IsSuicidalMoveForBothPlayers(board, n));
            if (!suicidalForBothPlayers)
            {
                //check covered eye
                if (!killerGroups.Any(kgroup => kgroup.Points.Any(n => EyeHelper.FindCoveredEye(board, n, c))))
                    return false;
            }

            //ensure shared liberty
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
        /// Fill eye point in killer group <see cref="UnitTestProject.BothAliveTest.BothAliveTest_Scenario_XuanXuanGo_A27" />
        /// <see cref="UnitTestProject.BothAliveTest.BothAliveTest_Scenario_Corner_B43" />
        /// Fill eye point in neighbour group <see cref="UnitTestProject.FillKoEyeMoveTest.FillKoEyeMoveTest_Scenario_WindAndTime_Q30275" />
        /// </summary>
        public static Board FillEyePointsBoard(Board board, Group killerGroup)
        {
            Content c = killerGroup.Content;
            Board filledBoard = null;

            //fill eye point in killer group
            List<Point> eyePoints = killerGroup.Points.Where(t => EyeHelper.FindCoveredEye(board, t, c)).ToList();
            if (eyePoints.Count > 0)
            {
                filledBoard = new Board(board);
                eyePoints.ForEach(p => filledBoard[p] = c);
            }

            //fill eye point in neighbour group
            foreach (Link<Point> p in LinkHelper.GetGroupDiagonals(board, killerGroup))
            {
                if (!EyeHelper.FindEye(board, p.Move, c.Opposite())) continue;
                if (board[(Point)p.CheckMove] != Content.Empty) continue;
                if (filledBoard == null) filledBoard = new Board(board);
                filledBoard[p.Move] = c.Opposite();
            }
            if (filledBoard == null) filledBoard = board;
            return filledBoard;
        }

        /// <summary>
        /// Get killer groups for both alive.
        /// Check covered eye in killer group <see cref="UnitTestProject.BothAliveTest.BothAliveTest_Scenario_WindAndTime_Q30005" />
        /// Not covered eye <see cref="UnitTestProject.BothAliveTest.BothAliveTest_Scenario_Corner_A123" />
        /// <see cref="UnitTestProject.BothAliveTest.BothAliveTest_Scenario_WindAndTime_Q30213" />
        /// </summary>
        public static IEnumerable<Group> GetKillerGroupsForBothAlive(Board board, Content c = Content.Unknown)
        {
            List<Group> killerGroups = GroupHelper.GetKillerGroups(board, c).Where(n => GroupHelper.CheckNeighbourGroupsOfKillerGroup(board, n).Item1).ToList();
            foreach (Group group in killerGroups)
            {
                if (group.Points.Count <= 2 && !EyeHelper.FindRealEyeWithinEmptySpace(board, group, EyeType.UnCoveredEye))
                {
                    foreach (Link<Point> p in LinkHelper.GetGroupDiagonals(board, group))
                    {
                        if (board[p.Move] != c.Opposite()) continue;
                        Group killerGroup = GroupHelper.GetDirectKillerGroup(board, p.Move, c);
                        if (!killerGroups.Contains(killerGroup)) continue;
                        if (ImmovableHelper.CheckConnectAndDie(board, board.GetGroupAt(p.Move), false)) continue;
                        Board b = new Board(board);
                        b[p.Move] = Content.Empty;
                        if (EyeHelper.FindRealEyeWithinEmptySpace(b, group, EyeType.UnCoveredEye))
                            yield return group;
                    }
                }
                else
                    yield return group;
            }
        }

        /// <summary>
        /// Add pass move for game try move.
        /// </summary>
        public static GameTryMove AddPassMove(Game g)
        {
            GameTryMove tryMove = new GameTryMove(g);
            tryMove.TryGame.Board.Move = Game.PassMove;
            tryMove.MakeMoveResult = MakeMoveResult.Legal;
            return tryMove;
        }
    }
}
