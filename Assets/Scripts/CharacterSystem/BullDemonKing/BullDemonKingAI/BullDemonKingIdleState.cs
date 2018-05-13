/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   BullDemonKingIdle.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/4/19 15:18:18
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;

public class BullDemonKingIdleState : IBullDemonKingState
{
    public BullDemonKingIdleState(BullDemonKingFSMSystem fsm, ICharacter character) : base(fsm, character)
    {
        mStateID = BullDemonKingStateID.Idle;
    }

    private float mTimer;
    private BullDemonKing mBullDemonKing;
    public override void DoBeforeEntering()
    {
        mTimer = 0;
        mBullDemonKing = mCharacter as BullDemonKing;
        mCharacter.PlayAnim("idle", 0);
    }

    public override void Act(E_ActionType actionType)
    {
        if(!mBullDemonKing.IsInvincible)
            mTimer += UnityEngine.Time.deltaTime;
        mCharacter.LookAtCamera();
    }

    public override void Reason(E_ActionType actionType)
    {
        if(mBullDemonKing.isDead)
        {
            mFSMSystem.PerformTransition(BullDemonKingTransition.NoHealth);
            return;
        }

        if (mTimer >= 2.0f)
        {
            if(mBullDemonKing.NeedBack())
            {
                mFSMSystem.PerformTransition(BullDemonKingTransition.NotInRightPosition);
                return;
            }


            bool skillIsOut = mBullDemonKing.CurrentLocationSkillIsOut();
            if(!skillIsOut)
                mBullDemonKing.UseSkill();
            else
            {
                mBullDemonKing.NextLocation();
                mFSMSystem.PerformTransition(BullDemonKingTransition.NotInRightPosition);
            }
        }
    }

}