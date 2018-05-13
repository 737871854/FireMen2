/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   ScoreChangeObserverStageSystem.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/3/8 9:57:45
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;

public class ScoreChangeObserverPlayerManager : IGameEventObserver
{
    private ScoreChangeSubject mSubject;
    private PlayerManager mPlayerManager;
    public ScoreChangeObserverPlayerManager(PlayerManager playerManager)
    {
        mPlayerManager = playerManager;
    }

    public override void SetSubject(IGameEventSubject sub)
    {
        mSubject = sub as ScoreChangeSubject;
    }

    public override void Update()
    {
        mPlayerManager.UpdateScores(mSubject.playerScore);
    }
}