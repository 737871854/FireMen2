/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   WolfLeaveState.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/5/3 14:35:43
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;
using UnityEngine;

public class WolfLeaveState : IWolfState
{
    public WolfLeaveState(WolfFSMSystem fsm, ICharacter character) : base(fsm, character)
    {
        mStateID = WolfStateID.Leave;
    }

    private Wolf mWolf;
    private Vector3 mTargetPos;
    private bool mReached;
    public override void DoBeforeEntering()
    {
        if(mCharacter.actionType == E_ActionType.Normal)
            ioo.cameraManager.PlayCPA();
        mWolf = mCharacter as Wolf;
        mWolf.CanWalk(true);
        mTargetPos = ioo.cameraManager.position - ioo.cameraManager.right;
    }

    public override void Act(E_ActionType actionType)
    {
        Vector3 pos;
        mWolf.MoveToTarget(mTargetPos, out pos);
        mReached = Vector3.Distance(mCharacter.position, pos) < 1.0f ? true : false;
    }

    public override void Reason(E_ActionType actionType)
    {
        if (mReached)
            mCharacter.Killed();
    }
}