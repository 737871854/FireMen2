/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   TownStrategyTime.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/4/18 10:07:10
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;

public class TownStrategyTime : IStrategyLevelPass
{
    public int GetTime(int level)
    {
        int time = 0;
        switch (level)
        {
            case 9:
                time = 22;
                break;
            default:
                time = 11;
                break;
        }
        return time;
    }

    public bool HasKillCondition(int level)
    {
        return false;
    }
}