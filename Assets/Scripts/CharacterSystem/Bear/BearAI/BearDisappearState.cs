/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   BearDisappearState.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/4/24 17:22:47
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;

public class BearDisappearState : IBearState
{
    public BearDisappearState(BearFSMSystem fsm, ICharacter character) : base(fsm, character)
    {
        mStateID = BearStateID.Disappear;
    }

    private float mValue;
    public override void Act(E_ActionType actionType)
    {
        mValue += UnityEngine.Time.deltaTime;
        mCharacter.BodyDisappear(mValue);
    }

    public override void Reason(E_ActionType actionType)
    {
        if (mValue >= 1.0f)
            mCharacter.Killed();
    }
}