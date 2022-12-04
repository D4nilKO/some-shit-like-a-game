using NTC.Global.Pool;
using UnityEngine;

namespace Components.Enemies
{
    public class OnceTurnedToPlayerMove : MonoBehaviour, IPoolItem
    {
        [SerializeField] private float speed;

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
            Move();
        }

        private void Move()
        {
            transform.Translate(Vector3.up * (speed * Time.fixedDeltaTime));
        }
    }
}