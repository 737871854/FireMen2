/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   DragonChase.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2017/12/21 11:49:04
 * 
 * 修改描述：   
 * 
 */


using System;
using System.Collections.Generic;
using UnityEngine;

public class DragonChase : FSMState
{
    private bool stayInArea;
    private DragonController dragonController;
    public DragonChase(UnityEngine.Vector3[] wayPoints, DragonController dragonController)
    {
        // TODO: Complete member initialization
        this.wayPoints = wayPoints;
        this.dragonController = dragonController;
        actionID = (int)DragonController.FSMActionID.Chase;

        animator = dragonController.Animator;
     
        if (dragonController.ActionType == E_ActionType.SpecialCircle && wayPoints != null && wayPoints.Length != 0)
            destPos = wayPoints[1];
        if(dragonController.ActionType == E_ActionType.ShakeScreen)
            AreaManager.Instance.GetRandomPositionInArea(dragonController.stayArea, ref destPos);

        curIndex = 0;
   
        info.name = "run";
        info.id = 1;
    }

    public override void Reason(UnityEngine.Transform player, UnityEngine.Transform npc)
    {
        if (dragonController.StateChange)
        {
            if (dragonController.ActionType == E_ActionType.SpecialCircle)
                dragonController.PerformTransition((int)DragonController.Transition.Attack);
            if (dragonController.ActionType == E_ActionType.ShakeScreen)
                dragonController.PerformTransition((int)DragonController.Transition.Hold);
        }
    }

    private int curIndex;
    public override void Act(UnityEngine.Transform player, UnityEngine.Transform npc)
    {
        dragonController.StateChange = false;
        AnimatorStateInfo stateinfo0 = animator.GetCurrentAnimatorStateInfo(0);
        if (!stateinfo0.IsName(info.name))
        {
            animator.SetInteger("State", info.id);
        }

        //float factor = ScenesManager.Instance.IsFreeze ? 0.3f : 1;

        #region SpecialCircle
        if (dragonController.ActionType == E_ActionType.SpecialCircle)
        {
            Vector3 direction = Vector3.zero;
            if (stayInArea)
            {
                direction = destPos - npc.position;
            }
            else
            {
                direction = ioo.cameraManager.position - npc.position;
            }
            Quaternion toRotation = Quaternion.LookRotation(direction);
            npc.rotation = Quaternion.Lerp(npc.rotation, toRotation, Time.fixedDeltaTime * dragonController.RotationSpeed);

            direction = destPos - npc.position;
            if (curIndex == 1)
                dragonController.StayTimeUpdate();

            if (direction.magnitude < attackDistance * 0.5f)
            {
                if (dragonController.stayTime > 0)
                {
                    if (!stayInArea)
                    {
                        ++curIndex;
                        stayInArea = true;
                    }
                    AreaManager.Instance.GetRandomPositionInArea(dragonController.stayArea, ref destPos);
                }
                else
                {
                    stayInArea = false;
                    ++curIndex;
                    if (curIndex >= wayPoints.Length)
                        dragonController.StateChange = true;
                    else
                        destPos = wayPoints[curIndex];
                }
            }
            //else
            //    npc.position += direction.normalized * Time.fixedDeltaTime * dragonController.MoveSpeed * factor;

        }
        #endregion

        #region ShakeScreen
        if (dragonController.ActionType == E_ActionType.ShakeScreen)
        {
            Vector3 direction = Vector3.zero;
            direction = destPos - npc.position;
            Quaternion toRotation = Quaternion.LookRotation(direction);
            npc.rotation = Quaternion.Lerp(npc.rotation, toRotation, Time.fixedDeltaTime * dragonController.RotationSpeed);
            direction = destPos - npc.position;

            if (direction.magnitude < attackDistance * 0.5f)
            {
                if (!dragonController.Actived)
                    AreaManager.Instance.GetRandomPositionInArea(dragonController.stayArea, ref destPos);
                else
                {
                    if (direction.magnitude < attackDistance * 0.5f)
                    {
                        if (FindNextPoint(curIndex))
                            dragonController.StateChange = true;
                        else
                            ++curIndex;
                    }
                }
            }
            //else
            //    npc.position += direction.normalized * Time.fixedDeltaTime * dragonController.MoveSpeed * factor;
         
        }
        #endregion
       
    }
}