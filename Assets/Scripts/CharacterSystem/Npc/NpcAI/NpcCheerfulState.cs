/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   NpcCheerfulState.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/3/20 16:42:03
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;

public class NpcCheerfulState : INpcState
{
    public NpcCheerfulState(NpcFSMSystem fsm, ICharacter character) : base(fsm, character)
    {
        mStateID = NpcStateID.Cheerful;
    }

    public override void Act(E_ActionType actionType)
    {
        
    }

    public override void Reason(E_ActionType actionType)
    {
       
    }
}