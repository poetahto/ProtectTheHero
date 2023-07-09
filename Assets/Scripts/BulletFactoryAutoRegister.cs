using UnityEngine;

namespace DefaultNamespace
{
    public class BulletFactoryAutoRegister : MonoBehaviour
    {
        public string id;
        public BulletView prefab;

        private void Awake()
        {
            if (TryGetComponent(out BulletFactory factory))
            {
                factory.Register(id, prefab);
            }
        }
    }
}