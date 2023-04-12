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
        public bool MustHaveNeutralPoint { get; set; }

        private bool? atariResolved = null;

        /// <summary>
        /// Atari resolved.
        /// </summary>
        public bool AtariResolved
        {
            get
            {
                if (atariResolved == null)
                    atariResolved = Board.ResolveAtari(CurrentGame.Board, TryGame.Board);
                return atariResolved.Value;
            }
            set
            {
                atariResolved = value;
            }
        }

        /// <summary>
        /// Atari without suicide.
        /// </summary>
        public bool AtariWithoutSuicide
        {
            get
            {
                Board tryBoard = this.TryGame.Board;
                return tryBoard.AtariTargets.Count > 0 && (tryBoard.MoveGroupLiberties > 1 || KoHelper.IsKoFight(tryBoard));
            }
        }

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
        /// Make ko move. Set KoGameCheck to allow only survive or kill for any further ko moves.
        /// </summary>
        public void MakeKoMove(Point p, SurviveOrKill surviveOrKill)
        {
            this.TryGame.KoGameCheck = (surviveOrKill == SurviveOrKill.Kill) ? KoCheck.Kill : KoCheck.Survive;
            this.TryGame.InternalMakeMove(p.x, p.y, true);
        }

        /// <summary>
        /// Is negligible.
        /// </summary>
        public bool IsNegligible
        {
            get
            {
                Board board = this.TryGame.Board;
                return (board.CapturedList.Count == 0 && !AtariResolved && !AtariWithoutSuicide);
            }
        }

        public static Boolean IsNegligibleForBoard(Board tryBoard, Board currentBoard, Func<Group, Boolean> func = null)
        {
            if (tryBoard.CapturedList.Count > 0) return false;
            if (Board.ResolveAtari(currentBoard, tryBoard)) return false;
            if ((tryBoard.MoveGroupLiberties > 1 || KoHelper.IsKoFight(tryBoard)) && tryBoard.AtariTargets.Any(t => (func != null) ? func(t) : true)) return false;
            return true;
        }

        /// <summary>
        /// Increased count of killer groups.
        /// </summary>
        public bool IncreasedKillerGroups
        {
            get
            {
                return GroupHelper.IncreasedKillerGroups(TryGame.Board, CurrentGame.Board);
            }
        }

        /// <summary>
        /// Make move at same point as opponent.
        /// </summary>
        public GameTryMove MakeMoveWithOpponentAtSamePoint(Boolean overrideKo = true)
        {
            Board opponentTryBoard = new Board(CurrentGame.Board);
            Content c = MoveContent;
            if (opponentTryBoard.InternalMakeMove(Move, c.Opposite(), overrideKo) == MakeMoveResult.Legal)
            {
                GameTryMove tryMove = new GameTryMove(CurrentGame);
                tryMove.TryGame.Board = opponentTryBoard;
                return tryMove;
            }
            return null;
        }

        /// <summary>
        /// Is redundant move.
        /// </summary>
        public bool IsRedundantMove
        {
            get
            {
                return IsEye || IsCoveredEyeMove || IsFillKoEyeMove || IsSuicidal || IsNeutralPoint || IsDiagonalEyeMove || IsRedundantKo || IsBaseLine || IsRedundantTigerMouth || IsRedundantEyeFiller || IsLeapMove || IsAtariRedundant;
            }
        }

        public Boolean LinkForGroups()
        {
            return LinkHelper.PossibleLinkForGroups(TryGame.Board, CurrentGame.Board);
        }

        public override string ToString()
        {
            return Move.ToString();
        }
    }
}
