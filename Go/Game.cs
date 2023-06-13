using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Go
{
    /// <summary>
    /// Contains core of methods for game play. The root of a specified game may be obtained using the Root property.
    /// </summary>
    public partial class Game
    {
        public Game Root { get; private set; }
        public Board Board { get; set; }

        public static int LookAheadDepth = 8;
        public static Boolean breakRealTime = Convert.ToBoolean(ConfigurationSettings.AppSettings["BREAK_REAL_TIME"]);
        public static int runTimeStop = 20000; //20 seconds default for real-time moves

        GameInfo gameInfo;
        /// <summary>
        /// Retrieves the game info is stored in the root of the game.
        /// </summary>
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
        /// Depth to start with. Pass move not counted.
        /// Look ahead depth is minimum depth to start with.
        /// </summary>
        public int GetStartingDepth()
        {
            int depth = GameInfo.SearchDepth + 2 - Board.LastMoves.Count;
            int passMoves = Board.LastMoves.Count(m => m.Equals(Game.PassMove));
            depth += passMoves;
            if (depth < LookAheadDepth)
                depth = LookAheadDepth;
            return depth;
        }


        /// <summary>
        /// Enable or disable break on real-time move.
        /// </summary>
        public static Boolean BreakRealTime
        {
            get
            {
                return breakRealTime && !MonteCarloMapping.mapMovesOrSearchAnswer;
            }
            set
            {
                breakRealTime = value;
            }
        }

        Stopwatch runTimeStopWatch;
        /// <summary>
        /// Stop watch for run time.
        /// </summary>
        public Stopwatch RunTimeStopWatch
        {
            get
            {
                Stopwatch stopWatch = this.Root.runTimeStopWatch;
                if (stopWatch == null)
                    stopWatch = new Stopwatch();
                return stopWatch;
            }
            set
            {
                this.Root.runTimeStopWatch = value;
            }
        }

        /// <summary>
        /// Time out if stop watch exceeds time limit set at runTimeStop.
        /// </summary>
        public static Boolean TimeOut(Game g)
        {
            return Game.BreakRealTime && g.Root.RunTimeStopWatch.ElapsedMilliseconds >= Game.runTimeStop;
        }

        /// <summary>
        /// Create new game.
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
        /// Initialize new game with root game and board parameters.
        /// </summary>
        private void InitializeFromGameInfo()
        {
            Root = this;
            Board = new Board(GameInfo);
        }

        /// <summary>
        /// Make move on initial board.
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

        /// <summary>
        /// Make move on the board and set pass move for ko moves.
        /// </summary>
        public MakeMoveResult MakeMove(int x, int y, Content content)
        {
            if (Game.TimeOut(this))
                return MakeMoveResult.Unknown;

            MakeMoveResult result = this.Board.InternalMakeMove(x, y, content);
            if (result == MakeMoveResult.Legal)
                return result;
            else if (result != MakeMoveResult.Pass)
            {
                this.Board.Move = Game.PassMove;
                this.Board.LastMoves.Add(Game.PassMove);
            }
            return result;
        }

        /// <summary>
        /// Setup move on the board from initial scenario.
        /// </summary>
        public void SetupMove(int x, int y, Content c)
        {
            if (Board[x, y] != Content.Empty)
                throw new Exception("Setup move position taken.");
            Board[x, y] = c;
            this.GameInfo.SetupMoves.Add(new SetupMove(new Point(x, y), c));
        }

        /// <summary>
        /// Makes move on board internally. Returns result as MakeMoveResult.
        /// </summary>
        public MakeMoveResult InternalMakeMove(int x, int y, Boolean overrideKo = false)
        {
            Content c = GameHelper.GetContentForNextMove(this.Board);
            return this.Board.InternalMakeMove(x, y, c, overrideKo);
        }

        /// <summary>
        /// Depth of game starting from zero.
        /// </summary>
        public int GameDepth(Game currentGame)
        {
            return currentGame.Board.LastMoves.Count - this.Board.LastMoves.Count;
        }

        #region mapped moves
        /// <summary>
        /// Check if move is present in solution or json map. Solution moves can end either on player or computer move, so check if solution is completed before and after making move.
        /// </summary>
        public ConfirmAliveResult CheckSolutionAndMappedPoints()
        {
            //check solution points
            ConfirmAliveResult result = ConfirmAliveResult.Unknown;
            if (UseSolutionPoints)
            {
                if (this.GameInfo.solutionPoints.Count > 0)
                {
                    ConfirmAliveResult solutionComplete = SolutionHelper.CheckSolutionComplete(this.Board);
                    if (solutionComplete != ConfirmAliveResult.Unknown)
                        return solutionComplete | ConfirmAliveResult.Mapped;
                    else
                    {
                        //get solution move and make move on board
                        if (SolutionHelper.UseSolutionPoints(this))
                        {
                            result = ConfirmAliveResult.Mapped | ConfirmAliveResult.UseSolution;
                            solutionComplete = SolutionHelper.CheckSolutionComplete(this.Board);
                            if (solutionComplete != ConfirmAliveResult.Unknown)
                                result |= solutionComplete;
                            return result;
                        }
                        else
                            result = ConfirmAliveResult.Incorrect;
                    }
                }
                else if (this.GameInfo.solutionPoints.Count == 0 && this.GameInfo.UserFirst == PlayerOrComputer.Computer)
                {
                    return ConfirmAliveResult.Mapped | ConfirmAliveResult.NoSolution;
                }
            }

            //check mapped points
            if (UseMapMoves)
            {
                if (!result.HasFlag(ConfirmAliveResult.Mapped))
                    result = UseDictatePoints(result);

                if (!result.HasFlag(ConfirmAliveResult.Mapped))
                {
                    int isChallenge = Convert.ToInt32(this.GameInfo.UserFirst == PlayerOrComputer.Computer);
                    if (this.Board.LastMoves.Count == 1 + isChallenge)
                    {
                        //get second mapped move from json
                        dynamic json = (isChallenge == 0) ? this.GameInfo.PlayerMoveJson : this.GameInfo.ChallengeMoveJson;
                        if (json == null) return result;
                        return FindSecondMoveMapped(json);
                    }
                    else if (this.Board.LastMoves.Count == 3 + isChallenge)
                    {
                        //get fourth mapped move from json
                        dynamic json = (isChallenge == 0) ? this.GameInfo.PlayerMoveJson : this.GameInfo.ChallengeMoveJson;
                        if (json == null) return result;
                        return FindFourthMoveMapped(json);
                    }
                    else if (this.Board.LastMoves.Count == 5 + isChallenge)
                    {
                        //get sixth mapped move from json
                        dynamic json = (isChallenge == 0) ? this.GameInfo.PlayerMoveJsonExtension : this.GameInfo.ChallengeMoveJsonExtension;
                        if (json == null) return result;
                        return FindSixthMoveMapped(json);
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Use dictate points specified which by-pass the mapped points. This allow for path correction and extension of mapped points beyond sixth move where much calculation time is required.
        /// </summary>
        private ConfirmAliveResult UseDictatePoints(ConfirmAliveResult result)
        {
            Point? p = SolutionHelper.GetDictateMove(this);
            if (p == null) return result;
            MakeMoveResult moveResult = this.MakeMove(p.Value);
            result = ConfirmAliveResult.Incorrect | ConfirmAliveResult.Mapped;
            if (moveResult == MakeMoveResult.KoBlocked)
                result |= ConfirmAliveResult.KoAlive;
            return result;
        }

        /// <summary>
        /// Get second move from json map and return confirm alive result.
        /// </summary>
        private ConfirmAliveResult FindSecondMoveMapped(dynamic jsonMap)
        {
            ConfirmAliveResult result = ConfirmAliveResult.Incorrect;
            if (this.Board.LastMoves.Count == 2 && !this.Board.LastMoves[0].Equals(this.GameInfo.solutionPoints[0][0]))
                return result;

            Point firstMovePt = this.Board.LastMoves[this.Board.LastMoves.Count - 1];
            JToken firstMove = ((JArray)jsonMap).Where(s => (int)s["FirstMove"]["x"] == firstMovePt.x && (int)s["FirstMove"]["y"] == firstMovePt.y).FirstOrDefault();

            if (firstMove == null) return result;
            int x = (int)firstMove["SecondMove"]["x"];
            int y = (int)firstMove["SecondMove"]["y"];
            MakeMoveResult moveResult = this.MakeMove(x, y);
            result |= ConfirmAliveResult.Mapped;

            if (moveResult == MakeMoveResult.KoBlocked)
                result |= ConfirmAliveResult.KoAlive;
            return result;
        }

        /// <summary>
        /// Get fourth move from json map and return confirm alive result.
        /// </summary>
        private ConfirmAliveResult FindFourthMoveMapped(dynamic jsonMap)
        {
            ConfirmAliveResult result = ConfirmAliveResult.Incorrect;
            if (this.Board.LastMoves.Count == 4 && !this.Board.LastMoves[0].Equals(this.GameInfo.solutionPoints[0][0]))
                return result;

            Point firstMovePt = this.Board.LastMoves[this.Board.LastMoves.Count - 3];
            Point secondMovePt = this.Board.LastMoves[this.Board.LastMoves.Count - 2];

            JObject firstLevelMove = (JObject)((JArray)jsonMap).Where(m => (int)m["FirstMove"]["x"] == firstMovePt.x && (int)m["FirstMove"]["y"] == firstMovePt.y && (int)m["SecondMove"]["x"] == secondMovePt.x && (int)m["SecondMove"]["y"] == secondMovePt.y).FirstOrDefault();

            if (firstLevelMove == null) return result;

            JArray SecondLevel = (JArray)firstLevelMove["SecondLevel"];
            if (SecondLevel == null) return result;
            Point lastMovePt = this.Board.LastMoves[this.Board.LastMoves.Count - 1];
            JToken secondLevelMove = SecondLevel.Where(m => (int)m["ThirdMove"]["x"] == lastMovePt.x && (int)m["ThirdMove"]["y"] == lastMovePt.y).FirstOrDefault();
            if (secondLevelMove == null) return result;
            JToken fourthMove = secondLevelMove["FourthMove"];
            int x = (int)fourthMove["x"];
            int y = (int)fourthMove["y"];
            MakeMoveResult moveResult = this.MakeMove(x, y);
            result |= ConfirmAliveResult.Mapped;

            if (moveResult == MakeMoveResult.KoBlocked)
                result |= ConfirmAliveResult.KoAlive;
            return result;
        }


        /// <summary>
        /// Get sixth move from json map and return confirm alive result.
        /// </summary>
        private ConfirmAliveResult FindSixthMoveMapped(dynamic jsonMap)
        {
            ConfirmAliveResult result = ConfirmAliveResult.Incorrect;
            if (this.Board.LastMoves.Count == 6 && !this.Board.LastMoves[0].Equals(this.GameInfo.solutionPoints[0][0]))
                return result;

            Point firstMovePt = this.Board.LastMoves[this.Board.LastMoves.Count - 5];
            Point secondMovePt = this.Board.LastMoves[this.Board.LastMoves.Count - 4];

            JObject firstLevelMove = (JObject)((JArray)jsonMap).Where(m => (int)m["FirstMove"]["x"] == firstMovePt.x && (int)m["FirstMove"]["y"] == firstMovePt.y && (int)m["SecondMove"]["x"] == secondMovePt.x && (int)m["SecondMove"]["y"] == secondMovePt.y).FirstOrDefault();

            if (firstLevelMove == null) return result;

            JArray SecondLevel = (JArray)firstLevelMove["SecondLevel"];
            if (SecondLevel == null) return result;

            Point thirdMovePt = this.Board.LastMoves[this.Board.LastMoves.Count - 3];
            Point fourthMovePt = this.Board.LastMoves[this.Board.LastMoves.Count - 2];

            JObject secondLevelMove = (JObject)SecondLevel.Where(m => (int)m["ThirdMove"]["x"] == thirdMovePt.x && (int)m["ThirdMove"]["y"] == thirdMovePt.y && (int)m["FourthMove"]["x"] == fourthMovePt.x && (int)m["FourthMove"]["y"] == fourthMovePt.y).FirstOrDefault();

            if (secondLevelMove == null) return result;

            JArray ThirdLevel = (JArray)secondLevelMove["ThirdLevel"];
            if (ThirdLevel == null) return result;

            Point lastMovePt = this.Board.LastMoves[this.Board.LastMoves.Count - 1];
            JToken thirdLevelMove = ThirdLevel.Where(m => (int)m["FifthMove"]["x"] == lastMovePt.x && (int)m["FifthMove"]["y"] == lastMovePt.y).FirstOrDefault();
            if (thirdLevelMove == null) return result;
            JToken sixthMove = thirdLevelMove["SixthMove"];
            int x = (int)sixthMove["x"];
            int y = (int)sixthMove["y"];
            MakeMoveResult moveResult = this.MakeMove(x, y);
            result |= ConfirmAliveResult.Mapped;

            if (moveResult == MakeMoveResult.KoBlocked)
                result |= ConfirmAliveResult.KoAlive;
            return result;
        }

        /// <summary>
        /// Include flag for target survived or killed.
        /// </summary>
        public ConfirmAliveResult CheckMappedResults(ConfirmAliveResult result)
        {
            if (result.HasFlag(ConfirmAliveResult.NoSolution) || result.HasFlag(ConfirmAliveResult.UseSolution) || result.HasFlag(ConfirmAliveResult.Answer) || result.HasFlag(ConfirmAliveResult.SolutionDisplayed))
                return result;
            if (result.HasFlag(ConfirmAliveResult.KoAlive)) return result;

            SurviveOrKill surviveOrKill = GameHelper.KillOrSurvivalForNextMove(this.Board).Opposite();
            result = LifeCheck.CheckIfTargetSurvivedOrKilled(result, surviveOrKill, this.Board);
            return result;
        }
        #endregion

        /// <summary>
        /// Call from immediate window to show game try moves for survival moves.
        /// </summary>
        public void PrintSurvivalList()
        {
            (ConfirmAliveResult result, List<GameTryMove> tryMoves, GameTryMove koBlockedMove) = GetSurvivalMoves();
            String content = DebugHelper.PrintGameTryMoves(this, tryMoves, null);
            Debug.WriteLine(content);
        }

        /// <summary>
        /// Call from immediate window to show game try moves for kill moves.
        /// </summary>
        public void PrintKillList()
        {
            (ConfirmAliveResult result, List<GameTryMove> tryMoves, GameTryMove koBlockedMove) = GetKillMoves();
            String content = DebugHelper.PrintGameTryMoves(this, tryMoves, null);
            Debug.WriteLine(content);
        }

        /// <summary>
        /// Print game moves on exhaustive mode.
        /// </summary>
        public void PrintGameMoveList(List<GameTryMove> tryMoves, List<GameTryMove> redundantTryMoves, Game currentGame)
        {
            int gameDepth = GameDepth(currentGame);
            if (IsExhaustiveMode(gameDepth))
            {
                String msg = "";
                foreach (GameTryMove g in tryMoves)
                {
                    if (msg != "") msg += ",";
                    msg += "(" + g.Move.x + "," + g.Move.y + ")";
                }
                DebugHelper.DebugWriteWithTab("Game try moves: " + msg, gameDepth);

                if (new StackTrace().GetFrame(3).GetMethod().Name == "btnPrintMoves_Click")
                {
                    String content = DebugHelper.PrintGameTryMoves(currentGame, tryMoves, redundantTryMoves);
                    Debug.WriteLine(content);
                }
            }
        }

        /// <summary>
        /// To print debug statements on exhaustive mode.
        /// </summary>
        public Boolean IsExhaustiveMode(int gameDepth)
        {
            return (debugMode && !useMonteCarloRuntime && gameDepth <= 3);
        }

    }
}
