using System;
using LuaInterface;

public class DG_Tweening_RotateModeWrap
{
	static LuaMethod[] enums = new LuaMethod[]
	{
		new LuaMethod("Fast", GetFast),
		new LuaMethod("FastBeyond360", GetFastBeyond360),
		new LuaMethod("WorldAxisAdd", GetWorldAxisAdd),
		new LuaMethod("LocalAxisAdd", GetLocalAxisAdd),
		new LuaMethod("IntToEnum", IntToEnum),
	};

	public static void Register(IntPtr L)
	{
		LuaScriptMgr.RegisterLib(L, "DG.Tweening.RotateMode", typeof(DG.Tweening.RotateMode), enums);
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetFast(IntPtr L)
	{
		LuaScriptMgr.Push(L, DG.Tweening.RotateMode.Fast);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetFastBeyond360(IntPtr L)
	{
		LuaScriptMgr.Push(L, DG.Tweening.RotateMode.FastBeyond360);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetWorldAxisAdd(IntPtr L)
	{
		LuaScriptMgr.Push(L, DG.Tweening.RotateMode.WorldAxisAdd);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetLocalAxisAdd(IntPtr L)
	{
		LuaScriptMgr.Push(L, DG.Tweening.RotateMode.LocalAxisAdd);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IntToEnum(IntPtr L)
	{
		int arg0 = (int)LuaDLL.lua_tonumber(L, 1);
		DG.Tweening.RotateMode o = (DG.Tweening.RotateMode)arg0;
		LuaScriptMgr.Push(L, o);
		return 1;
	}
}

