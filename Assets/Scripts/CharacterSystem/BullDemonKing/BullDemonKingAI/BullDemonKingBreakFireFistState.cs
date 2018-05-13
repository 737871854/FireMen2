/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   BullDemonKingBreakFireFistState.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/4/19 17:29:34
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;

public class BullDemonKingBreakFireFistState : IBullDemonKingState
{
    public BullDemonKingBreakFireFistState(BullDemonKingFSMSystem fsm, ICharacter character) : base(fsm, character)
    {
        mStateID = BullDemonKingStateID.BreakFireFist;
    }

    private bool mAnimIsOver;

    public override void DoBeforeEntering()
    {
        mAnimIsOver = false;
        mCharacter.AnimSpeed(1.0f);
        mCharacter.PlayAnim("breakfirefist", 7);
    }

    public override void Act(E_ActionType actionType)
    {
        mAnimIsOver = mCharacter.AnimIsOver("breakfirefist");
    }

    public override void Reason(E_ActionType actionType)
    {
        if (mAnimIsOver)
            mFSMSystem.PerformTransition(BullDemonKingTransition.Rest);
    }
}