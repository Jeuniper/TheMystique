using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PackagePanel : BasePanel
{
    private Transform UIMenu;
    private Transform UIMenuTools;
    private Transform UIMenuCloths;
    private Transform UICloseBtn;
    private Transform UICenter;
    private Transform UIScrollView;
    private Transform UIDetailPanel;
    private Transform UIRightBtn;//
    private Transform UIDetailBtn;

    public GameObject PackageUIItemPrefab;//每个物品cell的预制件

    override protected void Awake()
    {
        base.Awake();
        InitUI();
        InitClick();
    }

    private void Start()
    {
        //整个物品滚动容器部分
        RefreshUI();
    }


    private void RefreshUI()//刷新背包UI
    {
        RefreshScrollView();
    }
    private void RefreshScrollView()//刷新滚动容器
    {
        //把所有容器内容都删除
        RectTransform scrollContent = UIScrollView.GetComponent<ScrollRect>().content;
        for (int i = 0; i < scrollContent.childCount; i++)
        {
            Destroy(scrollContent.GetChild(i).gameObject);
        }
        //获取背包数据并整体显示
        foreach (PackageLocalItem localData in GameManager.Instance.GetPackageSortLocalData())
        {
            Transform PackageUIItem = Instantiate(PackageUIItemPrefab.transform, scrollContent) as Transform;
            UIPackageItem uIPackageItem = PackageUIItem.GetComponent<UIPackageItem>();
            uIPackageItem.Refresh(localData, this);

        }
    }


    private void InitUI()//注册UI组件
    {
        UIMenu = transform.Find("Top/Menu");
        UIMenuTools = transform.Find("Top/Menu/Tools");
        UIMenuCloths = transform.Find("Top/Menu/Cloths");
        UICloseBtn = transform.Find("Top/backButton");
        UICenter = transform.Find("Center");
        UIScrollView = transform.Find("Center/ScrollView");
        UIDetailPanel = transform.Find("Center/ItemDetail");
    }

    private void InitClick()//初始化所有点击事件
    {
        UIMenuTools.GetComponent<Button>().onClick.AddListener(OnClickTools);
        UICloseBtn.GetComponent<Button>().onClick.AddListener(OnClickClose);
    }

    private void OnClickTools()
    {
        print("OnClickTools");
    }
    private void OnClickClose()
    {
        Debug.Log(">>>>> OnClickClose");
        ClosePanel();
    }
}
