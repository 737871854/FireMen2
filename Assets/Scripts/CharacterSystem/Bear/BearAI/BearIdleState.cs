/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   BearIdleState.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/4/24 17:06:48
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;
using UnityEngine;

public class BearIdleState : IBearState
{
    public BearIdleState(BearFSMSystem fsm, ICharacter character) : base(fsm, character)
    {
        mStateID = BearStateID.Idle;
    }

    private float mTimer;
    private Bear mBear;
    public override void DoBeforeEntering()
    {
        mTimer = 0;
        mBear = mCharacter as Bear;
        mCharacter.PlayAnim("idle", 1);
        mBear.UseGravityAndNMA(true);
    }

    public override void Act(E_ActionType actionType)
    {
        if (!mBear.IsInvincible)
            mTimer += UnityEngine.Time.deltaTime;
        mBear.LookAtCamera();
    }

    public override void Reason(E_ActionType actionType)
    {
        if (mBear.isDead)
        {
            mFSMSystem.PerformTransition(BearTransition.NoHealth);
            return;
        }


        if (mTimer >= 2.0f)
        {
            if (mBear.IsStep1())
            {
                bool skillIsOut = mBear.CurrentStepSkillIsOut();
                if (!skillIsOut)
                {
                    mBear.UseSkill();
                    return;
                }
                else
                {
                    mBear.NexStep();
                }
            }

            if(mBear.IsStep3())
            {
                bool skillIsOut = mBear.CurrentStepSkillIsOut();
                if (skillIsOut)
                {
                    if(!ioo.cameraManager.CPAIsPlaying)
                        mBear.NexStep();
                    else
                        return;
                }
            }

            if (mBear.IsStep4() || mBear.IsStep6())
            {
                 bool skillIsOut = mBear.CurrentStepSkillIsOut();
                if (skillIsOut)
                {
                    mBear.NexStep();
                    ioo.cameraManager.PlayCPA();
                }
                else
                {
                    if(mBear.UseSkillImmediately())
                    {
                        mBear.UseSkill();
                        return;
                      
                    }
                    else
                    {
                        float distance = Vector3.Distance(mCharacter.position, ioo.cameraManager.position);
                        if (distance < 2.0f)
                        {
                            mBear.UseSkill();
                            return;
                        }
                    }
                }
            }

            Vector3 direction = ioo.cameraManager.position - mBear.position;
            if (direction.magnitude > 4.0f)
                mFSMSystem.PerformTransition(BearTransition.SeaEnemy);
        }
    }
}