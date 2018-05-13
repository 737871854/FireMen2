/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   FireMonsterExplodeState.cs
 * 
 * 简    介:    自爆
 * 
 * 创建标识：   Pancake 2018/4/8 14:30:32
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;

public class FireMonsterExplodeState : IFireMonsterState
{
    public FireMonsterExplodeState(FireMonsterFSMSystem fsm, ICharacter character) : base(fsm, character)
    {
        mStateID = FireMonsterStateID.Explode;
    }

    public override void Act(E_ActionType actionType)
    {
        mCharacter.Explode();
    }

    public override void Reason(E_ActionType actionType)
    {
       
    }
}