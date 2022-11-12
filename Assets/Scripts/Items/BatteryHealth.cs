using NTC.Global.Pool;
using UnityEngine;

public class BatteryHealth : MonoBehaviour
{
    public float hp;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            Player.systemHpScr.RecountHp(hp);
            NightPool.Despawn(gameObject);
        }
    }
}