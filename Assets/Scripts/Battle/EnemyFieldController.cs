using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class EnemyFieldController : MonoBehaviour
{
    [SerializeField]
    int partyNumber = 0;

    // Start is called before the first frame update
    void Awake()
    {
        GenerateEnemy(partyNumber);
    }


    void GenerateEnemy(int partyNumber)
    {
        if(EnemyData.Instance.GetPlayer(partyNumber) != null)
        {
            EnemyData.Instance.SetBattlePlayer(Instantiate(EnemyData.Instance.GetPlayer(partyNumber), transform.position, Quaternion.identity), partyNumber);
        }
    }
}
