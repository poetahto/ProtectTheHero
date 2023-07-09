using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class AggressionSource : MonoBehaviour
    {
        private static List<AggressionSource> _allFearObjects;

        public float radius = 2;

        public static bool InAggroRange(Vector3 position, out GameObject nearest)
        {
            foreach (var agroSource in _allFearObjects)
            {
                float distSqr = (agroSource.transform.position - position).sqrMagnitude;

                if (distSqr < agroSource.radius * agroSource.radius)
                {
                    nearest = agroSource.gameObject;
                    return true;
                }
            }

            nearest = null;
            return false;
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void ResetStatics()
        {
            _allFearObjects = new List<AggressionSource>();
        }

        private void OnEnable()
        {
            _allFearObjects.Add(this);
        }

        private void OnDisable()
        {
            _allFearObjects.Remove(this);
        }
    }
}
