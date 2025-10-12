using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Go
{
    public class KoHelper
    {
        /// <summary>
        /// Ko survival enabled.
        /// </summary>
        public static Boolean KoSurvivalEnabled(SurviveOrKill surviveOrKill, GameInfo gameInfo)
        {
            if (surviveOrKill == SurviveOrKill.Survive)
                return (gameInfo.Survival == SurviveOrKill.SurviveWithKo || gameInfo.Survival == SurviveOrKill.Kill);
            else if (surviveOrKill == SurviveOrKill.Kill)
                return (gameInfo.Survival == SurviveOrKill.KillWithKo || gameInfo.Survival == SurviveOrKill.Survive);
            return false;
        }

        /// <summary>
        /// Ko content enabled.
        /// </summary>
        public static Boolean KoContentEnabled(Content c, GameInfo gameInfo)
        {
            Content killContent = GameHelper.GetContentForSurviveOrKill(gameInfo, SurviveOrKill.Kill);
            return KoSurvivalEnabled((c == killContent) ? SurviveOrKill.Kill : SurviveOrKill.Survive, gameInfo);
        }

        /// <summary>
        /// Is ko fight.
        /// </summary>
        public static Boolean IsKoFight(Board board, Group group = null)
        {
            if (group == null) group = board.MoveGroup;
            else group = board.GetCurrentGroup(group);
            if (group.Points.Count != 1 || group.Liberties.Count != 1) return false;
            return IsKoFight(board, group.Liberties.First(), group.Content).Item1;
        }

        public static (Boolean, Group) IsKoFight(Board board, Point eye, Content c)
        {
            if (!EyeHelper.FindEye(board, eye, c)) return (false, null);
            List<Group> groups = board.OneLibertyGroup(eye, c.Opposite());
            if (groups.Count != 1) return (false, null);
            if (groups.First().Points.Count != 1) return (false, null);
            return (true, groups.First());
        }

        /// <summary>
        /// Is non killable group ko fight.
        /// <see cref="UnitTestProject.RedundantKoMoveTest.RedundantKoMoveTest_Scenario_GuanZiPu_A4Q11_101Weiqi" />
        /// <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_Scenario_XuanXuanQiJing_A64" />
        /// </summary>
        public static Boolean IsNonKillableGroupKoFight(Board tryBoard, Group group = null)
        {
            if (group == null) group = tryBoard.MoveGroup;
            else group = tryBoard.GetCurrentGroup(group);
            Content c = group.Content;
            if (!IsKoFight(tryBoard, group)) return false;
            Point eye = tryBoard.GetStoneNeighbours(group.Points.First()).First(n => tryBoard[n] == Content.Empty);
            List<Group> eyeGroups = tryBoard.GetGroupsFromStoneNeighbours(eye, c.Opposite()).ToList();
            if (eyeGroups.Where(n => !n.Equals(group)).All(n => WallHelper.IsNonKillableGroup(tryBoard, n)))
                return true;
            return false;
        }

        /// <summary>
        /// Is forward or reverse ko fight.
        /// </summary>
        public static Boolean IsForwardOrReverseKoFight(Board tryBoard)
        {
            Content c = tryBoard.MoveGroup.Content;
            List<Point> eyePoints = tryBoard.GetStoneNeighbours().Where(n => EyeHelper.FindEye(tryBoard, n, c)).ToList();
            return eyePoints.Any(p => KoHelper.IsKoFight(tryBoard, p, c).Item1);
        }

        /// <summary>
        /// Make ko fight.
        /// </summary>
        public static Boolean MakeKoFight(Board tryBoard, Point p, Content c)
        {
            if (tryBoard[p] != Content.Empty) return false;
            Board board = tryBoard.MakeMoveOnNewBoard(p, c, true);
            if (board == null) return false;
            return IsForwardOrReverseKoFight(board);
        }

        /// <summary>
        /// Reverse ko for neutral point move.
        /// <see cref="UnitTestProject.RedundantKoMoveTest.RedundantKoMoveTest_Scenario_Corner_A80" />
        /// <see cref="UnitTestProject.DailyGoProblems.DailyGoProblems_20230813" />
        /// <see cref="UnitTestProject.MustHaveNeutralMoveTest.MustHaveNeutralMoveTest_20221229_7" />
        /// </summary>
        public static Boolean CheckReverseKoForNeutralPoint(Board tryBoard)
        {
            Point move = tryBoard.Move.Value;
            Content c = tryBoard.MoveGroup.Content;
            if (tryBoard.PointWithinMiddleArea(move)) return false;
            if (tryBoard.MoveGroup.Points.Count != 1 || tryBoard.MoveGroupLiberties != 2) return false;
            foreach (Point d in tryBoard.GetDiagonalNeighbours().Where(n => tryBoard[n] == c))
            {
                Group dgroup = tryBoard.GetGroupAt(d);
                if (dgroup.Points.Count != 1) continue;
                Point lib = tryBoard.GetStoneNeighbours(d).FirstOrDefault(p => tryBoard[p] == Content.Empty && !tryBoard.PointWithinMiddleArea(p));
                if (!Convert.ToBoolean(lib.NotEmpty)) continue;
                Point lib2 = tryBoard.GetStoneNeighbours(lib).FirstOrDefault(p => tryBoard[p] == Content.Empty);
                if (!Convert.ToBoolean(lib2.NotEmpty)) continue;
                Point e = tryBoard.GetDiagonalNeighbours(lib).Intersect(tryBoard.GetStoneNeighbours(lib2)).First();
                if (tryBoard[e] != Content.Empty || WallHelper.NoEyeForSurvival(tryBoard, e)) continue;
                Board b = tryBoard.MakeMoveOnNewBoard(lib2, c);
                if (b != null && ImmovableHelper.CheckConnectAndDie(b, dgroup))
                    continue;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Get ko target groups.
        /// </summary>
        public static IEnumerable<Group> GetKoTargetGroups(Board board, Group group, Group excludeGroup = null)
        {
            return board.GetNeighbourGroups(group).Where(gr => gr != excludeGroup && KoHelper.IsKoFight(board, gr));
        }

        /// <summary>
        /// Get ko eye point.
        /// </summary>
        public static Point? GetKoEyePoint(Board tryBoard)
        {
            Content c = tryBoard.MoveGroup.Content;
            if (tryBoard.KoCapture != null) //ko moves
                return tryBoard.KoCapture.Value;
            //pre ko moves
            List<Point> eyePoints = tryBoard.GetStoneNeighbours().Where(n => IsKoFight(tryBoard, n, c).Item1).ToList();
            if (eyePoints.Count == 1) return eyePoints.First();
            return null;
        }


        /// <summary>
        /// Check for possibility of double ko, for both survival and kill.
        /// Survival double ko <see cref="UnitTestProject.CheckForRecursionTest.CheckForRecursionTest_Scenario_TianLongTu_Q16446" />
        /// <see cref="UnitTestProject.CheckForRecursionTest.CheckForRecursionTest_Scenario_TianLongTu_Q16975" />
        /// <see cref="UnitTestProject.FillKoEyeMoveTest.FillKoEyeMoveTest_Scenario_WindAndTime_Q30275_3" /> 
        /// Kill double ko <see cref="UnitTestProject.RedundantKoMoveTest.RedundantKoMoveTest_Scenario_Corner_A23" />
        /// <see cref="UnitTestProject.RedundantKoMoveTest.RedundantKoMoveTest_Scenario_WuQingYuan_Q30982_2" />
        /// <see cref="UnitTestProject.FillKoEyeMoveTest.FillKoEyeMoveTest_Scenario_WindAndTime_Q30275_2" /> 
        /// </summary>
        public static Boolean PossibilityOfDoubleKo(Board tryBoard, Board currentBoard)
        {
            Point move = tryBoard.Move.Value;
            Content c = tryBoard.MoveGroup.Content;
            if (tryBoard.KoCapture == null) return false;
            Point capturePoint = tryBoard.KoCapture.Value;
            //survival double ko
            List<Group> ngroups = currentBoard.GetGroupsFromStoneNeighbours(capturePoint, c.Opposite()).ToList();
            ngroups = LinkHelper.GetAllDiagonalGroups(currentBoard, ngroups.First()).ToList();
            List<Group> targetGroups = new List<Group>();
            ngroups.ForEach(n => targetGroups.AddRange(KoHelper.GetKoTargetGroups(currentBoard, n)));
            targetGroups = targetGroups.Distinct().ToList();
            if (targetGroups.Count >= 2)
            {
                List<Board> moveBoards = GameHelper.GetMoveBoards(currentBoard, targetGroups.Select(gr => gr.Liberties.First()), c).ToList();
                moveBoards.RemoveAll(n => IsNonKillableGroupKoFight(n));
                if (moveBoards.Count(k => !RedundantMoveHelper.CheckRedundantKoMove(k, currentBoard)) >= 2)
                    return true;
            }
            //kill double ko
            List<Group> connectedGroups = LinkHelper.GetAllDiagonalGroups(currentBoard, currentBoard.GetGroupAt(capturePoint));
            List<Group> koGroups = new List<Group>();
            foreach (Point liberty in currentBoard.GetLibertiesOfGroups(connectedGroups))
            {
                (Boolean isKoFight, Group group) = KoHelper.IsKoFight(currentBoard, liberty, c.Opposite());
                if (!isKoFight) continue;
                koGroups.Add(group);
            }
            if (koGroups.Count >= 2)
            {
                List<Board> moveBoards = GameHelper.GetMoveBoards(currentBoard, koGroups.Select(gr => gr.Liberties.First()), c).ToList();
                if (moveBoards.Count(k => !RedundantMoveHelper.CheckRedundantKoMove(k, currentBoard)) >= 2)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Double ko for neutral point.
        /// <see cref="UnitTestProject.NeutralPointMoveTest.NeutralPointMoveTest_Scenario_XuanXuanGo_A28_101Weiqi" />
        /// <see cref="UnitTestProject.NeutralPointMoveTest.NeutralPointMoveTest_Scenario_XuanXuanGo_A28_101Weiqi_4" />
        /// <see cref="UnitTestProject.RedundantKoMoveTest.RedundantKoMoveTest_Scenario_Corner_B41_2" />
        /// </summary>
        public static Boolean NeutralPointDoubleKo(Board board)
        {
            Content c = board.MoveGroup.Content;
            Point p = board.GetStoneNeighbours().FirstOrDefault(n => EyeHelper.FindCoveredEye(board, n, c));
            if (!Convert.ToBoolean(p.NotEmpty)) return false;
            return IsCoveredEyeDoubleKo(board);
        }

        /// <summary>
        /// Double ko for covered eye.
        /// <see cref="UnitTestProject.NeutralPointMoveTest.NeutralPointMoveTest_Scenario_XuanXuanGo_A28_101Weiqi_7" />
        /// </summary>
        public static Boolean IsCoveredEyeDoubleKo(Board board)
        {
            Content c = board.MoveGroup.Content;
            foreach (Group ngroup in LinkHelper.GetAllDiagonalGroups(board, board.MoveGroup))
            {
                foreach (Group koGroup in KoHelper.GetKoTargetGroups(board, ngroup))
                {
                    if (!ImmovableHelper.UnescapableGroup(board, koGroup).Item1) continue;
                    Point eye = koGroup.Liberties.First();
                    HashSet<Group> ngroups = board.GetGroupsFromStoneNeighbours(eye, c);
                    if (ngroups.Any(n => n != koGroup && ImmovableHelper.CheckConnectAndDie(board, n, false)))
                        continue;
                    return true;
                }
            }
            return false;
        }
    }
}
