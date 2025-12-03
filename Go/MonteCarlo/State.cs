using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Go
{
    [Serializable]
    public class State
    {
        private Game game;
        private SurviveOrKill surviveOrKill;
        private int visitCount;
        private double winScore;
        private int depth;
        private Boolean isKoBlocked;
        private ConfirmAliveResult confirmAlive = ConfirmAliveResult.Unknown;
        private Boolean winOrLose;

        public State()
        {
        }

        public State(Game game)
        {
            this.game = game;
        }

        public Game Game
        {
            get
            {
                return game;
            }
            set
            {
                this.game = value;
            }
        }

        public SurviveOrKill SurviveOrKill
        {
            get
            {
                return surviveOrKill;
            }
            set
            {
                this.surviveOrKill = value;
            }
        }

        public int VisitCount
        {
            get
            {
                return visitCount;
            }
            set
            {
                this.visitCount = value;
            }
        }

        public double WinScore
        {
            get
            {
                return winScore;
            }
            set
            {
                this.winScore = value;
            }
        }

        public int Depth
        {
            get
            {
                return depth;
            }
            set
            {
                this.depth = value;
            }
        }


        public Boolean IsKoBlocked
        {
            get
            {
                return isKoBlocked;
            }
            set
            {
                this.isKoBlocked = value;
            }
        }

        public ConfirmAliveResult ConfirmAlive
        {
            get
            {
                return confirmAlive;
            }
            set
            {
                confirmAlive = value;
            }
        }

        public Boolean WinOrLose
        {
            get
            {
                return winOrLose;
            }
            set
            {
                winOrLose = value;
            }
        }


        public List<State> AllPossibleStates
        {
            get
            {
                SurviveOrKill survivalOrKill = GameHelper.KillOrSurvivalForNextMove(this.Game.Board);
                List<GameTryMove> tryMoves = GameHelper.GetTryMovesForGame(this.Game);

                List<State> possibleStates = new List<State>();
                foreach (GameTryMove tryMove in tryMoves)
                {
                    State state = new State(tryMove.TryGame);
                    state.SurviveOrKill = survivalOrKill;
                    state.IsKoBlocked = (tryMove.MakeMoveResult == MakeMoveResult.KoBlocked);
                    possibleStates.Add(state);
                }
                return possibleStates;
            }
        }

        internal void IncrementVisit(int multiplier = 1)
        {
            this.visitCount += multiplier;
        }

        internal void AddScore(double score)
        {
            if (this.winScore != int.MinValue)
                this.winScore += score;
        }

        public override String ToString()
        {
            return "Move:" + this.Game.Board.LastMoves.GetConcatenatedString();
        }

    }
}
