/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   HelicopterReachSubject.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/4/19 9:03:49
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;

public class HelicopterReachSubject : IGameEventSubject
{
    private int mCitizenGUID;

    public int citizenGUID { get { return mCitizenGUID; } }

    public override void Notify(params int[] args)
    {
        mCitizenGUID = args[2];
        base.Notify(args);
    }
}