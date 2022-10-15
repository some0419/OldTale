using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;

namespace Battle
{
    public class StatusPanelController : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            SetActive(false);

            BattleManager.Instance.CurrentBattleState
                .DistinctUntilChanged()
                .Where(x => x == BattleState.OpenEquipmentStatus)
                .Subscribe(_ => OpenPanel(BattleManager.Instance.openingPartyNumber));

            BattleManager.Instance.CurrentBattleState
                .DistinctUntilChanged()
                .Where(x => x != BattleState.OpenEquipmentStatus)
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
        transform.Find("Image").GetComponent<Image>().sprite = PartyData.Instance.GetBattlePlayerObject(partyNumber).GetComponent<SpriteRenderer>().sprite;
        transform.Find("PlayerName").GetComponent<Text>().text = PartyData.Instance.GetBattlePlayerObject(partyNumber).GetComponent<CharacterStatus>().name;
        //transform.Find("MaxHp").GetComponent<Text>().text = PartyData.Instance.GetPlayer(partyNumber).GetComponent<CharacterStatus>().MaxHp.ToString();
        //transform.Find("MaxCp").GetComponent<Text>().text = PartyData.Instance.GetPlayer(partyNumber).GetComponent<CharacterStatus>().MaxCp.ToString();
        //transform.Find("CurrentHp").GetComponent<Text>().text = PartyData.Instance.GetPlayer(partyNumber).GetComponent<CharacterStatus>().CurrentHp.ToString();
        //transform.Find("CurrentCp").GetComponent<Text>().text = PartyData.Instance.GetPlayer(partyNumber).GetComponent<CharacterStatus>().CurrentCp.ToString();
        transform.Find("Power").GetComponent<Text>().text = PartyData.Instance.GetBattlePlayerObject(partyNumber).GetComponent<CharacterStatus>().Power.ToString();
        transform.Find("Defense").GetComponent<Text>().text = PartyData.Instance.GetBattlePlayerObject(partyNumber).GetComponent<CharacterStatus>().Defense.ToString();
        transform.Find("PowerWeapon").GetComponent<Text>().text = "(" + PartyData.Instance.GetBattlePlayerObject(partyNumber).GetComponent<CharacterStatus>().PowerWeapon.ToString() + ")";
        transform.Find("DefenseClothing").GetComponent<Text>().text = "(" + PartyData.Instance.GetBattlePlayerObject(partyNumber).GetComponent<CharacterStatus>().DefenseClothing.ToString() + ")";
        transform.Find("Level").GetComponent<Text>().text = "Lv." + PartyData.Instance.GetBattlePlayerObject(partyNumber).GetComponent<CharacterStatus>().CurrentLevel.ToString();

    }

    public void OnClickBackButton()
        {
            BattleManager.Instance.SetBattleState(BattleState.OpenPartyMenu);
        }
}
}

