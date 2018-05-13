/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   $safeitemname$.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake $time$
 * 
 * 修改描述：   
 * 
 */


using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PathEditor : Editor
{

    // 路径文件名
    public static string mapname = "pathData";
    // 可编辑路径根节点
    private static string mParentName = "PathRoot";

    [MenuItem("路径编辑/创建路径")]
    static void CreatePath()
    {
        Object[] objects = Object.FindObjectsOfType(typeof(GameObject));
        bool find = false;
        GameObject parent = null;
        foreach (GameObject sceneObject in objects)
        {
            if (sceneObject.name == mParentName)
            {
                parent = sceneObject;
                find = true;
                break;
            }
        }
        if (!find)
        {
            GameObject go = new GameObject();
            go.name = mParentName;
            go.transform.position = Vector3.zero;
            go.transform.localScale = Vector3.one;
            go.transform.localEulerAngles = Vector3.zero;
            go.GetOrAddComponent<MapDraw>();
            parent = go;
        }

        MapWayPoint[] mps = parent.GetComponentsInChildren<MapWayPoint>();
        int count = mps == null || mps.Length == 0 ? 0 : mps.Length;
        GameObject node = new GameObject();
        node.name = "Path_" + count;
        node.transform.SetParent(parent.transform);
        MapWayPoint mp = node.GetOrAddComponent<MapWayPoint>();
    }

    [MenuItem("路径编辑/保存路径")]
    static void SavePath()
    {
        //获取场景中全部道具
        Object[] objects = Object.FindObjectsOfType(typeof(GameObject));

        Dictionary<string, List<string>> post = new Dictionary<string, List<string>>();

        foreach (GameObject sceneObject in objects)
        {

            if (sceneObject.name == mParentName)
            {
                MapDraw mapDraw = sceneObject.GetComponent<MapDraw>();
                if (mapDraw == null)
                {
                    Debug.LogError("MapDraw 脚本为null");
                    return;
                }
                foreach (Transform child in sceneObject.transform)
                {
                    MapWayPoint editor = child.GetComponent<MapWayPoint>();
                    if (editor != null)
                    {
                        if (editor.pointList.Count <= 0) Debug.LogError("The point child is null : " + child.transform.position);
                        List<string> childlist = new List<string>();
                        childlist.Add(editor.pointList.Count.ToString());
                        for (int i = 0; i < editor.pointList.Count; ++i)
                        {
                            childlist.Add(Util.GetPosString(editor.pointList[i].position));
                        }
                        List<Vector3> path = mapDraw.pathDict[editor.name];
                        for (int i = 0; i < path.Count; ++i)
                        {
                            childlist.Add(Util.GetPosString(path[i]));
                        }

                        post.Add(editor.name, childlist);
                    }
                }
            }
        }

        //保存文件
        string filePath = Util.GetDataFilePath(mapname + ".text");
        byte[] byteArray = System.Text.Encoding.Default.GetBytes(JsonMapper.ToJson(post));
        Util.WriteByteToFile(byteArray, filePath);
        AssetDatabase.Refresh();

        Debug.Log(JsonMapper.ToJson(post));
    }
    //================================读取================================

    [MenuItem("路径编辑/读取路径")]
    public static void LoadPath()
    {
        List<GameObject> delarr = new List<GameObject>();
        GameObject WayPoint = null;
        foreach (GameObject sceneObject in Object.FindObjectsOfType(typeof(GameObject)))
        {
            if (sceneObject.name == mParentName)
            {
                WayPoint = sceneObject;
                break;
            }
        }

        if (WayPoint == null)
        {
            GameObject tGO = new GameObject(mParentName);
            tGO.AddComponent<MapDraw>();
            WayPoint = tGO;
        }

        //读取文件
        byte[] pointData = Util.ReadByteToFile(Util.GetDataFilePath(mapname + ".text"));

        if (pointData == null)
        {
            return;
        }

        foreach (Transform child in WayPoint.transform)
        {
            delarr.Add(child.gameObject);
        }
        //删除旧物体
        foreach (GameObject obj in delarr)
        {
            DestroyImmediate(obj);
        }

        string str = System.Text.Encoding.Default.GetString(pointData);
        Debug.Log(str);
        Dictionary<string, List<string>> post = JsonMapper.ToObject<Dictionary<string, List<string>>>(str);

        Dictionary<string, MapWayPoint> temp = new Dictionary<string, MapWayPoint>();
        foreach (KeyValuePair<string, List<string>> pair in post)
        {
            List<string> list = pair.Value;
            GameObject go = new GameObject();
            MapWayPoint mapWayPoint = go.GetOrAddComponent<MapWayPoint>();
            go.name = pair.Key;
            go.transform.SetParent(WayPoint.transform);
            int pointCount = int.Parse(list[0]);
            for (int i = 0; i < pointCount; ++i)
            {
                GameObject point = GameObject.CreatePrimitive(PrimitiveType.Cube);
                point.name = "point_" + i;
                point.transform.SetParent(go.transform);
                point.transform.localScale = Vector3.one;
                point.transform.position = Util.StrintToVector3(list[i + 1]);
                mapWayPoint.AddPoint(point);
            }
        }

    }

    [MenuItem("路径编辑/生成路径方式性能消耗严重，不建议在游戏中动态生成路径使用")]
    static void Tips()
    {

    }
}
