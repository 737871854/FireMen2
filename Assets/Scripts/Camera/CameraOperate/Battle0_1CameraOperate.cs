/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   Battle0_1CameraOperate.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/4/19 14:14:33
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;
using UnityEngine;

public class Battle0_1CameraOperate : ICameraOperate
{
    public Battle0_1CameraOperate(CameraManager cameraManager) : base(cameraManager)
    {
     
    }

    public override float GetCameraSpeed(string eventName)
    {
        throw new NotImplementedException();
    }

    public override void OnCustomEvent(string eventName)
    {
        throw new NotImplementedException();
    }

    public override void UpdateState(int state)
    {
        throw new NotImplementedException();
    }
}