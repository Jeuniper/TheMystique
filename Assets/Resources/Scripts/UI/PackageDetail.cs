using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PackageDetail : MonoBehaviour
{
    private Transform UITop;
    private Transform UIIcon;
    private Transform UINamePart;
    private Transform UIName;
    private Transform UICenter;
    private Transform UIDescr;
    private Transform UIBottom;
    private Transform UIBottomButton;

    private PackageLocalItem packageLocalData;
    private packageItems packageTableItem;
    private PackagePanel uiParent;

    private void Awake()
    {
        InitUI();
        //Test();
    }

    private void InitUI()//ע��UI���
    {
        UITop = transform.Find("Top");
        UIIcon = transform.Find("Top/Image");
        UINamePart = transform.Find("Top/NamePart");
        UIName = transform.Find("Top/NamePart/name");
        UICenter = transform.Find("Center");
        UIDescr = transform.Find("Center/descr");
        UIBottom = transform.Find("Bottom");
        UIBottomButton = transform.Find("Bottom/Button");
        
    }
    //private void Test()
    //{
    //    Refresh(GameManager.Instance.GetPackageLocalData()[1], null);
    //}

    public void Refresh(PackageLocalItem packageLocalData, PackagePanel uiParent)
    {
        //��ʼ����̬���ݡ���̬���ݺ͸�����
        this.uiParent = uiParent;
        this.packageLocalData = packageLocalData;
        this.packageTableItem = GameManager.Instance.GetPackageItemById(packageLocalData.id);

        //����
        UIName.GetComponent<Text>().text = this.packageTableItem.name;
        //����
        UIDescr.GetComponent<Text>().text = this.packageTableItem.description;
        //ͼƬ
        Texture2D icon = (Texture2D)Resources.Load(this.packageTableItem.imagePath);
        Sprite temp = Sprite.Create(icon, new Rect(0, 0, icon.width, icon.height), new Vector2(0, 0));
        UIIcon.GetComponent<Image>().sprite = temp;

    }
}
