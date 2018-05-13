/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   IBearState.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/4/24 16:58:10
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;
using UnityEngine;

public enum BearTransition
{
    NullTransition,
    Appear,
    Rest,
    SeaEnemy,
    Howl,
    Claw,
    Stone,
    Tree,
    Beat,
    FireBall,
    BreakBeat,
    BreakClaw,
    FireBallSuccess,
    SpeedUp,
    Follow,
    ToHome,
    NoHealth,
    Disappear,
}

public enum BearStateID
{
    NullState,
    Appear,
    Idle,
    Chase,
    Howl,
    Claw,
    Stone,
    Tree,
    Beat,
    FireBall,
    BreakBeat,
    BreakClaw,
    FireBallSuccess,
    SpeedUp,
    Follow,
    ToHome,
    Dead,
    Disappear,
}


public abstract class IBearState
{
    protected Dictionary<BearTransition, BearStateID> mMap = new Dictionary<BearTransition, BearStateID>();
    protected BearStateID mStateID;
    protected ICharacter mCharacter;
    protected BearFSMSystem mFSMSystem;

    public IBearState(BearFSMSystem fsm, ICharacter character)
    {
        mFSMSystem = fsm;
        mCharacter = character;
    }

    public BearStateID stateID { get { return mStateID; } }

    public void AddTransition(BearTransition trans, BearStateID id)
    {
        if (trans == BearTransition.NullTransition)
        {
            Debug.LogError("BearState Error: trans不能为空"); return;
        }
        if (id == BearStateID.NullState)
        {
            Debug.LogError("BearState Error: 状态ID不能为空"); return;
        }
        if (mMap.ContainsKey(trans))
        {
            Debug.LogError("BearState Error: " + trans + " 已经添加上了"); return;
        }
        mMap.Add(trans, id);
    }

    public void DeleteTransition(BearTransition trans)
    {
        if (mMap.ContainsKey(trans) == false)
        {
            Debug.LogError("删除转换条件的时候， 转换条件：[" + trans + "]不存在"); return;
        }
        mMap.Remove(trans);
    }

    public BearStateID GetOutPutState(BearTransition trans)
    {
        if (mMap.ContainsKey(trans) == false)
        {
            return BearStateID.NullState;
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