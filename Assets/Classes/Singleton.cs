using System;
using UnityEngine;

public class Singleton<T>
{
    private static T instance = default(T);
    private static readonly object instanceLock = new object();

    public static T Instance
    {
        get
        {
            lock (instanceLock)
            {
                if (instance == null)
                {
                    instance = (T)Activator.CreateInstance(typeof(T));
                }

                return instance;
            }
        }
    }
}

public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    protected static T singletonInstance;
    public static T Instance
    {
        get
        {
            if (singletonInstance == null)
            {
                UpdateInstance();
            }

            return singletonInstance;
        }
    }

    public static void UpdateInstance()
    {
        singletonInstance = FindObjectOfType(typeof(T)) as T;
    }

    private void Awake()
    {
        singletonInstance = null;
    }

    void OnDestroy()
    {
        singletonInstance = null;
    }

    public static void SetInstance(T inst)
    {
        singletonInstance = inst;
    }
}

public class CreatedSingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    protected static T singletonInstance;
    public static T Instance
    {
        get
        {
            if (singletonInstance == null)
            {
                UpdateInstance();
            }

            return singletonInstance;
        }
    }

    public static void UpdateInstance()
    {
        singletonInstance = FindObjectOfType(typeof(T)) as T;

        if (singletonInstance == null)
        {
            var g = new GameObject(typeof(T).Name);
            singletonInstance = g.AddComponent<T>();
        }
    }
}
