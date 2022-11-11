using UnityEngine;
using System;

public class SystemHp : MonoBehaviour
{
    [HideInInspector] public Main main;
    public float curHp;

    public event Action<float> HealthChanged;

    public float maxHp = 100;

    private Shield shieldScr;

    private void Start()
    {
        curHp = maxHp;
        shieldScr = Player.shieldScr;
        main = FindObjectOfType<Main>();
    }

    public void IncreaseMaxHp(float deltaHp, bool isAddition)
    {
        bool isMaxHp = curHp >= maxHp;

        if (isAddition)
        {
            maxHp += deltaHp;
        }
        else
        {
            maxHp *= deltaHp;
        }

        if (isMaxHp)
        {
            RecountHp(maxHp);
        }
    }

    public void RecountHp(float deltaHp)
    {
        if (deltaHp < 0 && shieldScr.isShieldEnable && shieldScr.Attribute.lvl != 0)
        {
            shieldScr.RecountEndurance(deltaHp);
            StopAllCoroutines();
            StartCoroutine(shieldScr.MainTimer());
        }
        else
        {
            curHp += deltaHp;
            if (curHp >= maxHp)
            {
                curHp = maxHp;
                HealthChanged?.Invoke(1);
            }
            else
            {
                var cuHpPercent = curHp / maxHp;
                HealthChanged?.Invoke(cuHpPercent);
            }

            if (curHp <= 0)
            {
                HealthChanged?.Invoke(0);
                Invoke(nameof(Lose), 1f);
            }
        }
    }

    private void Lose()
    {
        main.GetComponent<Main>().Lose();
    }
}