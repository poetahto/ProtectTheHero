using UnityEngine;
using UnityEngine.Pool;

namespace DefaultNamespace
{
    public class BulletFactory : MonoBehaviour
    {
        public BulletView prefab;

        private ObjectPool<BulletView> _pool;

        private void Start()
        {
            _pool = new ObjectPool<BulletView>(() =>
            {
                var instance = Instantiate(prefab, transform);
                instance.OnDeath += () => _pool.Release(instance);
                return instance;
            });
        }

        public BulletView CreateBullet(Vector2 position)
        {
            return CreateBullet(position, Quaternion.identity);
        }

        public BulletView CreateBullet(Vector2 position, Quaternion rotation)
        {
            var instance = _pool.Get();
            instance.transform.SetPositionAndRotation(position, rotation);
            return instance;
        }
    }
}
