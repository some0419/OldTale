using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpGaugeController : MonoBehaviour
{
    Slider gauge;

    // Start is called before the first frame update
    void Awake()
    {
        gauge = GetComponent<Slider>();
    }

    public void SetCurrentValue(CharacterStatus playerStatus)
    {
        gauge.value = playerStatus.CurrentHp.Value;
    }

    public void SetMaxValue(CharacterStatus playerStatus)
    {
        gauge.maxValue = playerStatus.MaxHp;
    }
}
