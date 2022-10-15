using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class MyItemData : SingletonMonoBehaviour<MyItemData>
{
    void Awake()
    {
        base.Awake();

        DontDestroyOnLoad(this.gameObject);
    }

    //バッグの情報

    //所持中のお金
    [SerializeField]
    ReactiveProperty<int> myMoney = new ReactiveProperty<int>(1000);

    //お金のプロパティ
    public ReactiveProperty<int> MyMoney
    {
        get{return myMoney;}
        set{myMoney = value;}
    }

    //所持中のポイント（開発中）
    public int myPoint = 10;

    //各バッグのサイズ
    public int bagSize_food = 20;

    public int bagSize_weapon = 20;

    public int bagSize_clothing = 20;

    public int bagSize_material = 20;

    //所持中のアイテム数
    public int myFoodCount = 0;

    //所持武器数
    public int myWeaponCount = 0;

    //所持服数
    public int myClothingCount = 0;

    //所持中の素材数
    public int myMaterialCount = 0;

    //所持アイテム一覧
    public GameObject[] myFoodArray = new GameObject[20];

    //所持武器一覧
    public GameObject[] myWeaponArray = new GameObject[20];

    //所持服一覧
    public GameObject[] myClothingArray = new GameObject[20];

    //所持素材一覧
    public GameObject[] myMaterialArray = new GameObject[20];

    //アイテムをバッグに入れる
    public bool PutItem(GameObject item)
    {
        //入れるアイテムが食べ物系の場合
        if(item.GetComponent<ItemController>().Type == ItemType.Food)
        {
            SortArray(myFoodArray);

            //バッグにまだ入るか確認
            if(myFoodCount < bagSize_food)
            {
                myFoodArray[myFoodCount] = item;
                myFoodCount++;
                return true;
            }
            else
            {
                Debug.Log("バッグがいっぱいです");
                return false;
            }
        }
        //入れるアイテムが武器の場合
        else if(item.GetComponent<ItemController>().Type == ItemType.Weapon)
        {
            SortArray(myWeaponArray);

            //バッグにまだ入るか確認
            if(myWeaponCount < bagSize_weapon)
            {
                myWeaponArray[myWeaponCount] = item;
                myWeaponCount++;
                return true;
            }
            else
            {
                Debug.Log("バッグがいっぱいです");
                return false;
            }
        }
        //入れるアイテムが防具の場合
        else if(item.GetComponent<ItemController>().Type == ItemType.Clothing)
        {
            SortArray(myClothingArray);

            //バッグにまだ入るか確認
            if(myClothingCount < bagSize_clothing)
            {
                myClothingArray[myClothingCount] = item;
                myClothingCount++;
                return true;
            }
            else
            {
                Debug.Log("バッグがいっぱいです");
                return false;
            }
        }
        //入れるアイテムが素材の場合
        else if(item.GetComponent<ItemController>().Type == ItemType.Material)
        {
            SortArray(myMaterialArray);

            //バッグにまだ入るか確認
            if(myMaterialCount < bagSize_material)
            {
                myMaterialArray[myMaterialCount] = item;
                myMaterialCount++;
                return true;
            }
            else
            {
                Debug.Log("バッグがいっぱいです");
                return false;
            }
        }


        Debug.Log("エラー");
        return false;
    }

    //アイテムをバッグから出す
    public GameObject TakeItem(ItemType type, int index)
    {
        //出すアイテムが食べ物系の場合
        if(type == ItemType.Food)
        {
            GameObject item = myFoodArray[index];
            myFoodArray[index] = null;
            myFoodCount--;
            SortArray(myFoodArray);
            return item;
        }
        //出すアイテムが武器の場合
        else if(type == ItemType.Weapon)
        {
            GameObject item = myWeaponArray[index];
            myWeaponArray[index] = null;
            myWeaponCount--;
            SortArray(myWeaponArray);
            return item;
        }
        //出すアイテムが防具の場合
        else if(type == ItemType.Clothing)
        {
            GameObject item = myClothingArray[index];
            myClothingArray[index] = null;
            myClothingCount--;
            SortArray(myClothingArray);
            return item;
        }
        //出すアイテムが素材の場合
        else if(type == ItemType.Material)
        {
            GameObject item = myMaterialArray[index];
            myMaterialArray[index] = null;
            myMaterialCount--;
            SortArray(myMaterialArray);
            return item;
        }

        Debug.Log("エラー");
        return null;
    }

    //隙間詰めソート
    private void SortArray(GameObject[] array)
    {
        int pointer = 0;
        for (int i = 0; i < array.Length; i++) 
        {
            GameObject currentCell = array[i];
            if(i != pointer && currentCell != null)
            {
                array[pointer] = currentCell;
                array[i] = null;
            }
            if(array[pointer] != null)
            {
                pointer++;
            }
        }
    }
}
