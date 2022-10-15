using System.Collections.Generic;
using UnityEngine;

public class WeaponShopData : SingletonMonoBehaviour<WeaponShopData>
{
    [SerializeField]
    GameObject[] allItemList = new GameObject[10];

    [SerializeField]
    public GameObject[] soldList = new GameObject[5];

    // Start is called before the first frame update
    void Awake()
    {
        GenerateSoldList();
    }

    void GenerateSoldList()
    {
        for(int i = 0; i < soldList.Length; i++)
        {
            soldList[i] = allItemList[Random.Range(0, allItemList.Length)];
        }
    }
}
