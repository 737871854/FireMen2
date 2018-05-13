using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

[Serializable]
public enum UIAnimType
{
    None,
    Scale,
    Position,
}

[Serializable]
public class AnimEntry
{
    public TweenMain tween;
    public UIAnimType Type = UIAnimType.None;
    public float Duration = 0;
    public Vector3 To;
    // 继续加不同类型的动画的参数数据
}

[Serializable]
public class UIAnimCtrl : MonoBehaviour {
    public bool IsAutoPlay = false;
    public int LayerContainerCount = 0;
    //public List<UILayerContainer<AnimEntry>> LayerContainerList = new List<UILayerContainer<AnimEntry>>();
    public List<UILayerContainerAnim> LayerContainerList = new List<UILayerContainerAnim>();
    public Dictionary<UILayerContainerType, UtilCommon.VoidDelegate> CallbackMap = new Dictionary<UILayerContainerType, UtilCommon.VoidDelegate>();
    void Awake()
    {
        InitTween();
        if (IsAutoPlay)
        {
            for (int i = 0; i < (int)UILayerContainerType.COUNT; i++)
            {
                Play((UILayerContainerType)i);
            }
        }
    }

    public void SetStateCallback(UILayerContainerType type, UtilCommon.VoidDelegate callback)
    {
        CallbackMap.Add(type, callback);
    }

    void InitTween()
    {
        for (int i = 0; i < LayerContainerList.Count; i++)
        {
            //UILayerContainer<AnimEntry> layerContainer = LayerContainerList[i];
            UILayerContainerAnim layerContainer = LayerContainerList[i];
            
            for (int j = 0; j < layerContainer.ChildList.Count; j++)
            {
                AnimEntry animEntry = layerContainer.ChildList[j];
                UIAnimType animType = animEntry.Type;
                if (animType == UIAnimType.Position)
                {
                    animEntry.tween = TweenPos.Tween(gameObject, animEntry.Duration, animEntry.To);

                }
                if (animType == UIAnimType.Scale)
                {
                    animEntry.tween = TweenScale.Tween(gameObject, animEntry.Duration, animEntry.To);
                }

                if (animType != UIAnimType.None)
                {
                    animEntry.tween.tweenFactor = 0;
                    animEntry.tween.enabled = false;
                }
                /*
                // 在这里继续添加其他类型的
                */

                // 这里就是让ChildList按顺序去播放
                if (j > 0)
                {
                    AnimEntry preAnimEntry = layerContainer.ChildList[j - 1];
                    AddTweenListener(preAnimEntry, animEntry);
                }
                // 添加回调，在最后一个动作回调
                if (j == layerContainer.ChildList.Count - 1)
                {
                    AnimEntry lastAnimEntry = layerContainer.ChildList[j];
                    AddLastTweenCallback(layerContainer.Type, lastAnimEntry);
                }

            }

        }
    }

    void AddLastTweenCallback(UILayerContainerType type , AnimEntry lastAnimEntry)
    {
        if (lastAnimEntry.tween == null) return;
        lastAnimEntry.tween.OnFinished.AddListener(() =>
        {
            UtilCommon.VoidDelegate callback;
            CallbackMap.TryGetValue(type, out callback);
            if (callback != null)
            {
                callback(gameObject);
            }
            
        });
    }

    void AddTweenListener(AnimEntry preAnimEntry, AnimEntry animEntry)
    {
        // 为了防止有空的Tween值
        if (preAnimEntry.tween == null || animEntry.tween == null) return;
        preAnimEntry.tween.OnFinished.AddListener(() => 
        {
            animEntry.tween.tweenFactor = 0;
            animEntry.tween.FromCurrentValue();
            animEntry.tween.enabled = true; 

        });
    }

    // 播放StateType类型的所有的UILayerContainer<AnimEntry>
    public void Play(UILayerContainerType stateType)
    {
        for (int i = 0; i < LayerContainerList.Count; i++)
        {
            //UILayerContainer<AnimEntry> layerContainer = LayerContainerList[i];
            UILayerContainerAnim layerContainer = LayerContainerList[i];
            if (layerContainer.Type == stateType && layerContainer.Type != UILayerContainerType.None)
            {
                PlayStateAnim(layerContainer);
            }
        }
    }

    // 播放某个UILayerContainer<AnimEntry>实例的ChildList
    //void PlayStateAnim(UILayerContainer<AnimEntry> layerContainer)
    void PlayStateAnim(UILayerContainerAnim layerContainer)
    {
        List<AnimEntry> animEntryList = layerContainer.ChildList;
        AnimEntry animEntry = animEntryList[0];
        if (animEntryList.Count <= 0 || animEntry.tween == null) return;
        animEntry.tween.tweenFactor = 0;
        animEntry.tween.FromCurrentValue();
        animEntry.tween.enabled = true;
        
        
    }

}
