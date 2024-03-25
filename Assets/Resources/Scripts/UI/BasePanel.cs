using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePanel : MonoBehaviour
{
    protected bool isRemove = false;
    protected new string name;
    virtual protected void Awake()
    {
        
    }
    public virtual void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }

    public virtual void OpenPanel(string name)
    {
        this.name = name;
        SetActive(true);
    }

    public virtual void ClosePanel()
    {
        isRemove = true;
        SetActive(false);
        Destroy(gameObject);
        //foreach (KeyValuePair<string, BasePanel> i in UIManager.Instance.panelDict)
        //{
        //    Debug.Log(i.Key);
        //}
        if (UIManager.Instance.panelDict.ContainsKey(name))
        {
            UIManager.Instance.panelDict.Remove(name);
        }
    }
}