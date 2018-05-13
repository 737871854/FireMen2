﻿/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   FireMonsterChaseState.cs
 * 
 * 简    介:    追击攻击玩家
 * 
 * 创建标识：   Pancake 2018/3/2 14:48:57
 * 
 * 修改描述：   
 * 
 */

using System.Collections.Generic;
using UnityEngine;

public class FireMonsterChaseState : IFireMonsterState
{
    public FireMonsterChaseState(FireMonsterFSMSystem fsm, ICharacter character) : base(fsm, character)
    {
        mStateID = FireMonsterStateID.Chase;
    }

    private List<Vector3> mPath = new List<Vector3>();
    private int mCurrentIndex;

    public override void DoBeforeEntering()
    {
        switch (mCharacter.actionType)
        {
            case E_ActionType.Normal:
                mCharacter.attackRange = Define.PATH_STEP;
                int location = -1;
                mCharacter.targetTran = ioo.stageSystem.GetTargetTran(ref location);
                mCharacter.location = location;
                if(mCharacter.targetTran == null)
                    mCharacter.Killed();
                break;
            case E_ActionType.AttackCitizen:
                mCharacter.attackRange = Define.PATH_STEP * 5;             
                break;
        }
    }

    public override void Act(E_ActionType actionType)
    {
        switch(actionType)
        {
            case E_ActionType.Normal:
                NormalAct();
                break;
            case E_ActionType.AttackCitizen:
                AttackCitizenAct();
                break;
        }
    }
    private void NormalAct()
    {
        if (mCharacter.targetTran != null)
        {
            if (mPath.Count == 0)
            {
                mCurrentIndex = 0;
                mPath = Util.GenBezierPath(mCharacter.position, mCharacter.targetTran.position, Util.Random(2, 4));
            }
            else
                mCharacter.MoveByPath(mPath, ref mCurrentIndex);
        }
    }

    private void AttackCitizenAct()
    {
        if (mCharacter.taretCharacter != null)
        {
            mCharacter.MoveTo(mCharacter.taretCharacter.attackPoint, 0.3f);
        }
    }

    public override void Reason(E_ActionType actionType)
    {
        switch(actionType)
        {
            case E_ActionType.Normal:
                NormalReason();
                break;
            case E_ActionType.AttackCitizen:
                AttackCitizenReason();
                break;
        }
    }

    private void NormalReason()
    {
        if (mPath != null && mPath.Count != 0)
        {
            float distance = Vector3.Distance(mCharacter.position, mPath[mPath.Count - 1]);
            if (distance <= mCharacter.attackRange)
            {
                mFSMSystem.PerformTransition(FireMonsterTransition.CanAttack);
            }
        }
    }

    private void AttackCitizenReason()
    {
        float distance = Vector3.Distance(mCharacter.position, mCharacter.taretCharacter.attackPoint.position);
        if (distance < mCharacter.attackRange)
        {
            mFSMSystem.PerformTransition(FireMonsterTransition.CanAttack);
        }
    }
}