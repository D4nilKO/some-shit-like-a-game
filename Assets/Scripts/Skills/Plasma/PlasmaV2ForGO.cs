using Enemies;
using UnityEngine;

public class PlasmaV2ForGO : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            var enemyScr = other.GetComponent<BaseEnemy>();
            enemyScr.RecountHp(-Player.plasmaV2Scr.damage);
        }
    }
}
