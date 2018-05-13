/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   NewStageObserverArchievement.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/3/5 9:42:19
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;

public class NewStageObserverCamera : IGameEventObserver
{
    private NewStageSubject mSubject;
    private CameraManager mCameraManager;

    public NewStageObserverCamera(CameraManager cameraManager)
    {
        mCameraManager = cameraManager;
    }

    public override void SetSubject(IGameEventSubject sub)
    {
        mSubject = sub as NewStageSubject;
    }

    public override void Update()
    {
        mCameraManager.UpdateState(mSubject.stageCount);
    }
}