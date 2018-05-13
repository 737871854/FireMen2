/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   BullFireCricleHitPoint.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/4/20 13:59:20
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;

public class BullFireCricleHitPoint : IHitPoint
{
    public override void BindEvent()
    {
        base.BindEvent();
        EventDispatcher.AddEventListener(EventDefine.Event_Bull_Demon_King_Use_Skill_Fire_Cricle, Active);
    }

    public override void RemoveEvent()
    {
        base.RemoveEvent();
        EventDispatcher.RemoveEventListener(EventDefine.Event_Bull_Demon_King_Use_Skill_Fire_Cricle, Active);
    }
    public override void Active()
    {
        base.Active();
    }

    public override void UnderAttack(Player player)
    {
        base.UnderAttack(player);
    }

    public override void UpdatePreFrame()
    {
        base.UpdatePreFrame();
    }
}