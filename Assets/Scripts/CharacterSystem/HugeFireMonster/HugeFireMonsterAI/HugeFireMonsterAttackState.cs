/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   HugeFireMonsterAttackState.cs
 * 
 * 简    介:    攻击状态
 * 
 * 创建标识：   Pancake 2018/4/8 9:16:35
 * 
 * 修改描述：   
 * 
 */

using System.Collections.Generic;
using UnityEngine;

public class HugeFireMonsterAttackState : IHugeFireMonsterState
{
    public HugeFireMonsterAttackState(HugeFireMonsterFSMSystem fsm, ICharacter character) : base(fsm, character)
    {
        mStateID = HugeFireMonsterStateID.Attack;
    }

    private bool mAttacked;

    //public override void DoBeforeEntering()
    //{
    //    mAttacked = false;
    //    switch (mCharacter.actionType)
    //    {
    //        case E_ActionType.Normal:
    //            mCharacter.PlayAnim("attack0", 2);
    //            break;
    //        case E_ActionType.AttackCitizen:
    //        case E_ActionType.AttackNpc:
    //            mCharacter.PlayAnim("attack1", 3);
    //            break;
    //    }
    //}

    public override void Act(E_ActionType actionType)
    {
        switch(actionType)
        {
            case E_ActionType.Normal:
                mAttacked = mCharacter.AnimIsOver("attack0", 2);
                break;
            case E_ActionType.AttackCitizen:
            case E_ActionType.AttackNpc:
                mAttacked = mCharacter.AnimIsOver("attack1", 3);
                break;
        }

        if(mAttacked)
        {
            switch (actionType)
            {
                case E_ActionType.Normal:
                    mCharacter.Hurt();
                    break;
                case E_ActionType.AttackCitizen:
                case E_ActionType.AttackNpc:

                    break;
            }
        }
    }

    public override void Reason(E_ActionType actionType)
    {
        if (mAttacked)
            switch(actionType)
            {
                case E_ActionType.Normal:
                    mFSM.PerformTransition(HugeFireMonsterTransition.Attacked);
                    break;
                case E_ActionType.AttackCitizen:
                case E_ActionType.AttackNpc:
                    mFSM.PerformTransition(HugeFireMonsterTransition.SeaEnemy);
                    break;
            }
    }
}