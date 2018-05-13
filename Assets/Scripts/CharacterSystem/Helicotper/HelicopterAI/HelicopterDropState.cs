/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   Drop.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/3/21 10:20:10
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;

public class HelicopterDropState : IHelicopterState
{
    public HelicopterDropState(HelicopterFSMSystem fsm, ICharacter character) : base(fsm, character)
    {
        mStateID = HelicopterStateID.Drop;
    }

    //public override void DoBeforeEntering()
    //{
    //    mCharacter.PlayAnim("drop", 2);
    //}

    public override void DoBeforeLeaving()
    {
        Helicopter helicopter = mCharacter as Helicopter;
        // 呼叫者id，直升机id，救援目标id
        int[] args = new int[] { helicopter.playerID, mCharacter.attr.baseAttr.id, helicopter.citizenGUID };
        ioo.gameEventSystem.NotifySubject(GameEventType.HelicopterReached, args);
    }

    private bool mDroped;
    public override void Act(E_ActionType actionType)
    {
        mDroped = mCharacter.AnimIsOver("drop", 2);
    }

    public override void Reason(E_ActionType actionType)
    {
        if (mDroped)
            mFSMSystem.PerformTransition(HelicopterTransition.Hold);
    }
}