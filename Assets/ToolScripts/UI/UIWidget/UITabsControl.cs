using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

[Serializable]
public class UITabControlEntry
{
    public GameObject Panel;
    public GameObject Tab;
    public UITabControlEntry(GameObject tab, GameObject panel)
    {
        Tab = tab;
        Panel = panel;
    }
}

public class UITabsControl : MonoBehaviour {
    //[SerializeField]
    //public UITabControlEntry[] entries;
    public List<UITabControlEntry> Entries;

    //public void Awake(){
        //Entries = new List<UITabControlEntry>();
    //}
    public void AddEntry(GameObject tab, GameObject panel)
    {
        Entries.Add(new UITabControlEntry(tab, panel));
    }

	// Use this for initialization
	public void Init () {
        BindButtonListener();
        if (Entries.Count > 0)
        {
            ActivePanel(Entries[0]);
        }
	}
	
	void BindButtonListener(){
        foreach (UITabControlEntry entry in Entries)
        {
            // 把上面两行换成下面的一个函数的话，就可以正常
            //Button btn = GetEntryButton(enery);
            //btn.onClick.AddListener (() => ActivePanel(enery));
            AddButtonListener(entry);
        }
    }
    private void AddButtonListener(UITabControlEntry entry)
    {
        Button btn = GetEntryButton(entry);
        btn.onClick.AddListener(() => ActivePanel(entry));
    }

    void ActivePanel(UITabControlEntry activeEntry){
        foreach (UITabControlEntry entry in Entries)
        {
            bool isSelect = (activeEntry == entry);
            //Debug.Log(isSelect);
            Button btn = GetEntryButton(entry);
            // 正在使用的，就是不可按的
            btn.interactable = !isSelect;
            entry.Panel.SetActive(isSelect);
        }

    }

    Button GetEntryButton(UITabControlEntry entry)
    {
        Button btn = entry.Tab.GetComponent<Button>();
        if (btn == null)
        {
            btn = entry.Tab.GetComponentInChildren<Button>();
        }
        return btn;
    }

    public void ActivePanel(int index){
        int count = Entries.Count;
        if (index >= 0 && index < count)
        {
            ActivePanel(Entries[index]);
        }
    }
}
