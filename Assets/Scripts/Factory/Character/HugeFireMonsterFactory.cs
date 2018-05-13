/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   HugeFireMonsterFactory.cs
 * 
 * 简    介:    大伙怪工厂
 * 
 * 创建标识：   Pancake 2018/4/8 9:22:39
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;

public class HugeFireMonsterFactory : ICharacterFactory
{
    public ICharacter CreateCharacter<T>(int characterID, CharacterRefreshPO characterRefreshPO) where T : ICharacter, new()
    {
        ICharacter character = new T();

        ICharacterBuilder builder = new HugeFireMonsterBuilder(character, characterID, characterRefreshPO);

        return CharacterBuilderDirector.Construct(builder);
    }
}