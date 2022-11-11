using System.Collections;
using NTC.Global.Pool;
using UnityEngine;

namespace Enemies
{
    public abstract class BaseEnemy : MonoBehaviour
    {
        public float speed; //скорость врага

        [SerializeField] private float maxHp;
        public float curHp;

        [SerializeField] private float xp;

        public bool isInTrigger;

        private const float StartTimeBtwDamage = 0.1f;
        private float timeBtwDamage;

        public float damage;


        public virtual void Initialization()
        {
            curHp = maxHp;
        }

        //движение на игрока
        public virtual void Move()
        {
            transform.position = Vector2.MoveTowards(gameObject.transform.position, Player.playerTransform.position,
                speed * Time.fixedDeltaTime);
        }

        private void Start()
        {
            Initialization();
        }

        private void FixedUpdate()
        {
            Move();
        }

        public void RecountHp(float deltaHp)
        {
            curHp += deltaHp;
            if (curHp <= 0)
            {
                Player.systemXpScr.RecountXp(xp); // добавление опыта 
                Death(); //удаление крипа
            }
        }

        public void Death()
        {
            NightPool.Despawn(gameObject);
            Initialization();
        }

        public virtual void DeathFromDeSpawnTor()
        {
            Death();
        }

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

        protected IEnumerator DamageObject()
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

        protected void DamagingPlayer()
        {
            Player.systemHpScr.RecountHp(-damage);
            isInTrigger = true;
        }

        #endregion
    }
}