/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   BullDemonKingFireFistState.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/4/19 17:28:23
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;

public class BullDemonKingFireFistState : IBullDemonKingState
{
    public BullDemonKingFireFistState(BullDemonKingFSMSystem fsm, ICharacter character) : base(fsm, character)
    {
        mStateID = BullDemonKingStateID.FireFist;
    }

    private bool mBeBreaked;
    private float mNormalTimer;
    private bool mAnimisOver;
    private BullDemonKing mBulldemonKing;
    public override void DoBeforeEntering()
    {
        mBeBreaked = false;
        mNormalTimer = 0;
        mAnimisOver = false;
        mBulldemonKing = mCharacter as BullDemonKing;
        mCharacter.PlayAnim("firefist", 4);
        mCharacter.AnimSpeed(0.05f);
        EventDispatcher.TriggerEvent(EventDefine.Event_Bull_Demon_King_Use_Skill_Fire_Fist);
        mBulldemonKing.DoLensOP();
    }

    public override void DoBeforeLeaving()
    {
        mBulldemonKing.DoLensReserve();
    }

    public override void Act(E_ActionType actionType)
    {
        mNormalTimer = mCharacter.AnimNormalizedTime("firefist");
        mAnimisOver = mCharacter.AnimIsOver("firefist");
        if (mNormalTimer < 0.5f)
        {
            if (!mBulldemonKing.IsInvincible)
            {
                mBeBreaked = true;
                mBulldemonKing.OnSkillBreaked();
            }
        }
        else
        {
            if(mBulldemonKing.IsInvincible)
            {
                // 回复正常播放动画速度
                mCharacter.AnimSpeed(1.0f);
                // 清除射击点
                EventDispatcher.TriggerEvent(EventDefine.Event_DisActive_HitPoint);
            }
        
            if(mAnimisOver)
            {
                // 相机震动
                ioo.cameraManager.BossShortShake();
                // 对玩家造成伤害
                int[] args = new int[] { -1, mCharacter.attr.baseAttr.id, mCharacter.attr.baseAttr.damageValue };
                ioo.gameEventSystem.NotifySubject(GameEventType.PlayerOnDamage, args);

                mBulldemonKing.CrashPoint.AddScreenCrash();
            }
        }
    }

    public override void Reason(E_ActionType actionType)
    {
        if(mBeBreaked)
        {
            mFSMSystem.PerformTransition(BullDemonKingTransition.BreakFireFist);
        }
        else if(mAnimisOver)
        {
            mFSMSystem.PerformTransition(BullDemonKingTransition.Rest);
        }
    }
}