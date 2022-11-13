using System;
using System.Collections;
using UnityEngine;

namespace Enemies
{
    public class HighLvlEnemy : StandardEnemy
    {
        private MoveTrack moveTrackScr;
        private float distance = 20f;

        private void Start()
        {
            moveTrackScr = Player.playerGameObject.gameObject.GetComponent<MoveTrack>();
        }

        public override void DeathFromDeSpawnTor()
        {
            var vectorTrack = moveTrackScr.MovementLogic();
            float rotationZ = 0;
            rotationZ =
                Mathf.Atan2(vectorTrack.y, vectorTrack.x) * Mathf.Rad2Deg; // считает поворот по Z
            transform.rotation =
                Quaternion.Euler(0f, 0f, rotationZ); // поворачивает объект в сторону куда идет персонаж

            transform.position = Player.playerTransform.position;
            transform.Translate(Vector3.right * distance);
            transform.rotation = Quaternion.Euler(Vector3.zero);
        }

        // #region PLAYER RECOUNT HP
        //
        // private void OnTriggerEnter2D(Collider2D other)
        // {
        //     if (other.gameObject.CompareTag("Player"))
        //     {
        //         isInTrigger = true;
        //         DamagingPlayer();
        //     }
        // }
        //
        // private void OnTriggerStay2D(Collider2D other)
        // {
        //     if (other.gameObject.CompareTag("Player") && isInTrigger)
        //     {
        //         StartCoroutine(DamageObject());
        //     }
        // }
        //
        // private void OnTriggerExit2D(Collider2D other)
        // {
        //     if (other.gameObject.CompareTag("Player"))
        //     {
        //         isInTrigger = false;
        //     }
        // }
        //
        // #endregion
    }
}