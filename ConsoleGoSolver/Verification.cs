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
            MappingVerification.WriteToFile("Start from: " + DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + Environment.NewLine + Environment.NewLine);
            for (int i = 0; i <= scenarioList.Count - 1; i++)
            {
                //verify player json
                Game g = ReplicateGame(gameSet, level, i);
                g.GameInfo.UserFirst = PlayerOrComputer.Player;
                MappingVerification.WriteToFile("Scenario " + (i + 1).ToString() + " : " + g.GameInfo.ScenarioName + Environment.NewLine + "Player json" + Environment.NewLine);
                MappingVerification verification = new MappingVerification();
                verification.MappingFirstLevel(g);
                if (verification.elapsedTime != null) MappingVerification.WriteToFile(DebugHelper.PrintTimeTaken(verification.elapsedTime.Value) + Environment.NewLine);
                MappingVerification.WriteToFile("Scenario errors: " + verification.errorCount + Environment.NewLine + Environment.NewLine);

                if (g.GameInfo.solutionPoints.Count == 0) continue;
                //verify challenge json
                g.GameInfo.UserFirst = PlayerOrComputer.Computer;
                g.InitializeComputerMove(true, true);
                MappingVerification.WriteToFile("Challenge json" + Environment.NewLine);
                MappingVerification verification2 = new MappingVerification();
                verification2.MappingFirstLevel(g);
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
                Game g = ReplicateGame(gameSet, level, i);
                Debug.WriteLine("Scenario " + (i + 1).ToString() + " :" + g.GameInfo.ScenarioName);
                if (g.GameInfo.solutionPoints.Count == 0) continue;
                List<Go.Point> solution = g.GameInfo.solutionPoints.First();

                g = ReplicateGame(gameSet, level, i);
                for (int j = 0; j <= solution.Count - 1; j++)
                {
                    g.heatMap = null;
                    List<GameTryMove> tryMoves = GameHelper.GetTryMovesForGame(g);
                    g.MakeMove(solution[j]);
                    Boolean isSolution = (g.Board.LastMoves.Count % 2 == 1);

                    //if solution not found in try moves then print to text
                    if (isSolution && !tryMoves.Any(tryMove => tryMove.Move.Equals(solution[j])))
                        DebugHelper.PrintBoardToText(g.Board, "VerifySolutionForRedundantMoves.txt");
                }
            }
        }

        /// <summary>
        /// Search answer for all scenarios.
        /// </summary>
        public static void SearchAnswerForAllScenarios(String gameSet, String level)
        {
            String fileName = "SearchAnswerForAllScenarios.txt";
            Game.SearchAnswer = true;
            List<Func<Scenario, Game>> scenarioList = ScenarioHelper.GetScenarioDelegates(gameSet, level);
            Debug.WriteLine("Search answer for: " + gameSet + ", " + level);
            DebugHelper.PrintToText("Start from: " + DateTime.Now.ToString("yyyyMMdd HH:mm:ss"), fileName);
            for (int i = 0; i <= scenarioList.Count - 1; i++)
            {
                Game g = ReplicateGame(gameSet, level, i);
                Debug.WriteLine("Scenario " + (i + 1).ToString() + " : " + g.GameInfo.ScenarioName);
                DebugHelper.PrintToText("Scenario " + (i + 1).ToString() + " : " + g.GameInfo.ScenarioName, fileName);

                //no solution for scenario
                if (g.GameInfo.solutionPoints.Count == 0) continue;

                //start monte carlo search
                (ConfirmAliveResult moveResult, Node answerNode, long? elapsedTime) = MonteCarloGame.MakeMonteCarloTreeSearch(g);

                //write results to file
                WriteSearchAnswersToFile(g, answerNode, elapsedTime, fileName);
            }
            Game.SearchAnswer = false;
        }

        /// <summary>
        /// Write search answers to file.
        /// </summary>
        private static void WriteSearchAnswersToFile(Game g, Node answerNode, long? elapsedTime, String fileName)
        {
            DebugHelper.PrintBoardToText(g.Board, fileName);
            if (elapsedTime != null) File.AppendAllText(fileName, DebugHelper.PrintTimeTaken(elapsedTime.Value) + Environment.NewLine);
            Boolean solutionCorrect = g.GameInfo.solutionPoints.Any(s => s.First().Equals(g.Board.Move));
            if (!solutionCorrect)
            {
                File.AppendAllText(fileName, "Incorrect. Answer: " + g.GameInfo.solutionPoints.First().First());
                //include answer moves in file
                File.AppendAllText(fileName, MonteCarloGame.GetAnswerJson(g, answerNode));
            }
            else
            {
                File.AppendAllText(fileName, "Correct.");
                Debug.WriteLine("Correct.");
                Debug.WriteLine(MonteCarloGame.GetAnswerJson(g, answerNode));
            }
            File.AppendAllText(fileName, Environment.NewLine + Environment.NewLine);
        }
    }
}