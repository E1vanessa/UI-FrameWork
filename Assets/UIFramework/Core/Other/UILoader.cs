using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class UILoader : MonoSingleton<UILoader>
{ 
    public string uiLoadPath = "UIPrefab";

    public GameObject LoadUIPrefabSync(string prefabName)
    {
        try
        {
            string prefabPath = $"{uiLoadPath}/{prefabName}";
            GameObject uiPrefab = Resources.Load<GameObject>(prefabPath);
            if (uiPrefab != null)
            {
                GameObject uiInstance = Instantiate(uiPrefab);
                uiInstance.name = prefabName;
                return uiInstance;
            }
            else
            {
                Debug.LogError($"δ���ҵ�Ԥ���壬·��: {prefabPath}");
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError($"ͬ������Ԥ����ʱ����: {e.Message}");
        }
        return null;
    }

    public void LoadUIPrefabAsync(string prefabName, Action<GameObject> onLoadComplete = null)
    {
        StartCoroutine(AsyncLoad(prefabName, onLoadComplete));
    }

    private IEnumerator AsyncLoad(string prefabName, Action<GameObject> onLoadComplete = null)
    {
        string prefabPath = $"{uiLoadPath}/{prefabName}";
        ResourceRequest request = Resources.LoadAsync<GameObject>(prefabPath);
        yield return request;

        if (request.asset != null)
        {
            GameObject uiPrefab = request.asset as GameObject;
            GameObject uiInstance = Instantiate(uiPrefab);
            uiInstance.name = prefabName;
            onLoadComplete?.Invoke(uiInstance);
        }
        else
        {
            Debug.LogError($"δ���ҵ�Ԥ���壬·��: {prefabPath}");
            onLoadComplete?.Invoke(null);
        }
    }
}
