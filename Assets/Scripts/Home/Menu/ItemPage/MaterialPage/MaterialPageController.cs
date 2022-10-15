using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class MaterialPageController : MonoBehaviour
{
    void Start()
    {
        SetActive(false);

        HomeManager.Instance.CurrentHomeState
            .DistinctUntilChanged()
            .Where(x => x == HomeState.OpenMaterialPage)
            .Subscribe(_ => SetActive(true));
        
        ShopManager.Instance.CurrentShopState
            .DistinctUntilChanged()
            .Where(x => x == ShopState.SellMaterial)
            .Subscribe(_ => SetActive(true));

        HomeManager.Instance.CurrentHomeState
            .DistinctUntilChanged()
            .Where(x => x == HomeState.OpenItemPage)
            .Subscribe(_ => SetActive(false));

        ShopManager.Instance.CurrentShopState
            .DistinctUntilChanged()
            .Where(x => x == ShopState.None)
            .Subscribe(_ => SetActive(false));

    }

    void SetActive(bool active)
    {
        this.gameObject.SetActive(active);
    }
}
