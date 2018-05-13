using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;
using System.Text;
using LitJson;


/// <summary>
/// 把Resource下的资源打包成.unity3d 到StreamingAssets目录下
/// </summary>
public class AssetbundleMaker : Editor
{
    private const string AssetBundlesOutputPath = "Assets/StreamingAssets/pc/assetbundle/";
    private const string jsonName = "FileList.json";
    //[MenuItem("Assetbundle/打包 UI")]
    //public static void BuildUIAssetBundleWindows()
    //{
    //    string sourcePath = Application.dataPath + "/Prefabs/ui";
    //    string fileListPath = AssetBundlesOutputPath;
    //    string outputPath = fileListPath;
    //    if (System.IO.Directory.Exists(outputPath))
    //    {
    //        FileUtil.DeleteFileOrDirectory(outputPath);
    //    }
    //    System.IO.Directory.CreateDirectory(outputPath);

    //    //根据BuildSetting里面所激活的平台进行打包
    //    BuildPipeline.BuildAssetBundles(outputPath, 0, BuildTarget.StandaloneWindows);

    //    FileList fl = new FileList();
    //    fl.platform = "Windows";
    //    fl.version = "1.0";
    //    Pack(sourcePath, fl);
    //    fl.WriteToFile(fileListPath + "//" + jsonName);

    //    AssetDatabase.Refresh();

    //    Debug.Log("打包完成");
    //}


    //[MenuItem("Assetbundle/打包 模型")]
    //public static void BuildModleAssetBundleWindows()
    //{
    //    string sourcePath = Application.dataPath + "/Prefabs/Modle";
    //    string fileListPath = Path.Combine(AssetBundlesOutputPath, Platform.GetPlatformFolder(EditorUserBuildSettings.activeBuildTarget));
    //    fileListPath += "//assetbundle";
    //    string outputPath = fileListPath + "//modle";
    //    if (System.IO.Directory.Exists(outputPath))
    //    {
    //        FileUtil.DeleteFileOrDirectory(outputPath);
    //    }
    //    System.IO.Directory.CreateDirectory(outputPath);

    //    //根据BuildSetting里面所激活的平台进行打包
    //    BuildPipeline.BuildAssetBundles(outputPath, 0, BuildTarget.StandaloneWindows);

    //    FileList fl = new FileList();
    //    fl.platform = "Windows";
    //    fl.version = "1.0";
    //    Pack(sourcePath, fl);
    //    fl.WriteToFile(fileListPath + "//" + jsonName);

    //    AssetDatabase.Refresh();

    //    Debug.Log("打包完成");
    //}


    //[MenuItem("Assetbundle/全部打包")]
    public static void BuildAllAssetBundleWindows()
    {
        string sourcePath_UI        = Application.dataPath + "/Prefabs/ui";
        string sourcePath_modle     = Application.dataPath + "/Prefabs/modle";
        string sourcePath_Audio     = Application.dataPath + "/Audios";
        //string sourcePath_Material  = Application.dataPath + "/Materials";
        //string sourcePath_Shader    = Application.dataPath + "/Shaders";
        //string soucePath_Movie      = Application.dataPath + "/Movie";
        string sourcePath_Atlas     = Application.dataPath + "/SpritesAtlas";
        string fileListPath         = AssetBundlesOutputPath;
        string outputPath           = fileListPath;
        if (System.IO.Directory.Exists(outputPath))
        {
            FileUtil.DeleteFileOrDirectory(outputPath);
        }
        System.IO.Directory.CreateDirectory(outputPath);

        FileList fl = new FileList();
        fl.platform = "Windows";
        fl.version  = "1.0";
        Pack(sourcePath_UI, fl);
        Pack(sourcePath_modle, fl);
        Pack(sourcePath_Audio, fl);
        //Pack(sourcePath_Material, fl);
        //Pack(sourcePath_Shader, fl);
        //Pack(soucePath_Movie, fl);
        Pack(sourcePath_Atlas, fl);
        fl.WriteToFile(fileListPath + "//" + jsonName);

        //根据BuildSetting里面所激活的平台进行打包
        BuildPipeline.BuildAssetBundles(outputPath, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows64);

        AssetDatabase.Refresh();

        Debug.Log("打包完成");
    }

    /// <summary>
    /// 清除之前设置过的AssetBundleName，避免产生不必要的资源也打包
    /// 之前说过，只要设置了AssetBundleName的，都会进行打包，不论在什么目录下
    /// </summary>
    static void ClearAssetBundlesName()
    {
        int length = AssetDatabase.GetAllAssetBundleNames().Length;
        Debug.Log(length);
        string[] oldAssetBundleNames = new string[length];
        for (int i = 0; i < length; i++)
        {
            oldAssetBundleNames[i] = AssetDatabase.GetAllAssetBundleNames()[i];
        }

        for (int j = 0; j < oldAssetBundleNames.Length; j++)
        {
            AssetDatabase.RemoveAssetBundleName(oldAssetBundleNames[j], true);
        }
        length = AssetDatabase.GetAllAssetBundleNames().Length;
        Debug.Log(length);
    }

    static void Pack(string source, FileList fl)
    {
        DirectoryInfo folder = new DirectoryInfo(source);
        FileSystemInfo[] files = folder.GetFileSystemInfos();
        int length = files.Length;
        
        for (int i = 0; i < length; i++)
        {
            if (files[i] is DirectoryInfo)
            {
                Pack(files[i].FullName, fl);
            }
            else
            {
                if (!files[i].Name.EndsWith(".meta"))
                {
                    file(files[i].FullName, fl);
                }
            }
        }
    }

    static void file(string source, FileList fl)
    {
        string _source = Replace(source);
        string _assetPath = "Assets" + _source.Substring(Application.dataPath.Length);
        //string _assetPath2 = _source.Substring(Application.dataPath.Length + 1);
        //Debug.Log (_assetPath);

        //在代码中给资源设置AssetBundleName
        AssetImporter assetImporter = AssetImporter.GetAtPath(_assetPath);
        string assetName = _assetPath.Replace("/", "_");
        assetName = assetName.Replace(Path.GetExtension(assetName), ".assetbundle");
        //Debug.Log (assetName);
        assetImporter.assetBundleName = assetName;

        fl.res2bundleData.Add(_assetPath, assetName);
        //writer.WritePropertyName(_assetPath);
        //writer.Write(assetName);
    }

    static string Replace(string s)
    {
        return s.Replace("\\", "/");
    }
}


public class Platform
{
    public static string GetPlatformFolder(BuildTarget target)
    {
        switch (target)
        {
            case BuildTarget.Android:
                return "Android";
            case BuildTarget.iOS:
                return "IOS";
            case BuildTarget.WebPlayer:
                return "WebPlayer";
            case BuildTarget.StandaloneWindows:
            case BuildTarget.StandaloneWindows64:
                return "Windows";
            case BuildTarget.StandaloneOSXIntel:
            case BuildTarget.StandaloneOSXIntel64:
            case BuildTarget.StandaloneOSXUniversal:
                return "OSX";
            default:
                return null;
        }
    }
}