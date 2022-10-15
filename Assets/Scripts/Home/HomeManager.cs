using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class HomeManager : SingletonMonoBehaviour<HomeManager>
{
    //現在のホームシーンの状態
    [SerializeField]
    private ReactiveProperty<HomeState> homeState = new ReactiveProperty<HomeState>(HomeState.None);

    //選択中のアイテムのインデックス（メニュー）
    private int selectedItemIndex;

    //現在ステータス画面を開いているパーティーメンバーの番号（メニュー）
    public int openingPartyNumber;

    //現在の状態を取得（プロパティ）
    public ReactiveProperty<HomeState> CurrentHomeState
    {
        get
        {
            return homeState;
        }
    }

    //選択中のアイテムのインデックスを取得（プロパティ）
    public int SelectedItemIndex
    {
        get{ return selectedItemIndex; }
        set{ selectedItemIndex = value; }
    }

     void Awake()
    {
        base.Awake();
    }

    //状態を変更
    public void SetHomeState(HomeState homeState)
    {
        this.homeState.Value = homeState;
    }
}

//Homeの状態の一覧
public enum HomeState
{
    Encounter,

    Talking,
    
    //買い物中
    Shopping,

    //メニュートップ
    OpenMenu,

    //アイテムページ
    OpenItemPage,

    //食べ物ページ
    OpenFoodPage,

    //食べ物選択中
    SelectFood,

    //食べ物使用対象選択中
    UseFood,

    //武器ページ
    OpenWeaponPage,

    //武器選択中
    SelectWeapon,

    //武器装備対象選択中
    UseWeapon,

    //服ページ
    OpenClothingPage,

    //服選択中
    SelectClothing,

    //服装備対象選択中
    UseClothing,

    //素材ページ
    OpenMaterialPage,

    //素材選択中
    SelectMaterial,

    //アイテム使用後状態
    UsedItem,

    //パーティーページ
    OpenPartyPage,

    //装備ステータス表示
    OpenEquipmentStatus,

    //スキルステータス表示
    OpenActionStatus,

    //アルバムページ
    OpenAlbumPage,

    //オプションページ
    OpenOptionPage,

    //バトルシーンへ
    ToBattle,
    
    ToFishing,

    //状態なし
    None,
}
