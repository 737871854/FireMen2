/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   DragonAttack.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2017/12/21 11:49:27
 * 
 * 修改描述：   
 * 
 */


using System;
using System.Collections.Generic;
using UnityEngine;

public class DragonAttack : FSMState
{
    private DragonController dragonController;

    public DragonAttack(UnityEngine.Vector3[] wayPoints, DragonController dragonController)
    {
        // TODO: Complete member initialization
        this.wayPoints = wayPoints;
        this.dragonController = dragonController;
        actionID = (int)DragonController.FSMActionID.Attack;
        animator = dragonController.Animator;

        info.name = "attack0";
        info.id = 2;
    }
    public override void Reason(UnityEngine.Transform player, UnityEngine.Transform npc)
    {
        if (dragonController.StateChange)
        {
            dragonController.PerformTransition((int)DragonController.Transition.Explode);
        }
    }

    public override void Act(UnityEngine.Transform player, UnityEngine.Transform npc)
    {
        dragonController.StateChange = false;
        AnimatorStateInfo stateinfo0 = animator.GetCurrentAnimatorStateInfo(0);
        if (!stateinfo0.IsName(info.name))
        {
            animator.SetInteger("State", info.id);
        }

        AnimatorStateInfo stateinfo1 = animator.GetCurrentAnimatorStateInfo(0);
        if (stateinfo1.IsName(info.name) && stateinfo1.normalizedTime >= 0.99f)
        {
            dragonController.StateChange = true;
            // 相机震动
            ioo.cameraManager.NormalShake();
            // 爆炸音效
            ioo.audioManager.PlaySound2D(dragonController.ExplodeEffectSound);
            //EventDispatcher.TriggerEvent(EventDefine.Event_Player_Damage, dragonController.AttackDamage);
        }
    }
}