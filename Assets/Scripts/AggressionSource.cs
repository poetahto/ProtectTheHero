using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class AggressionSource : MonoBehaviour
    {
        private static List<AggressionSource> _allFearObjects;

        public float radius = 2;

        public static bool InAggroRange(Vector3 position)
        {
            foreach (var fearObject in _allFearObjects)
            {
                float distSqr = (fearObject.transform.position - position).sqrMagnitude;

                if (distSqr < fearObject.radius * fearObject.radius)
                {
                    return true;
                }
            }

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
