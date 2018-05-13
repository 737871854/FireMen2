/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   BearBreakBeat.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/4/24 17:24:42
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;

public class BearBreakBeatState : IBearState
{
    public BearBreakBeatState(BearFSMSystem fsm, ICharacter character) : base(fsm, character)
    {
        mStateID = BearStateID.BreakBeat;
    }

    private bool mAnimIsOver;
    public override void DoBeforeEntering()
    {
        mAnimIsOver = false;
        mCharacter.AnimSpeed(1.0f);
        mCharacter.PlayAnim("breakBeat", 7);
        (mCharacter as Bear).UseGravityAndNMA(true);
    }

    public override void Act(E_ActionType actionType)
    {
        mAnimIsOver = mCharacter.AnimIsOver("breakBeat");
    }

    public override void Reason(E_ActionType actionType)
    {
        if (mAnimIsOver)
            mFSMSystem.PerformTransition(BearTransition.Rest);
    }
}