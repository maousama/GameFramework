using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{

    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = new GameObject(typeof(T).ToString());
                go.AddComponent<T>();
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("You cannot create this instance of " + this.GetType() + " repeatedly! the new instance of this scene will be deleted");
            Destroy(gameObject);
            return;
        }
        instance = GetComponent<T>();
        Init();
    }

    /// <summary>
    /// Use this instead of awake to init.
    /// </summary>
    protected virtual void Init() { }


    private void OnDestroy()
    {
        if (instance == this)
        {
            OnInstanceDestroy();
        }
    }

    protected virtual void OnInstanceDestroy()
    {
        instance = null;
        print(typeof(T) + " Instance has destroied");
    }

    public static T Check()
    {
        return Instance;
    }
}
