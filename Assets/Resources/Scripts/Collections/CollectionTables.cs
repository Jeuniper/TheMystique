using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CreateCollectionData/CollectionData", fileName = "CollectionTable")]
//������Ʒ�����ļ�
public class CollectionTables : ScriptableObject//���������ļ�
{
    public List<Collections> DataList = new List<Collections>();
    //ʹ��һ���б�����������Ʒ
}

[Serializable]//���л����ɱ༭
public class Collections
{
    public int id;//Ψһid
    public string name;//��Ʒ����
    public string description;//��Ʒ����
    public string imagePath;//ͼƬ·��

}
