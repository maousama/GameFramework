using AssetsManager;
using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;
namespace Assets.Scripts.MultipleLang
{
    public class Dictionary
    {
        internal static Lang lang;

        private static Dictionary<string, string> keyToString;

        public static string GetString(string key)
        {
            if (lang != Lang.English) return keyToString[key];
            else return key;
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize()
        {
            PlayerPrefs.SetInt("Lang", 0);
            int curLang = PlayerPrefs.GetInt("Lang");
            lang = (Lang)curLang;

            if (lang != Lang.English)
            {
                keyToString = new Dictionary<string, string>();

                TextAsset textAsset = AssetsAgent.GetAsset<TextAsset>("Lang/Lang");
                string content = textAsset.text;

                Dictionary<string, string>[] allData = JsonConvert.DeserializeObject<Dictionary<string, string>[]>(content);
            }

        }
    }

}
