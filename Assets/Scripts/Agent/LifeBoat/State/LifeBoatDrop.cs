/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   LifeBoatDrop.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2017/12/19 17:13:15
 * 
 * 修改描述：   
 * 
 */


using System;
using System.Collections.Generic;
using UnityEngine;

public class LifeBoatDrop : FSMState
{
    private LifeBoatController lifeBoatController;

    public LifeBoatDrop(LifeBoatController lifeBoatController)
    {
        // TODO: Complete member initialization
        this.lifeBoatController = lifeBoatController;
        actionID = (int)LifeBoatController.Transition.Drop;
    }
    public override void Reason(UnityEngine.Transform player, UnityEngine.Transform npc)
    {
       if(isStatic)
       {
           EventDispatcher.TriggerEvent(EventDefine.Event_Citizen_Boat_Is_Ready, lifeBoatController);
           lifeBoatController.PerformTransition((int)LifeBoatController.Transition.Wait);
       }
    }

    private bool isStatic;
    private Vector3 lastPos;
    public override void Act(UnityEngine.Transform player, UnityEngine.Transform npc)
    {
        if (npc.transform.position == lastPos)
        {
            isStatic = true;
        }
        else
        {
            lastPos = npc.position;
        }
    }
}