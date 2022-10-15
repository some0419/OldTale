using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;


public class MenuButtonController : MonoBehaviour
{
    void Start()
    {
        HomeManager.Instance.CurrentHomeState
            .DistinctUntilChanged()
            .Where(x => x != HomeState.Shopping && x != HomeState.Encounter)
            .Subscribe(_ => SetActive(true));

        HomeManager.Instance.CurrentHomeState
            .DistinctUntilChanged()
            .Where(x => x == HomeState.Shopping || x == HomeState.Encounter)
            .Subscribe(_ => SetActive(false));
    }

    void SetActive(bool active)
    {
        this.gameObject.SetActive(active);
    }

    //メニューボタン押下
    //メニュー画面開閉
    public void OnClickMenuButton()
    {
        if(HomeManager.Instance.CurrentHomeState.Value == HomeState.None)
        {
            //何も開いていなければメニューを開く
            HomeManager.Instance.SetHomeState(HomeState.OpenMenu);
        }
        else if(HomeManager.Instance.CurrentHomeState.Value == HomeState.OpenMenu)
        {
            //メニューを開いていればメニューを閉じる
            HomeManager.Instance.SetHomeState(HomeState.None);
        }
        else if(HomeManager.Instance.CurrentHomeState.Value == HomeState.OpenItemPage)
        {
            //アイテムページを開いていればメニューに戻る
            HomeManager.Instance.SetHomeState(HomeState.OpenMenu);
        }
        else if(HomeManager.Instance.CurrentHomeState.Value == HomeState.OpenFoodPage)
        {
            //食べ物ページを開いていればアイテムページに戻る
            HomeManager.Instance.SetHomeState(HomeState.OpenItemPage);
        }
        else if(HomeManager.Instance.CurrentHomeState.Value == HomeState.OpenWeaponPage)
        {
            //武器ページを開いていればアイテムページに戻る
            HomeManager.Instance.SetHomeState(HomeState.OpenItemPage);
        }
        else if(HomeManager.Instance.CurrentHomeState.Value == HomeState.OpenClothingPage)
        {
            //防具ページを開いていればアイテムページに戻る
            HomeManager.Instance.SetHomeState(HomeState.OpenItemPage);
        }
        else if(HomeManager.Instance.CurrentHomeState.Value == HomeState.OpenMaterialPage)
        {
            //素材ページを開いていればアイテムページに戻る
            HomeManager.Instance.SetHomeState(HomeState.OpenItemPage);
        }
        else if(HomeManager.Instance.CurrentHomeState.Value == HomeState.OpenPartyPage)
        {
            //パーティーページを開いていればメニューに戻る
            HomeManager.Instance.SetHomeState(HomeState.OpenMenu);
        }
        else if(HomeManager.Instance.CurrentHomeState.Value == HomeState.SelectFood)
        {
            //食べ物選択画面中なら食べ物ページに戻る
            HomeManager.Instance.SetHomeState(HomeState.OpenFoodPage);
        }
        else if(HomeManager.Instance.CurrentHomeState.Value == HomeState.SelectWeapon)
        {
            //武器選択画面中なら武器ページに戻る
            HomeManager.Instance.SetHomeState(HomeState.OpenWeaponPage);
        }
        else if(HomeManager.Instance.CurrentHomeState.Value == HomeState.SelectClothing)
        {
            //服選択画面中なら服ページに戻る
            HomeManager.Instance.SetHomeState(HomeState.OpenClothingPage);
        }
        else if(HomeManager.Instance.CurrentHomeState.Value == HomeState.SelectMaterial)
        {
            //素材選択画面中なら素材ページに戻る
            HomeManager.Instance.SetHomeState(HomeState.OpenMaterialPage);
        }
        else if(HomeManager.Instance.CurrentHomeState.Value == HomeState.UseFood)
        {
            //食べ物使用対象選択中なら食べ物ページに戻る
            HomeManager.Instance.SetHomeState(HomeState.OpenFoodPage);
        }
        else if(HomeManager.Instance.CurrentHomeState.Value == HomeState.UseWeapon)
        {
            //武器使用対象選択中なら武器ページに戻る
            HomeManager.Instance.SetHomeState(HomeState.OpenWeaponPage);
        }
        else if(HomeManager.Instance.CurrentHomeState.Value == HomeState.UseClothing)
        {
            //服使用対象選択中なら服ページに戻る
            HomeManager.Instance.SetHomeState(HomeState.OpenClothingPage);
        }
        else if(HomeManager.Instance.CurrentHomeState.Value == HomeState.UsedItem)
        {
            //アイテム使用後状態ならアイテムページに戻る
            HomeManager.Instance.SetHomeState(HomeState.OpenItemPage);
        }
        else if(HomeManager.Instance.CurrentHomeState.Value == HomeState.OpenEquipmentStatus)
        {
            //装備ステータス表示中ならパーティーページに戻る
            HomeManager.Instance.SetHomeState(HomeState.OpenPartyPage);
        }
        else if(HomeManager.Instance.CurrentHomeState.Value == HomeState.OpenActionStatus)
        {
            //スキルステータス表示中ならパーティーページに戻る
            HomeManager.Instance.SetHomeState(HomeState.OpenPartyPage);
        }
    }
}