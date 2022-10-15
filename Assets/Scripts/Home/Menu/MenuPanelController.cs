using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class MenuPanelController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SetActive(false);
        
        HomeManager.Instance.CurrentHomeState
            .DistinctUntilChanged()
            .Where(x => x == HomeState.OpenMenu)
            .Subscribe(_ => SetActive(true));
        
        HomeManager.Instance.CurrentHomeState
            .DistinctUntilChanged()
            .Where(x => x == HomeState.None)
            .Subscribe(_ => SetActive(false));
    }

    void SetActive(bool active)
    {
        this.gameObject.SetActive(active);
    }

    //メニュー画面上でアイテムぺージを開く
    public void OnClickItemPageButton()
    {
        HomeManager.Instance.SetHomeState(HomeState.OpenItemPage);
    }

    //メニュー画面上でパーティーぺージを開く
    public void OnClickPartyPageButton()
    {
        HomeManager.Instance.SetHomeState(HomeState.OpenPartyPage);
    }

    //メニュー画面上でアルバムぺージを開く
    public void OnClickAlbumPageButton()
    {
        HomeManager.Instance.SetHomeState(HomeState.OpenAlbumPage);
    }

    //メニュー画面上でオプションページを開く
    public void OnClickOptionPageButton()
    {
        HomeManager.Instance.SetHomeState(HomeState.OpenOptionPage);
    }
}
