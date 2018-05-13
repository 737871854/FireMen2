﻿/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   SpawnHugeFireMonsterCommand.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/4/8 9:43:32
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;

public class SpawnHugeFireMonsterCommand : ISpawnCommand
{
    public SpawnHugeFireMonsterCommand(int characterID, CharacterRefreshPO characterRefreshPO) : base(characterID, characterRefreshPO)
    {
    }

    public override void Execute()
    {
        FactoryManager.hugeFireMonsterFactory.CreateCharacter<HugeFireMonster>(mCharacterID, mCharacterRefreshPO);
    }
}