using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ScreenController : MonoBehaviour, IUIController
{
    [SerializeField]
    private string screenId;
    public string ScreenId 
    { 
        get { return screenId; }
        set { screenId = value; }
    }

    [SerializeField]
    private bool isVisible;
    public bool IsVisible
    {
        get { return isVisible; }
        set { isVisible = value; }
    }

    public virtual void Show()
    {
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
        }
        StartCoroutine(UIAnimStart());
    }

    public virtual void Hide()
    {
        StartCoroutine(UIAnimEnd());
    }

    public virtual void Destroy(float time)
    {
        Destroy(gameObject,time);
    }

    public virtual void SetParent(Transform targetHierarchy)
    {
        transform.SetParent(targetHierarchy,worldPositionStays:false);
    }

    public void SetSiblingIndex(int index)
    {
        transform.SetSiblingIndex(index);
    }

    public void BindEvent(string eventName, Action action,Button btn)
    {
        EventManager.Instance.RegisterEvent(eventName, action);
        btn.onClick.AddListener(() => TriggerEvent(eventName));
    }

    public void BindEvent<T>(string eventName, Action<T> action)
    {
        EventManager.Instance.RegisterEvent<T>(eventName, action);
    }

    public virtual void TriggerEvent(string eventName)
    {
        EventManager.Instance.TriggerEvent(eventName);
    }

    public virtual void TriggerEvent<T>(string eventName,T value)
    {
        EventManager.Instance.BroadcastEvent<T>(eventName, value);
    }

    public virtual void UnRegisterEvent(string eventName,Action action)
    {
        EventManager.Instance.UnregisterEvent(eventName, action);
    }

    protected virtual void OpenPanel(string screenId)
    {
        var UImanager = UIManager.Instance;
        if (!UImanager.RegisteredPanel.ContainsKey(screenId))
        {
            GameObject UIPrefab = UILoader.Instance.LoadUIPrefabSync(screenId);
            UImanager.RegisterPanel(screenId, UIPrefab.GetComponent<PanelController>());
        }
        else if (UImanager.RegisteredPanel[screenId].Priority >= UImanager.panelLayer.currentPanel.Priority)
        {
            UImanager.HidePanel(UImanager.panelLayer.currentPanel.ScreenId);
            UImanager.SetPanelSiblingIndex(screenId, -1);
            UImanager.ShowPanel(screenId);
            UImanager.panelLayer.currentPanel = UImanager.RegisteredPanel[screenId];
        }
    }

    protected virtual void OpenWindow(string screenId)
    {
        var UImanager = UIManager.Instance;
        if (!UImanager.RegisteredWindow.ContainsKey(screenId))
        {
            GameObject UIPrefab = UILoader.Instance.LoadUIPrefabSync(screenId);
            UImanager.RegisterWindow(screenId, UIPrefab.GetComponent<WindowController>());
            UImanager.ShowWindow(screenId);
        }
        else
        {
            if (!UImanager.RegisteredWindow[screenId].gameObject.activeSelf) UImanager.ShowWindow(screenId);
        } 
    }

    protected virtual void OpenWindow(string screenId,bool OnBlack)
    {
        var UImanager = UIManager.Instance;
        if (!UImanager.RegisteredWindow.ContainsKey(screenId))
        {
            GameObject UIPrefab = UILoader.Instance.LoadUIPrefabSync(screenId);
            UImanager.RegisterWindow(screenId, UIPrefab.GetComponent<WindowController>());
            UImanager.ShowWindow(screenId);
        }
        else
        {
            if (!UImanager.RegisteredWindow[screenId].gameObject.activeSelf) UImanager.ShowWindow(screenId);
        }
        if (!OnBlack) UImanager.DestoryBlackGrounds();
    }

    protected virtual string OpenTip(string screenId)
    {
        var UImanager = UIManager.Instance;
        GameObject UIPrefab = UILoader.Instance.LoadUIPrefabSync(screenId);
        string tipHashId = Time.time.GetHashCode().ToString() + " Tip";
        UImanager.RegisterTip(tipHashId, UIPrefab.GetComponent<TipController>());
        return tipHashId;
    }

    public virtual void OnDestroy()
    {
        if(this is WindowController)
        {
            UIManager.Instance.UnRegisterWindow(ScreenId);
        }
        /*else if(this is PanelController)
        {
            UIManager.Instance.UnRegisterPanel(ScreenId);
        }*/
    }

    public virtual float UIAnimShow() { return 0f; }
    

    public virtual float UIAnimHide() { return 0f; }
    

    public IEnumerator UIAnimStart()
    {
        float time = UIAnimShow();
        yield return new WaitForSeconds(time);
        IsVisible = true;
    }

    public IEnumerator UIAnimEnd()
    {
        float time = UIAnimHide();
        yield return new WaitForSeconds(time);
        IsVisible = false;
        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
    }
}
