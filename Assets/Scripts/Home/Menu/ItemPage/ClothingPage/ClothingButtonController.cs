using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class ClothingButtonController : MonoBehaviour
{
    [SerializeField]
    int buttonNumber = 0;

    Image image;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();

        HomeManager.Instance.CurrentHomeState
            .DistinctUntilChanged()
            .Where(x => x == HomeState.OpenClothingPage)
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
        if(MyItemData.Instance.myClothingArray[buttonNumber] != null)
        {
            transform.Find("ItemName").GetComponent<Text>().text = MyItemData.Instance.myClothingArray[buttonNumber].GetComponent<ItemController>().ItemName;
            transform.Find("Rarity").GetComponent<Text>().text = MyItemData.Instance.myClothingArray[buttonNumber].GetComponent<ItemController>().Rarity.ToString();
            image.sprite = MyItemData.Instance.myClothingArray[buttonNumber].GetComponent<SpriteRenderer>().sprite;
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
        if(MyItemData.Instance.myClothingArray[buttonNumber] != null)
        {
            HomeManager.Instance.SelectedItemIndex = buttonNumber;
            HomeManager.Instance.SetHomeState(HomeState.SelectClothing);
        }
    }
}
