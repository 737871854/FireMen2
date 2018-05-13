/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   BullDemonKingRunState.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/4/19 15:18:53
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;
using UnityEngine;

public class BullDemonKingMoveState : IBullDemonKingState
{
    public BullDemonKingMoveState(BullDemonKingFSMSystem fsm, ICharacter character) : base(fsm, character)
    {
        mStateID = BullDemonKingStateID.Move;
    }

    private Vector3 mTargetPos;
    public override void DoBeforeEntering()
    {
        mTargetPos = (mCharacter as BullDemonKing).CurrentLocationPosition;
    }

    private bool mReached;
    public override void Act(E_ActionType actionType)
    {
        mReached = mCharacter.MoveTo(mTargetPos);
    }

    public override void Reason(E_ActionType actionType)
    {
        if(mReached)
            mFSMSystem.PerformTransition(BullDemonKingTransition.Rest);
    }
}