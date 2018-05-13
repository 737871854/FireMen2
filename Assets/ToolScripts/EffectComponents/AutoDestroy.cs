using UnityEngine;
using System.Collections;

public class AutoDestroy : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		_startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Time.time - _startTime > _destroyTime)
			GameObject.Destroy(this.gameObject);
	}
	private float _startTime = 0.0f;
	public float _destroyTime = 1.0f;
}
