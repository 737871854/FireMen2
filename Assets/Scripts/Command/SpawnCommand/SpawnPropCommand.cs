/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   SpawnPropCommand.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/4/16 16:06:06
 * 
 * 修改描述：   
 * 
 */

using System.Collections.Generic;
using UnityEngine;

public class SpawnPropCommand : ISpawnCommand
{
    public SpawnPropCommand(int characterID, CharacterRefreshPO characterRefreshPO, Vector3 spawnPosition) : base(characterID, characterRefreshPO)
    {
        mSpawnPosition = spawnPosition;
    }

    public override void Execute()
    {
        Prop prop = FactoryManager.propFactory.CreateCharacter<Prop>(mCharacterID, mCharacterRefreshPO) as Prop;
        if (mSpawnPosition != Vector3.zero)
            prop.gameObject.transform.position = mSpawnPosition;
    }
}