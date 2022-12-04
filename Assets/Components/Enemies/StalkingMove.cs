using System;
using UnityEngine;

namespace Components.Enemies
{
    public class StalkingMove : MonoBehaviour
    {
        [SerializeField] private float speed;

        private void FixedUpdate()
        {
            Move();
        }
        
        private void Move()
        {
            transform.position = Vector2.MoveTowards(transform.position, Player.playerTransform.position,
                speed * Time.fixedDeltaTime);
        }
    }
}