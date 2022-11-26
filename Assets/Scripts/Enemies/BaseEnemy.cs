using System.Collections;
using NTC.Global.Pool;
using UnityEngine;

namespace Enemies
{
    public abstract class BaseEnemy : MonoBehaviour, IPoolItem
    {
        [SerializeField] private float speed; //скорость врага

        [SerializeField] private float maxHp;
        public float curHp;

        [SerializeField] private float xp;

        private bool isInTrigger;

        private const float StartTimeBtwDamage = 0.1f;
        private float timeBtwDamage;

        public float damage;
        [SerializeField] private bool isTeleportToPlayer = true;
        private MoveTrack moveTrackScr;
        private const float DistanceToTeleportFromPlayer = 20f;

        public virtual void Initialization()
        {
            curHp = maxHp;
        }

        public virtual void Move()
        {
            transform.position = Vector2.MoveTowards(gameObject.transform.position, Player.playerTransform.position,
                speed * Time.fixedDeltaTime);
        }

        private void Start()
        {
            Initialization();

            if (isTeleportToPlayer)
            {
                moveTrackScr = Player.playerGameObject.gameObject.GetComponent<MoveTrack>();
            }
        }

        private void FixedUpdate()
        {
            Move();
        }

        public void RecountHp(float deltaHp)
        {
            curHp += deltaHp * Stats.SkillDamageMultiplier;
            if (curHp <= 0)
            {
                Death(); //удаление крипа
            }
        }

        public virtual void Death()
        {
            Player.systemXpScr.RecountXp(xp); // добавление опыта
            NightPool.Despawn(gameObject);
            Stats.EnemyKilled++;

        }

        public void TouchToDeSpawnTor()
        {
            if (isTeleportToPlayer)
            {
                TeleportToNearAreaPlayer();
            }
            else
            {
                DeathFromDeSpawnTor();
            }
        }

        public virtual void DeathFromDeSpawnTor()
        {
            NightPool.Despawn(gameObject);
        }

        private void TeleportToNearAreaPlayer()
        {
            var vectorTrack = moveTrackScr.MovementLogic();
            var rotationZ =
                Mathf.Atan2(vectorTrack.y, vectorTrack.x) * Mathf.Rad2Deg; // считает поворот по Z
            transform.rotation =
                Quaternion.Euler(0f, 0f, rotationZ); // поворачивает объект в сторону куда идет персонаж

            transform.position = Player.playerTransform.position;
            transform.Translate(Vector3.right * DistanceToTeleportFromPlayer);
            transform.rotation = Quaternion.Euler(Vector3.zero);
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

        public void OnSpawn()
        {
            Initialization();
        }

        public void OnDespawn()
        {
            Initialization();
        }
    }
}