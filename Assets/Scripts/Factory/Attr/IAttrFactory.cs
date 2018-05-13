/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   IAttrFactory.cs
 * 
 * 简    介:    属性工厂基类
 * 
 * 创建标识：   Pancake 2018/3/2 13:35:11
 * 
 * 修改描述：   
 * 
 */


public interface IAttrFactory
{
    CharacterBaseAttr GetCharacterBaseAttr(int characterID);

}