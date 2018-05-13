/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   CameraManager.cs
 * 
 * 简    介:    相机管理类：多人镜头， 相机震动，相机用移动
 * 
 * 创建标识：   Pancake 2017/4/27 15:45:17
 * 
 * 修改描述：   增添相机移动功能, 集成ShakeController功能，2017/10/8 @Pancake
 * 
 */


using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using System;

public class CameraManager : MonoBehaviour
{
    private ICameraOperate mCameraOperate;

    public Transform cTransform { get { return mCameraOperate.cTransform; } }
    public Vector3 position { get { return mCameraOperate.cTransform.position; } }
    public Vector3 right { get { return mCameraOperate.cTransform.right; } }
    public Vector3 forward { get { return mCameraOperate.cTransform.forward; } }
    public Vector3 parcentRight { get { return mCameraOperate.parcentRight; } }
    public Vector3 parcentForward { get { return mCameraOperate.parcentForward; } }

    /// <summary>
    /// 相机移动完成后出发的事件
    /// </summary>
    /// <param name="action"></param>
    public void SetMoveEndAction(Action action) { mCameraOperate.SetMoveEndAction(action); }

    #region Unity Call Back
    void Awake()
    {
        ioo.gameManager.RegisterUpdate(UpdatePreFrame);
        ioo.gameManager.RegisterFixedUpdate(UpdateFixedFrame);

        ioo.gameEventSystem.RegisterObserver(GameEventType.NewStage, new NewStageObserverCamera(this));
    }

    void Destroy()
    {
        ioo.gameManager.UnregisterUpdate(UpdatePreFrame);
        ioo.gameManager.UnregisterFixedUpdate(UpdateFixedFrame);
    }
    #endregion

    #region Public Function

    #region   相机动画
    public void LookAt(Transform target)
    {
        mCameraOperate.LookAt(target);
    }

    public bool IsCPAPlaying() { return mCameraOperate.IsPlaying(); }
    
    /// <summary>
    /// 播放相机动画
    /// </summary>
    public void PlayCPA() { mCameraOperate.PlayCPA(); }
    /// <summary>
    /// 暂停播放
    /// </summary>
    public void PauseCPA() { mCameraOperate.PauseCPA(); }

    public bool CPAIsPlaying { get { return mCameraOperate.IsPlaying(); } }

    #region BullDemonKing 场景
    // BullDemonKing相机后退功能
    public void BullCameraBackLens()
    {
        Battle0_2_CameraOperate bco = mCameraOperate as Battle0_2_CameraOperate;
        bco.BullCameraBackLens();
    }
    // 切换到BulldemonKing
    public void BullMiddleSkilltLens()
    {
        Battle0_2_CameraOperate bco = mCameraOperate as Battle0_2_CameraOperate;
        bco.BullMiddleSkilltLens();
    }

    public void BullLeftSkillLens()
    {
        Battle0_2_CameraOperate bco = mCameraOperate as Battle0_2_CameraOperate;
        bco.BullLeftSkillLens();
    }

    public void BullRightSkillLens()
    {
        Battle0_2_CameraOperate bco = mCameraOperate as Battle0_2_CameraOperate;
        bco.BullRightSkillLens();
    }

    public void BullReversLens()
    {
        Battle0_2_CameraOperate bco = mCameraOperate as Battle0_2_CameraOperate;
        bco.BullReversLens();
    }

    #endregion

    #region Bear 场景
    public void SlowLens(float value)
    {
        mCameraOperate.CPASpeed(value);
    }

    public void NormalSpeed()
    {
        mCameraOperate.NormalSpeed();
    }
    #endregion
    #endregion

    #region 相机震动
    public void NormalShake() { mCameraOperate.NormalShake(); }
    public void BossShortShake() { mCameraOperate.BossShortShake(); }
    public void BossLongShake() { mCameraOperate.BossLongShake(); }
    #endregion

    /// <summary>
    /// 暂停或启用相机左右微转以及移动操作, （只在场景初始化时调用一次）
    /// </summary>
    /// <param name="flag"></param>
    public void InitSceneCamera() { InitCameraOperate(); }

    /// <summary>
    /// 更改当前场景关卡
    /// </summary>
    /// <param name="stage"></param>
    public void UpdateState(int stage) { if (mCameraOperate == null) return; mCameraOperate.UpdateState(stage); }

    #endregion

    #region Private Function
    /// <summary>
    /// 帧更新
    /// </summary>
    private void UpdatePreFrame() { if (mCameraOperate == null) return; mCameraOperate.UpdatePreFrame(); }

    /// <summary>
    /// 固定帧更新
    /// </summary>
    private void UpdateFixedFrame() { if (mCameraOperate == null) return; mCameraOperate.UpdateFixedFrame(); }

    /// <summary>
    /// 相机震动，与相机位移和左右微转相互排斥
    /// </summary>
    void LateUpdate() { if (mCameraOperate == null) return; mCameraOperate.LateUpdate(); }
     

    /// <summary>
    /// 初始化相机采用策略
    /// </summary>
    private void InitCameraOperate()
    {
        switch (Application.loadedLevelName)
        {
            case SceneNames.SelectScene:
                mCameraOperate = new SelectCameraOperate(this);
                break;
            case SceneNames.BattleScene0_0:
                mCameraOperate = new Battle0_0CameraOperate(this);
                break;
            case SceneNames.BattleScene0_1:
                mCameraOperate = new Battle0_1CameraOperate(this);
                break;
            case SceneNames.BattleScene0_2:
                mCameraOperate = new Battle0_2_CameraOperate(this);
                break;
            case SceneNames.BattleScene1:
                mCameraOperate = new Battle1CameraOperate(this);
                break;
            case SceneNames.BattleScene2:
                mCameraOperate = new Battle2CameraOperate(this);
                break;
        }

        mCameraOperate.InitCameraRoll();
    }
    #endregion
}
