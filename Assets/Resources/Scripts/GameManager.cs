using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private static GameManager _instance;//设置单例模式
    GameObject player;//玩家
    private PackageTables packageTables;//背包数据表

    public static GameManager Instance
    {
        get
        {
            return _instance;//方便调用
        }
    }

    private void Awake()
    {
        _instance = this;
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        //获取玩家
        player = GameObject.FindGameObjectsWithTag("Player")[0];

        //打开获取背包
        //UIManager.Instance.OpenPanel(UIConst.PackagePanel);
    }
    public void OnStartGameHandler(string sceneName)
    {
        //换场景的方法
        SceneManager.LoadScene(sceneName);
    }

    public void Interaction()//当玩家准备与场景中的物体交互时使用
    {
        player.GetComponent<PlayerController>().Attack();
    }

    public PackageTables GetPackageTables()//获取数据表
    {
        if (packageTables == null)
        {
            //加载数据表
            packageTables = Resources.Load<PackageTables>("TableData/PackageTable");
        }
        return packageTables;
    }

    public List<PackageLocalItem> GetPackageLocalData()
    {
        return PackageLocalData.Instance.LoadPackage();//返回List<PackageLocalItem>
    }
    public List<PackageLocalItem> GetPackageSortLocalData()//返回按照id从小到大排序的物品列表
    {
        List<PackageLocalItem> SortLocalData = GetPackageLocalData();
        PackageItemComparer packageItemComparer = new PackageItemComparer();
        PackageLocalItem temp;
        //返回List<PackageLocalItem>,按照id排序
        int ListLength = SortLocalData.Count;
        for (int i = 0; i <= ListLength; i++)
        {
            for (int j = 1; j < ListLength; j++)
            {
                if (SortLocalData[j].id > SortLocalData[j - 1].id)//前者>后者
                {
                    //交换
                    temp = SortLocalData[j];
                    SortLocalData[j] = SortLocalData[j - 1];
                    SortLocalData[j - 1] = temp;
                }
            }

        }
        ////删除id相同的，id相同的只保留一个（堆叠），并把数量叠上去
        //for (int i = 0; i < ListLength; i++)
        //{
        //    Debug.Log(SortLocalData[i].id);
        //    Debug.Log(GetPackageLocalData()[i].uid);
        //}
        return SortLocalData;
    }

    //根据uid获取物品，要找的是PackageLocalData里的PackageLocalItem
    public PackageLocalItem GetPackageItemByUId(string uid)
    {
        //通过packageItems组成的列表找到每一个packageItems
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

    //根据id获得物品，要找的是PackageTables里的packageItems
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


//背包物品排序(按照id从大到小排序）
public class PackageItemComparer : IComparer<PackageLocalItem>
{
    public int Compare(PackageLocalItem itemA, PackageLocalItem itemB)
    {
        packageItems x = GameManager.Instance.GetPackageItemById(itemA.id);
        packageItems y = GameManager.Instance.GetPackageItemById(itemB.id);
        //按照id从小到大排序
        int idComparison = y.id.CompareTo(x.id);
        return idComparison;
    }
}
