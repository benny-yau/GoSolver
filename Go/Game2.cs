using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

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

            if (GameHelper.KillOrSurvivalForNextMove(this.Board) == SurviveOrKill.Kill)
                (confirmAlive, bestResultMove) = this.MakeKillMove(depth);
            else
                (confirmAlive, bestResultMove) = this.MakeSurvivalMove(depth);

            if (debugMode)
            {
                this.RunTimeStopWatch.Stop();
                Debug.WriteLine("Time taken (exhaustive): " + this.RunTimeStopWatch.ElapsedMilliseconds);
            }
            return confirmAlive;
        }

        /// <summary>
        /// Get all possible survival moves. Check if the game has ended with any of the possible moves.
        /// Reorder try moves by priority. Check and remove redundant moves. 
        /// For survive only, add pass move to check for both alive where necessary.
        /// </summary>
        public (ConfirmAliveResult, List<GameTryMove>, GameTryMove) GetSurvivalMoves(Game g = null, Boolean getAll = false)
        {
            Game currentGame = g ?? this;
            GameInfo gameInfo = currentGame.GameInfo;
            Content content = GameHelper.GetContentForSurviveOrKill(currentGame.GameInfo, SurviveOrKill.Survive);
            List<GameTryMove> tryMoves = new List<GameTryMove>();
            GameTryMove koBlockedMove = null;
            Boolean mappingRange = MonteCarloMapping.MappingRange(currentGame.Board);

            for (int i = 0; i <= gameInfo.movablePoints.Count - 1; i++)
            {
                Point p = gameInfo.movablePoints[i];
                //create try moves
                GameTryMove move = new GameTryMove(currentGame);
                move.MakeMoveResult = move.TryGame.Board.InternalMakeMove(p, content);
                if (move.MakeMoveResult == MakeMoveResult.KoBlocked)
                {
                    //ko moves
                    Boolean koEnabled = KoHelper.KoSurvivalEnabled(SurviveOrKill.Survive, gameInfo);
                    if (!koEnabled) continue;
                    move.MakeKoMove(p, SurviveOrKill.Survive);
                    move.IsRedundantKo = RedundantMoveHelper.RedundantSurvivalKoMove(move);
                    if (mappingRange || (!move.IsRedundantKo))
                        koBlockedMove = move;
                }
                else if (move.MakeMoveResult == MakeMoveResult.Legal)
                {
                    //check if game ended - target group survived
                    if (move.TryGame.Board.MoveGroupLiberties > 1)
                    {
                        ConfirmAliveResult confirmAlive = LifeCheck.CheckIfDeadOrAlive(SurviveOrKill.Survive, move.TryGame, true);
                        if (!getAll && confirmAlive == ConfirmAliveResult.Alive)
                            return (ConfirmAliveResult.Alive, new List<GameTryMove>() { move }, null);
                    }
                    //check recursion and return as alive
                    if (CheckForRecursion(move))
                        return (ConfirmAliveResult.Alive, new List<GameTryMove>() { move }, null);
                    //process try moves
                    move.ProcessGameTryMoves();
                    //find redundant moves
                    CheckSurvivalRedundantMoves(move);
                    tryMoves.Add(move);
                }
            }
            List<GameTryMove> redundantTryMoves = null;
            if (!mappingRange)
            {
                //filter game try moves with runtime script
                if (gameInfo.RuntimeScript_SurvivalMove != null)
                    tryMoves = gameInfo.RuntimeScript_SurvivalMove.ScriptReducedList(tryMoves, currentGame, g);

                //remove redundant moves
                redundantTryMoves = tryMoves.Where(e => e.IsRedundantMove).ToList();
                redundantTryMoves.ForEach(e => tryMoves.Remove(e));
            }

            //sort game try moves
            tryMoves = (from entry in tryMoves orderby entry.TryGame.Board.AtariResolved descending, entry.IncreasedKillerGroups descending, entry.TryGame.Board.MoveGroupLiberties descending select entry).ToList();

            //check for bent four and both alive scenarios
            if (UniquePatternsHelper.CheckForBentFour(currentGame, tryMoves))
                tryMoves.Clear();
            else if (BothAliveHelper.EnableCheckForPassMove(currentGame.Board, tryMoves))
                tryMoves.Add(BothAliveHelper.AddPassMove(currentGame));
            else if (tryMoves.Count == 0 && redundantTryMoves.Any(move => move.IsDiagonalEyeMove))
                tryMoves.Add(redundantTryMoves.First(move => move.IsDiagonalEyeMove));

            //restore redundant ko
            RestoreRedundantKo(tryMoves, redundantTryMoves, koBlockedMove);
            if (!mappingRange && koBlockedMove != null && koBlockedMove.IsRedundantKo)
                koBlockedMove = null;

            //create random move
            CreateRandomMove(tryMoves, currentGame, SurviveOrKill.Survive);

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
            move.IsCoveredEyeMove = RedundantMoveHelper.FindCoveredEyeMove(move);
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
            move.IsBaseLine = RedundantMoveHelper.BaseLineSurvivalMove(move);
            if (move.IsBaseLine)
                return;
            move.IsRedundantTigerMouth = RedundantMoveHelper.RedundantTigerMouthMove(move);
            if (move.IsRedundantTigerMouth)
                return;
            move.IsRedundantEyeFiller = RedundantMoveHelper.SurvivalEyeFillerMove(move);
            if (move.IsRedundantEyeFiller)
                return;
            move.IsLeapMove = RedundantMoveHelper.SurvivalLeapMove(move);
            if (move.IsLeapMove)
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
            move.IsBaseLine = RedundantMoveHelper.BaseLineSurvivalMove(move);
            if (move.IsBaseLine)
                return;
            move.IsRedundantTigerMouth = RedundantMoveHelper.RedundantTigerMouthMove(move);
            if (move.IsRedundantTigerMouth)
                return;
            move.IsRedundantEyeFiller = RedundantMoveHelper.KillEyeFillerMove(move);
            if (move.IsRedundantEyeFiller)
                return;
            move.IsLeapMove = RedundantMoveHelper.KillLeapMove(move);
            if (move.IsLeapMove)
                return;
            move.IsAtariRedundant = RedundantMoveHelper.AtariRedundantMove(move);
            if (move.IsAtariRedundant)
                return;
        }


        /// <summary>
        /// Make all possible survival moves by exhaustive search.
        /// </summary>
        public (ConfirmAliveResult, GameTryMove) MakeSurvivalMove(int depth, Game m = null)
        {
            Game currentGame = m ?? this;
            GameTryMove bestResultMove = null;
            ConfirmAliveResult bestResult = ConfirmAliveResult.Dead;
            Boolean survivalWin = false;
            Boolean koEnabled = KoHelper.KoSurvivalEnabled(SurviveOrKill.Survive, currentGame.GameInfo);
            //if end of depth reached, then assume target group is dead
            if (depth <= 0)
            {
                this.Root.reachedEndOfDepth++;
                return (ConfirmAliveResult.Dead, bestResultMove);
            }
            //get all survival moves
            (ConfirmAliveResult result, List<GameTryMove> tryMoves, GameTryMove koBlockedMove) = GetSurvivalMoves(m);
            if (result == ConfirmAliveResult.Alive)
            {
                bestResultMove = tryMoves.First();
                if (m == null)
                    this.MakeMove(bestResultMove.Move);
                return (result, bestResultMove);
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
                    if (bestResult == ConfirmAliveResult.Alive || bestResult == ConfirmAliveResult.BothAlive || (koEnabled && bestResult == ConfirmAliveResult.KoAlive))
                    {
                        if (gameTryMove.Move.Equals(Game.PassMove) && gameTryMove.TryGame.KoGameCheck == KoCheck.None) bestResult = ConfirmAliveResult.BothAlive;
                        survivalWin = true;
                        break;
                    }
                }
            }

            //check for ko
            if (!survivalWin && koBlockedMove != null && koEnabled)
            {
                GameTryMove koMove = KoMoveCheck(currentGame, SurviveOrKill.Survive, koBlockedMove, depth);
                if (koMove != null)
                {
                    survivalWin = true;
                    bestResult = koMove.ConfirmAlive;
                    bestResultMove = koMove;
                }
            }

            //make the move at initial board
            if (m == null && survivalWin)
                this.MakeMove(bestResultMove.Move);

            return (bestResult, bestResultMove);
        }

        /// <summary>
        /// Makes ko move and returns result as KoAlive if ko move wins.
        /// </summary>
        private GameTryMove KoMoveCheck(Game currentGame, SurviveOrKill surviveOrKill, GameTryMove koBlockedMove, int depth)
        {
            Stopwatch watch = null;
            int gameDepth = GameDepth(currentGame);
            if (IsExhaustiveMode(gameDepth))
            {
                if (gameDepth == 0) Debug.WriteLine(Environment.NewLine);
                DebugHelper.DebugWriteWithTab("Trying Ko game move at (" + koBlockedMove.Move.x + ", " + koBlockedMove.Move.y + ") at depth " + depth + " | Last moves: " + koBlockedMove.TryGame.Board.GetLastMoves(), gameDepth);
                watch = Stopwatch.StartNew();
            }

            ConfirmAliveResult result = ConfirmAliveResult.Unknown;
            if (surviveOrKill == SurviveOrKill.Survive)
            {
                (koBlockedMove.ConfirmAlive, koBlockedMove.OpponentBestMove) = MakeKillMove(depth, koBlockedMove.TryGame); //make next opponent move
                result = koBlockedMove.ConfirmAlive;
                if (result.HasFlag(ConfirmAliveResult.Alive) || result.HasFlag(ConfirmAliveResult.BothAlive))
                    result = ConfirmAliveResult.KoAlive;
            }
            else if (surviveOrKill == SurviveOrKill.Kill)
            {
                (koBlockedMove.ConfirmAlive, koBlockedMove.OpponentBestMove) = MakeSurvivalMove(depth, koBlockedMove.TryGame); //make next opponent move
                result = koBlockedMove.ConfirmAlive;
                if (result.HasFlag(ConfirmAliveResult.Dead))
                    result = ConfirmAliveResult.KoAlive;
            }

            if (watch != null)
            {
                watch.Stop();
                DebugHelper.DebugWriteWithTab("Time taken for Ko (" + koBlockedMove.Move.x + ", " + koBlockedMove.Move.y + ") at depth " + depth + ": " + watch.ElapsedMilliseconds + " | Result: " + koBlockedMove.ConfirmAlive.ToString(), gameDepth);
            }

            if (result.HasFlag(ConfirmAliveResult.KoAlive))
            {
                koBlockedMove.ConfirmAlive = result;
                return koBlockedMove;
            }
            return null;
        }

        /// <summary>
        /// Get all possible kill moves. Check if the game has ended with any of the possible moves.
        /// Reorder try moves by priority. Check and remove redundant moves. 
        /// For kill only, replace neutral points where necessary. Add random move for kill where no move is available.
        /// </summary>
        public (ConfirmAliveResult, List<GameTryMove>, GameTryMove) GetKillMoves(Game g = null, Boolean getAll = false)
        {
            Game currentGame = g ?? this;
            GameInfo gameInfo = currentGame.GameInfo;
            Content content = GameHelper.GetContentForSurviveOrKill(gameInfo, SurviveOrKill.Kill);

            Boolean mappingRange = MonteCarloMapping.MappingRange(currentGame.Board);
            List<GameTryMove> tryMoves = new List<GameTryMove>();
            GameTryMove koBlockedMove = null;

            for (int i = 0; i <= gameInfo.killMovablePoints.Count - 1; i++)
            {
                Point p = gameInfo.killMovablePoints[i];
                //create try moves
                GameTryMove move = new GameTryMove(currentGame);
                move.MakeMoveResult = move.TryGame.Board.InternalMakeMove(p, content);
                if (move.MakeMoveResult == MakeMoveResult.KoBlocked)
                {
                    //ko moves
                    Boolean koEnabled = KoHelper.KoSurvivalEnabled(SurviveOrKill.Kill, gameInfo);
                    if (!koEnabled) continue;
                    move.MakeKoMove(p, SurviveOrKill.Kill);
                    move.IsRedundantKo = RedundantMoveHelper.RedundantKillerKoMove(move);
                    if (mappingRange || (!move.IsRedundantKo))
                        koBlockedMove = move;
                }
                else if (move.MakeMoveResult == MakeMoveResult.Legal)
                {
                    //check if game ended - target group or survival points killed
                    ConfirmAliveResult confirmAlive = LifeCheck.CheckIfDeadOrAlive(SurviveOrKill.Kill, move.TryGame, true);
                    if (!getAll && confirmAlive == ConfirmAliveResult.Dead)
                        return (ConfirmAliveResult.Dead, new List<GameTryMove>() { move }, null);
                    //process try moves
                    move.ProcessGameTryMoves();
                    //find redundant moves
                    CheckKillRedundantMoves(move);
                    tryMoves.Add(move);
                }
            }

            List<GameTryMove> redundantTryMoves = null;
            if (!mappingRange)
            {
                //filter game try moves with runtime script
                if (gameInfo.RuntimeScript_KillMove != null)
                    tryMoves = gameInfo.RuntimeScript_KillMove.ScriptReducedList(tryMoves, currentGame, g);

                //remove all redundant moves
                List<GameTryMove> neutralPointMoves = tryMoves.Where(e => e.IsNeutralPoint).ToList();
                redundantTryMoves = tryMoves.Where(e => e.IsRedundantMove).ToList();
                redundantTryMoves.ForEach(e => tryMoves.Remove(e));

                //restore neutral move where necessary
                RedundantMoveHelper.RestoreNeutralMove(currentGame, tryMoves, neutralPointMoves);
            }

            //sort game try moves
            tryMoves = (from entry in tryMoves orderby entry.TryGame.Board.AtariResolved descending, entry.TryGame.Board.MoveGroupLiberties descending select entry).ToList();

            //restore redundant ko
            RestoreRedundantKo(tryMoves, redundantTryMoves, koBlockedMove);

            //check for ten thousand year ko scenario
            if (UniquePatternsHelper.CheckForTenThousandYearKo(currentGame))
                tryMoves.Clear();
            else //create random move
                CreateRandomMove(tryMoves, currentGame, SurviveOrKill.Kill);

            PrintGameMoveList(tryMoves, redundantTryMoves, currentGame);

            return (ConfirmAliveResult.Unknown, tryMoves, koBlockedMove);
        }

        /// <summary>
        /// Restore redundant ko that are atari moves but not ko enabled.
        /// Double ko <see cref="UnitTestProject.CheckForRecursionTest.CheckForRecursionTest_Scenario_XuanXuanGo_A28_101Weiqi" />
        /// End ko <see cref="UnitTestProject.KoTest.KoTest_Scenario_TianLongTu_Q17077" />
        /// </summary>
        private void RestoreRedundantKo(List<GameTryMove> tryMoves, List<GameTryMove> redundantTryMoves, GameTryMove koBlockedMove)
        {
            if ((redundantTryMoves != null && redundantTryMoves.Any(t => t.IsRedundantKo)))
            {
                //all try moves are suicidal
                if (AllTryMovesSuicidal(tryMoves))
                {
                    GameTryMove koMove = redundantTryMoves.FirstOrDefault(t => t.IsRedundantKo && t.TryGame.Board.IsAtariMove);
                    if (koMove != null)
                        tryMoves.Add(koMove);
                }
            }
        }

        /// <summary>
        /// All remaining try moves are suicidal or opponent immovable.
        /// <see cref="UnitTestProject.RedundantKoMoveTest.RedundantKoMoveTest_Scenario_Nie20_2" />
        /// </summary>
        private Boolean AllTryMovesSuicidal(List<GameTryMove> tryMoves)
        {
            return tryMoves.All(tryMove => ((tryMove.TryGame.Board.MoveGroupLiberties == 1 && tryMove.TryGame.Board.CapturedList.Count == 0) || ImmovableHelper.SinglePointOpponentImmovable(tryMove)));
        }

        /// <summary>
        /// Make random move to wait a turn where no other move is available or on ko move from opponent.
        /// Double ko recursion for survive <see cref="UnitTestProject.CheckForRecursionTest.CheckForRecursionTest_Scenario_Corner_B41" />
        /// Last move is pass move <see cref="UnitTestProject.MonteCarloRuntimeTest.MonteCarloRuntimeTest_Scenario_Corner_A68" />
        /// </summary>
        private void CreateRandomMove(List<GameTryMove> tryMoves, Game currentGame, SurviveOrKill surviveOrKill)
        {
            Board board = currentGame.Board;
            if (surviveOrKill == SurviveOrKill.Kill)
            {
                //do not add move if last move is pass move
                Point? lastMove = board.LastMove;
                Boolean lastMovePass = lastMove == null || lastMove.Value.Equals(Game.PassMove);
                if (lastMovePass) return;
                //add move if no more try moves or to fight ko
                if (tryMoves.Count == 0 || AddPointToFightKo(tryMoves, currentGame, KoCheck.Survive))
                {
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
                        return;
                    GameTryMove move = new GameTryMove(currentGame);
                    move.MakeMoveResult = move.TryGame.InternalMakeMove(p.x, p.y);
                    tryMoves.Add(move);
                }
            }
            else if (surviveOrKill == SurviveOrKill.Survive)
            {
                if (AddPointToFightKo(tryMoves, currentGame, KoCheck.Kill))
                    tryMoves.Add(BothAliveHelper.AddPassMove(currentGame));
            }
        }

        /// <summary>
        /// Add random move to fight ko.
        /// <see cref="UnitTestProject.KoTest.KoTest_Scenario_TianLongTu_Q17077" />
        /// </summary>
        private Boolean AddPointToFightKo(List<GameTryMove> tryMoves, Game currentGame, KoCheck surviveOrKill)
        {
            if (currentGame.KoGameCheck == surviveOrKill && currentGame.Board.singlePointCapture != null)
            {
                //all try moves are suicidal
                if (AllTryMovesSuicidal(tryMoves))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Make all possible kill moves by exhaustive search.
        /// </summary>
        private (ConfirmAliveResult, GameTryMove) MakeKillMove(int depth, Game m = null)
        {
            Game currentGame = m ?? this;
            GameTryMove bestResultMove = null;
            ConfirmAliveResult bestResult = ConfirmAliveResult.Alive;
            Boolean killWin = false;
            Boolean koEnabled = KoHelper.KoSurvivalEnabled(SurviveOrKill.Kill, currentGame.GameInfo);
            //get all kill moves
            (ConfirmAliveResult result, List<GameTryMove> tryMoves, GameTryMove koBlockedMove) = GetKillMoves(m);
            if (result == ConfirmAliveResult.Dead)
            {
                bestResultMove = tryMoves.First();
                if (m == null)
                    this.MakeMove(bestResultMove.Move);
                return (result, bestResultMove);
            }

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
                    if (bestResult == ConfirmAliveResult.Dead || (koEnabled && gameTryMove.ConfirmAlive == ConfirmAliveResult.KoAlive))
                    {
                        killWin = true;
                        break;
                    }
                }
            }

            //check for ko
            if (!killWin && koBlockedMove != null && koEnabled)
            {
                GameTryMove koMove = KoMoveCheck(currentGame, SurviveOrKill.Kill, koBlockedMove, depth);
                if (koMove != null)
                {
                    killWin = true;
                    bestResult = koMove.ConfirmAlive;
                    bestResultMove = koMove;
                }
            }

            //make the move at initial board
            if (m == null && killWin)
                this.MakeMove(bestResultMove.Move);

            return (bestResult, bestResultMove);
        }

        /// <summary>
        /// Check for recursion or superkos that are 4 spaces to 12 spaces apart.
        /// https://senseis.xmp.net/?LongCycleRule
        /// <see cref="UnitTestProject.CheckForRecursionTest.CheckForRecursionTest_Scenario_TianLongTu_Q16446" />
        /// </summary>
        public static Boolean CheckForRecursion(GameTryMove tryMove)
        {
            Game tryGame = tryMove.TryGame;
            for (int j = 4; j <= 12; j++)
            {
                List<Point> lastMoves = tryGame.Board.LastMoves;
                int lastMoveCount = lastMoves.Count - 1;
                //find recurrence of last three moves
                Boolean recur = (lastMoveCount >= j + 2 && tryMove.Move.Equals(lastMoves[lastMoveCount - j]) && lastMoves[lastMoveCount - 1].Equals(lastMoves[lastMoveCount - (j + 1)]) && lastMoves[lastMoveCount - 2].Equals(lastMoves[lastMoveCount - (j + 2)]));

                if (recur)
                {
                    //get snapshot of board from last moves and compare if board is the same
                    int compareLastMoves = tryGame.Board.LastMoves.Count - j;
                    Board compareBoard = GameHelper.GetSnapshotBoard(tryGame, compareLastMoves);
                    if (tryGame.Board.Equals(compareBoard))
                        return true;
                }
            }
            return false;
        }

    }
}
