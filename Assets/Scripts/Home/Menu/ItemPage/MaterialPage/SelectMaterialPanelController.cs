using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class SelectMaterialPanelController : MonoBehaviour
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
            .Where(x => x == HomeState.SelectMaterial)
            .Subscribe(_ => SetActive(true));

        HomeManager.Instance.CurrentHomeState
            .DistinctUntilChanged()
            .Where(x => x == HomeState.OpenMaterialPage)
            .Subscribe(_ => SetActive(false));
    }

    void SetActive(bool active)
    {
        if(active)
        {
            itemEffect.text = MyItemData.Instance.myMaterialArray[HomeManager.Instance.SelectedItemIndex].GetComponent<ItemController>().itemDetail;
            itemImage.sprite = MyItemData.Instance.myMaterialArray[HomeManager.Instance.SelectedItemIndex].GetComponent<SpriteRenderer>().sprite;
        }
        this.gameObject.SetActive(active);
    }

    //アイテム使用画面の「戻る」ボタン
    //アイテムページに戻る
    public void OnClickBackButton()
    {
        HomeManager.Instance.SetHomeState(HomeState.OpenMaterialPage);
    }

    //素材は使えない
    /*public void OnClickUseButton()
    {
        HomeManager.Instance.SetHomeState(HomeState.UseClothing);
    }*/
}
