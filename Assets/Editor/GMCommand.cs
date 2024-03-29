//using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;


public class GMCommand
{
    [MenuItem("GMCommand/读取表格")]
    public static void ReadTable()
    {
        PackageTables packageTables = Resources.Load<PackageTables>("TableData/PackageTable");
        Debug.Log(packageTables);
        foreach (packageItems table in packageTables.DataList)
        {
            Debug.Log(string.Format("【id】：{0}，【name】：{1}", table.id, table.name));
        }
    }
    [MenuItem("GMCommand/创建背包测试数据")]
    public static void WritePackageTable()
    {
        //保存数据
        PackageLocalData.Instance.items = new List<PackageLocalItem>();
        for (int i = 0; i < 8; i++)
        {
            PackageLocalItem packageLocalItem = new PackageLocalItem()
            {
                uid = Guid.NewGuid().ToString(),
                id = i,
                num = i,
                isNew = 1 % 2 == 1
            };
            PackageLocalData.Instance.items.Add(packageLocalItem);
        }
        PackageLocalData.Instance.SavePackageData();
    }
    [MenuItem("GMCommand/读取背包测试数据")]
    public static void ReadPackageTable()
    {
        List<PackageLocalItem> readItems = PackageLocalData.Instance.LoadPackage();
        foreach (PackageLocalItem item in readItems)
        {
            Debug.Log(item);
        }
    }
    [MenuItem("GMCommand/打开背包界面")]
    public static void OpenPackagePanel()
    {
        UIManager.Instance.OpenPanel(UIConst.PackagePanel);
    }

    [MenuItem("GMCommand/测试功能")]
    public static void Test()
    {
        GameManager.Instance.GetPackageTables();
        PackageLocalData.Instance.LoadPackage();
    }
}
