using System;
using LuaInterface;
using DG.Tweening;

public class DG_Tweening_SequenceWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("InsertCallback", InsertCallback),
			new LuaMethod("PrependCallback", PrependCallback),
			new LuaMethod("AppendCallback", AppendCallback),
			new LuaMethod("PrependInterval", PrependInterval),
			new LuaMethod("AppendInterval", AppendInterval),
			new LuaMethod("Insert", Insert),
			new LuaMethod("Join", Join),
			new LuaMethod("Prepend", Prepend),
			new LuaMethod("Append", Append),
			new LuaMethod("New", _CreateDG_Tweening_Sequence),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
		};

		LuaScriptMgr.RegisterLib(L, "DG.Tweening.Sequence", typeof(DG.Tweening.Sequence), regs, fields, typeof(DG.Tweening.Tween));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateDG_Tweening_Sequence(IntPtr L)
	{
		LuaDLL.luaL_error(L, "DG.Tweening.Sequence class does not have a constructor function");
		return 0;
	}

	static Type classType = typeof(DG.Tweening.Sequence);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int InsertCallback(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		DG.Tweening.Sequence obj = (DG.Tweening.Sequence)LuaScriptMgr.GetNetObjectSelf(L, 1, "DG.Tweening.Sequence");
		float arg0 = (float)LuaScriptMgr.GetNumber(L, 2);
		DG.Tweening.TweenCallback arg1 = null;
		LuaTypes funcType3 = LuaDLL.lua_type(L, 3);

		if (funcType3 != LuaTypes.LUA_TFUNCTION)
		{
			 arg1 = (DG.Tweening.TweenCallback)LuaScriptMgr.GetNetObject(L, 3, typeof(DG.Tweening.TweenCallback));
		}
		else
		{
			LuaFunction func = LuaScriptMgr.GetLuaFunction(L, 3);
			arg1 = () =>
			{
				func.Call();
			};
		}

		DG.Tweening.Sequence o = obj.InsertCallback(arg0,arg1);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int PrependCallback(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		DG.Tweening.Sequence obj = (DG.Tweening.Sequence)LuaScriptMgr.GetNetObjectSelf(L, 1, "DG.Tweening.Sequence");
		DG.Tweening.TweenCallback arg0 = null;
		LuaTypes funcType2 = LuaDLL.lua_type(L, 2);

		if (funcType2 != LuaTypes.LUA_TFUNCTION)
		{
			 arg0 = (DG.Tweening.TweenCallback)LuaScriptMgr.GetNetObject(L, 2, typeof(DG.Tweening.TweenCallback));
		}
		else
		{
			LuaFunction func = LuaScriptMgr.GetLuaFunction(L, 2);
			arg0 = () =>
			{
				func.Call();
			};
		}

		DG.Tweening.Sequence o = obj.PrependCallback(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int AppendCallback(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		DG.Tweening.Sequence obj = (DG.Tweening.Sequence)LuaScriptMgr.GetNetObjectSelf(L, 1, "DG.Tweening.Sequence");
		DG.Tweening.TweenCallback arg0 = null;
		LuaTypes funcType2 = LuaDLL.lua_type(L, 2);

		if (funcType2 != LuaTypes.LUA_TFUNCTION)
		{
			 arg0 = (DG.Tweening.TweenCallback)LuaScriptMgr.GetNetObject(L, 2, typeof(DG.Tweening.TweenCallback));
		}
		else
		{
			LuaFunction func = LuaScriptMgr.GetLuaFunction(L, 2);
			arg0 = () =>
			{
				func.Call();
			};
		}

		DG.Tweening.Sequence o = obj.AppendCallback(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int PrependInterval(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		DG.Tweening.Sequence obj = (DG.Tweening.Sequence)LuaScriptMgr.GetNetObjectSelf(L, 1, "DG.Tweening.Sequence");
		float arg0 = (float)LuaScriptMgr.GetNumber(L, 2);
		DG.Tweening.Sequence o = obj.PrependInterval(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int AppendInterval(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		DG.Tweening.Sequence obj = (DG.Tweening.Sequence)LuaScriptMgr.GetNetObjectSelf(L, 1, "DG.Tweening.Sequence");
		float arg0 = (float)LuaScriptMgr.GetNumber(L, 2);
		DG.Tweening.Sequence o = obj.AppendInterval(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Insert(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		DG.Tweening.Sequence obj = (DG.Tweening.Sequence)LuaScriptMgr.GetNetObjectSelf(L, 1, "DG.Tweening.Sequence");
		float arg0 = (float)LuaScriptMgr.GetNumber(L, 2);
		DG.Tweening.Tween arg1 = (DG.Tweening.Tween)LuaScriptMgr.GetNetObject(L, 3, typeof(DG.Tweening.Tween));
		DG.Tweening.Sequence o = obj.Insert(arg0,arg1);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Join(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		DG.Tweening.Sequence obj = (DG.Tweening.Sequence)LuaScriptMgr.GetNetObjectSelf(L, 1, "DG.Tweening.Sequence");
		DG.Tweening.Tween arg0 = (DG.Tweening.Tween)LuaScriptMgr.GetNetObject(L, 2, typeof(DG.Tweening.Tween));
		DG.Tweening.Sequence o = obj.Join(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Prepend(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		DG.Tweening.Sequence obj = (DG.Tweening.Sequence)LuaScriptMgr.GetNetObjectSelf(L, 1, "DG.Tweening.Sequence");
		DG.Tweening.Tween arg0 = (DG.Tweening.Tween)LuaScriptMgr.GetNetObject(L, 2, typeof(DG.Tweening.Tween));
		DG.Tweening.Sequence o = obj.Prepend(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Append(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		DG.Tweening.Sequence obj = (DG.Tweening.Sequence)LuaScriptMgr.GetNetObjectSelf(L, 1, "DG.Tweening.Sequence");
		DG.Tweening.Tween arg0 = (DG.Tweening.Tween)LuaScriptMgr.GetNetObject(L, 2, typeof(DG.Tweening.Tween));
		DG.Tweening.Sequence o = obj.Append(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}
}

