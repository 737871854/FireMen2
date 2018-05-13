/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   DragonRetreat.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2017/12/21 11:52:51
 * 
 * 修改描述：   
 * 
 */


using System;
using System.Collections.Generic;
using UnityEngine;

public class DragonRetreat : FSMState
{
    private DragonController dragonController;

    public DragonRetreat(UnityEngine.Vector3[] wayPoints, DragonController dragonController)
    {
        // TODO: Complete member initialization
        this.wayPoints = wayPoints;
        this.dragonController = dragonController;
        actionID = (int)DragonController.FSMActionID.Retreat;
        animator = dragonController.Animator;

        info.name = "retreat";
        info.id = 4;
    }
    public override void Reason(UnityEngine.Transform player, UnityEngine.Transform npc)
    {
        if (dragonController.StateChange)
        {
            dragonController.PerformTransition((int)DragonController.Transition.Disappear);
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

        Vector3 direction = npc.position - ioo.cameraManager.position;
        npc.position += direction.normalized * Time.fixedDeltaTime * dragonController.MoveSpeed;

        AnimatorStateInfo info1 = animator.GetCurrentAnimatorStateInfo(0);
        if (info1.normalizedTime >= 0.99f)
        {
            for (int i = 0; i < ioo.playerManager.playerCount; ++i)
            {
                dragonController.StateChange = true;
                // 销毁音效
                ioo.audioManager.PlaySound2D(dragonController.DestroyEffectSound);
                // 死亡语音
                if (UnityEngine.Random.Range(0, 100) > 70)
                {
                    int rand = UnityEngine.Random.Range(0, dragonController.DestroySound.Length);
                    ioo.audioManager.PlayPersonSound(dragonController.DestroySound[rand]);
                }
                //EventDispatcher.TriggerEvent(EventDefine.Event_Add_Score, ioo.playerManager.GetPlayer(i), dragonController.Worth);
            }
        }
    }
}