using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using static NonRepeatingRandom;

public class LvlUpSystem : MonoBehaviour
{
    public List<Skill> skills;

    public SystemXp systemXpScr;

    public GameObject abilityPanel;

    public event Action LvlUpChooseEvent;

    private TimeManager timeManagerScr;

    private void Awake()
    {
        systemXpScr.LvlIncreaseEvent += LvlUpShuffle; // подписались
        timeManagerScr = GetComponent<TimeManager>();
    }

    private void OnDestroy()
    {
        systemXpScr.LvlIncreaseEvent -= LvlUpShuffle; // отписались
    }

    private void Start()
    {
        SetAbilityPanel(false);
    }

    private void LvlUpShuffle()
    {
        ShuffleList(skills);

        LvlUpChooseEvent?.Invoke();

        Time.timeScale = 0;
    }

    public void Choice(Skill skillScr)
    {
        StartCoroutine(timeManagerScr.WaitBeforeContinueTime());
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
    }

    public void SetAbilityPanel(bool set)
    {
        //место для анимации появления панельки?
        abilityPanel.SetActive(set);
    }
}