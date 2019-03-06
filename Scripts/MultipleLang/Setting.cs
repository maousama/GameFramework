using UnityEngine;

namespace Assets.Scripts.MultipleLang
{
    public class Setting
    {
        public static void Set(Lang lang)
        {
            PlayerPrefs.SetInt("Lang", (int)lang);
            Dictionary.lang = lang;
            

        }
    }
}
