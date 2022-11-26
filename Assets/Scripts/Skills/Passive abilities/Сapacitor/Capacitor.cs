using System;
using UnityEngine;
using UnityEngine.Serialization;

public class Capacitor : PassiveSkill //regen hp and increases max hp
{
    [FormerlySerializedAs("systemHpScr")] [SerializeField] private PlayerHealth playerHealthScr;

    [SerializeField] private float extraHpMultiplier;
    [SerializeField] private float overExtraHpMultiplier = 1.05f;

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
        public float extraHpMultiplier;
        public string upgradeText;
    }

    [Serializable]
    public class SkillUpgradeList

    {
        public SkillUpgrade[] skillUpgrade;
    }

    public SkillUpgradeList capacitorSkillUpgradeList = new SkillUpgradeList();

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
        capacitorSkillUpgradeList = JsonUtility.FromJson<SkillUpgradeList>(Attribute.jsonUpgradeData.text);

        Attribute.maxLvl = capacitorSkillUpgradeList.skillUpgrade.Length;

        base.InitializeSkill(withStart);

        if (withStart)
        {
            extraHpMultiplier = capacitorSkillUpgradeList.skillUpgrade[0].extraHpMultiplier;
            
            ActivateSkill();
        }
    }

    #endregion

    public override void ActivateSkill()
    {
        playerHealthScr.MultiplyMaxHealth(extraHpMultiplier);
    }

    public override void Upgrade()
    {
        if (Attribute.lvl <= Attribute.maxLvl)
        {
            extraHpMultiplier = capacitorSkillUpgradeList.skillUpgrade[Attribute.lvl - 1].extraHpMultiplier;
        }
        else
        {
            extraHpMultiplier = overExtraHpMultiplier;
        }

        ActivateSkill();
    }
}