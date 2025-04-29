using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CharacterWindowController : WindowController
{
    [Header("退出功能")]
    public Button backBtn;
    private Action backAction;
    public Button cancelBtn;
    private Action cancelAction;

    [Header("创建功能")]
    public Button createBtn;
    private Action createAction;
    public string createWindowId;
    public string createInfo;
    public float delayTime;

    public override void Awake()
    {
        base.Awake();
        Initialize();
    }

    public void Initialize()
    {
        backAction = ()=> UIManager.Instance.HideWindow(this.ScreenId);
        cancelAction = ()=> UIManager.Instance.HideWindow(this.ScreenId);
        createAction = ()=> Create();
        BindEvent("CharaterWindowHide", backAction, backBtn);
        BindEvent("CharaterWindowCancel", cancelAction, cancelBtn);
        BindEvent("CharaterWindowCreate", createAction, createBtn);
    }


    public override void OnDestroy()
    {
        UnRegisterEvent("CharaterWindowHide", backAction);
        UnRegisterEvent("CharaterWindowCancel", cancelAction);
        UnRegisterEvent("CharaterWindowCreate", createAction);
        base.OnDestroy();
    }

    public void Create()
    {
        OpenWindow(createWindowId);
        MessageWindowController message = UIManager.Instance.RegisteredWindow[createWindowId] as MessageWindowController;
        message.textCpt.text = createInfo;
        message.delayTime = delayTime;
        message.delayAction = () =>
        {
            UIManager.Instance.HideWindow(ScreenId);
        };
    }

    public override float UIAnimShow()
    {
        transform.localScale = Vector3.zero;
        transform.DOScale(0.7f, 0.3f).SetEase(Ease.OutExpo);
        transform.GetComponent<CanvasGroup>().alpha = 0;
        transform.GetComponent<CanvasGroup>().DOFade(1, 0.3f);
        return 0.3f;
    }

    public override float UIAnimHide()
    {
        transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.InBack);
        transform.GetComponent<CanvasGroup>().DOFade(0, 0.2f);
        return 0.3f;
    }
}
