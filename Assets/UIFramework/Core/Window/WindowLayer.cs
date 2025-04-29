using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Device;

public class WindowLayer : UILayer<WindowController>
{
    public Queue<WindowController> windowQueue;
    public Stack<WindowController> windowHistory;
    public WindowController RootWindow;

    public WindowController currentWindow;

    private CanvasGroup panelCanvasGroup => UIManager.Instance.panelCanvasGroup;

    protected override void Initialize()
    {
        base.Initialize();
        windowQueue = new Queue<WindowController>();
        windowHistory = new Stack<WindowController>();
        WindowController[] collection = GetComponentsInChildren<WindowController>(true);
        foreach (var tc in collection)
        {
            tc.InitParentPos();
            RegisterScreen(tc.gameObject.name, tc);
            Show(tc.ScreenId);
        }
    }

    public override void Show(string screenId)
    {
        if (registeredScreen.ContainsKey(screenId))
        {
            ResetHierarchy(screenId);
            PriorityHandle(registeredScreen[screenId]);
        }
    }
    public override void Hide(string screenId)
    {
        if (registeredScreen.ContainsKey(screenId) && windowHistory.Count > 0 && windowHistory.Peek() == registeredScreen[screenId])
        {
            registeredScreen[screenId].Hide();

            windowHistory.Pop();

            if (registeredScreen[screenId].Properties.Priority == WindowPriority.Modal)
            {
                if (registeredScreen[screenId].Properties.OnDarkBackground && UIManager.Instance.BlackGroundPos.childCount != 0)
                    Destroy(UIManager.Instance.BlackGroundPos.GetChild(0).gameObject);
            }

            if(windowHistory.Count != 0)
            {
                currentWindow = windowHistory.Peek();
                if (currentWindow.Properties.PanelblocksRaycasts)
                    panelCanvasGroup.blocksRaycasts = RootWindow.Properties.PanelblocksRaycasts;
                if(!currentWindow.gameObject.activeSelf) currentWindow.Show();
            }
            else
            {
                currentWindow = null;
                RootWindow = null;
                panelCanvasGroup.blocksRaycasts = true;
            }
        }
        UIPool.Instance.PushPool(registeredScreen[screenId].gameObject);
    }

    public override void ResetHierarchy(string screenId)
    {
        if (registeredScreen != null)
        {
            if (registeredScreen.ContainsKey(screenId))
            {
                Transform parent = registeredScreen[screenId].Parent;
                registeredScreen[screenId].SetParent(parent);
            }
        }
    }

    public override void PriorityHandle(WindowController controller)
    {
        if(currentWindow != null)
        {
            switch (currentWindow.Properties.Priority)
            {
                case WindowPriority.UnModal:
                    if (controller.Properties.Priority == WindowPriority.UnModal)
                        UnModelHandle(controller);
                    else
                        ModelHandle(controller);
                    break;
                case WindowPriority.Modal:
                    if (controller.Properties.Priority == WindowPriority.UnModal)
                        windowQueue.Enqueue(controller);
                    else
                        SpecialHandle(controller);
                    break;
            }
        }
        else
        {
            if (windowHistory.Count == 0) RootWindow = controller;
            if(controller.Properties.Priority == WindowPriority.UnModal) UnModelHandle(controller);
            if (controller.Properties.Priority == WindowPriority.Modal) ModelHandle(controller);
        }
    }

    private void UnModelHandle(WindowController controller)
    { 
        if(currentWindow != null) 
        {
            if (currentWindow.Properties.AutoHide && controller.root != currentWindow.GetComponent<WindowController>().GetType().ToString())
                Hide(currentWindow.ScreenId);
        }
        if (!controller.Properties.PanelblocksRaycasts) panelCanvasGroup.blocksRaycasts = false;

        windowHistory.Push(controller);
        controller.Show();
        currentWindow = controller;
    }

    private void ModelHandle(WindowController controller)
    {
        if (controller.Properties.OnDarkBackground) DarkBackground();
        if (!controller.Properties.PanelblocksRaycasts) panelCanvasGroup.blocksRaycasts = false;

        windowHistory.Push(controller);
        controller.Show();
        currentWindow = controller;
    }

    private void SpecialHandle(WindowController controller)
    {
        if (controller.Properties.OnDarkBackground) DarkBackground();
        if (!controller.Properties.PanelblocksRaycasts) panelCanvasGroup.blocksRaycasts = false;
    }

    private void DarkBackground()
    {
        UIManager.Instance.BlackGround();
    }
}
