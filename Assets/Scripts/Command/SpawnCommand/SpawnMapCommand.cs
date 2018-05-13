/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   ISpawnMapCommand.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/3/3 18:14:13
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;

public class SpawnMapCommand : ISpawnCommand
{
    public SpawnMapCommand(int characterID, CharacterRefreshPO characterRefreshPO) : base(characterID, characterRefreshPO)
    {
    }

    public override void Execute()
    {
         FactoryManager.selectMapFactory.CreateCharacter<SelectMap>(mCharacterID, mCharacterRefreshPO);
    }
}