using UnityEngine;
using System.Collections;
using UnityEngine.UI;
// 暂时固定死四个Sprite，以后如果要加的话，下面的Switch就要改了

public enum UISpriteState
{
    Normal,
    HighLighted,
    Press,
    Disable,
}

public class UISpriteChange : MonoBehaviour {
    public Sprite NormalSprite;
    public Sprite HighLightedSprite;
    public Sprite PressSprite;
    public Sprite DisableSprite;
    private Image _image;
    //void Start () {
        

    //}
    public void SetSprite(UISpriteState state)
    {
        if (_image == null)
        {
            _image = gameObject.GetComponent<Image>();
            if (_image == null) { Debugger.LogError("UISpriteSwap need Image Component"); }
        }
        switch(state){
            case UISpriteState.Normal:
                if (NormalSprite != null) _image.sprite = NormalSprite;
                break;
            case UISpriteState.HighLighted:
                if (HighLightedSprite != null)
                {
                    _image.sprite = HighLightedSprite;
                }
                break;
            case UISpriteState.Press:
                if (PressSprite != null) _image.sprite = PressSprite;
                break;
            case UISpriteState.Disable:
                if (DisableSprite != null) _image.sprite = DisableSprite;
                break;
        }
        
    }
	
	
}
