using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Go;
using ScenarioCollection;

namespace ConsoleGoSolver
{
    class Verification
    {
        /// <summary>
        /// Replicate game.
        /// </summary>
        public static Game ReplicateGame(String gameSet, String level, int scenarioNumber)
        {
            List<Func<Scenario, Game>> scenarioList = ScenarioHelper.GetScenarioDelegates(gameSet, level);
            return ScenarioHelper.GetScenarioFromList(scenarioList, scenarioNumber);
        }

        /// <summary>
        /// Verify mapped json. 
        /// </summary>
        public static void VerifyMappedJson(String gameSet, String level)
        {
            List<Func<Scenario, Game>> scenarioList = ScenarioHelper.GetScenarioDelegates(gameSet, level);
            MappingVerification.WriteToFile("Verify mapped json for: " + gameSet + ", " + level + Environment.NewLine);
            for (int i = 0; i <= scenarioList.Count - 1; i++)
            {
                //verify player json
                Game game = ReplicateGame(gameSet, level, i);
                game.GameInfo.UserFirst = PlayerOrComputer.Player;
                MappingVerification.WriteToFile("Scenario " + (i + 1).ToString() + " : " + game.GameInfo.ScenarioName + Environment.NewLine + "Player json" + Environment.NewLine);
                MappingVerification verification = new MappingVerification();
                verification.MappingFirstLevel(game);
                if (verification.elapsedTime != null) MappingVerification.WriteToFile(DebugHelper.PrintTimeTaken(verification.elapsedTime.Value) + Environment.NewLine);
                MappingVerification.WriteToFile("Scenario errors: " + verification.errorCount + Environment.NewLine + Environment.NewLine);

                if (game.GameInfo.solutionPoints.Count == 0) continue;
                //verify challenge json
                game.GameInfo.UserFirst = PlayerOrComputer.Computer;
                game.InitializeComputerMove(true, true);
                MappingVerification.WriteToFile("Challenge json" + Environment.NewLine);
                MappingVerification verification2 = new MappingVerification();
                verification2.MappingFirstLevel(game);
                if (verification2.elapsedTime != null) MappingVerification.WriteToFile(DebugHelper.PrintTimeTaken(verification2.elapsedTime.Value) + Environment.NewLine);
                MappingVerification.WriteToFile("Scenario errors: " + verification2.errorCount + Environment.NewLine + Environment.NewLine);
            }
        }

        /// <summary>
        /// Verify solution for all scenarios. 
        /// </summary>
        public static void VerifySolutionForAllScenarios(String gameSet, String level)
        {
            List<Func<Scenario, Game>> scenarioList = ScenarioHelper.GetScenarioDelegates(gameSet, level);
            Debug.WriteLine("Verify solution for: " + gameSet + ", " + level);
            for (int i = 0; i <= scenarioList.Count - 1; i++)
            {
                Game game = ReplicateGame(gameSet, level, i);
                Debug.WriteLine("Scenario " + (i + 1).ToString() + " :" + game.GameInfo.ScenarioName);
                if (game.GameInfo.solutionPoints.Count == 0) continue;
                List<Go.Point> solution = game.GameInfo.solutionPoints.First();

                game = ReplicateGame(gameSet, level, i);
                for (int j = 0; j <= solution.Count - 1; j++)
                {
                    game.heatMap = null;
                    List<GameTryMove> tryMoves = GameHelper.GetTryMovesForGame(game);
                    game.MakeMove(solution[j]);
                    Boolean isSolution = (game.Board.LastMoves.Count % 2 == 1);

                    //if solution not found in try moves then print to text
                    if (isSolution && !tryMoves.Any(tryMove => tryMove.Move.Equals(solution[j])))
                        DebugHelper.PrintBoardToText(game.Board, "VerifySolutionForRedundantMoves.txt");
                }
            }
        }

        /// <summary>
        /// Search answer for all scenarios.
        /// </summary>
        public static void SearchAnswerForAllScenarios(String gameSet, String level)
        {
            Game.SearchAnswer = true;
            List<Func<Scenario, Game>> scenarioList = ScenarioHelper.GetScenarioDelegates(gameSet, level);
            Debug.WriteLine("Search answer for: " + gameSet + ", " + level);
            for (int i = 0; i <= scenarioList.Count - 1; i++)
            {
                Game game = ReplicateGame(gameSet, level, i);
                Debug.WriteLine("Scenario " + (i + 1).ToString() + " : " + game.GameInfo.ScenarioName);
                DebugHelper.PrintToText("Scenario " + (i + 1).ToString() + " : " + game.GameInfo.ScenarioName, "SearchAnswerForAllScenarios.txt");

                //no solution for scenario
                if (game.GameInfo.solutionPoints.Count == 0) continue;

                //start monte carlo search
                (ConfirmAliveResult moveResult, Node answerNode, long? elapsedTime) = MonteCarloGame.MakeMonteCarloTreeSearch(game);

                //write results to file
                WriteSearchAnswersToFile(game, answerNode, elapsedTime, "SearchAnswerForAllScenarios.txt");
            }
            Game.SearchAnswer = false;
        }

        /// <summary>
        /// Write search answers to file.
        /// </summary>
        private static void WriteSearchAnswersToFile(Game game, Node answerNode, long? elapsedTime, String fileName)
        {
            DebugHelper.PrintBoardToText(game.Board, fileName);
            if (elapsedTime != null) File.AppendAllText(fileName, DebugHelper.PrintTimeTaken(elapsedTime.Value) + Environment.NewLine);
            Boolean solutionCorrect = game.GameInfo.solutionPoints.Any(s => s.First().Equals(game.Board.Move));
            if (game.Board.IsPassMove || !solutionCorrect)
            {
                File.AppendAllText(fileName, "Incorrect. Answer: " + game.GameInfo.solutionPoints.First().First());
                //include answer moves in file
                File.AppendAllText(fileName, MonteCarloGame.GetAnswerJson(game, answerNode));
            }
            else
            {
                File.AppendAllText(fileName, "Correct.");
                Debug.WriteLine("Correct.");
                Debug.WriteLine(MonteCarloGame.GetAnswerJson(game, answerNode));
            }
            File.AppendAllText(fileName, Environment.NewLine + Environment.NewLine);
        }
    }
}