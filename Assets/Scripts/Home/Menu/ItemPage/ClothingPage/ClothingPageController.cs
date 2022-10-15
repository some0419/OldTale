using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class ClothingPageController : MonoBehaviour
{
       void Start()
    {
        SetActive(false);

        HomeManager.Instance.CurrentHomeState
            .DistinctUntilChanged()
            .Where(x => x == HomeState.OpenClothingPage)
            .Subscribe(_ => SetActive(true));

        HomeManager.Instance.CurrentHomeState
            .DistinctUntilChanged()
            .Where(x => x == HomeState.OpenItemPage)
            .Subscribe(_ => SetActive(false));
    }

    void SetActive(bool active)
    {
        this.gameObject.SetActive(active);
    }
}
