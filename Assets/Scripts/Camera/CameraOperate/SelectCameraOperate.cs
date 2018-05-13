/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   SelectCameraOperate.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/4/10 17:53:13
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;

public class SelectCameraOperate : ICameraOperate
{
    public SelectCameraOperate(CameraManager cameraManager) : base(cameraManager)
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