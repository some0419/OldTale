using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class EnemyButtonController : MonoBehaviour
{
    [SerializeField]
    int buttonNumber = 0;

    GameObject character;
    CharacterStatus characterStatus;
    CharacterController characterController;

    HpGaugeController hpGauge;
    CpGaugeController cpGauge;

    [SerializeField]
    GameObject inArrow;

    [SerializeField]
    GameObject outArrow;
    
    private enum ButtonState
    {
        Waiting,
        Pushed,
    }

    private ButtonState buttonState = ButtonState.Waiting;

    void Awake()
    {
        
    }

    void Start() 
    {
        character = EnemyData.Instance.GetBattlePlayer(buttonNumber);
        characterStatus = character?.GetComponent<CharacterStatus>();
        characterController = character?.GetComponent<CharacterController>();
        hpGauge = transform.Find("HpGauge").gameObject.GetComponent<HpGaugeController>();
        cpGauge = transform.Find("CpGauge").gameObject.GetComponent<CpGaugeController>();

        //Hpの変動を検知してゲージを更新
        characterStatus.CurrentHp
            .DistinctUntilChanged()
            .Subscribe(_ => UpdateHpGauge());

        //担当キャラが行動するときの処理
        characterController.active
            .DistinctUntilChanged()
            .Where(x => x == true && characterController.State.Value != CharacterState.Dead)
            .Subscribe(_ => outArrow.SetActive(true));

        characterController.active
            .DistinctUntilChanged()
            .Where(x => x == false && characterController.State.Value != CharacterState.Dead)
            .Subscribe(_ => outArrow.SetActive(false));

        //担当キャラが行動対象になる時の処理
        characterController.target
            .DistinctUntilChanged()
            .Where(x => x == true && characterController.State.Value != CharacterState.Dead)
            .Subscribe(_ => inArrow.SetActive(true));

        characterController.target
            .DistinctUntilChanged()
            .Where(x => x == false && characterController.State.Value != CharacterState.Dead)
            .Subscribe(_ => inArrow.SetActive(false));

        characterController.State
            .DistinctUntilChanged()
            .Where(x => x == CharacterState.Dead)
            .Subscribe(_ => DeadEnemy());
        
        hpGauge.SetMaxValue(characterStatus);
        UpdateHpGauge();
        GenerateButton();
    }

    void GenerateButton()
    {
        if(EnemyData.Instance.GetBattlePlayer(buttonNumber) != null)
        {
            GetComponent<Image>().sprite = EnemyData.Instance.GetBattlePlayer(buttonNumber).transform.GetChild(0).gameObject.GetComponent<Image>().sprite;
        }
    }

    //ボタン押下時の処理
    public void EnemyButtonDown()
    {
        if(buttonState == ButtonState.Waiting)
        {
            SetButtonState(ButtonState.Pushed);

            //キャラが死んでなければ
            if(characterController.State.Value != CharacterState.Dead)
            {
                //ゲーム状態に応じた処理
                //攻撃対象選択状態の時
                if(BattleManager.Instance.CurrentBattleState.Value == BattleState.SelectingTarget)
                {
                    //ボタンに応じたエネミーをターゲットに設定
                    characterController.target.Value = true;
                    BattleManager.Instance.SetBattleState(BattleState.Action_player);
                }
                //ゲームクリア、ゲームオーバー状態の時
                else if(BattleManager.Instance.CurrentBattleState.Value == BattleState.GameClear || BattleManager.Instance.CurrentBattleState.Value == BattleState.GameOver)
                {
                    //何も起きない
                }
                //その他の状態の時
                else
                {
                    //ステータス表示
                }
            }
            else
            {
                //死んでる
                Debug.Log(buttonNumber + "は死んでいる");
                //ステータス表示
            }
        }
    }

    //ボタン離反時の処理
    public void EnemyButtonUp()
    {
        if(buttonState == ButtonState.Pushed)
        {
            SetButtonState(ButtonState.Waiting);
        }
    }

    void SetButtonState(ButtonState state)
    {
        buttonState = state;
    }

    public void SetColor(Color32 color)
    {
        GetComponent<Image>().color = color;
    }

    public void UpdateHpGauge()
    {
        hpGauge.SetCurrentValue(characterStatus);
    }

     //担当キャラクターが死んだときの処理
    void DeadEnemy()
    {
        SetColor(new Color32(120, 120, 120, 255));
    }
}
