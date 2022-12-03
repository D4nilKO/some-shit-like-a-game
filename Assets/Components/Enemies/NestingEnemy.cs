using System;
using Enemies;

namespace Components.Enemies
{
    public class NestingEnemy : StandardEnemy
    {
        public override void OnSpawn()
        {
            base.OnSpawn();
            healthScr.ApplyInvulnerability();
        }
    }
}