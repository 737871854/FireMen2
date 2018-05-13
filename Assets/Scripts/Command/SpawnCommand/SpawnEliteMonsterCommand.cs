/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   EliteMonsterCommand.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/3/22 10:00:20
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;

public class SpawnEliteMonsterCommand : ISpawnCommand
{
    public SpawnEliteMonsterCommand(int characterID, CharacterRefreshPO characterRefreshPO) : base(characterID, characterRefreshPO)
    {
    }

    public override void Execute()
    {
        FactoryManager.eliteMonsterFactory.CreateCharacter<EliteMonster>(mCharacterID, mCharacterRefreshPO);
    }
}