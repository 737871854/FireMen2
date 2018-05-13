/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   PoolManager.cs
 * 
 * 简    介:    用于创建配置GameObjectPool文件
 * 
 * 创建标识：   Pancake 2017/4/2 15:57:08
 * 
 * 修改描述：
 * 
 */


using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Text;
using System.IO;

public class PoolManagerEditor : EditorWindow
{
    [MenuItem("MyTools/Manager/Create GameObjectPoolConfig")]
    static void CreateGameObjectPoolList()
    {
        PoolManagerEditor window = EditorWindow.GetWindow<PoolManagerEditor>("对象池管理");
        window.Show();
    }

    private string poolName;
    private string poolPath;
    private Dictionary<string, string> poolDic = new Dictionary<string, string>();

    void Awake()
    {
        LoadPoolList();
    }

    void OnGUI()
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("对象名称");
        EditorGUILayout.LabelField("对象路径");
        EditorGUILayout.LabelField("操作");
        EditorGUILayout.EndHorizontal();

        foreach (string key in poolDic.Keys)
        {
            string value;
            poolDic.TryGetValue(key, out value);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(key);
            EditorGUILayout.LabelField(value);
            if (GUILayout.Button("删除"))
            {
                poolDic.Remove(key);
                SavePoolList();
                return;
            }
            EditorGUILayout.EndHorizontal();
        }

        if (GUILayout.Button("添加所有对象"))
        {
            poolDic.Clear();
            if (File.Exists(Const.Pool_Config_Path))
            {
                File.Delete(Const.Pool_Config_Path);
            }

            DirectoryInfo folder = new DirectoryInfo(Application.dataPath + "/RawRes/prefabs/pool");
            FileSystemInfo[] files = folder.GetFileSystemInfos();
            for (int i = 0; i < files.Length; ++i)
            {
                if (files[i].Extension.Equals(".meta"))
                    continue;

                poolName = files[i].Name;
                poolName = poolName.Remove(poolName.LastIndexOf('.'));

                //poolPath = files[i].FullName;
                //poolPath = poolPath.Replace("\\", "/");
                //poolPath = "Assets" + poolPath.Substring(Application.dataPath.Length);
                poolPath = "prefabs\\pool\\" + poolName;
                if (poolDic.ContainsKey(poolName))
                    poolDic[poolName] = poolPath;
                else
                {
                    poolDic.Add(poolName, poolPath);
                }
            }

            SavePoolList();
            CreatePools();
        }
    }

    private void CreatePools()
    {
        // 不仅只存在于内存当中，还存在与项目里面
        GameObjectPoolList poolList = ScriptableObject.CreateInstance<GameObjectPoolList>();
        poolList.poolList = new List<GameObjectPool>();
        foreach (KeyValuePair<string, string> kv in poolDic)
        {
            GameObjectPool pool = new GameObjectPool();
            pool.name = kv.Key;
            pool.path = kv.Value;
            poolList.poolList.Add(pool);
        }

        AssetDatabase.CreateAsset(poolList, Const.Pool_Config_Obj_Path);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    /// <summary>
    /// 保存对象池信息
    /// </summary>
    private void SavePoolList()
    {
        StringBuilder sb = new StringBuilder();
        // TODO 后面要改成Json格式
        foreach (string key in poolDic.Keys)
        {
            string value;
            poolDic.TryGetValue(key, out value);
            sb.Append(key + "," + value + "\n");

        }

        File.WriteAllText(Const.Pool_Config_Path, sb.ToString());

        AssetDatabase.Refresh();
    }

    private void LoadPoolList()
    {
        poolDic.Clear();

        if (!File.Exists(Const.Pool_Config_Path))
            return;

        string[] lines = File.ReadAllLines(Const.Pool_Config_Path);
        foreach (string line in lines)
        {
            if (string.IsNullOrEmpty(line)) continue;
            string[] keyvalue = line.Split(',');
            poolDic.Add(keyvalue[0], keyvalue[1]);
        }

    }
}
