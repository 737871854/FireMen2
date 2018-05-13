/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   SelectCharacterAttr.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/3/3 9:36:06
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;

public class SelectCharacterAttr : ICharacterAttr
{
    public SelectCharacterAttr(IAttrStrategy strategy, CharacterBaseAttr baseAttr) : base(strategy, baseAttr)
    {
    }
}