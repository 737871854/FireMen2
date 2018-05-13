/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   DragonDisappear.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2017/12/21 11:53:22
 * 
 * 修改描述：   
 * 
 */


using System;
using System.Collections.Generic;
using UnityEngine;

public class DragonDisappear : FSMState
{
    private DragonController dragonController;

    public DragonDisappear(UnityEngine.Vector3[] wayPoints, DragonController dragonController)
    {
        // TODO: Complete member initialization
        this.wayPoints = wayPoints;
        this.dragonController = dragonController;
        actionID = (int)DragonController.FSMActionID.Disappear;
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

        for (int index = 0; index < dragonController.Renderer.Length; ++index)
        {
            float value = dragonController.Renderer[index].material.GetFloat("_Cutoff");
            value += Time.fixedDeltaTime;
            value = value > 1 ? 1 : value;
            dragonController.Renderer[index].material.SetFloat("_Cutoff", value);
            bool hasDesappear = value == 1 ? true : false;
            if (hasDesappear)
                dragonController.DeSpawn();
        }
    }
}