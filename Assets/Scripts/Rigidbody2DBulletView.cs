using UnityEngine;

namespace DefaultNamespace
{
    public class Rigidbody2DBulletView : BulletView
    {
        public float damage;
        public float speed;
        public Rigidbody2D body;
        public string targetTag;

        public override void FireAt(Vector2 target)
        {
            body.velocity = (target - body.position).normalized * speed;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(targetTag) && other.TryGetComponent(out Entity entity))
            {
                entity.Damage(damage);
                Destroy(gameObject);
            }
        }
    }
}
