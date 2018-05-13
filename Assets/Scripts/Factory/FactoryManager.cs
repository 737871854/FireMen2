/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   FactoryManager.cs
 * 
 * 简    介:    工厂管理器
 * 
 * 创建标识：   Pancake 2018/3/2 11:51:20
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;

public static class FactoryManager
{
    private static ICharacterFactory mFireMonsterFactory = null;
    private static ICharacterFactory mHugeFireMonsterFactory = null;
    private static IAttrFactory mAttrFactory = null;
    private static ICharacterFactory mSelectCharacterFactory;
    private static ICharacterFactory mSelectMapFactory;
    private static ICharacterFactory mCitizenFactory;
    private static ICharacterFactory mNpcFactory;
    private static ICharacterFactory mHelicopterFactory;
    private static ICharacterFactory mEliteMonsterFactory;
    private static ICharacterFactory mCoinFactory;
    private static ICharacterFactory mPropFactory;
    private static ICharacterFactory mBullDemonKingFactory;
    private static ICharacterFactory mBearFactory;
    private static ICharacterFactory mWolfFactory;

    public static IAttrFactory attrFactory
    {
        get
        {
            if (mAttrFactory == null)
            {
                mAttrFactory = new AttrFactory();
            }
            return mAttrFactory;
        }
    }
    public static ICharacterFactory fireMonsterFactory
    {
        get
        {
            if (mFireMonsterFactory == null)
            {
                mFireMonsterFactory = new FireMonsterFactory();
            }
            return mFireMonsterFactory;
        }
    }
    public static ICharacterFactory hugeFireMonsterFactory
    {
        get
        {
            if(mHugeFireMonsterFactory == null)
            {
                mHugeFireMonsterFactory = new HugeFireMonsterFactory();
            }
            return mHugeFireMonsterFactory;
        }
    }
    public static ICharacterFactory selectCharacterFactory
    {
        get
        {
            if(mSelectCharacterFactory == null)
            {
                mSelectCharacterFactory = new SelectCharacterFactory();
            }
            return mSelectCharacterFactory;
        }
    }
    public static ICharacterFactory selectMapFactory
    {
        get
        {
            if(mSelectMapFactory == null)
            {
                mSelectMapFactory = new SelectMapFactory();
            }
            return mSelectMapFactory;
        }
    }
    public static ICharacterFactory citizenFactory
    {
        get
        {
            if(mCitizenFactory == null)
            {
                mCitizenFactory = new CitizenFactory();
            }
            return mCitizenFactory;
        }
    }
    public static ICharacterFactory npcFactory
    {
        get
        {
            if(mNpcFactory == null)
            {
                mNpcFactory = new NpcFactory();
            }
            return mNpcFactory;
        }
    }
    public static ICharacterFactory helicopterFactory
    {
        get
        {
            if(mHelicopterFactory == null)
            {
                mHelicopterFactory = new HelicopterFactory();
            }
            return mHelicopterFactory;
        }
    }
    public static ICharacterFactory eliteMonsterFactory
    {
        get
        {
            if(mEliteMonsterFactory == null)
            {
                mEliteMonsterFactory = new EliteMonsterFactory();
            }
            return mEliteMonsterFactory;
        }
    }
    public static ICharacterFactory coinFactory
    {
        get
        {
            if(mCoinFactory == null)
            {
                mCoinFactory = new CoinFactory();
            }
            return mCoinFactory;
        }
    }
    public static ICharacterFactory propFactory
    {
        get
        {
            if (mPropFactory == null)
                mPropFactory = new PropFactory();
            return mPropFactory;
        }
    }
    public static ICharacterFactory bullDemonKingFactory
    {
        get
        {
            if(mBullDemonKingFactory == null)
            {
                mBullDemonKingFactory = new BullDemonKingFactory();
            }
            return mBullDemonKingFactory;
        }
    }
    public static ICharacterFactory bearFactory
    {
        get
        {
            if (mBearFactory == null)
                mBearFactory = new BearFactory();
            return mBearFactory;
        }
    }
    public static ICharacterFactory wolfFactory
    {
        get
        {
            if (mWolfFactory == null)
                mWolfFactory = new WolfFactory();
            return mWolfFactory;
        }
    }
}