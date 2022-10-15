using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class FoodPageController : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        SetActive(false);
        
        HomeManager.Instance.CurrentHomeState
            .DistinctUntilChanged()
            .Where(x => x == HomeState.OpenFoodPage)
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
