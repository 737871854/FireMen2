/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   BearMoveState.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/4/24 17:07:36
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;
using UnityEngine;

public class BearChaseState : IBearState
{
    public BearChaseState(BearFSMSystem fsm, ICharacter character) : base(fsm, character)
    {
        mStateID = BearStateID.Chase;
    }

    private Bear mBear;
    private bool mReached;
    public override void DoBeforeEntering()
    {
        mBear = mCharacter as Bear;
        mCharacter.PlayAnim("run", 3);
        mReached = false;
        mBear.NormalSpeed();
    }

    public override void Act(E_ActionType actionType)
    {
        Vector3 direction = ioo.cameraManager.position - mCharacter.position;
        if (direction.magnitude >= mBear.CheckDistance())//TODO待实际情况调整
            mBear.NMAMove();
        else
            mReached = true;
    }

    public override void Reason(E_ActionType actionType)
    {
        bool skillIsOut = mBear.CurrentStepSkillIsOut();
     
        if(skillIsOut)
            mBear.NexStep();
        else
        {
            if (mReached)
                mBear.UseSkill();
        }
    }
}