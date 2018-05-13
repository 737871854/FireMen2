/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   IBattleScene.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/4/16 9:15:12
 * 
 * 修改描述：   
 * 
 */

using System.Collections.Generic;
using UnityEngine;

public abstract class IBattleScene : MonoBehaviour
{
    // 灯光圆环
    [SerializeField]
    protected CircleBehaviour mCircle0;
    [SerializeField]
    protected CircleBehaviour mCircle1;

    // 精英怪Hold点
    [SerializeField]
    protected List<Transform> mHoldPointList;

    // 飞机出生点和回收点
    [SerializeField]
    protected Transform mSupport0;
    [SerializeField]
    protected Transform mSupport1;

    protected List<E_CharacterType> mCheckWaterPullList;

    private IGameEventObserver mGameEventObserver;

    public CircleBehaviour circle0 {get{ return mCircle0; }}
    public CircleBehaviour circle1 { get { return mCircle1; } }
    public Transform support0 { get { return mSupport0; } }
    public Transform support1 { get { return mSupport1; } }

    private void Start()
    {
        mGameEventObserver = new CheckPullWaterBattleSceneObserver(this);
        ioo.gameEventSystem.RegisterObserver(GameEventType.PullWater, mGameEventObserver);
        Init();
    }

    private void OnDestroy()
    {
        ioo.gameEventSystem.RemoveObserver(GameEventType.PullWater, mGameEventObserver);
        Release();
    }

    public abstract void Init();
    public abstract void Release();

    public abstract Vector3 GetCirclePositionByName(string name);

    public abstract List<Transform> GetHoldPositionByLV(int lv);

    public virtual void UpdateWaterPull(E_CharacterType characterType)
    {
        if (!mCheckWaterPullList.Contains(characterType) || mCheckWaterPullList.Count == 0) return;
        mCheckWaterPullList.Remove(characterType);
        if(mCheckWaterPullList.Count == 0)
            ioo.gameMode.RunState(E_GameState.Water);
    }
}