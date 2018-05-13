/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   SelectMapFactory.cs
 * 
 * 简    介:    地图工厂
 * 
 * 创建标识：   Pancake 2018/3/3 11:05:58
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;

public class SelectMapFactory : ICharacterFactory
{
    public ICharacter CreateCharacter<T>(int characterID, CharacterRefreshPO characterRefreshPO) where T : ICharacter, new()
    {
        ICharacter character = new T();

        ICharacterBuilder builder = new SelectMapBuilder(character, characterID, characterRefreshPO);

        return CharacterBuilderDirector.Construct(builder);
    }
}