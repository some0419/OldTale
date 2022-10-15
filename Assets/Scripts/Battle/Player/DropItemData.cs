using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItemData : MonoBehaviour
{
    public GameObject[] dropItemArray = new GameObject[3];

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public GameObject DropItem()
    {
        return dropItemArray[Random.Range(0, dropItemArray.Length)];
    }
}
