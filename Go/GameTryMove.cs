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
        public LinkedPoint<Point> LinkPoint { get; set; }

        private bool? isKoFight = null;
        public bool IsKoFight
        {
            get
            {
                if (isKoFight == null)
                    isKoFight = KoHelper.IsKoFight(TryGame.Board);
                return isKoFight.Value;
            }
            set
            {
                isKoFight = value;
            }
        }

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
                return (board.CapturedList.Count == 0 && !AtariResolved && !board.IsAtariWithoutSuicide);
            }
        }

        /// <summary>
        /// Increased count of killer groups.
        /// </summary>
        public bool IncreasedKillerGroups
        {
            get
            {
                return IncreaseKillerGroups(TryGame.Board, CurrentGame.Board);
            }
        }

        public static Boolean IncreaseKillerGroups(Board tryBoard, Board currentBoard)
        {
            Content c = tryBoard.MoveGroup.Content;
            int tryCount = GroupHelper.GetKillerGroups(tryBoard, c).Count;
            int currentCount = GroupHelper.GetKillerGroups(currentBoard, c).Count;
            return (tryCount > currentCount);
        }


        /// <summary>
        /// Ko moves only - Check for atari moves. Return false if not redundant.
        /// Check if all target points captured <see cref="UnitTestProject.RedundantKoMoveTest.RedundantKoMoveTest_Scenario_GuanZiPu_A4Q11_101Weiqi" />
        /// </summary>
        public bool IsNegligibleForKo(Boolean opponentMove = false)
        {
            if (KoHelper.EssentialAtariForKoMove(this))
            {
                //check if all target points captured
                if (opponentMove)
                {
                    Board tryBoard = this.TryGame.Board;
                    if (AtariResolved) return false;
                    List<Point> targets = LifeCheck.GetTargets(tryBoard);
                    if (targets.Select(t => new { gr = tryBoard.GetGroupAt(t) }).All(t => t.gr.Liberties.Count == 1 && tryBoard.GetNeighbourGroups(t.gr).All(n => WallHelper.IsNonKillableGroup(tryBoard, n) || KoHelper.IsCaptureKoFight(tryBoard, n) != null)))
                        return true;
                }
                return false;
            }
            return true;
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
                return move;
            }
            return null;
        }

        public Boolean LinkForGroups()
        {
            return LinkHelper.LinkForGroups(TryGame.Board, CurrentGame.Board);
        }

        public override string ToString()
        {
            return Move.ToString();
        }
    }
}
