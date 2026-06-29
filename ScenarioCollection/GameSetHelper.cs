using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Go;

namespace ScenarioCollection
{
    public static class GameSetHelper
    {
        public static Dictionary<String, List<Game>> GameSets = new Dictionary<String, List<Game>>();

        /// <summary>
        /// Get all scenarios without loading json mapping.
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
        /// Check duplicates.
        /// </summary>
        public static void CheckDuplicates(GameInfo gi)
        {
            if (!Game.debugMode) return;
            if (gi.SetupMoves.Count != gi.SetupMoves.Distinct().Count())
                throw new Exception("Duplicate setup point found");

            if (gi.movablePoints.Count != gi.movablePoints.Distinct().Count())
                throw new Exception("Duplicate movable point found");

            if (gi.killMovablePoints.Count != gi.killMovablePoints.Distinct().Count())
                throw new Exception("Duplicate kill movable point found");
        }
    }
}
