/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   BearFireBallSuccessState.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/4/24 17:41:20
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;
using UnityEngine;

public class BearFireBallSuccessState : IBearState
{
    public enum E_SuccessType
    {
        Move,
        Lift,
        Home,
    }
    public BearFireBallSuccessState(BearFSMSystem fsm, ICharacter character) : base(fsm, character)
    {
        mStateID = BearStateID.FireBallSuccess;
    }

    private Bear mBear;
    private E_SuccessType mCurType;
    private Vector3 mOrginPos;
    private bool mReachHome;
    public override void DoBeforeEntering()
    {
        mBear = mCharacter as Bear;
        mCurType = E_SuccessType.Move;
        mOrginPos = mCharacter.position;
        mReachHome = false;
    }

    public override void Act(E_ActionType actionType)
    {
        if (mCurType == E_SuccessType.Move)
            MoveAct();

        if (mCurType == E_SuccessType.Lift)
            LiftAct();

        if (mCurType == E_SuccessType.Home)
            HomeAct();
    }
    private void MoveAct()
    {
        mBear.NMAMove();
        Vector3 direction = ioo.cameraManager.position - mCharacter.position;
        if (direction.magnitude < 1.0f)
        {
            mCurType = E_SuccessType.Lift;
            mBear.UseGravityAndNMA(false);
            // 相机震动
            ioo.cameraManager.BossShortShake();
            // 对玩家造成伤害
            int[] args = new int[] { -1, mCharacter.attr.baseAttr.id, mCharacter.attr.baseAttr.damageValue };
            ioo.gameEventSystem.NotifySubject(GameEventType.PlayerOnDamage, args);

            mBear.crashPoint.AddScreenCrash();
        }
    }

    private void LiftAct()
    {
        mCharacter.gameObject.transform.position += Vector3.up * Time.deltaTime * 10;
        if(mCharacter.gameObject.transform.position.y > 4.2f)
        {
            mCurType = E_SuccessType.Home;
            mCharacter.gameObject.transform.position -= ioo.cameraManager.right * 3;
            mBear.UseGravityAndNMA(true);
            mCharacter.PlayAnim("run", 3);
        }
    }

    private void HomeAct()
    {
        Vector3 pos;
        mBear.MoveToTarget(mOrginPos, out pos);
        Vector3 direction = pos - mCharacter.position;
        if(direction.magnitude < 0.2f)
        {
            mReachHome = true;
        }
    }

    public override void Reason(E_ActionType actionType)
    {
        if (mReachHome)
            mFSMSystem.PerformTransition(BearTransition.Rest);
    }
}