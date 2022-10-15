using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;
using DG.Tweening;

public class MessageController : MonoBehaviour
{
    [SerializeField]
    Text messageText;

    [SerializeField]
    GameObject screenButton;

    // Start is called before the first frame update
    void Start()
    {
        SetActive(false);

        BattleManager.Instance.CurrentBattleState
            .DistinctUntilChanged()
            .Where(x => x == BattleState.Opening || x == BattleState.Waiting || x == BattleState.SelectingTarget || x == BattleState.Action_player || x == BattleState.Action_enemy || x == BattleState.GameClear || x == BattleState.GameOver)
            .Subscribe(_ => SetText());
    }

    void SetActive(bool active)
    {
        this.gameObject.SetActive(active);
    }

    void SetText()
    {
        SetActive(true);
        screenButton.SetActive(true);
        if(BattleManager.Instance.CurrentBattleState.Value == BattleState.Opening)
        {
            messageText.DOText("敵が現れた", 0.5f).SetEase(Ease.Linear);
        }
        else if(BattleManager.Instance.CurrentBattleState.Value == BattleState.Waiting)
        {
            messageText.DOText("味方のターン", 0.5f).SetEase(Ease.Linear);
            screenButton.SetActive(false);
        }
        else if(BattleManager.Instance.CurrentBattleState.Value == BattleState.SelectingTarget)
        {
            messageText.DOText("ターゲットを選択", 0.5f).SetEase(Ease.Linear);
            screenButton.SetActive(false);
        }
        else if(BattleManager.Instance.CurrentBattleState.Value == BattleState.Action_player)
        {
            messageText.DOText("味方の攻撃", 0.5f).SetEase(Ease.Linear);
        }
        else if(BattleManager.Instance.CurrentBattleState.Value == BattleState.Action_enemy)
        {
            messageText.DOText("敵の攻撃", 0.5f).SetEase(Ease.Linear);
        }
        else if(BattleManager.Instance.CurrentBattleState.Value == BattleState.GameClear)
        {
            messageText.DOText("敵に勝利した", 0.5f).SetEase(Ease.Linear);
        }
        else if(BattleManager.Instance.CurrentBattleState.Value == BattleState.GameOver)
        {
            messageText.DOText("全滅した、、", 0.5f).SetEase(Ease.Linear);
        }
    }

    public void OnClickPanel()
    {
        messageText.text = "";
        SetActive(false);
        if(BattleManager.Instance.CurrentBattleState.Value == BattleState.Opening)
        {
            BattleManager.Instance.SetBattleState(BattleState.Waiting);
        }
        else if(BattleManager.Instance.CurrentBattleState.Value == BattleState.Action_player)
        {
            BattleManager.Instance.SetBattleState(BattleState.Gauge_enemy);
        }
        else if(BattleManager.Instance.CurrentBattleState.Value == BattleState.Action_enemy)
        {
            BattleManager.Instance.SetBattleState(BattleState.Waiting);
        }
        else if(BattleManager.Instance.CurrentBattleState.Value == BattleState.GameClear)
        {
            SceneManager.Instance.ToHomeScene();
        }
        else if(BattleManager.Instance.CurrentBattleState.Value == BattleState.GameOver)
        {
            SceneManager.Instance.ToHomeScene();
        }

    }

    
}
