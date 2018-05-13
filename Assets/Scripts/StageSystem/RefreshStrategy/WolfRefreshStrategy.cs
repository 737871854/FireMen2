/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   WolfRefreshStrategy.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/5/2 18:16:13
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;

public class WolfRefreshStrategy : IRefreshStrategy
{
    private string mSceneName;

    public WolfRefreshStrategy(string sceneName)
    {
        mSceneName = sceneName;
    }

    public int GetMonsterCount(int level)
    {
        int maxCount = 0;
        switch (mSceneName)
        {
            case SceneNames.BattleScene0_0:            
                break;
            case SceneNames.BattleScene0_1:
                break;
            case SceneNames.BattleScene0_2:
                break;
            case SceneNames.BattleScene1:
                switch (level)
                {
                    case 10:
                        maxCount = 1;
                        break;
                }
                break;
            case SceneNames.BattleScene2:
                break;
        }
        return maxCount;
    }
}