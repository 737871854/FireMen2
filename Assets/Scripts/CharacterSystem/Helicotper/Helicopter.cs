/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   Helicopter.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/3/21 8:52:20
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;

public class Helicopter : ICharacter
{
    protected HelicopterFSMSystem mFSMSystem;
    private Citizen mCitizen;
    private E_HelicopterMissionType mMissionType;
    public E_HelicopterMissionType missionType { get { return mMissionType; } }
    public UnityEngine.Vector3 citiizenPos { get { return mCitizen.position; } }
    public int citizenGUID { get { return mCitizen.guid; } }
    public void SetCitizen(Citizen citizen) { mCitizen = citizen; }
    public void SetMissionType(E_HelicopterMissionType type) { mMissionType = type; }

    private int mPlayerID;
    public int playerID { get { return mPlayerID; } set { mPlayerID = value; } }

    public Helicopter()
    {
        MakeFSM();
    }

    public override void UpdateFSMAI(E_ActionType actionType)
    {
        if (mIsKilled || mIsPause) return;
        mFSMSystem.currentState.Act(actionType);
        mFSMSystem.currentState.Reason(actionType);
    }

    private void MakeFSM()
    {
        mFSMSystem = new HelicopterFSMSystem();

        HelicopterComeState comeState = new HelicopterComeState(mFSMSystem, this);
        comeState.AddTransition(HelicopterTransition.Save, HelicopterStateID.Drop);

        HelicopterDropState dropState = new HelicopterDropState(mFSMSystem, this);
        dropState.AddTransition(HelicopterTransition.Hold, HelicopterStateID.Watting);

        HelicopterWaitingState waitingState = new HelicopterWaitingState(mFSMSystem, this);
        waitingState.AddTransition(HelicopterTransition.Rescued, HelicopterStateID.Recover);

        HelicopterRecoverState recoverState = new HelicopterRecoverState(mFSMSystem, this);
        recoverState.AddTransition(HelicopterTransition.Success, HelicopterStateID.Leave);

        HelicopterLeaveState leaveState = new HelicopterLeaveState(mFSMSystem, this);

        mFSMSystem.AddState(comeState, dropState, waitingState, recoverState, leaveState);

    }
}