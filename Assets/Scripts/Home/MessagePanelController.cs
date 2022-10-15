using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;

public class MessagePanelController : MonoBehaviour
{
    Text message;

    // Start is called before the first frame update
    void Start()
    {
        message = transform.GetChild(0).gameObject.GetComponent<Text>();

        SetActive(false);

        HomeManager.Instance.CurrentHomeState
            .DistinctUntilChanged()
            .Where(x => x == HomeState.Talking || x == HomeState.ToBattle || x == HomeState.ToFishing)
            .Subscribe();
    }

    void SetActive(bool active)
    {
        this.gameObject.SetActive(active);
    }
}
