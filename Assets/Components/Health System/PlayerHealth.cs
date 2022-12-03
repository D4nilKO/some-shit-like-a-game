using UnityEngine;

public class PlayerHealth : Health
{
    [SerializeField] private GameObject deathPanel;
    [SerializeField] private TimeManager timeManagerScr;
    private Shield shieldScr;

    //public event Action<float> HealthChanged;

    private void Start()
    {
        shieldScr = Player.shieldScr;
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

    public void Revive()
    {
        Stats.CountOfRevivals--;
        deathPanel.SetActive(false);
        UpdateHealthToMax();
        StopAllCoroutines();
        shieldScr.UpdateEnduranceToMax();
        StartCoroutine(timeManagerScr.WaitBeforeContinueTime());
    }

    protected override float ProcessDamage(float damage) => damage * Stats.DamageTakingMultiplier;
}