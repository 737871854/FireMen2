/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   SpawnNpcCommand.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/3/20 16:53:13
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;

public class SpawnNpcCommand : ISpawnCommand
{
    public SpawnNpcCommand(int characterID, CharacterRefreshPO characterRefreshPO) : base(characterID, characterRefreshPO)
    {
    }

    public override void Execute()
    {
        FactoryManager.npcFactory.CreateCharacter<Npc>(mCharacterID, mCharacterRefreshPO);
    }
}