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
        public static Boolean IsKoFight(Board board, Group targetGroup = null)
        {
            if (targetGroup == null) targetGroup = board.MoveGroup;
            Group group = board.GetCurrentGroup(targetGroup);
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
        /// Is Ko fight at non killable group.
        /// <see cref="UnitTestProject.RedundantKoMoveTest.RedundantKoMoveTest_Scenario_GuanZiPu_A4Q11_101Weiqi" />
        /// <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_Scenario_XuanXuanQiJing_A64" />
        /// </summary>
        public static Boolean IsKoFightAtNonKillableGroup(Board tryBoard, Group koGroup)
        {
            Content c = tryBoard.MoveGroup.Content;
            if (!IsKoFight(tryBoard, koGroup)) return false;
            Point eye = tryBoard.GetStoneNeighbours(koGroup.Points.First()).First(n => tryBoard[n] == Content.Empty);
            List<Group> eyeGroups = tryBoard.GetGroupsFromStoneNeighbours(eye, c.Opposite()).ToList();
            if (eyeGroups.Where(n => !n.Equals(koGroup)).All(n => WallHelper.IsNonKillableGroup(tryBoard, n)))
                return true;
            return false;
        }

        /// <summary>
        /// Reverse ko fight.
        /// </summary>
        public static Boolean IsReverseKoFight(Board tryBoard)
        {
            Content c = tryBoard.MoveGroup.Content;
            List<Point> eyePoints = tryBoard.GetStoneNeighbours().Where(n => EyeHelper.FindEye(tryBoard, n, c)).ToList();
            return eyePoints.Any(p => KoHelper.IsKoFight(tryBoard, p, c).Item1);
        }

        public static Boolean IsReverseKoFight(Board tryBoard, Point p, Content c)
        {
            Board board = tryBoard.MakeMoveOnNewBoard(p, c);
            if (board == null) return false;
            return IsReverseKoFight(board);
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
        /// Reverse ko for neutral point move.
        /// Rare scenario where neutral point required for ko <see cref="UnitTestProject.RedundantKoMoveTest.RedundantKoMoveTest_Scenario_Corner_A80" />
        /// </summary>
        public static Boolean CheckReverseKoForNeutralPoint(Board tryBoard)
        {
            Point move = tryBoard.Move.Value;
            Content c = tryBoard.MoveGroup.Content;
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

            //check one liberty non-ko move
            if (tryBoard.MoveGroupLiberties == 1 && !KoHelper.IsKoFight(tryBoard))
            {
                Board b = ImmovableHelper.CaptureSuicideGroup(tryBoard);
                if (b != null && !ImmovableHelper.CheckConnectAndDie(b))
                    return false;
            }
            //check redundant atari
            if (tryBoard.GetGroupsFromStoneNeighbours(move, c).Count == 1 && WallHelper.IsNonKillableGroup(currentBoard, atariTarget))
            {
                (Boolean unEscapable, _, Board b) = ImmovableHelper.UnescapableGroup(tryBoard, atariTarget, false);
                if (!unEscapable && b != null && b.MoveGroupLiberties > 1)
                {
                    atariTarget.IsNonKillable = true;
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Check for possibility of double ko, for both survival and kill. Check for end ko moves as well.
        /// Survival double ko <see cref="UnitTestProject.CheckForRecursionTest.CheckForRecursionTest_Scenario_TianLongTu_Q16446" />
        /// <see cref="UnitTestProject.CheckForRecursionTest.CheckForRecursionTest_Scenario_TianLongTu_Q16975" />
        /// <see cref="UnitTestProject.FillKoEyeMoveTest.FillKoEyeMoveTest_Scenario_WindAndTime_Q30275_3" /> 
        /// Kill double ko <see cref="UnitTestProject.RedundantKoMoveTest.RedundantKoMoveTest_Scenario_Corner_A23" />
        /// <see cref="UnitTestProject.RedundantKoMoveTest.RedundantKoMoveTest_Scenario_WuQingYuan_Q30982_2" />
        /// <see cref="UnitTestProject.FillKoEyeMoveTest.FillKoEyeMoveTest_Scenario_WindAndTime_Q30275_2" /> 
        /// </summary>
        public static Boolean PossibilityOfDoubleKo(GameTryMove tryMove)
        {
            Board currentBoard = tryMove.CurrentGame.Board;
            Board tryBoard = tryMove.TryGame.Board;
            return PossibilityOfDoubleKo(tryBoard, currentBoard);
        }

        public static Boolean PossibilityOfDoubleKo(Board tryBoard, Board currentBoard)
        {
            Point move = tryBoard.Move.Value;
            Content c = tryBoard.MoveGroup.Content;
            if (tryBoard.singlePointCapture == null) return false;
            Point capturePoint = tryBoard.singlePointCapture.Value;
            //survival double ko
            List<Group> ngroups = currentBoard.GetGroupsFromStoneNeighbours(capturePoint, c.Opposite()).ToList();
            ngroups = LinkHelper.GetAllDiagonalGroups(currentBoard, ngroups.First()).ToList();
            List<Group> targetGroups = new List<Group>();
            ngroups.ForEach(ngroup => targetGroups.AddRange(KoHelper.GetKoTargetGroups(currentBoard, ngroup)));
            targetGroups = targetGroups.Distinct().ToList();
            if (targetGroups.Count >= 2 && DoubleKoEnabled(tryBoard, currentBoard))
                return true;

            //kill double ko
            List<Group> connectedGroups = LinkHelper.GetAllDiagonalGroups(currentBoard, currentBoard.GetGroupAt(capturePoint));
            List<Group> koGroups = new List<Group>();
            foreach (Point liberty in currentBoard.GetLibertiesOfGroups(connectedGroups))
            {
                (Boolean isKoFight, Group group) = KoHelper.IsKoFight(currentBoard, liberty, c.Opposite());
                if (isKoFight)
                {
                    koGroups.Add(group);
                    if (koGroups.Count >= 2 && DoubleKoEnabled(tryBoard, currentBoard))
                        return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Double ko enabled.
        /// </summary>
        private static Boolean DoubleKoEnabled(Board tryBoard, Board currentBoard, Point? eyePoint = null)
        {
            Content c = tryBoard.MoveGroup.Content;
            eyePoint = eyePoint ?? tryBoard.singlePointCapture.Value;
            List<Group> groups = new List<Group>();
            //survival double ko
            if (WallHelper.TargetWithAllNonKillableGroups(tryBoard) && !Board.ResolveAtari(currentBoard, tryBoard))
                groups = currentBoard.GetGroupsFromStoneNeighbours(eyePoint.Value, c.Opposite()).ToList();
            //kill double ko
            else if (KoHelper.IsKoFightAtNonKillableGroup(tryBoard, tryBoard.MoveGroup))
                groups = tryBoard.GetNeighbourGroups();

            //ensure only one survival group
            if (groups.Count == 1)
            {
                Group group = groups.First();
                //ensure more than two iiberties
                if (group.Liberties.Count > 2 || currentBoard.GetNeighbourGroups(group).Where(n => n.Liberties.Count > 1).All(n => WallHelper.IsNonKillableGroup(currentBoard, n)))
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Double ko for neutral point.
        /// <see cref="UnitTestProject.NeutralPointMoveTest.NeutralPointMoveTest_Scenario_XuanXuanGo_A28_101Weiqi" />
        /// <see cref="UnitTestProject.NeutralPointMoveTest.NeutralPointMoveTest_Scenario_XuanXuanGo_A28_101Weiqi_4" />
        /// <see cref="UnitTestProject.RedundantKoMoveTest.RedundantKoMoveTest_Scenario_Corner_B41_2" />
        /// </summary>
        public static Boolean NeutralPointDoubleKo(Board tryBoard, Board currentBoard)
        {
            Content c = tryBoard.MoveGroup.Content;
            Boolean koEnabled = KoHelper.KoContentEnabled(c, tryBoard.GameInfo);
            if (koEnabled) return false;
            List<Point> stoneNeighbours = tryBoard.GetStoneNeighbours().Where(n => EyeHelper.FindCoveredEye(tryBoard, n, c)).ToList();
            if (stoneNeighbours.Count != 1) return false;
            Point eyePoint = stoneNeighbours.First();
            //make block move
            List<Point> emptyNeighbours = tryBoard.GetStoneNeighbours().Where(n => tryBoard[n] == Content.Empty && !n.Equals(eyePoint)).ToList();
            if (emptyNeighbours.Count != 1) return false;

            List<Group> ngroups = tryBoard.GetGroupsFromStoneNeighbours(eyePoint, c.Opposite()).ToList();
            ngroups = LinkHelper.GetAllDiagonalGroups(tryBoard, ngroups.First()).ToList();
            List<Group> targetGroups = new List<Group>();
            ngroups.ForEach(ngroup => targetGroups.AddRange(KoHelper.GetKoTargetGroups(tryBoard, ngroup)));
            targetGroups = targetGroups.Distinct().ToList();
            if (targetGroups.Count >= 1)
            {
                Board b = new Board(tryBoard);
                b[emptyNeighbours.First()] = c.Opposite();
                if (DoubleKoEnabled(b, currentBoard, eyePoint))
                    return true;
            }
            return false;
        }
    }
}
