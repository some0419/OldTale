using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class PlayerButtonController : MonoBehaviour
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
        character = PartyData.Instance.GetBattlePlayerObject(buttonNumber);
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
            .Subscribe(_ => DeadPlayer());

        
        hpGauge.SetMaxValue(characterStatus);
        UpdateHpGauge();
        GenerateButton();
    }

    void GenerateButton()
    {
        if(PartyData.Instance.GetBattlePlayerObject(buttonNumber) != null)
        {
            GetComponent<Image>().sprite = PartyData.Instance.GetBattlePlayerObject(buttonNumber).transform.GetChild(0).gameObject.GetComponent<Image>().sprite;
        }
    }

    //ボタン押下時の処理
    public void PlayerButtonDown()
    {
        if(buttonState == ButtonState.Waiting)
        {
            SetButtonState(ButtonState.Pushed);

            //キャラが死んでなければ
            if(characterController.State.Value != CharacterState.Dead)
            {
                if(BattleManager.Instance.CurrentBattleState.Value == BattleState.Waiting)
                {
                    //味方ゲージ表示状態に遷移
                    BattleManager.Instance.SetBattleState(BattleState.Gauge_player);

                     //担当キャラクターをアクティブ状態に変更
                    characterController.active.Value = true;
                }
                else if(BattleManager.Instance.CurrentBattleState.Value == BattleState.SelectingTarget)
                {
                    //担当キャラを対象状態に
                    characterController.target.Value = true;

                    //味方行動状態へ遷移
                    BattleManager.Instance.SetBattleState(BattleState.Action_player);
                }
                else if(BattleManager.Instance.CurrentBattleState.Value == BattleState.Gauge_enemy && characterController.target.Value == true)
                {
                    //敵行動状態へ遷移
                    BattleManager.Instance.SetBattleState(BattleState.StopGauge_enemy);

                    //色をもとに戻す
                    SetColor(new Color32(255,255,255,255));

                    //入力待ち状態へ遷移
                    BattleManager.Instance.SetBattleState(BattleState.Action_enemy);
                }
                else if(BattleManager.Instance.CurrentBattleState.Value == BattleState.GameClear || BattleManager.Instance.CurrentBattleState.Value == BattleState.GameOver)
                {
                    //何も起きない
                }
                else
                {
                    //ステータス表示
                }
            }
            //キャラが死んでいた場合の処理
            else
            {
                //死んでる
                Debug.Log(buttonNumber + "は死んでいる");
                //ステータス表示
            }
        }
        else
        {
            Debug.Log(buttonNumber + "は死んでいる");
        }
    }

    //ボタン離反時の処理
    public void PlayerButtonUp()
    {
        if(buttonState == ButtonState.Pushed)
        {
            SetButtonState(ButtonState.Waiting);

            if(characterController.State.Value != CharacterState.Dead)
            {
                if(BattleManager.Instance.CurrentBattleState.Value == BattleState.Gauge_player)
                {
                    BattleManager.Instance.SetBattleState(BattleState.StopGauge_player);
                }
                else if(BattleManager.Instance.CurrentBattleState.Value == BattleState.Gauge_enemy)
                {
                    //敵の攻撃をガード
                }
                else if(BattleManager.Instance.CurrentBattleState.Value == BattleState.GameClear || BattleManager.Instance.CurrentBattleState.Value == BattleState.GameOver)
                {
                    //何も起きない
                }
                else
                {
                    //ステータス表示
                }
            }
            else
            {
                Debug.Log(buttonNumber + "は死んでいる");
            }
        }
        else
        {
            
        }
    }

    //担当キャラクターが死んだときの処理
    void DeadPlayer()
    {
        SetColor(new Color32(120, 120, 120, 255));
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
}
