using Go;
using ScenarioCollection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                        Boolean completed = PlayOneRound(g);
                        if (!completed)
                            break;
                        Console.WriteLine("\nDo you want to play the scenario again (y/n)?");
                        String play_again = Console.ReadLine();
                        if (play_again.ToLower() != "y")
                            break;
                    }
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
            foreach (GameSet gameSet in ScenarioHelper.GameSets)
            {
                if (gameSet.Name == "Problem-Set") continue;
                if (gameSet.Levels == null || gameSet.Levels.Count == 0)
                    gameSetList.Add(gameSet.Name);
                else
                    gameSet.Levels.ForEach(level => gameSetList.Add(gameSet.Name + " - " + level));
            }
            Console.WriteLine("Select scenario:");
            for (int i = 0; i <= gameSetList.Count - 1; i++)
            {
                Console.WriteLine((i + 1).ToString().PadLeft(3, ' ') + ": " + gameSetList[i]);
            }
            Console.WriteLine("\nSelect game set (1 to " + gameSetList.Count.ToString() + "):");

            int gameSetSelected = 0;
            do
            {
                String gameSetString = Console.ReadLine();
                if (Int32.TryParse(gameSetString, out gameSetSelected))
                {
                    if (gameSetSelected > 0 && gameSetSelected <= gameSetList.Count)
                        break;
                }
            } while (true);

            String selectedGameSet = gameSetList[gameSetSelected - 1];
            String[] gameSetLevel = selectedGameSet.Split(new string[] { " - " }, StringSplitOptions.None);

            //get all scenarios for gameset and level
            List<Func<Scenario, Game>> scenarioList = ScenarioHelper.AddScenarios(gameSetLevel[0].Trim(), (gameSetLevel.Length == 2) ? gameSetLevel[1].Trim() : "");

            //select scenario number
            Console.WriteLine("Select scenario number (1 to " + scenarioList.Count.ToString() + ") : ");
            int scenarioSelected = 0;
            do
            {
                String scenarioString = Console.ReadLine();
                if (Int32.TryParse(scenarioString, out scenarioSelected))
                {
                    if (scenarioSelected > 0 && scenarioSelected <= scenarioList.Count)
                        break;
                }
            } while (true);

            //return selected scenario
            return ScenarioHelper.GetScenarioFromList(scenarioList, scenarioSelected - 1);
        }

        static Boolean PlayOneRound(Game game)
        {
            Game g = new Game(game);
            Console.WriteLine("{0}", g.Board);
            Console.WriteLine("\nDo you place the first step? [y/n] (Get answer[a], Search answer[s], Exit[x])");
            //set player to start first
            String input = Console.ReadLine().ToLower();
            if (input == "s" || input == "search")
                SearchAnswer(g);
            else if (input == "a" || input == "answer")
                GetAnswer(g);
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
            else
            {
                if (input == "y")
                    g.GameInfo.UserFirst = PlayerOrComputer.Player;
                else if (input == "n")
                    g.GameInfo.UserFirst = PlayerOrComputer.Computer;
                else
                    return false;

                Boolean gameEnded = false;
                //make player move
                if (g.GameInfo.UserFirst == PlayerOrComputer.Player)
                    GetNextMoveFromUser(g);
                do
                {
                    //make computer move
                    gameEnded = ComputerMakeMove(g);
                    if (gameEnded)
                        break;
                    //get player move
                    if (!GetNextMoveFromUser(g))
                        return false;
                } while (true);
            }
            return true;
        }

        public static void GetAnswer(Game g)
        {
            if (g.GameInfo.solutionPoints.Count == 0)
                Console.WriteLine("No answers for this scenario.");

            List<Point> solution = g.GameInfo.solutionPoints.First();

            String msg = "";
            for (int i = 0; i <= solution.Count - 1; i++)
            {
                Point p = solution[i];
                msg += p;
                if (i < solution.Count - 1)
                    msg += ",";
            }
            Console.WriteLine("\nSolution: " + msg + "\n");
        }

        public static void SearchAnswer(Game g)
        {
            if (g.GameInfo.solutionPoints.Count == 0)
                Console.WriteLine("No answers for this scenario.");

            Console.WriteLine("Calculating...");
            Game.UseSolutionPoints = Game.UseMapMoves = false;
            MonteCarloMapping.mapMoves = true;
            MonteCarloMapping.useMappingRange = false;
            (ConfirmAliveResult moveResult, Node answerNode) = MonteCarloGame.MonteCarloRealTimeMove(g);

            Game.UseSolutionPoints = Game.UseMapMoves = true;
            MonteCarloMapping.mapMoves = false;
            MonteCarloMapping.useMappingRange = true;

            Console.WriteLine("{0}", g.Board);
            if (g.Board.Move != null)
            {
                Console.WriteLine("Move: {0}", g.Board.Move + "\n");
                Boolean solutionCorrect = g.GameInfo.solutionPoints.Any(s => s.First().Equals(g.Board.Move));
                if (solutionCorrect)
                    Console.WriteLine("Correct.");
                else
                    Console.WriteLine("Incorrect.");
            }
        }

        static Boolean ComputerMakeMove(Game g)
        {
            ConfirmAliveResult moveResult = g.InitializeComputerMove();
            Console.WriteLine(Environment.NewLine + "Computer move: ");
            Console.WriteLine("{0}", g.Board);
            if (g.Board.Move != null && !g.Board.Move.Equals(Game.PassMove))
                Console.WriteLine("Move: {0}", g.Board.Move + "\n");

            String msg = ResultHelper.GameEndedMessage(moveResult, g);
            if (!String.IsNullOrEmpty(msg))
            {
                Console.WriteLine("Result: {0}", msg + "\n");
                return true;
            }
            return false;
        }

        static Boolean GetNextMoveFromUser(Game g)
        {
            int x, y;
            bool parseX, parseY;
            MakeMoveResult result = MakeMoveResult.Unknown;
            do
            {
                Console.WriteLine("Enter x position: ");
                String input = Console.ReadLine();
                if (input == "x")
                    return false;
                parseX = Int32.TryParse(input, out x);
                Console.WriteLine("Enter y position: ");
                parseY = Int32.TryParse(Console.ReadLine(), out y);

                if (parseX && parseY)
                {
                    SurviveOrKill survivalOrKill = GameHelper.KillOrSurvivalForNextMove(g.Board);
                    List<Point> rangePoints = (survivalOrKill == SurviveOrKill.Kill) ? g.GameInfo.killMovablePoints : g.GameInfo.movablePoints;

                    if (!rangePoints.Contains(new Point(x, y)))
                    {
                        Console.WriteLine("Outside of movable range.");
                        continue;
                    }

                    Game m = new Game(g);
                    result = m.InternalMakeMove(x, y);
                    if (result != MakeMoveResult.Legal)
                        Console.WriteLine("Illegal move.");
                    else
                        break;
                }
            } while (true);
            g.MakeMove(x, y);
            Console.WriteLine(Environment.NewLine + "Your move: ");
            Console.WriteLine("{0}", g.Board);
            Console.WriteLine("Move: {0}", g.Board.Move + "\n");
            return true;
        }

    }
}
