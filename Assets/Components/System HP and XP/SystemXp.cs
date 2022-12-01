using System;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;

public class SystemXp : MonoBehaviour
{
    [SerializeField] public int startLvl = 1;

    private int currentLevel;

    private int CurrentLevel
    {
        get => currentLevel;
        set
        {
            countOfLevelUpsAtOnce = value - currentLevel;
            LvlIncreaseEvent?.Invoke();
            currentLevel = value;
            meshPro.text = "LVL " + CurrentLevel;
        }
    }

    [FormerlySerializedAs("countOfLvlUpsAtOnce")] [HideInInspector]
    public int countOfLevelUpsAtOnce;

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
        currentLevel = startLvl;
        xpToLvlUp = startXpToLvlUp;
        CurXp = 0f;
    }

    public void AddExperience(float experience)
    {
        experience *= Stats.ExperienceGainMultiplier;
        while (true)
        {
            CurXp += experience;

            if (CurXp >= xpToLvlUp)
            {
                CurrentLevel++;
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

                experience = 0;

                continue;
            }

            break;
        }
    }

    public void PlusLvl()
    {
        CurrentLevel++;
    }

    public void PlusLvlX10()
    {
        CurrentLevel += 10;
    }
}