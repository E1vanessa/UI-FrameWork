using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using DG.Tweening;

public class MessageWindowController : WindowController
{
    public TMP_Text textCpt;
    [Header("确定按钮")]
    public Button trueBtn;
    private Action trueAction;
    [Header("取消按钮")]
    public Button falseBtn;
    private Action falseAction;

    public Action delayAction;
    public float delayTime;

    public override void Awake()
    {
        base.Awake();
        Initialize();
    }

    public void Initialize()
    {
        trueAction = () => GetBtnOnTrue();
        falseAction = () => UIManager.Instance.HideWindow(this.ScreenId);
        BindEvent("MessageWindowTure", trueAction, trueBtn);
        BindEvent("MessageWindowFalse", falseAction, falseBtn);
    }


    public override void OnDestroy()
    {
        UnRegisterEvent("MessageWindowTure", trueAction);
        UnRegisterEvent("MessageWindowFalse", falseAction);
        base.OnDestroy();
    }

    public void GetBtnOnTrue()
    {
        Invoke("DelayInvokeAction", delayTime);
        UIManager.Instance.HideWindow(this.ScreenId);
    }

    private void DelayInvokeAction()
    {
        delayAction?.Invoke();
    }

    public override float UIAnimShow()
    {
        transform.localScale = Vector3.zero;
        transform.DOScale(0.5f, 0.3f).SetEase(Ease.OutExpo);
        transform.GetComponent<CanvasGroup>().alpha = 0;
        transform.GetComponent<CanvasGroup>().DOFade(1, 0.3f);
        return 0.3f;
    }

    public override float UIAnimHide()
    {
        transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.InBack);
        transform.GetComponent<CanvasGroup>().DOFade(0, 0.3f);
        return 0.3f;
    }
}
