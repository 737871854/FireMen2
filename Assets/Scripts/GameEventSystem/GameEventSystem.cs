/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   GameEventSystem.cs
 * 
 * 简    介:    观察管理类
 * 
 * 创建标识：   Pancake 2018/3/5 9:35:38
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;

public enum GameEventType
{
    ScoreChange,
    CityzenRescued,
    NewStage,
    PlayerOnDamage,
    HelicopterReached,
    PullWater,
}

public class GameEventSystem
{
    private Dictionary<GameEventType, IGameEventSubject> mGameEvents = new Dictionary<GameEventType, IGameEventSubject>();

    /// <summary>
    /// 注册
    /// </summary>
    /// <param name="eventType"></param>
    /// <param name="observer"></param>
    public void RegisterObserver(GameEventType eventType, IGameEventObserver observer)
    {
        IGameEventSubject subject = GetGameEventSubject(eventType);
        if (subject == null) return;
        subject.RegisterObserver(observer);
        observer.SetSubject(subject);
    }

    /// <summary>
    /// 移除
    /// </summary>
    /// <param name="eventType"></param>
    /// <param name="observer"></param>
    public void RemoveObserver(GameEventType eventType, IGameEventObserver observer)
    {
        IGameEventSubject subject = GetGameEventSubject(eventType);
        if (subject == null) return;
        subject.RemoveObserver(observer);
        observer.SetSubject(null);
    }

    /// <summary>
    /// 广播
    /// </summary>
    /// <param name="eventType"></param>
    /// <param name="args"></param>
    public void NotifySubject(GameEventType eventType, params int[] args)
    {
        IGameEventSubject subject = GetGameEventSubject(eventType);
        if (subject == null) return;
        subject.Notify(args);
    }

    /// <summary>
    /// 添加消息类型
    /// </summary>
    /// <param name="eventType"></param>
    /// <returns></returns>
    private IGameEventSubject GetGameEventSubject(GameEventType eventType)
    {
        if (mGameEvents.ContainsKey(eventType) == false)
        {
            switch (eventType)
            {
                case GameEventType.ScoreChange:
                    mGameEvents.Add(GameEventType.ScoreChange, new ScoreChangeSubject());
                    break;
                case GameEventType.CityzenRescued:
                    mGameEvents.Add(GameEventType.CityzenRescued, new CitizenBeRescuedSubject());
                    break;
                case GameEventType.NewStage:
                    mGameEvents.Add(GameEventType.NewStage, new NewStageSubject());
                    break;
                case GameEventType.PlayerOnDamage:
                    mGameEvents.Add(GameEventType.PlayerOnDamage, new PlayerOnHurtSubject());
                    break;
                case GameEventType.HelicopterReached:
                    mGameEvents.Add(GameEventType.HelicopterReached, new HelicopterReachSubject());
                    break;
                case GameEventType.PullWater:
                    mGameEvents.Add(GameEventType.PullWater, new CheckPullWaterSubject());
                    break;
            }
        }
        return mGameEvents[eventType];
    }

}