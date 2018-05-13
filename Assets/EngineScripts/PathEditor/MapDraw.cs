/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   MapDraw.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/4/4 14:27:34
 * 
 * 修改描述：   
 * 
 */

using System.Collections.Generic;
using UnityEngine;

public class MapDraw : MonoBehaviour
{
    public float mStep = 0.5f;
    public int mPointNum;
    private int mCout;

    // 可编辑路径根节点
    private static string mParentName = "PathRoot";

    public Dictionary<string, List<Vector3>> pathDict = new Dictionary<string, List<Vector3>>();

    public void OnDrawGizmos()
    {

#if UNITY_EDITOR
        Gizmos.color = Color.red;

        //获取场景中全部道具
        Object[] objects = Object.FindObjectsOfType(typeof(GameObject));

        Dictionary<string, List<string>> post = new Dictionary<string, List<string>>();

        foreach (GameObject sceneObject in objects)
        {
            if (sceneObject.name == mParentName)
            {
                foreach (Transform child in sceneObject.transform)
                {
                    MapWayPoint editor = child.GetComponent<MapWayPoint>();
                    if (editor != null)
                    {
                        float distance = 0;
                        if (editor.pointList == null || editor.pointList.Count == 0) { return; }

                        for (int i = 0; i < editor.pointList.Count; ++i)
                        {
                            if (editor.pointList[i] == null)
                            {
                                editor.pointList.RemoveAt(i);
                                return;
                            }
                        }

                        for (int j = 0; j < editor.pointList.Count - 1; ++j)
                        {
                            distance += Vector3.Distance(editor.pointList[j].position, editor.pointList[j + 1].position);
                        }
                        mCout = Mathf.RoundToInt(distance / mStep);
                        List<Vector3> list = new List<Vector3>();
                        foreach (Transform go in editor.pointList)
                        {
                            list.Add(go.transform.position);
                        }
                        List<Vector3> path = new List<Vector3>();
                        for (int k = 0; k < mCout; ++k)
                        {
                            Vector3 pos = Vector3.zero;
                            Util.GenPoint(list, k, mCout, ref pos);
                            path.Add(pos);
                        }
                        mPointNum = path.Count;
                        if (!pathDict.ContainsKey(editor.name))
                            pathDict.Add(editor.name, new List<Vector3>());
                        pathDict[editor.name] = path;
                        for (int i = 0; i < mPointNum - 1; i++)
                        {
                            if (path[i] == null)
                            {
                                continue;
                            }
                            Gizmos.DrawLine(path[i], path[i + 1]);
                        }
                    }
                }
            }
        }
#endif
    }
}