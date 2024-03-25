using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

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

    //��ȡ��Ʒ�Ķ�̬����
    private PackageLocalItem packageLocalData;
    //��ȡ��Ʒ�ľ�̬����
    private packageItems packageTableItem;
    //��ȡ������
    private PackagePanel uiParent;

    private void Awake()
    {
        InitUI();
        InitClick();
    }

    private void InitUI()//ע��UI���
    {
        UISelected = transform.Find("Selected");
        UIDeleted = transform.Find("Deleted");
        UITop = transform.Find("Top");
        UIicon = transform.Find("Top/icon");
        UInew = transform.Find("Top/new");
        UInum = transform.Find("Bottom/bg/num");
        UIBottom = transform.Find("Bottom");
    }

    private void InitClick()//��ʼ�����е���¼�
    {
        GetComponent<Button>().onClick.AddListener(OnClickThis);
    }

    private void OnClickThis()
    {
        print("OnClickItem");
    }


    //ˢ����Ʒ״̬
    public void Refresh(PackageLocalItem packageLocalData, PackagePanel uiParent)
    { 
        //��ʼ���������
        this.packageLocalData = packageLocalData;
        this.packageTableItem = GameManager.Instance.GetPackageItemById(packageLocalData.id);
        this.uiParent = uiParent;
        //��ʼ��UI�ڸ����
        //��Ʒ����
        UInum.GetComponent<Text>().text = this.packageLocalData.num.ToString();//��������
        //�Ƿ�����Ʒ
        UInew.gameObject.SetActive(this.packageLocalData.isNew);
        //ͼƬ
        Texture2D t = (Texture2D)Resources.Load(this.packageTableItem.imagePath);
        Sprite temp = Sprite.Create(t, new Rect(0, 0, t.width, t.height), new Vector2(0, 0));
        UIicon.GetComponent<Image>().sprite = temp;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log(">>>>>>  ���"+eventData.ToString());
        //�ж��ظ����
        if (this.uiParent.chooseUID == this.packageLocalData.uid)
        {
            return;
        }
        else 
        {
            //��ֵ
            this.uiParent.chooseUID = this.packageLocalData.uid;
        }
    }
}
