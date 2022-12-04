using UnityEngine;

namespace Components.Enemies
{
    [RequireComponent(typeof(BaseEnemy))]
    public class StalkingMove : MonoBehaviour
    {
        private BaseEnemy enemyScr;

        //[SerializeField] private float speed;

        private void Start()
        {
            enemyScr = GetComponent<BaseEnemy>();
        }

        private void FixedUpdate()
        {
            if (enemyScr.isFrozen) return;
            Move();
        }

        private void Move()
        {
            transform.position = Vector2.MoveTowards(transform.position, Player.playerTransform.position,
                enemyScr.speed * Time.fixedDeltaTime);
        }
    }
}