using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackageLocalData//存取本地物品信息
{
    private static PackageLocalData instance;

    public static PackageLocalData Instance//如果没有数据，则新建数据
    {
        get
        {
            if (instance == null)
            {
                instance = new PackageLocalData();
            }
            return instance;
        }
    }
    public List<PackageLocalItem> items;//缓存文件
    public void SavePackageData()//存储
    { 
        string inventoryJson = JsonUtility.ToJson(this);//表格信息序列化为字符串
        PlayerPrefs.SetString("PackageLocalItem", inventoryJson);
        PlayerPrefs.Save();
    }
    public List<PackageLocalItem> LoadPackage()
    {
        if (items != null)//缓存文件已经存在，则代表已经读取过数据，直接返回值即可
        {
            return items;
        }
        if (PlayerPrefs.HasKey("PackageLocalItem"))
        {
            string inventoryJson = PlayerPrefs.GetString("PackageLocalItem");
            PackageLocalData packageLocalData = JsonUtility.FromJson<PackageLocalData>(inventoryJson);//把字符串反序列化为数据
            items = packageLocalData.items;
            return items;
        }
        else {
            items = new List<PackageLocalItem>();
            return items;
        }

    }
}

[System.Serializable]
public class PackageLocalItem
{
    //本地需要的玩家数据（物品数据）：id，数量，是否是新获得的
    public string uid;
    public int id;
    public int num;
    public bool isNew;
    public override string ToString()
    {
        return string.Format("【id】={0}，【num】={1}", id, num);
    }
}
