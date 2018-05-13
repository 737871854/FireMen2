/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   LifeBoatWait.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2017/12/19 17:12:20
 * 
 * 修改描述：   
 * 
 */


using System;
using System.Collections.Generic;
using UnityEngine;

public class LifeBoatWait : FSMState
{
    private LifeBoatController lifeBoatController;

    public LifeBoatWait(LifeBoatController lifeBoatController)
    {
        // TODO: Complete member initialization
        this.lifeBoatController = lifeBoatController;
        actionID = (int)LifeBoatController.Transition.Wait;
        existTime = 10;
    }
    public override void Reason(UnityEngine.Transform player, UnityEngine.Transform npc)
    {
        if (existTime == 0)
            lifeBoatController.PerformTransition((int)LifeBoatController.Transition.Disappear);
    }

    private float existTime;
    public override void Act(UnityEngine.Transform player, UnityEngine.Transform npc)
    {
        if (existTime > 0)
        {
            existTime -= Time.fixedDeltaTime;
        }
        else
        {
            existTime = 0;
        }
    }
}