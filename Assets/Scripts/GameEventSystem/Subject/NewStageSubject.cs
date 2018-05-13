/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   NewStageSubject.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/3/5 9:40:57
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;

public class NewStageSubject : IGameEventSubject
{
    private int mStageCount;

    public int stageCount { get { return mStageCount; } }

    public override void Notify(params int[] args)
    {
        mStageCount = args[0];
        base.Notify(args);
    }
}