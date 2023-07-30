using UnityEngine;
using static Enemy;

public class CharacterStats : MonoBehaviour
{

    public float maxHealth = 100f;

    public float health { get; private set; }

    public System.Action<float, float> onHealthChanged;

    private void Awake()
    {
        health = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        onHealthChanged?.Invoke(health, maxHealth);

        if (health <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        Debug.LogFormat("{0} died.", gameObject.name);
        GameObject.Find("GameTimer").GetComponent<GameTimer>().StopTimer();
        Destroy(gameObject);
    }
}
