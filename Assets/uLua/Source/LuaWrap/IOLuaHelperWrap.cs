using System;
using LuaInterface;

public class IOLuaHelperWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("RegesterListener", RegesterListener),
			new LuaMethod("RemoveListener", RemoveListener),
			new LuaMethod("TriggerListener", TriggerListener),
			new LuaMethod("New", _CreateIOLuaHelper),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("Instance", get_Instance, null),
		};

		LuaScriptMgr.RegisterLib(L, "IOLuaHelper", typeof(IOLuaHelper), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateIOLuaHelper(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			IOLuaHelper obj = new IOLuaHelper();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: IOLuaHelper.New");
		}

		return 0;
	}

	static Type classType = typeof(IOLuaHelper);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Instance(IntPtr L)
	{
		LuaScriptMgr.PushObject(L, IOLuaHelper.Instance);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int RegesterListener(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 4 && LuaScriptMgr.CheckTypes(L, 1, typeof(IOLuaHelper), typeof(int), typeof(UtilCommon.OnIOEventHandle), typeof(string)))
		{
			IOLuaHelper obj = (IOLuaHelper)LuaScriptMgr.GetNetObjectSelf(L, 1, "IOLuaHelper");
			int arg0 = (int)LuaDLL.lua_tonumber(L, 2);
			UtilCommon.OnIOEventHandle arg1 = null;
			LuaTypes funcType3 = LuaDLL.lua_type(L, 3);

			if (funcType3 != LuaTypes.LUA_TFUNCTION)
			{
				 arg1 = (UtilCommon.OnIOEventHandle)LuaScriptMgr.GetLuaObject(L, 3);
			}
			else
			{
				LuaFunction func = LuaScriptMgr.GetLuaFunction(L, 3);
				arg1 = (param0, param1) =>
				{
					int top = func.BeginPCall();
					LuaScriptMgr.Push(L, param0);
					LuaScriptMgr.Push(L, param1);
					func.PCall(top, 2);
					func.EndPCall(top);
				};
			}

			string arg2 = LuaScriptMgr.GetString(L, 4);
			obj.RegesterListener(arg0,arg1,arg2);
			return 0;
		}
		else if (count == 4 && LuaScriptMgr.CheckTypes(L, 1, typeof(IOLuaHelper), typeof(EnumIOEvent), typeof(UtilCommon.OnIOEventHandle), typeof(string)))
		{
			IOLuaHelper obj = (IOLuaHelper)LuaScriptMgr.GetNetObjectSelf(L, 1, "IOLuaHelper");
			EnumIOEvent arg0 = (EnumIOEvent)LuaScriptMgr.GetLuaObject(L, 2);
			UtilCommon.OnIOEventHandle arg1 = null;
			LuaTypes funcType3 = LuaDLL.lua_type(L, 3);

			if (funcType3 != LuaTypes.LUA_TFUNCTION)
			{
				 arg1 = (UtilCommon.OnIOEventHandle)LuaScriptMgr.GetLuaObject(L, 3);
			}
			else
			{
				LuaFunction func = LuaScriptMgr.GetLuaFunction(L, 3);
				arg1 = (param0, param1) =>
				{
					int top = func.BeginPCall();
					LuaScriptMgr.Push(L, param0);
					LuaScriptMgr.Push(L, param1);
					func.PCall(top, 2);
					func.EndPCall(top);
				};
			}

			string arg2 = LuaScriptMgr.GetString(L, 4);
			obj.RegesterListener(arg0,arg1,arg2);
			return 0;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: IOLuaHelper.RegesterListener");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int RemoveListener(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 3 && LuaScriptMgr.CheckTypes(L, 1, typeof(IOLuaHelper), typeof(int), typeof(string)))
		{
			IOLuaHelper obj = (IOLuaHelper)LuaScriptMgr.GetNetObjectSelf(L, 1, "IOLuaHelper");
			int arg0 = (int)LuaDLL.lua_tonumber(L, 2);
			string arg1 = LuaScriptMgr.GetString(L, 3);
			obj.RemoveListener(arg0,arg1);
			return 0;
		}
		else if (count == 3 && LuaScriptMgr.CheckTypes(L, 1, typeof(IOLuaHelper), typeof(EnumIOEvent), typeof(string)))
		{
			IOLuaHelper obj = (IOLuaHelper)LuaScriptMgr.GetNetObjectSelf(L, 1, "IOLuaHelper");
			EnumIOEvent arg0 = (EnumIOEvent)LuaScriptMgr.GetLuaObject(L, 2);
			string arg1 = LuaScriptMgr.GetString(L, 3);
			obj.RemoveListener(arg0,arg1);
			return 0;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: IOLuaHelper.RemoveListener");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int TriggerListener(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 4 && LuaScriptMgr.CheckTypes(L, 1, typeof(IOLuaHelper), typeof(int), typeof(int), typeof(bool)))
		{
			IOLuaHelper obj = (IOLuaHelper)LuaScriptMgr.GetNetObjectSelf(L, 1, "IOLuaHelper");
			int arg0 = (int)LuaDLL.lua_tonumber(L, 2);
			int arg1 = (int)LuaDLL.lua_tonumber(L, 3);
			bool arg2 = LuaDLL.lua_toboolean(L, 4);
			obj.TriggerListener(arg0,arg1,arg2);
			return 0;
		}
		else if (count == 4 && LuaScriptMgr.CheckTypes(L, 1, typeof(IOLuaHelper), typeof(EnumIOEvent), typeof(int), typeof(bool)))
		{
			IOLuaHelper obj = (IOLuaHelper)LuaScriptMgr.GetNetObjectSelf(L, 1, "IOLuaHelper");
			EnumIOEvent arg0 = (EnumIOEvent)LuaScriptMgr.GetLuaObject(L, 2);
			int arg1 = (int)LuaDLL.lua_tonumber(L, 3);
			bool arg2 = LuaDLL.lua_toboolean(L, 4);
			obj.TriggerListener(arg0,arg1,arg2);
			return 0;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: IOLuaHelper.TriggerListener");
		}

		return 0;
	}
}

