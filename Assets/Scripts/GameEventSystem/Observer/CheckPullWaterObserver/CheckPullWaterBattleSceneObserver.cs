/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   CheckPullWaterObserverBattleScene.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/4/23 9:04:33
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;

public class CheckPullWaterBattleSceneObserver : IGameEventObserver
{

    private CheckPullWaterSubject mSubject;
    private IBattleScene mBattleScene;

    public CheckPullWaterBattleSceneObserver(IBattleScene battleScene)
    {
        mBattleScene = battleScene;
    }

    public override void SetSubject(IGameEventSubject sub)
    {
        mSubject = sub as CheckPullWaterSubject;
    }

    public override void Update()
    {
        mBattleScene.UpdateWaterPull(mSubject.characterType);
    }
}