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
            if (group.Points.Count != 1 || group.Liberties.Count != 1) return false;
            Content c = group.Content;
            Point move = group.Points.First();
            Point eyePoint = board.GetStoneNeighbours(move).FirstOrDefault(n => EyeHelper.FindEye(board, n, c));
            return (Convert.ToBoolean(eyePoint.NotEmpty) && !board.GetGroupsFromStoneNeighbours(eyePoint, c.Opposite()).Any(g => g != group && g.Liberties.Count == 1));
        }

        public static (Boolean, Group) IsKoFight(Board board, Point eye, Content c)
        {
            if (!EyeHelper.FindEye(board, eye, c)) return (false, null);
            List<Group> eyeGroups = board.GetGroupsFromStoneNeighbours(eye, c.Opposite()).ToList();
            List<Group> groups = eyeGroups.Where(group => group.Points.Count == 1 && group.Liberties.Count == 1).ToList();
            if (groups.Count != 1) return (false, null);
            if (eyeGroups.Any(g => g != groups.First() && g.Liberties.Count == 1)) return (false, null);
            return (true, groups.First());
        }

        /// <summary>
        /// Reverse ko fight.
        /// </summary>
        public static Boolean IsReverseKoFight(Board tryBoard, Boolean checkKoEnabled = true)
        {
            Content c = tryBoard.MoveGroup.Content;
            if (checkKoEnabled && !KoHelper.KoContentEnabled(c, tryBoard.GameInfo)) return false;
            List<Point> eyePoints = tryBoard.GetStoneNeighbours().Where(n => EyeHelper.FindEye(tryBoard, n, c)).ToList();
            return eyePoints.Any(p => KoHelper.IsKoFight(tryBoard, p, c).Item1);
        }

        public static Boolean IsReverseKoFight(Board tryBoard, Point p, Content c, Boolean checkKoEnabled = true)
        {
            Board board = tryBoard.MakeMoveOnNewBoard(p, c);
            if (board == null) return false;
            return IsReverseKoFight(board, checkKoEnabled);
        }

        /// <summary>
        /// Is ko fight after capture.
        /// </summary>
        public static Board IsCaptureKoFight(Board board, Group group, Boolean overrideKo = false)
        {
            if (!KoHelper.IsKoFight(board, group)) return null;
            Board capturedBoard = ImmovableHelper.CaptureSuicideGroup(board, group, overrideKo);
            return capturedBoard;
        }

        /// <summary>
        /// Is ko fight at eye point.
        /// </summary>
        public static Board IsCaptureKoFight(Board board, Point eye, Content c, Boolean overrideKo = false)
        {
            (Boolean isKoFight, Group koGroup) = KoHelper.IsKoFight(board, eye, c);
            if (!isKoFight) return null;
            Board capturedBoard = ImmovableHelper.CaptureSuicideGroup(board, koGroup, overrideKo);
            return capturedBoard;
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

            //make opponent move to capture
            (Boolean suicidal, Board b) = ImmovableHelper.IsSuicidalMove(e, c.Opposite(), tryBoard);
            if (suicidal) return false;
            //make survival move to create ko
            (Boolean suicidal2, Board b2) = ImmovableHelper.IsSuicidalMove(lib2, c, b);
            if (suicidal2) return false;

            if (!b2.GetGroupsFromStoneNeighbours(lib, c.Opposite()).All(group => group.Points.Count == 1)) return false;
            if (b2.GetGroupAt(e).Liberties.Count != 1) return false;
            return true;
        }

        /// <summary>
        /// Get ko atari groups.
        /// </summary>
        public static IEnumerable<Group> GetKoTargetGroups(Board currentBoard, Group group, Group excludeGroup = null)
        {
            return currentBoard.GetNeighbourGroups(group).Where(gr => gr != excludeGroup && KoHelper.IsKoFight(currentBoard, gr));
        }

        public static Point? GetKoEyePoint(Board tryBoard)
        {
            Content c = tryBoard.MoveGroup.Content;
            if (tryBoard.singlePointCapture != null) //ko moves
                return tryBoard.singlePointCapture.Value;
            else
            {
                //pre ko moves
                List<Point> eyePoints = tryBoard.GetStoneNeighbours().Where(n => IsKoFight(tryBoard, n, c).Item1).ToList();
                if (eyePoints.Count != 1) return null;
                return eyePoints.First();
            }
        }

        /// <summary>
        /// Check base line leap link for redundant ko move and fill ko eye move.
        /// </summary>
        public static Boolean CheckBaseLineLeapLink(Board currentBoard, Point eyePoint, Content c)
        {
            List<Point> stoneNeighbours = currentBoard.GetStoneNeighbours(eyePoint).Where(n => currentBoard[n] == c && currentBoard.GetGroupAt(n).Liberties.Count > 1).ToList();
            if (stoneNeighbours.Count != 2) return false;
            if (stoneNeighbours.Any(n => currentBoard.PointWithinMiddleArea(n))) return false;
            if (currentBoard.GetGroupsFromPoints(stoneNeighbours).Count != 2) return false;
            Point firstStone = stoneNeighbours[0];
            if (!currentBoard.GetDiagonalNeighbours(firstStone).Any(n => n.Equals(stoneNeighbours[1])))
                return true;
            return false;
        }

        /// <summary>
        /// Check essential atari for ko move.
        /// Check redundant atari <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_Scenario_XuanXuanGo_A26_2" />
        /// Check one liberty non-ko move <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_Scenario_XuanXuanGo_A26_2" />
        /// </summary>
        public static Boolean EssentialAtariForKoMove(GameTryMove tryMove)
        {
            Board tryBoard = tryMove.TryGame.Board;
            Board currentBoard = tryMove.CurrentGame.Board;
            Point move = tryBoard.Move.Value;
            Content c = tryMove.MoveContent;

            if (!tryBoard.IsAtariMove) return false;
            //check one liberty non-ko move
            if (tryBoard.MoveGroupLiberties == 1 && !KoHelper.IsKoFight(tryBoard))
            {
                Board b = ImmovableHelper.CaptureSuicideGroup(tryBoard);
                if (b != null && !ImmovableHelper.CheckConnectAndDie(b))
                    return false;
            }
            if (tryBoard.AtariTargets.Count > 1) return true;
            Group atariTarget = tryBoard.AtariTargets.First();
            //check redundant atari
            if (tryBoard.GetGroupsFromStoneNeighbours(move, c).Count == 1 && WallHelper.IsNonKillableGroup(currentBoard, currentBoard.GetGroupAt(atariTarget.Points.First())))
            {
                (Boolean unEscapable, _, Board b) = ImmovableHelper.UnescapableGroup(tryBoard, atariTarget);
                if (!unEscapable && b != null && b.MoveGroupLiberties > 1)
                {
                    atariTarget.IsNonKillable = true;
                    return false;
                }
            }
            return true;
        }
    }
}
