
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Const
{
    public static bool DebugMode = true;					// 调试模式-用于内部测试  True: 使用Asset/Lua
    public static bool UpdateMode = false;					// 实时更新模式 
    public static bool LocalABMode = true;					// True: assetbundle

    public static bool AutoWrapMode = true;

    public static string VersionFile = "Version.json";		// 更新版本文件
    public static string ConfigFile = "Config.json";		// 配置路径

    public static int TimerInterval = 1;
    public static int GameFrameRate = 60;					//游戏帧频
    
    public static bool UsePbc = true;						//PBC
    public static bool UseLpeg = true;						//LPEG
    public static bool UsePbLua = true;					    //Protobuff-lua-gen
    public static bool UseCJson = true;					    //CJson
    public static bool UseSproto = true;					//Sproto

    public static bool LuaEncode = false;                   // 使用lua编码

    public static string UserId = string.Empty;				//用户ID
    public static string AppName = "Q5";					//应用程序名称
    public static string AppPrefix = AppName + "_";			//应用程序前缀
    public static string ExtName = ".unity3d";				//素材扩展名
    public static string AssetDirname = "StreamingAssets";	//素材目录

    public static string AssetBundleXMLName = "assetbundle.xml";

    public static string uLuaPath = Application.dataPath + "/uLua/";

    public static string LuaBasePath = Application.dataPath + "/uLua/Source/";
    public static string LuaWrapPath = LuaBasePath + "LuaWrap/";

#if UNITY_IPHONE
	public static string PackageUrl = "file://" + Application.streamingAssetsPath + "/";		//测试更新地址
#elif UNITY_STANDALONE_WIN
    public static string PackageUrl = "file:///" + Application.streamingAssetsPath + "/";		//测试更新地址
#elif UNITY_ANDROID
	public static string PackageUrl = Application.streamingAssetsPath + "/";				//测试更新地址
#else
	public static string PackageUrl = "file:///" + Application.streamingAssetsPath + "/";	//测试更新地址
#endif
    
    //本地编辑器测试用 测试更新地址
    public static string WebUrl = "http://192.168.16.96/q5/";											//测试更新地址
    //public static string LocalTestWebUrl = "http://192.168.16.96/develop_assetbundle/";				//资源开发测试指定路径
    public static string LocalTestWebUrl = "file:///" + Application.streamingAssetsPath + "/";		//默认安装包本地路径更新地址
    //public static string LocalTestWebUrl = "file:///C:/WorkS6/Res/AssetBundles/";		//本地指定路径

    public static int SocketPort = 8110;						//Socket服务器端口
    public static string SocketAddress = "192.168.0.21";		//Socket服务器地址

    public static Vector2 referenceResolution = new Vector2(1920, 1080);

    /// <summary>
    /// 用一个string持有本地化的Tag
    /// enUS，zhTW,zhCN,ruRU,ptBR,koKR,itIT,frFR,esMX,esES,deDE
    /// </summary>
    public static string L10n = "zhCN";

    public static string Level_Json_Path        = "json/Level.json";
    public static string Character_Json_Path    = "json/Character.json";
    public static string Refresh_Json_Path      = "json/CharacterRefresh.json";

    public static string Setting_Coinfig_Path   = "SettingConfig.json";
    public static string Audio_Coinfig_Path     = "aduiolist.txt";
    public static string Pool_Config_Path       = "poollist.txt";
    public static string Pool_Config_Obj_Path = "Assets/RawRes/prefabs/pool/gameobjectpool.asset";

    public static string GetLocalFileUrl(string parPath)
    {
        string path = "";
        path = Const.LocalTestWebUrl.Replace("file:///", "");
       
        return path + parPath;
    }

    public static string getStreamingPath_for_www(string parPath)
    {
        string pre = "file://";
        #if UNITY_EDITOR
                pre = "file://";
        #elif UNITY_ANDROID
                        pre = "";
        #elif UNITY_IPHONE
	                    pre = "file://";
        #endif
        string path = pre + Application.streamingAssetsPath + "/" + parPath;
        return path;
    }
   
}





