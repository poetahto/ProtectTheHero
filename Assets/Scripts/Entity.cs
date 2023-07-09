using UnityEngine;
using UnityEngine.Events;

public class Entity : MonoBehaviour
{
    public AudioClip damageSound;
    public float health;
    public UnityEvent<float> onDamage;

    public void Damage(float amount)
    {
        health -= amount;
        onDamage.Invoke(amount);

        if (damageSound != null)
        {
            AudioSource.PlayClipAtPoint(damageSound, transform.position);
        }
    }
}
