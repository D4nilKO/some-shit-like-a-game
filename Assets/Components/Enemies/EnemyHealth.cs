using Enemies;
using UnityEditor;

namespace Components.Enemies
{
    public class EnemyHealth : Health
    {
        private BaseEnemy enemy;

        private void Awake()
        {
            enemy = GetComponent<BaseEnemy>();
        }

        public override void ApplyDamage(float damage)
        {
            base.ApplyDamage(damage);
            if (CurrentHealth <= 0)
            {
                enemy.Death();
            }
        }
    }
}