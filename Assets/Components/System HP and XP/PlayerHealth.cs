using UnityEngine;
using System;
using UnityEditor.AssetImporters;

public class PlayerHealth : Health
{
    [HideInInspector] public MenuPause menuPause;
    
    //public event Action<float> HealthChanged;

    private Shield shieldScr;
    [SerializeField] private GameObject deathPanel;

    private void Start()
    {
        shieldScr = Player.shieldScr;
        menuPause = FindObjectOfType<MenuPause>().GetComponent<MenuPause>();
    }
    
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
        deathPanel.SetActive(true);
    }

    protected override float ProcessDamage(float damage) => damage * Stats.DamageTakingMultiplier;

}