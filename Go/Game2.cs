using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;

namespace Go
{
    public partial class Game
    {
        public static Boolean debugMode = Convert.ToBoolean(ConfigurationSettings.AppSettings["DEBUG_MODE"]);
        public static Boolean useMonteCarloRuntime = Convert.ToBoolean(ConfigurationSettings.AppSettings["USE_MONTE_CARLO_RUNTIME"]);

        public static Boolean UseSolutionPoints = true;
        public static Boolean UseMapMoves = true;
        public static readonly Point PassMove = new Point(-1, -1);
        public int reachedEndOfDepth = 0;

        /// <summary>
        /// Initializes start of search for exhaustive or real-time mcts move (default).
        /// To debug with exhaustive search, set USE_MONTE_CARLO_RUNTIME in app.config to false.
        /// </summary>
        public ConfirmAliveResult InitializeComputerMove()
        {
            try
            {
                this.Board.Move = null;
                ConfirmAliveResult result = CheckSolutionAndMappedPoints();

                if (!result.HasFlag(ConfirmAliveResult.Mapped))
                {
                    ConfirmAliveResult confirmAlive = ConfirmAliveResult.Unknown;
                    if (!useMonteCarloRuntime)
                        confirmAlive = MakeExhaustiveSearch();
                    else
                        confirmAlive = MonteCarloGame.MonteCarloRealTimeMove(this).Item1;
                    result |= confirmAlive;

                    if (debugMode)
                        Debug.WriteLine("Final move: " + this.Board.Move + " | Final result: " + confirmAlive.ToString() + " | Reached end of depth: " + (this.Root.reachedEndOfDepth > 0).ToString());
                }
                else
                {
                    result = CheckMappedResults(result);
                }
                if (this.Board.Move == null)
                {
                    this.Board.Move = Game.PassMove;
                    this.Board.LastMoves.Add(Game.PassMove);
                }
                return result;
            }
            catch (Exception ex)
            {
                if (Game.debugMode) Debugger.Break();
                return ConfirmAliveResult.Unknown;
            }
        }


        /// <summary>
        /// Start exhaustive search.
        /// </summary>
        public ConfirmAliveResult MakeExhaustiveSearch()
        {
            if (debugMode)
                this.RunTimeStopWatch = Stopwatch.StartNew();

            int depth = GetStartingDepth();
            ConfirmAliveResult confirmAlive = ConfirmAliveResult.Unknown;
            GameTryMove bestResultMove = null;

            //start kill or survival move
            SurviveOrKill surviveOrKill = GameHelper.KillOrSurvivalForNextMove(this.Board);
            if (surviveOrKill == SurviveOrKill.Kill)
                (confirmAlive, bestResultMove) = this.MakeKillMove(depth);
            else
                (confirmAlive, bestResultMove) = this.MakeSurvivalMove(depth);

            //make the move at initial board
            if (GameHelper.WinOrLose(surviveOrKill, confirmAlive, this.GameInfo))
                this.MakeMove(bestResultMove.Move);

            if (debugMode)
            {
                this.RunTimeStopWatch.Stop();
                Debug.WriteLine("Time taken (exhaustive): " + this.RunTimeStopWatch.ElapsedMilliseconds);
            }
            return confirmAlive;
        }

        /// <summary>
        /// Get all possible survival moves. Check if the game has ended with target survived. Check and remove redundant moves. 
        /// For survive only, check for recursion and add pass move to check for both alive where necessary.
        /// </summary>
        public (ConfirmAliveResult, List<GameTryMove>, GameTryMove) GetSurvivalMoves(Game g = null)
        {
            Game currentGame = g ?? this;
            GameInfo gameInfo = currentGame.GameInfo;
            Content c = GameHelper.GetContentForSurviveOrKill(gameInfo, SurviveOrKill.Survive);
            List<GameTryMove> tryMoves = new List<GameTryMove>();
            List<GameTryMove> redundantTryMoves = new List<GameTryMove>();
            GameTryMove koBlockedMove = null;
            Boolean mappingRange = MonteCarloMapping.MappingRange(currentGame.Board);

            for (int i = 0; i <= gameInfo.movablePoints.Count - 1; i++)
            {
                Point p = gameInfo.movablePoints[i];
                //create try moves
                GameTryMove move = new GameTryMove(currentGame);
                move.MakeMoveResult = move.TryGame.Board.InternalMakeMove(p, c);
                if (move.MakeMoveResult == MakeMoveResult.KoBlocked)
                {
                    //ko moves
                    move.MakeKoMove(p, SurviveOrKill.Survive);
                    move.IsRedundantKo = RedundantMoveHelper.RedundantSurvivalKoMove(move);
                    if (move.IsRedundantKo) redundantTryMoves.Add(move);
                    if (KoHelper.KoContentEnabled(c, gameInfo) && (!move.IsRedundantKo || mappingRange))
                        koBlockedMove = move;
                }
                else if (move.MakeMoveResult == MakeMoveResult.Legal)
                {
                    //check if game ended - target group survived
                    if (move.TryGame.Board.MoveGroupLiberties > 1)
                    {
                        ConfirmAliveResult confirmAlive = LifeCheck.CheckIfDeadOrAlive(SurviveOrKill.Survive, move.TryGame);
                        if (confirmAlive == ConfirmAliveResult.Alive)
                            return (ConfirmAliveResult.Alive, new List<GameTryMove>() { move }, null);
                    }
                    //check recursion and return as alive
                    if (GameHelper.CheckForRecursion(move))
                        return (ConfirmAliveResult.Alive, new List<GameTryMove>() { move }, null);
                    //find redundant moves
                    CheckSurvivalRedundantMoves(move);
                    tryMoves.Add(move);
                }
            }

            //remove redundant moves
            if (!mappingRange)
                tryMoves.Where(e => e.IsRedundantMove).ToList().ForEach(t => { redundantTryMoves.Add(t); tryMoves.Remove(t); });

            //sort game try moves
            tryMoves = (from tryMove in tryMoves orderby tryMove.AtariResolved descending, tryMove.TryGame.Board.IsAtariWithoutSuicide descending, tryMove.IncreasedKillerGroups descending, tryMove.TryGame.Board.MoveGroupLiberties descending select tryMove).ToList();

            //check for both alive
            if (BothAliveHelper.EnableCheckForPassMove(currentGame.Board, c, tryMoves))
                tryMoves.Add(BothAliveHelper.AddPassMove(currentGame));

            //restore diagonal eye move
            if (tryMoves.Count == 0 && redundantTryMoves.Any(move => move.IsDiagonalEyeMove))
                tryMoves.Add(redundantTryMoves.First(move => move.IsDiagonalEyeMove));

            //create random move
            CreateRandomMoveForRedundantKo(tryMoves, redundantTryMoves);

            PrintGameMoveList(tryMoves, redundantTryMoves, currentGame);

            return (ConfirmAliveResult.Unknown, tryMoves, koBlockedMove);
        }

        /// <summary>
        /// Check for various redundant moves for survival that can be eliminated to reduce range of possible moves.
        /// </summary>
        private void CheckSurvivalRedundantMoves(GameTryMove move)
        {
            move.IsEye = RedundantMoveHelper.FindPotentialEye(move);
            if (move.IsEye)
                return;
            move.IsCoveredEyeMove = RedundantMoveHelper.RedundantCoveredEyeMove(move);
            if (move.IsCoveredEyeMove)
                return;
            move.IsFillKoEyeMove = RedundantMoveHelper.FillKoEyeMove(move);
            if (move.IsFillKoEyeMove)
                return;
            move.IsSuicidal = RedundantMoveHelper.SuicidalRedundantMove(move);
            if (move.IsSuicidal)
                return;
            move.IsNeutralPoint = RedundantMoveHelper.NeutralPointSurvivalMove(move);
            if (move.IsNeutralPoint)
                return;
            move.IsDiagonalEyeMove = RedundantMoveHelper.SurvivalEyeDiagonalMove(move);
            if (move.IsDiagonalEyeMove)
                return;
            move.IsRedundantKo = RedundantMoveHelper.RedundantSurvivalPreKoMove(move);
            if (move.IsRedundantKo)
                return;
            move.IsRedundantTigerMouth = RedundantMoveHelper.RedundantTigerMouthMove(move);
            if (move.IsRedundantTigerMouth)
                return;
            move.IsRedundantEyeFiller = RedundantMoveHelper.SurvivalEyeFillerMove(move);
            if (move.IsRedundantEyeFiller)
                return;
            move.IsAtariRedundant = RedundantMoveHelper.AtariRedundantMove(move);
            if (move.IsAtariRedundant)
                return;
        }


        /// <summary>
        /// Check for various redundant moves for kill that can be eliminated to reduce range of possible moves.
        /// </summary>
        private void CheckKillRedundantMoves(GameTryMove move)
        {
            move.IsEye = RedundantMoveHelper.FindPotentialEye(move);
            if (move.IsEye)
                return;
            move.IsCoveredEyeMove = RedundantMoveHelper.RedundantCoveredEyeMove(move);
            if (move.IsCoveredEyeMove)
                return;
            move.IsFillKoEyeMove = RedundantMoveHelper.FillKoEyeMove(move);
            if (move.IsFillKoEyeMove)
                return;
            move.IsSuicidal = RedundantMoveHelper.SuicidalRedundantMove(move);
            if (move.IsSuicidal)
                return;
            move.IsNeutralPoint = RedundantMoveHelper.NeutralPointKillMove(move);
            if (move.IsNeutralPoint)
                return;
            move.IsNeutralPoint = RedundantMoveHelper.KillEyeDiagonalMove(move);
            if (move.IsNeutralPoint)
                return;
            move.IsRedundantKo = RedundantMoveHelper.RedundantKillerPreKoMove(move);
            if (move.IsRedundantKo)
                return;
            move.IsRedundantTigerMouth = RedundantMoveHelper.RedundantTigerMouthMove(move);
            if (move.IsRedundantTigerMouth)
                return;
            move.IsRedundantEyeFiller = RedundantMoveHelper.KillEyeFillerMove(move);
            if (move.IsRedundantEyeFiller)
                return;
            move.IsAtariRedundant = RedundantMoveHelper.AtariRedundantMove(move);
            if (move.IsAtariRedundant)
                return;
        }


        /// <summary>
        /// Make all possible survival moves by exhaustive search.
        /// </summary>
        public (ConfirmAliveResult, GameTryMove) MakeSurvivalMove(int depth, Game g = null)
        {
            Game currentGame = g ?? this;
            GameTryMove bestResultMove = null;
            ConfirmAliveResult bestResult = ConfirmAliveResult.Dead;
            //if end of depth reached, then assume target group is dead
            if (depth <= 0)
            {
                this.Root.reachedEndOfDepth++;
                return (ConfirmAliveResult.Dead, bestResultMove);
            }
            //get all survival moves
            (ConfirmAliveResult result, List<GameTryMove> tryMoves, GameTryMove koBlockedMove) = GetSurvivalMoves(g);
            if (result == ConfirmAliveResult.Alive)
                return (result, tryMoves.First());

            //try all possible moves
            for (int i = 0; i <= tryMoves.Count - 1; i++)
            {
                GameTryMove gameTryMove = tryMoves[i];
                Stopwatch watch = null;
                int gameDepth = GameDepth(currentGame);
                if (IsExhaustiveMode(gameDepth))
                {
                    if (gameDepth == 0) Debug.WriteLine(Environment.NewLine);
                    DebugHelper.DebugWriteWithTab("Trying game move at (" + gameTryMove.Move.x + ", " + gameTryMove.Move.y + ") at depth " + depth + " (" + (i + 1) + " out of " + tryMoves.Count + ") | Last moves: " + currentGame.Board.GetLastMoves(), gameDepth);
                    watch = Stopwatch.StartNew();
                }

                //ignore pass move for depth
                int nextDepth = gameTryMove.Move.Equals(Game.PassMove) ? depth : depth - 1;

                //make next opponent move
                (gameTryMove.ConfirmAlive, gameTryMove.OpponentBestMove) = MakeKillMove(nextDepth, gameTryMove.TryGame);

                if (watch != null)
                {
                    watch.Stop();
                    DebugHelper.DebugWriteWithTab("Time taken for (" + gameTryMove.Move.x + ", " + gameTryMove.Move.y + ") at depth " + depth + ": " + watch.ElapsedMilliseconds + " | Result: " + gameTryMove.ConfirmAlive.ToString(), gameDepth);
                }

                //check if game ended
                if (gameTryMove.ConfirmAlive != ConfirmAliveResult.Unknown && ((int)gameTryMove.ConfirmAlive > (int)bestResult))
                {
                    bestResult = gameTryMove.ConfirmAlive;
                    bestResultMove = gameTryMove;
                    if (GameHelper.WinOrLose(SurviveOrKill.Survive, bestResult, currentGame.GameInfo))
                    {
                        if (gameTryMove.Move.Equals(Game.PassMove) && gameTryMove.TryGame.KoGameCheck == KoCheck.None) bestResult = ConfirmAliveResult.BothAlive;
                        return (bestResult, bestResultMove);
                    }
                }
            }

            //check for ko
            if (KoMoveCheck(currentGame, SurviveOrKill.Survive, koBlockedMove, depth))
                return (koBlockedMove.ConfirmAlive, koBlockedMove);

            return (bestResult, bestResultMove);
        }

        /// <summary>
        /// Make ko move and return result as KoAlive if ko move wins.
        /// </summary>
        private Boolean KoMoveCheck(Game currentGame, SurviveOrKill surviveOrKill, GameTryMove koTryMove, int depth)
        {
            if (koTryMove == null) return false;
            Boolean koEnabled = KoHelper.KoSurvivalEnabled(surviveOrKill, currentGame.GameInfo);
            if (!koEnabled) return false;
            Game g = koTryMove.TryGame;
            Point move = koTryMove.Move;
            Stopwatch watch = null;
            int gameDepth = GameDepth(currentGame);
            if (IsExhaustiveMode(gameDepth))
            {
                if (gameDepth == 0) Debug.WriteLine(Environment.NewLine);
                DebugHelper.DebugWriteWithTab("Trying Ko game move at (" + move.x + ", " + move.y + ") at depth " + depth + " | Last moves: " + g.Board.GetLastMoves(), gameDepth);
                watch = Stopwatch.StartNew();
            }

            //make next opponent move
            if (surviveOrKill == SurviveOrKill.Survive)
                (koTryMove.ConfirmAlive, koTryMove.OpponentBestMove) = MakeKillMove(depth, g);
            else if (surviveOrKill == SurviveOrKill.Kill)
                (koTryMove.ConfirmAlive, koTryMove.OpponentBestMove) = MakeSurvivalMove(depth, g);

            if (watch != null)
            {
                watch.Stop();
                DebugHelper.DebugWriteWithTab("Time taken for Ko (" + move.x + ", " + move.y + ") at depth " + depth + ": " + watch.ElapsedMilliseconds + " | Result: " + koTryMove.ConfirmAlive.ToString(), gameDepth);
            }
            if (GameHelper.WinOrLose(surviveOrKill, koTryMove.ConfirmAlive, g.GameInfo))
            {
                koTryMove.ConfirmAlive = ConfirmAliveResult.KoAlive;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Get all possible kill moves. Check if the game has ended with target killed. Check and remove redundant moves. 
        /// For kill only, restore neutral points where necessary and add random move for kill where no move is available.
        /// </summary>
        public (ConfirmAliveResult, List<GameTryMove>, GameTryMove) GetKillMoves(Game g = null)
        {
            Game currentGame = g ?? this;
            GameInfo gameInfo = currentGame.GameInfo;
            Content c = GameHelper.GetContentForSurviveOrKill(gameInfo, SurviveOrKill.Kill);
            List<GameTryMove> tryMoves = new List<GameTryMove>();
            List<GameTryMove> redundantTryMoves = new List<GameTryMove>();
            GameTryMove koBlockedMove = null;
            Boolean mappingRange = MonteCarloMapping.MappingRange(currentGame.Board);

            for (int i = 0; i <= gameInfo.killMovablePoints.Count - 1; i++)
            {
                Point p = gameInfo.killMovablePoints[i];
                //create try moves
                GameTryMove move = new GameTryMove(currentGame);
                move.MakeMoveResult = move.TryGame.Board.InternalMakeMove(p, c);
                if (move.MakeMoveResult == MakeMoveResult.KoBlocked)
                {
                    //ko moves
                    move.MakeKoMove(p, SurviveOrKill.Kill);
                    move.IsRedundantKo = RedundantMoveHelper.RedundantKillerKoMove(move);
                    if (move.IsRedundantKo) redundantTryMoves.Add(move);
                    if (KoHelper.KoContentEnabled(c, gameInfo) && (!move.IsRedundantKo || mappingRange))
                        koBlockedMove = move;
                }
                else if (move.MakeMoveResult == MakeMoveResult.Legal)
                {
                    //check if game ended - target group or survival points killed
                    ConfirmAliveResult confirmAlive = LifeCheck.CheckIfDeadOrAlive(SurviveOrKill.Kill, move.TryGame);
                    if (confirmAlive == ConfirmAliveResult.Dead)
                        return (ConfirmAliveResult.Dead, new List<GameTryMove>() { move }, null);
                    //find redundant moves
                    CheckKillRedundantMoves(move);
                    tryMoves.Add(move);
                }
            }

            if (!mappingRange)
            {
                //remove all redundant moves
                tryMoves.Where(e => e.IsRedundantMove).ToList().ForEach(t => { redundantTryMoves.Add(t); tryMoves.Remove(t); });

                //restore neutral move where necessary
                RedundantMoveHelper.RestoreNeutralMove(currentGame, tryMoves, redundantTryMoves.Where(e => e.IsNeutralPoint).ToList());
            }

            //sort game try moves
            tryMoves = (from tryMove in tryMoves orderby tryMove.AtariResolved descending, tryMove.TryGame.Board.IsAtariWithoutSuicide descending, tryMove.TryGame.Board.MoveGroupLiberties descending select tryMove).ToList();

            //create random move
            CreateRandomMoveForKill(tryMoves, currentGame);
            CreateRandomMoveForRedundantKo(tryMoves, redundantTryMoves);

            PrintGameMoveList(tryMoves, redundantTryMoves, currentGame);

            return (ConfirmAliveResult.Unknown, tryMoves, koBlockedMove);
        }

        /// <summary>
        /// Create random move for redundant ko.
        /// Killer ko within killer group <see cref="UnitTestProject.RedundantKoMoveTest.RedundantKoMoveTest_Scenario_Corner_A79" />
        /// <see cref="UnitTestProject.RedundantKoMoveTest.RedundantKoMoveTest_Scenario_Corner_B39" />
        /// <see cref="UnitTestProject.RedundantKoMoveTest.RedundantKoMoveTest_Scenario_XuanXuanGo_A28_101Weiqi_5" />
        /// Check covered eye <see cref="UnitTestProject.NeutralPointMoveTest.NeutralPointMoveTest_Scenario_XuanXuanGo_A28_101Weiqi_6" />
        /// Check atari resolved <see cref="UnitTestProject.RedundantKoMoveTest.RedundantKoMoveTest_Scenario_TianLongTu_Q17078_2" />
        /// Check base line leap link <see cref="UnitTestProject.RedundantKoMoveTest.RedundantKoMoveTest_Scenario_TianLongTu_Q17078_3" />
        /// </summary>
        private void CreateRandomMoveForRedundantKo(List<GameTryMove> tryMoves, List<GameTryMove> redundantTryMoves)
        {
            if (tryMoves.Count > 1 || tryMoves.Any(t => !EyeHelper.FindCoveredEye(t.CurrentGame.Board, t.Move, t.MoveContent))) return;
            foreach (GameTryMove koMove in redundantTryMoves.Where(t => t.IsRedundantKo))
            {
                Board currentBoard = koMove.CurrentGame.Board;
                Board tryBoard = koMove.TryGame.Board;
                Content c = tryBoard.MoveGroup.Content;
                if (KoHelper.IsNonKillableGroupKoFight(tryBoard, tryBoard.MoveGroup))
                    continue;
                if (tryBoard.AtariTargets.Any(t => GroupHelper.GetKillerGroupFromCache(tryBoard, t.Points.First(), c) != null))
                {
                    GameTryMove move = GetRandomMove(koMove.CurrentGame);
                    if (move != null) tryMoves.Add(move);
                    break;
                }
            }
        }


        /// <summary>
        /// Create random move if no more try moves for kill.
        /// <see cref="UnitTestProject.KoTest.KoTest_Scenario_WuQingYuan_Q31498" />
        /// <see cref="UnitTestProject.KoTest.KoTest_Scenario_TianLongTu_Q17077" />
        /// </summary>
        private void CreateRandomMoveForKill(List<GameTryMove> tryMoves, Game currentGame)
        {
            Board board = currentGame.Board;
            //do not add move if last move is random or pass move
            Point? lastMove = board.LastMove;
            if (lastMove != null && (board.IsRandomMove || lastMove.Value.Equals(Game.PassMove))) return;

            //add move if no more try moves
            if (tryMoves.Count == 0)
            {
                GameTryMove move = GetRandomMove(currentGame);
                if (move != null) tryMoves.Add(move);
            }
        }

        private GameTryMove GetRandomMove(Game currentGame)
        {
            Board board = currentGame.Board;
            Point p = Game.PassMove;
            for (int i = 3; i < 11; i++)
            {
                for (int j = 3; j < 8; j++)
                {
                    if (board[i, j] == Content.Empty)
                    {
                        p = new Point(i, j);
                        break;
                    }
                }
                if (!p.Equals(Game.PassMove)) break;
            }
            if (p.Equals(Game.PassMove))
                return null;
            GameTryMove move = new GameTryMove(currentGame);
            move.MakeMoveResult = move.TryGame.InternalMakeMove(p.x, p.y);
            move.TryGame.Board.IsRandomMove = true;
            return move;
        }

        /// <summary>
        /// Make all possible kill moves by exhaustive search.
        /// </summary>
        private (ConfirmAliveResult, GameTryMove) MakeKillMove(int depth, Game g = null)
        {
            Game currentGame = g ?? this;
            GameTryMove bestResultMove = null;
            ConfirmAliveResult bestResult = ConfirmAliveResult.Alive;

            //get all kill moves
            (ConfirmAliveResult result, List<GameTryMove> tryMoves, GameTryMove koBlockedMove) = GetKillMoves(g);
            if (result == ConfirmAliveResult.Dead)
                return (result, tryMoves.First());

            if (Game.TimeOut(currentGame))
            {
                if (debugMode) Debug.WriteLine("Break real time...");
                return (ConfirmAliveResult.Unknown, bestResultMove);
            }
            //try all possible moves
            for (int i = 0; i <= tryMoves.Count - 1; i++)
            {
                GameTryMove gameTryMove = tryMoves[i];
                Stopwatch watch = null;
                int gameDepth = GameDepth(currentGame);
                if (IsExhaustiveMode(gameDepth))
                {
                    if (gameDepth == 0) Debug.WriteLine(Environment.NewLine);
                    DebugHelper.DebugWriteWithTab("Trying game move at (" + gameTryMove.Move.x + ", " + gameTryMove.Move.y + ") at depth " + depth + " (" + (i + 1) + " out of " + tryMoves.Count + ") | Last moves: " + currentGame.Board.GetLastMoves(), gameDepth);
                    watch = Stopwatch.StartNew();
                }

                //make next opponent move
                (gameTryMove.ConfirmAlive, gameTryMove.OpponentBestMove) = MakeSurvivalMove(depth - 1, gameTryMove.TryGame);

                if (watch != null)
                {
                    watch.Stop();
                    DebugHelper.DebugWriteWithTab("Time taken for (" + gameTryMove.Move.x + ", " + gameTryMove.Move.y + ") at depth " + depth + ": " + watch.ElapsedMilliseconds + " | Result: " + gameTryMove.ConfirmAlive.ToString(), gameDepth);
                }

                //check if game ended
                if (gameTryMove.ConfirmAlive != ConfirmAliveResult.Unknown && ((int)gameTryMove.ConfirmAlive < (int)bestResult))
                {
                    bestResult = gameTryMove.ConfirmAlive;
                    bestResultMove = gameTryMove;
                    if (GameHelper.WinOrLose(SurviveOrKill.Kill, bestResult, currentGame.GameInfo))
                        return (bestResult, bestResultMove);
                }
            }

            //check for ko
            if (KoMoveCheck(currentGame, SurviveOrKill.Kill, koBlockedMove, depth))
                return (koBlockedMove.ConfirmAlive, koBlockedMove);

            return (bestResult, bestResultMove);
        }

    }
}
