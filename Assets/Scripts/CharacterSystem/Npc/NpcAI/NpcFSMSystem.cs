/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   NpcFSMSystem.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/3/20 15:18:54
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;
using UnityEngine;

public class NpcFSMSystem
{
    private List<INpcState> mStates = new List<INpcState>();
    private INpcState mCurrentState;
    public INpcState currentState { get { return mCurrentState; } }

    public void AddState(params INpcState[] states)
    {
        foreach(INpcState s in states)
        {
            AddState(s);
        }
    }

    public void AddState(INpcState state)
    {
        if (state == null)
        {
            Debug.LogError("要添加的状态为空"); return;
        }

        if(mStates.Count == 0)
        {
            mStates.Add(state);
            mCurrentState = state;
            mCurrentState.DoBeforeEntering();
            return;
        }

        foreach(INpcState s in mStates)
        {
            if(s.stateID == state.stateID)
            {
                Debug.LogError("要添加的状态ID[" + s.stateID + "]已经添加");return;
            }
        }
        mStates.Add(state);
    }

    public void DeleteState(NpcStateID stateID)
    {
        if(stateID == NpcStateID.NullState)
        {
            Debug.LogError("要删除的状态ID为空" + stateID);return;
        }
        foreach(INpcState s in mStates)
        {
            if(s.stateID == stateID)
            {
                mStates.Remove(s);return;
            }
        }
        Debug.LogError("要删除的StateID不存在集合中：" + stateID);
    }

    public void PerformTransition(NpcTransition trans)
    {
        if(trans == NpcTransition.NullTansition)
        {
            Debug.LogError("要执行的转换条件为空 ： " + trans); return;
        }
        NpcStateID nextStateID = mCurrentState.GetOutPutState(trans);
        if(nextStateID == NpcStateID.NullState)
        {
            Debug.LogError("在转换条件 [" + trans + "] 下，没有对应的转换状态"); return;
        }
        foreach(INpcState s in mStates)
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