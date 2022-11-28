using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;


namespace Go
{
    /// <summary>
    /// Verification of the json map by searching for opponent moves after sixth move. Inherits the MonteCarloMapping class with modifications for third level mapping.
    /// Error is found if opponent seventh move wins and answer is returned.
    /// </summary>
    public class MappingVerification : MonteCarloMapping
    {
        public int errorCount = 0;
        public long elapsedTimeToLog = 30000;

        public static int VerifyScenario(Game game)
        {
            //verify player moves
            MappingVerification mctsVerification = new MappingVerification();
            mctsVerification.MappingFirstLevel(game);

            //verify challenge moves
            if (game.GameInfo.solutionPoints.Count > 0)
            {
                game.GameInfo.UserFirst = PlayerOrComputer.Computer;
                Point p = game.GameInfo.solutionPoints.First().First();
                game.MakeMove(p);

                mctsVerification.MappingFirstLevel(game);
            }
            return mctsVerification.errorCount;
        }

        public override void MappingFirstLevel(Game game)
        {
            base.MappingFirstLevel(game);
        }

        /// <summary>
        /// Overrides function to provide verification for third level.
        /// </summary>
        protected override void ThirdLevelMappingForSolution(Game game, JObject move = null)
        {
            if (move == null)
                return;

            if (move["ThirdLevel"] == null)
            {
                //check if solution move available
                Point? p = SolutionHelper.GetSolutionMove(game);
                if (p != null)
                {
                    if (game.GameInfo.UserFirst == PlayerOrComputer.Player)
                    {
                        Game g = new Game(game);
                        if (MakeMoveAndCheckIfAnswerFound(g, p.Value))
                            return;
                        FinalVerification(g);
                    }
                    return;
                }

                //verify second level if third level not mapped on json
                FinalVerification(game);
                return;
            }

            List<GameTryMove> possibleMoves = State.GetAllPossibleMoves(game);
            for (int j = 0; j <= possibleMoves.Count - 1; j++)
            {
                Game g = new Game(game);
                GameTryMove gameTryMove = possibleMoves[j];

                //make fifth move on the board
                if (MakeMoveAndCheckIfAnswerFound(g, gameTryMove.Move))
                    continue;

                //check if solution move available
                if (SolutionHelper.GetSolutionMove(g) != null)
                {
                    if (game.GameInfo.UserFirst == PlayerOrComputer.Player)
                        FinalVerification(g);
                    continue;
                }

                if (move != null && move["ThirdLevel"] != null)
                {
                    //check if mapped already
                    JObject thirdLevelMove = (JObject)(move["ThirdLevel"].Where(m => (int)m["FifthMove"]["x"] == gameTryMove.Move.x && (int)m["FifthMove"]["y"] == gameTryMove.Move.y).FirstOrDefault());

                    if (thirdLevelMove != null)
                    {
                        Point sixthMove = new Point((int)thirdLevelMove["SixthMove"]["x"], (int)thirdLevelMove["SixthMove"]["y"]);
                        //make sixth move on the board
                        if (MakeMoveAndCheckIfAnswerFound(g, sixthMove))
                            continue;

                        //verify sixth move
                        FinalVerification(g);
                    }
                }
            }
        }

        /// <summary>
        /// Verify mapped move with mcts. If answer is returned then error is found.
        /// </summary>
        private void FinalVerification(Game g)
        {
            //verify with mcts
            MonteCarloTreeSearch mcts = MonteCarloGame.InitializeMonteCarloComputerMove(g);
            if (mcts.AnswerNode != null)
            {
                errorCount += 1;
                WriteToFile("Incorrect: " + mcts.AnswerNode.GetLastMoves() + Environment.NewLine);
            }

            //log if time exceeded
            if (mcts.elapsedTime.Value > elapsedTimeToLog)
                WriteToFile("Elapsed time: " + mcts.elapsedTime.Value + " - " + g.Board.GetLastMoves() + Environment.NewLine);
        }

        public static void WriteToFile(String contents)
        {
            File.AppendAllText(Directory.GetCurrentDirectory() + "\\verification.txt", contents);
            Debug.WriteLine(contents);
        }


    }
}
