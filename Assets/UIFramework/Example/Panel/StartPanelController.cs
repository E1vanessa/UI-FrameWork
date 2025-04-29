using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartPanelController : PanelController
{
    public Button start;
    private Action startAction;
    public string mainId;

    public void Awake()
    {
        Initialize();
    }

    public void Initialize()
    {
        start = GetComponentInChildren<Button>();
        startAction = () => OpenPanel(mainId);
        BindEvent("StartPanelGoMainPanel", startAction,start);
    }


    public override void OnDestroy()
    {
        UnRegisterEvent("StartPanelGoMainPanel", startAction);
        base.OnDestroy();
    }

    public override float UIAnimShow()
    {
        transform.localScale = Vector3.zero;
        transform.DOScale(1f, 0.5f).SetEase(Ease.OutExpo);
        transform.GetComponentInParent<CanvasGroup>().alpha = 0;
        transform.GetComponentInParent<CanvasGroup>().DOFade(1, 0.4f);
        return 0.5f;
    }

    public override float UIAnimHide()
    {
        transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InOutExpo);
        transform.GetComponentInParent<CanvasGroup>().DOFade(0, 0.3f);
        return 0.5f;
    }
}
