using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Go
{
    public class State
    {
        private Node node;
        private Game game;
        private SurviveOrKill surviveOrKill;
        private int visitCount;
        private double winScore;
        private int depth;
        private Boolean isKoBlocked;
        private ConfirmAliveResult confirmAlive = ConfirmAliveResult.Unknown;
        private Boolean winOrLose;

        private int[,] heatMap;
        private double winrate;
        private Dictionary<String, double> stats;

        public State()
        {
        }

        public State(Game game)
        {
            this.game = game;
        }

        public virtual Node Node
        {
            get
            {
                return node;
            }
            set
            {
                this.node = value;
            }
        }

        public virtual Game Game
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

        public virtual SurviveOrKill SurviveOrKill //only survive or kill (without ko)
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

        public virtual int VisitCount
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

        public virtual double WinScore
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

        public virtual int Depth
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


        public virtual Boolean IsKoBlocked
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

        public virtual ConfirmAliveResult ConfirmAlive
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

        public virtual Boolean WinOrLose
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

        #region neural net values
        public int[,] HeatMap
        {
            get
            {
                return heatMap;
            }
            set
            {
                this.heatMap = value;
            }
        }

        public double Winrate
        {
            get
            {
                return winrate;
            }
            set
            {
                this.winrate = value;
            }
        }

        public Dictionary<String, double> Stats
        {
            get
            {
                if (stats == null)
                    stats = new Dictionary<string, double>() { { "N", 0 }, { "W", 0 }, { "Q", 0 }, { "P", 0 } };
                return stats;
            }
            set
            {
                this.stats = value;
            }
        }
        #endregion

        public virtual List<State> AllPossibleStates
        {
            get
            {
                SurviveOrKill stateSurvivalOrKill = GameHelper.KillOrSurvivalForNextMove(this.Game.Board);
                List<GameTryMove> tryMoves = GetAllPossibleMoves(this.Game);

                List<State> possibleStates = new List<State>();
                foreach (GameTryMove gameTryMove in tryMoves)
                {
                    State state = new State(gameTryMove.TryGame);
                    state.SurviveOrKill = stateSurvivalOrKill;
                    state.IsKoBlocked = (gameTryMove.MakeMoveResult == MakeMoveResult.KoBlocked);
                    possibleStates.Add(state);
                }
                return possibleStates;
            }
        }


        /// <summary>
        /// Get all possible moves for mcts, including ko moves.
        /// </summary>
        public static List<GameTryMove> GetAllPossibleMoves(Game g, Boolean includeKo = true, Boolean getAll = false)
        {
            SurviveOrKill stateSurvivalOrKill = GameHelper.KillOrSurvivalForNextMove(g.Board);
            List<GameTryMove> tryMoves;
            GameTryMove koBlockedMove;
            if (stateSurvivalOrKill == SurviveOrKill.Kill)
                (_, tryMoves, koBlockedMove) = g.GetKillMoves(null, getAll);
            else
                (_, tryMoves, koBlockedMove) = g.GetSurvivalMoves(null, getAll);

            if (tryMoves != null)
            {
                if (includeKo)
                    MonteCarloTreeSearch.MonteCarloIncludeKoMoves(g, tryMoves, koBlockedMove, stateSurvivalOrKill);
            }
            return tryMoves;
        }

        internal virtual void IncrementVisit(int multiplier = 1)
        {
            this.visitCount += multiplier;
        }

        internal virtual void AddScore(double score)
        {
            if (this.winScore != int.MinValue)
                this.winScore += score;
        }

        public override String ToString()
        {
            String rc = "";
            List<Point> lastMoves = this.Game.Board.LastMoves;
            for (int i = 0; i <= lastMoves.Count - 1; i++)
            {
                Point point = lastMoves[i];
                rc += point;
                if (i < lastMoves.Count - 1)
                    rc += ",";
            }
            rc = "Move:" + rc;
            if (heatMap != null)
            {
                rc += "\nHeatmap:";
                rc += "\n" + new String(' ', 4);
                for (int j = 0; j < this.Game.Board.SizeX; j++)
                {
                    rc += j.ToString().PadRight(2, ' ');
                }
                for (int i = 0; i < this.Game.Board.SizeY; i++)
                {
                    rc += "\n" + i.ToString().PadLeft(3, ' ') + " ";
                    for (int j = 0; j < this.Game.Board.SizeX; j++)
                    {
                        rc += heatMap[j, i] + " ";
                    }
                }
                rc += "\nWinrate:" + this.Winrate;
            }
            return rc;
        }

    }
}
