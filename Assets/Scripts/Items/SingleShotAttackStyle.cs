using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu]
    public class SingleShotAttackStyle : HeroAttackStyle
    {
        public float fireRate;
        public string bulletId = "hero";

        private float _fireCooldown;

        public override void OnLogic()
        {
            if (_fireCooldown <= 0 && AggressionSource.InAggroRange(Holder.transform.position, out GameObject nearest))
            {
                var bulletInstance = BulletFactory.Instance.CreateBullet(bulletId, Holder.transform.position);
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
