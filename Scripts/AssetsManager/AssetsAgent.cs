using System;
using UnityEngine;
namespace AssetsManager
{
    public class AssetsAgent
    {
        public static T GetAsset<T>(string name) where T : UnityEngine.Object
        {
            return Resources.Load<T>(name);
        }
        public static GameObject GetGameObject(string name)
        {
            return GetGameObject(name, null);
        }
        public static GameObject GetGameObject(string name, Transform parent)
        {
            GameObject prefab = Resources.Load<GameObject>(name);
            GameObject newGameObject = UnityEngine.Object.Instantiate(prefab, parent);
            return newGameObject;
        }
        public static void DestroyGameObject(GameObject gameObject)
        {
            DestroyGameObject(gameObject);
        }
    }
}

