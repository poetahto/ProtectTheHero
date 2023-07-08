using UnityEngine;

namespace DefaultNamespace
{
    public class SmoothFollow : MonoBehaviour
    {
        public Transform target;
        public float speed = 5;

        private void Update()
        {
            Transform t = transform;
            Vector3 startPos = t.position;
            Vector3 newPos = Vector3.Lerp(startPos, target.position, speed * Time.deltaTime);
            newPos.z = startPos.z;
            t.position = newPos;
        }
    }
}
