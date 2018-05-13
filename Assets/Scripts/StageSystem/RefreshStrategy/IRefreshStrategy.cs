/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   IRefreshStrategy.cs
 * 
 * 简    介:    场景对应关卡需要刷新怪物数量
 * 
 * 创建标识：   Pancake 2018/3/5 16:40:44
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;

public interface IRefreshStrategy
{
    int GetMonsterCount(int level);
}