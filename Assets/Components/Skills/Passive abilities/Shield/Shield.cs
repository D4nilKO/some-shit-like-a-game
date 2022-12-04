using System;
using System.Collections;
using UnityEngine;

public class Shield : PassiveSkill
{
    [SerializeField, Min(1)] private float overUpgradeIncrease = 1.05f;
    [SerializeField] private float startEndurance;
    [SerializeField] private float endurance;
    [SerializeField] private GameObject mainPrefab;

    public bool IsShieldEnable
    {
        get
        {
            if (!enabled)
                return false;
            else
            {
                IsShieldEnable = endurance > 0;
                return endurance > 0;
            }
        }
        private set => mainPrefab.SetActive(value);
    }

    #region ATTRIBUTE

    public AttributeSkill attribute;

    public override AttributeSkill Attribute
    {
        get => attribute;
        set => attribute = value;
    }

    #endregion

    #region JSON

    [Serializable]
    public class SkillUpgrade
    {
        public int lvl;
        public float timeBtwSpawns;
        public float startEndurance;
        public string upgradeText;
    }

    [Serializable]
    public class SkillUpgradeList

    {
        public SkillUpgrade[] skillUpgrade;
    }

    public SkillUpgradeList shieldSkillUpgradeList = new SkillUpgradeList();

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
        mainPrefab.SetActive(false);

        shieldSkillUpgradeList = JsonUtility.FromJson<SkillUpgradeList>(Attribute.jsonUpgradeData.text);

        startEndurance = shieldSkillUpgradeList.skillUpgrade[0].startEndurance;
        UpdateEnduranceToMax();
        Attribute.startTimeBtwSpawns = shieldSkillUpgradeList.skillUpgrade[0].timeBtwSpawns;
        Attribute.maxLvl = shieldSkillUpgradeList.skillUpgrade.Length;


        base.InitializeSkill(withStart);

        if (withStart)
        {
            StopAllCoroutines();
            mainPrefab.SetActive(true);
        }
    }

    #endregion

    public void ApplyDamageToShield(float damage)
    {
        if (damage < 0)
            throw new ArgumentOutOfRangeException(nameof(damage));

        var totalDamage = ProcessDamage(damage);

        if (totalDamage < 0)
            throw new ArgumentOutOfRangeException(nameof(totalDamage));

        endurance -= totalDamage;
    }

    public void UpdateEnduranceToMax()
    {
        endurance = startEndurance;
        if (IsShieldEnable) return;
    }

    public void ApplyRecharge()
    {
        StopCoroutine(MainTimer());
        StartCoroutine(MainTimer());
    }
    
    private IEnumerator MainTimer()
    {
        Attribute.timeBtwSpawns = Attribute.startTimeBtwSpawns;
        while (Attribute.timeBtwSpawns > 0)
        {
            Attribute.timeBtwSpawns -= Time.deltaTime;
            yield return null;
        }

        UpdateEnduranceToMax();
    }

    public override void Upgrade()
    {
        var isMaxEndurance = Math.Abs(startEndurance - endurance) < 1f;

        if (Attribute.lvl <= Attribute.maxLvl)
        {
            Attribute.startTimeBtwSpawns = shieldSkillUpgradeList.skillUpgrade[Attribute.lvl - 1].timeBtwSpawns;
            startEndurance = shieldSkillUpgradeList.skillUpgrade[Attribute.lvl - 1].startEndurance;
        }
        else
        {
            startEndurance *= overUpgradeIncrease;
        }

        if (isMaxEndurance)
        {
            UpdateEnduranceToMax();
        }
    }

    protected virtual float ProcessDamage(float damage) => damage * Stats.DamageTakingMultiplier;
}