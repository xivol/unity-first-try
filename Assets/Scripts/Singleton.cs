using UnityEngine;

// https://habrahabr.ru/post/341830/

public abstract class Singleton<T> : Core<T> where T : MonoBehaviour
{
    private static T instance = null;

    public static T Instance { get { return instance; } }

    public static bool IsReady { get { return instance != null; } }

    protected override void Awake()
    {
        base.Awake();
        if (instance == null)
        {
            instance = this as T;
        }
        else if (instance == this)
        {
            Destroy(gameObject);
        }

        if (Application.isPlaying)
            DontDestroyOnLoad(gameObject);

        Initialize();
    }

    protected abstract void Initialize();
}

//public abstract class Singleton<T> : ScriptableObject where T: ScriptableObject
//{
//    private static T _instance = null;

//    public static T Instance { get { return _instance; } }

//    public static bool IsReady { get { return _instance != null; } }

//    protected virtual void OnEnable()
//    {
//        if (_instance == null)
//        {
//            _instance = CreateInstance<T>;
//        }

//        Initialize();
//    }

//    protected abstract void Initialize();
//}
