/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   SpawnBullDemonKingCommand.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/4/19 16:29:21
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;

public class SpawnBullDemonKingCommand : ISpawnCommand
{
    public SpawnBullDemonKingCommand(int characterID, CharacterRefreshPO characterRefreshPO) : base(characterID, characterRefreshPO)
    {
    }

    public override void Execute()
    {
        FactoryManager.bullDemonKingFactory.CreateCharacter<BullDemonKing>(mCharacterID, mCharacterRefreshPO);
    }
}