using UnityEngine;

namespace DefaultNamespace
{
    public class HeroAttackStyleApplier : MonoBehaviour
    {
        public ItemController controller;
        public HeroAttackStyle style;

        private void OnEnable()
        {
            controller.onGrab.AddListener(HandleGrab);
            controller.onThrow.AddListener(HandleThrow);
        }

        private void OnDisable()
        {
            controller.onGrab.RemoveListener(HandleGrab);
            controller.onThrow.RemoveListener(HandleThrow);
        }

        private void HandleGrab(GameObject grabber)
        {
            if (grabber.TryGetComponent(out HeroController hero))
            {
                var instance = Instantiate(style);
                instance.Init(grabber);
                hero.fightingState.style = instance;
            }
        }

        private void HandleThrow(Collider2D thrower, Vector3 velocity)
        {
            if (thrower.TryGetComponent(out HeroController hero))
            {
                hero.fightingState.style.Dispose();
            }
        }
    }
}
