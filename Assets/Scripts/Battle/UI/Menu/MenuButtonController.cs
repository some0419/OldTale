using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace Battle
{
    public class MenuButtonController : MonoBehaviour
    {
        void Start()
        {
            SetActive(true);
            /*BattleManager.Instance.CurrentBattleState
                .DistinctUntilChanged()
                .Where(x => x != BattleState.Opening)
                .Subscribe(_ => SetActive(true));

            BattleManager.Instance.CurrentBattleState
                .DistinctUntilChanged()
                .Where(x => x == BattleState.Opening)
                .Subscribe(_ => SetActive(false));*/
        }

        void SetActive(bool active)
        {
            this.gameObject.SetActive(active);
        }

        //メニューボタン押下
        //メニュー画面開閉
        public void OnClickMenuButton()
        {
            if(BattleManager.Instance.CurrentBattleState.Value != BattleState.OpenMenu)
            {
                //メニューを開く
                BattleManager.Instance.SetBattleState(BattleState.OpenMenu);
            }
            else if(BattleManager.Instance.CurrentBattleState.Value == BattleState.OpenMenu)
            {
                //メニューを閉じる
                BattleManager.Instance.SetBattleState(BattleState.OpenMenu);
            }
        }

        
    }
}

