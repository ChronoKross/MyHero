using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    public float speed = 2f;       // how fast the enemy moves
    private Transform player;      // a reference to your Player’s position

    void Start()
    {
        // Find the Player in the scene by its Tag
        player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        if (player != null)
        {
            // Move the enemy a little closer to the Player every frame
            transform.position = Vector2.MoveTowards(
                transform.position,       // enemy’s current position
                player.position,          // target = player’s position
                speed * Time.deltaTime    // distance to move this frame
            );
        }
    }
}
