using System.Collections.Generic;
using UnityEngine;

public class CharacterSkill : MonoBehaviour
{
    string skillName;

    void Start()
    {
        
    }

    //スキル名のゲッター
    public string SkillName
    {
        get{return skillName;}
    }

    public void ActivateSkill()
    {
        Debug.Log(GetComponent<CharacterStatus>().Name + " のスキル " + skillName + " 発動");
    }
}
