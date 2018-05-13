using UnityEngine;
using System.Collections.Generic;
using System;

[Serializable]
public class GoEntry
{
    public GameObject go;
}


[Serializable]
public class UIActiveInLayer : MonoBehaviour {
    public bool IsAutoPlay = false;
    public UILayerContainerType AutoPlayType = UILayerContainerType.None;
    public int LayerCount = 0;
    //public List<UILayerContainer<GameObject>> LayerContainerList = new List<UILayerContainer<GameObject>>();
    public List<UILayerContainerGo> LayerContainerList = new List<UILayerContainerGo>();
    public Dictionary<UILayerContainerType, UtilCommon.VoidDelegate> CallbackMap = new Dictionary<UILayerContainerType, UtilCommon.VoidDelegate>();
    void Awake()
    {
        
        if (IsAutoPlay)
        {
            HideAll();
            Play(AutoPlayType);
        }
    }

    public void SetStateCallback(UILayerContainerType type, UtilCommon.VoidDelegate callback)
    {
        CallbackMap.Add(type, callback);
    }

    UILayerContainerGo FindGoEntry(UILayerContainerType type)
    {
        for (int index = 0; index < LayerContainerList.Count; index++)
        {
            UILayerContainerGo layerContainerGo = LayerContainerList[index];
            if (type == layerContainerGo.Type && type != UILayerContainerType.None)
            {
                return layerContainerGo;
            }
        }
        return null;
    }
    public void Hide(UILayerContainerType type)
    {
        UILayerContainerGo layerContainerGo = FindGoEntry(type);
        if (layerContainerGo != null)
        {
            for (int j = 0; j < layerContainerGo.ChildCount; j++)
            {
                GoEntry goEntry = layerContainerGo.ChildList[j];
                GameObject go = goEntry.go;
                if (go != null) go.SetActive(false);
            }
        }
        /*
        for (int index = 0; index < LayerContainerList.Count; index++)
        {
            UILayerContainerGo layerContainerGo = LayerContainerList[index];
            if (type == layerContainerGo.Type && type != UILayerContainerType.None)
            {
                for (int j = 0; j < layerContainerGo.ChildCount; j++)
                {
                    GoEntry goEntry = layerContainerGo.ChildList[j];
                    GameObject go = goEntry.go;
                    if (go != null) go.SetActive(false);
                }
            }
        }
         * */
    }
    // 互斥,
    public void Mutual(UILayerContainerType playType, UILayerContainerType hideType, bool isShowFirstType){
        if (isShowFirstType)
        {
            Play(playType);
            Hide(hideType);
        }
        else{
            Play(hideType);
            Hide(playType);
        }
    }
    public void Show(UILayerContainerType type, bool isShow)
    {
        if (isShow) Play(type);
        else Hide(type);
            
    }
    public void Play(UILayerContainerType type)
    {
        UILayerContainerGo layerContainerGo = FindGoEntry(type);
        if (layerContainerGo != null)
        {
            for (int j = 0; j < layerContainerGo.ChildCount; j++)
            {
                GoEntry goEntry = layerContainerGo.ChildList[j];
                GameObject go = goEntry.go;
                if (go != null) go.SetActive(true);
            }
            UtilCommon.VoidDelegate callback;
            CallbackMap.TryGetValue(type, out callback);
            if (callback != null)
            {
                callback(gameObject);
            }
        }
        /*
        for (int index = 0; index < LayerContainerList.Count; index++)
        {
            UILayerContainerGo layerContainerGo = LayerContainerList[index];
            if (type == layerContainerGo.Type && type != UILayerContainerType.None )
            {
                for (int j = 0; j < layerContainerGo.ChildCount; j++)
                {
                    GoEntry goEntry = layerContainerGo.ChildList[j];
                    GameObject go = goEntry.go;
                    if (go != null) go.SetActive(true);
                }
                UtilCommon.VoidDelegate callback;
                CallbackMap.TryGetValue(type, out callback);
                if (callback != null)
                {
                    callback(gameObject);
                }
            }
        }
         * */
    }

    public void HideAll()
    {
        for (int index = 0; index < LayerContainerList.Count; index++)
        {
            UILayerContainerGo layerContainerGo = LayerContainerList[index];
            for (int j = 0; j < layerContainerGo.ChildCount; j++)
            {
                GoEntry goEntry = layerContainerGo.ChildList[j];
                GameObject go = goEntry.go;
                if (go != null) go.SetActive(false);
            }
        }
    }
}
