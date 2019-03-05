using AssetsManager;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.MultipleLang
{
    public class Dictionary
    {
        internal static Dictionary<string, string> keyToString;

        internal static Lang lang;

        public static string GetString(string key)
        {
            if (lang != Lang.English) return keyToString[key];
            else return key;
        }

        public void InitDictionary()
        {
            int curLang = PlayerPrefs.GetInt("Lang");

            TextAsset textAsset = AssetsAgent.GetAsset<TextAsset>("Lang.json");
            string content = textAsset.text;
            Dictionary<string, object> locales = new Dictionary<string, object>();


            
        }
    }
}
