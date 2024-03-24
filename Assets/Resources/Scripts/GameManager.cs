using System.Collections;
using System.Collections.Generic;
//using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{

    private static GameManager _instance;//���õ���ģʽ
    GameObject player;//���
    private PackageTables packageTables;//�������ݱ�

    public static GameManager Instance
    {
        get
        {
            return _instance;//�������
        }
    }

    private void Awake()
    {
        _instance = this;
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        //��ȡ���
        player = GameObject.FindGameObjectsWithTag("Player")[0];

        //�򿪻�ȡ����
        //UIManager.Instance.OpenPanel(UIConst.PackagePanel);
    }
    public void OnStartGameHandler(string sceneName)
    {
        //�������ķ���
        SceneManager.LoadScene(sceneName);
    }

    public void Interaction()//�����׼���볡���е����彻��ʱʹ��
    {
        player.GetComponent<PlayerController>().Attack();//����ƴ���ˣ��ǵø�
    }

    public PackageTables GetPackageTables()//��ȡ���ݱ�
    {
        if (packageTables == null)
        {
            //�������ݱ�
            packageTables = Resources.Load<PackageTables>("TableData/PackageTable");
        }
        return packageTables;
    }

    public List<PackageLocalItem> GetPackageLocalData()
    {
        return PackageLocalData.Instance.LoadPackage();//����List<PackageLocalItem>
    }
    public List<PackageLocalItem> GetPackageSortLocalData()//���ذ���id��С�����������Ʒ�б�
    {
        List<PackageLocalItem> SortLocalData = GetPackageLocalData();
        PackageItemComparer packageItemComparer = new PackageItemComparer();
        PackageLocalItem temp;
        //����List<PackageLocalItem>,����id����
        int ListLength = SortLocalData.Count;
        for (int i = 0; i <= ListLength; i++)
        {
            for (int j = 1; j < ListLength; j++)
            {
                if (SortLocalData[j].id > SortLocalData[j - 1].id)//ǰ��>����
                {
                    //����
                    temp = SortLocalData[j];
                    SortLocalData[j] = SortLocalData[j - 1];
                    SortLocalData[j - 1] = temp;
                }
            }

        }
        ////ɾ��id��ͬ�ģ�id��ͬ��ֻ����һ�����ѵ�����������������ȥ
        //for (int i = 0; i < ListLength; i++)
        //{
        //    Debug.Log(SortLocalData[i].id);
        //    Debug.Log(GetPackageLocalData()[i].uid);
        //}
        return SortLocalData;
    }

    //����uid��ȡ��Ʒ��Ҫ�ҵ���PackageLocalData���PackageLocalItem
    public PackageLocalItem GetPackageItemByUId(string uid)
    {
        //ͨ��packageItems��ɵ��б��ҵ�ÿһ��packageItems
        List<PackageLocalItem> packageDataList = GetPackageLocalData();
        foreach (PackageLocalItem packageItem in packageDataList)
        {
            if (packageItem.uid == uid)
            {
                return packageItem;
            }
        }
        return null;
    }

    //����id�����Ʒ��Ҫ�ҵ���PackageTables���packageItems
    public packageItems GetPackageItemById(int id)
    {
        List<packageItems> packageItemList = GetPackageTables().DataList;
        foreach (packageItems item in packageItemList)
        {
            if (item.id == id)
            {
                return item;
            }
        }
        return null;
    }


}


//������Ʒ����(����id�Ӵ�С����
public class PackageItemComparer : IComparer<PackageLocalItem>
{
    public int Compare(PackageLocalItem itemA, PackageLocalItem itemB)
    {
        packageItems x = GameManager.Instance.GetPackageItemById(itemA.id);
        packageItems y = GameManager.Instance.GetPackageItemById(itemB.id);
        //����id��С��������
        int idComparison = y.id.CompareTo(x.id);
        return idComparison;
    }
}
