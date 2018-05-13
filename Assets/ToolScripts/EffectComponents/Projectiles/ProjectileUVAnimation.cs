using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProjectileUVAnimation : ProjectileBase {

	private const float kMaxFloat = 10000.0f;
	// Use this for initialization
	void Start () 
	{
		if(this.transform.childCount > 0)
			_cloneSource = this.transform.GetChild(0).gameObject;

		for (int i = 0; i < _transitObj.Count; ++i)
			AddTransitPos(_transitObj[i].transform.position, -1, false);

		if (_startObj != null)
			SetStartPosition(_startObj.transform.position);

		if (_endObj != null)
			SetEndPosition(_endObj.transform.position);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (_cloneSource == null) return;

		if (_startObj != null)
			SetStartPosition(_startObj.transform.position);

		if (_endObj != null)
			SetEndPosition(_endObj.transform.position);

		for (int i = 0; i < _transitObj.Count; ++i)
			UpdateTransitPos(_transitObj[i].transform.position, i);

		Renderer mainTexRenderer = _cloneSource.GetComponentInChildren<Renderer>();
		
		// 有限持续时计算显示时间
		if(mHoldTime > 0)
		{
			_totalShowTime += Time.deltaTime;
			if (_totalShowTime > mHoldTime)
				Destroy();
		}

		// 间歇持续时计算打断
		if (_intermittent && (_breakTime + _breakIntevalTime) > 0)
		{
			float timeCyclePoint = Time.time % (_breakTime + _breakIntevalTime);
			if (timeCyclePoint > _breakTime)
			{
				// 坐标设为不可见
				_cloneSource.transform.position = new Vector3(kMaxFloat, kMaxFloat, kMaxFloat);
				for (int i = 0; i < _cloneSections.Count; ++i)
					_cloneSections[i].transform.position = _cloneSource.transform.position;
			}
			else
			{
				// 恢复坐标
				if (_transitPos.Count == 0)
				{
					_cloneSource.transform.position = (_startPos + _endPos) * 0.5f;
				}
				else
				{
					_cloneSource.transform.position = (_startPos + _transitPos[0]) * 0.5f;

					for (int i = 1; i < _transitPos.Count; ++i)
						_cloneSections[i - 1].transform.position = (_transitPos[i] + _transitPos[i - 1]) * 0.5f;

					_cloneSections[_cloneSections.Count - 1].transform.position = (_endPos + _transitPos[_transitPos.Count - 1]) * 0.5f;
				}
			}
		}

		// 计算贴图序号
		if (_textureList.Count > 0)
		{
			_frameSwitchTime += Time.deltaTime;
			if(_frameSwitchTime > 1.0f / (float)_FPS)
			{
				_currentTexIndex = (_currentTexIndex + 1) % _textureList.Count;
				_frameSwitchTime = 0.0f;
			}
			mainTexRenderer.material.mainTexture = _textureList[_currentTexIndex];
			for(int i = 0; i < _cloneSections.Count; ++i)
			{
				Renderer cloneRenderer = _cloneSections[i].GetComponentInChildren<Renderer>();
				cloneRenderer.material.mainTexture = _textureList[_currentTexIndex];
			}
		}

		// 计算贴图位移
		if (_sliceLength > 0)
		{
			float sliceTime = _sliceLength / _sliceSpeed;
			Vector2 texOffset = new Vector2((Time.time % sliceTime) / sliceTime, 0.0f);
			mainTexRenderer.material.SetTextureOffset("_MainTex", texOffset);
			for (int i = 0; i < _cloneSections.Count; ++i)
			{
				Renderer cloneRenderer = _cloneSections[i].GetComponentInChildren<Renderer>();
				cloneRenderer.material.SetTextureOffset("_MainTex", texOffset);
			}
		}
	}

	// 设置起始位置
	public override void SetStartPosition(Vector3 start_pos)
	{
		_startPos = start_pos;
		_transitValueDirty = true;
		RecalcStrip();
	}

	// 设置结束位置
	public override void SetEndPosition(Vector3 end_pos)
	{
		_endPos = end_pos;
		_transitValueDirty = true;
		RecalcStrip();
	}
	
	// 重新计算各条带位置
	void RecalcStrip()
	{
		if (_cloneSource == null) return;

		if (_transitPos.Count == 0)
		{
			Vector3 vecDir = _endPos - _startPos;
			_cloneSource.transform.localScale = new Vector3(Vector3.Magnitude(vecDir), 1.0f, _sliceWidth);
			_cloneSource.transform.position = (_startPos + _endPos) * 0.5f;
			//_cloneSource.transform.right = vecDir;
			Vector3 camDir = Camera.main.transform.position - _cloneSource.transform.position;
			Vector3 vecForward = Vector3.Cross(camDir, vecDir);
			_cloneSource.transform.LookAt(_cloneSource.transform.position + vecForward, Vector3.Cross(vecDir, vecForward));

			if (_sliceLength > 0)
			{
				// 计算贴图平铺
				Renderer texRenderer = _cloneSource.GetComponentInChildren<Renderer>();
				float stripLength = Vector3.Magnitude(vecDir);
				texRenderer.material.SetTextureScale("_MainTex", new Vector2(stripLength / _sliceLength, 1.0f));
			}

			if (_transitCountDirty)
			{
				for (int i = 0; i < _cloneSections.Count; ++i)
					GameObject.Destroy(_cloneSections[i]);
				_cloneSections.Clear();
			}
			_transitCountDirty = false;
			_transitValueDirty = false;
		}
		else
		{
			if (_transitCountDirty || _transitValueDirty)
			{
				// 设置第一条的坐标
				Vector3 vecDir = _transitPos[0] - _startPos;
				_cloneSource.transform.localScale = new Vector3(Vector3.Magnitude(vecDir), 1.0f, _sliceWidth);
				_cloneSource.transform.position = (_startPos + _transitPos[0]) * 0.5f;
				//_cloneSource.transform.right = vecDir;
				Vector3 camDir = Camera.main.transform.position - _cloneSource.transform.position;
				Vector3 vecForward = Vector3.Cross(camDir, vecDir);
				_cloneSource.transform.LookAt(_cloneSource.transform.position + vecForward, Vector3.Cross(vecDir, vecForward));

				if (_sliceLength > 0)
				{
					// 计算贴图平铺
					Renderer texRenderer = _cloneSource.GetComponentInChildren<Renderer>();
					float stripLength = Vector3.Magnitude(vecDir);
					texRenderer.material.SetTextureScale("_MainTex", new Vector2(stripLength / _sliceLength, 1.0f));
				}

				if (_transitCountDirty)
				{
					for (int i = 0; i < _cloneSections.Count; ++i)
						GameObject.Destroy(_cloneSections[i]);
					_cloneSections.Clear();

					// 设置第2 -> N-1条坐标
					for (int i = 1; i < _transitPos.Count; ++i)
					{
						GameObject newStripObj = GameObject.Instantiate(_cloneSource);
						newStripObj.transform.SetParent(this.transform);
						Vector3 stripDir = _transitPos[i] - _transitPos[i - 1];
						newStripObj.transform.localScale = new Vector3(Vector3.Magnitude(stripDir), 1.0f, _sliceWidth);
						newStripObj.transform.position = (_transitPos[i] + _transitPos[i - 1]) * 0.5f;
						//newStripObj.transform.right = stripDir;
						Vector3 stripCamDir = Camera.main.transform.position - newStripObj.transform.position;
						Vector3 stripVecForward = Vector3.Cross(stripCamDir, stripDir);
						newStripObj.transform.LookAt(newStripObj.transform.position + stripVecForward, Vector3.Cross(stripDir, stripVecForward));

						if (_sliceLength > 0)
						{
							// 计算贴图平铺
							Renderer texRenderer = newStripObj.GetComponentInChildren<Renderer>();
							float stripLength = Vector3.Magnitude(stripDir);
							texRenderer.material.SetTextureScale("_MainTex", new Vector2(stripLength / _sliceLength, 1.0f));
						}
						_cloneSections.Add(newStripObj);
					} // for i

					// 设置第N条坐标
					GameObject newTailStripObj = GameObject.Instantiate(_cloneSource);
					newTailStripObj.transform.SetParent(this.transform);
					Vector3 tailDir = _endPos - _transitPos[_transitPos.Count - 1];
					newTailStripObj.transform.localScale = new Vector3(Vector3.Magnitude(tailDir), 1.0f, _sliceWidth);
					newTailStripObj.transform.position = (_endPos + _transitPos[_transitPos.Count - 1]) * 0.5f;
					//newTailStripObj.transform.right = tailDir;
					Vector3 tailCamDir = Camera.main.transform.position - newTailStripObj.transform.position;
					Vector3 tailVecForward = Vector3.Cross(tailCamDir, tailDir);
					newTailStripObj.transform.LookAt(newTailStripObj.transform.position + tailVecForward, Vector3.Cross(tailDir, tailVecForward));

					if (_sliceLength > 0)
					{
						// 计算贴图平铺
						Renderer texRenderer = newTailStripObj.GetComponentInChildren<Renderer>();
						float stripLength = Vector3.Magnitude(tailDir);
						texRenderer.material.SetTextureScale("_MainTex", new Vector2(stripLength / _sliceLength, 1.0f));
					}
					_cloneSections.Add(newTailStripObj);
				}
				else
				{
					// 设置第2 -> N-1条坐标
					for (int i = 1; i < _transitPos.Count; ++i)
					{
						GameObject stripObj = _cloneSections[i - 1];
						Vector3 stripDir = _transitPos[i] - _transitPos[i - 1];
						stripObj.transform.localScale = new Vector3(Vector3.Magnitude(stripDir), 1.0f, _sliceWidth);
						stripObj.transform.position = (_transitPos[i] + _transitPos[i - 1]) * 0.5f;
						//stripObj.transform.right = stripDir;
						Vector3 stripCamDir = Camera.main.transform.position - stripObj.transform.position;
						Vector3 stripVecForward = Vector3.Cross(stripCamDir, stripDir);
						stripObj.transform.LookAt(stripObj.transform.position + stripVecForward, Vector3.Cross(stripDir, stripVecForward));

						if (_sliceLength > 0)
						{
							// 计算贴图平铺
							Renderer texRenderer = stripObj.GetComponentInChildren<Renderer>();
							float stripLength = Vector3.Magnitude(stripDir);
							texRenderer.material.SetTextureScale("_MainTex", new Vector2(stripLength / _sliceLength, 1.0f));
						}
					} // for i

					// 设置第N条坐标
					GameObject tailStripObj = _cloneSections[_cloneSections.Count - 1];
					Vector3 tailDir = _endPos - _transitPos[_transitPos.Count - 1];
					tailStripObj.transform.localScale = new Vector3(Vector3.Magnitude(tailDir), 1.0f, _sliceWidth);
					tailStripObj.transform.position = (_endPos + _transitPos[_transitPos.Count - 1]) * 0.5f;
					// tailStripObj.transform.right = tailDir;
					Vector3 tailCamDir = Camera.main.transform.position - tailStripObj.transform.position;
					Vector3 tailVecForward = Vector3.Cross(tailCamDir, tailDir);
					tailStripObj.transform.LookAt(tailStripObj.transform.position + tailVecForward, Vector3.Cross(tailDir, tailVecForward));

					if (_sliceLength > 0)
					{
						// 计算贴图平铺
						Renderer texRenderer = tailStripObj.GetComponentInChildren<Renderer>();
						float stripLength = Vector3.Magnitude(tailDir);
						texRenderer.material.SetTextureScale("_MainTex", new Vector2(stripLength / _sliceLength, 1.0f));
					}
				} // if (_transitCountDirty)
			} // if(_transitCountDirty || _transitValueDirty)
			_transitCountDirty = false;
			_transitValueDirty = false;
		} // if(_transitPos.Count == 0)
	}

	// 设置贴图速度
	public override void SetSpeed(float speed)
	{
		_sliceSpeed = speed;
	}

	// 增加传递坐标, -1代表加在末尾
	public void AddTransitPos(Vector3 transitPos, int order = -1, bool updateLink = true)
	{
		if ((order > -1) && (order < _transitPos.Count))
			_transitPos.Insert(order, transitPos);
		else
			_transitPos.Add(transitPos);

		_transitCountDirty = true;

		if (updateLink)
			RecalcStrip();
	}

	// 移除传递坐标
	public void RemoveTransitPos(int order, bool updateLink = true)
	{
		if ((order > -1) && (order < _transitPos.Count))
			_transitPos.RemoveAt(order);

		_transitCountDirty = true;

		if (updateLink)
			RecalcStrip();
	}

	// 修改传递坐标
	public void UpdateTransitPos(Vector3 transitPos, int order, bool updateLink = true)
	{
		if ((order > -1) && (order < _transitPos.Count))
			_transitPos[order] = transitPos;
		_transitValueDirty = true;

		if (updateLink)
			RecalcStrip();
	}

	// 移除所有传递坐标
	public void RemoveAllTransitPos(bool updateLink = true)
	{
		_transitPos.Clear();
		_transitCountDirty = true;

		if (updateLink)
			RecalcStrip();
	}

	// 全流程是否结束
	public override bool IsFinished()
	{
		return _isFinished;
	}

	public override void Destroy()
	{
		_isFinished = true;
		GameObject.Destroy(this.gameObject);
	}

	// 当前图片索引
	int _currentTexIndex = 0;

	// 累积帧时间
	float _frameSwitchTime = 0.0f;

	// 显示总时间
	float _totalShowTime = 0.0f;

	// 技能是否结束
	bool _isFinished = false;

	// 材质贴图列表
	public List<Texture2D> _textureList = new List<Texture2D>();

	// 一秒播放的贴图数
	public int _FPS = 2;

	// 每个分片长度
	public float _sliceLength = 4.0f;

	// 分片宽度
	public float _sliceWidth = 1.0f;

	// 分片移动速度
	public float _sliceSpeed = 1.0f;

	// 持续时间
	public float mHoldTime = -1.0f;

	// 是否可中断
	public bool _intermittent = false;

	// 打断时间点
	public float _breakTime = 0.0f;

	// 打断时间间隔
	public float _breakIntevalTime = 0.0f;

	// 起始结束对象
	public GameObject _startObj = null;
	public GameObject _endObj = null;

	// 中间传递对象
	public List<GameObject> _transitObj = new List<GameObject>();

	// 起始结束坐标
	Vector3 _startPos = new Vector3(0, 0, 0);
	Vector3 _endPos = new Vector3(0, 0, 1);

	// 中间传递坐标
	List<Vector3> _transitPos = new List<Vector3>();

	// 闪电源对象
	GameObject _cloneSource = null;

	// 每段闪电对象
	List<GameObject> _cloneSections = new List<GameObject>();

	// 传递坐标数目是否需要更新
	bool _transitCountDirty = true;

	// 传递坐标值是否需要更新
	bool _transitValueDirty = true;
}
