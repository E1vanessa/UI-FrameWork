using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TipController : ScreenController,ITipController
{
    public TMP_Text tipText;
    public float aliveTime;
    private float time;
    public Transform endPos;

    public virtual void Update()
    {
        time += Time.deltaTime;
        if(time > aliveTime) 
        {
            UIManager.Instance.HideTip(ScreenId);
        }
    }
}
