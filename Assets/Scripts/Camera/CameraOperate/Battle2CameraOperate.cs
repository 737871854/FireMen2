/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   Battle2CameraStrategy.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/4/10 10:59:47
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;

public class Battle2CameraOperate : ICameraOperate
{
    public Battle2CameraOperate(CameraManager cameraManager) : base(cameraManager)
    {
    }

    public override float GetCameraSpeed(string eventName)
    {
        float speed = 0;


        return speed;
    }

    public override void OnCustomEvent(string eventName)
    {
        throw new NotImplementedException();
    }

    public override void UpdateState(int state)
    {
       
    }
}
