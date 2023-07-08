using UnityEngine;
using UnityEngine.Events;

public class Entity : MonoBehaviour
{
    public float health;
    public UnityEvent<float> onDamage;

    public void Damage(float amount)
    {
        health -= amount;
        onDamage.Invoke(amount);
    }
}