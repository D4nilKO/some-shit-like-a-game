using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using System;
using Components.Skills;

public class Flamethrower : DamagingSkill
{
    public bool status;

    private MoveTrack moveTrackScr;

    private Vector2 vectorTrack;

    //public float startDamage = 5f;
    //[NonSerialized] public float damage;

    //public float multiplierDamage = 1f;

    [FormerlySerializedAs("flamethrowerGameObject")]
    public GameObject mainPrefab;

    private float flamethrowerRotationZ;

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

    public SkillUpgradeList flamethrowerSkillUpgradeList = new SkillUpgradeList();

    #endregion

    #region LYFECYCLE

    private void Awake()
    {
        moveTrackScr = GetComponent<MoveTrack>(); //привязка скирапта отслеживания движения

        InitializeSkill(false);
    }

    private void Start()
    {
        InitializeSkill(true);
    }

    public override void InitializeSkill(bool withStart)
    {
        mainPrefab.SetActive(false);

        flamethrowerSkillUpgradeList = JsonUtility.FromJson<SkillUpgradeList>(Attribute.jsonUpgradeData.text);

        damage = flamethrowerSkillUpgradeList.skillUpgrade[0].damage;
        Attribute.startTimeBtwActions = flamethrowerSkillUpgradeList.skillUpgrade[0].timeBtwActions;
        Attribute.startTimeBtwSpawns = flamethrowerSkillUpgradeList.skillUpgrade[0].timeBtwSpawns;
        Attribute.maxLvl = flamethrowerSkillUpgradeList.skillUpgrade.Length;

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
            damage = flamethrowerSkillUpgradeList.skillUpgrade[Attribute.lvl - 1].damage;
            Attribute.startTimeBtwActions = flamethrowerSkillUpgradeList.skillUpgrade[Attribute.lvl - 1].timeBtwActions;
            Attribute.startTimeBtwSpawns = flamethrowerSkillUpgradeList.skillUpgrade[Attribute.lvl - 1].timeBtwSpawns;
        }
        else
        {
            damage *= 1.05f;
        }
    }


    public override IEnumerator DamageObjects()
    {
        status = true;
        mainPrefab.SetActive(true);

        while (Attribute.timeBtwActions > 0) //если время больше 0, то уменьшаем его по чуть-чуть
        {
            Attribute.timeBtwActions -= Time.deltaTime; //само уменьшение таймера
            yield return null; //продолжить выполнение после этого кадра
        }

        Attribute.timeBtwActions = Attribute.startTimeBtwActions;

        mainPrefab.SetActive(false);
        status = false;
    }

    private void TurnTowardsTheCharacter()
    {
        vectorTrack = moveTrackScr.MovementLogic();
        if (Mathf.Abs(vectorTrack.x) > 0.05f || Mathf.Abs(vectorTrack.y) > 0.05f)
            flamethrowerRotationZ =
                Mathf.Atan2(vectorTrack.y, vectorTrack.x) * Mathf.Rad2Deg; // считает поворот по Z
        mainPrefab.transform.rotation =
            Quaternion.Euler(0f, 0f, flamethrowerRotationZ); // поворачивает объект в сторону куда идет персонаж
    }

    private void Update()
    {
        if (status && Time.timeScale != 0)
        {
            TurnTowardsTheCharacter();
        }


        //print(playerSCR.moveInput);

        // if (playerSCR.controlType == player.ControlType.Android)
        //{
        //if (Mathf.Abs(joystik.Horizontal) > 0.05f || Mathf.Abs(joystik.Vertical) > 0.05f)
        //    flamethrowerRotation = Mathf.Atan2(joystik.Vertical, joystik.Horizontal) * Mathf.Rad2Deg;
        //flamethrowerPivot.transform.rotation = Quaternion.Euler(0f, 0f, flamethrowerRotation);
        //}
        //else 
        //if (playerSCR.controlType == player.ControlType.PC)
        //{
        //    //print(Input.GetAxisRaw("Horizontal"));
        //    if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0.05f || Mathf.Abs(Input.GetAxisRaw("Vertical")) > 0.05f)
        //        flamethrowerRotation = Mathf.Atan2(Input.GetAxisRaw("Vertical"), Input.GetAxisRaw("Horizontal")) * Mathf.Rad2Deg;
        //    flamethrowerPivot.transform.rotation = Quaternion.Euler(0f, 0f, flamethrowerRotation);
        //}

        ////var vectorz = MoveTrackSCR.getVec2();
        ////var rotation = Vector2.Angle(vectorZero, MoveTrackSCR.Vec2);
        //moveInput = new Vector2(joystik.Horizontal, joystik.Vertical);
        ////transform.forward = moveInput;
        //var flamethrower = new Vector3(0, 0, Vector2.Angle(transform.forward, moveInput));
        //flamethrowerPivot.transform.rotation = Quaternion.Euler(flamethrower);
    }
}