/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   ExtendMethod.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2017/4/5 15:07:24
 * 
 * 修改描述：   添加注释
 * 
 */


using UnityEngine;
using System.Collections.Generic;

public static class ExtendMethod
{
    /// <summary>
    /// 自动获取对象或赋予对象T组件，勿过于频繁调用
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="go"></param>
    /// <returns></returns>
    public static T GetOrAddComponent<T>(this GameObject go) where T : Component
    {
        T t = go.GetComponent<T>();
        if (null == t)
            t = go.AddComponent<T>();
        return t;
    }

    public static void AddScreenCrash(this GameObject go)
    {
        Canvas canvas = ioo.gameMode.UICanvas;
        Vector2 pos0 = Camera.main.WorldToScreenPoint(go.transform.position);
        Vector3 pos1;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(canvas.transform as RectTransform, pos0, canvas.worldCamera, out pos1))
        {
            ioo.TriggerListener(EventLuaDefine.Event_Screen_Crack, pos1);
        }
    }

    /// <summary>
    /// 字符串转浮点，如果转换失败返回0
    /// </summary>
    /// <param name="self"></param>
    /// <returns></returns>
    public static float ToFloat(this string self)
    {
        float f = 0;
        float.TryParse(self, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture.NumberFormat, out f);
        return f;
    }

    /// <summary>
    /// 字符串转整形 如果转换失败 返回0;
    /// </summary>
    /// <param name="self"></param>
    /// <returns></returns>
    public static int ToInt(this string self)
    {
        int i = 0;
        int.TryParse(self, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture.NumberFormat, out i);
        return i;
    }
}
