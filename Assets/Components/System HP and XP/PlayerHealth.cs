using UnityEngine;
using System;
using UnityEditor.AssetImporters;

public class PlayerHealth : Health
{
    [HideInInspector] public MenuPause menuPause;
    //public float curHp;

    //public event Action<float> HealthChanged;

    //public float maxHp = 100;

    private Shield shieldScr;

    private void Start()
    {
        shieldScr = Player.shieldScr;
        menuPause = FindObjectOfType<MenuPause>().GetComponent<MenuPause>();
    }

    // public void IncreaseMaxHp(float deltaHp, bool isAddition)
    // {
    //     bool isMaxHp = curHp >= maxHp;
    //
    //     if (isAddition)
    //     {
    //         maxHp += deltaHp;
    //     }
    //     else
    //     {
    //         maxHp *= deltaHp;
    //     }
    //
    //     if (isMaxHp)
    //     {
    //         RecountHp(maxHp);
    //     }
    //
    //     var cuHpPercent = curHp / maxHp;
    //     HealthChanged?.Invoke(cuHpPercent);
    // }
    //
    // public void RecountHp(float deltaHp)
    // {
    //     deltaHp *= Stats.DamageTakingMultiplier;
    //
    //     if (deltaHp < 0 && shieldScr.isShieldEnable && shieldScr.Attribute.lvl != 0)
    //     {
    //         shieldScr.RecountEndurance(deltaHp);
    //         StopAllCoroutines();
    //         StartCoroutine(shieldScr.MainTimer());
    //     }
    //     else
    //     {
    //         curHp += deltaHp;
    //         if (curHp >= maxHp)
    //         {
    //             curHp = maxHp;
    //             HealthChanged?.Invoke(1);
    //         }
    //         else
    //         {
    //             var cuHpPercent = curHp / maxHp;
    //             HealthChanged?.Invoke(cuHpPercent);
    //         }
    //
    //         if (curHp <= 0)
    //         {
    //             HealthChanged?.Invoke(0);
    //             Invoke(nameof(Lose), 1f);
    //         }
    //     }
    // }


    public new void ApplyDamage(float damage)
    {
        if (shieldScr.IsShieldEnable)
        {
            shieldScr.ApplyDamageToShield(damage);
            StopAllCoroutines();
            StartCoroutine(shieldScr.MainTimer());
        }
        else base.ApplyDamage(damage);

        if (!IsAlive)
        {
            Lose();
        }
    }

    private void Lose()
    {
        menuPause.RestartScene();
    }

    protected override float ProcessDamage(float damage) => damage * Stats.DamageTakingMultiplier;

}