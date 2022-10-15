using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PartyScriptableObjectData", menuName = "PartyScriptableObjectData")]
public class PartyScriptableObjectData : ScriptableObject
{   
    //パーティー参加中のキャラクター用配列
    [SerializeField]
    private List<GameObject> partyArray = new List<GameObject>(4);

    //番号に応じたプレイヤー取得
    public GameObject GetPlayer(int partyNumber)
    {
        return partyArray[partyNumber];
    }

    //パーティー配列取得
    public List<GameObject> GetParty()
    {
        return partyArray;
    }
}


