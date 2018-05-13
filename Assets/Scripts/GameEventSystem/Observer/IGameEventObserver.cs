/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   IGameEventObserver.cs
 * 
 * 简    介:    观察者基类
 * 
 * 创建标识：   Pancake 2018/3/5 9:36:46
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;

public abstract class IGameEventObserver
{
    /// <summary>
    /// 更新Observer
    /// </summary>
    public abstract void Update();
    
    /// <summary>
    /// 注入Subject
    /// </summary>
    /// <param name="sub"></param>
    public abstract void SetSubject(IGameEventSubject sub);
}