using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace Battle
{
    public class PartyButtonController : MonoBehaviour
{
    [SerializeField]
    int partyNumber = 0;

    // Start is called before the first frame update
    void Awake()
    {
        BattleManager.Instance.CurrentBattleState
            .DistinctUntilChanged()
            .Where(x => x == BattleState.OpenPartyMenu || x == BattleState.SelectPlayerToUseFood)
            .Subscribe(_ => GenerateButton());
    }

    void Start()
    {
        PartyData.Instance.GetBattlePlayerObject(partyNumber).GetComponent<CharacterStatus>().CurrentHp
            .DistinctUntilChanged()
            .Subscribe(_ => UpdateHpText());

        PartyData.Instance.GetBattlePlayerObject(partyNumber).GetComponent<CharacterStatus>().CurrentCp
            .DistinctUntilChanged()
            .Subscribe(_ => UpdateCpText());

        PartyData.Instance.GetBattlePlayerObject(partyNumber).GetComponent<CharacterStatus>().Power
            .DistinctUntilChanged()
            .Subscribe(_ => UpdatePowerText());

        PartyData.Instance.GetBattlePlayerObject(partyNumber).GetComponent<CharacterStatus>().Defense
            .DistinctUntilChanged()
            .Subscribe(_ => UpdateDefenseText());
        
        PartyData.Instance.GetBattlePlayerObject(partyNumber).GetComponent<CharacterStatus>().PowerWeapon
            .DistinctUntilChanged()
            .Subscribe(_ => UpdatePowerWeaponText());

        PartyData.Instance.GetBattlePlayerObject(partyNumber).GetComponent<CharacterStatus>().DefenseClothing
            .DistinctUntilChanged()
            .Subscribe(_ => UpdateDefenseClothingText());
    }

    void GenerateButton()
    {
        transform.Find("Image").GetComponent<Image>().sprite = PartyData.Instance.GetBattlePlayerObject(partyNumber).GetComponent<SpriteRenderer>().sprite;
        transform.Find("PlayerName").GetComponent<Text>().text = PartyData.Instance.GetBattlePlayerObject(partyNumber).GetComponent<CharacterStatus>().name;
        transform.Find("MaxHp").GetComponent<Text>().text = PartyData.Instance.GetBattlePlayerObject(partyNumber).GetComponent<CharacterStatus>().MaxHp.ToString();
        transform.Find("MaxCp").GetComponent<Text>().text = PartyData.Instance.GetBattlePlayerObject(partyNumber).GetComponent<CharacterStatus>().MaxCp.ToString();
        transform.Find("CurrentHp").GetComponent<Text>().text = PartyData.Instance.GetBattlePlayerObject(partyNumber).GetComponent<CharacterStatus>().CurrentHp.ToString();
        transform.Find("CurrentCp").GetComponent<Text>().text = PartyData.Instance.GetBattlePlayerObject(partyNumber).GetComponent<CharacterStatus>().CurrentCp.ToString();
        transform.Find("Power").GetComponent<Text>().text = PartyData.Instance.GetBattlePlayerObject(partyNumber).GetComponent<CharacterStatus>().Power.ToString();
        transform.Find("Defense").GetComponent<Text>().text = PartyData.Instance.GetBattlePlayerObject(partyNumber).GetComponent<CharacterStatus>().Defense.ToString();
        transform.Find("PowerWeapon").GetComponent<Text>().text = "(" + PartyData.Instance.GetBattlePlayerObject(partyNumber).GetComponent<CharacterStatus>().PowerWeapon.ToString() + ")";
        transform.Find("DefenseClothing").GetComponent<Text>().text = "(" + PartyData.Instance.GetBattlePlayerObject(partyNumber).GetComponent<CharacterStatus>().DefenseClothing.ToString() + ")";
        transform.Find("Level").GetComponent<Text>().text = "Lv." + PartyData.Instance.GetBattlePlayerObject(partyNumber).GetComponent<CharacterStatus>().CurrentLevel.ToString();
        //transform.Find("MaxLevel").GetComponent<Text>().text = PartyData.Instance.GetBattlePlayerObject(partyNumber).GetComponent<CharacterStatus>()..ToString();
        //Debug.Log(PartyData.Instance.GetBattlePlayerObject(partyNumber).GetComponent<CharacterStatus>().PowerWeapon);
    }

    void UpdateHpText()
    {
        transform.Find("CurrentHp").GetComponent<Text>().text = PartyData.Instance.GetBattlePlayerObject(partyNumber).GetComponent<CharacterStatus>().CurrentHp.ToString();
    }

    void UpdateCpText()
    {
        transform.Find("CurrentCp").GetComponent<Text>().text = PartyData.Instance.GetBattlePlayerObject(partyNumber).GetComponent<CharacterStatus>().CurrentCp.ToString();
    }

    void UpdatePowerText()
    {
        transform.Find("Power").GetComponent<Text>().text = PartyData.Instance.GetBattlePlayerObject(partyNumber).GetComponent<CharacterStatus>().Power.ToString();
    }

    void UpdatePowerWeaponText()
    {
        transform.Find("PowerWeapon").GetComponent<Text>().text = "(" + PartyData.Instance.GetBattlePlayerObject(partyNumber).GetComponent<CharacterStatus>().PowerWeapon.ToString() + ")";
    }

    void UpdateDefenseText()
    {
        transform.Find("Defense").GetComponent<Text>().text = PartyData.Instance.GetBattlePlayerObject(partyNumber).GetComponent<CharacterStatus>().Defense.ToString();
    }

    void UpdateDefenseClothingText()
    {
        transform.Find("DefenseClothing").GetComponent<Text>().text = "(" + PartyData.Instance.GetBattlePlayerObject(partyNumber).GetComponent<CharacterStatus>().DefenseClothing.ToString() + ")";
    }

    public void OnClickButton()
    {
        if(BattleManager.Instance.CurrentBattleState.Value == BattleState.OpenPartyMenu || BattleManager.Instance.CurrentBattleState.Value == BattleState.UsedItem)
        {
            BattleManager.Instance.openingPartyNumber = partyNumber;
            //ステータス詳細表示
            BattleManager.Instance.SetBattleState(BattleState.OpenEquipmentStatus);
        }
        else if(BattleManager.Instance.CurrentBattleState.Value == BattleState.SelectPlayerToUseFood)
        {
            //担当キャラクターに対して選択中のアイテムを使う
            MyItemData.Instance.TakeItem(ItemType.Food, BattleManager.Instance.SelectedItemIndex).GetComponent<ItemController>().Effect(PartyData.Instance.GetBattlePlayerObject(partyNumber).GetComponent<CharacterStatus>());
            BattleManager.Instance.SelectedItemIndex = -1;
            BattleManager.Instance.SetBattleState(BattleState.UsedItem);
            Debug.Log("アイテム使った");
        }
       
    }
}
}

