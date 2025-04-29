using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopTipController : TipController
{
    //提示UI比较简单，暂时没有功能，此脚本仅用于挂载
    //但此处随时能拓展功能

    public override float UIAnimShow()
    {
        GetComponent<RectTransform>().DOAnchorPosY(440, 0.7f).SetEase(Ease.OutBack);
        transform.GetComponent<CanvasGroup>().alpha = 0;
        transform.GetComponent<CanvasGroup>().DOFade(1, 0.3f);
        return 0.75f;
    }

    public override float UIAnimHide()
    {
        GetComponent<RectTransform>().DOAnchorPosY(656, 1f).SetEase(Ease.OutBack);
        transform.GetComponent<CanvasGroup>().DOFade(0, 0.3f);
        return 1f;
    }
}
