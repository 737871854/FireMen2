/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   SpritesManager.cs
 * 
 * 简    介:    图集处理
 * 
 * 创建标识：   Pancake 2017/9/6 13:51:00
 * 
 * 修改描述：   
 * 
 */


using UnityEngine;
using System.Collections.Generic;
using UnityEngine.U2D;
using UnityEngine.UI;

public class SpritesManager
{
    private readonly static object mObject = new object();
    private static SpritesManager mInstance;
    public static SpritesManager Instance
    {
        get
        {
            if (mInstance == null)
            {
                lock (mObject)
                {
                    mInstance = new SpritesManager();
                }
            }
            return mInstance;
        }
    }

    // 图集名，图集
    private Dictionary<string, SpriteAtlas> SpriteDic = new Dictionary<string, SpriteAtlas>();

    #region Public Function
    /// <summary>
    /// 从本地加载图集
    /// </summary>
    /// <param name="name"></param>
    public SpriteAtlas LoadAtlas(string atlasname)
    {
        SpriteAtlas atlas = null;
        if (!SpriteDic.ContainsKey(atlasname) || SpriteDic[atlasname] == null)
            atlas = ToLoadAtlas(atlasname);

        return atlas;
    }

    /// <summary>
    /// 获取指定Atlas的所有Sprite，无论Atlas是否存在
    /// </summary>
    /// <param name="atlasname"></param>
    /// <returns></returns>
    public List<Sprite> LoadAllSprites(string atlasname)
    {
        if (!SpriteDic.ContainsKey(atlasname) || SpriteDic[atlasname] == null)
        {
            ToLoadAtlas(atlasname);
        }

        Sprite[] sprites = new Sprite[SpriteDic[atlasname].spriteCount];
        SpriteDic[atlasname].GetSprites(sprites);
        List<Sprite> list = new List<Sprite>();
        list.AddRange(sprites);

        for (int i = 0; i < list.Count; ++i )
        {
            string name = list[i].name;
            if (name.Contains("(Clone)"))
            {
                int length = name.Length;
                name = name.Remove(length - 7, 7);
                list[i].name = name;
            }
        }

        return list;
    }

    /// <summary>
    /// 从指定Atlas中获取Sprite
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public Sprite GetSprite(string atlasname, string spritename)
    {
        if (!SpriteDic.ContainsKey(atlasname) || SpriteDic[atlasname] == null)
        {
            Debug.LogError("The atlas: " + atlasname + "is not exit or null");
            return null;
        }

        Sprite sprite = SpriteDic[atlasname].GetSprite(spritename);
        return sprite;
    }

    /// <summary>
    /// 获取指定Atlas中的所有Sprite
    /// </summary>
    /// <param name="spritename"></param>
    /// <param name="atlasname"></param>
    /// <returns></returns>
    public List<Sprite> GetSprites(string atlasname)
    {
        if (!SpriteDic.ContainsKey(atlasname) || SpriteDic[atlasname] == null)
        {
            Debug.LogError("The atlas: " + atlasname + "is not exit or null");
            return null;
        }

        Sprite[] sprites = null;
        SpriteDic[atlasname].GetSprites(sprites, atlasname);

        List<Sprite> list = new List<Sprite>();
        list.AddRange(sprites);

        return list;
    }

    /// <summary>
    /// 获取指定对象包含所有的Image
    /// </summary>
    /// <param name="go"></param>
    /// <returns></returns>
    public List<Image> FindAllImage(Transform tran)
    {
        List<Image> list = new List<Image>();
        FindImage(tran, list);

        Image[] imgs = new Image[list.Count];
        
        return list;
    }

    /// <summary>
    /// 移除图集
    /// </summary>
    /// <param name="name"></param>
    public void RemoveAtlas(string atlasname)
    {
        if (SpriteDic.ContainsKey(atlasname))
        {
            SpriteDic.Remove(atlasname);
        }
    }

    /// <summary>
    /// 清楚所有Atlas
    /// </summary>
    public void Clear()
    {
        SpriteDic.Clear();
    }
    #endregion

    #region Private Function
    /// <summary>
    /// 从本地加载图集
    /// </summary>
    /// <param name="atlasname"></param>
    private SpriteAtlas ToLoadAtlas(string atlasname)
    {
        SpriteAtlas spriteatla = null;

        ResourceMisc.AssetWrapper aw = ioo.resourceManager.LoadAsset(atlasname, typeof(SpriteAtlas));
        if (null == aw)
        {
            Debug.LogError("The SpriteAtlas name: " + atlasname + " is not exit, please check the assetbundle");
            return spriteatla;
        }
        spriteatla = (SpriteAtlas)aw.GetAsset();


        if (spriteatla != null)
        {
            if (SpriteDic.ContainsKey(atlasname))
            {
                SpriteDic[atlasname] = spriteatla;
            }
            else
            {
                SpriteDic.Add(atlasname, spriteatla);
            }
        }
        else
        {
            Debug.LogError("The path: " + atlasname + " is not Exit!");
        }

        return spriteatla;
    }

    /// <summary>
    /// 迭代找出所有Image
    /// </summary>
    /// <param name="go"></param>
    /// <param name="list"></param>
    private void FindImage(Transform go, List<Image> list)
    {
        Image img = go.GetComponent<Image>();
        if (img != null)
            list.Add(img);

        for (int i = 0; i < go.transform.childCount; ++i )
        {
            FindImage(go.transform.GetChild(i), list);
        }
    }
    #endregion
}
