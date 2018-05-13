/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   RightSofaHitPoint.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/4/21 15:31:49
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;

public class RightSofaHitPoint : IHitPoint
{
    public override void BindEvent()
    {
        base.BindEvent();
        EventDispatcher.AddEventListener(EventDefine.Event_Bull_Demon_King_Use_Skill_Right_Sofa, Active);
    }

    public override void RemoveEvent()
    {
        base.RemoveEvent();
        EventDispatcher.RemoveEventListener(EventDefine.Event_Bull_Demon_King_Use_Skill_Right_Sofa, Active);
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