/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   SpawnBearCommand.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/4/25 9:20:02
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;

public class SpawnBearCommand : ISpawnCommand
{
    public SpawnBearCommand(int characterID, CharacterRefreshPO characterRefreshPO) : base(characterID, characterRefreshPO)
    {
    }

    public override void Execute()
    {
        FactoryManager.bearFactory.CreateCharacter<Bear>(mCharacterID, mCharacterRefreshPO);
    }
}