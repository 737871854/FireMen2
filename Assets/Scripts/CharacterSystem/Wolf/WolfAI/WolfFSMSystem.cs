/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   WolfFSMSystem.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/5/2 16:22:01
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;
using UnityEngine;

public class WolfFSMSystem
{
    private List<IWolfState> mStates = new List<IWolfState>();
    private IWolfState mCurrentState;
    public IWolfState currentState { get { return mCurrentState; } }

    public void AddState(params IWolfState[] states)
    {
        foreach (IWolfState s in states)
        {
            AddState(s);
        }
    }

    public void AddState(IWolfState state)
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

        foreach (IWolfState s in mStates)
        {
            if (s.stateID == state.stateID)
            {
                Debug.LogError("要添加的状态ID[" + s.stateID + "]已经添加"); return;
            }
        }
        mStates.Add(state);
    }

    public void DeleteState(WolfStateID stateID)
    {
        if (stateID == WolfStateID.NullState)
        {
            Debug.LogError("要删除的状态ID为空" + stateID); return;
        }
        foreach (IWolfState s in mStates)
        {
            if (s.stateID == stateID)
            {
                mStates.Remove(s); return;
            }
        }
        Debug.LogError("要删除的StateID不存在集合中：" + stateID);
    }

    public void PerformTransition(WolfTransition trans)
    {
        if (trans == WolfTransition.NullTansition)
        {
            Debug.LogError("要执行的转换条件为空 ： " + trans); return;
        }
        WolfStateID nextStateID = mCurrentState.GetOutPutState(trans);
        if (nextStateID == WolfStateID.NullState)
        {
            Debug.LogError("在转换条件 [" + trans + "] 下，没有对应的转换状态"); return;
        }
        foreach (IWolfState s in mStates)
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