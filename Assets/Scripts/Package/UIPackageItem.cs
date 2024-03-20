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
    private Transform UIBottom;
    private Transform UInum;

    //获取物品的动态数据
    private PackageLocalItem packageLocalData;
    //获取物品的静态数据
    private packageItems packageTableItem;
    //获取父物体
    private PackagePanel uiParent;

    private void Awake()
    {
        InitUI();
        InitClick();
    }

    private void InitUI()//注册UI组件
    {
        UISelected = transform.Find("Selected");
        UIDeleted = transform.Find("Deleted");
        UITop = transform.Find("Top");
        UIicon = transform.Find("Top/icon");
        UInew = transform.Find("Top/new");
        UInum = transform.Find("Bottom/bg/num");
        UIBottom = transform.Find("Bottom");
    }

    private void InitClick()//初始化所有点击事件
    {
        GetComponent<Button>().onClick.AddListener(OnClickThis);
    }

    private void OnClickThis()
    {
        print("OnClickItem");
    }


    //刷新物品状态
    public void Refresh(PackageLocalItem packageLocalData, PackagePanel uiParent)
    { 
        //初始化整个组件
        this.packageLocalData = packageLocalData;
        this.packageTableItem = GameManager.Instance.GetPackageItemById(packageLocalData.id);
        this.uiParent = uiParent;
        //初始化UI内各组件
        //物品数量
        UInum.GetComponent<Text>().text = this.packageLocalData.num.ToString();//道具数量
        //是否新物品
        UInew.gameObject.SetActive(this.packageLocalData.isNew);
        //图片
        Texture2D t = (Texture2D)Resources.Load(this.packageTableItem.imagePath);
        Sprite temp = Sprite.Create(t, new Rect(0, 0, t.width, t.height), new Vector2(0, 0));
        UIicon.GetComponent<Image>().sprite = temp;
    }
}
