/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   CitizenFSMSystem.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/3/20 10:06:38
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;
using UnityEngine;

public class CitizenFSMSystem
{
    private List<ICitizenState> mStates = new List<ICitizenState>();
    private ICitizenState mCurrentState;
    public ICitizenState currentState { get { return mCurrentState; } }

    public void AddState(params ICitizenState[] states)
    {
        foreach(ICitizenState s in states)
        {
            AddState(s);
        }
    }

    public void AddState(ICitizenState state)
    {
        if (state == null)
        {
            Debug.LogError("要添加的状态为空");
            return;
        }

        if(mStates.Count == 0)
        {
            mStates.Add(state);
            mCurrentState = state;
            currentState.DoBeforeEntering();
            return;
        }

        foreach(ICitizenState s in mStates)
        {
            if(s.stateID == state.stateID)
            {
                Debug.LogError("要添加的状态ID[" + s.stateID + "]已经添加");
                return;
            }
        }
        mStates.Add(state);
    }

    public void DeleteState(CitizenStateID stateID)
    {
        if(stateID == CitizenStateID.NullState)
        {
            Debug.LogError("要删除的状态ID为空" + stateID);return;
        }
        foreach(ICitizenState s in mStates)
        {
            if(s.stateID == stateID)
            {
                mStates.Remove(s);return;
            }
        }
        Debug.LogError("要删除的SateID不存在集合中：" + stateID);
    }

    public void PerformTransition(CitizenTransition trans)
    {
        if(trans == CitizenTransition.NullTransition)
        {
            Debug.LogError("要执行的转换条件为空：" + trans);return;
        }

        CitizenStateID nextStateID = mCurrentState.GetOutPutState(trans);
        if(nextStateID == CitizenStateID.NullState)
        {
            Debug.LogError("在转换条件[" + trans + "]下，没有对应的转换状态");return;
        }
        foreach(ICitizenState s in mStates)
        {
            if(s.stateID == nextStateID)
            {
                mCurrentState.DoBeforeLeaving();
                mCurrentState = s;
                mCurrentState.DoBeforeEntering();
                return;
            }
        }
    }
}