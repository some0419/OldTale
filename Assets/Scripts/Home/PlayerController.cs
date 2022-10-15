using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class PlayerController : MonoBehaviour
{
    //ホームシーンのプレイヤーコントローラー

    //Rigidbody
    Rigidbody2D rb;

    //入力の方向を保持する変数
    Vector3 inputDirection;

    //歩くスピード
    [SerializeField]
    float moveSpeed = 4f;

    //接触オブジェクトを保持
    Collider2D collision;

    //アクション中はtrue
    public bool action = false;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        
    }

    // Update is called once per frame
    void Update()
    {
        if (HomeManager.Instance.CurrentHomeState.Value == HomeState.None || HomeManager.Instance.CurrentHomeState.Value == HomeState.Encounter)
        {
            //アクション中でなければ移動可能
            inputDirection = new Vector3(
            Input.GetAxis("Horizontal"),
            Input.GetAxis("Vertical"),
            0);
        }
        
        //アクション中でないときのスペースキー押下処理
        if (Input.GetMouseButton(0) && HomeManager.Instance.CurrentHomeState.Value == HomeState.Encounter)
        {
            //何かと接触中かつ接触イメージが表示されていればアクション開始
            if (collision != null)
            {
                //移動ストップ
                rb.velocity = new Vector3(0, 0, 0);

                if (collision.gameObject.name == "NPCEncounter")
                {
                    HomeManager.Instance.SetHomeState(HomeState.Talking);
                }
                else if (collision.gameObject.name == "ToBattleEncounter")
                {
                    HomeManager.Instance.SetHomeState(HomeState.ToBattle);
                }
                else if (collision.gameObject.name == "ToFishingEncounter")
                {
                    HomeManager.Instance.SetHomeState(HomeState.ToFishing);
                }
                else if (collision.gameObject.name == "FoodShopEncounter")
                {
                    ShopManager.Instance.SetShopState(ShopState.ShowFoodShopList);
                    HomeManager.Instance.SetHomeState(HomeState.Shopping);
                }
                else if(collision.gameObject.name == "WeaponShopEncounter")
                {
                    ShopManager.Instance.SetShopState(ShopState.ShowWeaponShopList);
                    HomeManager.Instance.SetHomeState(HomeState.Shopping);
                }
                else if(collision.gameObject.name == "ClothingShopEncounter")
                {
                    ShopManager.Instance.SetShopState(ShopState.ShowClothingShopList);
                    HomeManager.Instance.SetHomeState(HomeState.Shopping);
                }
                else if(collision.gameObject.name == "MaterialShopEncounter")
                {
                    ShopManager.Instance.SetShopState(ShopState.SellMaterial);
                    HomeManager.Instance.SetHomeState(HomeState.Shopping);
                }
            }
        }
        
    }

    private void FixedUpdate()
    {
        if (HomeManager.Instance.CurrentHomeState.Value == HomeState.None || HomeManager.Instance.CurrentHomeState.Value == HomeState.Encounter)
        {
            rb.velocity = inputDirection * moveSpeed;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //接触した相手がエンカウンターを持っていれば
        if (collision.gameObject.tag == "Encounter")
        {
            this.collision = collision;
            HomeManager.Instance.SetHomeState(HomeState.Encounter);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //エンカウンターを持つ相手から離れるときの処理
        if (collision.gameObject.tag == "Encounter")
        {
            this.collision = null;
            HomeManager.Instance.SetHomeState(HomeState.None);
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.tag == "Encounter")
        {
            this.collision = other;
            HomeManager.Instance.SetHomeState(HomeState.Encounter);
        }
    }
}
