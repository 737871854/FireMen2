﻿/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   BullDemonKingFSMSystem.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/4/19 15:19:39
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;
using UnityEngine;

public class BullDemonKingFSMSystem
{
    private List<IBullDemonKingState> mStates = new List<IBullDemonKingState>();
    private IBullDemonKingState mCurrentState;
    public IBullDemonKingState currentState { get { return mCurrentState; } }

    public void AddState(params IBullDemonKingState[] states)
    {
        foreach (IBullDemonKingState s in states)
        {
            AddState(s);
        }
    }

    public void AddState(IBullDemonKingState state)
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

        foreach (IBullDemonKingState s in mStates)
        {
            if (s.stateID == state.stateID)
            {
                Debug.LogError("要添加的状态ID[" + s.stateID + "]已经添加");
                return;
            }
        }
        mStates.Add(state);
    }

    public void DeleteState(BullDemonKingStateID stateID)
    {
        if (stateID == BullDemonKingStateID.NullState)
        {
            Debug.LogError("要删除的状态ID为空" + stateID); return;
        }
        foreach (IBullDemonKingState s in mStates)
        {
            if (s.stateID == stateID)
            {
                mStates.Remove(s); return;
            }
        }
        Debug.LogError("要删除的SateID不存在集合中：" + stateID);
    }

    public void PerformTransition(BullDemonKingTransition trans)
    {
        if (trans == BullDemonKingTransition.NullTransition)
        {
            Debug.LogError("要执行的转换条件为空：" + trans); return;
        }

        BullDemonKingStateID nextStateID = mCurrentState.GetOutPutState(trans);
        if (nextStateID == BullDemonKingStateID.NullState)
        {
            Debug.LogError("在转换条件[" + trans + "]下，没有对应的转换状态"); return;
        }
        foreach (IBullDemonKingState s in mStates)
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