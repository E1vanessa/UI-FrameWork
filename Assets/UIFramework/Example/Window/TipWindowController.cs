using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipWindowController : WindowController
{
    [Header("·µ»Ø°´Å¥")]
    public Button backBtn;

    public override void Awake()
    {
        base.Awake();
        Initialize();
    }

    public void Initialize()
    {
        BindEvent("TipWindowBackStartPanel", () => UIManager.Instance.HideWindow(this.ScreenId),backBtn);
    }


    public override void OnDestroy()
    {
        UnRegisterEvent("TipWindowBackStartPanel", () => UIManager.Instance.HideWindow(this.ScreenId));
        base.OnDestroy();
    }

    public override float UIAnimShow()
    {
        transform.localScale = Vector3.zero;
        transform.DOScale(0.9f, 0.3f).SetEase(Ease.OutExpo);
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
