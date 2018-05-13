/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   PterosaurStep2.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/1/6 9:07:12
 * 
 * 修改描述：   
 * 
 */


using System;
using System.Collections.Generic;

public class PterosaurStep9 : Step
{
    public PterosaurStep9(PterosaurBehaviour pterosaurBehaviour)
    {
        // TODO: Complete member initialization
        this.pterosaurBehaviour = pterosaurBehaviour;
        pterosaurStep = E_PterosaurStep.Step9;
        animator = pterosaurBehaviour.Animator;
        pterosaurBehaviour.AddStep(this);
    }
}