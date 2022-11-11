using Enemies;
using UnityEngine;

public class WipForGO : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            var enemyScr = col.GetComponent<BaseEnemy>();
            enemyScr.RecountHp(-Player.whipScr.damage);
        }
    }
}
