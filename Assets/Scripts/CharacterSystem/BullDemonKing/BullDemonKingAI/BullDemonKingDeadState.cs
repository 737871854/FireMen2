/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   BullDemonKingDeadState.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/4/19 17:15:37
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;

public class BullDemonKingDeadState : IBullDemonKingState
{
    public BullDemonKingDeadState(BullDemonKingFSMSystem fsm, ICharacter character) : base(fsm, character)
    {
        mStateID = BullDemonKingStateID.Dead;
    }

    private bool mAnimIsOver;
    public override void DoBeforeEntering()
    {
        mAnimIsOver = false;
        mCharacter.PlayAnim("dead", 10);
    }

    public override void Act(E_ActionType actionType)
    {
        mAnimIsOver = mCharacter.AnimIsOver("dead");
    }

    public override void Reason(E_ActionType actionType)
    {
       if(mAnimIsOver)
            mFSMSystem.PerformTransition(BullDemonKingTransition.Disappear);
    }
}