/**
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 2015,广州擎天柱网络科技有限公司
 * All rights reserved.
 *
 * 文件名称：LoadSceneMgr.cs
 * 简    述：控制场景的更新，在此进行特效和角色资源的预加载，导出到。    
 * 修改描述：修改 ： 加入直接跳转场景，修改跳转场景进度条功能，加入加载Json文件功能   Pancake 2017/3/14
 * 创建标识：Lorry 2015/10/19 
 */

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;


public class LoadingBar : MonoBehaviour
{
    private Image m_image;
    private Text m_text;
    void Awake()
    {
        m_image = GameObject.Find("ProgressBar/Image").GetComponent("Image") as Image;
        m_text = GameObject.Find("Text").GetComponent("Text") as Text;
    }

    public void SetPercentage(float percentage)
    {
        m_image.fillAmount = percentage;
    }

    //Tip集合，实际开发中需要从外部文件中读取
    private string[] mTips = new string[]
                  {
                    "异步加载过程中你可以浏览游戏攻略",
                    "异步加载过程中你可以查看当前进度",
                    "异步加载过程中你可以判断是否加载完成",
                    "非常好玩的蜗牛竞速",
                    "难道是好运气今天都给你了！",
                  };

    void Update()
    {
        if (m_text != null)
            m_text.text = ((int)(m_image.fillAmount * 100)).ToString();
    }
    void OnLevelWasLoaded(int level)
    {

    }
}

public class LoadSceneMgr
{
    public const string LOADING_UI_RES = "prefabs\\ui\\panel_loading";
    static LoadSceneMgr m_instance;

    public static LoadSceneMgr Instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = new LoadSceneMgr();
            }
            return m_instance;
        }
    }

    private LoadingBar m_bar;

    /// <summary>
    /// 本来命名为scene,现在决定与Application.LoadLevel对应
    /// </summary>
    private string m_levelToLoad;
    private string m_levelAssetToLoad;

    private ResourceMisc.AssetWrapper m_levelAsset;
    //需要预加载的资源列表;
    private List<string> m_resourceList = new List<string>();
    private List<int> m_goCountInPool = new List<int>();

    /// <summary>
    /// 判断是否由LoadSceneMgr控制载入;
    /// </summary>
    public bool IsStartLoad = false;
    
    #region //公共函数初始化
    public void Init(GameObject load)
    {
        if(load == null)
        {
            Debug.LogError("There is not LoadScene");
            return;
        }
        m_bar = load.AddComponent<LoadingBar>();
        GameObject.DontDestroyOnLoad(load);
    }

    public void SetLoadScene(string sceneName)
    {
        m_levelToLoad = sceneName;
        m_levelAssetToLoad = "Assets/Scenes/" + m_levelToLoad + "/" + m_levelToLoad + ".unity";
    }

    public void SetLoadScene(string assetName, string sceneName)
    {
        m_levelAssetToLoad = assetName;
        m_levelToLoad = sceneName;
    }

    public void AddPreLoadPrefab(string prefabName, int count = 0)
    {
        m_resourceList.Add(prefabName);
        m_goCountInPool.Add(count);
    }

    private string m_panelToLoad = string.Empty;
    public void AddPreLoadPanel(string panelName)
    {
        m_panelToLoad = panelName;
    }

    private List<string> m_atlasList = new List<string>();
    public void AddPreLoadAtlas(string atlasName)
    {
        m_atlasList.Add(atlasName);
    }

    public void LoadJsonFile()
    {
        LoadingManager.Instance.AddJsonFiles(Const.Level_Json_Path, LevelData.LoadHandler);
        LoadingManager.Instance.AddJsonFiles(Const.Character_Json_Path, CharacterData.LoadHandler);
        LoadingManager.Instance.AddJsonFiles(Const.Refresh_Json_Path, CharacterRefreshData.LoadHandler);
        LoadingManager.Instance.StartLoad();
    }

    // 直接切换场景
    public void ChangeScene()
    {
        IsStartLoad = true;
        SceneManager.LoadScene("empty");
    }

    // 进度条加载切换场景
    public void ChangeSceneDirect()
    {
        IsStartLoad = true;
        CoroutineController.Instance.StartCoroutine(AsyncLoadScene());
    }

    // 内存回收
    private void ClearCache()
    {
        ioo.characterSystem.Clear();
        PoolManager.Instance.Clear();
        ioo.audioManager.Clear();
        ioo.resourceManager.ClearAllAsset();
    }

    private IEnumerator<AsyncOperation> AsyncLoadScene()
    {
        AsyncOperation oper = SceneManager.LoadSceneAsync(m_levelToLoad);
        yield return oper;
        ClearCache();
    }
    #endregion
    
    #region 为了处理Loading背景和进度条而编写的函数(部分由GameManager调用)
    //载入了空场景
    public void OnLoadEmptylevel()
    {
        ClearCache();
        ResourceMisc.AssetWrapper asset = ioo.resourceManager.LoadAsset(LOADING_UI_RES, typeof(GameObject));
        GameObject obj = GameObject.Instantiate(asset.GetAsset()) as GameObject;
        Init(obj);
        m_bar.SetPercentage(0);
        ioo.resourceManager.ReleaseAsset(asset);
        CoroutineController.Instance.StartCoroutine(OnSceneAssetLoading());
    }

    IEnumerator OnSceneAssetLoading()
    {
        Debug.Log("Time StartLoadLevelAsset:" + Time.realtimeSinceStartup + "|" + m_levelAssetToLoad);
        if(m_levelAssetToLoad != null && m_levelAssetToLoad !="")
        {
            m_levelAsset = ioo.resourceManager.LoadAsset(m_levelAssetToLoad, typeof(GameObject));
        }
        Debug.Log("Time StartLoadLevel:" + Time.realtimeSinceStartup);
        yield return new WaitForEndOfFrame();
        CoroutineController.Instance.StartCoroutine(OnAsyncLoading());
    }

    IEnumerator OnAsyncLoading()
    {
        int displayProgress = 0;
        int toProgress      = 0;
        AsyncOperation op   = SceneManager.LoadSceneAsync(m_levelToLoad);
        op.allowSceneActivation = false;
        while (op.progress < 0.9f)
        {
            toProgress = (int)op.progress * 100;
            while (displayProgress < toProgress)
            {
                ++displayProgress;
                SetLoadingPercentage(displayProgress);
                yield return new WaitForEndOfFrame();
            }
        }

        if (m_resourceList.Count == 0)
            toProgress = 100;
        else
            toProgress = 90;

        while (displayProgress < toProgress)
        {
            ++displayProgress;
            SetLoadingPercentage(displayProgress);
            yield return new WaitForEndOfFrame();
        }
        op.allowSceneActivation = true;
    }

    void SetLoadingPercentage(int displayProgress)
    {
        m_bar.SetPercentage((float)displayProgress / 100.0f);
        if (displayProgress == 100)
        {
            displayProgress = 100;
        }
        int[] value = new int[3];
        int level = 100;
        int count = 0;
        for (int index = 0; index < 3; ++index)
        {
            int number = (displayProgress / level) % 10;
            value[count++] = number;
            level /= 10;
        }
    }

    int m_resIndex;
    int m_level;
    public void OnLoadScnene(int level)
    {
        Debug.Log("Time LoadLevelEnd:" + Time.realtimeSinceStartup);
        m_level     = level;
        m_resIndex  = 0;

        if (!string.IsNullOrEmpty(m_panelToLoad))
        {
            ioo.resourceManager.LoadAsset(m_panelToLoad, typeof(GameObject));
        }

        for (int i = 0; i < m_atlasList.Count; ++i)
        {
            SpritesManager.Instance.LoadAllSprites(m_atlasList[i]);
        }

        if (m_resourceList.Count == 0)
        {
            OnEndLoad();
            return;
        }

        CoroutineController.Instance.StartCoroutine(OnPreLoadingRes());
    }

    IEnumerator OnPreLoadingRes()
    {
        int resCount = m_resourceList.Count;
        float delta = 0.1f / resCount;

        int count = m_goCountInPool[m_resIndex];
        if (count == 0)
        {
            ioo.resourceManager.LoadAsset(m_resourceList[m_resIndex], typeof(GameObject));
        }
        else
        {
            PoolManager.Instance.CreatePool(m_resourceList[m_resIndex], count);
        }
        m_bar.SetPercentage(0.9f + (m_resIndex + 1) * delta);
        m_resIndex++;
        if (m_resIndex < resCount)
        {
            yield return new WaitForSeconds(0.1f);
            CoroutineController.Instance.StartCoroutine(OnPreLoadingRes());
        }
        else
        {
            yield return new WaitForSeconds(0.2f);
            Debug.Log("PreLoadingRes Over!!!!!!");
            //IsStartLoad = false;
            m_resourceList.Clear();
            m_goCountInPool.Clear();
            OnEndLoad();
        }
    }

    //场景及预加载资源完毕时进行的操作;
    void OnEndLoad()
    {
        if (m_levelAsset != null)
        {
            ioo.resourceManager.ReleaseAsset(m_levelAsset);
            m_levelAsset = null;
        }
        //处理map的无效shader
        GameObject _gameobject = GameObject.Find("map");
        if (_gameobject != null)
            ProjectileBase.excuteShader(_gameobject);
        //资源加载完成之后，所有camera 的初始化和其他变化还是交给lua处理。
        //AudioListener temp = m_bar.gameObject.GetComponent<AudioListener>();
        if (null != m_bar)
            GameObject.Destroy(m_bar.gameObject);

        //这里才调用lua的对应函数做进度;
        ioo.gameManager.uluaMgr.OnLevelLoaded(m_level);
        EventDispatcher.TriggerEvent(EventDefine.Event_All_Resource_Is_Load);
    }
    #endregion

}

