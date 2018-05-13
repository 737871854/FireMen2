/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   BearFireBallState.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/4/24 17:20:20
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;

public class BearFireBallState : IBearState
{
    public enum E_FireBall
    {
        Ready,
        Ball,
        Success,
    }

    public BearFireBallState(BearFSMSystem fsm, ICharacter character) : base(fsm, character)
    {
        mStateID = BearStateID.FireBall;
    }

    private Bear mBear;
    private bool mBeBreaked;
    private float mNormalTimer;
    private bool mAnimIsOver;
    private E_FireBall mBallType;
    public override void DoBeforeEntering()
    {
        mCharacter.PlayAnim("ball0", 8);
        mBear = mCharacter as Bear;
        mBeBreaked = false;
        mNormalTimer = 0;
        mAnimIsOver = false;
        mBallType = E_FireBall.Ready;
    }

    public override void Act(E_ActionType actionType)
    {
        if(mBallType == E_FireBall.Ready)
        {
            mAnimIsOver = mCharacter.AnimIsOver("ball0");
            if(mAnimIsOver)
            {
                mBallType = E_FireBall.Ball;
                mCharacter.PlayAnim("ball1", 9);
                EventDispatcher.TriggerEvent(EventDefine.Event_Bear_Use_Skill_Ball);
                return;
            }
        }

        if(mBallType == E_FireBall.Ball)
        {
            mNormalTimer = mCharacter.AnimNormalizedTime("ball1");
            if(mNormalTimer > 30)
            {
                mBallType = E_FireBall.Success;
                EventDispatcher.TriggerEvent(EventDefine.Event_DisActive_HitPoint);
            }
            else
            {
                if (!mBear.IsInvincible)
                {
                    mBeBreaked = true;
                    mBear.OnSkillBreaked();
                }
            }
            return;
        }
    }

    public override void Reason(E_ActionType actionType)
    {
        if (mBeBreaked)
        {
            mFSMSystem.PerformTransition(BearTransition.Rest);
            return;
        }
        
        if(mBallType == E_FireBall.Success)
            mFSMSystem.PerformTransition(BearTransition.FireBallSuccess);
    }
}