using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 3;
    int currentHealth;

    void OnEnable()
    {
        currentHealth = maxHealth; // reset every time enemy spawns
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

void OnTriggerEnter2D(Collider2D other)
{
    if (other.CompareTag("Player"))
    {
        var player = other.GetComponent<PlayerHealth>();
        if (player != null)
        {
            player.TakeDamage(1);
        }
    }
}



    void Die()
    {
        // TODO: add particles / sound later
        Destroy(gameObject); // for now just destroy
    }
}
