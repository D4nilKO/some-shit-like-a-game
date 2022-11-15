﻿using System;
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
        NullCheckAndSetText(curHpText, Player.systemHpScr.curHp.ToString(CultureInfo.CurrentCulture));
        NullCheckAndSetText(maxHpText, Player.systemHpScr.maxHp.ToString(CultureInfo.CurrentCulture));
        NullCheckAndSetText(skillDamageMultiplierText,
            (Stats.SkillDamageMultiplier * 100f).ToString(CultureInfo.CurrentCulture) + "%");
        NullCheckAndSetText(damageTakingMultiplierText,
            (Stats.DamageTakingMultiplier * 100f).ToString(CultureInfo.CurrentCulture) + "%");
        NullCheckAndSetText(xpGainMultiplierText,
            (Stats.XpGainMultiplier * 100f).ToString(CultureInfo.CurrentCulture) + "%");
    }

    private void NullCheckAndSetText(TextMeshProUGUI textObj, String value)
    {
        if (textObj != null)
        {
            textObj.text = value;
        }
        else print("null");
    }
}