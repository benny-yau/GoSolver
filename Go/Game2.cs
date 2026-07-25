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
        public static Boolean UseMCTS = false;
        public static Boolean MapMoves = false;
        public static Boolean SearchAnswer = false;
        public static readonly Point PassMove = new Point(-1, -1);
        public Boolean isMonteCarloPlayout = false;
        public int[,] heatMap;

        public static Boolean MapMovesOrSearchAnswer
        {
            get
            {
                return MapMoves || SearchAnswer;
            }
        }

        /// <summary>
        /// Initialize computer move. Exhaustive search or mcts move.
        /// </summary>
        public ConfirmAliveResult InitializeComputerMove(Boolean useMCTS = false, Boolean useMapMoves = false)
        {
            try
            {
                Game.UseMCTS = useMCTS;
                this.Board.Move = null;
                ConfirmAliveResult result = ConfirmAliveResult.Unknown;
                if (useMapMoves) result = SolutionHelper.CheckSolutionAndMappedPoints(this);
                if (!result.HasFlag(ConfirmAliveResult.Mapped))
                {
                    ConfirmAliveResult confirmAlive = ConfirmAliveResult.Unknown;
                    if (!useMCTS)
                        confirmAlive = MakeExhaustiveSearch();
                    else
                        confirmAlive = MonteCarloGame.MakeMonteCarloTreeSearch(this).Item1;
                    result |= confirmAlive;

                    DebugHelper.WriteLine("Final move: " + this.Board.Move + " | Final result: " + confirmAlive.ToString());
                }

                if (this.Board.Move == null)
                    this.Board.Move = Game.PassMove;
                return result;
            }
            catch (Exception ex)
            {
                if (Game.debugMode) Debugger.Break();
                return ConfirmAliveResult.Unknown;
            }
        }


        /// <summary>
        /// Make exhaustive search.
        /// </summary>
        public ConfirmAliveResult MakeExhaustiveSearch()
        {
            if (debugMode) this.RunTimeStopWatch = Stopwatch.StartNew();

            int depth = this.GameInfo.SearchDepth;
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
        /// Get survival moves. Check if the game has ended with target survived. Check and remove redundant moves. 
        /// For survive only, check for recursion and add pass move to check for both alive where necessary.
        /// </summary>
        public (ConfirmAliveResult, List<GameTryMove>, GameTryMove) GetSurvivalMoves(Game game = null)
        {
            Game g = game ?? this;
            GameInfo gi = g.GameInfo;
            Content c = GameHelper.GetContentForSurviveOrKill(gi, SurviveOrKill.Survive);
            List<GameTryMove> tryMoves = new List<GameTryMove>();
            List<GameTryMove> redundantTryMoves = new List<GameTryMove>();
            GameTryMove koBlockedMove = null;
            Boolean mappingRange = MonteCarloMapping.MappingRange(g.Board);

            for (int i = 0; i <= gi.movablePoints.Count - 1; i++)
            {
                Point p = gi.movablePoints[i];
                if (g.Board[p] != Content.Empty) continue;
                //create try moves
                GameTryMove tryMove = new GameTryMove(g);
                Board b = tryMove.TryGame.Board;
                tryMove.MakeMoveResult = b.InternalMakeMove(p, c);
                if (tryMove.MakeMoveResult == MakeMoveResult.KoBlocked)
                {
                    //ko moves
                    tryMove.MakeKoMove(p, SurviveOrKill.Survive);
                    tryMove.IsRedundantKo = RedundantMoveHelper.RedundantKoMove(tryMove);
                    if (tryMove.IsRedundantKo) redundantTryMoves.Add(tryMove);
                    if (KoHelper.KoContentEnabled(c, gi) && (!tryMove.IsRedundantKo || mappingRange))
                    {
                        koBlockedMove = tryMove;
                        //check recursion
                        if (GameHelper.CheckForRecursion(tryMove))
                            return (ConfirmAliveResult.KoAlive, new List<GameTryMove>(), koBlockedMove);
                    }
                }
                else if (tryMove.MakeMoveResult == MakeMoveResult.Legal)
                {
                    //check if game ended
                    ConfirmAliveResult confirmAlive = LifeCheck.CheckIfDeadOrAlive(SurviveOrKill.Survive, b, !tryMove.MoveConnectAndDie);
                    if (confirmAlive == ConfirmAliveResult.Alive)
                        return (ConfirmAliveResult.Alive, new List<GameTryMove>() { tryMove }, null);
                    //check recursion
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
            tryMoves = (from tryMove in tryMoves orderby tryMove.ConnectAndDie descending, tryMove.ConnectAndDieResolved descending, tryMove.Captured descending, tryMove.IncreasedKillerGroups descending, tryMove.AtariWithoutSuicide descending, tryMove.MoveGroupLiberties descending select tryMove).ToList();

            //restore neural net move
            if (tryMoves.Count == 0) RedundantMoveHelper.RestoreNeuralNetMove(tryMoves, redundantTryMoves);

            //check for both alive
            BothAliveHelper.EnablePassMoveForBothAlive(g, tryMoves, SurviveOrKill.Survive);

            //create random move
            CreateRandomMoveForCoveredEyeSurvival(tryMoves, g);
            CreateRandomMoveForRedundantKo(g, tryMoves, redundantTryMoves);

            PrintGameMoveList(tryMoves, g);

            return (ConfirmAliveResult.Unknown, tryMoves, koBlockedMove);
        }

        /// <summary>
        /// Check survival redundant moves that can be eliminated to reduce range of possible moves.
        /// </summary>
        private void CheckSurvivalRedundantMoves(GameTryMove tryMove)
        {
            tryMove.IsEye = RedundantMoveHelper.FindPotentialEye(tryMove);
            if (tryMove.IsEye)
                return;
            tryMove.IsFillKoEyeMove = RedundantMoveHelper.FillKoEyeMove(tryMove);
            if (tryMove.IsFillKoEyeMove)
                return;
            //check monte carlo playout
            if (isMonteCarloPlayout) return;
            tryMove.IsCoveredEyeMove = RedundantMoveHelper.RedundantCoveredEyeMove(tryMove);
            if (tryMove.IsCoveredEyeMove)
                return;
            tryMove.IsNeutralPoint = RedundantMoveHelper.NeutralPointSurvivalMove(tryMove);
            if (tryMove.IsNeutralPoint)
                return;
            tryMove.IsDiagonalEyeMove = RedundantMoveHelper.SurvivalEyeDiagonalMove(tryMove);
            if (tryMove.IsDiagonalEyeMove)
                return;
            tryMove.IsRedundantKo = RedundantMoveHelper.RedundantKoMove(tryMove);
            if (tryMove.IsRedundantKo)
                return;
            tryMove.IsRedundantTigerMouth = RedundantMoveHelper.RedundantTigerMouthMove(tryMove);
            if (tryMove.IsRedundantTigerMouth)
                return;
            tryMove.IsAtariRedundant = RedundantMoveHelper.AtariRedundantMove(tryMove);
            if (tryMove.IsAtariRedundant)
                return;
            tryMove.IsSuicidal = RedundantMoveHelper.SuicidalRedundantMove(tryMove);
            if (tryMove.IsSuicidal)
                return;
            tryMove.IsLeapMove = RedundantMoveHelper.RedundantSurvivalLeapMove(tryMove);
            if (tryMove.IsLeapMove)
                return;
            tryMove.IsNonSuicidal = RedundantMoveHelper.RedundantNonSuicidalMove(tryMove);
            if (tryMove.IsNonSuicidal)
                return;
            tryMove.IsFillerMove = RedundantMoveHelper.RedundantFillerMove(tryMove);
            if (tryMove.IsFillerMove)
                return;
            tryMove.IsRedundantNeuralNetMove = RedundantMoveHelper.RedundantNeuralNetMove(tryMove);
            if (tryMove.IsRedundantNeuralNetMove)
                return;
        }


        /// <summary>
        /// Check kill redundant moves that can be eliminated to reduce range of possible moves.
        /// </summary>
        private void CheckKillRedundantMoves(GameTryMove tryMove)
        {
            tryMove.IsEye = RedundantMoveHelper.FindPotentialEye(tryMove);
            if (tryMove.IsEye)
                return;
            tryMove.IsFillKoEyeMove = RedundantMoveHelper.FillKoEyeMove(tryMove);
            if (tryMove.IsFillKoEyeMove)
                return;
            //check monte carlo playout
            if (isMonteCarloPlayout) return;
            tryMove.IsCoveredEyeMove = RedundantMoveHelper.RedundantCoveredEyeMove(tryMove);
            if (tryMove.IsCoveredEyeMove)
                return;
            tryMove.IsNeutralPoint = RedundantMoveHelper.NeutralPointKillMove(tryMove);
            if (tryMove.IsNeutralPoint)
                return;
            tryMove.IsNeutralPoint = RedundantMoveHelper.KillEyeDiagonalMove(tryMove);
            if (tryMove.IsNeutralPoint)
                return;
            tryMove.IsRedundantKo = RedundantMoveHelper.RedundantKoMove(tryMove);
            if (tryMove.IsRedundantKo)
                return;
            tryMove.IsRedundantTigerMouth = RedundantMoveHelper.RedundantTigerMouthMove(tryMove);
            if (tryMove.IsRedundantTigerMouth)
                return;
            tryMove.IsAtariRedundant = RedundantMoveHelper.AtariRedundantMove(tryMove);
            if (tryMove.IsAtariRedundant)
                return;
            tryMove.IsSuicidal = RedundantMoveHelper.SuicidalRedundantMove(tryMove);
            if (tryMove.IsSuicidal)
                return;
            tryMove.IsLeapMove = RedundantMoveHelper.RedundantKillLeapMove(tryMove);
            if (tryMove.IsLeapMove)
                return;
            tryMove.IsNonSuicidal = RedundantMoveHelper.RedundantNonSuicidalMove(tryMove);
            if (tryMove.IsNonSuicidal)
                return;
            tryMove.IsFillerMove = RedundantMoveHelper.RedundantFillerMove(tryMove);
            if (tryMove.IsFillerMove)
                return;
            tryMove.IsRedundantNeuralNetMove = RedundantMoveHelper.RedundantNeuralNetMove(tryMove);
            if (tryMove.IsRedundantNeuralNetMove)
                return;
        }


        /// <summary>
        /// Make survival move by exhaustive search.
        /// </summary>
        public (ConfirmAliveResult, GameTryMove) MakeSurvivalMove(int depth, Game game = null)
        {
            Game g = game ?? this;
            GameTryMove bestResultMove = null;
            ConfirmAliveResult bestResult = ConfirmAliveResult.Dead;
            //if end of depth reached, then assume target group is dead
            if (depth <= 0)
                return (ConfirmAliveResult.Dead, bestResultMove);

            //get all survival moves
            (ConfirmAliveResult result, List<GameTryMove> tryMoves, GameTryMove koBlockedMove) = GetSurvivalMoves(g);
            if (result == ConfirmAliveResult.Alive)
                return (result, tryMoves.First());

            //try all possible moves
            for (int i = 0; i <= tryMoves.Count - 1; i++)
            {
                GameTryMove tryMove = tryMoves[i];
                Board b = tryMove.TryGame.Board;
                Stopwatch watch = null;
                int gameDepth = GameDepth(g);
                if (DebugPrintMode(gameDepth))
                {
                    if (gameDepth == 0) Debug.WriteLine(Environment.NewLine);
                    DebugHelper.WriteLine("Trying game move at " + tryMove.Move.ToString() + " at depth " + depth + " (" + (i + 1) + " out of " + tryMoves.Count + ") | Last moves: " + g.Board.GetLastMoves(), gameDepth);
                    watch = Stopwatch.StartNew();
                }

                //make next opponent move
                (tryMove.ConfirmAlive, tryMove.OpponentBestMove) = MakeKillMove(depth - 1, tryMove.TryGame);

                if (watch != null)
                {
                    watch.Stop();
                    DebugHelper.WriteLine("Time taken for " + tryMove.Move.ToString() + " at depth " + depth + ": " + watch.ElapsedMilliseconds + " | Result: " + tryMove.ConfirmAlive.ToString(), gameDepth);
                }

                //check if game ended
                if (tryMove.ConfirmAlive != ConfirmAliveResult.Unknown && ((int)tryMove.ConfirmAlive > (int)bestResult))
                {
                    bestResult = tryMove.ConfirmAlive;
                    bestResultMove = tryMove;
                    if (GameHelper.WinOrLose(SurviveOrKill.Survive, bestResult, g.GameInfo))
                    {
                        if (b.IsPassMove && b.KoGameCheck == KoCheck.None) bestResult = ConfirmAliveResult.BothAlive;
                        return (bestResult, bestResultMove);
                    }
                }
            }

            //check for ko
            if (KoMoveCheck(g, SurviveOrKill.Survive, koBlockedMove, depth))
                return (koBlockedMove.ConfirmAlive, koBlockedMove);

            return (bestResult, bestResultMove);
        }

        /// <summary>
        /// Make ko move and return result as KoAlive if ko move wins.
        /// </summary>
        private Boolean KoMoveCheck(Game g, SurviveOrKill surviveOrKill, GameTryMove koTryMove, int depth)
        {
            if (koTryMove == null) return false;
            Boolean koEnabled = KoHelper.KoSurvivalEnabled(surviveOrKill, g.GameInfo);
            if (!koEnabled) return false;
            Point move = koTryMove.Move;
            Stopwatch watch = null;
            int gameDepth = GameDepth(g);
            if (DebugPrintMode(gameDepth))
            {
                if (gameDepth == 0) Debug.WriteLine(Environment.NewLine);
                DebugHelper.WriteLine("Trying Ko game move at " + move.ToString() + " at depth " + depth + " | Last moves: " + g.Board.GetLastMoves(), gameDepth);
                watch = Stopwatch.StartNew();
            }

            //make next opponent move
            Game game = koTryMove.TryGame;
            if (surviveOrKill == SurviveOrKill.Survive)
                (koTryMove.ConfirmAlive, koTryMove.OpponentBestMove) = MakeKillMove(depth, game);
            else if (surviveOrKill == SurviveOrKill.Kill)
                (koTryMove.ConfirmAlive, koTryMove.OpponentBestMove) = MakeSurvivalMove(depth, game);

            if (watch != null)
            {
                watch.Stop();
                DebugHelper.WriteLine("Time taken for Ko " + move.ToString() + " at depth " + depth + ": " + watch.ElapsedMilliseconds + " | Result: " + koTryMove.ConfirmAlive.ToString(), gameDepth);
            }
            if (GameHelper.WinOrLose(surviveOrKill, koTryMove.ConfirmAlive, game.GameInfo))
            {
                koTryMove.ConfirmAlive = ConfirmAliveResult.KoAlive;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Get kill moves. Check if the game has ended with target killed. Check and remove redundant moves. 
        /// For kill only, restore neutral points where necessary and add random move for kill where no move is available.
        /// </summary>
        public (ConfirmAliveResult, List<GameTryMove>, GameTryMove) GetKillMoves(Game game = null)
        {
            Game g = game ?? this;
            GameInfo gi = g.GameInfo;
            Content c = GameHelper.GetContentForSurviveOrKill(gi, SurviveOrKill.Kill);
            List<GameTryMove> tryMoves = new List<GameTryMove>();
            List<GameTryMove> redundantTryMoves = new List<GameTryMove>();
            GameTryMove koBlockedMove = null;
            Boolean mappingRange = MonteCarloMapping.MappingRange(g.Board);

            for (int i = 0; i <= gi.killMovablePoints.Count - 1; i++)
            {
                Point p = gi.killMovablePoints[i];
                if (g.Board[p] != Content.Empty) continue;
                //create try moves
                GameTryMove tryMove = new GameTryMove(g);
                Board b = tryMove.TryGame.Board;
                tryMove.MakeMoveResult = b.InternalMakeMove(p, c);
                if (tryMove.MakeMoveResult == MakeMoveResult.KoBlocked)
                {
                    //ko moves
                    tryMove.MakeKoMove(p, SurviveOrKill.Kill);
                    tryMove.IsRedundantKo = RedundantMoveHelper.RedundantKoMove(tryMove);
                    if (tryMove.IsRedundantKo) redundantTryMoves.Add(tryMove);
                    if (KoHelper.KoContentEnabled(c, gi) && (!tryMove.IsRedundantKo || mappingRange))
                    {
                        koBlockedMove = tryMove;
                        //check recursion
                        if (GameHelper.CheckForRecursion(tryMove))
                            return (ConfirmAliveResult.KoAlive, new List<GameTryMove>(), koBlockedMove);
                    }
                }
                else if (tryMove.MakeMoveResult == MakeMoveResult.Legal)
                {
                    //check if game ended
                    ConfirmAliveResult confirmAlive = LifeCheck.CheckIfDeadOrAlive(SurviveOrKill.Kill, b);
                    if (confirmAlive == ConfirmAliveResult.Dead)
                        return (ConfirmAliveResult.Dead, new List<GameTryMove>() { tryMove }, null);
                    //check recursion
                    if (GameHelper.CheckForRecursion(tryMove))
                        return (ConfirmAliveResult.Alive, new List<GameTryMove>() { tryMove }, null);
                    //find redundant moves
                    CheckKillRedundantMoves(tryMove);
                    tryMoves.Add(tryMove);
                }
            }

            if (!mappingRange)
            {
                //remove all redundant moves
                tryMoves.Where(e => e.IsRedundantMove).ToList().ForEach(t => { redundantTryMoves.Add(t); tryMoves.Remove(t); });

                //restore neutral move
                RedundantMoveHelper.RestoreNeutralMove(g, tryMoves, redundantTryMoves);
            }

            //sort game try moves
            tryMoves = (from tryMove in tryMoves orderby tryMove.ConnectAndDie descending, tryMove.ConnectAndDieResolved descending, tryMove.Captured descending, tryMove.AtariWithoutSuicide descending, tryMove.MoveGroupLiberties descending select tryMove).ToList();

            //create random move
            CreateRandomMoveForKill(tryMoves, g);
            CreateRandomMoveForRedundantKo(g, tryMoves, redundantTryMoves);

            //check for both alive
            BothAliveHelper.EnablePassMoveForBothAlive(g, tryMoves, SurviveOrKill.Kill);
            PrintGameMoveList(tryMoves, g);

            return (ConfirmAliveResult.Unknown, tryMoves, koBlockedMove);
        }

        /// <summary>
        /// Create random move for redundant ko.
        /// Check double ko fight <see cref="UnitTestProject.NeutralPointMoveTest.NeutralPointMoveTest_Scenario_XuanXuanGo_A28_101Weiqi_7" />
        /// Killer ko within killer group <see cref="UnitTestProject.RedundantKoMoveTest.RedundantKoMoveTest_Scenario_Corner_A79" />
        /// <see cref="UnitTestProject.RedundantKoMoveTest.RedundantKoMoveTest_Scenario_Corner_B39" />
        /// <see cref="UnitTestProject.RedundantKoMoveTest.RedundantKoMoveTest_Scenario_XuanXuanGo_A28_101Weiqi_5" />
        /// Check covered eye <see cref="UnitTestProject.NeutralPointMoveTest.NeutralPointMoveTest_Scenario_XuanXuanGo_A28_101Weiqi_6" />
        /// Check atari resolved <see cref="UnitTestProject.RedundantKoMoveTest.RedundantKoMoveTest_Scenario_TianLongTu_Q17078_2" />
        /// Check base line leap link <see cref="UnitTestProject.RedundantKoMoveTest.RedundantKoMoveTest_Scenario_TianLongTu_Q17078_3" />
        /// </summary>
        private void CreateRandomMoveForRedundantKo(Game g, List<GameTryMove> tryMoves, List<GameTryMove> redundantTryMoves)
        {
            Board b = g.Board;
            if (b.IsPassMove) return;
            if (tryMoves.Count > 0)
            {
                if (tryMoves.Count != 1) return;
                Board tryBoard = tryMoves.First().TryGame.Board;
                if (tryBoard.IsRandomMove || tryBoard.IsPassMove) return;
                //check double ko fight
                if (!KoHelper.IsKoFight(b, tryBoard.Move.Value, tryBoard.MoveGroup.Content).Item1) return;
            }

            foreach (GameTryMove koMove in redundantTryMoves.Where(t => t.IsRedundantKo))
            {
                Board tryBoard = koMove.TryGame.Board;
                Content c = tryBoard.MoveGroup.Content;
                Point? koPoint = KoHelper.GetKoEyePoint(tryBoard);
                if (koPoint == null) continue;
                //check atari resolved
                if (koMove.AtariResolved) continue;
                if (KoHelper.IsNonKillableGroupKoFight(tryBoard))
                    continue;
                if (GroupHelper.GetDirectKillerGroup(b, koPoint.Value, c) == null) continue;

                //killer ko within killer group 
                if (tryBoard.AtariTargets.Any(t => !ImmovableHelper.CheckConnectAndDie(b, b.GetGroupAt(t.Points.First()), false)))
                {
                    GameTryMove tryMove = GetRandomMove(g);
                    if (tryMove != null) tryMoves.Add(tryMove);
                    break;
                }
            }
        }


        /// <summary>
        /// Create random move for kill.
        /// No more moves <see cref="UnitTestProject.KoTest.KoTest_Scenario_WuQingYuan_Q31498" />
        /// <see cref="UnitTestProject.KoTest.KoTest_Scenario_TianLongTu_Q17077" />
        /// Check ko fight after suicidal <see cref="UnitTestProject.KillerFormationTest.KillerFormationTest_Scenario_WuQingYuan_Q31498" />
        /// </summary>
        private void CreateRandomMoveForKill(List<GameTryMove> tryMoves, Game g)
        {
            Board b = g.Board;
            if (tryMoves.Count > 1) return;
            //check ko fight after suicidal
            if (tryMoves.Count == 1)
            {
                GameTryMove tryMove = tryMoves.First();
                Boolean rc = tryMove.MoveConnectAndDie && KillerFormationHelper.CheckKoFightAfterSuicidal(tryMove.TryGame.Board, tryMove.CaptureBoard);
                if (!rc) return;
            }

            //check random or pass move
            if (b.IsRandomMove || b.IsPassMove) return;

            GameTryMove randomMove = GetRandomMove(g);
            if (randomMove != null) tryMoves.Add(randomMove);
        }

        /// <summary>
        /// Create random move for covered eye survival.
        /// <see cref="UnitTestProject.LifeCheckTest.LifeCheckTest_20230422_8" />
        /// </summary>
        private void CreateRandomMoveForCoveredEyeSurvival(List<GameTryMove> tryMoves, Game g)
        {
            Board b = g.Board;
            if (tryMoves.Count > 0) return;
            List<Group> targets = LifeCheck.GetTargets(b);
            foreach (Group targetGroup in targets)
            {
                Content c = targetGroup.Content;
                List<Group> killerGroups = LifeCheck.GetTwoPossibleEyes(b, targetGroup);
                if (killerGroups == null) continue;
                //ensure at least one covered eye
                List<Point> coveredEyes = killerGroups.Where(n => n.Points.Count == 1 && EyeHelper.FindCoveredEye(b, n.Points.First(), c)).Select(n => n.Points.First()).ToList();
                if (coveredEyes.Count == 0) continue;
                if (coveredEyes.Count == 1 && !killerGroups.Where(n => !coveredEyes.Contains(n.Points.First())).Any(n => n.Points.Count <= 2)) continue;
                //check for strong groups at covered board
                if (!WallHelper.StrongGroupsAtCoveredBoard(b, targetGroup)) continue;
                GameTryMove tryMove = GetRandomMove(g);
                if (tryMove != null) tryMoves.Add(tryMove);
                return;
            }
        }

        /// <summary>
        /// Get random move.
        /// </summary>
        public static GameTryMove GetRandomMove(Game g)
        {
            Board board = g.Board;
            Point p = Game.PassMove;
            for (int i = 3; i < 11; i++)
            {
                for (int j = 3; j < 8; j++)
                {
                    if (board[i, j] != Content.Empty) continue;
                    p = new Point(i, j);
                    break;
                }
                if (!p.Equals(Game.PassMove)) break;
            }
            if (p.Equals(Game.PassMove))
                return null;
            GameTryMove tryMove = new GameTryMove(g, p);
            tryMove.TryGame.Board.IsRandomMove = true;
            return tryMove;
        }

        /// <summary>
        /// Make kill move by exhaustive search.
        /// </summary>
        private (ConfirmAliveResult, GameTryMove) MakeKillMove(int depth, Game game = null)
        {
            Game g = game ?? this;
            GameTryMove bestResultMove = null;
            ConfirmAliveResult bestResult = ConfirmAliveResult.Alive;

            //get all kill moves
            (ConfirmAliveResult result, List<GameTryMove> tryMoves, GameTryMove koBlockedMove) = GetKillMoves(g);
            if (result == ConfirmAliveResult.Dead)
                return (result, tryMoves.First());

            //try all possible moves
            for (int i = 0; i <= tryMoves.Count - 1; i++)
            {
                GameTryMove tryMove = tryMoves[i];
                Stopwatch watch = null;
                int gameDepth = GameDepth(g);
                if (DebugPrintMode(gameDepth))
                {
                    if (gameDepth == 0) Debug.WriteLine(Environment.NewLine);
                    DebugHelper.WriteLine("Trying game move at " + tryMove.Move.ToString() + " at depth " + depth + " (" + (i + 1) + " out of " + tryMoves.Count + ") | Last moves: " + g.Board.GetLastMoves(), gameDepth);
                    watch = Stopwatch.StartNew();
                }

                //make next opponent move
                (tryMove.ConfirmAlive, tryMove.OpponentBestMove) = MakeSurvivalMove(depth - 1, tryMove.TryGame);

                if (watch != null)
                {
                    watch.Stop();
                    DebugHelper.WriteLine("Time taken for " + tryMove.Move.ToString() + " at depth " + depth + ": " + watch.ElapsedMilliseconds + " | Result: " + tryMove.ConfirmAlive.ToString(), gameDepth);
                }

                //check if game ended
                if (tryMove.ConfirmAlive != ConfirmAliveResult.Unknown && ((int)tryMove.ConfirmAlive < (int)bestResult))
                {
                    bestResult = tryMove.ConfirmAlive;
                    bestResultMove = tryMove;
                    if (GameHelper.WinOrLose(SurviveOrKill.Kill, bestResult, g.GameInfo))
                        return (bestResult, bestResultMove);
                }
            }

            //check for ko
            if (KoMoveCheck(g, SurviveOrKill.Kill, koBlockedMove, depth))
                return (koBlockedMove.ConfirmAlive, koBlockedMove);

            return (bestResult, bestResultMove);
        }

    }
}
