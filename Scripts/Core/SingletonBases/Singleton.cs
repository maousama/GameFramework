using System.Reflection;
using System;

public abstract class Singleton<T> where T:Singleton<T>
{

    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                var ctor =typeof(T).GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, null, new Type[0], null);
                if (ctor == null)
                {
                    throw new NullReferenceException("This class must have a non-public non-parameter constructor!");
                }
                instance = (T)ctor.Invoke(null);
            }

            return instance;
        }
    }

}
