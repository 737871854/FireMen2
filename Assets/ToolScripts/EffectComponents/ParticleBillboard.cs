using UnityEngine;
using System.Collections;

#if UNITY_EDITOR 
using UnityEditor;
#endif

[ExecuteInEditMode]
public class ParticleBillboard : MonoBehaviour {
	public enum RenderMode
	{
		Billboard,
		HorizontalBillboard,
		VerticalBillboard,
		LookAtCamera,
	}

	// Use this for initialization
	void Start () {

#if UNITY_EDITOR
		if (Application.isPlaying)
		{
			mCamera = Camera.main;
			EditorApplication.update -= Update;
		}
		else
		{
			mCamera = SceneView.lastActiveSceneView.camera;
			EditorApplication.update += Update;
		}
	
#else
		mCamera = Camera.main;
#endif

	}

	// Update is called once per frame
	void Update () {
		if (mCamera == null)
		{
#if UNITY_EDITOR
			mCamera = SceneView.lastActiveSceneView.camera;
#else
			return;
#endif
		}

		switch(mRenderMode)
		{
		case RenderMode.Billboard:
			OnBillboard();
			break;
		case RenderMode.HorizontalBillboard:
			OnHorizontalBillboard();
			break;
		case RenderMode.VerticalBillboard:
			OnVerticalBillboard();
			break;
		case RenderMode.LookAtCamera:
			OnLookAtCamera();
			break;
		default:
			OnBillboard();
			break;
		}
	}

#if UNITY_EDITOR
	void OnDestroy ()
	{
		EditorApplication.update -= Update;
	}
#endif

	void OnBillboard()
	{
		transform.eulerAngles = mCamera.transform.eulerAngles;
	}

	void OnHorizontalBillboard()
	{
		transform.eulerAngles = new Vector3 (90, 0, 0);
	}

	void OnVerticalBillboard()
	{
		//transform.eulerAngles = new Vector3 (0, mCamera.transform.eulerAngles.y, mCamera.transform.eulerAngles.z );
		transform.eulerAngles = new Vector3 (mOffset.x, mCamera.transform.eulerAngles.y + mOffset.y, mOffset.z);
	}

	void OnLookAtCamera()
	{
		transform.LookAt (mCamera.transform.position);
	}
	
	public Camera mCamera = null;
	public RenderMode mRenderMode = RenderMode.Billboard;
	public Vector3 mOffset = new Vector3 (0, 0, 0);
}
