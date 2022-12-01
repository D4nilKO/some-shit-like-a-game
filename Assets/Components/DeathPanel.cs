using UnityEngine;
using System;
using System.Globalization;
using TMPro;

namespace Components
{
    public class DeathPanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI enemyKilledText;
        [SerializeField] private TextMeshProUGUI maxHpText;
        [SerializeField] private TextMeshProUGUI skillDamageMultiplierText;
        [SerializeField] private TextMeshProUGUI damageTakingMultiplierText;
        [SerializeField] private TextMeshProUGUI xpGainMultiplierText;

        [SerializeField] private TextMeshProUGUI timerText;
        [SerializeField] private TimeManager timeManagerScr;

        [SerializeField] private GameObject reviveButton; 
        private SurviveTimer timer;
        
        private void OnEnable()
        {
            Time.timeScale = 0;
            ShowStats();
        }

        private void ShowStats()
        {
            TextNullCheck(enemyKilledText, Stats.EnemyKilled.ToString(CultureInfo.CurrentCulture));
            TextNullCheck(maxHpText, Player.playerHealthScr.MaxHealth.ToString(CultureInfo.CurrentCulture));
            TextNullCheck(skillDamageMultiplierText,
                (Stats.SkillDamageMultiplier * 100f).ToString(CultureInfo.CurrentCulture) + "%");
            TextNullCheck(damageTakingMultiplierText,
                (Stats.DamageTakingMultiplier * 100f).ToString(CultureInfo.CurrentCulture) + "%");
            TextNullCheck(xpGainMultiplierText,
                (Stats.ExperienceGainMultiplier * 100f).ToString(CultureInfo.CurrentCulture) + "%");

            timer = timeManagerScr.gameTime;
            TextNullCheck(timerText, timer.FormattedTime());

            //тут место для кнопки просмотра рекламы чтобы выжить
            reviveButton.SetActive(Stats.CountOfRevivals > 0);
        }

        private void TextNullCheck(TextMeshProUGUI textObject, string value)
        {
            try
            {
                textObject.text = value;
            }
            catch (NullReferenceException ex)
            {
                Debug.Log("нуль text");
            }
        }
    }
}