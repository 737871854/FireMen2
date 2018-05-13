/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   IEliteMonsterState.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/3/22 9:19:15
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;
using UnityEngine;

public enum EliteMonsterTransition
{
    NullTransition,
    Waitting,
    SeaEnemy,
    Reached,
    HoldSuccess,
    HoldFail,
}

public enum EliteMonsterStateID
{
    NullState,
    Stroll,
    Chase,
    Holding,
    Success,
    Defeat,
}


public abstract class IEliteMonsterState
{
    protected Dictionary<EliteMonsterTransition, EliteMonsterStateID> mMap = new Dictionary<EliteMonsterTransition, EliteMonsterStateID>();
    protected EliteMonsterStateID mStateID;
    protected ICharacter mCharacter;
    protected EliteMonsterFSMSystem mFSMSystem;

    public IEliteMonsterState(EliteMonsterFSMSystem fsm, ICharacter character)
    {
        mFSMSystem = fsm;
        mCharacter = character;
    }

    public EliteMonsterStateID stateID { get { return mStateID; } }

    public void AddTransition(EliteMonsterTransition trans, EliteMonsterStateID id)
    {
        if (trans == EliteMonsterTransition.NullTransition)
        {
            Debug.LogError("EliteMonsterState Error: trans不能为空"); return;
        }
        if (id == EliteMonsterStateID.NullState)
        {
            Debug.LogError("EliteMonsterState Error: 状态ID不能为空"); return;
        }
        if (mMap.ContainsKey(trans))
        {
            Debug.LogError("EliteMonsterState Error: " + trans + " 已经添加上了"); return;
        }
        mMap.Add(trans, id);
    }

    public void DeleteTransition(EliteMonsterTransition trans)
    {
        if (mMap.ContainsKey(trans) == false)
        {
            Debug.LogError("删除转换条件的时候， 转换条件：[" + trans + "]不存在"); return;
        }
        mMap.Remove(trans);
    }

    public EliteMonsterStateID GetOutPutState(EliteMonsterTransition trans)
    {
        if (mMap.ContainsKey(trans) == false)
        {
            return EliteMonsterStateID.NullState;
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