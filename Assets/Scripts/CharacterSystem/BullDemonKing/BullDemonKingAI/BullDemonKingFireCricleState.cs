/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   BullDemonKingCricleState.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/4/19 17:32:32
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;

public class BullDemonKingFireCricleState : IBullDemonKingState
{
    public BullDemonKingFireCricleState(BullDemonKingFSMSystem fsm, ICharacter character) : base(fsm, character)
    {
        mStateID = BullDemonKingStateID.FireCricle;
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
        mCharacter.PlayAnim("firecricle", 6);
        mCharacter.AnimSpeed(0.5f);
        mBulldemonKing.DoLensOP();
        EventDispatcher.TriggerEvent(EventDefine.Event_Bull_Demon_King_Use_Skill_Fire_Cricle);
    }

    public override void DoBeforeLeaving()
    {
        mBulldemonKing.DoLensReserve();
    }

    public override void Act(E_ActionType actionType)
    {
        mNormalTimer = mCharacter.AnimNormalizedTime("firecricle");
        mAnimisOver = mCharacter.AnimIsOver("firecricle");
        if (mNormalTimer < 0.7f)
        {
            if (!mBulldemonKing.IsInvincible)
            {
                mBeBreaked = true;
                mBulldemonKing.OnSkillBreaked();
            }
        }
        else
        {
            if (mBulldemonKing.IsInvincible)
            {
                // 回复正常播放动画速度
                mCharacter.AnimSpeed(1.0f);
                // 清除射击点
                EventDispatcher.TriggerEvent(EventDefine.Event_DisActive_HitPoint);
            }

            if (mAnimisOver)
            {
                // 相机震动
                ioo.cameraManager.BossShortShake();

                mBulldemonKing.CrashPoint.AddScreenCrash();

                // 对玩家造成伤害
                int[] args = new int[] { -1, mCharacter.attr.baseAttr.id, mCharacter.attr.baseAttr.damageValue };
                ioo.gameEventSystem.NotifySubject(GameEventType.PlayerOnDamage, args);
            }
        }
    }

    public override void Reason(E_ActionType actionType)
    {
        if (mBeBreaked)
        {
            mFSMSystem.PerformTransition(BullDemonKingTransition.BreakFireCricle);
        }
        else if (mAnimisOver)
        {
            mFSMSystem.PerformTransition(BullDemonKingTransition.Rest);
        }
    }
}