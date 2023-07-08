using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class FearSource : MonoBehaviour
    {
        private static List<FearSource> _allFearObjects;

        public float radius = 2;

        public static bool InFearRange(Vector3 position)
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
            _allFearObjects = new List<FearSource>();
        }

        private void Start()
        {
            _allFearObjects.Add(this);
        }

        private void OnDestroy()
        {
            _allFearObjects.Remove(this);
        }
    }
}
