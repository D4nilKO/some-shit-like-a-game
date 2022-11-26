using System;
using System.Globalization;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;

public class StatsPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI enemyKilledText;
    [SerializeField] private TextMeshProUGUI curHpText;
    [SerializeField] private TextMeshProUGUI maxHpText;
    [SerializeField] private TextMeshProUGUI skillDamageMultiplierText;
    [SerializeField] private TextMeshProUGUI damageTakingMultiplierText;
    [SerializeField] private TextMeshProUGUI xpGainMultiplierText;

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
        ShowStats();
    }

    private void ShowStats()
    {
        NullCheckAndSetText(enemyKilledText, Stats.EnemyKilled.ToString(CultureInfo.CurrentCulture));
        NullCheckAndSetText(curHpText, Player.playerHealthScr.CurrentHealth.ToString(CultureInfo.CurrentCulture));
        NullCheckAndSetText(maxHpText, Player.playerHealthScr.MaxHealth.ToString(CultureInfo.CurrentCulture));
        NullCheckAndSetText(skillDamageMultiplierText,
            (Stats.SkillDamageMultiplier * 100f).ToString(CultureInfo.CurrentCulture) + "%");
        NullCheckAndSetText(damageTakingMultiplierText,
            (Stats.DamageTakingMultiplier * 100f).ToString(CultureInfo.CurrentCulture) + "%");
        NullCheckAndSetText(xpGainMultiplierText,
            (Stats.XpGainMultiplier * 100f).ToString(CultureInfo.CurrentCulture) + "%");
    }

    private void NullCheckAndSetText([NotNull] TextMeshProUGUI textObj, string value)
    {
        if (textObj != null)
        {
            textObj.text = value;
        }
        else throw new ArgumentNullException(nameof(textObj));
    }
}