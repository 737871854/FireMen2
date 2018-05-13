/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   CitizenRefreshStrategy.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/4/18 9:01:28
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;

public class CitizenRefreshStrategy : IRefreshStrategy
{
    private string mSceneName;

    public CitizenRefreshStrategy(string sceneName)
    {
        mSceneName = sceneName;
    }

    public int GetMonsterCount(int level)
    {
        int maxCount = 0;
        switch (mSceneName)
        {
            case SceneNames.BattleScene0_0:
                switch (level)
                {
                    case 0:
                        maxCount = 2;
                        break;
                    case 4:
                        maxCount = 2;
                        break;
                    case 9:
                        maxCount = 1;
                        break;
                }
                break;
            case SceneNames.BattleScene0_1:
                break;
            case SceneNames.BattleScene0_2:
                break;
            case SceneNames.BattleScene1:
                break;
            case SceneNames.BattleScene2:
                break;
        }
        return maxCount;
    }
}