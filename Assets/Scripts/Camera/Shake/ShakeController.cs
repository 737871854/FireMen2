using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class ShakeController
{
    private bool  startPlay;
    private float speedMount;
    private float waveMount;
    private float shakeStartTime;
    private ShakeAction currentShake;
    private Dictionary<int, ShakeAction> shakeDic;
    private Transform camera;
    private Vector3 original0;
    private Vector3 original1; 

	// Use this for initialization
	public void Init (Transform camera) 
    {
       this.camera      = camera;
       //int actionCount  = camera.childCount;
       int actionCount  = 5;
        shakeDic        = new Dictionary<int, ShakeAction>();
        for (int index = 1; index <= actionCount; ++index)
        {
            string goName   = "Action" + index.ToString();
            ShakeAction go  = this.camera.Find(goName).GetComponent<ShakeAction>();
            shakeDic.Add(go.shakeTypeName, go);
        }
        startPlay   = false;
	}

    public void LateUpdate()
    {
        if (camera != null && startPlay)
        {
            float passTime          = (Time.realtimeSinceStartup - shakeStartTime) * (speedMount / 10);
            camera.localPosition    = camera.localPosition + new Vector3(currentShake.xPosCurve.Evaluate(passTime), currentShake.yPosCurve.Evaluate(passTime), currentShake.zPosCurve.Evaluate(passTime)) * (waveMount / 10);
            camera.localEulerAngles = camera.localEulerAngles + new Vector3(currentShake.xRotateCurve.Evaluate(passTime), currentShake.yRotateCurve.Evaluate(passTime), currentShake.zRotateCurve.Evaluate(passTime)) * 360 * (waveMount / 10);
            if (passTime > currentShake.length * (speedMount / 10))
            {
                startPlay           = false;
                Sequence sequence   = DOTween.Sequence();
                sequence.Append(camera.DOLocalMove(original0, 0.1f));
                sequence.Join(camera.DOLocalRotate(original1, 0.1f));
            }
        }
    }

    public void OnEventPlay(int type)
    {
        // 暂定处理 当前动作在进行中时则不接受其他的请求
        if (startPlay)
            return;

        original0 = camera.localPosition;
        original1 = camera.localEulerAngles;

        float speedPlus     = 0.0f;
        float waveMountPlus = 0.0f;
        if (type == 1)
        {
            speedPlus     = 14.0f;
            waveMountPlus = 1.0f;
        }
        else if (type == 2)
        {
            speedPlus     = 17.0f;
            waveMountPlus = 2.0f;
        }
        else if (type == 3)
        {
            speedPlus     = 30.0f;
            waveMountPlus = 1.0f;
        }else if (type == 4)
        {
            speedPlus = 20.0f;
            waveMountPlus = 1.0f;
        }
        else if (type == 5)
        {
            speedPlus = 20.0f;
            waveMountPlus = 1.0f;
        }

        if (shakeDic.ContainsKey (type))
        {
			speedMount      = speedPlus;
			waveMount       = waveMountPlus;
            currentShake    = shakeDic[type];
			shakeStartTime  = Time.realtimeSinceStartup;
			startPlay       = true;	
		}
    }

    void OnGUI()
    {
        //if (GUI.Button(new Rect(115, 130, 80, 50), "Load"))
        //{
        //    OnEventPlay(1, 5.0f, 2.0f);
        //}
    }
}
