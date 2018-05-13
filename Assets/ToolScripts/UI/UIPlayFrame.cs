using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
public class UIPlayFrame : MonoBehaviour {
    public float DelayTime = 0.5f;
    public List<Sprite> Sprits;
    public bool Loop = true;
    public bool IsAutoPlay = false;

    private Image _image = null;
    public  bool _isFinish = true;
    private float _time = 0;
    private int _curIndex = 0;

    void Start()
    {
        Init();
        if (IsAutoPlay)
        {
            Play();
        }
    }
    public void Play()
    {
        _isFinish = false;
    }
	// Use this for initialization
	void Init () {
	    _image = GetComponent<Image>();
        if (_image == null) { _isFinish = true; return; }
        _time = DelayTime;
        _curIndex = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (_isFinish) return;
        _time -= Time.deltaTime;
        if(_time <= 0){
            ChangeSprite();
            _curIndex++;
            if (_curIndex >= Sprits.Count)
            {
                if (!Loop)
                {
                    _isFinish = true;
                }
                else 
                {
                    _curIndex = 0;
                    _time = DelayTime;
                }
                return;
            }
            
        }
	}
    void ChangeSprite()
    {
        _image.sprite = Sprits[_curIndex];
        _time = DelayTime;
    }
}
