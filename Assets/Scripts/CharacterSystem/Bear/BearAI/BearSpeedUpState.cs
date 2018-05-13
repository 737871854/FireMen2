/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   BearSpeedUpState.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/4/26 8:59:04
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;
using UnityEngine;

public class BearSpeedUpState : IBearState
{
    public BearSpeedUpState(BearFSMSystem fsm, ICharacter character) : base(fsm, character)
    {
        mStateID = BearStateID.SpeedUp;
    }

    private Bear mBear;
    private float mDistance;
    public override void DoBeforeEntering()
    {
        mBear = mCharacter as Bear;
        mBear.SpeedUp();
    }

    public override void Act(E_ActionType actionType)
    {
        Vector3 targetPos = ioo.cameraManager.position - ioo.cameraManager.parcentRight * 3 + ioo.cameraManager.parcentForward;
        Vector3 pos;
        mBear.MoveToTarget(targetPos, out pos);
        mDistance = Vector3.Distance(targetPos, mCharacter.position);
    }

    public override void Reason(E_ActionType actionType)
    {
        if (mDistance <= 1.0f)
            mFSMSystem.PerformTransition(BearTransition.SeaEnemy);
    }
}