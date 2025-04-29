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
                UIManager.Instance.RegisteredTip[tipHashId].tipText.text = "名字已经更改！";
                break;
            case "gold":
                UIManager.Instance.RegisteredTip[tipHashId].tipText.text = "金币已经更改！";
                break;
            case "diamond":
                UIManager.Instance.RegisteredTip[tipHashId].tipText.text = "钻石已经更改！";
                break;
            case "level":
                UIManager.Instance.RegisteredTip[tipHashId].tipText.text = "等级已经更改！";
                break;
            case "sugar":
                UIManager.Instance.RegisteredTip[tipHashId].tipText.text = "糖分任务更新！";
                break;
        }
    }
}
