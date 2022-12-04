using UnityEngine;

namespace Components.Enemies
{
    public class MoveToPlayer: MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private bool stalking = true;

        public void StalkingMove()
        {
            transform.position = Vector2.MoveTowards(gameObject.transform.position, Player.playerTransform.position,
                speed * Time.fixedDeltaTime);
        }

        private void FixedUpdate()
        {
            if (stalking)
            {
                StalkingMove();
            }
            else
            {
                OnceTurnedMove();
            }
        }

        private void OnceTurnedMove()
        {
            //повернуться в сторону игрока
            transform.Translate(Vector3.right * (speed * Time.deltaTime));
        }
    }
}