using UnityEngine;
using System.Collections;
using System.Text;
using System.Collections.Generic;
using UnityEngine.UI;

/// <summary>
/// Interface Manager Object 
/// </summary>
public class ioo {
    private static Hashtable prefabs = new Hashtable();

    /// <summary>
    /// 游戏管理器对象
    /// </summary>
    private static GameObject mManager = null;
    public static GameObject manager {
        get {
            if (mManager == null)
                mManager = GameObject.FindWithTag("GameManager");
            return mManager;
        }
    }

    /// <summary>
    /// 游戏管理器
    /// </summary>
    private static GameManager mGameManager = null;
    public static GameManager gameManager {
        get {
            if (mGameManager == null)
                mGameManager = manager.GetComponent<GameManager>();
            return mGameManager;
        }
    }

    /// <summary>
    /// 玩家管理
    /// </summary>
    private static PlayerManager mPlayerManager = null;
    public static PlayerManager playerManager
    {
        get
        {
            if (mPlayerManager == null)
                mPlayerManager = manager.GetComponent<PlayerManager>();
            return mPlayerManager;
        }
    }

    /// <summary>
    /// 相机管理
    /// </summary>
    private static CameraManager mCameraManager = null;
    public static CameraManager cameraManager
    {
        get
        {
            if (mCameraManager == null)
                mCameraManager = manager.GetComponent<CameraManager>();
            return mCameraManager;
        }
    }

    /// <summary>
    /// 协助播放序列帧动画
    /// </summary>
    private static AnimationHelper mAnimationHelper = null;
    public static AnimationHelper animationHelper
    {
        get
        {
            if (mAnimationHelper == null)
                mAnimationHelper = manager.GetComponent<AnimationHelper>();
            return mAnimationHelper;
        }
    }

    /// <summary>
    /// 加密狗
    /// </summary>
    private static SafeNet mSafeNet = null;
    public static SafeNet safeNet
    {
        get
        {
            if (mSafeNet == null)
                mSafeNet = manager.GetComponent<SafeNet>();
            return mSafeNet;
        }
    }

    /// <summary>
    /// 资源管理器
    /// </summary>
    private static ResourceManager mResourceManager = null;
    public static ResourceManager resourceManager {
        get {
            if (mResourceManager == null)
                mResourceManager = manager.GetComponent<ResourceManager>();
            return mResourceManager;
        }
    }

    /// <summary>
    /// 计时器管理器
    /// </summary>
    private static TimerManager mTimerManager = null;
    public static TimerManager timerManager {
        get {
            if (mTimerManager == null)
                mTimerManager = manager.GetComponent<TimerManager>();
            return mTimerManager;
        }
    }

    /// <summary>
    /// 游戏状态管理
    /// </summary>
    private static GameMode mGameMode = null;
    public static GameMode gameMode
    {
        get
        {
            if (mGameMode == null)
            {
                mGameMode = manager.GetComponent<GameMode>();
            }
            return mGameMode;
        }
    }

    /// <summary>
    /// 扩展的声音管理器
    /// </summary>
    private static AudioManager mAudioManager = null;
    public static AudioManager audioManager
    {
        get
        {
            if (mAudioManager == null)
            {
                mAudioManager = manager.GetComponent<AudioManager>();
            }
            return mAudioManager;
        }
    }

    /// <summary>
    /// 网络管理器
    /// </summary>
    private static NetworkManager mNetworkManager = null;
    public static NetworkManager networkManager {
        get {
            if (mNetworkManager == null)
                mNetworkManager = manager.GetComponent<NetworkManager>();
            return mNetworkManager;
        }
    }

    /// <summary>
    /// IO通信管理
    /// </summary>
    private static IOManager mIoManager = null;
    public static IOManager ioManager
    {
        get
        {
            if (mIoManager == null)
                mIoManager = manager.GetComponent<IOManager>();
            return mIoManager;
        }
    }

    /// <summary>
    /// 角色系统
    /// </summary>
    private static CharacterSystem mCharacterSystem;
    public static CharacterSystem characterSystem
    {
        get
        {
            if (mCharacterSystem == null)
                mCharacterSystem = new CharacterSystem();
            return mCharacterSystem;
        }
    }

    /// <summary>
    /// 关卡系统
    /// </summary>
    private static StageSystem mStageStstem;
    public static StageSystem stageSystem
    {
        get
        {
            if (mStageStstem == null)
                mStageStstem = new StageSystem();
            return mStageStstem;
        }
    }

    /// <summary>
    /// 观察者模式
    /// </summary>
    private static GameEventSystem mGameEventSystem;
    public static GameEventSystem gameEventSystem
    {
        get
        {
            if (mGameEventSystem == null)
                mGameEventSystem = new GameEventSystem();
            return mGameEventSystem;
        }
    }

    /// <summary>
    /// 不受TimeScale限制的计时
    /// </summary>
    private static NonStopTime mNonStopTime = null;
    public static NonStopTime nonStopTime
    {
        get
        {
            if (mNonStopTime == null)
                mNonStopTime = manager.GetComponent<NonStopTime>();
            return mNonStopTime;
        }
    }

    /// <summary>
    /// 获取描点对象
    /// </summary>
    private static Transform mMainUI;
    public static Transform MainUI {
        get {
            if (mMainUI == null)
                mMainUI = GameObject.FindWithTag("MainUI").transform;
            return mMainUI;
        }
    }

    private static IBattleScene mBattleScene;
    public static IBattleScene battleScene
    {
        get
        {
            if(mBattleScene == null)
            {
                GameObject go = GameObject.Find("SceneController");
                if (go != null)
                    mBattleScene = go.GetComponent<IBattleScene>();
            }
            return mBattleScene;
        }
    }

    /// <summary>
    /// 格式化字符串
    /// </summary>
    /// <returns></returns>
    public static string f(string format, params object[] args) {
        StringBuilder sb = new StringBuilder();
        return sb.AppendFormat(format, args).ToString();
    }

    /// <summary>
    /// 字符串连接
    /// </summary>
    public static string c(params object[] args) {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < args.Length; i++) {
            sb.Append(args[i].ToString());
        }
        return sb.ToString();
    }

    /// <summary>
    /// 添加Prefab
    /// </summary>
    public static void AddPrefab(string name, GameObject prefab) {
        prefabs.Add(name, prefab);
    }

    /// <summary>
    /// 获取Prefab
    /// </summary>
    public static GameObject GetPrefab(string name) {
        if (!prefabs.ContainsKey(name)) return null;
        return prefabs[name] as GameObject;
    }

    /// <summary>
    /// 移除Prefab
    /// </summary>
    /// <param name="name"></param>
    public static void RemovePrefab(string name) {
        prefabs.Remove(name);
    }

    /// <summary>
    /// 载入Prefab
    /// </summary>
    /// <param name="name"></param>
    public static GameObject LoadPrefab(string name) {
        GameObject go = GetPrefab(name);
        if (go != null) return go;
        go = Resources.Load("Prefabs/" + name, typeof(GameObject)) as GameObject;
        AddPrefab(name, go);
        return go;
    }

    /// <summary>
    /// GUI摄像机
    /// </summary>
    public static Transform guiCamera {
        get {
            GameObject go = GameObject.FindWithTag("GuiCamera");
            if (go != null) return go.transform;
            return null;
        }
    }

    #region GameMode
    public static bool preIsLoad { get { return gameMode.preIsLoad; } }
    public static bool StagetyCanUpdate { get { return gameMode.StagetyCanUpdate(); } }
    public static bool stageSysPuase { get { return stageSystem.isPuse; } set { stageSystem.isPuse = value; } }
    #endregion

    #region Player
    public static bool IsPlaying(int id) { return mPlayerManager.IsPlaying(id); }
    public static bool IsContinue(int id) { return mPlayerManager.IsContinue(id); }
    public static bool IsDead(int id) { return mPlayerManager.IsDead(id); }
    public static int playerCount { get { return mPlayerManager.playerCount; } }
    public static Vector3 WaterPosition(int id) { return mPlayerManager.Position(id); }
    public static float HPProgress(int id) { return mPlayerManager.HPProgress(id); }
    public static int Score(int id) { return mPlayerManager.Score(id); }
    public static int ContinueTime(int id) { return mPlayerManager.ContinueTime(id); }
    #endregion

    #region Audio
    public static void StopAll() { audioManager.StopAll(); }
    public static void PlayBackMusic(string audioName, bool loop) { audioManager.PlayBackMusic(audioName, loop); }
    public static void StopBackMusic(string audioName) { audioManager.StopBackMusic(audioName); }
    public static void PlaySound2D(string audioName) { audioManager.PlaySound2D(audioName); }
    #endregion

    #region  SettingManager
    public static int gameRate { get { return SettingManager.Instance.GameRate; } set { SettingManager.Instance.GameRate = value; } }
    public static int gameVolume { get { return SettingManager.Instance.GameVolume; } set { SettingManager.Instance.GameVolume = value; } }
    public static int gameLevel { get { return SettingManager.Instance.GameLevel; } set { SettingManager.Instance.GameLevel = value; } }
    public static int gameLanguage { get { return SettingManager.Instance.GameLanguage; } set { SettingManager.Instance.GameLanguage = value; } }
    public static int HasCoin(int id) { return SettingManager.Instance.HasCoin[id]; }
    public static List<float[]> GetMonthData() { return SettingManager.Instance.GetMonthData(); }
    public static float[] TotalRecord() { return SettingManager.Instance.TotalRecord(); }
    public static void ClearCoin() { SettingManager.Instance.ClearCoin(); }
    public static void ClearMonthInfo() { SettingManager.Instance.ClearMonthInfo(); }
    public static void ClearTotalRecord() { SettingManager.Instance.ClearTotalRecord(); }
    public static void AddCoin(int id) { SettingManager.Instance.AddCoin(id); mPlayerManager.AddCoin(id); }
    public static void UseCoin(int id) { SettingManager.Instance.UseCoin(id); }
    public static void LogNumberOfGame(int id) { SettingManager.Instance.LogNumberOfGame(id); }
    public static void Save() { SettingManager.Instance.Save(); }
    #endregion

    #region LoadSceneMgr
    public static void LoadJsonFile() { LoadSceneMgr.Instance.LoadJsonFile(); }
    public static void AddPreLoadPanel(string panelName) { LoadSceneMgr.Instance.AddPreLoadPanel(panelName); }
    public static void AddPreLoadPrefab(string prefabName, int cout) { LoadSceneMgr.Instance.AddPreLoadPrefab(prefabName, cout); }
    public static void AddPreLoadAtlas(string atlasName) { LoadSceneMgr.Instance.AddPreLoadAtlas(atlasName); }
    public static void SetLoadScene(string sceneName) { LoadSceneMgr.Instance.SetLoadScene(sceneName); }
    public static void SetLoadScene(string assetName, string sceneName) { LoadSceneMgr.Instance.SetLoadScene(assetName, sceneName); }
    public static void ChangeScene() { LoadSceneMgr.Instance.ChangeScene(); }
    public static void ChangeSceneDirect() { LoadSceneMgr.Instance.ChangeSceneDirect(); }
    #endregion

    #region SpritesManager
    public static List<Sprite> LoadAllSprites(string atlasname) { return SpritesManager.Instance.LoadAllSprites(atlasname); }
    public static List<Image> FindAllImage(Transform tran) { return SpritesManager.Instance.FindAllImage(tran); }
    public static void Clear() { SpritesManager.Instance.Clear(); }
    #endregion

    #region EventLuaHelper
    public static void RegesterListener(string type, UtilCommon.EventHandle eventHandle, string guid) { EventLuaHelper.Instance.RegesterListener(type, eventHandle, guid); }
    public static void RemoveListener(string type, string guid) { EventLuaHelper.Instance.RemoveListener(type, guid); }
    public static void TriggerListener(string type) { TriggerListener(type, null); }
    public static void TriggerListener(string type, object data) { EventLuaHelper.Instance.TriggerListener(type, data); }
    #endregion
}
