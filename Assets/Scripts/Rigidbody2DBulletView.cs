using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace DefaultNamespace
{
    public class Rigidbody2DBulletView : BulletView
    {
        public float damage;
        public float speed;
        public Rigidbody2D body;
        public Collider2D collider;
        public string targetTag;
        public new Renderer renderer;
        public ParticleSystem particles;
        public float lifetime = 2;

        public override event Action OnDeath;

        private Vector3 _originalScale;
        private bool _isDying;

        private void Awake()
        {
            _originalScale = renderer.transform.localScale;
        }

        public override void FireAt(Vector3 target)
        {
            collider.enabled = true;
            renderer.enabled = true;
            renderer.transform.localScale = _originalScale;
            Debug.DrawLine(body.position, target);
            LeanTween.alpha(renderer.gameObject, 1, 0.25f);
            body.velocity = (target - transform.position).normalized * speed;
            LifetimeTask().Forget();
            _isDying = false;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(targetTag) && other.TryGetComponent(out Entity entity))
            {
                entity.Damage(damage);
                DeathRoutine().Forget();
            }
        }

        private async UniTaskVoid LifetimeTask()
        {
            await Task.Delay(TimeSpan.FromSeconds(lifetime));
            await DeathRoutine();
        }

        private async UniTask DeathRoutine()
        {
            if (_isDying)
                return;

            collider.enabled = false;
            _isDying = true;
            particles.Play();
            LeanTween.alpha(renderer.gameObject, 0, 0.25f);
            LeanTween.scale(renderer.gameObject, _originalScale * 1.25f, 0.25f);
            await Task.Delay(TimeSpan.FromSeconds(particles.main.duration));
            renderer.enabled = false;
            OnDeath?.Invoke();
        }
    }
}
