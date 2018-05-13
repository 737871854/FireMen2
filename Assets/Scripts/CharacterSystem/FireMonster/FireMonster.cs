/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   IFireMonster.cs
 * 
 * 简    介:    小火怪，烟雾怪
 * 
 * 创建标识：   Pancake 2018/3/2 14:23:13
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;
using UnityEngine;

public class FireMonster : ICharacter
{
    protected FireMonsterFSMSystem mFSMSystem;

    public FireMonster()
    {
        MakeFSM();
    }

    public override void InitActionType(E_ActionType actionType)
    {
        //switch (actionType)
        //{
        //    case E_ActionType.AttackCitizen:
        //        mTargetCharacter = ioo.characterSystem.MonsterFindCitizen();
        //        if (mTargetCharacter != null)
        //        {
        //            mTargetCharacter.AddEnemy(this);
        //        }
        //        else
        //        {
        //            mCanDestroy = true;
        //        }
        //        break;
        //}
    }

    public override void UpdateFSMAI(E_ActionType actionType)
    {
        if (mIsKilled || mIsPause) return;
        mFSMSystem.currentState.Act(actionType);
        mFSMSystem.currentState.Reason(actionType);
    }

    protected override void UpdateExtra()
    {
        mRigidbody.velocity = Vector3.zero;
    }

    protected override void InitCollider()
    {
        mCollider = mGameObject.GetComponent<Collider>();
        if (mActionType == E_ActionType.Normal)
        {
            mCollider.isTrigger = true;
        }
        else if (mActionType == E_ActionType.AttackCitizen || mActionType == E_ActionType.AttackNpc)
        {
            mCollider.isTrigger = false;
        }
    }

    private void MakeFSM()
    {
        mFSMSystem = new FireMonsterFSMSystem();

        FireMonsterChaseState chaseState = new FireMonsterChaseState(mFSMSystem, this);
        chaseState.AddTransition(FireMonsterTransition.CanAttack, FireMonsterStateID.Attack);

        FireMonsterAttackState attackState = new FireMonsterAttackState(mFSMSystem, this);
        attackState.AddTransition(FireMonsterTransition.Explode, FireMonsterStateID.Explode);

        FireMonsterExplodeState explodeState = new FireMonsterExplodeState(mFSMSystem, this);

        mFSMSystem.AddState(chaseState, attackState, explodeState);
    }

    public override void UnderAttack(Player player)
    {
        if (mIsKilled || mIsInvincible) return;
        base.UnderAttack(player);
        DoPlayBeAttackedEffect();
        if(mAttr.currentHP <= 0)
        {
            mPlayerKill = player;
            Killed();
            return;
        }

        Pause();
    }
    
    public override void Killed()
    {
        base.Killed();
        // 没有可满足的攻击点而被销毁，此时attr还未被初始化
        if (attr == null || mPlayerKill == null) return;
        DoPlayBeDestroyEffectSound();
        DoPlayBeDestroySound();
        int[] args = new int[] {mPlayerKill.id, attr.baseAttr.id, attr.baseAttr.worth };
        ioo.gameEventSystem.NotifySubject(GameEventType.ScoreChange, args);
    }
}