using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Go
{
    public class JsonHelper
    {
        /// <summary>
        /// Get mapped json.
        /// </summary>
        public static JArray GetMappedJson(Game g)
        {
            JArray mappedJson;
            GameInfo gi = g.GameInfo;
            Boolean mapPlayerMove = (gi.UserFirst == PlayerOrComputer.Player);
            if (mapPlayerMove)
                mappedJson = (((JArray)gi.PlayerMoveJsonExtension).Any()) ? gi.PlayerMoveJsonExtension : gi.PlayerMoveJson;
            else
                mappedJson = (((JArray)gi.ChallengeMoveJsonExtension).Any()) ? gi.ChallengeMoveJsonExtension : gi.ChallengeMoveJson;
            return mappedJson;
        }

        /// <summary>
        /// Serialize json.
        /// </summary>
        public static void SerializeJson(Game g)
        {
            dynamic json = GetMappedJson(g);
            String jsonString = JsonConvert.SerializeObject(json);
            String jsonFormatted = (MonteCarloMapping.ThreeLevelMapping) ? jsonString : Regex.Replace(jsonString, "\"", "\\\"");
            String fileName = (g.GameInfo.UserFirst == PlayerOrComputer.Player) ? "\\playerJson.txt" : "\\challengeJson.txt";
            File.WriteAllText(Directory.GetCurrentDirectory() + fileName, jsonFormatted);
            FindPassMoveInJson(json);
        }

        /// <summary>
        /// Find pass move in json. 
        /// </summary>
        private static void FindPassMoveInJson(JArray json)
        {
            foreach (JToken firstLevel in json.Children())
            {
                if ((int)firstLevel["SecondMove"]["x"] == -1 && (int)firstLevel["SecondMove"]["y"] == -1)
                {
                    String a = "FirstMove: " + firstLevel["FirstMove"]["x"] + ", " + firstLevel["FirstMove"]["y"];
                    a += ", SecondMove: " + firstLevel["SecondMove"]["x"] + ", " + firstLevel["SecondMove"]["y"];
                    Debug.WriteLine(a);
                }
                if (firstLevel["SecondLevel"] == null) continue;
                JArray secondLevelList = (JArray)firstLevel["SecondLevel"];
                foreach (JToken secondLevel in secondLevelList)
                {
                    if ((int)secondLevel["FourthMove"]["x"] == -1 && (int)secondLevel["FourthMove"]["y"] == -1)
                    {
                        String b = "FirstMove: " + firstLevel["FirstMove"]["x"] + ", " + firstLevel["FirstMove"]["y"];
                        b += ", SecondMove: " + firstLevel["SecondMove"]["x"] + ", " + firstLevel["SecondMove"]["y"];
                        b += ", ThirdMove: " + secondLevel["ThirdMove"]["x"] + ", " + secondLevel["ThirdMove"]["y"];
                        b += ", FourthMove: " + secondLevel["FourthMove"]["x"] + ", " + secondLevel["FourthMove"]["y"];
                        Debug.WriteLine(b);
                    }

                    JArray thirdLevelList = (JArray)secondLevel["ThirdLevel"];
                    if (thirdLevelList == null) continue;

                    foreach (JToken thirdLevel in thirdLevelList)
                    {
                        if ((int)thirdLevel["SixthMove"]["x"] == -1 && (int)thirdLevel["SixthMove"]["y"] == -1)
                        {
                            String c = "FirstMove: " + firstLevel["FirstMove"]["x"] + ", " + firstLevel["FirstMove"]["y"];
                            c += ", SecondMove: " + firstLevel["SecondMove"]["x"] + ", " + firstLevel["SecondMove"]["y"];
                            c += ", ThirdMove: " + secondLevel["ThirdMove"]["x"] + ", " + secondLevel["ThirdMove"]["y"];
                            c += ", FourthMove: " + secondLevel["FourthMove"]["x"] + ", " + secondLevel["FourthMove"]["y"];
                            c += ", FifthMove: " + thirdLevel["FifthMove"]["x"] + ", " + thirdLevel["FifthMove"]["y"];
                            c += ", SixthMove: " + thirdLevel["SixthMove"]["x"] + ", " + thirdLevel["SixthMove"]["y"];

                            Debug.WriteLine(c);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// First level mapping.
        /// </summary>
        public static JObject FirstLevelMapping(dynamic json, Point firstMovePt, Point secondMovePt)
        {
            JObject firstLevel = (JObject)((JArray)json).Where(m => (int)m["FirstMove"]["x"] == firstMovePt.x && (int)m["FirstMove"]["y"] == firstMovePt.y && (int)m["SecondMove"]["x"] == secondMovePt.x && (int)m["SecondMove"]["y"] == secondMovePt.y).FirstOrDefault();
            if (firstLevel == null)
            {
                firstLevel = new JObject
                    {
                        { "FirstMove", 
                            new JObject {
                                { "x", firstMovePt.x }, 
                                { "y", firstMovePt.y } 
                            }
                        },
                        { "SecondMove", 
                            new JObject {
                                { "x", secondMovePt.x }, 
                                { "y", secondMovePt.y } 
                            }
                        }
                    };
                json.Add(firstLevel);
            }
            return firstLevel;
        }

        /// <summary>
        /// Second level mapping.
        /// </summary>
        public static JObject SecondLevelMapping(JObject firstLevel, Point thirdMovePt, Point fourthMovePt)
        {
            JArray secondLevelList;
            if (firstLevel["SecondLevel"] == null)
            {
                secondLevelList = new JArray();
                firstLevel.Add("SecondLevel", secondLevelList);
            }
            else
            {
                secondLevelList = (JArray)firstLevel["SecondLevel"];
            }

            JObject secondLevel = (JObject)(secondLevelList.Where(m => (int)m["ThirdMove"]["x"] == thirdMovePt.x && (int)m["ThirdMove"]["y"] == thirdMovePt.y && (int)m["FourthMove"]["x"] == fourthMovePt.x && (int)m["FourthMove"]["y"] == fourthMovePt.y)).FirstOrDefault();

            if (secondLevel == null)
            {
                secondLevel = new JObject
                    {
                        { "ThirdMove", 
                            new JObject {
                                { "x", thirdMovePt.x }, 
                                { "y", thirdMovePt.y } 
                            }
                        },
                        { "FourthMove", 
                            new JObject {
                                { "x", fourthMovePt.x }, 
                                { "y", fourthMovePt.y } 
                            }
                        }
                    };
                secondLevelList.Add(secondLevel);
            }
            return secondLevel;
        }

        public static void SecondLevelMapping(JObject firstLevel, JArray secondLevel)
        {
            if (firstLevel["SecondLevel"] == null)
                firstLevel.Add("SecondLevel", secondLevel);
        }


        /// <summary>
        /// Third level mapping.
        /// </summary>
        public static JObject ThirdLevelMapping(JObject secondLevel, Point fifthMovePt, Point sixthMovePt)
        {
            JArray thirdLevelList;
            if (secondLevel["ThirdLevel"] == null)
            {
                thirdLevelList = new JArray();
                secondLevel.Add("ThirdLevel", thirdLevelList);
            }
            else
            {
                thirdLevelList = (JArray)secondLevel["ThirdLevel"];
            }
            JObject thirdLevel = new JObject
            {
                { "FifthMove", 
                    new JObject {
                        { "x", fifthMovePt.x }, 
                        { "y", fifthMovePt.y } 
                    }
                },
                { "SixthMove", 
                    new JObject {
                        { "x", sixthMovePt.x }, 
                        { "y", sixthMovePt.y } 
                    }
                }
            };
            thirdLevelList.Add(thirdLevel);
            return thirdLevel;
        }

    }
}
