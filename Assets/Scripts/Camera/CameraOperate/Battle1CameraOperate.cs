/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   Battle1CameraStrategy.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/4/10 10:59:29
 * 
 * 修改描述：   
 * 
 */

using System.Collections.Generic;
using UnityEngine;

public class Battle1CameraOperate : ICameraOperate
{
    public class EventName
    {
        public const string Pause = "Pause"; // 暂停
        public const string SlowDown = "SlowDown"; // 激活
        public const string Normal = "Normal"; // 回复正常速度
        public const string ActiveStageSys = "ActiveStageSys"; // 激活怪物
        public const string Explode = "Explode"; // 油管爆炸
        public const string Destroy = "Destroy";
        public const string BossSpeed = "BossSpeed";
    }

    public Battle1CameraOperate(CameraManager cameraManager) : base(cameraManager)
    {
    }

    public override void InitCameraRoll()
    {
        base.InitCameraRoll();
        PauseRoll = true;
    }

    public override float GetCameraSpeed(string eventName)
    {
        float speed = 0;


        return speed;
    }

    public override void OnCustomEvent(string eventName)
    {
        switch (eventName)
        {
            case EventName.Pause:
                mCameraManager.PauseCPA();
                break;
            case EventName.SlowDown:
                CPASpeed(1.0f);
                break;
            case EventName.Normal:
                NormalSpeed();
                break;
            case EventName.ActiveStageSys:
                EventDispatcher.TriggerEvent(EventDefine.Event_Stage_System_Is_Pause, false);
                break;
            case EventName.Explode:
                break;
            case EventName.Destroy:
                ioo.characterSystem.Destroy();
                break;
            case EventName.BossSpeed:
                CPASpeed(1.5f);
                break;
        }
    }

    public override void UpdateState(int state)
    {
        if (mCurrentState == state) return;
        switch (mCurrentState)
        {
            case 0:
                break;
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
        }

        mCurrentState = state;
    }
}