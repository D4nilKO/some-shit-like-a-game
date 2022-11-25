using NTC.Global.Pool;
using UnityEngine;

namespace Enemies
{
    public class TwinEnemy : StandardEnemy
    {
        [SerializeField] private int countOfEnemySpawns;
        [SerializeField] private GameObject enemyToSpawn;
        [SerializeField] private GameObject enemyContainer;
        private GameObject currentEnemy;

        private FastRandom random = new FastRandom();
        
        private void SpawnEnemyAfterDeath()
        {
            for (int i = 0; i < countOfEnemySpawns; i++)
            {
                var zAngleEnemy = random.Range(0, 350);
                currentEnemy = NightPool.Spawn(enemyToSpawn, transform.position, Quaternion.Euler(0f, 0f, zAngleEnemy));
            }
        }

        public override void Death()
        {
            SpawnEnemyAfterDeath();
            base.Death();
        }
    }
}