using NTC.Global.Pool;
using UnityEngine;

public class BatteryHealth : MonoBehaviour
{
    public float hp;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            Player.playerHealthScr.Heal(hp);
            NightPool.Despawn(gameObject);
        }
    }
}