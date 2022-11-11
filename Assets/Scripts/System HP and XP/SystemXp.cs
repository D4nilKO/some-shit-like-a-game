using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class SystemXp : MonoBehaviour
{
    private int curLvl;
    [SerializeField] public int startLvl = 1;
    private float curXp = 0;
    [SerializeField] private float startXpToLvlUp = 10;
    private float xpToLvlUp;
    
    [Header("Settings")]
    [SerializeField] private int scaleArithmetic = 10;
    [SerializeField] private float scaleGeometric = 1.08f;

    public TypeOfProgress typeOfProgress;

    public event Action<float> XpChanged;

    public enum TypeOfProgress
    {
        Arithmetic,
        Geometric
    };

    public event Action LvlIncreaseEvent;
    [SerializeField] private TextMeshProUGUI meshPro;

    private void Start()
    {
        curLvl = startLvl;
        xpToLvlUp = startXpToLvlUp;
        XpChanged?.Invoke(0);
    }

    public void RecountXp(float deltaXp)
    {
        while (true)
        {
            curXp += deltaXp;
            
            var cuXpPercent = curXp / xpToLvlUp;
            XpChanged?.Invoke(cuXpPercent);
            
            if (curXp >= xpToLvlUp)
            {
                curLvl++;
                curXp -= xpToLvlUp;

                switch (typeOfProgress)
                {
                    case TypeOfProgress.Arithmetic:
                        xpToLvlUp += scaleArithmetic;
                        break;
                    case TypeOfProgress.Geometric:
                        xpToLvlUp *= scaleGeometric;
                        break;
                    default:
                        break;
                }

                meshPro.text = "LVL " + curLvl.ToString();
                deltaXp = 0;
                
                LvlIncreaseEvent?.Invoke();
                
                continue;
            }

            break;
        }
    }

    public void PlusLvl()
    {
        RecountXp(xpToLvlUp);
    }
}