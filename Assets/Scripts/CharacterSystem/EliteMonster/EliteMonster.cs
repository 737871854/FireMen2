/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   EliteMonster.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/3/22 9:16:54
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;
using UnityEngine;

public class EliteMonster : ICharacter
{
    private EliteMonsterFSMSystem mFSMSystem;

    public EliteMonster()
    {
        MakeFSM();
        EventDispatcher.AddEventListener<bool>(EventDefine.Event_Struggle_Hold_Success, OnStruggleResult);
    }

    public override void Release()
    {
        base.Release();
        EventDispatcher.RemoveEventListener<bool>(EventDefine.Event_Struggle_Hold_Success, OnStruggleResult);
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

    private void MakeFSM()
    {
        mFSMSystem = new EliteMonsterFSMSystem();

        EliteMonsterChaseState chaseState = new EliteMonsterChaseState(mFSMSystem, this);
        chaseState.AddTransition(EliteMonsterTransition.Waitting, EliteMonsterStateID.Stroll);
        chaseState.AddTransition(EliteMonsterTransition.Reached, EliteMonsterStateID.Holding);

        EliteMonsterStrollState idleState = new EliteMonsterStrollState(mFSMSystem, this);
        idleState.AddTransition(EliteMonsterTransition.SeaEnemy, EliteMonsterStateID.Chase);

        EliteMonsterHoldState holdState = new EliteMonsterHoldState(mFSMSystem, this);
        holdState.AddTransition(EliteMonsterTransition.HoldSuccess, EliteMonsterStateID.Success);
        holdState.AddTransition(EliteMonsterTransition.HoldFail, EliteMonsterStateID.Defeat);

        EliteMonsterSuccessState successState = new EliteMonsterSuccessState(mFSMSystem, this);

        EliteMonsterDefeatState defeatState = new EliteMonsterDefeatState(mFSMSystem, this);

        mFSMSystem.AddState(chaseState, idleState, holdState, successState, defeatState);
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
            return;
        }

        Pause();
    }

    public override void Killed()
    {
        base.Killed();
        DoPlayBeDestroyEffectSound();
        DoPlayBeDestroySound();
        // 玩家挣脱成功
        if(mPlayerKill == null)
        {
            for(int i = 0; i < ioo.playerCount;++i)
            {
                float contribution = ioo.gameMode.GetHoldContribution(i);
                int worth = (int)(contribution * attr.baseAttr.worth);
                int[] args = new int[] { i, attr.baseAttr.id, worth };
                ioo.gameEventSystem.NotifySubject(GameEventType.ScoreChange, args);
            }
        }
        else// 被玩家击杀
        {
            int[] args = new int[] { mPlayerKill.id, attr.baseAttr.id, attr.baseAttr.worth };
            ioo.gameEventSystem.NotifySubject(GameEventType.ScoreChange, args);
        }
    }

    public bool IsToHoldOrHolding()
    {
        if(mFSMSystem.currentState.stateID == EliteMonsterStateID.Chase || mFSMSystem.currentState.stateID == EliteMonsterStateID.Holding)
            return true;
        return false;
    }

    private void OnStruggleResult(bool result)
    {
        if (mFSMSystem.currentState.stateID != EliteMonsterStateID.Holding) return;

        // 玩家挣脱成功
        if(result)
        {
            mFSMSystem.PerformTransition(EliteMonsterTransition.HoldFail);
        }
        else// 玩家挣脱失败
        {
            mFSMSystem.PerformTransition(EliteMonsterTransition.HoldSuccess);
        }
    }
}