using System.Collections.Generic;
using UnityEngine;

public class EnemyData : SingletonMonoBehaviour<EnemyData>
{
    //シングルトンクラスの唯一のインスタンス
    //private static EnemyData instance = new EnemyData();

    //インスタンスのプロパティ(読み取り専用)
    /*public static EnemyData Instance
    {
        get
        {
            return instance;
        }
    }*/

    //コンストラクタ
    //外でインスタンスを作成できないようにprivate
    /*private EnemyData()
    {
        //TODO: initialization
    }*/

    //パーティー参加中のキャラクター用配列
    [SerializeField]
    private List<GameObject> enemyArray = new List<GameObject>(4);

    [SerializeField]
    private List<GameObject> battleEnemyArray = new List<GameObject>(4);

    public int aliveEnemyCount = 4;

    public GameObject GetPlayer(int partyNumber)
    {
        return enemyArray[partyNumber];
    }

    public void SetPlayer(GameObject player, int partyNumber)
    {
        enemyArray[partyNumber] = player;
    }

    public GameObject GetBattlePlayer(int partyNumber)
    {
        return battleEnemyArray[partyNumber];
    }

    public void SetBattlePlayer(GameObject player, int partyNumber)
    {
        battleEnemyArray[partyNumber] = player;
    }

    public List<GameObject> GetParty()
    {
        return enemyArray;
    }

    public List<GameObject> GetBattleParty()
    {
        return battleEnemyArray;
    }
}
