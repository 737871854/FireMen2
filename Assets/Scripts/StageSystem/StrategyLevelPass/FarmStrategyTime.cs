/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   FarmStrategyTime.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/4/18 10:18:26
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;

public class FarmStrategyTime : IStrategyLevelPass
{
    public int GetTime(int level)
    {
        int time = 0;
        switch (level)
        {
            case 9:
                time = 12;
                break;
            case 11:
                time = 99999999;
                break;
            default:
                time = 11;
                break;
        }
        return time;
    }

    protected float mTimer;
    public bool HasKillCondition(int level)
    {
        if(level != 11)
            return false;

        if (mTimer < 5.0f)
        {
            mTimer += UnityEngine.Time.deltaTime;
            return false;
        }
        return !ioo.characterSystem.IsBossLive();
    }
}