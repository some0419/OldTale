using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;

namespace Battle
{
    public class SelectFoodPanelController : MonoBehaviour
{
    //アイテム選択ウィンドウ上のボタン操作(使うor戻る)

    Text itemEffect;
    Image itemImage;

    void Start()
    {
        itemEffect = transform.Find("ItemEffectText").GetComponent<Text>();
        itemImage = transform.Find("ItemImage").gameObject.GetComponent<Image>();

        SetActive(false);

        BattleManager.Instance.CurrentBattleState
            .DistinctUntilChanged()
            .Where(x => x == BattleState.SelectFood)
            .Subscribe(_ => SetActive(true));

        BattleManager.Instance.CurrentBattleState
            .DistinctUntilChanged()
            .Where(x => x != BattleState.SelectFood)
            .Subscribe(_ => SetActive(false));
    }

    void SetActive(bool active)
    {
        if(active)
        {
            itemEffect.text = MyItemData.Instance.myFoodArray[BattleManager.Instance.SelectedItemIndex].GetComponent<ItemController>().itemEffect;
            itemImage.sprite = MyItemData.Instance.myFoodArray[BattleManager.Instance.SelectedItemIndex].GetComponent<SpriteRenderer>().sprite;
        }
        this.gameObject.SetActive(active);
    }

    //アイテム使用画面の「やめる」ボタン
    //アイテムページに戻る
    public void OnClickBackButton()
    {
        BattleManager.Instance.SetBattleState(BattleState.OpenBag);
    }

    //アイテム使用画面の「使う」ボタン
    //使用対象キャラを選択するため、パーティー画面に移動
    public void OnClickUseButton()
    {
        BattleManager.Instance.SetBattleState(BattleState.SelectPlayerToUseFood);
    }
}
}

