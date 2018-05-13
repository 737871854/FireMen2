/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   Wolf.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/5/2 16:21:32
 * 
 * 修改描述：   
 * 
 */

using UnityEngine;
using UnityEngine.AI;

public class Wolf : ICharacter
{
    protected WolfFSMSystem mFSMSystem;

    public GameObject crashPoint { get; set; }

    public Wolf()
    {
        MakeFSM();
        EventDispatcher.AddEventListener<bool>(EventDefine.Event_Struggle_Hold_Success, OnStruggleResult);
    }
    public override void Release()
    {
        base.Release();
        EventDispatcher.RemoveEventListener<bool>(EventDefine.Event_Struggle_Hold_Success, OnStruggleResult);
        mNMA.enabled = false;
    }

    protected override void Init()
    {
        base.Init();
        crashPoint = Util.FindTransformByName(mGameObject.transform, "CrashPoint").gameObject;
        mNMA.enabled = true;
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
        mFSMSystem = new WolfFSMSystem();

        WolfChaseState chaseState = new WolfChaseState(mFSMSystem, this);
        chaseState.AddTransition(WolfTransition.Reached, WolfStateID.AttackOrHold);

        WolfAttackOrHoldState attackOrHoldState = new WolfAttackOrHoldState(mFSMSystem, this);
        attackOrHoldState.AddTransition(WolfTransition.HoldSuccess, WolfStateID.Success);
        attackOrHoldState.AddTransition(WolfTransition.HoldFail, WolfStateID.Defeat);
        attackOrHoldState.AddTransition(WolfTransition.MissionComplete, WolfStateID.Leave);

        WolfHoldSuccessState holdSuccessState = new WolfHoldSuccessState(mFSMSystem, this);
        holdSuccessState.AddTransition(WolfTransition.MissionComplete, WolfStateID.Leave);

        WolfHoldFailState failState = new WolfHoldFailState(mFSMSystem, this);

        WolfLeaveState leaveState = new WolfLeaveState(mFSMSystem, this);

        mFSMSystem.AddState(chaseState, attackOrHoldState, holdSuccessState, failState, leaveState);
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

        //Pause();
    }

    public override void Killed()
    {
        base.Killed();
        DoPlayBeDestroyEffectSound();
        DoPlayBeDestroySound();
        // 玩家挣脱成功
        if (mPlayerKill == null)
        {
            if (actionType == E_ActionType.Normal) return;
            for (int i = 0; i < ioo.playerCount; ++i)
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

    private void OnStruggleResult(bool result)
    {
        if (mFSMSystem.currentState.stateID != WolfStateID.AttackOrHold) return;

        // 玩家挣脱成功
        if (result)
        {
            mFSMSystem.PerformTransition(WolfTransition.HoldFail);
        }
        else// 玩家挣脱失败
        {
            mFSMSystem.PerformTransition(WolfTransition.HoldSuccess);
        }
    }
}