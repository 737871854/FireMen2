/**
 * Copyright 2015,广州擎天柱网络科技有限公司
 * All rights reserved.
 *
 * 文件名称：GameManager.cs
 * 简    述：更新所有资源，初始化lua环境，通过start.lua启动整个游戏（这里还会接受输入方法的调用，并Call lua method）。
 * 创建标识：XXX 2015/XX/XX
 * 修改标识：Lorry 2015/7/16 整理代码模块，为进一步编码做准备。
 */
using UnityEngine;
using System;
using System.Collections.Generic;

//为了显示帧数计数; Lorry
//using CodeStage.AdvanecedFPSCounter;
//using CodeStage.AdvancedFPSCounter;

public class GameManager : BaseLua {

    protected List<System.Action> updateList = new List<Action>();
    protected List<System.Action> fixedUpdateList = new List<Action>();

    public LuaScriptMgr uluaMgr;

    #region 私有变量

    private string message = "No message!";
    private InputRoot inputManager = null;
    //private bool isUpdateLoadingFinish = false;

    #endregion //私有变量

    #region MonoBehaviour内部函数
    /// <summary>
    /// 初始化游戏管理器
    /// </summary>
    void Awake()
    {
   //     //添加屏幕日志组件 ky 2015-07-01;
   //     Log log = gameObject.GetComponent<Log>();
   //     if (Const.DebugMode)
   //     {
   //         if (log == null)
   //         {
   //             gameObject.AddComponent<Log>();
   //         }
			//Application.logMessageReceived += HandleLog;
   //     }
        Init();
        OnVersionInited();
        //OnResourceInited();
        //Application.LoadLevelAsync("loading");
    }

    /// <summary>
    /// 初始化场景
    /// </summary>
    public void OnInitScene()
    {
        Debugger.Log("OnInitScene-->>" + Application.loadedLevelName);
    }

    public void OnLevelWasLoaded(int level)
    {
        if(LoadSceneMgr.Instance.IsStartLoad)
        {
            if(Application.loadedLevelName == "empty")
            {
                LoadSceneMgr.Instance.OnLoadEmptylevel();
            }
            else
            {
                LoadSceneMgr.Instance.OnLoadScnene(level);
            }
        }
        else
        {
            if (uluaMgr != null)
                uluaMgr.OnLevelLoaded(level);
        }

        ioo.gameMode.LoadScene(level);
        //在这里显示所有的调试信息，正式版本应该是用不着的。
        //AFPSCounter.Instance.OperationMode = AFPSCounterOperationMode.Normal;
        //AFPSCounter.Instance.FontSize = 20;
        //AFPSCounter.Instance.memoryCounter.Anchor = CodeStage.AdvancedFPSCounter.Labels.LabelAnchor.LowerLeft;
        //AFPSCounter.Instance.deviceInfoCounter.Enabled = true;
    }

    public void RegisterUpdate(System.Action callBack)
    {
        if(!updateList.Contains(callBack))
        {
            updateList.Add(callBack);
        }      
    }

    public void RegisterFixedUpdate(System.Action callback)
    {
        if (!fixedUpdateList.Contains(callback))
        {
            fixedUpdateList.Add(callback);
        }
    }

    public void UnregisterUpdate(System.Action callBack)
    {
        if (updateList.Contains(callBack))
        {
            updateList.Remove(callBack);
        }
    }

    public void UnregisterFixedUpdate(System.Action callBack)
    {
        if (fixedUpdateList.Contains(callBack))
        {
            fixedUpdateList.Remove(callBack);
        }
    }

    private float _logUptime;
    public void Update()
    {
        if (uluaMgr != null)
            uluaMgr.Update();

        //特殊更新类型
        for(int i = 0; i < updateList.Count; ++i)
        {
            updateList[i]();
        }

        
        // log 开机时间
        if (_logUptime < 5)
        {
            _logUptime += Time.deltaTime;
        }else
        {
            _logUptime = 0;
            SettingManager.Instance.LogUpTime(5);
            //SettingManager.Instance.Save();
        }
    }

    public void LateUpdate()
    {
        if (uluaMgr != null)
            uluaMgr.LateUpdate();
    }

    public void FixedUpdate()
    {
        if (uluaMgr != null)
            uluaMgr.FixedUpdate();

        for (int i = 0; i < fixedUpdateList.Count; ++i )
        {
            fixedUpdateList[i]();
        }
    }

    public void OnApplicationQuit()
    {
        CallMethod("OnApplicationQuit");
    }

    /// <summary>
    /// 析构函数
    /// </summary>
    void OnDestroy()
    {
		if (Const.DebugMode)
        	Application.logMessageReceived -= HandleLog;
        CallMethod("OnDestroy");   //初始化完成
        Debugger.Log("~GameManager was destroyed");
        uluaMgr.Destroy();

        ioo.characterSystem.Release();
        ioo.stageSystem.Release();
    }

    //void OnGUI()
    //{
    //    //if (GUI.Button(new Rect(0, 80, 100, 50), "CallStart"))
    //    //{
    //    //    //CallMethod("XXXCall");   //初始化完成
    //    //    //GameLogic.Instance.StartGame();
    //    //}

    //    //if (GUI.Button(new Rect(0, 130, 100, 50), "CallRunEnd"))
    //    //{
    //    //    //CallMethod("XXXCall");   //初始化完成
    //    //    GameLogic.Instance.EndGame();
    //    //}

    ////    GUIStyle style = new GUIStyle();
    ////    style.normal.background = null;
    ////    style.normal.textColor = new Color(1, 1, 1);
    ////    style.fontSize = 10;
    ////    GUI.Label(new Rect(0, 0, 100, 10), "版本 : " + Util.GetVersion(), style);
    ////    GUI.Label(new Rect(120, 0, 960, 20), message);
       

    //}
    #endregion //MonoBehaviour内部函数

    #region 公有方法

    public InputRoot GetInputManager()
    {
        return inputManager;
    }

    #endregion //公有方法

    #region 私有方法

    void Init()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Application.targetFrameRate = Const.GameFrameRate;

        gameObject.name = "GameManager";
        gameObject.tag = "GameManager";

        DontDestroyOnLoad(gameObject);  //防止销毁自己
        InitDataPath();
        //InitInputRoot();

        UtilCommon.AddComponent<SocketClient>(gameObject);
        UtilCommon.AddComponent<NetworkManager>(gameObject);
        UtilCommon.AddComponent<ResourceManager>(gameObject);
        UtilCommon.AddComponent<AudioManager>(gameObject);
        UtilCommon.AddComponent<IOManager>(gameObject);
        UtilCommon.AddComponent<GameMode>(gameObject);
        UtilCommon.AddComponent<PlayerManager>(gameObject);
        UtilCommon.AddComponent<CameraManager>(gameObject);
        UtilCommon.AddComponent<NonStopTime>(gameObject);
        UtilCommon.AddComponent<SafeNet>(gameObject);
    }

    private void InitInputRoot()
    {
        // 添加触摸输入组件
        inputManager = UtilCommon.AddComponent<InputRoot>(gameObject) as InputRoot;
        inputManager.AddFingerDownMethod(this.OnFingerDownMethod);
        inputManager.AddFingerHoverMethod(this.OnFingerHoverMethod);
        inputManager.AddFingerMoveMethod(this.OnFingerMoveMethod);
        inputManager.AddFingerStationaryMethod(this.OnFingerStationaryMethod);
        inputManager.AddFingerUpMethod(this.OnFingerUpMethod);
        inputManager.AddFirstFingerDragMethod(this.OnFirstFingerDragMethod);
        inputManager.AddLongPressMethod(this.OnLongPressMethod);
        inputManager.AddTapMethod(this.OnTapMethod);
        inputManager.AddSwipeMethod(this.OnSwipeMethod);
        inputManager.AddPinchMethod(this.OnPinchMethod);
        inputManager.AddTwistMethod(this.OnTwistMethod);
    }

    private void InitDataPath()
    {
        if (Application.isEditor && !UtilCommon.IsDirectoryExist(UtilCommon.resourcesPath))
        {
            UtilCommon.CreateDirectory(UtilCommon.resourcesPath);
        }
        if (!UtilCommon.IsDirectoryExist(UtilCommon.storePath))
        {
            UtilCommon.CreateDirectory(UtilCommon.storePath);
        }
    }

    /// <summary>
    /// 版本控制初始化
    /// </summary>
    public void OnVersionInited()
	{
		Qtz.Q5.Version.VersionManager.Instance.StartInitResources(this);
    }

    /// <summary>
    /// 资源初始化结束,现在是在VersionManager的自动更新过程结束后被调用
    /// </summary>
    public void OnResourceInited()
    {
        ioo.resourceManager.Initialize("", null);
        
        //因为声音管理初始化资源有依赖于ResourceManager的部分，所以推迟到这里初始化 Lorry
        //Util.AddComponent<AudioManager>(gameObject);

        uluaMgr = LuaScriptMgr.Instance;

        uluaMgr.Start();
        uluaMgr.DoFile("start");      //加载游戏
        //------------------------------------------------------------
        //GameObject inputRootGO = Instantiate(Resources.Load("Prefabs/InputRoot", typeof(GameObject))) as GameObject;
        //inputManager = inputRootGO.GetComponent<InputRoot>();
		//ioo.networkManager.SendConnect ();
        //gameObject.AddComponent<Fight>();
        CallMethod("OnInitOK");   //初始化完成
        Log.PrintMsg("Call OnInitOK! ");
    }

    #endregion //私有方法

    #region InputMethod

    protected void OnFingerDownMethod(Vector2 vec2, GameObject go)
	{
		object[] args = new object[2];
		args.SetValue (vec2, 0);
		args.SetValue (go, 1);
		CallMethod ("OnFingerDown", args);
	}

	protected void OnFingerHoverMethod(Vector2 vec2, GameObject go, HoverEventPhase phase)
	{
		object[] args = new object[3];
		args.SetValue (vec2, 0);
		args.SetValue (go, 1);
		args.SetValue ((int)phase, 2);
		CallMethod ("OnFingerHover", args);
	}

	protected void OnFingerMoveMethod(Vector2 vec2, GameObject go, MotionEventPhase phase)
	{
		object[] args = new object[3];
		args.SetValue (vec2, 0);
		args.SetValue (go, 1);
		args.SetValue ((int)phase, 2);
		CallMethod ("OnFingerMove", args);
	}

	protected void OnFingerStationaryMethod(Vector2 vec2, GameObject go, MotionEventPhase phase)
	{
		object[] args = new object[3];
		args.SetValue (vec2, 0);
		args.SetValue (go, 1);
		args.SetValue ((int)phase, 2);
		CallMethod ("OnFingerStationary", args);
	}

	protected void OnFingerUpMethod(Vector2 vec2, GameObject go)
	{
		object[] args = new object[2];
		args.SetValue (vec2, 0);
		args.SetValue (go, 1);
		CallMethod ("OnFingerUp", args);
	}

	protected void OnFirstFingerDragMethod(Vector2 vec2, GameObject go, Vector2 deltaMove, ContinuousGestureEventPhase phase)
	{
		object[] args = new object[4];
		args.SetValue (vec2, 0);
		args.SetValue (go, 1);
		args.SetValue (deltaMove, 2);
		args.SetValue ((int)phase, 3);
		CallMethod ("OnFirstFingerDrag", args);
	}

	protected void OnLongPressMethod(Vector2 vec2, GameObject go)
	{
		object[] args = new object[2];
		args.SetValue (vec2, 0);
		args.SetValue (go, 1);
		CallMethod ("OnLongPress", args);
	}

	protected void OnTapMethod(Vector2 vec2, GameObject go)
	{
		object[] args = new object[2];
		args.SetValue (vec2, 0);
		args.SetValue (go, 1);
		CallMethod ("OnTap", args);

        //if (Util.IsOverUI()) //暂时添加点击UI发声
        //{
        //    ioo.audioManager.PlayOnPoint("Assets/Music/ogg/yx-ty0001.ogg", Camera.main.gameObject.transform.position);
        //}
	}

	protected void OnSwipeMethod(Vector2 vec2, GameObject go, GameObject startGO, float velocity, Vector2 move)
	{
		object[] args = new object[5];
		args.SetValue (vec2, 0);
		args.SetValue (go, 1);
		args.SetValue (startGO, 2);
		args.SetValue (velocity, 3);
		args.SetValue (move, 4);
		CallMethod ("OnSwipe", args);
	}

	protected void OnPinchMethod(Vector2 vec2, GameObject go, float delta, float gap, ContinuousGestureEventPhase phase)
	{
		object[] args = new object[5];
		args.SetValue (vec2, 0);
		args.SetValue (go, 1);
		args.SetValue (delta, 2);
		args.SetValue (gap, 3);
		args.SetValue ((int)phase, 4);
		CallMethod ("OnPinch", args);
	}

	protected void OnTwistMethod(Vector2 vec2, GameObject go, float deltaRotation, float totalRotation)
	{
		object[] args = new object[4];
		args.SetValue (vec2, 0);
		args.SetValue (go, 1);
		args.SetValue (deltaRotation, 2);
		args.SetValue (totalRotation, 3);
		CallMethod ("OnTwist", args);
	}

    #endregion //InputMethod


    void HandleLog(string condition, string stackTrace, LogType type)
    {
        if (type == LogType.Error 
            || type == LogType.Warning 
            || type == LogType.Exception)
        {
            Log.PrintMsg( type + ":" + condition + "\n"+stackTrace, type);
            //SetExceptionPref(condition + "\n stackTrace:" + stackTrace); //考虑记录错误到Prefab
        }
    }
}
