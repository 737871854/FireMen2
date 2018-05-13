using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Text;

public class UIProgress:MonoBehaviour
{
    private float _totalTime = 0;
    private float _passTime = 0;
    private float _savedPercent = 0;
    private float _savedRemainTime = 0;
    private bool _isFinish = false;
    private UtilCommon.VoidDelegate _callBack = null;
    private Image _image = null;
    private Text _text = null;
    private StringBuilder _timeStr = new StringBuilder();
    void Awake()
    {
        //_image = GetComponentInChildren<Image>();
        _image = transform.Find("ProgBar").GetComponent<Image>();
        _text = GetComponentInChildren<Text>();
        if(_image == null)
        {
            _isFinish = true;
        }

        if(_image == null)
        {
            _isFinish = true;
        }
    }
    StringBuilder GetTimeString()
    {
        int hour = 0,minute = 0,second = 0;
        _timeStr.Remove(0,_timeStr.Length);
        float remainTime = _totalTime - _passTime;
        float tempHour = remainTime / 3600;
        if(tempHour >= 1.0f)
        {
            hour = (int)tempHour;
            remainTime = remainTime - hour * 3600;
#if DEVELOP
            _timeStr.Append(string.Format(Util.TEXT("{0}小时"),hour));
#else
			_timeStr.Append(string.Format("{0}小时", hour));
#endif
        }

        float tempMinute = remainTime / 60;
        if(tempMinute >= 1.0f)
        {
            minute = (int)tempMinute;
            remainTime = remainTime - minute * 60;
#if DEVELOP
            _timeStr.Append(string.Format(Util.TEXT("{0}分钟"),minute));
#else
			_timeStr.Append(string.Format("{0}分钟", minute));
#endif
        }
        second = (int)remainTime;
        if(remainTime > 0)
        {
            second += 1;
        }
#if DEVELOP
        _timeStr.Append(string.Format(Util.TEXT("{0}秒"),second));
#else
		_timeStr.Append(string.Format("{0}秒", second));
#endif
        return _timeStr;

    }
    void Update()
    {
        
        if(_isFinish || _totalTime <= 0 || _savedRemainTime <= 0) return;
        _passTime = _passTime + Time.deltaTime;
        //目前进度 + 剩余进度 * (curPassTime / remainTime)
        float resultPercent = _savedPercent + (1 - _savedPercent) * (Time.deltaTime / (_savedRemainTime));//修改：by ljj 用这种方式来计算进度条的目的是，当totalTime在进度条去到中间时变化，进度条也不会跟着变化，而只是剩余时间的进度速度加快和减慢,意思就是每一帧都把目前进度保存起来，变化的只能是剩余进度
        _savedPercent = resultPercent;
        _savedRemainTime = _totalTime - _passTime;
        //float _curPercent = _passTime / _totalTime;
        _image.fillAmount = _savedPercent;
        GetTimeString();
        _text.text = _timeStr.ToString();
        if(resultPercent >= 1)
        {
            _isFinish = true;
            if(_callBack != null)
            {
                _callBack(gameObject);
            }
        }
    }

    public void InitUIProgress(float totalTime,float remainTime)
    {
        _totalTime = totalTime;
        _passTime = _totalTime - remainTime;
        _savedRemainTime = _totalTime - _passTime;
        this.Update();
    }

    public float TotalTime
    {
        get { return _totalTime; }
        set
        {
            _totalTime = value;
            _savedRemainTime = _totalTime;
            _passTime = 0;
            this.Update();
        }
    }

    public bool IsFinish
    {
        get { return _isFinish; }
    }

    public UtilCommon.VoidDelegate CallBack
    {
        set { _callBack = value; }
    }


}
