using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipment : MonoBehaviour
{
    [SerializeField]
    GameObject weapon;

    [SerializeField]
    GameObject clothing;

    public GameObject Weapon
    {
        get{return weapon;}
    }

    public GameObject Clothing
    {
        get{return clothing;}
    }

    void Start()
    {
        //ゲーム開始時から装備していた場合
        if(weapon != null)
        {
            GetComponent<CharacterStatus>().PowerWeapon.Value = this.weapon.GetComponent<ItemController>().amount;
            Debug.Log(GetComponent<CharacterStatus>().PowerWeapon.Value);
        }

        if(clothing != null)
        {
            GetComponent<CharacterStatus>().DefenseClothing.Value = this.clothing.GetComponent<ItemController>().amount;
        }

    }


    public void EquipWeapon(GameObject weapon)
    {
        if(this.weapon == null)
        {
            //武器を持っていなかった場合
            this.weapon = weapon;
        }
        else
        {
            //既に別の武器を持っていた場合
            TakeOffWeapon();
            this.weapon = weapon;
        }
        GetComponent<CharacterStatus>().PowerWeapon.Value = this.weapon.GetComponent<ItemController>().amount;
    }

    public void TakeOffWeapon()
    {
        MyItemData.Instance.PutItem(this.weapon);
        this.weapon = null;
        GetComponent<CharacterStatus>().PowerWeapon.Value = 0;
    }

    public void EquipClothing(GameObject clothing)
    {
        if(this.clothing == null)
        {
            //防具を装備していなかった場合
            this.clothing = clothing;
        }
        else
        {
            //既に別の防具を装備していた場合
            TakeOffClothing();
            this.clothing = clothing;
        }
        GetComponent<CharacterStatus>().DefenseClothing.Value = this.clothing.GetComponent<ItemController>().amount;
    }

    public void TakeOffClothing()
    {
        MyItemData.Instance.PutItem(this.clothing);
        this.clothing = null;
        GetComponent<CharacterStatus>().DefenseClothing.Value = 0;
    }
}
