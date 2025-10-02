using UnityEngine;

public class PlayerAutoShoot : MonoBehaviour
{
    [Header("Setup")]
    public GameObject bulletPrefab;   // drag your Bullet prefab here

    [Header("Behavior")]
    public float fireRate = 4f;       // shots per second
    public float shootRadius = 8f;    // only shoot if an enemy is within this radius

    private float _timer;

    void Update()
    {
        _timer += Time.deltaTime;
        float cooldown = 1f / Mathf.Max(0.01f, fireRate);

        if (_timer >= cooldown)
        {
            Transform target = FindClosestEnemyWithin(shootRadius);
            if (target != null)
            {
                // aim at the target
                Vector3 dir = (target.position - transform.position);
                dir.z = 0f;
                if (dir.sqrMagnitude < 0.0001f) dir = Vector3.up;
                dir.Normalize();

                // rotate bullet so its local "up" points at the enemy
                Quaternion rot = Quaternion.FromToRotation(Vector3.up, dir);
                Instantiate(bulletPrefab, transform.position, rot);

                _timer = 0f; // reset only if we actually shot
            }
            // else: no enemy in range → do nothing and keep accumulating time
        }
    }

    Transform FindClosestEnemyWithin(float radius)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Transform best = null;
        float bestDistSqr = radius * radius;
        Vector3 p = transform.position;

        foreach (var e in enemies)
        {
            if (!e.activeInHierarchy) continue;
            float d = (e.transform.position - p).sqrMagnitude;
            if (d < bestDistSqr)
            {
                bestDistSqr = d;
                best = e.transform;
            }
        }
        return best;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, shootRadius);
    }
}
