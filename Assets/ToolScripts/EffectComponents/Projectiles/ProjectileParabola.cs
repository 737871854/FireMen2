using UnityEngine;
using System.Collections;

public class ProjectileParabola : ProjectileBase
{

	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
		if (_parabolaControl != null)
		{
			_isTrajectoryFinished = !_parabolaControl.isRunning;
			// 轨道模拟完播放爆炸动画
			if (!_isTrajectoryFinishedLastFrame && _isTrajectoryFinished)
			{
				_hitStartTime = Time.time;

				if (_flyingObject != null)
					// 隐藏飞行对象
					_flyingObject.SetActive(false);

				if (_hitPrefab != null)
                {
                    _hitObject = GameObject.Instantiate(_hitPrefab, _endPos, Quaternion.identity) as GameObject;
                    excuteShader(_hitObject);
                }


                // 结束是飞行结束
			}
			_isTrajectoryFinishedLastFrame = _isTrajectoryFinished;

            // 结束计算伤害
			if (_isTrajectoryFinished && !_isFinished)// && (Time.fixedTime - _hitStartTime > kWaitForCalTime))
			{
                isHitPoint = true;
			}

			// 判定是否全程终止
			if (_isTrajectoryFinished && !_isFinished)// && (Time.fixedTime - _hitStartTime > kHitShowTime))
			{
				_isFinished = true;
				Destroy();
			}
		}
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
		if (!_isFinished)
			Destroy();

		if (_flyingPrefab != null)
		{
			_flyingObject = GameObject.Instantiate(_flyingPrefab, Vector3.zero, Quaternion.identity) as GameObject;
            excuteShader(_flyingObject);
			_parabolaControl = _flyingObject.GetComponent<ParabolaTrajectory>();
		}

		if (_parabolaControl != null)
		{
			_parabolaControl.startPosition = _startPos;
			_parabolaControl.targetPosition = _endPos;
			if (_parabolaControl.maxHeight > 0)
				_parabolaControl.maxHeight = Vector3.Distance(_startPos, _endPos) * 0.25f /*+ Mathf.Abs(_startPos.y - _endPos.y) * 0.5f*/;
			_parabolaControl.Start();
			_isFinished = false;
		}
	}

	// 设置速度
	public override void SetSpeed(float speed)
	{
		if (_parabolaControl != null)
			_parabolaControl.horizontalSpeed = speed;
	}

	// 销毁投射物
	public override void Destroy()
	{
		if(_flyingObject != null)
			GameObject.Destroy(_flyingObject);

		if(_hitObject != null)
			GameObject.Destroy(_hitObject);

		_isFinished = true;
	}

    public override GameObject GetMainObject() {
        return _flyingObject;
    }

	// 投射物飞行效果
	public GameObject _flyingPrefab = null;

	// 投射物击中效果
	public GameObject _hitPrefab = null;

	// 投射物飞行对象
	protected GameObject _flyingObject = null;

	// 投射物击中对象
	protected GameObject _hitObject = null;

	// 抛物线控制对象
	protected ParabolaTrajectory _parabolaControl = null;

	// 起始坐标
	protected Vector3 _startPos = new Vector3(0, 0, 0);
	
	// 终止坐标
	protected Vector3 _endPos = new Vector3(0, 0, 0);

	// 上帧轨道运行是否结束
	protected bool _isTrajectoryFinishedLastFrame = true;

	// 本帧轨道运行是否结束
	protected bool _isTrajectoryFinished = true;

	// 显示是否结束
	protected bool _isFinished = true;

	// 撞击开始时间
	protected float _hitStartTime = 0.0f;

	// 撞击效果持续时间
	protected const float kHitShowTime = 1.0f;

    // 飞行结束到hit点
    protected const float kWaitForCalTime = 0.07f;
}
