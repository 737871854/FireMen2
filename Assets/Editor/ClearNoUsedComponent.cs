/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   ClearModle.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2016/10/13 16:58:58
 * 
 * 修改描述：   用来清除场景中模型上的Animatorh和Collider Box
 * 
 */


using UnityEditor;
using UnityEngine;
using System.Collections;

public class ClearNoUsedComponent
{
    [MenuItem("Tools/删除场景中没用的MeshCollider和Animator")]
    static public void Remove()
    {
        // 获取当前场景里的所有游戏对象
        GameObject[] rootobjects = (GameObject[])UnityEngine.Object.FindObjectsOfType(typeof(GameObject));

        // 遍历游戏对象
        foreach (GameObject go in rootobjects)
        {
            if (null != go && go.transform.parent != null)
            {
                Renderer render = go.GetComponent<Renderer>();
                if (null != render && render.sharedMaterial != null && render.sharedMaterial.shader.name == "Diffuse" && render.sharedMaterial.color == Color.white)
                {
                    render.sharedMaterial.shader = Shader.Find("Mobile/Diffuse");
                }
            }
        }

        foreach (MeshCollider collider in UnityEngine.Object.FindObjectsOfType(typeof(MeshCollider)))
        {
            GameObject.DestroyImmediate(collider);
        }

        foreach (Animator animator in UnityEngine.Object.FindObjectsOfType(typeof(Animator)))
        {
            if (animator.runtimeAnimatorController == null)
            {
                GameObject.DestroyImmediate(animator);
            }
        }

        foreach (Animation animation in UnityEngine.Object.FindObjectsOfType(typeof(Animation)))
        {
            if (animation.clip == null)
            {
                GameObject.DestroyImmediate(animation);
            }
        }

        AssetDatabase.SaveAssets();
    }
}
