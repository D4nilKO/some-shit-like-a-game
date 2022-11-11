using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Attribute", menuName = "Skill/Attribute")]
public class AttributeSkill : ScriptableObject
{
    public new string name => this._name;

    [Header("Description")] [SerializeField]
    private string _name;

    public Sprite icon;

    [Header("Attribute")] 
    public float startTimeBtwActions;

    public TextAsset jsonUpgradeData;

    [NonSerialized] public float startTimeBtwSpawns;

    [NonSerialized] public int startLvl = 0;
    [NonSerialized] public int lvl = 0;
    [NonSerialized] public int maxLvl;

    [NonSerialized] public float timeBtwSpawns;
    [NonSerialized] public float timeBtwActions;
}