/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   BearFSMSystem.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/4/24 16:58:47
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;
using UnityEngine;

public class BearFSMSystem
{
    private List<IBearState> mStates = new List<IBearState>();
    private IBearState mCurrentState;
    public IBearState currentState { get { return mCurrentState; } }

    public void AddState(params IBearState[] states)
    {
        foreach (IBearState s in states)
        {
            AddState(s);
        }
    }

    public void AddState(IBearState state)
    {
        if (state == null)
        {
            Debug.LogError("要添加的状态为空");
            return;
        }

        if (mStates.Count == 0)
        {
            mStates.Add(state);
            mCurrentState = state;
            currentState.DoBeforeEntering();
            return;
        }

        foreach (IBearState s in mStates)
        {
            if (s.stateID == state.stateID)
            {
                Debug.LogError("要添加的状态ID[" + s.stateID + "]已经添加");
                return;
            }
        }
        mStates.Add(state);
    }

    public void DeleteState(BearStateID stateID)
    {
        if (stateID == BearStateID.NullState)
        {
            Debug.LogError("要删除的状态ID为空" + stateID); return;
        }
        foreach (IBearState s in mStates)
        {
            if (s.stateID == stateID)
            {
                mStates.Remove(s); return;
            }
        }
        Debug.LogError("要删除的SateID不存在集合中：" + stateID);
    }

    public void PerformTransition(BearTransition trans)
    {
        if (trans == BearTransition.NullTransition)
        {
            Debug.LogError("要执行的转换条件为空：" + trans); return;
        }

        BearStateID nextStateID = mCurrentState.GetOutPutState(trans);
        if (nextStateID == BearStateID.NullState)
        {
            Debug.LogError("在转换条件[" + trans + "]下，没有对应的转换状态"); return;
        }
        foreach (IBearState s in mStates)
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