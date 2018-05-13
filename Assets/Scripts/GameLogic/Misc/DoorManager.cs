/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   DoorManager.cs
 * 
 * 简    介:    借鉴消防员1代功能
 * 
 * 创建标识：   Pancake 2017/11/6 11:44:12
 * 
 * 修改描述：   
 * 
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;

public class DoorManager : SingletonBehaviour<DoorManager>
{

    public enum StateType
    {
        Unknown = 0,
        Opened = 1,
        Cloesd = 2,
        Opening = 3,
        Closing = 4
    }

    public enum MoveType
    {
        Unknown = 0,
        TowSide = 1,
        LeftRight = 2,
        HalfLet = 3,
        HalfRight = 4,
        Half160Left = 5,
    }

    [System.Serializable]
    public class Door
    {
        public GameObject parent;
        public GameObject left;
        public GameObject right;
        public StateType state;
        public MoveType type;
        public Vector3 toLeft;
        public Vector3 toRight;
        public Vector3 toLeftEnd;
        public Vector3 toRightEnd;
        public float existTime;
        [System.NonSerialized]
        public float time;
    }

    protected Dictionary<string, Door> prefabsDict;
    protected List<string> openedList;
    protected List<string> openingList;
    protected List<string> closingList;

    public List<Door> towSideList;
    public List<Door> leftRightInsideList;
    public List<Door> leftRightOutsideList;
    public List<Door> halfLeftList;
    public List<Door> halfRightList;
    public List<Door> half160LeftList;

    // Use this for initialization
    void Start()
    {
        prefabsDict = new Dictionary<string, Door>();
        openingList = new List<string>();
        closingList = new List<string>();
        openedList = new List<string>();

        for (int i = 0; i < towSideList.Count; ++i )
        {
            Door door = towSideList[i];
            prefabsDict.Add(door.parent.name, door);
        }

        for (int i = 0; i < leftRightInsideList.Count; ++i)
        {
            Door door = leftRightInsideList[i];
            prefabsDict.Add(door.parent.name, door);
        }

        for (int i = 0; i < leftRightOutsideList.Count; ++i)
        {
            Door door = leftRightOutsideList[i];
            prefabsDict.Add(door.parent.name, door);
        }

        for (int i = 0; i < halfLeftList.Count; ++i)
        {
            Door door = halfLeftList[i];
            prefabsDict.Add(door.parent.name, door);
        }

        for (int i = 0; i < halfRightList.Count; ++i)
        {
            Door door = halfRightList[i];
            prefabsDict.Add(door.parent.name, door);
        }

        for (int i = 0; i < half160LeftList.Count; ++i )
        {
            Door door = half160LeftList[i];
            prefabsDict.Add(door.parent.name, door);
        }
    }

    void Update()
    { 
        for (int index = 0; index < openedList.Count; ++index)
        {
            string name = openedList[index];
            Door go = prefabsDict[name];
            if (go.state != StateType.Opened)
            {
                continue;
            }
            go.time -= Time.deltaTime;
            if (go.time <= 0.0f)
            {
                CloseDoor(name);
                RemoveOpenedList(name);
                return;
            }
        }

        //foreach (KeyValuePair<string, Door> element in prefabsDict)
        //{
        //    if (element.Value.state != StateType.Opened)
        //    {
        //        continue;
        //    }

        //    element.Value.time -= Time.deltaTime;
        //    if (element.Value.time <= 0.0f)
        //    {
        //        CloseDoor(element.Key);
        //    }
        //}
    }

    public bool GetDoorPositionByName(string name, ref Vector3 pos)
    {
        if (prefabsDict.ContainsKey(name) == false)
            return false;

        GameObject go = prefabsDict[name].parent;
        if (go == null)
        {
            return false;
        }

        pos = go.transform.position;
        return true;
    }

    public bool OpenDoor(string name)
    {
        if (prefabsDict.ContainsKey(name) == false)
        {
            return false;
        }

        Door go = prefabsDict[name];
        if (go == null)
        {
            return false;
        }

        if (go.state == StateType.Closing)
        {
            return false;
        }

        if (go.state == StateType.Opening)
        {
            go.time = go.existTime;
            return true;
        }

        if (go.state == StateType.Opened)
        {
            go.time = go.existTime;
        }

        if (go.type == MoveType.LeftRight)
        {
            Tweener twLeft = go.left.transform.DOLocalRotate(go.toLeft, 1.0f);
            Tweener twRight = go.right.transform.DOLocalRotate(go.toRight, 1.0f);
            twLeft.OnComplete(OnOpened);
            AddOpeningList(name);
            go.state = StateType.Opening;
        }
        
        if (go.type == MoveType.TowSide)
        {
            Tweener twLeft = go.left.transform.DOLocalMoveX(go.left.transform.localPosition.x + go.toLeft.x, 1.0f);
            Tweener twRight = go.right.transform.DOLocalMoveX(go.right.transform.localPosition.x + go.toRight.x, 1.0f);
            twLeft.OnComplete(OnOpened);
            AddOpeningList(name);
            go.state = StateType.Opening;
        }

        if (go.type == MoveType.HalfLet)
        {
            Tweener twLeft = go.left.transform.DOLocalRotate(go.toLeft, 0.5f);
            twLeft.OnComplete(OnOpened);
            AddOpeningList(name);
            go.state = StateType.Opening;
        }

        if(go.type == MoveType.HalfRight)
        {
            Tweener twRight = go.right.transform.DOLocalRotate(go.toRight, 0.5f);
            twRight.OnComplete(OnOpened);
            AddOpeningList(name);
            go.state = StateType.Opening;
        }

        if(go.type == MoveType.Half160Left)
        {
            Tweener tw160Left = go.left.transform.DOLocalRotate(go.toLeft, 0.5f);
            tw160Left.OnComplete(OnOpened);
            AddOpenedList(name);
            go.state = StateType.Opening;
        }

        return true;
    }

    public bool CloseDoor(string name)
    {
        if (prefabsDict.ContainsKey(name) == false)
        {
            return false;
        }

        Door go = prefabsDict[name];
        if (go == null)
        {
            return false;
        }

        if (go.state != StateType.Opened)
        {
            return false;
        }

        if (go.type == MoveType.LeftRight)
        {
            Tweener twLeft = go.left.transform.DOLocalRotate(go.toLeftEnd, 0.6f);
            Tweener twRight = go.right.transform.DOLocalRotate(go.toRightEnd, 0.6f);
            twLeft.OnComplete(OnClosed);
            AddCloseList(name);
            go.state = StateType.Closing;
        }
        
        if (go.type == MoveType.TowSide)
        {
            Tweener twLeft = go.left.transform.DOLocalMoveX(go.left.transform.localPosition.x + go.toLeftEnd.x, 1.0f);
            Tweener twRight = go.right.transform.DOLocalMoveX(go.right.transform.localPosition.x + go.toRightEnd.x, 1.0f);
            twLeft.OnComplete(OnClosed);
            AddCloseList(name);
            go.state = StateType.Closing;
        }

        if (go.type == MoveType.HalfLet)
        {
            Tweener twLeft = go.left.transform.DOLocalRotate(go.toLeftEnd, 1.0f);
            twLeft.OnComplete(OnClosed);
            AddCloseList(name);
            go.state = StateType.Closing;
        }

        if (go.type == MoveType.HalfRight)
        {
            Tweener twRight = go.right.transform.DOLocalRotate(go.toRightEnd, 1.0f);
            twRight.OnComplete(OnClosed);
            AddCloseList(name);
            go.state = StateType.Closing;
        }

        return true;
    }

    void OnOpened()
    {
        if (openingList.Count > 0)
        {
            string name = openingList[0];
            Door go = prefabsDict[name];
            go.state = StateType.Opened;
            go.time = go.existTime;
            RemoveOpeningList(name);
            AddOpenedList(name);
        }
    }

    void OnClosed()
    {
        if (closingList.Count > 0)
        {
            string name = closingList[0];
            Door go = prefabsDict[name];
            go.state = StateType.Cloesd;
            RemvoeCloseList(name);
        }
    }

    void AddOpeningList(string name)
    {
        if (!openingList.Contains(name))
            openingList.Add(name);
    }

    void RemoveOpeningList(string name)
    {
        if (openingList.Contains(name))
            openingList.Remove(name);
    }

    void AddCloseList(string name)
    {
        if (!closingList.Contains(name))
            closingList.Add(name);
    }

    void RemvoeCloseList(string name)
    {
        if (closingList.Contains(name))
            closingList.Remove(name);
    }

    void AddOpenedList(string name)
    {
        if (!openedList.Contains(name))
            openedList.Add(name);
    }

    void RemoveOpenedList(string name)
    {
        if (openedList.Contains(name))
            openedList.Remove(name);
    }
}
