/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   Define.cs
 * 
 * 简    介:    存放游戏中枚举,静态变量，常量的定义
 * 
 * 创建标识：   Pancake 2017/4/24 18:16:24
 * 
 * 修改描述：
 * 
 */


using System;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    /// <summary>
    /// 玩家最大生命值
    /// </summary>
    public static int GAME_CONFIG_PLAYER_MAX_HEALTH = 10;

    /// <summary>
    /// 每帧时间;
    /// </summary>
    public static float FRAME_TIME = 0.0166666f;

    /// <summary>
    /// 水枪攻击间隔
    /// </summary>
    public static float GAME_CONFIG_WATER_DAMAGE_INTERVAL = 0.2f;

    /// <summary>
    /// 最大玩家数
    /// </summary>
    public static int MAX_PLAYER_NUMBER = 3;

    /// <summary>
    /// Npc种类个数
    /// </summary>
    public static int MAX_NPC_NUMBER = 5;

    /// <summary>
    /// 游戏难度
    /// </summary>
    public static int GAME_CONFIG_DIFFICULTY = 1;

    /// <summary>
    /// 默认场景ID
    /// </summary>
    public static int DEFAULT_MAP_ID = 0;

    /// <summary>
    /// 怪物受到攻击冻结时间
    /// </summary>
    public static float FREEZE_TIME = 0.2f;

    /// <summary>
    /// 窗户开关时间
    /// </summary>
    public static float GAME_CONFIG_CLOSE_DOOR_TIME = 5.0f;

    /// <summary>
    /// 屏幕分区数量
    /// </summary>
    public static int GAME_SCREEN_AREA_COUNT = 3;

    /// <summary>
    /// 屏幕分区生命单位最大个数
    /// </summary>
    public static int GAME_SCREEN_AREA_AGENT_MAX_COUNT = 2;

    /// <summary>
    /// 加水阶段，拍一次确认按钮，加水量
    /// </summary>
    public static float GAME_FILL_WATER_PRECENT_PRESS = 0.02f;
    /// <summary>
    /// 加水阶段，拍一次的加水量
    /// </summary>
    public static int GAME_FILL_WATER_PERCENT_VALUE = 3;

    /// <summary>
    /// 玩家被劫持阶段，拍一次确认按钮，劫持进度增加值
    /// </summary>
    public static float GAME_HOLD_PLAYER_PERCENT_PRESS = 0.03334f;

    public static float PATH_STEP = 0.1f;
}

public class DefineMaskLayer
{
    public static LayerMask  MASK_OBJECT = 1 << LayerMask.NameToLayer(SceneLayerMask.Actor);
    public static LayerMask MASK_TARGET = 1 << LayerMask.NameToLayer(SceneLayerMask.Target);
}


/// <summary>
/// 射击点信息
/// </summary>
public class HittingPart
{
    public Vector3 uiPos;
    public float maxHp;
    public float curHp;
    public float scale;
}

/// <summary>
/// 掉落物品id和概率
/// </summary>
public class DropProp
{
    public int ID;
    public int[] Rate = new int[2];
}

/// <summary>
/// 掉落信息
/// </summary>
public class DropPropInfo
{
    private List<DropProp> dropList;

    public int Count { get { return dropList.Count; } }
    public DropProp GetDrop(int index)
    {
        if (dropList.Count > index)
            return dropList[index];
        return null;
    }
    public void AddDrop(int[] drop)
    {
        if (dropList == null)
            dropList = new List<DropProp>();

        if (drop.Length != 3)
            return;
        DropProp dropProp   = new DropProp();
        dropProp.ID         = drop[0];
        dropProp.Rate[0]    = drop[1];
        dropProp.Rate[1]    = drop[2];
        dropList.Add(dropProp);
    }
}

/// <summary>
/// Tag
/// </summary>
public class GameTage
{
    /// <summary>
    /// UI相机
    /// </summary>
    public const string UICamera = "UICamera";
    /// <summary>
    /// 场景主相机
    /// </summary>
    public const string MainCanvas = "MainCanvas";

    /// <summary>
    /// 相机路径点父节点
    /// </summary>
    public const string WayParent = "WayParent";

    /// <summary>
    /// Battle0_0怪物路径点存放处
    /// </summary>
    public const string CameraPath = "CameraPath";

    /// <summary>
    /// 市民
    /// </summary>
    public const string Citizen = "Citizen";

    /// <summary>
    /// 怪物
    /// </summary>
    public const string Monster = "Monster";

    /// <summary>
    /// 相机路径
    /// </summary>
    public const string PluginCameraPath = "PluginCameraPath";

    /// <summary>
    /// 存放怪物路径的父节点
    /// </summary>
    public const string WayPoints = "WayPoints";

    /// <summary>
    /// 救生艇
    /// </summary>
    public const string LifeBoat = "LifeBoat";
}


/// <summary>
/// 层名定义
/// </summary>
public class SceneLayerMask
{
    ///// <summary>
    ///// 怪物层
    ///// </summary>
    //public const string Monster = "Monster";

    /// <summary>
    /// 绑定点层
    /// </summary>
    public const string Target = "Target";

    /// <summary>
    /// 可被射击层 角色，怪物，道具
    /// </summary>
    public const string Actor = "Actor";

    public const string Default = "Default";

    /// <summary>
    ///  地形
    /// </summary>
    public const string Terrain = "Terrain";
}

/// <summary>
/// 模型名称
/// </summary>
public class PoolItemName
{
    // 角色
    public const string Player0 = "player0";
    public const string Player1 = "player1";
    public const string Player2 = "player2";

    // 地图
    public const string Map0 = "map0";
    public const string Map1 = "map1";
    public const string Map2 = "map2";

    // 道具
    public const string Box = "box";
    public const string Extinguisher = "extinguisher";
    public const string Hydrant = "hydrant";
    public const string LifeBoat = "life_boat";
    public const string LifeBuoy = "life_buoy";
    public const string MoveExtinguisher = "move_extinguisher";
    public const string SuperWater = "super_water";
    public const string Telephone = "telephone";
    public const string Water = "water";
    public const string WaterBottle = "water_ bottle";
    public const string WaterWheel = "water_wheel";
    public const string Well = "well";
    public const string Coin = "coin";
    public const string Freeze = "freeze";
    public const string Support = "support";
    public const string SandBox = "sand_box";
    public const string Bucket = "bucket";

    // 小怪
    public const string HugeFireMonster = "huge_fire_monster";
    public const string MiniFireMonster = "mini_fire_monster";
    public const string SmokeFireMonster = "smoke_fire_monster";

    // Boss
    public const string BullMemonKing = "bull_demon_king";
    public const string AngerBear = "bear";
    public const string Pterosaur = "pterosaur";

    // 动物
    public const string Npc = "npc";

    // 直升机
    public const string Helicopter = "helicopter";

    // 市民
    public const string Citizen0 = "citizen0";
    public const string Citizen1 = "citizen1";
    public const string Citizen2 = "citizen2";

    // 精英怪
    public const string EliteMonster = "elite_monster";

    // 狼
    public const string Wolf = "wolf";

    public const string Dragon = "dragon";

    public const string Ball = "ball";

    #region 特效
    // 小火怪出生绑定特效
    public const string Effect_Mini_Monster_Born = "effect_mini_fire_monster_bron";
    // 烟雾怪出生绑定特效
    public const string Effect_Smoke_Monster_Born = "effect_smoke_monster_born";
    #endregion
}


/// <summary>
/// 场景名称
/// </summary>
public class SceneNames
{
    public const string CoinScene = "CoinScene";

    public const string SelectScene = "SelectScene";

    public const string empty = "empty";

    public const string BattleScene0_0 = "BattleScene0_0";
    public const string BattleScene0_1 = "BattleScene0_1";
    public const string BattleScene0_2 = "BattleScene0_2";

    public const string BattleScene1 = "BattleScene1";

    public const string BattleScene2 = "BattleScene2";

    public const string SettingScene = "SettingScene";
}

/// <summary>
/// 自定义监听事件
/// </summary>
public class EventDefine
{
    /// <summary>
    /// 激活灯光圆环，喷金币功能
    /// </summary>
    public const string Event_Active_Circle_Coin = "Event_Active_Circle_Coin";

    /// <summary>
    /// 灯光圆环，旋转
    /// </summary>
    public const string Event_Circle_Rotation = "Event_Circle_Rotation";

    /// <summary>
    /// 场景所有资源加载完毕
    /// </summary>
    public const string Event_All_Resource_Is_Load = "Event_All_Resource_Is_Load";

    /// <summary>
    /// 关卡系统可以更新事件
    /// </summary>
    public const string Event_Stage_System_Is_Pause = "Event_Stage_System_Is_Pause";

    /// <summary>
    /// 玩家选择角色
    /// </summary>
    public const string Event_Player_Select_Character   = "Event_Player_Select_Character";
    /// <summary>
    /// 选择地图
    /// </summary>
    public const string Event_Player_Select_Map         = "Event_Player_Select_Map";
    /// <summary>
    /// 选角色切换到选地图
    /// </summary>
    public const string Event_Character_To_Map          = "Event_Character_To_Map";

    /// <summary>
    /// 游戏结束（所有玩家死亡，或玩家通关）
    /// </summary>
    public const string Event_Game_Over                 = "Event_Game_Over";

    /// <summary>
    /// 拍确认按钮
    /// </summary>
    public const string Event_Key_Sure                  = "Event_Key_Sure";

    /// <summary>
    /// 救生艇准备好了
    /// </summary>
    public const string Event_Citizen_Boat_Is_Ready     = "Event_Citizen_Boat_Is_Ready";

    /// <summary>
    /// 冰冻道具使用
    /// </summary>
    public const string Event_Freeze_Prop               = "Event_Freeze_Prop";

    /// <summary>
    /// 获得支援道具
    /// </summary>
    public const string Event_Get_Support_Prop          = "Event_Get_Support_Prop";

    /// <summary>
    /// 通关失败
    /// </summary>
    public const string Event_Game_Defeat               = "Event_Game_Defeat";

    /// <summary>
    /// 通关成功
    /// </summary>
    public const string Event_Game_Success              = "Event_Game_Success";

    /// <summary>
    /// 沙箱可以被使用
    /// </summary>
    public const string Event_SandBox_Can_Be_Spray = "Event_SandBox_Can_Be_Spray";

    // 3D裸眼
    public const string Event_Active_Boss_Black = "Event_Active_Boss_Black";

    /// <summary>
    /// 牛魔王技能触发
    /// </summary>
    public const string Event_Bull_Demon_King_Use_Skill_Desk = "Event_Bull_Demon_King_Use_Skill_Desk";
    public const string Event_Bull_Demon_King_Use_Skill_Left_Sofa = "Event_Bull_Demon_King_Use_Skill_Left_Sofa";
    public const string Event_Bull_Demon_King_Use_Skill_Right_Sofa = "Event_Bull_Demon_King_Use_Skill_Right_Sofa";
    public const string Event_Bull_Demon_King_Use_Skill_Fire_Fist = "Event_Bull_Demon_King_Use_Skill_Fire_Fist";
    public const string Event_Bull_Demon_King_Use_Skill_Fire_Cricle = "Event_Bull_Demon_King_Use_Skill_Fire_Cricle";
    public const string Event_Bull_Demon_King_Use_Skill_OX_Horn = "Event_Bull_Demon_King_Use_Skill_OX_Horn";

    /// <summary>
    /// 熊使技能触发
    /// </summary>
    public const string Event_Bear_Use_Skill_Claw = "Event_Bear_Use_Skill_Claw";
    public const string Event_Bear_Use_Skill_Beat = "Event_Bear_Use_Skill_Beat";
    public const string Event_Bear_Use_Skill_Ball = "Event_Bear_Use_Skill_Ball";

    // 取消射击点
    public const string Event_DisActive_HitPoint = "Event_DisActive_HitPoint";

    /// <summary>
    /// 启动抓屏
    /// </summary>
    public const string Event_Monster_Hold_Screen = "Event_Monster_Hold_Screen";

    /// <summary>
    /// 劫持挣扎成功与否
    /// </summary>
    public const string Event_Struggle_Hold_Success = "Event_Struggle_Hold_Success";

    /// <summary>
    /// 海洋场景激活快艇
    /// </summary>
    public const string Event_Speed_Boat_Active = "Event_Speed_Boat_Active";

    ///// <summary>
    ///// 激活熊boss拍打射击点
    ///// </summary>
    //public const string Event_Active_Boss_Body_HitPoint_Claw = "Event_Active_Boss_Body_HitPoint_Claw";
    //public const string Event_Active_Boss_Body_HitPoint_Beat = "Event_Active_Boss_Body_HitPoint_Beat";
    //public const string Event_Active_Boss_Body_HitPoint_Ball = "Event_Active_Boss_Body_HitPoint_Ball";
    //public const string Event_Active_Boss_Ball = "Event_Active_Boss_Ball";
    // 召唤物
    public const string Summoned_Explosed = "Summoned_Explosed";

    public const string Boss_Hit_Point_Break = "Boss_Hit_Point_Break";

    public const string Boss_Change_State = "Boss_Change_State";

    public const string Event_Active_CanSkill = "Event_Active_CanSkill";

    public const string Event_Active_Can = "Event_Active_Can";

}


