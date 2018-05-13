using System;
using UnityEngine;
using LuaInterface;

public class PoolManagerWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("PreLoad", PreLoad),
			new LuaMethod("CreatePool", CreatePool),
			new LuaMethod("Spawn", Spawn),
			new LuaMethod("DeSpawn", DeSpawn),
			new LuaMethod("Clear", Clear),
			new LuaMethod("New", _CreatePoolManager),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("_parentTransform", get__parentTransform, set__parentTransform),
			new LuaField("Instance", get_Instance, null),
		};

		LuaScriptMgr.RegisterLib(L, "PoolManager", typeof(PoolManager), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreatePoolManager(IntPtr L)
	{
		LuaDLL.luaL_error(L, "PoolManager class does not have a constructor function");
		return 0;
	}

	static Type classType = typeof(PoolManager);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get__parentTransform(IntPtr L)
	{
		LuaScriptMgr.Push(L, PoolManager._parentTransform);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Instance(IntPtr L)
	{
		LuaScriptMgr.PushObject(L, PoolManager.Instance);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set__parentTransform(IntPtr L)
	{
		PoolManager._parentTransform = (Transform)LuaScriptMgr.GetUnityObject(L, 3, typeof(Transform));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int PreLoad(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		PoolManager obj = (PoolManager)LuaScriptMgr.GetNetObjectSelf(L, 1, "PoolManager");
		obj.PreLoad();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CreatePool(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		PoolManager obj = (PoolManager)LuaScriptMgr.GetNetObjectSelf(L, 1, "PoolManager");
		string arg0 = LuaScriptMgr.GetLuaString(L, 2);
		int arg1 = (int)LuaScriptMgr.GetNumber(L, 3);
		obj.CreatePool(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Spawn(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		PoolManager obj = (PoolManager)LuaScriptMgr.GetNetObjectSelf(L, 1, "PoolManager");
		string arg0 = LuaScriptMgr.GetLuaString(L, 2);
		GameObject o = obj.Spawn(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int DeSpawn(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		PoolManager obj = (PoolManager)LuaScriptMgr.GetNetObjectSelf(L, 1, "PoolManager");
		GameObject arg0 = (GameObject)LuaScriptMgr.GetUnityObject(L, 2, typeof(GameObject));
		obj.DeSpawn(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Clear(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		PoolManager obj = (PoolManager)LuaScriptMgr.GetNetObjectSelf(L, 1, "PoolManager");
		obj.Clear();
		return 0;
	}
}

