/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   IHugeFireMonster.cs
 * 
 * 简    介:    大火怪状态基类
 * 
 * 创建标识：   Pancake 2018/4/8 9:08:12
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;
using UnityEngine;

public enum HugeFireMonsterTransition
{
    NullTransition = 0,
    CanAttack,
    SeaEnemy,
    Attacked,
}

public enum HugeFireMonsterStateID
{
    NullState,
    Chase,
    Attack,
    Leave,
}
public abstract class IHugeFireMonsterState
{
    protected Dictionary<HugeFireMonsterTransition, HugeFireMonsterStateID> mMap = new Dictionary<HugeFireMonsterTransition, HugeFireMonsterStateID>();
    protected HugeFireMonsterStateID mStateID;
    protected ICharacter mCharacter;
    protected HugeFireMonsterFSMSystem mFSM;


    public IHugeFireMonsterState(HugeFireMonsterFSMSystem fsm, ICharacter character)
    {
        mFSM = fsm;
        mCharacter = character;
    }

    public HugeFireMonsterStateID stateID { get { return mStateID; } }

    public void AddTransition(HugeFireMonsterTransition trans, HugeFireMonsterStateID id)
    {
        if (trans == HugeFireMonsterTransition.NullTransition)
        {
            Debug.LogError("FireMonsterState Error: trans不能为空"); return;
        }
        if (id == HugeFireMonsterStateID.NullState)
        {
            Debug.LogError("FireMonsterState Error: 状态ID不能为空"); return;
        }
        if (mMap.ContainsKey(trans))
        {
            Debug.LogError("FireMonsterState Error: " + trans + " 已经添加上了"); return;
        }
        mMap.Add(trans, id);
    }

    public void DeleteTransition(HugeFireMonsterTransition trans)
    {
        if (mMap.ContainsKey(trans) == false)
        {
            Debug.LogError("删除转换条件的时候， 转换条件：[" + trans + "]不存在"); return;
        }
        mMap.Remove(trans);
    }

    public HugeFireMonsterStateID GetOutPutState(HugeFireMonsterTransition trans)
    {
        if (mMap.ContainsKey(trans) == false)
        {
            return HugeFireMonsterStateID.NullState;
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