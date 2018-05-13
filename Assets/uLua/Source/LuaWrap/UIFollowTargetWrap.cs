using System;
using UnityEngine;
using LuaInterface;
using Object = UnityEngine.Object;

public class UIFollowTargetWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("SetChildsVisible", SetChildsVisible),
			new LuaMethod("New", _CreateUIFollowTarget),
			new LuaMethod("GetClassType", GetClassType),
			new LuaMethod("__eq", Lua_Eq),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("gameCamera", get_gameCamera, set_gameCamera),
			new LuaField("uiCamera", get_uiCamera, set_uiCamera),
			new LuaField("dist", get_dist, set_dist),
			new LuaField("targetTran", get_targetTran, set_targetTran),
			new LuaField("scaleMyself", get_scaleMyself, set_scaleMyself),
			new LuaField("wPercent", get_wPercent, set_wPercent),
			new LuaField("hPercent", get_hPercent, set_hPercent),
		};

		LuaScriptMgr.RegisterLib(L, "UIFollowTarget", typeof(UIFollowTarget), regs, fields, typeof(MonoBehaviour));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateUIFollowTarget(IntPtr L)
	{
		LuaDLL.luaL_error(L, "UIFollowTarget class does not have a constructor function");
		return 0;
	}

	static Type classType = typeof(UIFollowTarget);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_gameCamera(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIFollowTarget obj = (UIFollowTarget)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name gameCamera");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index gameCamera on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.gameCamera);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_uiCamera(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIFollowTarget obj = (UIFollowTarget)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name uiCamera");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index uiCamera on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.uiCamera);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_dist(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIFollowTarget obj = (UIFollowTarget)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name dist");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index dist on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.dist);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_targetTran(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIFollowTarget obj = (UIFollowTarget)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name targetTran");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index targetTran on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.targetTran);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_scaleMyself(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIFollowTarget obj = (UIFollowTarget)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name scaleMyself");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index scaleMyself on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.scaleMyself);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_wPercent(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIFollowTarget obj = (UIFollowTarget)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name wPercent");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index wPercent on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.wPercent);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_hPercent(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIFollowTarget obj = (UIFollowTarget)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name hPercent");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index hPercent on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.hPercent);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_gameCamera(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIFollowTarget obj = (UIFollowTarget)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name gameCamera");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index gameCamera on a nil value");
			}
		}

		obj.gameCamera = (Camera)LuaScriptMgr.GetUnityObject(L, 3, typeof(Camera));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_uiCamera(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIFollowTarget obj = (UIFollowTarget)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name uiCamera");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index uiCamera on a nil value");
			}
		}

		obj.uiCamera = (Camera)LuaScriptMgr.GetUnityObject(L, 3, typeof(Camera));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_dist(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIFollowTarget obj = (UIFollowTarget)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name dist");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index dist on a nil value");
			}
		}

		obj.dist = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_targetTran(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIFollowTarget obj = (UIFollowTarget)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name targetTran");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index targetTran on a nil value");
			}
		}

		obj.targetTran = (Transform)LuaScriptMgr.GetUnityObject(L, 3, typeof(Transform));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_scaleMyself(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIFollowTarget obj = (UIFollowTarget)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name scaleMyself");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index scaleMyself on a nil value");
			}
		}

		obj.scaleMyself = LuaScriptMgr.GetVector3(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_wPercent(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIFollowTarget obj = (UIFollowTarget)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name wPercent");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index wPercent on a nil value");
			}
		}

		obj.wPercent = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_hPercent(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIFollowTarget obj = (UIFollowTarget)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name hPercent");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index hPercent on a nil value");
			}
		}

		obj.hPercent = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetChildsVisible(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		UIFollowTarget obj = (UIFollowTarget)LuaScriptMgr.GetUnityObjectSelf(L, 1, "UIFollowTarget");
		bool arg0 = LuaScriptMgr.GetBoolean(L, 2);
		obj.SetChildsVisible(arg0);
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

