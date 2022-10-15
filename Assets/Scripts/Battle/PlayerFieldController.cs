using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class PlayerFieldController : MonoBehaviour
{
    [SerializeField]
    int partyNumber = 0;

    public GameObject playerObject;

    // Start is called before the first frame update
    void Awake()
    {
        GeneratePlayer(partyNumber);
    }

    private void GeneratePlayer(int partyNumber)
    {
        playerObject = Instantiate(PartyData.Instance.GetBattlePlayer(partyNumber), transform.position, Quaternion.identity);
        //PartyData.Instance.SetBattlePlayer(playerObject, partyNumber);
        PartyData.Instance.SetBattlePlayerObject(playerObject, partyNumber);
    }
}
