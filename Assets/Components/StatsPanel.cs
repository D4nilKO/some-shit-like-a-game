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
    [SerializeField] private TextMeshProUGUI damageTakingMultiplierText;
    [SerializeField] private TextMeshProUGUI xpGainMultiplierText;

    private void OnEnable()
    {
        ShowStats();
    }

    private void ShowStats()
    {
        TextNullCheck(enemyKilledText, Stats.EnemyKilled.ToString(CultureInfo.CurrentCulture));
        TextNullCheck(curHpText, Player.playerHealthScr.CurrentHealth.ToString(CultureInfo.CurrentCulture));
        TextNullCheck(maxHpText, Player.playerHealthScr.MaxHealth.ToString(CultureInfo.CurrentCulture));
        TextNullCheck(skillDamageMultiplierText,
            (Stats.SkillDamageMultiplier * 100f).ToString(CultureInfo.CurrentCulture) + "%");
        TextNullCheck(damageTakingMultiplierText,
            (Stats.DamageTakingMultiplier * 100f).ToString(CultureInfo.CurrentCulture) + "%");
        TextNullCheck(xpGainMultiplierText,
            (Stats.XpGainMultiplier * 100f).ToString(CultureInfo.CurrentCulture) + "%");
    }

    private void TextNullCheck(TextMeshProUGUI textObject, string value)
    {
        try
        {
            textObject.text = value;
        }
        catch (NullReferenceException ex)
        {
            Debug.Log("нуль text");
        }
    }
}