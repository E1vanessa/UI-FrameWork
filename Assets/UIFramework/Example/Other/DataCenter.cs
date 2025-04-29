using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Device;

public class DataCenter : MonoSingleton<DataCenter>
{
    public ExamUIData UIData;

    public void UpdateData(string ver)
    {
        var UImanager = UIManager.Instance;
        GameObject UIPrefab = UILoader.Instance.LoadUIPrefabSync("TopTip");
        string tipHashId = Time.time.GetHashCode().ToString() + " Tip";
        UImanager.RegisterTip(tipHashId, UIPrefab.GetComponent<TipController>());
        
        switch (ver)
        {
            case "playerName":
                UIManager.Instance.RegisteredTip[tipHashId].tipText.text = "�����Ѿ����ģ�";
                break;
            case "gold":
                UIManager.Instance.RegisteredTip[tipHashId].tipText.text = "����Ѿ����ģ�";
                break;
            case "diamond":
                UIManager.Instance.RegisteredTip[tipHashId].tipText.text = "��ʯ�Ѿ����ģ�";
                break;
            case "level":
                UIManager.Instance.RegisteredTip[tipHashId].tipText.text = "�ȼ��Ѿ����ģ�";
                break;
            case "sugar":
                UIManager.Instance.RegisteredTip[tipHashId].tipText.text = "�Ƿ�������£�";
                break;
        }
    }
}
