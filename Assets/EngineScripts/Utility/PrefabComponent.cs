/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   EventLuaHelper.cs
 * 
 * 简    介:    本地序列化Sprite用
 * 
 * 创建标识：   Pancake 2016/7/14 14:23:14
 * 
 * 修改描述：  （废弃）
 * 
 */

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class UIPrefab
{
    public string name;
    public Sprite sprite;
}

public class PrefabComponent : MonoBehaviour 
{
    [SerializeField]
    public UIPrefab[] uiPrefab;

    private Dictionary<string, Sprite> spriteDic;

    public Dictionary<string, Sprite> SpriteDic
    {
        get { return spriteDic; }
    }

    public void Init()
    {
        spriteDic = new Dictionary<string, Sprite>();
        for (int i = 0; i < uiPrefab.Length; ++i )
        {
            spriteDic.Add(uiPrefab[i].name, uiPrefab[i].sprite);
        }
    }
}
