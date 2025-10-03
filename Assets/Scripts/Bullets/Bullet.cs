using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 12f;
    public float lifetime = 2f;

    void OnEnable()
    {
        // auto-despawn after a short time so bullets don't live forever
        Invoke(nameof(Despawn), lifetime);
    }

    void OnDisable()
    {
        CancelInvoke();
        
    }

    void Update()
    {
        // Move in the bullet's "up" direction (local Y)
        transform.Translate(Vector2.up * speed * Time.deltaTime, Space.Self);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Simple: kill enemies by tag. You'll replace this with a DamageSystem later.
         if (other.CompareTag("Enemy"))
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(1); // bullet deals 1 dmg
        }

        Despawn();
    }
    }

    private void Despawn()
    {
        gameObject.SetActive(false); // cheap "destroy" for when you add pooling later
        // For now you can use Destroy(gameObject); if you prefer:
        // Destroy(gameObject);
    }
}
