using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;


namespace Go
{    
    /// <summary>
    /// Map all possible moves for two or three levels on a json map.
    /// </summary>
    public class MonteCarloMapping
    {
        public static Boolean ThreeLevelMapping = Convert.ToBoolean(ConfigurationSettings.AppSettings["ONE_STOP_MAPPING"]);
        public long? elapsedTime;

        public static void MapScenario(Game game)
        {
            Game.MapMoves = true;
            MonteCarloMapping mctsMapping = new MonteCarloMapping();

            //map player moves
            mctsMapping.MappingFirstLevel(game);
            JsonHelper.SerializeJson(game);

            //map challenge moves
            if (game.GameInfo.solutionPoints.Count > 0)
            {
                game.GameInfo.UserFirst = PlayerOrComputer.Computer;
                Point p = game.GameInfo.solutionPoints.First().First();
                game.MakeMove(p);
                mctsMapping.MappingFirstLevel(game);
                JsonHelper.SerializeJson(game);
            }
            Game.MapMoves = false;
        }

        /// <summary>
        /// Start mapping from first level.
        /// </summary>
        public virtual void MappingFirstLevel(Game game)
        {
            Stopwatch watch = Stopwatch.StartNew();
            List<GameTryMove> possibleMoves = GameHelper.GetTryMovesForGame(game);
            Debug.WriteLine("Scenario: " + game.GameInfo.ScenarioName);
            Debug.WriteLine("Game moves: " + possibleMoves.GetConcatenatedString());
            JArray mappedJson = JsonHelper.GetMappedJson(game);

            for (int j = 0; j <= possibleMoves.Count - 1; j++)
            {
                GameTryMove tryMove = possibleMoves[j];
                Game g = new Game(game);

                //make first move on the board
                if (MakeMoveAndCheckIfAnswerFound(g, tryMove.Move))
                    continue;

                //check if second move mapped already
                JObject firstLevel = (JObject)(mappedJson.Where(m => (int)m["FirstMove"]["x"] == tryMove.Move.x && (int)m["FirstMove"]["y"] == tryMove.Move.y).FirstOrDefault());

                if (firstLevel != null)
                {
                    //make second move on the board
                    Point secondMove = new Point((int)firstLevel["SecondMove"]["x"], (int)firstLevel["SecondMove"]["y"]);
                    if (MakeMoveAndCheckIfAnswerFound(g, secondMove))
                        continue;

                    //continue with second level
                    SecondLevelMappingForSolution(g, firstLevel);
                }
                else //second move not mapped
                {
                    //check if solution move available
                    Point? solutionMove = SolutionHelper.GetSolutionMove(g.Board);
                    if (solutionMove != null)
                    {
                        //added solution move to json
                        Point secondMove = solutionMove.Value;
                        MonteCarloMapFirstSecondMove(g, tryMove.Move, secondMove);

                        //make second move on the board
                        if (MakeMoveAndCheckIfAnswerFound(g, secondMove))
                            continue;

                        //continue with second level
                        SecondLevelMappingForSolution(g);
                    }
                    else
                    {
                        //if not mapped and solution not found then search for answer by mcts
                        MonteCarloTreeSearch mcts = MonteCarloGame.InitializeMonteCarloComputerMove(g);
                        MapAnswerNodeToJson(g, tryMove.Move, mcts.AnswerNode);
                    }
                }
            }
            watch.Stop();
            Debug.WriteLine("Total time taken (verification): " + watch.ElapsedMilliseconds);
            elapsedTime = watch.ElapsedMilliseconds;
        }

        /// <summary>
        /// Second level mapping if first level has been mapped or solution found.
        /// </summary>
        private void SecondLevelMappingForSolution(Game game, JObject move = null)
        {
            List<GameTryMove> possibleMoves = GameHelper.GetTryMovesForGame(game);
            for (int j = 0; j <= possibleMoves.Count - 1; j++)
            {
                Game g = new Game(game);
                GameTryMove tryMove = possibleMoves[j];

                //make third move on the board
                if (MakeMoveAndCheckIfAnswerFound(g, tryMove.Move))
                    continue;

                JObject secondLevel = null;
                if (move != null && move["SecondLevel"] != null)
                {
                    //check if fourth move mapped already
                    secondLevel = (JObject)(move["SecondLevel"].Where(m => (int)m["ThirdMove"]["x"] == tryMove.Move.x && (int)m["ThirdMove"]["y"] == tryMove.Move.y).FirstOrDefault());
                    if (secondLevel != null)
                    {
                        Point fourthMove = new Point((int)secondLevel["FourthMove"]["x"], (int)secondLevel["FourthMove"]["y"]);

                        //make fourth move on the board
                        if (MakeMoveAndCheckIfAnswerFound(g, fourthMove))
                            continue;

                        //continue with third level
                        ThirdLevelMappingForSolution(g, secondLevel);
                    }
                }

                //fourth move not mapped
                if (secondLevel == null)
                {
                    //check if solution move available
                    Point? solutionMove = SolutionHelper.GetSolutionMove(g.Board);
                    if (solutionMove != null)
                    {
                        //added solution move to json
                        Point fourthMove = solutionMove.Value;
                        MonteCarloMapThirdFourthMove(g, tryMove.Move, fourthMove);

                        //make fourth move on the board
                        if (MakeMoveAndCheckIfAnswerFound(g, fourthMove))
                            continue;

                        //continue with third level
                        ThirdLevelMappingForSolution(g);
                    }
                    else
                    {
                        //if not mapped and solution not found then search for answer by mcts
                        MonteCarloTreeSearch mcts = MonteCarloGame.InitializeMonteCarloComputerMove(g);
                        Point answerMove = (mcts.AnswerNode != null) ? mcts.AnswerNode.State.Game.Board.Move.Value : Game.PassMove;
                        MonteCarloMapThirdFourthMove(g, tryMove.Move, answerMove, mcts.AnswerNode);
                    }
                }
            }
        }

        /// <summary>
        /// Third level mapping if second level has been mapped or solution found.
        /// </summary>
        protected virtual void ThirdLevelMappingForSolution(Game game, JObject move = null)
        {
            //if only two levels required as specified in config file then return
            if (!MonteCarloMapping.ThreeLevelMapping) return;

            List<GameTryMove> possibleMoves = GameHelper.GetTryMovesForGame(game);
            for (int j = 0; j <= possibleMoves.Count - 1; j++)
            {
                Game g = new Game(game);
                GameTryMove tryMove = possibleMoves[j];

                //make fifth move on the board
                if (MakeMoveAndCheckIfAnswerFound(g, tryMove.Move))
                    continue;

                //if solution found then all three levels completed
                if (SolutionHelper.GetSolutionMove(g.Board) != null)
                    continue;

                if (move != null && move["ThirdLevel"] != null)
                {
                    //check if third level move mapped already
                    JObject thirdLevel = (JObject)(move["ThirdLevel"].Where(m => (int)m["FifthMove"]["x"] == tryMove.Move.x && (int)m["FifthMove"]["y"] == tryMove.Move.y).FirstOrDefault());

                    //if mapped then all three levels completed
                    if (thirdLevel != null)
                        continue;
                }

                //if not mapped and solution not found then search for answer by mcts
                MonteCarloTreeSearch mcts = MonteCarloGame.InitializeMonteCarloComputerMove(g);
                Point answerMove = (mcts.AnswerNode != null) ? mcts.AnswerNode.State.Game.Board.Move.Value : Game.PassMove;
                MonteCarloMapFifthSixthMove(g, tryMove.Move, answerMove, mcts.AnswerNode);
            }
        }

        /// <summary>
        /// Make move on the board and check if game has ended by confirm alive or end of solution.
        /// </summary>
        protected virtual Boolean MakeMoveAndCheckIfAnswerFound(Game g, Point p)
        {
            if (!g.Board.PointWithinBoard(p.x, p.y))
                return true;

            //make move on the board
            SurviveOrKill surviveOrKill = GameHelper.KillOrSurvivalForNextMove(g.Board);
            MakeMoveResult result = g.MakeMove(p);
            if (result == MakeMoveResult.KoBlocked)
                return true;

            //check if game ended
            ConfirmAliveResult confirmAlive = LifeCheck.CheckIfDeadOrAlive(surviveOrKill, g.Board);
            if (confirmAlive != ConfirmAliveResult.Unknown && GameHelper.WinOrLose(surviveOrKill, confirmAlive, g.GameInfo))
                return true;

            return SolutionHelper.AnswerFound(g);
        }

        /// <summary>
        /// Map all three levels from answer node to json map.
        /// </summary>
        public static JObject MapAnswerNodeToJson(Game g, Point firstMovePt, Node answerNode, Boolean getMappedJson = true)
        {
            JArray json = new JArray();
            if (getMappedJson) json = JsonHelper.GetMappedJson(g);
            //first level
            Point answerMove = (answerNode != null && answerNode.State.Game.Board.Move != null) ? answerNode.State.Game.Board.Move.Value : Game.PassMove;
            JObject firstLevel = JsonHelper.FirstLevelMapping(json, firstMovePt, answerMove);
            if (answerNode == null) return firstLevel;
            //second level
            foreach (JObject move in answerNode.PrunedJson)
            {
                Point thirdMove = new Point((int)move["FirstMove"]["x"], (int)move["FirstMove"]["y"]);
                Point fourthMove = new Point((int)move["SecondMove"]["x"], (int)move["SecondMove"]["y"]);
                if (thirdMove.Equals(Game.PassMove)) continue;
                JObject secondLevel = JsonHelper.SecondLevelMapping(firstLevel, thirdMove, fourthMove);
                if (move["SecondLevel"] == null) continue;
                //third level
                if (!MonteCarloMapping.ThreeLevelMapping) continue;
                foreach (JObject move2 in move["SecondLevel"])
                {
                    Point fifthMove = new Point((int)move2["FirstMove"]["x"], (int)move2["FirstMove"]["y"]);
                    Point sixthMove = new Point((int)move2["SecondMove"]["x"], (int)move2["SecondMove"]["y"]);
                    if (fifthMove.Equals(Game.PassMove)) continue;
                    JsonHelper.ThirdLevelMapping(secondLevel, fifthMove, sixthMove);
                }
            }
            return firstLevel;
        }

        private static JObject MonteCarloMapFirstSecondMove(Game g, Point firstMovePt, Point secondMovePt)
        {
            JArray json = JsonHelper.GetMappedJson(g);
            JObject firstLevel = JsonHelper.FirstLevelMapping(json, firstMovePt, secondMovePt);
            return firstLevel;
        }

        /// <summary>
        /// Map second and third levels from answer node to json map.
        /// </summary>
        private static void MonteCarloMapThirdFourthMove(Game g, Point thirdMovePt, Point fourthMovePt, Node answerNode = null)
        {
            JArray json = JsonHelper.GetMappedJson(g);
            int isChallenge = Convert.ToInt32(g.GameInfo.UserFirst == PlayerOrComputer.Computer);
            //second level
            JObject firstLevel = JsonHelper.FirstLevelMapping(json, g.Board.LastMoves[0 + isChallenge], g.Board.LastMoves[1 + isChallenge]);
            JObject secondLevel = JsonHelper.SecondLevelMapping(firstLevel, thirdMovePt, fourthMovePt);
            if (answerNode == null) return;

            if (!MonteCarloMapping.ThreeLevelMapping) return;
            //third level
            foreach (JObject move in answerNode.PrunedJson)
            {
                Point fifthMove = new Point((int)move["FirstMove"]["x"], (int)move["FirstMove"]["y"]);
                Point sixthMove = new Point((int)move["SecondMove"]["x"], (int)move["SecondMove"]["y"]);
                if (fifthMove.Equals(Game.PassMove)) continue;
                JsonHelper.ThirdLevelMapping(secondLevel, fifthMove, sixthMove);
            }
        }

        /// <summary>
        /// Map third level from answer node to json map.
        /// </summary>
        private static void MonteCarloMapFifthSixthMove(Game g, Point fifthMovePt, Point sixthMovePt, Node answerNode)
        {
            JArray json = JsonHelper.GetMappedJson(g);
            int isChallenge = Convert.ToInt32(g.GameInfo.UserFirst == PlayerOrComputer.Computer);
            JObject firstLevel = JsonHelper.FirstLevelMapping(json, g.Board.LastMoves[0 + isChallenge], g.Board.LastMoves[1 + isChallenge]);
            JObject secondLevel = JsonHelper.SecondLevelMapping(firstLevel, g.Board.LastMoves[2 + isChallenge], g.Board.LastMoves[3 + isChallenge]);
            //third level
            JsonHelper.ThirdLevelMapping(secondLevel, fifthMovePt, sixthMovePt);
        }

        /// <summary>
        /// Mapping range.
        /// </summary>
        public static Boolean MappingRange(Board board)
        {
            if (!Game.MapMoves)
                return false;

            if (GameHelper.GetComputerOrPlayerForNextMove(board) == PlayerOrComputer.Computer)
                return false;

            int isChallenge = Convert.ToInt32(board.GameInfo.UserFirst == PlayerOrComputer.Computer);
            if (board.LastMoves.Count <= ((MonteCarloMapping.ThreeLevelMapping) ? 5 : 3) + isChallenge)
                return true;
            return false;
        }

    }
}
