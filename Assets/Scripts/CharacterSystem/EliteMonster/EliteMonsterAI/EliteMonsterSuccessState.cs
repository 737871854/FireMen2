/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   EliteMonsterSuccessState.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/4/16 19:25:35
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;

public class EliteMonsterSuccessState : IEliteMonsterState
{
    public EliteMonsterSuccessState(EliteMonsterFSMSystem fsm, ICharacter character) : base(fsm, character)
    {
        mStateID = EliteMonsterStateID.Success;
    }

    public override void DoBeforeEntering()
    {
        mCharacter.PlayAnim("explode", 5);
    }

    private bool mExploded;

    public override void Act(E_ActionType actionType)
    {
        mExploded = mCharacter.AnimIsOver("explode");
        if (!mExploded) return;
        mCharacter.Explode(true);
    }

    public override void Reason(E_ActionType actionType)
    {
        
    }
}