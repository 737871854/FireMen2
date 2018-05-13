using System;
using UnityEngine;
using System.Collections.Generic;
using LuaInterface;

public static class DelegateFactory
{
	delegate Delegate DelegateValue(LuaFunction func);
	static Dictionary<Type, DelegateValue> dict = new Dictionary<Type, DelegateValue>();

	[NoToLuaAttribute]
	public static void Register(IntPtr L)
	{
		dict.Add(typeof(Action<GameObject>), new DelegateValue(Action_GameObject));
		dict.Add(typeof(Action), new DelegateValue(Action));
		dict.Add(typeof(UnityEngine.Events.UnityAction), new DelegateValue(UnityEngine_Events_UnityAction));
		dict.Add(typeof(System.Reflection.MemberFilter), new DelegateValue(System_Reflection_MemberFilter));
        dict.Add(typeof(Func<System.Reflection.AssemblyName, System.Reflection.Assembly>), new DelegateValue(Func_System_Reflection_AssemblyName_System_Reflection_Assembly));
        dict.Add(typeof(Func<System.Reflection.Assembly, string, bool, Type>), new DelegateValue(Func_System_Reflection_Assembly_string_bool_Type));
		dict.Add(typeof(System.Reflection.TypeFilter), new DelegateValue(System_Reflection_TypeFilter));
		dict.Add(typeof(DG.Tweening.Core.DOGetter<float>), new DelegateValue(DOGetter_float));
		dict.Add(typeof(DG.Tweening.Core.DOSetter<float>), new DelegateValue(DOSetter_float));
		dict.Add(typeof(DG.Tweening.Core.DOGetter<int>), new DelegateValue(DOGetter_int));
		dict.Add(typeof(DG.Tweening.Core.DOSetter<int>), new DelegateValue(DOSetter_int));
		dict.Add(typeof(DG.Tweening.Core.DOGetter<uint>), new DelegateValue(DOGetter_uint));
		dict.Add(typeof(DG.Tweening.Core.DOSetter<uint>), new DelegateValue(DOSetter_uint));
		dict.Add(typeof(DG.Tweening.Core.DOGetter<long>), new DelegateValue(DOGetter_long));
		dict.Add(typeof(DG.Tweening.Core.DOSetter<long>), new DelegateValue(DOSetter_long));
		dict.Add(typeof(DG.Tweening.Core.DOGetter<ulong>), new DelegateValue(DOGetter_ulong));
		dict.Add(typeof(DG.Tweening.Core.DOSetter<ulong>), new DelegateValue(DOSetter_ulong));
		dict.Add(typeof(DG.Tweening.Core.DOGetter<string>), new DelegateValue(DOGetter_string));
		dict.Add(typeof(DG.Tweening.Core.DOSetter<string>), new DelegateValue(DOSetter_string));
		dict.Add(typeof(DG.Tweening.Core.DOGetter<Vector2>), new DelegateValue(DOGetter_Vector2));
		dict.Add(typeof(DG.Tweening.Core.DOSetter<Vector2>), new DelegateValue(DOSetter_Vector2));
		dict.Add(typeof(DG.Tweening.Core.DOGetter<Vector3>), new DelegateValue(DOGetter_Vector3));
		dict.Add(typeof(DG.Tweening.Core.DOSetter<Vector3>), new DelegateValue(DOSetter_Vector3));
		dict.Add(typeof(DG.Tweening.Core.DOGetter<Vector4>), new DelegateValue(DOGetter_Vector4));
		dict.Add(typeof(DG.Tweening.Core.DOSetter<Vector4>), new DelegateValue(DOSetter_Vector4));
		dict.Add(typeof(DG.Tweening.Core.DOGetter<Quaternion>), new DelegateValue(DOGetter_Quaternion));
		dict.Add(typeof(DG.Tweening.Core.DOSetter<Quaternion>), new DelegateValue(DOSetter_Quaternion));
		dict.Add(typeof(DG.Tweening.Core.DOGetter<Color>), new DelegateValue(DOGetter_Color));
		dict.Add(typeof(DG.Tweening.Core.DOSetter<Color>), new DelegateValue(DOSetter_Color));
		dict.Add(typeof(DG.Tweening.Core.DOGetter<Rect>), new DelegateValue(DOGetter_Rect));
		dict.Add(typeof(DG.Tweening.Core.DOSetter<Rect>), new DelegateValue(DOSetter_Rect));
		dict.Add(typeof(DG.Tweening.Core.DOGetter<RectOffset>), new DelegateValue(DOGetter_RectOffset));
		dict.Add(typeof(DG.Tweening.Core.DOSetter<RectOffset>), new DelegateValue(DOSetter_RectOffset));
		dict.Add(typeof(TimerTriggerCallback), new DelegateValue(TimerTriggerCallback));
		dict.Add(typeof(RectTransform.ReapplyDrivenProperties), new DelegateValue(RectTransform_ReapplyDrivenProperties));
		dict.Add(typeof(UtilCommon.VoidDelegate), new DelegateValue(UtilCommon_VoidDelegate));
		dict.Add(typeof(UtilCommon.OnTouchEventHandle), new DelegateValue(UtilCommon_OnTouchEventHandle));
		dict.Add(typeof(UtilCommon.OnIOEventHandle), new DelegateValue(UtilCommon_OnIOEventHandle));
		dict.Add(typeof(UtilCommon.EventHandle), new DelegateValue(UtilCommon_EventHandle));
		dict.Add(typeof(TestLuaDelegate.VoidDelegate), new DelegateValue(TestLuaDelegate_VoidDelegate));
		dict.Add(typeof(Camera.CameraCallback), new DelegateValue(Camera_CameraCallback));
		dict.Add(typeof(AudioClip.PCMReaderCallback), new DelegateValue(AudioClip_PCMReaderCallback));
		dict.Add(typeof(AudioClip.PCMSetPositionCallback), new DelegateValue(AudioClip_PCMSetPositionCallback));
		dict.Add(typeof(Application.LowMemoryCallback), new DelegateValue(Application_LowMemoryCallback));
		dict.Add(typeof(Application.AdvertisingIdentifierCallback), new DelegateValue(Application_AdvertisingIdentifierCallback));
		dict.Add(typeof(Application.LogCallback), new DelegateValue(Application_LogCallback));
	}

	[NoToLuaAttribute]
	public static Delegate CreateDelegate(Type t, LuaFunction func)
	{
		DelegateValue create = null;

		if (!dict.TryGetValue(t, out create))
		{
			Debugger.LogError("Delegate {0} not register", t.FullName);
			return null;
		}
		return create(func);
	}

	public static Delegate Action_GameObject(LuaFunction func)
	{
		Action<GameObject> d = (param0) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.Push(L, param0);
			func.PCall(top, 1);
			func.EndPCall(top);
		};
		return d;
	}

	public static Delegate Action(LuaFunction func)
	{
		Action d = () =>
		{
			func.Call();
		};
		return d;
	}

	public static Delegate UnityEngine_Events_UnityAction(LuaFunction func)
	{
		UnityEngine.Events.UnityAction d = () =>
		{
			func.Call();
		};
		return d;
	}

	public static Delegate System_Reflection_MemberFilter(LuaFunction func)
	{
		System.Reflection.MemberFilter d = (param0, param1) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.PushObject(L, param0);
			LuaScriptMgr.PushVarObject(L, param1);
			func.PCall(top, 2);
			object[] objs = func.PopValues(top);
			func.EndPCall(top);
			return (bool)objs[0];
		};
		return d;
	}

    public static Delegate Func_System_Reflection_AssemblyName_System_Reflection_Assembly(LuaFunction func)
	{
		Func<System.Reflection.AssemblyName,System.Reflection.Assembly> d = (param0) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.PushObject(L, param0);
			func.PCall(top, 1);
			object[] objs = func.PopValues(top);
			func.EndPCall(top);
			return (System.Reflection.Assembly)objs[0];
		};
		return d;
	}

    public static Delegate Func_System_Reflection_Assembly_string_bool_Type(LuaFunction func)
	{
		Func<System.Reflection.Assembly,string,bool,Type> d = (param0, param1, param2) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.PushObject(L, param0);
			LuaScriptMgr.Push(L, param1);
			LuaScriptMgr.Push(L, param2);
			func.PCall(top, 3);
			object[] objs = func.PopValues(top);
			func.EndPCall(top);
			return (Type)objs[0];
		};
		return d;
	}

	public static Delegate System_Reflection_TypeFilter(LuaFunction func)
	{
		System.Reflection.TypeFilter d = (param0, param1) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.Push(L, param0);
			LuaScriptMgr.PushVarObject(L, param1);
			func.PCall(top, 2);
			object[] objs = func.PopValues(top);
			func.EndPCall(top);
			return (bool)objs[0];
		};
		return d;
	}

	public static Delegate DOGetter_float(LuaFunction func)
	{
		DG.Tweening.Core.DOGetter<float> d = () =>
		{
			object[] objs = func.Call();
			return (float)objs[0];
		};
		return d;
	}

	public static Delegate DOSetter_float(LuaFunction func)
	{
		DG.Tweening.Core.DOSetter<float> d = (param0) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.Push(L, param0);
			func.PCall(top, 1);
			func.EndPCall(top);
		};
		return d;
	}

	public static Delegate DOGetter_int(LuaFunction func)
	{
		DG.Tweening.Core.DOGetter<int> d = () =>
		{
			object[] objs = func.Call();
			return (int)objs[0];
		};
		return d;
	}

	public static Delegate DOSetter_int(LuaFunction func)
	{
		DG.Tweening.Core.DOSetter<int> d = (param0) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.Push(L, param0);
			func.PCall(top, 1);
			func.EndPCall(top);
		};
		return d;
	}

	public static Delegate DOGetter_uint(LuaFunction func)
	{
		DG.Tweening.Core.DOGetter<uint> d = () =>
		{
			object[] objs = func.Call();
			return (uint)objs[0];
		};
		return d;
	}

	public static Delegate DOSetter_uint(LuaFunction func)
	{
		DG.Tweening.Core.DOSetter<uint> d = (param0) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.Push(L, param0);
			func.PCall(top, 1);
			func.EndPCall(top);
		};
		return d;
	}

	public static Delegate DOGetter_long(LuaFunction func)
	{
		DG.Tweening.Core.DOGetter<long> d = () =>
		{
			object[] objs = func.Call();
			return (long)objs[0];
		};
		return d;
	}

	public static Delegate DOSetter_long(LuaFunction func)
	{
		DG.Tweening.Core.DOSetter<long> d = (param0) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.Push(L, param0);
			func.PCall(top, 1);
			func.EndPCall(top);
		};
		return d;
	}

	public static Delegate DOGetter_ulong(LuaFunction func)
	{
		DG.Tweening.Core.DOGetter<ulong> d = () =>
		{
			object[] objs = func.Call();
			return (ulong)objs[0];
		};
		return d;
	}

	public static Delegate DOSetter_ulong(LuaFunction func)
	{
		DG.Tweening.Core.DOSetter<ulong> d = (param0) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.Push(L, param0);
			func.PCall(top, 1);
			func.EndPCall(top);
		};
		return d;
	}

	public static Delegate DOGetter_string(LuaFunction func)
	{
		DG.Tweening.Core.DOGetter<string> d = () =>
		{
			object[] objs = func.Call();
			return (string)objs[0];
		};
		return d;
	}

	public static Delegate DOSetter_string(LuaFunction func)
	{
		DG.Tweening.Core.DOSetter<string> d = (param0) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.Push(L, param0);
			func.PCall(top, 1);
			func.EndPCall(top);
		};
		return d;
	}

	public static Delegate DOGetter_Vector2(LuaFunction func)
	{
		DG.Tweening.Core.DOGetter<Vector2> d = () =>
		{
			object[] objs = func.Call();
			return (Vector2)objs[0];
		};
		return d;
	}

	public static Delegate DOSetter_Vector2(LuaFunction func)
	{
		DG.Tweening.Core.DOSetter<Vector2> d = (param0) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.Push(L, param0);
			func.PCall(top, 1);
			func.EndPCall(top);
		};
		return d;
	}

	public static Delegate DOGetter_Vector3(LuaFunction func)
	{
		DG.Tweening.Core.DOGetter<Vector3> d = () =>
		{
			object[] objs = func.Call();
			return (Vector3)objs[0];
		};
		return d;
	}

	public static Delegate DOSetter_Vector3(LuaFunction func)
	{
		DG.Tweening.Core.DOSetter<Vector3> d = (param0) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.Push(L, param0);
			func.PCall(top, 1);
			func.EndPCall(top);
		};
		return d;
	}

	public static Delegate DOGetter_Vector4(LuaFunction func)
	{
		DG.Tweening.Core.DOGetter<Vector4> d = () =>
		{
			object[] objs = func.Call();
			return (Vector4)objs[0];
		};
		return d;
	}

	public static Delegate DOSetter_Vector4(LuaFunction func)
	{
		DG.Tweening.Core.DOSetter<Vector4> d = (param0) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.Push(L, param0);
			func.PCall(top, 1);
			func.EndPCall(top);
		};
		return d;
	}

	public static Delegate DOGetter_Quaternion(LuaFunction func)
	{
		DG.Tweening.Core.DOGetter<Quaternion> d = () =>
		{
			object[] objs = func.Call();
			return (Quaternion)objs[0];
		};
		return d;
	}

	public static Delegate DOSetter_Quaternion(LuaFunction func)
	{
		DG.Tweening.Core.DOSetter<Quaternion> d = (param0) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.Push(L, param0);
			func.PCall(top, 1);
			func.EndPCall(top);
		};
		return d;
	}

	public static Delegate DOGetter_Color(LuaFunction func)
	{
		DG.Tweening.Core.DOGetter<Color> d = () =>
		{
			object[] objs = func.Call();
			return (Color)objs[0];
		};
		return d;
	}

	public static Delegate DOSetter_Color(LuaFunction func)
	{
		DG.Tweening.Core.DOSetter<Color> d = (param0) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.Push(L, param0);
			func.PCall(top, 1);
			func.EndPCall(top);
		};
		return d;
	}

	public static Delegate DOGetter_Rect(LuaFunction func)
	{
		DG.Tweening.Core.DOGetter<Rect> d = () =>
		{
			object[] objs = func.Call();
			return (Rect)objs[0];
		};
		return d;
	}

	public static Delegate DOSetter_Rect(LuaFunction func)
	{
		DG.Tweening.Core.DOSetter<Rect> d = (param0) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.PushValue(L, param0);
			func.PCall(top, 1);
			func.EndPCall(top);
		};
		return d;
	}

	public static Delegate DOGetter_RectOffset(LuaFunction func)
	{
		DG.Tweening.Core.DOGetter<RectOffset> d = () =>
		{
			object[] objs = func.Call();
			return (RectOffset)objs[0];
		};
		return d;
	}

	public static Delegate DOSetter_RectOffset(LuaFunction func)
	{
		DG.Tweening.Core.DOSetter<RectOffset> d = (param0) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.PushObject(L, param0);
			func.PCall(top, 1);
			func.EndPCall(top);
		};
		return d;
	}

	public static Delegate TimerTriggerCallback(LuaFunction func)
	{
		TimerTriggerCallback d = (param0) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.PushObject(L, param0);
			func.PCall(top, 1);
			func.EndPCall(top);
		};
		return d;
	}

	public static Delegate RectTransform_ReapplyDrivenProperties(LuaFunction func)
	{
		RectTransform.ReapplyDrivenProperties d = (param0) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.Push(L, param0);
			func.PCall(top, 1);
			func.EndPCall(top);
		};
		return d;
	}

	public static Delegate UtilCommon_VoidDelegate(LuaFunction func)
	{
		UtilCommon.VoidDelegate d = (param0, param1) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.Push(L, param0);
			LuaScriptMgr.PushObject(L, param1);
			func.PCall(top, 2);
			func.EndPCall(top);
		};
		return d;
	}

	public static Delegate UtilCommon_OnTouchEventHandle(LuaFunction func)
	{
		UtilCommon.OnTouchEventHandle d = (param0, param1, param2) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.Push(L, param0);
			LuaScriptMgr.PushVarObject(L, param1);
			LuaScriptMgr.PushArray(L, param2);
			func.PCall(top, 3);
			func.EndPCall(top);
		};
		return d;
	}

	public static Delegate UtilCommon_OnIOEventHandle(LuaFunction func)
	{
		UtilCommon.OnIOEventHandle d = (param0, param1) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.Push(L, param0);
			LuaScriptMgr.Push(L, param1);
			func.PCall(top, 2);
			func.EndPCall(top);
		};
		return d;
	}

	public static Delegate UtilCommon_EventHandle(LuaFunction func)
	{
		UtilCommon.EventHandle d = (param0) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.PushVarObject(L, param0);
			func.PCall(top, 1);
			func.EndPCall(top);
		};
		return d;
	}

	public static Delegate TestLuaDelegate_VoidDelegate(LuaFunction func)
	{
		TestLuaDelegate.VoidDelegate d = (param0) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.Push(L, param0);
			func.PCall(top, 1);
			func.EndPCall(top);
		};
		return d;
	}

	public static Delegate Camera_CameraCallback(LuaFunction func)
	{
		Camera.CameraCallback d = (param0) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.Push(L, param0);
			func.PCall(top, 1);
			func.EndPCall(top);
		};
		return d;
	}

	public static Delegate AudioClip_PCMReaderCallback(LuaFunction func)
	{
		AudioClip.PCMReaderCallback d = (param0) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.PushArray(L, param0);
			func.PCall(top, 1);
			func.EndPCall(top);
		};
		return d;
	}

	public static Delegate AudioClip_PCMSetPositionCallback(LuaFunction func)
	{
		AudioClip.PCMSetPositionCallback d = (param0) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.Push(L, param0);
			func.PCall(top, 1);
			func.EndPCall(top);
		};
		return d;
	}

	public static Delegate Application_LowMemoryCallback(LuaFunction func)
	{
		Application.LowMemoryCallback d = () =>
		{
			func.Call();
		};
		return d;
	}

	public static Delegate Application_AdvertisingIdentifierCallback(LuaFunction func)
	{
		Application.AdvertisingIdentifierCallback d = (param0, param1, param2) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.Push(L, param0);
			LuaScriptMgr.Push(L, param1);
			LuaScriptMgr.Push(L, param2);
			func.PCall(top, 3);
			func.EndPCall(top);
		};
		return d;
	}

	public static Delegate Application_LogCallback(LuaFunction func)
	{
		Application.LogCallback d = (param0, param1, param2) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			LuaScriptMgr.Push(L, param0);
			LuaScriptMgr.Push(L, param1);
			LuaScriptMgr.Push(L, param2);
			func.PCall(top, 3);
			func.EndPCall(top);
		};
		return d;
	}

	public static void Clear()
	{
		dict.Clear();
	}

}
