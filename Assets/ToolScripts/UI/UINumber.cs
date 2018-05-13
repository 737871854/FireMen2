/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 2015,擎天柱网络科技有限公司
 * All rights reserved.
 * 
 * 文件名称：UINumber.cs
 * 简    述：Unity 动态数字更新 模板1
 * 创建标识：Fanki  2015/11/02
 * 
 * 修改标识：
*/

using UnityEngine;
using UnityEngine.UI;

public class UINumber : MonoBehaviour
{
	#region 私有成员
	protected bool _isRunning = false;
	protected int _start = 0;
	protected int _end = 0;
	protected int _increase = 1;
	protected string _headString = "";
	
	protected bool _isDirect = false;
	protected Text _text = null;
	#endregion //私有成员

	/// <summary>
	/// 初始化
	/// </summary>
	void Awake ()
	{
		_text = transform.GetComponent<Text>();
	}

	/// <summary>
	/// 刷新
	/// </summary>
	virtual protected void Update ()
	{
		if (!_isRunning) return;

		_start += _increase;

		if ((_start >= _end && !_isDirect) || ((_start <= _end) && _isDirect))
			OnFinish();
		else
			_text.text = _headString + _start.ToString();
	}

	/// <summary>
	/// 完成
	/// </summary>
	virtual protected void OnFinish()
	{
		_start = _end;
		_text.text = _headString + _start.ToString();
		_isRunning = false;
	}

	virtual public void SetHeadString(string headStr)
	{
		_headString = headStr;
	}

	/// <summary>
	/// 设置基本值
	/// </summary>
	/// <param name="start"></param>
	virtual public void SetBaseNumber(int start, bool isUpdateUi = false)
	{
		_start = start;
		if (isUpdateUi)
			if (_text != null) _text.text = _headString + _start.ToString();
	}

	/// <summary>
	/// 设置参数 运行
	/// </summary>
	/// <param name="end"></param>
	/// <param name="increase"></param>
	public void SetNumber (int end, int increase = 0, int percentage = 100)
	{
		_end = end;

		_isDirect = _start > _end;

		if (increase == 0)
		{
			_increase = (_start - end) / percentage;
		}
		else
		{
			if (_isDirect)
				_increase = -increase;
			else
				_increase = increase;
		}

		_isRunning = true;
	}
}
