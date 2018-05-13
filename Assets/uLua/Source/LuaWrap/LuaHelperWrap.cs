using System;
using UnityEngine;
using LuaInterface;

public class LuaHelperWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("GetType", GetType),
			new LuaMethod("GetComponentInChildren", GetComponentInChildren),
			new LuaMethod("GetComponent", GetComponent),
			new LuaMethod("GetComponentsInChildren", GetComponentsInChildren),
			new LuaMethod("GetAllChild", GetAllChild),
			new LuaMethod("OnCallLuaFunc", OnCallLuaFunc),
			new LuaMethod("OnJsonCallFunc", OnJsonCallFunc),
			new LuaMethod("EventHandle", EventHandle),
			new LuaMethod("OnIOEventHandle", OnIOEventHandle),
			new LuaMethod("OnTouchEventHandle", OnTouchEventHandle),
			new LuaMethod("VoidDelegate", VoidDelegate),
			new LuaMethod("DragDelegate", DragDelegate),
			new LuaMethod("ShakeDelegate", ShakeDelegate),
			new LuaMethod("ShakeByBuildingDelegate", ShakeByBuildingDelegate),
			new LuaMethod("New", _CreateLuaHelper),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaScriptMgr.RegisterLib(L, "LuaHelper", regs);
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateLuaHelper(IntPtr L)
	{
		LuaDLL.luaL_error(L, "LuaHelper class does not have a constructor function");
		return 0;
	}

	static Type classType = typeof(LuaHelper);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetType(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		string arg0 = LuaScriptMgr.GetLuaString(L, 1);
		Type o = LuaHelper.GetType(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetComponentInChildren(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		GameObject arg0 = (GameObject)LuaScriptMgr.GetUnityObject(L, 1, typeof(GameObject));
		string arg1 = LuaScriptMgr.GetLuaString(L, 2);
		Component o = LuaHelper.GetComponentInChildren(arg0,arg1);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetComponent(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		GameObject arg0 = (GameObject)LuaScriptMgr.GetUnityObject(L, 1, typeof(GameObject));
		string arg1 = LuaScriptMgr.GetLuaString(L, 2);
		Component o = LuaHelper.GetComponent(arg0,arg1);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetComponentsInChildren(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		GameObject arg0 = (GameObject)LuaScriptMgr.GetUnityObject(L, 1, typeof(GameObject));
		string arg1 = LuaScriptMgr.GetLuaString(L, 2);
		Component[] o = LuaHelper.GetComponentsInChildren(arg0,arg1);
		LuaScriptMgr.PushArray(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetAllChild(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		GameObject arg0 = (GameObject)LuaScriptMgr.GetUnityObject(L, 1, typeof(GameObject));
		Transform[] o = LuaHelper.GetAllChild(arg0);
		LuaScriptMgr.PushArray(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnCallLuaFunc(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		LuaStringBuffer arg0 = LuaScriptMgr.GetStringBuffer(L, 1);
		LuaFunction arg1 = LuaScriptMgr.GetLuaFunction(L, 2);
		LuaHelper.OnCallLuaFunc(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnJsonCallFunc(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		string arg0 = LuaScriptMgr.GetLuaString(L, 1);
		LuaFunction arg1 = LuaScriptMgr.GetLuaFunction(L, 2);
		LuaHelper.OnJsonCallFunc(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int EventHandle(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		LuaFunction arg0 = LuaScriptMgr.GetLuaFunction(L, 1);
		UtilCommon.EventHandle o = LuaHelper.EventHandle(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnIOEventHandle(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		LuaFunction arg0 = LuaScriptMgr.GetLuaFunction(L, 1);
		UtilCommon.OnIOEventHandle o = LuaHelper.OnIOEventHandle(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnTouchEventHandle(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		LuaFunction arg0 = LuaScriptMgr.GetLuaFunction(L, 1);
		UtilCommon.OnTouchEventHandle o = LuaHelper.OnTouchEventHandle(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int VoidDelegate(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		LuaFunction arg0 = LuaScriptMgr.GetLuaFunction(L, 1);
		UtilCommon.VoidDelegate o = LuaHelper.VoidDelegate(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int DragDelegate(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		LuaFunction arg0 = LuaScriptMgr.GetLuaFunction(L, 1);
		UtilCommon.DragDelegate o = LuaHelper.DragDelegate(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ShakeDelegate(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		LuaFunction arg0 = LuaScriptMgr.GetLuaFunction(L, 1);
		UtilCommon.ShakeDelegate o = LuaHelper.ShakeDelegate(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ShakeByBuildingDelegate(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		LuaFunction arg0 = LuaScriptMgr.GetLuaFunction(L, 1);
		UtilCommon.ShakeByBuildingDelegate o = LuaHelper.ShakeByBuildingDelegate(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}
}

