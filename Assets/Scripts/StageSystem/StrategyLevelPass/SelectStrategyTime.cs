/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   SelectStrategyTime.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/4/18 12:27:02
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;

public class SelectStrategyTime : IStrategyLevelPass
{
    public int GetTime(int level)
    {
        return 100;
    }

    public bool HasKillCondition(int level)
    {
        return false;
    }
}