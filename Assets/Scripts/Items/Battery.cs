using NTC.Global.Pool;
using UnityEngine;
using UnityEngine.Pool;

public class Battery : MonoBehaviour
{
    public float xp;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            Player.systemXpScr.RecountXp(xp);
            NightPool.Despawn(gameObject);
        }
    }
}