using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.MultipleLang
{
    public class StringProvider
    {
        public static string Get(string key)
        {
            Setting setting = Setting.Instance;
            if (setting.lang == Lang.English) return key;
            return Setting.Instance.keyToString[key];
        }
    }
}
