/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 2015,擎天柱网络科技有限公司
 * All rights reserved.
 * 
 * 文件名称：HitEffectPlayer.cs
 * 简	述：c#控制被攻击的shader值处理。
 * 创建标识：Fanki  2015/07/06
 * 
 * 修改标识：
 * 修改描述：
*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HitEffectPlayer : MonoBehaviour
{
	#region 私有属性
	/// <summary>
	/// 记录材质
	/// </summary>
	private List<Material> _materialList = new List<Material>();

	/// <summary>
	/// 记录开始
	/// </summary>
	private bool _start = false;

	/// <summary>
	/// 延时值
	/// </summary>
	private float _delayCount = 0.0f;

	/// <summary>
	/// 增量
	/// </summary>
	private float _increase = 0.1f;

	/// <summary>
	/// 变更值
	/// </summary>
	private float _change = 1.0f;

	/// <summary>
	/// 运行时间
	/// </summary>
	private float _runTimeCount = 0;

	/// <summary>
	/// 使用变化的shader名字
	/// </summary>
	private const string _useShaderName = "Q5/Proj/HitEffect/LightShader";

	/// <summary>
	/// 暂时限制名称，迟些移除，先写死
	/// </summary>
	private string[] _omitNames = { "touying", "ring_02_al", "target", "lizi_01_ad", "b50003", "star_01_al", "smoke_", "qi_" };

	/// <summary>
	/// 有没有找到shader
	/// </summary>
	private bool _haveShader = true;
	#endregion

	#region 公共属性
	public bool mIsLoop = false;
	/// <summary>
	/// 次数
	/// </summary>
	public int _count = 1;
	/// <summary>
	/// 秒数
	/// </summary>
	public float _sec = 1;
	/// <summary>
	/// 延时
	/// </summary>
	public float _delay = 0f;
	/// <summary>
	/// 最大值
	/// </summary>
	public float _max = 0.8f;
	/// <summary>
	/// 最小值
	/// </summary>
	public float _min = 0;
	/// <summary>
	/// 外发光颜色
	/// </summary>
	public Color _color = new Color(1, 0, 0);
	/// <summary>
	/// 内发光颜色颜色
	/// </summary>
	public Color _innerColor = new Color(1, 0, 0);
	#endregion

	#region 公共方法
	/// <summary>
	/// 检测创建shader
	/// </summary>
	public void CreateMaterialShader()
	{
		Renderer[] renderers = gameObject.GetComponentsInChildren<Renderer>();
		int i_max = renderers.Length;
		for (int i = 0; i < i_max; i++)
		{
			Material[] materials = ((Renderer)renderers[i]).materials;
			int j_max = materials.Length;

			// 查找是否已经加上shader;
			bool is_add = false;
			for (int j = 0; j < j_max; j++)
			{
				for (int k = 0; k < _omitNames.Length; k++)
				{
					if (materials[j].name.Contains(_omitNames[k]))
					{
						goto end;
					}
				}

				int shaderType = CheckShaderName(materials[j].shader.name);
				if (shaderType != -1)
				{
					_materialList.Add(materials[j]);
					is_add = true;
					break;
				}
			}

			// 是否已经加上shader，如果没有加就加上;
			if (!is_add)
			{
				Shader s = Shader.Find(_useShaderName);
				if (s == null)
				{
					_haveShader = false;
					Debugger.Log("can't find shader " + _useShaderName);
					return;
				}

				Material[] newMaterial = new Material[j_max + 1];
				for (int c = 0; c != j_max; c++)
				{
					newMaterial[c] = materials[c];
				}

				Material lightMat = new Material(s);
				newMaterial[j_max] = lightMat;
				_materialList.Add(newMaterial[j_max]);

				((Renderer)renderers[i]).materials = newMaterial;
			}

		end: ;
		}
	}

	/// <summary>
	/// 通过配置设置值
	/// </summary>
	public void SetMaterialByConfig()
	{

	}

	/// <summary>
	/// 设置初始值
	/// </summary>
	public void InitMaterialData(Material mat)
	{
		mat.SetColor("_TintColor", _color);
		mat.SetColor("_InnerColor", _innerColor);
		mat.SetFloat("_InnerColorPower", 0);
		mat.SetFloat("_RimPower", 0.5f);
		mat.SetFloat("_AlphaPower", 8);
		mat.SetFloat("_Input", 0);
	}

	/// <summary>
	/// 设置值
	/// </summary>
	public void SetInitValue()
	{
		_start = true;
		SetInitValueBase();
	}

	private void SetInitValueBase()
	{
		_delayCount = 0.0f;
		_change = _min;
		_runTimeCount = 0;
		_increase = CalculateIncrease();
		// 设置初始参数;
		foreach (Material mat in _materialList)
		{
			InitMaterialData(mat);
		}
	}

	public float CalculateIncrease()
	{
		float len = Mathf.Abs(_max - _min) * 2;
		float increase = (len * FightTime.deltaTime) * (_count / _sec);
		return increase;
	}

	/// <summary>
	/// 攻击增加1
	/// </summary>
	public void AddAttack()
	{
		_start = true;
		_runTimeCount = 0;
	}

    /// <summary>
    /// run Effect
    /// </summary>
    public void RunEffect()
    {
        _start = true;
        //_runTimeCount = 0;
        if (_runTimeCount > 0)
        {
            _runTimeCount -= _sec;
        }
    }
	#endregion

	#region MonoBehaviour内部函数
	/// <summary>
	/// MonoBehaviour内部函数Start
	/// </summary>
	void Start()
	{
		CreateMaterialShader();
		SetInitValue();
	}

	/// <summary>
	/// MonoBehaviour内部函数Update
	/// </summary>
	void Update()
	{
		// 没有找到shader，直接返回
		if (!_haveShader)
			return;

		// 是否开始
		if (_start || mIsLoop)
		{
			// 是否有延时
			if (CheckDelayTime())
				return;

			OnInputChange();

			UpdateShader();

			CheckRunTime();
		}
	}
	#endregion

	#region 私有调用
	/// <summary>
	/// 检测是否有要操作的shader
	/// </summary>
	private int CheckShaderName(string shaderName)
	{
		if (_useShaderName.Equals(shaderName))
		{
			return 1;
		}
		else
		{
			return -1;
		}
	}

	/// <summary>
	/// 计算设置量
	/// </summary>
	private void OnInputChange()
	{
		_change += _increase;
		if (_change > _max)
		{
			_increase = -Mathf.Abs(_increase);
		}
		if (_change < _min)
		{
			_increase = Mathf.Abs(_increase);
		}
	}

	/// <summary>
	/// 更新Shader数值
	/// </summary>
	private void UpdateShader()
	{
		// 设置参数
		foreach (Material mat in _materialList)
		{
			mat.SetFloat("_Input", _change);
		}
	}

	/// <summary>
	/// 检查运行时间
	/// </summary>
	private void CheckRunTime()
	{
		_runTimeCount += FightTime.deltaTime;
		if (_runTimeCount > _sec)
		{
			OnFinish();
		}
	}

	/// <summary>
	/// 完成运动
	/// </summary>
	private void OnFinish()
	{
		_start = false;
		SetInitValueBase();
	}

	/// <summary>
	/// 检测是否有延时效果
	/// </summary>
	private bool CheckDelayTime()
	{
		if (_delayCount > 0)
		{
			_delayCount -= FightTime.deltaTime;
			return true;
		}
		else
		{
			return false;
		}
	}


	#endregion
}
