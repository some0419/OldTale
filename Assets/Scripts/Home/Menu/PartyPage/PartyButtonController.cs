using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace Home
{
    public class PartyButtonController : MonoBehaviour
{
    [SerializeField]
    int partyNumber = 0;

    // Start is called before the first frame update
    void Awake()
    {
        HomeManager.Instance.CurrentHomeState
            .DistinctUntilChanged()
            .Where(x => x == HomeState.OpenPartyPage || x == HomeState.UseFood || x == HomeState.UseWeapon || x == HomeState.UseClothing)
            .Subscribe(_ => GenerateButton());
    }

    void Start()
    {
        PartyData.Instance.GetBattlePlayer(partyNumber).GetComponent<CharacterStatus>().CurrentHp
            .DistinctUntilChanged()
            .Subscribe(_ => UpdateHpText());

        PartyData.Instance.GetBattlePlayer(partyNumber).GetComponent<CharacterStatus>().CurrentCp
            .DistinctUntilChanged()
            .Subscribe(_ => UpdateCpText());

        PartyData.Instance.GetBattlePlayer(partyNumber).GetComponent<CharacterStatus>().Power
            .DistinctUntilChanged()
            .Subscribe(_ => UpdatePowerText());

        PartyData.Instance.GetBattlePlayer(partyNumber).GetComponent<CharacterStatus>().Defense
            .DistinctUntilChanged()
            .Subscribe(_ => UpdateDefenseText());
        
        PartyData.Instance.GetBattlePlayer(partyNumber).GetComponent<CharacterStatus>().PowerWeapon
            .DistinctUntilChanged()
            .Subscribe(_ => UpdatePowerWeaponText());

        PartyData.Instance.GetBattlePlayer(partyNumber).GetComponent<CharacterStatus>().DefenseClothing
            .DistinctUntilChanged()
            .Subscribe(_ => UpdateDefenseClothingText());
    }

    void GenerateButton()
    {
        transform.Find("Image").GetComponent<Image>().sprite = PartyData.Instance.GetBattlePlayer(partyNumber).GetComponent<SpriteRenderer>().sprite;
        transform.Find("PlayerName").GetComponent<Text>().text = PartyData.Instance.GetBattlePlayer(partyNumber).GetComponent<CharacterStatus>().name;
        transform.Find("MaxHp").GetComponent<Text>().text = PartyData.Instance.GetBattlePlayer(partyNumber).GetComponent<CharacterStatus>().MaxHp.ToString();
        transform.Find("MaxCp").GetComponent<Text>().text = PartyData.Instance.GetBattlePlayer(partyNumber).GetComponent<CharacterStatus>().MaxCp.ToString();
        transform.Find("CurrentHp").GetComponent<Text>().text = PartyData.Instance.GetBattlePlayer(partyNumber).GetComponent<CharacterStatus>().CurrentHp.ToString();
        transform.Find("CurrentCp").GetComponent<Text>().text = PartyData.Instance.GetBattlePlayer(partyNumber).GetComponent<CharacterStatus>().CurrentCp.ToString();
        transform.Find("Power").GetComponent<Text>().text = PartyData.Instance.GetBattlePlayer(partyNumber).GetComponent<CharacterStatus>().Power.ToString();
        transform.Find("Defense").GetComponent<Text>().text = PartyData.Instance.GetBattlePlayer(partyNumber).GetComponent<CharacterStatus>().Defense.ToString();
        transform.Find("PowerWeapon").GetComponent<Text>().text = "(" + PartyData.Instance.GetBattlePlayer(partyNumber).GetComponent<CharacterStatus>().PowerWeapon.ToString() + ")";
        transform.Find("DefenseClothing").GetComponent<Text>().text = "(" + PartyData.Instance.GetBattlePlayer(partyNumber).GetComponent<CharacterStatus>().DefenseClothing.ToString() + ")";
        transform.Find("Level").GetComponent<Text>().text = "Lv." + PartyData.Instance.GetBattlePlayer(partyNumber).GetComponent<CharacterStatus>().CurrentLevel.ToString();
        //transform.Find("MaxLevel").GetComponent<Text>().text = PartyData.Instance.GetBattlePlayer(partyNumber).GetComponent<CharacterStatus>()..ToString();
        //Debug.Log(PartyData.Instance.GetBattlePlayer(partyNumber).GetComponent<CharacterStatus>().PowerWeapon);
    }

    void UpdateHpText()
    {
        transform.Find("CurrentHp").GetComponent<Text>().text = PartyData.Instance.GetBattlePlayer(partyNumber).GetComponent<CharacterStatus>().CurrentHp.ToString();
    }

    void UpdateCpText()
    {
        transform.Find("CurrentCp").GetComponent<Text>().text = PartyData.Instance.GetBattlePlayer(partyNumber).GetComponent<CharacterStatus>().CurrentCp.ToString();
    }

    void UpdatePowerText()
    {
        transform.Find("Power").GetComponent<Text>().text = PartyData.Instance.GetBattlePlayer(partyNumber).GetComponent<CharacterStatus>().Power.ToString();
    }

    void UpdatePowerWeaponText()
    {
        transform.Find("PowerWeapon").GetComponent<Text>().text = "(" + PartyData.Instance.GetBattlePlayer(partyNumber).GetComponent<CharacterStatus>().PowerWeapon.ToString() + ")";
    }

    void UpdateDefenseText()
    {
        transform.Find("Defense").GetComponent<Text>().text = PartyData.Instance.GetBattlePlayer(partyNumber).GetComponent<CharacterStatus>().Defense.ToString();
    }

    void UpdateDefenseClothingText()
    {
        transform.Find("DefenseClothing").GetComponent<Text>().text = "(" + PartyData.Instance.GetBattlePlayer(partyNumber).GetComponent<CharacterStatus>().DefenseClothing.ToString() + ")";
    }

    public void OnClickButton()
    {
        if(HomeManager.Instance.CurrentHomeState.Value == HomeState.OpenPartyPage || HomeManager.Instance.CurrentHomeState.Value == HomeState.UsedItem)
        {
            HomeManager.Instance.openingPartyNumber = partyNumber;
            //ステータス詳細表示
            HomeManager.Instance.SetHomeState(HomeState.OpenEquipmentStatus);
        }
        else if(HomeManager.Instance.CurrentHomeState.Value == HomeState.UseFood)
        {
            //担当キャラクターに対して選択中のアイテムを使う
            MyItemData.Instance.TakeItem(ItemType.Food, HomeManager.Instance.SelectedItemIndex).GetComponent<ItemController>().Effect(PartyData.Instance.GetBattlePlayer(partyNumber).GetComponent<CharacterStatus>());
            HomeManager.Instance.SelectedItemIndex = -1;
            HomeManager.Instance.SetHomeState(HomeState.UsedItem);
            Debug.Log("アイテム使った");
        }
        else if(HomeManager.Instance.CurrentHomeState.Value == HomeState.UseWeapon)
        {
            //担当キャラクターに対して選択中のアイテムを使う
            PartyData.Instance.GetBattlePlayer(partyNumber).GetComponent<PlayerEquipment>().EquipWeapon(MyItemData.Instance.TakeItem(ItemType.Weapon, HomeManager.Instance.SelectedItemIndex));
            HomeManager.Instance.SelectedItemIndex = -1;
            HomeManager.Instance.SetHomeState(HomeState.UsedItem);
        }
        else if(HomeManager.Instance.CurrentHomeState.Value == HomeState.UseClothing)
        {
            PartyData.Instance.GetBattlePlayer(partyNumber).GetComponent<PlayerEquipment>().EquipClothing(MyItemData.Instance.TakeItem(ItemType.Clothing, HomeManager.Instance.SelectedItemIndex));
            HomeManager.Instance.SelectedItemIndex = -1;
            HomeManager.Instance.SetHomeState(HomeState.UsedItem);
        }
    }
}

}
