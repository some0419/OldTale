using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;

public class MoneyPanelController : MonoBehaviour
{
    [SerializeField]
    Text moneyText;

    // Start is called before the first frame update
    void Start()
    {
        moneyText = transform.Find("MyMoneyText").gameObject.GetComponent<Text>();

        SetActive(false);

        HomeManager.Instance.CurrentHomeState
            .DistinctUntilChanged()
            .Where(x => x == HomeState.Shopping || x == HomeState.OpenMenu)
            .Subscribe(_ => SetActive(true));

        HomeManager.Instance.CurrentHomeState
            .DistinctUntilChanged()
            .Where(x => x != HomeState.Shopping && x != HomeState.OpenMenu)
            .Subscribe(_ => SetActive(false));

        MyItemData.Instance.MyMoney
            .DistinctUntilChanged()
            .Subscribe(x => UpdateMoneyText(x));
    }

    void UpdateMoneyText(int money)
    {
        moneyText.text = money.ToString();
        Debug.Log("おかね表示こうしん");
    }

    void SetActive(bool active)
    {
        this.gameObject.SetActive(active);
    }
}
