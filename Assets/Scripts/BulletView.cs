using System;
using UnityEngine;

namespace DefaultNamespace
{
    public abstract class BulletView : MonoBehaviour
    {
        public abstract event Action OnDeath;
        public abstract void FireAt(Vector3 target);
    }
}
