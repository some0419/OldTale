using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;

namespace Battle
{
    public class FoodButtonController : MonoBehaviour
{
    [SerializeField]
    int buttonNumber = 0;

    Image image;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();

        BattleManager.Instance.CurrentBattleState
            .DistinctUntilChanged()
            .Where(x => x == BattleState.OpenBag)
            .Subscribe(_ => GenerateButton());
    }

    public void OnClickButton()
    {
        SelectItem();
    }

    //メニューボタン押下時、リスト更新
    //メニューボタン押下時のOnClick
    void GenerateButton()
    {
        ResetButton();
        if(MyItemData.Instance.myFoodArray[buttonNumber] != null)
        {
            transform.Find("ItemName").GetComponent<Text>().text = MyItemData.Instance.myFoodArray[buttonNumber].GetComponent<ItemController>().ItemName;
            transform.Find("Rarity").GetComponent<Text>().text = MyItemData.Instance.myFoodArray[buttonNumber].GetComponent<ItemController>().Rarity.ToString();
            image.sprite = MyItemData.Instance.myFoodArray[buttonNumber].GetComponent<SpriteRenderer>().sprite;
        }
    }

    void ResetButton()
    {
        transform.Find("ItemName").GetComponent<Text>().text = "";
        transform.Find("Rarity").GetComponent<Text>().text = "";
        image.sprite = null;
    }

    //バッグ内のアイテムを選択したときの処理
    void SelectItem()
    {
        //選択したアイテムボックスが空でなければ
        if(MyItemData.Instance.myFoodArray[buttonNumber] != null)
        {
            BattleManager.Instance.SelectedItemIndex = buttonNumber;
            BattleManager.Instance.SetBattleState(BattleState.SelectFood);
        }
    }
}
}

