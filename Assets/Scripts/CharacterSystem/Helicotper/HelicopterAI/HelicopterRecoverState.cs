/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   RecoverState.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/3/21 10:22:31
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;

public class HelicopterRecoverState : IHelicopterState
{
    public HelicopterRecoverState(HelicopterFSMSystem fsm, ICharacter character) : base(fsm, character)
    {
        mStateID = HelicopterStateID.Recover;
    }

    //public override void DoBeforeEntering()
    //{
    //    mCharacter.PlayAnim("recover", 4);
    //}

    private bool mRecovered;
    public override void Act(E_ActionType actionType)
    {
        mRecovered = mCharacter.AnimIsOver("recover", 4);
    }

    public override void Reason(E_ActionType actionType)
    {
        if (mRecovered)
            mFSMSystem.PerformTransition(HelicopterTransition.Success);
    }
}