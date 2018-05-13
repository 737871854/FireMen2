using System;
using LuaInterface;

public class DelegateFactoryWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("Action_GameObject", Action_GameObject),
			new LuaMethod("Action", Action),
			new LuaMethod("UnityEngine_Events_UnityAction", UnityEngine_Events_UnityAction),
			new LuaMethod("System_Reflection_MemberFilter", System_Reflection_MemberFilter),
			new LuaMethod("Func_System_Reflection_AssemblyName_System_Reflection_Assembly", Func_System_Reflection_AssemblyName_System_Reflection_Assembly),
			new LuaMethod("Func_System_Reflection_Assembly_string_bool_Type", Func_System_Reflection_Assembly_string_bool_Type),
			new LuaMethod("System_Reflection_TypeFilter", System_Reflection_TypeFilter),
			new LuaMethod("DOGetter_float", DOGetter_float),
			new LuaMethod("DOSetter_float", DOSetter_float),
			new LuaMethod("DOGetter_int", DOGetter_int),
			new LuaMethod("DOSetter_int", DOSetter_int),
			new LuaMethod("DOGetter_uint", DOGetter_uint),
			new LuaMethod("DOSetter_uint", DOSetter_uint),
			new LuaMethod("DOGetter_long", DOGetter_long),
			new LuaMethod("DOSetter_long", DOSetter_long),
			new LuaMethod("DOGetter_ulong", DOGetter_ulong),
			new LuaMethod("DOSetter_ulong", DOSetter_ulong),
			new LuaMethod("DOGetter_string", DOGetter_string),
			new LuaMethod("DOSetter_string", DOSetter_string),
			new LuaMethod("DOGetter_Vector2", DOGetter_Vector2),
			new LuaMethod("DOSetter_Vector2", DOSetter_Vector2),
			new LuaMethod("DOGetter_Vector3", DOGetter_Vector3),
			new LuaMethod("DOSetter_Vector3", DOSetter_Vector3),
			new LuaMethod("DOGetter_Vector4", DOGetter_Vector4),
			new LuaMethod("DOSetter_Vector4", DOSetter_Vector4),
			new LuaMethod("DOGetter_Quaternion", DOGetter_Quaternion),
			new LuaMethod("DOSetter_Quaternion", DOSetter_Quaternion),
			new LuaMethod("DOGetter_Color", DOGetter_Color),
			new LuaMethod("DOSetter_Color", DOSetter_Color),
			new LuaMethod("DOGetter_Rect", DOGetter_Rect),
			new LuaMethod("DOSetter_Rect", DOSetter_Rect),
			new LuaMethod("DOGetter_RectOffset", DOGetter_RectOffset),
			new LuaMethod("DOSetter_RectOffset", DOSetter_RectOffset),
			new LuaMethod("TimerTriggerCallback", TimerTriggerCallback),
			new LuaMethod("RectTransform_ReapplyDrivenProperties", RectTransform_ReapplyDrivenProperties),
			new LuaMethod("UtilCommon_VoidDelegate", UtilCommon_VoidDelegate),
			new LuaMethod("UtilCommon_OnTouchEventHandle", UtilCommon_OnTouchEventHandle),
			new LuaMethod("UtilCommon_OnIOEventHandle", UtilCommon_OnIOEventHandle),
			new LuaMethod("UtilCommon_EventHandle", UtilCommon_EventHandle),
			new LuaMethod("TestLuaDelegate_VoidDelegate", TestLuaDelegate_VoidDelegate),
			new LuaMethod("Camera_CameraCallback", Camera_CameraCallback),
			new LuaMethod("AudioClip_PCMReaderCallback", AudioClip_PCMReaderCallback),
			new LuaMethod("AudioClip_PCMSetPositionCallback", AudioClip_PCMSetPositionCallback),
			new LuaMethod("Application_LowMemoryCallback", Application_LowMemoryCallback),
			new LuaMethod("Application_AdvertisingIdentifierCallback", Application_AdvertisingIdentifierCallback),
			new LuaMethod("Application_LogCallback", Application_LogCallback),
			new LuaMethod("Clear", Clear),
			new LuaMethod("New", _CreateDelegateFactory),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaScriptMgr.RegisterLib(L, "DelegateFactory", regs);
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateDelegateFactory(IntPtr L)
	{
		LuaDLL.luaL_error(L, "DelegateFactory class does not have a constructor function");
		return 0;
	}

	static Type classType = typeof(DelegateFactory);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Action_GameObject(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		LuaFunction arg0 = LuaScriptMgr.GetLuaFunction(L, 1);
		Delegate o = DelegateFactory.Action_GameObject(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Action(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		LuaFunction arg0 = LuaScriptMgr.GetLuaFunction(L, 1);
		Delegate o = DelegateFactory.Action(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int UnityEngine_Events_UnityAction(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		LuaFunction arg0 = LuaScriptMgr.GetLuaFunction(L, 1);
		Delegate o = DelegateFactory.UnityEngine_Events_UnityAction(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int System_Reflection_MemberFilter(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		LuaFunction arg0 = LuaScriptMgr.GetLuaFunction(L, 1);
		Delegate o = DelegateFactory.System_Reflection_MemberFilter(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Func_System_Reflection_AssemblyName_System_Reflection_Assembly(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		LuaFunction arg0 = LuaScriptMgr.GetLuaFunction(L, 1);
		Delegate o = DelegateFactory.Func_System_Reflection_AssemblyName_System_Reflection_Assembly(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Func_System_Reflection_Assembly_string_bool_Type(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		LuaFunction arg0 = LuaScriptMgr.GetLuaFunction(L, 1);
		Delegate o = DelegateFactory.Func_System_Reflection_Assembly_string_bool_Type(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int System_Reflection_TypeFilter(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		LuaFunction arg0 = LuaScriptMgr.GetLuaFunction(L, 1);
		Delegate o = DelegateFactory.System_Reflection_TypeFilter(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int DOGetter_float(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		LuaFunction arg0 = LuaScriptMgr.GetLuaFunction(L, 1);
		Delegate o = DelegateFactory.DOGetter_float(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int DOSetter_float(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		LuaFunction arg0 = LuaScriptMgr.GetLuaFunction(L, 1);
		Delegate o = DelegateFactory.DOSetter_float(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int DOGetter_int(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		LuaFunction arg0 = LuaScriptMgr.GetLuaFunction(L, 1);
		Delegate o = DelegateFactory.DOGetter_int(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int DOSetter_int(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		LuaFunction arg0 = LuaScriptMgr.GetLuaFunction(L, 1);
		Delegate o = DelegateFactory.DOSetter_int(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int DOGetter_uint(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		LuaFunction arg0 = LuaScriptMgr.GetLuaFunction(L, 1);
		Delegate o = DelegateFactory.DOGetter_uint(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int DOSetter_uint(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		LuaFunction arg0 = LuaScriptMgr.GetLuaFunction(L, 1);
		Delegate o = DelegateFactory.DOSetter_uint(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int DOGetter_long(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		LuaFunction arg0 = LuaScriptMgr.GetLuaFunction(L, 1);
		Delegate o = DelegateFactory.DOGetter_long(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int DOSetter_long(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		LuaFunction arg0 = LuaScriptMgr.GetLuaFunction(L, 1);
		Delegate o = DelegateFactory.DOSetter_long(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int DOGetter_ulong(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		LuaFunction arg0 = LuaScriptMgr.GetLuaFunction(L, 1);
		Delegate o = DelegateFactory.DOGetter_ulong(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int DOSetter_ulong(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		LuaFunction arg0 = LuaScriptMgr.GetLuaFunction(L, 1);
		Delegate o = DelegateFactory.DOSetter_ulong(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int DOGetter_string(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		LuaFunction arg0 = LuaScriptMgr.GetLuaFunction(L, 1);
		Delegate o = DelegateFactory.DOGetter_string(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int DOSetter_string(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		LuaFunction arg0 = LuaScriptMgr.GetLuaFunction(L, 1);
		Delegate o = DelegateFactory.DOSetter_string(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int DOGetter_Vector2(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		LuaFunction arg0 = LuaScriptMgr.GetLuaFunction(L, 1);
		Delegate o = DelegateFactory.DOGetter_Vector2(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int DOSetter_Vector2(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		LuaFunction arg0 = LuaScriptMgr.GetLuaFunction(L, 1);
		Delegate o = DelegateFactory.DOSetter_Vector2(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int DOGetter_Vector3(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		LuaFunction arg0 = LuaScriptMgr.GetLuaFunction(L, 1);
		Delegate o = DelegateFactory.DOGetter_Vector3(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int DOSetter_Vector3(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		LuaFunction arg0 = LuaScriptMgr.GetLuaFunction(L, 1);
		Delegate o = DelegateFactory.DOSetter_Vector3(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int DOGetter_Vector4(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		LuaFunction arg0 = LuaScriptMgr.GetLuaFunction(L, 1);
		Delegate o = DelegateFactory.DOGetter_Vector4(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int DOSetter_Vector4(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		LuaFunction arg0 = LuaScriptMgr.GetLuaFunction(L, 1);
		Delegate o = DelegateFactory.DOSetter_Vector4(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int DOGetter_Quaternion(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		LuaFunction arg0 = LuaScriptMgr.GetLuaFunction(L, 1);
		Delegate o = DelegateFactory.DOGetter_Quaternion(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int DOSetter_Quaternion(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		LuaFunction arg0 = LuaScriptMgr.GetLuaFunction(L, 1);
		Delegate o = DelegateFactory.DOSetter_Quaternion(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int DOGetter_Color(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		LuaFunction arg0 = LuaScriptMgr.GetLuaFunction(L, 1);
		Delegate o = DelegateFactory.DOGetter_Color(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int DOSetter_Color(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		LuaFunction arg0 = LuaScriptMgr.GetLuaFunction(L, 1);
		Delegate o = DelegateFactory.DOSetter_Color(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int DOGetter_Rect(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		LuaFunction arg0 = LuaScriptMgr.GetLuaFunction(L, 1);
		Delegate o = DelegateFactory.DOGetter_Rect(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int DOSetter_Rect(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		LuaFunction arg0 = LuaScriptMgr.GetLuaFunction(L, 1);
		Delegate o = DelegateFactory.DOSetter_Rect(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int DOGetter_RectOffset(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		LuaFunction arg0 = LuaScriptMgr.GetLuaFunction(L, 1);
		Delegate o = DelegateFactory.DOGetter_RectOffset(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int DOSetter_RectOffset(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		LuaFunction arg0 = LuaScriptMgr.GetLuaFunction(L, 1);
		Delegate o = DelegateFactory.DOSetter_RectOffset(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int TimerTriggerCallback(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		LuaFunction arg0 = LuaScriptMgr.GetLuaFunction(L, 1);
		Delegate o = DelegateFactory.TimerTriggerCallback(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int RectTransform_ReapplyDrivenProperties(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		LuaFunction arg0 = LuaScriptMgr.GetLuaFunction(L, 1);
		Delegate o = DelegateFactory.RectTransform_ReapplyDrivenProperties(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int UtilCommon_VoidDelegate(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		LuaFunction arg0 = LuaScriptMgr.GetLuaFunction(L, 1);
		Delegate o = DelegateFactory.UtilCommon_VoidDelegate(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int UtilCommon_OnTouchEventHandle(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		LuaFunction arg0 = LuaScriptMgr.GetLuaFunction(L, 1);
		Delegate o = DelegateFactory.UtilCommon_OnTouchEventHandle(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int UtilCommon_OnIOEventHandle(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		LuaFunction arg0 = LuaScriptMgr.GetLuaFunction(L, 1);
		Delegate o = DelegateFactory.UtilCommon_OnIOEventHandle(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int UtilCommon_EventHandle(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		LuaFunction arg0 = LuaScriptMgr.GetLuaFunction(L, 1);
		Delegate o = DelegateFactory.UtilCommon_EventHandle(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int TestLuaDelegate_VoidDelegate(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		LuaFunction arg0 = LuaScriptMgr.GetLuaFunction(L, 1);
		Delegate o = DelegateFactory.TestLuaDelegate_VoidDelegate(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Camera_CameraCallback(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		LuaFunction arg0 = LuaScriptMgr.GetLuaFunction(L, 1);
		Delegate o = DelegateFactory.Camera_CameraCallback(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int AudioClip_PCMReaderCallback(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		LuaFunction arg0 = LuaScriptMgr.GetLuaFunction(L, 1);
		Delegate o = DelegateFactory.AudioClip_PCMReaderCallback(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int AudioClip_PCMSetPositionCallback(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		LuaFunction arg0 = LuaScriptMgr.GetLuaFunction(L, 1);
		Delegate o = DelegateFactory.AudioClip_PCMSetPositionCallback(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Application_LowMemoryCallback(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		LuaFunction arg0 = LuaScriptMgr.GetLuaFunction(L, 1);
		Delegate o = DelegateFactory.Application_LowMemoryCallback(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Application_AdvertisingIdentifierCallback(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		LuaFunction arg0 = LuaScriptMgr.GetLuaFunction(L, 1);
		Delegate o = DelegateFactory.Application_AdvertisingIdentifierCallback(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Application_LogCallback(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		LuaFunction arg0 = LuaScriptMgr.GetLuaFunction(L, 1);
		Delegate o = DelegateFactory.Application_LogCallback(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Clear(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 0);
		DelegateFactory.Clear();
		return 0;
	}
}

