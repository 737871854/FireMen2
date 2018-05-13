/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   DragonExplode.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2017/12/21 16:01:52
 * 
 * 修改描述：   
 * 
 */


using System;
using System.Collections.Generic;

public class DragonExplode : FSMState
{
    private DragonController dragonController;

    public DragonExplode(UnityEngine.Vector3[] wayPoints, DragonController dragonController)
    {
        // TODO: Complete member initialization
        this.wayPoints = wayPoints;
        this.dragonController = dragonController;
        actionID = (int)DragonController.Transition.Explode;

    }

    public override void Reason(UnityEngine.Transform player, UnityEngine.Transform npc)
    {
        
    }

    public override void Act(UnityEngine.Transform player, UnityEngine.Transform npc)
    {
        dragonController.StateChange = false;
        //EventDispatcher.TriggerEvent(EventDefine.Event_Player_Damage, dragonController.AttackDamage);
        dragonController.DeSpawn();
    }
}