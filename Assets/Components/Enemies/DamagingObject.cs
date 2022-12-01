using System.Collections;
using UnityEngine;

namespace Components.Enemies
{
    public class DamagingObject : MonoBehaviour
    {
        [SerializeField] private float damage;
        
        private bool isInTrigger;

        private const float StartTimeBtwDamage = 0.1f;
        private float timeBtwDamage;

        #region PLAYER RECOUNT HP

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                isInTrigger = true;
                DamagingPlayer();
            }
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player") && isInTrigger)
            {
                StartCoroutine(DamageObject());
            }
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                isInTrigger = false;
            }
        }

        private IEnumerator DamageObject()
        {
            isInTrigger = false;

            timeBtwDamage = StartTimeBtwDamage;
            while (timeBtwDamage > 0)
            {
                timeBtwDamage -= Time.deltaTime;
                yield return null;
            }

            DamagingPlayer();
        }

        private void DamagingPlayer()
        {
            Player.playerHealthScr.ApplyDamage(damage);
            isInTrigger = true;
        }

        #endregion

    }
}