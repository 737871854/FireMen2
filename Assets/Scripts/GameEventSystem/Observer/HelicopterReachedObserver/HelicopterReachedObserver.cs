/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   HelicopterReachedObserver.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/4/19 9:07:42
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;

public class HelicopterReachedObserver : IGameEventObserver
{
    private Citizen mCitizen;
    private HelicopterReachSubject mSubject;

    public HelicopterReachedObserver(Citizen citizen)
    {
        mCitizen = citizen;
    }

    public override void SetSubject(IGameEventSubject sub)
    {
        mSubject = sub as HelicopterReachSubject;
    }

    public override void Update()
    {
        if (mCitizen.guid != mSubject.citizenGUID) return;
        mCitizen.HelicopterReached();
    }
}