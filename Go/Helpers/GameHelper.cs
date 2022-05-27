using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Go
{
    public class GameHelper
    {
        /// <summary>
        /// Get all try moves for next move.
        /// </summary>
        public static List<GameTryMove> GetTryMovesForGame(Game game)
        {
            ConfirmAliveResult result;
            GameTryMove koBlockedMove;
            List<GameTryMove> tryMoves;
            SurviveOrKill surviveOrKill = GameHelper.KillOrSurvivalForNextMove(game.Board);
            if (surviveOrKill == SurviveOrKill.Kill)
                (result, tryMoves, koBlockedMove) = game.GetKillMoves(game);
            else
                (result, tryMoves, koBlockedMove) = game.GetSurvivalMoves(game);

            return tryMoves;
        }



        /// <summary>
        /// Determine if win or lose based on result in mcts.
        /// </summary>
        public static Boolean WinOrLose(SurviveOrKill surviveOrKill, ConfirmAliveResult playoutResult, Game currentGame)
        {
            if (surviveOrKill == SurviveOrKill.Survive)
            {
                if (playoutResult.HasFlag(ConfirmAliveResult.Alive) || playoutResult.HasFlag(ConfirmAliveResult.BothAlive))
                    return true;
                else if (playoutResult.HasFlag(ConfirmAliveResult.KoAlive))
                    return KoHelper.KoSurvivalEnabled(surviveOrKill, currentGame.GameInfo);
            }
            else if (surviveOrKill == SurviveOrKill.Kill)
            {
                if (playoutResult.HasFlag(ConfirmAliveResult.Dead))
                    return true;
                else if (playoutResult.HasFlag(ConfirmAliveResult.KoAlive))
                    return KoHelper.KoSurvivalEnabled(surviveOrKill, currentGame.GameInfo);
            }
            return false;
        }

        /// <summary>
        /// Determine if next move is kill or survival, based on count of last moves and initial objective.
        /// </summary>
        public static SurviveOrKill KillOrSurvivalForNextMove(Board board)
        {
            int lastMoveMod = board.LastMoves.Count % 2;
            GameInfo gameInfo = board.GameInfo;
            SurviveOrKill surviveOrKill = (gameInfo.Survival == SurviveOrKill.Kill || gameInfo.Survival == SurviveOrKill.KillWithKo) ? SurviveOrKill.Kill : SurviveOrKill.Survive;

            //if current game is survival, next move returned is kill
            if (surviveOrKill == SurviveOrKill.Kill && lastMoveMod == 0 || surviveOrKill == SurviveOrKill.Survive && lastMoveMod == 1)
                return SurviveOrKill.Kill;
            else
                return SurviveOrKill.Survive;
        }

        /// <summary>
        /// Determine content for next move, based on count of last moves and start content.
        /// </summary>
        public static Content GetContentForNextMove(Board board)
        {
            Content startContent = board.GameInfo.StartContent;
            int lastMoveMod = board.LastMoves.Count % 2;
            return (lastMoveMod == 0) ? startContent : startContent.Opposite();
        }

        /// <summary>
        /// Determine if content for kill or survival, based on start content.
        /// </summary>
        public static Content GetContentForSurviveOrKill(GameInfo gameInfo, SurviveOrKill surviveOrKill)
        {
            if (surviveOrKill == SurviveOrKill.Survive && (gameInfo.Survival == SurviveOrKill.Survive || gameInfo.Survival == SurviveOrKill.SurviveWithKo) || surviveOrKill == SurviveOrKill.Kill && (gameInfo.Survival == SurviveOrKill.Kill || gameInfo.Survival == SurviveOrKill.KillWithKo))
                return gameInfo.StartContent;
            else
                return gameInfo.StartContent.Opposite();
        }

        /// <summary>
        /// Determine if setup move is available for kill or survival.
        /// </summary>
        public static Boolean SetupMoveAvailable(Board board, Point p)
        {
            GameInfo gameInfo = board.GameInfo;
            SurviveOrKill surviveOrKill = GameHelper.KillOrSurvivalForNextMove(board);
            if (surviveOrKill == SurviveOrKill.Survive)
                return gameInfo.IsMovablePoint[p.x, p.y];
            else if (surviveOrKill == SurviveOrKill.Kill)
                return gameInfo.IsKillMovablePoint[p.x, p.y];
            return false;
        }

        /// <summary>
        /// Determine if next move is computer or player, based on count of last moves and initial player.
        /// </summary>
        public static PlayerOrComputer GetComputerOrPlayerForNextMove(Board board)
        {
            int lastMoveMod = board.LastMoves.Count % 2;
            GameInfo gameInfo = board.GameInfo;
            return (lastMoveMod == 0) ? gameInfo.UserFirst : gameInfo.UserFirst.Opposite();
        }
        
        /// <summary>
        /// Get specific board from last moves based on moveCount parameter.
        /// Requires that the root of the game starts from initial setup.
        /// </summary>
        public static Board GetSnapshotBoard(Game g, int moveCount)
        {
            if (g.Board.LastMoves.Count < moveCount)
                return g.Board;
            Board rootBoard = new Board(g.Root.Board);
            for (int i = rootBoard.LastMoves.Count; i < moveCount; i++)
            {
                Point p = g.Board.LastMoves[i];
                Content c = GetContentForNextMove(rootBoard);
                rootBoard.InternalMakeMove(p, c, true);
            }
            return rootBoard;
        }
    }
}
