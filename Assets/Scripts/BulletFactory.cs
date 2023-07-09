using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace DefaultNamespace
{
    public class BulletFactory : MonoBehaviour
    {
        public static BulletFactory Instance { get; set; }

        private Dictionary<string, ObjectPool<BulletView>> _pools
            = new Dictionary<string, ObjectPool<BulletView>>();

        public void Register(string id, BulletView prefab)
        {
            if (!_pools.ContainsKey(id))
            {
                _pools.Add(id, new ObjectPool<BulletView>(() =>
                {
                    var instance = Instantiate(prefab, transform);
                    instance.OnDeath += () => _pools[id].Release(instance);
                    return instance;
                }));
            }
        }

        public BulletView CreateBullet(string id, Vector2 position)
        {
            return CreateBullet(id, position, Quaternion.identity);
        }

        public BulletView CreateBullet(string id, Vector2 position, Quaternion rotation)
        {
            var instance = _pools[id].Get();
            instance.transform.SetPositionAndRotation(position, rotation);
            return instance;
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void ResetStatics()
        {
            Instance = null;
        }

        private void Awake()
        {
            Instance = this;
        }
    }
}
