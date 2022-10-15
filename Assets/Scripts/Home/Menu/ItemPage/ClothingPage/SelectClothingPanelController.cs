using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class SelectClothingPanelController : MonoBehaviour
{
    Text itemEffect;
    Image itemImage;

    void Start()
    {
        itemEffect = transform.Find("ItemEffectText").GetComponent<Text>();
        itemImage = transform.Find("ItemImage").gameObject.GetComponent<Image>();
        
        SetActive(false);

        HomeManager.Instance.CurrentHomeState
            .DistinctUntilChanged()
            .Where(x => x == HomeState.SelectClothing)
            .Subscribe(_ => SetActive(true));

        HomeManager.Instance.CurrentHomeState
            .DistinctUntilChanged()
            .Where(x => x == HomeState.UseClothing || x == HomeState.OpenClothingPage)
            .Subscribe(_ => SetActive(false));
    }

    void SetActive(bool active)
    {
        if(active)
        {
            itemEffect.text = MyItemData.Instance.myClothingArray[HomeManager.Instance.SelectedItemIndex].GetComponent<ItemController>().itemEffect;
            itemImage.sprite = MyItemData.Instance.myClothingArray[HomeManager.Instance.SelectedItemIndex].GetComponent<SpriteRenderer>().sprite;
        }
        this.gameObject.SetActive(active);
    }

    //アイテム使用画面の「やめる」ボタン
    //アイテムページに戻る
    public void OnClickBackButton()
    {
        HomeManager.Instance.SetHomeState(HomeState.OpenClothingPage);
    }

    //アイテム使用画面の「使う」ボタン
    //使用対象キャラを選択するため、パーティー画面に移動
    public void OnClickUseButton()
    {
        HomeManager.Instance.SetHomeState(HomeState.UseClothing);
    }
}
