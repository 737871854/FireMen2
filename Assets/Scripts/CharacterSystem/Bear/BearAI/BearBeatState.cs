/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   BearBeatState.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/4/24 17:18:35
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;

public class BearBeatState : IBearState
{
    public BearBeatState(BearFSMSystem fsm, ICharacter character) : base(fsm, character)
    {
        mStateID = BearStateID.Beat;
    }

    private Bear mBear;
    private bool mBeBreaked;
    private float mNormalTimer;
    private bool mAnimisOver;
    private bool mUsedSkill;
    public override void DoBeforeEntering()
    {
        mCharacter.PlayAnim("beat", 6);
        mBear = mCharacter as Bear;
        mBeBreaked = false;
        mNormalTimer = 0;
        mAnimisOver = false;
        mUsedSkill = false;
        mBear.UseGravityAndNMA(false);
        ioo.cameraManager.SlowLens(0.01f); //TODO待实际情况调整
    }

    public override void Act(E_ActionType actionType)
    {
        mBear.LookAtCamera();
        mNormalTimer = mCharacter.AnimNormalizedTime("beat");
        mAnimisOver = mCharacter.AnimIsOver("beat");
        if (mNormalTimer < 0.47f && mNormalTimer >= 0.3f)
        {
            if (!mUsedSkill)
            {
                mUsedSkill = true;
                mCharacter.AnimSpeed(0.04f);
                EventDispatcher.TriggerEvent(EventDefine.Event_Bear_Use_Skill_Beat);
            }else if (!mBear.IsInvincible)
            {
                mBeBreaked = true;
                mBear.OnSkillBreaked();
                ioo.cameraManager.NormalSpeed();
            }
        }
        else if(mNormalTimer >= 0.44f)
        {
            if (mBear.IsInvincible)
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
                ioo.cameraManager.NormalSpeed();
                // 对玩家造成伤害
                int[] args = new int[] { -1, mCharacter.attr.baseAttr.id, mCharacter.attr.baseAttr.damageValue };
                ioo.gameEventSystem.NotifySubject(GameEventType.PlayerOnDamage, args);

                mBear.crashPoint.AddScreenCrash();
            }
        }
    }

    public override void Reason(E_ActionType actionType)
    {
        if (mBeBreaked)
            mFSMSystem.PerformTransition(BearTransition.BreakBeat);
        else if (mAnimisOver)
            mFSMSystem.PerformTransition(BearTransition.Rest);
    }
}