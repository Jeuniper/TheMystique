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
    private Transform UIScrollView;
    private Transform UIDetailPanel;
    private Transform UIRightBtn;//
    private Transform UIDetailBtn;

    public GameObject PackageUIItemPrefab;//ÿ����Ʒcell��Ԥ�Ƽ�

    override protected void Awake()
    {
        base.Awake();
        InitUI();
        InitClick();
    }

    private void Start()
    {
        //������Ʒ������������
        RefreshUI();
    }


    private void RefreshUI()//ˢ�±���UI
    {
        RefreshScrollView();
    }
    private void RefreshScrollView()//ˢ�¹�������
    {
        //�������������ݶ�ɾ��
        RectTransform scrollContent = UIScrollView.GetComponent<ScrollRect>().content;
        for (int i = 0; i < scrollContent.childCount; i++)
        {
            Destroy(scrollContent.GetChild(i).gameObject);
        }
        //��ȡ�������ݲ�������ʾ
        //Debug.Log(GameManager.Instance.GetPackageTables());
        foreach (PackageLocalItem localData in PackageLocalData.Instance.LoadPackage())//GameManager.Instance.GetPackageSortLocalData()
        {
            
            Transform PackageUIItem = Instantiate(PackageUIItemPrefab.transform, scrollContent) as Transform;
            UIPackageItem uIPackageItem = PackageUIItem.GetComponent<UIPackageItem>();
            uIPackageItem.Refresh(localData, this);

        }
    }


    private void InitUI()//ע��UI���
    {
        UIMenu = transform.Find("Top/Menu");
        UIMenuTools = transform.Find("Top/Menu/Tools");
        UIMenuCloths = transform.Find("Top/Menu/Cloths");
        CloseBtn = transform.Find("Top/backButton");
        Center = transform.Find("Center");
        UIScrollView = transform.Find("Center/ScrollView");
        UIDetailPanel = transform.Find("Center/ItemDetail");
    }

    private void InitClick()//��ʼ�����е���¼�
    {
        UIMenuTools.GetComponent<Button>().onClick.AddListener(OnClickTools);
    }

    private void OnClickTools()
    {
        print("OnClickTools");
    }
}
