using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject bulletPrefab;   // drag Bullet prefab here
    public float fireCooldown = 0.2f;

    float timer;

    void Update()
    {
        timer -= Time.deltaTime;

        // Hold Space to auto-fire
        if (Input.GetKey(KeyCode.Space) && timer <= 0f)
        {
            FireAtClosestEnemy();
            timer = fireCooldown;
        }
    }

    void FireAtClosestEnemy()
    {
        Transform target = FindClosestEnemy();
        Vector3 aimDir;

        if (target == null)
        {
            // No enemies? Just shoot up so you can still test.
            aimDir = Vector3.up;
        }
        else
        {
            aimDir = (target.position - transform.position);
            aimDir.z = 0f;
            if (aimDir.sqrMagnitude < 0.0001f) aimDir = Vector3.up;
            aimDir.Normalize();
        }

        // Rotate bullet so its local "up" faces aimDir
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, aimDir);
        Instantiate(bulletPrefab, transform.position, rot);
    }

    Transform FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Transform best = null;
        float bestDistSqr = float.PositiveInfinity;
        Vector3 p = transform.position;

        foreach (var e in enemies)
        {
            float d = (e.transform.position - p).sqrMagnitude;
            if (d < bestDistSqr)
            {
                bestDistSqr = d;
                best = e.transform;
            }
        }
        return best;
    }
}
