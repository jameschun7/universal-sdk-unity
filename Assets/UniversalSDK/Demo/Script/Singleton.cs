using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    private static object _lock = new object();
    public static T Instance
    {
        get
        {
            if (applicationIsQuitting)
            {
                applicationIsQuitting = false;
                Debug.LogWarning("[Singleton] Instance '" + typeof(T) + "' already destroyed on application quit.");
            }

            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = (T)FindObjectOfType(typeof(T));

                    if (FindObjectsOfType(typeof(T)).Length > 1)
                    {
                        Debug.LogError("[Singleton] Something went really wrong - there should never be more than 1 singleton! Reopening the scene might fix it.");
                        return _instance;
                    }

                    if (_instance == null)
                    {
                        GameObject singleton = new GameObject();
                        _instance = singleton.AddComponent<T>();
                        singleton.name = typeof(T).ToString();

                        DontDestroyOnLoad(singleton);
                    }
                }

                return _instance;
            }
        }
    }

    private static bool applicationIsQuitting = false;

    protected virtual void OnDestroy()
    {
        applicationIsQuitting = true;
    }
}
