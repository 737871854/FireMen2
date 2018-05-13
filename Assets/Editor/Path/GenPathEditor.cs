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


using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MapWayPoint), true)]
public class GenPathEditor : Editor
{

    private List<Transform> mPoints;

    public override void OnInspectorGUI()
    {
        MapWayPoint mapWayPoint = target as MapWayPoint;

        EditorGUILayout.PropertyField(serializedObject.FindProperty("pointList"), true);

        if (GUILayout.Button("+"))
        {
            Transform[] child = mapWayPoint.transform.GetComponentsInChildren<Transform>();
            int count = 1;
            //if (child.Length == 1)
            //    count = 2;
            //else
            //    count = 1;

            for (int i = 0; i < count; ++i)
            {
                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.name = "point_" + (child.Length - 1);
                mapWayPoint.AddPoint(cube);
                cube.transform.position = child[child.Length - 1].position;
            }
        }
    }
}
