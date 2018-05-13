using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

// Normal singleton base class
public abstract class Singleton<T> where T : class, new()
{

    /// <summary>
    /// The m_ instance.
    /// </summary>
    protected static T _Instance = null;

    /// <summary>
    /// Gets the instance.
    /// </summary>
    /// <value>The instance.</value>
    public static T Instance
    {
        get
        {
            if (null == _Instance)
            {
                _Instance = new T();
            }
            return _Instance;
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="XHEngine.Singleton`1"/> class.
    /// </summary>
    protected Singleton()
    {
        if (null != _Instance)
            throw new SingletonException("This " + (typeof(T)).ToString() + " Singleton Instance is not null !!!");
        Init();
    }


    /// <summary></summary>
    /// Init this Singleton.
    /// </summary>
    public virtual void Init() { }
}

// Singleton base class for MonoBehaviours
public abstract class SingletonBehaviour<T> : MonoBehaviour
                                              where T : MonoBehaviour
{
    protected static T mInstance;
    public static T Instance
    {
        get
        {
            return mInstance;
        }
    }

    public static bool hasInstance
    {
        get { return (SingletonBehaviour<T>.mInstance != null); }
    }

    public virtual void Awake()
    {
        if (mInstance == null)
        {
            mInstance = this as T;
        }
        else
        {
            GameObject.Destroy(this.gameObject);
            return;
        }
    }

    public virtual void OnDestroy()
    {
        if (mInstance == this as T)
        {
            mInstance = null;
        }
    }
}

public abstract class DDOLSingleton<T> : MonoBehaviour where T : DDOLSingleton<T>
{
    protected static T _Instance = null;

    public static T Instance
    {
        get
        {
            if (null == _Instance)
            {
				GameObject go = GameObject.Find("GameManager");
                if (null == go)
                {
					go = new GameObject("GameManager");
                    DontDestroyOnLoad(go);
                }
                _Instance = go.AddComponent<T>();

            }
            return _Instance;
        }
    }

    /// <summary>
    /// Raises the application quit event.
    /// </summary>
    private void OnApplicationQuit()
    {
        _Instance = null;
    }
}

public class SingletonException : System.Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SingletonException"/> class.
    /// </summary>
    /// <param name="msg">Message.</param>
    public SingletonException(string msg)
        : base(msg)
    {
    }
}

// Singleton base class for ScriptableObject
public abstract class ScriptableObjectSingleton<T> : ScriptableObject where T : ScriptableObject
{
    protected static T mInstance = null;

    public static T Instance
    {
        get
        {
            if (mInstance != null)
            {
                return mInstance;
            }

            //DebugUtils.Log("Loading " + typeof(T).Name + " from resource folder");
            mInstance = Resources.Load(typeof(T).Name, typeof(T)) as T;

            if (mInstance == null)
            {
#if UNITY_EDITOR
                //DebugUtils.LogWarning(typeof(T).Name + " resource does not exist. Creating in Assets/Resources");
                mInstance = ScriptableObject.CreateInstance<T>();

                System.IO.DirectoryInfo directory = new System.IO.DirectoryInfo("Assets/Resources");
                if (!directory.Exists)
                {
                    directory.Create();
                }

                AssetDatabase.CreateAsset(mInstance, "Assets/Resources/" + typeof(T).Name + ".asset");
                AssetDatabase.SaveAssets();
#else		
				//DebugUtils.LogError("Error getting the " + typeof(T).Name + " resource");
#endif
            }

            return mInstance;
        }
    }
}

// A singleton behaviour that will create itself if it doesn't already exist
public abstract class AutoSingletonBehaviour<T> : MonoBehaviour
                                                  where T : MonoBehaviour
{
    protected static T mInstance = null;
    public static T Instance
    {
        get
        {
            if (mInstance == null && (!Application.isEditor || Application.isPlaying))
            {
                CreateInstance();
            }
            return mInstance;
        }
    }

    public static bool hasInstance
    {
        get { return (AutoSingletonBehaviour<T>.mInstance != null); }
    }

    public static void EnsureInstanceExists()
    {
        if (mInstance == null && (!Application.isEditor || Application.isPlaying))
        {
            CreateInstance();
        }
    }

    static void CreateInstance()
    {
        GameObject go = GameObject.Find("_Singletons");
        if (go == null)
        {
            go = new GameObject("_Singletons");
            DontDestroyOnLoad(go);
        }
        GameObject newGo = new GameObject("" + typeof(T));
        newGo.transform.parent = go.transform;
        mInstance = newGo.AddComponent<T>();
    }

    public virtual void Awake()
    {
        if (mInstance == null)
        {
            mInstance = this as T;
        }
        else
        {
            GameObject.Destroy(this.gameObject);
            return;
        }
    }

    public virtual void OnDestroy()
    {
        if (mInstance == this as T)
        {
            GameObject.Destroy(mInstance.gameObject);
            mInstance = null;
        }
    }
}