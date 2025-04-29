using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UILayer<TC> : MonoBehaviour where TC : IUIController
{
    public Dictionary<string, TC> registeredScreen;

    protected virtual void Initialize()
    {
        if(registeredScreen == null) registeredScreen = new Dictionary<string, TC>();
    }

    public abstract void Show(string screenId);

    public abstract void Hide(string screenId);

    public virtual void HideAll()
    {
        if(registeredScreen != null)
        {
            foreach(var screen in registeredScreen)
            {
                screen.Value.Hide();
            }
        }
    }

    public virtual void DestroyScreen(string screenId)
    {
        registeredScreen[screenId].Destroy(0);
    }

    public virtual void RegisterScreen(string screenId, TC controller)
    {
        if (!registeredScreen.ContainsKey(screenId))
        {
            controller.ScreenId = screenId;
            registeredScreen.Add(screenId, controller);
        }
        else
        {
            Debug.LogError(gameObject.name + "ÒÑ¾­×¢²áÁË" + screenId);
        }
    }

    public virtual void UnRegisterScreen(string screenId)
    {
        if (registeredScreen.ContainsKey(screenId))
        {
            registeredScreen.Remove(screenId);
        }
    }

    public virtual void ResetHierarchy(string screenId)
    {
        if(registeredScreen != null)
        {
            if (registeredScreen.ContainsKey(screenId))
            {
                registeredScreen[screenId].SetParent(transform);
            }
        }
    }

    public abstract void PriorityHandle(TC controller);
    
    public virtual void SetSiblingIndex(string screenId,int index)
    {
        registeredScreen[screenId].SetSiblingIndex(index);
    }
    public virtual void Awake()
    {
        Initialize();
    }
}
