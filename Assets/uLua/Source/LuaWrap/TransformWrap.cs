using System;
using UnityEngine;
using System.Collections;
using LuaInterface;
using Object = UnityEngine.Object;
using DG.Tweening;

public class TransformWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("SetParent", SetParent),
			new LuaMethod("SetPositionAndRotation", SetPositionAndRotation),
			new LuaMethod("Translate", Translate),
			new LuaMethod("Rotate", Rotate),
			new LuaMethod("RotateAround", RotateAround),
			new LuaMethod("LookAt", LookAt),
			new LuaMethod("TransformDirection", TransformDirection),
			new LuaMethod("InverseTransformDirection", InverseTransformDirection),
			new LuaMethod("TransformVector", TransformVector),
			new LuaMethod("InverseTransformVector", InverseTransformVector),
			new LuaMethod("TransformPoint", TransformPoint),
			new LuaMethod("InverseTransformPoint", InverseTransformPoint),
			new LuaMethod("DetachChildren", DetachChildren),
			new LuaMethod("SetAsFirstSibling", SetAsFirstSibling),
			new LuaMethod("SetAsLastSibling", SetAsLastSibling),
			new LuaMethod("SetSiblingIndex", SetSiblingIndex),
			new LuaMethod("GetSiblingIndex", GetSiblingIndex),
			new LuaMethod("Find", Find),
			new LuaMethod("IsChildOf", IsChildOf),
			new LuaMethod("GetEnumerator", GetEnumerator),
			new LuaMethod("GetChild", GetChild),
			new LuaMethod("DOBlendableScaleBy", DOBlendableScaleBy),
			new LuaMethod("DOBlendableLocalRotateBy", DOBlendableLocalRotateBy),
			new LuaMethod("DOBlendableRotateBy", DOBlendableRotateBy),
			new LuaMethod("DOBlendableLocalMoveBy", DOBlendableLocalMoveBy),
			new LuaMethod("DOBlendableMoveBy", DOBlendableMoveBy),
			new LuaMethod("DOLocalPath", DOLocalPath),
			new LuaMethod("DOPath", DOPath),
			new LuaMethod("DOLocalJump", DOLocalJump),
			new LuaMethod("DOJump", DOJump),
			new LuaMethod("DOShakeScale", DOShakeScale),
			new LuaMethod("DOShakeRotation", DOShakeRotation),
			new LuaMethod("DOShakePosition", DOShakePosition),
			new LuaMethod("DOPunchRotation", DOPunchRotation),
			new LuaMethod("DOPunchScale", DOPunchScale),
			new LuaMethod("DOPunchPosition", DOPunchPosition),
			new LuaMethod("DOLookAt", DOLookAt),
			new LuaMethod("DOScaleZ", DOScaleZ),
			new LuaMethod("DOScaleY", DOScaleY),
			new LuaMethod("DOScaleX", DOScaleX),
			new LuaMethod("DOScale", DOScale),
			new LuaMethod("DOLocalRotate", DOLocalRotate),
			new LuaMethod("DORotate", DORotate),
			new LuaMethod("DOLocalMoveZ", DOLocalMoveZ),
			new LuaMethod("DOLocalMoveY", DOLocalMoveY),
			new LuaMethod("DOLocalMoveX", DOLocalMoveX),
			new LuaMethod("DOLocalMove", DOLocalMove),
			new LuaMethod("DOMoveZ", DOMoveZ),
			new LuaMethod("DOMoveY", DOMoveY),
			new LuaMethod("DOMoveX", DOMoveX),
			new LuaMethod("DOMove", DOMove),
			new LuaMethod("New", _CreateTransform),
			new LuaMethod("GetClassType", GetClassType),
			new LuaMethod("__eq", Lua_Eq),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("position", get_position, set_position),
			new LuaField("localPosition", get_localPosition, set_localPosition),
			new LuaField("eulerAngles", get_eulerAngles, set_eulerAngles),
			new LuaField("localEulerAngles", get_localEulerAngles, set_localEulerAngles),
			new LuaField("right", get_right, set_right),
			new LuaField("up", get_up, set_up),
			new LuaField("forward", get_forward, set_forward),
			new LuaField("rotation", get_rotation, set_rotation),
			new LuaField("localRotation", get_localRotation, set_localRotation),
			new LuaField("localScale", get_localScale, set_localScale),
			new LuaField("parent", get_parent, set_parent),
			new LuaField("worldToLocalMatrix", get_worldToLocalMatrix, null),
			new LuaField("localToWorldMatrix", get_localToWorldMatrix, null),
			new LuaField("root", get_root, null),
			new LuaField("childCount", get_childCount, null),
			new LuaField("lossyScale", get_lossyScale, null),
			new LuaField("hasChanged", get_hasChanged, set_hasChanged),
			new LuaField("hierarchyCapacity", get_hierarchyCapacity, set_hierarchyCapacity),
			new LuaField("hierarchyCount", get_hierarchyCount, null),
		};

		LuaScriptMgr.RegisterLib(L, "UnityEngine.Transform", typeof(Transform), regs, fields, typeof(Component));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateTransform(IntPtr L)
	{
		LuaDLL.luaL_error(L, "Transform class does not have a constructor function");
		return 0;
	}

	static Type classType = typeof(Transform);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_position(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Transform obj = (Transform)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name position");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index position on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.position);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_localPosition(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Transform obj = (Transform)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name localPosition");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index localPosition on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.localPosition);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_eulerAngles(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Transform obj = (Transform)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name eulerAngles");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index eulerAngles on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.eulerAngles);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_localEulerAngles(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Transform obj = (Transform)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name localEulerAngles");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index localEulerAngles on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.localEulerAngles);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_right(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Transform obj = (Transform)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name right");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index right on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.right);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_up(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Transform obj = (Transform)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name up");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index up on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.up);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_forward(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Transform obj = (Transform)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name forward");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index forward on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.forward);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_rotation(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Transform obj = (Transform)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name rotation");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index rotation on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.rotation);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_localRotation(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Transform obj = (Transform)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name localRotation");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index localRotation on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.localRotation);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_localScale(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Transform obj = (Transform)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name localScale");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index localScale on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.localScale);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_parent(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Transform obj = (Transform)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name parent");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index parent on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.parent);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_worldToLocalMatrix(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Transform obj = (Transform)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name worldToLocalMatrix");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index worldToLocalMatrix on a nil value");
			}
		}

		LuaScriptMgr.PushValue(L, obj.worldToLocalMatrix);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_localToWorldMatrix(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Transform obj = (Transform)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name localToWorldMatrix");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index localToWorldMatrix on a nil value");
			}
		}

		LuaScriptMgr.PushValue(L, obj.localToWorldMatrix);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_root(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Transform obj = (Transform)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name root");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index root on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.root);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_childCount(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Transform obj = (Transform)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name childCount");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index childCount on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.childCount);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_lossyScale(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Transform obj = (Transform)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name lossyScale");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index lossyScale on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.lossyScale);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_hasChanged(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Transform obj = (Transform)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name hasChanged");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index hasChanged on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.hasChanged);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_hierarchyCapacity(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Transform obj = (Transform)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name hierarchyCapacity");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index hierarchyCapacity on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.hierarchyCapacity);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_hierarchyCount(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Transform obj = (Transform)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name hierarchyCount");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index hierarchyCount on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.hierarchyCount);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_position(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Transform obj = (Transform)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name position");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index position on a nil value");
			}
		}

		obj.position = LuaScriptMgr.GetVector3(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_localPosition(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Transform obj = (Transform)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name localPosition");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index localPosition on a nil value");
			}
		}

		obj.localPosition = LuaScriptMgr.GetVector3(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_eulerAngles(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Transform obj = (Transform)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name eulerAngles");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index eulerAngles on a nil value");
			}
		}

		obj.eulerAngles = LuaScriptMgr.GetVector3(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_localEulerAngles(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Transform obj = (Transform)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name localEulerAngles");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index localEulerAngles on a nil value");
			}
		}

		obj.localEulerAngles = LuaScriptMgr.GetVector3(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_right(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Transform obj = (Transform)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name right");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index right on a nil value");
			}
		}

		obj.right = LuaScriptMgr.GetVector3(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_up(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Transform obj = (Transform)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name up");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index up on a nil value");
			}
		}

		obj.up = LuaScriptMgr.GetVector3(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_forward(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Transform obj = (Transform)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name forward");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index forward on a nil value");
			}
		}

		obj.forward = LuaScriptMgr.GetVector3(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_rotation(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Transform obj = (Transform)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name rotation");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index rotation on a nil value");
			}
		}

		obj.rotation = LuaScriptMgr.GetQuaternion(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_localRotation(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Transform obj = (Transform)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name localRotation");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index localRotation on a nil value");
			}
		}

		obj.localRotation = LuaScriptMgr.GetQuaternion(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_localScale(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Transform obj = (Transform)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name localScale");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index localScale on a nil value");
			}
		}

		obj.localScale = LuaScriptMgr.GetVector3(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_parent(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Transform obj = (Transform)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name parent");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index parent on a nil value");
			}
		}

		obj.parent = (Transform)LuaScriptMgr.GetUnityObject(L, 3, typeof(Transform));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_hasChanged(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Transform obj = (Transform)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name hasChanged");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index hasChanged on a nil value");
			}
		}

		obj.hasChanged = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_hierarchyCapacity(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Transform obj = (Transform)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name hierarchyCapacity");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index hierarchyCapacity on a nil value");
			}
		}

		obj.hierarchyCapacity = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetParent(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2)
		{
			Transform obj = (Transform)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Transform");
			Transform arg0 = (Transform)LuaScriptMgr.GetUnityObject(L, 2, typeof(Transform));
			obj.SetParent(arg0);
			return 0;
		}
		else if (count == 3)
		{
			Transform obj = (Transform)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Transform");
			Transform arg0 = (Transform)LuaScriptMgr.GetUnityObject(L, 2, typeof(Transform));
			bool arg1 = LuaScriptMgr.GetBoolean(L, 3);
			obj.SetParent(arg0,arg1);
			return 0;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: Transform.SetParent");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetPositionAndRotation(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		Transform obj = (Transform)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Transform");
		Vector3 arg0 = LuaScriptMgr.GetVector3(L, 2);
		Quaternion arg1 = LuaScriptMgr.GetQuaternion(L, 3);
		obj.SetPositionAndRotation(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Translate(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2)
		{
			Transform obj = (Transform)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Transform");
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 2);
			obj.Translate(arg0);
			return 0;
		}
		else if (count == 3 && LuaScriptMgr.CheckTypes(L, 1, typeof(Transform), typeof(LuaTable), typeof(Space)))
		{
			Transform obj = (Transform)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Transform");
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 2);
			Space arg1 = (Space)LuaScriptMgr.GetLuaObject(L, 3);
			obj.Translate(arg0,arg1);
			return 0;
		}
		else if (count == 3 && LuaScriptMgr.CheckTypes(L, 1, typeof(Transform), typeof(LuaTable), typeof(Transform)))
		{
			Transform obj = (Transform)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Transform");
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 2);
			Transform arg1 = (Transform)LuaScriptMgr.GetLuaObject(L, 3);
			obj.Translate(arg0,arg1);
			return 0;
		}
		else if (count == 4)
		{
			Transform obj = (Transform)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Transform");
			float arg0 = (float)LuaScriptMgr.GetNumber(L, 2);
			float arg1 = (float)LuaScriptMgr.GetNumber(L, 3);
			float arg2 = (float)LuaScriptMgr.GetNumber(L, 4);
			obj.Translate(arg0,arg1,arg2);
			return 0;
		}
		else if (count == 5 && LuaScriptMgr.CheckTypes(L, 1, typeof(Transform), typeof(float), typeof(float), typeof(float), typeof(Space)))
		{
			Transform obj = (Transform)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Transform");
			float arg0 = (float)LuaDLL.lua_tonumber(L, 2);
			float arg1 = (float)LuaDLL.lua_tonumber(L, 3);
			float arg2 = (float)LuaDLL.lua_tonumber(L, 4);
			Space arg3 = (Space)LuaScriptMgr.GetLuaObject(L, 5);
			obj.Translate(arg0,arg1,arg2,arg3);
			return 0;
		}
		else if (count == 5 && LuaScriptMgr.CheckTypes(L, 1, typeof(Transform), typeof(float), typeof(float), typeof(float), typeof(Transform)))
		{
			Transform obj = (Transform)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Transform");
			float arg0 = (float)LuaDLL.lua_tonumber(L, 2);
			float arg1 = (float)LuaDLL.lua_tonumber(L, 3);
			float arg2 = (float)LuaDLL.lua_tonumber(L, 4);
			Transform arg3 = (Transform)LuaScriptMgr.GetLuaObject(L, 5);
			obj.Translate(arg0,arg1,arg2,arg3);
			return 0;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: Transform.Translate");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Rotate(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2)
		{
			Transform obj = (Transform)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Transform");
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 2);
			obj.Rotate(arg0);
			return 0;
		}
		else if (count == 3 && LuaScriptMgr.CheckTypes(L, 1, typeof(Transform), typeof(LuaTable), typeof(Space)))
		{
			Transform obj = (Transform)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Transform");
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 2);
			Space arg1 = (Space)LuaScriptMgr.GetLuaObject(L, 3);
			obj.Rotate(arg0,arg1);
			return 0;
		}
		else if (count == 3 && LuaScriptMgr.CheckTypes(L, 1, typeof(Transform), typeof(LuaTable), typeof(float)))
		{
			Transform obj = (Transform)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Transform");
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 2);
			float arg1 = (float)LuaDLL.lua_tonumber(L, 3);
			obj.Rotate(arg0,arg1);
			return 0;
		}
		else if (count == 4 && LuaScriptMgr.CheckTypes(L, 1, typeof(Transform), typeof(float), typeof(float), typeof(float)))
		{
			Transform obj = (Transform)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Transform");
			float arg0 = (float)LuaDLL.lua_tonumber(L, 2);
			float arg1 = (float)LuaDLL.lua_tonumber(L, 3);
			float arg2 = (float)LuaDLL.lua_tonumber(L, 4);
			obj.Rotate(arg0,arg1,arg2);
			return 0;
		}
		else if (count == 4 && LuaScriptMgr.CheckTypes(L, 1, typeof(Transform), typeof(LuaTable), typeof(float), typeof(Space)))
		{
			Transform obj = (Transform)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Transform");
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 2);
			float arg1 = (float)LuaDLL.lua_tonumber(L, 3);
			Space arg2 = (Space)LuaScriptMgr.GetLuaObject(L, 4);
			obj.Rotate(arg0,arg1,arg2);
			return 0;
		}
		else if (count == 5)
		{
			Transform obj = (Transform)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Transform");
			float arg0 = (float)LuaScriptMgr.GetNumber(L, 2);
			float arg1 = (float)LuaScriptMgr.GetNumber(L, 3);
			float arg2 = (float)LuaScriptMgr.GetNumber(L, 4);
			Space arg3 = (Space)LuaScriptMgr.GetNetObject(L, 5, typeof(Space));
			obj.Rotate(arg0,arg1,arg2,arg3);
			return 0;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: Transform.Rotate");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int RotateAround(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 4);
		Transform obj = (Transform)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Transform");
		Vector3 arg0 = LuaScriptMgr.GetVector3(L, 2);
		Vector3 arg1 = LuaScriptMgr.GetVector3(L, 3);
		float arg2 = (float)LuaScriptMgr.GetNumber(L, 4);
		obj.RotateAround(arg0,arg1,arg2);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int LookAt(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(Transform), typeof(Transform)))
		{
			Transform obj = (Transform)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Transform");
			Transform arg0 = (Transform)LuaScriptMgr.GetLuaObject(L, 2);
			obj.LookAt(arg0);
			return 0;
		}
		else if (count == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(Transform), typeof(LuaTable)))
		{
			Transform obj = (Transform)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Transform");
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 2);
			obj.LookAt(arg0);
			return 0;
		}
		else if (count == 3 && LuaScriptMgr.CheckTypes(L, 1, typeof(Transform), typeof(Transform), typeof(LuaTable)))
		{
			Transform obj = (Transform)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Transform");
			Transform arg0 = (Transform)LuaScriptMgr.GetLuaObject(L, 2);
			Vector3 arg1 = LuaScriptMgr.GetVector3(L, 3);
			obj.LookAt(arg0,arg1);
			return 0;
		}
		else if (count == 3 && LuaScriptMgr.CheckTypes(L, 1, typeof(Transform), typeof(LuaTable), typeof(LuaTable)))
		{
			Transform obj = (Transform)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Transform");
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 2);
			Vector3 arg1 = LuaScriptMgr.GetVector3(L, 3);
			obj.LookAt(arg0,arg1);
			return 0;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: Transform.LookAt");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int TransformDirection(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2)
		{
			Transform obj = (Transform)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Transform");
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 2);
			Vector3 o = obj.TransformDirection(arg0);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 4)
		{
			Transform obj = (Transform)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Transform");
			float arg0 = (float)LuaScriptMgr.GetNumber(L, 2);
			float arg1 = (float)LuaScriptMgr.GetNumber(L, 3);
			float arg2 = (float)LuaScriptMgr.GetNumber(L, 4);
			Vector3 o = obj.TransformDirection(arg0,arg1,arg2);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: Transform.TransformDirection");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int InverseTransformDirection(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2)
		{
			Transform obj = (Transform)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Transform");
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 2);
			Vector3 o = obj.InverseTransformDirection(arg0);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 4)
		{
			Transform obj = (Transform)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Transform");
			float arg0 = (float)LuaScriptMgr.GetNumber(L, 2);
			float arg1 = (float)LuaScriptMgr.GetNumber(L, 3);
			float arg2 = (float)LuaScriptMgr.GetNumber(L, 4);
			Vector3 o = obj.InverseTransformDirection(arg0,arg1,arg2);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: Transform.InverseTransformDirection");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int TransformVector(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2)
		{
			Transform obj = (Transform)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Transform");
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 2);
			Vector3 o = obj.TransformVector(arg0);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 4)
		{
			Transform obj = (Transform)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Transform");
			float arg0 = (float)LuaScriptMgr.GetNumber(L, 2);
			float arg1 = (float)LuaScriptMgr.GetNumber(L, 3);
			float arg2 = (float)LuaScriptMgr.GetNumber(L, 4);
			Vector3 o = obj.TransformVector(arg0,arg1,arg2);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: Transform.TransformVector");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int InverseTransformVector(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2)
		{
			Transform obj = (Transform)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Transform");
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 2);
			Vector3 o = obj.InverseTransformVector(arg0);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 4)
		{
			Transform obj = (Transform)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Transform");
			float arg0 = (float)LuaScriptMgr.GetNumber(L, 2);
			float arg1 = (float)LuaScriptMgr.GetNumber(L, 3);
			float arg2 = (float)LuaScriptMgr.GetNumber(L, 4);
			Vector3 o = obj.InverseTransformVector(arg0,arg1,arg2);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: Transform.InverseTransformVector");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int TransformPoint(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2)
		{
			Transform obj = (Transform)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Transform");
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 2);
			Vector3 o = obj.TransformPoint(arg0);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 4)
		{
			Transform obj = (Transform)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Transform");
			float arg0 = (float)LuaScriptMgr.GetNumber(L, 2);
			float arg1 = (float)LuaScriptMgr.GetNumber(L, 3);
			float arg2 = (float)LuaScriptMgr.GetNumber(L, 4);
			Vector3 o = obj.TransformPoint(arg0,arg1,arg2);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: Transform.TransformPoint");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int InverseTransformPoint(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2)
		{
			Transform obj = (Transform)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Transform");
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 2);
			Vector3 o = obj.InverseTransformPoint(arg0);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 4)
		{
			Transform obj = (Transform)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Transform");
			float arg0 = (float)LuaScriptMgr.GetNumber(L, 2);
			float arg1 = (float)LuaScriptMgr.GetNumber(L, 3);
			float arg2 = (float)LuaScriptMgr.GetNumber(L, 4);
			Vector3 o = obj.InverseTransformPoint(arg0,arg1,arg2);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: Transform.InverseTransformPoint");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int DetachChildren(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		Transform obj = (Transform)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Transform");
		obj.DetachChildren();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetAsFirstSibling(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		Transform obj = (Transform)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Transform");
		obj.SetAsFirstSibling();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetAsLastSibling(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		Transform obj = (Transform)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Transform");
		obj.SetAsLastSibling();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetSiblingIndex(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		Transform obj = (Transform)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Transform");
		int arg0 = (int)LuaScriptMgr.GetNumber(L, 2);
		obj.SetSiblingIndex(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetSiblingIndex(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		Transform obj = (Transform)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Transform");
		int o = obj.GetSiblingIndex();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Find(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		Transform obj = (Transform)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Transform");
		string arg0 = LuaScriptMgr.GetLuaString(L, 2);
		Transform o = obj.Find(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IsChildOf(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		Transform obj = (Transform)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Transform");
		Transform arg0 = (Transform)LuaScriptMgr.GetUnityObject(L, 2, typeof(Transform));
		bool o = obj.IsChildOf(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetEnumerator(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		Transform obj = (Transform)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Transform");
		IEnumerator o = obj.GetEnumerator();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetChild(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		Transform obj = (Transform)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Transform");
		int arg0 = (int)LuaScriptMgr.GetNumber(L, 2);
		Transform o = obj.GetChild(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int DOBlendableScaleBy(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		Transform obj = (Transform)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Transform");
		Vector3 arg0 = LuaScriptMgr.GetVector3(L, 2);
		float arg1 = (float)LuaScriptMgr.GetNumber(L, 3);
		DG.Tweening.Tweener o = obj.DOBlendableScaleBy(arg0,arg1);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int DOBlendableLocalRotateBy(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 4);
		Transform obj = (Transform)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Transform");
		Vector3 arg0 = LuaScriptMgr.GetVector3(L, 2);
		float arg1 = (float)LuaScriptMgr.GetNumber(L, 3);
		DG.Tweening.RotateMode arg2 = (DG.Tweening.RotateMode)LuaScriptMgr.GetNetObject(L, 4, typeof(DG.Tweening.RotateMode));
		DG.Tweening.Tweener o = obj.DOBlendableLocalRotateBy(arg0,arg1,arg2);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int DOBlendableRotateBy(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 4);
		Transform obj = (Transform)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Transform");
		Vector3 arg0 = LuaScriptMgr.GetVector3(L, 2);
		float arg1 = (float)LuaScriptMgr.GetNumber(L, 3);
		DG.Tweening.RotateMode arg2 = (DG.Tweening.RotateMode)LuaScriptMgr.GetNetObject(L, 4, typeof(DG.Tweening.RotateMode));
		DG.Tweening.Tweener o = obj.DOBlendableRotateBy(arg0,arg1,arg2);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int DOBlendableLocalMoveBy(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 4);
		Transform obj = (Transform)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Transform");
		Vector3 arg0 = LuaScriptMgr.GetVector3(L, 2);
		float arg1 = (float)LuaScriptMgr.GetNumber(L, 3);
		bool arg2 = LuaScriptMgr.GetBoolean(L, 4);
		DG.Tweening.Tweener o = obj.DOBlendableLocalMoveBy(arg0,arg1,arg2);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int DOBlendableMoveBy(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 4);
		Transform obj = (Transform)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Transform");
		Vector3 arg0 = LuaScriptMgr.GetVector3(L, 2);
		float arg1 = (float)LuaScriptMgr.GetNumber(L, 3);
		bool arg2 = LuaScriptMgr.GetBoolean(L, 4);
		DG.Tweening.Tweener o = obj.DOBlendableMoveBy(arg0,arg1,arg2);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int DOLocalPath(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 7);
		Transform obj = (Transform)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Transform");
		Vector3[] objs0 = LuaScriptMgr.GetArrayObject<Vector3>(L, 2);
		float arg1 = (float)LuaScriptMgr.GetNumber(L, 3);
		DG.Tweening.PathType arg2 = (DG.Tweening.PathType)LuaScriptMgr.GetNetObject(L, 4, typeof(DG.Tweening.PathType));
		DG.Tweening.PathMode arg3 = (DG.Tweening.PathMode)LuaScriptMgr.GetNetObject(L, 5, typeof(DG.Tweening.PathMode));
		int arg4 = (int)LuaScriptMgr.GetNumber(L, 6);
		Nullable<Color> arg5 = (Nullable<Color>)LuaScriptMgr.GetNetObject(L, 7, typeof(Nullable<Color>));
		DG.Tweening.Core.TweenerCore<Vector3,DG.Tweening.Plugins.Core.PathCore.Path,DG.Tweening.Plugins.Options.PathOptions> o = obj.DOLocalPath(objs0,arg1,arg2,arg3,arg4,arg5);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int DOPath(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 7);
		Transform obj = (Transform)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Transform");
		Vector3[] objs0 = LuaScriptMgr.GetArrayObject<Vector3>(L, 2);
		float arg1 = (float)LuaScriptMgr.GetNumber(L, 3);
		DG.Tweening.PathType arg2 = (DG.Tweening.PathType)LuaScriptMgr.GetNetObject(L, 4, typeof(DG.Tweening.PathType));
		DG.Tweening.PathMode arg3 = (DG.Tweening.PathMode)LuaScriptMgr.GetNetObject(L, 5, typeof(DG.Tweening.PathMode));
		int arg4 = (int)LuaScriptMgr.GetNumber(L, 6);
		Nullable<Color> arg5 = (Nullable<Color>)LuaScriptMgr.GetNetObject(L, 7, typeof(Nullable<Color>));
		DG.Tweening.Core.TweenerCore<Vector3,DG.Tweening.Plugins.Core.PathCore.Path,DG.Tweening.Plugins.Options.PathOptions> o = obj.DOPath(objs0,arg1,arg2,arg3,arg4,arg5);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int DOLocalJump(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 6);
		Transform obj = (Transform)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Transform");
		Vector3 arg0 = LuaScriptMgr.GetVector3(L, 2);
		float arg1 = (float)LuaScriptMgr.GetNumber(L, 3);
		int arg2 = (int)LuaScriptMgr.GetNumber(L, 4);
		float arg3 = (float)LuaScriptMgr.GetNumber(L, 5);
		bool arg4 = LuaScriptMgr.GetBoolean(L, 6);
		DG.Tweening.Sequence o = obj.DOLocalJump(arg0,arg1,arg2,arg3,arg4);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int DOJump(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 6);
		Transform obj = (Transform)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Transform");
		Vector3 arg0 = LuaScriptMgr.GetVector3(L, 2);
		float arg1 = (float)LuaScriptMgr.GetNumber(L, 3);
		int arg2 = (int)LuaScriptMgr.GetNumber(L, 4);
		float arg3 = (float)LuaScriptMgr.GetNumber(L, 5);
		bool arg4 = LuaScriptMgr.GetBoolean(L, 6);
		DG.Tweening.Sequence o = obj.DOJump(arg0,arg1,arg2,arg3,arg4);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int DOShakeScale(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 5 && LuaScriptMgr.CheckTypes(L, 1, typeof(Transform), typeof(float), typeof(LuaTable), typeof(int), typeof(float)))
		{
			Transform obj = (Transform)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Transform");
			float arg0 = (float)LuaDLL.lua_tonumber(L, 2);
			Vector3 arg1 = LuaScriptMgr.GetVector3(L, 3);
			int arg2 = (int)LuaDLL.lua_tonumber(L, 4);
			float arg3 = (float)LuaDLL.lua_tonumber(L, 5);
			DG.Tweening.Tweener o = obj.DOShakeScale(arg0,arg1,arg2,arg3);
			LuaScriptMgr.PushObject(L, o);
			return 1;
		}
		else if (count == 5 && LuaScriptMgr.CheckTypes(L, 1, typeof(Transform), typeof(float), typeof(float), typeof(int), typeof(float)))
		{
			Transform obj = (Transform)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Transform");
			float arg0 = (float)LuaDLL.lua_tonumber(L, 2);
			float arg1 = (float)LuaDLL.lua_tonumber(L, 3);
			int arg2 = (int)LuaDLL.lua_tonumber(L, 4);
			float arg3 = (float)LuaDLL.lua_tonumber(L, 5);
			DG.Tweening.Tweener o = obj.DOShakeScale(arg0,arg1,arg2,arg3);
			LuaScriptMgr.PushObject(L, o);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: Transform.DOShakeScale");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int DOShakeRotation(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 5 && LuaScriptMgr.CheckTypes(L, 1, typeof(Transform), typeof(float), typeof(LuaTable), typeof(int), typeof(float)))
		{
			Transform obj = (Transform)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Transform");
			float arg0 = (float)LuaDLL.lua_tonumber(L, 2);
			Vector3 arg1 = LuaScriptMgr.GetVector3(L, 3);
			int arg2 = (int)LuaDLL.lua_tonumber(L, 4);
			float arg3 = (float)LuaDLL.lua_tonumber(L, 5);
			DG.Tweening.Tweener o = obj.DOShakeRotation(arg0,arg1,arg2,arg3);
			LuaScriptMgr.PushObject(L, o);
			return 1;
		}
		else if (count == 5 && LuaScriptMgr.CheckTypes(L, 1, typeof(Transform), typeof(float), typeof(float), typeof(int), typeof(float)))
		{
			Transform obj = (Transform)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Transform");
			float arg0 = (float)LuaDLL.lua_tonumber(L, 2);
			float arg1 = (float)LuaDLL.lua_tonumber(L, 3);
			int arg2 = (int)LuaDLL.lua_tonumber(L, 4);
			float arg3 = (float)LuaDLL.lua_tonumber(L, 5);
			DG.Tweening.Tweener o = obj.DOShakeRotation(arg0,arg1,arg2,arg3);
			LuaScriptMgr.PushObject(L, o);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: Transform.DOShakeRotation");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int DOShakePosition(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 6 && LuaScriptMgr.CheckTypes(L, 1, typeof(Transform), typeof(float), typeof(LuaTable), typeof(int), typeof(float), typeof(bool)))
		{
			Transform obj = (Transform)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Transform");
			float arg0 = (float)LuaDLL.lua_tonumber(L, 2);
			Vector3 arg1 = LuaScriptMgr.GetVector3(L, 3);
			int arg2 = (int)LuaDLL.lua_tonumber(L, 4);
			float arg3 = (float)LuaDLL.lua_tonumber(L, 5);
			bool arg4 = LuaDLL.lua_toboolean(L, 6);
			DG.Tweening.Tweener o = obj.DOShakePosition(arg0,arg1,arg2,arg3,arg4);
			LuaScriptMgr.PushObject(L, o);
			return 1;
		}
		else if (count == 6 && LuaScriptMgr.CheckTypes(L, 1, typeof(Transform), typeof(float), typeof(float), typeof(int), typeof(float), typeof(bool)))
		{
			Transform obj = (Transform)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Transform");
			float arg0 = (float)LuaDLL.lua_tonumber(L, 2);
			float arg1 = (float)LuaDLL.lua_tonumber(L, 3);
			int arg2 = (int)LuaDLL.lua_tonumber(L, 4);
			float arg3 = (float)LuaDLL.lua_tonumber(L, 5);
			bool arg4 = LuaDLL.lua_toboolean(L, 6);
			DG.Tweening.Tweener o = obj.DOShakePosition(arg0,arg1,arg2,arg3,arg4);
			LuaScriptMgr.PushObject(L, o);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: Transform.DOShakePosition");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int DOPunchRotation(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 5);
		Transform obj = (Transform)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Transform");
		Vector3 arg0 = LuaScriptMgr.GetVector3(L, 2);
		float arg1 = (float)LuaScriptMgr.GetNumber(L, 3);
		int arg2 = (int)LuaScriptMgr.GetNumber(L, 4);
		float arg3 = (float)LuaScriptMgr.GetNumber(L, 5);
		DG.Tweening.Tweener o = obj.DOPunchRotation(arg0,arg1,arg2,arg3);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int DOPunchScale(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 5);
		Transform obj = (Transform)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Transform");
		Vector3 arg0 = LuaScriptMgr.GetVector3(L, 2);
		float arg1 = (float)LuaScriptMgr.GetNumber(L, 3);
		int arg2 = (int)LuaScriptMgr.GetNumber(L, 4);
		float arg3 = (float)LuaScriptMgr.GetNumber(L, 5);
		DG.Tweening.Tweener o = obj.DOPunchScale(arg0,arg1,arg2,arg3);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int DOPunchPosition(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 6);
		Transform obj = (Transform)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Transform");
		Vector3 arg0 = LuaScriptMgr.GetVector3(L, 2);
		float arg1 = (float)LuaScriptMgr.GetNumber(L, 3);
		int arg2 = (int)LuaScriptMgr.GetNumber(L, 4);
		float arg3 = (float)LuaScriptMgr.GetNumber(L, 5);
		bool arg4 = LuaScriptMgr.GetBoolean(L, 6);
		DG.Tweening.Tweener o = obj.DOPunchPosition(arg0,arg1,arg2,arg3,arg4);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int DOLookAt(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 5);
		Transform obj = (Transform)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Transform");
		Vector3 arg0 = LuaScriptMgr.GetVector3(L, 2);
		float arg1 = (float)LuaScriptMgr.GetNumber(L, 3);
		DG.Tweening.AxisConstraint arg2 = (DG.Tweening.AxisConstraint)LuaScriptMgr.GetNetObject(L, 4, typeof(DG.Tweening.AxisConstraint));
		Nullable<Vector3> arg3 = (Nullable<Vector3>)LuaScriptMgr.GetNetObject(L, 5, typeof(Nullable<Vector3>));
		DG.Tweening.Tweener o = obj.DOLookAt(arg0,arg1,arg2,arg3);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int DOScaleZ(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		Transform obj = (Transform)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Transform");
		float arg0 = (float)LuaScriptMgr.GetNumber(L, 2);
		float arg1 = (float)LuaScriptMgr.GetNumber(L, 3);
		DG.Tweening.Tweener o = obj.DOScaleZ(arg0,arg1);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int DOScaleY(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		Transform obj = (Transform)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Transform");
		float arg0 = (float)LuaScriptMgr.GetNumber(L, 2);
		float arg1 = (float)LuaScriptMgr.GetNumber(L, 3);
		DG.Tweening.Tweener o = obj.DOScaleY(arg0,arg1);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int DOScaleX(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		Transform obj = (Transform)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Transform");
		float arg0 = (float)LuaScriptMgr.GetNumber(L, 2);
		float arg1 = (float)LuaScriptMgr.GetNumber(L, 3);
		DG.Tweening.Tweener o = obj.DOScaleX(arg0,arg1);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int DOScale(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 3 && LuaScriptMgr.CheckTypes(L, 1, typeof(Transform), typeof(float), typeof(float)))
		{
			Transform obj = (Transform)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Transform");
			float arg0 = (float)LuaDLL.lua_tonumber(L, 2);
			float arg1 = (float)LuaDLL.lua_tonumber(L, 3);
			DG.Tweening.Tweener o = obj.DOScale(arg0,arg1);
			LuaScriptMgr.PushObject(L, o);
			return 1;
		}
		else if (count == 3 && LuaScriptMgr.CheckTypes(L, 1, typeof(Transform), typeof(LuaTable), typeof(float)))
		{
			Transform obj = (Transform)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Transform");
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 2);
			float arg1 = (float)LuaDLL.lua_tonumber(L, 3);
			DG.Tweening.Tweener o = obj.DOScale(arg0,arg1);
			LuaScriptMgr.PushObject(L, o);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: Transform.DOScale");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int DOLocalRotate(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 4);
		Transform obj = (Transform)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Transform");
		Vector3 arg0 = LuaScriptMgr.GetVector3(L, 2);
		float arg1 = (float)LuaScriptMgr.GetNumber(L, 3);
		DG.Tweening.RotateMode arg2 = (DG.Tweening.RotateMode)LuaScriptMgr.GetNetObject(L, 4, typeof(DG.Tweening.RotateMode));
		DG.Tweening.Tweener o = obj.DOLocalRotate(arg0,arg1,arg2);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int DORotate(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 4);
		Transform obj = (Transform)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Transform");
		Vector3 arg0 = LuaScriptMgr.GetVector3(L, 2);
		float arg1 = (float)LuaScriptMgr.GetNumber(L, 3);
		DG.Tweening.RotateMode arg2 = (DG.Tweening.RotateMode)LuaScriptMgr.GetNetObject(L, 4, typeof(DG.Tweening.RotateMode));
		DG.Tweening.Tweener o = obj.DORotate(arg0,arg1,arg2);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int DOLocalMoveZ(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 4);
		Transform obj = (Transform)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Transform");
		float arg0 = (float)LuaScriptMgr.GetNumber(L, 2);
		float arg1 = (float)LuaScriptMgr.GetNumber(L, 3);
		bool arg2 = LuaScriptMgr.GetBoolean(L, 4);
		DG.Tweening.Tweener o = obj.DOLocalMoveZ(arg0,arg1,arg2);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int DOLocalMoveY(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 4);
		Transform obj = (Transform)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Transform");
		float arg0 = (float)LuaScriptMgr.GetNumber(L, 2);
		float arg1 = (float)LuaScriptMgr.GetNumber(L, 3);
		bool arg2 = LuaScriptMgr.GetBoolean(L, 4);
		DG.Tweening.Tweener o = obj.DOLocalMoveY(arg0,arg1,arg2);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int DOLocalMoveX(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 4);
		Transform obj = (Transform)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Transform");
		float arg0 = (float)LuaScriptMgr.GetNumber(L, 2);
		float arg1 = (float)LuaScriptMgr.GetNumber(L, 3);
		bool arg2 = LuaScriptMgr.GetBoolean(L, 4);
		DG.Tweening.Tweener o = obj.DOLocalMoveX(arg0,arg1,arg2);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int DOLocalMove(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 4);
		Transform obj = (Transform)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Transform");
		Vector3 arg0 = LuaScriptMgr.GetVector3(L, 2);
		float arg1 = (float)LuaScriptMgr.GetNumber(L, 3);
		bool arg2 = LuaScriptMgr.GetBoolean(L, 4);
		DG.Tweening.Tweener o = obj.DOLocalMove(arg0,arg1,arg2);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int DOMoveZ(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 4);
		Transform obj = (Transform)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Transform");
		float arg0 = (float)LuaScriptMgr.GetNumber(L, 2);
		float arg1 = (float)LuaScriptMgr.GetNumber(L, 3);
		bool arg2 = LuaScriptMgr.GetBoolean(L, 4);
		DG.Tweening.Tweener o = obj.DOMoveZ(arg0,arg1,arg2);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int DOMoveY(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 4);
		Transform obj = (Transform)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Transform");
		float arg0 = (float)LuaScriptMgr.GetNumber(L, 2);
		float arg1 = (float)LuaScriptMgr.GetNumber(L, 3);
		bool arg2 = LuaScriptMgr.GetBoolean(L, 4);
		DG.Tweening.Tweener o = obj.DOMoveY(arg0,arg1,arg2);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int DOMoveX(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 4);
		Transform obj = (Transform)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Transform");
		float arg0 = (float)LuaScriptMgr.GetNumber(L, 2);
		float arg1 = (float)LuaScriptMgr.GetNumber(L, 3);
		bool arg2 = LuaScriptMgr.GetBoolean(L, 4);
		DG.Tweening.Tweener o = obj.DOMoveX(arg0,arg1,arg2);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int DOMove(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 4);
		Transform obj = (Transform)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Transform");
		Vector3 arg0 = LuaScriptMgr.GetVector3(L, 2);
		float arg1 = (float)LuaScriptMgr.GetNumber(L, 3);
		bool arg2 = LuaScriptMgr.GetBoolean(L, 4);
		DG.Tweening.Tweener o = obj.DOMove(arg0,arg1,arg2);
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

