using System;
using System.Globalization;
using TMPro;
using UnityEngine;

public class StatsPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI enemyKilledText;
    [SerializeField] private TextMeshProUGUI curHpText;
    [SerializeField] private TextMeshProUGUI maxHpText;
    [SerializeField] private TextMeshProUGUI skillDamageMultiplierText;
    [SerializeField] private TextMeshProUGUI armorText;

    private void Awake()
    {
        //Stats.EnemyKilledEvent += OnEnemyKilledChanged;
    }

    private void Start()
    {
    }

    // private void OnDestroy()
    // {
    //     Stats.EnemyKilledEvent -= OnEnemyKilledChanged;
    // }
    //
    // private void OnEnemyKilledChanged()
    // {
    //     enemyKilledText.text = Stats.EnemyKilled.ToString();
    // }

    private void OnEnable()
    {
        enemyKilledText.text = Stats.EnemyKilled.ToString(CultureInfo.CurrentCulture);
        curHpText.text = Player.systemHpScr.curHp.ToString(CultureInfo.CurrentCulture);
        maxHpText.text = Player.systemHpScr.maxHp.ToString(CultureInfo.CurrentCulture);
        skillDamageMultiplierText.text = (Stats.SkillDamageMultiplier * 100f).ToString(CultureInfo.CurrentCulture) + "%";
    }
}