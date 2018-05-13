using System;
using UnityEngine;
using UnityEngine.EventSystems;
using LuaInterface;
using Object = UnityEngine.Object;

public class UIEventListenerWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("Get", Get),
			new LuaMethod("OnDrag", OnDrag),
			new LuaMethod("OnEndDrag", OnEndDrag),
			new LuaMethod("OnDrop", OnDrop),
			new LuaMethod("OnPointerClick", OnPointerClick),
			new LuaMethod("OnPointerDown", OnPointerDown),
			new LuaMethod("OnPointerUp", OnPointerUp),
			new LuaMethod("OnPointerEnter", OnPointerEnter),
			new LuaMethod("OnPointerExit", OnPointerExit),
			new LuaMethod("OnSelect", OnSelect),
			new LuaMethod("OnUpdateSelected", OnUpdateSelected),
			new LuaMethod("OnDeselect", OnDeselect),
			new LuaMethod("OnScroll", OnScroll),
			new LuaMethod("OnMove", OnMove),
			new LuaMethod("SetEventHandle", SetEventHandle),
			new LuaMethod("New", _CreateUIEventListener),
			new LuaMethod("GetClassType", GetClassType),
			new LuaMethod("__eq", Lua_Eq),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("onClick", get_onClick, set_onClick),
			new LuaField("onDoubleClick", get_onDoubleClick, set_onDoubleClick),
			new LuaField("onDown", get_onDown, set_onDown),
			new LuaField("onEnter", get_onEnter, set_onEnter),
			new LuaField("onExit", get_onExit, set_onExit),
			new LuaField("onUp", get_onUp, set_onUp),
			new LuaField("onSelect", get_onSelect, set_onSelect),
			new LuaField("onUpdateSelect", get_onUpdateSelect, set_onUpdateSelect),
			new LuaField("onDeSelect", get_onDeSelect, set_onDeSelect),
			new LuaField("onDrag", get_onDrag, set_onDrag),
			new LuaField("onDragEnd", get_onDragEnd, set_onDragEnd),
			new LuaField("onDrop", get_onDrop, set_onDrop),
			new LuaField("onScroll", get_onScroll, set_onScroll),
			new LuaField("onMove", get_onMove, set_onMove),
		};

		LuaScriptMgr.RegisterLib(L, "UIEventListener", typeof(UIEventListener), regs, fields, typeof(MonoBehaviour));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateUIEventListener(IntPtr L)
	{
		LuaDLL.luaL_error(L, "UIEventListener class does not have a constructor function");
		return 0;
	}

	static Type classType = typeof(UIEventListener);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onClick(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIEventListener obj = (UIEventListener)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onClick");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onClick on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.onClick);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onDoubleClick(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIEventListener obj = (UIEventListener)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onDoubleClick");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onDoubleClick on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.onDoubleClick);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onDown(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIEventListener obj = (UIEventListener)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onDown");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onDown on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.onDown);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onEnter(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIEventListener obj = (UIEventListener)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onEnter");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onEnter on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.onEnter);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onExit(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIEventListener obj = (UIEventListener)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onExit");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onExit on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.onExit);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onUp(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIEventListener obj = (UIEventListener)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onUp");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onUp on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.onUp);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onSelect(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIEventListener obj = (UIEventListener)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onSelect");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onSelect on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.onSelect);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onUpdateSelect(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIEventListener obj = (UIEventListener)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onUpdateSelect");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onUpdateSelect on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.onUpdateSelect);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onDeSelect(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIEventListener obj = (UIEventListener)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onDeSelect");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onDeSelect on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.onDeSelect);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onDrag(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIEventListener obj = (UIEventListener)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onDrag");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onDrag on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.onDrag);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onDragEnd(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIEventListener obj = (UIEventListener)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onDragEnd");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onDragEnd on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.onDragEnd);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onDrop(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIEventListener obj = (UIEventListener)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onDrop");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onDrop on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.onDrop);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onScroll(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIEventListener obj = (UIEventListener)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onScroll");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onScroll on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.onScroll);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onMove(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIEventListener obj = (UIEventListener)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onMove");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onMove on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.onMove);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_onClick(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIEventListener obj = (UIEventListener)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onClick");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onClick on a nil value");
			}
		}

		obj.onClick = (TouchHandle)LuaScriptMgr.GetNetObject(L, 3, typeof(TouchHandle));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_onDoubleClick(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIEventListener obj = (UIEventListener)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onDoubleClick");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onDoubleClick on a nil value");
			}
		}

		obj.onDoubleClick = (TouchHandle)LuaScriptMgr.GetNetObject(L, 3, typeof(TouchHandle));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_onDown(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIEventListener obj = (UIEventListener)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onDown");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onDown on a nil value");
			}
		}

		obj.onDown = (TouchHandle)LuaScriptMgr.GetNetObject(L, 3, typeof(TouchHandle));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_onEnter(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIEventListener obj = (UIEventListener)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onEnter");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onEnter on a nil value");
			}
		}

		obj.onEnter = (TouchHandle)LuaScriptMgr.GetNetObject(L, 3, typeof(TouchHandle));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_onExit(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIEventListener obj = (UIEventListener)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onExit");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onExit on a nil value");
			}
		}

		obj.onExit = (TouchHandle)LuaScriptMgr.GetNetObject(L, 3, typeof(TouchHandle));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_onUp(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIEventListener obj = (UIEventListener)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onUp");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onUp on a nil value");
			}
		}

		obj.onUp = (TouchHandle)LuaScriptMgr.GetNetObject(L, 3, typeof(TouchHandle));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_onSelect(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIEventListener obj = (UIEventListener)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onSelect");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onSelect on a nil value");
			}
		}

		obj.onSelect = (TouchHandle)LuaScriptMgr.GetNetObject(L, 3, typeof(TouchHandle));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_onUpdateSelect(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIEventListener obj = (UIEventListener)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onUpdateSelect");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onUpdateSelect on a nil value");
			}
		}

		obj.onUpdateSelect = (TouchHandle)LuaScriptMgr.GetNetObject(L, 3, typeof(TouchHandle));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_onDeSelect(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIEventListener obj = (UIEventListener)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onDeSelect");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onDeSelect on a nil value");
			}
		}

		obj.onDeSelect = (TouchHandle)LuaScriptMgr.GetNetObject(L, 3, typeof(TouchHandle));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_onDrag(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIEventListener obj = (UIEventListener)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onDrag");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onDrag on a nil value");
			}
		}

		obj.onDrag = (TouchHandle)LuaScriptMgr.GetNetObject(L, 3, typeof(TouchHandle));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_onDragEnd(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIEventListener obj = (UIEventListener)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onDragEnd");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onDragEnd on a nil value");
			}
		}

		obj.onDragEnd = (TouchHandle)LuaScriptMgr.GetNetObject(L, 3, typeof(TouchHandle));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_onDrop(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIEventListener obj = (UIEventListener)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onDrop");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onDrop on a nil value");
			}
		}

		obj.onDrop = (TouchHandle)LuaScriptMgr.GetNetObject(L, 3, typeof(TouchHandle));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_onScroll(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIEventListener obj = (UIEventListener)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onScroll");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onScroll on a nil value");
			}
		}

		obj.onScroll = (TouchHandle)LuaScriptMgr.GetNetObject(L, 3, typeof(TouchHandle));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_onMove(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIEventListener obj = (UIEventListener)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onMove");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onMove on a nil value");
			}
		}

		obj.onMove = (TouchHandle)LuaScriptMgr.GetNetObject(L, 3, typeof(TouchHandle));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Get(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		GameObject arg0 = (GameObject)LuaScriptMgr.GetUnityObject(L, 1, typeof(GameObject));
		UIEventListener o = UIEventListener.Get(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnDrag(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		UIEventListener obj = (UIEventListener)LuaScriptMgr.GetUnityObjectSelf(L, 1, "UIEventListener");
		PointerEventData arg0 = (PointerEventData)LuaScriptMgr.GetNetObject(L, 2, typeof(PointerEventData));
		obj.OnDrag(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnEndDrag(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		UIEventListener obj = (UIEventListener)LuaScriptMgr.GetUnityObjectSelf(L, 1, "UIEventListener");
		PointerEventData arg0 = (PointerEventData)LuaScriptMgr.GetNetObject(L, 2, typeof(PointerEventData));
		obj.OnEndDrag(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnDrop(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		UIEventListener obj = (UIEventListener)LuaScriptMgr.GetUnityObjectSelf(L, 1, "UIEventListener");
		PointerEventData arg0 = (PointerEventData)LuaScriptMgr.GetNetObject(L, 2, typeof(PointerEventData));
		obj.OnDrop(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnPointerClick(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		UIEventListener obj = (UIEventListener)LuaScriptMgr.GetUnityObjectSelf(L, 1, "UIEventListener");
		PointerEventData arg0 = (PointerEventData)LuaScriptMgr.GetNetObject(L, 2, typeof(PointerEventData));
		obj.OnPointerClick(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnPointerDown(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		UIEventListener obj = (UIEventListener)LuaScriptMgr.GetUnityObjectSelf(L, 1, "UIEventListener");
		PointerEventData arg0 = (PointerEventData)LuaScriptMgr.GetNetObject(L, 2, typeof(PointerEventData));
		obj.OnPointerDown(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnPointerUp(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		UIEventListener obj = (UIEventListener)LuaScriptMgr.GetUnityObjectSelf(L, 1, "UIEventListener");
		PointerEventData arg0 = (PointerEventData)LuaScriptMgr.GetNetObject(L, 2, typeof(PointerEventData));
		obj.OnPointerUp(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnPointerEnter(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		UIEventListener obj = (UIEventListener)LuaScriptMgr.GetUnityObjectSelf(L, 1, "UIEventListener");
		PointerEventData arg0 = (PointerEventData)LuaScriptMgr.GetNetObject(L, 2, typeof(PointerEventData));
		obj.OnPointerEnter(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnPointerExit(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		UIEventListener obj = (UIEventListener)LuaScriptMgr.GetUnityObjectSelf(L, 1, "UIEventListener");
		PointerEventData arg0 = (PointerEventData)LuaScriptMgr.GetNetObject(L, 2, typeof(PointerEventData));
		obj.OnPointerExit(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnSelect(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		UIEventListener obj = (UIEventListener)LuaScriptMgr.GetUnityObjectSelf(L, 1, "UIEventListener");
		BaseEventData arg0 = (BaseEventData)LuaScriptMgr.GetNetObject(L, 2, typeof(BaseEventData));
		obj.OnSelect(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnUpdateSelected(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		UIEventListener obj = (UIEventListener)LuaScriptMgr.GetUnityObjectSelf(L, 1, "UIEventListener");
		BaseEventData arg0 = (BaseEventData)LuaScriptMgr.GetNetObject(L, 2, typeof(BaseEventData));
		obj.OnUpdateSelected(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnDeselect(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		UIEventListener obj = (UIEventListener)LuaScriptMgr.GetUnityObjectSelf(L, 1, "UIEventListener");
		BaseEventData arg0 = (BaseEventData)LuaScriptMgr.GetNetObject(L, 2, typeof(BaseEventData));
		obj.OnDeselect(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnScroll(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		UIEventListener obj = (UIEventListener)LuaScriptMgr.GetUnityObjectSelf(L, 1, "UIEventListener");
		PointerEventData arg0 = (PointerEventData)LuaScriptMgr.GetNetObject(L, 2, typeof(PointerEventData));
		obj.OnScroll(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnMove(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		UIEventListener obj = (UIEventListener)LuaScriptMgr.GetUnityObjectSelf(L, 1, "UIEventListener");
		AxisEventData arg0 = (AxisEventData)LuaScriptMgr.GetNetObject(L, 2, typeof(AxisEventData));
		obj.OnMove(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetEventHandle(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (LuaScriptMgr.CheckTypes(L, 1, typeof(UIEventListener), typeof(int), typeof(UtilCommon.OnTouchEventHandle)) && LuaScriptMgr.CheckParamsType(L, typeof(object), 4, count - 3))
		{
			UIEventListener obj = (UIEventListener)LuaScriptMgr.GetUnityObjectSelf(L, 1, "UIEventListener");
			int arg0 = (int)LuaDLL.lua_tonumber(L, 2);
			UtilCommon.OnTouchEventHandle arg1 = null;
			LuaTypes funcType3 = LuaDLL.lua_type(L, 3);

			if (funcType3 != LuaTypes.LUA_TFUNCTION)
			{
				 arg1 = (UtilCommon.OnTouchEventHandle)LuaScriptMgr.GetLuaObject(L, 3);
			}
			else
			{
				LuaFunction func = LuaScriptMgr.GetLuaFunction(L, 3);
				arg1 = (param0, param1, param2) =>
				{
					int top = func.BeginPCall();
					LuaScriptMgr.Push(L, param0);
					LuaScriptMgr.PushVarObject(L, param1);
					LuaScriptMgr.PushArray(L, param2);
					func.PCall(top, 3);
					func.EndPCall(top);
				};
			}

			object[] objs2 = LuaScriptMgr.GetParamsObject(L, 4, count - 3);
			obj.SetEventHandle(arg0,arg1,objs2);
			return 0;
		}
		else if (LuaScriptMgr.CheckTypes(L, 1, typeof(UIEventListener), typeof(EnumTouchEventType), typeof(UtilCommon.OnTouchEventHandle)) && LuaScriptMgr.CheckParamsType(L, typeof(object), 4, count - 3))
		{
			UIEventListener obj = (UIEventListener)LuaScriptMgr.GetUnityObjectSelf(L, 1, "UIEventListener");
			EnumTouchEventType arg0 = (EnumTouchEventType)LuaScriptMgr.GetLuaObject(L, 2);
			UtilCommon.OnTouchEventHandle arg1 = null;
			LuaTypes funcType3 = LuaDLL.lua_type(L, 3);

			if (funcType3 != LuaTypes.LUA_TFUNCTION)
			{
				 arg1 = (UtilCommon.OnTouchEventHandle)LuaScriptMgr.GetLuaObject(L, 3);
			}
			else
			{
				LuaFunction func = LuaScriptMgr.GetLuaFunction(L, 3);
				arg1 = (param0, param1, param2) =>
				{
					int top = func.BeginPCall();
					LuaScriptMgr.Push(L, param0);
					LuaScriptMgr.PushVarObject(L, param1);
					LuaScriptMgr.PushArray(L, param2);
					func.PCall(top, 3);
					func.EndPCall(top);
				};
			}

			object[] objs2 = LuaScriptMgr.GetParamsObject(L, 4, count - 3);
			obj.SetEventHandle(arg0,arg1,objs2);
			return 0;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: UIEventListener.SetEventHandle");
		}

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

