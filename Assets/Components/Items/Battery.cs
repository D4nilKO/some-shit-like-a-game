using NTC.Global.Pool;
using UnityEngine;

public class Battery : MonoBehaviour
{
    public float xp;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.TryGetComponent(out SystemXp systemXpScr)) return;
        
        systemXpScr.AddExperience(xp);
        NightPool.Despawn(gameObject);
    }
}