/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   BullDemonKingDisappearState.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/4/19 17:12:17
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;

public class BullDemonKingDisappearState : IBullDemonKingState
{
    public BullDemonKingDisappearState(BullDemonKingFSMSystem fsm, ICharacter character) : base(fsm, character)
    {
        mStateID = BullDemonKingStateID.Disappear;
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
            (mCharacter as BullDemonKing).Killed();
    }
}