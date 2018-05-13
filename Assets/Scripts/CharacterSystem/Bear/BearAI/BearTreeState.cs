/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   BearTreeState.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/4/24 17:16:37
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;

public class BearTreeState : IBearState
{
    public BearTreeState(BearFSMSystem fsm, ICharacter character) : base(fsm, character)
    {
        mStateID = BearStateID.Tree;
    }

    public override void Act(E_ActionType actionType)
    {
        throw new NotImplementedException();
    }

    public override void Reason(E_ActionType actionType)
    {
        throw new NotImplementedException();
    }
}