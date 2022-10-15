using System.Collections.Generic;
using UnityEngine;

public class FoodShopData : SingletonMonoBehaviour<FoodShopData>
{
    //すべての在庫
    //店頭に並び得るアイテム
    [SerializeField]
    GameObject[] allItemList = new GameObject[10];

    //実際に現在売られているアイテム
    [SerializeField]
    public GameObject[] soldList = new GameObject[5];

    void Awake()
    {
        GenerateSoldList();
    }

    //店頭に並ぶアイテムの決定
    void GenerateSoldList()
    {
        for(int i = 0; i < soldList.Length; i++)
        {
            soldList[i] = allItemList[Random.Range(0, allItemList.Length)];
        }
    }
}
