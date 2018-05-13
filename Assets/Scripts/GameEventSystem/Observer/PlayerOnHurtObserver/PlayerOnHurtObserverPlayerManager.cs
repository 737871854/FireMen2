/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   PlayerOnHurtObserverPlayerManager.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/3/8 15:02:36
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;

public class PlayerOnHurtObserverPlayerManager : IGameEventObserver
{
    private PlayerManager mPlayerManager;
    private PlayerOnHurtSubject mSubject;

    public PlayerOnHurtObserverPlayerManager(PlayerManager playerManager)
    {
        mPlayerManager = playerManager;
    }

    public override void SetSubject(IGameEventSubject sub)
    {
        mSubject = sub as PlayerOnHurtSubject;
    }

    public override void Update()
    {
        mPlayerManager.OnDamage(mSubject.damagedID, mSubject.damageValue);
    }
}