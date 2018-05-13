/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   SpawnHelicopterCommand.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/3/21 13:31:20
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;

public class SpawnHelicopterCommand : ISpawnCommand
{
    protected Citizen mCitizen;
    protected int mPlayerID;
    public SpawnHelicopterCommand(int characterID, CharacterRefreshPO characterRefreshPO, Citizen citizen, int playerID) : base(characterID, characterRefreshPO)
    {
        mCitizen = citizen;
        mPlayerID = playerID;
    }

    public override void Execute()
    {
        Helicopter helicopter = FactoryManager.helicopterFactory.CreateCharacter<Helicopter>(mCharacterID, mCharacterRefreshPO) as Helicopter;
        helicopter.playerID = mPlayerID;
        if(mCitizen == null)
        {
            helicopter.SetMissionType(E_HelicopterMissionType.FireFighting);
            helicopter.gameObject.transform.position = ioo.battleScene.support0.position;
        }
        else
        {
            mCitizen.canCallRescued = false;
            helicopter.SetCitizen(mCitizen);
            helicopter.SetMissionType(E_HelicopterMissionType.SaveCitizen);
            helicopter.gameObject.transform.position = mCitizen.gameObject.transform.right * 8 + mCitizen.gameObject.transform.position + UnityEngine.Vector3.up * 1.0f + mCitizen.gameObject.transform.forward * 0.21f;
        }
    }
}