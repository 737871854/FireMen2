/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   LeaveState.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/3/21 10:23:19
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterLeaveState : IHelicopterState
{
    public HelicopterLeaveState(HelicopterFSMSystem fsm, ICharacter character) : base(fsm, character)
    {
        mStateID = HelicopterStateID.Leave;
    }

    public override void DoBeforeEntering()
    {
        mCharacter.PlayAnim("run", 1);
        mTargetPos = mCharacter.position + mCharacter.gameObject.transform.forward * 8;
    }

    private Vector3 mTargetPos;
    private float mDistance;
    public override void Act(E_ActionType actionType)
    {
        mCharacter.MoveStraight(mTargetPos);
    }

    public override void Reason(E_ActionType actionType)
    {
        mDistance = Vector3.Distance(mCharacter.position, mTargetPos);
        if (mDistance < 0.5f)
            mCharacter.Killed();
    }
}