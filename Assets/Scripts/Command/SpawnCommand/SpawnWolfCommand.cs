/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   SpawnWolfCommand.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/5/2 16:42:21
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;

public class SpawnWolfCommand : ISpawnCommand
{
    public SpawnWolfCommand(int characterID, CharacterRefreshPO characterRefreshPO) : base(characterID, characterRefreshPO)
    {
    }

    public override void Execute()
    {
        FactoryManager.wolfFactory.CreateCharacter<Wolf>(mCharacterID, mCharacterRefreshPO);
    }
}