using System.Collections;
using NTC.Global.Pool;
using UnityEngine;

namespace Enemies
{
    public class TwinEnemy : StandardEnemy
    {
        [SerializeField] private int countOfEnemySpawns;
        [SerializeField] private GameObject enemyToSpawn;
        private GameObject currentEnemy;
        private Transform positionToSpawn;

        private FastRandom random = new FastRandom();
        
        private IEnumerator SpawnEnemyAfterDeath()
        {
            positionToSpawn = transform;
            for (int i = 0; i < countOfEnemySpawns; i++)
            {
                var zAngleEnemy = random.Range(0, 350);
                currentEnemy = NightPool.Spawn(enemyToSpawn, positionToSpawn.position, Quaternion.Euler(0f, 0f, zAngleEnemy));
                //Нужно прописать еще чтобы некоторое время после спавна игнорировался урон, а то они раздуплиться не успевают(
                yield return null;
            }
        }

        public override void Death()
        {
            StartCoroutine(SpawnEnemyAfterDeath());
            base.Death();
        }
    }
}