/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   ICitizenState.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/3/20 10:05:00
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;
using UnityEngine;

public enum CitizenTransition
{
    NullTransition,
    Trapped, // 被困
    AirPlaneReached, // 救援到达
    BeChased,// 被追逐
    Rescued,
}

public enum CitizenStateID
{
    NullState,
    Help, // 呼救
    Climb,// 攀爬
    Run,// 奔跑
    Disappear,
}


public abstract class ICitizenState
{
    protected Dictionary<CitizenTransition, CitizenStateID> mMap = new Dictionary<CitizenTransition, CitizenStateID>();
    protected CitizenStateID mStateID;
    protected ICharacter mCharacter;
    protected CitizenFSMSystem mFSMSystem;

    public ICitizenState(CitizenFSMSystem fsm, ICharacter character)
    {
        mFSMSystem = fsm;
        mCharacter = character;
    }

    public CitizenStateID stateID { get { return mStateID; } }

    public void AddTransition(CitizenTransition trans, CitizenStateID id)
    {
        if(trans == CitizenTransition.NullTransition)
        {
            Debug.LogError("CitizenrState Error: trans不能为空"); return;
        }
        if (id == CitizenStateID.NullState)
        {
            Debug.LogError("CitizenrState Error: 状态ID不能为空"); return;
        }
        if (mMap.ContainsKey(trans))
        {
            Debug.LogError("CitizenrState Error: " + trans + " 已经添加上了"); return;
        }
        mMap.Add(trans, id);
    }

    public void DeleteTransition(CitizenTransition trans)
    {
        if(mMap.ContainsKey(trans) == false)
        {
            Debug.LogError("删除转换条件的时候， 转换条件：[" + trans + "]不存在"); return;
        }
        mMap.Remove(trans);
    }

    public CitizenStateID GetOutPutState(CitizenTransition trans)
    {
        if(mMap.ContainsKey(trans) == false)
        {
            return CitizenStateID.NullState;
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