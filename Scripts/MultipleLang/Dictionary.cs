﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}