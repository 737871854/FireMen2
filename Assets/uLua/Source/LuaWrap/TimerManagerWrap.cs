using System;
using UnityEngine;
using LuaInterface;
using Object = UnityEngine.Object;

public class TimerManagerWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("StartTimer", StartTimer),
			new LuaMethod("StopTimer", StopTimer),
			new LuaMethod("AddTimerEvent", AddTimerEvent),
			new LuaMethod("RemoveTimerEvent", RemoveTimerEvent),
			new LuaMethod("PauseTimerEvent", PauseTimerEvent),
			new LuaMethod("ResumeTimerEvent", ResumeTimerEvent),
			new LuaMethod("GetTimerObject", GetTimerObject),
			new LuaMethod("New", _CreateTimerManager),
			new LuaMethod("GetClassType", GetClassType),
			new LuaMethod("__eq", Lua_Eq),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("Interval", get_Interval, set_Interval),
		};

		LuaScriptMgr.RegisterLib(L, "TimerManager", typeof(TimerManager), regs, fields, typeof(SingletonBehaviour<TimerManager>));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateTimerManager(IntPtr L)
	{
		LuaDLL.luaL_error(L, "TimerManager class does not have a constructor function");
		return 0;
	}

	static Type classType = typeof(TimerManager);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Interval(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		TimerManager obj = (TimerManager)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name Interval");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index Interval on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.Interval);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_Interval(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		TimerManager obj = (TimerManager)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name Interval");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index Interval on a nil value");
			}
		}

		obj.Interval = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int StartTimer(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		TimerManager obj = (TimerManager)LuaScriptMgr.GetUnityObjectSelf(L, 1, "TimerManager");
		float arg0 = (float)LuaScriptMgr.GetNumber(L, 2);
		obj.StartTimer(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int StopTimer(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		TimerManager obj = (TimerManager)LuaScriptMgr.GetUnityObjectSelf(L, 1, "TimerManager");
		obj.StopTimer();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int AddTimerEvent(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2)
		{
			TimerManager obj = (TimerManager)LuaScriptMgr.GetUnityObjectSelf(L, 1, "TimerManager");
			TimerObject arg0 = (TimerObject)LuaScriptMgr.GetNetObject(L, 2, typeof(TimerObject));
			obj.AddTimerEvent(arg0);
			return 0;
		}
		else if (count == 3)
		{
			TimerManager obj = (TimerManager)LuaScriptMgr.GetUnityObjectSelf(L, 1, "TimerManager");
			int arg0 = (int)LuaScriptMgr.GetNumber(L, 2);
			TimerTriggerCallback arg1 = null;
			LuaTypes funcType3 = LuaDLL.lua_type(L, 3);

			if (funcType3 != LuaTypes.LUA_TFUNCTION)
			{
				 arg1 = (TimerTriggerCallback)LuaScriptMgr.GetNetObject(L, 3, typeof(TimerTriggerCallback));
			}
			else
			{
				LuaFunction func = LuaScriptMgr.GetLuaFunction(L, 3);
				arg1 = (param0) =>
				{
					int top = func.BeginPCall();
					LuaScriptMgr.PushObject(L, param0);
					func.PCall(top, 1);
					func.EndPCall(top);
				};
			}

			int o = obj.AddTimerEvent(arg0,arg1);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 6)
		{
			TimerManager obj = (TimerManager)LuaScriptMgr.GetUnityObjectSelf(L, 1, "TimerManager");
			int arg0 = (int)LuaScriptMgr.GetNumber(L, 2);
			int arg1 = (int)LuaScriptMgr.GetNumber(L, 3);
			int arg2 = (int)LuaScriptMgr.GetNumber(L, 4);
			TimerTriggerCallback arg3 = null;
			LuaTypes funcType5 = LuaDLL.lua_type(L, 5);

			if (funcType5 != LuaTypes.LUA_TFUNCTION)
			{
				 arg3 = (TimerTriggerCallback)LuaScriptMgr.GetNetObject(L, 5, typeof(TimerTriggerCallback));
			}
			else
			{
				LuaFunction func = LuaScriptMgr.GetLuaFunction(L, 5);
				arg3 = (param0) =>
				{
					int top = func.BeginPCall();
					LuaScriptMgr.PushObject(L, param0);
					func.PCall(top, 1);
					func.EndPCall(top);
				};
			}

			bool arg4 = LuaScriptMgr.GetBoolean(L, 6);
			int o = obj.AddTimerEvent(arg0,arg1,arg2,arg3,arg4);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: TimerManager.AddTimerEvent");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int RemoveTimerEvent(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(TimerManager), typeof(int)))
		{
			TimerManager obj = (TimerManager)LuaScriptMgr.GetUnityObjectSelf(L, 1, "TimerManager");
			int arg0 = (int)LuaDLL.lua_tonumber(L, 2);
			TimerObject o = obj.RemoveTimerEvent(arg0);
			LuaScriptMgr.PushObject(L, o);
			return 1;
		}
		else if (count == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(TimerManager), typeof(TimerObject)))
		{
			TimerManager obj = (TimerManager)LuaScriptMgr.GetUnityObjectSelf(L, 1, "TimerManager");
			TimerObject arg0 = (TimerObject)LuaScriptMgr.GetLuaObject(L, 2);
			obj.RemoveTimerEvent(arg0);
			return 0;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: TimerManager.RemoveTimerEvent");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int PauseTimerEvent(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(TimerManager), typeof(TimerObject)))
		{
			TimerManager obj = (TimerManager)LuaScriptMgr.GetUnityObjectSelf(L, 1, "TimerManager");
			TimerObject arg0 = (TimerObject)LuaScriptMgr.GetLuaObject(L, 2);
			obj.PauseTimerEvent(arg0);
			return 0;
		}
		else if (count == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(TimerManager), typeof(int)))
		{
			TimerManager obj = (TimerManager)LuaScriptMgr.GetUnityObjectSelf(L, 1, "TimerManager");
			int arg0 = (int)LuaDLL.lua_tonumber(L, 2);
			obj.PauseTimerEvent(arg0);
			return 0;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: TimerManager.PauseTimerEvent");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ResumeTimerEvent(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(TimerManager), typeof(TimerObject)))
		{
			TimerManager obj = (TimerManager)LuaScriptMgr.GetUnityObjectSelf(L, 1, "TimerManager");
			TimerObject arg0 = (TimerObject)LuaScriptMgr.GetLuaObject(L, 2);
			obj.ResumeTimerEvent(arg0);
			return 0;
		}
		else if (count == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(TimerManager), typeof(int)))
		{
			TimerManager obj = (TimerManager)LuaScriptMgr.GetUnityObjectSelf(L, 1, "TimerManager");
			int arg0 = (int)LuaDLL.lua_tonumber(L, 2);
			obj.ResumeTimerEvent(arg0);
			return 0;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: TimerManager.ResumeTimerEvent");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetTimerObject(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		TimerManager obj = (TimerManager)LuaScriptMgr.GetUnityObjectSelf(L, 1, "TimerManager");
		int arg0 = (int)LuaScriptMgr.GetNumber(L, 2);
		TimerObject o = obj.GetTimerObject(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
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

