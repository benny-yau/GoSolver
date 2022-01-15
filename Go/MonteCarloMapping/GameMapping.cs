using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Go
{
    public static class GameMapping
    {
        public static Boolean OneStopMapping = Convert.ToBoolean(ConfigurationSettings.AppSettings["ONE_STOP_MAPPING"]);

        /// <summary>
        /// Retrieves the mapped json from GameInfo object.
        /// </summary>
        public static JArray GetMappedJson(Game game)
        {
            JArray mappedJson;
            Boolean mapPlayerMove = (game.GameInfo.UserFirst == PlayerOrComputer.Player);
            if (mapPlayerMove)
                mappedJson = (((JArray)game.GameInfo.PlayerMoveJsonExtension).Any()) ? game.GameInfo.PlayerMoveJsonExtension : game.GameInfo.PlayerMoveJson;
            else
                mappedJson = (((JArray)game.GameInfo.ChallengeMoveJsonExtension).Any()) ? game.GameInfo.ChallengeMoveJsonExtension : game.GameInfo.ChallengeMoveJson;
            return mappedJson;
        }

        /// <summary>
        /// Serialize json to string and write to file. It is possible to call from immediate window, even if mapping is not fully completed to retrieve partial json map.
        /// </summary>
        public static void SerializeJson(Game g)
        {
            dynamic json = GetMappedJson(g);
            String jsonString = JsonConvert.SerializeObject(json);
            String jsonFormatted = (GameMapping.OneStopMapping) ? jsonString : Regex.Replace(jsonString, "\"", "\\\"");
            String fileName = (g.GameInfo.UserFirst == PlayerOrComputer.Player) ? "\\playerJson.txt" : "\\challengeJson.txt";
            File.WriteAllText(Directory.GetCurrentDirectory() + fileName, jsonFormatted);
            FindPassMoveInJson(json);
        }

        /// <summary>
        /// Find if any pass move in json which are likely errors. Pass move can also be a valid move returned by computer move. 
        /// </summary>
        private static void FindPassMoveInJson(JArray playerMoveJson)
        {
            foreach (JToken firstLevelMove in playerMoveJson.Children())
            {
                if ((int)firstLevelMove["SecondMove"]["x"] == -1 && (int)firstLevelMove["SecondMove"]["y"] == -1)
                {
                    String a = "FirstMove: " + firstLevelMove["FirstMove"]["x"] + ", " + firstLevelMove["FirstMove"]["y"];
                    a += ", SecondMove: " + firstLevelMove["SecondMove"]["x"] + ", " + firstLevelMove["SecondMove"]["y"];
                    Debug.WriteLine(a);
                }
                if (firstLevelMove["SecondLevel"] == null) continue;
                JArray secondLevelList = (JArray)firstLevelMove["SecondLevel"];
                foreach (JToken secondLevelMove in secondLevelList)
                {
                    if ((int)secondLevelMove["FourthMove"]["x"] == -1 && (int)secondLevelMove["FourthMove"]["y"] == -1)
                    {
                        String b = "FirstMove: " + firstLevelMove["FirstMove"]["x"] + ", " + firstLevelMove["FirstMove"]["y"];
                        b += ", SecondMove: " + firstLevelMove["SecondMove"]["x"] + ", " + firstLevelMove["SecondMove"]["y"];
                        b += ", ThirdMove: " + secondLevelMove["ThirdMove"]["x"] + ", " + secondLevelMove["ThirdMove"]["y"];
                        b += ", FourthMove: " + secondLevelMove["FourthMove"]["x"] + ", " + secondLevelMove["FourthMove"]["y"];
                        Debug.WriteLine(b);
                    }

                    JArray thirdLevelList = (JArray)secondLevelMove["ThirdLevel"];
                    if (thirdLevelList == null) continue;

                    foreach (JToken thirdLevelMove in thirdLevelList)
                    {
                        if ((int)thirdLevelMove["SixthMove"]["x"] == -1 && (int)thirdLevelMove["SixthMove"]["y"] == -1)
                        {
                            String c = "FirstMove: " + firstLevelMove["FirstMove"]["x"] + ", " + firstLevelMove["FirstMove"]["y"];
                            c += ", SecondMove: " + firstLevelMove["SecondMove"]["x"] + ", " + firstLevelMove["SecondMove"]["y"];
                            c += ", ThirdMove: " + secondLevelMove["ThirdMove"]["x"] + ", " + secondLevelMove["ThirdMove"]["y"];
                            c += ", FourthMove: " + secondLevelMove["FourthMove"]["x"] + ", " + secondLevelMove["FourthMove"]["y"];
                            c += ", FifthMove: " + thirdLevelMove["FifthMove"]["x"] + ", " + thirdLevelMove["FifthMove"]["y"];
                            c += ", SixthMove: " + thirdLevelMove["SixthMove"]["x"] + ", " + thirdLevelMove["SixthMove"]["y"];

                            Debug.WriteLine(c);
                        }
                    }
                }
            }
        }
    }
}
