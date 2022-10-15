using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class BattleManager : SingletonMonoBehaviour<BattleManager>
{
    int battleNumber;

    private int selectedItemIndex;
    public int openingPartyNumber;

    public int SelectedItemIndex
    {
        get{ return selectedItemIndex; }
        set{ selectedItemIndex = value; }
    }
    
    [SerializeField]
    private ReactiveProperty<BattleState> battleState = new ReactiveProperty<BattleState>(BattleState.Opening);

    [SerializeField]
    private BattleState previousState = BattleState.Opening;

    void Awake()
    {
        base.Awake();
    }

    void Start()
    {
        
    }

    //状態を取得
    public ReactiveProperty<BattleState> CurrentBattleState
    {
        get
        {
            return battleState;
        }
    }

    //状態を変更
    public void SetBattleState(BattleState battleState)
    {
        this.previousState = this.battleState.Value;
        this.battleState.Value = battleState;

        switch(this.battleState.Value)
        {
            case BattleState.Opening:
                Opening();
                break;
            case BattleState.Walking:
                Walking();
                break;
            case BattleState.Waiting:
                Waiting();
                break;
            case BattleState.Gauge_player:
                Gauge_player();
                break;
            case BattleState.StopGauge_player:
                StopGauge_player();
                break;
            case BattleState.SelectingTarget:
                SelectingTarget();
                break;
            case BattleState.Action_player:
                Action_player();
                break;
            case BattleState.Gauge_enemy:
                Gauge_enemy();
                break;
            case BattleState.StopGauge_enemy:
                StopGauge_enemy();
                break;
            case BattleState.Action_enemy:
                Action_enemy();
                break;
            case BattleState.Win:
                Win();
                break;
            case BattleState.Lose:
                Lose();
                break;
            case BattleState.GameClear:
                GameClear();
                break;
            case BattleState.GameOver:
                GameOver();
                break;
            case BattleState.OpenMenu:
                OpenMenu();
                break;
            case BattleState.OpenPartyMenu:
                OpenPartyMenu();
                break;
            case BattleState.OpenEquipmentStatus:
                OpenPartyMenu();
                break;
            case BattleState.SelectFood:
                OpenPartyMenu();
                break;
            case BattleState.SelectPlayerToUseFood:
                OpenPartyMenu();
                break;
            case BattleState.OpenBag:
                OpenPartyMenu();
                break;
            case BattleState.UsedItem:
                UsedItem();
                break;
            default:
                Debug.Log("状態が存在しません");
                return;
        }
    }

    public void ReturnToPreviousState()
    {
        SetBattleState(previousState);
    }

    private void Opening()
    {
        
    }

    private void Walking()
    {

    }

    private void Waiting()
    {

    }

    private void Gauge_player()
    {
        
    }

    private void StopGauge_player()
    {
        
    }

    private void SelectingTarget()
    {
        
    }

    private void Action_player()
    {
        
    }

    private void Gauge_enemy()
    {

    }

    private void StopGauge_enemy()
    {
        
    }

    private void Action_enemy()
    {
        
    }

    private void Win()
    {
        SetBattleState(BattleState.GameClear);
    }

    private void Lose()
    {
        SetBattleState(BattleState.GameOver);
    }

    private void GameClear()
    {
        Debug.Log("GameClear");
        PartyData.Instance.SetObjectToBattleArray();
    }

    private void GameOver()
    {
        Debug.Log("GameOver");
        PartyData.Instance.SetObjectToBattleArray();
    }

    private void OpenMenu()
    {
        
    }

    private void OpenPartyMenu()
    {
        
    }

    private void UsedItem()
    {
        SetBattleState(BattleState.Gauge_enemy);
    }

}

public enum BattleState
    {
        Opening,
        Walking,
        Waiting,

        Gauge_player,

        StopGauge_player,

        SelectingTarget,

        Action_player,

        Gauge_enemy,

        StopGauge_enemy,

        Action_enemy,

        Win,
        Lose,

        GameClear,
        GameOver,

        Pause,
        OpenMenu,
        OpenPartyMenu,
        OpenOptionMenu,
        SelectPlayerToUseFood,
        UsedItem,
        OpenEquipmentStatus,
        OpenBag,
        SelectFood,
    }
