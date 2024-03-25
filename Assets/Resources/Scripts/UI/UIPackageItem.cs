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

    //��ȡ��Ʒ�Ķ�̬����
    private PackageLocalItem packageLocalData;
    //��ȡ��Ʒ�ľ�̬����
    private packageItems packageTableItem;
    //��ȡ������
    private PackagePanel uiParent;
    //��ȡ�������


    //�����Ƿ�ѡ��
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

    private void InitUI()//ע��UI���
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
        //�Ƿ�ѡ��
        UISelected.gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log(">>>>>>  ���"+eventData.ToString());
        //�ж��ظ�������ظ����ֻ������
        if (this.uiParent.chooseUID == this.packageLocalData.uid)
        {
            UISelectAni.gameObject.SetActive(true);
            UISelectAni.GetComponent<Animator>().SetTrigger("Select");
            return;
        }

        //��ֵ
        this.uiParent.chooseUID = this.packageLocalData.uid;
        UISelectAni.gameObject.SetActive(true);
        UISelectAni.GetComponent<Animator>().SetTrigger("Select");
        this.UISelected.gameObject.SetActive(true);
        
        //ˢ���������е�cell��ˢΪδѡ��״̬

        
        

    }
}
