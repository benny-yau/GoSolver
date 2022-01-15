using ScenarioCollection;
using Go;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ScenarioCollection
{
    public static class GameSetHelper
    {
        public static Dictionary<String, List<Game>> GameSets = new Dictionary<String, List<Game>>();

        /// <summary>
        /// Get all scenarios and return only the required properties of the scenarios.
        /// </summary>
        public static List<dynamic> GetAllLevelSetups(String gameSet, String level)
        {
            List<Game> Scenarios = GetAllScenarios(gameSet, level);

            List<dynamic> SetupList = new List<dynamic>();
            for (int i = 0; i <= Scenarios.Count - 1; i++)
            {
                Game g = Scenarios[i];
                GameInfo gi = g.GameInfo;
                CheckScenarioInfo(i, gi);

                //CurrentSetup (synchronise with web)
                dynamic Setup = new ExpandoObject();
                Setup.Game = g;
                Setup.GameSet = gameSet;
                Setup.Level = level;
                Setup.ScenarioNumber = i;
                Setup.Survival = gi.Survival;
                Setup.StartContent = gi.StartContent;
                Setup.SetupMoves = gi.SetupMoves;
                Setup.solutionPoints = gi.solutionPoints;
                Setup.movablePoints = gi.movablePoints;
                Setup.killMovablePoints = gi.killMovablePoints;
                Setup.ScenarioName = gi.ScenarioName;
                Setup.solved = false;
                Setup.attempts = 0;

                SetupList.Add(Setup);
            }
            return SetupList;
        }

        /// <summary>
        /// Retrieve all scenarios based on game set and level without loading json mapping.
        /// </summary>
        public static List<Game> GetAllScenarios(String gameSet, String level = "")
        {
            String gameSetAndLevel = gameSet + "|" + level;
            List<Game> Scenarios = new List<Game>();

            if (!GameSets.ContainsKey(gameSetAndLevel))
            {
                Stopwatch watch = new Stopwatch();
                if (Game.debugMode)
                    watch.Start();

                List<Func<Scenario, Game>> scenarioList = ScenarioHelper.AddScenarios(gameSet, level);
                Scenario s = new Scenario();
                lock (GameInfo._lockFullLoading)
                {
                    //prevent loading json maps
                    GameInfo.EnableFullLoading = false;
                    for (int i = 0; i <= scenarioList.Count - 1; i++)
                    {
                        //add all games to list
                        Func<Scenario, Game> handler = scenarioList[i];
                        Game g = handler(s);
                        CheckDuplicates(g.GameInfo);
                        Scenarios.Add(g);
                    }
                    GameInfo.EnableFullLoading = true;
                }
                //add to cache
                GameSets.Add(gameSetAndLevel, Scenarios);

                if (Game.debugMode)
                {
                    watch.Stop();
                    Debug.Print("Time taken to load scenarios: " + watch.ElapsedMilliseconds);
                }
            }
            else
            {
                //retrieve from cache
                Scenarios = GameSets[gameSetAndLevel];
            }
            return Scenarios;
        }

        /// <summary>
        /// Check duplicates of setup and movable points on debug mode.
        /// </summary>
        public static void CheckDuplicates(GameInfo gi)
        {
            if (Game.debugMode)
            {
                if (gi.SetupMoves.Count != gi.SetupMoves.Distinct().Count())
                    throw new Exception("Duplicate setup point found");

                if (gi.movablePoints.Count != gi.movablePoints.Distinct().Count())
                    throw new Exception("Duplicate movable point found");

                if (gi.killMovablePoints.Count != gi.killMovablePoints.Distinct().Count())
                    throw new Exception("Duplicate kill movable point found");
            }
        }

        /// <summary>
        /// Retrieve scenario info only on debug mode.
        /// </summary>
        private static void CheckScenarioInfo(int i, GameInfo gi)
        {

            if (Game.debugMode)
            {
                int solutionPt = (gi.solutionPoints.Count == 0 ? 0 : gi.solutionPoints.Min(p => p.Count()));
                Debug.WriteLine(i + " " + gi.ScenarioName + ", Survival: " + gi.Survival + ", Content: " + gi.StartContent + ", Target: " + gi.targetPoints.Count + ", Solutions: " + gi.solutionPoints.Count() + ", SolutionPt: " + solutionPt + ", Extension: " + ((((JArray)gi.PlayerMoveJsonExtension).Count() > 0) ? "true" : "false") + ", SearchDepth: " + gi.SearchDepth);

                if (gi.solutionPoints.Count() > 1)
                {
                    foreach (List<Point> solution in gi.solutionPoints)
                    {
                        for (int h = 0; h <= gi.solutionPoints[0].Count - 1; h++)
                        {
                            if (!gi.solutionPoints[0][h].Equals(solution[h]))
                            {
                                if (h % 2 == 1)
                                    Debug.WriteLine("Challenge Solution Found.");
                                break;
                            }
                            if (solution.Count == h - 1)
                                break;
                        }
                    }
                }

                if (gi.RuntimeScript_KillMove != null || gi.RuntimeScript_SurvivalMove != null)
                {
                    Debug.WriteLine(gi.ScenarioName + " contains scripts. ");
                }
            }
        }
    }
}
