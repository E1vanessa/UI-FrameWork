using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public interface IUIController
{
    string ScreenId { get; set; }
    bool IsVisible { get; }

    public void Show();

    public void Hide();

    public void SetParent(Transform targetHierarchy);

    public void Destroy(float time);

    public void SetSiblingIndex(int index);

    public void BindEvent(string eventName,Action action,Button btn);

    public void BindEvent<T>(string eventName, Action<T> action);

    public void TriggerEvent(string eventName);

    public void TriggerEvent<T>(string eventName,T value);

    public void UnRegisterEvent(string eventName,Action action);

    public float UIAnimShow();

    public float UIAnimHide();

    public IEnumerator UIAnimStart();

    public IEnumerator UIAnimEnd();
}


public interface IPanelController : IUIController
{
    public PanelPriority Priority { get; }
}

public interface IWindowController : IUIController
{
    public WindowProperties Properties { get; }

    public Transform Parent { get; set; }
}

public interface ITipController : IUIController
{

}
