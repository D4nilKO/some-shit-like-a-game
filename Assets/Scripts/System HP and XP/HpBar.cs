using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    [FormerlySerializedAs("HealthFill")] [SerializeField] private Image healthFill;
    [FormerlySerializedAs("systemHp")] [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private Gradient gradient;


    private void Awake()
    {
        playerHealth.HealthChangedEvent += OnHealthChanged;
        healthFill.color = gradient.Evaluate(1);
    }

    private void OnDestroy()
    {
        playerHealth.HealthChangedEvent -= OnHealthChanged;
    }
    private void OnHealthChanged(float valueAsPercent)
    {
        healthFill.fillAmount = valueAsPercent;
        healthFill.color = gradient.Evaluate(valueAsPercent);
    }
}
