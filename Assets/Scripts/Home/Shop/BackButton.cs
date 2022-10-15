using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class BackButton : MonoBehaviour
{
    void Start()
    {
        ShopManager.Instance.CurrentShopState
            .DistinctUntilChanged()
            .Where(x => x == ShopState.SellMaterial)
            .Subscribe(_ => SetActive(true));

        HomeManager.Instance.CurrentHomeState
            .DistinctUntilChanged()
            .Where(x => x != HomeState.Shopping)
            .Subscribe(_ => SetActive(false));
    }

    void SetActive(bool active)
    {
        this.gameObject.SetActive(active);
    }

    //ボタン押下
    //素材屋から出る
    public void OnClickButton()
    {
        if(ShopManager.Instance.CurrentShopState.Value == ShopState.SellMaterial)
        {
            HomeManager.Instance.SetHomeState(HomeState.Encounter);
            ShopManager.Instance.SetShopState(ShopState.None);

        }
        else if(ShopManager.Instance.CurrentShopState.Value == ShopState.SelectSellMaterial)
        {
            ShopManager.Instance.SetShopState(ShopState.SellMaterial);
        }
    }
}

