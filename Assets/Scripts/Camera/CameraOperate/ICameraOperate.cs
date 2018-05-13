/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   ICameraStrategy.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2018/4/10 10:58:26
 * 
 * 修改描述：   
 * 
 */

using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class ICameraOperate
{
    /// <summary>
    /// 主相机
    /// </summary>
    protected Camera mMainCamera;
    /// <summary>
    /// 主相机父节点
    /// </summary>
    protected Transform mParcent;
    /// <summary>
    /// 相机震动
    /// </summary>
    protected ShakeController mShakeController;
    //protected List<Vector3> mShakeScreenList;

    /// <summary>
    /// 相机路径管理
    /// </summary>
    protected CameraPathAnimator mCpa;
    protected float mPercent;
    protected float mSpeed;

    //// 特色圆环
    //protected CircleBehaviour[] mCircle;
    //protected Transform[] mSupport;

    protected CameraManager mCameraManager;
    protected int mCurrentState;

    #region 左右微转
    /// <summary>
    /// 相机左右饶Y轴选择的允许最大值
    /// </summary>
    protected float mAngle = 10;

    protected int mLeftRotation;
    protected int mRightRotation;

    protected float mRotationSpeed0 = 10;

    /// <summary>
    /// 左微转
    /// </summary>
    protected bool mLeft;

    /// <summary>
    /// 右微转
    /// </summary>
    protected bool mRight;

    /// <summary>
    /// 检测半径
    /// </summary>
    protected float mCheckRadius = 300;

    /// <summary>
    /// 相机镜头左右摇摆
    /// </summary>
    protected bool mPauseRoll;

    /// <summary>
    /// 当期相机欧拉角y值
    /// </summary>
    //private float _curRollAngle;
    protected float mCurRotation;
    protected int mFixRotation;
    //private float _curCorrected;
    #endregion

    #region 相机移动
    /// <summary>
    /// 相机运动
    /// </summary>
    private bool mPauseMove;
    /// <summary>
    /// 移动完成调用方法
    /// </summary>
    private Action mMoveEndAction;
    #endregion

    public Camera mainCamera { get { return mMainCamera; } }
    public Transform parcent { get { return mMainCamera.transform.parent; } }
    public Transform cTransform { get { return mMainCamera.transform; } }
    public Vector3 Position { get { return mMainCamera.transform.position; } }
    public Vector3 parcentRight { get { return mParcent.right; } }
    public Vector3 parcentForward { get { return mParcent.forward; } }


    public bool Left { set { mLeft = value; } }
    public bool Right { set { mRight = value; } }


    /// <summary>
    /// 相机位移和左右微转功能不可同时开启，开启Roll会自动关闭Move
    /// </summary>
    public bool PauseRoll
    {
        get
        {
            return mPauseRoll;
        }
        set
        {
            mPauseRoll = value;
            if (!mPauseRoll)
            {
                mPauseMove = true;
            }
        }
    }

    /// <summary>
    /// 相机位移和左右微转功能不可同时开启，开启Move会自动关闭Roll
    /// </summary>
    public bool PauseMove
    {
        get
        {
            return mPauseMove;
        }
        set
        {
            mPauseMove = value;
            if (!mPauseMove)
                PauseRoll = true;
        }
    }

    public ICameraOperate(CameraManager cameraManager)
    {
        mCameraManager = cameraManager;

        mMainCamera = Camera.main;
        mParcent = mMainCamera.transform.parent;
        mShakeController = new ShakeController();
        mShakeController.Init(mMainCamera.transform);

        PauseMove = true;
        PauseRoll = true;

        mPercent = 0;
        GameObject obj = GameObject.FindGameObjectWithTag(GameTage.PluginCameraPath);
        if (obj != null)
        {
            mCpa = obj.GetComponent<CameraPathAnimator>();
            mCpa.AnimationCustomEvent += OnCustomEvent;
        }
    }

    public void LookAt(Transform target)
    {
        Vector3 direction = target.position - mMainCamera.transform.position;
        Quaternion toRotation = Quaternion.LookRotation(direction);
        mMainCamera.transform.rotation = Quaternion.Lerp(mMainCamera.transform.rotation, toRotation, Time.deltaTime * 10);
        //mParcent.LookAt(target);
    }

    /// <summary>
    /// 依据节点事件，返回运动速度
    /// </summary>
    /// <returns></returns>
    public abstract float GetCameraSpeed(string eventName);

    /// <summary>
    /// 更新当前场景关卡数
    /// </summary>
    /// <param name="state"></param>
    public abstract void UpdateState(int state);

    /// <summary>
    /// 路径消息处理
    /// </summary>
    /// <param name="eventName"></param>
    public abstract void OnCustomEvent(string eventName);

    public void UpdatePreFrame()
    {
        if (mMainCamera == null || mParcent == null)
            return;

        if (!PauseRoll)
        {
            UpdateCameraRoll();
        }
    }

    public void UpdateFixedFrame()
    {
        if (mMainCamera == null || mParcent == null)
            return;

        if (ioo.gameMode.State == E_GameState.Summary)
        {
            if (mCpa != null)
            {
                mCpa.AnimationCustomEvent -= OnCustomEvent;
                mCpa = null;
            }
        }
    }

    public void SetMoveEndAction(Action action) { mMoveEndAction = action; }

    private bool[] leftList;
    private bool[] rightList;
    /// <summary>
    /// 每次重新启用左右微转时，调用
    /// </summary>
    public virtual void InitCameraRoll()
    {
        PauseRoll = true; // 暂时禁用

        mFixRotation = Mathf.RoundToInt(AngleCorrection(mParcent.localEulerAngles.y));

        mFixRotation %= 360;

        mLeftRotation = (int)(mFixRotation - mAngle);
        mRightRotation = (int)(mFixRotation + mAngle);

        leftList = new bool[Define.MAX_PLAYER_NUMBER];
        rightList = new bool[Define.MAX_PLAYER_NUMBER];
    }

    public void CPASpeed(float value)
    {
        mSpeed = mCpa.pathSpeed;
        mCpa.pathSpeed = value;
    }

    public void NormalSpeed()
    {
        mCpa.pathSpeed = mSpeed;
    }

    #region 相机震动
    /// <summary>
    /// 普通震屏
    /// </summary>
    public void NormalShake()
    {
        mShakeController.OnEventPlay(UnityEngine.Random.Range(1, 4));
    }

    /// <summary>
    /// Boss普通震屏
    /// </summary>
    public void BossShortShake()
    {
        mShakeController.OnEventPlay(4);
    }

    /// <summary>
    /// Boss长震屏
    /// </summary>
    public void BossLongShake()
    {
        mShakeController.OnEventPlay(5);
    }

    public void LateUpdate()
    {
        if (mShakeController == null) return;
        mShakeController.LateUpdate();
    }
    #endregion

    #region 动画播放那
    public bool IsPlaying() { return mCpa.isPlaying; }
    
    public bool PlayCPA()
    {
        if (mCpa == null || mCpa.isPlaying) return false;

        mCpa.gameObject.SetActive(true);

        mCpa.Seek(mPercent);
        mCpa.Play();
        PauseMove = true;
        PauseRoll = true;
        return true;
    }

    public bool PauseCPA()
    {
        if (mCpa == null || !mCpa.isPlaying) return false;

        mPercent = mCpa.percentage;
        mSpeed = mCpa.pathSpeed;
        mCpa.Pause();
        mCpa.gameObject.SetActive(false);
        PauseMove = false;
        PauseRoll = true; // 暂时禁用
        return true;
    }
    #endregion

    #region 左右微转    
    //欧拉角转换
    private float AngleCorrection(float angle)
    {
        if (angle < 0) return 360 + angle;
        if (angle > 360) return angle - 360;
        return angle;
    }

    /// <summary>
    /// 检测相机选择方向
    /// </summary>
    private void CheckeRotationDir()
    {
        ResetFlag();
        for (int i = 0; i < Define.MAX_PLAYER_NUMBER; ++i)
        {
            Vector3 pos = ioo.playerManager.Position(i);
            if (pos.x - mCheckRadius <= 0)
            {
                Left = true;
                leftList[i] = true;
            }
            else
            {
                leftList[i] = false;
            }

            if (pos.x + mCheckRadius >= Screen.width)
            {
                Right = true;
                rightList[i] = true;
            }
            else
            {
                rightList[i] = false;
            }
        }

        mLeft = false;
        for (int i = 0; i < leftList.Length; ++i)
        {
            if (leftList[i])
            {
                mLeft = true;
                break;
            }
        }

        mRight = false;
        for (int i = 0; i < rightList.Length; ++i)
        {
            if (rightList[i])
            {
                mRight = true;
                break;
            }
        }
    }

    /// <summary>
    /// 每帧被调用
    /// </summary>
    private void ResetFlag()
    {
        for (int i = 0; i < leftList.Length; ++i)
        {
            leftList[i] = false;
        }

        for (int i = 0; i < rightList.Length; ++i)
        {
            rightList[i] = false;
        }
    }

    /// <summary>
    /// 更新相机左右微转
    /// </summary>
    private void UpdateCameraRoll()
    {
        CheckeRotationDir();

        mCurRotation = AngleCorrection(mParcent.localEulerAngles.y);

        // 相机左转
        if (mLeft && !mRight)
        {
            if (mLeftRotation >= 0)
            {
                if (mCurRotation > mLeftRotation)
                {
                    mCurRotation -= ioo.nonStopTime.deltaTime * mRotationSpeed0;
                    if (mCurRotation < mLeftRotation)
                        mCurRotation = mLeftRotation;
                    mParcent.localEulerAngles = new Vector3(mParcent.localEulerAngles.x, mCurRotation, mParcent.localEulerAngles.z);
                }
            }
            else
            {
                if (Mathf.Abs(mCurRotation - mFixRotation) > (mAngle + 1))
                {
                    mCurRotation -= 360;
                }

                mCurRotation -= ioo.nonStopTime.deltaTime * mRotationSpeed0;

                if (mCurRotation < mLeftRotation)
                    mCurRotation = mLeftRotation;

                mParcent.localEulerAngles = new Vector3(mParcent.localEulerAngles.x, AngleCorrection(mCurRotation), mParcent.localEulerAngles.z);
            }
        }

        // 相机右转
        if (!mLeft && mRight)
        {
            //// mRightRotation > 360的情况，且mCurRotation达到360时会发生
            //if (Mathf.Abs(mRightRotation - mCurRotation) > mAngle)
            //{
            //    float temp = mRightRotation - 360;
            //    if (mCurRotation < temp)
            //    {
            //        mCurRotation += ioo.nonStopTime.deltaTime * mRotationSpeed0;
            //        if (mCurRotation > temp)
            //            mCurRotation = temp;
            //        mParcent.localEulerAngles = new Vector3(mParcent.localEulerAngles.x, mCurRotation, mParcent.localEulerAngles.z);
            //    }
            //}else
            if (mCurRotation < mRightRotation)
            {
                mCurRotation += ioo.nonStopTime.deltaTime * mRotationSpeed0;
                if (mCurRotation > mRightRotation)
                    mCurRotation = mRightRotation;
                mParcent.localEulerAngles = new Vector3(mParcent.localEulerAngles.x, mCurRotation, mParcent.localEulerAngles.z);
            }
        }

        // 相机复位
        if (!mLeft && !mRight)
        {
            if (Mathf.Abs(mCurRotation - mFixRotation) > (mAngle + 1))
            {
                mCurRotation -= 360;
            }

            if (mCurRotation > mFixRotation)
            {
                mCurRotation -= ioo.nonStopTime.deltaTime * mRotationSpeed0;
                if (mCurRotation < mFixRotation)
                    mCurRotation = mFixRotation;
            }

            if (mCurRotation < mFixRotation)
            {
                mCurRotation += ioo.nonStopTime.deltaTime * mRotationSpeed0;
                if (mCurRotation > mFixRotation)
                    mCurRotation = mFixRotation;
            }

            mParcent.localEulerAngles = new Vector3(mParcent.localEulerAngles.x, AngleCorrection(mCurRotation), mParcent.localEulerAngles.z);
        }
    }
    #endregion
}