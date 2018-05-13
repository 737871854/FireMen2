/*************************************************
author：ricky pu
data：2014.4.12
email:32145628@qq.com
 * 
 * 修改：Pancake 2017.8 接入IO通信，接入C#和Lua逻辑通信
**********************************************/
using UnityEngine;
using System.Collections.Generic;
using System.Reflection;
using LuaInterface;
using System;

public static class LuaHelper {

    /// <summary>
    /// getType
    /// </summary>
    /// <param name="classname"></param>
    /// <returns></returns>
    public static System.Type GetType(string classname) {
        Assembly assb = Assembly.GetExecutingAssembly();  //.GetExecutingAssembly();
        System.Type t = null;
        t = assb.GetType(classname); ;
        if (t == null) {
            t = assb.GetType(classname);
        }
        return t;
    }

    /// <summary>
    /// GetComponentInChildren
    /// </summary>
    public static Component GetComponentInChildren(GameObject obj, string classname) {
        System.Type t = GetType(classname);
        Component comp = null;
        if (t != null && obj != null) comp = obj.GetComponentInChildren(t);
        return comp;
    }

    /// <summary>
    /// GetComponent
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="classname"></param>
    /// <returns></returns>
    public static Component GetComponent(GameObject obj, string classname) {
        if (obj == null) return null; 
        return obj.GetComponent(classname);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="classname"></param>
    /// <returns></returns>
    public static Component[] GetComponentsInChildren(GameObject obj, string classname) {
        System.Type t = GetType(classname);
        if (t != null && obj != null) return obj.transform.GetComponentsInChildren(t);
        return null;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static Transform[] GetAllChild(GameObject obj) {
        Transform[] child = null;
        int count = obj.transform.childCount;
        child = new Transform[count];
        for (int i = 0; i < count; i++) {
            child[i] = obj.transform.GetChild(i);
        }
        return child;
    }

    /// <summary>
    /// pbc/pblua函数回调
    /// </summary>
    /// <param name="func"></param>
    public static void OnCallLuaFunc(LuaStringBuffer data, LuaFunction func) {
        byte[] buffer = data.buffer;
        Debug.LogWarning("OnCallLuaFunc buffer:>>" + buffer + " lenght:>>" + buffer.Length);
        if (func != null) Util.PushBufferToLua(func, buffer);
    }

    /// <summary>
    /// cjson函数回调
    /// </summary>
    /// <param name="data"></param>
    /// <param name="func"></param>
    public static void OnJsonCallFunc(string data, LuaFunction func) {
        Debug.LogWarning("OnJsonCallback data:>>" + data + " lenght:>>" + data.Length);
        if (func != null) func.Call(data);
    }

    /// <summary>
    /// C#和Lua逻辑通信用
    /// </summary>
    /// <param name="func"></param>
    /// <returns></returns>
    public static Util.EventHandle EventHandle(LuaFunction func)
    {
        Util.EventHandle action = (data) =>
            {
                func.Call(data);
            };
        return action;
    }

    /// <summary>
    /// IO通信回调
    /// </summary>
    /// <param name="func"></param>
    /// <returns></returns>
    public static Util.OnIOEventHandle OnIOEventHandle(LuaFunction func)
    {
        Util.OnIOEventHandle action = (id, flag) =>
            {
                func.Call(id, flag);
            };
        return action;
    }

    public static Util.OnTouchEventHandle OnTouchEventHandle(LuaFunction func)
    {
        Util.OnTouchEventHandle action = (go, args, parmas) =>
        {
            func.Call(go, args, parmas);
        };
        return action;
    }

    public static Util.VoidDelegate VoidDelegate(LuaFunction func)
    {
        Util.VoidDelegate action = (go, eventData) =>
        {
            func.Call(go, eventData);
        };
        return action;
    }

    public static Util.DragDelegate DragDelegate(LuaFunction func)
    {
        Util.DragDelegate action = (eventData) =>
        {
            func.Call(eventData);
        };
        return action;
    }

    public static Util.ShakeDelegate ShakeDelegate(LuaFunction func){
        Util.ShakeDelegate action = (i) =>
        {
           func.Call(i);
        };
        return action;
    }

    public static Util.ShakeByBuildingDelegate ShakeByBuildingDelegate(LuaFunction func)
    {
        Util.ShakeByBuildingDelegate action = (str, level) =>
        {
            func.Call(str, level);
        };
        return action;
    }
}
