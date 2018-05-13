/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   BullDemonKingBreakFireCricleState.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/4/19 17:34:01
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;

public class BullDemonKingBreakFireCricleState : IBullDemonKingState
{
    public BullDemonKingBreakFireCricleState(BullDemonKingFSMSystem fsm, ICharacter character) : base(fsm, character)
    {
        mStateID = BullDemonKingStateID.BreakFireCricle;
    }

    private bool mAnimIsOver;
    public override void DoBeforeEntering()
    {
        mAnimIsOver = false;
        mCharacter.AnimSpeed(1.0f);
        mCharacter.PlayAnim("breakfirecricle", 9);
    }

    public override void Act(E_ActionType actionType)
    {
        mAnimIsOver = mCharacter.AnimIsOver("breakfirecricle");
    }

    public override void Reason(E_ActionType actionType)
    {
        if (mAnimIsOver)
            mFSMSystem.PerformTransition(BullDemonKingTransition.Rest);
    }
}