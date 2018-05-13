/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   EliteMonsterFactory.cs
 * 
 * 简    介:    精英怪工厂
 * 
 * 创建标识：   Pancake 2018/3/22 9:35:55
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;

public class EliteMonsterFactory : ICharacterFactory
{
    public ICharacter CreateCharacter<T>(int characterID, CharacterRefreshPO characterRefreshPO) where T : ICharacter, new()
    {
        ICharacter character = new T();

        ICharacterBuilder builder = new EliteMonsterBuilder(character, characterID, characterRefreshPO);

        return CharacterBuilderDirector.Construct(builder);
    }
}