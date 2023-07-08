using UnityEngine;

public class Scale : MonoBehaviour
{
    public float speed = 1;
    public float amount = 0.1f;

    private Vector3 _originalScale;

    private void Start()
    {
        _originalScale = transform.localScale;
    }

    private void Update()
    {
        float t = (Mathf.Sin(Time.time * speed) + 1) / 2 * amount;
        transform.localScale = _originalScale + _originalScale * t;
    }
}
