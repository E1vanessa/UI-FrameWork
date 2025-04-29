using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipLayer : UILayer<TipController>
{
    public Queue<TipController> tipQueue;
    public TipController currentTip;

    protected override void Initialize()
    {
        base.Initialize();
        tipQueue = new Queue<TipController>();
    }

    public override void Show(string screenId)
    {
        if (registeredScreen.ContainsKey(screenId))
        {
            registeredScreen[screenId].Show();
        }
    }

    public override void RegisterScreen(string screenId, TipController controller)
    {
        base.RegisterScreen(screenId, controller);
        ResetHierarchy(screenId);
        PriorityHandle(registeredScreen[screenId]);
    }

    public override void Hide(string screenId)
    {
        if (registeredScreen.ContainsKey(screenId))
        {
            registeredScreen[screenId].Hide();
            Destroy(registeredScreen[screenId].gameObject,1.2f);
            tipQueue.Dequeue();
            UnRegisterScreen(screenId);
            if(tipQueue.Count > 0)
            {
                currentTip = tipQueue.Peek();
                currentTip.Show();
            }
        }
    }

    public override void PriorityHandle(TipController controller)
    {
        tipQueue.Enqueue(controller);
        controller.SetSiblingIndex(0);
        if(tipQueue.Count == 1)
        {
            currentTip = controller;
            Show(controller.ScreenId);
        }
    }

    
}
