using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using LuaInterface;

public class iooWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("f", f),
			new LuaMethod("c", c),
			new LuaMethod("AddPrefab", AddPrefab),
			new LuaMethod("GetPrefab", GetPrefab),
			new LuaMethod("RemovePrefab", RemovePrefab),
			new LuaMethod("LoadPrefab", LoadPrefab),
			new LuaMethod("IsPlaying", IsPlaying),
			new LuaMethod("IsContinue", IsContinue),
			new LuaMethod("IsDead", IsDead),
			new LuaMethod("WaterPosition", WaterPosition),
			new LuaMethod("HPProgress", HPProgress),
			new LuaMethod("Score", Score),
			new LuaMethod("ContinueTime", ContinueTime),
			new LuaMethod("StopAll", StopAll),
			new LuaMethod("PlayBackMusic", PlayBackMusic),
			new LuaMethod("StopBackMusic", StopBackMusic),
			new LuaMethod("PlaySound2D", PlaySound2D),
			new LuaMethod("HasCoin", HasCoin),
			new LuaMethod("GetMonthData", GetMonthData),
			new LuaMethod("TotalRecord", TotalRecord),
			new LuaMethod("ClearCoin", ClearCoin),
			new LuaMethod("ClearMonthInfo", ClearMonthInfo),
			new LuaMethod("ClearTotalRecord", ClearTotalRecord),
			new LuaMethod("AddCoin", AddCoin),
			new LuaMethod("UseCoin", UseCoin),
			new LuaMethod("LogNumberOfGame", LogNumberOfGame),
			new LuaMethod("Save", Save),
			new LuaMethod("LoadJsonFile", LoadJsonFile),
			new LuaMethod("AddPreLoadPanel", AddPreLoadPanel),
			new LuaMethod("AddPreLoadPrefab", AddPreLoadPrefab),
			new LuaMethod("AddPreLoadAtlas", AddPreLoadAtlas),
			new LuaMethod("SetLoadScene", SetLoadScene),
			new LuaMethod("ChangeScene", ChangeScene),
			new LuaMethod("ChangeSceneDirect", ChangeSceneDirect),
			new LuaMethod("LoadAllSprites", LoadAllSprites),
			new LuaMethod("FindAllImage", FindAllImage),
			new LuaMethod("Clear", Clear),
			new LuaMethod("RegesterListener", RegesterListener),
			new LuaMethod("RemoveListener", RemoveListener),
			new LuaMethod("TriggerListener", TriggerListener),
			new LuaMethod("New", _Createioo),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("manager", get_manager, null),
			new LuaField("gameManager", get_gameManager, null),
			new LuaField("playerManager", get_playerManager, null),
			new LuaField("cameraManager", get_cameraManager, null),
			new LuaField("animationHelper", get_animationHelper, null),
			new LuaField("safeNet", get_safeNet, null),
			new LuaField("resourceManager", get_resourceManager, null),
			new LuaField("timerManager", get_timerManager, null),
			new LuaField("gameMode", get_gameMode, null),
			new LuaField("audioManager", get_audioManager, null),
			new LuaField("networkManager", get_networkManager, null),
			new LuaField("ioManager", get_ioManager, null),
			new LuaField("characterSystem", get_characterSystem, null),
			new LuaField("stageSystem", get_stageSystem, null),
			new LuaField("gameEventSystem", get_gameEventSystem, null),
			new LuaField("nonStopTime", get_nonStopTime, null),
			new LuaField("MainUI", get_MainUI, null),
			new LuaField("battleScene", get_battleScene, null),
			new LuaField("guiCamera", get_guiCamera, null),
			new LuaField("preIsLoad", get_preIsLoad, null),
			new LuaField("StagetyCanUpdate", get_StagetyCanUpdate, null),
			new LuaField("stageSysPuase", get_stageSysPuase, set_stageSysPuase),
			new LuaField("playerCount", get_playerCount, null),
			new LuaField("gameRate", get_gameRate, set_gameRate),
			new LuaField("gameVolume", get_gameVolume, set_gameVolume),
			new LuaField("gameLevel", get_gameLevel, set_gameLevel),
			new LuaField("gameLanguage", get_gameLanguage, set_gameLanguage),
		};

		LuaScriptMgr.RegisterLib(L, "ioo", typeof(ioo), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _Createioo(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			ioo obj = new ioo();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: ioo.New");
		}

		return 0;
	}

	static Type classType = typeof(ioo);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_manager(IntPtr L)
	{
		LuaScriptMgr.Push(L, ioo.manager);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_gameManager(IntPtr L)
	{
		LuaScriptMgr.Push(L, ioo.gameManager);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_playerManager(IntPtr L)
	{
		LuaScriptMgr.Push(L, ioo.playerManager);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_cameraManager(IntPtr L)
	{
		LuaScriptMgr.Push(L, ioo.cameraManager);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_animationHelper(IntPtr L)
	{
		LuaScriptMgr.Push(L, ioo.animationHelper);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_safeNet(IntPtr L)
	{
		LuaScriptMgr.Push(L, ioo.safeNet);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_resourceManager(IntPtr L)
	{
		LuaScriptMgr.Push(L, ioo.resourceManager);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_timerManager(IntPtr L)
	{
		LuaScriptMgr.Push(L, ioo.timerManager);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_gameMode(IntPtr L)
	{
		LuaScriptMgr.Push(L, ioo.gameMode);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_audioManager(IntPtr L)
	{
		LuaScriptMgr.Push(L, ioo.audioManager);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_networkManager(IntPtr L)
	{
		LuaScriptMgr.Push(L, ioo.networkManager);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_ioManager(IntPtr L)
	{
		LuaScriptMgr.Push(L, ioo.ioManager);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_characterSystem(IntPtr L)
	{
		LuaScriptMgr.PushObject(L, ioo.characterSystem);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_stageSystem(IntPtr L)
	{
		LuaScriptMgr.PushObject(L, ioo.stageSystem);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_gameEventSystem(IntPtr L)
	{
		LuaScriptMgr.PushObject(L, ioo.gameEventSystem);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_nonStopTime(IntPtr L)
	{
		LuaScriptMgr.Push(L, ioo.nonStopTime);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_MainUI(IntPtr L)
	{
		LuaScriptMgr.Push(L, ioo.MainUI);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_battleScene(IntPtr L)
	{
		LuaScriptMgr.Push(L, ioo.battleScene);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_guiCamera(IntPtr L)
	{
		LuaScriptMgr.Push(L, ioo.guiCamera);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_preIsLoad(IntPtr L)
	{
		LuaScriptMgr.Push(L, ioo.preIsLoad);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_StagetyCanUpdate(IntPtr L)
	{
		LuaScriptMgr.Push(L, ioo.StagetyCanUpdate);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_stageSysPuase(IntPtr L)
	{
		LuaScriptMgr.Push(L, ioo.stageSysPuase);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_playerCount(IntPtr L)
	{
		LuaScriptMgr.Push(L, ioo.playerCount);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_gameRate(IntPtr L)
	{
		LuaScriptMgr.Push(L, ioo.gameRate);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_gameVolume(IntPtr L)
	{
		LuaScriptMgr.Push(L, ioo.gameVolume);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_gameLevel(IntPtr L)
	{
		LuaScriptMgr.Push(L, ioo.gameLevel);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_gameLanguage(IntPtr L)
	{
		LuaScriptMgr.Push(L, ioo.gameLanguage);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_stageSysPuase(IntPtr L)
	{
		ioo.stageSysPuase = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_gameRate(IntPtr L)
	{
		ioo.gameRate = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_gameVolume(IntPtr L)
	{
		ioo.gameVolume = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_gameLevel(IntPtr L)
	{
		ioo.gameLevel = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_gameLanguage(IntPtr L)
	{
		ioo.gameLanguage = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int f(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);
		string arg0 = LuaScriptMgr.GetLuaString(L, 1);
		object[] objs1 = LuaScriptMgr.GetParamsObject(L, 2, count - 1);
		string o = ioo.f(arg0,objs1);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int c(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);
		object[] objs0 = LuaScriptMgr.GetParamsObject(L, 1, count);
		string o = ioo.c(objs0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int AddPrefab(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		string arg0 = LuaScriptMgr.GetLuaString(L, 1);
		GameObject arg1 = (GameObject)LuaScriptMgr.GetUnityObject(L, 2, typeof(GameObject));
		ioo.AddPrefab(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetPrefab(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		string arg0 = LuaScriptMgr.GetLuaString(L, 1);
		GameObject o = ioo.GetPrefab(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int RemovePrefab(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		string arg0 = LuaScriptMgr.GetLuaString(L, 1);
		ioo.RemovePrefab(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int LoadPrefab(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		string arg0 = LuaScriptMgr.GetLuaString(L, 1);
		GameObject o = ioo.LoadPrefab(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IsPlaying(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		int arg0 = (int)LuaScriptMgr.GetNumber(L, 1);
		bool o = ioo.IsPlaying(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IsContinue(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		int arg0 = (int)LuaScriptMgr.GetNumber(L, 1);
		bool o = ioo.IsContinue(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IsDead(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		int arg0 = (int)LuaScriptMgr.GetNumber(L, 1);
		bool o = ioo.IsDead(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int WaterPosition(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		int arg0 = (int)LuaScriptMgr.GetNumber(L, 1);
		Vector3 o = ioo.WaterPosition(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int HPProgress(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		int arg0 = (int)LuaScriptMgr.GetNumber(L, 1);
		float o = ioo.HPProgress(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Score(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		int arg0 = (int)LuaScriptMgr.GetNumber(L, 1);
		int o = ioo.Score(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ContinueTime(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		int arg0 = (int)LuaScriptMgr.GetNumber(L, 1);
		int o = ioo.ContinueTime(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int StopAll(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 0);
		ioo.StopAll();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int PlayBackMusic(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		string arg0 = LuaScriptMgr.GetLuaString(L, 1);
		bool arg1 = LuaScriptMgr.GetBoolean(L, 2);
		ioo.PlayBackMusic(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int StopBackMusic(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		string arg0 = LuaScriptMgr.GetLuaString(L, 1);
		ioo.StopBackMusic(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int PlaySound2D(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		string arg0 = LuaScriptMgr.GetLuaString(L, 1);
		ioo.PlaySound2D(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int HasCoin(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		int arg0 = (int)LuaScriptMgr.GetNumber(L, 1);
		int o = ioo.HasCoin(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetMonthData(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 0);
		List<float[]> o = ioo.GetMonthData();
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int TotalRecord(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 0);
		float[] o = ioo.TotalRecord();
		LuaScriptMgr.PushArray(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ClearCoin(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 0);
		ioo.ClearCoin();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ClearMonthInfo(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 0);
		ioo.ClearMonthInfo();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ClearTotalRecord(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 0);
		ioo.ClearTotalRecord();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int AddCoin(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		int arg0 = (int)LuaScriptMgr.GetNumber(L, 1);
		ioo.AddCoin(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int UseCoin(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		int arg0 = (int)LuaScriptMgr.GetNumber(L, 1);
		ioo.UseCoin(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int LogNumberOfGame(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		int arg0 = (int)LuaScriptMgr.GetNumber(L, 1);
		ioo.LogNumberOfGame(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Save(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 0);
		ioo.Save();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int LoadJsonFile(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 0);
		ioo.LoadJsonFile();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int AddPreLoadPanel(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		string arg0 = LuaScriptMgr.GetLuaString(L, 1);
		ioo.AddPreLoadPanel(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int AddPreLoadPrefab(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		string arg0 = LuaScriptMgr.GetLuaString(L, 1);
		int arg1 = (int)LuaScriptMgr.GetNumber(L, 2);
		ioo.AddPreLoadPrefab(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int AddPreLoadAtlas(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		string arg0 = LuaScriptMgr.GetLuaString(L, 1);
		ioo.AddPreLoadAtlas(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetLoadScene(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 1)
		{
			string arg0 = LuaScriptMgr.GetLuaString(L, 1);
			ioo.SetLoadScene(arg0);
			return 0;
		}
		else if (count == 2)
		{
			string arg0 = LuaScriptMgr.GetLuaString(L, 1);
			string arg1 = LuaScriptMgr.GetLuaString(L, 2);
			ioo.SetLoadScene(arg0,arg1);
			return 0;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: ioo.SetLoadScene");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ChangeScene(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 0);
		ioo.ChangeScene();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ChangeSceneDirect(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 0);
		ioo.ChangeSceneDirect();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int LoadAllSprites(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		string arg0 = LuaScriptMgr.GetLuaString(L, 1);
		List<Sprite> o = ioo.LoadAllSprites(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int FindAllImage(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		Transform arg0 = (Transform)LuaScriptMgr.GetUnityObject(L, 1, typeof(Transform));
		List<Image> o = ioo.FindAllImage(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Clear(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 0);
		ioo.Clear();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int RegesterListener(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		string arg0 = LuaScriptMgr.GetLuaString(L, 1);
		UtilCommon.EventHandle arg1 = null;
		LuaTypes funcType2 = LuaDLL.lua_type(L, 2);

		if (funcType2 != LuaTypes.LUA_TFUNCTION)
		{
			 arg1 = (UtilCommon.EventHandle)LuaScriptMgr.GetNetObject(L, 2, typeof(UtilCommon.EventHandle));
		}
		else
		{
			LuaFunction func = LuaScriptMgr.GetLuaFunction(L, 2);
			arg1 = (param0) =>
			{
				int top = func.BeginPCall();
				LuaScriptMgr.PushVarObject(L, param0);
				func.PCall(top, 1);
				func.EndPCall(top);
			};
		}

		string arg2 = LuaScriptMgr.GetLuaString(L, 3);
		ioo.RegesterListener(arg0,arg1,arg2);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int RemoveListener(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		string arg0 = LuaScriptMgr.GetLuaString(L, 1);
		string arg1 = LuaScriptMgr.GetLuaString(L, 2);
		ioo.RemoveListener(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int TriggerListener(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 1)
		{
			string arg0 = LuaScriptMgr.GetLuaString(L, 1);
			ioo.TriggerListener(arg0);
			return 0;
		}
		else if (count == 2)
		{
			string arg0 = LuaScriptMgr.GetLuaString(L, 1);
			object arg1 = LuaScriptMgr.GetVarObject(L, 2);
			ioo.TriggerListener(arg0,arg1);
			return 0;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: ioo.TriggerListener");
		}

		return 0;
	}
}

