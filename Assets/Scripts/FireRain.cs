using System.Collections;
using UnityEngine;

public class FireRain : MonoBehaviour
{
    public GameObject bossBulletPrefab;
    public GameObject warningPrefab;

    public float warningTime = 1.5f;
    public float interval = 4f;

    public float minX = -10.3f;
    public float maxX = 22.5f;
    public float groundY = 5f;
    public float spawnHeight = 8f;

    IEnumerator Start()
    {
        while (true)
        {
            float x = Random.Range(minX, maxX);

            GameObject warning = Instantiate(
                warningPrefab,
                new Vector3(x, groundY, 0),
                Quaternion.identity
            );

            yield return new WaitForSeconds(warningTime);

            Destroy(warning);

            GameObject bullet = Instantiate(
                bossBulletPrefab,
                new Vector3(x, spawnHeight, 0),
                Quaternion.identity
            );

            BossProjectile projectile = bullet.GetComponent<BossProjectile>();

            if (projectile != null)
            {
                projectile.enabled = false;
            }

            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                rb.gravityScale = 0;
                rb.linearVelocity = Vector2.down * 7f;
            }

            yield return new WaitForSeconds(interval);
        }
    }
}
