using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Device;

public class PanelLayer : UILayer<PanelController>
{
    [SerializeField]
    private PanelPriorityLayerList panelPriorityLayerList;

    public PanelController currentPanel;

    protected override void Initialize()
    {
        base.Initialize();
        panelPriorityLayerList = new PanelPriorityLayerList();
        PanelController[] collection = GetComponentsInChildren<PanelController>(true);
        foreach (var tc in collection)
        {
            RegisterScreen(tc.gameObject.name, tc);
        }
    }

    public override void Show(string screenId)
    {
        if (registeredScreen.ContainsKey(screenId))
        {
            registeredScreen[screenId].Show();
        }
    }

    public override void Hide(string screenId)
    {
        if (registeredScreen.ContainsKey(screenId))
        {
            registeredScreen[screenId].Hide();
        }
    }

    public override void RegisterScreen(string screenId, PanelController controller)
    {
        if(currentPanel != null)
        {
            Hide(currentPanel.ScreenId);
        }
        base.RegisterScreen(screenId, controller);
        ResetHierarchy(screenId);
        PriorityHandle(registeredScreen[screenId]);
    }

    public override void UnRegisterScreen(string screenId)
    {   
        base.UnRegisterScreen(screenId);
        int index = 0;
        foreach (var tc in panelPriorityLayerList.panelList)
        {
            if (tc.Controller == registeredScreen[screenId])
            {
                panelPriorityLayerList.panelList.RemoveAt(index);
            }
            index++;
        }
    }

    public override void PriorityHandle(PanelController controller)
    {
        if(controller != null)
        {
            PanelPriorityLayerListEntry entry = new PanelPriorityLayerListEntry(controller, controller.Priority);
            entry.Name = controller.gameObject.name;
            panelPriorityLayerList.panelList.Add(entry);

            panelPriorityLayerList.SortByPriority();
        }

        
        PanelController[] list = new PanelController[panelPriorityLayerList.panelList.Count];
        for(int i = 0; i < list.Length; i++)
        {
            list[i] = panelPriorityLayerList.panelList[i].Controller;
        }
        PriorityShow(list);
        currentPanel = list[list.Length - 1];

        controller.SetSiblingIndex(-1);

        /*if (controller.Priority == currentPanel.Priority && controller != currentPanel)
        {
            int lastIndex = panelPriorityLayerList.panelList.Count - 1;
            int secondLastIndex = panelPriorityLayerList.panelList.Count - 2;

            var temp = panelPriorityLayerList.panelList[lastIndex];
            panelPriorityLayerList.panelList[lastIndex] = panelPriorityLayerList.panelList[secondLastIndex];
            panelPriorityLayerList.panelList[secondLastIndex] = temp;

            currentPanel = controller;
            
        }*/
    }

    public virtual void PriorityShow(PanelController[] controllers)
    {
        int layerIndex = 0;
        foreach (PanelController controller in controllers)
        {
            if (layerIndex < controllers.Length - 1) controller.Hide();
            else controller.Show();
            controller.SetSiblingIndex(layerIndex++);
        }

    }
}
