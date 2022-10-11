using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;


namespace Go
{
    /// <summary>
    /// Mapping all possible moves up to three levels on a json map, including ko moves, redundant moves, etc.
    /// This will enable a spontaneous response in real-time play up to three levels.
    /// </summary>
    public class MonteCarloMapping
    {
        public static Boolean mapMoves = false;
        public static Boolean useMappingRange = Convert.ToBoolean(ConfigurationSettings.AppSettings["USE_MAPPING_RANGE"]);
        public long? elapsedTime;

        public static void MapScenario(Game game)
        {
            mapMoves = true;
            MonteCarloMapping mctsMapping = new MonteCarloMapping();

            //map player moves
            mctsMapping.MappingFirstLevel(game);
            GameMapping.SerializeJson(game);

            //map challenge moves
            if (game.GameInfo.solutionPoints.Count > 0)
            {
                game.GameInfo.UserFirst = PlayerOrComputer.Computer;
                Point p = game.GameInfo.solutionPoints.First().First();
                game.MakeMove(p);
                mctsMapping.MappingFirstLevel(game);
                GameMapping.SerializeJson(game);
            }
            mapMoves = false;
        }

        /// <summary>
        /// Start mapping from first level.
        /// </summary>
        public virtual void MappingFirstLevel(Game game)
        {
            Stopwatch watch = Stopwatch.StartNew();
            List<GameTryMove> possibleMoves = State.GetAllPossibleMoves(game, false);
            Debug.WriteLine("Scenario: " + game.GameInfo.ScenarioName);
            String msg = "";
            foreach (GameTryMove g in possibleMoves)
            {
                if (msg != "") msg += ", ";
                msg += "(" + g.Move.x + ", " + g.Move.y + ")";
            }
            Debug.WriteLine("Game moves: " + msg);
            JArray mappedJson = GameMapping.GetMappedJson(game);

            for (int j = 0; j <= possibleMoves.Count - 1; j++)
            {
                GameTryMove gameTryMove = possibleMoves[j];
                Game g = new Game(game);

                //make first move on the board
                if (MakeMoveAndCheckIfAnswerFound(g, gameTryMove.Move))
                    continue;

                //check if second move mapped already
                JObject firstLevelMove = (JObject)(mappedJson.Where(m => (int)m["FirstMove"]["x"] == gameTryMove.Move.x && (int)m["FirstMove"]["y"] == gameTryMove.Move.y).FirstOrDefault());

                if (firstLevelMove != null)
                {
                    //make second move on the board
                    Point secondMove = new Point((int)firstLevelMove["SecondMove"]["x"], (int)firstLevelMove["SecondMove"]["y"]);
                    if (MakeMoveAndCheckIfAnswerFound(g, secondMove))
                        continue;

                    //continue with second level
                    SecondLevelMappingForSolution(g, firstLevelMove);
                }
                else //second move not mapped
                {
                    //check if solution move available
                    Point? solutionMove = SolutionHelper.GetSolutionMove(g);
                    if (solutionMove != null)
                    {
                        //added solution move to json
                        Point secondMove = solutionMove.Value;
                        MonteCarloMapFirstSecondMove(g, gameTryMove.Move, secondMove);

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
                        MapAnswerNodeToJson(g, gameTryMove.Move, mcts.AnswerNode);
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
            List<GameTryMove> possibleMoves = State.GetAllPossibleMoves(game, false, true);
            for (int j = 0; j <= possibleMoves.Count - 1; j++)
            {
                Game g = new Game(game);
                GameTryMove gameTryMove = possibleMoves[j];

                //make third move on the board
                if (MakeMoveAndCheckIfAnswerFound(g, gameTryMove.Move))
                    continue;

                JObject secondLevelMove = null;
                if (move != null && move["SecondLevel"] != null)
                {
                    //check if fourth move mapped already
                    secondLevelMove = (JObject)(move["SecondLevel"].Where(m => (int)m["ThirdMove"]["x"] == gameTryMove.Move.x && (int)m["ThirdMove"]["y"] == gameTryMove.Move.y).FirstOrDefault());
                    if (secondLevelMove != null)
                    {
                        Point fourthMove = new Point((int)secondLevelMove["FourthMove"]["x"], (int)secondLevelMove["FourthMove"]["y"]);

                        //make fourth move on the board
                        if (MakeMoveAndCheckIfAnswerFound(g, fourthMove))
                            continue;

                        //continue with third level
                        ThirdLevelMappingForSolution(g, secondLevelMove);
                    }
                }

                //fourth move not mapped
                if (secondLevelMove == null)
                {
                    //check if solution move available
                    Point? solutionMove = SolutionHelper.GetSolutionMove(g);
                    if (solutionMove != null)
                    {
                        //added solution move to json
                        Point fourthMove = solutionMove.Value;
                        MonteCarloMapThirdFourthMove(g, gameTryMove.Move, fourthMove);

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
                        MonteCarloMapThirdFourthMove(g, gameTryMove.Move, answerMove, mcts.AnswerNode);
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
            if (!GameMapping.OneStopMapping) return;

            List<GameTryMove> possibleMoves = State.GetAllPossibleMoves(game, false, true);
            for (int j = 0; j <= possibleMoves.Count - 1; j++)
            {
                Game g = new Game(game);
                GameTryMove gameTryMove = possibleMoves[j];

                //make fifth move on the board
                if (MakeMoveAndCheckIfAnswerFound(g, gameTryMove.Move))
                    continue;

                //if solution found then all three levels completed
                if (SolutionHelper.GetSolutionMove(g) != null)
                    continue;

                if (move != null && move["ThirdLevel"] != null)
                {
                    //check if third level move mapped already
                    JObject thirdLevelMove = (JObject)(move["ThirdLevel"].Where(m => (int)m["FifthMove"]["x"] == gameTryMove.Move.x && (int)m["FifthMove"]["y"] == gameTryMove.Move.y).FirstOrDefault());

                    //if mapped then all three levels completed
                    if (thirdLevelMove != null)
                        continue;
                }

                //if not mapped and solution not found then search for answer by mcts
                MonteCarloTreeSearch mcts = MonteCarloGame.InitializeMonteCarloComputerMove(g);
                Point answerMove = (mcts.AnswerNode != null) ? mcts.AnswerNode.State.Game.Board.Move.Value : Game.PassMove;
                MonteCarloMapFifthSixthMove(g, gameTryMove.Move, answerMove, mcts.AnswerNode);
            }
        }

        /// <summary>
        /// Make move on the board and check if game has ended by confirm alive or end of solution.
        /// </summary>
        protected virtual Boolean MakeMoveAndCheckIfAnswerFound(Game g, Point p)
        {
            if (p.Equals(Game.PassMove) && GameHelper.GetComputerOrPlayerForNextMove(g.Board) == PlayerOrComputer.Player)
                return true;

            //make move on the board
            MakeMoveResult result = g.MakeMove(p);
            if (result == MakeMoveResult.KoBlocked)
                return true;

            //check if game ended
            SurviveOrKill surviveOrKill = GameHelper.KillOrSurvivalForNextMove(g.Board);
            ConfirmAliveResult confirmAlive = LifeCheck.CheckIfDeadOrAlive(surviveOrKill, g);
            if (confirmAlive == ConfirmAliveResult.Alive || confirmAlive == ConfirmAliveResult.Dead)
                return true;

            //check if answer found
            return (SolutionHelper.AnswerFound(g));
        }

        /// <summary>
        /// Map all three levels from answer node to json map.
        /// </summary>
        public static JObject MapAnswerNodeToJson(Game g, Point firstMovePt, Node answerNode, Boolean getMappedJson = true)
        {
            JArray json = new JArray();
            if (getMappedJson) json = GameMapping.GetMappedJson(g);
            //first level
            Point answerMove = (answerNode != null && answerNode.State.Game.Board.Move != null) ? answerNode.State.Game.Board.Move.Value : Game.PassMove;
            JObject firstLevelMove = JsonHelper.FirstLevelMapping(json, firstMovePt, answerMove);
            if (answerNode == null) return firstLevelMove;
            //second level
            foreach (JObject move in answerNode.PrunedJson)
            {
                Point thirdMove = new Point((int)move["FirstMove"]["x"], (int)move["FirstMove"]["y"]);
                Point fourthMove = new Point((int)move["SecondMove"]["x"], (int)move["SecondMove"]["y"]);
                if (thirdMove.Equals(Game.PassMove)) continue;
                JObject secondLevelMove = JsonHelper.SecondLevelMapping(firstLevelMove, thirdMove, fourthMove);
                if (move["SecondLevel"] == null) continue;
                //third level
                if (!GameMapping.OneStopMapping) continue;
                foreach (JObject move2 in move["SecondLevel"])
                {
                    Point fifthMove = new Point((int)move2["FirstMove"]["x"], (int)move2["FirstMove"]["y"]);
                    Point sixthMove = new Point((int)move2["SecondMove"]["x"], (int)move2["SecondMove"]["y"]);
                    if (fifthMove.Equals(Game.PassMove)) continue;
                    JsonHelper.ThirdLevelMapping(secondLevelMove, fifthMove, sixthMove);
                }
            }
            return firstLevelMove;
        }

        private static JObject MonteCarloMapFirstSecondMove(Game g, Point firstMovePt, Point secondMovePt)
        {
            JArray json = GameMapping.GetMappedJson(g);
            JObject firstLevelMove = JsonHelper.FirstLevelMapping(json, firstMovePt, secondMovePt);
            return firstLevelMove;
        }

        /// <summary>
        /// Map second and third levels from answer node to json map.
        /// </summary>
        private static void MonteCarloMapThirdFourthMove(Game g, Point thirdMovePt, Point fourthMovePt, Node answerNode = null)
        {
            JArray json = GameMapping.GetMappedJson(g);
            int isChallenge = Convert.ToInt32(g.GameInfo.UserFirst == PlayerOrComputer.Computer);
            //second level
            JObject firstLevelMove = JsonHelper.FirstLevelMapping(json, g.Board.LastMoves[0 + isChallenge], g.Board.LastMoves[1 + isChallenge]);
            JObject secondLevelMove = JsonHelper.SecondLevelMapping(firstLevelMove, thirdMovePt, fourthMovePt);
            if (answerNode == null) return;

            if (GameMapping.OneStopMapping) return;
            //third level
            foreach (JObject move in answerNode.PrunedJson)
            {
                Point fifthMove = new Point((int)move["FirstMove"]["x"], (int)move["FirstMove"]["y"]);
                Point sixthMove = new Point((int)move["SecondMove"]["x"], (int)move["SecondMove"]["y"]);
                if (fifthMove.Equals(Game.PassMove)) continue;
                JsonHelper.ThirdLevelMapping(secondLevelMove, fifthMove, sixthMove);
            }
        }

        /// <summary>
        /// Map third level from answer node to json map.
        /// </summary>
        private static void MonteCarloMapFifthSixthMove(Game g, Point fifthMovePt, Point sixthMovePt, Node answerNode)
        {
            JArray json = GameMapping.GetMappedJson(g);
            int isChallenge = Convert.ToInt32(g.GameInfo.UserFirst == PlayerOrComputer.Computer);
            JObject firstLevelMove = JsonHelper.FirstLevelMapping(json, g.Board.LastMoves[0 + isChallenge], g.Board.LastMoves[1 + isChallenge]);
            JObject secondLevelMove = JsonHelper.SecondLevelMapping(firstLevelMove, g.Board.LastMoves[2 + isChallenge], g.Board.LastMoves[3 + isChallenge]);
            //third level
            JsonHelper.ThirdLevelMapping(secondLevelMove, fifthMovePt, sixthMovePt);
        }

        /// <summary>
        /// On mapping, return true for the first three levels (or first six moves) in order to map all moves whether redundant or not.
        /// </summary>
        public static Boolean MappingRange(Board board)
        {
            if (!MonteCarloMapping.mapMoves || !MonteCarloMapping.useMappingRange)
                return false;

            if (GameHelper.GetComputerOrPlayerForNextMove(board) == PlayerOrComputer.Computer)
                return false;

            int isChallenge = Convert.ToInt32(board.GameInfo.UserFirst == PlayerOrComputer.Computer);
            if (board.LastMoves.Count <= ((GameMapping.OneStopMapping) ? 5 : 3) + isChallenge)
                return true;
            return false;
        }

    }
}
