using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//アイテムのタイプ一覧
public enum ItemType
{
    Food,
    Weapon,
    Clothing,
    Material,
}

public class ItemController : MonoBehaviour
{
    //全アイテム用ベースクラス
    
    //タイプ
    [SerializeField]
    ItemType type = ItemType.Food;
    
    //タイプのプロパティ
    public ItemType Type { get{return type;} }

    //名前
    [SerializeField]
    string itemName = "お名前";

    //名前のプロパティ
    public string ItemName { get{return itemName;} }

    //レア度
    public int Rarity { get; set; } = 1;

    //アイテムの説明
    [TextArea(1, 3)]public string itemDetail;

    //アイテムの効果
    [TextArea(1, 2)]public string itemEffect;

    //買値
    [SerializeField]
    int price = 100;

    //売値
    [SerializeField]
    int sellingPrice = 50;

    //ショップでの買値のプロパティ
    public int Price { get{return price;}}

    //ショップでの売値のプロパティ
    public int SellingPrice { get{return sellingPrice;}}

    //トレード店での価格
    public int Point { get; set; } = 20;

    //回復量、上昇攻撃力などの数値
    [SerializeField]
    public int amount = 10;

    //使用時のHp回復
    public void Effect(CharacterStatus target)
    {
        target.IncreaseCurrentHp(amount);
    }
}