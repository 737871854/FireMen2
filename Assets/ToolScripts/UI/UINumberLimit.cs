/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 2015,擎天柱网络科技有限公司
 * All rights reserved.
 * 
 * 文件名称：UINumberLimit.cs
 * 简    述：Unity 动态数字更新 模板1
 * 创建标识：Fanki  2015/11/02
 * 
 * 修改标识：
*/

using UnityEngine;
using System.Collections;

public class UINumberLimit : UINumber
{
	#region 私有成员
	protected int _startLimit = 0;
	protected int _endLimit = 0;
	protected int _limitIncrease = 0;
	protected string _limitSeparator = "/";
	protected bool _isLimitDirect = false;
	#endregion //私有成员

	/// <summary>
	/// 刷新
	/// </summary>
	override protected void Update()
	{
		if (!_isRunning) return;

		bool res1 = (_start >= _end && !_isDirect) || ((_start <= _end) && _isDirect);
		bool res2 = (_startLimit >= _endLimit && !_isLimitDirect) || ((_startLimit <= _endLimit) && _isLimitDirect);

		if (!res1) _start += _increase;
		if (!res2) _startLimit += _limitIncrease;

		if (res1 && res2)
			OnFinish();
		else
			_text.text = string.Format("{0}{1}{2}", _start, _limitSeparator, _startLimit);
	}

	/// <summary>
	/// 完成
	/// </summary>
	virtual protected void OnFinish()
	{
		_start = _end;
		_startLimit = _endLimit;
		_text.text = string.Format("{0}{1}{2}", _start, _limitSeparator, _startLimit);
		_isRunning = false;
	}

	/// <summary>
	/// 设置分隔符
	/// </summary>
	/// <param name="separator"></param>
	public void SetSeparator(string separator)
	{
		_limitSeparator = separator;
	}

	/// <summary>
	/// 设置基础数值
	/// </summary>
	/// <param name="start"></param>
	/// <param name="startLimit"></param>
	public void SetBaseNumber(int start, int startLimit, bool isUpdateUi = false)
	{
		_start = start;
		_startLimit = startLimit;
		if (isUpdateUi)
			if (_text != null) _text.text = string.Format("{0}{1}{2}", _start, _limitSeparator, _startLimit);
	}

	/// <summary>
	/// 设置参数 运行
	/// </summary>
	/// <param name="end"></param>
	/// <param name="limit"></param>
	/// <param name="increase"></param>
	public void SetNumber(int end, int limit, int increase = 0, int percentage = 100)
	{
		base.SetNumber (end, increase, percentage);
		
		_endLimit = limit;
		_isDirect = _startLimit > _endLimit;

		if (increase == 0)
		{
			_limitIncrease = (_endLimit - _startLimit) / percentage;
		}
		else
		{
			if (_isDirect)
				_limitIncrease = -increase;
			else
				_limitIncrease = increase;
		}

		_isRunning = true;
	}
}
