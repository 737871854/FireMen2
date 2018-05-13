using UnityEngine;
using System.Collections;

public class UIBase
{
    #region Singleton
    protected static UIBase instance = null;
    protected UIBase()
    {
        
    }

    public static UIBase Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new UIBase();
            }
            return instance;
        }
    }
    #endregion

    public GameObject UIRoot
    {
        protected set;
        get;
    }

    public GameObject UICamera
    {
        protected set;
        get;
    }

    public GameObject MainCanvas
    {
        protected set;
        get;
    }

    public GameObject SceneCanvas
    {
        protected set;
        get;
    }

    public void ReInit()
    {
        UIRoot = GameObject.Find("ui_root_race");
        UICamera = GameObject.Find("ui_root_race/ui_camera");
        MainCanvas = GameObject.Find("ui_root_race/main_canvas");
        SceneCanvas = GameObject.Find("ui_root_race/scene_canvas");
    }

    public GameObject GetUIGameObjectUnderMainCanvas(string path)
    {
        if(MainCanvas == null)
        {
            return null;
        }
        GameObject go = MainCanvas.transform.Find(path).gameObject;
        return go;
    }

    public GameObject GetUIGameObjectUnderSceneCanvas(string path)
    {
        if (SceneCanvas == null)
        {
            return null;
        }
        GameObject go = SceneCanvas.transform.Find(path).gameObject;
        return go;
    }
}
