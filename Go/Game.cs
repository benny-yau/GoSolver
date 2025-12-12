using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Go
{
    [Serializable]
    public partial class Game
    {
        public Game Root { get; private set; }
        public Board Board { get; set; }

        [NonSerialized]
        Stopwatch RunTimeStopWatch;

        /// <summary>
        /// Game info.
        /// </summary>
        GameInfo gameInfo;
        public GameInfo GameInfo
        {
            get
            {
                return (this != this.Root) ? this.Root.GameInfo : gameInfo;
            }
            set
            {
                gameInfo = value;
            }
        }

        /// <summary>
        /// Create new game from game info.
        /// </summary>
        public Game(GameInfo gi)
        {
            GameInfo = gi;
            InitializeFromGameInfo();
        }

        /// <summary>
        /// Create new game from previous game.
        /// </summary>
        public Game(Game fromGame)
        {
            Board = new Board(fromGame.Board);
            Board.GameInfo = fromGame.Root.GameInfo;
            Root = fromGame.Root;
        }

        /// <summary>
        /// Initialize from game info.
        /// </summary>
        private void InitializeFromGameInfo()
        {
            Root = this;
            Board = new Board(GameInfo);
        }

        /// <summary>
        /// Make move.
        /// </summary>
        public MakeMoveResult MakeMove(Board board)
        {
            Point move = board.Move.Value;
            MakeMoveResult result = MakeMove(move);
            this.Board.IsRandomMove = board.IsRandomMove;
            this.Board.KoGameCheck = board.KoGameCheck;
            return result;
        }

        public MakeMoveResult MakeMove(Point p)
        {
            Content c = GameHelper.GetContentForNextMove(this.Board);
            return MakeMove(p.x, p.y, c);
        }

        public MakeMoveResult MakeMove(int x, int y)
        {
            Content c = GameHelper.GetContentForNextMove(this.Board);
            return MakeMove(x, y, c);
        }

        public MakeMoveResult MakeMove(int x, int y, Content c)
        {
            MakeMoveResult result = this.Board.InternalMakeMove(x, y, c);
            if (result == MakeMoveResult.Legal)
                return result;
            else if (result != MakeMoveResult.Pass)
                this.Board.Move = Game.PassMove;
            return result;
        }

        /// <summary>
        /// Setup move.
        /// </summary>
        public void SetupMove(int x, int y, Content c)
        {
            if (Board[x, y] != Content.Empty)
                throw new Exception("Setup move position taken.");
            Board[x, y] = c;
            this.GameInfo.SetupMoves.Add(new SetupMove(new Point(x, y), c));
        }

        /// <summary>
        /// Internal make move.
        /// </summary>
        public MakeMoveResult InternalMakeMove(int x, int y, Boolean overrideKo = false)
        {
            Content c = GameHelper.GetContentForNextMove(this.Board);
            return this.Board.InternalMakeMove(x, y, c, overrideKo);
        }

        /// <summary>
        /// Game depth.
        /// </summary>
        public int GameDepth(Game g)
        {
            return g.Board.LastMoves.Count - this.Board.LastMoves.Count;
        }

        /// <summary>
        /// Print game moves list.
        /// </summary>
        public void PrintGameMoveList(List<GameTryMove> tryMoves, Game g)
        {
            int gameDepth = GameDepth(g);
            if (!DebugPrintMode(gameDepth)) return;
            DebugHelper.WriteLine("Game try moves: " + tryMoves.GetConcatenatedString(), gameDepth);
        }

        /// <summary>
        /// Debug print mode.
        /// </summary>
        public Boolean DebugPrintMode(int gameDepth)
        {
            return (debugMode && !UseMCTS && gameDepth <= 3);
        }

    }
}
