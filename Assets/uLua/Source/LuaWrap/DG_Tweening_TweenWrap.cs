using System;
using UnityEngine;
using LuaInterface;
using DG.Tweening;

public class DG_Tweening_TweenWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("PathLength", PathLength),
			new LuaMethod("PathGetPoint", PathGetPoint),
			new LuaMethod("IsPlaying", IsPlaying),
			new LuaMethod("IsInitialized", IsInitialized),
			new LuaMethod("IsComplete", IsComplete),
			new LuaMethod("IsBackwards", IsBackwards),
			new LuaMethod("IsActive", IsActive),
			new LuaMethod("ElapsedDirectionalPercentage", ElapsedDirectionalPercentage),
			new LuaMethod("ElapsedPercentage", ElapsedPercentage),
			new LuaMethod("Elapsed", Elapsed),
			new LuaMethod("Duration", Duration),
			new LuaMethod("Delay", Delay),
			new LuaMethod("CompletedLoops", CompletedLoops),
			new LuaMethod("WaitForStart", WaitForStart),
			new LuaMethod("WaitForPosition", WaitForPosition),
			new LuaMethod("WaitForElapsedLoops", WaitForElapsedLoops),
			new LuaMethod("WaitForKill", WaitForKill),
			new LuaMethod("WaitForRewind", WaitForRewind),
			new LuaMethod("WaitForCompletion", WaitForCompletion),
			new LuaMethod("GotoWaypoint", GotoWaypoint),
			new LuaMethod("TogglePause", TogglePause),
			new LuaMethod("Rewind", Rewind),
			new LuaMethod("Restart", Restart),
			new LuaMethod("PlayForward", PlayForward),
			new LuaMethod("PlayBackwards", PlayBackwards),
			new LuaMethod("Play", Play),
			new LuaMethod("Pause", Pause),
			new LuaMethod("Kill", Kill),
			new LuaMethod("Goto", Goto),
			new LuaMethod("ForceInit", ForceInit),
			new LuaMethod("Flip", Flip),
			new LuaMethod("Complete", Complete),
			new LuaMethod("SetSpeedBased", SetSpeedBased),
			new LuaMethod("SetRelative", SetRelative),
			new LuaMethod("SetDelay", SetDelay),
			new LuaMethod("SetAs", SetAs),
			new LuaMethod("OnWaypointChange", OnWaypointChange),
			new LuaMethod("OnKill", OnKill),
			new LuaMethod("OnComplete", OnComplete),
			new LuaMethod("OnStepComplete", OnStepComplete),
			new LuaMethod("OnUpdate", OnUpdate),
			new LuaMethod("OnRewind", OnRewind),
			new LuaMethod("OnPause", OnPause),
			new LuaMethod("OnPlay", OnPlay),
			new LuaMethod("OnStart", OnStart),
			new LuaMethod("SetUpdate", SetUpdate),
			new LuaMethod("SetRecyclable", SetRecyclable),
			new LuaMethod("SetEase", SetEase),
			new LuaMethod("SetLoops", SetLoops),
			new LuaMethod("SetTarget", SetTarget),
			new LuaMethod("SetId", SetId),
			new LuaMethod("SetAutoKill", SetAutoKill),
			new LuaMethod("New", _CreateDG_Tweening_Tween),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("timeScale", get_timeScale, set_timeScale),
			new LuaField("isBackwards", get_isBackwards, set_isBackwards),
			new LuaField("id", get_id, set_id),
			new LuaField("target", get_target, set_target),
			new LuaField("easeOvershootOrAmplitude", get_easeOvershootOrAmplitude, set_easeOvershootOrAmplitude),
			new LuaField("easePeriod", get_easePeriod, set_easePeriod),
			new LuaField("fullPosition", get_fullPosition, set_fullPosition),
		};

		LuaScriptMgr.RegisterLib(L, "DG.Tweening.Tween", typeof(DG.Tweening.Tween), regs, fields, typeof(DG.Tweening.Core.ABSSequentiable));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateDG_Tweening_Tween(IntPtr L)
	{
		LuaDLL.luaL_error(L, "DG.Tweening.Tween class does not have a constructor function");
		return 0;
	}

	static Type classType = typeof(DG.Tweening.Tween);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_timeScale(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		DG.Tweening.Tween obj = (DG.Tweening.Tween)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name timeScale");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index timeScale on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.timeScale);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_isBackwards(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		DG.Tweening.Tween obj = (DG.Tweening.Tween)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name isBackwards");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index isBackwards on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.isBackwards);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		DG.Tweening.Tween obj = (DG.Tweening.Tween)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name id");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index id on a nil value");
			}
		}

		LuaScriptMgr.PushVarObject(L, obj.id);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_target(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		DG.Tweening.Tween obj = (DG.Tweening.Tween)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name target");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index target on a nil value");
			}
		}

		LuaScriptMgr.PushVarObject(L, obj.target);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_easeOvershootOrAmplitude(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		DG.Tweening.Tween obj = (DG.Tweening.Tween)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name easeOvershootOrAmplitude");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index easeOvershootOrAmplitude on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.easeOvershootOrAmplitude);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_easePeriod(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		DG.Tweening.Tween obj = (DG.Tweening.Tween)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name easePeriod");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index easePeriod on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.easePeriod);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_fullPosition(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		DG.Tweening.Tween obj = (DG.Tweening.Tween)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name fullPosition");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index fullPosition on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.fullPosition);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_timeScale(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		DG.Tweening.Tween obj = (DG.Tweening.Tween)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name timeScale");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index timeScale on a nil value");
			}
		}

		obj.timeScale = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_isBackwards(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		DG.Tweening.Tween obj = (DG.Tweening.Tween)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name isBackwards");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index isBackwards on a nil value");
			}
		}

		obj.isBackwards = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		DG.Tweening.Tween obj = (DG.Tweening.Tween)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name id");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index id on a nil value");
			}
		}

		obj.id = LuaScriptMgr.GetVarObject(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_target(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		DG.Tweening.Tween obj = (DG.Tweening.Tween)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name target");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index target on a nil value");
			}
		}

		obj.target = LuaScriptMgr.GetVarObject(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_easeOvershootOrAmplitude(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		DG.Tweening.Tween obj = (DG.Tweening.Tween)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name easeOvershootOrAmplitude");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index easeOvershootOrAmplitude on a nil value");
			}
		}

		obj.easeOvershootOrAmplitude = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_easePeriod(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		DG.Tweening.Tween obj = (DG.Tweening.Tween)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name easePeriod");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index easePeriod on a nil value");
			}
		}

		obj.easePeriod = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_fullPosition(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		DG.Tweening.Tween obj = (DG.Tweening.Tween)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name fullPosition");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index fullPosition on a nil value");
			}
		}

		obj.fullPosition = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int PathLength(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		DG.Tweening.Tween obj = (DG.Tweening.Tween)LuaScriptMgr.GetNetObjectSelf(L, 1, "DG.Tweening.Tween");
		float o = obj.PathLength();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int PathGetPoint(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		DG.Tweening.Tween obj = (DG.Tweening.Tween)LuaScriptMgr.GetNetObjectSelf(L, 1, "DG.Tweening.Tween");
		float arg0 = (float)LuaScriptMgr.GetNumber(L, 2);
		Vector3 o = obj.PathGetPoint(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IsPlaying(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		DG.Tweening.Tween obj = (DG.Tweening.Tween)LuaScriptMgr.GetNetObjectSelf(L, 1, "DG.Tweening.Tween");
		bool o = obj.IsPlaying();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IsInitialized(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		DG.Tweening.Tween obj = (DG.Tweening.Tween)LuaScriptMgr.GetNetObjectSelf(L, 1, "DG.Tweening.Tween");
		bool o = obj.IsInitialized();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IsComplete(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		DG.Tweening.Tween obj = (DG.Tweening.Tween)LuaScriptMgr.GetNetObjectSelf(L, 1, "DG.Tweening.Tween");
		bool o = obj.IsComplete();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IsBackwards(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		DG.Tweening.Tween obj = (DG.Tweening.Tween)LuaScriptMgr.GetNetObjectSelf(L, 1, "DG.Tweening.Tween");
		bool o = obj.IsBackwards();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IsActive(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		DG.Tweening.Tween obj = (DG.Tweening.Tween)LuaScriptMgr.GetNetObjectSelf(L, 1, "DG.Tweening.Tween");
		bool o = obj.IsActive();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ElapsedDirectionalPercentage(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		DG.Tweening.Tween obj = (DG.Tweening.Tween)LuaScriptMgr.GetNetObjectSelf(L, 1, "DG.Tweening.Tween");
		float o = obj.ElapsedDirectionalPercentage();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ElapsedPercentage(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		DG.Tweening.Tween obj = (DG.Tweening.Tween)LuaScriptMgr.GetNetObjectSelf(L, 1, "DG.Tweening.Tween");
		bool arg0 = LuaScriptMgr.GetBoolean(L, 2);
		float o = obj.ElapsedPercentage(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Elapsed(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		DG.Tweening.Tween obj = (DG.Tweening.Tween)LuaScriptMgr.GetNetObjectSelf(L, 1, "DG.Tweening.Tween");
		bool arg0 = LuaScriptMgr.GetBoolean(L, 2);
		float o = obj.Elapsed(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Duration(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		DG.Tweening.Tween obj = (DG.Tweening.Tween)LuaScriptMgr.GetNetObjectSelf(L, 1, "DG.Tweening.Tween");
		bool arg0 = LuaScriptMgr.GetBoolean(L, 2);
		float o = obj.Duration(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Delay(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		DG.Tweening.Tween obj = (DG.Tweening.Tween)LuaScriptMgr.GetNetObjectSelf(L, 1, "DG.Tweening.Tween");
		float o = obj.Delay();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CompletedLoops(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		DG.Tweening.Tween obj = (DG.Tweening.Tween)LuaScriptMgr.GetNetObjectSelf(L, 1, "DG.Tweening.Tween");
		int o = obj.CompletedLoops();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int WaitForStart(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		DG.Tweening.Tween obj = (DG.Tweening.Tween)LuaScriptMgr.GetNetObjectSelf(L, 1, "DG.Tweening.Tween");
		Coroutine o = obj.WaitForStart();
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int WaitForPosition(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		DG.Tweening.Tween obj = (DG.Tweening.Tween)LuaScriptMgr.GetNetObjectSelf(L, 1, "DG.Tweening.Tween");
		float arg0 = (float)LuaScriptMgr.GetNumber(L, 2);
		YieldInstruction o = obj.WaitForPosition(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int WaitForElapsedLoops(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		DG.Tweening.Tween obj = (DG.Tweening.Tween)LuaScriptMgr.GetNetObjectSelf(L, 1, "DG.Tweening.Tween");
		int arg0 = (int)LuaScriptMgr.GetNumber(L, 2);
		YieldInstruction o = obj.WaitForElapsedLoops(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int WaitForKill(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		DG.Tweening.Tween obj = (DG.Tweening.Tween)LuaScriptMgr.GetNetObjectSelf(L, 1, "DG.Tweening.Tween");
		YieldInstruction o = obj.WaitForKill();
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int WaitForRewind(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		DG.Tweening.Tween obj = (DG.Tweening.Tween)LuaScriptMgr.GetNetObjectSelf(L, 1, "DG.Tweening.Tween");
		YieldInstruction o = obj.WaitForRewind();
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int WaitForCompletion(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		DG.Tweening.Tween obj = (DG.Tweening.Tween)LuaScriptMgr.GetNetObjectSelf(L, 1, "DG.Tweening.Tween");
		YieldInstruction o = obj.WaitForCompletion();
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GotoWaypoint(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		DG.Tweening.Tween obj = (DG.Tweening.Tween)LuaScriptMgr.GetNetObjectSelf(L, 1, "DG.Tweening.Tween");
		int arg0 = (int)LuaScriptMgr.GetNumber(L, 2);
		bool arg1 = LuaScriptMgr.GetBoolean(L, 3);
		obj.GotoWaypoint(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int TogglePause(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		DG.Tweening.Tween obj = (DG.Tweening.Tween)LuaScriptMgr.GetNetObjectSelf(L, 1, "DG.Tweening.Tween");
		obj.TogglePause();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Rewind(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		DG.Tweening.Tween obj = (DG.Tweening.Tween)LuaScriptMgr.GetNetObjectSelf(L, 1, "DG.Tweening.Tween");
		bool arg0 = LuaScriptMgr.GetBoolean(L, 2);
		obj.Rewind(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Restart(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		DG.Tweening.Tween obj = (DG.Tweening.Tween)LuaScriptMgr.GetNetObjectSelf(L, 1, "DG.Tweening.Tween");
		bool arg0 = LuaScriptMgr.GetBoolean(L, 2);
		obj.Restart(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int PlayForward(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		DG.Tweening.Tween obj = (DG.Tweening.Tween)LuaScriptMgr.GetNetObjectSelf(L, 1, "DG.Tweening.Tween");
		obj.PlayForward();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int PlayBackwards(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		DG.Tweening.Tween obj = (DG.Tweening.Tween)LuaScriptMgr.GetNetObjectSelf(L, 1, "DG.Tweening.Tween");
		obj.PlayBackwards();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Play(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		DG.Tweening.Tween obj = (DG.Tweening.Tween)LuaScriptMgr.GetNetObjectSelf(L, 1, "DG.Tweening.Tween");
		DG.Tweening.Tween o = obj.Play();
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Pause(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		DG.Tweening.Tween obj = (DG.Tweening.Tween)LuaScriptMgr.GetNetObjectSelf(L, 1, "DG.Tweening.Tween");
		DG.Tweening.Tween o = obj.Pause();
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Kill(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		DG.Tweening.Tween obj = (DG.Tweening.Tween)LuaScriptMgr.GetNetObjectSelf(L, 1, "DG.Tweening.Tween");
		bool arg0 = LuaScriptMgr.GetBoolean(L, 2);
		obj.Kill(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Goto(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		DG.Tweening.Tween obj = (DG.Tweening.Tween)LuaScriptMgr.GetNetObjectSelf(L, 1, "DG.Tweening.Tween");
		float arg0 = (float)LuaScriptMgr.GetNumber(L, 2);
		bool arg1 = LuaScriptMgr.GetBoolean(L, 3);
		obj.Goto(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ForceInit(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		DG.Tweening.Tween obj = (DG.Tweening.Tween)LuaScriptMgr.GetNetObjectSelf(L, 1, "DG.Tweening.Tween");
		obj.ForceInit();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Flip(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		DG.Tweening.Tween obj = (DG.Tweening.Tween)LuaScriptMgr.GetNetObjectSelf(L, 1, "DG.Tweening.Tween");
		obj.Flip();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Complete(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		DG.Tweening.Tween obj = (DG.Tweening.Tween)LuaScriptMgr.GetNetObjectSelf(L, 1, "DG.Tweening.Tween");
		obj.Complete();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetSpeedBased(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 1)
		{
			DG.Tweening.Tween obj = (DG.Tweening.Tween)LuaScriptMgr.GetNetObjectSelf(L, 1, "DG.Tweening.Tween");
			DG.Tweening.Tween o = obj.SetSpeedBased();
			LuaScriptMgr.PushObject(L, o);
			return 1;
		}
		else if (count == 2)
		{
			DG.Tweening.Tween obj = (DG.Tweening.Tween)LuaScriptMgr.GetNetObjectSelf(L, 1, "DG.Tweening.Tween");
			bool arg0 = LuaScriptMgr.GetBoolean(L, 2);
			DG.Tweening.Tween o = obj.SetSpeedBased(arg0);
			LuaScriptMgr.PushObject(L, o);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: DG.Tweening.Tween.SetSpeedBased");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetRelative(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 1)
		{
			DG.Tweening.Tween obj = (DG.Tweening.Tween)LuaScriptMgr.GetNetObjectSelf(L, 1, "DG.Tweening.Tween");
			DG.Tweening.Tween o = obj.SetRelative();
			LuaScriptMgr.PushObject(L, o);
			return 1;
		}
		else if (count == 2)
		{
			DG.Tweening.Tween obj = (DG.Tweening.Tween)LuaScriptMgr.GetNetObjectSelf(L, 1, "DG.Tweening.Tween");
			bool arg0 = LuaScriptMgr.GetBoolean(L, 2);
			DG.Tweening.Tween o = obj.SetRelative(arg0);
			LuaScriptMgr.PushObject(L, o);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: DG.Tweening.Tween.SetRelative");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetDelay(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		DG.Tweening.Tween obj = (DG.Tweening.Tween)LuaScriptMgr.GetNetObjectSelf(L, 1, "DG.Tweening.Tween");
		float arg0 = (float)LuaScriptMgr.GetNumber(L, 2);
		DG.Tweening.Tween o = obj.SetDelay(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetAs(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(DG.Tweening.Tween), typeof(DG.Tweening.TweenParams)))
		{
			DG.Tweening.Tween obj = (DG.Tweening.Tween)LuaScriptMgr.GetNetObjectSelf(L, 1, "DG.Tweening.Tween");
			DG.Tweening.TweenParams arg0 = (DG.Tweening.TweenParams)LuaScriptMgr.GetLuaObject(L, 2);
			DG.Tweening.Tween o = obj.SetAs(arg0);
			LuaScriptMgr.PushObject(L, o);
			return 1;
		}
		else if (count == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(DG.Tweening.Tween), typeof(DG.Tweening.Tween)))
		{
			DG.Tweening.Tween obj = (DG.Tweening.Tween)LuaScriptMgr.GetNetObjectSelf(L, 1, "DG.Tweening.Tween");
			DG.Tweening.Tween arg0 = (DG.Tweening.Tween)LuaScriptMgr.GetLuaObject(L, 2);
			DG.Tweening.Tween o = obj.SetAs(arg0);
			LuaScriptMgr.PushObject(L, o);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: DG.Tweening.Tween.SetAs");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnWaypointChange(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		DG.Tweening.Tween obj = (DG.Tweening.Tween)LuaScriptMgr.GetNetObjectSelf(L, 1, "DG.Tweening.Tween");
		DG.Tweening.TweenCallback<int> arg0 = null;
		LuaTypes funcType2 = LuaDLL.lua_type(L, 2);

		if (funcType2 != LuaTypes.LUA_TFUNCTION)
		{
			 arg0 = (DG.Tweening.TweenCallback<int>)LuaScriptMgr.GetNetObject(L, 2, typeof(DG.Tweening.TweenCallback<int>));
		}
		else
		{
			LuaFunction func = LuaScriptMgr.GetLuaFunction(L, 2);
			arg0 = (param0) =>
			{
				int top = func.BeginPCall();
				LuaScriptMgr.Push(L, param0);
				func.PCall(top, 1);
				func.EndPCall(top);
			};
		}

		DG.Tweening.Tween o = obj.OnWaypointChange(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnKill(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		DG.Tweening.Tween obj = (DG.Tweening.Tween)LuaScriptMgr.GetNetObjectSelf(L, 1, "DG.Tweening.Tween");
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

		DG.Tweening.Tween o = obj.OnKill(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnComplete(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		DG.Tweening.Tween obj = (DG.Tweening.Tween)LuaScriptMgr.GetNetObjectSelf(L, 1, "DG.Tweening.Tween");
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

		DG.Tweening.Tween o = obj.OnComplete(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnStepComplete(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		DG.Tweening.Tween obj = (DG.Tweening.Tween)LuaScriptMgr.GetNetObjectSelf(L, 1, "DG.Tweening.Tween");
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

		DG.Tweening.Tween o = obj.OnStepComplete(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnUpdate(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		DG.Tweening.Tween obj = (DG.Tweening.Tween)LuaScriptMgr.GetNetObjectSelf(L, 1, "DG.Tweening.Tween");
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

		DG.Tweening.Tween o = obj.OnUpdate(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnRewind(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		DG.Tweening.Tween obj = (DG.Tweening.Tween)LuaScriptMgr.GetNetObjectSelf(L, 1, "DG.Tweening.Tween");
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

		DG.Tweening.Tween o = obj.OnRewind(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnPause(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		DG.Tweening.Tween obj = (DG.Tweening.Tween)LuaScriptMgr.GetNetObjectSelf(L, 1, "DG.Tweening.Tween");
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

		DG.Tweening.Tween o = obj.OnPause(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnPlay(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		DG.Tweening.Tween obj = (DG.Tweening.Tween)LuaScriptMgr.GetNetObjectSelf(L, 1, "DG.Tweening.Tween");
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

		DG.Tweening.Tween o = obj.OnPlay(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnStart(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		DG.Tweening.Tween obj = (DG.Tweening.Tween)LuaScriptMgr.GetNetObjectSelf(L, 1, "DG.Tweening.Tween");
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

		DG.Tweening.Tween o = obj.OnStart(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetUpdate(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(DG.Tweening.Tween), typeof(DG.Tweening.UpdateType)))
		{
			DG.Tweening.Tween obj = (DG.Tweening.Tween)LuaScriptMgr.GetNetObjectSelf(L, 1, "DG.Tweening.Tween");
			DG.Tweening.UpdateType arg0 = (DG.Tweening.UpdateType)LuaScriptMgr.GetLuaObject(L, 2);
			DG.Tweening.Tween o = obj.SetUpdate(arg0);
			LuaScriptMgr.PushObject(L, o);
			return 1;
		}
		else if (count == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(DG.Tweening.Tween), typeof(bool)))
		{
			DG.Tweening.Tween obj = (DG.Tweening.Tween)LuaScriptMgr.GetNetObjectSelf(L, 1, "DG.Tweening.Tween");
			bool arg0 = LuaDLL.lua_toboolean(L, 2);
			DG.Tweening.Tween o = obj.SetUpdate(arg0);
			LuaScriptMgr.PushObject(L, o);
			return 1;
		}
		else if (count == 3)
		{
			DG.Tweening.Tween obj = (DG.Tweening.Tween)LuaScriptMgr.GetNetObjectSelf(L, 1, "DG.Tweening.Tween");
			DG.Tweening.UpdateType arg0 = (DG.Tweening.UpdateType)LuaScriptMgr.GetNetObject(L, 2, typeof(DG.Tweening.UpdateType));
			bool arg1 = LuaScriptMgr.GetBoolean(L, 3);
			DG.Tweening.Tween o = obj.SetUpdate(arg0,arg1);
			LuaScriptMgr.PushObject(L, o);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: DG.Tweening.Tween.SetUpdate");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetRecyclable(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 1)
		{
			DG.Tweening.Tween obj = (DG.Tweening.Tween)LuaScriptMgr.GetNetObjectSelf(L, 1, "DG.Tweening.Tween");
			DG.Tweening.Tween o = obj.SetRecyclable();
			LuaScriptMgr.PushObject(L, o);
			return 1;
		}
		else if (count == 2)
		{
			DG.Tweening.Tween obj = (DG.Tweening.Tween)LuaScriptMgr.GetNetObjectSelf(L, 1, "DG.Tweening.Tween");
			bool arg0 = LuaScriptMgr.GetBoolean(L, 2);
			DG.Tweening.Tween o = obj.SetRecyclable(arg0);
			LuaScriptMgr.PushObject(L, o);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: DG.Tweening.Tween.SetRecyclable");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetEase(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(DG.Tweening.Tween), typeof(DG.Tweening.EaseFunction)))
		{
			DG.Tweening.Tween obj = (DG.Tweening.Tween)LuaScriptMgr.GetNetObjectSelf(L, 1, "DG.Tweening.Tween");
			DG.Tweening.EaseFunction arg0 = null;
			LuaTypes funcType2 = LuaDLL.lua_type(L, 2);

			if (funcType2 != LuaTypes.LUA_TFUNCTION)
			{
				 arg0 = (DG.Tweening.EaseFunction)LuaScriptMgr.GetLuaObject(L, 2);
			}
			else
			{
				LuaFunction func = LuaScriptMgr.GetLuaFunction(L, 2);
				arg0 = (param0, param1, param2, param3) =>
				{
					int top = func.BeginPCall();
					LuaScriptMgr.Push(L, param0);
					LuaScriptMgr.Push(L, param1);
					LuaScriptMgr.Push(L, param2);
					LuaScriptMgr.Push(L, param3);
					func.PCall(top, 4);
					object[] objs = func.PopValues(top);
					func.EndPCall(top);
					return (float)objs[0];
				};
			}

			DG.Tweening.Tween o = obj.SetEase(arg0);
			LuaScriptMgr.PushObject(L, o);
			return 1;
		}
		else if (count == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(DG.Tweening.Tween), typeof(AnimationCurve)))
		{
			DG.Tweening.Tween obj = (DG.Tweening.Tween)LuaScriptMgr.GetNetObjectSelf(L, 1, "DG.Tweening.Tween");
			AnimationCurve arg0 = (AnimationCurve)LuaScriptMgr.GetLuaObject(L, 2);
			DG.Tweening.Tween o = obj.SetEase(arg0);
			LuaScriptMgr.PushObject(L, o);
			return 1;
		}
		else if (count == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(DG.Tweening.Tween), typeof(DG.Tweening.Ease)))
		{
			DG.Tweening.Tween obj = (DG.Tweening.Tween)LuaScriptMgr.GetNetObjectSelf(L, 1, "DG.Tweening.Tween");
			DG.Tweening.Ease arg0 = (DG.Tweening.Ease)LuaScriptMgr.GetLuaObject(L, 2);
			DG.Tweening.Tween o = obj.SetEase(arg0);
			LuaScriptMgr.PushObject(L, o);
			return 1;
		}
		else if (count == 3)
		{
			DG.Tweening.Tween obj = (DG.Tweening.Tween)LuaScriptMgr.GetNetObjectSelf(L, 1, "DG.Tweening.Tween");
			DG.Tweening.Ease arg0 = (DG.Tweening.Ease)LuaScriptMgr.GetNetObject(L, 2, typeof(DG.Tweening.Ease));
			float arg1 = (float)LuaScriptMgr.GetNumber(L, 3);
			DG.Tweening.Tween o = obj.SetEase(arg0,arg1);
			LuaScriptMgr.PushObject(L, o);
			return 1;
		}
		else if (count == 4)
		{
			DG.Tweening.Tween obj = (DG.Tweening.Tween)LuaScriptMgr.GetNetObjectSelf(L, 1, "DG.Tweening.Tween");
			DG.Tweening.Ease arg0 = (DG.Tweening.Ease)LuaScriptMgr.GetNetObject(L, 2, typeof(DG.Tweening.Ease));
			float arg1 = (float)LuaScriptMgr.GetNumber(L, 3);
			float arg2 = (float)LuaScriptMgr.GetNumber(L, 4);
			DG.Tweening.Tween o = obj.SetEase(arg0,arg1,arg2);
			LuaScriptMgr.PushObject(L, o);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: DG.Tweening.Tween.SetEase");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetLoops(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2)
		{
			DG.Tweening.Tween obj = (DG.Tweening.Tween)LuaScriptMgr.GetNetObjectSelf(L, 1, "DG.Tweening.Tween");
			int arg0 = (int)LuaScriptMgr.GetNumber(L, 2);
			DG.Tweening.Tween o = obj.SetLoops(arg0);
			LuaScriptMgr.PushObject(L, o);
			return 1;
		}
		else if (count == 3)
		{
			DG.Tweening.Tween obj = (DG.Tweening.Tween)LuaScriptMgr.GetNetObjectSelf(L, 1, "DG.Tweening.Tween");
			int arg0 = (int)LuaScriptMgr.GetNumber(L, 2);
			DG.Tweening.LoopType arg1 = (DG.Tweening.LoopType)LuaScriptMgr.GetNetObject(L, 3, typeof(DG.Tweening.LoopType));
			DG.Tweening.Tween o = obj.SetLoops(arg0,arg1);
			LuaScriptMgr.PushObject(L, o);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: DG.Tweening.Tween.SetLoops");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetTarget(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		DG.Tweening.Tween obj = (DG.Tweening.Tween)LuaScriptMgr.GetNetObjectSelf(L, 1, "DG.Tweening.Tween");
		object arg0 = LuaScriptMgr.GetVarObject(L, 2);
		DG.Tweening.Tween o = obj.SetTarget(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetId(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		DG.Tweening.Tween obj = (DG.Tweening.Tween)LuaScriptMgr.GetNetObjectSelf(L, 1, "DG.Tweening.Tween");
		object arg0 = LuaScriptMgr.GetVarObject(L, 2);
		DG.Tweening.Tween o = obj.SetId(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetAutoKill(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 1)
		{
			DG.Tweening.Tween obj = (DG.Tweening.Tween)LuaScriptMgr.GetNetObjectSelf(L, 1, "DG.Tweening.Tween");
			DG.Tweening.Tween o = obj.SetAutoKill();
			LuaScriptMgr.PushObject(L, o);
			return 1;
		}
		else if (count == 2)
		{
			DG.Tweening.Tween obj = (DG.Tweening.Tween)LuaScriptMgr.GetNetObjectSelf(L, 1, "DG.Tweening.Tween");
			bool arg0 = LuaScriptMgr.GetBoolean(L, 2);
			DG.Tweening.Tween o = obj.SetAutoKill(arg0);
			LuaScriptMgr.PushObject(L, o);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: DG.Tweening.Tween.SetAutoKill");
		}

		return 0;
	}
}

