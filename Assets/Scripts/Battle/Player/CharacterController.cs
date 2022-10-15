using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

    //キャラクターの生死状態
    public enum CharacterState
    {
        Alive,
        Dead,
    }

    //キャラクターのタイプ
    public enum CharacterType
    {
        Player,
        Enemy,
    }

//全キャラクター共通のコントローラー
public class CharacterController : MonoBehaviour
{
    [SerializeField]
    //キャラクターの生死状態
    ReactiveProperty<CharacterState> state = new ReactiveProperty<CharacterState>(CharacterState.Alive);

    //キャラクターのタイプ
    [SerializeField]
    CharacterType type = CharacterType.Player;

     //キャラクターの生死状態のゲッター
    public ReactiveProperty<CharacterState> State
    {
        get{return state;}
        set{this.state = value;}
    }

    //キャラクターのタイプのゲッター
    public CharacterType Type
    {
        get{return type;}
    }

    [SerializeField]
    public ReactiveProperty<bool> target = new ReactiveProperty<bool>(false);

    public ReactiveProperty<bool> active = new ReactiveProperty<bool>(false);

    Animator animator;

    private void Start()
    {
        target.Value = false;
        active.Value = false;

        BattleManager.Instance.CurrentBattleState
            .DistinctUntilChanged()
            .Where(x => x == BattleState.Action_player || x == BattleState.Action_enemy)
            .Where(x => target.Value == true)
            .Subscribe(_ => AcceptAction());

        BattleManager.Instance.CurrentBattleState
            .DistinctUntilChanged()
            .Where(x => x == BattleState.Action_player || x == BattleState.Action_enemy)
            .Where(x => active.Value == true)
            .Subscribe(_ => active.Value = false);
        
    }

    //歩く
    public void Walk()
    {
        //歩くアニメーション
    }

    //行動
    void AcceptAction()
    {
        PlayerPartyManager.Instance.ActionCharacter(GetComponent<CharacterStatus>());
        target.Value = false;
    }

    public void Dead()
    {
        //死にアニメーション
        if(Type == CharacterType.Player)
        {
            PlayerPartyManager.Instance.DeadPlayer();
        }
        else
        {
            MyItemData.Instance.PutItem(GetComponent<DropItemData>().DropItem());
            PlayerPartyManager.Instance.DeadEnemy();
        }
        
        state.Value = CharacterState.Dead;
    }
}
