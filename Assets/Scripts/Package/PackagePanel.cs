using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PackagePanel : BasePanel
{
    private Transform UIMenu;
    private Transform UIMenuTools;
    private Transform UIMenuCloths;
    private Transform CloseBtn;
    private Transform Center;
    private Transform ScrollView;
    private Transform UIDetailPanel;
    private Transform UIRightBtn;//
    private Transform UIDetailBtn;

    override protected void Awake()
    {
        base.Awake();
        InitUI();
        InitClick();
    }
    
    private void InitUI()//注册UI组件
    {
        UIMenu = transform.Find("Top/Menu");
        UIMenuTools = transform.Find("Top/Menu/Tools");
        UIMenuCloths = transform.Find("Top/Menu/Cloths");
        CloseBtn = transform.Find("Top/backButton");
        Center = transform.Find("Center");
        ScrollView = transform.Find("Center/ScrollView");
        UIDetailPanel = transform.Find("Center/ItemDetail");
    }

    private void InitClick()//初始化所有点击事件
    {
        UIMenuTools.GetComponent<Button>().onClick.AddListener(OnClickTools);
    }

    private void OnClickTools()
    {
        print("OnClickTools");
    }
}
