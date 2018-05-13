/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   CityzenBeRescuedSubject.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/3/5 9:51:39
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;

public class CitizenBeRescuedSubject : IGameEventSubject
{
    private int mPlayerID;
    private int mCitizenGUID;

    public int playerID { get { return mPlayerID; } }
    public int citizenGUID { get { return mCitizenGUID; } }

    public override void Notify(params int[] args)
    {
        mPlayerID = args[0];
        mCitizenGUID = args[2];
        base.Notify();
    }
}