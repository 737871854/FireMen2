/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   BullDemonKingOXHornState.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/4/19 17:30:15
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;
using UnityEngine;

public class BullDemonKingOXHornState : IBullDemonKingState
{
    public BullDemonKingOXHornState(BullDemonKingFSMSystem fsm, ICharacter character) : base(fsm, character)
    {
        mStateID = BullDemonKingStateID.OXHorn;
    }

    private float mWaittingTimer;
    private bool mEvnetIsDispatchered;
    private bool mBeBreaked;
    private bool mReached;
    private float mLastTimer;
    private Vector3 mTargetPos;
    private BullDemonKing mBulldemonKing;
    public override void DoBeforeEntering()
    {
        mBulldemonKing = mCharacter as BullDemonKing;
        mTargetPos = mBulldemonKing.Forward();
        mReached = false;
        mLastTimer = 0;
        mBeBreaked = false;
        mEvnetIsDispatchered = false;
        mWaittingTimer = 0.8f;
        mCharacter.PlayAnim("oxhorn0", 5);
        mBulldemonKing.DoLensOP();
        EventDispatcher.TriggerEvent(EventDefine.Event_Active_Boss_Black, true);
    }

    public override void DoBeforeLeaving()
    {
        EventDispatcher.TriggerEvent(EventDefine.Event_Active_Boss_Black, false);
    }

    public override void Act(E_ActionType actionType)
    {
        if(mWaittingTimer > 0)
        {
            mWaittingTimer -= Time.deltaTime;
            return;
        }
        mReached = mCharacter.MoveStraight(mTargetPos);
        mCharacter.LookAtCamera();
        if (mReached)
        {
            // 现将启动射击点消息发出去，在下一帧在对射击点进行检测
            if(!mEvnetIsDispatchered)
            {
                mEvnetIsDispatchered = true;
                mBulldemonKing.ReachForward();
                mCharacter.AnimSpeed(0.1f);
                EventDispatcher.TriggerEvent(EventDefine.Event_Bull_Demon_King_Use_Skill_OX_Horn);
            }
            else
            {
                mLastTimer += Time.deltaTime;
                //mAnimNormal = mCharacter.AnimNormalizedTime("oxhorn1");
                if (mLastTimer > 3.0f)
                {
                    // 回复正常播放动画速度
                    mCharacter.AnimSpeed(1.0f);
                    // 清除射击点
                    EventDispatcher.TriggerEvent(EventDefine.Event_DisActive_HitPoint);
                    // 相机震动
                    ioo.cameraManager.BossShortShake();
                    mBulldemonKing.CrashPoint.AddScreenCrash();
                    // 对玩家造成伤害
                    int[] args = new int[] { -1, mCharacter.attr.baseAttr.id, mCharacter.attr.baseAttr.damageValue };
                    ioo.gameEventSystem.NotifySubject(GameEventType.PlayerOnDamage, args);
                }
                else
                {
                    if (!mBulldemonKing.IsInvincible)
                    {
                        mBeBreaked = true;
                        mBulldemonKing.OnSkillBreaked();
                    }
                }
            }
        }
    }

    public override void Reason(E_ActionType actionType)
    {
        if (!mReached) return;
        if (mBeBreaked)
        {
            mFSMSystem.PerformTransition(BullDemonKingTransition.BreakOXhorn);
        }
        else if (mLastTimer > 3.0f)
        {
            mFSMSystem.PerformTransition(BullDemonKingTransition.Rest);
        }
    }
}