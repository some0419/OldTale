using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class CharacterAction : MonoBehaviour
{
    Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
        
        PlayerPartyManager.Instance.subject
            .DistinctUntilChanged()
            .Where(_ => GetComponent<CharacterController>().active.Value == true)
            .Subscribe(x => SelectAction(x));
    }

    //ゲージ値に応じて行動が変化
    void SelectAction(int gaugeValue)
    {
        //Debug.Log("行動が選ばれました");

        if(gaugeValue < 30)
        {
            PlayerPartyManager.Instance.Event += Action1;
            if(GetComponent<CharacterController>().Type == CharacterType.Player)
            {
                BattleManager.Instance.SetBattleState(BattleState.Action_player);
            }
            else if(GetComponent<CharacterController>().Type == CharacterType.Enemy)
            {
                BattleManager.Instance.SetBattleState(BattleState.Action_enemy);
            }
        }
        else if(30 <= gaugeValue && gaugeValue <= 80)
        {
            PlayerPartyManager.Instance.Event += Action2;
            BattleManager.Instance.SetBattleState(BattleState.SelectingTarget);
        }
        else
        {
            PlayerPartyManager.Instance.Event += Action3;
            if(GetComponent<CharacterController>().Type == CharacterType.Player)
            {
                for(int i = 0; i < EnemyData.Instance.GetBattleParty().Count; i++)
                {
                    EnemyData.Instance.GetBattlePlayer(i).GetComponent<CharacterController>().target.Value = true;
                }
                BattleManager.Instance.SetBattleState(BattleState.Action_player);
            }
            else if(GetComponent<CharacterController>().Type == CharacterType.Enemy)
            {
                for(int i = 0; i < PartyData.Instance.GetBattleParty().Count; i++)
                {
                    PartyData.Instance.GetBattlePlayerObject(i).GetComponent<CharacterController>().target.Value = true;
                }
                BattleManager.Instance.SetBattleState(BattleState.Action_enemy);
            }
            
        }
    }

    void Action1(CharacterStatus target)
    {
        Debug.Log("ミス");
    }

    void Action2(CharacterStatus target)
    {
        //Debug.Log("普通の攻撃");
        int damage = (GetComponent<CharacterStatus>().Power.Value + GetComponent<CharacterStatus>().PowerWeapon.Value) + Random.Range(10, 30);
        //Debug.Log(damage + "のダメージ");
        target.DecreaseCurrentHp(damage);
    }

    void Action3(CharacterStatus target)
    {
        //Debug.Log("全体攻撃!!!");
       
        int damage = (GetComponent<CharacterStatus>().Power.Value + GetComponent<CharacterStatus>().PowerWeapon.Value) + Random.Range(10, 20);
        //Debug.Log(damage + "のダメージ");
        target.DecreaseCurrentHp(damage);
    }
}
