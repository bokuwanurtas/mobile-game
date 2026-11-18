using UnityEngine;

public class Bullet : MonoBehaviour
{
    public PlayerStats stats;   // ставит Shooter при выстреле

    void Start()
    {
        GetComponent<Rigidbody2D>().linearVelocity = transform.right * stats.bulletSpeed;
        Destroy(gameObject, stats.bulletLifetime);      // пуля сама умрёт через 3 секунды
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        BossHealth boss = other.GetComponent<BossHealth>();

        if (boss != null)
        {
            boss.TakeDamage(stats.bulletDamage);
            Destroy(gameObject);
            return;
        }

        EnemyHealth enemy = other.GetComponent<EnemyHealth>();

        if (enemy != null)
        {
            enemy.TakeDamage(stats.bulletDamage);
            Destroy(gameObject);
        }
    }
}
