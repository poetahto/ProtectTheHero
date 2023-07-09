using System;
using FSM;
using UnityEngine;

namespace DefaultNamespace
{
    [Serializable]
    public class HeroFightingState : StateBase
    {
        public BulletFactory bullet;
        public float fireRate;
        public Transform transform;

        private float _fireCooldown;

        public HeroFightingState() : base(needsExitTime: false)
        {
        }

        public override void OnLogic()
        {
            if (_fireCooldown <= 0 && AggressionSource.InAggroRange(transform.position, out GameObject nearest))
            {
                var bulletInstance = bullet.CreateBullet(transform.position);
                bulletInstance.FireAt(nearest.transform.position);
                _fireCooldown = fireRate;
            }
            else
            {
                _fireCooldown -= Time.deltaTime;
            }
        }
    }
}
