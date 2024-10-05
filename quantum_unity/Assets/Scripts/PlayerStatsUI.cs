using TMPro;
using UniRx;
using UnityEngine;

public class PlayerStatsUI : MonoBehaviour
{
    [SerializeField] private TMP_Text attackRangeText;
    [SerializeField] private TMP_Text damageText;
    [SerializeField] private TMP_Text speedText;
    [SerializeField] private TMP_Text killCount;

    private void Awake()
    {
        PlayerUpgrader.LocalPlayerInited += Subscribe;
    }

    private void OnDestroy()
    {
        PlayerUpgrader.LocalPlayerInited -= Subscribe;
    }

    private void Subscribe(PlayerUpgrader upgrader)
    {
        upgrader.PlayerDataReactive.Subscribe(x =>
        {
            attackRangeText.SetText("AttackRange: " + x.AttackRange.Value);
            damageText.SetText("Damage: " + x.Damage.Value);
            speedText.SetText("Speed: " + x.Speed.Value);
            killCount.SetText("KillCount: " + x.KillCount);
        }).AddTo(this);
    }
}