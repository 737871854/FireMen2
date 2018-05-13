/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   DragonIdle.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2017/12/21 11:48:54
 * 
 * 修改描述：   
 * 
 */


using System;
using System.Collections.Generic;
using UnityEngine;

public class DragonIdle : FSMState
{
    private DragonController dragonController;
    public DragonIdle(UnityEngine.Vector3[] wayPoints, DragonController dragonController)
    {
        // TODO: Complete member initialization
        this.wayPoints = wayPoints;
        this.dragonController = dragonController;
        actionID = (int)DragonController.FSMActionID.Idle;
        animator = dragonController.Animator;

        info.name = "idle";
        info.id = 0;
    }
    public override void Reason(UnityEngine.Transform player, UnityEngine.Transform npc)
    {
        
    }

    public override void Act(UnityEngine.Transform player, UnityEngine.Transform npc)
    {
        AnimatorStateInfo stateinfo = animator.GetCurrentAnimatorStateInfo(0);
        if (!stateinfo.IsName(info.name))
        {
            animator.SetInteger("State", info.id);
        }
    }
}