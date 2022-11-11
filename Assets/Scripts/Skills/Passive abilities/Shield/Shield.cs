using System;
using System.Collections;
using UnityEngine;

public class Shield : PassiveSkill
{
    public float startEndurance;
    public float endurance;
    [SerializeField] private float overUpgradeIncrease = 1.05f;
    public bool isShieldEnable;
    public GameObject mainPrefab;

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
        endurance = startEndurance;
        Attribute.startTimeBtwSpawns = shieldSkillUpgradeList.skillUpgrade[0].timeBtwSpawns;
        Attribute.maxLvl = shieldSkillUpgradeList.skillUpgrade.Length;

        isShieldEnable = false;

        base.InitializeSkill(withStart);

        if (withStart)
        {
            StopAllCoroutines();
            isShieldEnable = true;
            mainPrefab.SetActive(true);
        }
    }

    #endregion

    public void RecountEndurance(float deltaEndurance)
    {
        endurance += deltaEndurance;
        if (endurance <= 0)
        {
            isShieldEnable = false;
            mainPrefab.SetActive(false);
        }
    }

    public IEnumerator MainTimer()
    {
        Attribute.timeBtwSpawns = Attribute.startTimeBtwSpawns;
        while (Attribute.timeBtwSpawns > 0)
        {
            Attribute.timeBtwSpawns -= Time.deltaTime;
            yield return null;
        }

        endurance = startEndurance;
        
        isShieldEnable = true;
        mainPrefab.SetActive(true);
    }

    public override void Upgrade()
    {
        bool isMaxEndurance = Math.Abs(startEndurance - endurance) < 1f;

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
            endurance = startEndurance;
        }
    }
}