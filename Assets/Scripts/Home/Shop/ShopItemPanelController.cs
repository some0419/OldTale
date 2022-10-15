using System.Collections.Generic;
using UnityEngine;
using UniRx;
//using DG.Tweening;
using UnityEngine.UI;

public class ShopItemPanelController : MonoBehaviour
{
    Text itemName;
    Text itemPrice;
    //Text itemEffect;
    Text itemDetail;
    GameObject soldOutPanel;
    Image itemImage;

    // Start is called before the first frame update
    void Start()
    {
        itemName = transform.Find("ItemNameText").gameObject.GetComponent<Text>();
        itemPrice = transform.Find("ItemPriceText").gameObject.GetComponent<Text>();
        //itemEffect = transform.Find("ItemEffectText").gameObject.GetComponent<Text>();
        itemDetail = transform.Find("ItemDetailText").gameObject.GetComponent<Text>();
        itemImage = transform.Find("ItemImage").gameObject.GetComponent<Image>();

        soldOutPanel = transform.Find("SoldOutPanel").gameObject;

        SetActive(false);

        ShopManager.Instance.CurrentShopState
            .DistinctUntilChanged()
            .Where(x => x == ShopState.SelectFoodShopItem || x == ShopState.SelectWeaponShopItem || x == ShopState.SelectClothingShopItem)
            .Subscribe(_ => SetActive(true));

        ShopManager.Instance.CurrentShopState
            .DistinctUntilChanged()
            .Where(x => x == ShopState.None)
            .Subscribe(_ => SetActive(false));

        ShopManager.Instance.SelectedItemIndex
            .DistinctUntilChanged()
            .Subscribe(_ => GenerateItemPanel());
        
        ShopManager.Instance.CurrentShopState
            .DistinctUntilChanged()
            .Where(x => x == ShopState.SelectFoodShopItem || x == ShopState.SelectWeaponShopItem || x == ShopState.SelectClothingShopItem)
            .Subscribe(_ => GenerateItemPanel());
    }

    void SetActive(bool active)
    {
        this.gameObject.SetActive(active);
    }

    public void OnClickBuyButton()
    {
        if(ShopManager.Instance.CurrentShopState.Value == ShopState.SelectFoodShopItem)
        {
            if(FoodShopData.Instance.soldList[ShopManager.Instance.SelectedItemIndex.Value].GetComponent<ItemController>().Price <= MyItemData.Instance.MyMoney.Value)
            {
                if(MyItemData.Instance.PutItem(FoodShopData.Instance.soldList[ShopManager.Instance.SelectedItemIndex.Value]))
                {
                    MyItemData.Instance.MyMoney.Value -= FoodShopData.Instance.soldList[ShopManager.Instance.SelectedItemIndex.Value].GetComponent<ItemController>().Price;
                    FoodShopData.Instance.soldList[ShopManager.Instance.SelectedItemIndex.Value] = null;
                    ShopManager.Instance.SetShopState(ShopState.ShowFoodShopList);
                    GenerateItemPanel();
                }
                else
                {
                    Debug.Log("バッグがいっぱい");
                }
            }
            else
            {
                Debug.Log("お金が足りない");
            }
        }
        else if(ShopManager.Instance.CurrentShopState.Value == ShopState.SelectWeaponShopItem)
        {
            if(WeaponShopData.Instance.soldList[ShopManager.Instance.SelectedItemIndex.Value].GetComponent<ItemController>().Price <= MyItemData.Instance.MyMoney.Value)
            {
                if(MyItemData.Instance.PutItem(WeaponShopData.Instance.soldList[ShopManager.Instance.SelectedItemIndex.Value]))
                {
                    MyItemData.Instance.MyMoney.Value -= WeaponShopData.Instance.soldList[ShopManager.Instance.SelectedItemIndex.Value].GetComponent<ItemController>().Price;
                    WeaponShopData.Instance.soldList[ShopManager.Instance.SelectedItemIndex.Value] = null;
                    ShopManager.Instance.SetShopState(ShopState.ShowWeaponShopList);
                    GenerateItemPanel();
                }
                else
                {
                    Debug.Log("バッグがいっぱい");
                }
            }
            else
            {
                Debug.Log("お金が足りない");
            }
        }
        else if(ShopManager.Instance.CurrentShopState.Value == ShopState.SelectClothingShopItem)
        {
            if(ClothingShopData.Instance.soldList[ShopManager.Instance.SelectedItemIndex.Value].GetComponent<ItemController>().Price <= MyItemData.Instance.MyMoney.Value)
            {
                if(MyItemData.Instance.PutItem(ClothingShopData.Instance.soldList[ShopManager.Instance.SelectedItemIndex.Value]))
                {
                    MyItemData.Instance.MyMoney.Value -= ClothingShopData.Instance.soldList[ShopManager.Instance.SelectedItemIndex.Value].GetComponent<ItemController>().Price;
                    ClothingShopData.Instance.soldList[ShopManager.Instance.SelectedItemIndex.Value] = null;
                    ShopManager.Instance.SetShopState(ShopState.ShowClothingShopList);
                    GenerateItemPanel();
                }
                else
                {
                    Debug.Log("バッグがいっぱい");
                }
            }
            else
            {
                Debug.Log("お金が足りない");
            }
        }
        
        

    }

    //選択中のアイテムの詳細を表示
    void GenerateItemPanel()
    {
        //食べ物屋にいるときの処理
        if(ShopManager.Instance.CurrentShopState.Value == ShopState.SelectFoodShopItem)
        {
            //売り切れてなければ（選択中の番号に応じた配列の要素が空でなければ）
            if(FoodShopData.Instance.soldList[ShopManager.Instance.SelectedItemIndex.Value] != null)
            {
                soldOutPanel.SetActive(false);
                itemName.text = FoodShopData.Instance.soldList[ShopManager.Instance.SelectedItemIndex.Value].GetComponent<ItemController>().ItemName;
                itemPrice.text = FoodShopData.Instance.soldList[ShopManager.Instance.SelectedItemIndex.Value].GetComponent<ItemController>().Price.ToString() + "円";
                itemDetail.text = FoodShopData.Instance.soldList[ShopManager.Instance.SelectedItemIndex.Value].GetComponent<ItemController>().itemDetail;
                itemImage.sprite = FoodShopData.Instance.soldList[ShopManager.Instance.SelectedItemIndex.Value].GetComponent<SpriteRenderer>().sprite;
            }
            else
            {
                soldOutPanel.SetActive(true);
            }
        }
        //武器屋にいるときの処理
        else if(ShopManager.Instance.CurrentShopState.Value == ShopState.SelectWeaponShopItem)
        {
            //売り切れてなければ（選択中の番号に応じた配列の要素が空でなければ）
            if(WeaponShopData.Instance.soldList[ShopManager.Instance.SelectedItemIndex.Value] != null)
            {
                soldOutPanel.SetActive(false);
                itemName.text = WeaponShopData.Instance.soldList[ShopManager.Instance.SelectedItemIndex.Value].GetComponent<ItemController>().ItemName;
                itemPrice.text = WeaponShopData.Instance.soldList[ShopManager.Instance.SelectedItemIndex.Value].GetComponent<ItemController>().Price.ToString() + "円";
                itemDetail.text = WeaponShopData.Instance.soldList[ShopManager.Instance.SelectedItemIndex.Value].GetComponent<ItemController>().itemDetail;
                itemImage.sprite = WeaponShopData.Instance.soldList[ShopManager.Instance.SelectedItemIndex.Value].GetComponent<SpriteRenderer>().sprite;
            }
            else
            {
                soldOutPanel.SetActive(true);
            }
        }
        //服屋にいるときの処理
        else if(ShopManager.Instance.CurrentShopState.Value == ShopState.SelectClothingShopItem)
        {
            //売り切れてなければ（選択中の番号に応じた配列の要素が空でなければ）
            if(ClothingShopData.Instance.soldList[ShopManager.Instance.SelectedItemIndex.Value] != null)
            {
                soldOutPanel.SetActive(false);
                itemName.text = ClothingShopData.Instance.soldList[ShopManager.Instance.SelectedItemIndex.Value].GetComponent<ItemController>().ItemName;
                itemPrice.text = ClothingShopData.Instance.soldList[ShopManager.Instance.SelectedItemIndex.Value].GetComponent<ItemController>().Price.ToString() + "円";
                itemDetail.text = ClothingShopData.Instance.soldList[ShopManager.Instance.SelectedItemIndex.Value].GetComponent<ItemController>().itemDetail;
                itemImage.sprite = ClothingShopData.Instance.soldList[ShopManager.Instance.SelectedItemIndex.Value].GetComponent<SpriteRenderer>().sprite;
            }
            else
            {
                soldOutPanel.SetActive(true);
            }
        }
    }
}
