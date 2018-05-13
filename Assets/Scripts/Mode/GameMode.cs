/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   GameMode.cs
 * 
 * 简    介:    游戏玩家所有状态处理中心，以及当前游戏全局事件（劫持，加水）处理
 * 
 * 创建标识：   Pancake 2017/4/26 8:29:05
 * 
 * 修改描述：   
 * 
 */


using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public partial class GameMode : MonoBehaviour
{
    /// <summary>
    /// 游戏状态
    /// </summary>
    private E_GameState mState;
    // 雾气
    private enum FogType
    {
        Normal,
        Foging,
    }
    private FogType mFogType = FogType.Normal;
    private float mFogTime;
    // 救援
    private enum SupportType
    {
        Normal,
        Support,
    }
    private SupportType mSupportType = SupportType.Normal;
    private float mSupportTime;
    // 场景号
    private int mLevel;
  
    // 预加载资源是否加载完毕
    private bool mPreIsLoad;
    public bool preIsLoad { get { return mPreIsLoad; } }
    #region Boss血条
    private float mBossProgress;
    public float BossProgress { get { return mBossProgress; } }
    public void UpdateBossProgress(float value) { mBossProgress = value; }
    #endregion

    /// <summary>
    /// 屏幕坐标
    /// </summary>
    private Vector2[] screenPos;

    public E_GameState State { get { return mState; } }

    /// <summary>
    /// UI相机
    /// </summary>
    private Camera mUICamera;
    public Camera UICamera 
    {
        get
        {
            if (null == mUICamera)
                mUICamera = GameObject.FindGameObjectWithTag(GameTage.UICamera).gameObject.GetComponent<Camera>();
            return mUICamera;
        } 
    }

    private Canvas mUICanvas;
    public Canvas UICanvas
    {
        get
        {
            if (null == mUICanvas)
                mUICanvas = GameObject.FindGameObjectWithTag(GameTage.MainCanvas).gameObject.GetComponent<Canvas>();
            return mUICanvas;
        }
    }

    #region Unity Call Back
    void Awake()
    {
        ioo.gameManager.RegisterUpdate(UpdatePreFrame);
        ioo.gameManager.RegisterFixedUpdate(UpdateFixedFrame);

        EventDispatcher.AddEventListener(EventDefine.Event_Game_Defeat, OnDefeat);
        EventDispatcher.AddEventListener(EventDefine.Event_Game_Success, OnSuccess);
        EventDispatcher.AddEventListener<float, float>(EventDefine.Event_Freeze_Prop, OnFreezeProp);
        EventDispatcher.AddEventListener<float>(EventDefine.Event_Get_Support_Prop, AddSupportEvent);

        EventDispatcher.AddEventListener(EventDefine.Event_All_Resource_Is_Load, OnAllIsLoad);
        EventDispatcher.AddEventListener<bool>(EventDefine.Event_Stage_System_Is_Pause, OnStageSysIsPause);
        EventDispatcher.AddEventListener<int>(EventDefine.Event_Key_Sure, OnEventSure);
    }

    void Start()
    {
        screenPos = new Vector2[3];
    }

    void Destroy()
    {
        ioo.gameManager.UnregisterUpdate(UpdatePreFrame);
        ioo.gameManager.UnregisterFixedUpdate(UpdateFixedFrame);

        EventDispatcher.RemoveEventListener(EventDefine.Event_Game_Defeat, OnDefeat);
        EventDispatcher.RemoveEventListener(EventDefine.Event_Game_Success, OnSuccess);
        EventDispatcher.RemoveEventListener<float, float>(EventDefine.Event_Freeze_Prop, OnFreezeProp);
        EventDispatcher.RemoveEventListener<float>(EventDefine.Event_Get_Support_Prop, AddSupportEvent);

        EventDispatcher.RemoveEventListener(EventDefine.Event_All_Resource_Is_Load, OnAllIsLoad);
        EventDispatcher.RemoveEventListener<bool>(EventDefine.Event_Stage_System_Is_Pause, OnStageSysIsPause);
        EventDispatcher.RemoveEventListener<int>(EventDefine.Event_Key_Sure, OnEventSure);
    }
    #endregion
    
    #region Public Function
    /// <summary>
    /// 设置游戏状态
    /// </summary>
    /// <param name="state"></param>
    public void RunState(E_GameState state)
    {
        switch (state)
        {
            case E_GameState.Start:
                ioo.playerManager.Reset();
                break;
            case E_GameState.SelectMap:
                EventDispatcher.TriggerEvent(EventDefine.Event_Character_To_Map);
                ioo.TriggerListener(EventLuaDefine.Character_Select_End);
                break;
            case E_GameState.Play:
                if (mState == E_GameState.Water)
                {
                    if(mCPAIsPlaying)
                        ioo.cameraManager.PlayCPA();
                    OnStageSysIsPause(false);
                    ioo.characterSystem.Active();
                    ioo.TriggerListener(EventLuaDefine.Event_Fill_Water, false);
                    break;
                }
                if (mState == E_GameState.Hold)
                {
                    ioo.TriggerListener(EventLuaDefine.Event_Hold_Player, false);
                    break;
                }
                ioo.cameraManager.InitSceneCamera();
                break;
            case E_GameState.Water:
                mWaterTime = 30;
                mWaterValue = 0;
                OnStageSysIsPause(true);
                ioo.characterSystem.Pause();
                mPressCount = new int[Define.MAX_PLAYER_NUMBER];
                ioo.TriggerListener(EventLuaDefine.Event_Fill_Water, true);
                if(ioo.cameraManager.IsCPAPlaying())
                {
                    mCPAIsPlaying = true;
                    ioo.cameraManager.PauseCPA();
                }
                break;
            case E_GameState.Summary:
                //ioo.cameraManager.InitSceneCamera();
                //ioo.cameraManager.PauseRoll = true;
                //ioo.cameraManager.PauseMove = true;
                break;
            case E_GameState.Hold:
                mHoldTime = 8;
                mHoldValue = 0;
                mPressCount = new int[Define.MAX_PLAYER_NUMBER];
                ioo.TriggerListener(EventLuaDefine.Event_Hold_Player, true);
                break;
        }
        mState = state;
    }

    /// <summary>
    /// 游戏结束，不可继续游戏
    /// </summary>
    /// <returns></returns>
    public bool IsGameOver()
    {
        return State == E_GameState.Summary;
    }

    /// <summary>
    /// 屏幕坐标
    /// </summary>
    /// <param name="screenpos"></param>
    /// <returns></returns>
    public void ScreenPosition(int id, Vector2 pos)
    {
        screenPos[id] = pos;
    }

    /// <summary>
    /// 计算单个玩家在接触被劫持状态中的贡献率
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public float GetHoldContribution(int index)
    {
        int total = (int)(1 / Define.GAME_HOLD_PLAYER_PERCENT_PRESS);
        int count = mPressCount[index];
        return 1.0f * count / total;
    }

    #region HittingPart
    /// <summary>
    /// 射击点信息
    /// </summary>
    private List<HittingPart> mAllHittingPartList = new List<HittingPart>();
    private List<HittingPart> mUsingHittingParList = new List<HittingPart>();
    public List<HittingPart> usingHittingParList { get { UpdateUsingHP(); return mUsingHittingParList; } }
    public bool IsUsingHittingPartAllBreaked()
    {
        bool ret = true;
        foreach (HittingPart hp in usingHittingParList)
        {
            if (hp.curHp != 0)
            {
                ret = false;
                break;
            }
        }
        if (ret) ClearHitPoint();
        return ret;
    }
    public void AddHPToAllHitPoint(HittingPart hp)
    {
        if (mAllHittingPartList.Contains(hp)) return;
        mAllHittingPartList.Add(hp);
    }
    public void RemoveHPFromAllHitPoint(HittingPart hp)
    {
        if (!mAllHittingPartList.Contains(hp)) return;
        mAllHittingPartList.Remove(hp);
    }
    public void ClearHitPoint()
    {
        mUsingHittingParList.Clear();
    }
    private void UpdateUsingHP()
    {
        foreach (HittingPart hp in mAllHittingPartList)
        {
            if (hp.curHp > 0 && !mUsingHittingParList.Contains(hp))
                mUsingHittingParList.Add(hp);
        }
    }
    #endregion

    /// <summary>
    /// 设置选择的地图ID
    /// </summary>
    /// <param name="mapID"></param>
    public void SetSelectedMap(int mapID)
    {
        EventDispatcher.TriggerEvent(EventDefine.Event_Player_Select_Map);

        ioo.TriggerListener(EventLuaDefine.Map_Select_End, mapID);
    }

    /// <summary>
    /// 场景被加载
    /// </summary>
    /// <param name="level"></param>
    public void LoadScene(int level)
    {
        mLevel = level;
        string scenename = Application.loadedLevelName;
        switch (scenename)
        {
            case SceneNames.CoinScene:
                RunState(E_GameState.Start);
                break;
            case SceneNames.SelectScene:
                RunState(E_GameState.SelectCharacter);
                ioo.stageSystem.InitStageChain(level, scenename);
                break;
            case SceneNames.empty:
                RunState(E_GameState.Loading);
                break;
            case SceneNames.BattleScene0_0:
            case SceneNames.BattleScene0_1:
            case SceneNames.BattleScene0_2:
            case SceneNames.BattleScene1:
            case SceneNames.BattleScene2:
                RunState(E_GameState.Play);
                ioo.stageSystem.InitStageChain(level, scenename);
                break;
            case SceneNames.SettingScene:
                RunState(E_GameState.Setting);
                break;
        }
        mPreIsLoad = false;
    }
       
    #endregion

    #region Private Function
    /// <summary>
    /// 能否射击
    /// </summary>
    /// <returns></returns>
    private bool CanUpdateWater()
    {
        if (State == E_GameState.SelectCharacter ||
            State == E_GameState.SelectMap || 
            State == E_GameState.Play || 
            State == E_GameState.Waitting ||
            State == E_GameState.Hold)
            return true;
        return false;
    }
    /// <summary>
    /// 是否处于游戏装填
    /// </summary>
    /// <returns></returns>
    private bool IsPlaying()
    {
        return State == E_GameState.Play;
    }
    /// <summary>
    /// 是否为加水阶段
    /// </summary>
    /// <returns></returns>
    private bool IsWater()
    {
        return mState == E_GameState.Water;
    }    

    /// <summary>
    ///  关卡计时是否可以更新
    /// </summary>
    /// <returns></returns>
    public bool StagetyCanUpdate()
    {
        if (mState == E_GameState.Hold) return false;
        if (mState == E_GameState.Continue) return false;
        if (mState == E_GameState.Water) return false;
        if (ioo.characterSystem.HasHelicopter()) return false;
        if (ioo.characterSystem.HasHoldWolf()) return false;

        return true;
    }
    #endregion

    
    #region Event Function
    /// <summary>
    /// 响应确认按钮
    /// </summary>
    void OnEventSure(int index)
    {
        // 飞机救援市民和其他事件互斥
        if (OnRescue(index))
            return;
        // 支援飞机，加水，劫持事件不可能同时发生，就直接执行
        OnSupport(index);
        OnWater(index);
        OnHolde(index);
    }

    /// <summary>
    /// 游戏失败
    /// </summary>
    void OnDefeat()
    {
        RunState(E_GameState.Summary);
        ioo.TriggerListener(EventLuaDefine.Event_To_Summary, false);
        StartCoroutine(GameDefeat());
    }
    /// <summary>
    /// 通关成功
    /// </summary>
    void OnSuccess()
    {
        switch (Application.loadedLevelName)
        {
            case SceneNames.BattleScene0_0:
            case SceneNames.BattleScene0_1:
                ioo.TriggerListener(EventLuaDefine.Event_Level_Pass, mLevel);
                break;
            case SceneNames.BattleScene0_2:
            case SceneNames.BattleScene1:
            case SceneNames.BattleScene2:
                RunState(E_GameState.Summary);
                ioo.TriggerListener(EventLuaDefine.Event_To_Summary, true);
                StartCoroutine(GameSuccess());
                break;
        }
      
    }
    /// <summary>
    /// 救援
    /// </summary>
    /// <param name="index"></param>
    private int mHelicopterID = 1025;
    private bool OnRescue(int index)
    {
        Citizen citizen = ioo.characterSystem.CitizenIsWaittingForHelp();
        if (citizen == null) return false;

        CharacterBaseAttr baseAttr = FactoryManager.attrFactory.GetCharacterBaseAttr(mHelicopterID);
        ISpawnCommand command = new SpawnHelicopterCommand(baseAttr.id, null, citizen, index);
        command.Execute();
        return true;
    }

    /// <summary>
    /// 支援飞机
    /// </summary>
    /// <param name="index"></param>
    private void OnSupport(int index)
    {
        if (mSupportType != SupportType.Support)
            return;

        mSupportType = SupportType.Normal;
        Player player = ioo.playerManager.GetPlayer(index);
        //ScenesManager.Instance.OnCallSupport(player);
    }

    /// <summary>
    /// 加水
    /// </summary>
    private void OnWater(int index)
    {
        if (State != E_GameState.Water)
            return;
        // 如果是加水完毕，结算各个玩家加水水量有用，否则无用
        ++mPressCount[index];
        mWaterValue += Define.GAME_FILL_WATER_PRECENT_PRESS;
        if (mWaterValue >= 1.0f)
            RunState(E_GameState.Play);

        int value = 0;
        bool flag = mPressCount[index] % 5 == 0;
        value = flag ? 1 : 0;
        ioo.playerManager.FillWater(index, value);
    }

    /// <summary>
    /// 玩家被劫持
    /// </summary>
    /// <param name="index"></param>
    private void OnHolde(int index)
    {
        if (State != E_GameState.Hold)
            return;

        ++mPressCount[index];
        mHoldValue += Define.GAME_HOLD_PLAYER_PERCENT_PRESS;
        if (mHoldValue >= 1.0f)
        {
            RunState(E_GameState.Play);
            EventDispatcher.TriggerEvent(EventDefine.Event_Struggle_Hold_Success, true);
        }
    }

    /// <summary>
    /// 响应冰冻道具
    /// </summary>
    /// <param name="fogTime"></param>
    /// <param name="freezeTime"></param>
    void OnFreezeProp(float fogTime, float freezeTime)
    {
        // 想下位机发送喷雾消息
        Debugger.Log("开始喷雾");
        mFogTime = fogTime;
        mFogType = FogType.Foging;
        //ScenesManager.Instance.OnFreeProp(freezeTime);
    }
    /// <summary>
    /// 响应支援道具
    /// </summary>
    /// <param name="validTime"></param>
    void AddSupportEvent(float validTime)
    {
        mSupportTime = validTime;
        mSupportType = SupportType.Support;
    }
    /// <summary>
    /// 预加载资源完毕
    /// </summary>
    void OnAllIsLoad() { mPreIsLoad = true; }
    /// <summary>
    /// 关卡逻辑控制
    /// </summary>
    /// <param name="value"></param>
    void OnStageSysIsPause(bool value) { ioo.stageSysPuase = value; }
    /// <summary>
    /// 帧更新
    /// </summary>
    private void UpdatePreFrame()
    {
        if (IsPlaying())
        {
            //OnJudgePassCondition();
        }

        if (mFogType == FogType.Foging)
        {
            if (mFogTime > 0)
                mFogTime -= Time.deltaTime;
            else
            {
                mFogType = FogType.Normal;
                // 向下位机发送结束冰冻消息
                Debugger.Log("结束喷雾消息");
            }
        }

        if (mSupportType == SupportType.Support)
        {
            if (mSupportTime > 0)
                mSupportTime -= Time.deltaTime;
            else
            {
                mSupportType = SupportType.Normal;
            }
        }
    }


    // 加水倒计时
    private float mWaterTime;
    public int WaterTime { get { return (int)mWaterTime; } }
    // 加水水量
    private float mWaterValue;
    public float WaterValue { get { return mWaterValue; } }
    // 劫持时间
    private float mHoldTime;
    public float HoldTime { get { return mHoldTime; } }
    // 劫持进度
    private float mHoldValue;
    public float HoldValue { get { return mHoldValue; } }
    // 加水时或被劫持时，各个玩家按下次数
    private int[] mPressCount;
    // 记录相机是否在运动
    private bool mCPAIsPlaying;
    /// <summary>
    /// 固定帧更新
    /// </summary>
    private void UpdateFixedFrame()
    {
        // 执行玩家输入
        if (CanUpdateWater())
        {
            OnPlayerInput();
        }

        // 加水
        if (IsWater())
        {
            if (mWaterTime > 0)
                mWaterTime -= Time.fixedDeltaTime;
            else
            {
                mWaterTime = 0;
                RunState(E_GameState.Play);
            }
        }

        if (mState == E_GameState.Hold)
        {
            if (mHoldTime > 0)
                mHoldTime -= Time.fixedDeltaTime;
            else
            {
                mHoldTime = 0;
                RunState(E_GameState.Play);
                EventDispatcher.TriggerEvent(EventDefine.Event_Struggle_Hold_Success, false);
            }
        }
    }
    #endregion

    IEnumerator GameSuccess()
    {
        yield return new WaitForSeconds(10);
        ioo.TriggerListener(EventLuaDefine.Event_Level_Pass, mLevel);
    }

    IEnumerator GameDefeat()
    {
        yield return new WaitForSeconds(10);
        ioo.TriggerListener(EventLuaDefine.Event_Game_Over);
    }
}
