/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   IGameEventSubject.cs
 * 
 * 简    介:    Subject事件基类
 * 
 * 创建标识：   Pancake 2018/3/5 9:37:45
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;

public class IGameEventSubject
{
    /// <summary>
    /// 观察者列表
    /// </summary>
    private List<IGameEventObserver> mObservers = new List<IGameEventObserver>();

    public void RegisterObserver(IGameEventObserver ob)
    {
        mObservers.Add(ob);
    }

    public void RemoveObserver(IGameEventObserver ob)
    {
        mObservers.Remove(ob);
    }

    /// <summary>
    /// 更新Observer
    /// </summary>
    /// <param name="args"></param>
    public virtual void Notify(params int[] args)
    {
        foreach(IGameEventObserver ob in mObservers)
        {
            ob.Update();
        }
    }
}