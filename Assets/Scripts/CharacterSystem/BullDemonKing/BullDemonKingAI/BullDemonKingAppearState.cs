/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   BullDemonKingAppearState.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/4/19 17:16:19
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;

public class BullDemonKingAppearState : IBullDemonKingState
{
    public BullDemonKingAppearState(BullDemonKingFSMSystem fsm, ICharacter character) : base(fsm, character)
    {
        mStateID = BullDemonKingStateID.Appear;
    }

    public override void DoBeforeLeaving()
    {
        ioo.TriggerListener(EventLuaDefine.Event_Boss_Born);
    }

    private bool mReached;
    private float mWaittingTimer = 0.8f;
    public override void Act(E_ActionType actionType)
    {
        BullDemonKing bdk = mCharacter as BullDemonKing;
        UnityEngine.Vector3 pos = bdk.MiddlePos();
        if(mWaittingTimer > 0)
        {
            mWaittingTimer -= UnityEngine.Time.deltaTime;
            return;
        }
        mReached = mCharacter.MoveStraight(pos);
        mCharacter.LookAtCamera();
    }

    public override void Reason(E_ActionType actionType)
    {
        if (mReached)
        {
            (mCharacter as BullDemonKing).ReachMiddle();
            mFSMSystem.PerformTransition(BullDemonKingTransition.Rest);
        }
    }
}