using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Go
{
    /// <summary>
    /// Game try move.
    /// </summary>
    public class GameTryMove
    {
        public Game TryGame { get; set; }
        public Game CurrentGame { get; set; }
        public MakeMoveResult MakeMoveResult { get; set; }
        public ConfirmAliveResult ConfirmAlive { get; set; }
        public GameTryMove OpponentBestMove { get; set; }
        public Board CaptureBoard { get; set; }

        public bool IsEye { get; set; }
        public bool IsCoveredEyeMove { get; set; }
        public bool IsFillKoEyeMove { get; set; }
        public bool IsSuicidal { get; set; }
        public bool IsNeutralPoint { get; set; }
        public bool IsDiagonalEyeMove { get; set; }
        public bool IsRedundantKo { get; set; }
        public bool IsRedundantTigerMouth { get; set; }
        public bool IsAtariRedundant { get; set; }
        public bool IsLeapMove { get; set; }
        public bool IsNonSuicidal { get; set; }
        public bool IsFillerMove { get; set; }
        public bool IsRedundantNeuralNetMove { get; set; }

        /// <summary>
        /// Atari resolved.
        /// </summary>
        private bool? atariResolved = null;
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
                return AtariHelper.IsAtariWithoutSuicide(TryGame.Board);
            }
        }

        /// <summary>
        /// Captured.
        /// </summary>
        public bool Captured
        {
            get
            {
                return TryGame.Board.CapturedList.Count > 0;
            }
        }

        public Point Move
        {
            get
            {
                return TryGame.Board.Move.Value;
            }
        }

        public Content MoveContent
        {
            get
            {
                return TryGame.Board[Move];
            }
        }

        public int MoveGroupLiberties
        {
            get
            {
                return TryGame.Board.MoveGroupLiberties;
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
            this.TryGame.Board.KoGameCheck = (surviveOrKill == SurviveOrKill.Kill) ? KoCheck.Kill : KoCheck.Survive;
            this.TryGame.InternalMakeMove(p.x, p.y, true);
        }

        /// <summary>
        /// Is negligible.
        /// </summary>
        public bool IsNegligible
        {
            get
            {
                return (!Captured && !AtariResolved && !AtariWithoutSuicide);
            }
        }

        /// <summary>
        /// Move connect and die.
        /// </summary>
        private bool? moveConnectAndDie = null;
        public bool MoveConnectAndDie
        {
            get
            {
                if (moveConnectAndDie == null)
                {
                    (Boolean connectAndDie, Board captureBoard) = ImmovableHelper.ConnectAndDie(TryGame.Board, TryGame.Board.MoveGroup, false);
                    this.moveConnectAndDie = connectAndDie;
                    this.CaptureBoard = captureBoard;
                }
                return moveConnectAndDie.Value;
            }
        }

        /// <summary>
        /// Connect and die resolved.
        /// </summary>
        private bool? connectAndDieResolved = null;
        public bool ConnectAndDieResolved
        {
            get
            {
                if (connectAndDieResolved == null)
                {
                    if (MoveConnectAndDie) return false;
                    if (AtariResolved) return true;
                    //check connect and die by opponent move
                    if (OpponentMove == null) return false;
                    Board b = OpponentMove.TryGame.Board;
                    this.connectAndDieResolved = b.GetGroupsFromStoneNeighbours().Any(n => ImmovableHelper.UnescapableGroup(b, n).Item1);
                }
                return connectAndDieResolved.Value;
            }
        }

        /// <summary>
        /// Increased killer groups.
        /// </summary>
        public bool IncreasedKillerGroups
        {
            get
            {
                return GroupHelper.IncreasedKillerGroups(TryGame.Board, CurrentGame.Board);
            }
        }

        /// <summary>
        /// Opponent move.
        /// </summary>
        GameTryMove opponentMove = null;
        public GameTryMove OpponentMove
        {
            get
            {
                if (opponentMove == null)
                    opponentMove = MakeMoveWithOpponentAtSamePoint();
                return opponentMove;
            }
        }

        /// <summary>
        /// Make move with opponent at same point.
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
                return IsEye || IsCoveredEyeMove || IsFillKoEyeMove || IsSuicidal || IsNeutralPoint || IsDiagonalEyeMove || IsRedundantKo || IsRedundantTigerMouth || IsAtariRedundant || IsLeapMove || IsNonSuicidal || IsFillerMove || IsRedundantNeuralNetMove;
            }
        }

        /// <summary>
        /// Link for groups.
        /// </summary>
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
