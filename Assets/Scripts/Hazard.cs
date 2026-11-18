using UnityEngine;

public class Hazard : MonoBehaviour
{
    public ItemStats stats;   // ItemStats.asset

    void Start()
    {
        Destroy(gameObject, stats.hazardLifetime);
    }
    void OnCollisionEnter2D(Collision2D c)
    {
        if (!c.gameObject.CompareTag("Player")) return;

        OnHitPlayer(c.gameObject.GetComponent<Health>());
        Destroy(gameObject);
    }

    protected virtual void OnHitPlayer(Health player)
    {
        player.TakeDamage(stats.hazardDamage);
    }
}
