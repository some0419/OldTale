using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class CharacterStatus : MonoBehaviour
{
    //キャラの名前
    public string name;

    Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    //ステータス
    //レベル
    [Min(1)]int currentLevel = 1;
    //最大HP
    [Min(1)]int maxHp = 100;

    //現在のHP
    [SerializeField]
    ReactiveProperty<int> currentHp = new ReactiveProperty<int>(60);

    //最大SP
    [Min(1)]int maxCp = 100;

    [SerializeField]
    //現在のSP
    ReactiveProperty<int> currentCp = new ReactiveProperty<int>(100);

    [SerializeField]
    //防御力
    ReactiveProperty<int> defense = new ReactiveProperty<int>(10);

    [SerializeField]
    //服による防御値上昇値
    ReactiveProperty<int> defenseClothing = new ReactiveProperty<int>(0);

    [SerializeField]
    //攻撃力
    ReactiveProperty<int> power = new ReactiveProperty<int>(10);

    [SerializeField]
    //武器による攻撃力上昇値
    ReactiveProperty<int> powerWeapon = new ReactiveProperty<int>(0);
    

    //プレイヤー名のゲッター
    public string Name
    {
        get{return name;}
    }

    //現在のレベルのゲッター
    public int CurrentLevel
    {
        get{return currentLevel;}
    }

    //最大HPのゲッター
    public int MaxHp
    {
        get{return maxHp;}
    }

    //現在のHpのゲッター
    public ReactiveProperty<int> CurrentHp
    {
        get{return currentHp;}
        set{currentHp = value;}
    }

    //最大Cpのゲッター
    public int MaxCp
    {
        get{return maxCp;}
    }

    //現在のCpのゲッター
    public ReactiveProperty<int> CurrentCp
    {
        get{return currentCp;}
    }

    //防御力のゲッター
    public ReactiveProperty<int> Defense
    {
        get{return defense;}
    }

    //攻撃力のゲッター
    public ReactiveProperty<int> Power
    {
        get{return power;}
    }

    //服による防御値上昇値のプロパティ
    public ReactiveProperty<int> DefenseClothing
    {
        get{return defenseClothing;}
        set{defenseClothing = value;}
    }

    //武器による攻撃力上昇値のプロパティ
    public ReactiveProperty<int> PowerWeapon
    {
        get{return powerWeapon;}
        set{powerWeapon = value;}
    }

    //レベル増加
    public void IncreaseLevel()
    {
        currentLevel += 1;
    }

    //現在のHp増加
    public void IncreaseCurrentHp(int amount)
    {
        if(currentHp.Value + amount >= maxHp)
        {
            currentHp.Value = maxHp;
        }
        else
        {
            currentHp.Value += amount;
        }
    }

    //現在のHp減少
    public void DecreaseCurrentHp(int amount)
    {
        animator.SetTrigger("Damage");
        int damage = amount - (GetComponent<CharacterStatus>().Defense.Value + GetComponent<CharacterStatus>().DefenseClothing.Value);

        if(damage <= 0)
        {
            damage = Random.Range(1, 3);
        }
        
        if(currentHp.Value - damage > 0)
        {
            currentHp.Value -= damage;
        }
        else
        {
            currentHp.Value = 0;
            GetComponent<CharacterController>().Dead();
        }
    }

    

    //現在のCp増加
    public void IncreaseCurrentCp(int amount)
    {
        if(currentCp.Value + amount >= maxCp)
        {
            currentCp.Value = maxCp;
        }
        else
        {
            currentCp.Value += amount;
        }
    }

    //現在のHp減少
    public void DecreaseCurrentCp(int amount)
    {
        if(currentCp.Value - amount > 0)
        {
            currentCp.Value -= amount;
        }
        else
        {
            currentCp.Value = 0;
        }
    }

    

    //防御力増加
    public void IncreaseDefense(int amount)
    {
        defense.Value += amount;
    }

    //防御力減少
    public void DecreaseDefense(int amount)
    {
        if(defense.Value - amount >= 0)
        {
            defense.Value -= amount;
        }
        else
        {
            defense.Value = 0;
        }
    }

    //攻撃力増加
    public void IncreasePower(int amount)
    {
        power.Value += amount;
    }

    //攻撃力減少
    public void DecreasePower(int amount)
    {
        if(power.Value - amount >= 0)
        {
            power.Value -= amount;
        }
        else
        {
            power.Value = 0;
        }
    }
}
