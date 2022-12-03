using System;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;

public class Experience : MonoBehaviour
{
    [SerializeField] public int startLevel = 1;

    [HideInInspector] public int countOfLevelUpsAtOnce;

    private float currentExperience;

    private float CurrentExperience
    {
        get => currentExperience;
        set
        {
            currentExperience = value;
            var percentExperience = CurrentExperience / experienceToLvlUp;
            XpChanged?.Invoke(percentExperience);
        }
    }

    private int currentLevel;

    private int CurrentLevel
    {
        get => currentLevel;
        set
        {
            countOfLevelUpsAtOnce = value - currentLevel;
            LvlIncreaseEvent?.Invoke();
            currentLevel = value;
            levelText.text = "LVL " + CurrentLevel;
        }
    }

    public event Action<float> XpChanged;

    [SerializeField] private float startXpToLvlUp = 10;
    private float experienceToLvlUp;

    [Header("Settings")] [SerializeField] private int scaleArithmetic = 10;

    [SerializeField] private float scaleGeometric = 1.08f;

    public TypeOfXpProgress typeOfXpProgress;


    public enum TypeOfXpProgress
    {
        Arithmetic,
        Geometric
    };

    public event Action LvlIncreaseEvent;
    [FormerlySerializedAs("meshPro")] [SerializeField] private TextMeshProUGUI levelText;

    private void Start()
    {
        currentLevel = startLevel;
        experienceToLvlUp = startXpToLvlUp;
        CurrentExperience = 0f;
    }

    public void AddExperience(float experience)
    {
        experience *= Stats.ExperienceGainMultiplier;
        while (true)
        {
            CurrentExperience += experience;

            if (CurrentExperience >= experienceToLvlUp)
            {
                CurrentLevel++;
                CurrentExperience -= experienceToLvlUp;

                switch (typeOfXpProgress)
                {
                    case TypeOfXpProgress.Arithmetic:
                        experienceToLvlUp += scaleArithmetic;
                        break;
                    case TypeOfXpProgress.Geometric:
                        experienceToLvlUp *= scaleGeometric;
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