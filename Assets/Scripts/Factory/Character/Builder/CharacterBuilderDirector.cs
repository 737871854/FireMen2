/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   CharacterBuilderDirector.cs
 * 
 * 简    介:    构建管理器
 * 
 * 创建标识：   Pancake 2018/3/2 11:55:38
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;

public class CharacterBuilderDirector
{
    public static ICharacter Construct(ICharacterBuilder builder)
    {
        builder.AddCharacterAttr();
        builder.AddGameObject();
        builder.AddInCharacterSystem();
        return builder.GetResult();
    }
}