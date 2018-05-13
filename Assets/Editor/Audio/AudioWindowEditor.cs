/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   AudioWindowEditor.cs
 * 
 * 简    介:    音效管理面板
 * 
 * 创建标识：   Pancake 2017/4/19 15:12:10
 * 
 * 修改描述：
 * 
 */


using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Text;
using System.IO;

public class AudioWindowEditor : EditorWindow
{
    [MenuItem("MyTools/Manager/AudioManager")]
    static void CreateWindow()
    {
        AudioWindowEditor window = EditorWindow.GetWindow<AudioWindowEditor>("音效管理");
        window.Show();
    }

    private string audioName;
    private string audioPath;
    private Dictionary<string, string> audioDic = new Dictionary<string, string>();

    void Awake()
    {
        LoadAudioList();
    }

    void OnGUI()
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("音效名称");
        EditorGUILayout.LabelField("音效路径");
        EditorGUILayout.LabelField("操作");
        EditorGUILayout.EndHorizontal();

        foreach(string key in audioDic.Keys)
        {
            string value;
            audioDic.TryGetValue(key, out value);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(key);
            EditorGUILayout.LabelField(value);
            if(GUILayout.Button("删除"))
            {
                audioDic.Remove(key);
                SaveAudioList();
                return;
            }
            EditorGUILayout.EndHorizontal();
        }

        //audioName = EditorGUILayout.TextField("音效名字", audioName);
        //audioPath = EditorGUILayout.TextField("音效路径", audioPath);
        //if(GUILayout.Button("添加音效"))
        //{
        //    object o = AssetDatabase.LoadAssetAtPath(audioPath, typeof(object));
        //    if (null == o)
        //    {
        //        Debug.LogWarning("音效不存在于" + audioPath + " 添加不成功");
        //        audioPath = "";
        //    }
        //    else
        //    {
        //        if (audioDic.ContainsKey(audioName))
        //            Debug.Log("名字已存在，请修改");
        //        else
        //        {
        //            audioDic.Add(audioName, audioPath);
        //            SaveAudioList();
        //        }
        //    }
        //}

        if (GUILayout.Button("添加所有音效"))
        {
            audioDic.Clear();
            if (File.Exists(Const.GetLocalFileUrl(Const.Audio_Coinfig_Path)))
            {
                File.Delete(Const.GetLocalFileUrl(Const.Audio_Coinfig_Path));
            }

            DirectoryInfo folder = new DirectoryInfo(Application.dataPath + "/RawRes/audios");
            FileSystemInfo[] files = folder.GetFileSystemInfos();
            for (int i = 0; i < files.Length; ++i )
            {
                if (files[i].Extension.Equals(".meta"))
                    continue;

                audioName = files[i].Name;
                audioName = audioName.Remove(audioName.LastIndexOf('.'));

                audioPath = "audios\\" + audioName;
                //audioPath = files[i].FullName;
                //audioPath = audioPath.Remove(audioPath.LastIndexOf('.'));
                //audioPath.Substring((Application.dataPath + "/RawRes/").Length);
                //audioPath = audioPath.Replace("\\", "/");
                if (audioDic.ContainsKey(audioName))
                    audioDic[audioName] = audioPath;
                else
                {
                    audioDic.Add(audioName, audioPath);
                }
            }

            SaveAudioList();
        }
    }

    /// <summary>
    /// 保存音效信息
    /// </summary>
    private void SaveAudioList()
    {
        StringBuilder sb = new StringBuilder();
        // TODO 后面要改成Json格式
        foreach (string key in audioDic.Keys)
        {
            string value;
            audioDic.TryGetValue(key, out value);
            sb.Append(key + "," + value + "\n");

        }

        File.WriteAllText(Const.GetLocalFileUrl(Const.Audio_Coinfig_Path), sb.ToString());

        AssetDatabase.Refresh();
    }

    private void LoadAudioList()
    {
        audioDic.Clear();

        if (!File.Exists(Const.GetLocalFileUrl(Const.Audio_Coinfig_Path)))
            return;

        string[] lines = File.ReadAllLines(Const.GetLocalFileUrl(Const.Audio_Coinfig_Path));
        foreach(string line in lines)
        {
            if (string.IsNullOrEmpty(line)) continue;
            string[] keyvalue = line.Split(',');
            audioDic.Add(keyvalue[0], keyvalue[1]);
        }

    }
}
