using System;
using System.ComponentModel;
using System.Globalization;
using TMPro;
using UnityEngine;
using Object = UnityEngine.Object;

public class StatsPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI enemyKilledText;
    [SerializeField] private TextMeshProUGUI curHpText;
    [SerializeField] private TextMeshProUGUI curHpText1;
    [SerializeField] private TextMeshProUGUI maxHpText;
    [SerializeField] private TextMeshProUGUI skillDamageMultiplierText;
    [SerializeField] private TextMeshProUGUI damageTakingMultiplierText;
    [SerializeField] private TextMeshProUGUI xpGainMultiplierText;
    
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TimeManager timeManagerScr;
    private SurviveTimer timer;

    private void Start()
    {
        timer = timeManagerScr.gameTime;
        curHpText = null;
    }

    private void OnEnable()
    {
        ShowStats();
    }

    private void ShowStats()
    {
        NullCheckAndSetText(curHpText1, " ");
        print(curHpText1);
        
        NullCheckAndSetText(curHpText, " ");
        print(curHpText);
        
        NullCheckAndSetText(enemyKilledText, Stats.EnemyKilled.ToString(CultureInfo.CurrentCulture));
        //NullCheckAndSetText(curHpText, Player.playerHealthScr.CurrentHealth.ToString(CultureInfo.CurrentCulture));
        //NullCheckAndSetText(maxHpText, Player.playerHealthScr.MaxHealth.ToString(CultureInfo.CurrentCulture));
        NullCheckAndSetText(skillDamageMultiplierText,
            (Stats.SkillDamageMultiplier * 100f).ToString(CultureInfo.CurrentCulture) + "%");
        NullCheckAndSetText(damageTakingMultiplierText,
            (Stats.DamageTakingMultiplier * 100f).ToString(CultureInfo.CurrentCulture) + "%");
        NullCheckAndSetText(xpGainMultiplierText,
            (Stats.XpGainMultiplier * 100f).ToString(CultureInfo.CurrentCulture) + "%");
        NullCheckAndSetText(timerText, timer.FormattedTime());
        
    }

    private void NullCheckAndSetText(TextMeshProUGUI textObject, string value)
    {
        if (textObject == null)
        {
            print("нуль");
        }
        else
            textObject.text = value;
    }
}