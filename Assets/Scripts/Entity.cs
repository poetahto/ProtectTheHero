using UnityEngine;
using UnityEngine.Events;

public class Entity : MonoBehaviour
{
    public AudioClip damageSound;
    public AudioClip deathSound;
    public float health;
    public UnityEvent<float> onDamage;
    public UnityEvent onDeath;

    public void Damage(float amount)
    {
        if (health <= 0)
        {
            return;
        }

        health -= amount;
        onDamage.Invoke(amount);

        if (damageSound != null)
        {
            AudioSource.PlayClipAtPoint(damageSound, transform.position);
        }

        if (health <= 0)
        {
            if (deathSound != null)
            {
                AudioSource.PlayClipAtPoint(deathSound, transform.position);
            }
            onDeath.Invoke();
        }
    }
}
