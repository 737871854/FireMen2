/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   OceanStrategyTime.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/4/18 10:19:03
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;

public class OceanStrategyTime : IStrategyLevelPass
{
    public int GetTime(int level)
    {
        throw new NotImplementedException();
    }


    protected float mTimer;
    public bool HasKillCondition(int level)
    {
        if (level != 11)
            return false;

        if (mTimer < 5.0f)
        {
            mTimer += UnityEngine.Time.deltaTime;
            return false;
        }
        return !ioo.characterSystem.IsBossLive();
    }
}