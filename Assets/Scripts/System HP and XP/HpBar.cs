using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    [SerializeField] private Image hPFill;
    [SerializeField] private SystemHp systemHp;
    [SerializeField] private Gradient gradient;


    private void Awake()
    {
        systemHp.HealthChanged += OnHealthChanged;
        hPFill.color = gradient.Evaluate(1);
    }

    private void OnDestroy()
    {
        systemHp.HealthChanged -= OnHealthChanged;
    }
    private void OnHealthChanged(float valueAsPercent)
    {
        //Debug.Log(valueAsPersent);
        hPFill.fillAmount = valueAsPercent;
        hPFill.color = gradient.Evaluate(valueAsPercent);
    }
}
