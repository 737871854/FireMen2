/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   EliteMonsterHold.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/4/16 19:24:20
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;

public class EliteMonsterHoldState : IEliteMonsterState
{
    public EliteMonsterHoldState(EliteMonsterFSMSystem fsm, ICharacter character) : base(fsm, character)
    {
        mStateID = EliteMonsterStateID.Holding;
    }

    public override void DoBeforeEntering()
    {
        mCharacter.EnterInvincible();
        mCharacter.PlayAnim("attack2", 4);
        EventDispatcher.TriggerEvent(EventDefine.Event_Monster_Hold_Screen);
        ioo.gameMode.RunState(E_GameState.Hold);
    }

    public override void Act(E_ActionType actionType)
    {
        
    }


    public override void Reason(E_ActionType actionType)
    {
        
    }
}