using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;

namespace Battle
{
    public class BagButtonController : MonoBehaviour
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
        public void OnClickBagButton()
        {
            if(BattleManager.Instance.CurrentBattleState.Value != BattleState.OpenBag)
            {
                //メニューを開く
                BattleManager.Instance.SetBattleState(BattleState.OpenBag);
            }
            else if(BattleManager.Instance.CurrentBattleState.Value == BattleState.OpenBag)
            {
                //メニューを閉じる
                BattleManager.Instance.ReturnToPreviousState();
            }
        }

       
}
}

