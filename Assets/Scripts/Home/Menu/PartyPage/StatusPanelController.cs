using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;

public class StatusPanelController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SetActive(false);

        HomeManager.Instance.CurrentHomeState
            .DistinctUntilChanged()
            .Where(x => x == HomeState.OpenEquipmentStatus)
            .Subscribe(_ => OpenPanel(HomeManager.Instance.openingPartyNumber));

        HomeManager.Instance.CurrentHomeState
            .DistinctUntilChanged()
            .Where(x => x != HomeState.OpenEquipmentStatus)
            .Subscribe(_ => SetActive(false));

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
        transform.Find("Image").GetComponent<Image>().sprite = PartyData.Instance.GetPlayer(partyNumber).GetComponent<SpriteRenderer>().sprite;
        transform.Find("PlayerName").GetComponent<Text>().text = PartyData.Instance.GetPlayer(partyNumber).GetComponent<CharacterStatus>().name;
        //transform.Find("MaxHp").GetComponent<Text>().text = PartyData.Instance.GetPlayer(partyNumber).GetComponent<CharacterStatus>().MaxHp.ToString();
        //transform.Find("MaxCp").GetComponent<Text>().text = PartyData.Instance.GetPlayer(partyNumber).GetComponent<CharacterStatus>().MaxCp.ToString();
        //transform.Find("CurrentHp").GetComponent<Text>().text = PartyData.Instance.GetPlayer(partyNumber).GetComponent<CharacterStatus>().CurrentHp.ToString();
        //transform.Find("CurrentCp").GetComponent<Text>().text = PartyData.Instance.GetPlayer(partyNumber).GetComponent<CharacterStatus>().CurrentCp.ToString();
        transform.Find("Power").GetComponent<Text>().text = PartyData.Instance.GetPlayer(partyNumber).GetComponent<CharacterStatus>().Power.ToString();
        transform.Find("Defense").GetComponent<Text>().text = PartyData.Instance.GetPlayer(partyNumber).GetComponent<CharacterStatus>().Defense.ToString();
        transform.Find("PowerWeapon").GetComponent<Text>().text = "(" + PartyData.Instance.GetPlayer(partyNumber).GetComponent<CharacterStatus>().PowerWeapon.ToString() + ")";
        transform.Find("DefenseClothing").GetComponent<Text>().text = "(" + PartyData.Instance.GetPlayer(partyNumber).GetComponent<CharacterStatus>().DefenseClothing.ToString() + ")";
        transform.Find("Level").GetComponent<Text>().text = "Lv." + PartyData.Instance.GetPlayer(partyNumber).GetComponent<CharacterStatus>().CurrentLevel.ToString();

    }

    
}
