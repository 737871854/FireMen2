/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   INpcState.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/3/20 15:18:02
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;
using UnityEngine;

public enum NpcTransition
{
    NullTansition,
    Fired,
    Rescued,
}

public enum NpcStateID
{
    NullState,
    Run,
    Cheerful,
}

public abstract class INpcState
{
    protected Dictionary<NpcTransition, NpcStateID> mMap = new Dictionary<NpcTransition, NpcStateID>();
    protected NpcStateID mStateID;
    protected ICharacter mCharacter;
    protected NpcFSMSystem mFSM;

    public INpcState(NpcFSMSystem fsm, ICharacter character)
    {
        mFSM = fsm;
        mCharacter = character;
    }

    public NpcStateID stateID { get { return mStateID; } }

    public void AddTransition(NpcTransition trans, NpcStateID id)
    {
        if(trans == NpcTransition.NullTansition)
        {
            Debug.LogError("NpcState Error: trans不能为空"); return;
        }
        if(id == NpcStateID.NullState)
        {
            Debug.LogError("NpcState Error: 状态ID不能为空"); return;
        }
        if(mMap.ContainsKey(trans))
        {
            Debug.LogError("NpcState Error: " + trans + " 已经添加上了"); return;
        }
        mMap.Add(trans, id);
    }

    public void DeleteTransition(NpcTransition trans)
    {
        if(mMap.ContainsKey(trans) == false)
        {
            Debug.LogError("删除转换条件的时候， 转换条件：[" + trans + "]不存在"); return;
        }
        mMap.Remove(trans);
    }

    public NpcStateID GetOutPutState(NpcTransition trans)
    {
        if(mMap.ContainsKey(trans) == false)
        {
            return NpcStateID.NullState;
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