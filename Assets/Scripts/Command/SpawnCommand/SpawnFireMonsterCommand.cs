/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   SpawnFireMonsterCommand.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/3/3 18:15:25
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;

public class SpawnFireMonsterCommand : ISpawnCommand
{
    public SpawnFireMonsterCommand(int characterID, CharacterRefreshPO characterRefreshPO) : base(characterID, characterRefreshPO)
    {
    }

    public override void Execute()
    {
        FactoryManager.fireMonsterFactory.CreateCharacter<FireMonster>(mCharacterID, mCharacterRefreshPO);
    }
}