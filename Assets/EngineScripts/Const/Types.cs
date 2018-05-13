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
    /// IOͨ��
    /// </summary>
    External = 0,
    /// <summary>
    /// ����
    /// </summary>
    Builtin = 1,
}


/// <summary>
/// ��Ϸ״̬
/// </summary>
public enum E_GameState
{
    /// <summary>
    /// ��ʼ����
    /// </summary>
    Start,

    /// <summary>
    /// ������Ƶ
    /// </summary>
    Movie,

    /// <summary>
    /// ѡ��
    /// </summary>
    SelectCharacter,

    /// <summary>
    /// ѡ��ͼ
    /// </summary>
    SelectMap,

    /// <summary>
    /// ����
    /// </summary>
    Loading,

    /// <summary>
    /// ѡ�����
    /// </summary>
    SelectEnd,

    /// <summary>
    /// �ȴ�
    /// </summary>
    Waitting,

    /// <summary>
    /// ������Ϸ
    /// </summary>
    Play,

    /// <summary>
    /// ��ˮ
    /// </summary>
    Water,

    /// <summary>
    /// ����ٳ����
    /// </summary>
    Hold,

    /// <summary>
    /// �Ƿ������Ϸ
    /// </summary>
    Continue,

    /// <summary>
    /// ��Ϸ����
    /// </summary>
    Summary,

    /// <summary>
    /// ��̨
    /// </summary>
    Setting,
}


/// <summary>
/// ��������
/// </summary>
public enum E_AgentType
{
    /// <summary>
    /// ��ɫ
    /// </summary>
    Player = 0,
    /// <summary>
    /// ��ͼ
    /// </summary>
    Map = 1,
    /// <summary>
    /// Ӧ����
    /// </summary>
    Box = 2,
    /// <summary>
    /// �����
    /// </summary>
    Extinguisher = 3,
    /// <summary>
    /// ����˨
    /// </summary>
    Hydrant = 4,
    /// <summary>
    /// ����ͧ
    /// </summary>
    LifeBoat = 5,
    /// <summary>
    /// ����Ȧ
    /// </summary>
    LifeBuoy = 6,
    /// <summary>
    /// �ƶ������
    /// </summary>
    MoveExtinguisher = 7,
    /// <summary>
    /// ����ˮ��
    /// </summary>
    SuperWater = 8,
    /// <summary>
    /// �绰
    /// </summary>
    Telephone = 9,
    /// <summary>
    /// ˮ��
    /// </summary>
    Water = 10,
    /// <summary>
    /// ˮƿ
    /// </summary>
    WaterBottle = 11,
    /// <summary>
    /// ˮ��
    /// </summary>
    WaterWheel = 12,
    /// <summary>
    /// ˮ��
    /// </summary>
    Well = 13,
    /// <summary>
    /// ����
    /// </summary>
    HugeFireMonster = 14,
    /// <summary>
    /// С���
    /// </summary>
    MiniFireMonster = 15,
    /// <summary>
    /// �̹�
    /// </summary>
    SmokeMonster = 16,
    /// <summary>
    /// ţħ��
    /// </summary>
    BullDemonKing = 17,
    /// <summary>
    /// ��
    /// </summary>
    AngerBear = 18,
    /// <summary>
    /// ��
    /// </summary>
    Pterosaur = 19,
    /// <summary>
    /// ����
    /// </summary>
    Npc = 20,
    /// <summary>
    /// ֱ����
    /// </summary>
    Helicopter = 21,
    /// <summary>
    /// ����
    /// </summary>
    Citizen0 = 22,
    Citizen1 = 23,
    Citizen2 = 24,
    /// <summary>
    /// ���
    /// </summary>
    Coin = 25,
    /// <summary>
    /// ��Ӣ��
    /// </summary>
    Elite = 26,
    /// <summary>
    /// ��������
    /// </summary>
    Freeze = 27,
    /// <summary>
    /// ֧Ԯ����
    /// </summary>
    Support = 28,
    /// <summary>
    /// ɳ��
    /// </summary>
    SandBox = 29,
    /// <summary>
    /// ��
    /// </summary>
    Wolf = 30,
    /// <summary>
    /// ��Ӣ��
    /// </summary>
    Dragon = 31,
}

public enum E_CharacterType
{
    /// <summary>
    /// ��ɫ
    /// </summary>
    Player = 0,
    /// <summary>
    /// ��ͼ
    /// </summary>
    Map = 1,
    /// <summary>
    /// Ӧ����
    /// </summary>
    Box = 2,
    /// <summary>
    /// �����
    /// </summary>
    Extinguisher = 3,
    /// <summary>
    /// ����˨
    /// </summary>
    Hydrant = 4,
    /// <summary>
    /// ����ͧ
    /// </summary>
    LifeBoat = 5,
    /// <summary>
    /// ����Ȧ
    /// </summary>
    LifeBuoy = 6,
    /// <summary>
    /// �ƶ������
    /// </summary>
    MoveExtinguisher = 7,
    /// <summary>
    /// ����ˮ��
    /// </summary>
    SuperWater = 8,
    /// <summary>
    /// �绰
    /// </summary>
    Telephone = 9,
    /// <summary>
    /// ˮ��
    /// </summary>
    Water = 10,
    /// <summary>
    /// ˮƿ
    /// </summary>
    WaterBottle = 11,
    /// <summary>
    /// ˮ��
    /// </summary>
    WaterWheel = 12,
    /// <summary>
    /// ˮ��
    /// </summary>
    Well = 13,
    /// <summary>
    /// ����
    /// </summary>
    HugeFireMonster = 14,
    /// <summary>
    /// С���
    /// </summary>
    MiniFireMonster = 15,
    /// <summary>
    /// �̹�
    /// </summary>
    SmokeMonster = 16,
    /// <summary>
    /// ţħ��
    /// </summary>
    BullDemonKing = 17,
    /// <summary>
    /// ��
    /// </summary>
    AngerBear = 18,
    /// <summary>
    /// ��
    /// </summary>
    Pterosaur = 19,
    /// <summary>
    /// ����
    /// </summary>
    Npc = 20,
    /// <summary>
    /// ֱ����
    /// </summary>
    Helicopter = 21,
    /// <summary>
    /// ����
    /// </summary>
    Citizen0 = 22,
    Citizen1 = 23,
    Citizen2 = 24,
    /// <summary>
    /// ���
    /// </summary>
    Coin = 25,
    /// <summary>
    /// ��Ӣ��
    /// </summary>
    Elite = 26,
    /// <summary>
    /// ��������
    /// </summary>
    Freeze = 27,
    /// <summary>
    /// ֧Ԯ����
    /// </summary>
    Support = 28,
    /// <summary>
    /// ɳ��
    /// </summary>
    SandBox = 29,
    /// <summary>
    /// ��
    /// </summary>
    Wolf = 30,
    /// <summary>
    /// ��Ӣ��
    /// </summary>
    Dragon = 31,
    /// <summary>
    /// ˮͰ
    /// </summary>
    Bucket = 32,
}


public enum E_SceneLevelID
{
    // ��ɫ��ͼѡ�񳡾�
    Level_0 = 0,
    // С��
    Level_1_0 = 1,

    Level_1_1 = 2,

    Level_1_2 = 3,
    // ũ��
    Level_2 = 4,
    // ����
    Level_3 = 5,
}

/// <summary>
/// �����Ϸ״̬
/// </summary>
public enum E_PlayerState
{
    /// <summary>
    /// �ȴ�
    /// </summary>
    Waitting = 0,
    /// <summary>
    /// ������
    /// </summary>
    Play = 1,
    /// <summary>
    /// �Ƿ�����
    /// </summary>
    Continue = 2,
    /// <summary>
    /// �ٳ�
    /// </summary>
    Hold = 3,
    /// <summary>
    /// ����
    /// </summary>
    Dead = 4,
}


/// <summary>
/// ��ʧ����
/// </summary>
public enum E_DisappearType
{
    /// <summary>
    /// ���������٣��򱻴ݻ��⣬������ʧ
    /// </summary>
    Normal,
    /// <summary>
    /// ʱ��ľ�����ʧ
    /// </summary>
    CanDisappear,
}

public enum E_Fitment
{
    /// <summary>
    /// ����
    /// </summary>
    Desk = 0,
    /// <summary>
    /// 0������
    /// </summary>
    Sofa0 = 1,
    /// <summary>
    /// 2������
    /// </summary>
    Sofa1 = 2,
}


/// <summary>
/// ��Ϊ����
/// </summary>
public enum E_ActionType
{
    UnKonw = -1,
    /// <summary>
    /// ��������Ϊ,��С��֣�����ֹ�����Ļ�����񱻻��׷��
    /// </summary>
    Normal = 0,
    /// <summary>
    /// ���﹥������
    /// </summary>
    AttackCitizen = 1,
    /// <summary>
    /// ����ȴ�ֱ������Ԯ
    /// </summary>
    WaitForHelp = 2,
    /// <summary>
    /// ��ɫԲ��
    /// </summary>
    SpecialCircle = 3,
    /// <summary>
    /// ҡ��
    /// </summary>
    ShakeScreen = 4,
    /// <summary>
    /// ��������
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
    /// �ȴ�����ͧ
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
    /// ������
    /// </summary>
    SaveCitizen,
    /// <summary>
    /// �Ȼ�
    /// </summary>
    FireFighting,
}


class Types
{

}