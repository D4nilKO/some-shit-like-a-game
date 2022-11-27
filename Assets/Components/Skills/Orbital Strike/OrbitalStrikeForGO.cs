using Enemies;
using UnityEngine;

public class OrbitalStrikeForGO : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            var enemyScr = col.GetComponent<BaseEnemy>();
            enemyScr.RecountHp(Player.orbitalStrikeScr.damage);
        }
    }
}