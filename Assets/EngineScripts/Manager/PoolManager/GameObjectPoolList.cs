/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   GameObjectPoolList.cs
 * 
 * 简    介:    对象池的List，管理所有的资源池，用于配置
 * 
 * 创建标识：   Pancake 2017/4/2 15:53:07
 * 
 * 修改描述：
 * 
 */


using UnityEngine;
using System.Collections.Generic;

public class GameObjectPoolList : ScriptableObject  // 表示吧类GameObjectPoolList变成可以自定义资源配置的文件
{
    public List<GameObjectPool> poolList;


}
