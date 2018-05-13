using System;
using UnityEngine;
using LuaInterface;
using Object = UnityEngine.Object;

public class UIProgressWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("InitUIProgress", InitUIProgress),
			new LuaMethod("New", _CreateUIProgress),
			new LuaMethod("GetClassType", GetClassType),
			new LuaMethod("__eq", Lua_Eq),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("TotalTime", get_TotalTime, set_TotalTime),
			new LuaField("IsFinish", get_IsFinish, null),
			new LuaField("CallBack", null, set_CallBack),
		};

		LuaScriptMgr.RegisterLib(L, "UIProgress", typeof(UIProgress), regs, fields, typeof(MonoBehaviour));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateUIProgress(IntPtr L)
	{
		LuaDLL.luaL_error(L, "UIProgress class does not have a constructor function");
		return 0;
	}

	static Type classType = typeof(UIProgress);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_TotalTime(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIProgress obj = (UIProgress)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name TotalTime");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index TotalTime on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.TotalTime);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_IsFinish(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIProgress obj = (UIProgress)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name IsFinish");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index IsFinish on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.IsFinish);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_TotalTime(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIProgress obj = (UIProgress)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name TotalTime");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index TotalTime on a nil value");
			}
		}

		obj.TotalTime = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_CallBack(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIProgress obj = (UIProgress)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name CallBack");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index CallBack on a nil value");
			}
		}

		LuaTypes funcType = LuaDLL.lua_type(L, 3);

		if (funcType != LuaTypes.LUA_TFUNCTION)
		{
			obj.CallBack = (UtilCommon.VoidDelegate)LuaScriptMgr.GetNetObject(L, 3, typeof(UtilCommon.VoidDelegate));
		}
		else
		{
			LuaFunction func = LuaScriptMgr.ToLuaFunction(L, 3);
			obj.CallBack = (param0, param1) =>
			{
				int top = func.BeginPCall();
				LuaScriptMgr.Push(L, param0);
				LuaScriptMgr.PushObject(L, param1);
				func.PCall(top, 2);
				func.EndPCall(top);
			};
		}
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int InitUIProgress(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		UIProgress obj = (UIProgress)LuaScriptMgr.GetUnityObjectSelf(L, 1, "UIProgress");
		float arg0 = (float)LuaScriptMgr.GetNumber(L, 2);
		float arg1 = (float)LuaScriptMgr.GetNumber(L, 3);
		obj.InitUIProgress(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Lua_Eq(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		Object arg0 = LuaScriptMgr.GetLuaObject(L, 1) as Object;
		Object arg1 = LuaScriptMgr.GetLuaObject(L, 2) as Object;
		bool o = arg0 == arg1;
		LuaScriptMgr.Push(L, o);
		return 1;
	}
}

