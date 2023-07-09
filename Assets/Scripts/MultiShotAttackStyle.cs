using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu]
    public class MultiShotAttackStyle : HeroAttackStyle
    {
        public AudioClip audio;
        public float volume = 1;
        public int count = 3;
        public float spread = 15;
        public float fireRate;
        public string bulletId = "hero";

        private float _fireCooldown;

        public override void OnLogic()
        {
            if (_fireCooldown <= 0 && AggressionSource.InAggroRange(Holder.transform.position, out GameObject nearest))
            {
                AudioSource.PlayClipAtPoint(audio, Holder.transform.position, volume);
                float startingAngle = spread * (count - 1) / -2;
                Vector3 vectorToTarget = nearest.transform.position - Holder.transform.position;

                for (int i = 0; i < count; i++)
                {
                    Vector3 rotatedVector = Quaternion.AngleAxis(startingAngle, Vector3.forward) * vectorToTarget;
                    var bulletInstance = BulletFactory.Instance.CreateBullet(bulletId, Holder.transform.position);
                    bulletInstance.FireAt(Holder.transform.position + rotatedVector);
                    startingAngle += spread;
                }
                _fireCooldown = fireRate;
            }
            else
            {
                _fireCooldown -= Time.deltaTime;
            }
        }
    }
}
