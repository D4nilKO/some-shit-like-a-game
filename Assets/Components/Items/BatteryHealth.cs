using NTC.Global.Pool;
using UnityEngine;

public class BatteryHealth : MonoBehaviour
{
    //[SerializeField] private float hp;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.TryGetComponent(out PlayerHealth playerHealthScr)) return;
        
        playerHealthScr.UpdateHealthToMax();
        NightPool.Despawn(gameObject);
    }
}