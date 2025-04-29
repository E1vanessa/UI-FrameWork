using System;
using UnityEngine;

public enum WindowPriority
{
    Modal,
    UnModal
}

[Serializable]
public class WindowProperties
{
    [SerializeField]
    private WindowPriority priority;

    public WindowPriority Priority
    {
        get { return priority; }
        set { priority = value; }
    }

    public bool PanelblocksRaycasts = true;

    public bool OnDarkBackground = false;

    public bool AutoHide = false;
}