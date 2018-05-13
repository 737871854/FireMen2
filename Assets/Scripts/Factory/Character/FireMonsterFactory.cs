/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   FireMonsterFactory.cs
 * 
 * 简    介:    小火怪，烟雾怪工厂
 * 
 * 创建标识：   Pancake 2018/3/2 11:35:10
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;
using UnityEngine;

public class FireMonsterFactory : ICharacterFactory
{
    public ICharacter CreateCharacter<T>(int characterID, CharacterRefreshPO characterRefreshPO) where T : ICharacter, new()
    {
        ICharacter character = new T();

        ICharacterBuilder builder = new FireMonsterBuilder(character, characterID, characterRefreshPO);

        return CharacterBuilderDirector.Construct(builder);
    }
}