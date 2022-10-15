using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpTextCont : MonoBehaviour
{
    //display hp by text 
    public Slider slider;
    Text hpText;
    // Start is called before the first frame update
    void Start()
    {
        hpText = GetComponent<Text>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        hpText.text = $"{slider.value}/{slider.maxValue}";
    }
}
