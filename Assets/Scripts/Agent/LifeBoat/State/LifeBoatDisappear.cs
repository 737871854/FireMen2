/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   LifeBoatDisappear.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2017/12/20 15:55:24
 * 
 * 修改描述：   
 * 
 */


using System.Collections.Generic;
using UnityEngine;

public class LifeBoatDisappear : FSMState
{
    private LifeBoatController lifeBoatController;

    public LifeBoatDisappear(LifeBoatController lifeBoatController)
    {
        // TODO: Complete member initialization
        this.lifeBoatController = lifeBoatController;
    }
    public override void Reason(UnityEngine.Transform player, UnityEngine.Transform npc)
    {
        
    }

    public override void Act(UnityEngine.Transform player, UnityEngine.Transform npc)
    {
        for (int index = 0; index < lifeBoatController.Renderer.Length; ++index)
        {
            float value = lifeBoatController.Renderer[index].material.GetFloat("_Cutoff");
            value += Time.fixedDeltaTime;
            value = value > 1 ? 1 : value;
            lifeBoatController.Renderer[index].material.SetFloat("_Cutoff", value);
            bool hasDesappear = value == 1 ? true : false;
            if (hasDesappear)
            {
                lifeBoatController.DeSpawn();
            }
        }
    }
}