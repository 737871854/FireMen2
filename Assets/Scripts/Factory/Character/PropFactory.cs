/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   PropFactory.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/4/16 16:04:17
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;

public class PropFactory : ICharacterFactory
{
    public ICharacter CreateCharacter<T>(int characterID, CharacterRefreshPO characterRefreshPO) where T : ICharacter, new()
    {
        ICharacter character = new T();

        ICharacterBuilder builder = new PropBuilder(character, characterID, characterRefreshPO);

        return CharacterBuilderDirector.Construct(builder);
    }
}