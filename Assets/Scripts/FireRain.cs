using System.Collections;
using UnityEngine;

public class FireRain : MonoBehaviour
{
    public GameObject bossBulletPrefab;
    public GameObject warningPrefab;

    public BossStats stats;   // BossStats.asset

    IEnumerator Start()
    {
        while (true)
        {
            float x = Random.Range(stats.fireMinX, stats.fireMaxX);

            GameObject warning = Instantiate(
                warningPrefab,
                new Vector3(x, stats.fireGroundY, 0),
                Quaternion.identity
            );

            yield return new WaitForSeconds(stats.fireWarningTime);

            Destroy(warning);

            GameObject bullet = Instantiate(
                bossBulletPrefab,
                new Vector3(x, stats.fireSpawnHeight, 0),
                Quaternion.identity
            );

            BossProjectile projectile = bullet.GetComponent<BossProjectile>();

            if (projectile != null)
            {
                projectile.damage = stats.bulletDamage;
                projectile.enabled = false;
            }

            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                rb.gravityScale = 0;
                rb.linearVelocity = Vector2.down * stats.fireFallSpeed;
            }

            yield return new WaitForSeconds(stats.fireInterval);
        }
    }
}
