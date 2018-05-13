using UnityEngine;
using System.Collections;

public class ProjectileFan : ProjectileBase
{

	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{

	}

	// 全流程是否结束
	public override bool IsFinished()
	{
		return _isFinished;
	}

	// 设置开始位置
	public override void SetStartPosition(Vector3 start_pos)
	{
		_startPos = start_pos;
	}

	// 设置结束位置并启动流程
	public override void SetEndPosition(Vector3 end_pos)
	{
		_endPos = end_pos;
		_endPos.y = _startPos.y;

		if (!_isFinished)
			Destroy();

		if (_fanPrefab != null)
		{
			_fanObject = GameObject.Instantiate(_fanPrefab, _startPos, Quaternion.LookRotation(_endPos - _startPos)) as GameObject;
            _fanObject.transform.SetParent(transform);
            _fanObject.transform.localPosition = Vector3.zero;
            excuteShader(_fanObject);
			_isFinished = false;
		}
	}

	// 设置速度
	public override void SetSpeed(float speed)
	{
	}

	// 销毁投射物
	public override void Destroy()
	{
		if (_fanObject != null)
			GameObject.Destroy(_fanObject);

		_isFinished = true;
        isHitPoint = true;
	}

    public override GameObject GetMainObject() {
        return _fanObject;
    }

	// 起始坐标
	protected Vector3 _startPos = new Vector3(0, 0, 0);
	
	// 终止坐标
	protected Vector3 _endPos = new Vector3(0, 0, 0);

	// 扇面攻击效果
	public GameObject _fanPrefab = null;

	// 扇面显示对象
	protected GameObject _fanObject = null;

	// 显示是否结束
	protected bool _isFinished = true;

}
