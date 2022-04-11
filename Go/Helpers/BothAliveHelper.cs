using System;
using System.Collections.Generic;
using System.Linq;

namespace Go
{
    public class BothAliveHelper
    {
        /// <summary>
        /// Get killer group fully surrounded by survival stones. Content in parameter refer to target content (usually survival). 
        /// </summary>
        public static List<Group> GetCorneredKillerGroup(Board board, Content content, Boolean checkLiberties = true)
        {
            List<Group> killerGroups = null;
            if (board.CorneredKillerGroup == null || !board.CorneredKillerGroup.ContainsKey(content))
            {
                //get cornered group
                killerGroups = GetCorneredGroup(board, content);
                if (killerGroups.Count > 0)
                {
                    //cache groups in board
                    if (board.CorneredKillerGroup == null)
                        board.CorneredKillerGroup = new Dictionary<Content, List<Group>>();
                    board.CorneredKillerGroup.Add(content, killerGroups);
                }
            }
            else
            {
                //retrieve from cache
                killerGroups = board.CorneredKillerGroup[content];
            }

            if (checkLiberties)
            {
                //ensure group contain two liberties
                if (killerGroups.Count > 0 && !IsLibertyGroup(killerGroups.First(), board))
                    return new List<Group>();
            }
            return killerGroups;
        }

        public static List<Group> GetCorneredKillerGroup(Board m, Boolean checkLiberties = true)
        {
            Content c = GameHelper.GetContentForSurviveOrKill(m.GameInfo, SurviveOrKill.Survive);
            return GetCorneredKillerGroup(m, c, checkLiberties);
        }


        /// <summary>
        /// Get cornered group fully surrounded by opponent.
        /// Remove where group is covered eye <see cref="UnitTestProject.BothAliveTest.BothAliveTest_Scenario_WindAndTime_Q30005" />
        /// </summary>
        public static List<Group> GetCorneredGroup(Board board, Content content)
        {
            List<Group> killerGroups = new List<Group>();
            Board filledBoard = new Board(board);
            //cover all empty points
            GameInfo gameInfo = board.GameInfo;
            Boolean isKill = (GameHelper.GetContentForSurviveOrKill(gameInfo, SurviveOrKill.Kill) == content.Opposite());
            List<Point> coverPoints = (isKill) ? gameInfo.killMovablePoints : gameInfo.movablePoints;
            List<Point> emptyPoints = coverPoints.Where(p => filledBoard[p] == Content.Empty).ToList();
            emptyPoints.ForEach(p => filledBoard[p] = content.Opposite());

            HashSet<Group> groups = filledBoard.GetGroupsFromPoints(emptyPoints);
            foreach (Group group in groups)
            {
                //find killer groups with no liberties left
                if (group.Liberties.Count == 0)
                {
                    if (IsLibertyGroup(group, board))
                    {
                        //return as liberty group as first group
                        killerGroups.Insert(0, group);
                        continue;
                    }
                    killerGroups.Add(group);
                }
            }

            Board clearedBoard = new Board(board);
            //clear all killer groups with empty points
            killerGroups.ForEach(group => group.Points.ToList().ForEach(p => clearedBoard[p] = Content.Empty));
            //remove where group is covered eye (or false eye)
            killerGroups.RemoveAll(group => group.Points.Count <= 2 && !EyeHelper.FindRealEyeWithinEmptySpace(clearedBoard, group, EyeType.UnCoveredEye));
            return killerGroups;
        }

        /// <summary>
        /// Get killer group cached in board for single point.
        /// </summary>
        public static Group GetKillerGroupFromCache(Board board, Point p, Content c = Content.Unknown)
        {
            c = (c == Content.Unknown) ? GameHelper.GetContentForSurviveOrKill(board.GameInfo, SurviveOrKill.Survive) : c;
            List<Group> groups = GetCorneredKillerGroup(board, c, false);
            Group group = groups.FirstOrDefault(g => g.Points.Contains(p));
            if (group == null) return null;
            return (c == group.Content.Opposite()) ? group : null;
        }

        /// <summary>
        /// Get killer group for killer role.
        /// Survival in killer role <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_GuanZiPu_B3" />
        /// </summary>
        public static Group GetKillerGroupForKillerRole(Board board, Point p, Content c)
        {
            List<Group> groups = GetCorneredGroup(board, c);
            Group group = groups.FirstOrDefault(g => g.Points.Contains(p));
            if (group == null) return null;

            List<Group> killerGroups = GetCorneredGroup(board, c.Opposite());
            Group killerGroup = killerGroups.FirstOrDefault(g => g.Points.Contains(p));
            if (killerGroup == null) return group;
            if (killerGroup != null && group.Points.Count < killerGroup.Points.Count) return group;
            return null;
        }

        /// <summary>
        /// Liberty group requires at least two content points and two empty points.
        /// </summary>
        private static Boolean IsLibertyGroup(Group group, Board board)
        {
            if (group.Content == Content.Empty) return false;
            return (group.Points.Count(t => board[t] == group.Content) >= 2 && group.Points.Count(t => board[t] == Content.Empty) >= 2);
        }


        /// <summary>
        /// Add pass move to survival try moves if enabled, in order to check for both alive. Ensure no other try move present other than those within killer group.
        /// Enable pass move for ten thousand year ko as well.
        /// For simple seki which is usually the case, find one killer group with at least two liberties, and one survival neighbour group with at least two liberties. Simple seki <see cref="UnitTestProject.BothAliveTest.BothAliveTest_Scenario_SimpleSeki" />
        /// In most cases, there is only one killer group and one neighbour survival group, but there can also be two neighbour survival groups. Simple seki with two neighbour survival groups <see cref="UnitTestProject.BothAliveTest.BothAliveTest_Scenario_XuanXuanGo_A151_101Weiqi" />
        /// <see cref="UnitTestProject.BothAliveTest.BothAliveTest_Scenario3dan16" />
        /// One killer group with eye <see cref="UnitTestProject.BothAliveTest.BothAliveTest_Scenario_XuanXuanGo_A27" />
        /// Two liberties for content group <see cref="UnitTestProject.BothAliveTest.BothAliveTest_Scenario_Corner_B43" />
        /// Two liberties within killer group <see cref="UnitTestProject.BothAliveTest.BothAliveTest_Scenario_Corner_A87" />
        /// Ensure at least two liberties in survival neighbour group <see cref="UnitTestProject.BothAliveTest.BothAliveTest_Scenario3dan22_2" />
        /// More than one content group in simple seki <see cref="UnitTestProject.BothAliveTest.BothAliveTest_Scenario_WuQingYuan_Q31646" />
        /// </summary>
        public static Boolean EnableCheckForPassMove(Board board, List<GameTryMove> tryMoves = null)
        {
            List<Group> killerGroups = GetCorneredKillerGroup(board);
            if (killerGroups.Count == 0) return false;
            if (UniquePatternsHelper.CheckForTenThousandYearKo(board))
                return true;

            Group killerGroup = killerGroups[0];
            Content content = killerGroup.Content;
            List<Point> emptyPoints = killerGroup.Points.Where(k => board[k] == Content.Empty).ToList();
            if (emptyPoints.Count < 2 || emptyPoints.Count > 4) return false;
            if (emptyPoints.Count > 2 && !emptyPoints.Any(p => board.GetStoneNeighbours(p.x, p.y).All(n => board[n] != Content.Empty)))
                return false;

            if (tryMoves != null)
            {
                if (tryMoves.Any(t => t.TryGame.Board.CapturedPoints.Count() > 0))
                    return false;

                //all game try moves should be within killer group to enable pass move
                List<GameTryMove> externalMoves = tryMoves.Where(p => !emptyPoints.Contains(p.Move) && !ImmovableHelper.SinglePointOpponentImmovable(p)).ToList();
                if (externalMoves.Count > 0)
                {
                    //enable pass move for ten thousand year ko
                    if (externalMoves.Count == 1)
                    {
                        Board b = board.MakeMoveOnNewBoard(externalMoves.First().Move, content, true);
                        if (b != null && UniquePatternsHelper.CheckForTenThousandYearKo(b))
                            return true;
                    }
                    return false;
                }
            }

            //ensure at least two liberties in survival neighbour group
            List<Group> neighbourGroups = board.GetNeighbourGroups(killerGroup);
            if (neighbourGroups.Any(g => g.Liberties.Count == 1 && BothAliveHelper.GetKillerGroupFromCache(board, g.Points.First(), g.Content.Opposite()) == null))
                return false;

            Board filledBoard = FillEyePointsBoard(board, killerGroup);

            //check for simple seki and complex seki
            List<Point> contentPoints = killerGroup.Points.Where(t => board[t] == killerGroup.Content).ToList();
            List<Group> contentGroups = filledBoard.GetGroupsFromPoints(contentPoints).ToList();

            //more than one content group
            if (contentGroups.Count > 2 || (contentGroups.Count == 2 && emptyPoints.Count != 2)) return false;
            if (contentGroups.Count == 2 && !LinkHelper.IsDiagonallyConnectedGroups(board, contentGroups[0], contentGroups[1])) return false;

            if (killerGroups.Count == 1)  //simple seki
            {
                return CheckSimpleSeki(board, filledBoard, neighbourGroups, killerGroup, emptyPoints, contentPoints, contentGroups);
            }
            else if (killerGroups.Count >= 2) //complex seki
            {
                if (emptyPoints.Count > 3) return false;
                //two liberties for content group
                Boolean oneLiberty = board.GetGroupsFromPoints(contentPoints).Any(group => group.Liberties.Count == 1);
                if (oneLiberty) return false;

                foreach (Point p in emptyPoints)
                {
                    //ensure shared liberty
                    if (board.GetGroupsFromStoneNeighbours(p, content).Any(group => group.Liberties.Count > 1))
                    {
                        //ensure suicidal for both players
                        if (!ImmovableHelper.IsSuicidalMoveForBothPlayers(board, p)) return false;
                    }
                }

                //find diagonal cut
                (_, List<Point> pointsBetweenDiagonals) = LinkHelper.FindDiagonalCut(board, killerGroup);
                //check complex seki without diagonal cut
                if (pointsBetweenDiagonals == null) return CheckComplexSeki(board, killerGroups, neighbourGroups);

                //check complex seki with diagonal cut
                HashSet<Group> diagonalGroups = board.GetGroupsFromPoints(pointsBetweenDiagonals);
                foreach (Group diagonalGroup in diagonalGroups)
                {
                    Group diagonalKillerGroup = BothAliveHelper.GetKillerGroupFromCache(board, diagonalGroup.Points.First(), killerGroup.Content);
                    if (diagonalKillerGroup == null) continue;
                    List<Group> cutKillerGroups = killerGroups.Where(g => diagonalKillerGroup.Points.Contains(g.Points.First())).ToList();
                    List<Group> cutNeighbourGroups = neighbourGroups.Where(group => diagonalKillerGroup.Points.Contains(group.Points.First())).ToList();
                    if (CheckComplexSeki(board, cutKillerGroups, cutNeighbourGroups))
                        return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Check simple seki.
        /// Cover eye point <see cref="UnitTestProject.BothAliveTest.BothAliveTest_Scenario_Corner_B43" />
        /// Check diagonal at eye point <see cref="UnitTestProject.BothAliveTest.BothAliveTest_Scenario_Corner_A75" />
        /// <see cref="UnitTestProject.BothAliveTest.BothAliveTest_Scenario_WindAndTime_Q30275" />
        /// Check killer formation for two liberties <see cref="UnitTestProject.BothAliveTest.BothAliveTest_Scenario_Side_A23_2" />
        /// Check killer formation for three or more liberties <see cref="UnitTestProject.BothAliveTest.BothAliveTest_Scenario_WuQingYuan_Q31493_4" />
        /// Ensure killer group does not have real eye <see cref="UnitTestProject.BothAliveTest.BothAliveTest_Scenario_TianLongTu_Q16424_2" />
        /// Check for increased killer groups <see cref="UnitTestProject.BothAliveTest.BothAliveTest_Scenario_WuQingYuan_Q31445_2" />
        /// </summary>
        private static Boolean CheckSimpleSeki(Board board, Board filledBoard, List<Group> neighbourGroups, Group killerGroup, List<Point> emptyPoints, List<Point> contentPoints, List<Group> contentGroups)
        {
            Content content = killerGroup.Content;
            if (neighbourGroups.Count > 2) return false;
            //at least three content points in killer group
            if (contentPoints.Count < 3) return false;
            //two liberties for content group
            Boolean oneLiberty = contentGroups.Any(group => group.Liberties.Count == 1);
            Boolean koEnabled = KoHelper.KoSurvivalEnabled(SurviveOrKill.Survive, board.GameInfo);
            if (koEnabled && oneLiberty) return false;
            else if (oneLiberty && (contentGroups.Count != 1 || contentGroups.Any(group => group.Liberties.Count != 2))) return false;

            //ensure at least two liberties shared with killer group
            Boolean sharedLiberty = neighbourGroups.All(neighbourGroup => neighbourGroup.Liberties.Intersect(emptyPoints).Count() >= 2);
            if (!sharedLiberty) return false;

            //ensure at least two liberties within killer group in survival neighbour group
            if (neighbourGroups.Any(n => n.Liberties.Count(p => GetKillerGroupFromCache(board, p) != null) < 2))
                return false;

            //check diagonal at eye point
            List<Point> eyePoints = emptyPoints.Where(p => EyeHelper.FindEye(board, p, content)).ToList();
            if (eyePoints.Count > 0 && eyePoints.All(p => board.GetDiagonalNeighbours(p.x, p.y).Any(n => board[n] == Content.Empty && !ImmovableHelper.IsSuicidalMoveForBothPlayers(board, n))))
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
                Board b = board.MakeMoveOnNewBoard(emptyPoint, content.Opposite(), true);
                if (b != null && b.MoveGroupLiberties > 1 && GameTryMove.IncreaseKillerGroups(b, board))
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Check complex seki.
        /// With diagonal group <see cref="UnitTestProject.BothAliveTest.BothAliveTest_Scenario3dan22" />
        /// Without diagonal group <see cref="UnitTestProject.BothAliveTest.BothAliveTest_Scenario_Corner_A123" />
        /// </summary>
        private static Boolean CheckComplexSeki(Board board, List<Group> killerGroups, List<Group> groups)
        {
            if (killerGroups.Count == 0) return false;
            IEnumerable<Point> killerLiberties = killerGroups.First().Points.Where(p => board[p] == Content.Empty);
            //ensure at least one liberty shared with killer group
            Boolean sharedLiberty = groups.All(group => group.Liberties.Intersect(killerLiberties).Any());
            if (!sharedLiberty) return false;

            //ensure at least two liberties within killer group in survival neighbour group
            if (groups.Any(n => n.Liberties.Count(p => GetKillerGroupFromCache(board, p) != null) < 2))
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
            List<Point> eyePoints = killerLiberties.Where(t => EyeHelper.FindEye(board, t.x, t.y, killerGroup.Content)).ToList();
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
                if (EyeHelper.FindUncoveredEye(clearedBoard, p.x, p.y, group.Content.Opposite()))
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
