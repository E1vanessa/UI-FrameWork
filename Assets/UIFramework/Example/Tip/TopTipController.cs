using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopTipController : TipController
{
    //��ʾUI�Ƚϼ򵥣���ʱû�й��ܣ��˽ű������ڹ���
    //���˴���ʱ����չ����

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
