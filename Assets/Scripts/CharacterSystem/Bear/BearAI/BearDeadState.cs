/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   BearDeadState.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/4/24 17:22:07
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;

public class BearDeadState : IBearState
{
    public BearDeadState(BearFSMSystem fsm, ICharacter character) : base(fsm, character)
    {
        mStateID = BearStateID.Dead;
    }

    public override void DoBeforeEntering()
    {
        mCharacter.PlayAnim("dead", 10);
    }

    private float mNormalTimer;
    public override void Act(E_ActionType actionType)
    {
        mNormalTimer = mCharacter.AnimNormalizedTime("dead");
    }

    public override void Reason(E_ActionType actionType)
    {
        if (mNormalTimer > 0.99f)
            mFSMSystem.PerformTransition(BearTransition.Disappear);
    }
}