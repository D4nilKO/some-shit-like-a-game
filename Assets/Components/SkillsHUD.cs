using System;
using System.Globalization;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SkillsHUD : MonoBehaviour
{
    [FormerlySerializedAs("lvlUpSystemScr")] [FormerlySerializedAs("skillSystemScr")] [SerializeField]
    private LvlUpPanelSystem lvlUpPanelSystemScr;

    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI buyUpgradeText;
    [SerializeField] private TextMeshProUGUI lvlText;

    [SerializeField] private Image imageObject;

    public Skill skill;

    private int index;

    private void Awake()
    {
        index = transform.GetSiblingIndex();
        lvlUpPanelSystemScr.LvlUpChooseEvent += ShowSkill; // подписались
    }

    private void OnDestroy()
    {
        lvlUpPanelSystemScr.LvlUpChooseEvent -= ShowSkill; // отписались
    }
    
    private void ShowSkill()
    {
        lvlUpPanelSystemScr.SetAbilityPanel(true);

        skill = lvlUpPanelSystemScr.skills[index];
        
        nameText.text = skill.Attribute.name;
        //print(skill + " lvl = " + skill.Attribute.lvl);
        if (skill.Attribute.lvl == 0)
        {
            lvlText.text = string.Empty;
            buyUpgradeText.text = $"<color=#FF3A39>New!</color>"; // красный
        }
        else
        {
            lvlText.text = " LVL " + skill.Attribute.lvl.ToString(CultureInfo.CurrentCulture);
            //nameText.text +=" LVL " + skill.Attribute.lvl;
            buyUpgradeText.text = $"<color=#3957FF>Upgrade</color>"; // синий 
        }

        // пока нет на всех скилах картинок
        #region Временное

        imageObject.sprite = skill.Attribute.icon != null ? skill.Attribute.icon : null;

        //раскомментить когда уйдет временное
        //imageObject.sprite = skill.Attribute.icon;
        
        #endregion
    }

    public void Choice()
    {
        lvlUpPanelSystemScr.Choice(skill);
    }
}