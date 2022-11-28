using System;
using System.Collections;
using System.Collections.Generic;
using NTC.Global.Pool;
using UnityEngine;
using UnityEngine.Serialization;

namespace Components.Skills.Orbital_Strike
{
    public class OrbitalStrike : DamagingSkill
    {
        [FormerlySerializedAs("missilePref")] [SerializeField]
        private GameObject mainPrefab;

        public float countOfStrikes;

        [SerializeField] private float attackRadius = 10f;

        private MoveTrack moveTrackScr;
        private readonly FastRandom random = new FastRandom();


        //private Vector2 heroTrack;
        private Vector3 randomPosition;

        //public float damage;

        List<GameObject> targetList = new List<GameObject>();

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
            public int countOfStrikes;
            public float damage;
            public string upgradeText;
        }

        [Serializable]
        public class SkillUpgradeList
        {
            public SkillUpgrade[] skillUpgrade;
        }

        public SkillUpgradeList orbitalStrikeSkillUpgradeList = new SkillUpgradeList();

        #endregion

        #region LYFECYCLE

        private void Awake()
        {
            InitializeSkill(false);
        }

        private void Start()
        {
            mainPrefab.SetActive(false);
            InitializeSkill(true);
        }

        public override void InitializeSkill(bool withStart)
        {
            orbitalStrikeSkillUpgradeList = JsonUtility.FromJson<SkillUpgradeList>(Attribute.jsonUpgradeData.text);
            damage = orbitalStrikeSkillUpgradeList.skillUpgrade[0].damage;
            countOfStrikes = orbitalStrikeSkillUpgradeList.skillUpgrade[0].countOfStrikes;
            Attribute.startTimeBtwSpawns = orbitalStrikeSkillUpgradeList.skillUpgrade[0].timeBtwSpawns;
            Attribute.maxLvl = orbitalStrikeSkillUpgradeList.skillUpgrade.Length;

            base.InitializeSkill(withStart);
        }

        #endregion

        public override IEnumerator MainTimer()
        {
            Attribute.timeBtwSpawns = Attribute.startTimeBtwSpawns; //обнуляем таймер
            while (Attribute.timeBtwSpawns > 0) //если время больше 0, то уменьшаем его по чуть-чуть
            {
                Attribute.timeBtwSpawns -= Time.deltaTime; //само уменьшение таймера
                yield return null; //продолжить выполнение после этого кадра
            }

            //ActivateSkill(); //активация оружия
            yield return StartCoroutine(DamageObjects()); // запускает корутину и ждет ее выполнения(пидорас)
            StartCoroutine(MainTimer()); //перезапуск таймера спавна
        }


        public override void ActivateSkill()
        {
            StartCoroutine(DamageObjects());
        }

        public override void Upgrade()
        {
            if (Attribute.lvl <= Attribute.maxLvl)
            {
                damage = orbitalStrikeSkillUpgradeList.skillUpgrade[Attribute.lvl - 1].damage;
                countOfStrikes = orbitalStrikeSkillUpgradeList.skillUpgrade[Attribute.lvl - 1].countOfStrikes;
                Attribute.startTimeBtwSpawns = orbitalStrikeSkillUpgradeList.skillUpgrade[Attribute.lvl - 1].timeBtwSpawns;
            }
            else
            {
                damage *= 1.05f;
            }
        }

        public override IEnumerator DamageObjects()
        {
            for (var i = 0; i < countOfStrikes; i++)
            {
                randomPosition = (random.GetInsideCircle(attackRadius));
                var curObj = NightPool.Spawn(mainPrefab, randomPosition + Player.playerTransform.position);
                targetList.Add(curObj);
                yield return null; // хочу чтобы взрывы были капельку расснхронизированы
            }

            while (Attribute.timeBtwActions > 0)
            {
                Attribute.timeBtwActions -= Time.deltaTime;
                yield return null;
            }

            for (var i = 0; i < targetList.Count; i++)
            {
                NightPool.Despawn(targetList[i]);
            }

            targetList.Clear();
            yield return null;
            Attribute.timeBtwActions = Attribute.startTimeBtwActions;
        }
    }
}