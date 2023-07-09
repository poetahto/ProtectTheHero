using UnityEngine;

namespace DefaultNamespace
{
    public class HeroHealingItem : MonoBehaviour
    {
        public float healing = 3.0f;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out HeroController hero))
            {
                hero.heroEntity.health += healing;
                Destroy(gameObject);
            }
        }
    }
}
