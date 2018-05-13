/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   SelectSceneCamera.cs
 * 
 * 简    介:    从平视角色视角切换到俯视地图视角
 * 
 * 创建标识：   Pancake 2017/9/23 15:35:48
 * 
 * 修改描述：
 * 
 */


using UnityEngine;
using System.Collections;
using DG.Tweening;
using DG.Tweening.Core;

public class SelectSceneCamera : MonoBehaviour
{
    public Transform TargetTran;
    public Transform CameraTran;
    // Use this for initialization
    void Start()
    {
        EventDispatcher.AddEventListener(EventDefine.Event_Character_To_Map, OnCameraSwitch);    }

    void Destroy()
    {
        EventDispatcher.RemoveEventListener(EventDefine.Event_Character_To_Map, OnCameraSwitch);
    }

    private void OnCameraSwitch()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(CameraTran.transform.DOLocalMove(TargetTran.localPosition, 1));
        sequence.Join(CameraTran.transform.DOLocalRotate(TargetTran.localEulerAngles, 1));

        ioo.playerManager.CharacterEnd();
    }
}
