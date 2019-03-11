using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UI;

namespace Assets.Scripts.MultipleLang
{
    public static class StringProvider
    {
        public static void Bind(this Text text, string key)
        {
            Setting setting = Setting.Instance;
            if (setting.Lang == Lang.English) text.text = key;
            else text.text = setting.keyToString[key];
            setting.textToKey.Add(text, key);
        }
        public static void Unbind(this Text text)
        {
            text.text = string.Empty;
            Setting.Instance.textToKey.Remove(text);
        }
    }
}
