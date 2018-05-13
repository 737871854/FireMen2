using UnityEngine;
using System.Collections.Generic;

public class UIDelayActive : MonoBehaviour
{
    public bool IsAutoPlay = false;
    public List<float> ListDelayTime = new List<float>();
    public List<GameObject> ListObj = new List<GameObject>();

    private bool _isFinish = true;
    private bool Loop = false;
    private int _curIndex;
    private int _maxIndex;
    private float _time;
    private UtilCommon.VoidDelegate _play = null;

    public void SetPlayMusicCallBack(UtilCommon.VoidDelegate play)
    {
        _play = play;
    }

    // Use this for initialization
    void Reset()
    {
        for (int i = 0; i < ListObj.Count; i++)
        {
            GameObject go = ListObj[i];
            go.SetActive(false);
        }
        float delayTime = ListDelayTime[0];
        _curIndex = 0;
        _time = delayTime;
        _isFinish = false;
    }

    void Init()
    {
        Reset();
    }

    void Start()
    {
        if (IsAutoPlay)
        {
            Play();
        }
    }

    public void Hide()
    {
        for (int i = 0; i < ListObj.Count; i++)
        {
            GameObject go = ListObj[i];
            go.SetActive(false);
        }
        _isFinish = true;
    }

    public void Play(int maxIndex = -1)
    {
        if (maxIndex < 0)
            _maxIndex = ListObj.Count;
        else
            _maxIndex = maxIndex;

        Init();
    }

    // Update is called once per frame
    void Update()
    {
        if (_isFinish) return;
        if (_curIndex == _maxIndex)
        {
            if (Loop)
            {
                Reset();
            }
            else _isFinish = true;
        }
        else
        {
            _time -= Time.deltaTime;
            if ((_time <= 0))
            {
                ListObj[_curIndex].SetActive(true);
                _curIndex++;
                if (_play != null) _play(null, null);
                if (_curIndex == ListObj.Count) return;
                _time = ListDelayTime[_curIndex];
            }
        }
    }
}
