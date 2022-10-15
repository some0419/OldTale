using System.Collections.Generic;
using UnityEngine;
using UniRx;

public delegate void Delegate(CharacterStatus target);

public class PlayerPartyManager : SingletonMonoBehaviour<PlayerPartyManager>
{
    public Subject<int> subject = new Subject<int>();

    //プレイヤーパネル
    [SerializeField]
    PlayerPanelController playerPanelController;

    [SerializeField]
    EnemyPanelController enemyPanelController;

    public event Delegate Event;

    public int Life_player{ set; get; }

    public int Life_enemy{ set; get; }

    void Awake()
    {
        base.Awake();

        BattleManager.Instance.CurrentBattleState
            .DistinctUntilChanged()
            .Where(x => x == BattleState.Gauge_enemy)
            .Subscribe(_ => SelectRandomActiveCharacter());
        
        BattleManager.Instance.CurrentBattleState
            .DistinctUntilChanged()
            .Where(x => x == BattleState.Gauge_enemy)
            .Subscribe(_ => SelectRandomTarget());
        
        Life_player = PartyData.Instance.alivePlayerCount;
        Life_enemy = EnemyData.Instance.aliveEnemyCount;
    }

    public void SelectAction(int gaugeValue)
    {
        Event = null;
        subject.OnNext(gaugeValue);
        //Debug.Log(gaugeValue);
    }

    public void ActionCharacter(CharacterStatus target)
    {
        Event?.Invoke(target);
    }

    public void DeadPlayer()
    {
        Life_player--;
        if(Life_player <= 0)
        {
            BattleManager.Instance.SetBattleState(BattleState.Lose);
        }
    }

    public void DeadEnemy()
    {
        Life_enemy--;
        if(Life_enemy <= 0)
        {
            BattleManager.Instance.SetBattleState(BattleState.Win);
        }
    }

    void SelectRandomActiveCharacter()
    {
        for(int i = 0; i < EnemyData.Instance.aliveEnemyCount; i++)
        {
            if(EnemyData.Instance.GetBattlePlayer(i).GetComponent<CharacterController>().State.Value != CharacterState.Dead)
            {
                EnemyData.Instance.GetBattlePlayer(i).GetComponent<CharacterController>().active.Value = true;
                return;
            }
        }
    }

    void SelectRandomTarget()
    {
        for(int i = 0; i < PartyData.Instance.alivePlayerCount; i++)
        {
            if(PartyData.Instance.GetBattlePlayerObject(i).GetComponent<CharacterController>().State.Value != CharacterState.Dead)
            {
                PartyData.Instance.GetBattlePlayerObject(i).GetComponent<CharacterController>().target.Value = true;
                return;
            }
        }
    }
}
