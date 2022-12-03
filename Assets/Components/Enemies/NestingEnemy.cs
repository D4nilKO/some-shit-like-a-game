using System;
using Enemies;

namespace Components.Enemies
{
    public class NestingEnemy : StandardEnemy
    {
        public override void Awake()
        {
            //healthScr.ApplyInvulnerability();
            StartCoroutine(healthScr.Invulnerability());
            base.Awake();
        }

        private void Update()
        {
            
        }
    }
}