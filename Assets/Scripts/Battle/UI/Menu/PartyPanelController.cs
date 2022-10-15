using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace Battle
{
    public class PartyPanelController : MonoBehaviour
    {
        void Start()
        {
            SetActive(false);

            BattleManager.Instance.CurrentBattleState
                .DistinctUntilChanged()
                .Where(x => x == BattleState.OpenPartyMenu || x == BattleState.SelectPlayerToUseFood)
                .Subscribe(_ => SetActive(true));

            BattleManager.Instance.CurrentBattleState
                .DistinctUntilChanged()
                .Where(x => x != BattleState.OpenPartyMenu && x != BattleState.SelectPlayerToUseFood)
                .Subscribe(_ => SetActive(false));
        }

        void SetActive(bool active)
        {
            this.gameObject.SetActive(active);
        }
    }
}
