/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 2015,擎天柱网络科技有限公司
 * All rights reserved.
 * 
 * 文件名称：UIProgressAnimation.cs
 * 简    述：Unity 动态进度条
 * 创建标识：Fanki  2015/11/02
 * 
 * 修改标识：
*/

using UnityEngine;
using UnityEngine.UI;

public class UIProgressAnimation : MonoBehaviour
{
	#region 私有成员
	protected bool _isRunning = false;
	protected int _times = 0;
	protected float _start = 0;
	protected float _end = 0;

	protected float _speed = 0.01f;
	protected const float MINSPEED = 0.001f;

	protected Image _image = null;
	protected bool _isDirect = false;
	#endregion //私有成员

	/// <summary>
	/// 初始化
	/// </summary>
	void Awake ()
	{
		_image = transform.GetComponent<Image>();
	}

	/// <summary>
	/// 刷新
	/// </summary>
	void Update ()
	{
		if (!_isRunning) return;

		_start += _speed;

		if (_times > 0)
		{
			if (_start >= 1)
			{
				_start = 0;
				_times -= 1;
			}
			_image.fillAmount = _start;
			return;
		}

		if ((_start >= _end && !_isDirect) || (_start <= _end) && _isDirect)
			OnFinish();
		else
			_image.fillAmount = _start;
	}

	/// <summary>
	/// 完成
	/// </summary>
	virtual protected void OnFinish()
	{
		_start = _end;
		_image.fillAmount = _start;
		_isRunning = false;
	}

	/// <summary>
	/// 设置基本值
	/// </summary>
	/// <param name="start"></param>
	virtual public void SetBaseNumber(float start, bool isUpdateUi = false)
	{
		_start = start;
		if (isUpdateUi)
			if (_image != null) _image.fillAmount = _start;
	}

	/// <summary>
	/// 设置参数 运行
	/// </summary>
	/// <param name="end"></param>
	/// <param name="times"></param>
	public void SetProgressBar (float end, int times = 0, bool isDir = false)
	{
		if (!isDir && (_times < 1) && ((_start > end) || (_isRunning && (_end > end)))) _times += 1;

		_end = end;

		_isDirect = false;
		if (isDir)
		{
			if (_start > _end)
			{
				_speed = -Mathf.Abs(_speed);
				_isDirect = isDir;
			}
			else
				_speed = Mathf.Abs(_speed);
		}
		else{
			_speed = Mathf.Abs(_speed);
		}

		if (times > 0) SetProgressBarTimes(times);
		_isRunning = true;
	}

	/// <summary>
	/// 设置次数
	/// </summary>
	/// <param name="times"></param>
	public void SetProgressBarTimes (int times)
	{
		_times = times;
		_isRunning = true;
	}

	/// <summary>
	/// 设置动画速度
	/// </summary>
	/// <param name="speed"></param>
	public void SetSpeed (float speed)
	{
		_speed = speed;
		if (_speed <= 0) _speed = MINSPEED;
	}
}
