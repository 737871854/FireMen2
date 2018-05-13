/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   Npc.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/3/20 15:17:17
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Npc : ICharacter
{
    protected NpcFSMSystem mFSMSystem;
    
    public Npc()
    {
        MakeFSM();
    }

    public override void UpdateFSMAI(E_ActionType actionType)
    {
        if (mIsKilled || mIsPause) return;
        mFSMSystem.currentState.Act(actionType);
        mFSMSystem.currentState.Reason(actionType);
    }

    protected override void UpdateExtra()
    {
        mRigidbody.velocity = Vector3.zero;
    }

    private void MakeFSM()
    {
        mFSMSystem = new NpcFSMSystem();

        NpcRunState runState = new NpcRunState(mFSMSystem, this);
        runState.AddTransition(NpcTransition.Rescued, NpcStateID.Cheerful);

        NpcCheerfulState cheerfulState = new NpcCheerfulState(mFSMSystem, this);

        mFSMSystem.AddState(runState, cheerfulState);
    }
}