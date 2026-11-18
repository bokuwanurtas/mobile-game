using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject explosionPrefab;

    ItemStats stats;

    void Start()
    {
        stats = GetComponent<Hazard>().stats;
        Invoke(nameof(Explode), stats.bombFuseTime);
    }

    void Explode()
    {
        // здесь: анимация взрыва, звук, урон вокруг (урок 9 и A7)
        GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        Destroy(explosion, stats.explosionTime);
        Destroy(gameObject);
    }
}
