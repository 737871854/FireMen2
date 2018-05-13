/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   BearToHomeState.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/4/26 16:39:00
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;
using UnityEngine;

public class BearToHomeState : IBearState
{
    public BearToHomeState(BearFSMSystem fsm, ICharacter character) : base(fsm, character)
    {
        mStateID = BearStateID.ToHome;
    }

    private Bear mBear;
    private Vector3 mTargetPos;
    private float mDistance;
    public override void DoBeforeEntering()
    {
        mBear = mCharacter as Bear;
        mCharacter.PlayAnim("run", 3);
        mBear.NormalSpeed();
        mDistance = 0;
        string name = "";
        if (mBear.IsStep4()) name = "WaitPoint0";
        else name = "WaitPoint1";
        mTargetPos = GameObject.Find(name).transform.position;
    }

    public override void Act(E_ActionType actionType)
    {
        Vector3 pos;
        mBear.MoveToTarget(mTargetPos, out pos);
        mDistance = Vector3.Distance(mCharacter.position, pos);
    }

    public override void Reason(E_ActionType actionType)
    {
        if (mDistance < 0.2f)
            mFSMSystem.PerformTransition(BearTransition.Rest);
    }
}