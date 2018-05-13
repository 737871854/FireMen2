/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   IHelicopterState.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/3/21 8:52:51
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;
using UnityEngine;

public enum HelicopterTransition
{
    NullTransition,
    ToSave,
    Save,
    Hold,
    Rescued,
    Success,
}

public enum HelicopterStateID
{
    NullState,
    Come,
    Drop,
    Watting,
    Recover,
    Leave,
}


public abstract class IHelicopterState
{
    protected Dictionary<HelicopterTransition, HelicopterStateID> mMap = new Dictionary<HelicopterTransition, HelicopterStateID>();
    protected HelicopterStateID mStateID;
    protected ICharacter mCharacter;
    protected HelicopterFSMSystem mFSMSystem;

    public IHelicopterState(HelicopterFSMSystem fsm, ICharacter character)
    {
        mFSMSystem = fsm;
        mCharacter = character;
    }

    public HelicopterStateID stateID { get { return mStateID; } }

    public void AddTransition(HelicopterTransition trans, HelicopterStateID id)
    {
        if (trans == HelicopterTransition.NullTransition)
        {
            Debug.LogError("HelicopterState Error: trans不能为空"); return;
        }
        if (id == HelicopterStateID.NullState)
        {
            Debug.LogError("HelicopterState Error: 状态ID不能为空"); return;
        }
        if (mMap.ContainsKey(trans))
        {
            Debug.LogError("HelicopterState Error: " + trans + " 已经添加上了"); return;
        }
        mMap.Add(trans, id);
    }

    public void DeleteTransition(HelicopterTransition trans)
    {
        if(mMap.ContainsKey(trans) == false)
        {
            Debug.LogError("删除转换条件的时候， 转换条件：[" + trans + "]不存在"); return;
        }
        mMap.Remove(trans);
    }

    public HelicopterStateID GetOutPutState(HelicopterTransition trans)
    {
        if(mMap.ContainsKey(trans) == false)
        {
            return HelicopterStateID.NullState;
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