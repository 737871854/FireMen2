/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   HugeFireMonster.cs
 * 
 * 简    介:    大火怪
 * 
 * 创建标识：   Pancake 2018/4/8 9:07:47
 * 
 * 修改描述：   
 * 
 */

using System.Collections.Generic;
using UnityEngine;

public class HugeFireMonster : ICharacter
{
    protected HugeFireMonsterFSMSystem mFSMSystem;

    public HugeFireMonster()
    {
        MakeFSM();
    }

    public override void InitActionType(E_ActionType actionType)
    {
        switch (actionType)
        {
            case E_ActionType.AttackCitizen:
            case E_ActionType.AttackNpc:
                mTargetCharacter = ioo.characterSystem.MonsterFindCitizen();
                if (mTargetCharacter == null) mTargetCharacter = ioo.characterSystem.MonsterFindNpc();
                if (mTargetCharacter != null)
                    mTargetCharacter.AddEnemy(this);
                else
                    mIsKilled = true;
                break;
        }
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
        mFSMSystem = new HugeFireMonsterFSMSystem();

        HugeFireMonsterChaseState chaseState = new HugeFireMonsterChaseState(mFSMSystem, this);
        chaseState.AddTransition(HugeFireMonsterTransition.CanAttack, HugeFireMonsterStateID.Attack);

        HugeFireMonsterAttackState attackState = new HugeFireMonsterAttackState(mFSMSystem, this);
        attackState.AddTransition(HugeFireMonsterTransition.Attacked, HugeFireMonsterStateID.Leave);
        attackState.AddTransition(HugeFireMonsterTransition.SeaEnemy, HugeFireMonsterStateID.Chase);

        HugeFireMonsterLeaveState leaveState = new HugeFireMonsterLeaveState(mFSMSystem, this);
        leaveState.AddTransition(HugeFireMonsterTransition.SeaEnemy, HugeFireMonsterStateID.Chase);

        mFSMSystem.AddState(chaseState, attackState, leaveState);
    }

    public override void UnderAttack(Player player)
    {
        if (mIsKilled || mIsInvincible) return;
        base.UnderAttack(player);
        DoPlayBeAttackedEffect();
        if (mAttr.currentHP <= 0)
        {
            mPlayerKill = player;
            Killed();
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
        int[] args = new int[] { mPlayerKill.id, attr.baseAttr.id, attr.baseAttr.worth };
        ioo.gameEventSystem.NotifySubject(GameEventType.ScoreChange, args);
    }
}