using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Assets.Scripts.MultipleLang
{
    [CreateAssetMenu(fileName = "LangSetting", menuName = "ScriptableObject/LangSetting")]
    public class Setting : ScriptableObject
    {
        private static Setting instance;

        internal Dictionary<Text, string> textToKey = new Dictionary<Text, string>();
        internal Dictionary<string, string> keyToString;
        [SerializeField, ShowProperty]
        private Lang lang;

        public static Setting Instance
        {
            get
            {
                if (!instance) instance = AssetsAgent.GetAsset<Setting>("Setting/LangSetting");
                return instance;
            }
        }

        public Lang Lang
        {
            get { return lang; }
            set
            {
                if (lang == value) return;
                lang = value;
                UpdateDictionary();
                if (lang == Lang.English) foreach (Text text in textToKey.Keys) text.text = textToKey[text];
                else foreach (Text text in textToKey.Keys) text.text = keyToString[textToKey[text]];
            }
        }
        private void Awake()
        {
            Debug.Log("Language setting created.");
            UpdateDictionary();
        }

#if UNITY_EDITOR
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void EditorAwake()
        {
            Instance.Awake();
        }
#endif

        private void UpdateDictionary()
        {
            Debug.Log("Update Dictionary");
            int langInt = (int)Lang;
            if (Lang == Lang.English)
            {
                keyToString = null;
            }
            else
            {
                TextAsset textAsset = AssetsAgent.GetAsset<TextAsset>("Lang/Lang");
                string content = textAsset.text;
                Dictionary<string, string>[] allData = JsonConvert.DeserializeObject<Dictionary<string, string>[]>(content);
                keyToString = allData[langInt];
                AssetsAgent.ReleaseAsset(textAsset);
            }
        }
    }
}
