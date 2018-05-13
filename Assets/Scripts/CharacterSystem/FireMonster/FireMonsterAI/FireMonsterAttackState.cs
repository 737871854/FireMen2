/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   FireMonsterAttackState.cs
 * 
 * 简    介:    攻击状态
 * 
 * 创建标识：   Pancake 2018/3/2 14:53:36
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;
using UnityEngine;

public class FireMonsterAttackState : IFireMonsterState
{
    public FireMonsterAttackState(FireMonsterFSMSystem fsm, ICharacter character) : base(fsm, character)
    {
        mStateID = FireMonsterStateID.Attack;
    }

    private bool mAttacked;

    //public override void DoBeforeEntering()
    //{
    //    mCharacter.PlayAnim("attack0", 2);
    //}

    public override void Act(E_ActionType actionType)
    {
        mAttacked = mCharacter.AnimIsOver("attack0", 2);
    }

    public override void Reason(E_ActionType actionType)
    {
        if (!mAttacked) return;

        switch (actionType)
        {
            case E_ActionType.Normal:
                mFSMSystem.PerformTransition(FireMonsterTransition.Explode);
                break;
            case E_ActionType.AttackCitizen:
                mFSMSystem.PerformTransition(FireMonsterTransition.SeaEnemy);
                break;
        }
    }
}