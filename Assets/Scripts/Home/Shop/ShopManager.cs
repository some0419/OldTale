using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class ShopManager : SingletonMonoBehaviour<ShopManager>
{
    //現在のShopの状態
    [SerializeField]
    private ReactiveProperty<ShopState> shopState = new ReactiveProperty<ShopState>(ShopState.None);

    //状態を取得（プロパティ）
    public ReactiveProperty<ShopState> CurrentShopState
    {
        get
        {
            return shopState;
        }
    }

    //選択中アイテムの番号
    ReactiveProperty<int> selectedItemIndex = new ReactiveProperty<int>(0);

    //選択中アイテムのプロパティ
    public ReactiveProperty<int> SelectedItemIndex
    {
        get{return selectedItemIndex;}
        set{selectedItemIndex = value;}
    }

     void Awake()
    {
        base.Awake();
    }

    //状態を変更
    public void SetShopState(ShopState shopState)
    {
        this.shopState.Value = shopState;
    }
}

//Shopの状態一覧
public enum ShopState
{
    None,

    //食べ物屋の状態
    ShowFoodShopList,

    SelectFoodShopItem,

    //武器屋の状態
    ShowWeaponShopList,

    SelectWeaponShopItem,

    //服屋の状態
    ShowClothingShopList,

    SelectClothingShopItem,

    //素材屋の状態
    ShowMaterialShopList,

    SelectMaterialShopItem,

    SellMaterial,

    SelectSellMaterial,

}
