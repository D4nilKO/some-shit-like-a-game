using Components.Enemies;
using Enemies;
using UnityEngine;

namespace Components.Skills
{
    public class DamagingSkillForGameObject : MonoBehaviour
    {
        [SerializeField] private DamagingSkill skill;
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.TryGetComponent(out BaseEnemy enemy))
            {
                enemy.healthScr.ApplyDamage(skill.damage);
            }
        }
    }
}