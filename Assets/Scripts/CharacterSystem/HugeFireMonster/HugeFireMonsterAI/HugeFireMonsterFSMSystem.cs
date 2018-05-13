/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   HugeMonsterFSMSystem.cs
 * 
 * 简    介:    大火怪状态管理器
 * 
 * 创建标识：   Pancake 2018/4/8 9:09:18
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;
using UnityEngine;

public class HugeFireMonsterFSMSystem
{
    private List<IHugeFireMonsterState> mStates = new List<IHugeFireMonsterState>();
    private IHugeFireMonsterState mCurrentState;
    public IHugeFireMonsterState currentState { get { return mCurrentState; } }

    public void AddState(params IHugeFireMonsterState[] states)
    {
        foreach (IHugeFireMonsterState s in states)
        {
            AddState(s);
        }
    }

    public void AddState(IHugeFireMonsterState state)
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

        foreach (IHugeFireMonsterState s in mStates)
        {
            if (s.stateID == state.stateID)
            {
                Debug.LogError("要添加的状态ID[" + s.stateID + "]已经添加"); return;
            }
        }
        mStates.Add(state);
    }

    public void DeleteState(HugeFireMonsterStateID stateID)
    {
        if (stateID == HugeFireMonsterStateID.NullState)
        {
            Debug.LogError("要删除的状态ID为空" + stateID); return;
        }
        foreach (IHugeFireMonsterState s in mStates)
        {
            if (s.stateID == stateID)
            {
                mStates.Remove(s); return;
            }
        }
        Debug.LogError("要删除的StateID不存在集合中:" + stateID);
    }

    public void PerformTransition(HugeFireMonsterTransition trans)
    {
        if (trans == HugeFireMonsterTransition.NullTransition)
        {
            Debug.LogError("要执行的转换条件为空 ： " + trans); return;
        }
        HugeFireMonsterStateID nextStateID = mCurrentState.GetOutPutState(trans);
        if (nextStateID == HugeFireMonsterStateID.NullState)
        {
            Debug.LogError("在转换条件 [" + trans + "] 下，没有对应的转换状态"); return;
        }
        foreach (IHugeFireMonsterState s in mStates)
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