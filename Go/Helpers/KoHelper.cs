using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dh = Go.DirectionHelper;

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
        public static Boolean IsReverseKoFight(Board tryBoard, Boolean checkKoEnabled = true)
        {
            Point p = tryBoard.Move.Value;
            Content c = tryBoard.MoveGroup.Content;
            if (checkKoEnabled && !KoHelper.KoContentEnabled(c, tryBoard.GameInfo)) return false;
            List<Point> eyePoints = tryBoard.GetStoneNeighbours().Where(n => EyeHelper.FindEye(tryBoard, n, c)).ToList();
            foreach (Point eyePoint in eyePoints)
            {
                List<Group> eyeGroups = tryBoard.GetGroupsFromStoneNeighbours(eyePoint, c.Opposite()).ToList();
                if (eyeGroups.Any(n => n.Points.Count == 1 && n.Liberties.Count == 1))
                {
                    if (eyeGroups.Count(g => g.Liberties.Count == 1) != 1) continue;
                    return true;
                }
            }
            return false;
        }

        public static Boolean IsReverseKoFight(Board tryBoard, Point p, Content c, Boolean checkKoEnabled = true)
        {
            Board board = tryBoard.MakeMoveOnNewBoard(p, c);
            if (board == null) return false;
            return IsReverseKoFight(board, checkKoEnabled);
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

        /// <summary>
        /// Ensure neutral point move is not required for ko.
        /// Rare scenario where neutral point required for ko <see cref="UnitTestProject.RedundantKoMoveTest.RedundantKoMoveTest_Scenario_Corner_A80" />
        /// </summary>
        public static Boolean CheckKoForNeutralPoint(Board tryBoard)
        {
            Point move = tryBoard.Move.Value;
            Content c = tryBoard.MoveGroup.Content;
            if (!tryBoard.IsSinglePoint()) return false;
            if (!KoHelper.KoSurvivalEnabled(SurviveOrKill.Survive, tryBoard.GameInfo))
                return false;
            if (tryBoard.GetClosestNeighbour(move, 1).Count == 0)
                return false;
            Direction wallDirection = WallHelper.IsWallNeighbour(tryBoard, move).Item2;
            if (wallDirection == Direction.None) return false;
            Point p1 = dh.GetPointInDirection(tryBoard, move, wallDirection.Opposite());
            Point p2 = dh.GetPointInDirection(tryBoard, p1, wallDirection.Opposite());
            if (p1.Equals(Game.PassMove) || p2.Equals(Game.PassMove))
                return false;

            if (tryBoard[p1] == Content.Empty && tryBoard[p2] == Content.Empty)
            {
                Board b = tryBoard.MakeMoveOnNewBoard(p2, c, true);
                if (b.GetGroupsFromStoneNeighbours(p1, c.Opposite()).All(group => group.Points.Count == 1))
                {
                    List<Point> diagonals = b.GetDiagonalNeighbours(p1.x, p1.y);
                    diagonals = diagonals.Where(diagonal => BothAliveHelper.GetKillerGroupFromCache(tryBoard, diagonal) != null).ToList();
                    if (diagonals.Any(diagonal => b[diagonal] == Content.Empty && ImmovableHelper.FindTigerMouth(b, c, diagonal)))
                        return true;
                }
            }
            return false;
        }
    }
}
