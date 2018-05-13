/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   BearAppearState.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/4/24 17:05:35
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;
using UnityEngine;

public class BearAppearState : IBearState
{
    public BearAppearState(BearFSMSystem fsm, ICharacter character) : base(fsm, character)
    {
        mStateID = BearStateID.Appear;
    }      

    private Bear mBear;
    private bool mAppearEnd;

    public override void DoBeforeEntering()
    {
        ioo.TriggerListener(EventLuaDefine.Event_Boss_Born);
    }

    public override void Act(E_ActionType actionType)
    {
        if (mBear == null) mBear = mCharacter as Bear;
        Vector3 landPos = mCharacter.GetTerrainPos(mCharacter.position);
        Vector3 direction = landPos - mCharacter.position;

        if (direction.magnitude < 0.1f) { mBear.UseGravityAndNMA(true); }

        float normalizendTime = mCharacter.AnimNormalizedTime("jump");
        if(normalizendTime != -1)
        {
            if (normalizendTime < 0.1f)
            {
                mCharacter.AnimSpeed(0.1f);
                direction = mBear.jumpPos - mCharacter.position;
                mCharacter.gameObject.transform.position += mBear.jumpSpeed * Time.deltaTime * direction.normalized;
                //mCharacter.MoveStraight(mBear.jumpPos);
            }
        }

        if(mCharacter.isOnNavMesh) mCharacter.AnimSpeed(1.0f);

        if (normalizendTime == -1)
        {
            normalizendTime = mCharacter.AnimNormalizedTime("roar");
            if (normalizendTime >= 0.1f && normalizendTime < 0.13f) ioo.cameraManager.BossLongShake();
            if (normalizendTime >= 0.99f) mAppearEnd = true;
        }

    }

    public override void Reason(E_ActionType actionType)
    {
        if (mAppearEnd) mFSMSystem.PerformTransition(BearTransition.Rest);
    }
}