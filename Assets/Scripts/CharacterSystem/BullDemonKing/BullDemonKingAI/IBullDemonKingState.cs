/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   IBullDemonKingStage.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/4/19 15:17:23
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;
using UnityEngine;

public enum BullDemonKingTransition
{
    NullTransition,
    Appear,// 出场
    Rest,// 休息
    NotInRightPosition,// 玩家
    Desk,
    Sofa,

    FireFist,
    OXHorn,
    FireCricle,
    BreakFireFist,
    BreakOXhorn,
    BreakFireCricle,

    NoHealth,
    Disappear,
}

public enum BullDemonKingStateID
{
    NullState,
    Appear,
    Idle,
    Move,
    Desk,
    Sofa,

    FireFist,
    OXHorn,
    FireCricle,
    BreakFireFist,
    BreakOXhorn,
    BreakFireCricle,

    Dead,
    Disappear,
}

public abstract class IBullDemonKingState
{
    protected Dictionary<BullDemonKingTransition, BullDemonKingStateID> mMap = new Dictionary<BullDemonKingTransition, BullDemonKingStateID>();
    protected BullDemonKingStateID mStateID;
    protected ICharacter mCharacter;
    protected BullDemonKingFSMSystem mFSMSystem;

    public IBullDemonKingState(BullDemonKingFSMSystem fsm, ICharacter character )
    {
        mFSMSystem = fsm;
        mCharacter = character;
    }

    public BullDemonKingStateID stateID { get { return mStateID; } }

    public void AddTransition(BullDemonKingTransition trans, BullDemonKingStateID id)
    {
        if (trans == BullDemonKingTransition.NullTransition)
        {
            Debug.LogError("BullDemonKingState Error: trans不能为空"); return;
        }
        if (id == BullDemonKingStateID.NullState)
        {
            Debug.LogError("BullDemonKingState Error: 状态ID不能为空"); return;
        }
        if (mMap.ContainsKey(trans))
        {
            Debug.LogError("BullDemonKingState Error: " + trans + " 已经添加上了"); return;
        }
        mMap.Add(trans, id);
    }

    public void DeleteTransition(BullDemonKingTransition trans)
    {
        if (mMap.ContainsKey(trans) == false)
        {
            Debug.LogError("删除转换条件的时候， 转换条件：[" + trans + "]不存在"); return;
        }
        mMap.Remove(trans);
    }

    public BullDemonKingStateID GetOutPutState(BullDemonKingTransition trans)
    {
        if (mMap.ContainsKey(trans) == false)
        {
            return BullDemonKingStateID.NullState;
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