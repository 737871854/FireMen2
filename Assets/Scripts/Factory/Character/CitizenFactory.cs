/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   CitizenFactory.cs
 * 
 * 简    介:    市民工厂
 * 
 * 创建标识：   Pancake 2018/3/20 11:33:31
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;

public class CitizenFactory : ICharacterFactory
{
    public ICharacter CreateCharacter<T>(int characterID, CharacterRefreshPO characterRefreshPO) where T : ICharacter, new()
    {
        ICharacter character = new T();

        ICharacterBuilder builder = new CitizenBuilder(character, characterID, characterRefreshPO);

        return CharacterBuilderDirector.Construct(builder);
    }
}