using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;

namespace Battle
{
    public class EquipmentPageController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SetActive(false);

        BattleManager.Instance.CurrentBattleState
            .DistinctUntilChanged()
            .Where(x => x == BattleState.OpenEquipmentStatus)
            .Subscribe(_ => OpenPanel(BattleManager.Instance.openingPartyNumber));
    }

    public void OpenPanel(int partyNumber)
    {
        SetActive(true);
        GeneratePanel(partyNumber);
    }

    void SetActive(bool active)
    {
        this.gameObject.SetActive(active);
    }

    void GeneratePanel(int partyNumber)
    {
        if(PartyData.Instance.GetPlayer(partyNumber).GetComponent<PlayerEquipment>().Weapon != null)
        {
            transform.Find("WeaponImage").GetComponent<Image>().sprite = PartyData.Instance.GetBattlePlayer(partyNumber).GetComponent<PlayerEquipment>().Weapon.GetComponent<SpriteRenderer>().sprite;
            transform.Find("WeaponName").GetComponent<Text>().text = PartyData.Instance.GetBattlePlayer(partyNumber).GetComponent<PlayerEquipment>().Weapon.GetComponent<ItemController>().ItemName;
            transform.Find("WeaponEffect").GetComponent<Text>().text = PartyData.Instance.GetBattlePlayer(partyNumber).GetComponent<PlayerEquipment>().Weapon.GetComponent<ItemController>().itemEffect;
            transform.Find("WeaponDetail").GetComponent<Text>().text = PartyData.Instance.GetBattlePlayer(partyNumber).GetComponent<PlayerEquipment>().Weapon.GetComponent<ItemController>().itemDetail;
        }
        else
        {
            transform.Find("WeaponImage").GetComponent<Image>().sprite = null;
            transform.Find("WeaponName").GetComponent<Text>().text = "";
            transform.Find("WeaponEffect").GetComponent<Text>().text = "";
            transform.Find("WeaponDetail").GetComponent<Text>().text = "";
        }

        if(PartyData.Instance.GetPlayer(partyNumber).GetComponent<PlayerEquipment>().Clothing != null)
        {
            transform.Find("ClothingImage").GetComponent<Image>().sprite = PartyData.Instance.GetBattlePlayer(partyNumber).GetComponent<PlayerEquipment>().Clothing.GetComponent<SpriteRenderer>().sprite;
            transform.Find("ClothingName").GetComponent<Text>().text = PartyData.Instance.GetBattlePlayer(partyNumber).GetComponent<PlayerEquipment>().Clothing.GetComponent<ItemController>().ItemName;
            transform.Find("ClothingEffect").GetComponent<Text>().text = PartyData.Instance.GetBattlePlayer(partyNumber).GetComponent<PlayerEquipment>().Clothing.GetComponent<ItemController>().itemEffect;
            transform.Find("ClothingDetail").GetComponent<Text>().text = PartyData.Instance.GetBattlePlayer(partyNumber).GetComponent<PlayerEquipment>().Clothing.GetComponent<ItemController>().itemDetail;
        }
        else
        {
            transform.Find("ClothingImage").GetComponent<Image>().sprite = null;
            transform.Find("ClothingName").GetComponent<Text>().text = "";
            transform.Find("ClothingEffect").GetComponent<Text>().text = "";
            transform.Find("ClothingDetail").GetComponent<Text>().text = "";
        }
    }
}

}
