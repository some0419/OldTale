using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class ShopListButtonController : MonoBehaviour
{
    [SerializeField]
    int buttonNumber = 0;

    Text itemName;
    Text itemPrice;

    // Start is called before the first frame update
    void Start()
    {
        itemName = transform.Find("ItemNameText").gameObject.GetComponent<Text>();
        itemPrice = transform.Find("ItemPriceText").gameObject.GetComponent<Text>();

        ShopManager.Instance.CurrentShopState
            .DistinctUntilChanged()
            .Where(x => x == ShopState.ShowFoodShopList)
            .Subscribe(_ => GenerateFoodButton());

        ShopManager.Instance.CurrentShopState
            .DistinctUntilChanged()
            .Where(x => x == ShopState.ShowWeaponShopList)
            .Subscribe(_ => GenerateWeaponButton());

        ShopManager.Instance.CurrentShopState
            .DistinctUntilChanged()
            .Where(x => x == ShopState.ShowClothingShopList)
            .Subscribe(_ => GenerateClothingButton());
    }

    //ボタン押下時の処理
    public void OnClickButton()
    {
        //食べ物屋にいるときの処理
        if(ShopManager.Instance.CurrentShopState.Value == ShopState.ShowFoodShopList || ShopManager.Instance.CurrentShopState.Value == ShopState.SelectFoodShopItem)
        {
            ShopManager.Instance.SelectedItemIndex.Value = buttonNumber;
            ShopManager.Instance.SetShopState(ShopState.SelectFoodShopItem);
        }
        //武器屋にいるときの処理
        else if(ShopManager.Instance.CurrentShopState.Value == ShopState.ShowWeaponShopList || ShopManager.Instance.CurrentShopState.Value == ShopState.SelectWeaponShopItem)
        {
            ShopManager.Instance.SelectedItemIndex.Value = buttonNumber;
            ShopManager.Instance.SetShopState(ShopState.SelectWeaponShopItem);
        }
        //服屋にいるときの処理
        else if(ShopManager.Instance.CurrentShopState.Value == ShopState.ShowClothingShopList || ShopManager.Instance.CurrentShopState.Value == ShopState.SelectClothingShopItem)
        {
            ShopManager.Instance.SelectedItemIndex.Value = buttonNumber;
            ShopManager.Instance.SetShopState(ShopState.SelectClothingShopItem);
        }
    }

    void GenerateFoodButton()
    {
        if(FoodShopData.Instance.soldList[buttonNumber] != null)
        {
            itemName.text = FoodShopData.Instance.soldList[buttonNumber].GetComponent<ItemController>().ItemName;
            itemPrice.text = FoodShopData.Instance.soldList[buttonNumber].GetComponent<ItemController>().Price.ToString();
        }
        else
        {
            itemPrice.text = "売り切れ";
        }
    }

    void GenerateWeaponButton()
    {
        if(WeaponShopData.Instance.soldList[buttonNumber] != null)
        {
            itemName.text = WeaponShopData.Instance.soldList[buttonNumber].GetComponent<ItemController>().ItemName;
            itemPrice.text = WeaponShopData.Instance.soldList[buttonNumber].GetComponent<ItemController>().Price.ToString();
        }
        else
        {
            itemPrice.text = "売り切れ";
        }
    }

    void GenerateClothingButton()
    {
        if(ClothingShopData.Instance.soldList[buttonNumber] != null)
        {
            itemName.text = ClothingShopData.Instance.soldList[buttonNumber].GetComponent<ItemController>().ItemName;
            itemPrice.text = ClothingShopData.Instance.soldList[buttonNumber].GetComponent<ItemController>().Price.ToString();
        }
        else
        {
            itemPrice.text = "売り切れ";
        }
    }
}
