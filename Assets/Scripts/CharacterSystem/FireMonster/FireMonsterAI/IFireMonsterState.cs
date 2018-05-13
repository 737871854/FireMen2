/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   IFireMonsterState.cs
 * 
 * 简    介:    小火怪，烟雾怪状态基类
 * 
 * 创建标识：   Pancake 2018/3/2 14:25:36
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;
using UnityEngine;

public enum FireMonsterTransition
{
    NullTransition  = 0,
    CanAttack,
    SeaEnemy,
    Explode,
}

public enum FireMonsterStateID
{
    NullState,
    Chase,
    Attack,
    Explode,
}

public abstract class IFireMonsterState
{
    protected Dictionary<FireMonsterTransition, FireMonsterStateID> mMap = new Dictionary<FireMonsterTransition, FireMonsterStateID>();
    protected FireMonsterStateID mStateID;
    protected ICharacter mCharacter;
    protected FireMonsterFSMSystem mFSMSystem;


    public IFireMonsterState(FireMonsterFSMSystem fsm, ICharacter character)
    {
        mFSMSystem = fsm;
        mCharacter = character;
    }

    public FireMonsterStateID stateID { get { return mStateID; } }

    public void AddTransition(FireMonsterTransition trans, FireMonsterStateID id)
    {
        if (trans == FireMonsterTransition.NullTransition)
        {
            Debug.LogError("FireMonsterState Error: trans不能为空"); return;
        }
        if (id == FireMonsterStateID.NullState)
        {
            Debug.LogError("FireMonsterState Error: 状态ID不能为空"); return;
        }
        if (mMap.ContainsKey(trans))
        {
            Debug.LogError("FireMonsterState Error: " + trans + " 已经添加上了"); return;
        }
        mMap.Add(trans, id);
    }

    public void DeleteTransition(FireMonsterTransition trans)
    {
        if(mMap.ContainsKey(trans) == false)
        {
            Debug.LogError("删除转换条件的时候， 转换条件：[" + trans + "]不存在"); return;
        }
        mMap.Remove(trans);
    }

    public FireMonsterStateID GetOutPutState(FireMonsterTransition trans)
    {
        if(mMap.ContainsKey(trans) == false)
        {
            return FireMonsterStateID.NullState;
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