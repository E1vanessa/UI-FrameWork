using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopWindowController : WindowController
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
        BindEvent("ShopWindowBack", () => UIManager.Instance.HideWindow(this.ScreenId), backBtn);
    }


    public override void OnDestroy()
    {
        UnRegisterEvent("ShopWindowBack", () => UIManager.Instance.HideWindow(this.ScreenId));
        base.OnDestroy();
    }

    public override float UIAnimShow()
    {
        GetComponent<RectTransform>().DOAnchorPosX(800, 0.7f).SetEase(Ease.OutBack);
        transform.GetComponent<CanvasGroup>().alpha = 0;
        transform.GetComponent<CanvasGroup>().DOFade(1, 0.3f);
        return 0.75f;
    }

    public override float UIAnimHide()
    {
        GetComponent<RectTransform>().DOAnchorPosX(1400, 1f).SetEase(Ease.OutBack);
        transform.GetComponent<CanvasGroup>().DOFade(0, 0.3f);
        return 1f;
    }
}
