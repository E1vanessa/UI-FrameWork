using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPool : MonoSingleton<UIPool>
{
    public List<GameObject> UIList;
    private Dictionary<GameObject, Coroutine> DestroyCoroutine = new Dictionary<GameObject, Coroutine>();
    public float time = 5f;

    protected override void Awake()
    {
        base.Awake();
        UIList = new List<GameObject>();
    }

    public void Update()
    {
        CheckListAction();
    }

    public void PushPool(GameObject gameObject)
    {
        if (gameObject == null) return;

        gameObject.SetActive(false);
        UIList.Add(gameObject);

        if (DestroyCoroutine.TryGetValue(gameObject, out Coroutine existingCoroutine))
        {
            StopCoroutine(existingCoroutine);
        }

        DestroyCoroutine[gameObject] = StartCoroutine(AutoDestroy(gameObject));
    }

    public IEnumerator AutoDestroy(GameObject gameObject)
    {
        yield return new WaitForSeconds(time);

        if (gameObject != null && UIList.Contains(gameObject))
        {
            UIList.Remove(gameObject);
            DestroyCoroutine.Remove(gameObject);
            Destroy(gameObject);
        }
    }

    private void CheckListAction()
    {
        if (UIList.Count == 0) return;

        for (int i = UIList.Count - 1; i >= 0; i--)
        {
            GameObject obj = UIList[i];
            if (obj == null)
            {
                UIList.RemoveAt(i);
                continue;
            }

            if (obj.activeSelf)
            {
                if (DestroyCoroutine.TryGetValue(obj, out Coroutine coroutine))
                {
                    StopCoroutine(coroutine);
                    DestroyCoroutine.Remove(obj);
                }
                UIList.RemoveAt(i);
            }
        }
    }
}
