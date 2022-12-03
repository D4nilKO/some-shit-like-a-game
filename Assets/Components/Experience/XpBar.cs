using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class XpBar : MonoBehaviour
{
    [FormerlySerializedAs("xPFill")] [SerializeField] private Image xpFill;
    [FormerlySerializedAs("systemXp")] [SerializeField] private Experience experience;
    
    private void Awake()
    {
        experience.XpChanged += OnXpChanged;
    }

    private void OnDestroy()
    {
        experience.XpChanged -= OnXpChanged;
    }
    private void OnXpChanged(float valueAsPercent)
    {
        xpFill.fillAmount = valueAsPercent;
    }
}

