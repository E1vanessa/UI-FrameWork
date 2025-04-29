using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class MainPanelController : PanelController
{
    [Header("���ذ�ť")]
    public Button backBtn;
    private Action backAction;
    public string backPanelId;

    [Header("ͷ��ť")]
    public Button avatarBtn;
    private Action avatarAction;
    public string avatarId;

    [Header("���ﰴť")]
    public Button giftBtn;
    private Action giftAction;
    public string giftTipId;
    public string giftTipInfo;

    [Header("������ť")]
    public Button levelBtn;
    private Action levelAction;

    [Header("���䰴ť")]
    public Button sugarBtn;
    private Action sugarAction;

    [Header("����ť")]
    public Button buyBtn;
    public string buyId;
    private Action buyAction;

    [Header("��ɰ�ť")]
    public Button GoBtn;
    public string GoId;
    public string finishText;
    private Action goAction;

    [Header("�ȼ�")]
    public TMP_Text level;

    [Header("���")]
    public TMP_Text gold;

    [Header("��ʯ")]
    public TMP_Text diamond;

    [Header("����")]
    public TMP_Text playerName;

    public TMP_Text currentPanelName;

    public void Awake()
    {
        Initialize();
    }

    public void Initialize()
    {
        backAction = ()=> OpenPanel(backPanelId);
        avatarAction = ()=> OpenWindow(avatarId);
        giftAction = ()=> OpenGift();
        levelAction = ()=> UpLevel();
        sugarAction = () => Sugar();
        buyAction = () => OpenWindow(buyId);
        goAction = () => Finish();
        BindEvent("MainPanelBackStartPanel", backAction, backBtn);
        BindEvent("MainPanelOpenAvatarWindow", avatarAction, avatarBtn);
        BindEvent("MainPanelGift",giftAction, giftBtn);
        BindEvent("UpLevel",levelAction, levelBtn);
        BindEvent("Sugar", sugarAction, sugarBtn);
        BindEvent("Shop",buyAction,buyBtn);
        BindEvent("Finish", goAction, GoBtn);
    }


    public override void OnDestroy()
    {
        UnRegisterEvent("MainPanelBackStartPanel", backAction);
        UnRegisterEvent("MainPanelOpenAvatarWindow", avatarAction);
        UnRegisterEvent("MainPanelGift", giftAction);
        UnRegisterEvent("UpLevel", levelAction);
        UnRegisterEvent("Sugar", sugarAction);
        UnRegisterEvent("Shop", buyAction);
        UnRegisterEvent("Finish", goAction);
        base.OnDestroy();
    }

    public void OpenGift()
    {
        string tipHashId = OpenTip(giftTipId);
        UIManager.Instance.RegisteredTip[tipHashId].tipText.text = giftTipInfo;
        DataCenter.Instance.UIData.uiData.gold += 1000;
        DataCenter.Instance.UIData.uiData.diamond += 100;
    }

    public void UpLevel()
    {
        if (DataCenter.Instance.UIData.uiData.gold >= 200) 
        {
            DataCenter.Instance.UIData.uiData.gold -= 200;
            DataCenter.Instance.UIData.uiData.level += 1;
            DataCenter.Instance.UpdateData("level");
        } 
    }

    public void Sugar()
    {
        if (DataCenter.Instance.UIData.uiData.diamond >= 100)
        {
            DataCenter.Instance.UIData.uiData.diamond -= 100;
            DataCenter.Instance.UIData.uiData.sugar += 1;
            DataCenter.Instance.UpdateData("sugar");
        }
    }

    public void FixedUpdate()
    {
        level.text = DataCenter.Instance.UIData.uiData.level.ToString();
        gold.text = DataCenter.Instance.UIData.uiData.gold.ToString();
        diamond.text = DataCenter.Instance.UIData.uiData.diamond.ToString();
        playerName.text = DataCenter.Instance.UIData.uiData.playerName.ToString();
        if (UIManager.Instance.windowLayer.currentWindow != null) currentPanelName.text = "��ǰ�������֣�" + UIManager.Instance.windowLayer.currentWindow.gameObject.name.ToString();
        else currentPanelName.text = "��ǰ�������֣���";
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

    public void Finish()
    {
        OpenWindow(GoId,false);
        MessageWindowController message = UIManager.Instance.RegisteredWindow[GoId] as MessageWindowController;
        message.textCpt.text = finishText;
        message.delayTime = 0.3f;
        message.delayAction = () =>
        {
            OpenPanel(backPanelId);
        };
    }
}
