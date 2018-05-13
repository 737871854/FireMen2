/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   SpawnCoinCommand.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/4/13 14:10:16
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;

public class SpawnCoinCommand : ISpawnCommand
{
    public SpawnCoinCommand(int characterID, CharacterRefreshPO characterRefreshPO) : base(characterID, characterRefreshPO)
    {
    }

    public override void Execute()
    {
        FactoryManager.coinFactory.CreateCharacter<Coin>(mCharacterID, mCharacterRefreshPO);
    }
}