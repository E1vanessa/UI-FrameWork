using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoSingleton<UIManager>
{
    public PanelLayer panelLayer;
    private Dictionary<string, PanelController> registeredPanel => panelLayer.registeredScreen;
    public Dictionary<string, PanelController> RegisteredPanel { get { return registeredPanel; } }

    [HideInInspector]
    public CanvasGroup panelCanvasGroup;

    public WindowLayer windowLayer;
    private Dictionary<string, WindowController> registeredWindow => windowLayer.registeredScreen;
    public Dictionary<string, WindowController> RegisteredWindow { get { return registeredWindow; } }

    public TipLayer tipLayer;
    private Dictionary<string, TipController> registeredTip => tipLayer.registeredScreen;
    public Dictionary<string, TipController> RegisteredTip { get { return registeredTip; } }

    public Transform BlackGroundPos;

    protected override void Awake()
    {
        base.Awake();
        Initialize();
    }

    private void Initialize()
    {
        panelCanvasGroup = panelLayer.GetComponent<CanvasGroup>();
    }

    public void ShowPanel(string screenId)
    {
        panelLayer.Show(screenId);
    }

    public void HidePanel(string screenId)
    {
        panelLayer.Hide(screenId);
    }

    public void HideAllPanel()
    {
        panelLayer.HideAll();
    }

    public void RegisterPanel(string screenId,PanelController controller)
    {
        panelLayer.RegisterScreen(screenId, controller);
    }

    public void UnRegisterPanel(string screenId)
    {
        panelLayer.UnRegisterScreen(screenId);
    }

    public void SetPanelSiblingIndex(string screenId,int index)
    {
        panelLayer.SetSiblingIndex(screenId,index);
    }

    public void ShowWindow(string screenId)
    {
        windowLayer.Show(screenId);
    }

    public void HideWindow(string screenId)
    {
        windowLayer.Hide(screenId);
    }

    public void HideAllWindow()
    {
        windowLayer.HideAll();
    }

    public void RegisterWindow(string screenId, WindowController controller)
    {
        windowLayer.RegisterScreen(screenId, controller);
    }

    public void UnRegisterWindow(string screenId)
    {
        windowLayer.UnRegisterScreen(screenId);
    }

    public void SetWindowSiblingIndex(string screenId, int index)
    {
        windowLayer.SetSiblingIndex(screenId, index);
    }

    public void ShowTip(string screenId)
    {
        tipLayer.Show(screenId);
    }

    public void HideTip(string screenId)
    {
        tipLayer.Hide(screenId);
    }

    public void RegisterTip(string screenId, TipController controller)
    {
        tipLayer.RegisterScreen(screenId, controller);
    }

    public void UnRegisterTip(string screenId)
    {
        tipLayer.UnRegisterScreen(screenId);
    }

    public void BlackGround()
    {
        GameObject mask = UILoader.Instance.LoadUIPrefabSync("BlackMask");
        mask.transform.SetParent(BlackGroundPos, worldPositionStays: false);
    }

    public void DestoryBlackGrounds()
    {
        Destroy(BlackGroundPos.GetChild(0).gameObject);
    }
}
