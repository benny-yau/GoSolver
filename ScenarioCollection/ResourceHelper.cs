using Go;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;

namespace ScenarioCollection
{
    public class ResourceHelper
    {

        private static Dictionary<String, ResourceManager> resourceManagers = new Dictionary<String, ResourceManager>();

        public static String GetXuanXuanQiJingMappedJsonExtensionString(String key)
        {
            return GetResourceString(key, "ScenarioCollection.Resources.XuanXuanQiJing_MappedJsonExtension");
        }
        public static String GetGuanZiPuMappedJsonExtensionString(String key)
        {
            return GetResourceString(key, "ScenarioCollection.Resources.GuanZiPu_MappedJsonExtension");
        }

        public static String GetGoSeigenMappedJsonExtensionString(String key)
        {
            return GetResourceString(key, "ScenarioCollection.Resources.GoSeigen_MappedJsonExtension");
        }

        public static String GetHashimotoUtaroMappedJsonExtensionString(String key)
        {
            return GetResourceString(key, "ScenarioCollection.Resources.HashimotoUtaro_MappedJsonExtension");
        }

        public static String GetKweonKabYongMappedJsonExtensionString(String key)
        {
            return GetResourceString(key, "ScenarioCollection.Resources.KweonKabYong_MappedJsonExtension");
        }

        public static String GetKweonKabYongMappedJsonString(String key)
        {
            return GetResourceString(key, "ScenarioCollection.Resources.KweonKabYong_MappedJson");
        }

        public static String GetMappedJsonExtensionString(String key)
        {
            return GetResourceString(key, "ScenarioCollection.Resources.MappedJsonExtension");
        }

        public static String GetMappedJsonString(String key)
        {
            return GetResourceString(key, "ScenarioCollection.Resources.MappedJson");
        }

        /// <summary>
        /// Get full mapping from resource strings stored in resx files.
        /// </summary>
        public static String GetResourceString(String key, String path)
        {
            //return full mapping only on replicate game on player move at start of scenario
            if (!GameInfo.EnableFullLoading)
                return String.Empty;

            //add to list of resource managers for each of the game sets
            ResourceManager resourceManager;
            if (!resourceManagers.ContainsKey(path))
            {
                resourceManager = new ResourceManager(path, Assembly.GetExecutingAssembly());
                resourceManagers.Add(path, resourceManager);
            }
            else
            {
                resourceManager = resourceManagers[path];
            }
            return resourceManager.GetString(key);
        }
    }
}
