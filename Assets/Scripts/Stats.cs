using System;
using UnityEngine;

public static class Stats
{
    //public static event Action EnemyKilledEvent;
    private static int _enemyKilled;

    public static int EnemyKilled
    {
        get => _enemyKilled;
        set
        {
            if (value < 0) throw new ArgumentOutOfRangeException(nameof(value));
            _enemyKilled = value;

            //EnemyKilledEvent?.Invoke();
        }
    }

    private static float _startSkillDamageMultiplier = 1f;

    public static float StartSkillDamageMultiplier
    {
        get => _startSkillDamageMultiplier;
        set
        {
            if (value < 0) throw new ArgumentOutOfRangeException(nameof(value));
            _startSkillDamageMultiplier = value;
        }
    }

    private static float _skillDamageMultiplier = StartSkillDamageMultiplier;

    public static float SkillDamageMultiplier
    {
        get => _skillDamageMultiplier;
        set
        {
            if (value < 0) throw new ArgumentOutOfRangeException(nameof(value));
            _skillDamageMultiplier = value;
        }
    }

    private static float _startDamageTakingMultiplier = 1f;

    public static float StartDamageTakingMultiplier
    {
        get => _startDamageTakingMultiplier;
        set
        {
            if (value < 0) throw new ArgumentOutOfRangeException(nameof(value));
            _startDamageTakingMultiplier = value;
        }
    }

    private static float _damageTakingMultiplier = StartDamageTakingMultiplier;

    public static float DamageTakingMultiplier
    {
        get => _damageTakingMultiplier;
        set
        {
            if (value < 0) throw new ArgumentOutOfRangeException(nameof(value));
            _damageTakingMultiplier = value;
        }
    }

    private static float _startXpGainMultiplier = 1f;

    public static float StartXpGainMultiplier
    {
        get => _startXpGainMultiplier;
        set
        {
            if (value < 0) throw new ArgumentOutOfRangeException(nameof(value));
            _startXpGainMultiplier = value;
        }
    }

    private static float _xpGainMultiplier = StartXpGainMultiplier;

    public static float XpGainMultiplier
    {
        get => _xpGainMultiplier;
        set
        {
            if (value < 0) throw new ArgumentOutOfRangeException(nameof(value));
            _xpGainMultiplier = value;
        }
    }
}