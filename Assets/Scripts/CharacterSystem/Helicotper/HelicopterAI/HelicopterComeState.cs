/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   Come.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/3/21 10:19:42
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterComeState : IHelicopterState
{
    public HelicopterComeState(HelicopterFSMSystem fsm, ICharacter character) : base(fsm, character)
    {
        mStateID = HelicopterStateID.Come;
    }


    public override void Act(E_ActionType actionType)
    {
        Helicopter helicopter = mCharacter as Helicopter;
        switch(helicopter.missionType)
        {
            case E_HelicopterMissionType.FireFighting:
                FireFightingAct();
                break;
            case E_HelicopterMissionType.SaveCitizen:
                SaveCitizenAct();
                break;
        }
    }

    private void FireFightingAct()
    {

    }


    private bool mReached;
    private void SaveCitizenAct()
    {
        Helicopter helicopter = mCharacter as Helicopter;
        Vector3 targetPos = helicopter.citiizenPos + (mCharacter.position.y - helicopter.citiizenPos.y) * Vector3.up;
        mReached = mCharacter.MoveTo(targetPos);
    }

    public override void Reason(E_ActionType actionType)
    {
        Helicopter helicopter = mCharacter as Helicopter;
        switch (helicopter.missionType)
        {
            case E_HelicopterMissionType.FireFighting:
                FireFightingReasont();
                break;
            case E_HelicopterMissionType.SaveCitizen:
                SaveCitizenReason();
                break;
        }
    }

    private void FireFightingReasont()
    {

    }

    public void SaveCitizenReason()
    {
        if(mReached)
        {
            mFSMSystem.PerformTransition(HelicopterTransition.Save);
        }
    }
}