using NTC.Global.Pool;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float hp;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            Player.systemHpScr.RecountHp(hp);
            NightPool.Despawn(gameObject);

            //Death();
        }
    }

    // public void Death()
    // {
    //     NightPool.Despawn(gameObject);
    // }

}