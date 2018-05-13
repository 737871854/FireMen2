/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   BeatFollowState.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/4/26 11:32:40
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;
using UnityEngine;

public class BearFollowState : IBearState
{
    public enum E_Follow
    {
        FollowCamera,
        ToTarget,
    }

    public BearFollowState(BearFSMSystem fsm, ICharacter character) : base(fsm, character)
    {
        mStateID = BearStateID.Follow;
    }

    private Bear mBear;
    private float mDistance;
    private Vector3 mTargetPos;
    private E_Follow mFollowType;
    public override void DoBeforeEntering()
    {
        mBear = mCharacter as Bear;
        mBear.FollowSpeed();
        string name = "";
        if (mBear.IsStep3()) name = "WaitPoint0";
        else name = "WaitPoint1";
        mTargetPos = GameObject.Find(name).transform.position;
        mFollowType = E_Follow.FollowCamera;
    }

    public override void DoBeforeLeaving()
    {
        mBear.NormalSpeed();
    }

    public override void Act(E_ActionType actionType)
    {
        Vector3 pos = Vector3.zero;
        if(mFollowType == E_Follow.FollowCamera)
        {
            mBear.NMAMove();
            if(Vector3.Distance(mTargetPos, mCharacter.position)<= 2.0f)
            {
                mFollowType = E_Follow.ToTarget;
            }
        }
        else if(mFollowType == E_Follow.ToTarget)
        {
            mBear.MoveToTarget(mTargetPos, out pos);
        }
        mDistance = Vector3.Distance(pos, mCharacter.position);
    }

    public override void Reason(E_ActionType actionType)
    {
        if (mDistance <= 0.2f)
            mFSMSystem.PerformTransition(BearTransition.Rest);
    }
}