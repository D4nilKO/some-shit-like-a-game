using System;
using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField, Min(0)] private float damageMultiplier = 1f;

    public event Action<float> HealthChangedEvent;
    public event Action DamageApplied;
    
    public float startTimeToIgnoreDamage = 0.5f;
    [SerializeField]public float timeToIgnoreDamage;

    public bool IgnoreDamage { get; set; }
    public bool IgnoreHeal { get; set; }
    public bool IsAlive => CurrentHealth > 0;

    [SerializeField] private float currentHealth;
    [SerializeField] private float maxHealth = 100f;

    public float CurrentHealth
    {
        get => currentHealth;
        private set
        {
            currentHealth = Mathf.Clamp(value, 0f, maxHealth);
            var healthPercent = CurrentHealth / MaxHealth;
            HealthChangedEvent?.Invoke(healthPercent);
        }
    }

    public float MaxHealth
    {
        get => maxHealth;
        private set => maxHealth = value;
    }

    private void Awake()
    {
        UpdateHealthToMax();
    }

    public void ApplyInvulnerability()
    {
        StopCoroutine(Invulnerability(startTimeToIgnoreDamage));
        StartCoroutine(Invulnerability(startTimeToIgnoreDamage));
    }

    public void ApplyInvulnerability(float startTimeToIgnoreDamage)
    {
        StopCoroutine(Invulnerability(startTimeToIgnoreDamage));
        StartCoroutine(Invulnerability(startTimeToIgnoreDamage));
    }

    private IEnumerator Invulnerability(float startTimeToIgnoreDamage)
    {
        IgnoreDamage = true;

        timeToIgnoreDamage = startTimeToIgnoreDamage;
        while (timeToIgnoreDamage > 0)
        {
            timeToIgnoreDamage -= Time.deltaTime;
            yield return null;
        }

        IgnoreDamage = false;
    }

    public void Heal(float value)
    {
        if (IgnoreHeal)
            return;

        if (value < 0)
            throw new ArgumentOutOfRangeException(nameof(value));

        CurrentHealth += value;
    }

    public virtual void ApplyDamage(float damage)
    {
        if (IgnoreDamage)
            return;

        if (damage < 0)
            throw new ArgumentOutOfRangeException(nameof(damage));

        var totalDamage = ProcessDamage(damage);

        if (totalDamage < 0)
            throw new ArgumentOutOfRangeException(nameof(totalDamage));

        CurrentHealth -= totalDamage;

        DamageApplied?.Invoke();
    }

    public void SetMaxHealth(float maxHealth)
    {
        if (maxHealth <= 0)
            throw new ArgumentOutOfRangeException(nameof(maxHealth));

        this.MaxHealth = maxHealth;

        if (CurrentHealth > this.MaxHealth)
        {
            CurrentHealth = this.MaxHealth;
        }
    }

    public void AddMaxHealth(float value)
    {
        if (value <= 0)
            throw new ArgumentOutOfRangeException(nameof(value));
        SetMaxHealth(MaxHealth + value);
    }

    public void MultiplyMaxHealth(float multiplier)
    {
        if (multiplier <= 1)
            throw new ArgumentOutOfRangeException(nameof(multiplier));
        SetMaxHealth(MaxHealth * multiplier);
    }

    public void UpdateHealthToMax()
    {
        CurrentHealth = MaxHealth;
    }

    protected virtual float ProcessDamage(float damage) => damage * damageMultiplier;
}