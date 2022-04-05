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
        /// Enables one way ko check - either survive or kill
        /// If objective is survive with ko or kill, then ko for survive is enabled, else if objective is survive or kill with ko, then ko for kill is enabled
        /// </summary>
        /// <returns></returns>
        public static Boolean KoSurvivalEnabled(SurviveOrKill surviveOrKill, GameInfo gameInfo)
        {
            if (surviveOrKill == SurviveOrKill.Survive)
                return (gameInfo.Survival == SurviveOrKill.SurviveWithKo || gameInfo.Survival == SurviveOrKill.Kill);
            else if (surviveOrKill == SurviveOrKill.Kill)
                return (gameInfo.Survival == SurviveOrKill.KillWithKo || gameInfo.Survival == SurviveOrKill.Survive);
            return false;
        }

        public static Boolean KoContentEnabled(Content c, GameInfo gameInfo)
        {
            Content killContent = GameHelper.GetContentForSurviveOrKill(gameInfo, SurviveOrKill.Kill);
            return KoSurvivalEnabled((c == killContent) ? SurviveOrKill.Kill : SurviveOrKill.Survive, gameInfo);
        }

        /// <summary>
        /// Is Ko fight, including both pre-ko and ko.
        /// </summary>
        public static Boolean IsKoFight(Board board, Group group = null)
        {
            if (group == null) group = board.MoveGroup;
            if (group.Points.Count != 1) return false;
            Content c = group.Content;
            Point move = group.Points.First();
            Point eyePoint = board.GetStoneNeighbours(move.x, move.y).FirstOrDefault(n => EyeHelper.FindEye(board, n, c));
            return (Convert.ToBoolean(eyePoint.NotEmpty) && group.Points.Count == 1 && group.Liberties.Count == 1 && !board.GetGroupsFromStoneNeighbours(eyePoint, c.Opposite()).Any(g => g != group && g.Liberties.Count == 1));
        }

        /// <summary>
        /// Reverse ko fight.
        /// </summary>
        public static Boolean IsReverseKoFight(Board tryBoard, Point p, Content c)
        {
            Board board = tryBoard.MakeMoveOnNewBoard(p, c);
            if (board == null) return false;
            List<Point> eyePoints = board.GetStoneNeighbours().Where(n => EyeHelper.FindEye(board, n, c)).ToList();
            foreach (Point eyePoint in eyePoints)
            {
                List<Group> eyeGroups = board.GetGroupsFromStoneNeighbours(eyePoint, c.Opposite()).ToList();
                if (eyeGroups.Any(n => n.Points.Count == 1 && n.Liberties.Count == 1))
                {
                    if (eyeGroups.Count(g => g.Liberties.Count == 1) != 1) continue;
                    return true;
                }
            }
            return false;
        }


        /// <summary>
        /// Check if ko fight for try move.
        /// <see cref="UnitTestProject.PerformanceBenchmarkTest.PerformanceBenchmarkTest_Scenario_TianLongTu_Q17160" />
        /// </summary>
        public static Boolean CheckIsKoFight(GameTryMove tryMove)
        {
            Board board = tryMove.TryGame.Board;
            if (IsKoFight(board))
            {
                tryMove.IsKoFight = true;
                return true;
            }
            return false;
        }


        /// <summary>
        /// Double ko fight.
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_Corner_A85_2" />
        /// <see cref="UnitTestProject.SpecificNeutralMoveTest.SpecificNeutralMoveTest_Scenario_Corner_A85" />
        /// <see cref="UnitTestProject.CheckForRecursionTest.CheckForRecursionTest_Scenario_XuanXuanGo_A28_101Weiqi" />
        /// Not double ko <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanGo_A82_101Weiqi_2" />
        /// </summary>
        public static (Boolean, Board) DoubleKoFight(Board board, Group targetGroup)
        {
            List<Group> groups = board.GetNeighbourGroups(targetGroup).Where(group => group.Liberties.Count == 1).ToList();
            if (groups.Count < 2) return (false, null);
            foreach (Group atariTarget in groups)
            {
                Board b = ImmovableHelper.CaptureSuicideGroup(board, atariTarget);
                if (b != null && b.AtariTargets.Count > 0 && IsKoFight(b))
                {
                    foreach (Group target in b.AtariTargets)
                    {
                        if (!ImmovableHelper.UnescapableGroup(b, target).Item1) continue;
                        Board b2 = ImmovableHelper.CaptureSuicideGroup(b, target);
                        if (b2 != null && b2.CapturedList.Any(captured => groups.Any(g => !g.Equals(atariTarget) && g.Points.Contains(captured.Points.First()))))
                            return (true, b);
                    }
                }
            }
            return (false, null);
        }

    }
}
