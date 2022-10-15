using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class ItemPageController : MonoBehaviour
{
    void Start()
    {
        SetActive(false);

        HomeManager.Instance.CurrentHomeState
            .DistinctUntilChanged()
            .Where(x => x == HomeState.OpenItemPage)
            .Subscribe(_ => SetActive(true));
        
        HomeManager.Instance.CurrentHomeState
            .DistinctUntilChanged()
            .Where(x => x == HomeState.OpenMenu)
            .Subscribe(_ => SetActive(false));
    }

    void SetActive(bool active)
    {
        this.gameObject.SetActive(active);
    }

    public void OnClickFoodPageButton()
    {
        HomeManager.Instance.SetHomeState(HomeState.OpenFoodPage);
    }

    public void OnClickWeaponPageButton()
    {
        HomeManager.Instance.SetHomeState(HomeState.OpenWeaponPage);
    }

    public void OnClickClothingPageButton()
    {
        HomeManager.Instance.SetHomeState(HomeState.OpenClothingPage);
    }

    public void OnClickMaterialPageButton()
    {
        HomeManager.Instance.SetHomeState(HomeState.OpenMaterialPage);
    }
}
