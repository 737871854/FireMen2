/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   CitizenHelpState.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/3/20 10:31:28
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;

public class CitizenHelpState : ICitizenState
{
    public CitizenHelpState(CitizenFSMSystem fsm, ICharacter character) : base(fsm, character)
    {
        mStateID = CitizenStateID.Help;
    }

    private Citizen mCitizen;
    public override void DoBeforeEntering()
    {
        mCharacter.PlayAnim("help", 2);
        mCitizen = mCharacter as Citizen;
        mCitizen.canCallRescued = true;
    }

    public override void Act(E_ActionType actionType)
    {
        
    }

    public override void Reason(E_ActionType actionType)
    {
        if (mCitizen.helicopterReached)
            mFSMSystem.PerformTransition(CitizenTransition.AirPlaneReached);
    }
}