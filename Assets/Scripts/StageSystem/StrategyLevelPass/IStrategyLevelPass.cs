/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   ITimeStrategy.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/4/18 10:04:58
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;

public interface IStrategyLevelPass
{
    /// <summary>
    /// 获取当前场景指定镜头阶段时间
    /// </summary>
    /// <param name="level"></param>
    /// <returns></returns>
    int GetTime(int level);

    bool HasKillCondition(int level);
}