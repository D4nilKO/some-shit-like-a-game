using System.Collections;
using Enemies;
using UnityEngine;

namespace Components.Skills
{
    public class EnduringDamagingSkillForGameObject : MonoBehaviour
    {
        [SerializeField] private DamagingSkill skill;
        public bool isInTrigger;
        public float startTimeBtwDamage;
        public float timeBtwDamage;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out BaseEnemy enemy))
            {
                isInTrigger = true;
                Damaging(enemy);
            }
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.TryGetComponent(out BaseEnemy enemy) && isInTrigger)
            {
                StartCoroutine(DamageObject(enemy));
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent(out BaseEnemy enemy))
            {
                isInTrigger = false;
                StopCoroutine(DamageObject(enemy));
            }
        }

        private void Damaging(BaseEnemy enemy)
        {
            //var enemyScr = other.GetComponent<BaseEnemy>();
            enemy.healthScr.ApplyDamage(skill.damage);
            isInTrigger = true;
        }

        private IEnumerator DamageObject(BaseEnemy enemy)
        {
            isInTrigger = false;
            timeBtwDamage = startTimeBtwDamage;
            while (timeBtwDamage > 0)
            {
                if (!enemy.gameObject.activeInHierarchy)
                {
                    yield break;
                }

                timeBtwDamage -= Time.deltaTime;
                yield return null;
            }

            Damaging(enemy);
        }
    }
}