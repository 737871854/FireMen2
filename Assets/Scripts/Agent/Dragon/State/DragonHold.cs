/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   DragonHold.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2017/12/21 11:49:53
 * 
 * 修改描述：   
 * 
 */


using System;
using System.Collections.Generic;
using UnityEngine;

public class DragonHold : FSMState
{
    private DragonController dragonController;

    public DragonHold(UnityEngine.Vector3[] wayPoints, DragonController dragonController)
    {
        // TODO: Complete member initialization
        this.wayPoints = wayPoints;
        this.dragonController = dragonController;
        actionID = (int)DragonController.FSMActionID.Hold;
        animator = dragonController.Animator;

        info.name = "hold";
        info.id = 3;
    }
    public override void Reason(UnityEngine.Transform player, UnityEngine.Transform npc)
    {
       
    }

    private bool executed;
    public override void Act(UnityEngine.Transform player, UnityEngine.Transform npc)
    {
        Vector3 direction = ioo.cameraManager.position - Vector3.up * 0.55f - npc.position;
        Quaternion toRotation = Quaternion.LookRotation(direction);
        npc.rotation = Quaternion.Lerp(npc.rotation, toRotation, Time.fixedDeltaTime * dragonController.RotationSpeed);

        if (executed)
            return;

        executed = true;
        dragonController.StateChange = false;
        dragonController.EnterHold();
        AnimatorStateInfo stateinfo = animator.GetCurrentAnimatorStateInfo(0);
        if (!stateinfo.IsName(info.name))
        {
            animator.SetInteger("State", info.id);
        }

        EventDispatcher.TriggerEvent(EventDefine.Event_Monster_Hold_Screen);
    }
}