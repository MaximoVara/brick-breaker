using UnityEngine;

public abstract class Singleton<T>: MonoBehaviour where T : Singleton<T>
{
    private static T instance;
    private static object mutex = new object();
    public static T Instance
    {
        get
        {
            // ensures thread safety...
            lock (Singleton<T>.mutex)
            {
                if(Singleton<T>.instance == null)
                {
                    // Unity Check...
                    Singleton<T>.instance = Object.FindObjectOfType<T>();
                    // If it doesn't exist in the scene...
                    if(Singleton<T>.instance == null)
                    {
                        var go = new GameObject("[Singleton]" + typeof(T).Name);
                        Singleton<T>.instance = go.AddComponent<T>();
                    }
                }
                // prevents singleton component in Unity...
                // from being destroyed when sqitching scenes...
                DontDestroyOnLoad(Singleton<T>.instance);
            }
            return Singleton<T>.instance;
        }
    }
}