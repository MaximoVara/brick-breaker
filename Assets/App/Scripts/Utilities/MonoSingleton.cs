using UnityEngine;

public abstract class MonoSingleton<T>: MonoBehaviour where T : MonoSingleton<T>
{
    private static T instance;
    private static object mutex = new object();
    public static T Instance
    {
        get
        {
            // ensures thread safety...
            lock (MonoSingleton<T>.mutex)
            {
                if(MonoSingleton<T>.instance == null)
                {
                    // Unity Check...
                    MonoSingleton<T>.instance = Object.FindObjectOfType<T>();
                    // If it doesn't exist in the scene...
                    if(MonoSingleton<T>.instance == null)
                    {
                        var go = new GameObject("[Singleton]" + typeof(T).Name);
                        MonoSingleton<T>.instance = go.AddComponent<T>();
                    }
                }
                // prevents singleton component in Unity...
                // from being destroyed when sqitching scenes...
                DontDestroyOnLoad(MonoSingleton<T>.instance);
            }
            return MonoSingleton<T>.instance;
        }
    }
}

public abstract class SOSingleton<T> : ScriptableObject where T : SOSingleton<T>
{
    private static T instance;
    private static object mutex = new object();

    public static T Instance
    {
        get
        {
            lock (SOSingleton<T>.mutex)
            {
                if(SOSingleton<T>.instance == null)
                {
                    var asset = Resources.Load<T>(typeof(T).Name);
                    if(asset == null)
                    {
                        throw new System.NullReferenceException("Asset was not able to be loaded for " + typeof(T).Name);
                    }
                    SOSingleton<T>.instance = asset; // save the loaded asset. 
                }
            }
            return SOSingleton<T>.instance;
        }
    }
}