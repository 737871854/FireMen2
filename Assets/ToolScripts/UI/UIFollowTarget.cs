using UnityEngine;
using System.Collections;

public class UIFollowTarget : MonoBehaviour {
	// Update is called once per frame
	void Update () {
		if (_gameCamera == null || mUICamera == null || _targetTran == null)
		{
			return;
		}

		Vector3 pos = _gameCamera.WorldToScreenPoint (_targetTran.position);
		float half_w = Screen.width / 2;
		float half_h = Screen.height / 2;
		float offset_x = (pos.x - half_w) / half_w;
		float offset_y = (pos.y - half_h) / half_h;
		float newFomat = _dist / Vector3.Distance (_targetTran.position, _gameCamera.transform.position);
		pos.x += offset_x * 10 * newFomat;
		pos.y += offset_y * 10 * newFomat;
		pos.x += Screen.width * _w_percent * newFomat;
		pos.y += Screen.height * _h_percent * newFomat;
		Vector3 ui_pos = mUICamera.ScreenToWorldPoint (pos);
		this.transform.position = ui_pos;

		Vector3 temp = Vector3.one * newFomat;
		temp.Scale (_scaleMyself);
		this.transform.localScale = temp;
	}

	void OnEnable()
	{
		this.Update ();
	}

	public void SetChildsVisible(bool val)
	{
		_isVisible = val;
		this.gameObject.SetActive (val);
	}

	public Camera gameCamera
	{
		set
		{
			_gameCamera = value;
		}

		get
		{
			return _gameCamera;
		}
	}

	public Camera uiCamera
	{
		set
		{
			mUICamera = value;
		}

		get
		{
			return uiCamera;
		}
	}

	public float dist
	{
		set
		{
			_dist = value;
		}
		get
		{
			return _dist;
		}
	}

	public Transform targetTran
	{
		set
		{
			_targetTran = value;
            if (_targetTran != null)
            {
                Update();
            }
		}

		get
		{
			return _targetTran;
		}
	}

	public Vector3 scaleMyself
	{
		set
		{
			_scaleMyself = value;
		}

		get
		{
			return _scaleMyself;
		}

	}

	public float wPercent
	{
		set
		{
			_w_percent = value;
		}

		get
		{
			return _w_percent;
		}
	}

	public float hPercent
	{
		set
		{
			_h_percent = value;
		}

		get
		{
			return _h_percent;
		}
	}

	private Vector3 _scaleMyself = Vector3.one;
	private Camera _gameCamera = null;
	private Camera mUICamera = null;
	private Transform _targetTran = null;
	private float _dist = 18;
	private bool _isVisible = true;
	private float _w_percent = 0.0f;
	private float _h_percent = 0.0f;
}
