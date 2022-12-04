using NTC.Global.Pool;
using UnityEngine;

namespace Components.Enemies
{
    public class NestingObject : MonoBehaviour, IPoolItem
    {
        private Health healthScr;

        private void Awake()
        {
            if (TryGetComponent(out Health healthScr))
            {
                this.healthScr = healthScr;
            }
        }

        public void OnSpawn()
        {
            healthScr.ApplyInvulnerability();
        }

        public void OnDespawn()
        {
            
        }
    }
}