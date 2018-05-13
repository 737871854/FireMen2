/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   BullDemonKingSofaState.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/4/19 17:23:28
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;

public class BullDemonKingSofaState : IBullDemonKingState
{
    public BullDemonKingSofaState(BullDemonKingFSMSystem fsm, ICharacter character) : base(fsm, character)
    {
        mStateID = BullDemonKingStateID.Sofa;
    }

    private float mNormalTimer;
    private bool mAnimisOver;
    private bool mUsedSkill;
    private BullDemonKing mBulldemonKing;
    public override void DoBeforeEntering()
    {
        mUsedSkill = false;
        mAnimisOver = false;
        mNormalTimer = 0;
        mBulldemonKing = mCharacter as BullDemonKing;
        mCharacter.PlayAnim("sofa", 3);
    }

    public override void Act(E_ActionType actionType)
    {
        mNormalTimer = mCharacter.AnimNormalizedTime("sofa");
        mAnimisOver = mCharacter.AnimIsOver("sofa");
        if (mNormalTimer >= 0.7f && !mUsedSkill)
        {
            mUsedSkill = true;
            if(mBulldemonKing.currentLocation == BullDemonKing.E_BossLocation.Left)
                EventDispatcher.TriggerEvent(EventDefine.Event_Bull_Demon_King_Use_Skill_Left_Sofa);
            else if(mBulldemonKing.currentLocation == BullDemonKing.E_BossLocation.Right)
                EventDispatcher.TriggerEvent(EventDefine.Event_Bull_Demon_King_Use_Skill_Right_Sofa);
        }
    }

    public override void Reason(E_ActionType actionType)
    {
        if (mAnimisOver)
            mFSMSystem.PerformTransition(BullDemonKingTransition.Rest);
    }
}