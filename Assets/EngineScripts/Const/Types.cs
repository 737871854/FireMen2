using UnityEngine;
using System.Collections;


public enum E_HardwareType
{
    iPhone3G = 0,
    iPhone4G = 1,
    iPad = 2,
    Max = 3,
}

public enum E_WeaponType 
{
	None = -1,
	Katana = 0,
	Body,
    Bow,
	Max,
}

public enum E_BlockState
{
    None = -1,
    Start = 0,
    Loop,
    End,
    HitBlocked,
    Failed
}

public enum E_KnockdownState
{
    None = -1,
    Down = 0,
    Loop,
    Up,
    Fatality,
}

public enum E_WeaponState
{
    NotInHands,
	Ready,
	Attacking,
	Reloading,
	Empty,
}

public enum E_AttackType
{
	None = -1,
	X = 0,
	O = 1,
    BossBash = 2,
    Fatality = 3,
    Counter = 4,
    Berserk = 5,
	Max = 6,
}
    
public enum E_EnemyType
{
	None = -1,
    SwordsMan = 0,
    Peasant = 1,
    TwoSwordsMan = 2,
    Bowman = 3,
    PeasantLow = 4,
    MiniBoss01 = 5,
    SwordsManLow = 6,
    notUsed03 = 7,
    notUsed04 = 8,
    notUsed05 = 9,
    BossOrochi = 10,
	Max
}

public enum E_GameType
{
	SinglePlayer,
    ChapterOnly,
	Survival,
    FirstTimeTutorial,
    Tutorial,
    SaleScreen,
}

public enum E_GameDifficulty
{
    Easy,
	Normal,
	Hard,
}

public enum E_DamageType
{
    Front,
    Back,
    BreakBlock,
    InKnockdown,
    Enviroment,
}

public enum E_CriticalHitType
{
    None,
    Vertical,
    Horizontal,
}

public enum E_DeadBodyType
{
    None = -1,
    Legs = 0,
    Beheaded,
    HalfBody,
    SliceFrontBack,
    SliceLeftRight,
    Max,
}

public enum E_ComboLevel
{
	One = 1,
	Two = 2,
    Three = 3,
    Max = 3
}

public enum E_ComboLevelPrice
{
    One = 0,
    Two = 1000,
    Three = 2000
}

public enum E_SwordLevel
{
    One = 1,
    Two = 2,
    Three = 3,
    Four = 4,
    Five = 5,
    Max = 5
}

public enum E_SwordLevelPrice
{
    One = 0,
    Two = 1000,
    Three = 1500,
    Four = 2000,
    Five = 3000,
}

public enum E_HealthLevel
{
    One = 1,
    Two = 2,
    Three = 3,
    Max = 3
}

public enum E_HealtLevelPrice
{
    One = 0,
    Two = 1500,
    Three = 3000,
}

public enum E_RotationType
{
    Left,
    Right
}

public enum E_MotionType
{
	None,
	Walk,
	Run,
    Sprint,
    Roll,
    Attack,
    Block,
    BlockingAttack,
    Injury,
    Knockdown,
    Death,
    AnimationDrive,
}

public enum E_MoveType
{
    None,
    Forward,
    Backward,
    StrafeLeft,
    StrafeRight,
}

public enum E_LookType
{
    None,
    TrackTarget,
}


public enum E_InteractionObjects
{
    None,
    UseLever,
    Trigger,
    UseExperience,
    TriggerAnim,
}

public enum E_InteractionType
{
    None,
    On,
    Off
}

public enum E_EventTypes
{
    None,
    EnemyStep,
    EnemySee,
    EnemyLost,
    Hit,
    Died,
    ImInPain,
    HitBlocked,
    Knockdown,
    FriendInjured,
}

public enum E_Direction
{
    Forward,
    Backward,
    Left,
    Right,
    Up,
    Down
}


public enum E_InputType
{
    None = -1,
    /// <summary>
    /// IO通信
    /// </summary>
    External = 0,
    /// <summary>
    /// 键盘
    /// </summary>
    Builtin = 1,
}


/// <summary>
/// 游戏状态
/// </summary>
public enum E_GameState
{
    /// <summary>
    /// 开始界面
    /// </summary>
    Start,

    /// <summary>
    /// 待机视频
    /// </summary>
    Movie,

    /// <summary>
    /// 选择
    /// </summary>
    SelectCharacter,

    /// <summary>
    /// 选地图
    /// </summary>
    SelectMap,

    /// <summary>
    /// 加载
    /// </summary>
    Loading,

    /// <summary>
    /// 选择结束
    /// </summary>
    SelectEnd,

    /// <summary>
    /// 等待
    /// </summary>
    Waitting,

    /// <summary>
    /// 进行游戏
    /// </summary>
    Play,

    /// <summary>
    /// 加水
    /// </summary>
    Water,

    /// <summary>
    /// 怪物劫持玩家
    /// </summary>
    Hold,

    /// <summary>
    /// 是否继续游戏
    /// </summary>
    Continue,

    /// <summary>
    /// 游戏结束
    /// </summary>
    Summary,

    /// <summary>
    /// 后台
    /// </summary>
    Setting,
}


/// <summary>
/// 代理类型
/// </summary>
public enum E_AgentType
{
    /// <summary>
    /// 角色
    /// </summary>
    Player = 0,
    /// <summary>
    /// 地图
    /// </summary>
    Map = 1,
    /// <summary>
    /// 应急包
    /// </summary>
    Box = 2,
    /// <summary>
    /// 灭火器
    /// </summary>
    Extinguisher = 3,
    /// <summary>
    /// 消防栓
    /// </summary>
    Hydrant = 4,
    /// <summary>
    /// 救生艇
    /// </summary>
    LifeBoat = 5,
    /// <summary>
    /// 救生圈
    /// </summary>
    LifeBuoy = 6,
    /// <summary>
    /// 移动灭火器
    /// </summary>
    MoveExtinguisher = 7,
    /// <summary>
    /// 超级水泡
    /// </summary>
    SuperWater = 8,
    /// <summary>
    /// 电话
    /// </summary>
    Telephone = 9,
    /// <summary>
    /// 水炮
    /// </summary>
    Water = 10,
    /// <summary>
    /// 水瓶
    /// </summary>
    WaterBottle = 11,
    /// <summary>
    /// 水车
    /// </summary>
    WaterWheel = 12,
    /// <summary>
    /// 水井
    /// </summary>
    Well = 13,
    /// <summary>
    /// 大火怪
    /// </summary>
    HugeFireMonster = 14,
    /// <summary>
    /// 小火乖
    /// </summary>
    MiniFireMonster = 15,
    /// <summary>
    /// 烟乖
    /// </summary>
    SmokeMonster = 16,
    /// <summary>
    /// 牛魔王
    /// </summary>
    BullDemonKing = 17,
    /// <summary>
    /// 熊
    /// </summary>
    AngerBear = 18,
    /// <summary>
    /// 龙
    /// </summary>
    Pterosaur = 19,
    /// <summary>
    /// 动物
    /// </summary>
    Npc = 20,
    /// <summary>
    /// 直升机
    /// </summary>
    Helicopter = 21,
    /// <summary>
    /// 市民
    /// </summary>
    Citizen0 = 22,
    Citizen1 = 23,
    Citizen2 = 24,
    /// <summary>
    /// 金币
    /// </summary>
    Coin = 25,
    /// <summary>
    /// 精英怪
    /// </summary>
    Elite = 26,
    /// <summary>
    /// 冰冻道具
    /// </summary>
    Freeze = 27,
    /// <summary>
    /// 支援道具
    /// </summary>
    Support = 28,
    /// <summary>
    /// 沙箱
    /// </summary>
    SandBox = 29,
    /// <summary>
    /// 狼
    /// </summary>
    Wolf = 30,
    /// <summary>
    /// 精英龙
    /// </summary>
    Dragon = 31,
}

public enum E_CharacterType
{
    /// <summary>
    /// 角色
    /// </summary>
    Player = 0,
    /// <summary>
    /// 地图
    /// </summary>
    Map = 1,
    /// <summary>
    /// 应急包
    /// </summary>
    Box = 2,
    /// <summary>
    /// 灭火器
    /// </summary>
    Extinguisher = 3,
    /// <summary>
    /// 消防栓
    /// </summary>
    Hydrant = 4,
    /// <summary>
    /// 救生艇
    /// </summary>
    LifeBoat = 5,
    /// <summary>
    /// 救生圈
    /// </summary>
    LifeBuoy = 6,
    /// <summary>
    /// 移动灭火器
    /// </summary>
    MoveExtinguisher = 7,
    /// <summary>
    /// 超级水泡
    /// </summary>
    SuperWater = 8,
    /// <summary>
    /// 电话
    /// </summary>
    Telephone = 9,
    /// <summary>
    /// 水炮
    /// </summary>
    Water = 10,
    /// <summary>
    /// 水瓶
    /// </summary>
    WaterBottle = 11,
    /// <summary>
    /// 水车
    /// </summary>
    WaterWheel = 12,
    /// <summary>
    /// 水井
    /// </summary>
    Well = 13,
    /// <summary>
    /// 大火怪
    /// </summary>
    HugeFireMonster = 14,
    /// <summary>
    /// 小火乖
    /// </summary>
    MiniFireMonster = 15,
    /// <summary>
    /// 烟乖
    /// </summary>
    SmokeMonster = 16,
    /// <summary>
    /// 牛魔王
    /// </summary>
    BullDemonKing = 17,
    /// <summary>
    /// 熊
    /// </summary>
    AngerBear = 18,
    /// <summary>
    /// 龙
    /// </summary>
    Pterosaur = 19,
    /// <summary>
    /// 动物
    /// </summary>
    Npc = 20,
    /// <summary>
    /// 直升机
    /// </summary>
    Helicopter = 21,
    /// <summary>
    /// 市民
    /// </summary>
    Citizen0 = 22,
    Citizen1 = 23,
    Citizen2 = 24,
    /// <summary>
    /// 金币
    /// </summary>
    Coin = 25,
    /// <summary>
    /// 精英怪
    /// </summary>
    Elite = 26,
    /// <summary>
    /// 冰冻道具
    /// </summary>
    Freeze = 27,
    /// <summary>
    /// 支援道具
    /// </summary>
    Support = 28,
    /// <summary>
    /// 沙箱
    /// </summary>
    SandBox = 29,
    /// <summary>
    /// 狼
    /// </summary>
    Wolf = 30,
    /// <summary>
    /// 精英龙
    /// </summary>
    Dragon = 31,
    /// <summary>
    /// 水桶
    /// </summary>
    Bucket = 32,
}


public enum E_SceneLevelID
{
    // 角色地图选择场景
    Level_0 = 0,
    // 小镇
    Level_1_0 = 1,

    Level_1_1 = 2,

    Level_1_2 = 3,
    // 农场
    Level_2 = 4,
    // 海洋
    Level_3 = 5,
}

/// <summary>
/// 玩家游戏状态
/// </summary>
public enum E_PlayerState
{
    /// <summary>
    /// 等待
    /// </summary>
    Waitting = 0,
    /// <summary>
    /// 进行中
    /// </summary>
    Play = 1,
    /// <summary>
    /// 是否续币
    /// </summary>
    Continue = 2,
    /// <summary>
    /// 劫持
    /// </summary>
    Hold = 3,
    /// <summary>
    /// 死亡
    /// </summary>
    Dead = 4,
}


/// <summary>
/// 消失类型
/// </summary>
public enum E_DisappearType
{
    /// <summary>
    /// 除自我销毁，或被摧毁外，不可消失
    /// </summary>
    Normal,
    /// <summary>
    /// 时间耗尽动消失
    /// </summary>
    CanDisappear,
}

public enum E_Fitment
{
    /// <summary>
    /// 桌子
    /// </summary>
    Desk = 0,
    /// <summary>
    /// 0号椅子
    /// </summary>
    Sofa0 = 1,
    /// <summary>
    /// 2号椅子
    /// </summary>
    Sofa1 = 2,
}


/// <summary>
/// 行为类型
/// </summary>
public enum E_ActionType
{
    UnKonw = -1,
    /// <summary>
    /// 无特殊行为,大小火怪，烟雾怪攻击屏幕；市民被火怪追逐
    /// </summary>
    Normal = 0,
    /// <summary>
    /// 怪物攻击市民
    /// </summary>
    AttackCitizen = 1,
    /// <summary>
    /// 市民等待直升机救援
    /// </summary>
    WaitForHelp = 2,
    /// <summary>
    /// 特色圆环
    /// </summary>
    SpecialCircle = 3,
    /// <summary>
    /// 摇屏
    /// </summary>
    ShakeScreen = 4,
    /// <summary>
    /// 攻击怪物
    /// </summary>
    AttackNpc = 5,
    /// <summary>
    /// 
    /// </summary>
    RunAndForHelp = 6,
    /// <summary>
    /// 
    /// </summary>
    RunAndEcape = 7,
    /// <summary>
    /// 等待救生艇
    /// </summary>
    WaitForBoat = 8,
}

public enum E_BearStep
{
    Step1,
    Step2,
    Step3,
    Step4,
    Step5,
    Step6,
}

public enum E_PterosaurStep
{
    UnKnow,
    Step1,
    Step2,
    Step3,
    Step4,
    Step5,
    Step6,
    Step7,
    Step8,
    Step9,
    Step10,
}

public enum E_BearSkill
{
    Jump = 0,
    Claw = 1,
    Beat = 2,
    Ball = 3,
}

public enum E_BearState
{
    UnKnow = -1,
    Idle = 0,
    Move = 1,
    Run = 2,
    Jump = 3,
    Ball = 4,
    Beat = 5,
    BeatBreak = 6,
    Claw = 7,
    ClawBreak = 8,
    Dead = 9,
    Disappear = 10,
    MoveToTarget = 11,
    Follow = 12,
    Lift = 13,
    ToHome = 14,
}

public enum E_PterosaurState
{
    UnKnow = -1,
    Idle = 0,
    Run = 1,
    Rain = 2,
    Claw = 3,
    Beat = 4,
    Throw = 5,
    Pat = 6,
    XXX = 7,
    Climb = 8,
    Eye = 9,
    Mouse = 10,
    Lift = 11,
    ClawBreak = 12,
    BeatBreak = 13,
    UpRush0 = 14,
    UpRush1 = 15,
    Wander = 16,
    PatBreak = 17,
    XXXBreak = 18,
    ClimbBreak = 19,
}

public enum E_HelicopterMissionType
{
    /// <summary>
    /// 救市民
    /// </summary>
    SaveCitizen,
    /// <summary>
    /// 救火
    /// </summary>
    FireFighting,
}


class Types
{

}