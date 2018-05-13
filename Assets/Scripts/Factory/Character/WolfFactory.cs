/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   WolfFactory.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/5/2 16:29:08
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;

public class WolfFactory : ICharacterFactory
{
    public ICharacter CreateCharacter<T>(int characterID, CharacterRefreshPO characterRefreshPO) where T : ICharacter, new()
    {
        ICharacter character = new T();

        ICharacterBuilder builder = new WolfBuilder(character, characterID, characterRefreshPO);

        return CharacterBuilderDirector.Construct(builder);
    }
}