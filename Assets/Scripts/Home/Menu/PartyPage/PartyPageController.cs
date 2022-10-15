using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class PartyPageController : MonoBehaviour
{   
    void Start()
    {
        SetActive(false);
        
        HomeManager.Instance.CurrentHomeState
            .DistinctUntilChanged()
            .Where(x => x == HomeState.UseFood || x == HomeState.UseWeapon || x == HomeState.UseClothing || x == HomeState.OpenPartyPage)
            .Subscribe(_ => SetActive(true));

        HomeManager.Instance.CurrentHomeState
            .DistinctUntilChanged()
            .Where(x => x == HomeState.OpenMenu || x == HomeState.OpenItemPage || x == HomeState.OpenFoodPage || x == HomeState.OpenWeaponPage || x == HomeState.OpenClothingPage)
            .Subscribe(_ => SetActive(false));
    }

     void SetActive(bool active)
    {
        this.gameObject.SetActive(active);
    }
}
