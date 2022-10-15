using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FishingManager : MonoBehaviour
{
    [SerializeField]
    GameObject messageWindow;

    [SerializeField]
    Text messageText;

    [SerializeField]
    MyItemData myItemData;

    float time;

    bool isFishing = false;

    [SerializeField]
    GameObject player;

    int lineupCount;

    [SerializeField]
    GameObject[] lineupArray = new GameObject[10];

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKey(KeyCode.Space) && !isFishing)
        {
            isFishing = true;
            time += Time.deltaTime;

        }
    }

    void GetItem()
    {
        
    }
}
