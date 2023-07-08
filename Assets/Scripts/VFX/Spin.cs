using UnityEngine;

public class Spin : MonoBehaviour
{
    public float speed = 1;

    private void Update()
    {
        transform.Rotate(0, 0, speed * Time.deltaTime);
    }
}
