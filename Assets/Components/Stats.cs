using System;

public static class Stats
{
    //for load\reload scene
    //often using in class Stats Initialization
    public static void Initialization() 
    {
        EnemyKilled = 0;
        CountOfRevivals = StartCountOfRevivals;
        SkillDamageMultiplier = StartSkillDamageMultiplier;
        DamageTakingMultiplier = StartDamageTakingMultiplier;
        ExperienceGainMultiplier = StartExperienceGainMultiplier;
    }
    
    #region EnemyKilled
    
    private static int _enemyKilled;

    public static int EnemyKilled
    {
        get => _enemyKilled;
        set
        {
            if (value < 0) throw new ArgumentOutOfRangeException(nameof(value));
            _enemyKilled = value;
        }
    }
    
    #endregion

    #region Count Of Revivals

    private static float _startCountOfRevivals = 1f;

    public static float StartCountOfRevivals
    {
        get => _startCountOfRevivals;
        set
        {
            if (value < 0) throw new ArgumentOutOfRangeException(nameof(value));
            _startCountOfRevivals = value;
        }
    }

    private static float _countOfRevivals = StartCountOfRevivals;

    public static float CountOfRevivals
    {
        get => _countOfRevivals;
        set
        {
            if (value < 0) throw new ArgumentOutOfRangeException(nameof(value));
            _countOfRevivals = value;
        }
    }
    
    #endregion
    
    #region Skill Damage Multiplier
    
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

    #endregion

    #region Damage Taking Multiplier

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
    
    #endregion
    
    #region Experience Gain Multiplier

    private static float _startExperienceGainMultiplier = 1f;

    public static float StartExperienceGainMultiplier
    {
        get => _startExperienceGainMultiplier;
        set
        {
            if (value < 0) throw new ArgumentOutOfRangeException(nameof(value));
            _startExperienceGainMultiplier = value;
        }
    }

    private static float _experienceGainMultiplier = StartExperienceGainMultiplier;

    public static float ExperienceGainMultiplier
    {
        get => _experienceGainMultiplier;
        set
        {
            if (value < 0) throw new ArgumentOutOfRangeException(nameof(value));
            _experienceGainMultiplier = value;
        }
    }
    
    #endregion
}