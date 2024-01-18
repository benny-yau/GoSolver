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
        /// Game sets. 
        /// </summary>
        public static List<GameSet> GameSets
        {
            get
            {
                if (gameSets == null)
                {
                    gameSets = new List<GameSet>();
                    gameSets.Add(new GameSet("Demo", new List<String>() { "Level 1", "Level 2", "Level 3" }));
                    gameSets.Add(new GameSet("Fundamentals-Corner", new List<String>() { "Attack", "Defense" }));
                    gameSets.Add(new GameSet("Fundamentals-Side", new List<String>() { "Attack", "Defense" }));
                    gameSets.Add(new GameSet("Classics-A", new List<String>(), "Classics-A  (Xuanxuan Qijing)"));
                    gameSets.Add(new GameSet("Classics-B", new List<String>(), "Classics-B  (GuanZi Pu)"));
                    gameSets.Add(new GameSet("Classics-C", new List<String>(), "Classics-C  (Go Seigen)"));
                    gameSets.Add(new GameSet("Classics-D", new List<String>(), "Classics-D  (Hashimoto Utaro)"));
                    gameSets.Add(new GameSet("Classics-E", new List<String>(), "Classics-E  (Kweon Kab-yong)"));

                    if (Game.debugMode)
                        gameSets.Add(new GameSet("Problem-Set", new List<String>() { "Fundamentals-Corner", "XuanXuanGo", "GuanZiPu", "GoSeigen", "HashimotoUtaro", "KweonKabyong" }));
                }
                return gameSets;
            }
        }

        /// <summary>
        /// Verify for all scenarios.
        /// </summary>
        public static void VerifyForAllScenarios(Action<String, String> action)
        {
            for (int i = 0; i <= ScenarioHelper.GameSets.Count - 1; i++)
            {
                GameSet gameSet = ScenarioHelper.GameSets[i];
                if (gameSet.Name == "Problem-Set") continue;
                if (gameSet.Levels.Count == 0)
                    action(gameSet.Name, "");
                else
                {
                    for (int j = 0; j <= gameSet.Levels.Count - 1; j++)
                    {
                        String level = gameSet.Levels[j];
                        action(gameSet.Name, level);
                    }
                }
            }
        }

        private static Dictionary<String, List<Func<Scenario, Game>>> scenarioDelegates = new Dictionary<String, List<Func<Scenario, Game>>>();

        /// <summary>
        /// Get scenario delegates.
        /// </summary>
        public static List<Func<Scenario, Game>> GetScenarioDelegates(String gameSet, String level)
        {
            String key = gameSet + "|" + level;
            if (scenarioDelegates.ContainsKey(key))
                return scenarioDelegates[key];
            else
            {
                List<Func<Scenario, Game>> scenarioList = ScenarioHelper.AddScenarios(gameSet, level);
                scenarioDelegates.Add(key, scenarioList);
                return scenarioList;
            }
        }

        public static Dictionary<String, List<Func<Scenario, Game>>> GetScenarioDelegates(String gameSet, List<String> levels)
        {
            Dictionary<String, List<Func<Scenario, Game>>> scenarios = new Dictionary<String, List<Func<Scenario, Game>>>();
            if (levels == null || levels.Count == 0)
                levels = new List<string>() { String.Empty };

            for (int i = 0; i <= levels.Count - 1; i++)
            {
                String level = levels[i];
                List<Func<Scenario, Game>> scenarioList = GetScenarioDelegates(gameSet, level);
                scenarios.Add(level, scenarioList);
            }
            return scenarios;
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
            else if (gameSet == "Fundamentals-Corner")
                AddFundamentalsCornerSet(scenarioList, level);
            else if (gameSet == "Fundamentals-Side")
                AddFundamentalsSideSet(scenarioList, level);
            else if (gameSet == "Classics-A")
                AddXuanXuanGoSet_Release(scenarioList);
            else if (gameSet == "Classics-B")
                AddGuanZiPuSet_Release(scenarioList);
            else if (gameSet == "Classics-C")
                AddWuQingYuanSet_Release(scenarioList);
            else if (gameSet == "Classics-D")
                AddWindAndTimeSet(scenarioList);
            else if (gameSet == "Classics-E")
                AddTianLongTuSet_Release(scenarioList);

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


        private static void AddFundamentalsCornerSet(List<Func<Scenario, Game>> scenarioList, String level)
        {
            if (level == "Attack")
            {
                scenarioList.Add(x => x.Scenario_Corner_A1());
                scenarioList.Add(x => x.Scenario_Corner_A3());
                scenarioList.Add(x => x.Scenario_Corner_A4());
                scenarioList.Add(x => x.Scenario_Corner_A5());
                scenarioList.Add(x => x.Scenario_Corner_A6());
                scenarioList.Add(x => x.Scenario_Corner_A8());
                scenarioList.Add(x => x.Scenario_Corner_A9_Ext());
                scenarioList.Add(x => x.Scenario_Corner_A11());
                scenarioList.Add(x => x.Scenario_Corner_A12());
                scenarioList.Add(x => x.Scenario_Corner_A23());
                scenarioList.Add(x => x.Scenario_Phenomena_Q25112());
                scenarioList.Add(x => x.Scenario_Corner_A27());
                scenarioList.Add(x => x.Scenario_Corner_A28());
                scenarioList.Add(x => x.Scenario_SiHuoDaQuan_CornerA29());
                scenarioList.Add(x => x.Scenario_Corner_A30());
                scenarioList.Add(x => x.Scenario_Corner_A33());
                scenarioList.Add(x => x.Scenario_Corner_A34());
                scenarioList.Add(x => x.Scenario_Corner_A35());
                scenarioList.Add(x => x.Scenario_Corner_A36());
                scenarioList.Add(x => x.Scenario_Corner_A37());
                scenarioList.Add(x => x.Scenario_Corner_A38());
                scenarioList.Add(x => x.Scenario_Corner_A39());
                scenarioList.Add(x => x.Scenario_Corner_A40());
                scenarioList.Add(x => x.Scenario_Corner_A41());
                scenarioList.Add(x => x.Scenario_Corner_A42());
                scenarioList.Add(x => x.Scenario_Corner_A43());
                scenarioList.Add(x => x.Scenario_Corner_A45());
                scenarioList.Add(x => x.Scenario_Nie32());
                scenarioList.Add(x => x.Scenario_Nie4());
                scenarioList.Add(x => x.Scenario_XuanXuanQiJing_A33());
                scenarioList.Add(x => x.Scenario_Corner_A49());
                scenarioList.Add(x => x.Scenario_Corner_A50());
                scenarioList.Add(x => x.Scenario_Corner_A51());
                scenarioList.Add(x => x.Scenario_Corner_A52());
                scenarioList.Add(x => x.Scenario_Corner_A53());
                scenarioList.Add(x => x.Scenario_Corner_A55());
                scenarioList.Add(x => x.Scenario_Corner_A56());
                scenarioList.Add(x => x.Scenario_Corner_A58());
                scenarioList.Add(x => x.Scenario_Corner_A59());
                scenarioList.Add(x => x.Scenario_Corner_A60());
                scenarioList.Add(x => x.Scenario_Corner_A61());
                scenarioList.Add(x => x.Scenario_Corner_A62());
                scenarioList.Add(x => x.Scenario_Corner_A63());
                scenarioList.Add(x => x.Scenario_Corner_A64());
                scenarioList.Add(x => x.Scenario_Corner_A65());
                scenarioList.Add(x => x.Scenario_Corner_A67());
                scenarioList.Add(x => x.Scenario_Corner_A68());
                scenarioList.Add(x => x.Scenario_Corner_A69());
                scenarioList.Add(x => x.Scenario_Corner_A71());
                scenarioList.Add(x => x.Scenario_Corner_A72());
                scenarioList.Add(x => x.Scenario_Corner_A74());
                scenarioList.Add(x => x.Scenario_Corner_A75());
                scenarioList.Add(x => x.Scenario_Corner_A79());
                scenarioList.Add(x => x.Scenario_Corner_A80());
                scenarioList.Add(x => x.Scenario_Corner_A82());
                scenarioList.Add(x => x.Scenario_XuanXuanGo_A6());
                scenarioList.Add(x => x.Scenario_Corner_A84());
                scenarioList.Add(x => x.Scenario_Corner_A85());
                scenarioList.Add(x => x.Scenario_Corner_A86());
                scenarioList.Add(x => x.Scenario_Corner_A87());
                scenarioList.Add(x => x.Scenario_Corner_A94());
                scenarioList.Add(x => x.Scenario_Corner_A95());
                scenarioList.Add(x => x.Scenario_Corner_A20());
                scenarioList.Add(x => x.Scenario_Corner_A21());
                scenarioList.Add(x => x.Scenario_Corner_A108());
                scenarioList.Add(x => x.Scenario_Corner_A109());
                scenarioList.Add(x => x.Scenario_Corner_A113());
                scenarioList.Add(x => x.Scenario_SiHuoDaQuan_CornerA117());
                scenarioList.Add(x => x.Scenario_XuanXuanGo_A15());
                scenarioList.Add(x => x.Scenario_Corner_A120());
                scenarioList.Add(x => x.Scenario_Corner_A123());
                scenarioList.Add(x => x.Scenario_Corner_A124());
                scenarioList.Add(x => x.Scenario_Corner_A125());
            }
            else if (level == "Defense")
            {
                scenarioList.Add(x => x.Scenario_Corner_B2());
                scenarioList.Add(x => x.Scenario_Corner_B4());
                scenarioList.Add(x => x.Scenario_Corner_B5());
                scenarioList.Add(x => x.Scenario_Corner_B6());
                scenarioList.Add(x => x.Scenario_Corner_B7());
                scenarioList.Add(x => x.Scenario_Corner_B8());
                scenarioList.Add(x => x.Scenario_Corner_B9());
                scenarioList.Add(x => x.Scenario_Corner_B11());
                scenarioList.Add(x => x.Scenario_Corner_B12());
                scenarioList.Add(x => x.Scenario_Corner_B13());
                scenarioList.Add(x => x.Scenario_Corner_B15());
                scenarioList.Add(x => x.Scenario_Corner_B16());
                scenarioList.Add(x => x.Scenario_Phenomena_B6());
                scenarioList.Add(x => x.Scenario_Phenomena_B8());
                scenarioList.Add(x => x.Scenario_Corner_B20());
                scenarioList.Add(x => x.Scenario_Corner_B21());
                scenarioList.Add(x => x.Scenario_Corner_A73_Ext1());
                scenarioList.Add(x => x.Scenario_Corner_B22());
                scenarioList.Add(x => x.Scenario_Corner_B23());
                scenarioList.Add(x => x.Scenario_Phenomena_B7());
                scenarioList.Add(x => x.Scenario_Corner_B24());
                scenarioList.Add(x => x.Scenario_Corner_B25());
                scenarioList.Add(x => x.Scenario_Corner_B28());
                scenarioList.Add(x => x.Scenario_Phenomena_Q25182());
                scenarioList.Add(x => x.Scenario_Phenomena_Q25185());
                scenarioList.Add(x => x.Scenario_Phenomena_B12());
                scenarioList.Add(x => x.Scenario_Corner_B29());
                scenarioList.Add(x => x.Scenario_Corner_B30());
                scenarioList.Add(x => x.Scenario_Corner_B31());
                scenarioList.Add(x => x.Scenario_Phenomena_B18());
                scenarioList.Add(x => x.Scenario_Corner_B33());
                scenarioList.Add(x => x.Scenario_Corner_B34());
                scenarioList.Add(x => x.Scenario_Corner_B36());
                scenarioList.Add(x => x.Scenario_Corner_B39());
                scenarioList.Add(x => x.Scenario_Corner_B40());
                scenarioList.Add(x => x.Scenario_Corner_B41());
                scenarioList.Add(x => x.Scenario_Corner_B42());
                scenarioList.Add(x => x.Scenario_Corner_B43());
            }
        }

        private static void AddFundamentalsSideSet(List<Func<Scenario, Game>> scenarioList, String level)
        {
            if (level == "Attack")
            {
                scenarioList.Add(x => x.Scenario_Side_A1());
                scenarioList.Add(x => x.Scenario_Side_A2());
                scenarioList.Add(x => x.Scenario_Side_A3());
                scenarioList.Add(x => x.Scenario_Side_A4());
                scenarioList.Add(x => x.Scenario_Side_A5());
                scenarioList.Add(x => x.Scenario_Side_A6());
                scenarioList.Add(x => x.Scenario_Side_A7());
                scenarioList.Add(x => x.Scenario_Side_A8());
                scenarioList.Add(x => x.Scenario_Side_A9());
                scenarioList.Add(x => x.Scenario_Side_A10());
                scenarioList.Add(x => x.Scenario_Side_A11());
                scenarioList.Add(x => x.Scenario_Side_A12());
                scenarioList.Add(x => x.Scenario_Side_A13());
                scenarioList.Add(x => x.Scenario_Side_A14());
                scenarioList.Add(x => x.Scenario_Side_A15());
                scenarioList.Add(x => x.Scenario_Side_A19());
                scenarioList.Add(x => x.Scenario_Side_A20());
                scenarioList.Add(x => x.Scenario_Side_A21());
                scenarioList.Add(x => x.Scenario_Side_A22());
                scenarioList.Add(x => x.Scenario_Side_A28());
                scenarioList.Add(x => x.Scenario_Side_A23());
                scenarioList.Add(x => x.Scenario_Side_A24());
                scenarioList.Add(x => x.Scenario_Side_A25());
                scenarioList.Add(x => x.Scenario_Side_A26());
                scenarioList.Add(x => x.Scenario_Side_A27());
            }
            else if (level == "Defense")
            {
                scenarioList.Add(x => x.Scenario_Side_B1());
                scenarioList.Add(x => x.Scenario_Side_B2());
                scenarioList.Add(x => x.Scenario_Side_B3());
                scenarioList.Add(x => x.Scenario_Side_B4());
                scenarioList.Add(x => x.Scenario_Side_B5());
                scenarioList.Add(x => x.Scenario_Side_B6());
                scenarioList.Add(x => x.Scenario_Side_B7());
                scenarioList.Add(x => x.Scenario_Side_B8());
                scenarioList.Add(x => x.Scenario_Side_B9());
                scenarioList.Add(x => x.Scenario_Side_B10());
                scenarioList.Add(x => x.Scenario_Side_B11());
                scenarioList.Add(x => x.Scenario_Side_B12());
                scenarioList.Add(x => x.Scenario_Side_B13());
                scenarioList.Add(x => x.Scenario_Side_B14());
                scenarioList.Add(x => x.Scenario_Side_B15());
                scenarioList.Add(x => x.Scenario_Side_B16());
                scenarioList.Add(x => x.Scenario_Side_B17());
                scenarioList.Add(x => x.Scenario_Side_B18());
                scenarioList.Add(x => x.Scenario_Side_B19());
                scenarioList.Add(x => x.Scenario_Side_B20());
                scenarioList.Add(x => x.Scenario_Side_B21());
                scenarioList.Add(x => x.Scenario_Side_B22());
                scenarioList.Add(x => x.Scenario_Side_B23());
                scenarioList.Add(x => x.Scenario_Side_B24());
                scenarioList.Add(x => x.Scenario_Side_B25());
                scenarioList.Add(x => x.Scenario_Side_B26());
                scenarioList.Add(x => x.Scenario_Side_B28());
                scenarioList.Add(x => x.Scenario_Side_B29());
                scenarioList.Add(x => x.Scenario_Side_B30());
                scenarioList.Add(x => x.Scenario_Side_B31());
                scenarioList.Add(x => x.Scenario_Side_B32());
                scenarioList.Add(x => x.Scenario_Phenomena_B35());
                scenarioList.Add(x => x.Scenario_Side_B33());
                scenarioList.Add(x => x.Scenario_Side_B34());
                scenarioList.Add(x => x.Scenario_Side_B35());
                scenarioList.Add(x => x.Scenario_Side_B36());
                scenarioList.Add(x => x.Scenario_Side_B37());
                scenarioList.Add(x => x.Scenario_Side_B38());
            }
        }



    }
}
