using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Components.Skills.Plasma
{
    public class PlasmaV2 : DamagingSkill
    {
        private readonly FastRandom random = new FastRandom();
        [FormerlySerializedAs("plasmaPivot")] public GameObject mainPrefab;

        //public float startDamage;
        //public float damage;

        public AttributeSkill attribute;

        public override AttributeSkill Attribute
        {
            get => attribute;
            set => attribute = value;
        }

        #region JSON

        [Serializable]
        public class SkillUpgrade
        {
            public int lvl;
            public float timeBtwSpawns;
            public float timeBtwActions;
            public float damage;
            public string upgradeText;
        }

        [Serializable]
        public class SkillUpgradeList

        {
            public SkillUpgrade[] skillUpgrade;
        }

        public SkillUpgradeList plasmaSkillUpgradeList = new SkillUpgradeList();

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

            plasmaSkillUpgradeList = JsonUtility.FromJson<SkillUpgradeList>(Attribute.jsonUpgradeData.text);

            damage = plasmaSkillUpgradeList.skillUpgrade[0].damage;
            Attribute.startTimeBtwActions = plasmaSkillUpgradeList.skillUpgrade[0].timeBtwActions;
            Attribute.startTimeBtwSpawns = plasmaSkillUpgradeList.skillUpgrade[0].timeBtwSpawns;
            Attribute.maxLvl = plasmaSkillUpgradeList.skillUpgrade.Length;

            base.InitializeSkill(withStart);
        }

        #endregion

        public override void ActivateSkill()
        {
            StartCoroutine(DamageObjects());
        }

        public override void Upgrade()
        {
            if (Attribute.lvl <= Attribute.maxLvl)
            {
                damage = plasmaSkillUpgradeList.skillUpgrade[Attribute.lvl - 1].damage;
                Attribute.startTimeBtwActions = plasmaSkillUpgradeList.skillUpgrade[Attribute.lvl - 1].timeBtwActions;
                Attribute.startTimeBtwSpawns = plasmaSkillUpgradeList.skillUpgrade[Attribute.lvl - 1].timeBtwSpawns;
            }
            else
            {
                damage *= 1.05f;
            }
        }

        // public override IEnumerator MainTimer()
        // {
        //     while (Attribute.timeBtwSpawns > 0)
        //     {
        //         Attribute.timeBtwSpawns -= Time.deltaTime;
        //         yield return null;
        //     }
        //     Attribute.timeBtwSpawns = Attribute.startTimeBtwSpawns;
        //
        //     ActivateSkill();
        //     StartCoroutine(MainTimer());
        // }

        public override IEnumerator DamageObjects()
        {
            mainPrefab.SetActive(true);

            var prefabRotation = new Vector3(0, 0, random.Range(0, 360));
            mainPrefab.transform.rotation = Quaternion.Euler(prefabRotation);

            Attribute.timeBtwActions = Attribute.startTimeBtwActions;
            while (Attribute.timeBtwActions > 0) //если время больше 0, то уменьшаем его по чуть-чуть
            {
                Attribute.timeBtwActions -= Time.deltaTime; //само уменьшение таймера
                yield return null; //продолжить выполнение после этого кадра
            }

            mainPrefab.SetActive(false);
        }
    }
}