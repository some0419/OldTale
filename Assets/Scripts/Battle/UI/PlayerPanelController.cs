using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UniRx;

public class PlayerPanelController : MonoBehaviour
{
    PlayerButtonController[] playerButton = new PlayerButtonController[4];

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
            playerButton[i] = transform.GetChild(i).gameObject.GetComponent<PlayerButtonController>();
        }
    }

    //プレイヤーパネル表示非表示
    void SetActive(bool active)
    {
        this.gameObject.SetActive(active);
    }

    public void UpdateHpGauge(int buttonNumber)
    {
        playerButton[buttonNumber].UpdateHpGauge();
    }

    public void SetColor(int buttonNumber, Color32 color)
    {
        playerButton[buttonNumber].SetColor(color);
    }
}
