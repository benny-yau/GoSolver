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
            List<Group> killerGroups = board.GetStoneAndDiagonalNeighbours().Where(n => board[n] != c).Select(n => GroupHelper.GetKillerGroupFromCache(board, n, c)).Distinct().ToList();

            if (killerGroups.Any(n => n != null && CheckForBothAlive(board, n)))
                return true;
            return false;
        }

        /// <summary>
        /// Check for both alive.
        /// Simple seki <see cref="UnitTestProject.BothAliveTest.BothAliveTest_Scenario_SimpleSeki" />
        /// Fill eye points with content <see cref="UnitTestProject.BothAliveTest.BothAliveTest_Scenario_XuanXuanGo_A27" />
        /// <see cref="UnitTestProject.BothAliveTest.BothAliveTest_Scenario_Corner_B43" />
        /// More than one content group <see cref="UnitTestProject.BothAliveTest.BothAliveTest_Scenario_WuQingYuan_Q31646" />
        /// <see cref="UnitTestProject.BothAliveTest.BothAliveTest_20230430_8" />
        /// Complex seki <see cref="UnitTestProject.BothAliveTest.BothAliveTest_Scenario_WuQingYuan_Q15126_2" />
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
            if (contentGroups.Count > 2) return false;

            List<Group> ngroups = GroupHelper.GetNeighbourGroupsOfKillerGroup(board, killerGroup);
            List<Group> killerGroups = GetKillerGroupsForBothAlive(board, c.Opposite()).ToList();

            if (killerGroups.Count == 1)  //simple seki
            {
                if (ngroups.Count > 2) return false;
                if (contentPoints.Count < 3) return false;
                if (contentGroups.Any(n => n.Liberties.Count == 1)) return false;
                return CheckSimpleSeki(board, filledBoard, ngroups, killerGroup, emptyPoints);
            }
            else if (killerGroups.Count >= 2) //complex seki
            {
                Boolean oneLiberty = board.GetGroupsFromPoints(contentPoints).Any(n => n.Liberties.Count == 1);
                if (oneLiberty) return false;

                if (!emptyPoints.Any(p => ImmovableHelper.IsSuicidalMove(board, p, c)))
                    return false;

                //check complex seki without diagonal cut
                (_, List<Point> diagonals) = LinkHelper.FindDiagonalCut(board, killerGroup);
                if (diagonals == null) return CheckComplexSeki(board, killerGroups, ngroups);

                //check complex seki with diagonal cut
                HashSet<Group> dgroups = board.GetGroupsFromPoints(diagonals);
                if (dgroups.Any(n => ImmovableHelper.CheckConnectAndDie(board, n))) return false;

                foreach (Group dgroup in dgroups)
                {
                    Group dkillerGroup = GroupHelper.GetKillerGroupFromCache(board, dgroup.Points.First(), c);
                    if (dkillerGroup == null) continue;
                    List<Group> cutKillerGroups = killerGroups.Where(n => GroupHelper.GetKillerGroupFromCache(board, n.Points.First(), c) == dkillerGroup).ToList();
                    List<Group> cutTargetGroups = ngroups.Where(n => GroupHelper.GetKillerGroupFromCache(board, n.Points.First(), c) == dkillerGroup).ToList();
                    if (CheckComplexSeki(board, cutKillerGroups, cutTargetGroups))
                        return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Check simple seki.
        /// Check for two liberty formation <see cref="UnitTestProject.BothAliveTest.BothAliveTest_Scenario_Side_A23_2" />
        /// Check for three or more liberty formation <see cref="UnitTestProject.BothAliveTest.BothAliveTest_Scenario_WuQingYuan_Q31493_4" />
        /// Ensure no real eye <see cref="UnitTestProject.BothAliveTest.BothAliveTest_Scenario_TianLongTu_Q16424_2" />
        /// Check for increased killer groups <see cref="UnitTestProject.BothAliveTest.BothAliveTest_Scenario_WuQingYuan_Q31445_2" />
        /// Check content group connect and die <see cref="UnitTestProject.BothAliveTest.BothAliveTest_Scenario_TianLongTu_Q16424_3" />
        /// Check for two groups <see cref="UnitTestProject.BothAliveTest.BothAliveTest_20230430_8_2" />
        /// </summary>
        private static Boolean CheckSimpleSeki(Board board, Board filledBoard, List<Group> ngroups, Group killerGroup, List<Point> emptyPoints)
        {
            Content c = killerGroup.Content;
            //ensure at least two liberties within killer group
            if (ngroups.Any(n => n.Liberties.Count(p => GroupHelper.GetKillerGroupFromCache(board, p, c.Opposite()) == killerGroup || BothAliveDiagonalEye(board, killerGroup, p)) < 2))
                return false;

            int groupCount = filledBoard.GetGroupsFromPoints(killerGroup.Points.Where(p => board[p] == c).ToList()).Count();
            int emptyPointCount = killerGroup.Points.Count(k => filledBoard[k] == Content.Empty);
            if (emptyPointCount >= 3)
            {
                if (groupCount == 1)
                {
                    if (!WallHelper.StrongNeighbourGroups(board, ngroups))
                        return false;
                    //check for three or more liberty formation
                    if (!KillerFormationHelper.DeadFormationInBothAlive(filledBoard, killerGroup, emptyPointCount, 2))
                        return false;
                }
            }
            //check for two liberty formation
            else if (KillerFormationHelper.DeadFormationInBothAlive(filledBoard, killerGroup, emptyPointCount))
                return false;

            //ensure no real eye
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
                if (board.GetDiagonalNeighbours(eye).Any(n => GroupHelper.GetKillerGroupFromCache(board, n, c.Opposite()) == killerGroup))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Check complex seki.
        /// With diagonal group <see cref="UnitTestProject.BothAliveTest.BothAliveTest_Scenario3dan22" />
        /// <see cref="UnitTestProject.BothAliveTest.BothAliveTest_20230422_8" />
        /// Without diagonal group <see cref="UnitTestProject.BothAliveTest.BothAliveTest_Scenario_Corner_A123" />
        /// <see cref="UnitTestProject.BothAliveTest.BothAliveTest_Scenario_GuanZiPu_B18" />
        /// Check covered eye <see cref="UnitTestProject.BothAliveTest.BothAliveTest_Scenario_XuanXuanGo_A28_101Weiqi" />
        /// Ensure shared liberty <see cref="UnitTestProject.BothAliveTest.BothAliveTest_Scenario_XuanXuanGo_A28_101Weiqi_3" />
        /// </summary>
        private static Boolean CheckComplexSeki(Board board, List<Group> killerGroups, List<Group> ngroups)
        {
            if (killerGroups.Count == 0) return false;
            Content c = killerGroups.First().Content;

            //ensure at least two liberties
            if (ngroups.Any(n => n.Liberties.Count(p => GroupHelper.GetKillerGroupFromCache(board, p, c.Opposite()) != null) < 2))
                return false;

            //ensure suicidal move
            HashSet<Point> liberties = board.GetLibertiesOfGroups(ngroups);
            Boolean suicidalForBothPlayers = liberties.Any(n => ImmovableHelper.IsSuicidalMoveForBothPlayers(board, n));
            if (!suicidalForBothPlayers)
            {
                //check covered eye
                if (!killerGroups.Any(kgroup => kgroup.Points.Any(n => EyeHelper.FindCoveredEyeWithLiberties(board, n, c))))
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
        /// </summary>
        public static Board FillEyePointsBoard(Board board, Group killerGroup)
        {
            Content c = killerGroup.Content;
            //ensure only one killer group with or without eye
            List<Point> eyePoints = killerGroup.Points.Where(t => EyeHelper.FindEye(board, t, c)).ToList();
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
                    Board b = new Board(board);
                    List<LinkedPoint<Point>> rc = LinkHelper.GetGroupDiagonals(board, group);
                    foreach (LinkedPoint<Point> p in rc)
                    {
                        if (b[p.Move] != c.Opposite()) continue;
                        Group killerGroup = GroupHelper.GetKillerGroupFromCache(board, p.Move, c);
                        if (!killerGroups.Contains(killerGroup)) continue;
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
