/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   Citizen.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/3/20 10:04:38
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;
using UnityEngine;

public class Citizen : ICharacter
{
    protected CitizenFSMSystem mFSMSystem;

    #region 飞机救援
    // 需要飞机前来救援
    private bool mCanCallRescued;
    public bool canCallRescued { get { return mCanCallRescued; } set { mCanCallRescued = value; } }
    // 救援飞机到达
    private bool mHelicopterReached;
    public bool helicopterReached { get { return mHelicopterReached; } }
    public void HelicopterReached() { mHelicopterReached = true; }
    // 被救援
    private bool mRescued;
    public bool rescued { get { return mRescued; } }
    #endregion

    private Vector3 mOrginPos;
    public Vector3 orginPos { get { return mOrginPos; } }

    public Citizen()
    {
        MakeFSM();
    }

    private IGameEventObserver mGameEventReachedObserver;
    private IGameEventObserver mGameEventRescuedObserver;
    protected override void Init()
    {
        base.Init();
        mRigidbody.useGravity = true;
        mOrginPos = position;
    }

    public override void Release()
    {
        base.Release();
        switch (actionType)
        {
            case E_ActionType.WaitForHelp:
                ioo.gameEventSystem.RemoveObserver(GameEventType.CityzenRescued, mGameEventRescuedObserver);
                ioo.gameEventSystem.RemoveObserver(GameEventType.HelicopterReached, mGameEventReachedObserver);
                break;
            case E_ActionType.WaitForBoat:
                break;
        }
    }

    public override void InitActionType(E_ActionType actionType)
    {
        switch(actionType)
        {
            case E_ActionType.WaitForHelp:
                mGameEventRescuedObserver = new CitizenBeRescedObserver(this);
                mGameEventReachedObserver = new HelicopterReachedObserver(this);
                ioo.gameEventSystem.RegisterObserver(GameEventType.CityzenRescued, mGameEventRescuedObserver);
                ioo.gameEventSystem.RegisterObserver(GameEventType.HelicopterReached, mGameEventReachedObserver);
                break;
            case E_ActionType.WaitForBoat:
                break;
        }
    }

    public override void UpdateFSMAI(E_ActionType actionType)
    {
        if (mIsKilled) return;
        mFSMSystem.currentState.Act(actionType);
        mFSMSystem.currentState.Reason(actionType);
    }

    protected override void UpdateExtra()
    {
        mRigidbody.velocity = Vector3.zero;
    }

    private void MakeFSM()
    {
        mFSMSystem = new CitizenFSMSystem();

        CitizenRunState runState = new CitizenRunState(mFSMSystem, this);
        runState.AddTransition(CitizenTransition.Trapped, CitizenStateID.Help);

        CitizenHelpState helpState = new CitizenHelpState(mFSMSystem, this);
        helpState.AddTransition(CitizenTransition.AirPlaneReached, CitizenStateID.Climb);

        CitizenClimbState climbState = new CitizenClimbState(mFSMSystem, this);
        climbState.AddTransition(CitizenTransition.Rescued, CitizenStateID.Disappear);

        CitizenDisappearState disappearState = new CitizenDisappearState(mFSMSystem, this);

        mFSMSystem.AddState(runState, helpState, climbState, disappearState);
    }

    public override void UnderAttack(Player player)
    {
        if (mIsKilled) return;
        base.UnderAttack(player);
        DoPlayBeAttackedEffect();
        if(mAttr.currentHP <= 0)
        {
            mPlayerKill = player;
            Killed();
        }
    }

    public override void Killed()
    {
        base.Killed();
    }

    public void SaveByPlayer(int id)
    {
        mRescued = true;
        int[] args = new int[] { id, attr.baseAttr.id, attr.baseAttr.worth };
        ioo.gameEventSystem.NotifySubject(GameEventType.ScoreChange, args);
    }
}