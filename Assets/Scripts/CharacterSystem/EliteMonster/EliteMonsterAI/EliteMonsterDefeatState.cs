/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   EliteMonsterDefeatState.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/4/16 19:26:21
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;

public class EliteMonsterDefeatState : IEliteMonsterState
{
    public EliteMonsterDefeatState(EliteMonsterFSMSystem fsm, ICharacter character) : base(fsm, character)
    {
        mStateID = EliteMonsterStateID.Defeat;
    }

    private bool mDefeated;

    public override void DoBeforeEntering()
    {
        mCharacter.PlayAnim("retreat", 6);
    }

    public override void Act(E_ActionType actionType)
    {
        mDefeated = mCharacter.AnimIsOver("retreat");
        if (!mDefeated) return;
        mCharacter.Killed();
    }

    public override void Reason(E_ActionType actionType)
    {
        
    }
}