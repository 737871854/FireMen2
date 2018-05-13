/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   SpawnCitizenCommand.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/3/20 13:34:22
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;

public class SpawnCitizenCommand : ISpawnCommand
{
    public SpawnCitizenCommand(int characterID, CharacterRefreshPO characterRefreshPO) : base(characterID, characterRefreshPO)
    {
    }

    public override void Execute()
    {
        FactoryManager.citizenFactory.CreateCharacter<Citizen>(mCharacterID, mCharacterRefreshPO);
    }
}