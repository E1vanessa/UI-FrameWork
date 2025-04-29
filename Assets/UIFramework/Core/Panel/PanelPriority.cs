using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum PanelPriority
{
    None = 0,
    priority = 1,
    tutorial = 2,
    blocker = 3
}
[Serializable]
public class PanelPriorityLayerListEntry
{
    private PanelController controller;
    [SerializeField]
    private PanelPriority priority;
    [SerializeField]
    private string name;

    public string Name 
    {
        get { return name; } 
        set { name = value; }
    }

    public PanelController Controller { get { return controller; } }

    public PanelPriority Priority { get { return priority; } }

    
    public PanelPriorityLayerListEntry(PanelController controller, PanelPriority priority)
    {
        this.controller = controller;
        this.priority = priority;
    }
}

[Serializable]
public class PanelPriorityLayerList
{
    public List<PanelPriorityLayerListEntry> panelList = new List<PanelPriorityLayerListEntry>();

    public void SortByPriority()
    {
        if (panelList != null)
        {
            panelList.Sort((x, y) => x.Priority.CompareTo(y.Priority));
        }
    }
}
