/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   IWolfState.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/5/2 16:21:49
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;
using UnityEngine;

public enum WolfTransition
{
    NullTansition,
    SeaEnemy,
    Reached,
    HoldSuccess,
    HoldFail,
    MissionComplete,
}

public enum WolfStateID
{
    NullState,
    Chase,
    AttackOrHold,
    Success,
    Defeat,
    Leave,
}

public abstract class IWolfState
{
    protected Dictionary<WolfTransition, WolfStateID> mMap = new Dictionary<WolfTransition, WolfStateID>();
    protected WolfStateID mStateID;
    protected ICharacter mCharacter;
    protected WolfFSMSystem mFSMSystem;

    public IWolfState(WolfFSMSystem fsm, ICharacter character)
    {
        mFSMSystem = fsm;
        mCharacter = character;
    }

    public WolfStateID stateID { get { return mStateID; } }

    public void AddTransition(WolfTransition trans, WolfStateID id)
    {
        if (trans == WolfTransition.NullTansition)
        {
            Debug.LogError("WolfState Error: trans不能为空"); return;
        }
        if (id == WolfStateID.NullState)
        {
            Debug.LogError("WolfState Error: 状态ID不能为空"); return;
        }
        if (mMap.ContainsKey(trans))
        {
            Debug.LogError("WolfState Error: " + trans + " 已经添加上了"); return;
        }
        mMap.Add(trans, id);
    }

    public void DeleteTransition(WolfTransition trans)
    {
        if (mMap.ContainsKey(trans) == false)
        {
            Debug.LogError("删除转换条件的时候， 转换条件：[" + trans + "]不存在"); return;
        }
        mMap.Remove(trans);
    }

    public WolfStateID GetOutPutState(WolfTransition trans)
    {
        if (mMap.ContainsKey(trans) == false)
        {
            return WolfStateID.NullState;
        }
        else
        {
            return mMap[trans];
        }
    }

    public virtual void DoBeforeEntering() { }
    public virtual void DoBeforeLeaving() { }
    public abstract void Reason(E_ActionType actionType);
    public abstract void Act(E_ActionType actionType);

}