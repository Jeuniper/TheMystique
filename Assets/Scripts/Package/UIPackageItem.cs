using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UIPackageItem : MonoBehaviour
{
    private Transform UISelected;
    private Transform UIDeleted;
    private Transform UITop;
    private Transform UIicon;
    private Transform UIbg;
    private Transform UInew;
    private Transform Bottom;

    private void InitUI()//ע��UI���
    {
        UISelected = transform.Find("Selected");
        UIDeleted = transform.Find("Deleted");
        UITop = transform.Find("Top");
        UIicon = transform.Find("Top/icon");
        UInew = transform.Find("Top/new");
    }

    private void InitClick()//��ʼ�����е���¼�
    {
        GetComponent<Button>().onClick.AddListener(OnClickThis);
    }

    private void OnClickThis()
    {
        print("OnClickItem");
    }
}
