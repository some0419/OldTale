using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class ToBattlePanelController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SetActive(false);

        HomeManager.Instance.CurrentHomeState
            .DistinctUntilChanged()
            .Where(x => x == HomeState.ToBattle)
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

    public void OnClickYesButton()
    {
        SceneManager.Instance.FadeOut();
    }

    public void OnClickNoButton()
    {
        HomeManager.Instance.SetHomeState(HomeState.None);
    }


}
