using UnityEngine;
using System.Collections;

public class ScaleByCamera : MonoBehaviour {
	void Start()
	{
		if (mGameCamera == null)
		{
			mGameCamera = Camera.main;
		}
	}

	void Update()
	{
		if (mGameCamera == null)
			return;
		float newFomat = Vector3.Distance (transform.position, mGameCamera.transform.position) / mDist;
		this.transform.localScale = Vector3.one * newFomat;
	}

	private Camera mGameCamera = null;
	public float mDist = 18;
}
