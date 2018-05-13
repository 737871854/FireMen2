/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   HelicopterFactory.cs
 * 
 * 简    介:    直升机工厂
 * 
 * 创建标识：   Pancake 2018/3/21 10:29:24
 * 
 * 修改描述：   
 * 
 */


public class HelicopterFactory : ICharacterFactory
{
    public ICharacter CreateCharacter<T>(int characterID, CharacterRefreshPO characterRefreshPO) where T : ICharacter, new()
    {
        ICharacter character = new T();

        ICharacterBuilder builder = new HelicopterBuilder(character, characterID, characterRefreshPO);

        return CharacterBuilderDirector.Construct(builder);
    }
}