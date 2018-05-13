/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   EventLuaHelper.cs
 * 
 * 简    介:    负责Lua和C#之间消息事件的传递
 * 
 * 创建标识：   Pancake 2017/9/11 14:25:54
 * 
 * 修改描述：
 * 
 */


using UnityEngine;
using System.Collections.Generic;
using System;

public class EventLuaDefine
{
    // 待机视频的播放与结束
    public static string Coin_Event_Play_Idle_Movie     = "Coin_Event_Play_Idle_Movie";
    public static string Coin_Event_Stop_Idle_Movie     = "Coin_Event_Stop_Idle_Movie";
    public static string Coin_Event_End_Idle_Movie      = "Coin_Event_End_Idle_Movie";

    // 角色被正被选择
    public static string Character_Is_Been_Spray        = "Character_Is_Been_Spray";
    // 地图被正被选择
    public static string Map_Is_Been_Spray              = "Map_Is_Been_Spray";

    // 进入场景选择
    public static string Character_Select_End           = "Character_Select_End";

    public static string N0_Character_Is_Selected       = "N0_Character_Is_Selected";

    // 没有场景被选择
    public static string No_Map_Is_Selected             = "No_Map_Is_Selected";

    // 场景选择结束
    public static string Map_Select_End                 = "Map_Select_End";

    // 游戏结束
    public static string Event_Game_Over                = "Event_Game_Over";

    // 关卡通过
    public static string Event_Level_Pass               = "Event_Level_Pass";

    // 通知UI进入结算界面
    public static string Event_To_Summary               = "Event_To_Summary";

    // 通知UI进入加水界面 
    public static string Event_Fill_Water               = "Event_Fill_Water";

    // 通知UI玩家被劫持
    public static string Event_Hold_Player              = "Event_Hold_Player";

    // Boss出生
    public static string Event_Boss_Born                = "Event_Boss_Born";

    // Boss死亡
    public static string Event_Boss_Dead                = "Event_Boss_Dead";

    // 屏幕破碎
    public static string Event_Screen_Crack             = "Event_Screen_Crack";

}

public class EventLuaHelper
{
    private static readonly object mObject = new object();
    private static EventLuaHelper mInstance;
    public static EventLuaHelper Instance
    {
        get
        {
            if (null == mInstance)
            {
                lock (mObject)
                {
                    if (null == mInstance)
                        mInstance = new EventLuaHelper();
                }
            }
            return mInstance;
        }
    }

    private Dictionary<string, Dictionary<string, UtilCommon.EventHandle>> m_eventDict = new Dictionary<string, Dictionary<string, UtilCommon.EventHandle>>();

    #region 外部调用接口
    /// <summary>
    /// 
    /// </summary>
    /// <param name="_type"></param>
    /// <param name="_handle"></param>
    public void RegesterListener(string _type, UtilCommon.EventHandle _handle, string _guid)
    {
        if (!m_eventDict.ContainsKey(_type))
        {
            m_eventDict.Add(_type, new Dictionary<string, UtilCommon.EventHandle>());
        }

        if (!HasRegestered(_type, _guid))
            m_eventDict[_type].Add(_guid, _handle);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="_type"></param>
    /// <param name="_handle"></param>
    public void RemoveListener(string _type, string _guid)
    {
        if (m_eventDict.ContainsKey(_type))
        {
            if (m_eventDict[_type].ContainsKey(_guid))
            {
                m_eventDict[_type].Remove(_guid);
            }

            if (m_eventDict[_type].Count <= 0)
                m_eventDict.Remove(_type);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="_type"></param>
    public void TriggerListener(string _type)
    {
        object _data = null;
        TriggerListener(_type, _data);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="_type"></param>
    /// <param name="_playerID"></param>
    /// <param name="_value"></param>
    public void TriggerListener(string _type, object _data)
    {
        if (null == m_eventDict || !m_eventDict.ContainsKey(_type))
            return;
         
        Dictionary<string, UtilCommon.EventHandle> dict = m_eventDict[_type];

        List<UtilCommon.EventHandle> list = new List<UtilCommon.EventHandle>(dict.Values);
        for (int i = 0; i < dict.Count; ++i)
        {
            UtilCommon.EventHandle handle = list[i];
            if (null != handle)
                handle(_data);
        }
    }
    #endregion


    /// <summary>
    /// 
    /// </summary>
    /// <param name="_type"></param>
    /// <param name="_handle"></param>
    /// <returns></returns>
    private bool HasRegestered(string _type, string _guid)
    {
        if (m_eventDict[_type].ContainsKey(_guid))
        {
            Debugger.Log("Event " + _guid + " has already been regestered!");
            return true;
        }

        return false;
    }
}
