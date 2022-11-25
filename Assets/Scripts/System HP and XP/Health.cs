using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField, Min(0)] private float damageMultiplier = 1f;

    public event Action<float> OnHealthChanged;
    public event Action<float> OnDamageApplied;

    public bool IgnoreDamage { get; set; }
    public bool IgnoreHeal { get; set; }
    public bool IsAlive => CurrentHealth > 0;

    public float CurrentHealth
    {
        get => _currentHealth;
        private set
        {
            _currentHealth = Mathf.Clamp(value, 0f, _maxHealth);
            OnHealthChanged?.Invoke(_currentHealth);
        }
    }

    private float _maxHealth = 100f;
    private float _currentHealth;

    private void Awake()
    {
        UpdateHealthToMax();
    }

    public void Heal(float value)
    {
        if (IgnoreHeal)
            return;

        if (value < 0)
            throw new ArgumentOutOfRangeException(nameof(value));

        CurrentHealth += value;
    }

    public void ApplyDamage(float damage)
    {
        if (IgnoreDamage)
            return;

        if (damage < 0)
            throw new ArgumentOutOfRangeException(nameof(damage));

        var totalDamage = ProcessDamage(damage);

        if (totalDamage < 0)
            throw new ArgumentOutOfRangeException(nameof(totalDamage));

        CurrentHealth -= totalDamage;

        OnDamageApplied?.Invoke(totalDamage);
    }

    public void SetMaxHealth(float maxHealth)
    {
        if (maxHealth <= 0)
            throw new ArgumentOutOfRangeException(nameof(maxHealth));

        _maxHealth = maxHealth;

        if (CurrentHealth > _maxHealth)
        {
            CurrentHealth = _maxHealth;
        }
    }

    public void UpdateHealthToMax()
    {
        CurrentHealth = _maxHealth;
    }

    protected virtual float ProcessDamage(float damage) => damage * damageMultiplier;
}