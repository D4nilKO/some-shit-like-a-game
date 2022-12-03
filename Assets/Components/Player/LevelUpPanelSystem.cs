using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using static NonRepeatingRandom;

public class LevelUpPanelSystem : MonoBehaviour
{
    public List<Skill> skills;

    [FormerlySerializedAs("systemXpScr")] public Experience experienceScr;

    public GameObject abilityPanel;

    public event Action LvlUpChooseEvent;

    private TimeManager timeManagerScr;

    private void Awake()
    {
        experienceScr.LvlIncreaseEvent += LvlUpIncreasedCheck; // подписались
        timeManagerScr = GetComponent<TimeManager>();
    }

    private void OnDestroy()
    {
        experienceScr.LvlIncreaseEvent -= LvlUpIncreasedCheck; // отписались
    }

    private void Start()
    {
        SetAbilityPanel(false);
    }

    private void LvlUpIncreasedCheck()
    {
        if (experienceScr.countOfLevelUpsAtOnce <= 0) return;
        SetAbilityPanel(true);
        ShuffleList(skills);

        LvlUpChooseEvent?.Invoke();

        Time.timeScale = 0;
    }

    public void Choice(Skill skillScr)
    {
        if (skillScr.Attribute.startLvl != 0)
        {
            skillScr.Attribute.lvl++;
            skillScr.Upgrade();
        }
        else
        {
            skillScr.Attribute.startLvl = 1;
            skillScr.enabled = true;
        }

        if (!skillScr.Attribute.isUpgradeAfterMaxLvl && skillScr.Attribute.lvl == skillScr.Attribute.maxLvl)
        {
            skills.Remove(skillScr);
        }

        SetAbilityPanel(false);

        experienceScr.countOfLevelUpsAtOnce--;
        LvlUpIncreasedCheck();

        if (experienceScr.countOfLevelUpsAtOnce > 0) return;
        StartCoroutine(timeManagerScr.WaitBeforeContinueTime());
    }

    public void SetAbilityPanel(bool set)
    {
        //место для анимации появления панельки?
        abilityPanel.SetActive(set);
    }
}