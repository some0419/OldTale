using System.Collections.Generic;
using UnityEngine;

public class PartyData : SingletonMonoBehaviour<PartyData>
{
    //シングルトンクラスの唯一のインスタンス
    //private static PartyData instance = new PartyData();

    //インスタンスのプロパティ(読み取り専用)
    /*public static PartyData Instance
    {
        get
        {
            return instance;
        }
    }*/

    //コンストラクタ
    //外でインスタンスを作成できないようにprivate
    /*private PartyData()
    {
        //TODO: initialization
    }*/

    //パーティー参加中のキャラクター用配列
    [SerializeField]
    private List<GameObject> partyArray = new List<GameObject>(4);

    [SerializeField]
    private List<GameObject> battlePartyArray = new List<GameObject>(4);

    [SerializeField]
    private List<GameObject> battlePartyObjectArray = new List<GameObject>(4);

    //生存プレイヤーの数
    public int alivePlayerCount = 4;

    //bool addable = true;

    void Awake()
    {
        base.Awake();

        //なんとかManager的なSceneを跨いでこのGameObjectを有効にしたい場合は
        //↓コメントアウトを外す
        DontDestroyOnLoad(this.gameObject);

        GenerateBattlePartyArray();
    }

    //番号に応じたプレイヤー取得
    public GameObject GetPlayer(int partyNumber)
    {
        return partyArray[partyNumber];
    }

    //指定の場所に任意のプレイヤーを編成
    public void SetPlayer(GameObject player, int partyNumber)
    {
        partyArray[partyNumber] = player;
    }

    public GameObject GetBattlePlayer(int partyNumber)
    {
        return battlePartyArray[partyNumber];
    }

    public GameObject GetBattlePlayerObject(int partyNumber)
    {
        return battlePartyObjectArray[partyNumber];
    }

    public void SetBattlePlayer(GameObject player, int partyNumber)
    {
        battlePartyArray[partyNumber] = player;
    }

    public void SetBattlePlayerObject(GameObject player, int partyNumber)
    {
        battlePartyObjectArray[partyNumber] = player;
    }

    //パーティー配列取得
    public List<GameObject> GetParty()
    {
        return partyArray;
    }

    public List<GameObject> GetBattleParty()
    {
        return battlePartyArray;
    }

    //プレイヤーのプレハブデータをインスタンス化
    public void GenerateBattlePartyArray()
    {
        for(int i = 0; i < GetParty().Count; i++)
        {
            SetBattlePlayer(GetPlayer(i), i);
        }
    }

    //バトル終了時など、プレイヤーオブジェクトの情報をデータに反映する際に呼び出す
    public void SetObjectToBattleArray()
    {
        Debug.Log(GetBattleParty().Count);
        for(int i = 0; i < GetBattleParty().Count; i++)
        {
            Debug.Log(GetBattlePlayerObject(i).GetComponent<CharacterStatus>().CurrentHp.Value);
            GetBattlePlayer(i).GetComponent<CharacterStatus>().CurrentHp.Value = GetBattlePlayerObject(i).GetComponent<CharacterStatus>().CurrentHp.Value;
        }
    }

    //キャラをパーティーにセット
    /*
    public void SetParty(GameObject player)
    {
        for (int i = 0; i < playerParty.Length; i++)
        {
            //already in party or not
            if (partyArray[i].GetComponent<PlayerCont>().number == player.GetComponent<PlayerCont>().number)
            {
                addable = false;
                partyArray[i] = null;
                break;
            }
            else
            {
                addable = true;
            }
        }

        //if my party have some empty, we can set this player
        for (int i = 0; i < partyArray.Length; i++)
        {
            if (partyArray[i] == null)
            {
                if (addable)
                {
                    partyArray[i] = player;
                    break;
                }
            }
            
        }
        
    }
    */
}
