/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   BearFactory.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/4/25 9:21:24
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;

public class BearFactory : ICharacterFactory
{
    public ICharacter CreateCharacter<T>(int characterID, CharacterRefreshPO characterRefreshPO) where T : ICharacter, new()
    {
        ICharacter character = new T();

        ICharacterBuilder builder = new BearBuilder(character, characterID, characterRefreshPO);

        return CharacterBuilderDirector.Construct(builder);
    }
}