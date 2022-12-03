using System;
using NTC.Global.Pool;
using UnityEngine;
using UnityEngine.Serialization;
using static NTC.Global.Pool.NightPool;

namespace Components.Enemies
{
    [RequireComponent(typeof(EnemyHealth))]
    public abstract class BaseEnemy : MonoBehaviour, IPoolItem
    {
        public Health healthScr;
        //[SerializeField] private Animation damagingAnimation;

        [FormerlySerializedAs("xp")] [SerializeField] private float experience;
        [SerializeField] private float speed; //скорость врага

        [SerializeField] private bool isTeleportToPlayer = true;

        private const float DistanceToTeleportFromPlayer = 25f;
        private MoveTrack moveTrackScr;

        public virtual void Awake()
        {
            healthScr.DamageApplied += OnDamageEvent;
        }

        private void OnDestroy()
        {
            healthScr.DamageApplied -= OnDamageEvent;
        }

        private void Start()
        {
            Initialization();

            if (isTeleportToPlayer)
            {
                moveTrackScr = Player.playerGameObject.gameObject.GetComponent<MoveTrack>();
            }
        }

        public virtual void Initialization()
        {
            if (TryGetComponent (out EnemyHealth healthScr))
            {
                this.healthScr = healthScr;
                healthScr.UpdateHealthToMax(); 
            }
        }

        public virtual void Move()
        {
            transform.position = Vector2.MoveTowards(gameObject.transform.position, Player.playerTransform.position,
                speed * Time.fixedDeltaTime);
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void OnDamageEvent()
        {
            //место для анимации
            //damagingAnimation.Play();
        }

        public virtual void Death()
        {
            Player.experienceScr.AddExperience(experience);
            Despawn(gameObject);
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
            Despawn(gameObject);
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
        
        public virtual void OnSpawn()
        {
            Initialization();
        }

        public virtual void OnDespawn()
        {
            Initialization();
        }
    }
}