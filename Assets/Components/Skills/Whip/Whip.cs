using System;
using System.Collections;
using Components.Skills;
using UnityEngine;
using UnityEngine.Serialization;

public class Whip : DamagingSkill
{
    [FormerlySerializedAs("whipPrefab")] public GameObject mainPrefab;
    //[NonSerialized] public float damage;

    private float countOfStrikes;
    private float angle;

    private float durationTime;
    [SerializeField] private float startDurationTime;

    public AttributeSkill attribute; //?? ????????????!!! ?????? ????? ??? ???? ????? ??????? ????? Attribute

    public override AttributeSkill Attribute // ??? ???? ?????!
    {
        get { return attribute; }
        set { attribute = value; }
    }

    public JsonReader.SkillUpgradeList whipSkillUpgradeList = new JsonReader.SkillUpgradeList();

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

        whipSkillUpgradeList = JsonUtility.FromJson<JsonReader.SkillUpgradeList>(Attribute.jsonUpgradeData.text);
        damage = whipSkillUpgradeList.skillUpgrade[0].damage;
        countOfStrikes = whipSkillUpgradeList.skillUpgrade[0].countOfStrikes;
        Attribute.startTimeBtwSpawns = whipSkillUpgradeList.skillUpgrade[0].timeBtwSpawns;
        Attribute.maxLvl = whipSkillUpgradeList.skillUpgrade.Length;

        durationTime = startDurationTime;
        angle = 360 / countOfStrikes;

        base.InitializeSkill(withStart);
    }

    public override void ActivateSkill()
    {
        StartCoroutine(DamageObjects());
    }

    public override void Upgrade()
    {
        if (Attribute.lvl <= Attribute.maxLvl)
        {
            damage = whipSkillUpgradeList.skillUpgrade[Attribute.lvl - 1].damage;
            countOfStrikes = whipSkillUpgradeList.skillUpgrade[Attribute.lvl - 1].countOfStrikes;
            Attribute.startTimeBtwSpawns = whipSkillUpgradeList.skillUpgrade[Attribute.lvl - 1].timeBtwSpawns;

            angle = 360 / countOfStrikes;
        }
        else
        {
            damage *= 1.05f;
        }
    }

    public override IEnumerator MainTimer()
    {
        Attribute.timeBtwSpawns = Attribute.startTimeBtwSpawns;
        while (Attribute.timeBtwSpawns > 0)
        {
            Attribute.timeBtwSpawns -= Time.deltaTime;
            yield return null;
        }

        yield return StartCoroutine(DamageObjects()); // ????????? ???????? ? ???? ?? ??????????(???????)
        mainPrefab.transform.localPosition = (Vector3.zero);
        mainPrefab.SetActive(false);

        StartCoroutine(MainTimer());
    }

    public override IEnumerator DamageObjects()
    {
        // ??????? ??????? ????????? ???-?? ???
        for (var i = 1; i <= countOfStrikes; i++)
        {
            mainPrefab.SetActive(true);

            durationTime = startDurationTime;
            while (durationTime > 0)
            {
                durationTime -= Time.deltaTime;
                yield return null;
            }

            mainPrefab.SetActive(false);

            Attribute.timeBtwActions = Attribute.startTimeBtwActions;
            while (Attribute.timeBtwActions > 0)
            {
                Attribute.timeBtwActions -= Time.deltaTime;
                yield return null;
            }

            var position = Player.playerTransform.position;
            mainPrefab.transform.RotateAround(position, Vector3.forward, angle);
        }
    }
}