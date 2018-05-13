/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   CoinAttr.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/4/13 14:07:07
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;

public class CoinAttr : ICharacterAttr
{
    public CoinAttr(IAttrStrategy strategy, CharacterBaseAttr baseAttr) : base(strategy, baseAttr)
    {
    }
}