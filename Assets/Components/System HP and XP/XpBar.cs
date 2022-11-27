using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class XpBar : MonoBehaviour
{
    [FormerlySerializedAs("xPFill")] [SerializeField] private Image xpFill;
    [SerializeField] private SystemXp systemXp;
    
    private void Awake()
    {
        systemXp.XpChanged += OnXpChanged;
    }

    private void OnDestroy()
    {
        systemXp.XpChanged -= OnXpChanged;
    }
    private void OnXpChanged(float valueAsPercent)
    {
        xpFill.fillAmount = valueAsPercent;
    }
}

