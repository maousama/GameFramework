using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.MultipleLang
{
    [CreateAssetMenu(fileName = "LangSetting", menuName = "ScriptableObject/LangSetting")]
    public class Setting : ScriptableObject
    {
        private static Setting instance;

        public Action OnLanguageChange;

        [SerializeField]
        internal Lang lang;

        internal Dictionary<string, string> keyToString;

        public static Setting Instance
        {
            get
            {
                if (!instance) instance = AssetsAgent.GetAsset<Setting>("LangSetting");
                return instance;
            }
        }

        public void Set(Lang lang)
        {
            if (this.lang == lang) return;
            this.lang = lang;
            UpdateDictionary();
            OnLanguageChange();
        }

        private void UpdateDictionary()
        {
            int langInt = (int)lang;
            if (lang == Lang.English)
            {
                keyToString.Clear();
            }
            else
            {
                TextAsset textAsset = AssetsAgent.GetAsset<TextAsset>("Lang/Lang");
                string content = textAsset.text;
                Dictionary<string, string>[] allData = JsonConvert.DeserializeObject<Dictionary<string, string>[]>(content);
                keyToString = allData[langInt];
            }
        }
    }
}
