using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CreateCollectionData/CollectionData", fileName = "CollectionTable")]
//创建物品配置文件
public class CollectionTables : ScriptableObject//创建配置文件
{
    public List<Collections> DataList = new List<Collections>();
    //使用一个列表容纳所有物品
}

[Serializable]//序列化，可编辑
public class Collections
{
    public int id;//唯一id
    public string name;//藏品名称
    public string description;//藏品描述
    public string imagePath;//图片路径

}
