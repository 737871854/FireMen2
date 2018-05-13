/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   CityzenBeRescedObserver.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/3/5 9:54:20
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;

public class CitizenBeRescedObserver : IGameEventObserver
{
    private Citizen mCitizen;
    private CitizenBeRescuedSubject mSubject;

    public CitizenBeRescedObserver(Citizen citizen)
    {
        mCitizen = citizen;
    }

    public override void SetSubject(IGameEventSubject sub)
    {
        mSubject = sub as CitizenBeRescuedSubject;
    }

    public override void Update()
    {
        if (mCitizen.guid != mSubject.citizenGUID) return;
        mCitizen.SaveByPlayer(mSubject.playerID);
    }
}