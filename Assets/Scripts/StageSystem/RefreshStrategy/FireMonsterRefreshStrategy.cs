/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   FireMonsterRefreshStrategy.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/3/5 16:42:26
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;

public class FireMonsterRefreshStrategy : IRefreshStrategy
{
    private string mSceneName;
    public FireMonsterRefreshStrategy(string sceneName)
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
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                    case 7:
                    case 9:
                        maxCount = 6;
                        break;
                }
                break;
            case SceneNames.BattleScene0_1:
                break;
            case SceneNames.BattleScene0_2:
                break;
            case SceneNames.BattleScene1:
                switch (level)
                {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                    case 7:
                        maxCount = 6;
                        break;
                    case 10:
                        maxCount = 4;
                        break;
                }
                break;
            case SceneNames.BattleScene2:
                break;
        }
        return maxCount;
    }
}