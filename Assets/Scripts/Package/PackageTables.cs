using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CreateItemData/PackageData", fileName = "PackageTable")]
//创建物品配置文件
public class PackageTables : ScriptableObject//创建配置文件
{
    public List<packageItems> DataList = new List<packageItems>();
    //使用一个列表容纳所有物品
}

[Serializable]//序列化，可编辑
public class packageItems
{
    public int id;//唯一id
    public int type;//类型，1=普通物品，2=服装物品
    public string name;//物品名称
    public string description;//物品描述
    public string imagePath;//图片路径
    public int num;//物品数量

}
