using System;
using UnityEngine;

public class Memory : PassiveSkill
{
    #region ATTRIBUTE

    public AttributeSkill attribute;

    public override AttributeSkill Attribute
    {
        get { return attribute; }
        set { attribute = value; }
    }

    #endregion

    #region JSON

    [Serializable]
    public class SkillUpgrade
    {
        public int lvl;
        public float xpMultiplier;
        public string upgradeText;
    }

    [Serializable]
    public class SkillUpgradeList

    {
        public SkillUpgrade[] skillUpgrade;
    }

    public SkillUpgradeList memorySkillUpgradeList = new SkillUpgradeList();

    #endregion

    #region LYFECYCLE

    private void Awake()
    {
        InitializeSkill(false);
    }

    private void Start()
    {
        InitializeSkill(true);
    }

    public override void InitializeSkill(bool withStart)
    {
        memorySkillUpgradeList = JsonUtility.FromJson<SkillUpgradeList>(Attribute.jsonUpgradeData.text);

        Attribute.maxLvl = memorySkillUpgradeList.skillUpgrade.Length;

        base.InitializeSkill(withStart);

        if (withStart)
        {
            Stats.XpGainMultiplier += memorySkillUpgradeList.skillUpgrade[0].xpMultiplier;
        }
    }

    #endregion

    public override void Upgrade()
    {
        if (Attribute.lvl <= Attribute.maxLvl)
        {
            Stats.XpGainMultiplier += memorySkillUpgradeList.skillUpgrade[Attribute.lvl - 1].xpMultiplier;
        }
    }
    
}