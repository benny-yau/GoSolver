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

                    DebugHelper.DebugWriteWithTab("Final move: " + this.Board.Move + " | Final result: " + confirmAlive.ToString() + " | Reached end of depth: " + (this.Root.reachedEndOfDepth > 0).ToString());
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
                this.MakeMove(bestResultMove.TryGame.Board);

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
                if (currentGame.Board[p] != Content.Empty) continue;
                //create try moves
                GameTryMove tryMove = new GameTryMove(currentGame);
                tryMove.MakeMoveResult = tryMove.TryGame.Board.InternalMakeMove(p, c);
                if (tryMove.MakeMoveResult == MakeMoveResult.KoBlocked)
                {
                    //ko moves
                    tryMove.MakeKoMove(p, SurviveOrKill.Survive);
                    tryMove.IsRedundantKo = RedundantMoveHelper.RedundantSurvivalKoMove(tryMove);
                    if (tryMove.IsRedundantKo) redundantTryMoves.Add(tryMove);
                    if (KoHelper.KoContentEnabled(c, gameInfo) && (!tryMove.IsRedundantKo || mappingRange))
                        koBlockedMove = tryMove;
                }
                else if (tryMove.MakeMoveResult == MakeMoveResult.Legal)
                {
                    //check if game ended - target group survived
                    if (tryMove.MoveGroupLiberties > 1)
                    {
                        ConfirmAliveResult confirmAlive = LifeCheck.CheckIfDeadOrAlive(SurviveOrKill.Survive, tryMove.TryGame.Board);
                        if (confirmAlive == ConfirmAliveResult.Alive)
                            return (ConfirmAliveResult.Alive, new List<GameTryMove>() { tryMove }, null);
                    }
                    //check recursion and return as alive
                    if (GameHelper.CheckForRecursion(tryMove))
                        return (ConfirmAliveResult.Alive, new List<GameTryMove>() { tryMove }, null);
                    //find redundant moves
                    CheckSurvivalRedundantMoves(tryMove);
                    tryMoves.Add(tryMove);
                }
            }

            //remove redundant moves
            if (!mappingRange)
                tryMoves.Where(e => e.IsRedundantMove).ToList().ForEach(t => { redundantTryMoves.Add(t); tryMoves.Remove(t); });

            //sort game try moves
            tryMoves = (from tryMove in tryMoves orderby tryMove.AtariResolved descending, tryMove.AtariWithoutSuicide descending, tryMove.Captured descending, tryMove.IncreasedKillerGroups descending, tryMove.TryGame.Board.MoveGroupLiberties descending select tryMove).ToList();

            //restore diagonal eye move
            if (tryMoves.Count == 0 && redundantTryMoves.Any(move => move.IsDiagonalEyeMove))
                tryMoves.Add(redundantTryMoves.First(move => move.IsDiagonalEyeMove));

            //check for both alive
            BothAliveHelper.EnablePassMoveForBothAlive(currentGame, tryMoves, SurviveOrKill.Survive);

            //create random move
            CreateRandomMoveForCoveredEyeSurvival(tryMoves, currentGame);
            CreateRandomMoveForRedundantKo(currentGame, tryMoves, redundantTryMoves);

            PrintGameMoveList(tryMoves, redundantTryMoves, currentGame);

            return (ConfirmAliveResult.Unknown, tryMoves, koBlockedMove);
        }

        /// <summary>
        /// Check for various redundant moves for survival that can be eliminated to reduce range of possible moves.
        /// </summary>
        private void CheckSurvivalRedundantMoves(GameTryMove tryMove)
        {
            tryMove.IsEye = RedundantMoveHelper.FindPotentialEye(tryMove);
            if (tryMove.IsEye)
                return;
            tryMove.IsCoveredEyeMove = RedundantMoveHelper.RedundantCoveredEyeMove(tryMove);
            if (tryMove.IsCoveredEyeMove)
                return;
            tryMove.IsFillKoEyeMove = RedundantMoveHelper.FillKoEyeMove(tryMove);
            if (tryMove.IsFillKoEyeMove)
                return;
            tryMove.IsSuicidal = RedundantMoveHelper.SuicidalRedundantMove(tryMove);
            if (tryMove.IsSuicidal)
                return;
            tryMove.IsNeutralPoint = RedundantMoveHelper.NeutralPointSurvivalMove(tryMove);
            if (tryMove.IsNeutralPoint)
                return;
            tryMove.IsDiagonalEyeMove = RedundantMoveHelper.SurvivalEyeDiagonalMove(tryMove);
            if (tryMove.IsDiagonalEyeMove)
                return;
            tryMove.IsRedundantKo = RedundantMoveHelper.RedundantSurvivalPreKoMove(tryMove);
            if (tryMove.IsRedundantKo)
                return;
            tryMove.IsRedundantTigerMouth = RedundantMoveHelper.RedundantTigerMouthMove(tryMove);
            if (tryMove.IsRedundantTigerMouth)
                return;
            tryMove.IsAtariRedundant = RedundantMoveHelper.AtariRedundantMove(tryMove);
            if (tryMove.IsAtariRedundant)
                return;
        }


        /// <summary>
        /// Check for various redundant moves for kill that can be eliminated to reduce range of possible moves.
        /// </summary>
        private void CheckKillRedundantMoves(GameTryMove tryMove)
        {
            tryMove.IsEye = RedundantMoveHelper.FindPotentialEye(tryMove);
            if (tryMove.IsEye)
                return;
            tryMove.IsCoveredEyeMove = RedundantMoveHelper.RedundantCoveredEyeMove(tryMove);
            if (tryMove.IsCoveredEyeMove)
                return;
            tryMove.IsFillKoEyeMove = RedundantMoveHelper.FillKoEyeMove(tryMove);
            if (tryMove.IsFillKoEyeMove)
                return;
            tryMove.IsSuicidal = RedundantMoveHelper.SuicidalRedundantMove(tryMove);
            if (tryMove.IsSuicidal)
                return;
            tryMove.IsNeutralPoint = RedundantMoveHelper.NeutralPointKillMove(tryMove);
            if (tryMove.IsNeutralPoint)
                return;
            tryMove.IsNeutralPoint = RedundantMoveHelper.KillEyeDiagonalMove(tryMove);
            if (tryMove.IsNeutralPoint)
                return;
            tryMove.IsRedundantKo = RedundantMoveHelper.RedundantKillerPreKoMove(tryMove);
            if (tryMove.IsRedundantKo)
                return;
            tryMove.IsRedundantTigerMouth = RedundantMoveHelper.RedundantTigerMouthMove(tryMove);
            if (tryMove.IsRedundantTigerMouth)
                return;
            tryMove.IsAtariRedundant = RedundantMoveHelper.AtariRedundantMove(tryMove);
            if (tryMove.IsAtariRedundant)
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
                if (DebugPrintMode(gameDepth))
                {
                    if (gameDepth == 0) Debug.WriteLine(Environment.NewLine);
                    DebugHelper.DebugWriteWithTab("Trying game move at (" + gameTryMove.Move.x + ", " + gameTryMove.Move.y + ") at depth " + depth + " (" + (i + 1) + " out of " + tryMoves.Count + ") | Last moves: " + currentGame.Board.GetLastMoves(), gameDepth);
                    watch = Stopwatch.StartNew();
                }

                //make next opponent move
                (gameTryMove.ConfirmAlive, gameTryMove.OpponentBestMove) = MakeKillMove(depth - 1, gameTryMove.TryGame);

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
                        if (gameTryMove.Move.Equals(Game.PassMove) && gameTryMove.TryGame.Board.KoGameCheck == KoCheck.None) bestResult = ConfirmAliveResult.BothAlive;
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
            if (DebugPrintMode(gameDepth))
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
                if (currentGame.Board[p] != Content.Empty) continue;
                //create try moves
                GameTryMove tryMove = new GameTryMove(currentGame);
                tryMove.MakeMoveResult = tryMove.TryGame.Board.InternalMakeMove(p, c);
                if (tryMove.MakeMoveResult == MakeMoveResult.KoBlocked)
                {
                    //ko moves
                    tryMove.MakeKoMove(p, SurviveOrKill.Kill);
                    tryMove.IsRedundantKo = RedundantMoveHelper.RedundantKillerKoMove(tryMove);
                    if (tryMove.IsRedundantKo) redundantTryMoves.Add(tryMove);
                    if (KoHelper.KoContentEnabled(c, gameInfo) && (!tryMove.IsRedundantKo || mappingRange))
                        koBlockedMove = tryMove;
                }
                else if (tryMove.MakeMoveResult == MakeMoveResult.Legal)
                {
                    //check if game ended - target group or survival points killed
                    ConfirmAliveResult confirmAlive = LifeCheck.CheckIfDeadOrAlive(SurviveOrKill.Kill, tryMove.TryGame.Board);
                    if (confirmAlive == ConfirmAliveResult.Dead)
                        return (ConfirmAliveResult.Dead, new List<GameTryMove>() { tryMove }, null);
                    //find redundant moves
                    CheckKillRedundantMoves(tryMove);
                    tryMoves.Add(tryMove);
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
            tryMoves = (from tryMove in tryMoves orderby tryMove.AtariResolved descending, tryMove.AtariWithoutSuicide descending, tryMove.Captured descending, tryMove.TryGame.Board.MoveGroupLiberties descending select tryMove).ToList();

            //create random move
            CreateRandomMoveForKill(tryMoves, currentGame);
            CreateRandomMoveForRedundantKo(currentGame, tryMoves, redundantTryMoves);

            //check for both alive
            BothAliveHelper.EnablePassMoveForBothAlive(currentGame, tryMoves, SurviveOrKill.Kill);
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
        private void CreateRandomMoveForRedundantKo(Game currentGame, List<GameTryMove> tryMoves, List<GameTryMove> redundantTryMoves)
        {
            Board currentBoard = currentGame.Board;
            if (currentBoard.LastMove != null && currentBoard.LastMove.Value.Equals(Game.PassMove)) return;
            if (tryMoves.Count == 1 && tryMoves.Select(n => n.TryGame.Board).Any(b => b.IsRandomMove || b.Move.Equals(Game.PassMove))) return;

            foreach (GameTryMove koMove in redundantTryMoves.Where(t => t.IsRedundantKo))
            {
                Board tryBoard = koMove.TryGame.Board;
                Content c = tryBoard.MoveGroup.Content;
                if (koMove.AtariResolved) continue;
                if (KoHelper.IsNonKillableGroupKoFight(tryBoard, tryBoard.MoveGroup))
                    continue;
                //killer ko within killer group 
                if (tryBoard.AtariTargets.Any(t => GroupHelper.GetKillerGroupFromCache(tryBoard, t.Points.First(), c) != null && !ImmovableHelper.CheckConnectAndDie(currentBoard, currentBoard.GetGroupAt(t.Points.First()), false)))
                {
                    GameTryMove tryMove = GetRandomMove(currentGame);
                    if (tryMove != null) tryMoves.Add(tryMove);
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
            if (tryMoves.Count > 0)
            {
                Boolean suicidal = tryMoves.Count == 1 && KillerFormationHelper.CheckKoFightAfterSuicidal(tryMoves.First().TryGame.Board);
                if (!suicidal) return;
            }
            //do not add move if last move is random or pass move
            Point? lastMove = board.LastMove;
            if (lastMove != null && (board.IsRandomMove || lastMove.Value.Equals(Game.PassMove))) return;

            GameTryMove tryMove = GetRandomMove(currentGame);
            if (tryMove != null) tryMoves.Add(tryMove);
        }

        /// <summary>
        /// Create random move for covered eye survival.
        /// <see cref="UnitTestProject.LifeCheckTest.LifeCheckTest_20230422_8" />
        /// </summary>
        private void CreateRandomMoveForCoveredEyeSurvival(List<GameTryMove> tryMoves, Game currentGame)
        {
            Board board = currentGame.Board;
            if (tryMoves.Count > 0) return;
            List<Group> targets = LifeCheck.GetTargets(currentGame.Board);
            foreach (Group targetGroup in targets)
            {
                Content c = targetGroup.Content;
                List<Group> killerGroups = LifeCheck.GetTwoPossibleEyes(board, targetGroup);
                if (killerGroups == null) continue;
                //ensure at least one covered eye
                List<Point> coveredEyes = killerGroups.Where(n => n.Points.Count == 1 && EyeHelper.FindCoveredEye(board, n.Points.First(), c)).Select(n => n.Points.First()).ToList();
                if (coveredEyes.Count == 0) continue;
                if (coveredEyes.Count == 1 && !killerGroups.Where(n => !coveredEyes.Contains(n.Points.First())).Any(n => n.Points.Count <= 2)) continue;
                //check for strong groups at covered board
                if (!WallHelper.StrongGroupsAtCoveredBoard(board, targetGroup)) continue;
                GameTryMove tryMove = GetRandomMove(currentGame);
                if (tryMove != null) tryMoves.Add(tryMove);
                return;
            }
        }

        /// <summary>
        /// Get random move.
        /// </summary>
        public static GameTryMove GetRandomMove(Game currentGame)
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
            GameTryMove tryMove = new GameTryMove(currentGame);
            tryMove.MakeMoveResult = tryMove.TryGame.InternalMakeMove(p.x, p.y);
            tryMove.TryGame.Board.IsRandomMove = true;
            return tryMove;
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
                if (DebugPrintMode(gameDepth))
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
