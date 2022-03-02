using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Go
{
    /// <summary>
    /// Possible try move made in board of TryGame with all associated properties of the move.
    /// The new move is made on the board of the TryGame, while the old board is retained on the CurrentGame.
    /// </summary>
    public class GameTryMove
    {
        public Game TryGame { get; set; }
        public Game CurrentGame { get; set; }
        public MakeMoveResult MakeMoveResult { get; set; }
        public ConfirmAliveResult ConfirmAlive { get; set; }
        public GameTryMove OpponentBestMove { get; set; }


        public bool IsEye { get; set; }
        public bool IsCoveredEyeMove { get; set; }
        public bool IsFillKoEyeMove { get; set; }
        public bool IsSuicidal { get; set; }
        public bool IsNeutralPoint { get; set; }
        public bool IsDiagonalEyeMove { get; set; }
        public bool IsRedundantKo { get; set; }
        public bool IsBaseLine { get; set; }
        public bool IsRedundantTigerMouth { get; set; }
        public bool IsRedundantEyeFiller { get; set; }
        public bool IsLeapMove { get; set; }
        public bool IsAtariRedundant { get; set; }
        public bool IsKoFight { get; set; }
        public bool MustHaveNeutralPoint { get; set; }
        public LinkedPoint<Point> LinkPoint { get; set; }

        public Point Move
        {
            get
            {
                if (TryGame != null)
                    return TryGame.Board.Move.Value;
                else
                    return Game.PassMove;
            }
        }

        public Content MoveContent
        {
            get
            {
                if (Move.Equals(Game.PassMove)) return Content.Unknown;
                return TryGame.Board[Move];
            }
        }

        public GameTryMove(Game game)
        {
            TryGame = new Game(game);
            CurrentGame = game;
        }

        public GameTryMove(Game game, Point p)
        {
            TryGame = new Game(game);
            CurrentGame = game;
            TryGame.MakeMove(p.x, p.y);
        }

        /// <summary>
        /// Make ko move within board. Set KoGameCheck to mark game within ko and koMoveRepeat to limit ko repeats.
        /// </summary>
        public void MakeKoMove(Point p, SurviveOrKill surviveOrKill)
        {
            this.TryGame.KoGameCheck = (surviveOrKill == SurviveOrKill.Kill) ? KoCheck.Kill : KoCheck.Survive;
            this.TryGame.InternalMakeMove(p.x, p.y, true);
            ProcessGameTryMoves();
        }

        public bool IsRedundantMove
        {
            get
            {
                return IsEye || IsCoveredEyeMove || IsFillKoEyeMove || IsSuicidal || IsNeutralPoint || IsDiagonalEyeMove || IsRedundantKo || IsBaseLine || IsRedundantTigerMouth || IsRedundantEyeFiller || IsLeapMove || IsAtariRedundant;
            }
        }
        /// <summary>
        /// Negligible moves are checked for redundancy. Return false if not redundant.
        /// </summary>
        public bool IsNegligible
        {
            get
            {
                Board board = this.TryGame.Board;
                return (board.CapturedList.Count == 0 && !board.AtariResolved && !(board.IsAtariMove && board.MoveGroupLiberties > 1));
            }
        }

        /// <summary>
        /// Increased count of killer groups.
        /// </summary>
        public bool IncreasedKillerGroups
        {
            get
            {
                int tryCount = BothAliveHelper.GetCorneredKillerGroup(TryGame.Board, false).Count;
                int currentCount = BothAliveHelper.GetCorneredKillerGroup(CurrentGame.Board, false).Count;
                return (tryCount > currentCount);
            }
        }


        /// <summary>
        /// Ko moves only - Check for atari moves. Return false if not redundant.
        /// </summary>
        public bool IsNegligibleForKo
        {
            get
            {
                Board tryBoard = this.TryGame.Board;
                if (tryBoard.IsAtariMove)
                    return false;

                return true;
            }
        }

        /// <summary>
        /// Make move at same point as game try move but as opponent instead.
        /// </summary>
        public GameTryMove MakeMoveWithOpponentAtSamePoint(Boolean excludeKo = true)
        {
            Board opponentTryBoard = new Board(CurrentGame.Board);
            Point p = Move;
            Content c = MoveContent;
            if (opponentTryBoard.InternalMakeMove(p, c.Opposite(), excludeKo) == MakeMoveResult.Legal)
            {
                GameTryMove move = new GameTryMove(CurrentGame);
                move.TryGame.Board = opponentTryBoard;
                AtariHelper.FindAndResolveAtari(move);
                return move;
            }
            return null;
        }

        public Boolean LinkForGroups()
        {
            return LinkHelper.LinkForGroups(TryGame.Board, CurrentGame.Board);
        }

        /// <summary>
        /// Process game try moves for finding redundant moves and sorting.
        /// </summary>
        public void ProcessGameTryMoves()
        {
            AtariHelper.FindAndResolveAtari(this);
            KoHelper.CheckIsKoFight(this);
        }

        public override string ToString()
        {
            return Move.ToString();
        }
    }
}
