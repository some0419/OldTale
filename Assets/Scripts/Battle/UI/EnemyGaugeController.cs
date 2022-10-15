using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using DG.Tweening;

public class EnemyGaugeController : MonoBehaviour
{
    //ゲージが最大になるまでの時間
    int gaugeSpeed = 1;

    //ゲージ値
    [SerializeField]
    int gaugeValue = 0;

    //ゲージ最大値
    int gaugeMaxValue = 100;

    //シリンダーUI参照
    Slider gauge;

    //Tweenアニメーション
    Tween gaugeAnimation;

    void Awake()
    {
        SetActive(false);

        gauge = transform.Find("Gauge").gameObject.GetComponent<Slider>();

        BattleManager.Instance.CurrentBattleState
            .DistinctUntilChanged()
            .Where(x => x == BattleState.Gauge_enemy)
            .Subscribe(_ => SetActive(true));

        BattleManager.Instance.CurrentBattleState
            .DistinctUntilChanged()
            .Where(x => x == BattleState.Gauge_enemy)
            .Subscribe(_ => StartGauge());

        BattleManager.Instance.CurrentBattleState
            .DistinctUntilChanged()
            .Where(x => x == BattleState.StopGauge_enemy)
            .Subscribe(_ => StopGauge());

        BattleManager.Instance.CurrentBattleState
            .DistinctUntilChanged()
            .Where(x => x == BattleState.Action_enemy)
            .Subscribe(_ => SetActive(false));
            
    }

    //ゲージ開始
    void StartGauge()
    {
        gauge.value = 0;
        gaugeValue = 0;
        gaugeAnimation = DOTween.To
        (
            () => gaugeValue,
            (x) => gaugeValue = x,
            gaugeMaxValue,
            gaugeSpeed
        )
        .SetLoops(-1, LoopType.Yoyo)
        .SetEase(Ease.InCubic)
        .OnUpdate(() => gauge.value = gaugeValue)
        ;
    }

    //ゲージストップ
    void StopGauge()
    {
        gaugeAnimation.Kill();
        gaugeAnimation = null;
        PlayerPartyManager.Instance.SelectAction(gaugeValue);
    }

    //ゲージの表示非表示
    void SetActive(bool active)
    {
        this.gameObject.SetActive(active);
    }

    public void SetGaugeSpeed(int speed)
    {
        gaugeSpeed = speed;
    }
}
