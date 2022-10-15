using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;

namespace Battle
{
    public class BagPanelController : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        SetActive(false);
        
        BattleManager.Instance.CurrentBattleState
            .DistinctUntilChanged()
            .Where(x => x == BattleState.OpenBag)
            .Subscribe(_ => SetActive(true));

        BattleManager.Instance.CurrentBattleState
            .DistinctUntilChanged()
            .Where(x => x != BattleState.OpenBag)
            .Subscribe(_ => SetActive(false));
    }

    void SetActive(bool active)
    {
        this.gameObject.SetActive(active);
    }
}
}   

