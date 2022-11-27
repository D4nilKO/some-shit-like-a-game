using Enemies;
using NTC.Global.Pool;
using UnityEngine;

public class DeSpawnTor : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            var enemyScr = other.GetComponent<BaseEnemy>();
            enemyScr.TouchToDeSpawnTor();
        }

        if (other.CompareTag("Battery"))
        {
            //var otherScr = other.GetComponent<Battery>();
            //otherScr.Death();
            NightPool.Despawn(other);
        }
    }
}