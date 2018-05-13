/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   CoinFactory.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/4/13 14:11:40
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;

public class CoinFactory : ICharacterFactory
{
    public ICharacter CreateCharacter<T>(int characterID, CharacterRefreshPO characterRefreshPO) where T : ICharacter, new()
    {
        ICharacter character = new T();

        CoinBuilder builder = new CoinBuilder(character, characterID, characterRefreshPO);

        return CharacterBuilderDirector.Construct(builder);
    }
}