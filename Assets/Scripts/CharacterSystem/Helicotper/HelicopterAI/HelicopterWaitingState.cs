/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   WaitingState.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/3/21 10:21:15
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;

public class HelicopterWaitingState : IHelicopterState
{
    public HelicopterWaitingState(HelicopterFSMSystem fsm, ICharacter character) : base(fsm, character)
    {
        mStateID = HelicopterStateID.Watting;
    }

    public override void DoBeforeEntering()
    {
        mCharacter.PlayAnim("waitting", 3);
        mHelicopter = mCharacter as Helicopter;
    }

    public override void DoBeforeLeaving()
    { 
        // 呼叫者id，直升机id，救援目标id
        int[] args = new int[] { mHelicopter.playerID, mCharacter.attr.baseAttr.id, mHelicopter.citizenGUID};
        ioo.gameEventSystem.NotifySubject(GameEventType.CityzenRescued, args);
    }

    private float mDistance;
    private Helicopter mHelicopter;
    public override void Act(E_ActionType actionType)
    {
        mDistance = UnityEngine.Vector3.Distance(mCharacter.position, mHelicopter.citiizenPos);
    }

    public override void Reason(E_ActionType actionType)
    {
        if (mDistance < 0.2f)
            mFSMSystem.PerformTransition(HelicopterTransition.Rescued);
    }
}