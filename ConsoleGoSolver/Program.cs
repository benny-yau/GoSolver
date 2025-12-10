using Go;
using ScenarioCollection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ConsoleGoSolver
{
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                while (true)
                {
                    Game g = GetScenarioGame();
                    while (true)
                    {
                        if (PlayOneRound(g))
                            break;
                    }
                    Console.WriteLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static Game GetScenarioGame()
        {
            //select game set and level
            List<String> gameSetList = new List<String>();
            ScenarioHelper.FindAllScenarios((gs, level) => gameSetList.Add(gs + (level == "" ? "" : " - ") + level));
            Console.WriteLine("Select scenario:");
            for (int i = 0; i <= gameSetList.Count - 1; i++)
                Console.WriteLine((i + 1).ToString().PadLeft(3, ' ') + ": " + gameSetList[i]);

            Console.WriteLine("\nSelect game set (1 to " + gameSetList.Count.ToString() + "):");

            int gameSet = SelectFromList<String>(gameSetList);
            String selected = gameSetList[gameSet - 1];
            String[] rc = selected.Split(new string[] { " - " }, StringSplitOptions.None);

            //get all scenarios for gameset and level
            List<Func<Scenario, Game>> scenarioList = ScenarioHelper.AddScenarios(rc[0], (rc.Length == 2) ? rc[1] : "");

            //select scenario number
            Console.WriteLine("Select scenario number (1 to " + scenarioList.Count.ToString() + ") : ");
            int scenario = SelectFromList<Func<Scenario, Game>>(scenarioList);

            //return selected scenario
            return ScenarioHelper.GetScenarioFromList(scenarioList, scenario - 1);
        }

        static int SelectFromList<T>(List<T> list)
        {
            do
            {
                int selected = 0;
                String input = Console.ReadLine();
                if (Int32.TryParse(input, out selected))
                {
                    if (selected > 0 && selected <= list.Count)
                        return selected;
                }
            } while (true);
        }

        static Boolean PlayOneRound(Game g)
        {
            Console.WriteLine("{0}", g.Board);
            Console.WriteLine("\n" + g.GameInfo.StartContent.ToString() + " to move.");
            Boolean koToWin = (g.GameInfo.Survival == SurviveOrKill.KillWithKo || g.GameInfo.Survival == SurviveOrKill.SurviveWithKo);
            Console.WriteLine(koToWin ? "Ko to win." : "");
            Console.WriteLine("Do you place the first step? [y/n] (Get answer[a], Search answer[s])");
            String input = Console.ReadLine().ToLower();
            if (input == "")
                return true;
            else if (input == "y" || input == "n")
            {
                g.GameInfo.UserFirst = (input == "y") ? PlayerOrComputer.Player : PlayerOrComputer.Computer;

                //make player move
                if (g.GameInfo.UserFirst == PlayerOrComputer.Player)
                    GetNextMoveFromUser(g);
                do
                {
                    //make computer move
                    if (ComputerMakeMove(g))
                        return true;
                    //get player move
                    if (!GetNextMoveFromUser(g))
                        return true;
                } while (true);
            }
            else if (input == "a" || input == "answer")
                GetAnswer(g);
            else if (input == "s" || input == "search")
            {
                SearchAnswer(g);
                return true;
            }
            else if (input == "r" || input == "move")
                GetUserInput(g);
            else if (input == "e" || input == "movablepoints")
            {
                List<Point> movablePoints = GameHelper.GetMovablePoints(g.Board);
                Console.WriteLine(DebugHelper.ShowPointsInBoard(g, movablePoints));
            }
            else if (input == "t" || input == "trymoves")
                Console.WriteLine(DebugHelper.ShowTryMoves(g));
            else if (input == "m" || input == "mapping")
            {
                MonteCarloMapping.MapScenario(g);
                Console.WriteLine("Mapping completed.");
            }
            else if (input == "v" || input == "verification")
            {
                int error = MappingVerification.VerifyScenario(g);
                Console.WriteLine("Verification completed. Errors: " + error);
            }
            else if (input == "vs" || input == "verify_solution")
                ScenarioHelper.FindAllScenarios(Verification.VerifySolutionForAllScenarios);
            else if (input == "va" || input == "verify_all")
                ScenarioHelper.FindAllScenarios(Verification.VerifyMappedJson);
            else if (input == "sa" || input == "search_all")
                ScenarioHelper.FindAllScenarios(Verification.SearchAnswerForAllScenarios);
            return false;
        }

        public static void GetAnswer(Game g)
        {
            if (g.GameInfo.solutionPoints.Count == 0)
                Console.WriteLine("No answers for this scenario.");

            List<Point> solution = g.GameInfo.solutionPoints.First();
            Console.WriteLine("\nSolution: " + solution.GetConcatenatedString() + "\n");
        }

        public static void SearchAnswer(Game g)
        {
            Boolean start = g.Board.LastMoves.Count == 0;
            if (start && g.GameInfo.solutionPoints.Count == 0)
            {
                Console.WriteLine("No answers for this scenario.");
                return;
            }
            Console.WriteLine("Calculating...");
            Game.SearchAnswer = true;
            (ConfirmAliveResult moveResult, Node answerNode, long? elapsedTime) = MonteCarloGame.MakeMonteCarloTreeSearch(g);
            Game.SearchAnswer = false;
            Console.WriteLine("{0}", g.Board);
            Boolean solutionCorrect = false;
            if (g.Board.Move != null)
            {
                Console.WriteLine("Move: {0}", g.Board.Move + "\n");
                solutionCorrect = g.GameInfo.solutionPoints.Any(s => s.First().Equals(g.Board.Move));
                if (start && solutionCorrect)
                    Console.WriteLine("Correct.");
            }

            if (start && !solutionCorrect)
                Console.WriteLine("Incorrect. Answer: " + g.GameInfo.solutionPoints.First().First());
            if (elapsedTime != null)
                Console.WriteLine(DebugHelper.PrintTimeTaken(elapsedTime.Value));
        }

        static Boolean ComputerMakeMove(Game g)
        {
            ConfirmAliveResult moveResult = g.InitializeComputerMove(true, true);
            Console.WriteLine(Environment.NewLine + "Computer move: ");
            Console.WriteLine("{0}", g.Board);
            if (g.Board.Move != null && !g.Board.IsPassMove)
                Console.WriteLine("Move: {0}", g.Board.Move + "\n");

            String msg = SolutionHelper.GameEndedMessage(moveResult, g);
            if (!String.IsNullOrEmpty(msg))
            {
                Console.WriteLine("Result: {0}", msg + "\n");
                return true;
            }
            return false;
        }

        static Boolean GetNextMoveFromUser(Game g)
        {
            do
            {
                (Boolean result, Point? move) = GetUserInput(g);
                if (!result) continue;
                if (move == null) return false;
                break;
            } while (true);
            Console.WriteLine(Environment.NewLine + "Your move: ");
            Console.WriteLine("{0}", g.Board);
            Console.WriteLine("Move: {0}", g.Board.Move + "\n");
            return true;
        }

        static (Boolean, Point?) GetUserInput(Game g)
        {
            int x, y;
            Console.WriteLine("Enter x position: ");
            String input = Console.ReadLine();
            bool parseX = Int32.TryParse(input, out x);
            if (input == "")
                return (true, null);
            Console.WriteLine("Enter y position: ");
            bool parseY = Int32.TryParse(Console.ReadLine(), out y);

            if (parseX && parseY)
            {
                if (!GameHelper.GetMovablePoints(g.Board).Contains(new Point(x, y)))
                {
                    Console.WriteLine("Outside of movable range.");
                    return (false, null);
                }
                MakeMoveResult result = g.InternalMakeMove(x, y);
                if (result == MakeMoveResult.KoBlocked)
                {
                    Console.WriteLine("Ko blocked move.");
                    return (false, null);
                }
                else if (result != MakeMoveResult.Legal)
                {
                    Console.WriteLine("Illegal move.");
                    return (false, null);
                }
                return (true, g.Board.Move);
            }
            return (false, null);
        }
    }
}
