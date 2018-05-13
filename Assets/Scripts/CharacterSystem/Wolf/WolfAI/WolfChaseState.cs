/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   WolfChaseState.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/5/3 8:34:12
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;
using UnityEngine;

public class WolfChaseState : IWolfState
{
    public WolfChaseState(WolfFSMSystem fsm, ICharacter character) : base(fsm, character)
    {
        mStateID = WolfStateID.Chase;
    }

    //private Wolf mWolf;
    private bool mReached;
    private FarmBattleScene mFBS;
    public override void Act(E_ActionType actionType)
    {
        //if (mWolf == null) mWolf = mCharacter as Wolf;
        if (mFBS == null) mFBS = ioo.battleScene as FarmBattleScene;
        if (mCharacter.isOnNavMesh == false) return;
        if(actionType == E_ActionType.Normal)
        {
            NormalAct();
        }else if(actionType == E_ActionType.ShakeScreen)
        {
            SpecialAct();
        }
    }

    private void NormalAct()
    {
        Vector3 pos;
        Vector3 targetPos = ioo.cameraManager.position + ioo.cameraManager.forward;
        mCharacter.MoveToTarget(targetPos, out pos);
        float distance = Vector3.Distance(mCharacter.position, pos);
        if (distance < 1.0f)
        {
            mReached = true;
            mCharacter.CanWalk(false);
        }
    }

    private void SpecialAct()
    {
        Vector3 pos;
        Vector3 targetPos = mFBS.wolfHoldPoint.position;
        mCharacter.MoveToTarget(targetPos, out pos);
        float distance = Vector3.Distance(mCharacter.position, pos);
        if (distance < 0.05f)
        {
            mReached = true;
            mCharacter.CanWalk(false);
        }
    }

    public override void Reason(E_ActionType actionType)
    {
        if (actionType == E_ActionType.Normal)
        {
            NormalReason();
        }
        else if (actionType == E_ActionType.ShakeScreen)
        {
            SpecialReason();
        }
    }

    private void NormalReason()
    {
        if (mReached)
            mFSMSystem.PerformTransition(WolfTransition.Reached);
    }

    private void SpecialReason()
    {
        if (mReached)
            mFSMSystem.PerformTransition(WolfTransition.Reached);
    }
}