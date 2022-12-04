using NTC.Global.Pool;
using UnityEngine;

namespace Components.Enemies
{
    [RequireComponent(typeof(BaseEnemy))]
    public class OnceTurnedToPlayerMove : MonoBehaviour, IPoolItem
    {
        private BaseEnemy enemyScr;
        
        //[SerializeField] private float speed;

        private void Start()
        {
            enemyScr = GetComponent<BaseEnemy>();
        }

        public void OnSpawn()
        {
            transform.rotation = Quaternion.LookRotation(Vector3.forward,
                Player.playerTransform.position - transform.position);
        }

        public void OnDespawn()
        {
        }

        private void FixedUpdate()
        {
            if (enemyScr.isFrozen) return;
            Move();
        }

        private void Move()
        {
            transform.Translate(Vector3.up * (enemyScr.speed * Time.fixedDeltaTime));
        }
    }
}