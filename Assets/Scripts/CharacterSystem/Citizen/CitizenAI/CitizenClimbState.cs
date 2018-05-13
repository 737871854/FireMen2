/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   CitizenClimbState.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/3/20 10:30:49
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;
using UnityEngine;

public class CitizenClimbState : ICitizenState
{
    public CitizenClimbState(CitizenFSMSystem fsm, ICharacter character) : base(fsm, character)
    {
        mStateID = CitizenStateID.Climb;
    }

    private Citizen mCitizen;
    public override void DoBeforeEntering()
    {
        mCharacter.rigidbody.useGravity = false;
        mCharacter.PlayAnim("climb", 3);
        mCitizen = mCharacter as Citizen;
    }

    public override void Act(E_ActionType actionType)
    {
        float speed = mCharacter.attr.baseAttr.baseSpeed * mCharacter.factorSpeed;
        mCharacter.gameObject.transform.position += Vector3.up * Time.deltaTime * speed;
    }

    public override void Reason(E_ActionType actionType)
    {
        if (mCitizen.rescued)
            mFSMSystem.PerformTransition(CitizenTransition.Rescued);
    }
}