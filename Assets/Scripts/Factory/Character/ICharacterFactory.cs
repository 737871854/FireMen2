/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   ICharacterFactory.cs
 * 
 * 简    介:    角色工厂基类
 * 
 * 创建标识：   Pancake 2018/3/2 11:33:32
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacterFactory
{
    ICharacter CreateCharacter<T>(int characterID, CharacterRefreshPO characterRefreshPO) where T : ICharacter, new();
}