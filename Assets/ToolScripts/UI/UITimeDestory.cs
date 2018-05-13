using UnityEngine;
using System.Collections;

public class UITimeDestory : MonoBehaviour {
    public float Time;
    private float _totalTime = 0;
    public UtilCommon.VoidDelegate DestoryDelegate;
	
	// Update is called once per frame
	void Update () {
        _totalTime += UnityEngine.Time.deltaTime;
        if (_totalTime >= Time)
        {
            if (DestoryDelegate != null)
            {
                DestoryDelegate(gameObject);
            }
            GameObject.Destroy(gameObject);
        }
	}
}
