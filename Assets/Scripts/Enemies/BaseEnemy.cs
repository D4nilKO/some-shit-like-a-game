using System.Collections;
using NTC.Global.Pool;
using UnityEngine;

namespace Enemies
{
    public abstract class BaseEnemy : MonoBehaviour
    {
        [SerializeField]private float speed; //скорость врага

        [SerializeField] private float maxHp;
        public float curHp;

        [SerializeField] private float xp;

        private bool isInTrigger;

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
                Death(); //удаление крипа
            }
        }

        public void Death()
        {
            Player.systemXpScr.RecountXp(xp); // добавление опыта
            NightPool.Despawn(gameObject);
            Stats.EnemyKilled++;
            
            Initialization();
        }

        public virtual void DeathFromDeSpawnTor()
        {
            NightPool.Despawn(gameObject);
            Initialization();
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