/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   HelicopterFSMSystem.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/4/18 16:54:59
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterFSMSystem
{
    private List<IHelicopterState> mStates = new List<IHelicopterState>();
    private IHelicopterState mCurrentState;
    public IHelicopterState currentState { get { return mCurrentState; } }

    public void AddState(params IHelicopterState[] states)
    {
        foreach (IHelicopterState s in states)
        {
            AddState(s);
        }
    }

    public void AddState(IHelicopterState state)
    {
        if (state == null)
        {
            Debug.LogError("要添加的状态为空"); return;
        }

        if (mStates.Count == 0)
        {
            mStates.Add(state);
            mCurrentState = state;
            mCurrentState.DoBeforeEntering();
            return;
        }

        foreach (IHelicopterState s in mStates)
        {
            if (s.stateID == state.stateID)
            {
                Debug.LogError("要添加的状态ID[" + s.stateID + "]已经添加"); return;
            }
        }
        mStates.Add(state);
    }

    public void DeleteState(HelicopterStateID stateID)
    {
        if (stateID == HelicopterStateID.NullState)
        {
            Debug.LogError("要删除的状态ID为空" + stateID); return;
        }
        foreach (IHelicopterState s in mStates)
        {
            if (s.stateID == stateID)
            {
                mStates.Remove(s); return;
            }
        }
        Debug.LogError("要删除的StateID不存在集合中:" + stateID);
    }

    public void PerformTransition(HelicopterTransition trans)
    {
        if (trans == HelicopterTransition.NullTransition)
        {
            Debug.LogError("要执行的转换条件为空 ： " + trans); return;
        }
        HelicopterStateID nextStateID = mCurrentState.GetOutPutState(trans);
        if (nextStateID == HelicopterStateID.NullState)
        {
            Debug.LogError("在转换条件 [" + trans + "] 下，没有对应的转换状态"); return;
        }
        foreach (IHelicopterState s in mStates)
        {
            if (s.stateID == nextStateID)
            {
                mCurrentState.DoBeforeLeaving();
                mCurrentState = s;
                mCurrentState.DoBeforeEntering();
                return;
            }
        }
    }
}