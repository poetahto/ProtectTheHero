using UnityEngine;

namespace DefaultNamespace
{
    public class ColorApplier : MonoBehaviour
    {
        public Color color = Color.white;

        private void Start()
        {
            foreach (var spriteRenderer in GetComponentsInChildren<SpriteRenderer>())
            {
                spriteRenderer.color = color;
            }

            foreach (var spriteRenderer in GetComponentsInChildren<ParticleSystem>())
            {
                var main = spriteRenderer.main;
                main.startColor = color;
            }
        }
    }
}
