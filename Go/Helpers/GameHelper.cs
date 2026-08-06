using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Go
{
    public class GameHelper
    {
        /// <summary>
        /// Win or lose.
        /// </summary>
        public static Boolean WinOrLose(SurviveOrKill surviveOrKill, ConfirmAliveResult result, GameInfo gi)
        {
            if (surviveOrKill == SurviveOrKill.Survive)
            {
                if (result.HasFlag(ConfirmAliveResult.Alive))
                    return true;
                if (gi.SurvivalWinForBothAlive && result.HasFlag(ConfirmAliveResult.BothAlive))
                    return true;
            }
            else if (surviveOrKill == SurviveOrKill.Kill)
            {
                if (result.HasFlag(ConfirmAliveResult.Dead))
                    return true;
                if (!gi.SurvivalWinForBothAlive && result.HasFlag(ConfirmAliveResult.BothAlive))
                    return true;
            }
            if (result.HasFlag(ConfirmAliveResult.KoAlive))
                return KoHelper.KoSurvivalEnabled(surviveOrKill, gi);
            return false;
        }

        /// <summary>
        /// Kill or survival for next move.
        /// </summary>
        public static SurviveOrKill KillOrSurvivalForNextMove(Board board)
        {
            int lastMoveMod = board.LastMoves.Count % 2;
            Boolean isKill = IsSurviveOrKill(board.GameInfo, SurviveOrKill.Kill);
            if (isKill && lastMoveMod == 0 || !isKill && lastMoveMod == 1)
                return SurviveOrKill.Kill;
            else
                return SurviveOrKill.Survive;
        }

        /// <summary>
        /// Get content for next move.
        /// </summary>
        public static Content GetContentForNextMove(Board board)
        {
            if (board.LastMove != null && !board.IsPassMove)
                return board.MoveGroup.Content.Opposite();
            Content c = board.GameInfo.StartContent;
            return (board.LastMoves.Count % 2 == 0) ? c : c.Opposite();
        }

        /// <summary>
        /// Get content for kill or survival.
        /// </summary>
        public static Content GetContentForSurviveOrKill(GameInfo gi, SurviveOrKill surviveOrKill)
        {
            if (IsSurviveOrKill(gi, surviveOrKill))
                return gi.StartContent;
            else
                return gi.StartContent.Opposite();
        }

        /// <summary>
        /// Is survive or kill.
        /// </summary>
        public static Boolean IsSurviveOrKill(GameInfo gi, SurviveOrKill surviveOrKill)
        {
            if (surviveOrKill == SurviveOrKill.Survive && (gi.Survival == SurviveOrKill.Survive || gi.Survival == SurviveOrKill.SurviveWithKo))
                return true;
            if (surviveOrKill == SurviveOrKill.Kill && (gi.Survival == SurviveOrKill.Kill || gi.Survival == SurviveOrKill.KillWithKo))
                return true;
            return false;
        }

        /// <summary>
        /// Setup move available.
        /// </summary>
        public static Boolean SetupMoveAvailable(Board board, Point p, Content c = Content.Empty)
        {
            GameInfo gi = board.GameInfo;
            if (c == Content.Empty) c = GetContentForNextMove(board);
            if (GetContentForSurviveOrKill(gi, SurviveOrKill.Survive) == c)
                return gi.IsMovablePoint[p.x, p.y];
            else
                return gi.IsKillMovablePoint[p.x, p.y];
        }

        /// <summary>
        /// Get movable points.
        /// </summary>
        public static List<Point> GetMovablePoints(Board board)
        {
            SurviveOrKill survivalOrKill = GameHelper.KillOrSurvivalForNextMove(board);
            List<Point> movablePoints = (survivalOrKill == SurviveOrKill.Kill || survivalOrKill == SurviveOrKill.KillWithKo) ? board.GameInfo.killMovablePoints : board.GameInfo.movablePoints;
            return movablePoints;
        }

        /// <summary>
        /// Get computer or player for next move.
        /// </summary>
        public static PlayerOrComputer GetComputerOrPlayerForNextMove(Board board)
        {
            GameInfo gi = board.GameInfo;
            return (board.LastMoves.Count % 2 == 0) ? gi.UserFirst : gi.UserFirst.Opposite();
        }

        /// <summary>
        /// Check for recursion.
        /// https://senseis.xmp.net/?LongCycleRule
        /// <see cref="UnitTestProject.CheckForRecursionTest.CheckForRecursionTest_Scenario_TianLongTu_Q16446" />
        /// </summary>
        public static Boolean CheckForRecursion(GameTryMove tryMove)
        {
            Game g = tryMove.TryGame;
            foreach (int j in CheckForRecursion(g.Board))
            {
                //get snapshot of board from last moves and compare if board is the same
                int count = g.Board.LastMoves.Count - j;
                Board compareBoard = GameHelper.GetSnapshotBoard(g, count);
                if (g.Board.Equals(compareBoard))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Check for recursion.
        /// </summary>
        public static IEnumerable<int> CheckForRecursion(Board tryBoard)
        {
            Point move = tryBoard.Move.Value;
            List<Point> lastMoves = tryBoard.LastMoves;
            //check 4 spaces to 12 spaces apart
            for (int j = 4; j <= 12; j++)
            {
                int rc = lastMoves.Count - 1;
                //find recurrence of last three moves
                Boolean recur = (rc >= j + 2 && move.Equals(lastMoves[rc - j]) && lastMoves[rc - 1].Equals(lastMoves[rc - (j + 1)]) && lastMoves[rc - 2].Equals(lastMoves[rc - (j + 2)]));
                if (recur)
                    yield return j;
            }
        }

        /// <summary>
        /// Get snapshot board. Requires that the root of the game starts from initial setup.
        /// </summary>
        public static Board GetSnapshotBoard(Game g, int count)
        {
            if (g.Board.LastMoves.Count < count)
                return g.Board;
            Board rootBoard = new Board(g.Root.Board);
            for (int i = rootBoard.LastMoves.Count; i < count; i++)
            {
                Point p = g.Board.LastMoves[i];
                Content c = GetContentForNextMove(rootBoard);
                rootBoard.InternalMakeMove(p, c, true);
            }
            return rootBoard;
        }

        /// <summary>
        /// Get move boards.
        /// </summary>
        public static IEnumerable<Board> GetMoveBoards(Board currentBoard, IEnumerable<Point> moves, Content c, Boolean checkSuicidal = false)
        {
            foreach (Point p in moves)
            {
                if (!GameHelper.SetupMoveAvailable(currentBoard, p, c)) continue;
                Board b = currentBoard.MakeMoveOnNewBoard(p, c, true);
                if (b == null) continue;
                if (checkSuicidal && ImmovableHelper.IsSuicidalWithoutKo(b)) continue;
                yield return b;
            }
        }

        /// <summary>
        /// Get try moves for game.
        /// </summary>
        public static List<GameTryMove> GetTryMovesForGame(Game g)
        {
            ConfirmAliveResult result;
            GameTryMove koBlockedMove;
            List<GameTryMove> tryMoves;
            SurviveOrKill surviveOrKill = GameHelper.KillOrSurvivalForNextMove(g.Board);
            if (surviveOrKill == SurviveOrKill.Kill)
                (result, tryMoves, koBlockedMove) = g.GetKillMoves();
            else
                (result, tryMoves, koBlockedMove) = g.GetSurvivalMoves();
            if (koBlockedMove != null) tryMoves.Add(koBlockedMove);
            return tryMoves;
        }
    }
}
