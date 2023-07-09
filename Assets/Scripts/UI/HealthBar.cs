using UnityEngine;
using Slider = UnityEngine.UI.Slider;

namespace DefaultNamespace.UI
{
    public class HealthBar : MonoBehaviour
    {
        public Entity entity;
        public Slider slider;

        private float _staringHealth;

        private void Awake()
        {
            _staringHealth = entity.health;
        }

        private void Update()
        {
            slider.value = Mathf.Clamp01(entity.health / _staringHealth);
        }
    }
}
