
using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    public float speed = 7f;
    public Vector2 direction = Vector2.left;

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.linearVelocity = direction * speed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Health health = other.GetComponent<Health>();

            if (health != null)
            {
                health.TakeDamage(1);
            }

            Destroy(gameObject);
        }
    }
}
