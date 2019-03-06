using UnityEngine;
namespace Assets.Scripts.MultipleLang
{
    internal class AssetsAgent
    {
        internal static T GetAsset<T>(string name) where T : UnityEngine.Object
        {
            return Resources.Load<T>(name);
        }
        internal static GameObject GetGameObject(string name)
        {
            return GetGameObject(name, null);
        }
        internal static GameObject GetGameObject(string name, Transform parent)
        {
            GameObject prefab = Resources.Load<GameObject>(name);
            GameObject newGameObject = UnityEngine.Object.Instantiate(prefab, parent);
            return newGameObject;
        }
        internal static void DestroyGameObject(GameObject gameObject)
        {
            DestroyGameObject(gameObject);
        }
    }
}

