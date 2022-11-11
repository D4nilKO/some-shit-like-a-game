using System.Collections;
using Enemies;
using UnityEngine;

public class FlamethrowerForGO : MonoBehaviour
{
    public bool isInTrigger;
    public float startTimeBtwDamage;
    public float timeBtwDamage;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") && isInTrigger)
        {
            StartCoroutine(DamageObject(other));
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Enemy")) return;
        isInTrigger = true;
        Damaging(other);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            isInTrigger = false;
        }
    }

    private void Damaging(Collider2D other)
    {
        var enemyScr = other.GetComponent<BaseEnemy>();
        enemyScr.RecountHp(-Player.flamethrowerScr.damage);
        isInTrigger = true;
    }

    private IEnumerator DamageObject(Collider2D other)
    {
        isInTrigger = false;
        timeBtwDamage = startTimeBtwDamage;
        while (timeBtwDamage > 0)
        {
            if (!other.gameObject.activeInHierarchy)
            {
                yield break;
            }

            timeBtwDamage -= Time.deltaTime;
            yield return null;
        }

        Damaging(other);
    }
}