using Go;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScenarioCollection
{
    public class GameSet
    {
        public String Name;
        public String Description;
        public List<String> Levels; 

        public GameSet(String name, List<String> levels, String description = null)
        {
            this.Name = name;
            this.Description = (description == null) ? name : description;
            this.Levels = levels;
        }

        public override string ToString()
        {
            return this.Description;
        }
    }

    public partial class ScenarioHelper
    {
        private static List<GameSet> gameSets;
        /// <summary>
        /// List of game set with description and levels. Problem set included for debug mode.
        /// </summary>
        public static List<GameSet> GameSets
        {
            get
            {
                if (gameSets == null)
                {
                    gameSets = new List<GameSet>();
                    gameSets.Add(new GameSet("Demo", new List<String>() { "Level 1", "Level 2", "Level 3" }));
                    gameSets.Add(new GameSet("Classics-A", new List<String>(), "Classics-A  (Xuanxuan Qijing)"));
                }
                return gameSets;
            }
        }

        /// <summary>
        /// Get scenario from list with full loading of json map.
        /// </summary>
        public static Game GetScenarioFromList(List<Func<Scenario, Game>> scenarioList, int scenarioNumber)
        {
            lock (GameInfo._lockFullLoading)
            {
                Scenario s = new Scenario();
                GameInfo.EnableFullLoading = true;
                Func<Scenario, Game> handler = scenarioList[scenarioNumber];
                Game g = handler(s);
                return g;
            }
        }

        /// <summary>
        /// Add all scenario handlers for each game set and level.
        /// </summary>
        public static List<Func<Scenario, Game>> AddScenarios(string gameSet, String level = "")
        {
            List<Func<Scenario, Game>> scenarioList = new List<Func<Scenario, Game>>();
            if (gameSet == "Demo")
                AddDemoSet_Release(scenarioList, level);
            else if (gameSet == "Classics-A")
                AddXuanXuanGoSet_Release(scenarioList);
            
            return scenarioList;
        }


        private static void AddDemoSet_Release(List<Func<Scenario, Game>> scenarioList, String level)
        {
            if (level == "Level 1")
            {
                scenarioList.Add(x => x.Scenario6kyu15());
                scenarioList.Add(x => x.Scenario6kyu32());
                scenarioList.Add(x => x.Scenario6kyu16());
                scenarioList.Add(x => x.Scenario5kyu29());
                scenarioList.Add(x => x.Scenario6kyu13());
                scenarioList.Add(x => x.Scenario7kyu26());
                scenarioList.Add(x => x.Scenario7kyu31());
                scenarioList.Add(x => x.Scenario6kyu9());
                scenarioList.Add(x => x.Scenario7kyu25());
                scenarioList.Add(x => x.Scenario5kyu21());
                scenarioList.Add(x => x.Scenario4kyu28());
                scenarioList.Add(x => x.Scenario3kyu10());
                scenarioList.Add(x => x.Scenario6kyu25());
                scenarioList.Add(x => x.Scenario3kyu15());
                scenarioList.Add(x => x.Scenario3kyu24());
                scenarioList.Add(x => x.Scenario3kyu25());
                scenarioList.Add(x => x.Scenario3kyu28());
                scenarioList.Add(x => x.Scenario2kyu13());
                scenarioList.Add(x => x.Scenario2kyu19());
                scenarioList.Add(x => x.Scenario1kyu11());
                scenarioList.Add(x => x.Scenario1kyu25());
                scenarioList.Add(x => x.Scenario1dan10());
                scenarioList.Add(x => x.Scenario1dan31());
                scenarioList.Add(x => x.Scenario1dan21());
                scenarioList.Add(x => x.Scenario1dan29()); 
            }
            else if (level == "Level 2")
            {
                scenarioList.Add(x => x.Scenario2dan15());
                scenarioList.Add(x => x.Scenario_Nie4());
                scenarioList.Add(x => x.Scenario_Nie19());
                scenarioList.Add(x => x.Scenario2dan8());
                scenarioList.Add(x => x.Scenario2dan21());
                scenarioList.Add(x => x.Scenario3dan8());
                scenarioList.Add(x => x.Scenario1dan20());
                scenarioList.Add(x => x.Scenario2kyu18());
                scenarioList.Add(x => x.Scenario1dan4());
                scenarioList.Add(x => x.Scenario1kyu29());
                scenarioList.Add(x => x.Scenario1dan19());
                scenarioList.Add(x => x.Scenario_Nie74());
                scenarioList.Add(x => x.Scenario3dan22());
                scenarioList.Add(x => x.Scenario3dan23());
                scenarioList.Add(x => x.Scenario3dan18());
                scenarioList.Add(x => x.Scenario_Nie20());
                scenarioList.Add(x => x.Scenario_AncientJapanese_B6());
                scenarioList.Add(x => x.Scenario4dan17());
                scenarioList.Add(x => x.Scenario4dan18());
                scenarioList.Add(x => x.Scenario_SiHuoDaQuan_CornerA29());
                scenarioList.Add(x => x.Scenario_Nie32());
                scenarioList.Add(x => x.Scenario_Nie67());
                scenarioList.Add(x => x.Scenario_Nie73());
                scenarioList.Add(x => x.Scenario3dan17());
                scenarioList.Add(x => x.Scenario_Nie50()); 
            }
            else if (level == "Level 3")
            {
                scenarioList.Add(x => x.Scenario_Nie1());
                scenarioList.Add(x => x.Scenario_AncientJapanese_A7());
                scenarioList.Add(x => x.Scenario_AncientJapanese_A30());
                scenarioList.Add(x => x.Scenario4dan13());
                scenarioList.Add(x => x.Scenario4dan29());
                scenarioList.Add(x => x.Scenario5dan9());
                scenarioList.Add(x => x.Scenario_Nie61());
                scenarioList.Add(x => x.Scenario_Nie71());
                scenarioList.Add(x => x.Scenario_Nie166());
                scenarioList.Add(x => x.Scenario_Nie60());
                scenarioList.Add(x => x.Scenario_Nie63());
                scenarioList.Add(x => x.Scenario_Nie87());
                scenarioList.Add(x => x.Scenario_Nie100());
                scenarioList.Add(x => x.Scenario_Nie109());
                scenarioList.Add(x => x.Scenario_Nie114());
                scenarioList.Add(x => x.Scenario5dan27());
                scenarioList.Add(x => x.Scenario4dan10());
                scenarioList.Add(x => x.Scenario_Nie66());
                scenarioList.Add(x => x.Scenario_Nie137());
                scenarioList.Add(x => x.Scenario5dan18());
                scenarioList.Add(x => x.Scenario5dan25());
                scenarioList.Add(x => x.ScenarioHighLevel8());
                scenarioList.Add(x => x.ScenarioHighLevel18());
                scenarioList.Add(x => x.ScenarioHighLevel23());
                scenarioList.Add(x => x.ScenarioHighLevel32());
                scenarioList.Add(x => x.Scenario_Corner_A128());
                scenarioList.Add(x => x.Scenario_Corner_A132());
                scenarioList.Add(x => x.Scenario_Corner_A136());
            }
        }



    }
}
