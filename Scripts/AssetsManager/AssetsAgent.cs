using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace AssetsManager
{
    public class AssetsAgent
    {
        public static T GetAsset<T>(string name) where T : Object
        {
            return Resources.Load<T>(name);
        }
    }
}

