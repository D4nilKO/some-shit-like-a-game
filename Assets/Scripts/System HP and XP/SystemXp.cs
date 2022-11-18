using System;
using UnityEngine;
using TMPro;

public class SystemXp : MonoBehaviour
{
    [SerializeField] public int startLvl = 1;

    private int curLvl;

    private int CurLvl
    {
        get => curLvl;
        set
        {
            countOfLvlUpsAtOnce = value - curLvl;
            LvlIncreaseEvent?.Invoke();
            curLvl = value;
            meshPro.text = "LVL " + CurLvl;
        }
    }

    [HideInInspector] public int countOfLvlUpsAtOnce;

    private float curXp;

    private float CurXp
    {
        get => curXp;
        set
        {
            curXp = value;
            var cuXpPercent = CurXp / xpToLvlUp;
            XpChanged?.Invoke(cuXpPercent);
        }
    }

    public event Action<float> XpChanged;

    [SerializeField] private float startXpToLvlUp = 10;
    private float xpToLvlUp;

    [Header("Settings")] [SerializeField] private int scaleArithmetic = 10;

    [SerializeField] private float scaleGeometric = 1.08f;

    public TypeOfXpProgress typeOfXpProgress;


    public enum TypeOfXpProgress
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
        CurXp = 0f;
    }

    public void RecountXp(float deltaXp)
    {
        deltaXp *= Stats.XpGainMultiplier;
        while (true)
        {
            CurXp += deltaXp;

            if (CurXp >= xpToLvlUp)
            {
                CurLvl++;
                CurXp -= xpToLvlUp;

                switch (typeOfXpProgress)
                {
                    case TypeOfXpProgress.Arithmetic:
                        xpToLvlUp += scaleArithmetic;
                        break;
                    case TypeOfXpProgress.Geometric:
                        xpToLvlUp *= scaleGeometric;
                        break;
                    default:
                        break;
                }

                deltaXp = 0;

                continue;
            }

            break;
        }
    }

    public void PlusLvl()
    {
        CurLvl++;
    }

    public void PlusLvlX10()
    {
        CurLvl += 10;
    }
}