using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
public class UIManager
{
    private static UIManager _instance;
    private Transform _uiRoot;
    // ·�������ֵ�
    private Dictionary<string, string> pathDict;
    // Ԥ�Ƽ������ֵ�
    private Dictionary<string, GameObject> prefabDict;
    // �Ѵ򿪽���Ļ����ֵ�
    public Dictionary<string, BasePanel> panelDict;

    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new UIManager();
            }
            return _instance;
        }
    }

    public Transform UIRoot
    {
        get
        {
            if (_uiRoot == null)
            {
                _uiRoot = GameObject.Find("Canvas").transform;
                return _uiRoot;
            };
            return _uiRoot;
        }
    }

    private UIManager()
    {
        InitDicts();
    }

    private void InitDicts()//��Ҫ����
    {
        prefabDict = new Dictionary<string, GameObject>();
        panelDict = new Dictionary<string, BasePanel>();

        pathDict = new Dictionary<string, string>()
        {
            {UIConst.PackagePanel, "Package/PackagePanel"},
        };
    }

    public BasePanel OpenPanel(string name)
    {
        BasePanel panel = null;
        // ����Ƿ��Ѵ�
        if (panelDict.TryGetValue(name, out panel))
        {
            Debug.LogError("�����Ѵ�: " + name);
            return null;
        }

        // ���·���Ƿ�����
        string path = "";
        if (!pathDict.TryGetValue(name, out path))
        {
            Debug.LogError("�������ƴ��󣬻�δ����·��: " + name);
            return null;
        }

        // ʹ�û���Ԥ�Ƽ�
        GameObject panelPrefab = null;
        if (!prefabDict.TryGetValue(name, out panelPrefab))
        {
            string realPath = "Prefabs/UI/" + path;
            panelPrefab = Resources.Load<GameObject>(realPath) as GameObject;
            prefabDict.Add(name, panelPrefab);
        }
        Debug.Log(panelPrefab);
        // �򿪽���
        GameObject panelObject = GameObject.Instantiate(panelPrefab, UIRoot, false);
        panel = panelObject.GetComponent<BasePanel>();
        panelDict.Add(name, panel);
        panel.OpenPanel(name);
        return panel;
    }

    public bool ClosePanel(string name)
    {
        BasePanel panel = null;
        if (!panelDict.TryGetValue(name, out panel))
        {
            Debug.LogError("����δ��: " + name);
            return false;
        }

        panel.ClosePanel();
        // panelDict.Remove(name);
        return true;
    }

    //public void ShowTip(string tip)
    //{
    //    MenuTipPanel menuTipPanel = OpenPanel(UIConst.MenuTipPanel) as MenuTipPanel;
    //    menuTipPanel.InitTip(Globals.TipCreateOne);
    //}
}

public class UIConst//��Ҫ����
{
    // menu panels
    public const string PackagePanel = "PackagePanel";
}