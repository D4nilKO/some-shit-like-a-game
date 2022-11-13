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
}