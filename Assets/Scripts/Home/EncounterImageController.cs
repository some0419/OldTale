using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class EncounterImageController : MonoBehaviour
{
    void Start()
    {
        SetActive(false);

        HomeManager.Instance.CurrentHomeState
            .DistinctUntilChanged()
            .Where(x => x == HomeState.Encounter)
            .Subscribe(_ => SetActive(true));
        
        HomeManager.Instance.CurrentHomeState
            .DistinctUntilChanged()
            .Where(x => x != HomeState.Encounter)
            .Subscribe(_ => SetActive(false));
    }

    void SetActive(bool active)
    {
        this.gameObject.SetActive(active);
    }
}
