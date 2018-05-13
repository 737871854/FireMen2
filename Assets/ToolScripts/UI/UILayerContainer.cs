using UnityEngine;
using System.Collections.Generic;
using System;

[Serializable]
public enum UILayerContainerType
{
    None,
    Normal,
    Active,
    InActive,
    Press,
    Disable,
    COUNT
}
/*
[Serializable]
public class UILayerContainer<T> : 
{
    public UILayerContainerType Type = UILayerContainerType.None;
    public int ChildCount = 0;
    public List<T> ChildList = new List<T>();
}
*/

[Serializable]
public class UILayerContainerAnim
{
    public UILayerContainerType Type = UILayerContainerType.None;
    public int ChildCount = 0;
    public List<AnimEntry> ChildList = new List<AnimEntry>();
}

[Serializable]
public class UILayerContainerGo
{
    public UILayerContainerType Type = UILayerContainerType.None;
    public int ChildCount = 0;
    public List<GoEntry> ChildList = new List<GoEntry>();
}