using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using NodeCanvas.Tasks.Actions;

public class UIPackageItem : MonoBehaviour,IPointerClickHandler
{
    private Transform UISelected;
    private Transform UIDeleted;
    private Transform UITop;
    private Transform UIicon;
    private Transform UIbg;
    private Transform UInew;
    private Transform UIBottom;
    private Transform UInum;
    private Transform UISelectAni;
    private Transform UIIsSelectedAni;

    //获取物品的动态数据
    private PackageLocalItem packageLocalData;
    //获取物品的静态数据
    private packageItems packageTableItem;
    //获取父物体
    private PackagePanel uiParent;
    //获取动画组件


    //定义是否选中
    public bool isSelected = false;


    private void Awake()
    {
        InitUI();
        InitClick();
    }

    private void Update()
    {
        if (this.uiParent.chooseUID == this.packageLocalData.uid)
        {
            isSelected = true;
        }
        else
        {
            isSelected = false;
        }
        this.UISelected.GetComponent<Animator>().SetBool("isSelected", isSelected);
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
        UISelectAni = transform.Find("SelectAnimation");

        UISelected.gameObject.SetActive(false);
        UISelectAni.gameObject.SetActive(false);
        UIDeleted.gameObject.SetActive(false);
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
        //是否被选中
        UISelected.gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log(">>>>>>  点击"+eventData.ToString());
        //判断重复点击，重复点击只播动画
        if (this.uiParent.chooseUID == this.packageLocalData.uid)
        {
            UISelectAni.gameObject.SetActive(true);
            UISelectAni.GetComponent<Animator>().SetTrigger("Select");
            return;
        }

        //赋值
        this.uiParent.chooseUID = this.packageLocalData.uid;
        UISelectAni.gameObject.SetActive(true);
        UISelectAni.GetComponent<Animator>().SetTrigger("Select");
        this.UISelected.gameObject.SetActive(true);
        
        //刷新其他所有的cell，刷为未选中状态

        
        

    }
}
