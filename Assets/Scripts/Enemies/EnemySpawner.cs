using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;   // assign your Enemy prefab
    public float spawnInterval = 2f; // time between spawns
    public float spawnRadius = 10f;  // how far from the player enemies appear

    private float timer;
    private Transform player;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnEnemy();
            timer = 0f;
        }
    }

    void SpawnEnemy()
    {
        if (player == null) return;

        // pick a random spot around the player
        float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
        Vector2 offset = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * spawnRadius;

        Vector3 spawnPos = player.position + new Vector3(offset.x, offset.y, 0f);

        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
    }
}
