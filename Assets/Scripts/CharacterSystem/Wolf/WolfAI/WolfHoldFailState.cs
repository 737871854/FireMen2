/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   WolfHoldFailState.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/5/3 8:35:03
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;

public class WolfHoldFailState : IWolfState
{
    public WolfHoldFailState(WolfFSMSystem fsm, ICharacter character) : base(fsm, character)
    {
        mStateID = WolfStateID.Defeat;
    }

    private Wolf mWolf;
    private float mNormalTime;
    public override void DoBeforeEntering()
    {
        mWolf = mCharacter as Wolf;
        mCharacter.PlayAnim("retreat", 4);
    }

    public override void Act(E_ActionType actionType)
    {
        mNormalTime = mCharacter.AnimNormalizedTime("retreat");
    }

    public override void Reason(E_ActionType actionType)
    {
        if (mNormalTime > 0.99f)
            mWolf.Killed();
    }
}