using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowController : ScreenController, IWindowController
{
    [SerializeField]
    private WindowProperties properties;
    public WindowProperties Properties
    {
        get { return properties; }
    }

    public Transform parent;
    public string parentId;
    public Transform Parent 
    { 
        get { return parent; }
        set { parent = value; }
    }

    public string root;


    public virtual void Awake()
    {
        if(Parent==null) InitParentPos();
    }

    public virtual void InitParentPos()
    {
        switch (parentId)
        {
            case "UnModel":
                Parent = GameObject.Find("UnModel").transform;
                break;
            case "Model":
                Parent = GameObject.Find("Model").transform;
                break;
            default:
                Parent = UIManager.Instance.RegisteredWindow[parentId].transform;
                break;
        };
    }
    
    public override void OnDestroy()
    {
        base.OnDestroy();
        UIManager.Instance.UnRegisterWindow(ScreenId);
    }
}
