/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   SelectScene.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/4/20 9:45:58
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;
using UnityEngine;

public class SelectScene : IBattleScene
{
    public override void Init()
    {
        EventLuaHelper.Instance.RegesterListener(EventLuaDefine.N0_Character_Is_Selected, OnNoCharacterIsSelected, "NoCharacterIsSelected");
        EventLuaHelper.Instance.RegesterListener(EventLuaDefine.No_Map_Is_Selected, OnNoMapIsSelected, "NoMapIsSelected");
    }

    public override void Release()
    {
        EventLuaHelper.Instance.RemoveListener(EventLuaDefine.N0_Character_Is_Selected, "NoCharacterIsSelected");
        EventLuaHelper.Instance.RemoveListener(EventLuaDefine.No_Map_Is_Selected, "NoMapIsSelected");
    }

    /// <summary>
    /// 角色选定完毕
    /// </summary>
    /// <param name="data"></param>
    private void OnNoCharacterIsSelected(object data)
    {
        ioo.characterSystem.NoCharacterIsSelect();

        ioo.gameMode.RunState(E_GameState.SelectMap);
    }

    /// <summary>
    /// 倒计时结束，还没有场景被选择，则选择默认场景
    /// </summary>
    /// <param name="data"></param>
    private void OnNoMapIsSelected(object data)
    {
        ioo.gameMode.SetSelectedMap(Define.DEFAULT_MAP_ID);
    }

    public override Vector3 GetCirclePositionByName(string name)
    {
        throw new NotImplementedException();
    }

    public override List<Transform> GetHoldPositionByLV(int lv)
    {
        throw new NotImplementedException();
    }
}