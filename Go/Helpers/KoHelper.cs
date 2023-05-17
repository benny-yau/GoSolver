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
        /// Is ko fight, including both pre-ko and ko.
        /// </summary>
        public static Boolean IsKoFight(Board board, Group targetGroup = null)
        {
            if (targetGroup == null) targetGroup = board.MoveGroup;
            Group group = board.GetCurrentGroup(targetGroup);
            if (group.Points.Count != 1 || group.Liberties.Count != 1) return false;
            return IsKoFight(board, group.Liberties.First(), group.Content).Item1;
        }

        public static (Boolean, Group) IsKoFight(Board board, Point eye, Content c)
        {
            if (!EyeHelper.FindEye(board, eye, c)) return (false, null);
            List<Group> eyeGroups = board.GetGroupsFromStoneNeighbours(eye, c.Opposite()).ToList();
            List<Group> groups = eyeGroups.Where(n => n.Points.Count == 1 && n.Liberties.Count == 1).ToList();
            if (groups.Count != 1) return (false, null);
            if (eyeGroups.Any(g => g != groups.First() && g.Liberties.Count == 1)) return (false, null);
            return (true, groups.First());
        }

        /// <summary>
        /// Is non killable group ko fight.
        /// <see cref="UnitTestProject.RedundantKoMoveTest.RedundantKoMoveTest_Scenario_GuanZiPu_A4Q11_101Weiqi" />
        /// <see cref="UnitTestProject.CoveredEyeMoveTest.CoveredEyeMoveTest_Scenario_XuanXuanQiJing_A64" />
        /// </summary>
        public static Boolean IsNonKillableGroupKoFight(Board tryBoard, Group koGroup)
        {
            Content c = koGroup.Content;
            if (!IsKoFight(tryBoard, koGroup)) return false;
            Point eye = tryBoard.GetStoneNeighbours(koGroup.Points.First()).First(n => tryBoard[n] == Content.Empty);
            List<Group> eyeGroups = tryBoard.GetGroupsFromStoneNeighbours(eye, c.Opposite()).ToList();
            if (eyeGroups.Where(n => !n.Equals(koGroup)).All(n => WallHelper.IsNonKillableGroup(tryBoard, n)))
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
            Board board = tryBoard.MakeMoveOnNewBoard(p, c, true);
            if (board == null) return false;
            return IsForwardOrReverseKoFight(board);
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
        /// <see cref="UnitTestProject.RedundantKoMoveTest.RedundantKoMoveTest_Scenario_Corner_A80" />
        /// </summary>
        public static Boolean CheckReverseKoForNeutralPoint(Board tryBoard)
        {
            Point move = tryBoard.Move.Value;
            Content c = tryBoard.MoveGroup.Content;
            if (tryBoard.PointWithinMiddleArea(move)) return false;
            if (tryBoard.MoveGroup.Points.Count != 1 || tryBoard.MoveGroupLiberties != 2) return false;
            foreach (Point diagonal in tryBoard.GetDiagonalNeighbours().Where(n => tryBoard[n] == c))
            {
                if (tryBoard.GetGroupAt(diagonal).Points.Count != 1) continue;
                List<Point> pointsBetweenDiagonals = LinkHelper.PointsBetweenDiagonals(move, diagonal);
                List<Point> liberties = pointsBetweenDiagonals.Where(p => tryBoard[p] == Content.Empty && !tryBoard.PointWithinMiddleArea(p)).ToList();
                if (liberties.Count != 1) continue;
                Point lib = liberties.First();
                List<Point> liberties2 = tryBoard.GetStoneNeighbours(lib).Where(p => tryBoard[p] == Content.Empty).ToList();
                if (liberties2.Count != 1) continue;
                Point lib2 = liberties2.First();
                Point e = tryBoard.GetDiagonalNeighbours(lib).Intersect(tryBoard.GetStoneNeighbours(lib2)).First();

                //make opponent move to capture
                (Boolean suicidal, Board b) = ImmovableHelper.IsSuicidalMove(e, c.Opposite(), tryBoard);
                if (suicidal) continue;
                //make survival move to create ko
                (Boolean suicidal2, Board b2) = ImmovableHelper.IsSuicidalMove(lib2, c, b);
                if (suicidal2) continue;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Get ko atari groups.
        /// </summary>
        public static IEnumerable<Group> GetKoTargetGroups(Board currentBoard, Group group, Group excludeGroup = null)
        {
            return currentBoard.GetNeighbourGroups(group).Where(gr => gr != excludeGroup && KoHelper.IsKoFight(currentBoard, gr));
        }

        /// <summary>
        /// Get ko eye point.
        /// </summary>
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
        /// Check for possibility of double ko, for both survival and kill.
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
            if (targetGroups.Count >= 2)
            {
                List<Board> moveBoards = GameHelper.GetMoveBoards(currentBoard, targetGroups.Select(gr => gr.Liberties.First()), c).ToList();
                moveBoards.RemoveAll(n => IsNonKillableGroupKoFight(n, n.MoveGroup));
                moveBoards.RemoveAll(n => ImmovableHelper.GetDiagonalsOfTigerMouth(currentBoard, capturePoint, c).All(d => n[d] == c, true) && WallHelper.TargetWithAllNonKillableGroups(n) && !Board.ResolveAtari(currentBoard, n));
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
                moveBoards.RemoveAll(n => ImmovableHelper.GetDiagonalsOfTigerMouth(n, n.Move.Value, c.Opposite()).All(d => n[d] == c.Opposite(), true) && IsNonKillableGroupKoFight(n, n.MoveGroup) && !n.IsAtariMove);
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
        public static Boolean NeutralPointDoubleKo(Board tryBoard, Board currentBoard)
        {
            Content c = tryBoard.MoveGroup.Content;
            List<Point> stoneNeighbours = tryBoard.GetStoneNeighbours().Where(n => EyeHelper.FindCoveredEye(tryBoard, n, c)).ToList();
            if (stoneNeighbours.Count != 1) return false;
            Point eyePoint = stoneNeighbours.First();
            List<Group> ngroups = tryBoard.GetGroupsFromStoneNeighbours(eyePoint, c.Opposite()).ToList();
            ngroups = LinkHelper.GetAllDiagonalGroups(tryBoard, ngroups.First()).ToList();
            List<Group> targetGroups = new List<Group>();
            ngroups.ForEach(ngroup => targetGroups.AddRange(KoHelper.GetKoTargetGroups(tryBoard, ngroup)));
            targetGroups = targetGroups.Distinct().ToList();
            if (targetGroups.Count >= 1)
                return true;
            return false;
        }
    }
}
