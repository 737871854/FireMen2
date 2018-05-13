/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   PlayerOnHurtSubject.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/3/8 15:03:19
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;

public class PlayerOnHurtSubject : IGameEventSubject
{
    private int mDamagedID;
    private int mDamageValue;

    public int damagedID { get { return mDamagedID; } }
    public int damageValue { get { return mDamageValue; } }

    public override void Notify(params int[] args)
    {
        mDamagedID = args[0];
        mDamageValue = args[2];

        base.Notify();
    }
}