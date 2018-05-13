/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   ISpawnCharacter.cs
 * 
 * 简    介:    命令基类
 * 
 * 创建标识：   Pancake 2018/3/3 17:57:59
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class ISpawnCommand
{
    // Character基本信息
    protected int mCharacterID;
    // Character刷新信息
    protected CharacterRefreshPO mCharacterRefreshPO;

    protected Vector3 mSpawnPosition = Vector3.zero;

    public ISpawnCommand(int characterID, CharacterRefreshPO characterRefreshPO)
    {
        mCharacterID = characterID;
        mCharacterRefreshPO = characterRefreshPO;
    }

    /// <summary>
    /// 执行命令
    /// </summary>
    public abstract void Execute();
}