using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace DefaultNamespace
{
    public class EnemyFX : MonoBehaviour
    {
        public Rigidbody2D rb;
        public Collider2D col;
        public SpriteRenderer renderer;
        public ParticleSystem deathParticles;
        public Behaviour[] logic;
        [SerializeField] private int flashDuration;
        [SerializeField] private Color deathColor;
        [SerializeField] private Color damageColor;

        public void PlayDeathEffect()
        {
            DeathEffectTask(gameObject.GetCancellationTokenOnDestroy()).Forget();
        }

        public void PlayDamageEffect()
        {
            DamageEffectTask(gameObject.GetCancellationTokenOnDestroy()).Forget();
        }

        private async UniTaskVoid DamageEffectTask(CancellationToken token)
        {
            var orig = renderer.color;
            renderer.color = damageColor;
            await Task.Delay(flashDuration, token);
            renderer.color = orig;
        }

        private async UniTaskVoid DeathEffectTask(CancellationToken token)
        {
            // todo: death sprite
            foreach (var l in logic)
            {
                l.enabled = false;
            }

            rb.velocity = -rb.velocity.normalized * 5;
            deathParticles.Play();
            LeanTween.color(renderer.gameObject, deathColor, 1.0f);
            await Task.Delay(1000, token);
            col.enabled = false;
            rb.velocity = Vector3.zero;
            rb.isKinematic = true;
        }
    }
}
