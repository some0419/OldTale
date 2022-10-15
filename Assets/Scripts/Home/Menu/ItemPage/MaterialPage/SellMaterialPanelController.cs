using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class SellMaterialPanelController : MonoBehaviour
{
    Text itemEffect;
    Image itemImage;

    void Start()
    {
        itemEffect = transform.Find("ItemEffectText").GetComponent<Text>();
        itemImage = transform.Find("ItemImage").gameObject.GetComponent<Image>();
        
        SetActive(false);

        ShopManager.Instance.CurrentShopState
            .DistinctUntilChanged()
            .Where(x => x == ShopState.SelectSellMaterial)
            .Subscribe(_ => SetActive(true));

        ShopManager.Instance.CurrentShopState
            .DistinctUntilChanged()
            .Where(x => x == ShopState.SellMaterial)
            .Subscribe(_ => SetActive(false));
    }

    void SetActive(bool active)
    {
        if(active)
        {
            itemEffect.text = MyItemData.Instance.myMaterialArray[HomeManager.Instance.SelectedItemIndex].GetComponent<ItemController>().SellingPrice.ToString() + "円";
            itemImage.sprite = MyItemData.Instance.myMaterialArray[HomeManager.Instance.SelectedItemIndex].GetComponent<SpriteRenderer>().sprite;
        }
        this.gameObject.SetActive(active);
    }

    //アイテム使用画面の「戻る」ボタン
    //アイテムページに戻る
    public void OnClickBackButton()
    {
        ShopManager.Instance.SetShopState(ShopState.SellMaterial);
    }

    //素材を売る
    public void OnClickUseButton()
    {
        MyItemData.Instance.MyMoney.Value += MyItemData.Instance.myMaterialArray[HomeManager.Instance.SelectedItemIndex].GetComponent<ItemController>().SellingPrice;
        MyItemData.Instance.myMaterialArray[HomeManager.Instance.SelectedItemIndex] = null;
        ShopManager.Instance.SetShopState(ShopState.SellMaterial);
    }
}
