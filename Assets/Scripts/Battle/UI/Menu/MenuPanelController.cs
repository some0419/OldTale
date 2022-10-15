using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace Battle
{
    public class MenuPanelController : MonoBehaviour
    {
        void Start()
        {
            SetActive(false);
        
            BattleManager.Instance.CurrentBattleState
                .DistinctUntilChanged()
                .Where(x => x == BattleState.OpenMenu)
                .Subscribe(_ => SetActive(true));

            BattleManager.Instance.CurrentBattleState
                .DistinctUntilChanged()
                .Where(x => x != BattleState.OpenMenu)
                .Subscribe(_ => SetActive(false));
        }

        void SetActive(bool active)
        {
            this.gameObject.SetActive(active);
        }

        //メニュー画面上でアイテムぺージを開く
        public void OnClickPartyPageButton()
        {   
            BattleManager.Instance.SetBattleState(BattleState.OpenPartyMenu);
        }

        //メニュー画面上でパーティーぺージを開く
        public void OnClickOptionPageButton()
        {
            BattleManager.Instance.SetBattleState(BattleState.OpenOptionMenu);
        }
    }
}

