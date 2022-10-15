using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class ShopListPanelController : MonoBehaviour
{
    void Start()
    {
        SetActive(false);

        ShopManager.Instance.CurrentShopState
            .DistinctUntilChanged()
            .Where(x => x == ShopState.ShowFoodShopList || x == ShopState.ShowWeaponShopList || x == ShopState.ShowClothingShopList)
            .Subscribe(_ => SetActive(true));

        ShopManager.Instance.CurrentShopState
            .DistinctUntilChanged()
            .Where(x => x == ShopState.None)
            .Subscribe(_ => SetActive(false));
    }

    void SetActive(bool active)
    {
        this.gameObject.SetActive(active);
    }

    //戻る（店を出る）ボタン
    public void OnClickBackButton()
    {
        ShopManager.Instance.SetShopState(ShopState.None);
        HomeManager.Instance.SetHomeState(HomeState.Encounter);
    }
}
