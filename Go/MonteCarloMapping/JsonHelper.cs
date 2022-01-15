using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Go
{
    /// <summary>
    /// Creates the json map that extends up to three levels.
    /// </summary>
    public class JsonHelper
    {
        /// <summary>
        /// Create or retrieve first level move in json.
        /// </summary>
        public static JObject FirstLevelMapping(dynamic json, Point firstMovePt, Point secondMovePt)
        {
            JObject firstLevelMove = (JObject)((JArray)json).Where(m => (int)m["FirstMove"]["x"] == firstMovePt.x && (int)m["FirstMove"]["y"] == firstMovePt.y && (int)m["SecondMove"]["x"] == secondMovePt.x && (int)m["SecondMove"]["y"] == secondMovePt.y).FirstOrDefault();
            if (firstLevelMove == null)
            {
                firstLevelMove = new JObject
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
                json.Add(firstLevelMove);
            }
            return firstLevelMove;
        }

        /// <summary>
        /// Create or retrieve second level move in json.
        /// </summary>
        public static JObject SecondLevelMapping(JObject firstLevelMove, Point thirdMovePt, Point fourthMovePt)
        {
            JArray secondLevelList;
            if (firstLevelMove["SecondLevel"] == null)
            {
                secondLevelList = new JArray();
                firstLevelMove.Add("SecondLevel", secondLevelList);
            }
            else
            {
                secondLevelList = (JArray)firstLevelMove["SecondLevel"];
            }

            JObject secondLevelMove = (JObject)(secondLevelList.Where(m => (int)m["ThirdMove"]["x"] == thirdMovePt.x && (int)m["ThirdMove"]["y"] == thirdMovePt.y && (int)m["FourthMove"]["x"] == fourthMovePt.x && (int)m["FourthMove"]["y"] == fourthMovePt.y)).FirstOrDefault();

            if (secondLevelMove == null)
            {
                secondLevelMove = new JObject
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
                secondLevelList.Add(secondLevelMove);
            }
            return secondLevelMove;
        }

        public static void SecondLevelMapping(JObject firstLevelMove, JArray secondLevelMove)
        {
            if (firstLevelMove["SecondLevel"] == null)
                firstLevelMove.Add("SecondLevel", secondLevelMove);
        }


        /// <summary>
        /// Create or retrieve third level move in json.
        /// </summary>
        public static JObject ThirdLevelMapping(JObject secondLevelMove, Point fifthMovePt, Point sixthMovePt)
        {
            JArray thirdLevelList;
            if (secondLevelMove["ThirdLevel"] == null)
            {
                thirdLevelList = new JArray();
                secondLevelMove.Add("ThirdLevel", thirdLevelList);
            }
            else
            {
                thirdLevelList = (JArray)secondLevelMove["ThirdLevel"];
            }
            JObject thirdLevelMove = new JObject
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
            thirdLevelList.Add(thirdLevelMove);
            return thirdLevelMove;
        }

    }
}
