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
            Point eyePoint = board.GetStoneNeighbours(move).FirstOrDefault(n => EyeHelper.FindEye(board, n, c));
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
            List<Group> koGroups = board.GetNeighbourGroups(targetGroup).Where(group => KoHelper.IsKoFight(board, group)).ToList();
            if (koGroups.Count < 2) return (false, null);
            foreach (Group koGroup in koGroups)
            {
                Board b = ImmovableHelper.CaptureSuicideGroup(board, koGroup);
                if (b == null) continue;
                foreach (Group target in b.AtariTargets)
                {
                    if (!ImmovableHelper.UnescapableGroup(b, target).Item1) continue;
                    Board b2 = ImmovableHelper.CaptureSuicideGroup(b, target);
                    if (b2 == null) continue;
                    if (b2.CapturedList.Any(captured => koGroups.Any(g => !g.Equals(koGroup) && g.Points.Contains(captured.Points.First()))))
                        return (true, b);
                }
            }
            return (false, null);
        }

        /// <summary>
        /// Reverse ko for neutral point move.
        /// Rare scenario where neutral point required for ko <see cref="UnitTestProject.RedundantKoMoveTest.RedundantKoMoveTest_Scenario_Corner_A80" />
        /// </summary>
        public static Boolean CheckReverseKoForNeutralPoint(Board tryBoard)
        {
            Point move = tryBoard.Move.Value;
            Content c = tryBoard.MoveGroup.Content;
            if (!KoHelper.KoContentEnabled(c, tryBoard.GameInfo)) return false;
            if (tryBoard.PointWithinMiddleArea(move)) return false;
            if (tryBoard.MoveGroup.Points.Count != 1 || tryBoard.MoveGroupLiberties != 2) return false;
            List<Point> diagonals = tryBoard.GetDiagonalNeighbours().Where(n => tryBoard[n] == c).ToList();
            if (diagonals.Count != 1) return false;
            Point diagonal = diagonals.First();
            if (tryBoard.GetGroupAt(diagonal).Points.Count != 1) return false;
            List<Point> pointsBetweenDiagonals = LinkHelper.PointsBetweenDiagonals(move, diagonal);
            if (!pointsBetweenDiagonals.Any(p => tryBoard[p] == c.Opposite())) return false;
            List<Point> liberties = pointsBetweenDiagonals.Where(p => tryBoard[p] == Content.Empty && !tryBoard.PointWithinMiddleArea(p)).ToList();
            if (liberties.Count != 1) return false;
            Point lib = liberties.First();
            List<Point> liberties2 = tryBoard.GetStoneNeighbours(lib).Where(p => tryBoard[p] == Content.Empty).ToList();
            if (liberties2.Count != 1) return false;
            Point lib2 = liberties2.First();
            Point e = tryBoard.GetDiagonalNeighbours(lib).Intersect(tryBoard.GetStoneNeighbours(lib2)).First();
            if (BothAliveHelper.GetKillerGroupFromCache(tryBoard, e, c) == null) return false;

            //make opponent move to capture
            Board b = tryBoard.MakeMoveOnNewBoard(e, c.Opposite());
            if (b == null || b.MoveGroupLiberties == 1) return false;
            //make survival move to create ko
            Board b2 = b.MakeMoveOnNewBoard(lib2, c);
            if (b2 == null || b2.MoveGroupLiberties == 1) return false;

            if (!b2.GetGroupsFromStoneNeighbours(lib, c.Opposite()).All(group => group.Points.Count == 1)) return false;
            if (b2.GetGroupAt(e).Liberties.Count != 1) return false;
            return true;
        }


        /// <summary>
        /// Check killer ko within killer group.
        /// <see cref="UnitTestProject.RedundantKoMoveTest.RedundantKoMoveTest_Scenario_Corner_B39" /> 
        /// <see cref="UnitTestProject.RedundantKoMoveTest.RedundantKoMoveTest_Scenario_Corner_A85" /> 
        /// </summary>
        public static Boolean CheckKillerKoWithinKillerGroup(GameTryMove tryMove)
        {
            Board currentBoard = tryMove.CurrentGame.Board;
            Board tryBoard = tryMove.TryGame.Board;
            if (tryBoard.singlePointCapture == null) return false;
            Point move = tryBoard.Move.Value;
            Content c = tryBoard.MoveGroup.Content; 
            Group killerGroup = BothAliveHelper.GetKillerGroupFromCache(tryBoard, move, c.Opposite());
            if (killerGroup == null) return false;
            List<Group> neighbourGroups = tryBoard.GetNeighbourGroups(killerGroup);
            //ensure all neighbour groups within killer group
            if (neighbourGroups.All(n => BothAliveHelper.GetKillerGroupFromCache(tryBoard, n.Points.First(), c) == null)) return false;
            List<Group> ngroups = tryBoard.GetGroupsFromStoneNeighbours(tryBoard.singlePointCapture.Value, c.Opposite()).Where(ngroup => ngroup != tryBoard.MoveGroup).ToList();
            if (ngroups.Count == 1 && currentBoard.GetGroupLiberties(ngroups.First().Points.First()) == 1)
            {
                //ensure real eye within neighbour groups
                HashSet<Point> killerLiberties = tryBoard.GetLibertiesOfGroups(neighbourGroups);
                if (!killerLiberties.Any(liberty => EyeHelper.FindRealEyeWithinEmptySpace(tryBoard, liberty, c.Opposite())))
                    return false;
                //double ko fight
                if (killerLiberties.Any(liberty => EyeHelper.FindCoveredEye(tryBoard, liberty, c.Opposite())))
                    return true;

                //all strong neighbour groups
                if (neighbourGroups.All(n => WallHelper.IsStrongNeighbourGroup(tryBoard, n)))
                    return true;
            }
            return false;
        }

        public static Point? GetKoEyePoint(Board tryBoard)
        {
            Content c = tryBoard.MoveGroup.Content;
            if (tryBoard.singlePointCapture != null) //ko moves
                return tryBoard.singlePointCapture.Value;
            else
            {
                //pre ko moves
                List<Point> eyePoints = tryBoard.GetStoneNeighbours().Where(n => EyeHelper.FindEye(tryBoard, n, c)).ToList();
                if (eyePoints.Count != 1) return null;
                return eyePoints.First();
            }
        }

        /// <summary>
        /// Check break link for ko move.
        /// </summary>
        public static Boolean CheckBreakLinkKoMove(Board currentBoard, Point eyePoint, Content c)
        {
            List<Point> stoneNeighbours = currentBoard.GetStoneNeighbours(eyePoint).Where(n => currentBoard[n] == c).ToList();
            stoneNeighbours = stoneNeighbours.Where(n => currentBoard.GetGroupAt(n).Liberties.Count > 1).ToList();
            if (stoneNeighbours.Count == 2)
            {
                Point firstStone = stoneNeighbours[0];
                if (!currentBoard.GetDiagonalNeighbours(firstStone).Any(n => n.Equals(stoneNeighbours[1])))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Check essential atari for ko move.
        /// </summary>
        public static Boolean EssentialAtariForKoMove(GameTryMove tryMove)
        {
            Board tryBoard = tryMove.TryGame.Board;
            Board currentBoard = tryMove.CurrentGame.Board;
            Point move = tryBoard.Move.Value;
            Content c = tryMove.MoveContent;

            if (!tryBoard.IsAtariMove) return false;
            if (tryBoard.AtariTargets.Count > 1) return true;
            Group atariTarget = tryBoard.AtariTargets.First();
            Boolean redundantAtari = tryBoard.GetGroupsFromStoneNeighbours(move, c).Count == 1 && WallHelper.IsNonKillableGroup(currentBoard, currentBoard.GetGroupAt(atariTarget.Points.First())) && !ImmovableHelper.UnescapableGroup(tryBoard, atariTarget).Item1;
            if (!redundantAtari)
                return true;
            atariTarget.IsNonKillable = true;
            return false;
        }
    }
}
