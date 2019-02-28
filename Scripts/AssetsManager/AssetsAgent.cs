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
        public static GameObject GetGameObject(string name)
        {
            return GetGameObject(name,null);
        }
        public static GameObject GetGameObject(string name, Transform parent)
        {
            GameObject prefab = Resources.Load<GameObject>(name);
            GameObject newGameObject = Object.Instantiate(prefab, parent);
            return newGameObject;
        }
    }
}

