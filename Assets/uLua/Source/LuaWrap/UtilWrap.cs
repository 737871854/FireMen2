using System;
using UnityEngine;
using LuaInterface;

public class UtilWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("PushBufferToLua", PushBufferToLua),
			new LuaMethod("CreateMat", CreateMat),
			new LuaMethod("ShaderFind", ShaderFind),
			new LuaMethod("SetFloat", SetFloat),
			new LuaMethod("SetInit", SetInit),
			new LuaMethod("SetColor", SetColor),
			new LuaMethod("GetLocalIPAddress", GetLocalIPAddress),
			new LuaMethod("FindTransformByName", FindTransformByName),
			new LuaMethod("TEXT", TEXT),
			new LuaMethod("ScreenPointToLocalPointInRectangle", ScreenPointToLocalPointInRectangle),
			new LuaMethod("New", _CreateUtil),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
		};

		LuaScriptMgr.RegisterLib(L, "Util", typeof(Util), regs, fields, typeof(UtilCommon));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateUtil(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			Util obj = new Util();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: Util.New");
		}

		return 0;
	}

	static Type classType = typeof(Util);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int PushBufferToLua(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		LuaFunction arg0 = LuaScriptMgr.GetLuaFunction(L, 1);
		byte[] objs1 = LuaScriptMgr.GetArrayNumber<byte>(L, 2);
		Util.PushBufferToLua(arg0,objs1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CreateMat(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 1 && LuaScriptMgr.CheckTypes(L, 1, typeof(Material)))
		{
			Material arg0 = (Material)LuaScriptMgr.GetLuaObject(L, 1);
			Material o = Util.CreateMat(arg0);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 1 && LuaScriptMgr.CheckTypes(L, 1, typeof(string)))
		{
			string arg0 = LuaScriptMgr.GetString(L, 1);
			Material o = Util.CreateMat(arg0);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: Util.CreateMat");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ShaderFind(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		string arg0 = LuaScriptMgr.GetLuaString(L, 1);
		Shader o = Util.ShaderFind(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetFloat(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		Material arg0 = (Material)LuaScriptMgr.GetUnityObject(L, 1, typeof(Material));
		string arg1 = LuaScriptMgr.GetLuaString(L, 2);
		float arg2 = (float)LuaScriptMgr.GetNumber(L, 3);
		Util.SetFloat(arg0,arg1,arg2);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetInit(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		Material arg0 = (Material)LuaScriptMgr.GetUnityObject(L, 1, typeof(Material));
		string arg1 = LuaScriptMgr.GetLuaString(L, 2);
		int arg2 = (int)LuaScriptMgr.GetNumber(L, 3);
		Util.SetInit(arg0,arg1,arg2);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetColor(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		Material arg0 = (Material)LuaScriptMgr.GetUnityObject(L, 1, typeof(Material));
		string arg1 = LuaScriptMgr.GetLuaString(L, 2);
		Color arg2 = LuaScriptMgr.GetColor(L, 3);
		Util.SetColor(arg0,arg1,arg2);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetLocalIPAddress(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 0);
		string o = Util.GetLocalIPAddress();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int FindTransformByName(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		Transform arg0 = (Transform)LuaScriptMgr.GetUnityObject(L, 1, typeof(Transform));
		string arg1 = LuaScriptMgr.GetLuaString(L, 2);
		Transform o = Util.FindTransformByName(arg0,arg1);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int TEXT(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		string arg0 = LuaScriptMgr.GetLuaString(L, 1);
		string o = Util.TEXT(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ScreenPointToLocalPointInRectangle(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		RectTransform arg0 = (RectTransform)LuaScriptMgr.GetUnityObject(L, 1, typeof(RectTransform));
		Vector2 arg1 = LuaScriptMgr.GetVector2(L, 2);
		Camera arg2 = (Camera)LuaScriptMgr.GetUnityObject(L, 3, typeof(Camera));
		Vector2 o = Util.ScreenPointToLocalPointInRectangle(arg0,arg1,arg2);
		LuaScriptMgr.Push(L, o);
		return 1;
	}
}

