/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   SelectMapBuilder.cs
 * 
 * 简    介:    构建地图
 * 
 * 创建标识：   Pancake 2018/3/3 11:05:01
 * 
 * 修改描述：   
 * 
 */

using UnityEngine;

public class SelectMapBuilder : ICharacterBuilder
{
    public SelectMapBuilder(ICharacter character, int characterID, CharacterRefreshPO characterRefreshPO) : base(character, characterID, characterRefreshPO)
    {
    }

    public override void AddCharacterAttr()
    {
        CharacterBaseAttr baseAttr = FactoryManager.attrFactory.GetCharacterBaseAttr(mCharacterID);
        mPrefabName = baseAttr.prefabName;
        IAttrStrategy attrStrategy = new SelectMapAttrStrategy();
        mSpawnPosition = attrStrategy.GetSpawnPosition(mCharacterRefreshPO);
        ICharacterAttr attr = new SelectMapAttr(attrStrategy, baseAttr);
        mCharacter.attr = attr;
    }

    public override void AddGameObject()
    {
        GameObject characterGO = PoolManager.Instance.Spawn(mPrefabName);
        characterGO.transform.position = mSpawnPosition;
        mCharacter.gameObject = characterGO;
    }

    public override void AddInCharacterSystem()
    {
        ioo.characterSystem.AddSelectMap(mCharacter as SelectMap);
    }

    public override ICharacter GetResult()
    {
        return mCharacter;
    }
}
