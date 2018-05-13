/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   LifeBoatIdle.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2017/12/19 17:11:51
 * 
 * 修改描述：   
 * 
 */


using System;
using System.Collections.Generic;

public class LifeBoatIdle : FSMState
{
    private LifeBoatController lifeBoatController;

    public LifeBoatIdle(LifeBoatController lifeBoatController)
    {
        // TODO: Complete member initialization
        this.lifeBoatController = lifeBoatController;
        actionID = (int)LifeBoatController.Transition.Idle;
    }
    public override void Reason(UnityEngine.Transform player, UnityEngine.Transform npc)
    {
        
    }

    public override void Act(UnityEngine.Transform player, UnityEngine.Transform npc)
    {
        
    }
}