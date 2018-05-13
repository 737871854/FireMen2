/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   IOHelp.cs
 * 
 * 简    介:    辅助Lua来注册和响应IO消息
 * 
 * 创建标识：   Pancake 2017/4/14 10:13:09
 * 
 * 修改描述：
 * 
 */


using UnityEngine;
using System.Collections.Generic;


public enum EnumIOEvent
{
    /// <summary>
    /// 投币
    /// </summary>
    onCoin,
    /// <summary>
    /// 确认
    /// </summary>
    onSure,
    /// <summary>
    /// 后台确认
    /// </summary>
    onButtonA,
    /// <summary>
    /// 后台选择
    /// </summary>
    onButtonB,
    /// <summary>
    /// 鼠标位置
    /// </summary>
    onMousePos,
}


public class IOLuaHelper
{
    private static readonly object mObject = new object();
    private static IOLuaHelper mInstance;
    public static IOLuaHelper Instance
    {
        get
        {
            if (null == mInstance)
            {
                lock(mObject)
                {
                    if (null == mInstance)
                        mInstance = new IOLuaHelper();
                }
            }
            return mInstance;
        }
    }

    private Dictionary<EnumIOEvent,Dictionary<string, UtilCommon.OnIOEventHandle>> m_eventDict = new Dictionary<EnumIOEvent, Dictionary<string, UtilCommon.OnIOEventHandle>>();

    #region 外部调用接口
    /// <summary>
    /// 
    /// </summary>
    /// <param name="_num"></param>
    /// <param name="_handle"></param>
    public void RegesterListener(int _num, UtilCommon.OnIOEventHandle _handle, string _guid)
    {
        EnumIOEvent _type = (EnumIOEvent)_num;
        RegesterListener(_type, _handle, _guid);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="_type"></param>
    /// <param name="_handle"></param>
    public void RegesterListener(EnumIOEvent _type, UtilCommon.OnIOEventHandle _handle, string _guid)
    {
        if (!m_eventDict.ContainsKey(_type))
        {
            m_eventDict.Add(_type, new Dictionary<string,UtilCommon.OnIOEventHandle>());
        }

        if (!HasRegestered(_type, _guid))
            m_eventDict[_type].Add(_guid, _handle);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="_num"></param>
    /// <param name="_handle"></param>
    public void RemoveListener(int _num, string _guid)
    {
        EnumIOEvent _type = (EnumIOEvent)_num;
        RemoveListener(_type, _guid);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="_type"></param>
    /// <param name="_handle"></param>
    public void RemoveListener(EnumIOEvent _type, string _guid)
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
    /// <param name="_num"></param>
    /// <param name="_playerID"></param>
    /// <param name="_value"></param>
    public void TriggerListener(int _num, int _playerID = 0, bool _flag = true)
    {
        EnumIOEvent _type = (EnumIOEvent)_num;
        TriggerListener(_type, _playerID, _flag);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="_type"></param>
    /// <param name="_playerID"></param>
    /// <param name="_value"></param>
    public void TriggerListener(EnumIOEvent _type, int _playerID = 0, bool _flag = true)
    {
        if (null == m_eventDict || !m_eventDict.ContainsKey(_type))
            return;

        Dictionary<string, UtilCommon.OnIOEventHandle> dict = m_eventDict[_type];

        List<UtilCommon.OnIOEventHandle> list = new List<UtilCommon.OnIOEventHandle>(dict.Values);
        for (int i = 0; i < dict.Count; ++i)
        {
            UtilCommon.OnIOEventHandle handle = list[i];
            if (null != handle)
               handle(_playerID, _flag);
        }
    }
    #endregion

   
    /// <summary>
    /// 
    /// </summary>
    /// <param name="_type"></param>
    /// <param name="_handle"></param>
    /// <returns></returns>
    private bool HasRegestered(EnumIOEvent _type, string _guid)
    {
        if (m_eventDict[_type].ContainsKey(_guid))
        {
            Debug.Log("Event " + _guid + " has already been regestered!");
            return true;
        }

        return false;
    }
}
