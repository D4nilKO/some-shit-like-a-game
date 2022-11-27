using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SkillButtonGUI : MonoBehaviour
{
    [FormerlySerializedAs("lvlUpPanelSystemScr")] [FormerlySerializedAs("lvlUpSystemScr")] [FormerlySerializedAs("skillSystemScr")] [SerializeField]
    private LevelUpPanelSystem levelUpPanelSystemScr;

    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI buyUpgradeText;
    [SerializeField] private TextMeshProUGUI lvlText;

    [SerializeField] private Image imageObject;

    public Skill skill;

    private int index;

    private void Awake()
    {
        index = transform.GetSiblingIndex();
        levelUpPanelSystemScr.LvlUpChooseEvent += ShowSkill; // подписались
    }

    private void OnDestroy()
    {
        levelUpPanelSystemScr.LvlUpChooseEvent -= ShowSkill; // отписались
    }
    
    private void ShowSkill()
    {
        levelUpPanelSystemScr.SetAbilityPanel(true);

        skill = levelUpPanelSystemScr.skills[index];
        
        nameText.text = skill.Attribute.name;
        if (skill.Attribute.lvl == 0)
        {
            lvlText.text = string.Empty;
            buyUpgradeText.text = $"<color=#FF3A39>New!</color>"; // красный
        }
        else
        {
            lvlText.text = " LVL " + skill.Attribute.lvl.ToString(CultureInfo.CurrentCulture);
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
        levelUpPanelSystemScr.Choice(skill);
    }
}