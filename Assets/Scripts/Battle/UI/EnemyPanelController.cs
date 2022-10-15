using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class EnemyPanelController : MonoBehaviour
{
    EnemyButtonController[] enemyButton = new EnemyButtonController[4];

    // Start is called before the first frame update
    void Start()
    {
        BattleManager.Instance.CurrentBattleState
            .DistinctUntilChanged()
            .Where(x => x == BattleState.Walking)
            .Subscribe(_ => SetActive(false));

        BattleManager.Instance.CurrentBattleState
            .DistinctUntilChanged()
            .Where(x => x != BattleState.Walking)
            .Subscribe(_ => SetActive(true));

        for(int i = 0; i < 4; i++)
        {
            enemyButton[i] = transform.GetChild(i).gameObject.GetComponent<EnemyButtonController>();
        }
    }

    //プレイヤーパネル表示非表示
    void SetActive(bool active)
    {
        this.gameObject.SetActive(active);
    }

      public void UpdateHpGauge(int buttonNumber)
    {
        enemyButton[buttonNumber].UpdateHpGauge();
    }

    public void SetColor(int buttonNumber, Color32 color)
    {
        enemyButton[buttonNumber].SetColor(color);
    }
}
