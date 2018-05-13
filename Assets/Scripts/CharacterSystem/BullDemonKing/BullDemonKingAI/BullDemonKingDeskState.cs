/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   BullDemonKingDeskState.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/4/19 17:22:41
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;

public class BullDemonKingDeskState : IBullDemonKingState
{
    public BullDemonKingDeskState(BullDemonKingFSMSystem fsm, ICharacter character) : base(fsm, character)
    {
        mStateID = BullDemonKingStateID.Desk;
    }

    public override void DoBeforeEntering()
    {
        mUsedSkill = false;
        mBulldemonKing = mCharacter as BullDemonKing;
        mCharacter.PlayAnim("desk", 2);
    }

    private float mNormalTimer;
    private bool mAnimisOver;
    private bool mUsedSkill;
    private BullDemonKing mBulldemonKing;
    public override void Act(E_ActionType actionType)
    {
        mNormalTimer = mCharacter.AnimNormalizedTime("desk");
        mAnimisOver = mCharacter.AnimIsOver("desk");
        if (mNormalTimer >= 0.4f && !mUsedSkill)
        {
            mUsedSkill = true;
            EventDispatcher.TriggerEvent(EventDefine.Event_Bull_Demon_King_Use_Skill_Desk);
        }
    }

    public override void Reason(E_ActionType actionType)
    {
        if(mAnimisOver)
            mFSMSystem.PerformTransition(BullDemonKingTransition.Rest);
    }
}